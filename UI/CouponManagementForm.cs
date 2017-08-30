// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2016 Fortunet

//US4852: Product Center > Coupons: Require spend
//DE13319: Product Center > Coupons: Error when deleting a coupon
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
        private static CouponMangementAddForm couponManagementAddForm;
        public static string compSelected;
        public static int compIdSelected;
        public static int CompAwardTypeID;
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
            m_productCenterSettings = settings;

            InitializeComponent();

            //Set new flat background
            //System.Drawing.Color defaultBackground = System.Drawing.ColorTranslator.FromHtml("#44658D");
            //this.BackColor = defaultBackground;
            //this.ForeColor = System.Drawing.Color.White;

            lCouponItem = new List<PlayerComp>();
            AcceptButton = imgbtnUpdate;
        }

        private void CouponManagementForm_Load(object sender, EventArgs e)
        {
            try
            {
                m_packages = PackageItems.Sorted;
            }
            catch (Exception ex)
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
            if (CouponAwardToPlayer.isAwarded == false)
            {
                if (lblSavedSuccessfully.Visible != false)
                {
                    lblSavedSuccessfully.Visible = false;
                }

                if (lblUnableToDeleteCoupon.Visible != false)
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
            if (/*m_isCouponExpired == false*/couponItemSelected.IsExpired == false)
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
            switch (e.KeyCode)
            {
                case Keys.Insert:
                    AddCouponClick(this, e);
                    break;
                case Keys.Enter://Edit if the coupon selected is not = 0
                    if (gtiListViewCoupon.SelectedItems.Count > 0)
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
            if (Keys.Enter == e.KeyCode)
            {
                if (gtiListViewCoupon.SelectedItems.Count > 0)
                {
                    editToolStripMenuItem2_Click(this, e);
                }
            }
            else if (Keys.Delete == e.KeyCode)
            {
                if (gtiListViewCoupon.SelectedItems.Count > 0)
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
            CompAwardTypeID = 1;
            clearAnyDisplayMessage();
            //MessageBox.Show("1");

            DialogResult dialogResult = MessageForm.Show("Do you want to award " + compSelected + " to all players?", "Award to All Players", MessageFormTypes.YesCancel /*MessageFormTypes.YesCancelComp*/);

            if (dialogResult == DialogResult.Yes)
            {
                SetCompAwardedToPlayer scatp = new SetCompAwardedToPlayer();
                scatp.DefID = 0;
                scatp.AwardTypeID = CompAwardTypeID;
                scatp.set(CouponManagementForm.compIdSelected, 0, couponItemSelected.CouponMaxUsage);
                //SetCompAwardedToPlayer.RunMessage(CouponManagementForm.compIdSelected, 0, couponItemSelected.CouponMaxUsage);
                // if (lblSavedSuccessfully.Visible != true) { lblSavedSuccessfully.Visible = true; }  
                if (lblSavedSuccessfully.Visible != true)
                {
                    lblSavedSuccessfully.Visible = true;
                    lblSavedSuccessfully.Text = "       Awarded Successfully";
                }

                PopulateCoupon = CouponItems.Sorted("");//Repopulate the listview coupon.
            }
            else
            {

            }

        }

        /// <summary>
        /// Award to a player.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageButton1_Click_1(object sender, EventArgs e)
        {
            //SAVE(knc.20150722)
            //===================================
            //MagneticCardReader magCardReader = new MagneticCardReader(Configuration.mSentinels) { StripNonAlphanumericChars = Configuration.mStripNonAlphanumeric };
            //magCardReader.BeginReading();
            //PlayerSearchForm search = new PlayerSearchForm(false, Configuration.operatorID, magCardReader,
            //                                Configuration.mMachineAccounts, Configuration.mForceEnglish);

            //search.ShowDialog();
            //magCardReader.EndReading();

            //if (search.DialogResult == DialogResult.OK)
            //{
            //    //fieldPerson.Text = search.SelectedPlayer.ToString(!Configuration.mMachineAccounts);
            //   m_playerID = search.SelectedPlayer.Id;
            //}
            //else
            //{
            //    //fieldPerson.Text = "All";
            //    m_playerID = 0;

            //    if (search.DialogResult == DialogResult.Abort)
            //    {
            //        MessageForm.Show("");
            //    }
            //}
            //========================================


            clearAnyDisplayMessage();
            CompAwardTypeID = 1;
            CouponAwardToPlayer catp = new CouponAwardToPlayer(Settings.MSRSettingInfo);
            catp.compSelected = couponItemSelected.Name;
            catp.compIDSeleccted = couponItemSelected.Id;
            catp.maxUsage = couponItemSelected.CouponMaxUsage;

            catp.ShowDialog();
            if (CouponAwardToPlayer.isAwarded == true)
            {
                ListViewItem singleItem = gtiListViewCoupon.Items[gtiListViewCoupon.FocusedItem.Index];
                singleItem.Selected = false;

                if (lblSavedSuccessfully.Visible != true)
                {
                    lblSavedSuccessfully.Visible = true;
                    lblSavedSuccessfully.Text = "       Awarded Successfully";
                }

                //?Why do we need to repopulate the list if we are just rewarding the coupon to a player?
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
            CompAwardTypeID = 2;
            CouponAwardToGroup catg = new CouponAwardToGroup();
            catg.CompID = couponItemSelected.Id;
            catg.Comp = couponItemSelected.Name;
            catg.MaxUsage = couponItemSelected.CouponMaxUsage;
            catg.OperatorID = m_OperatorID;
            catg.ShowDialog();

            if (catg.isAwarded == true)
            {
                ListViewItem singleItem = gtiListViewCoupon.Items[gtiListViewCoupon.FocusedItem.Index];
                singleItem.Selected = false;

                if (lblSavedSuccessfully.Visible != true)
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

            if (selectedItems.Count > 0)
            {
                // Display text of first item selected.
                if (tempindex != -1 && couponItemSelected.IsExpired == true)
                {
 //                   gtiListViewCoupon.Items[tempindex].ForeColor = Color.Gray;
                }

                compSelected = selectedItems[0].Text;

                //check for null
                if (gtiListViewCoupon.FocusedItem == null)
                {
                    tempindex = -1;
                    return;
                }

                tempindex = gtiListViewCoupon.FocusedItem.Index;
                compIdSelected = lCouponItem[tempindex].Id;

                //Get the whole package data.
                couponItemSelected = lCouponItem[tempindex];

                //Make expired coupon readable.
                if (couponItemSelected.IsExpired == true)
                {
 //                   gtiListViewCoupon.Items[tempindex].ForeColor = Color.White;
                }
            }
            else //Not selecting any item
            {
                if (tempindex != -1 && couponItemSelected.IsExpired == true)
                {
  //                  gtiListViewCoupon.Items[tempindex].ForeColor = Color.Gray;
                }
                tempindex = -1;
            }


        }
        
        private void imgbtnAdd_Enter(object sender, EventArgs e)
        {
            clearAnyDisplayMessage();
            //MessageBox.Show("6");
        }

        private void contextMenuCoupon_Click(object sender, EventArgs e)
        {
            clearAnyDisplayMessage();
            //MessageBox.Show("7");
        }

        private void menuStripCoupon_Enter(object sender, EventArgs e)
        {
            clearAnyDisplayMessage();
            //MessageBox.Show("8");
        }

        private void CouponManagementForm_Activated(object sender, EventArgs e)
        {
            clearAnyDisplayMessage();
            //MessageBox.Show("9");
        }

        /// <summary>
        /// Show all coupon or show only active couppon.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            clearAnyDisplayMessage();
            if (checkBox1.Checked == true)//Show all coupon
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
            couponManagementAddForm = new CouponMangementAddForm(OperatorID, m_productCenterSettings);
            couponManagementAddForm.dIsNew = true;
            couponManagementAddForm.dIsCouponExpired = false;


            Cursor = Cursors.Default;

            if (couponManagementAddForm.ShowDialog(this) == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                try
                {
                    PlayerComp couponItem = couponManagementAddForm.GenerateCoupon();
                    couponItem.Id = 0; // should be zero, but force it just in case

                    int tempCompID = SetCompMessage.RunMessage(couponItem);//Save coupon to Daily db.
                    PopulateCoupon = CouponItems.Sorted("");//Repopulate the listview coupon.
                    tempindex = -1;
                    if (lblSavedSuccessfully.Visible != true)
                    {
                        lblSavedSuccessfully.Visible = true;
                        lblSavedSuccessfully.Text = "       Saved successfully.";
                        lblSavedSuccessfully.ForeColor = Color.Black;
                    }
                }
                catch (Exception ex)
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
                if (status == 0)
                {
                    //Display message if success.
                    if (lblUnableToDeleteCoupon.Visible != false) { lblUnableToDeleteCoupon.Visible = false; }
                    if (lblSavedSuccessfully.Visible != true)
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
                    if (lblUnableToDeleteCoupon.Visible != true)
                    {
                        lblUnableToDeleteCoupon.Visible = true;
                    }
                }
            }
            catch (Exception ex)
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
            bool IsModified = false;

            if (compMgmtAddForm.ShowDialog(this) == DialogResult.OK)
            {
                //Check if something has changed.
                if (
                    compMgmtAddForm.dCouponID != couponItemSelected.Id
                    || compMgmtAddForm.dCouponName != couponItemSelected.Name
                    || compMgmtAddForm.dStartDate != couponItemSelected.StartDate
                    || compMgmtAddForm.dEndDate != couponItemSelected.EndDate
                    || compMgmtAddForm.dValue != couponItemSelected.Value
                    || compMgmtAddForm.dCouponMaxUsage != couponItemSelected.CouponMaxUsage
                    || compMgmtAddForm.SelectedCouponType != couponItemSelected.CouponType
                    || !Enumerable.Equals(compMgmtAddForm.AwardedPackageIds, couponItemSelected.EarnedPackageIDs)
                    || compMgmtAddForm.AwardType != couponItemSelected.AwardType
                    || compMgmtAddForm.UnlockSessionCount != couponItemSelected.UnlockSessionCount
                    || compMgmtAddForm.UnlockSpend != couponItemSelected.UnlockSpend
                    || compMgmtAddForm.MinimumSpendToQualify != couponItemSelected.MinimumSpendToQualify //US4852
                    || compMgmtAddForm.IsRestrictionsModified//US4852
                    || compMgmtAddForm.IgnoreValidationsForIgnoredPackages != couponItemSelected.IgnoreValidationsForIgnoredPackages // DE13267
                    )
                { IsModified = true; }

                if (IsModified == true)
                {
                    Cursor = Cursors.WaitCursor;
                    PlayerComp couponItem = compMgmtAddForm.GenerateCoupon();

                    try
                    {
                        int tempCompID = SetCompMessage.RunMessage(couponItem);//Save coupon to Daily db.
                        PopulateCoupon = CouponItems.Sorted("");//Repopulate the listview coupon.
                        tempindex = -1; //No item selection
                        if (lblSavedSuccessfully.Visible != true)
                        {
                            lblSavedSuccessfully.Visible = true;
                            lblSavedSuccessfully.Text = "       Updated successfully.";
                            lblSavedSuccessfully.ForeColor = Color.Black;
                        }
                    }
                    catch (Exception ex)
                    {
                        string error = "Error editing selected coupon. {0}";
                        Logger.LogSevere(String.Format(error, ex.ToString()), "CouponManagementForm.cs", 0);
                        MessageForm.Show(String.Format(error, ex.Message));
                    }

                    Cursor = Cursors.Default;
                    Application.DoEvents();
                }
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

            if (dialogResult == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                PlayerComp couponItem = new PlayerComp(couponItemSelected);
                couponItem.EndDate = DateTime.Now.AddMinutes(-1);

                int tempCompID = SetCompMessage.RunMessage(couponItem);//Save coupon to Daily db.
                PopulateCoupon = CouponItems.Sorted("");//Repopulate the listview coupon.

                //Display message if the coupon was forcefully expire.
                if (lblSavedSuccessfully.Visible != true)
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
                foreach (var coupon in value)
                {
                    if (isShowAllCoupon || !coupon.IsExpired) //Either show all coupons or only the ones that haven't expired yet
                    {
                        ListViewItem lvi = gtiListViewCoupon.Items.Add(coupon.Name);
                        lvi.SubItems.Add(coupon.StartDate.ToString());
                        lvi.SubItems.Add(coupon.EndDate.ToString());
                        if (coupon.CouponType == PlayerComp.CouponTypes.FixedValue)
                        {
                            lvi.SubItems.Add(String.Format("{0} Off", Helper.DecimalStringToMoneyString(coupon.Value.ToString())));
                        }
                        else if (coupon.CouponType == PlayerComp.CouponTypes.PercentPackage)
                        {
                            string packageName = "Multiple Packages";

                            if (coupon.EarnedPackageIDs.Count == 1)
                            {
                                int packageID = coupon.EarnedPackageIDs.First();
                                PackageItem match = m_packages.FirstOrDefault(x => x.PackageId == packageID); // find the package name for display

                                if (match != null)
                                    packageName = match.PackageName;
                                else
                                    packageName = "Package " + packageID;
                            }
                            lvi.SubItems.Add(String.Format("{0}% off {1}", coupon.Value.ToString(), packageName));
                        }
                        else // Alt Price Package
                        {
                            string packageName = "Multiple Packages";

                            if (coupon.EarnedPackageIDs.Count == 1)
                            {
                                int packageID = coupon.EarnedPackageIDs.First();
                                PackageItem match = m_packages.FirstOrDefault(x => x.PackageId == packageID); // find the package name for display

                                if (match != null)
                                    packageName = match.PackageName;
                                else
                                    packageName = "Package " + packageID;
                            }

                            lvi.SubItems.Add(String.Format("Alt Price {0}", packageName));
                        }
                        lvi.Tag = coupon;

                        if (coupon.IsExpired)
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
            if (lCouponItem[e.ItemIndex].IsExpired)
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
            if (e.Item.Selected)
            {
                using (var brush = new LinearGradientBrush(e.Bounds, ControlPaint.LightLight(gtiListViewCoupon.SelectedBackgroundColor),
                    gtiListViewCoupon.SelectedBackgroundColor, LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }
            else
            {
                using (var brush = new LinearGradientBrush(e.Bounds, ControlPaint.LightLight(gtiListViewCoupon.UnSelectedBackgroundColor),
                    gtiListViewCoupon.UnSelectedBackgroundColor, LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }

            bool isColor = e.SubItem.Tag is bool ? (bool)e.SubItem.Tag : false;
            if (isColor)
            {
                using (var brush = new SolidBrush(e.SubItem.BackColor))
                {
                    Rectangle r1 = e.Bounds;
                    r1.Inflate(-4, -4);
                    e.Graphics.FillRectangle(brush, r1);
                }
            }

            using (var sf = new StringFormat())
            {
                switch (e.Header.TextAlign)
                {
                    case HorizontalAlignment.Center:
                        sf.Alignment = StringAlignment.Center;
                        break;
                    case HorizontalAlignment.Right:
                        sf.Alignment = StringAlignment.Far;
                        break;
                }

                if (lCouponItem[e.ItemIndex].IsExpired)
                {
                    if (e.Item.Selected)
                    {
                        using (var brush = new SolidBrush(Color.LightGray))
                            e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, brush, e.Bounds, sf);
                    }
                    else
                    {
                        using (var brush = new SolidBrush(Color.Gray))
                            e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, brush, e.Bounds, sf);
                    }
                }
                else
                {
                    if (!(e.Item.ForeColor.A == 255 && e.Item.ForeColor.B == 0 &&
                        e.Item.ForeColor.G == 0 && e.Item.ForeColor.R == 0))
                    {
                        using (var brush = new SolidBrush(e.Item.ForeColor))
                            e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, brush, e.Bounds, sf);
                    }
                    else
                    {
                        if (e.Item.Selected)
                        {
                            using (var brush = new SolidBrush(gtiListViewCoupon.SelectedForegroundColor))
                                e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, brush, e.Bounds, sf);
                        }
                        else
                        {
                            using (var brush = new SolidBrush(gtiListViewCoupon.UnSelectedForegroundColor))
                                e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, brush, e.Bounds, sf);
                        }
                    }
                }
            }
        }
    }
}


#region Fordatagridviewreference

//private void gtiListViewCoupon_KeyUp(object sender, KeyEventArgs e)
//{
//    if (m_isCouponExpired == true)
//    {
//        int T_index = gtiListViewCoupon.FocusedItem.Index;
//        gtiListViewCoupon.Items[T_index].ForeColor = Color.Gray;
//    }
//}

//private void gtiListViewCoupon_Enter(object sender, EventArgs e)
//{
//    //if (CouponAwardToPlayer.isAwarded == true)
//    //{
//        clearAnyDisplayMessage();
//        MessageBox.Show("5");
//    //}
//}

//public void LoadCoupondgv()
//{
//    //Delete this method once the listview is solid
//    Data.TempSQL.GetCoupon gc = new Data.TempSQL.GetCoupon();
//    lcouponData = gc.getCoupon();
//    dtgviewCoupon.DataSource = null;
//    dtgviewCoupon.Rows.Clear();
//    dtgviewCoupon.DataSource = lcouponData;
//}

//private void gtiListViewCoupon_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
//  {

//      clearAnyDisplayMessage();

//      var selectedItems = gtiListViewCoupon.SelectedItems;
//      if (selectedItems.Count > 0)
//      {
//          // Display text of first item selected.
//          if (tempindex != -1 && couponItemSelected.isExpired == true)
//          {
//              gtiListViewCoupon.Items[tempindex].ForeColor = Color.Gray;
//          }

//          compSelected = selectedItems[0].Text;
//          tempindex = gtiListViewCoupon.FocusedItem.Index;
//          compIdSelected = LCouponItem2.lCouponItem[tempindex].cID2;

//          //Get the whole package data.
//          couponItemSelected = LCouponItem2.lCouponItem[tempindex];

//          //Make expired coupon readable.
//          if (couponItemSelected.isExpired == true)
//          {
//              gtiListViewCoupon.Items[tempindex].ForeColor = Color.White;
//          }
//      }
//      else //Not selecting any item
//      {
//          if (tempindex != -1 && couponItemSelected.isExpired == true)
//          {
//              gtiListViewCoupon.Items[tempindex].ForeColor = Color.Gray;
//          }
//          tempindex = -1;
//      }

//      //var test = e.Item;
//      ////Revert back coupon item to gray("or disable status") if its expired before selecting a new coupon item.
//      ////if (m_isCouponExpired == true)
//      ////{
//      ////    gtiListViewCoupon.Items[tempindex].ForeColor = Color.Gray;
//      ////}
//      ////MessageBox.Show("Item Changed");

//      //DateTime exprDate = Convert.ToDateTime(gtiListViewCoupon.Items[test.Index].SubItems[2].Text);

//      //if (exprDate > DateTime.Now)
//      //{
//      //    //MessageBox.Show("Its not expired");
//      //    //fire the event
//      //    m_isCouponExpired = false;


//      //    //gtiListViewCoupon.SelectedIndexChanged += new EventHandler(gtiListViewCoupon_SelectedIndexChanged);

//      //}
//      //else
//      //{
//      //    //MessageBox.Show("Its expired");
//      //    //if (e.IsSelected) { e.Item.Selected = false; }//Unable to select the item if it expired.

//      //    //Stop the other event execution
//      //   // gtiListViewCoupon.SelectedIndexChanged -= new EventHandler(gtiListViewCoupon_SelectedIndexChanged);
//      //    m_isCouponExpired = true;
//      //  //  gtiListViewCoupon.SelectedIndexChanged += new EventHandler(gtiListViewCoupon_SelectedIndexChanged);
//      //   // return;
//      //}
//      ////gtiListViewCoupon.SelectedIndexChanged += new EventHandler(gtiListViewCoupon_SelectedIndexChanged);
//      //runOnce = false;
//  }


//  //private int countTestSelectedIndexChanged = 0;

//  private void gtiListViewCoupon_SelectedIndexChanged(object sender, EventArgs e)
//  {

//     //// MessageBox.Show("SelectedIndexChanged");

//     // //if (count == 0)
//     // //{
//     // if (runOnce == false)
//     // {

//     //     countTestSelectedIndexChanged = countTestSelectedIndexChanged + 1;
//     //     MessageBox.Show(countTestSelectedIndexChanged.ToString());

//     //     var selectedItems = gtiListViewCoupon.SelectedItems;
//     //     if (selectedItems.Count > 0)
//     //     {
//     //         // Display text of first item selected.
//     //         compSelected = selectedItems[0].Text;
//     //         tempindex = gtiListViewCoupon.FocusedItem.Index;
//     //         compIdSelected = LCouponItem2.lCouponItem[tempindex].cID2;

//     //         //Get the whole package data.
//     //         couponItemSelected = LCouponItem2.lCouponItem[tempindex];

//     //         //Make expired coupon readable.
//     //         //if (m_isCouponExpired == true)
//     //         //{
//     //         //    gtiListViewCoupon.Items[tempindex].ForeColor = Color.White;
//     //         //}
//     //     }

//     //     runOnce = true;
//     // }    

//     // //    }
//     // //    else
//     // //    {

//     // //    }
//     // //}
//     // //count = count + 1;
//     // //if (count == 2)
//     // //{
//     // //    count = 0;
//     // //}
//  }



#endregion