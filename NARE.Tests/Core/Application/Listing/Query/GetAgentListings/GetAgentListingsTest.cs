using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Moq;
using NARE.Application.Listing.Model;
using NARE.Application.Listing.Query.GetAgentListings;
using NARE.Application.Listing.Query.GetAllListings;
using NARE.Application.Listing.Query.GetPaginatedListingsResult;
using NARE.Application.Utilities;
using NARE.Domain.Entities;
using Xunit;

namespace NARE.Tests.Core.Application.Listing.Query.GetAgentListings
{
    public class GetAgentListingsTest
    {
        public Mock<IMediator> Mediator { get; }
        public IMapper Mapper { get; }
        public GetAgentListingsQueryHandler Handler { get; }
        public List<NARE.Domain.Entities.Listing> Listings { get; }

        public GetAgentListingsTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            Handler = new GetAgentListingsQueryHandler(Mediator.Object, Mapper);
            Listings = new List<NARE.Domain.Entities.Listing>()
            {
                new NARE.Domain.Entities.Listing() { Address = "123 test", Agent = new NARE.Domain.Entities.Agent(){ Id = "123" }},
                new NARE.Domain.Entities.Listing() { Address = "456 test", Agent = new NARE.Domain.Entities.Agent(){ Id = "456" }}
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetAllListingsQuery>(), default(CancellationToken))).ReturnsAsync(Listings);
        }

        [Theory]
        [InlineData("123", 1)]
        [InlineData("456", 1)]
        [InlineData("123123", 0)]
        public async Task GetAgentListing_ReturnsCorrectListingsForAgent(string agentId, int count)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetPaginatedListingsResultQuery>(), default(CancellationToken))).ReturnsAsync(new PaginatedListingsDto() { Listings = Listings.Where(l => l.Agent.Id == agentId).ToList(), PaginationModel = new PaginationModel()});
            // Act
            var result = await Handler.Handle(new GetAgentListingsQuery(agentId, new PaginationModel()), CancellationToken.None);
            // Assert
            Assert.Equal(result.Listings.Count, count);
        }
    }
}
