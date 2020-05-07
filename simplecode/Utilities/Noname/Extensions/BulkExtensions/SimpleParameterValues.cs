﻿namespace GenericRepository.Extensions.BulkExtensions
{
    using Microsoft.EntityFrameworkCore.Query;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    internal class SimpleParameterValues : IParameterValues
    {
        private readonly Dictionary<string, object> _parameterValues = new Dictionary<string, object>();

        public SimpleParameterValues()
        {
        }

        public void AddParameter(string name, object value)
        {
            _parameterValues.Add(name, value);
        }

        public object RemoveParameter(string name)
        {
            _parameterValues.TryGetValue(name, out var val);
            _parameterValues.Remove(name);
            return val;
        }

        public void SetParameter(string name, object value)
        {
            _parameterValues[name] = value;
        }

        public IReadOnlyDictionary<string, object> ParameterValues => new ReadOnlyDictionary<string, object>(_parameterValues);
    }
}