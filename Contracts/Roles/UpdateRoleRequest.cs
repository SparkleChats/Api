﻿namespace Sparkle.Contracts.Roles
{
    public record UpdateRoleRequest
    {
        /// <summary>
        /// Name of the role
        /// </summary>
        public string Name { get; init; }
        /// <summary>
        /// Color of the role in hex format
        /// </summary>
        public string Color { get; init; }
        /// <summary>
        /// Priority of the role
        /// </summary>
        public int Priority { get; init; }
        /// <summary>
        /// Claims of the role
        /// </summary>
        public IEnumerable<Claim> Claims { get; init; }
    }

    public record Claim(string Type, bool Value);
}
