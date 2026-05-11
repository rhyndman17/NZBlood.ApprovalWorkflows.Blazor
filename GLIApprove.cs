using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web;
using Wisej.Web;


namespace NZBlood.ApprovalWorkflowsWI
{
    public partial class GLIApprove : Page
    {
        
        private string userId;
        private string userEmail;
        private bool isAdminUser;
        private int deleteDistributionCol = 5;

        public GLIApprove()
        {
            InitializeComponent();
        }

        private void GoHome()
        {
            //var home = new Home();
            //home.Show();
            Application.OpenPages["home"].Show();
        }

        private void fillRCList()
        {
            lvAccountList.Items.Clear();

            SqlConnection cnn = new SqlConnection(Globals.sqlConn);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("select RC from nzbRCList order by RC", cnn);           

            cnn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
                        
            while (rd.Read())
            {
               string rc = rd["RC"].ToString();
               cboRCList.Items.Add(rc);
            }

            cnn.Close();
        }

        private void fillAttachments()
        {
            try
            {
                string sql = "select FileName,RecId " +
                                "from nzbWFGLIAttachments " +
                                "where Id =" + txtReference.Text + " order by RecId";

                SqlConnection cnn = new SqlConnection(Globals.sqlConn);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = sql;
                cnn.Open();
                SqlDataReader rdc = cmd.ExecuteReader();

                string resources = HttpRuntime.AppDomainAppPath;

                var imageList = new ImageList();
                imageList.Images.Add("dynGPImage", Bitmap.FromFile(resources + "\\icon\\DynamicsGP.ico"));
                imageList.Images.Add("wordImage", Bitmap.FromFile(resources + "\\icon\\Word.ico"));
                imageList.Images.Add("outlookImage", Bitmap.FromFile(resources + "\\icon\\Outlook_MSG.ico"));
                imageList.Images.Add("pdfImage", Bitmap.FromFile(resources + "\\icon\\AcroRd32.ico"));
                imageList.Images.Add("xlImage", Bitmap.FromFile(resources + "\\icon\\Excel.ico"));
                lvAttachments.SmallImageList = imageList;

                while (rdc.Read())
                {
                    string fileName = rdc["FileName"].ToString().TrimEnd();
                    string attachmentId = rdc["RecId"].ToString().TrimEnd();

                    string[] row = { fileName };
                    var ListViewItem = new ListViewItem(row);
                    lvAttachments.Items.Add(ListViewItem);

                    ListViewItem.ImageKey = "dynGPImage";

                    if (fileName.IndexOf("pdf") != -1 | fileName.IndexOf("PDF") != -1)
                    {
                        ListViewItem.ImageKey = "pdfImage";
                    }
                    if (fileName.IndexOf("xl") != -1)
                    {
                        ListViewItem.ImageKey = "xlImage";
                    }
                    if (fileName.IndexOf("doc") != -1)
                    {
                        ListViewItem.ImageKey = "wordImage";
                    }
                    if (fileName.IndexOf("msg") != -1)
                    {
                        ListViewItem.ImageKey = "outlookImage";
                    }

                    ListViewItem.SubItems.Add(attachmentId);

                }

                lvAttachments.Columns[1].Visible = false;

                if (lvAttachments.Items.Count == 0)
                {
                    lblAttachments.Visible = false;
                    lvAttachments.Visible = false;
                }

                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Globals.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void fillAccounts()
        {
            lvAccountList.Items.Clear();

            SqlConnection cnn = new SqlConnection(Globals.sqlConn);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("nzbWFGLIAccounts", cnn);
            cmd.Parameters.AddWithValue("@rc", cboRCList.Text);
            cmd.Parameters.AddWithValue("@filter", txtFilter.Text);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cnn.Open();
            SqlDataReader rd = cmd.ExecuteReader();

            if (!rd.HasRows) { return; }

            while (rd.Read())
            {
                string account = rd["ACTNUMST"].ToString();
                string desc = rd["ACTDESCR"].ToString();

                string[] row = { account };
                var ListViewItem = new ListViewItem(row);
                lvAccountList.Items.Add(ListViewItem);
                ListViewItem.SubItems.Add(desc);
            }

            cnn.Close();
        }

        private void tbGLIApproval_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            //Cursor = Cursors.WaitCursor;

            if (e.Button.Name == "tbHome")
            {
                GoHome();
            }

            if (e.Button.Name == "tbReset")
            {
                DialogResult result = MessageBox.Show("Reseting the document will mark the transaction as available in Dynamics GP. Are you sure you want to reset this document?  ", Globals.appName, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //check status before resetting to ensure it is not already reset or approved
                    Procs.GetGLInvoiceStatus(Convert.ToInt32(txtReference.Text), out int docOkToReset);
                    if (docOkToReset == 1)
                    {
                        Application.ShowLoader = true;
                        Application.Update(this);
                        Procs.ExecuteSQL("execute nzbWFGLIDocumentReset " + Convert.ToInt32(txtReference.Text) + ",'" + txtApproverComments.Text + "','" + userId + "'," + Convert.ToInt32(isAdminUser));
                        MessageBox.Show("Document has been reset.", Globals.appName, MessageBoxButtons.OK);

                        GoHome();

                    }
                    else
                    {
                        MessageBox.Show("Document has already been processed and cannot be reset.", Globals.appName, MessageBoxButtons.OK);
                        GoHome();
                    }
                }

                Cursor = Cursors.Default;
            }

            if (e.Button.Name == "tbReject")
            {
                if (txtApproverComments.Text == "")
                {
                    MessageBox.Show("Comments are required to process a rejection.  Please enter a comment to continue.", Globals.appName, MessageBoxButtons.OK);
                    return;
                }

                DialogResult result = MessageBox.Show("Are you sure you want to reject this document?  ", Globals.appName, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //check status before resetting to ensure it is not already reset or approved
                    Procs.GetGLInvoiceStatus(Convert.ToInt32(txtReference.Text), out int docOKToReject);
                    if (docOKToReject == 1)
                    {
                        Application.ShowLoader = true;
                        Application.Update(this);
                        Procs.ExecuteSQL("execute nzbWFGLIDocumentReject " + Convert.ToInt32(txtReference.Text) + ",'" + txtApproverComments.Text + "','" + userId + "'," + Convert.ToInt32(isAdminUser));
                        MessageBox.Show("Document has been rejected.", Globals.appName, MessageBoxButtons.OK);

                        GoHome();

                    }
                    else
                    {
                        MessageBox.Show("Document has already been processed and cannot be rejected.", Globals.appName, MessageBoxButtons.OK);
                        GoHome();
                    }
                }

            }
                        
            if (e.Button.Name == "tbApprove")
            {
                //check status before approving to ensure it is not already reset or approved
                Procs.GetGLInvoiceStatus(Convert.ToInt32(txtReference.Text), out int docStatus);
                if (docStatus != 1)
                {
                    MessageBox.Show("Document has already been processed and cannot be approved.", Globals.appName, MessageBoxButtons.OK);
                    GoHome();
                }
                    
                if (txtApproverComments.Text == "")
                {
                    MessageBox.Show("Comments are required to process an approval.  Please enter a comment to continue.", Globals.appName, MessageBoxButtons.OK);
                    return;
                }

                if(dgDistributions.RowCount == 0)
                {
                    MessageBox.Show("Please enter the GL distributions.", Globals.appName, MessageBoxButtons.OK);
                    return;
                }

                //check GL selection balances
                //totalCalc();
                decimal totalAmount = Convert.ToDecimal(txtTotalAmount.Text.Replace("$", ""));
                decimal purchAmount = Convert.ToDecimal(txtPurchaseAmount.Text.Replace("$", ""));

                if (totalAmount != purchAmount)
                {
                    MessageBox.Show("GL account selection does not balance to the purchase amount.", Globals.appName, MessageBoxButtons.OK);
                    return;
                }

                DialogResult result = MessageBox.Show("Are you sure you want to approve this document?  ", Globals.appName, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Application.ShowLoader = true;
                    Application.Update(this);

                    //create and post invoice
                    ApproveInvoice(Convert.ToInt32(txtReference.Text));
                    MessageBox.Show("Document has been approved.", Globals.appName, MessageBoxButtons.OK);

                    GoHome();

                }
            }

            //Cursor = Cursors.Default;

        }

        private void GLIApprove_Load(object sender, EventArgs e)
        {
            userId = Application.Session["UserID"];
            userEmail = Application.Session["UserEmail"];
            isAdminUser = Application.Session["IsAdminUser"];
            sbcUserName.Text = userId;
            txtTotalAmount.Text = "$0.00";
            txtAmount.Text="$0.00";
            //lvAttachments.Scrollable = false;
            txtApproverComments.Watermark = ConfigurationManager.AppSettings["PPVApprovalCommentNote"];

             //this.ScrollBars = ScrollBars.Both;

            if (isAdminUser)
            {
                tbReset.Visible = true;
                tbApprove.Enabled = false;
                tbReject.Enabled = false;
            }
            else
            {
                tbReset.Visible = false;
                tbApprove.Enabled = true;
                tbReject.Enabled = true;
            }

            fillRCList();
            fillAttachments();
            cboRCList.Text = txtRC.Text;
            //fillAccounts();
        }

        private void lvAttachments_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (lvAttachments.Items.Count != 0)
                {
                    Application.ShowLoader = true;
                    Application.Update(this);

                    string attachmentId = lvAttachments.SelectedItems[0].SubItems[1].Text;
                    string fileName = lvAttachments.SelectedItems[0].Text;

                    Procs.DownloadGLInvoiceAttachment(attachmentId, out string previewFile);

                    using (Stream stream = new FileStream(previewFile, FileMode.Open))
                    {
                        Application.Download(stream, fileName);
                    }

                    //if (fileName.IndexOf("pdf") != -1 | fileName.IndexOf("PDF") != -1)
                    //{
                    //    //var pdfv = new fPreview();
                    //    //pdfv.Show();
                    //    //pdfv.pdfView.PdfSource = previewFile;

                    //    using (Stream stream = new FileStream(previewFile, FileMode.Open))
                    //    {
                    //        Application.DownloadAndOpen("_blank", stream, previewFile);
                    //    }
                    //}
                    //else
                    //{
                    //    using (Stream stream = new FileStream(previewFile, FileMode.Open))
                    //    {
                    //        Application.Download(stream, fileName);
                    //    }                        
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Globals.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void txtPurchaseAmount_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void lvAccountList_SelectedItemChanged(object sender, EventArgs e)
        {      
            if(lvAccountList.SelectedItem != null)
            {
                txtAccountDescription.Text = lvAccountList.SelectedItem.SubItems[1].Text;
            }
            
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            
        }

        private void cmdAddAccount_Click(object sender, EventArgs e)
        {
            AddGLAccount(1);
        }

        private void dgDistributions_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            totalCalc();
        }

        void totalCalc()
        {
            _imgExclamation.Visible = false;
            _imgCheck.Visible = false;

            decimal totalAmount = 0;
            foreach (DataGridViewRow row in dgDistributions.Rows)
            {
                decimal lineAmount = Convert.ToDecimal(row.Cells["_amount"].Value.ToString().Replace("$",""));
                totalAmount += lineAmount;
            }

            decimal purchAmount = Convert.ToDecimal(txtPurchaseAmount.Text.Replace("$", ""));
            if(purchAmount == totalAmount) { _imgCheck.Visible = true; }
            if(purchAmount != totalAmount) { _imgExclamation.Visible = true; }

            txtTotalAmount.Text = totalAmount.ToString("C2");
        }

        private void dgDistributions_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            totalCalc();
        }

        private void dgDistributions_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == deleteDistributionCol)
            {
                Cursor = Cursors.Hand;
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }

        private void dgDistributions_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            int rowIndex = dgDistributions.CurrentCell.RowIndex;

            if (e.ColumnIndex == deleteDistributionCol)
            {
                dgDistributions.Rows.RemoveAt(rowIndex);
            }
            
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            fillAccounts();
            
            if(txtFilter.Text != "")
            {
                lvAccountList.Focus();
                lvAccountList.DroppedDown = true;
            }            
        }

        private void ApproveInvoice(int Id)
        {
            //clear current distributions
            Procs.ExecuteSQL("delete nzbWFGLITransactionDist where Id=" + Id);
            //add distributions for posting
            foreach (DataGridViewRow row in dgDistributions.Rows)
            {
                decimal lineAmount = Convert.ToDecimal(row.Cells["_amount"].Value.ToString().Replace("$", ""));
                string accountNumber = row.Cells["_account"].Value.ToString();
                string accountDescription = row.Cells["_accountDescription"].Value.ToString().Replace("'", "''");
                string capexNo= row.Cells["CapexNo"].Value.ToString().Replace("'", "''");
                string additionalReference= row.Cells["Reference"].Value.ToString().Replace("'", "''");

                Procs.ExecuteSQL("insert into nzbWFGLITransactionDist select " + Id + ",'" + accountNumber + "','" + accountDescription + "'," + lineAmount + ",'" + capexNo + "','" + additionalReference + "'");
                
            }

            Procs.ExecuteSQL("execute nzbWFGLICreateApprovedInvoice " + Id + ",'" + userId + "','" + txtApproverComments.Text + "'");


        }

        private void lvAttachments_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void AddGLAccount(int option)
        {
            decimal amount;
            if (option == 2)
            {
                amount = Convert.ToDecimal(txtPurchaseAmount.Text.Replace("$", "")) - Convert.ToDecimal(txtTotalAmount.Text.Replace("$", ""));
            }
            else
            {
                amount = Convert.ToDecimal(txtAmount.Text.Replace("$", ""));
            }

            if (lvAccountList.Text != "")
            {
                Procs.GetRecordCount("select * from nzbAccountOptions where ACCT_OPTIONS_AdditionalReferenceRequired=1 and ACCT_OPTIONS_Key1='" + lvAccountList.Text + "'", out int referenceRequired);
                if (referenceRequired != 0)
                {
                    if (txtGLReference.Text == "")
                    {
                        errorProv.SetError(txtGLReference, "A reference or comment is required for this account");
                        return;
                    }
                }

                if (lvAccountList.Text.Contains("82145") == true)
                {
                    if (txtCapexNo.Text == "")
                    {
                        errorProv.SetError(txtCapexNo, "A capex number is required for this account");
                        return;
                    }
                }


                //decimal amount = Convert.ToDecimal(txtAmount.Text.Replace("$", ""));
                if (amount > 0)
                {
                    dgDistributions.Rows.Add(lvAccountList.Text, txtAccountDescription.Text, txtCapexNo.Text, txtGLReference.Text, amount);

                    lvAccountList.Text = "";
                    txtAccountDescription.Text = "";
                    txtAmount.Text = "0";
                    txtCapexNo.Text = "";
                    txtGLReference.Text = "";
                    errorProv.Clear();
                }

            }
        }

        private void cmdAddRemaining_Click(object sender, EventArgs e)
        {
            AddGLAccount(2);           
        }

        private void cboRCList_SelectedItemChanged(object sender, EventArgs e)
        {
            
        }

        private void cboRCList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboRCList.Text != "")
            {
                fillAccounts();
            }
            
        }
    }
}
