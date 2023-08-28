﻿using Application.Queries.GetServer;
using Tests.Common;

namespace Tests.Servers.Queries
{
    public class GetServersListQueryTests : TestBase
    {
        [Fact]
        public async Task Handle_ReturnsExpectedServers()
        {
            // Arrange
            CreateDatabase();
            int userId = Ids.UserAId;
            CancellationToken cancellationToken = CancellationToken.None;

            SetAuthorizedUserId(userId);
            GetServersRequestHandler handler = new(Context, UserProvider, Mapper);

            GetServersRequest request = new();

            // Act
            List<GetServerLookupDto> result = await handler.Handle(request, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Server 1", result[0].Title);
        }
        [Fact]
        public async Task Handle_ReturnsUnexpectedServers()
        {
            // Arrange
            CreateDatabase();
            int userId = int.MaxValue;
            CancellationToken cancellationToken = CancellationToken.None;

            SetAuthorizedUserId(userId);
            
            GetServersRequestHandler handler = new(Context,UserProvider, Mapper);

            GetServersRequest request = new();

            // Act
            List<GetServerLookupDto> result = await handler.Handle(request, cancellationToken);

            // Assert
            Assert.Empty(result);
        }
    }
}
