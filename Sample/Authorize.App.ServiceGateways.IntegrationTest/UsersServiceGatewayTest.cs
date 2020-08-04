using Authorize.App.ServiceGateways.ServiceContracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Authorize.App.ServiceGateways.IntegrationTest
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
            var actual = await _sut.GetPermissions("integrationTest", "authorize.application").ConfigureAwait(false);

            //Assert
            Assert.IsNotNull(actual);
        }
    }
}