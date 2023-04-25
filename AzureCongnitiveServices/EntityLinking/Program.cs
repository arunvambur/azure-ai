using Azure;
using System;
using System.Globalization;
using Azure.AI.TextAnalytics;
using System.Threading;

namespace EntityLinkingExample
{
    class Program
    {
        private static readonly AzureKeyCredential credentials = new AzureKeyCredential("0c7e11947caa4ca8885b719b5c821848");
        private static readonly Uri endpoint = new Uri("https://languageservice-transunion.cognitiveservices.azure.com/");

        // Example method for recognizing entities and providing a link to an online data source.
        static void EntityLinkingExample(TextAnalyticsClient client)
        {
            var response = client.RecognizeLinkedEntities(
                "TransUnion is an American consumer credit reporting agency. TransUnion collects and aggregates information on over one billion individual consumers in over thirty countries including \"200 million files profiling nearly every credit - active consumer in the United States\". Its customers include over 65,000 businesses. Based in Chicago, Illinois, TransUnion's 2014 revenue was US$1.3 billion. It is the smallest of the three largest credit agencies, along with Experian and Equifax (known as the \"Big Three\")");
            Console.WriteLine("Linked Entities:");
            foreach (var entity in response.Value)
            {
                Console.WriteLine($"\tName: {entity.Name},\tID: {entity.DataSourceEntityId},\tURL: {entity.Url}\tData Source: {entity.DataSource}");
                Console.WriteLine("\tMatches:");
                foreach (var match in entity.Matches)
                {
                    Console.WriteLine($"\t\tText: {match.Text}");
                    Console.WriteLine($"\t\tScore: {match.ConfidenceScore:F2}\n");
                }
            }
        }

        static void Main(string[] args)
        {
            var client = new TextAnalyticsClient(endpoint, credentials);
            EntityLinkingExample(client);

            Console.Write("Press any key to exit.");
            Console.ReadKey();
        }

    }
}