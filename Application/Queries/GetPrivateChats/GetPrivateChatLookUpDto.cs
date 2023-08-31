﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Application.Interfaces;
using Application.Models;
using Application.Queries.GetMessages;
using AutoMapper;
using MongoDB.Bson;

namespace Application.Queries.GetPrivateChats
{
    public record GetPrivateChatLookUpDto : IMapWith<PrivateChat>
    {
        [StringLength(24, MinimumLength = 24)]
        [DefaultValue("5f95a3c3d0ddad0017ea9291")]
        public string Id { get; init; }
        [DataType(DataType.ImageUrl)]
        [DefaultValue("https://localhost:7060/api/media/5f95a3c3d0ddad0017ea9291")]
        public string? Image { get; init; }
        [DefaultValue("Title")]
        public string? Title { get; init; }
        public List<UserLookUp> Users { get; init; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PrivateChat, GetPrivateChatLookUpDto>()
                // .ForMember(dto => dto.Users,
                // opt => opt.MapFrom(chat => chat.Users))
                ;
        }
    }
}