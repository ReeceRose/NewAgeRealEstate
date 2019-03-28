using NARE.Application.Agent.Query.GetAllAgentsPaginated;
using NARE.Domain.Entities;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Query.GetAllAgentsPaginated
{
    public class GetAllAgentsPaginatedValidatorTest
    {
        public GetAllAgentsPaginatedQueryValidator Validator { get; }

        public GetAllAgentsPaginatedValidatorTest()
        {
            // Arrange
            Validator = new GetAllAgentsPaginatedQueryValidator();
        }

        [Theory]
        [InlineData(1, 10)]
        [InlineData(2, 15)]
        [InlineData(3, 100)]
        public void GetAllAgentsPaginated_PaginatedModelIdIsValid(int currentPage, int pageSize)
        {
            // Act
            var model = new PaginationModel() {CurrentPage = currentPage, PageSize = pageSize};
            var result = Validator.Validate(new GetAllAgentsPaginatedQuery(model));
            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void GetAllAgentsPaginated_PaginatedModelIsInvalid()
        {
            // Act
            var result = Validator.Validate(new GetAllAgentsPaginatedQuery(null));
            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Pagination model required", result.Errors[0].ErrorMessage);
        }
    }
}
