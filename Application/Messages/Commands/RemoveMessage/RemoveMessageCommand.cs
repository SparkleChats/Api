﻿using MediatR;
using Sparkle.Domain;
using System.ComponentModel;

namespace Sparkle.Application.Messages.Commands.RemoveMessage
{
    public record RemoveMessageCommand : IRequest<Chat>
    {
        /// <summary>
        /// Id of the message to remove
        /// </summary>
        [DefaultValue("5f95a3c3d0ddad0017ea9291")]
        public string MessageId { get; init; }
        public string ChatId { get; init; }
    }
}