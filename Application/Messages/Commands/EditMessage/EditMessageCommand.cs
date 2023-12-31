﻿using MediatR;
using System.ComponentModel;

namespace Sparkle.Application.Messages.Commands.EditMessage
{
    public record EditMessageCommand : IRequest
    {
        /// <summary>
        /// Id of the message to edit
        /// </summary>
        [DefaultValue("5f95a3c3d0ddad0017ea9291")]
        public string MessageId { get; init; }

        /// <summary>
        /// New message text. May include links
        /// </summary>
        [DefaultValue("NewTextString")]
        public string NewText { get; init; }
    }

}