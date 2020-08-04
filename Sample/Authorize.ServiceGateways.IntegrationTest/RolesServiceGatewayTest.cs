using Authorize.ServiceGateways.ServiceContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace Authorize.ServiceGateways.IntegrationTest
{
    [TestClass]
    public class RolesServiceGatewayTest
    {
        private static IRolesServiceGateway _sut;
        private static IHttpContextAccessor _context;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            var serviceProvider = IoCConfig.GetServiceProvider(context);
            _sut = serviceProvider.GetService<IRolesServiceGateway>();
            _context = serviceProvider.GetService<IHttpContextAccessor>();
        }
        [TestMethod]
        [Ignore]
        public async Task Get_token()
        {
            var token = await _context.HttpContext.GetTokenAsync("access_token");
        }

        [TestMethod]
        public async Task Get_Return_Role()
        {
            //Act
            var actual = await _sut.Get("integrationtest").ConfigureAwait(false); 
            
            //Assert
            Assert.IsNotNull(actual);
        }
    }
}
