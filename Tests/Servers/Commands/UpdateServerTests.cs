﻿using Application.Commands.Servers.UpdateServer;
using Application.Common.Exceptions;
using Application.Models;
using Application.Providers;
using Tests.Common;

namespace Tests.Servers.Commands
{
    public class UpdateServerTests : TestBase
    {
        [Fact]
        public async Task Success()
        {
            //Arrange
            CreateDatabase();
            Guid userId = Ids.UserBId;
            string serverId = Ids.Server2;
            const string newTitle = "Updated title";

            Mock<IAuthorizedUserProvider> userProvider = new();
            userProvider.Setup(p => p.GetUserId()).Returns(userId);

            UpdateServerRequest request = new()
            {
                ServerId = serverId,
                Image = null,
                Title = newTitle
            };
            UpdateServerRequestHandler handler = new(Context, userProvider.Object);

            //Act
            Context.SetToken(CancellationToken);
            await handler.Handle(request, CancellationToken);
            Server updatedServer = await Context.Servers.FindAsync(serverId);

            //Assert
            Assert.NotNull(updatedServer);
            Assert.Equal(newTitle, updatedServer.Title);
        }

        [Fact]
        public async Task Unauthorized_Fail()
        {
            //Arrange
            CreateDatabase();
            Guid userId = Ids.UserAId;
            string serverId = Ids.Server2;
            const string newTitle = "Updated title";

            Mock<IAuthorizedUserProvider> userProvider = new();
            userProvider.Setup(p => p.GetUserId()).Returns(userId);

            UpdateServerRequest request = new()
            {
                ServerId = serverId,
                Image = null,
                Title = newTitle
            };
            UpdateServerRequestHandler handler = new(Context, userProvider.Object);

            //Act
            //Assert
            await Assert.ThrowsAsync<NoPermissionsException>(async () => await handler.Handle(request, CancellationToken));
        }
    }
}
