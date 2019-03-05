using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NARE.Domain.Entities;

namespace NARE.Tests.Helpers
{
    public class MockUserManager : UserManager<Agent>
    {
        public MockUserManager()
            : base(new Mock<IUserStore<Agent>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<Agent>>().Object,
                new IUserValidator<Agent>[0],
                new IPasswordValidator<Agent>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<Agent>>>().Object)
        { }
    }
}
