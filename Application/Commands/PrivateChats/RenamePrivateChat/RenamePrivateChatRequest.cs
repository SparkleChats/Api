﻿using System.ComponentModel.DataAnnotations;
using Application.Models;
using MediatR;
using MongoDB.Bson;

namespace Application.Commands.PrivateChats.RenamePrivateChat
{
    public class RenamePrivateChatRequest : IRequest<PrivateChat>
    {
        public string ChatId { get; init; }
        public string NewTitle { get; init; }
    }
}