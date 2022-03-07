using CommandLine;
using DocuWare.Platform.ServerClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuwareDiagnosis
{
    [Verb("listOrganizations")]
    internal class QueryOrganizationsDiagnosticCommand : DiagnosticCommand
    {
        public override DiagnosticResult PerformDiagnosis()
        {
            return new DiagnosticResult
            {
                Summary = string.Join(",", PlatformClient.Organizations.Select(t => t.Name + " " + t.Guid))
            };

        }
    }


    [Verb("listUsers")]
    internal class QueryUsersByOrganizationDiagnosticCommand : DiagnosticCommand
    {
        [Option('o', "organization", HelpText = "GUID of the organization")]
        public string OrganizationGuid { get; set; }

        public override DiagnosticResult PerformDiagnosis()
        {
            Organization organization = PlatformClient.Organizations.Where(org => org.Guid == OrganizationGuid).First();
            DiagnosticResult result = new DiagnosticResult
            {
                Summary = string.Join(",", organization.GetUsersFromUsersRelation().User.Select(user => user.Name))
            };
            return result;
        }
    }
}
