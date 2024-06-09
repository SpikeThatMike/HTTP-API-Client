using HTTP.APIClient;

namespace Tests
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        public void CreateClient()
        {
            using (var client = new Client())
            {
                Assert.IsNotNull(client);
            }
        }

        [TestMethod]
        public void CreateClient_WithUrl()
        {
            using (var client = new Client("https://spikethatmike.dev"))
            {
                Assert.AreEqual("https://spikethatmike.dev/", client.BaseUrl.ToString());
            }
        }

        [TestMethod]
        public void CreateClient_WithUrlAndOptions()
        {
            using (var client = new Client("https://spikethatmike.dev", new ClientOptions() { Timeout = 60000 }))
            {
                Assert.AreEqual("https://spikethatmike.dev/", client.BaseUrl.ToString());
                Assert.AreEqual(60000, client.Options.Timeout);
            }
        }
    }
}