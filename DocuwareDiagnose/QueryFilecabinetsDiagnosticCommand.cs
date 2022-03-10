using CommandLine;
using DocuWare.Platform.ServerClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuwareDiagnosis
{
    [Verb("listFilecabinets")]
    internal class QueryFilecabinetsDiagnosticCommand : DiagnosticCommand
    {
        [Option('o', "organization", HelpText = "GUID of the organization")]
        public string OrganizationGuid { get; set; }

        public override DiagnosticResult PerformDiagnosis()
        {
            Organization organization = PlatformClient.Organizations.Where(org => org.Guid == OrganizationGuid).First();

            DiagnosticResult result = new DiagnosticResult
            {
                Summary = string.Join("\n", organization.GetFileCabinetsFromFilecabinetsRelation().FileCabinet.Where(item => item.IsBasket == false).Select(item => item.GetFileCabinetFromSelfRelation()).Select(item => item.Name + SEPERATOR + item.Id))
            };
            return result;
        }
    }

}
