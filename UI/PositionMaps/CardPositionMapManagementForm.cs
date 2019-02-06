using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GTI.Modules.ProductCenter.Business;
using GTI.Modules.Shared;
using GTI.Modules.Shared.Business;
using GTI.Modules.Shared.Data;

namespace GTI.Modules.ProductCenter.UI.PositionMaps
{
    public partial class CardPositionMapManagementForm : GradientForm
    {
        private const int ColumnIndex_MapName = 0;
        private const int ColumnIndex_AllowElec = 1;
        private const int ColumnIndex_GUID = 2;

        private bool m_isModified;
        private OpenFileDialog m_ofd = null;

        public CardPositionMapManagementForm()
        {
            InitializeComponent();

            var filter = String.Format("Position Map File (*.{0})|*.{0}|All Files (*.*)|*.*", PositionMapFile.StandardExtension);
            m_ofd = new OpenFileDialog();
            m_ofd.DefaultExt = PositionMapFile.StandardExtension;
            m_ofd.Filter = filter;

            RemoveCardPositionsMapMessage.RemoveAllNonfinalCardPositionsMaps();

            LoadMaps();
        }

        public void LoadMaps()
        {
            var starPositionMaps = GetCardPositionMapsMessage.GetPositionMaps();
            positionMapsDGV.Rows.Clear();

            positionMapsDGV.SuspendLayout();
            var c = positionMapsDGV.Columns[ColumnIndex_AllowElec] as DataGridViewCheckBoxColumn;
            c.TrueValue = true;
            c.FalseValue = false;

            var guidC = positionMapsDGV.Columns[ColumnIndex_GUID];
            guidC.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;

            foreach(var pm in starPositionMaps)
            {
                var i = positionMapsDGV.Rows.Add(pm.PositionMapName, pm.IsActive, pm.PositionMapGUID);
                var r = positionMapsDGV.Rows[i];
                r.Tag = pm;
            }
            positionMapsDGV.ResumeLayout();
        }

        public bool IsModified
        {
            get { return m_isModified; }
            set
            {
                if(m_isModified != value)
                {
                    m_isModified = value;
                    saveCmd.Enabled = m_isModified;
                    revertCmd.Enabled = m_isModified;
                    importCmd.Enabled = !m_isModified;
                }
            }
        }

        private void RefreshIsModified()
        {
            bool differenceFound = false;
            foreach(DataGridViewRow r in positionMapsDGV.Rows)
            {
                var cpm = r.Tag as CardPositionMapHandle;
                if(cpm == null)
                    r.ErrorText = "Position Map Update Failure";
                else
                {
                    var nameCellValue = r.Cells[ColumnIndex_MapName].Value.ToString();
                    bool allowElecCellValue = Convert.ToBoolean(r.Cells[ColumnIndex_AllowElec].Value);

                    if(cpm.PositionMapName != nameCellValue
                        || cpm.IsActive != allowElecCellValue
                    )
                    {
                        differenceFound = true;
                        break;
                    }
                }
            }

            IsModified = differenceFound;
        }

        private void revertCmd_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow r in positionMapsDGV.Rows)
            {
                var cpm = r.Tag as CardPositionMapHandle;
                if(cpm == null)
                {
                    r.Cells[ColumnIndex_MapName].Value = "[MISSING]";
                    r.Cells[ColumnIndex_AllowElec].Value = false;
                    r.Cells[ColumnIndex_GUID].Value = "";
                }
                else
                {
                    r.Cells[ColumnIndex_MapName].Value = cpm.PositionMapName;
                    r.Cells[ColumnIndex_AllowElec].Value = cpm.IsActive;
                    r.Cells[ColumnIndex_GUID].Value = cpm.PositionMapGUID;
                }
            }
            RefreshIsModified();
        }

        private void positionMapsDGV_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if(positionMapsDGV.IsCurrentCellDirty)
            {
                DataGridViewDataErrorContexts dummy = new DataGridViewDataErrorContexts();
                positionMapsDGV.CommitEdit(dummy);
            }
        }

        private void positionMapsDGV_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            RefreshIsModified();
        }

        private void saveCmd_Click(object sender, EventArgs e)
        {
            try
            {
                foreach(DataGridViewRow r in positionMapsDGV.Rows)
                {
                    var map = r.Tag as CardPositionMapHandle;
                    var nameCellValue = r.Cells[ColumnIndex_MapName].Value.ToString();
                    bool allowElecCellValue = Convert.ToBoolean(r.Cells[ColumnIndex_AllowElec].Value);

                    if(map.PositionMapName != nameCellValue || map.IsActive != allowElecCellValue)
                    {
                        var cpm = new CardPositionMapHandle();
                        cpm.LoadFrom(map);
                        cpm.PositionMapName = nameCellValue;
                        cpm.IsActive = allowElecCellValue;
                        r.Tag = SetCardPositionMapInfoMessage.SetCardPositionMapInfo(cpm);
                    }
                }
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show("", "", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                LoadMaps();
            }
            finally
            {
                RefreshIsModified();
            }
        }

        private class ImportSpecification
        {
            public String Name;
            public PositionMapFile SourceMap;
        }

        private void importCmd_Click(object sender, EventArgs e)
        {
            var ofdResult = m_ofd.ShowDialog();
            if(ofdResult == System.Windows.Forms.DialogResult.Cancel)
                return;

            var fName = m_ofd.FileName;
            string loadError;
            var sourceMap = PositionMapFile.FromFile(fName, out loadError);

            if(sourceMap == null)
            {
                MessageBox.Show(loadError, "Error Loading Map File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult dr;

            string name = null;
            var tPrompt = new TextEntryForm();
            tPrompt.Text = "Set Position Map Name";
            tPrompt.Description = "Enter the name that will be used for the position map.";
            tPrompt.TextResult = System.IO.Path.GetFileNameWithoutExtension(sourceMap.FileName);
            dr = tPrompt.ShowDialog();
            if(dr != System.Windows.Forms.DialogResult.OK)
                return;
            else
                name = tPrompt.TextResult;

            if(String.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show(loadError, "Position Maps must be given names", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach(DataGridViewRow r in positionMapsDGV.Rows)
                if((r.Tag as CardPositionMapHandle).PositionMapName.ToLower() == name.ToLower())
                {
                    MessageBox.Show(loadError, "Position Maps must be given unique names", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            if(MessageBox.Show("Warning: Importing position maps can take some time, " + Environment.NewLine
                                + "and put an additional load on the server and database." + Environment.NewLine + Environment.NewLine
                                + "Are you sure you want to continue this process now?"
                                , "Confirmation Required"
                                , MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) 
                == System.Windows.Forms.DialogResult.No
                )
                return;

            var importSpec = new ImportSpecification() { Name = name, SourceMap = sourceMap };

            importBGW.WorkerSupportsCancellation = true;
            var importProgressForm = new ProgressPopup(importBGW, importSpec, String.Format("Importing Position Map from {0}", fName));
            dr = importProgressForm.ShowDialog();

            //System.Windows.MessageBox.Show(String.Format("Final Result: {0}\r\nDialogResult: {1}", importProgressForm.Result, dr));

            LoadMaps();
        }

        private void importBGW_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var startTime = DateTime.Now;
            var reportingInterval = new TimeSpan(0, 0, 0, 0, 500);
            var nextReportAt = startTime;

            var bgw = sender as System.ComponentModel.BackgroundWorker;
            var spec = e.Argument as ImportSpecification;
            var specMap = spec.SourceMap as PositionMapFile;

            var intendedCPMH = new CardPositionMapHandle();
            intendedCPMH.PositionMapName = spec.Name;
            intendedCPMH.IsActive = false;
            intendedCPMH.PositionsCovered = specMap.MapSpace;
            intendedCPMH.SequenceLength = specMap.MapSpace;
            intendedCPMH.NumSequences = specMap.NumSequences;
            intendedCPMH.PositionMapGUID = specMap.MapId.ToString();
            intendedCPMH.PositionMapPath = specMap.FileName;

            CardPositionMapHandle resultingCPMH = null;
            try
            {
                resultingCPMH = SetCardPositionMapInfoMessage.SetCardPositionMapInfo(intendedCPMH);
            }
            catch
            {
                resultingCPMH = null;
            }

            if(resultingCPMH == null)
            {
                e.Result = null;
                return;
            }

            int failures = 0;

            int seqPerMsg = 1024;
            for(int seqNum = 0; seqNum < specMap.NumSequences && failures == 0 && !bgw.CancellationPending; seqNum += seqPerMsg)
            {
                try
                {
                    var sequences = specMap.GetSequences(seqNum, seqPerMsg);
                    var normSeqs = new List<byte[]>();
                    foreach(var seq in sequences)
                        normSeqs.Add(seq.GetNormalizedSequence());

                    SetCardPositionMapSequencesMessage.SetCardPositionMapSequences(resultingCPMH.Id, seqNum, normSeqs);

                    try
                    {
                        if(DateTime.Now > nextReportAt)
                        {
                            var seqCompleted = seqNum + seqPerMsg;
                            var reportTime = DateTime.Now;
                            var ticksPerSeq = (reportTime - startTime).Ticks / seqCompleted;
                            var etr = new TimeSpan(ticksPerSeq * (specMap.NumSequences - seqCompleted));
                            var te = reportTime - startTime;
                            bgw.ReportProgress(100 * seqNum / specMap.NumSequences
                                , String.Format("{0} of {1} sequences imported.", seqNum, specMap.NumSequences) + Environment.NewLine
                                + String.Format("Estimated time remaining: {0:d2}:{1:d2}:{2:d2}", etr.Hours, etr.Minutes, etr.Seconds) + Environment.NewLine
                                + String.Format("Time elapse: {0:d2}:{1:d2}:{2:d2}", te.Hours, te.Minutes, te.Seconds)
                                );
                            nextReportAt = DateTime.Now + reportingInterval;
                        }
                    }
                    catch
                    {
                        System.Diagnostics.Debug.Print("Progress reporting error");
                    }
                }
                catch { failures++; }
            }

            if(bgw.CancellationPending || failures != 0)
            {
                RemoveCardPositionsMapMessage.RemoveCardPositionsMap(resultingCPMH.Id);
                e.Cancel = bgw.CancellationPending;
                e.Result = null;
            }
            else
            {
                FinalizeCardPositionMapMessage.FinalizeCardPositionMap(resultingCPMH.Id);
                e.Result = resultingCPMH;
            }
        }
    }
}
