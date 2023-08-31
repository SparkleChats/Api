﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Application.Models;
using MediatR;
using MongoDB.Bson;

namespace Application.Commands.Messages.RemoveAttachment
{
    public class RemoveAttachmentRequest : IRequest<Chat>
    {
        [Required]
        [StringLength(24, MinimumLength = 24)]
        [DefaultValue("5f95a3c3d0ddad0017ea9291")]
        public string MessageId { get; init; }
        
        [Required]
        [DefaultValue(0)]
        public int AttachmentIndex { get; init; }
    }
}