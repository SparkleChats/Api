﻿using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Application.Commands.PrivateChat.CreatePrivateChat
{
    public class CreatePrivateChatRequest : IRequest<Models.PrivateChat>
    {
        [Required, MaxLength(255)] 
        public string? Title { get; init; }

        [DataType(DataType.ImageUrl)] 
        public string? Image { get; init; }

        [MaxLength(11)]
        public List<int> UsersId { get; init; }
    }
}