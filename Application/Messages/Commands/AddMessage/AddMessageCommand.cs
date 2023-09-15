﻿using MediatR;
using Sparkle.Application.Models;
using Sparkle.Application.Models.LookUps;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sparkle.Application.Messages.Commands.AddMessage
{
    public record AddMessageCommand : IRequest<MessageDto>
    {
        /// <summary>
        /// Text of the message. Can include links
        /// </summary>
        [MaxLength(2000)]
        [DefaultValue("MessageText")]
        public string Text { get; init; }

        /// <summary>
        /// Id of the chat to send message to
        /// </summary>
        [Required]
        [StringLength(24, MinimumLength = 24)]
        [DefaultValue("5f95a3c3d0ddad0017ea9291")]
        public string ChatId { get; init; }

        /// <summary>
        /// List of URL attachments that are not included in the message text
        /// </summary>
        public List<Attachment>? Attachments { get; init; }
    }

}