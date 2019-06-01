// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2018 Fortunet

// US4852: Product Center > Coupons: Require spend
// DE13319: Product Center > Coupons: Error when deleting a coupon
// US5417: Advanced coupon limits
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using GTI.Modules.ProductCenter.Data;
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.Business;
using GTI.Modules.Shared.Business;



namespace GTI.Modules.ProductCenter.UI
{
    public partial class CouponManagementForm : GradientForm
    {
        #region Private Properties

        private List<PlayerComp> lCouponItem;
        public static string compSelected;
        public static int compIdSelected;
        internal MagneticCardReader MagCardReader { get; private set; }
        private bool isShowAllCoupon = false;
        private PlayerComp couponItemSelected = new PlayerComp();
        private int tempindex;
        private int m_OperatorID;
        private ProductCenterSettings m_productCenterSettings;

        /// <summary>
        /// List of packages used for displaying what's been assigned to coupons
        /// </summary>
        private List<PackageItem> m_packages = new List<PackageItem>();

        #endregion

        public CouponManagementForm(ProductCenterSettings settings)
        {   
            InitializeComponent();
            m_productCenterSettings = settings;
            lCouponItem = new List<PlayerComp>();
            AcceptButton = imgbtnUpdate;
        }

        private void CouponManagementForm_Load(object sender, EventArgs e)
        {
            try
            {
                m_packages = PackageItems.Sorted;
            }
            catch(Exception ex)
            {
                string err = "Error loading coupon packages " + ex.ToString();
                Logger.LogSevere(err, "CouponMangementAddForm.cs", 0);
                MessageBox.Show(err);
            }
            PopulateCoupon = CouponItems.Sorted("");//Run message to poluate the listview.    

            clearAnyDisplayMessage();
        }

        #region METHOD

        public void HookIdle()
        {
            Application.Idle += OnIdle;
        }
        public void UnHookIdle()
        {
            Application.Idle -= OnIdle;
        }

        //Clear the message "Awarded Successfully"."
        public void clearAnyDisplayMessage()
        {
            if(CouponAwardToPlayer.isAwarded == false)
            {
                if(lblSavedSuccessfully.Visible != false)
                {
                    lblSavedSuccessfully.Visible = false;
                }

                if(lblUnableToDeleteCoupon.Visible != false)
                {
                    lblUnableToDeleteCoupon.Visible = false;
                }
            }
            else
            {
                CouponAwardToPlayer.isAwarded = false;
            }
        }

        #endregion

        #region EVENT

        private void OnIdle(object sender, EventArgs e)
        {
            editToolStripMenuItem.Enabled = gtiListViewCoupon.SelectedIndices.Count > 0;
            delToolStripMenuItem.Enabled = gtiListViewCoupon.SelectedIndices.Count > 0;

            editToolStripMenuItem2.Enabled = gtiListViewCoupon.SelectedIndices.Count > 0;
            deleteCouponToolStripMenuItem.Enabled = gtiListViewCoupon.SelectedIndices.Count > 0;

            //Enable the award to player and award to all players button if the coupon is not expired.
            if(/*m_isCouponExpired == false*/couponItemSelected.IsExpired == false)
            {
                imgbtnAwardToAllPlayers.Enabled = gtiListViewCoupon.SelectedIndices.Count > 0;
                imgbtnAwardToPlayer.Enabled = gtiListViewCoupon.SelectedIndices.Count > 0;
                imgbtnAwardGroup.Enabled = gtiListViewCoupon.SelectedIndices.Count > 0;
                imgbtnExpiredComp.Enabled = gtiListViewCoupon.SelectedIndices.Count > 0;
                //imgbtnUpdate.Enabled = false;
            }
            else
            {
                imgbtnAwardToAllPlayers.Enabled = false;
                imgbtnAwardToPlayer.Enabled = false;
                imgbtnExpiredComp.Enabled = false;
                imgbtnAwardGroup.Enabled = false;
                // imgbtnUpdate.Enabled = gtiListViewCoupon.SelectedIndices.Count > 0;
            }

            imgbtnDelete.Enabled = gtiListViewCoupon.SelectedIndices.Count > 0;
            imgbtnUpdate.Enabled = gtiListViewCoupon.SelectedIndices.Count > 0;
        }

        private void gtiListViewCoupon_KeyDown(object sender, KeyEventArgs e)
        {
            clearAnyDisplayMessage();
            switch(e.KeyCode)
            {
                case Keys.Insert:
                    AddCouponClick(this, e);
                    break;
                case Keys.Enter://Edit if the coupon selected is not = 0
                    if(gtiListViewCoupon.SelectedItems.Count > 0)
                    {
                        editToolStripMenuItem2_Click(this, e);
                    }
                    break;
                case Keys.Delete:
                    imgbtnDelete_Click(this, e);
                    break;

            }
        }

        /// <summary>
        /// Handle Enter key stroke on context menu strip.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuCoupon_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            clearAnyDisplayMessage();
            if(Keys.Enter == e.KeyCode)
            {
                if(gtiListViewCoupon.SelectedItems.Count > 0)
                {
                    editToolStripMenuItem2_Click(this, e);
                }
            }
            else if(Keys.Delete == e.KeyCode)
            {
                if(gtiListViewCoupon.SelectedItems.Count > 0)
                {
                    imgbtnDelete_Click(this, e);
                }
            }
        }

        /// <summary>
        /// Award to all player.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgbtnAwardToAllPlayer_Click(object sender, EventArgs e)
        {
            clearAnyDisplayMessage();
            //MessageBox.Show("1");

            DialogResult dialogResult = MessageForm.Show("Do you want to award " + compSelected + " to all players?", "Award to All Players", MessageFormTypes.YesCancel /*MessageFormTypes.YesCancelComp*/);

            if(dialogResult == DialogResult.Yes)
            {
                SetCompAwardedToPlayer.SetCompAwardToPlayer(CouponManagementForm.compIdSelected, 0);
                // if (lblSavedSuccessfully.Visible != true) { lblSavedSuccessfully.Visible = true; }  
                if(lblSavedSuccessfully.Visible != true)
                {
                    lblSavedSuccessfully.Visible = true;
                    lblSavedSuccessfully.Text = "       Awarded Successfully";
                }

                PopulateCoupon = CouponItems.Sorted("");//Repopulate the listview coupon.
            }
        }

        /// <summary>
        /// Award to a player.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageButton1_Click_1(object sender, EventArgs e)
        {           
            clearAnyDisplayMessage();
            CouponAwardToPlayer catp = new CouponAwardToPlayer(Settings.MSRSettingInfo, Settings.ThirdPartyPlayerInterfaceID);//knc
            catp.compSelected = couponItemSelected.Name;
            catp.compIDSeleccted = couponItemSelected.Id;
            catp.ShowDialog();

            if(CouponAwardToPlayer.isAwarded == true)
            {
                ListViewItem singleItem = gtiListViewCoupon.Items[gtiListViewCoupon.FocusedItem.Index];
                singleItem.Selected = false;

                if(lblSavedSuccessfully.Visible != true)
                {
                    lblSavedSuccessfully.Visible = true;
                    lblSavedSuccessfully.Text = "       Awarded Successfully";
                }

                PopulateCoupon = CouponItems.Sorted("");//Repopulate the listview coupon.
            }
        }

        /// <summary>
        /// Award to group of player.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgbtnAwardGroup_Click(object sender, EventArgs e)
        {
            clearAnyDisplayMessage();
            CouponAwardToGroup catg = new CouponAwardToGroup();
            catg.CompID = couponItemSelected.Id;
            catg.Comp = couponItemSelected.Name;
            catg.MaxUsage = couponItemSelected.CouponMaxUsage;
            catg.OperatorID = m_OperatorID;
            catg.ShowDialog();

            if(catg.isAwarded == true)
            {
                ListViewItem singleItem = gtiListViewCoupon.Items[gtiListViewCoupon.FocusedItem.Index];
                singleItem.Selected = false;

                if(lblSavedSuccessfully.Visible != true)
                {
                    lblSavedSuccessfully.Visible = true;
                    lblSavedSuccessfully.Text = "       Awarded Successfully";
                }
                PopulateCoupon = CouponItems.Sorted("");
            }
        }

        private void gtiListViewCoupon_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            clearAnyDisplayMessage();

            var selectedItems = gtiListViewCoupon.SelectedItems;

            if(selectedItems.Count > 0)
            {
                // Display text of first item selected.
                if(tempindex != -1 && couponItemSelected.IsExpired == true)
                {
                    //                   gtiListViewCoupon.Items[tempindex].ForeColor = Color.Gray;
                }

                compSelected = selectedItems[0].Text;

                //check for null
                if(gtiListViewCoupon.FocusedItem == null)
                {
                    tempindex = -1;
                    return;
                }

                tempindex = gtiListViewCoupon.FocusedItem.Index;
                compIdSelected = lCouponItem[tempindex].Id;

                //Get the whole package data.
                couponItemSelected = lCouponItem[tempindex];

                //Make expired coupon readable.
                if(couponItemSelected.IsExpired == true)
                {
                    //                   gtiListViewCoupon.Items[tempindex].ForeColor = Color.White;
                }
            }
            else //Not selecting any item
            {
                if(tempindex != -1 && couponItemSelected.IsExpired == true)
                {
                    //                  gtiListViewCoupon.Items[tempindex].ForeColor = Color.Gray;
                }
                tempindex = -1;
            }
        }

        private void imgbtnAdd_Enter(object sender, EventArgs e)
        {
            clearAnyDisplayMessage();
        }

        private void contextMenuCoupon_Click(object sender, EventArgs e)
        {
            clearAnyDisplayMessage();
        }

        private void menuStripCoupon_Enter(object sender, EventArgs e)
        {
            clearAnyDisplayMessage();
        }

        private void CouponManagementForm_Activated(object sender, EventArgs e)
        {
            clearAnyDisplayMessage();
        }

        /// <summary>
        /// Show all coupon or show only active couppon.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            clearAnyDisplayMessage();
            if(checkBox1.Checked == true)//Show all coupon
            {
                isShowAllCoupon = true;
            }
            else
            {
                isShowAllCoupon = false;
            }
            PopulateCoupon = CouponItems.Sorted("");
            tempindex = -1;
        }

        /// <summary>
        /// Show add coupon UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCouponClick(object sender, EventArgs e)
        {
            clearAnyDisplayMessage();
            Cursor = Cursors.WaitCursor;
            var couponManagementAddForm = new CouponMangementAddForm(OperatorID, m_productCenterSettings);
            couponManagementAddForm.dIsCouponExpired = false;


            Cursor = Cursors.Default;

            if(couponManagementAddForm.ShowDialog(this) == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                try
                {
                    PlayerComp couponItem = couponManagementAddForm.GenerateCoupon();
                    couponItem.Id = 0; // should be zero, but force it just in case

                    int tempCompID = SetCompMessage.RunMessage(couponItem);//Save coupon to Daily db.
                    PopulateCoupon = CouponItems.Sorted("");//Repopulate the listview coupon.
                    tempindex = -1;
                    if(lblSavedSuccessfully.Visible != true)
                    {
                        lblSavedSuccessfully.Visible = true;
                        lblSavedSuccessfully.Text = "       Saved successfully.";
                        lblSavedSuccessfully.ForeColor = Color.Black;
                    }
                }
                catch(Exception ex)
                {
                    string error = "Error adding coupon. {0}";
                    Logger.LogSevere(String.Format(error, ex.ToString()), "CouponManagementForm.cs", 0);
                    MessageForm.Show(String.Format(error, ex.Message));
                }
                Cursor = Cursors.Default;
                Application.DoEvents();
            }
        }

        /// <summary>
        /// Delete coupon.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgbtnDelete_Click(object sender, EventArgs e)
        {
            //DE13319: added better exception handling if and error occurs
            try
            {
                int status = SetDeleteCompMessage.RunMessage(compIdSelected);
                if(status == 0)
                {
                    //Display message if success.
                    if(lblUnableToDeleteCoupon.Visible != false) { lblUnableToDeleteCoupon.Visible = false; }
                    if(lblSavedSuccessfully.Visible != true)
                    {
                        lblSavedSuccessfully.Visible = true;
                        lblSavedSuccessfully.Text = "       Deleted Successfully";
                    }
                    tempindex = -1;
                    Cursor = Cursors.WaitCursor;
                    PopulateCoupon = CouponItems.Sorted("");//Repopulate the listview coupon.
                    Cursor = Cursors.Default;
                    Application.DoEvents();
                }
                else
                {
                    lblUnableToDeleteCoupon.Text = "Can't delete the coupon because it has been awarded.";
                    if(lblUnableToDeleteCoupon.Visible != true)
                    {
                        lblUnableToDeleteCoupon.Visible = true;
                    }
                }
            }
            catch(Exception ex)
            {
                string error = "Error deleting selected coupon. {0}";
                Logger.LogSevere(String.Format(error, ex.ToString()), "CouponManagementForm.cs", 0);
                MessageForm.Show(String.Format(error, ex.Message));
            }
        }

        /// <summary>
        /// Edit coupon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            // Pass down coupon detail.
            CouponMangementAddForm compMgmtAddForm = new CouponMangementAddForm(OperatorID, m_productCenterSettings);
            compMgmtAddForm.SetCouponData(couponItemSelected);
            compMgmtAddForm.Text = "Edit Coupon";

            Cursor = Cursors.Default;

            var dr = compMgmtAddForm.ShowDialog(this);

            if(dr == DialogResult.OK && compMgmtAddForm.Modified)
            {
                Cursor = Cursors.WaitCursor;
                PlayerComp couponItem = compMgmtAddForm.GenerateCoupon();

                try
                {
                    int tempCompID = SetCompMessage.RunMessage(couponItem);//Save coupon to Daily db.
                    PopulateCoupon = CouponItems.Sorted("");//Repopulate the listview coupon.
                    tempindex = -1; //No item selection
                    if(lblSavedSuccessfully.Visible != true)
                    {
                        lblSavedSuccessfully.Visible = true;
                        lblSavedSuccessfully.Text = "       Updated successfully.";
                        lblSavedSuccessfully.ForeColor = Color.Black;
                    }
                }
                catch(Exception ex)
                {
                    string error = "Error editing selected coupon. {0}";
                    Logger.LogSevere(String.Format(error, ex.ToString()), "CouponManagementForm.cs", 0);
                    MessageForm.Show(String.Format(error, ex.Message));
                }

                Cursor = Cursors.Default;
                Application.DoEvents();
            }
        }

        /// <summary>
        /// Forced to expire a  comp.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgbtnExpiredComp_Click(object sender, EventArgs e)
        {
            clearAnyDisplayMessage();
            DialogResult dialogResult = MessageForm.Show("Do you want to expire " + compSelected + "?", "Confirm", MessageFormTypes.YesNo_regular_DefNo);

            if(dialogResult == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                PlayerComp couponItem = new PlayerComp(couponItemSelected);
                couponItem.EndDate = DateTime.Now.AddMinutes(-1);

                int tempCompID = SetCompMessage.RunMessage(couponItem);//Save coupon to Daily db.
                PopulateCoupon = CouponItems.Sorted("");//Repopulate the listview coupon.

                //Display message if the coupon was forcefully expire.
                if(lblSavedSuccessfully.Visible != true)
                {
                    lblSavedSuccessfully.Visible = true;
                    lblSavedSuccessfully.Text = "       Expired successfully.";
                }

                Cursor = Cursors.Default;
                Application.DoEvents();
            }
        }

        /// <summary>
        /// Actions that occur when the user double-clicks on the list view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gtiListViewCoupon_DoubleClick(object sender, EventArgs e)
        {
            editToolStripMenuItem2_Click(this, e);
        }
        #endregion

        #region MEMBER PROPERTIES

        public int OperatorID
        {
            get { return m_OperatorID; }
            set { m_OperatorID = value; }
        }

        public ProductCenterSettings Settings
        {
            get
            {
                return m_productCenterSettings;
            }

            set
            {
                m_productCenterSettings = value;
            }
        }

        /// <summary>
        ///  Get coupon items.
        /// </summary>
        public List<PlayerComp> PopulateCoupon
        {
            set
            {
                gtiListViewCoupon.Items.Clear();
                lCouponItem.Clear();
                foreach(var coupon in value)
                {
                    if(isShowAllCoupon || !coupon.IsExpired) //Either show all coupons or only the ones that haven't expired yet
                    {
                        ListViewItem lvi = gtiListViewCoupon.Items.Add(coupon.Name);
                        lvi.SubItems.Add(EnumToString.GetDescription(coupon.CouponType));
                        if(coupon.CouponType == PlayerComp.CouponTypes.FixedValue)
                            lvi.SubItems.Add(Helper.DecimalStringToMoneyString(coupon.Value));
                        else if(coupon.CouponType == PlayerComp.CouponTypes.PercentPackage)
                            lvi.SubItems.Add(String.Format("{0}%", coupon.Value));
                        else
                            lvi.SubItems.Add("");
                        lvi.SubItems.Add(coupon.StartDate.ToString());
                        lvi.SubItems.Add(coupon.EndDate.ToString());
                        lvi.SubItems.Add(EnumToString.GetDescription(coupon.AwardType));

                        lvi.Tag = coupon;

                        if(coupon.IsExpired)
                        {
                            lvi.ForeColor = Color.Gray;
                        }
                        lCouponItem.Add(coupon);
                    }
                }
            }
        }
        #endregion

        private void gtiListViewCoupon_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if(lCouponItem[e.ItemIndex].IsExpired)
            {
                if(gtiListViewCoupon.Items[e.ItemIndex].Selected)
                    gtiListViewCoupon.ForeColor = Color.Black;
                else
                    gtiListViewCoupon.ForeColor = Color.Gray;
            }
            else
            {
                gtiListViewCoupon.ForeColor = Color.White;
            }

            e.DrawText();

            e.DrawFocusRectangle();
        }

        private void gtiListViewCoupon_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if(e.Item.Selected)
            {
                using(var brush = new LinearGradientBrush(e.Bounds, ControlPaint.LightLight(gtiListViewCoupon.SelectedBackgroundColor),
                    gtiListViewCoupon.SelectedBackgroundColor, LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }
            else
            {
                Rectangle r = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);

                if(e.ColumnIndex == 0)
                    r.Inflate(0, -1);

                using(var brush = new LinearGradientBrush(e.Bounds, ControlPaint.LightLight(gtiListViewCoupon.UnSelectedBackgroundColor),
                    gtiListViewCoupon.UnSelectedBackgroundColor, LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, r);
                }
            }

            bool isColor = e.SubItem.Tag is bool ? (bool)e.SubItem.Tag : false;
            if(isColor)
            {
                using(var brush = new SolidBrush(e.SubItem.BackColor))
                {
                    Rectangle r1 = e.Bounds;
                    r1.Inflate(-4, -4);
                    e.Graphics.FillRectangle(brush, r1);
                }
            }

            using(var sf = new StringFormat())
            {
                switch(e.Header.TextAlign)
                {
                    case HorizontalAlignment.Center:
                        sf.Alignment = StringAlignment.Center;
                        break;
                    case HorizontalAlignment.Right:
                        sf.Alignment = StringAlignment.Far;
                        break;
                }

                if(lCouponItem[e.ItemIndex].IsExpired)
                {
                    if(e.Item.Selected)
                    {
                        using(var brush = new SolidBrush(Color.LightGray))
                            e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, brush, e.Bounds, sf);
                    }
                    else
                    {
                        Rectangle r = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);

                        if(e.ColumnIndex == 0)
                            r.Inflate(0, -1);

                        using(var brush = new SolidBrush(Color.Gray))
                            e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, brush, r, sf);
                    }
                }
                else
                {
                    if(!(e.Item.ForeColor.A == 255 && e.Item.ForeColor.B == 0 &&
                        e.Item.ForeColor.G == 0 && e.Item.ForeColor.R == 0))
                    {
                        using(var brush = new SolidBrush(e.Item.ForeColor))
                            e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, brush, e.Bounds, sf);
                    }
                    else
                    {
                        if(e.Item.Selected)
                        {
                            using(var brush = new SolidBrush(gtiListViewCoupon.SelectedForegroundColor))
                                e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, brush, e.Bounds, sf);
                        }
                        else
                        {
                            Rectangle r = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);

                            if(e.ColumnIndex == 0)
                                r.Inflate(0, -1);

                            using(var brush = new SolidBrush(gtiListViewCoupon.UnSelectedForegroundColor))
                                e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, brush, r, sf);
                        }
                    }
                }
            }
        }
    }
}