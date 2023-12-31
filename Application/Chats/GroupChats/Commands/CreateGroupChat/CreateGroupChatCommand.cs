﻿using MediatR;
using Sparkle.Domain;
using System.ComponentModel.DataAnnotations;

namespace Sparkle.Application.Chats.GroupChats.Commands.CreateGroupChat
{
    public record CreateGroupChatCommand : IRequest<GroupChat>
    {
        /// <summary>
        /// The title of the group chat.
        /// </summary>
        public string? Title { get; init; }

        /// <summary>
        /// The URL of the image for the group chat. (Optional)
        /// </summary>
        [DataType(DataType.ImageUrl)]
        public string? Image { get; init; }

        /// <summary>
        /// The list of unique identifiers of users to be added to the group chat.
        /// </summary>
        public List<Guid> UserIds { get; init; }
    }
}
