using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.Planning;
using Microsoft.SemanticKernel;
using System.Text.Json;
using SKConsole.Skills;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
.AddJsonFile("appsettings.json")
.Build();

IConfigurationSection section = configuration.GetSection("OpenAI");

IKernel kernel = new KernelBuilder().Build();
kernel.Config.AddAzureChatCompletionService(
    section["ModelName"],
    section["Endpoint"],
    section["APIKey"]
);

string skillsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Skills");
kernel.ImportSemanticSkillFromDirectory(skillsDirectory, "SummarizeSkill");

kernel.ImportSkill(new CognitiveSearchSkill(), "CognitiveSearchSkill");

var planner = new SequentialPlanner(kernel);

string input = "Face AI";
var plan = await planner.CreatePlanAsync(input);

Console.WriteLine(JsonSerializer.Serialize(plan, options: new()
{
    WriteIndented = true,
    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All),
}));

SKContext result = await kernel.RunAsync(plan);
Console.WriteLine(result);
