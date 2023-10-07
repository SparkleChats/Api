﻿using Microsoft.AspNetCore.Authorization;
using Sparkle.Application.Common.Interfaces;
using Sparkle.Application.Common.Interfaces.Repositories;
using Sparkle.Application.Models;
using Sparkle.WebApi.Authorization.Requirements;

namespace Sparkle.WebApi.Authorization.Handlers
{
    public class ServerClaimsAuthorizationRequirementHandler :
        AuthorizationHandler<RoleClaimsAuthorizationRequirement>
    {
        private readonly IAuthorizedUserProvider _userProvider;
        private readonly IUserProfileRepository _repository;

        public ServerClaimsAuthorizationRequirementHandler(IAuthorizedUserProvider userProvider, IUserProfileRepository repository)
        {
            _userProvider = userProvider;
            _repository = repository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            RoleClaimsAuthorizationRequirement requirement)
        {
            UserProfile? profile = await _repository
                .FindOrDefaultAsync(requirement.UserProfileId, includeRoles: true);

            if (profile is null)
                return;

            if (_userProvider.IsAdmin(profile)
                || await _userProvider.HasClaimsAsync(profile, requirement.ClaimTypes))
            {
                context.Succeed(requirement);
            }
        }
    }
}