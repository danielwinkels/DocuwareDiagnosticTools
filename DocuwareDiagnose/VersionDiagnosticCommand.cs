using System;
using DocuWare.Platform.ServerClient;
using CommandLine;

namespace DocuwareDiagnose
{
    [Verb("dwversion")]
    class VersionDiagnosticCommand : DiagnosticCommand
    {
        public override void PerformQuery()
        {
            Console.WriteLine(PlatformClient.ServiceDescription.Version);
        }
    }
}
