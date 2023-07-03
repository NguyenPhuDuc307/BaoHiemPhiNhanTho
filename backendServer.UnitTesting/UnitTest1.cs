//using Xunit;
//using BackendServer;
//using Microsoft.AspNetCore.Mvc.Testing;
//using System.Threading.Tasks;

//namespace backendServer.UnitTesting
//{
//    public class UnitTest1
//    {
//        private readonly WebApplicationFactory<Startup> _factory;

//        public UnitTest1()
//        {
//            _factory = new WebApplicationFactory<Startup>();
//        }

//        [Fact]
//        public async Task Index()
//        {
//            var client = _factory.CreateClient();

//            var response = await client.GetAsync("/api/AnnexContractController/getList");

//            int code = (int)response.StatusCode;

//            Assert.Equal(200, code);
//        }
//    }
//}
