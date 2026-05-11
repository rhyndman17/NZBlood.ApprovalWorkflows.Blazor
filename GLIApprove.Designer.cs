
namespace NZBlood.ApprovalWorkflowsWI
{
    partial class GLIApprove
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

        #region Wisej.NET Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Wisej.Web.DataGridViewCellStyle dataGridViewCellStyle1 = new Wisej.Web.DataGridViewCellStyle();
            Wisej.Web.DataGridViewCellStyle dataGridViewCellStyle2 = new Wisej.Web.DataGridViewCellStyle();
            this.tbGLIApproval = new Wisej.Web.ToolBar();
            this.tbHome = new Wisej.Web.ToolBarButton();
            this.tbApprove = new Wisej.Web.ToolBarButton();
            this.tbReject = new Wisej.Web.ToolBarButton();
            this.tbReset = new Wisej.Web.ToolBarButton();
            this.txtReference = new Wisej.Web.TextBox();
            this.lblReference = new Wisej.Web.Label();
            this.lblDashboardHeading = new Wisej.Web.Label();
            this.lblAdditionalComments = new Wisej.Web.Label();
            this.txtComments = new Wisej.Web.TextBox();
            this.txtVendorName = new Wisej.Web.TextBox();
            this.txtVendorID = new Wisej.Web.TextBox();
            this.lblVendorID = new Wisej.Web.Label();
            this.txtVendorDocNo = new Wisej.Web.TextBox();
            this.lblVendorDocNo = new Wisej.Web.Label();
            this.lvAttachments = new Wisej.Web.ListView();
            this._fileName = new Wisej.Web.ColumnHeader();
            this._attachmentId = new Wisej.Web.ColumnHeader();
            this.lblPurchaseAmount = new Wisej.Web.Label();
            this.txtPurchaseAmount = new Wisej.Web.TextBox();
            this.txtApproverComments = new Wisej.Web.TextBox();
            this.lblApproverUseOnly = new Wisej.Web.Label();
            this.label1 = new Wisej.Web.Label();
            this.lvAccountList = new Wisej.Web.ListViewComboBox();
            this.account = new Wisej.Web.ColumnHeader();
            this.accountDescription = new Wisej.Web.ColumnHeader();
            this.txtAccountDescription = new Wisej.Web.TextBox();
            this.lblAccount = new Wisej.Web.Label();
            this.lblCapexNo = new Wisej.Web.Label();
            this.lblAmount = new Wisej.Web.Label();
            this.cmdAddAccount = new Wisej.Web.Button();
            this.txtAmount = new Wisej.Web.TypedTextBox();
            this.dgDistributions = new Wisej.Web.DataGridView();
            this._account = new Wisej.Web.DataGridViewTextBoxColumn();
            this._accountDescription = new Wisej.Web.DataGridViewTextBoxColumn();
            this.CapexNo = new Wisej.Web.DataGridViewTextBoxColumn();
            this.Reference = new Wisej.Web.DataGridViewTextBoxColumn();
            this._amount = new Wisej.Web.DataGridViewTextBoxColumn();
            this._delete = new Wisej.Web.DataGridViewImageColumn();
            this.label2 = new Wisej.Web.Label();
            this.txtTotalAmount = new Wisej.Web.TextBox();
            this.txtRC = new Wisej.Web.TextBox();
            this.label3 = new Wisej.Web.Label();
            this.txtFilter = new Wisej.Web.TextBox();
            this.lblFilter = new Wisej.Web.Label();
            this._imgExclamation = new Wisej.Web.Button();
            this._imgCheck = new Wisej.Web.Button();
            this.line1 = new Wisej.Web.Line();
            this.sbcUserName = new Wisej.Web.TextBox();
            this.lblAttachments = new Wisej.Web.Label();
            this.cmdAddRemaining = new Wisej.Web.Button();
            this.lblGLReference = new Wisej.Web.Label();
            this.txtCapexNo = new Wisej.Web.TextBox();
            this.txtGLReference = new Wisej.Web.TextBox();
            this.errorProv = new Wisej.Web.ErrorProvider(this.components);
            this.cboRCList = new Wisej.Web.ListViewComboBox();
            this.lblFilterCaption = new Wisej.Web.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgDistributions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProv)).BeginInit();
            this.SuspendLayout();
            // 
            // tbGLIApproval
            // 
            this.tbGLIApproval.AutoSize = false;
            this.tbGLIApproval.Buttons.AddRange(new Wisej.Web.ToolBarButton[] {
            this.tbHome,
            this.tbApprove,
            this.tbReject,
            this.tbReset});
            this.tbGLIApproval.Location = new System.Drawing.Point(0, 0);
            this.tbGLIApproval.Name = "tbGLIApproval";
            this.tbGLIApproval.Size = new System.Drawing.Size(1477, 51);
            this.tbGLIApproval.TabIndex = 1;
            this.tbGLIApproval.TabStop = false;
            this.tbGLIApproval.ButtonClick += new Wisej.Web.ToolBarButtonClickEventHandler(this.tbGLIApproval_ButtonClick);
            // 
            // tbHome
            // 
            this.tbHome.AutoShowLoader = true;
            this.tbHome.ImageSource = "resource.wx/Wisej.Ext.BootstrapIcons/clipboard-data.svg";
            this.tbHome.Name = "tbHome";
            this.tbHome.Text = "Home";
            // 
            // tbApprove
            // 
            this.tbApprove.ImageSource = "resource.wx/Wisej.Ext.BootstrapIcons/card-checklist.svg";
            this.tbApprove.Name = "tbApprove";
            this.tbApprove.Text = "Approve";
            // 
            // tbReject
            // 
            this.tbReject.ImageSource = "resource.wx/Wisej.Ext.BootstrapIcons/exclamation-triangle-fill.svg";
            this.tbReject.Name = "tbReject";
            this.tbReject.Text = "Reject";
            // 
            // tbReset
            // 
            this.tbReset.ImageSource = "resource.wx/Wisej.Ext.BootstrapIcons/hammer.svg";
            this.tbReset.Name = "tbReset";
            this.tbReset.Text = "Reset";
            // 
            // txtReference
            // 
            this.txtReference.BackColor = System.Drawing.Color.FromName("@table-row-background-odd");
            this.txtReference.Focusable = false;
            this.txtReference.Font = new System.Drawing.Font("default", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.txtReference.Location = new System.Drawing.Point(129, 91);
            this.txtReference.Name = "txtReference";
            this.txtReference.ReadOnly = true;
            this.txtReference.Size = new System.Drawing.Size(103, 30);
            this.txtReference.TabIndex = 15;
            this.txtReference.TabStop = false;
            // 
            // lblReference
            // 
            this.lblReference.AutoSize = true;
            this.lblReference.Location = new System.Drawing.Point(22, 94);
            this.lblReference.Name = "lblReference";
            this.lblReference.Size = new System.Drawing.Size(64, 18);
            this.lblReference.TabIndex = 14;
            this.lblReference.Text = "Reference:";
            // 
            // lblDashboardHeading
            // 
            this.lblDashboardHeading.AutoSize = true;
            this.lblDashboardHeading.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblDashboardHeading.Location = new System.Drawing.Point(22, 57);
            this.lblDashboardHeading.Name = "lblDashboardHeading";
            this.lblDashboardHeading.Size = new System.Drawing.Size(284, 25);
            this.lblDashboardHeading.TabIndex = 13;
            this.lblDashboardHeading.Text = "Invoice Approval Review";
            // 
            // lblAdditionalComments
            // 
            this.lblAdditionalComments.Location = new System.Drawing.Point(23, 223);
            this.lblAdditionalComments.Name = "lblAdditionalComments";
            this.lblAdditionalComments.Size = new System.Drawing.Size(87, 42);
            this.lblAdditionalComments.TabIndex = 26;
            this.lblAdditionalComments.Text = "Additional Comments:";
            // 
            // txtComments
            // 
            this.txtComments.BackColor = System.Drawing.Color.FromName("@table-row-background-odd");
            this.txtComments.Focusable = false;
            this.txtComments.Font = new System.Drawing.Font("default", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.txtComments.ForeColor = System.Drawing.Color.DarkRed;
            this.txtComments.Location = new System.Drawing.Point(129, 223);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.ReadOnly = true;
            this.txtComments.Size = new System.Drawing.Size(574, 68);
            this.txtComments.TabIndex = 27;
            this.txtComments.TabStop = false;
            // 
            // txtVendorName
            // 
            this.txtVendorName.BackColor = System.Drawing.Color.FromName("@table-row-background-odd");
            this.txtVendorName.BorderStyle = Wisej.Web.BorderStyle.None;
            this.txtVendorName.Focusable = false;
            this.txtVendorName.Font = new System.Drawing.Font("default", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.txtVendorName.Location = new System.Drawing.Point(238, 157);
            this.txtVendorName.Name = "txtVendorName";
            this.txtVendorName.ReadOnly = true;
            this.txtVendorName.Size = new System.Drawing.Size(470, 30);
            this.txtVendorName.TabIndex = 25;
            this.txtVendorName.TabStop = false;
            // 
            // txtVendorID
            // 
            this.txtVendorID.BackColor = System.Drawing.Color.FromName("@table-row-background-odd");
            this.txtVendorID.Focusable = false;
            this.txtVendorID.Font = new System.Drawing.Font("default", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.txtVendorID.Location = new System.Drawing.Point(129, 155);
            this.txtVendorID.Name = "txtVendorID";
            this.txtVendorID.ReadOnly = true;
            this.txtVendorID.Size = new System.Drawing.Size(103, 30);
            this.txtVendorID.TabIndex = 24;
            this.txtVendorID.TabStop = false;
            // 
            // lblVendorID
            // 
            this.lblVendorID.AutoSize = true;
            this.lblVendorID.Location = new System.Drawing.Point(22, 158);
            this.lblVendorID.Name = "lblVendorID";
            this.lblVendorID.Size = new System.Drawing.Size(65, 18);
            this.lblVendorID.TabIndex = 23;
            this.lblVendorID.Text = "Vendor ID:";
            // 
            // txtVendorDocNo
            // 
            this.txtVendorDocNo.BackColor = System.Drawing.Color.FromName("@table-row-background-odd");
            this.txtVendorDocNo.Focusable = false;
            this.txtVendorDocNo.Font = new System.Drawing.Font("default", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.txtVendorDocNo.Location = new System.Drawing.Point(129, 123);
            this.txtVendorDocNo.Name = "txtVendorDocNo";
            this.txtVendorDocNo.ReadOnly = true;
            this.txtVendorDocNo.Size = new System.Drawing.Size(103, 30);
            this.txtVendorDocNo.TabIndex = 22;
            this.txtVendorDocNo.TabStop = false;
            // 
            // lblVendorDocNo
            // 
            this.lblVendorDocNo.AutoSize = true;
            this.lblVendorDocNo.Location = new System.Drawing.Point(22, 126);
            this.lblVendorDocNo.Name = "lblVendorDocNo";
            this.lblVendorDocNo.Size = new System.Drawing.Size(97, 18);
            this.lblVendorDocNo.TabIndex = 21;
            this.lblVendorDocNo.Text = "Vendor Doc No:";
            // 
            // lvAttachments
            // 
            this.lvAttachments.BackColor = System.Drawing.Color.FromName("@table-row-background-odd");
            this.lvAttachments.BorderStyle = Wisej.Web.BorderStyle.None;
            this.lvAttachments.Columns.AddRange(new Wisej.Web.ColumnHeader[] {
            this._fileName,
            this._attachmentId});
            this.lvAttachments.GridLines = false;
            this.lvAttachments.HeaderStyle = Wisej.Web.ColumnHeaderStyle.None;
            this.lvAttachments.Location = new System.Drawing.Point(736, 106);
            this.lvAttachments.Name = "lvAttachments";
            this.lvAttachments.Scrollable = false;
            this.lvAttachments.Size = new System.Drawing.Size(386, 222);
            this.lvAttachments.TabIndex = 29;
            this.lvAttachments.View = Wisej.Web.View.SmallIcon;
            this.lvAttachments.SelectedIndexChanged += new System.EventHandler(this.lvAttachments_SelectedIndexChanged);
            this.lvAttachments.DoubleClick += new System.EventHandler(this.lvAttachments_DoubleClick);
            // 
            // _fileName
            // 
            this._fileName.AutoEllipsis = true;
            this._fileName.BackColor = System.Drawing.Color.FromName("@table-row-background-odd");
            this._fileName.MinimumWidth = 50;
            this._fileName.Name = "_fileName";
            this._fileName.Text = "Attachments (double click to view)";
            this._fileName.Width = 600;
            // 
            // _attachmentId
            // 
            this._attachmentId.Name = "_attachmentId";
            this._attachmentId.Text = "Id";
            this._attachmentId.Visible = false;
            this._attachmentId.Width = 20;
            // 
            // lblPurchaseAmount
            // 
            this.lblPurchaseAmount.Location = new System.Drawing.Point(23, 296);
            this.lblPurchaseAmount.Name = "lblPurchaseAmount";
            this.lblPurchaseAmount.Size = new System.Drawing.Size(97, 32);
            this.lblPurchaseAmount.TabIndex = 31;
            this.lblPurchaseAmount.Text = "Purchase Amount:";
            // 
            // txtPurchaseAmount
            // 
            this.txtPurchaseAmount.BackColor = System.Drawing.Color.FromName("@table-row-background-odd");
            this.txtPurchaseAmount.Focusable = false;
            this.txtPurchaseAmount.Font = new System.Drawing.Font("default", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.txtPurchaseAmount.Location = new System.Drawing.Point(130, 298);
            this.txtPurchaseAmount.Name = "txtPurchaseAmount";
            this.txtPurchaseAmount.ReadOnly = true;
            this.txtPurchaseAmount.Size = new System.Drawing.Size(103, 30);
            this.txtPurchaseAmount.TabIndex = 32;
            this.txtPurchaseAmount.TabStop = false;
            this.txtPurchaseAmount.TextAlign = Wisej.Web.HorizontalAlignment.Right;
            this.txtPurchaseAmount.TextChanged += new System.EventHandler(this.txtPurchaseAmount_TextChanged);
            // 
            // txtApproverComments
            // 
            this.txtApproverComments.Font = new System.Drawing.Font("default", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.txtApproverComments.ForeColor = System.Drawing.Color.FromName("@windowText");
            this.txtApproverComments.Location = new System.Drawing.Point(344, 730);
            this.txtApproverComments.Multiline = true;
            this.txtApproverComments.Name = "txtApproverComments";
            this.txtApproverComments.Size = new System.Drawing.Size(580, 124);
            this.txtApproverComments.TabIndex = 36;
            this.txtApproverComments.TabStop = false;
            // 
            // lblApproverUseOnly
            // 
            this.lblApproverUseOnly.AutoSize = true;
            this.lblApproverUseOnly.Font = new System.Drawing.Font("@defaultBold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblApproverUseOnly.ForeColor = System.Drawing.Color.DarkRed;
            this.lblApproverUseOnly.Location = new System.Drawing.Point(344, 706);
            this.lblApproverUseOnly.Name = "lblApproverUseOnly";
            this.lblApproverUseOnly.Size = new System.Drawing.Size(188, 18);
            this.lblApproverUseOnly.TabIndex = 34;
            this.lblApproverUseOnly.Text = "Approver / Admin Comments:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("@defaultBold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(22, 356);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 18);
            this.label1.TabIndex = 37;
            this.label1.Text = "GL Account Selection:";
            // 
            // lvAccountList
            // 
            this.lvAccountList.Columns.AddRange(new Wisej.Web.ColumnHeader[] {
            this.account,
            this.accountDescription});
            this.lvAccountList.Location = new System.Drawing.Point(129, 416);
            this.lvAccountList.Name = "lvAccountList";
            this.lvAccountList.Size = new System.Drawing.Size(475, 30);
            this.lvAccountList.TabIndex = 38;
            this.lvAccountList.SelectedItemChanged += new System.EventHandler(this.lvAccountList_SelectedItemChanged);
            // 
            // account
            // 
            this.account.Name = "account";
            this.account.Text = "Account";
            this.account.Width = 125;
            // 
            // accountDescription
            // 
            this.accountDescription.AutoEllipsis = true;
            this.accountDescription.Name = "accountDescription";
            this.accountDescription.Text = "Description";
            this.accountDescription.Width = 300;
            // 
            // txtAccountDescription
            // 
            this.txtAccountDescription.BackColor = System.Drawing.Color.FromName("@table-row-background-odd");
            this.txtAccountDescription.BorderStyle = Wisej.Web.BorderStyle.None;
            this.txtAccountDescription.Focusable = false;
            this.txtAccountDescription.Font = new System.Drawing.Font("default", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.txtAccountDescription.Location = new System.Drawing.Point(613, 416);
            this.txtAccountDescription.Name = "txtAccountDescription";
            this.txtAccountDescription.ReadOnly = true;
            this.txtAccountDescription.Size = new System.Drawing.Size(545, 30);
            this.txtAccountDescription.TabIndex = 39;
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Location = new System.Drawing.Point(23, 420);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(54, 18);
            this.lblAccount.TabIndex = 41;
            this.lblAccount.Text = "Account:";
            // 
            // lblCapexNo
            // 
            this.lblCapexNo.AutoSize = true;
            this.lblCapexNo.Location = new System.Drawing.Point(22, 452);
            this.lblCapexNo.Name = "lblCapexNo";
            this.lblCapexNo.Size = new System.Drawing.Size(63, 18);
            this.lblCapexNo.TabIndex = 42;
            this.lblCapexNo.Text = "Capex No:";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(23, 484);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(54, 18);
            this.lblAmount.TabIndex = 44;
            this.lblAmount.Text = "Amount:";
            // 
            // cmdAddAccount
            // 
            this.cmdAddAccount.ImageSource = "resource.wx/Wisej.Ext.BootstrapIcons/save.svg";
            this.cmdAddAccount.Location = new System.Drawing.Point(235, 484);
            this.cmdAddAccount.Name = "cmdAddAccount";
            this.cmdAddAccount.Size = new System.Drawing.Size(71, 27);
            this.cmdAddAccount.TabIndex = 45;
            this.cmdAddAccount.Text = "Add";
            this.cmdAddAccount.Click += new System.EventHandler(this.cmdAddAccount_Click);
            // 
            // txtAmount
            // 
            this.txtAmount.CustomFormat = "c";
            this.txtAmount.InputType.Min = "0";
            this.txtAmount.InputType.Mode = Wisej.Web.TextBoxMode.Decimal;
            this.txtAmount.KeepFormatOnEnter = false;
            this.txtAmount.Location = new System.Drawing.Point(129, 481);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.SelectOnEnter = true;
            this.txtAmount.Size = new System.Drawing.Size(100, 30);
            this.txtAmount.TabIndex = 46;
            this.txtAmount.TextAlign = Wisej.Web.HorizontalAlignment.Right;
            this.txtAmount.ValueType = typeof(decimal);
            // 
            // dgDistributions
            // 
            this.dgDistributions.BackColor = System.Drawing.Color.FromName("@table-row-background-odd");
            this.dgDistributions.Columns.AddRange(new Wisej.Web.DataGridViewColumn[] {
            this._account,
            this._accountDescription,
            this.CapexNo,
            this.Reference,
            this._amount,
            this._delete});
            this.dgDistributions.Location = new System.Drawing.Point(23, 521);
            this.dgDistributions.MultiSelect = false;
            this.dgDistributions.Name = "dgDistributions";
            this.dgDistributions.RowHeadersVisible = false;
            this.dgDistributions.ShowColumnVisibilityMenu = false;
            this.dgDistributions.Size = new System.Drawing.Size(1135, 170);
            this.dgDistributions.TabIndex = 47;
            this.dgDistributions.RowsAdded += new Wisej.Web.DataGridViewRowsAddedEventHandler(this.dgDistributions_RowsAdded);
            this.dgDistributions.RowsRemoved += new Wisej.Web.DataGridViewRowsRemovedEventHandler(this.dgDistributions_RowsRemoved);
            this.dgDistributions.CellMouseClick += new Wisej.Web.DataGridViewCellMouseEventHandler(this.dgDistributions_CellMouseClick);
            this.dgDistributions.CellMouseMove += new Wisej.Web.DataGridViewCellMouseEventHandler(this.dgDistributions_CellMouseMove);
            // 
            // _account
            // 
            this._account.HeaderText = "Account";
            this._account.Name = "_account";
            this._account.ReadOnly = true;
            this._account.Width = 150;
            // 
            // _accountDescription
            // 
            this._accountDescription.AutoSizeMode = Wisej.Web.DataGridViewAutoSizeColumnMode.Fill;
            this._accountDescription.HeaderText = "Description";
            this._accountDescription.Name = "_accountDescription";
            this._accountDescription.ReadOnly = true;
            this._accountDescription.Width = 250;
            // 
            // CapexNo
            // 
            this.CapexNo.HeaderText = "Capex No";
            this.CapexNo.Name = "CapexNo";
            this.CapexNo.Width = 150;
            // 
            // Reference
            // 
            this.Reference.HeaderText = "Reference";
            this.Reference.Name = "Reference";
            this.Reference.Width = 200;
            // 
            // _amount
            // 
            dataGridViewCellStyle1.Alignment = Wisej.Web.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "C2";
            this._amount.DefaultCellStyle = dataGridViewCellStyle1;
            this._amount.HeaderText = "Amount";
            this._amount.Name = "_amount";
            this._amount.ReadOnly = true;
            this._amount.Width = 125;
            // 
            // _delete
            // 
            this._delete.CellImageAlignment = Wisej.Web.DataGridViewContentAlignment.NotSet;
            this._delete.CellImageSource = "resource.wx/Wisej.Ext.BootstrapIcons/x-lg.svg";
            dataGridViewCellStyle2.Alignment = Wisej.Web.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackgroundImageSource = "(none)";
            this._delete.DefaultCellStyle = dataGridViewCellStyle2;
            this._delete.HeaderText = "Delete";
            this._delete.Name = "_delete";
            this._delete.Width = 80;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 706);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 18);
            this.label2.TabIndex = 48;
            this.label2.Text = "Total Amount:";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.BackColor = System.Drawing.Color.FromName("@table-row-background-odd");
            this.txtTotalAmount.Location = new System.Drawing.Point(132, 703);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(100, 30);
            this.txtTotalAmount.TabIndex = 49;
            this.txtTotalAmount.TextAlign = Wisej.Web.HorizontalAlignment.Right;
            // 
            // txtRC
            // 
            this.txtRC.BackColor = System.Drawing.Color.FromName("@table-row-background-odd");
            this.txtRC.Focusable = false;
            this.txtRC.Font = new System.Drawing.Font("default", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.txtRC.Location = new System.Drawing.Point(129, 187);
            this.txtRC.Name = "txtRC";
            this.txtRC.ReadOnly = true;
            this.txtRC.Size = new System.Drawing.Size(103, 30);
            this.txtRC.TabIndex = 52;
            this.txtRC.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 18);
            this.label3.TabIndex = 51;
            this.label3.Text = "RC:";
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(277, 384);
            this.txtFilter.MaxLength = 100;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.SelectOnEnter = true;
            this.txtFilter.Size = new System.Drawing.Size(327, 30);
            this.txtFilter.TabIndex = 54;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(22, 387);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(46, 18);
            this.lblFilter.TabIndex = 56;
            this.lblFilter.Text = "Search:";
            // 
            // _imgExclamation
            // 
            this._imgExclamation.BackColor = System.Drawing.Color.FromName("@danger");
            this._imgExclamation.ImageSource = "resource.wx/Wisej.Ext.BootstrapIcons/exclamation-square.svg";
            this._imgExclamation.Location = new System.Drawing.Point(238, 703);
            this._imgExclamation.Name = "_imgExclamation";
            this._imgExclamation.Size = new System.Drawing.Size(36, 37);
            this._imgExclamation.TabIndex = 58;
            // 
            // _imgCheck
            // 
            this._imgCheck.BackColor = System.Drawing.Color.FromName("@success");
            this._imgCheck.ImageSource = "resource.wx/Wisej.Ext.BootstrapIcons/check-square.svg";
            this._imgCheck.Location = new System.Drawing.Point(238, 703);
            this._imgCheck.Name = "_imgCheck";
            this._imgCheck.Size = new System.Drawing.Size(36, 37);
            this._imgCheck.TabIndex = 59;
            this._imgCheck.Visible = false;
            // 
            // line1
            // 
            this.line1.Anchor = ((Wisej.Web.AnchorStyles)(((Wisej.Web.AnchorStyles.Top | Wisej.Web.AnchorStyles.Left) 
            | Wisej.Web.AnchorStyles.Right)));
            this.line1.Location = new System.Drawing.Point(12, 334);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(1603, 16);
            // 
            // sbcUserName
            // 
            this.sbcUserName.Anchor = ((Wisej.Web.AnchorStyles)(((Wisej.Web.AnchorStyles.Top | Wisej.Web.AnchorStyles.Left) 
            | Wisej.Web.AnchorStyles.Right)));
            this.sbcUserName.BackColor = System.Drawing.Color.FromName("@toolbar");
            this.sbcUserName.BorderStyle = Wisej.Web.BorderStyle.None;
            this.sbcUserName.Focusable = false;
            this.sbcUserName.Location = new System.Drawing.Point(298, 12);
            this.sbcUserName.Name = "sbcUserName";
            this.sbcUserName.ReadOnly = true;
            this.sbcUserName.Size = new System.Drawing.Size(372, 30);
            this.sbcUserName.TabIndex = 61;
            this.sbcUserName.TabStop = false;
            this.sbcUserName.TextAlign = Wisej.Web.HorizontalAlignment.Right;
            this.sbcUserName.Visible = false;
            // 
            // lblAttachments
            // 
            this.lblAttachments.AutoSize = true;
            this.lblAttachments.Location = new System.Drawing.Point(736, 82);
            this.lblAttachments.Name = "lblAttachments";
            this.lblAttachments.Size = new System.Drawing.Size(236, 18);
            this.lblAttachments.TabIndex = 62;
            this.lblAttachments.Text = "Attachments (double click to download):";
            // 
            // cmdAddRemaining
            // 
            this.cmdAddRemaining.ImageSource = "resource.wx/Wisej.Ext.BootstrapIcons/save-fill.svg";
            this.cmdAddRemaining.Location = new System.Drawing.Point(314, 484);
            this.cmdAddRemaining.Name = "cmdAddRemaining";
            this.cmdAddRemaining.Size = new System.Drawing.Size(129, 27);
            this.cmdAddRemaining.TabIndex = 64;
            this.cmdAddRemaining.Text = "Add Remaining";
            this.cmdAddRemaining.Click += new System.EventHandler(this.cmdAddRemaining_Click);
            // 
            // lblGLReference
            // 
            this.lblGLReference.AutoSize = true;
            this.lblGLReference.Location = new System.Drawing.Point(262, 452);
            this.lblGLReference.Name = "lblGLReference";
            this.lblGLReference.Size = new System.Drawing.Size(64, 18);
            this.lblGLReference.TabIndex = 68;
            this.lblGLReference.Text = "Reference:";
            // 
            // txtCapexNo
            // 
            this.txtCapexNo.Location = new System.Drawing.Point(129, 449);
            this.txtCapexNo.MaxLength = 30;
            this.txtCapexNo.Name = "txtCapexNo";
            this.txtCapexNo.Size = new System.Drawing.Size(100, 30);
            this.txtCapexNo.TabIndex = 70;
            // 
            // txtGLReference
            // 
            this.txtGLReference.Location = new System.Drawing.Point(344, 449);
            this.txtGLReference.MaxLength = 50;
            this.txtGLReference.Name = "txtGLReference";
            this.txtGLReference.Size = new System.Drawing.Size(260, 30);
            this.txtGLReference.TabIndex = 71;
            // 
            // errorProv
            // 
            this.errorProv.ContainerControl = this;
            // 
            // cboRCList
            // 
            this.cboRCList.Columns.AddRange(new Wisej.Web.ColumnHeader[] {
            this.account,
            this.accountDescription});
            this.cboRCList.Location = new System.Drawing.Point(130, 383);
            this.cboRCList.Name = "cboRCList";
            this.cboRCList.Size = new System.Drawing.Size(76, 30);
            this.cboRCList.TabIndex = 73;
            this.cboRCList.SelectedItemChanged += new System.EventHandler(this.cboRCList_SelectedItemChanged);
            this.cboRCList.SelectedValueChanged += new System.EventHandler(this.cboRCList_SelectedValueChanged);
            // 
            // lblFilterCaption
            // 
            this.lblFilterCaption.AutoSize = true;
            this.lblFilterCaption.Location = new System.Drawing.Point(222, 387);
            this.lblFilterCaption.Name = "lblFilterCaption";
            this.lblFilterCaption.Size = new System.Drawing.Size(36, 18);
            this.lblFilterCaption.TabIndex = 74;
            this.lblFilterCaption.Text = "Filter:";
            // 
            // GLIApprove
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = Wisej.Web.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromName("@table-row-background-odd");
            this.Controls.Add(this.lblFilterCaption);
            this.Controls.Add(this.cboRCList);
            this.Controls.Add(this.txtGLReference);
            this.Controls.Add(this.txtCapexNo);
            this.Controls.Add(this.lblGLReference);
            this.Controls.Add(this.cmdAddRemaining);
            this.Controls.Add(this.lblAttachments);
            this.Controls.Add(this.sbcUserName);
            this.Controls.Add(this._imgCheck);
            this.Controls.Add(this._imgExclamation);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.txtRC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTotalAmount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgDistributions);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.cmdAddAccount);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.lblCapexNo);
            this.Controls.Add(this.lblAccount);
            this.Controls.Add(this.txtAccountDescription);
            this.Controls.Add(this.lvAccountList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtApproverComments);
            this.Controls.Add(this.lblApproverUseOnly);
            this.Controls.Add(this.line1);
            this.Controls.Add(this.txtPurchaseAmount);
            this.Controls.Add(this.lblPurchaseAmount);
            this.Controls.Add(this.lvAttachments);
            this.Controls.Add(this.lblAdditionalComments);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this.txtVendorName);
            this.Controls.Add(this.txtVendorID);
            this.Controls.Add(this.lblVendorID);
            this.Controls.Add(this.txtVendorDocNo);
            this.Controls.Add(this.lblVendorDocNo);
            this.Controls.Add(this.txtReference);
            this.Controls.Add(this.lblReference);
            this.Controls.Add(this.lblDashboardHeading);
            this.Controls.Add(this.tbGLIApproval);
            this.Name = "GLIApprove";
            this.RightToLeft = Wisej.Web.RightToLeft.No;
            this.ScrollBars = Wisej.Web.ScrollBars.Vertical;
            this.Selectable = true;
            this.Size = new System.Drawing.Size(1460, 594);
            this.Load += new System.EventHandler(this.GLIApprove_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgDistributions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Wisej.Web.ToolBar tbGLIApproval;
        private Wisej.Web.ToolBarButton tbHome;
        private Wisej.Web.ToolBarButton tbApprove;
        private Wisej.Web.ToolBarButton tbReject;
        private Wisej.Web.ToolBarButton tbReset;
        public Wisej.Web.TextBox txtReference;
        private Wisej.Web.Label lblReference;
        private Wisej.Web.Label lblDashboardHeading;
        private Wisej.Web.Label lblAdditionalComments;
        public Wisej.Web.TextBox txtComments;
        public Wisej.Web.TextBox txtVendorName;
        public Wisej.Web.TextBox txtVendorID;
        private Wisej.Web.Label lblVendorID;
        public Wisej.Web.TextBox txtVendorDocNo;
        private Wisej.Web.Label lblVendorDocNo;
        private Wisej.Web.ListView lvAttachments;
        private Wisej.Web.ColumnHeader _fileName;
        private Wisej.Web.ColumnHeader _attachmentId;
        private Wisej.Web.Label lblPurchaseAmount;
        public Wisej.Web.TextBox txtPurchaseAmount;
        public Wisej.Web.TextBox txtApproverComments;
        private Wisej.Web.Label lblApproverUseOnly;
        private Wisej.Web.Label label1;
        private Wisej.Web.ListViewComboBox lvAccountList;
        private Wisej.Web.ColumnHeader account;
        private Wisej.Web.ColumnHeader accountDescription;
        private Wisej.Web.TextBox txtAccountDescription;
        private Wisej.Web.Label lblAccount;
        private Wisej.Web.Label lblCapexNo;
        private Wisej.Web.Label lblAmount;
        private Wisej.Web.Button cmdAddAccount;
        private Wisej.Web.TypedTextBox txtAmount;
        private Wisej.Web.DataGridView dgDistributions;
        private Wisej.Web.DataGridViewTextBoxColumn _account;
        private Wisej.Web.DataGridViewTextBoxColumn _accountDescription;
        private Wisej.Web.DataGridViewTextBoxColumn _amount;
        private Wisej.Web.Label label2;
        private Wisej.Web.TextBox txtTotalAmount;
        private Wisej.Web.DataGridViewImageColumn _delete;
        public Wisej.Web.TextBox txtRC;
        private Wisej.Web.Label label3;
        private Wisej.Web.TextBox txtFilter;
        private Wisej.Web.Label lblFilter;
        private Wisej.Web.Button _imgExclamation;
        private Wisej.Web.Button _imgCheck;
        private Wisej.Web.Line line1;
        private Wisej.Web.TextBox sbcUserName;
        private Wisej.Web.Label lblAttachments;
        private Wisej.Web.Button cmdAddRemaining;
        private Wisej.Web.DataGridViewTextBoxColumn CapexNo;
        private Wisej.Web.DataGridViewTextBoxColumn Reference;
        private Wisej.Web.Label lblGLReference;
        private Wisej.Web.TextBox txtCapexNo;
        private Wisej.Web.TextBox txtGLReference;
        private Wisej.Web.ErrorProvider errorProv;
        private Wisej.Web.ListViewComboBox cboRCList;
        private Wisej.Web.Label lblFilterCaption;
    }
}
