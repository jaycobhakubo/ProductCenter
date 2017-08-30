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

namespace GTI.Modules.ProductCenter.UI
{
    public partial class CardLevelForm : GradientForm
    {
        #region Declarations
        protected DisplayMode DisplayMode = new NormalDisplayMode();
        private static CardLevelDetailForm cardLevelDetailForm;
        private static CardLevelItem copiedCardLevel;
        private static bool isCopyValid;
        private static List<GetCardsetLevelDataMessage.CardColorLevel> _paperColors { get; set; }  // RALLY US4547
        private static List<string> _paperColorNames { get; set; }
        #endregion

        public CardLevelForm()
        {
            InitializeComponent();

            //Set new flat background
            //System.Drawing.Color defaultBackground = System.Drawing.ColorTranslator.FromHtml("#44658D");
            //this.BackColor = defaultBackground;
            //this.ForeColor = System.Drawing.Color.White;

            if (_paperColors == null)// RALLY US4547 fill in the paper color list. It's static, so only need to do it once
            {
                ReloadColorLists();
            }
        }
        public void HookIdle()
        {
            Application.Idle += OnIdle;
        }
        public void UnHookIdle()
        {
            Application.Idle -= OnIdle;
        }
        /// <summary>
        /// Reloads the paper color lists
        /// </summary>
        private void ReloadColorLists()
        {
            try
            {
                _paperColors = GetCardsetLevelDataMessage.GetCardsetLevelData();
                _paperColorNames = _paperColors.ConvertAll(x => x.ColorName);
            }
            catch (Exception ex)
            {
                Logger.LogSevere("Error loading paper cardset color info: " + ex.ToString(), "CardLevelForm.cs", 60);
            }
        }

        #region Member Methods
        private void OnIdle(object sender, EventArgs e)
        {
            //When form is in idle state will execute this.
            //Enable or Disable controls here.

            // Context menu stuff
            EditContextButton.Enabled = ListViewCardLevels.SelectedIndices.Count > 0;
            DeleteContextButton.Enabled = ListViewCardLevels.SelectedIndices.Count > 0;
            CopyContextButton.Enabled = ListViewCardLevels.SelectedIndices.Count > 0;
            //PasteContextButton.Enabled = !string.IsNullOrEmpty(copiedCardLevel.CardLevelName);
            PasteContextButton.Enabled = isCopyValid;

            // Top menu stuff
            SystemEditCardLevelMenuItem.Enabled = ListViewCardLevels.SelectedIndices.Count > 0;
            SystemCopyCardLevelMenuItem.Enabled = ListViewCardLevels.SelectedIndices.Count > 0;
            //SystemPasteCardLevelMenuItem.Enabled = !string.IsNullOrEmpty(copiedCardLevel.CardLevelName);
            SystemPasteCardLevelMenuItem.Enabled = isCopyValid;
            SystemDeleteCardLevelMenuItem.Enabled = ListViewCardLevels.SelectedIndices.Count > 0;
        }

        private void AddLevelClick(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            //Initialize the form.
            cardLevelDetailForm = new CardLevelDetailForm(_paperColorNames)
                                  {
                                      OriginalItem = new CardLevelItem { LevelColor = Color.White.ToArgb(), CardLevelName = "", Multiplier="1.0" },
                                      CardLevelColor = Color.White.ToArgb(),
                                      CardLevelMultiplier = "1.0",
                                      CardLevelName = ""
                                  };

            Cursor = Cursors.Default;

            if (cardLevelDetailForm.ShowDialog(this) == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;

                 //Add this product in the database.
                CardLevelItem cardLevelItem = new CardLevelItem
                                    {
                                        CardLevelId = 0,
                                        CardLevelName = cardLevelDetailForm.CardLevelName,
                                        Multiplier = cardLevelDetailForm.CardLevelMultiplier,
                                        LevelColor = cardLevelDetailForm.CardLevelColor,
                                        IsActive = true
                                    };

                // FIX : DE3187 handle in use card levels
                int cardLevelId = SetCardLevelMessage.SaveNew(cardLevelItem);
                if (cardLevelId == 0)
                {
                    string msg = String.Format(Resources.CardLevelInUse, cardLevelItem.CardLevelName);
                    MessageForm.Show(msg, "ALERT", MessageFormTypes.OK);
                } // END: DE3187
                else if (cardLevelDetailForm.SelectedPaperColors != null) // since it's a new item, don't need to clear anything
                {   // RALLY US4547 save paper color-level link. 
                    foreach (var paperColor in cardLevelDetailForm.SelectedPaperColors)
                    {
                        GetCardsetLevelDataMessage.CardColorLevel cardColorLevel = new GetCardsetLevelDataMessage.CardColorLevel();
                        cardColorLevel.ColorName = paperColor;
                        cardColorLevel.LevelId = cardLevelId; // DE12900 - was using the wrong card level ID here

                        SetCardsetLevelDataMessage.SetCardsetLevelData(cardColorLevel);
                    }
                }
                PopulateCardLevels = CardLevelItems.Sorted(0);
                ReloadColorLists();

                Cursor = Cursors.Default;
                Application.DoEvents();
            }
        }

        private void EditLevelClick(object sender, EventArgs e)
        {
            if (ListViewCardLevels.SelectedIndices.Count > 0)
            {
                Cursor = Cursors.WaitCursor;

                // Get the Product Info from the Listview tag
                var cardLevelItem = (CardLevelItem)ListViewCardLevels.SelectedItems[0].Tag;
                List<GetCardsetLevelDataMessage.CardColorLevel> matches = null;
                List<string> matchAsStr = null;
                if(_paperColors != null)
                    matches = _paperColors.Where(x => x.LevelId == cardLevelItem.CardLevelId).ToList();// RALLY US4547
                if (matches != null) // get the matching paper card color and assign it to the sent in item
                {
                    matchAsStr = matches.ConvertAll(x => x.ColorName);
                    cardLevelItem.PaperCardColors = matchAsStr;
                }

                //Initialize the form.
                cardLevelDetailForm = new CardLevelDetailForm(_paperColorNames, matchAsStr)
                                      {
                                          OriginalItem = cardLevelItem,
                                          CardLevelColor = cardLevelItem.LevelColor,
                                          CardLevelMultiplier = cardLevelItem.Multiplier,
                                          CardLevelName = cardLevelItem.CardLevelName
                                      };

                //if (match != null) // RALLY US4547
                //{   // have to separate this since the "OriginalItem" is a struct and is passed in by value, not reference. Don't want to set it unless it's a valid value
                //    cardLevelDetailForm.SelectedPaperColor = match.ColorName;
                //}

                Cursor = Cursors.Default;

                if (cardLevelDetailForm.ShowDialog(this) == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    // Get the Product info from the form
                    cardLevelItem.CardLevelName = cardLevelDetailForm.CardLevelName;
                    cardLevelItem.Multiplier = cardLevelDetailForm.CardLevelMultiplier;
                    cardLevelItem.LevelColor = cardLevelDetailForm.CardLevelColor;

                    // Update this product in the database.
                    // FIX : DE3187 handle in use card levels
                    if (!SetCardLevelMessage.Save(cardLevelItem.CardLevelId, cardLevelItem))
                    {
                        string msg = String.Format(Resources.CardLevelInUse, cardLevelItem.CardLevelName);
                        MessageForm.Show(msg, "ALERT", MessageFormTypes.OK);
                    } // END: DE3187
                    else
                    {   // RALLY US4547 save paper color-level link.
                        // RALLY DE12917 need to clear out previous card level info
                        GetCardsetLevelDataMessage.CardColorLevel cardColorLevel = new GetCardsetLevelDataMessage.CardColorLevel();
                        cardColorLevel.ColorName = null;
                        cardColorLevel.LevelId = cardLevelItem.CardLevelId;
                        SetCardsetLevelDataMessage.SetCardsetLevelData(cardColorLevel);// remove links to this card level

                        if (cardLevelDetailForm.SelectedPaperColors != null)
                        {
                            foreach (var paperColor in cardLevelDetailForm.SelectedPaperColors)
                            {
                                cardColorLevel = new GetCardsetLevelDataMessage.CardColorLevel();
                                cardColorLevel.ColorName = paperColor;
                                cardColorLevel.LevelId = cardLevelItem.CardLevelId;

                                SetCardsetLevelDataMessage.SetCardsetLevelData(cardColorLevel);
                            }
                        }
                    }

                    PopulateCardLevels = CardLevelItems.Sorted(0);
                    ReloadColorLists();
                    Cursor = Cursors.Default;
                }
            }
        }

        private void CopyLevelClick(object sender, EventArgs e)
        {
            if (ListViewCardLevels.SelectedIndices.Count > 0)
            {
                copiedCardLevel = (CardLevelItem)ListViewCardLevels.SelectedItems[0].Tag;
                copiedCardLevel.CardLevelName = String.Empty;
                isCopyValid = true;
            }
        }

        private void PasteLevelClick(object sender, EventArgs e)
        {
            if (isCopyValid)
            {
                Cursor = Cursors.WaitCursor;

                //Initialize the form.
                cardLevelDetailForm = new CardLevelDetailForm(_paperColorNames, copiedCardLevel.PaperCardColors)
                                      {
                                          OriginalItem = copiedCardLevel,
                                          CardLevelColor = copiedCardLevel.LevelColor,
                                          CardLevelMultiplier = copiedCardLevel.Multiplier,
                                          CardLevelName = copiedCardLevel.CardLevelName
                                      };

                Cursor = Cursors.Default;

                if (cardLevelDetailForm.ShowDialog(this) == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    var cardLevelItem = new CardLevelItem
                    {
                        CardLevelId = 0,
                        CardLevelName = cardLevelDetailForm.CardLevelName,
                        Multiplier = cardLevelDetailForm.CardLevelMultiplier,
                        LevelColor = cardLevelDetailForm.CardLevelColor,
                        IsActive = true
                    };

                    // FIX : DE3187 handle in use card levels
                    int cardLevelId = SetCardLevelMessage.SaveNew(cardLevelItem);
                    if (cardLevelId == 0)
                    {
                        string msg = String.Format(Resources.CardLevelInUse, cardLevelItem.CardLevelName);
                        MessageForm.Show(msg, "ALERT", MessageFormTypes.OK);
                    } // END: DE3187
                    else if (cardLevelDetailForm.SelectedPaperColors != null) // since it's a new item, don't need to clear anything
                    {   // RALLY US4547 save paper color-level link. 
                        foreach (var paperColor in cardLevelDetailForm.SelectedPaperColors)
                        {
                            GetCardsetLevelDataMessage.CardColorLevel cardColorLevel = new GetCardsetLevelDataMessage.CardColorLevel();
                            cardColorLevel.ColorName = paperColor;
                            cardColorLevel.LevelId = cardLevelItem.CardLevelId;

                            SetCardsetLevelDataMessage.SetCardsetLevelData(cardColorLevel);
                        }
                    }

                    PopulateCardLevels = CardLevelItems.Sorted(0);
                    ReloadColorLists();
                    Cursor = Cursors.Default;
                }
            }
        }

        private void DeleteLevelClick(object sender, EventArgs e)
        {
            //START RALLY DE9025
            int count=ListViewCardLevels.Items.Count;
            if (count == 1)
            {
                MessageForm.Show(Resources.CannotDelete, Resources.DeleteLevelsTitle,MessageFormTypes.OK);
               
            }
            //END RALLY DE9025
            else
            if (ListViewCardLevels.SelectedIndices.Count > 0)
            {
                if (MessageForm.Show(Resources.ConfirmDelete, Resources.DeleteLevelsTitle, MessageFormTypes.YesNo, 0) == DialogResult.Yes)//RALLY DE 6657
                {
                    Cursor = Cursors.WaitCursor;

                    // Get the Product Item info.
                    var cardLevelItem = (CardLevelItem)ListViewCardLevels.SelectedItems[0].Tag;

                    // Update the product listview...
                    ListViewCardLevels.SelectedItems[0].Remove();

                    cardLevelItem.IsActive = false;
                    // FIX : DE3187 handle in use card levels
                    if (!SetCardLevelMessage.Save(cardLevelItem.CardLevelId, cardLevelItem))
                    {
                        string msg = String.Format(Resources.DeleteInUseCardLevel, cardLevelItem.CardLevelName);
                        MessageForm.Show(msg, "ALERT", MessageFormTypes.OK);
                    } // END: DE3187
                    else
                    {   // RALLY US4547 delete all paper color links to level
                        GetCardsetLevelDataMessage.CardColorLevel cardColorLevel = new GetCardsetLevelDataMessage.CardColorLevel();
                        cardColorLevel.ColorName = null;
                        cardColorLevel.LevelId = cardLevelItem.CardLevelId;

                        SetCardsetLevelDataMessage.SetCardsetLevelData(cardColorLevel);
                    }

                    PopulateCardLevels = CardLevelItems.Sorted(0);
                    ReloadColorLists();

                    Cursor = Cursors.Default;
                }
            }
        }

        void ListViewCardLevelsKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    EditLevelClick(this, e);
                    break;
                case Keys.Delete:
                    DeleteLevelClick(this, e);
                    break;
                case Keys.Insert:
                    AddLevelClick(this, e);
                    break;
            }
        }

        #endregion Member Methods

        #region Member Properties
        /// <summary>
        /// Populates the form's Product List.
        /// </summary>
        public List<CardLevelItem> PopulateCardLevels
        {
            set
            {
                // Clear the Product Item List.
                ListViewCardLevels.Items.Clear();

                 //Populate the Product Item List.
                foreach (var cardLevel in value)
                {
                    // the stored procedure should only return active items,
                    //  but just to be sure we check it here
                    if (cardLevel.IsActive)
                    {
                        ListViewItem lvi = ListViewCardLevels.Items.Add(cardLevel.CardLevelName);
                        lvi.SubItems.Add(cardLevel.Multiplier);
                        lvi.UseItemStyleForSubItems = false;
                        // FIX: DE1907 TA2616 Allow Game Color instead of Level Color
                        Color tmpColor = Color.FromArgb(cardLevel.LevelColor);
                        if (tmpColor.ToArgb() == 0)
                        {
                            lvi.SubItems.Add("Game");
                        }
                        else
                        {
                            ListViewItem.ListViewSubItem lvsi = lvi.SubItems.Add("");
                            lvsi.BackColor = tmpColor;
                            lvsi.Tag = true;
                        }
                        // END: DE1907 TA2616 Allow Game Color instead of Level Color
                        lvi.Tag = cardLevel;
                    }
                }
            }
        }
        #endregion Member Properties
    }
}