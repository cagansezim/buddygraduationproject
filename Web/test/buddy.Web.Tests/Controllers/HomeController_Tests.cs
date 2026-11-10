using System.Threading.Tasks;
using buddy.Models.TokenAuth;
using buddy.Web.Controllers;
using Shouldly;
using Xunit;

namespace buddy.Web.Tests.Controllers
{
    public class HomeController_Tests: buddyWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}