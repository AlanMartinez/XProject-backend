namespace XProject.IntegrationTests
{
    using Microsoft.AspNetCore.Mvc.Testing;
    using System.Net.Http;

    public static class TestClient
    {
        public static HttpClient GetClient()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            return webAppFactory.CreateDefaultClient();
        }
    }
}