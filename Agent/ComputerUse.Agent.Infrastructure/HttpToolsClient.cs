using Azure.Core;
using ComputerUse.Agent.Core.Tools;
using ComputerUse.Agent.Core.Tools.DragAndDropItem;
using ComputerUse.Agent.Core.Tools.GetCursorPosition;
using ComputerUse.Agent.Core.Tools.MoveCursorAndClickWithPressedKey;
using ComputerUse.Agent.Core.Tools.MoveCursorAndScrollWithPressedKey;
using ComputerUse.Agent.Core.Tools.MoveCursorPosition;
using ComputerUse.Agent.Core.Tools.OpenBrowser;
using ComputerUse.Agent.Core.Tools.PressKey;
using ComputerUse.Agent.Core.Tools.Screenshot;
using ComputerUse.Agent.Core.Tools.TypeText;
using System.Net.Http.Json;

namespace ComputerUse.Agent.Infrastructure
{
    internal class HttpToolsClient : ITools
    {
        protected HttpClient? httpClient;

        public string Id { get; protected init; }

        public virtual async Task<ToolStatus> GetStatus() 
        {
            try
            {
                var client = GetHttpClient();

                using (var response = await client.GetAsync($"/Display/ping"))
                {
                    response.EnsureSuccessStatusCode();

                    return ToolStatus.Running;
                }
            }
            catch (Exception ex)
            {

                return ToolStatus.Stopped;
            }
        }

        public HttpToolsClient(HttpClient? httpClient = null)
        {
            this.httpClient = httpClient;
            this.Id = Guid.NewGuid().ToString();
        }

        protected HttpClient GetHttpClient()
        {
            if (httpClient == null) {
                throw new Exception("HttpClient was not set up.");
            }

            return httpClient;
        }


        public async Task<BaseImageToolResponse> DragAndDropItemAsync(DragAndDropItemRequest request)
        {
            var client = GetHttpClient();

            using (var response = await client.PostAsJsonAsync($"/Mouse/dragAndDropItem?identifier={request.Identifier}", request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadFromJsonAsync<BaseImageToolResponse>();

                return body ?? throw new Exception("Could not deserialize body in \"DragAndDropItemAsync\"");
            }
        }

        public async Task<GetCursorPositionResponse> GetCursorPositionAsync(BaseToolRequest request)
        {
            var client = GetHttpClient();

            using (var response = await client.GetAsync($"/Mouse/position?identifier={request.Identifier}"))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadFromJsonAsync<GetCursorPositionResponse>();

                return body ?? throw new Exception("Could not deserialize body for \"GetCursorPositionAsync\"");
            }
        }

        public async Task<BaseImageToolResponse> TakeScreenshotAsync(WaitAndScreenshotRequest request)
        {
            var client = GetHttpClient();

            using (var response = await client.PostAsJsonAsync($"/Display/screenshot?identifier={request.Identifier}", request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadFromJsonAsync<BaseImageToolResponse>();

                return body ?? throw new Exception("Could not deserialize body in \"GetScreenshotAsync\"");
            }
        }

        public async Task<BaseImageToolResponse> MoveCursorPositionAsync(MoveCursorPositionRequest request)
        {
            var client = GetHttpClient();

            using (var response = await client.PostAsJsonAsync($"/Mouse/position?identifier={request.Identifier}", request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadFromJsonAsync<BaseImageToolResponse>();

                return body ?? throw new Exception("Could not deserialize body in \"MoveCursorPositionAsync\"");
            }
        }

        public async Task<BaseImageToolResponse> MoveCursorAndClickWithPressedKeyAsync(MoveCursorAndClickWithPressedKeyRequest request)
        {
            var client = GetHttpClient();

            using (var response = await client.PostAsJsonAsync($"/Mouse/moveCursorAndClickWithPressedKey?identifier={request.Identifier}", request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadFromJsonAsync<BaseImageToolResponse>();

                return body ?? throw new Exception("Could not deserialize body in \"MoveCursorAndClickWithPressedKeyAsync\"");
            }
        }

        public async Task<BaseImageToolResponse> MoveCursorAndScrollWithPressedKeyAsync(MoveCursorAndScrollWithPressedKeyRequest request)
        {
            var client = GetHttpClient();

            using (var response = await client.PostAsJsonAsync($"/Mouse/moveCursorAndScrollWithPressedKey?identifier={request.Identifier}", request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadFromJsonAsync<BaseImageToolResponse>();

                return body ?? throw new Exception("Could not deserialize body in \"MoveCursorAndScrollWithPressedKeyAsync\"");
            }
        }

        public async Task<BaseImageToolResponse> PressKeyAsync(PressKeyRequest request)
        {
            var client = GetHttpClient();

            using (var response = await client.PostAsJsonAsync($"/Keyboard/pressKey?identifier={request.Identifier}", request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadFromJsonAsync<BaseImageToolResponse>();

                return body ?? throw new Exception("Could not deserialize body in \"PressKeyAsync\"");
            }
        }

        public async Task<BaseImageToolResponse> TypeTextAsync(TypeTextRequest request)
        {
            var client = GetHttpClient();

            using (var response = await client.PostAsJsonAsync($"/Keyboard/typeText?identifier={request.Identifier}", request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadFromJsonAsync<BaseImageToolResponse>();

                return body ?? throw new Exception("Could not deserialize body in \"TypeTextAsync\"");
            }
        }

        public async Task<BaseImageToolResponse> OpenBrowserAsync(OpenBrowserRequest request)
        {
            var client = GetHttpClient();

            using (var response = await client.PostAsJsonAsync($"/Applications/openBrowser?identifier={request.Identifier}", request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadFromJsonAsync<BaseImageToolResponse>();

                return body ?? throw new Exception("Could not deserialize body in \"TypeTextAsync\"");
            }
        }

        public virtual Task Start()
        {
            return Task.CompletedTask;
        }

        public virtual Task Stop()
        {
            return Task.CompletedTask;
        }
    }
}
