using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace ImageClassification;

public static class Program
{
    // You can obtain these values from the Keys and Endpoint page for your Custom Vision resource in the Azure Portal.
    private static string trainingEndpoint = "https://transunioncustomvision.cognitiveservices.azure.com/";
    private static string trainingKey = "64d085d7ce7e4b88a6829748e06f73ee";
    // You can obtain these values from the Keys and Endpoint page for your Custom Vision Prediction resource in the Azure Portal.
    private static string predictionEndpoint = "https://transunioncustomvision-prediction.cognitiveservices.azure.com/";
    private static string predictionKey = "81298fb81e844d5282a8f42095676c26";
    // You can obtain this value from the Properties page for your Custom Vision Prediction resource in the Azure Portal. See the "Resource ID" field. This typically has a value such as:
    // /subscriptions/<your subscription ID>/resourceGroups/<your resource group>/providers/Microsoft.CognitiveServices/accounts/<your Custom Vision prediction resource name>
    private static string predictionResourceId = "/subscriptions/1598e60e-cbcc-4093-a1fd-a6a6ea0d7dd8/resourceGroups/congnitive-svc-rg/providers/Microsoft.CognitiveServices/accounts/transunioncustomvision-Prediction";

    public static void Main()
    {
        CustomVisionTrainingClient trainingApi = AuthenticateTraining(trainingEndpoint, trainingKey);
        CustomVisionPredictionClient predictionApi = AuthenticatePrediction(predictionEndpoint, predictionKey);

    }

    private static CustomVisionTrainingClient AuthenticateTraining(string endpoint, string trainingKey)
    {
        // Create the Api, passing in the training key
        CustomVisionTrainingClient trainingApi = new CustomVisionTrainingClient(new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.ApiKeyServiceClientCredentials(trainingKey))
        {
            Endpoint = endpoint
        };
        return trainingApi;
    }

    private static CustomVisionPredictionClient AuthenticatePrediction(string endpoint, string predictionKey)
    {
        // Create a prediction endpoint, passing in the obtained prediction key
        CustomVisionPredictionClient predictionApi = new CustomVisionPredictionClient(new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.ApiKeyServiceClientCredentials(predictionKey))
        {
            Endpoint = endpoint
        };
        return predictionApi;
    }

}