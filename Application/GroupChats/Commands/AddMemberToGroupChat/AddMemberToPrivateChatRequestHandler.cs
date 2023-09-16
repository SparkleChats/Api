﻿using AutoMapper;
using MediatR;
using MongoDB.Driver;
using Sparkle.Application.Common.Exceptions;
using Sparkle.Application.Common.Interfaces;
using Sparkle.Application.Models;

namespace Sparkle.Application.GroupChats.Commands.AddMemberToGroupChat
{
    public class AddMemberToGroupChatCommandHandler : RequestHandlerBase, IRequestHandler<AddMemberToGroupChatCommand>
    {
        public async Task Handle(AddMemberToGroupChatCommand command, CancellationToken cancellationToken)
        {
            Context.SetToken(cancellationToken);

            GroupChat chat = await Context.GroupChats.FindAsync(command.ChatId);

            if (!chat.Users.Any(u => u == UserId))
                throw new NoPermissionsException("User is not a member of the chat");
            if (chat.Users.Any(u => u == command.NewMemberId))
                throw new NoPermissionsException("User is already a member of the chat");


            chat.Users.Add(command.NewMemberId);

            await Context.GroupChats.UpdateAsync(chat);
        }

        public AddMemberToGroupChatCommandHandler(IAppDbContext context, IAuthorizedUserProvider userProvider, IMapper mapper) : base(context, userProvider, mapper)
        {
        }
    }
}