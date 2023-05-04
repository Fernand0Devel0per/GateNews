using OpenAI;

namespace GateNewsApi.Helpers.OpenIA
{
    public class ContentModerationService
    {
        private readonly OpenAIClient _apiClient;

        public ContentModerationService(IConfiguration configuration)
        {
            _apiClient = new OpenAIClient(configuration["OpenAI:ApiKey"]);
        }

        public async Task<bool> CheckForInappropriateContent(string text)
        {
            var result = await _apiClient.ModerationsEndpoint.GetModerationAsync(text);
            return result;
        }
    }
}
