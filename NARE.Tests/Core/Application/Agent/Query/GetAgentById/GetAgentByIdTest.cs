using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Moq;
using NARE.Application.Agent.Query.GetAgentById;
using NARE.Application.Agent.Query.GetAgentDtoById;
using NARE.Application.Utilities;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Query.GetAgentById
{
    public class GetAgentByIdTest
    {
        public Mock<IMediator> Mediator { get; }
        public IMapper Mapper { get; }
        public GetAgentDtoByIdQueryHandler Handler { get; }   

        public GetAgentByIdTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            Mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
                cfg.ValidateInlineMaps = false;
            }));
            Handler = new GetAgentDtoByIdQueryHandler(Mediator.Object, Mapper);
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public async Task GetAgentById_ReturnsNull(string agentId)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetAgentByIdQuery>(), default(CancellationToken))).ReturnsAsync((NARE.Domain.Entities.Agent) null);
            // Act
            var result = await Handler.Handle(new GetAgentDtoByIdQuery(agentId), CancellationToken.None);
            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public async Task GetAgentById_ReturnsValidAgent(string agentId)
        {
            var agent = new NARE.Domain.Entities.Agent()
            {
                Id = agentId,
                Email = "test@test.ca"
            };
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetAgentByIdQuery>(), default(CancellationToken))).ReturnsAsync(agent);
            // Act
            var result = await Handler.Handle(new GetAgentDtoByIdQuery(agentId), CancellationToken.None);
            // Assert
            Assert.Equal(agentId, result.Id);
            Assert.Equal(agent.Email, result.Email);
        }
    }
}
