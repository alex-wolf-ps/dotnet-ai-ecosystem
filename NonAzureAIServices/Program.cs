using OllamaSharp;
using OpenAI;
using OpenAI.Chat;

#region OpenAI
OpenAIClient azureClient = new("your-openai-key");

// This must match the custom deployment name you chose for your model
ChatClient chatClient = azureClient.GetChatClient("gpt-4o");

Console.WriteLine("Your OpenAI question:");
var prompt = Console.ReadLine();
var response = chatClient.CompleteChatStreamingAsync(prompt);

Console.WriteLine($"Assistant response:");
await foreach (var chatUpdate in response)
{
    foreach (var contentPart in chatUpdate.ContentUpdate)
    {
        Console.Write(contentPart.Text);
    }
}
Console.WriteLine();
#endregion

#region Ollama
// set up the client
var uri = new Uri("http://localhost:11434");
var ollama = new OllamaApiClient(uri);

// select a model which should be used for further operations
ollama.SelectedModel = "phi3:mini";

var chat = new Chat(ollama);
while (true)
{
    Console.WriteLine("Your Ollama question:");
    var message = Console.ReadLine();
    Console.WriteLine("Ollama response:");
    await foreach (var answerToken in chat.SendAsync(message))
        Console.Write(answerToken);
}

#endregion