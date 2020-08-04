using Authorize.ServiceGateways.ServiceContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Authorize.ServiceGateways.Models.Applications;
using Authorize.ServiceGateways.Models.Common;
using System.Collections.Generic;

namespace Authorize.ServiceGateways.IntegrationTest
{
    [TestClass]
    public class ApplicationsServiceGatewayTest
    {
        private static IApplicationsServiceGateway _sut;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            var serviceProvider = IoCConfig.GetServiceProvider(context);
            _sut = serviceProvider.GetService<IApplicationsServiceGateway>();
        }

        [TestMethod]
        public async Task Get_Return_ApplicationWithPermissions()
        {
            //Act
            var actual = await _sut.Get("authorize.application").ConfigureAwait(false);

            //Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public async Task GetPermissions_Return_ApplicationPermissions()
        {
            //Act
            var actual = await _sut.GetPermissions("authorize.application").ConfigureAwait(false);

            //Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public async Task SearchApplication_Return_Application()
        {
            //Act
            var actual = await _sut.SearchApplication("authorize.application", 1, 1).ConfigureAwait(false);

            //Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public async Task CreateAndRemoveApplication_Success()
        {
            //Arrange
            var integrationTestApplicationName = "integration.test.application";

            var createApplicationRequest = new CreateApplicationRequest
            {
                Name = integrationTestApplicationName,
                Description = "integration.test.application description",
                Permissions = new List<PermissionApplication>
                {
                    new PermissionApplication
                    {
                        Name = "integration.test.permission.one",
                        Description = "integration.test.permission.one description",
                    }
                }
            };

            var removeApplicationRequest = new RemoveApplicationRequest()
            {
                Name = integrationTestApplicationName,
            };

            //Act
            await _sut.Registry(createApplicationRequest).ConfigureAwait(false);

            await _sut.RemoveApplication(removeApplicationRequest).ConfigureAwait(false);
        }
    }
}
