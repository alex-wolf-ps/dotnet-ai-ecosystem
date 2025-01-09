using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.AddOpenAIChatCompletion(
    modelId: "gpt-4o",
    apiKey: "your-openai-key"
);
Kernel kernel = kernelBuilder.Build();

var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

ChatHistory history = [];
Console.WriteLine("Your question:");
history.AddUserMessage(Console.ReadLine());

var response = chatCompletionService.GetStreamingChatMessageContentsAsync(
    chatHistory: history,
    kernel: kernel
);

Console.WriteLine($"Assistant response:");
await foreach (var chunk in response)
{
    Console.Write(chunk);
}