﻿using MediatR;

namespace Sparkle.Application.Invitations.Commands.CreateInvitation
{
    public record CreateInvitationCommand : IRequest<string>
    {
        /// <summary>
        /// The unique identifier of the server to create an invitation for.
        /// </summary>
        public string ServerId { get; set; }

        /// <summary>
        /// Indicates whether to include user information in the invitation.
        /// </summary>
        public bool IncludeUser { get; set; }

        /// <summary>
        /// The expiration time of the invitation. (Optional)
        /// </summary>
        public DateTime? ExpireTime { get; set; }
    }
}
