using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Moq;
using NARE.Application.Agent.Model;
using NARE.Application.Agent.Query.GetAllAgents;
using NARE.Application.Agent.Query.GetAllAgentsPaginated;
using NARE.Application.Agent.Query.GetPaginatedAgentsResult;
using NARE.Application.Utilities;
using NARE.Domain.Entities;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Query.GetAllAgentsPaginated
{
    public class GetAllAgentsPaginatedTest
    {
        public Mock<IMediator> Mediator { get; }
        public IMapper Mapper { get; }
        public GetAllAgentsPaginatedQueryHandler Handler { get; }

        public List<NARE.Domain.Entities.Agent> Agents { get; }

        public GetAllAgentsPaginatedTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            Mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
                cfg.ValidateInlineMaps = false;
            }));
            Handler = new GetAllAgentsPaginatedQueryHandler(Mediator.Object, Mapper);
            Agents = new List<NARE.Domain.Entities.Agent>()
            {
                new NARE.Domain.Entities.Agent() {Email = "test@test.ca"},
                new NARE.Domain.Entities.Agent() {Email = "user@domain.com"},
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetAllAgentsQuery>(), default(CancellationToken))).ReturnsAsync(Agents);
        }

        [Theory]
        // Based off of the agent list above
        [InlineData(3, 1, 1)]
        [InlineData(1, 1, 2)]
        [InlineData(10, 1, 1)]
        public async Task GetAllAgentsPaginated_ReturnsExpected(int pageSize, int currentPage, int totalPages)
        {
            // Arrange
            var paginatedAgents = new PaginatedAgentsDto()
            {
                PaginationModel = new PaginationModel()
                {
                    CurrentPage = currentPage,
                    PageSize = pageSize
                },
                Agents = Mapper.Map<List<AgentDto>>(Agents)
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetPaginatedAgentResultQuery>(), default(CancellationToken))).ReturnsAsync(paginatedAgents);
            // Act
            var result = await Handler.Handle(new GetAllAgentsPaginatedQuery(paginatedAgents.PaginationModel), CancellationToken.None);
            // Assert
            Assert.Equal(pageSize, result.PaginationModel.PageSize);
            Assert.Equal(currentPage, result.PaginationModel.CurrentPage);
            Assert.Equal(totalPages, result.PaginationModel.TotalPages);
        }
    }
}
