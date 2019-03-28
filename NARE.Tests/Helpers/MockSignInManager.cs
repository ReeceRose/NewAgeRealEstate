using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NARE.Domain.Entities;

namespace NARE.Tests.Helpers
{
    public class MockSignInManager : SignInManager<Agent>
    {
        public MockSignInManager()
            : base(new Mock<MockUserManager>().Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<Agent>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<Agent>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object)
        { }
    }
}