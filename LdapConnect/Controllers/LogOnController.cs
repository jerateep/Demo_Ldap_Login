using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LdapConnect.Controllers
{
    public class LogOnController : Controller
    {
        string initLDAPPath = "dc=<dsquery>, dc=<dsquery>";
        string initLDAPServer = "<ip server ad>";
        string initShortDomainName = "rfs";
        string strErrMsg;
        // GET: LogOn
        public ActionResult Index(string User, string Pwd)
        {
            string DomainAndUsername = "";
            string strCommu;
            bool flgLogin = false;
            strCommu = ("LDAP://"
                        + (initLDAPServer + ("/" + initLDAPPath)));
            DomainAndUsername = (initShortDomainName + ("\\" + User));
            DirectoryEntry entry = new DirectoryEntry(strCommu, DomainAndUsername, Pwd);
            object obj;
            try
            {
                obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                SearchResult result;
                search.Filter = ("(SAMAccountName="
                            + (User + ")"));
                search.PropertiesToLoad.Add("cn");
                result = search.FindOne();
                if ((result == null))
                {
                    flgLogin = false;
                    strErrMsg = "Please check user/password";
                }
                else
                {
                    flgLogin = true;
                }
            }
            catch (Exception ex)
            {
                flgLogin = false;
                strErrMsg = "Please check user/password";
            }
            if ((flgLogin == true))
            {
                //this.lbDisplay.Text = ("Welcome " + txtUser.Text);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //this.lbDisplay.Text = strErrMsg;
                //return RedirectToAction("Index", "LogOn");
            }
            return View();
        }
    }
}