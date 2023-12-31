﻿using FluentValidation;
using Sparkle.Application.Common.Constants;
using Sparkle.Application.Common.Validation;

namespace Sparkle.Application.Chats.GroupChats.Commands.RenameGroupChat
{
    public class RenameGroupChatCommandValidator : AbstractValidator<RenameGroupChatCommand>
    {
        public RenameGroupChatCommandValidator()
        {
            RuleFor(c => c.ChatId).NotNull().IsObjectId();
            RuleFor(c => c.NewTitle).MaximumLength(Constants.Channel.TitleMaxLength);
        }
    }
}
