using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Translation;

class Program
{
    // This example requires environment variables named "SPEECH_KEY" and "SPEECH_REGION"
    static string speechKey = "60FpTf43tHUfmL0JYwTHiH4RYPqwyNeXH9aQKXeZeIxWAnrmr4pHJQQJ99BCACYeBjFXJ3w3AAAEACOGsFiY";
    static string speechRegion = "eastus";

    static void OutputSpeechRecognitionResult(TranslationRecognitionResult translationRecognitionResult)
    {
        switch (translationRecognitionResult.Reason)
        {
            case ResultReason.TranslatedSpeech:
                Console.WriteLine($"English: {translationRecognitionResult.Text}");
                foreach (var element in translationRecognitionResult.Translations)
                {
                    Console.WriteLine($"Deutch: '{element.Key}': {element.Value}");
                }
                break;
            case ResultReason.NoMatch:
                Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                break;
            case ResultReason.Canceled:
                var cancellation = CancellationDetails.FromResult(translationRecognitionResult);
                Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                if (cancellation.Reason == CancellationReason.Error)
                {
                    Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                    Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                    Console.WriteLine($"CANCELED: Did you set the speech resource key and region values?");
                }
                break;
        }
    }

    async static Task Main(string[] args)
    {
        var speechTranslationConfig = SpeechTranslationConfig.FromSubscription(speechKey, speechRegion);
        speechTranslationConfig.SpeechRecognitionLanguage = "en-IN";
        speechTranslationConfig.AddTargetLanguage("de");

        using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
        using var translationRecognizer = new TranslationRecognizer(speechTranslationConfig, audioConfig);

        Console.WriteLine("Speak into your microphone.");
        while (true)
        {
            var translationRecognitionResult = await translationRecognizer.RecognizeOnceAsync();
            OutputSpeechRecognitionResult(translationRecognitionResult);
        }
    }
}