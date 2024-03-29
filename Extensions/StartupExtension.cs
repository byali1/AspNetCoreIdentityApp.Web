﻿using AspNetCoreIdentityApp.Web.CustomValidations;
using AspNetCoreIdentityApp.Web.Localizations;
using AspNetCoreIdentityApp.Web.Models;
using AspNetCoreIdentityApp.Web.OptionsModels;
using AspNetCoreIdentityApp.Web.Services.Abstract;
using AspNetCoreIdentityApp.Web.Services.Concrete;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.Extensions
{
    public static class StartupExtension
    {
        public static void AddIdentityExtension(this IServiceCollection services)
        {
            //Token süresi
            services.Configure<DataProtectionTokenProviderOptions>(option =>
            {
                option.TokenLifespan = TimeSpan.FromMinutes(15);
            });



            services.AddIdentity<AppUser, AppRole>(options =>
            {
                //User
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvwxyzABCDEFGHIJKLMNOPRSTUVWXYZ1234567890_";
                //options.SignIn.RequireConfirmedEmail = true;

                //Password
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true; //!*
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;

                //Lockout
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 3;


            }).AddPasswordValidator<PasswordValidator>().
                AddUserValidator<UserValidator>().
                AddErrorDescriber<LocalizationIdentityErrorDescriber>().
                AddDefaultTokenProviders(). //Token (ForgotMyPassword)
                AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
