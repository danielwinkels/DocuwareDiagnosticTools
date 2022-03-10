using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using DocuWare.Platform.ServerClient;

namespace DocuwareDiagnosis
{
    
    internal class UpdateDocumentDiagnosticCommand : DiagnosticCommand
    {

        public override DiagnosticResult PerformDiagnosis()
        {
            Organization organization = PlatformClient.Organizations[0];

            List<FileCabinet> filecabinets = organization.GetFileCabinetsFromFilecabinetsRelation().FileCabinet;
            Dialog searchDialog = filecabinets.Where(fileCabinet => fileCabinet.Name.Equals("Dokumentenpool")).First().GetDialogInfosFromDialogsRelation().Dialog.Where(dialog => dialog.IsDefault && dialog.Type == DialogTypes.Search).First().GetDialogFromSelfRelation();

            DialogExpression expression = new DialogExpression
            {
                Operation = DialogExpressionOperation.And,
                Condition = new List<DialogExpressionCondition>
                {
                    DialogExpressionCondition.Create("DWDOCID", "70000", "70999")
                },
                SortOrder = new List<SortedField>
                {
                    SortedField.Create("DWDOCID", SortDirection.Asc)
                },
                Count = 10000,
                //Count = 5,
                ExcludeDefaultSystemFields = false
            };
            var queryResult = searchDialog.GetDocumentsResult(expression);
            StringBuilder sb = new StringBuilder();
            
            Console.WriteLine(queryResult.ToString());
            foreach (var document in queryResult.Items)
            {
                sb.AppendLine(document.Id.ToString());
                var doc = document.GetDocumentFromSelfRelation();
                Console.WriteLine(doc.GetDocumentIndexFieldsFromFieldsRelation().Field.Where(field => field.FieldName.Equals("ALTEDOCID")).First().Item);
                doc.PutToFieldsRelationForDocumentIndexFields(new DocumentIndexFields
                {
                    Field = new List<DocumentIndexField>
                    {
                       DocumentIndexField.Create("ALTEDOCID", doc.Id.ToString())
                    }
                });
                Console.WriteLine(document.Id.ToString() + " Fertig");
                //sb.AppendLine(document.Id + " " + document.Version);
            }


            while (queryResult.NextRelationLink != null)
            {
                queryResult = queryResult.GetDocumentsQueryResultFromNextRelation();
                foreach (var document in queryResult.Items)
                {
                    Console.WriteLine(document.GetDocumentFromSelfRelation().ToString());
                    sb.AppendLine(document.Id.ToString());
                }
            }
            


            return new DiagnosticResult
            {
                Summary = sb.ToString(),
            };
        }
    }
}
