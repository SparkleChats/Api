﻿using MediatR;
using Sparkle.Application.Common.Exceptions;
using Sparkle.Application.Common.Interfaces;
using Sparkle.Application.Common.Interfaces.Repositories;
using Sparkle.Domain;
using Sparkle.Domain.LookUps;

namespace Sparkle.Application.Chats.PersonalChats.Queries
{
    public class GetPersonalChatByUserIdQueryHandler : RequestHandlerBase,
        IRequestHandler<GetPersonalChatByUserIdQuery, PrivateChatViewModel>
    {
        private readonly IConvertor _convertor;
        private readonly IRelationshipRepository _relationshipRepository;
        public GetPersonalChatByUserIdQueryHandler(IAppDbContext context,
            IAuthorizedUserProvider userProvider,
            IConvertor convertor,
            IRelationshipRepository relationshipRepository)
            : base(context, userProvider)
        {
            _convertor = convertor;
            _relationshipRepository = relationshipRepository;
        }

        public async Task<PrivateChatViewModel> Handle(GetPersonalChatByUserIdQuery query, CancellationToken cancellationToken)
        {
            Relationship relationship = await _relationshipRepository.FindAsync((query.UserId, UserId), cancellationToken);

            if (relationship.PersonalChatId is null)
                throw new EntityNotFoundException($"Chat with user {query.UserId} does not exists", query.UserId);

            PersonalChat chat = await Context.PersonalChats.FindAsync(relationship.PersonalChatId, cancellationToken);

            return await _convertor.ConvertToViewModelAsync(chat, cancellationToken);
        }
    }
}
