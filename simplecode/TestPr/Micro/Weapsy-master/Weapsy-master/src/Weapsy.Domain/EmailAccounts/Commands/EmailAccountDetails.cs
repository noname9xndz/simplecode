﻿using System;

namespace Weapsy.Domain.EmailAccounts.Commands
{
    public class EmailAccountDetails : BaseSiteCommand
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string DisplayName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool DefaultCredentials { get; set; }
        public bool Ssl { get; set; }
    }
}
