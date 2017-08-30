namespace GTI.Modules.ProductCenter.UI
{
    partial class CardLevelForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardLevelForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ListViewCardLevels = new GTI.Controls.GTIListView();
            this.cardLevelNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cardLevelMultiplierHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cardLevelColorHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuProduct = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddContextButton = new System.Windows.Forms.ToolStripMenuItem();
            this.EditContextButton = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyContextButton = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteContextButton = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteContextButton = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.SystemEditMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SystemAddCardLevelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SystemEditCardLevelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SystemCopyCardLevelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SystemPasteCardLevelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SystemDeleteCardLevelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.contextMenuProduct.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.ListViewCardLevels);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // ListViewCardLevels
            // 
            this.ListViewCardLevels.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cardLevelNameHeader,
            this.cardLevelMultiplierHeader,
            this.cardLevelColorHeader});
            this.ListViewCardLevels.ContextMenuStrip = this.contextMenuProduct;
            resources.ApplyResources(this.ListViewCardLevels, "ListViewCardLevels");
            this.ListViewCardLevels.FullRowSelect = true;
            this.ListViewCardLevels.GridLines = true;
            this.ListViewCardLevels.HideSelection = false;
            this.ListViewCardLevels.MultiSelect = false;
            this.ListViewCardLevels.Name = "ListViewCardLevels";
            this.ListViewCardLevels.OwnerDraw = true;
            this.ListViewCardLevels.SelectedBackgroundColor = System.Drawing.Color.DarkSlateBlue;
            this.ListViewCardLevels.SortColumn = 0;
            this.ListViewCardLevels.UseCompatibleStateImageBehavior = false;
            this.ListViewCardLevels.View = System.Windows.Forms.View.Details;
            this.ListViewCardLevels.DoubleClick += new System.EventHandler(this.EditLevelClick);
            this.ListViewCardLevels.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListViewCardLevelsKeyDown);
            // 
            // cardLevelNameHeader
            // 
            this.cardLevelNameHeader.Tag = "alpha";
            resources.ApplyResources(this.cardLevelNameHeader, "cardLevelNameHeader");
            // 
            // cardLevelMultiplierHeader
            // 
            this.cardLevelMultiplierHeader.Tag = "numeric";
            resources.ApplyResources(this.cardLevelMultiplierHeader, "cardLevelMultiplierHeader");
            // 
            // cardLevelColorHeader
            // 
            resources.ApplyResources(this.cardLevelColorHeader, "cardLevelColorHeader");
            // 
            // contextMenuProduct
            // 
            resources.ApplyResources(this.contextMenuProduct, "contextMenuProduct");
            this.contextMenuProduct.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddContextButton,
            this.EditContextButton,
            this.CopyContextButton,
            this.PasteContextButton,
            this.DeleteContextButton});
            this.contextMenuProduct.Name = "contextMenuProduct";
            // 
            // AddContextButton
            // 
            this.AddContextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AddContextButton.Name = "AddContextButton";
            resources.ApplyResources(this.AddContextButton, "AddContextButton");
            this.AddContextButton.Click += new System.EventHandler(this.AddLevelClick);
            // 
            // EditContextButton
            // 
            this.EditContextButton.Name = "EditContextButton";
            resources.ApplyResources(this.EditContextButton, "EditContextButton");
            this.EditContextButton.Click += new System.EventHandler(this.EditLevelClick);
            // 
            // CopyContextButton
            // 
            this.CopyContextButton.Name = "CopyContextButton";
            resources.ApplyResources(this.CopyContextButton, "CopyContextButton");
            this.CopyContextButton.Click += new System.EventHandler(this.CopyLevelClick);
            // 
            // PasteContextButton
            // 
            this.PasteContextButton.Name = "PasteContextButton";
            resources.ApplyResources(this.PasteContextButton, "PasteContextButton");
            this.PasteContextButton.Click += new System.EventHandler(this.PasteLevelClick);
            // 
            // DeleteContextButton
            // 
            this.DeleteContextButton.Name = "DeleteContextButton";
            resources.ApplyResources(this.DeleteContextButton, "DeleteContextButton");
            this.DeleteContextButton.Click += new System.EventHandler(this.DeleteLevelClick);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(222)))), ((int)(((byte)(237)))));
            resources.ApplyResources(this.mainMenuStrip, "mainMenuStrip");
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SystemEditMenu});
            this.mainMenuStrip.Name = "mainMenuStrip";
            // 
            // SystemEditMenu
            // 
            this.SystemEditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SystemAddCardLevelMenuItem,
            this.SystemEditCardLevelMenuItem,
            this.SystemCopyCardLevelMenuItem,
            this.SystemPasteCardLevelMenuItem,
            this.SystemDeleteCardLevelMenuItem});
            this.SystemEditMenu.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.SystemEditMenu.MergeIndex = 1;
            this.SystemEditMenu.Name = "SystemEditMenu";
            resources.ApplyResources(this.SystemEditMenu, "SystemEditMenu");
            // 
            // SystemAddCardLevelMenuItem
            // 
            this.SystemAddCardLevelMenuItem.Name = "SystemAddCardLevelMenuItem";
            resources.ApplyResources(this.SystemAddCardLevelMenuItem, "SystemAddCardLevelMenuItem");
            this.SystemAddCardLevelMenuItem.Click += new System.EventHandler(this.AddLevelClick);
            // 
            // SystemEditCardLevelMenuItem
            // 
            this.SystemEditCardLevelMenuItem.Name = "SystemEditCardLevelMenuItem";
            resources.ApplyResources(this.SystemEditCardLevelMenuItem, "SystemEditCardLevelMenuItem");
            this.SystemEditCardLevelMenuItem.Click += new System.EventHandler(this.EditLevelClick);
            // 
            // SystemCopyCardLevelMenuItem
            // 
            this.SystemCopyCardLevelMenuItem.Name = "SystemCopyCardLevelMenuItem";
            resources.ApplyResources(this.SystemCopyCardLevelMenuItem, "SystemCopyCardLevelMenuItem");
            this.SystemCopyCardLevelMenuItem.Click += new System.EventHandler(this.CopyLevelClick);
            // 
            // SystemPasteCardLevelMenuItem
            // 
            this.SystemPasteCardLevelMenuItem.Name = "SystemPasteCardLevelMenuItem";
            resources.ApplyResources(this.SystemPasteCardLevelMenuItem, "SystemPasteCardLevelMenuItem");
            this.SystemPasteCardLevelMenuItem.Click += new System.EventHandler(this.PasteLevelClick);
            // 
            // SystemDeleteCardLevelMenuItem
            // 
            this.SystemDeleteCardLevelMenuItem.Name = "SystemDeleteCardLevelMenuItem";
            resources.ApplyResources(this.SystemDeleteCardLevelMenuItem, "SystemDeleteCardLevelMenuItem");
            this.SystemDeleteCardLevelMenuItem.Click += new System.EventHandler(this.DeleteLevelClick);
            // 
            // CardLevelForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.mainMenuStrip);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "CardLevelForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.groupBox1.ResumeLayout(false);
            this.contextMenuProduct.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private GTI.Controls.GTIListView ListViewCardLevels;
        private System.Windows.Forms.ColumnHeader cardLevelNameHeader;
        private System.Windows.Forms.ColumnHeader cardLevelMultiplierHeader;
        private System.Windows.Forms.ColumnHeader cardLevelColorHeader;
        private System.Windows.Forms.ContextMenuStrip contextMenuProduct;
        private System.Windows.Forms.ToolStripMenuItem AddContextButton;
        private System.Windows.Forms.ToolStripMenuItem EditContextButton;
        private System.Windows.Forms.ToolStripMenuItem DeleteContextButton;
        private System.Windows.Forms.ToolStripMenuItem CopyContextButton;
        private System.Windows.Forms.ToolStripMenuItem PasteContextButton;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem SystemEditMenu;
        private System.Windows.Forms.ToolStripMenuItem SystemAddCardLevelMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SystemEditCardLevelMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SystemCopyCardLevelMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SystemPasteCardLevelMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SystemDeleteCardLevelMenuItem;

    }
}