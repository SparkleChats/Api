﻿using MediatR;
using Sparkle.Domain;
using System.ComponentModel;

namespace Sparkle.Application.Servers.Commands.JoinServer
{
    public record JoinServerCommand : IRequest<ServerProfile>
    {
        [DefaultValue("5f95a3c3d0ddad0017ea9291")]
        public string InvitationId { get; set; }
    }
}