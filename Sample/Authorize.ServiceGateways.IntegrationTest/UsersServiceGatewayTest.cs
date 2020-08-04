using Authorize.ServiceGateways.ServiceContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Authorize.ServiceGateways.IntegrationTest
{
    [TestClass]
    public class UsersServiceGatewayTest
    {
        private static IUsersServiceGateway _sut;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            var serviceProvider = IoCConfig.GetServiceProvider(context);
            _sut = serviceProvider.GetService<IUsersServiceGateway>();
        }

        [TestMethod]
        public async Task GetPermissions_Return_Permissions()
        {
            //Act
            var actual = await _sut.GetPermissions("integrationtest", "authorize.application").ConfigureAwait(false);

            //Assert
            Assert.IsNotNull(actual);
        }
    }
}
