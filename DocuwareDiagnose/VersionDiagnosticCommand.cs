using System;
using DocuWare.Platform.ServerClient;
using CommandLine;

namespace DocuwareDiagnosis
{
    [Verb("dwversion")]
    class VersionDiagnosticCommand : DiagnosticCommand
    {
        public override DiagnosticResult PerformDiagnosis()
        {
            DiagnosticResult result = new DiagnosticResult();
            result.Summary = PlatformClient.ServiceDescription.Version;
            return result;
        }
    }
}
