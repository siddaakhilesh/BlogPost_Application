﻿using Microsoft.Identity.Client;

namespace MVC_Application.Models.ViewModels
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
