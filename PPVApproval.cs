using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Web;
using Wisej.Core;
using Wisej.Web;


namespace NZBlood.ApprovalWorkflowsWI
{
    public partial class PPVApproval : Page
    {
        private int poNumberCol = 3;
        private string userId;
        private string userEmail;
        private bool isAdminUser;
        public int requiresManagerApproval;
        private bool isManager;
        private string managerAccountName;
        private string managerEmailAddress;
        //private string commentHelpText;

        public PPVApproval()
        {
            InitializeComponent();
        }

        private void GoHome()
        {
            Application.OpenPages["home"].Show();
        }

        private void tbPPVApproval_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            bool approve = false;            

            if (e.Button.Name == "tbHome")
            {
                GoHome();
            }

            if (e.Button.Name == "tbReset")
            {
                DialogResult result = MessageBox.Show("Reseting the receipt will mark the transaction as available in Dynamics GP. Are you sure you want to reset this receipt?  ", Globals.appName, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //check status before resetting to ensure it is not already reset or approved
                    Procs.GetPPVReceiptResetlStatus(txtReceiptNumber.Text, out int receiptOkToReset);
                    if (receiptOkToReset == 1)
                    {
                        Application.ShowLoader = true;
                        Application.Update(this);
                        Procs.ExecuteSQL("execute nzbWFPPVReceiptReset '" + txtReceiptNumber.Text + "','" + txtApproverComments.Text.Replace("'","''") + "','" + userId + "'," + Convert.ToInt32(isManager) + "," + Convert.ToInt32(isAdminUser));
                        MessageBox.Show("Receipt has been reset.", Globals.appName, MessageBoxButtons.OK);

                        GoHome();

                    }
                    else
                    {
                        MessageBox.Show("Receipt has already been processed and cannot be reset.", Globals.appName, MessageBoxButtons.OK);
                        GoHome();
                    }                    
                }
               
            }

            if (e.Button.Name == "tbApprove")
            {               

                if (txtApproverComments.Text == "")
                {
                    MessageBox.Show("Comments are required to process an approval.  Please enter a comment to continue.", Globals.appName, MessageBoxButtons.OK);
                    return;
                }

                DialogResult result = MessageBox.Show("Are you sure you want to approve this receipt?  ", Globals.appName, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //check status before resetting to ensure it is not already reset or approved
                    Procs.GetPPVReceiptUserAppprovalStatus(txtReceiptNumber.Text, userId, out int receiptStatus);
                    if (receiptStatus == 1)
                    {
                        Application.ShowLoader = true;
                        Application.Update(this);

                        //does this receipt require manager approval?
                        if (requiresManagerApproval == 1)
                        {
                            //is this user the manager?
                            if (isManager == true)
                            {
                                //if this is the manager it means it has passed first phase so continue to approve and post
                                approve = true;
                            }
                            else
                            {
                                //create manager approval record
                                Procs.ExecuteSQL("execute nzbWFPPVCreateReceiptManagerApproval '" + txtReceiptNumber.Text + "','" + managerAccountName + "','" +
                                                managerEmailAddress + "','" + txtApproverComments.Text.Replace("'", "''") + "','" + userId + "'," + Convert.ToInt32(isManager) + "," + Convert.ToInt32(isAdminUser));
                            }

                        }
                        //if this receipt does not require manager approval then continue to approve and post
                        if (requiresManagerApproval == 0)
                        {
                            approve = true;
                        }

                        if (approve == true)
                        {
                            Procs.ExecuteSQL("execute nzbWFPPVReceiptApproval '" + txtReceiptNumber.Text + "','" + txtApproverComments.Text.Replace("'", "''") + "','" + userId + "'," + Convert.ToInt32(isManager) + "," + Convert.ToInt32(isAdminUser));
                        }

                        MessageBox.Show("Receipt has been approved.", Globals.appName, MessageBoxButtons.OK);

                        GoHome();

                    }
                    else
                    {
                        MessageBox.Show("Receipt has already been processed and cannot be approved.", Globals.appName, MessageBoxButtons.OK);
                        GoHome();
                    }
                }
            }

            if (e.Button.Name == "tbReject")
            {
                if (txtApproverComments.Text == "")
                {
                    MessageBox.Show("Comments are required to process a rejection.  Please enter a comment to continue.", Globals.appName, MessageBoxButtons.OK);
                    return;
                }
                
                DialogResult result = MessageBox.Show("Are you sure you want to reject this receipt?  ", Globals.appName, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //check status before resetting to ensure it is not already reset or approved
                    Procs.GetPPVReceiptUserAppprovalStatus(txtReceiptNumber.Text, userId, out int receiptStatus);
                    if (receiptStatus == 1)
                    {
                        Application.ShowLoader = true;
                        Application.Update(this);
                        Procs.ExecuteSQL("execute nzbWFPPVReceiptReject '" + txtReceiptNumber.Text + "','" + txtApproverComments.Text.Replace("'", "''") + "','" + userId + "'," + Convert.ToInt32(isManager) + "," + Convert.ToInt32(isAdminUser));
                        MessageBox.Show("Invoice has been rejected.", Globals.appName, MessageBoxButtons.OK);

                        //string rejectMessage = "The receipt " + txtReceiptNumber.Text + " has been rejected by " + userId + " with the following comments." + Environment.NewLine + Environment.NewLine;
                        //rejectMessage = rejectMessage + txtApproverComments.Text;
                        //Procs.SendEmail(lblSubmittedUserEmailAddress.Text, userEmail, "Receipt Variance Rejection - " + txtReceiptNumber.Text,rejectMessage);

                        GoHome();

                    }
                    else
                    {
                        MessageBox.Show("Receipt has already been processed and cannot be rejected.", Globals.appName, MessageBoxButtons.OK);
                        GoHome();
                    }
                }
                
            }
        }

        private void PPVApproval_Load(object sender, EventArgs e)
        {
            userId = Application.Session["UserID"];
            userEmail = Application.Session["UserEmail"];
            isAdminUser = Application.Session["IsAdminUser"];
            //managerAccountName = Application.Session["ManagerAccountName"];
            //managerEmailAddress = Application.Session["ManagerEmailAddress"];
            isManager = false;
            //isManager=Application.Session["isManager"];
            //lblCommentHelp.Text = ConfigurationManager.AppSettings["PPVApprovalCommentNote"];
            sbcUserName.Text = userId;
            lblHistory.Visible = false;
            dgReceiptHistory.Visible = false;

            txtApproverComments.Watermark= ConfigurationManager.AppSettings["PPVApprovalCommentNote"];

            Procs.GetRecordCount("select * from nzbPPVSetup where ManagerADAccount='" + userId + "' and Active='Yes' and ApprovalID='PPV1'", out int recCount);
            if (recCount != 0)
            {
                isManager = true;

                Procs.GetManagerEmailAddress(userId, out string managerEmailAddress);
                Application.Session["ManagerAccountName"] = userId; //ConfigurationManager.AppSettings["ManagerAccountName"];
                Application.Session["ManagerEmailAddress"] = managerEmailAddress; //ConfigurationManager.AppSettings["ManagerEmailAddress"];

            }

            //if (userId == managerAccountName)
            //{
            //    isManager = true;
            //}

            //MessageBox.Show("Requires Manager Approval: " + requiresManagerApproval.ToString());
            //MessageBox.Show("Is Manager: " + isManager.ToString());

            Cursor = Cursors.Default;

            if (isAdminUser | isManager)
            {
                lblHistory.Visible = true;
                dgReceiptHistory.Visible = true;
            }
            
            if (isAdminUser)
            {
                tbReset.Visible = true;
                tbApprove.Enabled = false;
                tbReject.Enabled = false;
                //lblHistory.Visible = true;
                //dgReceiptHistory.Visible = true;
            }
            else
            {
                tbReset.Visible = false;
                tbApprove.Enabled = true;
                tbReject.Enabled = true;
                //lblHistory.Visible = false;
                //dgReceiptHistory.Visible = false;
            }

            if (txtReceiptNumber.Text != "")
            {
                fillPPVItems();
                Procs.GetPPVComment(txtReceiptNumber.Text, out string ppvComment);
                txtComments.Text = ppvComment;
                fillAttachmentList();
                fillHistory();
               
            }
        }

        private void txtReceiptNumber_TextChanged(object sender, EventArgs e)
        {
            
        }

        void fillPPVItems()
        {
            string sql = "select rtrim(ItemNumber) ItemNumber, rtrim(ItemDescription) ItemDescription, rtrim(VendorItemNumber) VendorItemNumber,Variance,POCost,InvoiceCost,rtrim(PONumber) PONumber,rtrim(ShipmentNumber) ShipmentNumber " + 
                                    " from nzbWFPPVTransactionLne where ReceiptNumber='" + txtReceiptNumber.Text.TrimEnd() + "'";

            
            using (SqlConnection sqlcon = new SqlConnection(Globals.sqlConn))
            {
                SqlDataAdapter sqldat = new SqlDataAdapter(sql, sqlcon);
                DataTable dtbl = new DataTable();
                sqldat.Fill(dtbl);
                dgPPVItems.DataSource = dtbl;
            }

            if (dgPPVItems.SelectedRows.Count > 0)
            {
                dgPPVItems.ClearSelection();
            }

            //foreach (DataGridViewRow row in dgPPVItems.Rows)
            //{
            //    Console.WriteLine(row.Index);
            //    if (row.Index == 0 | row.Index % 2 == 0)
            //    {
            //        row.DefaultCellStyle.BackColor = System.Drawing.Color.AliceBlue;
            //    }

            //}

        }

        void fillAttachmentList()
        {
            
            try
            {
                string sql = "select co3.filename,co1.attachment_id " +
                                "from CO00102 co1 " +
                                "join CO00101 co3 on co3.Attachment_ID = co1.Attachment_ID " +
                                "join coAttachmentItems co2 on co1.Attachment_ID = co2.Attachment_ID " +
                                "where BusObjKey like '%" + txtReceiptNumber.Text + "%' and DELETE1=0";

                SqlConnection cnn = new SqlConnection(Globals.sqlConn);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = sql;
                cnn.Open();
                SqlDataReader rdc = cmd.ExecuteReader();

                string resources=HttpRuntime.AppDomainAppPath;

                var imageList = new ImageList();
                imageList.Images.Add("dynGPImage", Bitmap.FromFile(resources + "\\icon\\DynamicsGP.ico"));
                imageList.Images.Add("wordImage", Bitmap.FromFile(resources + "\\icon\\Word.ico"));
                imageList.Images.Add("outlookImage", Bitmap.FromFile(resources + "\\icon\\Outlook_MSG.ico"));
                imageList.Images.Add("pdfImage", Bitmap.FromFile(resources + "\\icon\\AcroRd32.ico"));
                imageList.Images.Add("xlImage", Bitmap.FromFile(resources + "\\icon\\Excel.ico"));
                lvAttachments.SmallImageList = imageList;

                while (rdc.Read())
                {
                    string fileName = rdc["filename"].ToString().TrimEnd();
                    string attachmentId = rdc["attachment_id"].ToString().TrimEnd();

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

                if(lvAttachments.Items.Count == 0)
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

        void fillHistory()
        {
            string sql = "select StatusDateTime, rtrim(UserId) UserID, case StatusId when 1 then 'Submitted' when 2 then 'Approved' when 3 then 'Rejected' else 'Reset' end as 'StatusId',rtrim(Comment) Comment " +
                                    "from nzbWFPPVInvoiceHistory  where ReceiptNumber='" + txtReceiptNumber.Text.TrimEnd() + "' order by StatusDateTime desc";


            using (SqlConnection sqlcon = new SqlConnection(Globals.sqlConn))
            {
                SqlDataAdapter sqldat = new SqlDataAdapter(sql, sqlcon);
                DataTable dtbl = new DataTable();
                sqldat.Fill(dtbl);
                dgReceiptHistory.DataSource = dtbl;
            }

            //foreach (DataGridViewRow row in dgReceiptHistory.Rows)
            //{
            //    Console.WriteLine(row.Index);
            //    if (row.Index == 0 | row.Index % 2 == 0)
            //    {
            //        row.DefaultCellStyle.BackColor = System.Drawing.Color.AliceBlue;
            //    }

            //}
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
                                        
                    Procs.DownloadDocumentNoGUID(attachmentId, out string previewFile);

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

                    //    //MessageBox.Show("This file type cannot be viewed at this time.", Globals.appName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    //Application.DownloadAndOpen("_blank", fileName);
                    //    //Process.Start(previewFile);

                        
                        
                    //}

                }

               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, Globals.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            

        }

        private void PPVApproval_Click(object sender, EventArgs e)
        {
            //close the preview form if the user clicks away (auto close does not work)
            if (fPreview.ActiveForm != null)
            {
                fPreview.ActiveForm.Close();
            }
        }

        private void dgPPVItems_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == poNumberCol)
            {
                Cursor = Cursors.Hand;
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }

        private void dgPPVItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == poNumberCol)
            {
                try
                {
                    Application.ShowLoader = true;
                    Application.Update(this);

                    int rowIndex = dgPPVItems.CurrentCell.RowIndex;

                    string poNumber = dgPPVItems.Rows[rowIndex].Cells["poNumber"].Value.ToString();
                    string receiptNumber = txtReceiptNumber.Text;
                    string fileName="";

                    SqlConnection cnn = new SqlConnection(Globals.sqlConn);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = "select top 1 (POBinary) from nzbWFPPVTransactionLne where ReceiptNumber='" + receiptNumber + "' and PONumber='" + poNumber + "'";

                    try
                    {
                        cnn.Open();
                        SqlDataReader rd = cmd.ExecuteReader();

                        while (rd.Read())
                        {
                            string processfolder = Globals.outputTemp;
                            fileName = processfolder + poNumber + ".pdf";

                            byte[] fileData = (byte[])rd["POBinary"];
                            File.WriteAllBytes(fileName, fileData);
                            
                        }

                        if (File.Exists(fileName))
                        {
                            //var pdfv = new fPreview();
                            //pdfv.Show();
                            //pdfv.pdfView.PdfSource = fileName;

                            using (Stream stream = new FileStream(fileName, FileMode.Open))
                            {
                                Application.Download(stream, fileName);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Globals.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Globals.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void lvAttachments_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgPPVItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
