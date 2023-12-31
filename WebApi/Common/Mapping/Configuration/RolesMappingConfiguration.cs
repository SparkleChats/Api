﻿using Mapster;
using Microsoft.AspNetCore.Identity;
using Sparkle.Domain;
using Sparkle.Application.Servers.Roles.Commands.Create;
using Sparkle.Application.Servers.Roles.Commands.Update;
using Sparkle.Contracts.Roles;

namespace Sparkle.WebApi.Common.Mapping.Configuration
{
    public class RolesMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(Guid RoleId, Claim Claim), IdentityRoleClaim<Guid>>()
                .Map(dest => dest.ClaimType, src => src.Claim.Type)
                .Map(dest => dest.RoleId, src => src.RoleId)
                .Map(dest => dest.ClaimValue, src => src.Claim.Value.ToString());

            config.NewConfig<IdentityRoleClaim<Guid>, Claim>()
                .Map(dest => dest.Type, src => src.ClaimType)
                .Map(dest => dest.Value, src => bool.Parse(src.ClaimValue ?? "true"));

            config.NewConfig<(string ServerId, CreateRoleRequest Request), CreateRoleCommand>()
                .Map(dest => dest.ServerId, src => src.ServerId)
                .Map(dest => dest, src => src.Request);

            config.NewConfig<(Guid RoleId, UpdateRoleRequest Request), UpdateRoleCommand>()
                .Map(dest => dest.Id, src => src.RoleId)
                .Map(dest => dest, src => src.Request);

            config.NewConfig<Role, RoleResponse>();

            config.NewConfig<(Role Role, List<IdentityRoleClaim<Guid>> Claims), RoleResponse>()
                .Map(dest => dest.Claims, src => src.Claims)
                .Map(dest => dest, src => src.Role);
        }
    }
}
