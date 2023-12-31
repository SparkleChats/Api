﻿using FluentValidation;
using Sparkle.Application.Common.Constants;
using Sparkle.Application.Common.Validation;
using Sparkle.Application.Servers.Roles.Common.Validation;

namespace Sparkle.Application.Servers.Roles.Commands.Update
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(c => c.Id).NotDefaultRoleId();

            RuleFor(c => c.Name).RequiredMaximumLength(
               Constants.Roles.RoleNameMaxLength)
                .NotDefaultRoleName();

            RuleFor(c => c.Color).NotNull().NotEmpty();
            RuleFor(c => c.Color).Color();

            RuleFor(c => c.Priority).NotNull().GreaterThan(0).LessThan(100);

            RuleFor(c => c.Claims).NotNull();
            RuleForEach(c => c.Claims).SetValidator(new ClaimValidator());
        }
    }
}
