﻿using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sparkle.Application.Servers.ServerProfiles.Commands.UnbanUser
{
    public record UnbanUserCommand : IRequest
    {
        [DefaultValue("5f95a3c3d0ddad0017ea9291")]
        public string ServerId { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}