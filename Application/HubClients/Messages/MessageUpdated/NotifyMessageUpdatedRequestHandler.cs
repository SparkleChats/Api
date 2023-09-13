using AutoMapper;
using MediatR;
using Sparkle.Application.Common.Interfaces;
using Sparkle.Application.Models;
using Sparkle.Application.Models.LookUps;

namespace Sparkle.Application.HubClients.Messages.MessageUpdated
{
    public class NotifyMessageUpdatedRequestHandler : HubRequestHandlerBase, IRequestHandler<NotifyMessageUpdatedRequest>
    {
        public async Task Handle(NotifyMessageUpdatedRequest request, CancellationToken cancellationToken)
        {
            SetToken(cancellationToken);
            Message message = await Context.Messages.FindAsync(request.MessageId);
            MessageDto messageDto = Mapper.Map<MessageDto>(message);
            messageDto.User = Mapper.Map<UserLookUp>(await Context.SqlUsers.FindAsync(message.User));
            Chat chat = await Context.Chats.FindAsync(message.ChatId);
            if (chat is Channel channel) messageDto.ServerId = channel.ServerId;

            await SendAsync(ClientMethods.MessageUpdated, messageDto, GetConnections(chat));
        }

        public NotifyMessageUpdatedRequestHandler(IHubContextProvider hubContextProvider, IAppDbContext context, IMapper mapper) :
            base(hubContextProvider, context, mapper)
        { }
    }
}