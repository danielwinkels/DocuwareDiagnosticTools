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
        protected ServiceConnection PlatformClient { get; set; }

        [Option("platform", HelpText = "URI to the DocuWare Platform.", Required = true)]
        public string Platform { get; set; }

        [Option('u', "username", HelpText = "DocuWare Username", Group = "Credentials")]
        public string Username { get; set; }

        [Option('p', "password", HelpText = "DocuWare Password")]
        public string Password { get; set; }

        [Option("sso", HelpText = "Use single sign on.", Group = "Credentials")]
        public bool SingleSignOn { get; set; }

        public abstract DiagnosticResult PerformDiagnosis();

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
