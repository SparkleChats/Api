﻿using MediatR;
using Sparkle.Application.Common.Exceptions;
using Sparkle.Application.Common.Interfaces;
using Sparkle.Application.Common.Interfaces.Repositories;
using Sparkle.Domain;

namespace Sparkle.Application.Chats.GroupChats.Commands.AddMemberToGroupChat
{
    public class AddMemberToGroupChatCommandHandler : RequestHandlerBase, IRequestHandler<AddMemberToGroupChatCommand, Chat>
    {
        private readonly IUserProfileRepository _repository;
        private readonly IRoleFactory _roleFactory;
        public async Task<Chat> Handle(AddMemberToGroupChatCommand command, CancellationToken cancellationToken)
        {
            Context.SetToken(cancellationToken);

            GroupChat chat = await Context.GroupChats.FindAsync(command.ChatId, cancellationToken);

            if (await _repository.ChatContainsUserAsync(chat.Id, command.NewMemberId, cancellationToken))
                throw new NoPermissionsException("User is already a member of the chat");

            UserProfile profile = new()
            {
                ChatId = chat.Id,
                UserId = command.NewMemberId,
                Roles = { _roleFactory.GroupChatMemberRole }
            };

            chat.Profiles.Add(profile.Id);
            await _repository.AddAsync(profile, cancellationToken);
            await Context.GroupChats.UpdateAsync(chat, cancellationToken);

            return chat;
        }

        public AddMemberToGroupChatCommandHandler(IAppDbContext context, Common.Interfaces.Repositories.IUserProfileRepository repository, IRoleFactory roleFactory)
            : base(context)
        {
            _repository = repository;
            _roleFactory = roleFactory;
        }
    }
}