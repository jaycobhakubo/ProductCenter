// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2007 GameTech
// International, Inc.

using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.Properties;
using GTI.Modules.ProductCenter.Data;
using GTI.Modules.Shared.Data;
using CardColorSet = GameTech.Elite.Base.CardColorSet;
using CardColorSetColor = GameTech.Elite.Base.CardColorSetColor;
using NIntValueChangedArg = GameTech.Elite.Base.Support.ValueChangeEventArg<System.Int32?>;
using System.Collections.ObjectModel;

namespace GTI.Modules.ProductCenter.UI
{
    public partial class CardColorSetManagementForm : GradientForm
    {
        private const float c_foreColorFlipThreshold = 0.6f;

        private static Color GetForeground(Color backgroundColor)
        {
            //return (backgroundColor != Color.Transparent && backgroundColor.GetBrightness() < c_foreColorFlipThreshold) ? Color.White : Color.Black;


            // Counting the perceptive luminance - human eye favors green color... 
            double luminance = (0.299 * backgroundColor.R + 0.587 * backgroundColor.G + 0.114 * backgroundColor.B) / 255;
            return luminance > 0.5 ? Color.Black : Color.White;
        }

        private class CardColorSetEvaluation
        {
            public bool isNormal = true;
            public int? pageCount = null;
            public int? rowCount = null;
            public int? columnCount = null;
        }

        #region Events
        public event EventHandler EditModeChanged;
        public event EventHandler<NIntValueChangedArg> CurrentPageCountChanged;
        public event EventHandler<NIntValueChangedArg> CurrentRowCountChanged;
        public event EventHandler<NIntValueChangedArg> CurrentColumnCountChanged;

        protected virtual void OnEditModeChanged()
        {
            var h = EditModeChanged;
            if(h != null)
                h(this, null);
        }

        protected virtual void OnCurrentPageCountChanged(Int32? oldVal, Int32? newVal)
        {
            var h = CurrentPageCountChanged;
            if(h != null)
                h(this, new NIntValueChangedArg(oldVal, newVal));
        }

        protected virtual void OnCurrentRowCountChanged(Int32? oldVal, Int32? newVal)
        {
            var h = CurrentRowCountChanged;
            if(h != null)
                h(this, new NIntValueChangedArg(oldVal, newVal));
        }

        protected virtual void OnCurrentColumnCountChanged(Int32? oldVal, Int32? newVal)
        {
            var h = CurrentColumnCountChanged;
            if(h != null)
                h(this, new NIntValueChangedArg(oldVal, newVal));
        }
        #endregion Events

        #region Declarations
        private bool m_editMode = false;
        private List<CardColorSet> m_cardColorSets;
        private static readonly CardColorSet s_cardColorSetNonSelection = CardColorSet.GetNoSet("[Select a Card Color Set]");
        private CardColorSet m_currentCardColorSet;
        private List<PaletteColor> m_userColors = new List<PaletteColor>();
        private ObservableCollection<PaletteColor> m_palette;
        private ReadOnlyObservableCollection<PaletteColor> m_paletteRO;
        private int? m_currentPageCount = null;
        private int? m_currentRowCount = null;
        private int? m_currentColCount = null;
        private bool m_loadingDetails = false;
        private List<ProductItemList> m_productItems;
        private readonly object m_productsLoadingLock = new object();
        private bool m_productsLoading;

        #endregion

        public CardColorSetManagementForm()
        {
            InitializeComponent();
            m_palette = new ObservableCollection<PaletteColor>();
            m_paletteRO = new ReadOnlyObservableCollection<PaletteColor>(m_palette);
            cardColorSetCombo.MouseWheel += combobox_MouseWheel;
        }

        void combobox_MouseWheel(object sender, MouseEventArgs e)
        {
            var combo = sender as ComboBox;
            var eH = e as HandledMouseEventArgs;
            if(!combo.DroppedDown)
                eH.Handled = true;
        }

        #region Member Methods
        private void this_Activated(object sender, EventArgs e)
        {
            mainGB.Visible = false;
            LoadData();
            mainGB.Visible = true;
            var str = String.Format("Form Activated: {0}", DateTime.Now);
            System.Diagnostics.Debug.WriteLine(str);
        }

        private void this_Deactivate(object sender, EventArgs e)
        {
            var str = String.Format("Form Deactivated: {0}", DateTime.Now);
            System.Diagnostics.Debug.WriteLine(str);
        }

        private void cardColorSetCombo_SelectedValueChanged(object sender, EventArgs e) { CurrentSet = cardColorSetCombo.SelectedItem as CardColorSet; }

        private void editSetBtn_Click(object sender, EventArgs e) { BeginEdit(); }

        private void copySetBtn_Click(object sender, EventArgs e)
        {
            var copy = new CardColorSet(0, "Copy of " + CurrentSet.Name, CurrentSet.Colors);
            cardColorSetCombo.Items.Add(copy);
            cardColorSetCombo.SelectedItem = copy;
            BeginEdit();
        }

        private void createSetBtn_Click(object sender, EventArgs e)
        {
            var candidate = new CardColorSet(0, "New Card Color Set");
            cardColorSetCombo.Items.Add(candidate);
            cardColorSetCombo.SelectedItem = candidate;
            BeginEdit();
        }

        private void cancelBtn_Click(object sender, EventArgs e) { CancelEdit(); }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            LoadDetails(true);
        }

        private void saveChangesBtn_Click(object sender, EventArgs e) { ApplyChanges(); }

        private void addColorBtn_Click(object sender, EventArgs e)
        {
            var pc = PaletteColor.Random("New Color");
            AddPaletteColor(pc, true);
        }

        private void LoadData()
        {
            mainGB.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                m_cardColorSets = GameTech.Elite.Client.GetCardColorSetData.GetCardColorSets().OrderBy((cs) => cs.Name).ToList();
                LoadProductsAsync();

                ShowAvailableColors();

                cardColorSetCombo.BeginUpdate();
                cardColorSetCombo.DisplayMember = "Name";
                cardColorSetCombo.ValueMember = "Id";
                cardColorSetCombo.Items.Clear();
                cardColorSetCombo.Items.Add(s_cardColorSetNonSelection);
                if(m_cardColorSets != null)
                    cardColorSetCombo.Items.AddRange(m_cardColorSets.ToArray());
                cardColorSetCombo.SelectedItem = s_cardColorSetNonSelection;
                cardColorSetCombo.EndUpdate();

                mainGB.Enabled = true;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void LoadProductsAsync()
        {
            productLoaderBGW.DoWork += productLoaderBGW_DoWork;
            productLoaderBGW.RunWorkerCompleted += productLoaderBGW_RunWorkerCompleted;
            productLoaderBGW.RunWorkerAsync();
        }

        void productLoaderBGW_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                m_productsLoading = true;
                lock(m_productsLoadingLock)
                {
                    m_productItems = GetProductItemMessage.GetProductItems(0);
                    m_productItems.RemoveAll((p) => !p.BarcodedPaper);
                }
                m_productsLoading = false;
                e.Result = true;
            }
            catch
            {
                m_productItems = null;
                m_productsLoading = false;
                e.Result = false;
            }
        }

        void productLoaderBGW_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if(CurrentSet != null)
                ShowProducts();
        }

        private void ShowAvailableColors()
        {
            m_palette.Clear();
            var palleteColors = (from s in m_cardColorSets
                                 from c in s.Colors
                                 select new PaletteColor(c.Name, c.UserColorCode ?? c.DefaultColorCode)
                                 ).Distinct(PaletteColorComparer.Default).OrderBy((pc) => pc.Name).ToList();

            var ucs = (from pc in m_userColors
                       select pc
                       ).Distinct(PaletteColorComparer.Default).OrderBy((pc) => pc.Name).ToList();

            foreach(var uc in ucs)
                if(!palleteColors.Exists(pc => pc.Name == uc.Name && pc.ColorValue == uc.ColorValue))
                    palleteColors.Add(uc);

            colorPaletteDGV.Rows.Clear();
            colorPaletteDGV.Columns.Clear();
            colorPaletteDGV.SuspendLayout();
            var nameCol = new DataGridViewTextBoxColumn();
            nameCol.HeaderText = "Name";
            nameCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            nameCol.MaxInputLength = 64;
            var colorCol = new DataGridViewTextBoxColumn();
            colorCol.HeaderText = "";
            colorCol.ReadOnly = true;
            colorCol.Width = 25;
            colorCol.MinimumWidth = colorCol.Width;

            colorPaletteDGV.Columns.Add(colorCol);
            colorPaletteDGV.Columns.Add(nameCol);

            positionColorCMS.Items.Clear();

            foreach(var pc in palleteColors)
            {
                m_palette.Add(pc);
                pc.ColorChanged += paletteColor_ColorChanged;
                pc.NameChanged += paletteColor_NameChanged;

                var r = colorPaletteDGV.Rows.Add();
                SetRowPaletteColor(colorPaletteDGV.Rows[r], pc);

                SetCMSPaletteColor(pc, positionColorCMS.Items.Count);
            }
            colorPaletteDGV.ResumeLayout();
        }

        private void SetCMSPaletteColor(PaletteColor pc, int atIndex = 0)
        {
            var tsi = new ToolStripMenuItem() { Tag = pc };
            tsi.Click += colorTSI_Click;
            tsi.Paint += colorTSI_Paint;
            positionColorCMS.Items.Insert(atIndex, tsi);
        }

        void paletteColor_NameChanged(object sender, EventArgs e) { pagesTC.Refresh(); }

        void paletteColor_ColorChanged(object sender, EventArgs e) { pagesTC.Refresh(); }

        void colorTSI_Click(object sender, EventArgs e)
        {
            var tsi = sender as ToolStripItem;
            var cms = tsi.Owner as ContextMenuStrip;
            var colorPositionCtl = cms.SourceControl;
            colorPositionCtl.Tag = tsi.Tag as PaletteColor;
            colorPositionCtl.Refresh();
        }

        void colorTSI_Paint(object sender, PaintEventArgs e)
        {
            var tsi = sender as ToolStripItem;
            var pc = tsi.Tag as PaletteColor;
            tsi.Text = pc == null ? "Huh, weird..." : pc.Name;
            tsi.BackColor = pc == null ? Color.Transparent : pc.Color;
            tsi.ForeColor = GetForeground(tsi.BackColor);
        }

        private void LoadDetails(bool applyOriginalColors)
        {
            detailPnl.Visible = false;
            m_loadingDetails = true;
            if(CurrentSet != null && CurrentSet.Id != CardColorSet.NoSet.Id)
            {
                ShowProducts();

                var eval = SetIsNormal(CurrentSet);
                if(eval.isNormal)
                {
                    CurrentPageCount = eval.pageCount;
                    CurrentRowCount = eval.rowCount;
                    CurrentColumnCount = eval.columnCount;

                    nameTxt.Text = CurrentSet.Name;

                    detailPnl.Visible = true;
                }
                UpdateColorPages(applyOriginalColors, true);
            }
            m_loadingDetails = false;
        }

        private void ShowProducts()
        {
            associatedProductsTxt.Clear();
            if(m_productsLoading)
                associatedProductsTxt.Text = "[Loading...]";
            else
            {
                if(CurrentSet != null && m_productItems != null)
                {
                    List<string> prods;
                    lock(m_productsLoadingLock)
                        prods = (from p in m_productItems
                                 where p.CardColorSetId == CurrentSet.Id
                                 select p.ProductItemName).ToList();
                    prods.Sort();
                    associatedProductsTxt.Lines = prods.ToArray();
                }
            }
        }

        private void UpdateColorPages(bool applyOriginalColors, bool force = false)
        {
            if(m_loadingDetails && !force)
                return;

            pagesTC.SuspendLayout();
            Int32 actualPageCount = CurrentPageCount ?? 1;
            for(int p = 0; p < actualPageCount; ++p)
            {
                TabPage tp = null;
                if(p >= pagesTC.TabPages.Count)
                {
                    tp = new TabPage(String.Format("Page {0}", pagesTC.TabPages.Count + 1)) { Tag = pagesTC.TabPages.Count + 1 };
                    pagesTC.TabPages.Add(tp);
                }

                UpdateColorPage(pagesTC.TabPages[p], applyOriginalColors);

            }
            while(pagesTC.TabPages.Count > actualPageCount)
                pagesTC.TabPages.RemoveAt(actualPageCount);

            TabPage firstTP = pagesTC.TabPages[0];
            if(CurrentPageCount == null)
                firstTP.Text = "All Pages";
            else
                firstTP.Text = "Page 1";
            pagesTC.ResumeLayout();
        }

        private void UpdateColorPage(TabPage tp, bool applyOriginalColors)
        {
            var colorPositionCtlAnchoring = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
            var pageNum = (int)tp.Tag;
            if(tp.Controls.Count == 0)
            {
                var tlp = new TableLayoutPanel();
                tlp.Dock = DockStyle.Fill;
                tp.Controls.Add(tlp);
            }

            var pageGrid = tp.Controls[0] as TableLayoutPanel;
            pageGrid.Enabled = EditMode;
            pageGrid.SuspendLayout();
            var effRowCount = CurrentRowCount ?? 1;
            var effColCount = CurrentColumnCount ?? 1;

            #region Remove excess color panels
            Label[] currentColorPositionCtls = new Label[pageGrid.Controls.Count];
            pageGrid.Controls.CopyTo(currentColorPositionCtls, 0);
            foreach(Label p in currentColorPositionCtls)
                if(pageGrid.GetColumn(p) >= effColCount || pageGrid.GetRow(p) >= effRowCount)
                {
                    pageGrid.Controls.Remove(p);
                    p.Dispose();
                }
            #endregion

            #region Extend pageGrid and add needed color panels if needed

            pageGrid.RowCount = effRowCount;
            pageGrid.ColumnCount = effColCount;

            for(int r = 0; r < pageGrid.RowCount; ++r)
                for(int c = 0; c < pageGrid.ColumnCount; ++c)
                    if(pageGrid.GetControlFromPosition(c, r) == null)
                    {
                        var colorPositionCtl = new Label()
                        {
                            Name = String.Format("colorPositionCtl_{0}_{1}", r, c),
                            Dock = DockStyle.Fill,
                            Anchor = colorPositionCtlAnchoring,
                            BorderStyle = BorderStyle.Fixed3D,
                            AutoSize = false,
                            TextAlign = ContentAlignment.MiddleCenter,
                            ContextMenuStrip = positionColorCMS,
                            Tag = null
                        };
                        colorPositionCtl.MouseClick += colorPositionCtl_MouseClick;
                        colorPositionCtl.Paint += colorPositionCtl_Paint;
                        pageGrid.Controls.Add(colorPositionCtl, c, r);
                    }

            #endregion

            #region Update Row and Column Styles

            float rowPerc = 100f / pageGrid.RowCount;
            for(int r = 0; r < pageGrid.RowCount; ++r)
                if(r < pageGrid.RowStyles.Count)
                    pageGrid.RowStyles[r].Height = rowPerc;
                else
                    pageGrid.RowStyles.Add(new RowStyle(SizeType.Percent, rowPerc));
            while(pageGrid.RowStyles.Count > pageGrid.RowCount)
                pageGrid.RowStyles.RemoveAt(pageGrid.RowCount);

            float columnPerc = 100f / pageGrid.ColumnCount;
            for(int r = 0; r < pageGrid.ColumnCount; ++r)
                if(r < pageGrid.ColumnStyles.Count)
                    pageGrid.ColumnStyles[r].Width = columnPerc;
                else
                    pageGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPerc));
            while(pageGrid.ColumnStyles.Count > pageGrid.ColumnCount)
                pageGrid.ColumnStyles.RemoveAt(pageGrid.ColumnCount);

            #endregion

            if(applyOriginalColors /* Prevent design changes from wiping colors */)
            {
                #region Apply Colors
                for(int r = 0; r < pageGrid.RowCount; r++)
                    for(int c = 0; c < pageGrid.ColumnCount; c++)
                    {
                        var colorPositionCtl = pageGrid.GetControlFromPosition(c, r) as Label;

                        var setColor = CurrentSet.Colors.FirstOrDefault(
                            (color) => (color.Page == pageNum || color.Page == null)
                                && (color.Row == (r + 1) || color.Row == null)
                                && (color.Column == (c + 1) || color.Column == null)
                            );

                        PaletteColor paletteColor = null;

                        if(setColor != null)
                        {
                            paletteColor = m_palette.FirstOrDefault((x) => x.ColorValue == (setColor.UserColorCode ?? setColor.DefaultColorCode) && x.Name == setColor.Name);

                            if(paletteColor == null)
                            {
                                paletteColor = new PaletteColor(setColor.Name, setColor.UserColorCode ?? setColor.DefaultColorCode);
                                AddPaletteColor(paletteColor, false);
                            }
                        }

                        colorPositionCtl.Tag = paletteColor;
                        colorPositionCtl.Refresh();

                    }
                #endregion
            }

            pageGrid.ResumeLayout();

        }

        void colorPositionCtl_Paint(object sender, PaintEventArgs e)
        {
            var colorPositionCtl = sender as Control;
            var paletteColor = colorPositionCtl.Tag as PaletteColor;
            if(paletteColor != null)
            {
                colorPositionCtl.BackColor = paletteColor.Color;
                colorPositionCtl.Text = paletteColor.Name;
            }
            else
            {
                colorPositionCtl.BackColor = Color.Transparent;
                colorPositionCtl.Text = "[No Color Set]";
            }
            colorPositionCtl.ForeColor = GetForeground(colorPositionCtl.BackColor);
        }

        void colorPositionCtl_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                var ctl = sender as Control;
                positionColorCMS.Show(ctl, e.X, e.Y);
            }
        }

        private static CardColorSetEvaluation SetIsNormal(CardColorSet cs)
        {
            CardColorSetEvaluation eval = new CardColorSetEvaluation();
            if(cs.Colors.Count == 0)
                eval.isNormal = true;
            else if(cs.Colors.Exists((c) => c.Page == null) && cs.Colors.Exists((c) => c.Page != null))
                eval.isNormal = false;
            else if(cs.Colors.Exists((c) => c.Row == null) && cs.Colors.Exists((c) => c.Row != null))
                eval.isNormal = false;
            else if(cs.Colors.Exists((c) => c.Column == null) && cs.Colors.Exists((c) => c.Column != null))
                eval.isNormal = false;
            else
            {
                var pageNums = (from c in cs.Colors select c.Page).Distinct().ToList();
                var rowNums = (from c in cs.Colors select c.Row).Distinct().ToList();
                var colNums = (from c in cs.Colors select c.Column).Distinct().ToList();

                if(pageNums.Count == 1 && pageNums[0] == null)
                    eval.pageCount = null;
                else
                {
                    eval.pageCount = pageNums.Count;
                    eval.isNormal = eval.isNormal && !pageNums.Exists((n) => n == null || n < 1 || n > pageNums.Count);
                }

                if(rowNums.Count == 1 && rowNums[0] == null)
                    eval.rowCount = null;
                else
                {
                    eval.rowCount = rowNums.Count;
                    eval.isNormal = eval.isNormal && !rowNums.Exists((n) => n == null || n < 1 || n > rowNums.Count);
                }

                if(colNums.Count == 1 && colNums[0] == null)
                    eval.columnCount = null;
                else
                {
                    eval.columnCount = colNums.Count;
                    eval.isNormal = eval.isNormal && !colNums.Exists((n) => n == null || n < 1 || n > colNums.Count);
                }

            }

            return eval;
        }

        #region Color Management

        private void AddPaletteColor(PaletteColor pc, bool userColor)
        {
            if(userColor)
                m_userColors.Add(pc);

            if(m_palette.Any(c => c.ColorValue == pc.ColorValue && c.Name == pc.Name))
                return;

            m_palette.Add(pc);
            pc.ColorChanged += paletteColor_ColorChanged;
            pc.NameChanged += paletteColor_NameChanged;

            colorPaletteDGV.SuspendLayout();
            colorPaletteDGV.Rows.Insert(0, 1);
            var row = colorPaletteDGV.Rows[0];
            SetRowPaletteColor(row, pc);
            colorPaletteDGV.ResumeLayout();

            SetCMSPaletteColor(pc);

        }

        private void SetRowPaletteColor(DataGridViewRow row, PaletteColor pc)
        {
            row.Cells[1].Value = pc.Name;
            var colorCell = row.Cells[0];
            colorCell.Style.BackColor = pc.Color;
            colorCell.Style.ForeColor = GetForeground(pc.Color);
            colorCell.Style.SelectionBackColor = colorCell.Style.BackColor;
            colorCell.Style.SelectionForeColor = colorCell.Style.ForeColor;
            row.Tag = pc;
        }

        private void colorPaletteDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = sender as DataGridView;
            if(dgv.ReadOnly)
                return;

            if(e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                var row = dgv.Rows[e.RowIndex];
                var pc = row.Tag as PaletteColor;
                ColorDialog cd = new ColorDialog();
                cd.Color = pc.Color;
                if(cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    pc.Color = cd.Color;
                    SetRowPaletteColor(row, pc);
                }
                cd.Dispose();
            }
        }

        private void colorPaletteDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = sender as DataGridView;
            if(dgv.ReadOnly)
                return;

            if(e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                var row = dgv.Rows[e.RowIndex];
                var pc = row.Tag as PaletteColor;
                pc.Name = row.Cells[e.ColumnIndex].Value.ToString();
                SetRowPaletteColor(row, pc);
            }

        }
        #endregion

        private bool ApplyChanges()
        {
            if(String.IsNullOrWhiteSpace(nameTxt.Text))
            {
                MessageForm.Show(this, "A card color set must have a name.", "Name Needed");
                return false;
            }

            var normName = nameTxt.Text.Trim();
            var nameCollision = m_cardColorSets.FirstOrDefault((s) => s.Name.Equals(normName, StringComparison.CurrentCultureIgnoreCase) && s.Id != CurrentSet.Id);
            if(nameCollision != null)
            {
                MessageForm.Show(this, String.Format("The card color set name '{0}' is already used by another card color set.", nameCollision.Name));
                return false;
            }

            List<CardColorSetColor> colors = new List<CardColorSetColor>();
            for(int p = 0; p < (CurrentPageCount ?? 1); ++p)
            {
                var tp = pagesTC.TabPages[p];
                var pageTLP = tp.Controls[0] as TableLayoutPanel;

                Int32? pageNum = CurrentPageCount == null ? (Int32?)null : p + 1;
                for(int r = 0; r < (CurrentRowCount ?? 1); ++r)
                {
                    Int32? rowNum = CurrentRowCount == null ? (Int32?)null : r + 1;
                    for(int c = 0; c < (CurrentColumnCount ?? 1); ++c)
                    {
                        Int32? colNum = CurrentColumnCount == null ? (Int32?)null : c + 1;
                        var colorPositionCtl = pageTLP.GetControlFromPosition(c, r);
                        if(colorPositionCtl == null)
                        {
                            MessageForm.Show(this, String.Format("Control not found for color position ({0}, {1}, {2})", p + 1, r + 1, c + 1), "Error occurred");
                            return false;
                        }

                        var paletteColor = colorPositionCtl.Tag as PaletteColor;
                        if(paletteColor == null)
                        {
                            MessageForm.Show(this, String.Format("No color defined for position (Page {0}, Row {1}, Column {2})", p + 1, r + 1, c + 1), "Incomplete");
                            return false;
                        }

                        colors.Add(new CardColorSetColor()
                        {
                            Page = pageNum,
                            Row = rowNum,
                            Column = colNum,
                            Name = paletteColor.Name,
                            DefaultColorCode = paletteColor.ColorValue,
                            UserColorCode = paletteColor.ColorValue
                        });
                    }
                }
            }

            var ccs = new CardColorSet(CurrentSet.Id, normName, colors);

            var created = GameTech.Elite.Client.SetCardColorSetData.SetCardColorSet(ccs);

            if(created == null)
            {
                MessageForm.Show(this, "Save failed.", "Save Failed");
                return false;
            }
            else
            {
                EndEdit();
                m_cardColorSets.Remove(CurrentSet);
                cardColorSetCombo.Items.Remove(CurrentSet);
                m_cardColorSets.Add(created);
                cardColorSetCombo.Items.Add(created);
                cardColorSetCombo.SelectedItem = created;
                CurrentSet = created;
                return true;
            }
        }

        private void BeginEdit()
        {
            EditMode = true;
            cardColorSetCombo.Enabled = false;
            editSetBtn.Enabled = false;

            addColorBtn.Enabled = true;
            colorPaletteDGV.ReadOnly = false;

            nameTxt.Enabled = true;
            dimensionsTLP.Enabled = true;
            foreach(TabPage tp in pagesTC.TabPages)
                tp.Controls[0].Enabled = true;

            saveChangesBtn.Enabled = true;
            resetBtn.Enabled = true;
            cancelBtn.Enabled = true;
        }

        private void CancelEdit()
        {
            EndEdit();
            if(CurrentSet.Id == 0)
            {
                cardColorSetCombo.Items.Remove(CurrentSet);
                cardColorSetCombo.SelectedItem = s_cardColorSetNonSelection;
            }
            else
                LoadDetails(true);
        }

        private void EndEdit()
        {
            EditMode = false;

            cardColorSetCombo.Enabled = true;
            editSetBtn.Enabled = true;

            addColorBtn.Enabled = false;
            colorPaletteDGV.ReadOnly = true;

            nameTxt.Enabled = false;
            dimensionsTLP.Enabled = false;
            foreach(TabPage tp in pagesTC.TabPages)
                tp.Controls[0].Enabled = false;

            saveChangesBtn.Enabled = false;
            resetBtn.Enabled = false;
            cancelBtn.Enabled = false;

        }

        private void pagesDecreaseBtn_Click(object sender, EventArgs e) { CurrentPageCount--; }

        private void pagesIncreaseBtn_Click(object sender, EventArgs e) { CurrentPageCount++; }

        private void pagesAnyAllChk_CheckedChanged(object sender, EventArgs e)
        {
            var anyAll = pagesAnyAllChk.Checked;
            CurrentPageCount = (anyAll ? (Int32?)null : (CurrentPageCount ?? 1));
        }

        private void rowsDecreaseBtn_Click(object sender, EventArgs e) { CurrentRowCount--; }

        private void rowsIncreaseBtn_Click(object sender, EventArgs e) { CurrentRowCount++; }

        private void rowsAnyAllChk_CheckedChanged(object sender, EventArgs e)
        {
            var anyAll = rowsAnyAllChk.Checked;
            CurrentRowCount = (anyAll ? (Int32?)null : (CurrentRowCount ?? 1));
        }

        private void colsDecreaseBtn_Click(object sender, EventArgs e) { CurrentColumnCount--; }

        private void colsIncreaseBtn_Click(object sender, EventArgs e) { CurrentColumnCount++; }

        private void colsAnyAllChk_CheckedChanged(object sender, EventArgs e)
        {
            var anyAll = colsAnyAllChk.Checked;
            CurrentColumnCount = (anyAll ? (Int32?)null : (CurrentColumnCount ?? 1));
        }
        #endregion Member Methods

        #region Member Properties
        public bool EditMode
        {
            get { return m_editMode; }
            set
            {
                if(m_editMode != value)
                {
                    m_editMode = value;
                    OnEditModeChanged();
                }
            }
        }

        public CardColorSet CurrentSet
        {
            get { return m_currentCardColorSet; }
            private set
            {
                m_currentCardColorSet = value;
                editSetBtn.Enabled = m_currentCardColorSet != null
                    && m_currentCardColorSet.Id != CardColorSet.NoSet.Id
                    && m_currentCardColorSet.InvoiceColorSetId == null
                    ;
                copySetBtn.Enabled = editSetBtn.Enabled;
                cardColorSetReadOnlyLbl.Visible = m_currentCardColorSet != null && m_currentCardColorSet.InvoiceColorSetId != null;

                LoadDetails(true);
            }
        }

        private int? CurrentPageCount
        {
            get { return m_currentPageCount; }
            set
            {
                if(m_currentPageCount != value)
                {
                    var prev = m_currentPageCount;
                    m_currentPageCount = value;
                    pagesDecreaseBtn.Enabled = m_currentPageCount.HasValue && m_currentPageCount.Value > 1;
                    pagesIncreaseBtn.Enabled = m_currentPageCount.HasValue;
                    pagesAnyAllChk.Checked = m_currentPageCount == null;
                    pageCountLbl.Text = m_currentPageCount.HasValue ? m_currentPageCount.Value.ToString() : "---";
                    UpdateColorPages(false);
                    OnCurrentPageCountChanged(prev, m_currentPageCount);
                }
            }
        }

        private int? CurrentRowCount
        {
            get { return m_currentRowCount; }
            set
            {
                if(m_currentRowCount != value)
                {
                    var prev = m_currentRowCount;
                    m_currentRowCount = value;
                    rowsDecreaseBtn.Enabled = m_currentRowCount.HasValue && m_currentRowCount.Value > 1;
                    rowsIncreaseBtn.Enabled = m_currentRowCount.HasValue;
                    rowsAnyAllChk.Checked = m_currentRowCount == null;
                    rowCountLbl.Text = m_currentRowCount.HasValue ? m_currentRowCount.Value.ToString() : "---";
                    UpdateColorPages(false);
                    OnCurrentRowCountChanged(prev, m_currentRowCount);
                }
            }
        }

        private int? CurrentColumnCount
        {
            get { return m_currentColCount; }
            set
            {
                if(m_currentColCount != value)
                {
                    var prev = m_currentColCount;
                    m_currentColCount = value;
                    colsDecreaseBtn.Enabled = m_currentColCount.HasValue && m_currentColCount.Value > 1;
                    colsIncreaseBtn.Enabled = m_currentColCount.HasValue;
                    colsAnyAllChk.Checked = m_currentColCount == null;
                    colCountLbl.Text = m_currentColCount.HasValue ? m_currentColCount.Value.ToString() : "---";
                    UpdateColorPages(false);
                    OnCurrentColumnCountChanged(prev, m_currentColCount);
                }
            }
        }
        #endregion Member Properties
    }
}