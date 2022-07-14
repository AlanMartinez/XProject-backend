﻿using XProject.Core.Enumerations;

namespace XProject.Core.DTOs
{
    public class SecurityDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public RoleType Role { get; set; }
    }
}