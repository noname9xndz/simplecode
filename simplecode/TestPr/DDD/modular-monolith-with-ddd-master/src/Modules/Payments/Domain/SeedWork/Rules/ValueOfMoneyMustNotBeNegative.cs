﻿using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork.Rules
{
    public class ValueOfMoneyMustNotBeNegative : IBusinessRule
    {
        private readonly decimal _value;

        public ValueOfMoneyMustNotBeNegative(decimal value)
        {
            _value = value;
        }

        public bool IsBroken() => _value < 0;

        public string Message => "Value of money must not be negative.";
    }
}