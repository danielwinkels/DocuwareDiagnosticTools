using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using DocuWare.Platform.ServerClient;

namespace DocuwareDiagnose
{
    abstract internal class DiagnosticCommand
    {
        protected ServiceConnection PlatformClient { get; set; }

        [Option("platform", HelpText = "URI to the DocuWare Platform.", Required = true)]
        public String Platform { get; set; }

        [Option('u', "username", HelpText = "DocuWare Username")]
        public String Username { get; set; }

        [Option('p', "password", HelpText = "DocuWare Password")]
        public String Password { get; set; }

        [Option("sso", HelpText = "Use single sign on.")]
        public Boolean SingleSignOn { get; set; }

        public abstract void PerformQuery();

        protected void Connect()
        {
            Uri platformUri = new Uri(Platform);
            if (SingleSignOn)
            {
                PlatformClient = ServiceConnection.CreateWithWindowsAuthentication(platformUri, System.Net.CredentialCache.DefaultCredentials);
            } else
            {

                PlatformClient = ServiceConnection.Create(platformUri, Username, Password);
            }

        }

        public void Execute()
        {
            Connect();
            PerformQuery();
            Disconnect();
        }

        public void Disconnect()
        {
            PlatformClient.Disconnect();
        }
    }
}
