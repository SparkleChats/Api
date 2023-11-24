﻿using MediatR;
using Sparkle.Application.Models;
using System.ComponentModel;

namespace Sparkle.Application.Chats.GroupChats.Commands.ChangeGroupChatOwner
{
    public record ChangeGroupChatOwnerCommand : IRequest<GroupChat>
    {
        /// <summary>
        /// The unique identifier of the group chat to change an owner in.
        /// </summary>
        [DefaultValue("5f95a3c3d0ddad0017ea9291")]
        public string ChatId { get; init; }

        /// <summary>
        /// The unique identifier of the member to be made the owner.
        /// </summary>
        public Guid ProfileId { get; init; }
    }
}
