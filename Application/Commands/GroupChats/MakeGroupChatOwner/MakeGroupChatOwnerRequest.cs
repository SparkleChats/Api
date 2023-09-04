﻿using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.GroupChats.MakeGroupChatOwner
{
    public record MakeGroupChatOwnerRequest : IRequest
    {
        /// <summary>
        /// The unique identifier of the group chat for which to make a member the owner.
        /// </summary>
        [Required]
        [StringLength(24, MinimumLength = 24)]
        [DefaultValue("5f95a3c3d0ddad0017ea9291")]
        public string ChatId { get; init; }

        /// <summary>
        /// The unique identifier of the member to be made the owner.
        /// </summary>
        [Required]
        [DefaultValue(1)]
        public int MemberId { get; init; }
    }
}