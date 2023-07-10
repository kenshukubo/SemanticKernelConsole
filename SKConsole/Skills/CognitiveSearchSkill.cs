using Azure;
using Azure.Search.Documents;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.SkillDefinition;
using SKConsole.Model;
using System.Text.Json;

namespace SKConsole.Skills
{
    public class CognitiveSearchSkill
    {
        [SKFunction("ユーザーの入力値からドキュメントの文章を取得する")]
        public async Task<string> GetDocumetContent(string input)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            IConfigurationSection section = configuration.GetSection("CognitiveSearch");

            string searchServiceEndPoint = $"https://{section["SearchName"]}.search.windows.net";
            string queryApiKey = section["APIKey"];

            SearchClient searchClient = new SearchClient(new Uri(searchServiceEndPoint), section["SearchIndex"], new AzureKeyCredential(queryApiKey));
            SearchOptions options = new SearchOptions()
            {
                Size = 5
            };

            var response = await searchClient.SearchAsync<SearchModel>(input, options);
            var resString = response.GetRawResponse().Content.ToString();

            var value = JsonSerializer.Deserialize<SearchModel>(resString).Value;

            List<SearchValueModel> searchResults = new List<SearchValueModel>();
            foreach (var item in value.RootElement.EnumerateArray())
            {
                var searchResult = JsonSerializer.Deserialize<SearchValueModel>(item.ToString());
                searchResults.Add(searchResult);
            }
            return searchResults[0].Content;
        }
    }
}
