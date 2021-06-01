using System;
using System.Collections.Generic;
using System.Linq;

namespace mxwlf.validation
{
    public static class ValidationExtensions
    {
        public static T ThrowIfNull<T>(this T @object, string argumentName = null) where T: class
        {
            return @object ?? throw new ArgumentNullException(argumentName);
        }

        public static T ThrowIfNoValue<T>(this T? @object, string argumentName = null) where T: struct
        {
            if (!@object.HasValue)
                throw new ArgumentNullException(argumentName);

            return @object.Value;
        }

        public static string ThrowIfNullEmptyOrWhiteSpace(this string argumentValue, string argumentName = null, string message = null)
        {
            if(string.IsNullOrEmpty(argumentValue?.Trim()))
            {
                throw new ArgumentNullException(argumentName, message);
            }

            return argumentValue;
        }

        public static Guid ThrowIfNotGuid(this string stringValue, string argumentName = null)
        {
            if (!Guid.TryParse(stringValue, out Guid guidValue))
            {
                return guidValue;
            }
            
            throw new ArgumentException($"The '{argumentName ?? ("string")}' argument should be a valid GUID");
        }

        public static IEnumerable<T> ThrowIfNullOrNoElements<T>(this IEnumerable<T> @object, string argumentName = null) where T: class
        {
            @object.ThrowIfNull(argumentName);
            if (!@object.Any())
            {
                throw new ArgumentException("The collection does not contain any element", argumentName);
            }

            return @object;
        }
    }
}   