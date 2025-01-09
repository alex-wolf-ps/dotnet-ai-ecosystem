using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using OpenAI;
using System.ClientModel;

// Create the IChatClient - just needs to be any implementation of IChatClient
IChatClient client =
    new OpenAIClient("your-openai-key")
    .AsChatClient("gpt-4o");

// IChatClient client =
//    new AzureOpenAIClient(new Uri("your-azure-openai-endpoint"), new ApiKeyCredential("your-azure-openai-key"))
//        .AsChatClient("your-deployment-name");

// Get the prompt from the user
Console.WriteLine("Your question:");
string prompt = Console.ReadLine();

// Submit the prompt to the AI model
var response = client.CompleteStreamingAsync(prompt);

// Print out the response
Console.WriteLine($"Assistant response:");
await foreach (var chunk in response)
{
    Console.Write(chunk);
}