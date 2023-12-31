﻿using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sparkle.Application.Users.Queries
{
    public record GetUserDetailsQuery : IRequest<GetUserDetailsDto>
    {
        /// <summary>
        /// The unique identifier of the user to retrieve details for
        /// </summary>
        [Required]
        public Guid UserId { get; init; }

        /// <summary>
        /// Id of the server for providing user's ServerProfile for this server
        /// </summary>
        [DefaultValue("5f95a3c3d0ddad0017ea9291")]
        public string? ServerId { get; init; }
    }
}