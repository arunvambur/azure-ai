using Azure;
using Azure.AI.Language.QuestionAnswering;
using System;

namespace question_answering
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri endpoint = new Uri("https://ai-language-service-arvenka.cognitiveservices.azure.com/");
            AzureKeyCredential credential = new AzureKeyCredential("9D8M5I7tyg80TBZTHb6cTEcwrDarM3jVQZtwki6dwnl3TVmBH1TNJQQJ99BDACYeBjFXJ3w3AAAaACOGtzyN");
            string projectName = "SurfaceBookProject";
            string deploymentName = "production";

            string question = "How long should my Surface battery last?";

            QuestionAnsweringClient client = new QuestionAnsweringClient(endpoint, credential);
            QuestionAnsweringProject project = new QuestionAnsweringProject(projectName, deploymentName);

            while (true)
            {
                Console.Write("Q:>");
                question = Console.ReadLine();

                Response<AnswersResult> response = client.GetAnswers(question, project);

                foreach (KnowledgeBaseAnswer answer in response.Value.Answers)
                {
                    //Console.WriteLine($"Q:{question}");
                    Console.WriteLine($"A:>{answer.Answer}");
                }
            }
        }
    }
}