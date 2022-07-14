using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using XProject.API.Wrappers;
using Xunit;

namespace XProject.IntegrationTests.Controllers
{
    public class AuthControllerTests
    {
        [Fact]
        public async Task Auth_ValidUser_OK()
        {
            var client = TestClient.GetClient();

            var request = new { 
                UserLogin =
                new {
                    UserName = "string@string.com",
                    Password = "string"
                } 
            };

            var myContent = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var response = await client.PostAsync("/api/Auth", byteContent);
            var stringResult = await response.Content.ReadAsStringAsync();

            var res = JsonConvert.DeserializeObject<Response<string>>(stringResult);

            Assert.NotNull(res);
            Assert.True(res.Succeeded);
            Assert.NotEmpty(res.Data);
        }

        [Fact]
        public async Task Auth_InvalidUser_ThrowException()
        {
            var client = TestClient.GetClient();

            var request = new
            {
                UserLogin =
                new
                {
                    UserName = "falso@string.com",
                    Password = "string"
                }
            };

            var myContent = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var response = await client.PostAsync("/api/Auth", byteContent);
            var stringResult = await response.Content.ReadAsStringAsync();

            var res = JsonConvert.DeserializeObject<Response<string>>(stringResult);

            Assert.NotNull(res);
            Assert.False(res.Succeeded);
        }
    }
}