using CommandLine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuwareDiagnose
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<DummyCommand, VersionDiagnosticCommand>(args).WithParsed<DiagnosticCommand>(t => t.Execute());
        }
    }

    [Verb("dummy")]
    class DummyCommand : DiagnosticCommand
    {
        public override void PerformQuery()
        {
            throw new System.NotImplementedException();
        }
    }
}
