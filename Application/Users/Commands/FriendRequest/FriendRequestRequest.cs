﻿using MediatR;

namespace Sparkle.Application.Users.Commands.FriendRequest
{
    public record FriendRequestRequest : IRequest<string?>
    {
        /// <summary>
        /// The unique identifier of the user to send a friend request to.
        /// </summary>
        public string UserName { get; init; }
    }
}