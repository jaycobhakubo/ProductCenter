namespace GTI.Modules.ProductCenter.UI
{
    partial class ProductsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductsForm));
            this.productGroupBox = new System.Windows.Forms.GroupBox();
            this.m_chkShowInactive = new System.Windows.Forms.CheckBox();
            this.m_filteredByText = new System.Windows.Forms.Label();
            this.productListView = new GTI.Controls.GTIListView();
            this.productNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.productTypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.productGroupHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.salesSourceHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.paperLayoutHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IsActiveHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuProduct = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuAddProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuEditProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuCopyProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuPasteProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuDeleteProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuActivateProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuFilterProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.editMainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuAddProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuEditProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuCopyProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuPasteProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuDeleteProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuActivateProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.editProductGroupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productGroupBox.SuspendLayout();
            this.contextMenuProduct.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // productGroupBox
            // 
            this.productGroupBox.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.productGroupBox, "productGroupBox");
            this.productGroupBox.Controls.Add(this.m_chkShowInactive);
            this.productGroupBox.Controls.Add(this.m_filteredByText);
            this.productGroupBox.Controls.Add(this.productListView);
            this.productGroupBox.ForeColor = System.Drawing.Color.Black;
            this.productGroupBox.Name = "productGroupBox";
            this.productGroupBox.TabStop = false;
            // 
            // m_chkShowInactive
            // 
            resources.ApplyResources(this.m_chkShowInactive, "m_chkShowInactive");
            this.m_chkShowInactive.ForeColor = System.Drawing.Color.Black;
            this.m_chkShowInactive.Name = "m_chkShowInactive";
            this.m_chkShowInactive.UseVisualStyleBackColor = true;
            this.m_chkShowInactive.Click += new System.EventHandler(this.m_chkShowInactive_CheckedChanged);
            // 
            // m_filteredByText
            // 
            resources.ApplyResources(this.m_filteredByText, "m_filteredByText");
            this.m_filteredByText.Name = "m_filteredByText";
            // 
            // productListView
            // 
            this.productListView.AllowEraseBackground = true;
            resources.ApplyResources(this.productListView, "productListView");
            this.productListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.productNameHeader,
            this.productTypeHeader,
            this.productGroupHeader,
            this.salesSourceHeader,
            this.paperLayoutHeader,
            this.IsActiveHeader});
            this.productListView.ContextMenuStrip = this.contextMenuProduct;
            this.productListView.FullRowSelect = true;
            this.productListView.GridLines = true;
            this.productListView.HideSelection = false;
            this.productListView.MultiSelect = false;
            this.productListView.Name = "productListView";
            this.productListView.OwnerDraw = true;
            this.productListView.SelectedBackgroundColor = System.Drawing.Color.DarkSlateBlue;
            this.productListView.SortColumn = 0;
            this.productListView.UseCompatibleStateImageBehavior = false;
            this.productListView.View = System.Windows.Forms.View.Details;
            this.productListView.DoubleClick += new System.EventHandler(this.ProductListDoubleClick);
            this.productListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProductList_KeyDown);
            this.productListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.productListView_MouseClick);
            // 
            // productNameHeader
            // 
            this.productNameHeader.Tag = "alpha";
            resources.ApplyResources(this.productNameHeader, "productNameHeader");
            // 
            // productTypeHeader
            // 
            this.productTypeHeader.Tag = "alpha";
            resources.ApplyResources(this.productTypeHeader, "productTypeHeader");
            // 
            // productGroupHeader
            // 
            this.productGroupHeader.Tag = "alpha";
            resources.ApplyResources(this.productGroupHeader, "productGroupHeader");
            // 
            // salesSourceHeader
            // 
            this.salesSourceHeader.Tag = "alpha";
            resources.ApplyResources(this.salesSourceHeader, "salesSourceHeader");
            // 
            // paperLayoutHeader
            // 
            this.paperLayoutHeader.Tag = "alpha";
            resources.ApplyResources(this.paperLayoutHeader, "paperLayoutHeader");
            // 
            // IsActiveHeader
            // 
            resources.ApplyResources(this.IsActiveHeader, "IsActiveHeader");
            // 
            // contextMenuProduct
            // 
            resources.ApplyResources(this.contextMenuProduct, "contextMenuProduct");
            this.contextMenuProduct.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuAddProduct,
            this.contextMenuEditProduct,
            this.contextMenuCopyProduct,
            this.contextMenuPasteProduct,
            this.contextMenuDeleteProduct,
            this.contextMenuActivateProduct,
            this.toolStripSeparator1,
            this.contextMenuFilterProduct});
            this.contextMenuProduct.Name = "contextMenuProduct";
            // 
            // contextMenuAddProduct
            // 
            this.contextMenuAddProduct.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.contextMenuAddProduct.Name = "contextMenuAddProduct";
            resources.ApplyResources(this.contextMenuAddProduct, "contextMenuAddProduct");
            this.contextMenuAddProduct.Click += new System.EventHandler(this.AddProduct_Click);
            // 
            // contextMenuEditProduct
            // 
            this.contextMenuEditProduct.Name = "contextMenuEditProduct";
            resources.ApplyResources(this.contextMenuEditProduct, "contextMenuEditProduct");
            this.contextMenuEditProduct.Click += new System.EventHandler(this.EditProduct_Click);
            // 
            // contextMenuCopyProduct
            // 
            this.contextMenuCopyProduct.Name = "contextMenuCopyProduct";
            resources.ApplyResources(this.contextMenuCopyProduct, "contextMenuCopyProduct");
            this.contextMenuCopyProduct.Click += new System.EventHandler(this.CopyProduct_Click);
            // 
            // contextMenuPasteProduct
            // 
            this.contextMenuPasteProduct.Name = "contextMenuPasteProduct";
            resources.ApplyResources(this.contextMenuPasteProduct, "contextMenuPasteProduct");
            this.contextMenuPasteProduct.Click += new System.EventHandler(this.PasteProduct_Click);
            // 
            // contextMenuDeleteProduct
            // 
            this.contextMenuDeleteProduct.Name = "contextMenuDeleteProduct";
            resources.ApplyResources(this.contextMenuDeleteProduct, "contextMenuDeleteProduct");
            this.contextMenuDeleteProduct.Click += new System.EventHandler(this.DeleteProduct_Click);
            // 
            // contextMenuActivateProduct
            // 
            this.contextMenuActivateProduct.Name = "contextMenuActivateProduct";
            resources.ApplyResources(this.contextMenuActivateProduct, "contextMenuActivateProduct");
            this.contextMenuActivateProduct.Click += new System.EventHandler(this.contextMenuActivateProduct_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // contextMenuFilterProduct
            // 
            this.contextMenuFilterProduct.Name = "contextMenuFilterProduct";
            resources.ApplyResources(this.contextMenuFilterProduct, "contextMenuFilterProduct");
            this.contextMenuFilterProduct.Click += new System.EventHandler(this.ContextMenuFilterProduct_Click);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(222)))), ((int)(((byte)(237)))));
            resources.ApplyResources(this.mainMenuStrip, "mainMenuStrip");
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editMainMenu});
            this.mainMenuStrip.Name = "mainMenuStrip";
            // 
            // editMainMenu
            // 
            this.editMainMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editMenuAddProduct,
            this.editMenuEditProduct,
            this.editMenuCopyProduct,
            this.editMenuPasteProduct,
            this.editMenuDeleteProduct,
            this.editMenuActivateProduct,
            this.editProductGroupsToolStripMenuItem});
            this.editMainMenu.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.editMainMenu.MergeIndex = 1;
            this.editMainMenu.Name = "editMainMenu";
            resources.ApplyResources(this.editMainMenu, "editMainMenu");
            // 
            // editMenuAddProduct
            // 
            this.editMenuAddProduct.Name = "editMenuAddProduct";
            resources.ApplyResources(this.editMenuAddProduct, "editMenuAddProduct");
            this.editMenuAddProduct.Click += new System.EventHandler(this.AddProduct_Click);
            // 
            // editMenuEditProduct
            // 
            this.editMenuEditProduct.Name = "editMenuEditProduct";
            resources.ApplyResources(this.editMenuEditProduct, "editMenuEditProduct");
            this.editMenuEditProduct.Click += new System.EventHandler(this.EditProduct_Click);
            // 
            // editMenuCopyProduct
            // 
            this.editMenuCopyProduct.Name = "editMenuCopyProduct";
            resources.ApplyResources(this.editMenuCopyProduct, "editMenuCopyProduct");
            this.editMenuCopyProduct.Click += new System.EventHandler(this.CopyProduct_Click);
            // 
            // editMenuPasteProduct
            // 
            this.editMenuPasteProduct.Name = "editMenuPasteProduct";
            resources.ApplyResources(this.editMenuPasteProduct, "editMenuPasteProduct");
            this.editMenuPasteProduct.Click += new System.EventHandler(this.PasteProduct_Click);
            // 
            // editMenuDeleteProduct
            // 
            this.editMenuDeleteProduct.Name = "editMenuDeleteProduct";
            resources.ApplyResources(this.editMenuDeleteProduct, "editMenuDeleteProduct");
            this.editMenuDeleteProduct.Click += new System.EventHandler(this.DeleteProduct_Click);
            // 
            // editMenuActivateProduct
            // 
            this.editMenuActivateProduct.Name = "editMenuActivateProduct";
            resources.ApplyResources(this.editMenuActivateProduct, "editMenuActivateProduct");
            this.editMenuActivateProduct.Click += new System.EventHandler(this.contextMenuActivateProduct_Click);
            // 
            // editProductGroupsToolStripMenuItem
            // 
            this.editProductGroupsToolStripMenuItem.Name = "editProductGroupsToolStripMenuItem";
            resources.ApplyResources(this.editProductGroupsToolStripMenuItem, "editProductGroupsToolStripMenuItem");
            this.editProductGroupsToolStripMenuItem.Click += new System.EventHandler(this.editProductGroupsToolStripMenuItem_Click);
            // 
            // ProductsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.mainMenuStrip);
            this.Controls.Add(this.productGroupBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "ProductsForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.productGroupBox.ResumeLayout(false);
            this.productGroupBox.PerformLayout();
            this.contextMenuProduct.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.GroupBox productGroupBox;
        private GTI.Controls.GTIListView productListView;
        private System.Windows.Forms.ColumnHeader productNameHeader;
        private System.Windows.Forms.ColumnHeader productTypeHeader;
        private System.Windows.Forms.ColumnHeader salesSourceHeader;
        private System.Windows.Forms.ContextMenuStrip contextMenuProduct;
        private System.Windows.Forms.ToolStripMenuItem contextMenuAddProduct;
        private System.Windows.Forms.ToolStripMenuItem contextMenuEditProduct;
        private System.Windows.Forms.ToolStripMenuItem contextMenuDeleteProduct;
        private System.Windows.Forms.ToolStripMenuItem contextMenuCopyProduct;
        private System.Windows.Forms.ToolStripMenuItem contextMenuPasteProduct;
        private System.Windows.Forms.ToolStripMenuItem contextMenuFilterProduct;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editMainMenu;
        private System.Windows.Forms.ToolStripMenuItem editMenuAddProduct;
        private System.Windows.Forms.ToolStripMenuItem editMenuEditProduct;
        private System.Windows.Forms.ToolStripMenuItem editMenuCopyProduct;
        private System.Windows.Forms.ToolStripMenuItem editMenuPasteProduct;
        private System.Windows.Forms.ToolStripMenuItem editMenuDeleteProduct;
        private System.Windows.Forms.ColumnHeader productGroupHeader;
        private System.Windows.Forms.ToolStripMenuItem editProductGroupsToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader paperLayoutHeader;
        private System.Windows.Forms.Label m_filteredByText;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.CheckBox m_chkShowInactive;
        private System.Windows.Forms.ColumnHeader IsActiveHeader;
        private System.Windows.Forms.ToolStripMenuItem contextMenuActivateProduct;
        private System.Windows.Forms.ToolStripMenuItem editMenuActivateProduct;

    }
}