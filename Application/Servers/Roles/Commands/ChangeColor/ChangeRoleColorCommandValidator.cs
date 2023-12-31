﻿using FluentValidation;
using Sparkle.Application.Servers.Roles.Common.Validation;

namespace Sparkle.Application.Servers.Roles.Commands.ChangeColor
{
    public class ChangeRoleColorCommandValidator : AbstractValidator<ChangeRoleColorCommand>
    {
        public ChangeRoleColorCommandValidator()
        {
            RuleFor(c => c.RoleId).NotDefaultRoleId();

            RuleFor(c => c.Color).NotNull().Color();
        }
    }
}
