using System;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using Wisej.Web;
using System.Security.Principal;
using System.Web;
using System.DirectoryServices;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace NZBlood.ApprovalWorkflowsWI
{
    public partial class Home : Page
    {
        private string sql;
        private int editReceiptCol = 11;
        private int editInvoiceCol = 11;
        private string userId;
        private string userEmail;
        private bool isAdminUser;
        private bool isManager;

        public Home()
        {
            InitializeComponent();
        }

        private void activate()
        {
            userId = Application.Session["UserID"];
            userEmail = Application.Session["UserEmail"];
            isAdminUser = Application.Session["IsAdminUser"];
            isManager = Application.Session["IsManager"];
            sbcUserName.Text = userId;
                        
            fillPPVDocuments();
            fillGLIDocuments();

            //Globals.ppvApprovalCount = 0;
            //Globals.gliApprovalCount = 0;
            //MessageBox.Show("ppv: " + Globals.ppvApprovalCount.ToString() + " gli: " + Globals.gliApprovalCount.ToString());

            lblDashboardMessage.Visible = false;
            //if (Globals.ppvApprovalCount + Globals.gliApprovalCount == 0) {lblDashboardMessage.Visible = true;}
            if (Globals.ppvApprovalCount == 0) { tbPPV.Visible = false; }
            if (Globals.gliApprovalCount == 0) { tbGLI.Visible = false; }
            if (Globals.ppvApprovalCount >= Globals.gliApprovalCount) { tbApprovals.SelectedIndex = 0; } else { tbApprovals.SelectedIndex = 1; }


        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        void fillGLIDocuments()
        {
            
            sql = "select Id,VendorID,rtrim(VendorName) VendorName,DocumentNumber,LastSubmittedBy,LastSubmittedDate,Approvers,PurchaseOrderNumber,Comment,PurchaseAmount,LocationCode from nzbWFGLIGPDashboard where Status='*WORKFLOW' ";

            if (isAdminUser)
            {
                dgGLIDocuments.Columns["_approvers"].Visible = true;
            }
            else
            {
                sql += " and Id in (select Id from nzbWFGLIApproverList where AccountName='" + userId + "' )";
            }

            sql += " order by Id";

            Procs.GetRecordCount(sql, out Globals.gliApprovalCount);

            tbGLI.Text = "Invoices (" + Globals.gliApprovalCount + " remaining)";

            //if (Globals.gliApprovalCount == 0)
            //{
            //    dgGLIDocuments.Visible = false;
            //}

            using (SqlConnection sqlcon = new SqlConnection(Globals.sqlConn))
            {
                SqlDataAdapter sqldat = new SqlDataAdapter(sql, sqlcon);
                DataTable dtbl = new DataTable();
                sqldat.Fill(dtbl);
                dgGLIDocuments.DataSource = dtbl;
                
            }


            //foreach (DataGridViewRow row in dgGLIDocuments.Rows)
            //{
            //    if (row.Index == 0 | row.Index % 2 == 0)
            //    {
            //        row.DefaultCellStyle.BackColor = System.Drawing.Color.AliceBlue;
            //    }

            //}
        }

        void fillPPVDocuments()
        {
            
            sql = "select ReceiptNumber,VendorID,VendorName,VendorDocNumber,SubmittedBy,SubmittedByEmailAddress,SubmittedDateTime,Approvers,PurchaseOrders,ManagerApproval,ManagerApprovalDisplay,Comment,FirstLevelApprover from nzbWFPPVDashboard ";

            if (isManager)
            {
                //dgPPVDocuments.Columns["firstLevelApprover"].Visible = true;
            }

            if (isAdminUser)
            {
                //sql = Globals.PPVDashboardSQL + " group by s.ReceiptNumber,h.VendorDocNumber,h.VendorID,h.VendorName,s.UserId,h.UserEmailAddress ,s.StatusDateTime,s.ManagerApproval";
                dgPPVDocuments.Columns["approvers"].Visible = true;
                dgPPVDocuments.Columns["managerApprovalDisplay"].Visible = true;
                //dgPPVDocuments.Columns["firstLevelApprover"].Visible = true;
            }
            else
            {
                sql += " where ReceiptNumber in (select ReceiptNumber from nzbWFPPVApproverList where AccountName='" + userId + "' )";
            }

            sql += " order by ReceiptNumber ";

            Procs.GetRecordCount(sql, out Globals.ppvApprovalCount);
            tbPPV.Text = "Price Variances (" + Globals.ppvApprovalCount + " remaining)";

            //if (Globals.ppvApprovalCount == 0)
            //{
            //    //lblDashboardMessage.Visible = true;
            //    dgPPVDocuments.Visible = false;

            //}

            //if (Globals.ppvApprovalCount != 0)
            //{
            //    fillPPVDocuments(ppv_sql);
            //}
            using (SqlConnection sqlcon = new SqlConnection(Globals.sqlConn))
            {
                SqlDataAdapter sqldat = new SqlDataAdapter(sql, sqlcon);
                DataTable dtbl = new DataTable();
                sqldat.Fill(dtbl);
                dgPPVDocuments.DataSource = dtbl;
            }

            //foreach (DataGridViewRow row in dgPPVDocuments.Rows)
            //{
            //    if (row.Index == 0 | row.Index % 2 == 0)
            //    {
            //        row.DefaultCellStyle.BackColor = System.Drawing.Color.AliceBlue;
            //    }

            //}

        }

        private void dgPPVDocuments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            int receiptStatus;
            int rowIndex = dgPPVDocuments.CurrentCell.RowIndex;
            string receiptNumber = dgPPVDocuments.Rows[rowIndex].Cells["receiptNumber"].Value.ToString();

            if(e.ColumnIndex == editReceiptCol)
            {
                Procs.GetPPVReceiptUserAppprovalStatus(receiptNumber, userId, out receiptStatus);
                
                if (receiptStatus != 0)
                {
                    try
                    {
                        Application.ShowLoader = true;
                        Application.Update(this);

                        string vendorDocNo = dgPPVDocuments.Rows[rowIndex].Cells["vendorDocNumber"].Value.ToString();
                        string vendorId = dgPPVDocuments.Rows[rowIndex].Cells["vendorId"].Value.ToString();
                        string vendorName = dgPPVDocuments.Rows[rowIndex].Cells["vendorName"].Value.ToString();
                        string submittedUser = dgPPVDocuments.Rows[rowIndex].Cells["submittedBy"].Value.ToString();
                        string submittedEmailAddress = dgPPVDocuments.Rows[rowIndex].Cells["submittedEmailAddress"].Value.ToString();
                        int managerApproval = Convert.ToInt32(dgPPVDocuments.Rows[rowIndex].Cells["managerApproval"].Value.ToString());

                        var ppvapproval = new PPVApproval();
                        ppvapproval.lblFullUserDisplay.Text = lblFullUserDisplay.Text;
                        ppvapproval.txtReceiptNumber.Text = receiptNumber;
                        ppvapproval.txtVendorDocNo.Text = vendorDocNo;
                        ppvapproval.txtVendorID.Text = vendorId;
                        ppvapproval.txtVendorName.Text = vendorName;
                        ppvapproval.lblSubmittedUserID.Text = submittedUser;
                        ppvapproval.lblSubmittedUserEmailAddress.Text = submittedEmailAddress;
                        ppvapproval.requiresManagerApproval = managerApproval;

                        ppvapproval.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Globals.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Receipt has already been processed.", Globals.appName, MessageBoxButtons.OK);
                    activate();
                }
            }

            
        }

        private void dgPPVDocuments_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == editReceiptCol)
            {
                Cursor = Cursors.Hand;
            }
            else
            {
                Cursor = Cursors.Default;
            }

            dgPPVDocuments.Rows[e.RowIndex].Selected = true;

        }

        private void dgPPVDocuments_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            dgPPVDocuments.Rows[e.RowIndex].Selected = false;
        }

        private void lblPPVHeading_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_PanelCollapsed(object sender, EventArgs e)
        {

        }

        private void tbApprovals_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgGLIDocuments_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == editInvoiceCol)
            {
                Cursor = Cursors.Hand;
            }
            else
            {
                Cursor = Cursors.Default;
            }

            dgGLIDocuments.Rows[e.RowIndex].Selected = true;

        }

        private void dgGLIDocuments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            
            if (e.ColumnIndex == editInvoiceCol)
            {
                int rowIndex = dgGLIDocuments.CurrentCell.RowIndex;
                int _id = Convert.ToInt32(dgGLIDocuments.Rows[rowIndex].Cells["_id"].Value.ToString());
                Procs.GetGLInvoiceStatus(_id, out int status);

                if (status == 1)
                {
                    try
                    {
                        Application.ShowLoader = true;
                        Application.Update(this);

                        string id = dgGLIDocuments.Rows[rowIndex].Cells["_id"].Value.ToString();
                        string vendorDocNo = dgGLIDocuments.Rows[rowIndex].Cells["_vendorDocNumber"].Value.ToString().TrimEnd();
                        string vendorId = dgGLIDocuments.Rows[rowIndex].Cells["_vendorId"].Value.ToString().TrimEnd();
                        string vendorName = dgGLIDocuments.Rows[rowIndex].Cells["_vendorName"].Value.ToString().TrimEnd();
                        string comment = dgGLIDocuments.Rows[rowIndex].Cells["_comment"].Value.ToString().TrimEnd();
                        string purchaseAmount = dgGLIDocuments.Rows[rowIndex].Cells["purchaseAmount"].Value.ToString();
                        string rc = dgGLIDocuments.Rows[rowIndex].Cells["_rc"].Value.ToString().TrimEnd();
                        //string submittedUser = dgPPVDocuments.Rows[rowIndex].Cells["submittedBy"].Value.ToString();
                        //string submittedEmailAddress = dgPPVDocuments.Rows[rowIndex].Cells["submittedEmailAddress"].Value.ToString();

                        var gliApproval = new GLIApprove();
                        gliApproval.txtReference.Text = id;
                        gliApproval.txtVendorDocNo.Text = vendorDocNo;
                        gliApproval.txtVendorID.Text = vendorId;
                        gliApproval.txtVendorName.Text = vendorName;
                        //gliApproval.lblVendorName.Text = vendorName;
                        gliApproval.txtComments.Text = comment;
                        gliApproval.txtPurchaseAmount.Text = Convert.ToDecimal(purchaseAmount).ToString("C2");
                        gliApproval.txtRC.Text = rc;

                        gliApproval.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Globals.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Document has already been processed.", Globals.appName, MessageBoxButtons.OK);
                    activate();
                }
            }
            
        }
               
        private void Home_Activated(object sender, EventArgs e)
        {
            activate();
        }

        private void tbHomePage_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            if (e.Button.Name == "tbRefresh")
            {
                //GoHome();
                activate();
            }

            Cursor = Cursors.Default;
        }

        private void dgGLIDocuments_Click(object sender, EventArgs e)
        {

        }

        private void dgPPVDocuments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgGLIDocuments_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            dgGLIDocuments.Rows[e.RowIndex].Selected = false;
        }
    }

}
