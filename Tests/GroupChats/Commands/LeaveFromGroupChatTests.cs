﻿using Application.Commands.GroupChats.LeaveFromGroupChat;
using Application.Common.Exceptions;
using Application.Models;
using Tests.Common;

namespace Tests.GroupChats.Commands
{
    public class LeaveFromGroupChatTests : TestBase
    {
        [Fact]
        public async Task Success_Owner()
        {
            //Arrange
            CreateDatabase();
            string chatId = Ids.GroupChat7;
            Guid userId = Ids.UserBId;

            SetAuthorizedUserId(userId);

            LeaveFromGroupChatRequest request = new()
            {
                ChatId = chatId,
            };

            LeaveFromGroupChatRequestHandler handler = new(Context, UserProvider);

            //Act

            await handler.Handle(request, CancellationToken);
            PersonalChat chat = await Context.PersonalChats.FindAsync(chatId);

            //Assert
            Assert.DoesNotContain(chat.Users, user => user == userId);
            GroupChat? groupChat = chat as GroupChat;
            Assert.NotNull(groupChat);
            Assert.NotEqual(userId, groupChat.OwnerId);
        }

        [Fact]
        public async Task Success_LastUser()
        {
            //Arrange
            CreateDatabase();
            string chatId = Ids.GroupChat4;
            Guid userId = Ids.UserAId;

            SetAuthorizedUserId(userId);

            LeaveFromGroupChatRequest request = new()
            {
                ChatId = chatId,
            };

            LeaveFromGroupChatRequestHandler handler = new(Context, UserProvider);

            //Act

            await handler.Handle(request, CancellationToken);

            //Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(
                async () => await Context.PersonalChats.FindAsync(chatId));
        }
        [Fact]
        public async Task Fail_NoSuchUser()
        {
            //Arrange
            CreateDatabase();
            string chatId = Ids.GroupChat4;
            Guid userId = Ids.UserBId;

            SetAuthorizedUserId(userId);

            LeaveFromGroupChatRequest request = new()
            {
                ChatId = chatId,
            };

            LeaveFromGroupChatRequestHandler handler = new(Context, UserProvider);

            //Act
            //Assert
            await Assert.ThrowsAsync<NoSuchUserException>(async ()
                => await handler.Handle(request, CancellationToken));
        }
    }
}
