using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZBlood.ApprovalWorkflowsWI
{
    
    public class Globals
    {
        //public static string userId;
        //public static string userEmail;
        public static string sqlConn;
        public static int ppvApprovalCount;
        public static int gliApprovalCount;
        public static string adminUsers;
        public static bool  isManager;
        public static string domainName;
        //public static bool isAdminUser;
        public static string appName="Approvals Workflow";
        public static string outputTemp;
        public static int pdfPreviewFormStatus;
        public static string sessionId;

        public static string smtpServer;
        public static string smtpUserId;
        public static string smtpUserPass;
        public static string smtpPort;
        public static string smtpSSL;
        //public static string ppvRejectMessageId;

        //public static string PPVDashboardSQL = "select s.ReceiptNumber,h.VendorDocNumber,h.VendorID,rtrim(h.VendorName) VendorName,s.UserId 'SubmittedBy',s.StatusDateTime 'SubmittedDateTime',h.UserEmailAddress, s.ManagerApproval " +
        //                                        "from nzbWFPPVInvoiceStatus s " +
        //                                        "join nzbWFPPVTransactionHdr h on h.ReceiptNumber=s.ReceiptNumber " +
        //                                        "join nzbWFPPVApproverList a on a.ReceiptNumber= s.ReceiptNumber " +
        //                                        "where s.StatusId=1 ";
    }
}
