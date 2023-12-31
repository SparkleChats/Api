﻿using AutoMapper;
using Sparkle.Domain;
using Sparkle.Domain.Common.Interfaces;
using System.ComponentModel;

namespace Sparkle.Application.Users.Queries
{
    public record GetUserDetailsServerProfileDto : IMapWith<ServerProfile>
    {
        public Guid Id { get; set; }
        /// <summary>
        /// User's username displayed on this server.
        /// </summary>
        [DefaultValue("𝕾𝖊𝖗𝖛𝖊𝖗 𝕯𝖎𝖘𝖕𝖑𝖆𝖞𝕹𝖆𝖒𝖊")]
        public string? Name { get; set; }

        /// <summary>
        /// List of user's roles on this server.
        /// </summary>
        public List<GetUserDetailsRolesDto> Roles { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ServerProfile, GetUserDetailsServerProfileDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DisplayName));
        }
    }
}
