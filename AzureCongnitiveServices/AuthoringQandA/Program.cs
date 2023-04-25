using Azure;
using Azure.AI.Language.QuestionAnswering;
using Azure.AI.Language.QuestionAnswering.Authoring;
using Azure.Core;
using System;

namespace question_answering
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri endpoint = new Uri("https://qna-transunion.cognitiveservices.azure.com/");
            AzureKeyCredential credential = new AzureKeyCredential("a125c94692314e4393caf95bc2f70d5d");
            string projectName = "SurfaceBookProject";
            string deploymentName = "production";

            QuestionAnsweringAuthoringClient client = new QuestionAnsweringAuthoringClient(endpoint, credential);

            RequestContent creationRequestContent = RequestContent.Create(
               new
               {
                    description = "This is the description for a test project",
                    language = "en",
                    multilingualResource = false,
                    settings = new
                    {
                        defaultAnswer = "No answer found for your question."
                    }
                }
               );

            Response creationResponse = client.CreateProject(projectName, creationRequestContent);

            // Projects can be retrieved as follows
            Pageable<BinaryData> projects = client.GetProjects();

            Console.WriteLine("Projects: ");
            foreach (BinaryData project in projects)
            {
                Console.WriteLine(project);
            }

            string sourceUri = "https://download.microsoft.com/download/7/B/1/7B10C82E-F520-4080-8516-5CF0D803EEE0/surface-book-user-guide-EN.pdf";
            RequestContent updateSourcesRequestContent = RequestContent.Create(
                new[] {
                    new {
                            op = "add",
                            value = new
                            {
                                displayName = "MicrosoftFAQ",
                                source = sourceUri,
                                sourceUri = sourceUri,
                                sourceKind = "url",
                                contentStructureKind = "unstructured",
                                refresh = false
                            }
                        }
                });

            Operation<Pageable<BinaryData>> updateSourcesOperation = client.UpdateSources(WaitUntil.Completed, projectName, updateSourcesRequestContent);

            // Knowledge Sources can be retrieved as follows
            Pageable<BinaryData> sources = updateSourcesOperation.Value;

            Console.WriteLine("Sources: ");
            foreach (BinaryData source in sources)
            {
                Console.WriteLine(source);
            }

            Operation<BinaryData> deploymentOperation = client.DeployProject(WaitUntil.Completed, projectName, deploymentName);

            // Deployments can be retrieved as follows
            Pageable<BinaryData> deployments = client.GetDeployments(projectName);
            Console.WriteLine("Deployments: ");
            foreach (BinaryData deployment in deployments)
            {
                Console.WriteLine(deployment);
            }
        }
    }
}