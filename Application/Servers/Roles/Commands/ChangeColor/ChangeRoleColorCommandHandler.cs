﻿using MediatR;
using Sparkle.Application.Common.Interfaces;
using Sparkle.Domain;

namespace Sparkle.Application.Servers.Roles.Commands.ChangeColor
{
    public class ChangeRoleColorCommandHandler : RequestHandlerBase, IRequestHandler<ChangeRoleColorCommand, Role>
    {
        public ChangeRoleColorCommandHandler(IAppDbContext context) : base(context)
        {
        }

        public async Task<Role> Handle(ChangeRoleColorCommand command, CancellationToken cancellationToken)
        {
            Context.SetToken(cancellationToken);

            Role role = await Context.SqlRoles.FindAsync(command.RoleId);
            role.Color = command.Color;

            await Context.SqlRoles.UpdateAsync(role);
            return role;
        }
    }
}
