using CommandLine;
using DocuWare.Platform.ServerClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuwareDiagnosis
{
    [Verb("listOrganizations", HelpText = "Alle Organisationen eines Systems auflisten.")]
    internal class QueryOrganizationsDiagnosticCommand : DiagnosticCommand
    {
        public override DiagnosticResult PerformDiagnosis()
        {
            return new DiagnosticResult
            {
                Summary = string.Join("\n", PlatformClient.Organizations.Select(t => t.Name + SEPERATOR + t.Guid))
            };

        }
    }


    [Verb("listUsers", HelpText = "Alle Benutzer einer Organisation auflisten.")]
    internal class QueryUsersByOrganizationDiagnosticCommand : DiagnosticCommand
    {
        [Option('o', "organization", HelpText = "GUID der Organisation.")]
        public string OrganizationGuid { get; set; }

        public override DiagnosticResult PerformDiagnosis()
        {
            Organization organization = PlatformClient.Organizations.Where(org => org.Guid == OrganizationGuid).First();
            DiagnosticResult result = new DiagnosticResult
            {
                Summary = string.Join("\n", organization.GetUsersFromUsersRelation().User.Select(user => user.Name))
            };
            return result;
        }
    }
}
