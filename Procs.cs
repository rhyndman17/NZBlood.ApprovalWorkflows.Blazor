using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Wisej.Web;

namespace NZBlood.ApprovalWorkflowsWI
{
    public class Procs
    {

        public static string GetUserEmail(string UserId)
        {
            if (UserId == "rhadmin") { UserId = "HyndmanR"; }

            var searcher = new DirectorySearcher("LDAP://" + UserId.Split('\\').First().ToLower())
            {
                Filter = "(&(ObjectClass=person)(sAMAccountName=" + UserId.Split('\\').Last().ToLower() + "))"
            };

            var result = searcher.FindOne();
            if (result == null)
                return string.Empty;

            return result.Properties["mail"][0].ToString();

        }

        public static void GetRecordCount(string sql, out int iRecordCount)
        {
            iRecordCount = 0;
            SqlConnection cnn = new SqlConnection(Globals.sqlConn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = sql;
            cnn.Open();
            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                iRecordCount++;
            }

            cnn.Close();
        }

        public static void GetPPVComment(string receiptNumber, out string ppvComment)
        {
            ppvComment = "";
            SqlConnection cnn = new SqlConnection(Globals.sqlConn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = "select Comment from nzbWFPPVTransactionHdr where ReceiptNumber='" + receiptNumber + "'";
            cnn.Open();
            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                ppvComment = rd["Comment"].ToString();

            }

            cnn.Close();
        }

        public static void DownloadGLInvoiceAttachment(string attachmentID, out string outFile)
        {
            outFile = "";

            try
            {
                string sql = "select FileName,FileBinary from nzbWFGLIAttachments  " +
                               " where RecId = '" + attachmentID + "'";

                SqlConnection cnn = new SqlConnection(Globals.sqlConn);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = sql;
                cnn.Open();
                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    string filename = rd["FileName"].ToString().TrimEnd();
                    byte[] data = (byte[])rd["FileBinary"];
                    outFile = Globals.outputTemp + filename;
                    File.Delete(outFile);
                    File.WriteAllBytes(outFile, data);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Globals.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public static void DownloadDocumentNoGUID(string attachmentID, out string outFile)
        {
            outFile = "";

            try
            {
                string sql = "select binaryblob,co2.filename,co1.attachment_id from coAttachmentItems co1 " +
                               "join CO00101 co2 on co1.Attachment_ID = co2.Attachment_ID where co1.attachment_id = '" + attachmentID + "'";

                SqlConnection cnn = new SqlConnection(Globals.sqlConn);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = sql;

                cnn.Open();
                SqlDataReader rd = cmd.ExecuteReader();

                //Guid attachFileGuid = Guid.NewGuid();
                while (rd.Read())
                {
                    string filename = rd["fileName"].ToString().TrimEnd();
                    byte[] data = (byte[])rd["binaryblob"];
                    outFile = Globals.outputTemp + filename;
                    File.Delete(outFile);
                    File.WriteAllBytes(outFile, data);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Globals.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public static void GetManagerDetails(out string managerAccount, out string managerEmailAddress)
        {
            managerAccount = "";
            managerEmailAddress = "";

            try
            {
                SqlConnection cnn = new SqlConnection(Globals.sqlConn);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = "select PropertyName,PropertyValue from SY90000 where ObjectType='WF' and ObjectID='WF'"; // and PropertyName='WFPPVMANAGER'";               

                cnn.Open();
                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    if (rd["PropertyName"].ToString().TrimEnd() == "WFPPVMANAGER"){managerAccount = rd["PropertyValue"].ToString().TrimEnd();}
                    if (rd["PropertyName"].ToString().TrimEnd() == "WFPPVMANAGEREMAILADDRESS"){managerEmailAddress = rd["PropertyValue"].ToString().TrimEnd();}
                }

                cnn.Close();

            }
            catch (Exception ex){MessageBox.Show(ex.Message);}



        }

        public static void IsAdminUser(out bool adminUser)
        {
            string userId = Application.Session["UserID"];
            //string userId = "HyndmanR";
            if (userId == "rhadmin"){ userId = "HyndmanR"; }

            adminUser = false;

            try
            {
                SqlConnection cnn = new SqlConnection(Globals.sqlConn);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                //cmd.CommandText = "select UserId " +
                //                    "from nzbWFSecurityRoles " +
                //                    "where PropertyName='WFPPVADMINUSERSROLE'";

                cmd.CommandText = "select ADUserID from nzbApprovalAdmins where Inactive='No'";
                 
                cnn.Open();
                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    if (rd["ADUserID"].ToString().TrimEnd().ToUpper() == userId.ToUpper())
                    {
                        adminUser = true;
                    }
                }

                cnn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        public static void GetPPVReceiptResetlStatus(string receiptNumber, out int receiptOkToReset)
        {            
            string sql = "select count(*) recCount from nzbWFPPVInvoiceStatus where ReceiptNumber='" + receiptNumber + "'";
            receiptOkToReset = 0;

            SqlConnection cnn = new SqlConnection(Globals.sqlConn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = sql;
            cnn.Open();
            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                receiptOkToReset = Convert.ToInt32(rd["recCount"]);
            }

            cnn.Close();
        }

        public static void GetGLInvoiceStatus(int docId, out int status)
        {
            status = 0;
            string sql= "select DocStatus from nzbWFGLITransactionHdr where Id=" + docId ;
           
            SqlConnection cnn = new SqlConnection(Globals.sqlConn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = sql;
            cnn.Open();
            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                status = Convert.ToInt32(rd["DocStatus"]);
            }

            cnn.Close();
        }

        public static void GetManagerEmailAddress(string managerId, out string emailAddress)
        {
            emailAddress = "";
            string sql = "select top 1 ManagerEmailAddress from nzbPPVSetup where ManagerADAccount='" + managerId + "'";

            SqlConnection cnn = new SqlConnection(Globals.sqlConn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = sql;
            cnn.Open();
            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                emailAddress = Convert.ToString(rd["ManagerEmailAddress"]);
            }

            cnn.Close();
        }

        public static void GetPPVReceiptUserAppprovalStatus(string receiptNumber,string approverName, out int statusId)
        {
            statusId = 0;
            string sql;

            if (Application.Session["IsAdminUser"])
            {
                sql = "select count(*) recCount from nzbWFPPVApproverList where ReceiptNumber='" + receiptNumber + "'";
            }
            else
            {
                sql="select count(*) recCount from nzbWFPPVApproverList where ReceiptNumber='" + receiptNumber + "' and AccountName='" + approverName + "'";
            }

           
            SqlConnection cnn = new SqlConnection(Globals.sqlConn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = sql;
            //cmd.CommandText = "select count(*) recCount from nzbWFPPVInvoiceStatus where ReceiptNumber='" + receiptNumber + "' and StatusId=1";
            cnn.Open();
            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                statusId = Convert.ToInt32(rd["recCount"]);
            }

            cnn.Close();
        }

        public static void ExecuteSQL(string sSQL)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(Globals.sqlConn))
                {
                    using (SqlCommand cmd = new SqlCommand(sSQL, cnn))
                    {
                        Console.Write(sSQL);
                        cnn.Open();
                        cmd.CommandTimeout = 600;
                        cmd.ExecuteNonQuery();
                    }

                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,Globals.appName,MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


        }

        public static void GetPPVApprovers(string receiptNumber,out string approvers)
        {
            approvers = "";
            SqlConnection cnn = new SqlConnection(Globals.sqlConn);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("nzbWFGetPPVApprovers", cnn);
            //sql.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@receiptnumber", receiptNumber);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cnn.Open();
            SqlDataReader rd = cmd.ExecuteReader();

            try
            {
                while (rd.Read())
                {
                    approvers = rd["Approvers"].ToString().TrimEnd();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Globals.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public static void SendEmail(string recipient, string from, string reference, string message)
        {
            try
            {
                int smtpport = Convert.ToInt32(Globals.smtpPort);

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(Globals.smtpServer);

                SmtpServer.Port = smtpport;
                SmtpServer.Credentials = new System.Net.NetworkCredential(Globals.smtpUserId, Globals.smtpUserPass);
                SmtpServer.EnableSsl = Convert.ToBoolean(Globals.smtpSSL);

                mail.From = new MailAddress(from);
                mail.ReplyToList.Add(from);
                mail.To.Add(recipient.Replace(" ", ""));

                mail.Body = message;
                mail.Subject = reference;
                SmtpServer.Send(mail);
                SmtpServer.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Globals.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public static void GetEmailMessageBody(string messageID, out string messageBodyText)
        {
            
            messageBodyText = "";
            string sql = "select EmailMessageBody from SY04901 where EmailMessageID='" + messageID + "'";
            SqlConnection cnn = new SqlConnection(Globals.sqlConn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = sql;
            cnn.Open();
            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                messageBodyText = Convert.ToString(rd.GetValue(0));

            }

            cnn.Close();

        }
    }
}
