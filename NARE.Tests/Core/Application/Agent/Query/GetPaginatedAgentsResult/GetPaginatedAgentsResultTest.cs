using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NARE.Application.Agent.Model;
using NARE.Application.Agent.Query.GetPaginatedAgentsResult;
using NARE.Domain.Entities;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Query.GetPaginatedAgentsResult
{
    public class GetPaginatedAgentsResultTest
    {
        public List<AgentDto> Agents { get; }
        public GetPaginatedAgentsResultQueryHandler Handler { get; }

        public GetPaginatedAgentsResultTest()
        {
            // Arrange
            Agents = new List<AgentDto>()
            {
                new AgentDto() { Email = "test@test.ca", Id = "123", UserName = "test@test.ca" },
                new AgentDto() { Email = "user@domain.com", Id = "1234", UserName = "user@domain.com" }
            };
            Handler = new GetPaginatedAgentsResultQueryHandler();
        }

        [Theory]
        // Based off of the agent list above
        [InlineData(3, 1)]
        [InlineData(1, 1)]
        [InlineData(10, 1)]
        public async Task GetPaginatedAgentsResult_ReturnsExpected(int pageSize, int currentPage)
        {
            // Arrange
            var model = new PaginationModel()
            {
                PageSize = pageSize,
                CurrentPage = currentPage
            };
            // Act
            var result = await Handler.Handle(new GetPaginatedAgentResultQuery(Agents, model), CancellationToken.None);
            // Assert
            Assert.Equal(2, result.Agents.Count);
            Assert.Equal(pageSize, result.PaginationModel.PageSize);
            Assert.Equal(currentPage, result.PaginationModel.CurrentPage);
        }
    }
}
