﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerMGT.Application.ViewModels
{
    public class CreateUserModel
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Repassword { get; set; }

        public string Phone { get; set; }

        public bool IsAdmin { get; set; }
    }
}
