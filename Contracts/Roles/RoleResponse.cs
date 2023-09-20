﻿using Microsoft.AspNetCore.Identity;

namespace Sparkle.Contracts.Roles
{
    /// <summary>
    /// Role details
    /// </summary>
    public class RoleResponse
    {
        /// <summary>
        /// Id of the role
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Non-unique name of the role
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Color of the role in hex format
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Id of the server where the role is located
        /// </summary>
        public string ServerId { get; set; }

        /// <summary>
        /// Priority that indicates how high the role is in the hierarchy
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Claims of the role
        /// </summary>
        public IEnumerable<IdentityRoleClaim<Guid>> Claims { get; set; }
    }
}
