﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GTI.Modules.Shared;
using GTI.Modules.Shared.Data;
using System.Web.UI.WebControls;
using CardPositionMapHandle = GTI.Modules.Shared.Business.CardPositionMapHandle;

namespace GTI.Modules.ProductCenter.UI.ProductPackage
{
    public partial class StarCardPositionMapsForm : GradientForm
    {
        private List<CardPositionMapHandle> m_starPositionMaps;
        List<StarCodeInfo> m_starCodeInfo;
        private int m_maxCardPositions = 0;

        private List<StarCount> m_starCounts = new List<StarCount>();

        #region Public Properties

        public CardPositionMapHandle SelectedStarPositionMap
        {
            get { return starPositionMapsSelector.SelectedItem as CardPositionMapHandle; }
        }

        public List<StarCount> StarCounts { get { return m_starCounts; } }

        #endregion

        public StarCardPositionMapsForm(int starPositionMapID, SortedList<byte, byte> positionStarCodes)
        {
            InitializeComponent();

            starCountsLst.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.None);
            starCountsLst.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.None);

            LoadData(starPositionMapID);

            byte prevCode = 0;
            byte currentCount = 0;

            if (positionStarCodes != null)
            {
                foreach (var kvp in positionStarCodes)
                {
                    var currentCode = kvp.Value;
                    if (currentCode != prevCode)
                    {
                        if (currentCount > 0)
                        {
                            var prevCodeInfo = m_starCodeInfo.FirstOrDefault((c) => c.Code == prevCode);
                            if (prevCodeInfo == null)
                                prevCodeInfo = new StarCodeInfo()
                                {
                                    Code = prevCode,
                                    Name = String.Format("[Code {0}]", prevCode)
                                };
                            AddStarCount(new StarCount()
                            {
                                StarCode = prevCodeInfo,
                                Count = currentCount
                            });
                        }

                        currentCount = 1;
                        prevCode = currentCode;
                    }
                    else
                        currentCount++;
                }
            }

            if(currentCount > 0)
            {
                var prevCodeInfo = m_starCodeInfo.FirstOrDefault((c) => c.Code == prevCode);
                if(prevCodeInfo == null)
                    prevCodeInfo = new StarCodeInfo() { Code = prevCode, Name = String.Format("[Code {0}]", prevCode) };
                AddStarCount(new StarCount() { StarCode = prevCodeInfo, Count = currentCount });
            }
        }

        private void LoadData(int initStarPositionMapId = 0)
        {
            m_starCodeInfo = GetStarCodeInfoMessage.GetStarCodeInfo();

            starCodeSelector.SuspendLayout();
            starCodeSelector.DisplayMember = "Name";
            starCodeSelector.ValueMember = "Code";
            starCodeSelector.Items.Clear();
            foreach(var ci in m_starCodeInfo)
                starCodeSelector.Items.Add(ci);
            starCodeSelector.ResumeLayout();

            m_starPositionMaps = GetCardPositionMapsMessage.GetPositionMaps();

            starPositionMapsSelector.SuspendLayout();
            starPositionMapsSelector.DisplayMember = "PositionMapName";
            starPositionMapsSelector.ValueMember = "Id";
            starPositionMapsSelector.Items.Clear();
            starPositionMapsSelector.SelectedItem = null;
            foreach(var item in m_starPositionMaps)
                if(item.IsActive || item.Id == initStarPositionMapId)
                    starPositionMapsSelector.Items.Add(item);

            foreach(var i in starPositionMapsSelector.Items)
            {
                var pm = i as CardPositionMapHandle;
                if(pm.Id == initStarPositionMapId)
                {
                    starPositionMapsSelector.SelectedItem = i;
                    break;
                }
            }
            if(starPositionMapsSelector.SelectedItem == null && starPositionMapsSelector.Items.Count == 1)
                starPositionMapsSelector.SelectedItem = starPositionMapsSelector.Items[0];
            starPositionMapsSelector.ResumeLayout();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if(starPositionMapsSelector.SelectedIndex == -1)
            {
                MessageBox.Show("A Star definition must be selected.", "Invalid Specification");
                starPositionMapsSelector.Focus();
                return;
            }

            if(m_starCounts.Sum(i => i.Count) < 1)
            {
                MessageBox.Show("At least one Star must be defined.", "Invalid Specification");
                starCodeSelector.Focus();
                return;
            }

            if (m_starCounts.Sum(i => i.Count) > m_maxCardPositions)
            {
                MessageBox.Show("No more than "+m_maxCardPositions.ToString()+" Star"+(m_maxCardPositions == 1? "" : "s")+" may be defined.", "Invalid Specification");
                starCodeSelector.Focus();
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            var lvis = new List<ListViewItem>();
            foreach(ListViewItem lvi in starCountsLst.SelectedItems)
                lvis.Add(lvi);
            foreach(var lvi in lvis)
            {
                starCountsLst.Items.Remove(lvi);
                m_starCounts.Remove(lvi.Tag as StarCount);
            }

            UpdateStarsTotal();
        }

        private void starPositionMapsSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            //find the max stars for the card
            if (starPositionMapsSelector.SelectedIndex == -1)
            {
                m_maxCardPositions = 0;
                lblStarsPerCard.Hide();
            }
            else
            {
                m_maxCardPositions = ((CardPositionMapHandle)starPositionMapsSelector.SelectedItem).SequenceLength;
                lblStarsPerCard.Text = "Stars per card: 1" + (m_maxCardPositions > 1 ? " to " + m_maxCardPositions.ToString() : "");
                lblStarsPerCard.Show();
            }

            UpdateStarsTotal();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            var codeInfo = starCodeSelector.SelectedItem as StarCodeInfo;
            if(codeInfo == null)
            {
                MessageBox.Show("A star type must be selected", "Incomplete Specification");
                return;
            }

            var count = (int)txtStarCount.NumericTextBoxValue;
            if(count > byte.MaxValue || count <= 0)
            {
                MessageBox.Show("Star count must be less than 255", "Invalid Specification");
                return;
            }

            var sc = new StarCount() { StarCode = codeInfo, Count = (byte)count };
            AddStarCount(sc);

            txtStarCount.Text = "1";
        }

        private void AddStarCount(StarCount cc, int atIndex = -1, bool selected = false)
        {
            if(atIndex == -1)
                m_starCounts.Add(cc);
            else
                m_starCounts.Insert(atIndex, cc);

            ListViewItem lvi;
            if(atIndex == -1)
                lvi = starCountsLst.Items.Add(cc.StarCode.Name);
            else
                lvi = starCountsLst.Items.Insert(atIndex, cc.StarCode.Name);
            lvi.SubItems.Add(cc.Count.ToString());
            lvi.Tag = cc;
            if(selected)
                lvi.Selected = true;

            UpdateStarsTotal();
        }

        private void UpdateStarsTotal()
        {
            int stars = m_starCounts.Sum(i => i.Count);

            if (stars == 0)
            {
                lblStarTotalText.Hide();
                lblTotalStarsOnCard.Hide();
            }
            else
            {
                lblStarTotalText.Show();
                lblTotalStarsOnCard.Text = stars.ToString();

                if (stars > m_maxCardPositions)
                    lblTotalStarsOnCard.ForeColor = Color.Red;
                else
                    lblTotalStarsOnCard.ForeColor = Color.Black;

                lblTotalStarsOnCard.Show();
            }
        }

        private void starCountsLst_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var selectionCount = starCountsLst.SelectedItems.Count;
            var i = selectionCount == 1 ? starCountsLst.SelectedIndices[0] : -1;
            removeBtn.Enabled = selectionCount != 0;
            starCountPriorityIncreaseBtn.Enabled = selectionCount == 1 && i > 0;
            starCountPriorityDecreaseBtn.Enabled = selectionCount == 1 && i < (starCountsLst.Items.Count - 1);
        }

        private void starCountsLst_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = starCountsLst.Columns[e.ColumnIndex].Width;
        }

        private void starCountPriorityIncreaseBtn_Click(object sender, EventArgs e)
        {
            var i = starCountsLst.SelectedIndices[0];
            var sc = starCountsLst.Items[i].Tag as StarCount;

            starCountsLst.Items.RemoveAt(i);
            m_starCounts.RemoveAt(i);
            AddStarCount(sc, i - 1, true);
            starCountsLst.Focus();
        }

        private void starCountPriorityDecreaseBtn_Click(object sender, EventArgs e)
        {
            var i = starCountsLst.SelectedIndices[0];
            var sc = starCountsLst.Items[i].Tag as StarCount;

            starCountsLst.Items.RemoveAt(i);
            m_starCounts.RemoveAt(i);
            AddStarCount(sc, i + 1, true);
            starCountsLst.Focus();
        }

        public class StarCount
        {
            public StarCodeInfo StarCode;
            public byte Count;
        }
    }
}
