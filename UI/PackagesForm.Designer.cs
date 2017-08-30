namespace GTI.Modules.ProductCenter.UI
{
    partial class PackagesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackagesForm));
            this.packagesGroupBox = new System.Windows.Forms.GroupBox();
            this.m_filteredbyLabel = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treeViewPackages = new System.Windows.Forms.TreeView();
            this.contextMenuPackage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuAddPackage = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuEditPackage = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuCopyPackage = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuPastePackage = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuDeletePackage = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuPackageSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewProducts = new GTI.Controls.GTIListView();
            this.productNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.quantityHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.priceHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gameCategoryHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cardTypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cardLevelHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cardCountHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gameTypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.productTypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cardMediaHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.programGameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.isTaxedHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.salesSourceHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pointPerQuantityHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pointsPerDollarHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pointsToRedeemHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.numbersRequiredHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuProduct = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuAddProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuEditProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuCopyProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuPasteProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuDeleteProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.editMainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuAddPackage = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuEditPackage = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuCopyPackage = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuPastePackage = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuDeletePackage = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.editMenuAddProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuEditProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuCopyProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuPasteProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuDeleteProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.packagesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.contextMenuPackage.SuspendLayout();
            this.contextMenuProduct.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // packagesGroupBox
            // 
            this.packagesGroupBox.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.packagesGroupBox, "packagesGroupBox");
            this.packagesGroupBox.Controls.Add(this.m_filteredbyLabel);
            this.packagesGroupBox.Controls.Add(this.splitContainer);
            this.packagesGroupBox.Name = "packagesGroupBox";
            this.packagesGroupBox.TabStop = false;
            // 
            // m_filteredbyLabel
            // 
            resources.ApplyResources(this.m_filteredbyLabel, "m_filteredbyLabel");
            this.m_filteredbyLabel.Name = "m_filteredbyLabel";
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeViewPackages);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.listViewProducts);
            // 
            // treeViewPackages
            // 
            resources.ApplyResources(this.treeViewPackages, "treeViewPackages");
            this.treeViewPackages.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewPackages.ContextMenuStrip = this.contextMenuPackage;
            this.treeViewPackages.ForeColor = System.Drawing.SystemColors.WindowText;
            this.treeViewPackages.HideSelection = false;
            this.treeViewPackages.Name = "treeViewPackages";
            this.treeViewPackages.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(resources.GetObject("treeViewPackages.Nodes")))});
            this.treeViewPackages.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewPackagesAfterSelect);
            this.treeViewPackages.DoubleClick += new System.EventHandler(this.EditPackageClick);
            this.treeViewPackages.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeViewPackagesKeyDown);
            // 
            // contextMenuPackage
            // 
            this.contextMenuPackage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuAddPackage,
            this.contextMenuEditPackage,
            this.contextMenuCopyPackage,
            this.contextMenuPastePackage,
            this.contextMenuDeletePackage,
            this.contextMenuPackageSearch});
            this.contextMenuPackage.Name = "contextMenuPackage";
            resources.ApplyResources(this.contextMenuPackage, "contextMenuPackage");
            // 
            // contextMenuAddPackage
            // 
            this.contextMenuAddPackage.Name = "contextMenuAddPackage";
            resources.ApplyResources(this.contextMenuAddPackage, "contextMenuAddPackage");
            this.contextMenuAddPackage.Click += new System.EventHandler(this.AddPackageClick);
            // 
            // contextMenuEditPackage
            // 
            this.contextMenuEditPackage.Name = "contextMenuEditPackage";
            resources.ApplyResources(this.contextMenuEditPackage, "contextMenuEditPackage");
            this.contextMenuEditPackage.Click += new System.EventHandler(this.EditPackageClick);
            // 
            // contextMenuCopyPackage
            // 
            this.contextMenuCopyPackage.Name = "contextMenuCopyPackage";
            resources.ApplyResources(this.contextMenuCopyPackage, "contextMenuCopyPackage");
            this.contextMenuCopyPackage.Click += new System.EventHandler(this.CopyPackageClick);
            // 
            // contextMenuPastePackage
            // 
            this.contextMenuPastePackage.Name = "contextMenuPastePackage";
            resources.ApplyResources(this.contextMenuPastePackage, "contextMenuPastePackage");
            this.contextMenuPastePackage.Click += new System.EventHandler(this.PastePackageClick);
            // 
            // contextMenuDeletePackage
            // 
            this.contextMenuDeletePackage.Name = "contextMenuDeletePackage";
            resources.ApplyResources(this.contextMenuDeletePackage, "contextMenuDeletePackage");
            this.contextMenuDeletePackage.Click += new System.EventHandler(this.DeletePackageClick);
            // 
            // contextMenuPackageSearch
            // 
            this.contextMenuPackageSearch.Name = "contextMenuPackageSearch";
            resources.ApplyResources(this.contextMenuPackageSearch, "contextMenuPackageSearch");
            this.contextMenuPackageSearch.Click += new System.EventHandler(this.PackageSearchClick);
            // 
            // listViewProducts
            // 
            resources.ApplyResources(this.listViewProducts, "listViewProducts");
            this.listViewProducts.BackColor = System.Drawing.SystemColors.Window;
            this.listViewProducts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.productNameHeader,
            this.quantityHeader,
            this.priceHeader,
            this.gameCategoryHeader,
            this.cardTypeHeader,
            this.cardLevelHeader,
            this.cardCountHeader,
            this.gameTypeHeader,
            this.productTypeHeader,
            this.cardMediaHeader,
            this.programGameHeader,
            this.isTaxedHeader,
            this.salesSourceHeader,
            this.pointPerQuantityHeader,
            this.pointsPerDollarHeader,
            this.pointsToRedeemHeader,
            this.numbersRequiredHeader});
            this.listViewProducts.ContextMenuStrip = this.contextMenuProduct;
            this.listViewProducts.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listViewProducts.FullRowSelect = true;
            this.listViewProducts.GridLines = true;
            this.listViewProducts.HideSelection = false;
            this.listViewProducts.MultiSelect = false;
            this.listViewProducts.Name = "listViewProducts";
            this.listViewProducts.OwnerDraw = true;
            this.listViewProducts.SelectedBackgroundColor = System.Drawing.Color.DarkSlateBlue;
            this.listViewProducts.SortColumn = 0;
            this.listViewProducts.UseCompatibleStateImageBehavior = false;
            this.listViewProducts.View = System.Windows.Forms.View.Details;
            this.listViewProducts.DoubleClick += new System.EventHandler(this.EditProductClick);
            this.listViewProducts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListViewProductsKeyDown);
            // 
            // productNameHeader
            // 
            this.productNameHeader.Tag = "alpha";
            resources.ApplyResources(this.productNameHeader, "productNameHeader");
            // 
            // quantityHeader
            // 
            this.quantityHeader.Tag = "numeric";
            resources.ApplyResources(this.quantityHeader, "quantityHeader");
            // 
            // priceHeader
            // 
            this.priceHeader.Tag = "numeric";
            resources.ApplyResources(this.priceHeader, "priceHeader");
            // 
            // gameCategoryHeader
            // 
            this.gameCategoryHeader.Tag = "alpha";
            resources.ApplyResources(this.gameCategoryHeader, "gameCategoryHeader");
            // 
            // cardTypeHeader
            // 
            this.cardTypeHeader.Tag = "alpha";
            resources.ApplyResources(this.cardTypeHeader, "cardTypeHeader");
            // 
            // cardLevelHeader
            // 
            this.cardLevelHeader.Tag = "alpha";
            resources.ApplyResources(this.cardLevelHeader, "cardLevelHeader");
            // 
            // cardCountHeader
            // 
            this.cardCountHeader.Tag = "numeric";
            resources.ApplyResources(this.cardCountHeader, "cardCountHeader");
            // 
            // gameTypeHeader
            // 
            this.gameTypeHeader.Tag = "alpha";
            resources.ApplyResources(this.gameTypeHeader, "gameTypeHeader");
            // 
            // productTypeHeader
            // 
            this.productTypeHeader.Tag = "alpha";
            resources.ApplyResources(this.productTypeHeader, "productTypeHeader");
            // 
            // cardMediaHeader
            // 
            this.cardMediaHeader.Tag = "alpha";
            resources.ApplyResources(this.cardMediaHeader, "cardMediaHeader");
            // 
            // programGameHeader
            // 
            this.programGameHeader.Tag = "alpha";
            resources.ApplyResources(this.programGameHeader, "programGameHeader");
            // 
            // isTaxedHeader
            // 
            this.isTaxedHeader.Tag = "alpha";
            resources.ApplyResources(this.isTaxedHeader, "isTaxedHeader");
            // 
            // salesSourceHeader
            // 
            this.salesSourceHeader.Tag = "alpha";
            resources.ApplyResources(this.salesSourceHeader, "salesSourceHeader");
            // 
            // pointPerQuantityHeader
            // 
            this.pointPerQuantityHeader.Tag = "numeric";
            resources.ApplyResources(this.pointPerQuantityHeader, "pointPerQuantityHeader");
            // 
            // pointsPerDollarHeader
            // 
            this.pointsPerDollarHeader.Tag = "numeric";
            resources.ApplyResources(this.pointsPerDollarHeader, "pointsPerDollarHeader");
            // 
            // pointsToRedeemHeader
            // 
            this.pointsToRedeemHeader.Tag = "numeric";
            resources.ApplyResources(this.pointsToRedeemHeader, "pointsToRedeemHeader");
            // 
            // numbersRequiredHeader
            // 
            this.numbersRequiredHeader.Tag = "numeric";
            resources.ApplyResources(this.numbersRequiredHeader, "numbersRequiredHeader");
            // 
            // contextMenuProduct
            // 
            this.contextMenuProduct.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuAddProduct,
            this.contextMenuEditProduct,
            this.contextMenuCopyProduct,
            this.contextMenuPasteProduct,
            this.contextMenuDeleteProduct});
            this.contextMenuProduct.Name = "contextMenuProduct";
            resources.ApplyResources(this.contextMenuProduct, "contextMenuProduct");
            // 
            // contextMenuAddProduct
            // 
            this.contextMenuAddProduct.Name = "contextMenuAddProduct";
            resources.ApplyResources(this.contextMenuAddProduct, "contextMenuAddProduct");
            this.contextMenuAddProduct.Click += new System.EventHandler(this.AddProductClick);
            // 
            // contextMenuEditProduct
            // 
            this.contextMenuEditProduct.Name = "contextMenuEditProduct";
            resources.ApplyResources(this.contextMenuEditProduct, "contextMenuEditProduct");
            this.contextMenuEditProduct.Click += new System.EventHandler(this.EditProductClick);
            // 
            // contextMenuCopyProduct
            // 
            this.contextMenuCopyProduct.Name = "contextMenuCopyProduct";
            resources.ApplyResources(this.contextMenuCopyProduct, "contextMenuCopyProduct");
            this.contextMenuCopyProduct.Click += new System.EventHandler(this.CopyProductClick);
            // 
            // contextMenuPasteProduct
            // 
            this.contextMenuPasteProduct.Name = "contextMenuPasteProduct";
            resources.ApplyResources(this.contextMenuPasteProduct, "contextMenuPasteProduct");
            this.contextMenuPasteProduct.Click += new System.EventHandler(this.PasteProductClick);
            // 
            // contextMenuDeleteProduct
            // 
            this.contextMenuDeleteProduct.Name = "contextMenuDeleteProduct";
            resources.ApplyResources(this.contextMenuDeleteProduct, "contextMenuDeleteProduct");
            this.contextMenuDeleteProduct.Click += new System.EventHandler(this.DeleteProductClick);
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
            this.editMenuAddPackage,
            this.editMenuEditPackage,
            this.editMenuCopyPackage,
            this.editMenuPastePackage,
            this.editMenuDeletePackage,
            this.editMenuSeparator,
            this.editMenuAddProduct,
            this.editMenuEditProduct,
            this.editMenuCopyProduct,
            this.editMenuPasteProduct,
            this.editMenuDeleteProduct});
            this.editMainMenu.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.editMainMenu.MergeIndex = 1;
            this.editMainMenu.Name = "editMainMenu";
            resources.ApplyResources(this.editMainMenu, "editMainMenu");
            // 
            // editMenuAddPackage
            // 
            this.editMenuAddPackage.Name = "editMenuAddPackage";
            resources.ApplyResources(this.editMenuAddPackage, "editMenuAddPackage");
            this.editMenuAddPackage.Click += new System.EventHandler(this.AddPackageClick);
            // 
            // editMenuEditPackage
            // 
            this.editMenuEditPackage.Name = "editMenuEditPackage";
            resources.ApplyResources(this.editMenuEditPackage, "editMenuEditPackage");
            this.editMenuEditPackage.Click += new System.EventHandler(this.EditPackageClick);
            // 
            // editMenuCopyPackage
            // 
            this.editMenuCopyPackage.Name = "editMenuCopyPackage";
            resources.ApplyResources(this.editMenuCopyPackage, "editMenuCopyPackage");
            this.editMenuCopyPackage.Click += new System.EventHandler(this.CopyPackageClick);
            // 
            // editMenuPastePackage
            // 
            this.editMenuPastePackage.Name = "editMenuPastePackage";
            resources.ApplyResources(this.editMenuPastePackage, "editMenuPastePackage");
            this.editMenuPastePackage.Click += new System.EventHandler(this.PastePackageClick);
            // 
            // editMenuDeletePackage
            // 
            this.editMenuDeletePackage.Name = "editMenuDeletePackage";
            resources.ApplyResources(this.editMenuDeletePackage, "editMenuDeletePackage");
            this.editMenuDeletePackage.Click += new System.EventHandler(this.DeletePackageClick);
            // 
            // editMenuSeparator
            // 
            this.editMenuSeparator.Name = "editMenuSeparator";
            resources.ApplyResources(this.editMenuSeparator, "editMenuSeparator");
            // 
            // editMenuAddProduct
            // 
            this.editMenuAddProduct.Name = "editMenuAddProduct";
            resources.ApplyResources(this.editMenuAddProduct, "editMenuAddProduct");
            this.editMenuAddProduct.Click += new System.EventHandler(this.AddProductClick);
            // 
            // editMenuEditProduct
            // 
            this.editMenuEditProduct.Name = "editMenuEditProduct";
            resources.ApplyResources(this.editMenuEditProduct, "editMenuEditProduct");
            this.editMenuEditProduct.Click += new System.EventHandler(this.EditProductClick);
            // 
            // editMenuCopyProduct
            // 
            this.editMenuCopyProduct.Name = "editMenuCopyProduct";
            resources.ApplyResources(this.editMenuCopyProduct, "editMenuCopyProduct");
            this.editMenuCopyProduct.Click += new System.EventHandler(this.CopyProductClick);
            // 
            // editMenuPasteProduct
            // 
            this.editMenuPasteProduct.Name = "editMenuPasteProduct";
            resources.ApplyResources(this.editMenuPasteProduct, "editMenuPasteProduct");
            this.editMenuPasteProduct.Click += new System.EventHandler(this.PasteProductClick);
            // 
            // editMenuDeleteProduct
            // 
            this.editMenuDeleteProduct.Name = "editMenuDeleteProduct";
            resources.ApplyResources(this.editMenuDeleteProduct, "editMenuDeleteProduct");
            this.editMenuDeleteProduct.Click += new System.EventHandler(this.DeleteProductClick);
            // 
            // PackagesForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.mainMenuStrip);
            this.Controls.Add(this.packagesGroupBox);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "PackagesForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.packagesGroupBox.ResumeLayout(false);
            this.packagesGroupBox.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.contextMenuPackage.ResumeLayout(false);
            this.contextMenuProduct.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox packagesGroupBox;
        private System.Windows.Forms.TreeView treeViewPackages;
        private GTI.Controls.GTIListView listViewProducts;
        private System.Windows.Forms.ColumnHeader productNameHeader;
        private System.Windows.Forms.ColumnHeader productTypeHeader;
        private System.Windows.Forms.ColumnHeader salesSourceHeader;
        private System.Windows.Forms.ColumnHeader gameCategoryHeader;
        private System.Windows.Forms.ColumnHeader quantityHeader;
        private System.Windows.Forms.ColumnHeader priceHeader;
        private System.Windows.Forms.ColumnHeader pointsToRedeemHeader;
        private System.Windows.Forms.ColumnHeader isTaxedHeader;
        private System.Windows.Forms.ColumnHeader gameTypeHeader;
        private System.Windows.Forms.ColumnHeader cardLevelHeader;
        private System.Windows.Forms.ColumnHeader cardMediaHeader;
        private System.Windows.Forms.ColumnHeader cardTypeHeader;
        private System.Windows.Forms.ColumnHeader programGameHeader;
        private System.Windows.Forms.ColumnHeader cardCountHeader;
        private System.Windows.Forms.ColumnHeader pointPerQuantityHeader;
        private System.Windows.Forms.ColumnHeader pointsPerDollarHeader;
        private System.Windows.Forms.ColumnHeader numbersRequiredHeader;
        private System.Windows.Forms.ContextMenuStrip contextMenuProduct;
        private System.Windows.Forms.ToolStripMenuItem contextMenuAddProduct;
        private System.Windows.Forms.ToolStripMenuItem contextMenuEditProduct;
        private System.Windows.Forms.ToolStripMenuItem contextMenuCopyProduct;
        private System.Windows.Forms.ToolStripMenuItem contextMenuPasteProduct;
        private System.Windows.Forms.ToolStripMenuItem contextMenuDeleteProduct;
        private System.Windows.Forms.ContextMenuStrip contextMenuPackage;
        private System.Windows.Forms.ToolStripMenuItem contextMenuAddPackage;
        private System.Windows.Forms.ToolStripMenuItem contextMenuEditPackage;
        private System.Windows.Forms.ToolStripMenuItem contextMenuCopyPackage;
        private System.Windows.Forms.ToolStripMenuItem contextMenuPastePackage;
        private System.Windows.Forms.ToolStripMenuItem contextMenuDeletePackage;
        private System.Windows.Forms.ToolStripMenuItem contextMenuPackageSearch;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editMainMenu;
        private System.Windows.Forms.ToolStripMenuItem editMenuAddPackage;
        private System.Windows.Forms.ToolStripMenuItem editMenuEditPackage;
        private System.Windows.Forms.ToolStripMenuItem editMenuCopyPackage;
        private System.Windows.Forms.ToolStripMenuItem editMenuPastePackage;
        private System.Windows.Forms.ToolStripMenuItem editMenuDeletePackage;
        private System.Windows.Forms.ToolStripSeparator editMenuSeparator;
        private System.Windows.Forms.ToolStripMenuItem editMenuAddProduct;
        private System.Windows.Forms.ToolStripMenuItem editMenuEditProduct;
        private System.Windows.Forms.ToolStripMenuItem editMenuCopyProduct;
        private System.Windows.Forms.ToolStripMenuItem editMenuPasteProduct;
        private System.Windows.Forms.ToolStripMenuItem editMenuDeleteProduct;
        private System.Windows.Forms.Label m_filteredbyLabel;

    }
}