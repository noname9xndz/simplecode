﻿using System;
using Weapsy.Domain.EmailAccounts;

namespace Weapsy.Reporting.EmailAccounts
{
    public class EmailAccountModel
    {
        public Guid SiteId { get; set; }
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string DisplayName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool DefaultCredentials { get; set; }
        public bool Ssl { get; set; }
        public EmailAccountStatus Status { get; set; }
    }
}
