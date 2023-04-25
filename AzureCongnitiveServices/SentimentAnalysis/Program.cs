using Azure;
using System;
using Azure.AI.TextAnalytics;
using System.Collections.Generic;

namespace Example
{
    class Program
    {
        private static readonly AzureKeyCredential credentials = new AzureKeyCredential("0c7e11947caa4ca8885b719b5c821848");
        private static readonly Uri endpoint = new Uri("https://languageservice-transunion.cognitiveservices.azure.com/");


        // Example method for detecting opinions text. 
        static void SentimentAnalysisWithOpinionMiningExample(TextAnalyticsClient client)
        {
            var documents = new List<string>
            {
                "The food and service were unacceptable. The concierge was nice, however."
            };

            AnalyzeSentimentResultCollection reviews = client.AnalyzeSentimentBatch(documents, options: new AnalyzeSentimentOptions()
            {
                IncludeOpinionMining = true
            });

           PrintReviews(reviews);

            var phrases = new (string id, string language, List<string> text)[]
            {
                 ("1", "en", new List<string> {"I really enjoy the new XBox One S. It has a clean look, it has 4K/HDR resolution and it is affordable." }),
                ("2", "es",  new List<string> {  "Este ha sido un dia terrible, llegué tarde al trabajo debido a un accidente automobilistico." })
            };

            foreach(var phrase in phrases)
            {
                AnalyzeSentimentResultCollection rvs = client.AnalyzeSentimentBatch(phrase.text, phrase.language, options: new AnalyzeSentimentOptions()
                {
                    IncludeOpinionMining = true
                });

                PrintReviews(rvs);
            }

       }

        static void PrintReviews(AnalyzeSentimentResultCollection reviews)
        {
            foreach (AnalyzeSentimentResult review in reviews)
            {
                Console.WriteLine($"Document sentiment: {review.DocumentSentiment.Sentiment}\n");
                Console.WriteLine($"\tPositive score: {review.DocumentSentiment.ConfidenceScores.Positive:0.00}");
                Console.WriteLine($"\tNegative score: {review.DocumentSentiment.ConfidenceScores.Negative:0.00}");
                Console.WriteLine($"\tNeutral score: {review.DocumentSentiment.ConfidenceScores.Neutral:0.00}\n");
                foreach (SentenceSentiment sentence in review.DocumentSentiment.Sentences)
                {
                    Console.WriteLine($"\tText: \"{sentence.Text}\"");
                    Console.WriteLine($"\tSentence sentiment: {sentence.Sentiment}");
                    Console.WriteLine($"\tSentence positive score: {sentence.ConfidenceScores.Positive:0.00}");
                    Console.WriteLine($"\tSentence negative score: {sentence.ConfidenceScores.Negative:0.00}");
                    Console.WriteLine($"\tSentence neutral score: {sentence.ConfidenceScores.Neutral:0.00}\n");

                    foreach (SentenceOpinion sentenceOpinion in sentence.Opinions)
                    {
                        Console.WriteLine($"\tTarget: {sentenceOpinion.Target.Text}, Value: {sentenceOpinion.Target.Sentiment}");
                        Console.WriteLine($"\tTarget positive score: {sentenceOpinion.Target.ConfidenceScores.Positive:0.00}");
                        Console.WriteLine($"\tTarget negative score: {sentenceOpinion.Target.ConfidenceScores.Negative:0.00}");
                        foreach (AssessmentSentiment assessment in sentenceOpinion.Assessments)
                        {
                            Console.WriteLine($"\t\tRelated Assessment: {assessment.Text}, Value: {assessment.Sentiment}");
                            Console.WriteLine($"\t\tRelated Assessment positive score: {assessment.ConfidenceScores.Positive:0.00}");
                            Console.WriteLine($"\t\tRelated Assessment negative score: {assessment.ConfidenceScores.Negative:0.00}");
                        }
                    }
                }
                Console.WriteLine($"\n");
            }
        }

        static void Main(string[] args)
        {
            var client = new TextAnalyticsClient(endpoint, credentials);
            SentimentAnalysisWithOpinionMiningExample(client);

            Console.Write("Press any key to exit.");
            Console.ReadKey();
        }

    }
}