﻿using System;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Contracts
{
    public abstract class CommandBase<TResult> : ICommand<TResult>
    {
        protected CommandBase()
        {
            this.Id = Guid.NewGuid();
        }

        protected CommandBase(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}