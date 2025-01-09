using Azure;
using Azure.AI.OpenAI;
using Azure.AI.Translation.Text;
using OpenAI.Chat;

// Create the Azure Client
AzureOpenAIClient azureClient = new(
    new Uri("your-azure-openai-uri"),
    new AzureKeyCredential("your-azure-openai-key"));

// This must match the custom deployment name you chose for your model
ChatClient chatClient = azureClient.GetChatClient("gpt-4o");

// Get the user's question
Console.WriteLine("Your question:");
var prompt = Console.ReadLine();

// Get the response from the AI model
var response = chatClient.CompleteChatStreamingAsync(prompt);

// Print out the response
Console.WriteLine($"Assistant response:");
await foreach (var chatUpdate in response)
{
    foreach (var contentPart in chatUpdate.ContentUpdate)
    {
        Console.Write(contentPart.Text);
    }
}

// Azure AI translate - a specialized Azure service instead of general generative AI
TextTranslationClient client = new(new AzureKeyCredential("your-azure-translate-key"), "eastus");

var textToTranslate = "This text will be translated";
Console.WriteLine(textToTranslate);
var translation = (await client.TranslateAsync("de", textToTranslate)).Value.FirstOrDefault();

Console.WriteLine("Translation:");
Console.WriteLine(translation?.Translations?.FirstOrDefault().Text);