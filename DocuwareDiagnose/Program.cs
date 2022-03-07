using CommandLine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuwareDiagnosis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<QueryOrganizationsDiagnosticCommand, VersionDiagnosticCommand, QueryUsersByOrganizationDiagnosticCommand>(args).WithParsed<DiagnosticCommand>(t => t.Execute());
        }
    }

    [Verb("dummy")]
    class DummyCommand : DiagnosticCommand
    {
        public override DiagnosticResult PerformDiagnosis()
        {
            throw new System.NotImplementedException();
        }
    }
}
