using System;
using System.Collections.Generic;

namespace demo_RaceCondition.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }
        public bool? IsComfirmEmail { get; set; }
    }
}
