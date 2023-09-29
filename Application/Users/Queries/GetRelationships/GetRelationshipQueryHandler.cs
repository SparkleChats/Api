﻿using AutoMapper;
using MediatR;
using Sparkle.Application.Common.Interfaces;

namespace Sparkle.Application.Users.Queries.GetRelationships
{
    public class GetRelationshipQueryHandler : RequestHandlerBase,
        IRequestHandler<GetRelationshipQuery, List<RelationshipDto>>
    {
        public GetRelationshipQueryHandler(IAppDbContext context, IAuthorizedUserProvider userProvider,
            IMapper mapper) : base(context, userProvider, mapper)
        {
        }

        public Task<List<RelationshipDto>> Handle(GetRelationshipQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //public async Task<List<RelationshipDto>> Handle(GetRelationshipQuery query,
        //    CancellationToken cancellationToken)
        //{
        //    Context.SetToken(cancellationToken);

        //    RelationshipList? relationships =
        //        await Context.RelationshipLists.FindOrDefaultAsync(UserId) ??
        //        new RelationshipList()
        //        {
        //            Id = UserId,
        //            Relationships = new List<Relationship>()
        //        };

        //    List<RelationshipDto> relationshipDtos = relationships.Relationships
        //        .ConvertAll(relationship => new RelationshipDto()
        //        {
        //            User = Mapper.Map<UserLookUp>(Context.SqlUsers.FindAsync(relationship.UserId).Result),
        //            RelationshipType = relationship.RelationshipType
        //        });

        //    return relationshipDtos;
        //}
    }
}