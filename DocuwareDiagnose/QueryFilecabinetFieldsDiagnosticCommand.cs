using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using DocuWare.Platform.ServerClient;

namespace DocuwareDiagnosis
{

    [Verb("listFilecabinetFields", HelpText = "Alle Felder eines Archivs auflisten.")]
    internal class QueryFilecabinetFieldsDiagnosticCommand : DiagnosticCommand
    {
        [Option('o', "organization", HelpText = "GUID der Organisation.", Required = true)]
        public string OrganizationGuid { get; set; }

        [Option('f', "filcabinet", Required = true, HelpText = "GUID des Archivs.")]
        public string FilecabinetGuid { get; set; }

        public override DiagnosticResult PerformDiagnosis()
        {
            Organization organization = PlatformClient.Organizations.Where(org => org.Guid == OrganizationGuid).First();
            FileCabinet filecabinet = organization.GetFileCabinetsFromFilecabinetsRelation().FileCabinet.Where(fc => fc.Id == FilecabinetGuid).First();
            filecabinet = filecabinet.GetFileCabinetFromSelfRelation();
            Console.WriteLine(filecabinet.Name);
            DiagnosticResult result = new DiagnosticResult
            {
                Summary = String.Join("\n", filecabinet.Fields.Select(field => field.DisplayName + SEPERATOR + field.DWFieldType.ToString() + "(" + field.Length + ")" + SEPERATOR + field.DBFieldName))
            };
            return result;
        }
    }
}
