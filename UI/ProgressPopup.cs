using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GTI.Modules.ProductCenter.UI
{
    public partial class ProgressPopup : GTI.Modules.Shared.GradientForm
    {
        private BackgroundWorker m_bgw;

        public ProgressPopup(BackgroundWorker bgw, object workArgs = null, string title = null)
        {
            InitializeComponent();

            m_bgw = bgw;

            theTitleLabel.Visible = !String.IsNullOrWhiteSpace(title);
            theTitleLabel.Text = title;

            theCancelCmd.Visible = m_bgw.WorkerSupportsCancellation;

            if(m_bgw.WorkerReportsProgress)
            {
                theProgressBar.Visible = true;
                theProgressMessageLabel.Visible = true;
                m_bgw.ProgressChanged += new ProgressChangedEventHandler(bgw_ProgressChanged);
            }
            else
            {
                theProgressBar.Visible = true;
                theProgressMessageLabel.Visible = true;
            }

            m_bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);

            m_bgw.RunWorkerAsync(workArgs);
        }

        public object Result { get; private set; }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Cancelled)
            {
                Result = null;
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
            else
            {
                Result = e.Result;
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            theProgressBar.Value = e.ProgressPercentage;
            theProgressMessageLabel.Text = e.UserState as string;
        }

        private void theCancelCmd_Click(object sender, EventArgs e)
        {
            if(!m_bgw.CancellationPending)
            {
                m_bgw.CancelAsync();
                theCancelCmd.Enabled = false;
            }
        }
    }
}
