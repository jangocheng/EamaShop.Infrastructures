using System;

namespace EamaBuilder.Extensions.Primitives.Rabbit
{
    internal static class ArgumentUtilities
    {
        public static T NotNull<T>(T obj, string parameterName) where T : class
        {
            if (obj == null)
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException(parameterName);
            }
            return obj;
        }

        public static string NotEmpty(string value, string parameterName)
        {
            if (value == null)
            {
                NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentNullException(parameterName);
            }

            if (value.Trim() == "")
            {
                NotEmpty(parameterName, nameof(parameterName));
                var message = string.Format("Argument of {0} only support non-empty value", parameterName);
                throw new ArgumentException(message, parameterName);
            }
            return value;
        }
    }
}
