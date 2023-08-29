﻿using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Models;
using MediatR;
using MongoDB.Driver;

namespace Application.Commands.PrivateChats.LeaveFromPrivateChat
{
    public class LeaveFromPrivateChatRequestHandler : RequestHandlerBase, IRequestHandler<LeaveFromPrivateChatRequest>
    {
        public async Task Handle(LeaveFromPrivateChatRequest request, CancellationToken cancellationToken)
        {
            Context.SetToken(cancellationToken);
            
            PrivateChat chat = await Context.PrivateChats.FindAsync(request.ChatId);
            
            if (!chat.Users.Any(u => u.Id == UserId))
                throw new NoSuchUserException("User is not a member of the chat");

            if (chat.Users.Count <= 2)
            {
                await Context.Chats.DeleteAsync(chat);
            }
            else
            {
                chat.Users.Remove(chat.Users.Find(u => u.Id == UserId)?? throw new NoSuchUserException());
                if (chat.OwnerId == UserId) chat.OwnerId = chat.Users.First(u => u.Id != UserId).Id;
                
                await Context.PrivateChats.UpdateAsync(chat);
            }
        }

        public LeaveFromPrivateChatRequestHandler(IAppDbContext context, IAuthorizedUserProvider userProvider) : base(
            context, userProvider)
        {
        }
    }
}