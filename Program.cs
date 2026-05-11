using System;
using System.Configuration;
using System.Drawing;
using System.Web;
//using System.Web.Providers.Entities;
using Wisej.Web;

namespace NZBlood.ApprovalWorkflowsWI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 


        static void Main()
        {
            Application.Title = "Approvals Workflow";
            Application.FavIconSource = "DynamicsGP.ico";

            //get user information and store in globals or sessions
            Globals.sqlConn=ConfigurationManager.AppSettings["SQLConnectionString"];
            //Globals.adminUsers = ConfigurationManager.AppSettings["AdminUsers"];
            Globals.outputTemp = ConfigurationManager.AppSettings["OutputTEMP"];
            //Globals.smtpServer= ConfigurationManager.AppSettings["SMTPServer"];
            //Globals.smtpUserId = ConfigurationManager.AppSettings["SMTPUserID"];
            //Globals.smtpUserPass = ConfigurationManager.AppSettings["SMTPUserPass"];
            //Globals.smtpPort = ConfigurationManager.AppSettings["SMTPPort"];
            //Globals.smtpSSL = ConfigurationManager.AppSettings["SMTPSSL"];
            Application.Session["IsManager"] = false;

            string userId = HttpContext.Current.User.Identity.Name;            

            if (userId == "")
            {
                userId = Environment.UserName;
            }

            Globals.domainName = ConfigurationManager.AppSettings["DomainName"]; ;
            //Application.Session["UserID"]= userId.Replace(@"NZBLOOD\", "");
            Application.Session["UserID"] = userId.Replace(Globals.domainName + @"\", "");

            Procs.GetRecordCount("select * from nzbPPVSetup where ManagerADAccount='" + userId + "' and Active='Yes' and ApprovalID='PPV1'", out int recCount);
            if (recCount != 0)
            {
                Application.Session["IsManager"] = true;

                Procs.GetManagerEmailAddress(userId, out string managerEmailAddress);
                Application.Session["ManagerAccountName"] = userId; //ConfigurationManager.AppSettings["ManagerAccountName"];
                Application.Session["ManagerEmailAddress"] = managerEmailAddress; //ConfigurationManager.AppSettings["ManagerEmailAddress"];

            }

            //Procs.GetManagerDetails(out string managerAccount, out string managerEmailAddress);
            //Application.Session["ManagerAccountName"] = managerAccount; //ConfigurationManager.AppSettings["ManagerAccountName"];
            //Application.Session["ManagerEmailAddress"] = managerEmailAddress; //ConfigurationManager.AppSettings["ManagerEmailAddress"];

            //if(userId.ToUpper() == managerAccount.ToUpper())
            //{
            //    Application.Session["IsManager"] = true;
            //}

            if (userId != "")
            {
                Application.Session["UserEmail"] = Procs.GetUserEmail(userId.Replace(Globals.domainName + @"\", ""));
            }

            Procs.IsAdminUser(out bool isAdminUser);
            Application.Session["IsAdminUser"] = isAdminUser;

            //Application.Theme.Colors["table-row-background-odd"] = Color.AliceBlue;
            //Application.Theme.Colors["table-row-background-even"] = Color.Yellow;
            Application.MainPage = new Home();
            

        }

        //
        // You can use the entry method below
        // to receive the parameters from the URL in the args collection.
        //
        //static void Main(NameValueCollection args)
        //{
        //}
    }
}