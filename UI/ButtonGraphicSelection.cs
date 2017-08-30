#region Copyright
// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply: 2016 © FortuNet, Inc.
#endregion

using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using GTI.Controls;
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.Data;
using GTI.Modules.Shared.Business;
using GTI.Modules.Shared.Data;
using GTI.Modules.ProductCenter.Properties;


namespace GTI.Modules.ProductCenter.UI
{
    public partial class ButtonGraphicSelection : GradientForm
    { //US4935
        #region Member Variables
        private readonly List<ButtonGraphic> graphics;
        private List<ImageButton> orignals;
        private List<ImageButton> gels;
        private List<ImageButton> flats;
        #endregion

        public ButtonGraphicSelection()
        {
            InitializeComponent();
            Selected = false;
            graphics = GetButtonGraphicsMessage.GetButtonGraphics(-1);

            orignals = new List<ImageButton>();
            gels = new List<ImageButton>();
            flats = new List<ImageButton>();

            SortButtons();

            fillTab(orignals, tabPage1, 10, 80, 138, 3);
            fillTab(gels, tabPage2, 10, 80, 138, 3);
            fillTab(flats, tabPage3, 10, 80, 138, 3);
        }

        #region Member Properties
        private Point getButtonLoc(int buttonSpacing, int buttonHeight, int buttonWidth, int curColumn, int curRow)
        {
            Point buttonLoc = new Point();

            buttonLoc.X += ((buttonSpacing + buttonWidth) * curColumn);
            buttonLoc.Y += ((buttonSpacing + buttonHeight) * curRow);

            return buttonLoc;
        }

        public bool Selected { get; set; }

        public int ButtonGraphicId
        {
            get;
            set;
        }

        public ImageButton imgButton
        {
            get;
            set;
        }
        #endregion

        #region Member Methods
        private void SortButtons()
        {
            foreach (var bg in graphics)
            {
                ImageButton imgBut = new ImageButton();
                imgBut.Height = 80;
                imgBut.Width = 138;
                imgBut.MouseClick += ButtonClick;

                if (bg.ButtonGraphicDescription.Contains("3D"))
                {
                    string imgUp = bg.ButtonGraphicDescription.Remove(bg.ButtonGraphicDescription.Length - 2) + "GelButtonUp";
                    string imgDown = bg.ButtonGraphicDescription.Remove(bg.ButtonGraphicDescription.Length - 2) + "GelButtonDown";
                    imgBut.ImageNormal = (Image)Resources.ResourceManager.GetObject(imgUp);
                    imgBut.ImagePressed = (Image)Resources.ResourceManager.GetObject(imgDown);
                    imgBut.Stretch = false;
                    imgBut.Name = bg.ButtonGraphicDescription;
                    gels.Add(imgBut);
                }
                else if (bg.ButtonGraphicDescription.Contains("Flat"))
                {
                    string imgUp = bg.ButtonGraphicDescription + "ButtonUp";
                    string imgDown = bg.ButtonGraphicDescription + "ButtonDown";
                    imgBut.ImageNormal = (Image)Resources.ResourceManager.GetObject(imgUp);
                    imgBut.ImagePressed = (Image)Resources.ResourceManager.GetObject(imgDown);
                    imgBut.Stretch = false;
                    imgBut.Name = bg.ButtonGraphicDescription;
                    flats.Add(imgBut);
                }
                else
                {
                    string name = bg.ButtonGraphicDescription;
                    
                    if (bg.ButtonGraphicDescription == "None")
                        name = "Gray";
                    
                    string imgUp = name + "ButtonUp";
                    string imgDown = name + "ButtonDown";
                    imgBut.ImageNormal = (Image)Resources.ResourceManager.GetObject(imgUp);
                    imgBut.ImagePressed = (Image)Resources.ResourceManager.GetObject(imgDown);
                    imgBut.Stretch = true;
                    imgBut.Name = bg.ButtonGraphicDescription;
                    orignals.Add(imgBut);
                }
            }
        }

        private void fillTab(List<ImageButton> buttons, TabPage page, int buttonSpacing, int buttonHeight, int buttonWidth, int maxColumn)
        {
            int curColumn = 0;
            int curRow = 0;
            foreach (ImageButton button in buttons)
            {
                button.Location = getButtonLoc(buttonSpacing, buttonHeight, buttonWidth, curColumn, curRow);
                page.Controls.Add(button);

                if (curColumn != (maxColumn - 1))
                    curColumn++;
                else
                {
                    curRow++;
                    curColumn = 0;
                }
            }
        }

        private void ButtonClick(object sender, MouseEventArgs e)
        {
            Selected = true;
            ImageButton button = (ImageButton)sender;

            foreach (ButtonGraphic graphic in graphics) 
            {
                if (graphic.ButtonGraphicDescription == button.Name)
                {
                    ButtonGraphicId = graphic.ButtonGraphicId;
                    imgButton = button;
                }
            }

            this.Close();
        }

        #endregion
    }
}
