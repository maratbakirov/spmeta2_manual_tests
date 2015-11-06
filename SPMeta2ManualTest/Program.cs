using Microsoft.SharePoint.Client;
using SPMeta2.CSOM.Standard.Services;
using SPMeta2.Services.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPMeta2ManualTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var username = AppSettings.sharePointUserName;
            var targetSiteUrl = AppSettings.targetSiteUrl;



            using (ClientContext context = new ClientContext(targetSiteUrl))
            {

                //Console.WriteLine("enter your password");
                //var password = Console.ReadLine();
                //var securedPassword = new SecureString();

                //foreach (var c in password.ToCharArray())
                //{
                //    securedPassword.AppendChar(c);
                //}



                context.RequestTimeout = Timeout.Infinite;

                var securedPassword = Helpers.GetPassword();

                context.Credentials = new SharePointOnlineCredentials(username, securedPassword);

                var service = new StandardCSOMProvisionService();
                service.PreDeploymentServices.Add(new DefaultRequiredPropertiesValidationService());

                SimpleModel.Provision(context,service);

            }

        }
        }
}
