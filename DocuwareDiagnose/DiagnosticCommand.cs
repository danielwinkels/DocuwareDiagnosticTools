using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using DocuWare.Platform.ServerClient;

namespace DocuwareDiagnosis
{
    abstract internal class DiagnosticCommand
    {
        protected const string SEPERATOR = ";";
        protected ServiceConnection PlatformClient { get; set; }

        [Option("platform", HelpText = "URI to the DocuWare Platform.", Required = true)]
        public string Platform { get; set; }

        [Option('u', "username", HelpText = "DocuWare Username", SetName = "DocuwareLogin")]
        public string Username { get; set; }

        [Option('p', "password", HelpText = "DocuWare Password", SetName = "DocuwareLogin")]
        public string Password { get; set; }

        [Option("sso", HelpText = "Use single sign on.")]
        public bool SingleSignOn { get; set; }

        //[Option("trustedUsername", SetName = "TrustedLogin")]
        //public string TrustedUsername { get; set; }

        //[Option("impersonatedUsername", SetName = "TrustedLogin")]
        //public string ImpersonatedUsername { get; set; }

        //[Option("trustedPassword", SetName = "TrustedLogin")]
        //public string TrustedPassword { get; set; }


        public abstract DiagnosticResult PerformDiagnosis();

        protected void Connect()
        {
            Uri platformUri = new Uri(Platform);
            if (SingleSignOn)
            {
                PlatformClient = ServiceConnection.CreateWithWindowsAuthentication(platformUri, System.Net.CredentialCache.DefaultCredentials);
            }
            else
            {
                if (Username != null)
                {
                    PlatformClient = ServiceConnection.Create(platformUri, Username, Password);
                }
            }


        }

        public void Execute()
        {
            try
            {
                Connect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            DiagnosticResult result = PerformDiagnosis();
            Console.WriteLine(result.Summary);
            Disconnect();
        }

        public void Disconnect()
        {
            PlatformClient.Disconnect();
        }
    }
}
