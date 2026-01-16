using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.AI;
using Azure;
using Azure.AI.OpenAI;

namespace AIDataForm
{
    internal class AzureOpenAIBaseService
    {
        #region Fields

        /// <summary>
        /// The Azure OpenAI EndPoint
        /// </summary>
        private const string endpoint = "AZURE_OPENAI_ENDPOINT";

        /// <summary>
        /// The Deployment name
        /// </summary>
        private const string deploymentName = "DEPLOYMENT_NAME";

        /// <summary>
        /// The API key
        /// </summary>
        private const string key = "API_KEY";

        /// <summary>
        /// The OpenAI
        /// </summary>
        private IChatClient? client;
        private bool isAlreadyValidated;

        internal bool IsCredentialValid { get; private set; }

        internal AzureOpenAIBaseService()
        {
        }

        internal async Task InitializeAsync()
        {
            if (isAlreadyValidated) return;

            try
            {
                var azureClient = new AzureOpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));
                client = azureClient.AsChatClient(modelId: deploymentName);

                var completion = await client.CompleteAsync("Hello");
                string text = ExtractText(completion);

                IsCredentialValid = !string.IsNullOrWhiteSpace(text);
                isAlreadyValidated = IsCredentialValid;
            }
            catch
            {
                IsCredentialValid = false;
                isAlreadyValidated = false;
            }
        }

        internal async Task<string> GetAIResponse(string userPrompt)
        {
            if (!IsCredentialValid || client == null) return string.Empty;

            try
            {
                var completion = await client.CompleteAsync(userPrompt);
                return Normalize(ExtractText(completion));
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string ExtractText(ChatCompletion? completion)
        {
            if (completion == null) return string.Empty;

            var msg = completion.Message;
            if (msg != null)
            {
                if (!string.IsNullOrWhiteSpace(msg.Text))
                    return msg.Text;

                if (msg.Contents != null)
                {
                    var textParts = msg.Contents
                        .OfType<TextContent>()
                        .Select(c => c.Text)
                        .Where(t => !string.IsNullOrWhiteSpace(t));
                    var combined = string.Join("", textParts);
                    if (!string.IsNullOrWhiteSpace(combined))
                        return combined;
                }
            }

            return completion.ToString() ?? string.Empty;
        }

        private static string Normalize(string? text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;
            return text.Trim().Trim('"').Trim();
        }
    }
}