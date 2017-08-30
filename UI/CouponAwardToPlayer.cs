using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.Data;

namespace GTI.Modules.ProductCenter.UI
{
    public partial class CouponAwardToPlayer : EliteGradientForm
    {
        #region Constants and Data Types
       // protected readonly Size WinSize = new Size(320, 240);
        protected readonly Point WinMessageLoc = new Point(8, 75);
        protected readonly Font WinMessageFont = new Font("Trebuchet MS", 12F, FontStyle.Bold);
        protected readonly Size WinButtonSize = new Size(130, 30);
        protected readonly Point WinButton1Loc = new Point(13, 159);
        protected readonly Point WinButton2Loc = new Point(172, 159);
        protected readonly Font WinButtonFont = new Font("Trebuchet MS", 10F, FontStyle.Bold);
        #endregion

        #region Member Variables
        protected bool m_isTouchScreen;
        protected MagneticCardReader m_magCardReader; // PDTS 1064
        protected BarcodeReader m_barcodeReader;
        protected bool m_detectedSwipe; // PDTS 1064
        protected bool m_readWholeCard;
        protected StringBuilder m_cardData = new StringBuilder(); // PDTS 1064
        #endregion

        private bool IsMagCard = false;
        private bool IsLastChar = false;
        private PlayerName playername = new PlayerName();
        private string m_compSelected;// = CouponManagementForm.compSelected;
        private int m_compIDSelected;// = CouponManagementForm.compIdSelected;
        private int m_maxUsage;
        public static bool isAwarded = false;


        #region Constructors

        public int maxUsage
        {
            get { return m_maxUsage; }
            set { m_maxUsage = value; }
        }

        public string compSelected
        {
            get { return m_compSelected; }
            set { m_compSelected = value; }
        }

        public int compIDSeleccted
        {
            get { return m_compIDSelected; }
            set { m_compIDSelected = value; }
        }

        /// <summary>
        /// Initializes a new instance of the MagCardForm class in window 
        /// mode.  The form will also return all key presses captured.
        /// </summary>
        public CouponAwardToPlayer(MSRSettings magCardSettings)
            : base(new NormalDisplayMode())
        {
            m_magCardReader = new MagneticCardReader(magCardSettings);
            m_barcodeReader = new BarcodeReader();

            m_magCardReader.CardSwiped += new MagneticCardSwipedHandler(m_magCardReader_CardSwiped);
            m_magCardReader.BeginReading();

            m_barcodeReader.BarcodeScanned += new BarcodeScanHandler(m_barcodeReader_BarcodeScanned);

            InitializeComponent();

            ApplyDisplayMode();

            //Set new flat background
            //System.Drawing.Color defaultBackground = System.Drawing.ColorTranslator.FromHtml("#44658D");
            //this.BackColor = defaultBackground;
            //this.ForeColor = System.Drawing.Color.White;

            rdoByCardNumber.Checked = true;
            rdoByName.Visible = false;
        }

        void m_barcodeReader_BarcodeScanned(object sender, string e)
        {
            txtbxCardNumber.Text = e;
            imgbtnAdd.PerformClick();
        }

        void m_magCardReader_CardSwiped(object sender, MagneticCardSwipeArgs e)
        {
            txtbxCardNumber.Text = e.CardData;
            imgbtnAdd.PerformClick();
        }

        #endregion

        #region Member Methods
        /// <summary>
        /// Sets the settings of this form based on the current display mode.
        /// </summary>
        protected override void ApplyDisplayMode()
        {
            if(m_isTouchScreen)
            {
                base.ApplyDisplayMode();
                StartPosition = FormStartPosition.CenterParent;
                Size = m_displayMode.DialogSize;
            }
            else // Window mode.
            {
                FormBorderStyle = FormBorderStyle.FixedSingle;
                BackgroundImage = null;
                DrawGradient = true;  
            }

            System.Drawing.Color defaultBackground = System.Drawing.ColorTranslator.FromHtml("#44658D");
            this.BackColor = defaultBackground;
            //this.ForeColor = System.Drawing.Color.White;
        }

        private bool isSwipe = false;
        private bool isSwipeLastCharacter = false;
        private int count = 0;

        /// <summary>
        /// Handles the form's KeyPress event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">An KeyPressEventArgs object that contains the 
        /// event data.</param>
        private void OnKeyPress(object sender, KeyPressEventArgs e)             
        {
            if (m_magCardReader.ReadingCards)
            {
                if (m_magCardReader.ProcessCharacter(e.KeyChar))
                {
                    e.Handled = true;
                    return;
                }
            }

            m_barcodeReader.ProcessCharacter(e.KeyChar);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Gets the mag. card data read in.
        /// </summary>
        public string MagCardNumber
        {
            get
            {
                // PDTS 1064
                return m_cardData.ToString();
            }
        }

        #endregion

        private void rdoByName_CheckedChanged(object sender, EventArgs e)
        {
            IsMagCard = false;
            disablePlayerNMagCard();

            RadioButton currentRdo = (RadioButton)sender;
            if (currentRdo.Name == "rdoByName") 
            {
                if (panel1.Enabled != true) { panel1.Enabled = true; }
                IsMagCard = false;
                if (txtbxCardNumber.Text != string.Empty) { txtbxCardNumber.Text = string.Empty; }
            }
           // else if (currentRdo.Name == "rdoByPlayer") { }
            else if (currentRdo.Name == "rdoByCardNumber") 
            {
                IsMagCard = true;
                if (txtbxCardNumber.Enabled != true) { txtbxCardNumber.Enabled = true; }
            }
        }

        private void disablePlayerNMagCard()
        {
            if (panel1.Enabled != false) { panel1.Enabled = false; }
            if (txtbxCardNumber.Enabled != false) { txtbxCardNumber.Enabled = false; }
        }

        private void imageButton1_Click(object sender, EventArgs e)
        {
            clearAnyMessage();
            this.Close();
        }

        private void imgbtnAdd_Click(object sender, EventArgs e)
        {
            clearAnyMessage();

            if (!ValidateChildren(ValidationConstraints.Enabled | ValidationConstraints.Visible))        //Validate player entry
                return;

            if (txtbxCardNumber.Text != string.Empty)
                playername = GetPlayerByMagCardMessage.RunMessage(txtbxCardNumber.Text);
                
            if (playername.PlayerID != 0)
            {
                DialogResult dialogResult = MessageForm.Show("Do you want to award " + compSelected + " to "   + playername.Fname + " " + playername.Lname + "?", "Confirm", MessageFormTypes.YesCancel);

                if (dialogResult == DialogResult.Yes)
                {
                    SetCompAwardedToPlayer scatp = new SetCompAwardedToPlayer();
                    scatp.DefID = 0;
                    scatp.AwardTypeID = GTI.Modules.ProductCenter.UI.CouponManagementForm.CompAwardTypeID;
                    scatp.set(m_compIDSelected, playername.PlayerID, m_maxUsage);          
                    //if (lblSavedSuccessfully.Visible != true) { lblSavedSuccessfully.Visible = true; } No need since the UI closes after accept button.
                    isAwarded = true;
                    this.Close();
                }
                else
                {

                }
            }
            else
            {
               // MessageForm.Show("Unable to find a player with that card number.", "", MessageFormTypes.OK);
                errorProvider1.SetError(txtbxCardNumber, "Unable to find a player with that card number.");
            }
        }

        private void clearAnyMessage()
        {
           // if (lblSavedSuccessfully.Visible != false) { lblSavedSuccessfully.Visible = false; }
            errorProvider1.Clear();
        }

        private void rdoByName_Enter(object sender, EventArgs e)
        {
            clearAnyMessage();
        }

        private void checkCardNumberIfEmpty()
        {

            if (txtbxCardNumber.Text.Count() == 0)
                m_cardData.Clear();
        }

        private void txtbxCardNumber_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtbxCardNumber.Text.Count() == 0)
                m_cardData.Clear();
        }
    }
}

























//        public CouponAwardToPlayer()
//        {
//            InitializeComponent();
//        }

//        private void imgbtnAdd_Click(object sender, EventArgs e)
//        {
//            //MessageForm.Show("The player dont exists.", "", MessageFormTypes.OK);
//            //0,		-- Player or Staff Flag
//            //0,		-- Player or Staff Login Number
//            //N'',	-- First Name
//            //N'',	-- Last Name
//            //N''		-- Mag Card
//            //18104
//            //exec spFindPlayerOrStaff 0,0,N'',N'Camac',N''
//        }






























//        private void rdoByName_CheckedChanged(object sender, EventArgs e)
//        {

//            SetAllEntryToDisable();
//            RadioButton selectedRdo = (RadioButton)sender;
//            if (selectedRdo.Name == "rdoByName")
//            {
//                if (panel1.Enabled != true) { panel1.Enabled = true; }
//            }
//            else if (selectedRdo.Name == "rdoByPlayer")
//            {
//                if (txtbxPlayerNumber.Enabled != true) {txtbxPlayerNumber.Enabled = true;}
//            }
//            else if (selectedRdo.Name == "rdoByCardNumber") 
//            {
//                if (txtbxFirstName.Enabled != true) { txtbxCardNumber.Enabled = true; }
//            }
//        }


//        private void SetAllEntryToDisable()
//        {
//            if (panel1.Enabled != false) { panel1.Enabled = false; }
//            if (txtbxPlayerNumber.Enabled != false) { txtbxPlayerNumber.Enabled = false; }
//            if (txtbxCardNumber.Enabled != false) { txtbxCardNumber.Enabled = false; }
//        }
//    }
//}
