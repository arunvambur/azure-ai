using Azure;
using System;
using Azure.AI.TextAnalytics;

namespace KeyPhraseExtractionExample
{
    class Program
    {
        private static readonly AzureKeyCredential credentials = new AzureKeyCredential("0c7e11947caa4ca8885b719b5c821848");
        private static readonly Uri endpoint = new Uri("https://languageservice-transunion.cognitiveservices.azure.com/");

        // Example method for extracting key phrases from text
        static void KeyPhraseExtractionExample(TextAnalyticsClient client)
        {



            var response = client.ExtractKeyPhrases(@"The food was delicious and the staff were wonderful");

            // Printing key phrases
            Console.WriteLine("Key phrases:");

            foreach (string keyphrase in response.Value)
            {
                Console.WriteLine($"\t{keyphrase}");
            }

            var phrases = new (string id, string language, string text)[]
            {
                ("1", "ja", "猫は幸せ"),
                ("2", "de", "Fahrt nach Stuttgart und dann zum Hotel zu Fu."),
                ("3","en", "My cat might need to see a veterinarian."),
                ("4", "es", "A mi me encanta el fútbol!")
            };

            foreach(var ph in phrases)
            {
                var res = client.ExtractKeyPhrases(ph.text, ph.language);

                // Printing key phrases
                Console.WriteLine($"Key phrases: {ph.id}, {ph.text}");

                foreach (string keyphrase in res.Value)
                {
                    Console.WriteLine($"\t{keyphrase}");
                }

            }
        }

        static void Main(string[] args)
        {
            var client = new TextAnalyticsClient(endpoint, credentials);
            KeyPhraseExtractionExample(client);

            Console.Write("Press any key to exit.");
            Console.ReadKey();
        }

    }
}