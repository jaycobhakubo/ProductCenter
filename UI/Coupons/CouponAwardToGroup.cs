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
    public partial class CouponAwardToGroup : GradientForm
    {
        #region MEMBER VARIABLES
        private int m_CompID;
        private string m_Comp;
        private int? m_maxUsage;
        private bool m_IsAwarded;
        private int m_DefID;
        private int m_OperatorID;
        Dictionary<int, int> IndexToDefID = new Dictionary<int, int>();
        #endregion

        #region PROPERTIES
        public int OperatorID
        {
            get { return m_OperatorID; }
            set { m_OperatorID = value; }
        }

        public int CompID
        {
            get { return m_CompID; }
            set { m_CompID = value; }
        }

        public string Comp
        {
            get { return m_Comp; }
            set { m_Comp = value; }
        }

        public int? MaxUsage
        {
            get { return m_maxUsage; }
            set { m_maxUsage = value; }
        }


        public bool isAwarded
        {
            get { return m_IsAwarded; }
            set { m_IsAwarded = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CouponAwardToGroup()
        {
            InitializeComponent();

            //Set new flat background
            //System.Drawing.Color defaultBackground = System.Drawing.ColorTranslator.FromHtml("#44658D");
            //this.BackColor = defaultBackground;
            //this.ForeColor = System.Drawing.Color.White;


            LoadPlayerListGroupCmbx();
        }
        #endregion

        private void LoadPlayerListGroupCmbx()
        {
            if (cmbxGroupList.Items.Count > 0)
            {
                cmbxGroupList.Items.Clear();
            }

            GetPlayerGroupList get_pld = new GetPlayerGroupList();
            //List<PlayerListDefinition> List_pld = get_pld.get_playerListdef();
            List<PlayerListDefinition> List_pld = get_pld.GetPlayerListDefinitionMSG();

            int indexOf = 0;

            if (List_pld.Count > 0)
            {
                var sortPlayerListDef = List_pld.OrderBy(x => x.DefinitionName);
                foreach (PlayerListDefinition pld in sortPlayerListDef)
                {
                    cmbxGroupList.Items.Add(pld.DefinitionName);
                    IndexToDefID.Add(indexOf, pld.DefId);
                    indexOf = indexOf + 1;
                }

                if (cmbxGroupList.Items.Count > 0)
                {
                    cmbxGroupList.SelectedIndex = 0;
                }
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageButton1_Click(object sender, EventArgs e)
        {
            m_IsAwarded = false;
            this.Close();
        }

        /// <summary>
        ///  Award to a group of players. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgbtnAdd_Click(object sender, EventArgs e)
        {
            if (cmbxGroupList.SelectedIndex != -1)
            {
                var groupId = cmbxGroupList.SelectedItem;
                this.Cursor = Cursors.WaitCursor;
                var task1 = System.Threading.Tasks.Task.Factory.StartNew(() => SetCompAwardedToPlayer.SetCompAwardToGroup(m_CompID, m_DefID));
                task1.Wait();
                this.Cursor = Cursors.Default;
                m_IsAwarded = true;

                this.Close();
            }
        }

        private void cmbxGroupList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbxGroupList.SelectedIndex != -1)
            {
                m_DefID = IndexToDefID[cmbxGroupList.SelectedIndex];
            }
        }
    }


    struct PlayerListDefinition
    {
        public int DefId;
        public string DefinitionName;
    }

    struct PlayerListSetting
    {
        public int SettingID;// { get; set; }
        public string SettingValue;// { get; set; }
    }



}
