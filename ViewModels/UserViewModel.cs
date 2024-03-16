﻿namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class UserViewModel
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }



    }
}
