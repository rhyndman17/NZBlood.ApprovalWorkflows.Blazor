
namespace NZBlood.ApprovalWorkflowsWI
{
    partial class fPreview
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
            this.pdfView = new Wisej.Web.PdfViewer();
            this.SuspendLayout();
            // 
            // pdfView
            // 
            this.pdfView.Anchor = ((Wisej.Web.AnchorStyles)((((Wisej.Web.AnchorStyles.Top | Wisej.Web.AnchorStyles.Bottom) 
            | Wisej.Web.AnchorStyles.Left) 
            | Wisej.Web.AnchorStyles.Right)));
            this.pdfView.Location = new System.Drawing.Point(0, 0);
            this.pdfView.Name = "pdfView";
            this.pdfView.Selectable = true;
            this.pdfView.Size = new System.Drawing.Size(547, 592);
            this.pdfView.TabIndex = 0;
            // 
            // fPreview
            // 
            this.AutoClose = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = Wisej.Web.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 595);
            this.Controls.Add(this.pdfView);
            this.HeaderBackColor = System.Drawing.Color.FromName("@gray-100");
            this.HeaderForeColor = System.Drawing.Color.SlateGray;
            this.KeepOnScreen = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fPreview";
            this.StartPosition = Wisej.Web.FormStartPosition.CenterParent;
            this.Text = "Document Preview";
            this.Load += new System.EventHandler(this.fPreview_Load);
            this.FormClosed += new Wisej.Web.FormClosedEventHandler(this.fPreview_FormClosed);
            this.Leave += new System.EventHandler(this.fPreview_Leave);
            this.ResumeLayout(false);

        }

        #endregion
        public Wisej.Web.PdfViewer pdfView;
    }
}