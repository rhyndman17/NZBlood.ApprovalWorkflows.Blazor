
namespace NZBlood.ApprovalWorkflowsWI
{
    partial class PPVApproval
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

        #region Wisej Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Wisej.Web.DataGridViewCellStyle dataGridViewCellStyle21 = new Wisej.Web.DataGridViewCellStyle();
            Wisej.Web.DataGridViewCellStyle dataGridViewCellStyle22 = new Wisej.Web.DataGridViewCellStyle();
            Wisej.Web.DataGridViewCellStyle dataGridViewCellStyle23 = new Wisej.Web.DataGridViewCellStyle();
            Wisej.Web.DataGridViewCellStyle dataGridViewCellStyle24 = new Wisej.Web.DataGridViewCellStyle();
            Wisej.Web.DataGridViewCellStyle dataGridViewCellStyle25 = new Wisej.Web.DataGridViewCellStyle();
            Wisej.Web.DataGridViewCellStyle dataGridViewCellStyle26 = new Wisej.Web.DataGridViewCellStyle();
            Wisej.Web.DataGridViewCellStyle dataGridViewCellStyle27 = new Wisej.Web.DataGridViewCellStyle();
            Wisej.Web.DataGridViewCellStyle dataGridViewCellStyle28 = new Wisej.Web.DataGridViewCellStyle();
            Wisej.Web.DataGridViewCellStyle dataGridViewCellStyle29 = new Wisej.Web.DataGridViewCellStyle();
            Wisej.Web.DataGridViewCellStyle dataGridViewCellStyle30 = new Wisej.Web.DataGridViewCellStyle();
            this.tbPPVApproval = new Wisej.Web.ToolBar();
            this.tbHome = new Wisej.Web.ToolBarButton();
            this.tbApprove = new Wisej.Web.ToolBarButton();
            this.tbReject = new Wisej.Web.ToolBarButton();
            this.tbReset = new Wisej.Web.ToolBarButton();
            this.lblFullUserDisplay = new Wisej.Web.Label();
            this.lblDashboardHeading = new Wisej.Web.Label();
            this.lblReceiptNumberCaption = new Wisej.Web.Label();
            this.txtReceiptNumber = new Wisej.Web.TextBox();
            this.lblVendorDocNo = new Wisej.Web.Label();
            this.txtVendorDocNo = new Wisej.Web.TextBox();
            this.lblVendorID = new Wisej.Web.Label();
            this.txtVendorID = new Wisej.Web.TextBox();
            this.txtVendorName = new Wisej.Web.TextBox();
            this.dgPPVItems = new Wisej.Web.DataGridView();
            this.itemNumber = new Wisej.Web.DataGridViewTextBoxColumn();
            this.itemDesc = new Wisej.Web.DataGridViewTextBoxColumn();
            this.vendorItem = new Wisej.Web.DataGridViewTextBoxColumn();
            this.poNumber = new Wisej.Web.DataGridViewTextBoxColumn();
            this.shipmentNo = new Wisej.Web.DataGridViewTextBoxColumn();
            this.variance = new Wisej.Web.DataGridViewTextBoxColumn();
            this.poCost = new Wisej.Web.DataGridViewTextBoxColumn();
            this.invCost = new Wisej.Web.DataGridViewTextBoxColumn();
            this.lblAdditionalComments = new Wisej.Web.Label();
            this.txtComments = new Wisej.Web.TextBox();
            this.lvAttachments = new Wisej.Web.ListView();
            this._fileName = new Wisej.Web.ColumnHeader();
            this._attachmentId = new Wisej.Web.ColumnHeader();
            this.lblAttachments = new Wisej.Web.Label();
            this.lblApproverComments = new Wisej.Web.Label();
            this.txtApproverComments = new Wisej.Web.TextBox();
            this.lblSubmittedUserID = new Wisej.Web.Label();
            this.lblSubmittedUserEmailAddress = new Wisej.Web.Label();
            this.lblCommentHelp = new Wisej.Web.Label();
            this.lblHistory = new Wisej.Web.Label();
            this.dgReceiptHistory = new Wisej.Web.DataGridView();
            this.dateTime = new Wisej.Web.DataGridViewTextBoxColumn();
            this.status = new Wisej.Web.DataGridViewTextBoxColumn();
            this.who = new Wisej.Web.DataGridViewTextBoxColumn();
            this.comment = new Wisej.Web.DataGridViewTextBoxColumn();
            this.sbcUserName = new Wisej.Web.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgPPVItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgReceiptHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // tbPPVApproval
            // 
            this.tbPPVApproval.AutoSize = false;
            this.tbPPVApproval.Buttons.AddRange(new Wisej.Web.ToolBarButton[] {
            this.tbHome,
            this.tbApprove,
            this.tbReject,
            this.tbReset});
            this.tbPPVApproval.Location = new System.Drawing.Point(0, 0);
            this.tbPPVApproval.Name = "tbPPVApproval";
            this.tbPPVApproval.Size = new System.Drawing.Size(1831, 51);
            this.tbPPVApproval.TabIndex = 0;
            this.tbPPVApproval.TabStop = false;
            this.tbPPVApproval.ButtonClick += new Wisej.Web.ToolBarButtonClickEventHandler(this.tbPPVApproval_ButtonClick);
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
            // lblFullUserDisplay
            // 
            this.lblFullUserDisplay.Anchor = ((Wisej.Web.AnchorStyles)((Wisej.Web.AnchorStyles.Top | Wisej.Web.AnchorStyles.Right)));
            this.lblFullUserDisplay.AutoSize = true;
            this.lblFullUserDisplay.Location = new System.Drawing.Point(2014, 14);
            this.lblFullUserDisplay.Name = "lblFullUserDisplay";
            this.lblFullUserDisplay.Size = new System.Drawing.Size(13, 18);
            this.lblFullUserDisplay.TabIndex = 1;
            this.lblFullUserDisplay.Text = "...";
            this.lblFullUserDisplay.Visible = false;
            // 
            // lblDashboardHeading
            // 
            this.lblDashboardHeading.AutoSize = true;
            this.lblDashboardHeading.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblDashboardHeading.Location = new System.Drawing.Point(23, 58);
            this.lblDashboardHeading.Name = "lblDashboardHeading";
            this.lblDashboardHeading.Size = new System.Drawing.Size(362, 25);
            this.lblDashboardHeading.TabIndex = 9;
            this.lblDashboardHeading.Text = "Price Variance Approval Review";
            // 
            // lblReceiptNumberCaption
            // 
            this.lblReceiptNumberCaption.AutoSize = true;
            this.lblReceiptNumberCaption.Location = new System.Drawing.Point(23, 95);
            this.lblReceiptNumberCaption.Name = "lblReceiptNumberCaption";
            this.lblReceiptNumberCaption.Size = new System.Drawing.Size(101, 18);
            this.lblReceiptNumberCaption.TabIndex = 11;
            this.lblReceiptNumberCaption.Text = "Receipt Number:";
            // 
            // txtReceiptNumber
            // 
            this.txtReceiptNumber.BackColor = System.Drawing.Color.FromName("@table-row-background-odd");
            this.txtReceiptNumber.Focusable = false;
            this.txtReceiptNumber.Font = new System.Drawing.Font("default", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.txtReceiptNumber.Location = new System.Drawing.Point(130, 92);
            this.txtReceiptNumber.Name = "txtReceiptNumber";
            this.txtReceiptNumber.ReadOnly = true;
            this.txtReceiptNumber.Size = new System.Drawing.Size(103, 30);
            this.txtReceiptNumber.TabIndex = 12;
            this.txtReceiptNumber.TabStop = false;
            this.txtReceiptNumber.TextChanged += new System.EventHandler(this.txtReceiptNumber_TextChanged);
            // 
            // lblVendorDocNo
            // 
            this.lblVendorDocNo.AutoSize = true;
            this.lblVendorDocNo.Location = new System.Drawing.Point(23, 126);
            this.lblVendorDocNo.Name = "lblVendorDocNo";
            this.lblVendorDocNo.Size = new System.Drawing.Size(97, 18);
            this.lblVendorDocNo.TabIndex = 13;
            this.lblVendorDocNo.Text = "Vendor Doc No:";
            // 
            // txtVendorDocNo
            // 
            this.txtVendorDocNo.BackColor = System.Drawing.Color.FromName("@table-row-background-odd");
            this.txtVendorDocNo.Focusable = false;
            this.txtVendorDocNo.Font = new System.Drawing.Font("default", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.txtVendorDocNo.Location = new System.Drawing.Point(130, 123);
            this.txtVendorDocNo.Name = "txtVendorDocNo";
            this.txtVendorDocNo.ReadOnly = true;
            this.txtVendorDocNo.Size = new System.Drawing.Size(103, 30);
            this.txtVendorDocNo.TabIndex = 14;
            this.txtVendorDocNo.TabStop = false;
            // 
            // lblVendorID
            // 
            this.lblVendorID.AutoSize = true;
            this.lblVendorID.Location = new System.Drawing.Point(23, 157);
            this.lblVendorID.Name = "lblVendorID";
            this.lblVendorID.Size = new System.Drawing.Size(65, 18);
            this.lblVendorID.TabIndex = 15;
            this.lblVendorID.Text = "Vendor ID:";
            // 
            // txtVendorID
            // 
            this.txtVendorID.BackColor = System.Drawing.Color.FromName("@table-row-background-odd");
            this.txtVendorID.Font = new System.Drawing.Font("default", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.txtVendorID.Location = new System.Drawing.Point(130, 154);
            this.txtVendorID.Name = "txtVendorID";
            this.txtVendorID.ReadOnly = true;
            this.txtVendorID.Size = new System.Drawing.Size(103, 30);
            this.txtVendorID.TabIndex = 16;
            this.txtVendorID.TabStop = false;
            // 
            // txtVendorName
            // 
            this.txtVendorName.BackColor = System.Drawing.Color.FromName("@table-row-background-odd");
            this.txtVendorName.BorderStyle = Wisej.Web.BorderStyle.None;
            this.txtVendorName.Font = new System.Drawing.Font("default", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.txtVendorName.Location = new System.Drawing.Point(239, 154);
            this.txtVendorName.Name = "txtVendorName";
            this.txtVendorName.ReadOnly = true;
            this.txtVendorName.Size = new System.Drawing.Size(465, 30);
            this.txtVendorName.TabIndex = 17;
            this.txtVendorName.TabStop = false;
            // 
            // dgPPVItems
            // 
            this.dgPPVItems.AllowUserToResizeRows = false;
            this.dgPPVItems.Anchor = ((Wisej.Web.AnchorStyles)(((Wisej.Web.AnchorStyles.Top | Wisej.Web.AnchorStyles.Left) 
            | Wisej.Web.AnchorStyles.Right)));
            this.dgPPVItems.BackColor = System.Drawing.Color.FromName("@table-row-background-odd");
            this.dgPPVItems.Columns.AddRange(new Wisej.Web.DataGridViewColumn[] {
            this.itemNumber,
            this.itemDesc,
            this.vendorItem,
            this.poNumber,
            this.shipmentNo,
            this.variance,
            this.poCost,
            this.invCost});
            this.dgPPVItems.Location = new System.Drawing.Point(23, 290);
            this.dgPPVItems.MultiSelect = false;
            this.dgPPVItems.Name = "dgPPVItems";
            this.dgPPVItems.RowHeadersVisible = false;
            this.dgPPVItems.Selectable = true;
            this.dgPPVItems.Size = new System.Drawing.Size(1775, 206);
            this.dgPPVItems.TabIndex = 18;
            this.dgPPVItems.TabStop = false;
            this.dgPPVItems.CellClick += new Wisej.Web.DataGridViewCellEventHandler(this.dgPPVItems_CellClick);
            this.dgPPVItems.CellDoubleClick += new Wisej.Web.DataGridViewCellEventHandler(this.dgPPVItems_CellDoubleClick);
            this.dgPPVItems.CellMouseMove += new Wisej.Web.DataGridViewCellMouseEventHandler(this.dgPPVItems_CellMouseMove);
            // 
            // itemNumber
            // 
            this.itemNumber.AutoEllipsis = true;
            this.itemNumber.DataPropertyName = "ItemNumber";
            dataGridViewCellStyle21.Alignment = Wisej.Web.DataGridViewContentAlignment.MiddleLeft;
            this.itemNumber.HeaderStyle = dataGridViewCellStyle21;
            this.itemNumber.HeaderText = "Item Number";
            this.itemNumber.Name = "itemNumber";
            this.itemNumber.ReadOnly = true;
            this.itemNumber.Width = 150;
            // 
            // itemDesc
            // 
            this.itemDesc.AutoEllipsis = true;
            this.itemDesc.AutoSizeMode = Wisej.Web.DataGridViewAutoSizeColumnMode.Fill;
            this.itemDesc.DataPropertyName = "ItemDescription";
            dataGridViewCellStyle22.Alignment = Wisej.Web.DataGridViewContentAlignment.MiddleLeft;
            this.itemDesc.HeaderStyle = dataGridViewCellStyle22;
            this.itemDesc.HeaderText = "Item Description";
            this.itemDesc.MinimumWidth = 100;
            this.itemDesc.Multiline = true;
            this.itemDesc.Name = "itemDesc";
            this.itemDesc.ReadOnly = true;
            // 
            // vendorItem
            // 
            this.vendorItem.AutoEllipsis = true;
            this.vendorItem.DataPropertyName = "VendorItemNumber";
            dataGridViewCellStyle23.Alignment = Wisej.Web.DataGridViewContentAlignment.MiddleLeft;
            this.vendorItem.HeaderStyle = dataGridViewCellStyle23;
            this.vendorItem.HeaderText = "Vendor Item";
            this.vendorItem.Name = "vendorItem";
            this.vendorItem.ReadOnly = true;
            this.vendorItem.Width = 200;
            // 
            // poNumber
            // 
            this.poNumber.AutoEllipsis = true;
            this.poNumber.DataPropertyName = "PONumber";
            dataGridViewCellStyle24.BackgroundImageSource = "resource.wx/Wisej.Ext.BootstrapIcons/card-heading.svg";
            dataGridViewCellStyle24.Padding = new Wisej.Web.Padding(20, 0, 0, 0);
            this.poNumber.DefaultCellStyle = dataGridViewCellStyle24;
            dataGridViewCellStyle25.Alignment = Wisej.Web.DataGridViewContentAlignment.MiddleLeft;
            this.poNumber.HeaderStyle = dataGridViewCellStyle25;
            this.poNumber.HeaderText = "PO Number";
            this.poNumber.Name = "poNumber";
            this.poNumber.ReadOnly = true;
            this.poNumber.Width = 200;
            // 
            // shipmentNo
            // 
            this.shipmentNo.AutoEllipsis = true;
            this.shipmentNo.DataPropertyName = "ShipmentNumber";
            dataGridViewCellStyle26.Alignment = Wisej.Web.DataGridViewContentAlignment.MiddleLeft;
            this.shipmentNo.HeaderStyle = dataGridViewCellStyle26;
            this.shipmentNo.HeaderText = "Shipment No";
            this.shipmentNo.Name = "shipmentNo";
            this.shipmentNo.ReadOnly = true;
            this.shipmentNo.Width = 200;
            // 
            // variance
            // 
            this.variance.DataPropertyName = "Variance";
            dataGridViewCellStyle27.Alignment = Wisej.Web.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle27.Format = "C2";
            dataGridViewCellStyle27.NullValue = null;
            this.variance.DefaultCellStyle = dataGridViewCellStyle27;
            this.variance.HeaderText = "Variance";
            this.variance.Name = "variance";
            this.variance.ReadOnly = true;
            this.variance.Width = 150;
            // 
            // poCost
            // 
            this.poCost.DataPropertyName = "POCost";
            dataGridViewCellStyle28.Alignment = Wisej.Web.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle28.Format = "C2";
            this.poCost.DefaultCellStyle = dataGridViewCellStyle28;
            this.poCost.HeaderText = "PO Cost";
            this.poCost.Name = "poCost";
            this.poCost.ReadOnly = true;
            this.poCost.Width = 150;
            // 
            // invCost
            // 
            this.invCost.DataPropertyName = "InvoiceCost";
            dataGridViewCellStyle29.Alignment = Wisej.Web.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle29.Format = "C2";
            this.invCost.DefaultCellStyle = dataGridViewCellStyle29;
            this.invCost.HeaderText = "Inv. Cost";
            this.invCost.Name = "invCost";
            this.invCost.ReadOnly = true;
            this.invCost.Width = 150;
            // 
            // lblAdditionalComments
            // 
            this.lblAdditionalComments.Location = new System.Drawing.Point(23, 202);
            this.lblAdditionalComments.Name = "lblAdditionalComments";
            this.lblAdditionalComments.Size = new System.Drawing.Size(87, 42);
            this.lblAdditionalComments.TabIndex = 19;
            this.lblAdditionalComments.Text = "Additional Comments:";
            // 
            // txtComments
            // 
            this.txtComments.BackColor = System.Drawing.Color.FromName("@table-row-background-odd");
            this.txtComments.Focusable = false;
            this.txtComments.Font = new System.Drawing.Font("@default", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtComments.ForeColor = System.Drawing.Color.DarkRed;
            this.txtComments.Location = new System.Drawing.Point(130, 202);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.ReadOnly = true;
            this.txtComments.Size = new System.Drawing.Size(574, 68);
            this.txtComments.TabIndex = 20;
            this.txtComments.TabStop = false;
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
            this.lvAttachments.Location = new System.Drawing.Point(719, 123);
            this.lvAttachments.Name = "lvAttachments";
            this.lvAttachments.Size = new System.Drawing.Size(304, 147);
            this.lvAttachments.TabIndex = 21;
            this.lvAttachments.View = Wisej.Web.View.SmallIcon;
            this.lvAttachments.SelectedIndexChanged += new System.EventHandler(this.lvAttachments_SelectedIndexChanged);
            this.lvAttachments.DoubleClick += new System.EventHandler(this.lvAttachments_DoubleClick);
            // 
            // _fileName
            // 
            this._fileName.AutoEllipsis = true;
            this._fileName.MinimumWidth = 100;
            this._fileName.Name = "_fileName";
            this._fileName.Text = "File Name";
            this._fileName.Width = 600;
            // 
            // _attachmentId
            // 
            this._attachmentId.Name = "_attachmentId";
            this._attachmentId.Visible = false;
            // 
            // lblAttachments
            // 
            this.lblAttachments.Location = new System.Drawing.Point(719, 101);
            this.lblAttachments.Name = "lblAttachments";
            this.lblAttachments.Size = new System.Drawing.Size(276, 21);
            this.lblAttachments.TabIndex = 22;
            this.lblAttachments.Text = "Invoice Attachments: (double click to download)";
            // 
            // lblApproverComments
            // 
            this.lblApproverComments.Font = new System.Drawing.Font("default", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblApproverComments.ForeColor = System.Drawing.Color.DarkRed;
            this.lblApproverComments.Location = new System.Drawing.Point(23, 513);
            this.lblApproverComments.Name = "lblApproverComments";
            this.lblApproverComments.Size = new System.Drawing.Size(271, 25);
            this.lblApproverComments.TabIndex = 25;
            this.lblApproverComments.Text = "Approver / Admin Comments:";
            // 
            // txtApproverComments
            // 
            this.txtApproverComments.AutoSize = false;
            this.txtApproverComments.Font = new System.Drawing.Font("default", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.txtApproverComments.ForeColor = System.Drawing.Color.FromName("@windowText");
            this.txtApproverComments.Location = new System.Drawing.Point(23, 541);
            this.txtApproverComments.Multiline = true;
            this.txtApproverComments.Name = "txtApproverComments";
            this.txtApproverComments.Size = new System.Drawing.Size(472, 133);
            this.txtApproverComments.TabIndex = 1;
            this.txtApproverComments.TabStop = false;
            // 
            // lblSubmittedUserID
            // 
            this.lblSubmittedUserID.AutoSize = true;
            this.lblSubmittedUserID.Location = new System.Drawing.Point(1234, 65);
            this.lblSubmittedUserID.Name = "lblSubmittedUserID";
            this.lblSubmittedUserID.Size = new System.Drawing.Size(13, 18);
            this.lblSubmittedUserID.TabIndex = 28;
            this.lblSubmittedUserID.Text = "...";
            this.lblSubmittedUserID.Visible = false;
            // 
            // lblSubmittedUserEmailAddress
            // 
            this.lblSubmittedUserEmailAddress.AutoSize = true;
            this.lblSubmittedUserEmailAddress.Location = new System.Drawing.Point(1266, 65);
            this.lblSubmittedUserEmailAddress.Name = "lblSubmittedUserEmailAddress";
            this.lblSubmittedUserEmailAddress.Size = new System.Drawing.Size(13, 18);
            this.lblSubmittedUserEmailAddress.TabIndex = 29;
            this.lblSubmittedUserEmailAddress.Text = "...";
            this.lblSubmittedUserEmailAddress.Visible = false;
            // 
            // lblCommentHelp
            // 
            this.lblCommentHelp.AutoSize = true;
            this.lblCommentHelp.Font = new System.Drawing.Font("default", 13F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.lblCommentHelp.ForeColor = System.Drawing.Color.FromName("@danger");
            this.lblCommentHelp.Location = new System.Drawing.Point(130, 671);
            this.lblCommentHelp.Name = "lblCommentHelp";
            this.lblCommentHelp.Size = new System.Drawing.Size(13, 18);
            this.lblCommentHelp.TabIndex = 31;
            this.lblCommentHelp.Text = "...";
            this.lblCommentHelp.Visible = false;
            // 
            // lblHistory
            // 
            this.lblHistory.AutoSize = true;
            this.lblHistory.Font = new System.Drawing.Font("@defaultBold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblHistory.ForeColor = System.Drawing.Color.DarkRed;
            this.lblHistory.Location = new System.Drawing.Point(536, 513);
            this.lblHistory.Name = "lblHistory";
            this.lblHistory.Size = new System.Drawing.Size(91, 18);
            this.lblHistory.TabIndex = 33;
            this.lblHistory.Text = "Status History";
            this.lblHistory.Visible = false;
            // 
            // dgReceiptHistory
            // 
            this.dgReceiptHistory.AllowUserToResizeRows = false;
            this.dgReceiptHistory.AutoSizeRowsMode = Wisej.Web.DataGridViewAutoSizeRowsMode.DoubleClick;
            this.dgReceiptHistory.BackColor = System.Drawing.Color.FromName("@table-row-background-odd");
            this.dgReceiptHistory.Columns.AddRange(new Wisej.Web.DataGridViewColumn[] {
            this.dateTime,
            this.status,
            this.who,
            this.comment});
            dataGridViewCellStyle30.WrapMode = Wisej.Web.DataGridViewTriState.NotSet;
            this.dgReceiptHistory.DefaultCellStyle = dataGridViewCellStyle30;
            this.dgReceiptHistory.Location = new System.Drawing.Point(536, 541);
            this.dgReceiptHistory.MultiSelect = false;
            this.dgReceiptHistory.Name = "dgReceiptHistory";
            this.dgReceiptHistory.ReadOnly = true;
            this.dgReceiptHistory.RowHeadersVisible = false;
            this.dgReceiptHistory.Size = new System.Drawing.Size(721, 133);
            this.dgReceiptHistory.TabIndex = 34;
            this.dgReceiptHistory.TabStop = false;
            this.dgReceiptHistory.Visible = false;
            // 
            // dateTime
            // 
            this.dateTime.DataPropertyName = "StatusDateTime";
            this.dateTime.HeaderText = "Date/Time";
            this.dateTime.Name = "dateTime";
            this.dateTime.Width = 150;
            // 
            // status
            // 
            this.status.DataPropertyName = "StatusId";
            this.status.HeaderText = "Status";
            this.status.Name = "status";
            // 
            // who
            // 
            this.who.DataPropertyName = "UserId";
            this.who.HeaderText = "Who";
            this.who.Name = "who";
            this.who.Width = 150;
            // 
            // comment
            // 
            this.comment.AutoSizeMode = Wisej.Web.DataGridViewAutoSizeColumnMode.Fill;
            this.comment.DataPropertyName = "Comment";
            this.comment.HeaderText = "Comment";
            this.comment.Name = "comment";
            // 
            // sbcUserName
            // 
            this.sbcUserName.BackColor = System.Drawing.Color.FromName("@toolbar");
            this.sbcUserName.BorderStyle = Wisej.Web.BorderStyle.None;
            this.sbcUserName.Focusable = false;
            this.sbcUserName.Location = new System.Drawing.Point(465, 12);
            this.sbcUserName.Name = "sbcUserName";
            this.sbcUserName.ReadOnly = true;
            this.sbcUserName.Size = new System.Drawing.Size(224, 30);
            this.sbcUserName.TabIndex = 35;
            this.sbcUserName.TabStop = false;
            this.sbcUserName.Visible = false;
            // 
            // PPVApproval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = Wisej.Web.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromName("@table-row-background-odd");
            this.Controls.Add(this.sbcUserName);
            this.Controls.Add(this.dgReceiptHistory);
            this.Controls.Add(this.lblHistory);
            this.Controls.Add(this.lblCommentHelp);
            this.Controls.Add(this.lblSubmittedUserEmailAddress);
            this.Controls.Add(this.lblSubmittedUserID);
            this.Controls.Add(this.lblApproverComments);
            this.Controls.Add(this.txtApproverComments);
            this.Controls.Add(this.lblAttachments);
            this.Controls.Add(this.lvAttachments);
            this.Controls.Add(this.lblAdditionalComments);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this.dgPPVItems);
            this.Controls.Add(this.txtVendorName);
            this.Controls.Add(this.txtVendorID);
            this.Controls.Add(this.lblVendorID);
            this.Controls.Add(this.txtVendorDocNo);
            this.Controls.Add(this.lblVendorDocNo);
            this.Controls.Add(this.txtReceiptNumber);
            this.Controls.Add(this.lblReceiptNumberCaption);
            this.Controls.Add(this.lblDashboardHeading);
            this.Controls.Add(this.lblFullUserDisplay);
            this.Controls.Add(this.tbPPVApproval);
            this.Name = "PPVApproval";
            this.ScrollBars = Wisej.Web.ScrollBars.Vertical;
            this.Selectable = true;
            this.Size = new System.Drawing.Size(1814, 677);
            this.Load += new System.EventHandler(this.PPVApproval_Load);
            this.Click += new System.EventHandler(this.PPVApproval_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dgPPVItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgReceiptHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Wisej.Web.ToolBar tbPPVApproval;
        private Wisej.Web.ToolBarButton tbHome;
        public Wisej.Web.Label lblFullUserDisplay;
        private Wisej.Web.Label lblDashboardHeading;
        private Wisej.Web.Label lblReceiptNumberCaption;
        public Wisej.Web.TextBox txtReceiptNumber;
        private Wisej.Web.Label lblVendorDocNo;
        public Wisej.Web.TextBox txtVendorDocNo;
        private Wisej.Web.Label lblVendorID;
        public Wisej.Web.TextBox txtVendorID;
        public Wisej.Web.TextBox txtVendorName;
        private Wisej.Web.DataGridView dgPPVItems;
        private Wisej.Web.DataGridViewTextBoxColumn itemNumber;
        private Wisej.Web.DataGridViewTextBoxColumn itemDesc;
        private Wisej.Web.DataGridViewTextBoxColumn vendorItem;
        private Wisej.Web.DataGridViewTextBoxColumn shipmentNo;
        private Wisej.Web.DataGridViewTextBoxColumn variance;
        private Wisej.Web.DataGridViewTextBoxColumn poCost;
        private Wisej.Web.DataGridViewTextBoxColumn invCost;
        private Wisej.Web.Label lblAdditionalComments;
        public Wisej.Web.TextBox txtComments;
        private Wisej.Web.ListView lvAttachments;
        private Wisej.Web.ColumnHeader _fileName;
        private Wisej.Web.ColumnHeader _attachmentId;
        private Wisej.Web.Label lblAttachments;
        private Wisej.Web.ToolBarButton tbApprove;
        private Wisej.Web.ToolBarButton tbReset;
        private Wisej.Web.Label lblApproverComments;
        public Wisej.Web.TextBox txtApproverComments;
        private Wisej.Web.DataGridViewTextBoxColumn poNumber;
        private Wisej.Web.ToolBarButton tbReject;
        public Wisej.Web.Label lblSubmittedUserID;
        public Wisej.Web.Label lblSubmittedUserEmailAddress;
        private Wisej.Web.Label lblCommentHelp;
        private Wisej.Web.Label lblHistory;
        private Wisej.Web.DataGridView dgReceiptHistory;
        private Wisej.Web.DataGridViewTextBoxColumn dateTime;
        private Wisej.Web.DataGridViewTextBoxColumn status;
        private Wisej.Web.DataGridViewTextBoxColumn who;
        private Wisej.Web.DataGridViewTextBoxColumn comment;
        private Wisej.Web.TextBox sbcUserName;
    }
}
