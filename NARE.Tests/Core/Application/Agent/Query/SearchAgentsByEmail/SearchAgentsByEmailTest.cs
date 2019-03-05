using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Moq;
using NARE.Application.Agent.Model;
using NARE.Application.Agent.Query.GetAllAgents;
using NARE.Application.Agent.Query.GetPaginatedResults;
using NARE.Application.Agent.Query.SearchAgentsByEmail;
using NARE.Application.Utilities;
using NARE.Domain.Entities;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Query.SearchAgentsByEmail
{
    public class SearchAgentsByEmailTest
    {
        public Mock<IMediator> Mediator { get; }
        public IMapper Mapper { get; }
        public SearchAgentsByEmailQueryHandler Handler { get; } 

        public SearchAgentsByEmailTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            Mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
                cfg.ValidateInlineMaps = false;
            }));
            Handler = new SearchAgentsByEmailQueryHandler(Mediator.Object, Mapper);
        }

        [Theory]
        [InlineData("test", 1, 1)]
        [InlineData("user", 15, 2)]
        [InlineData(".com", 101, 11)]
        public async Task SearchAgentsByEmail_ReturnsExpected(string email, int agentCount, int pageCount)
        {
            // Arrange
            var agents = new List<NARE.Domain.Entities.Agent>();
            for (var i = 0; i < agentCount; i++)
            {
                // Create a unique email
                agents.Add(new NARE.Domain.Entities.Agent() { Email =  $"{i}{email}"});
            }

            var mappedAgents = Mapper.Map<List<AgentDto>>(agents);
            var paginationModel = new PaginationModel() { Count = agentCount };
            Mediator.Setup(m => m.Send(It.IsAny<GetAllAgentsQuery>(), default(CancellationToken))).ReturnsAsync(agents);
            Mediator.Setup(m => m.Send(It.IsAny<GetPaginatedResultsQuery>(), default(CancellationToken)))
                                .ReturnsAsync(new PaginatedAgentsDto()
                                { PaginationModel = paginationModel, Agents = mappedAgents });

            // Act
            var result = await Handler.Handle(new SearchAgentsByEmailQuery(email), CancellationToken.None);
            // Assert
            Assert.Equal(pageCount, result.PaginationModel.TotalPages);
            Assert.Equal(agentCount, result.PaginationModel.Count);
            Assert.Equal(agents.Count, result.Agents.Count);
        }
    }
}
