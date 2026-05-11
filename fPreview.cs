using System;
using Wisej.Web;

namespace NZBlood.ApprovalWorkflowsWI
{
    public partial class fPreview : Form
    {
        public fPreview()
        {
            InitializeComponent();
        }

        
        private void fPreview_Load(object sender, EventArgs e)
        {
            this.AutoClose = true;
            Globals.pdfPreviewFormStatus = 1;
            
        }

        private void fPreview_FormClosed(object sender, FormClosedEventArgs e)
        {
            Globals.pdfPreviewFormStatus = 0;
        }

        private void fPreview_Leave(object sender, EventArgs e)
        {
            
        }
    }
}
