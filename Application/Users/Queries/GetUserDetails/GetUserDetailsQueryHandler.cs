﻿using AutoMapper;
using MediatR;
using Sparkle.Application.Common.Interfaces;
using Sparkle.Application.Models;

namespace Sparkle.Application.Users.Queries.GetUserDetails
{
    public class GetUserDetailsQueryHandler : RequestHandlerBase, IRequestHandler<GetUserDetailsQuery, GetUserDetailsDto>
    {

        public async Task<GetUserDetailsDto> Handle(GetUserDetailsQuery query, CancellationToken cancellationToken)
        {
            Context.SetToken(cancellationToken);

            User user = await Context.SqlUsers.FindAsync(query.UserId);
            GetUserDetailsDto userDto = Mapper.Map<GetUserDetailsDto>(user);

            if (query.ServerId is not null)
            {
                Server server = await Context.Servers.FindAsync(query.ServerId);
                ServerProfile? serverProfile = server.ServerProfiles.FirstOrDefault(profile => profile.UserId == query.UserId);
                userDto.ServerProfile = Mapper.Map<GetUserDetailsServerProfileDto>(serverProfile);
            }
            return userDto;
        }

        public GetUserDetailsQueryHandler(IAppDbContext context, IAuthorizedUserProvider userProvider, IMapper mapper) : base(context, userProvider, mapper)
        {
        }
    }
}