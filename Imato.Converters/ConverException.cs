using System;

namespace Imato.Converters
{
    public class ConverException<T> : ApplicationException
    {
        public ConverException(string value)
            : base($"Cannot parse value {value} as {typeof(T).Name}")
        {
        }
    }
}