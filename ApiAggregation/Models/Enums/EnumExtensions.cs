using System.Reflection;
using System.Runtime.Serialization;

namespace ApiAggregation.Models.Enums
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Retrieves the value of the <see cref="EnumMemberAttribute"/> for a given enum value.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>The value specified in the <see cref="EnumMemberAttribute"/>, or null if not found.</returns>
        /// <exception cref="ArgumentException">Thrown when T is not an enum type.</exception>
        public static string GetEnumMemberValue<T>(this T enumValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            // Retrieve the member info of the enum
            var memberInfo = typeof(T).GetMember(enumValue.ToString()!).FirstOrDefault();

            // Get the EnumMemberAttribute data
            var enumMemberAttribute = memberInfo?.GetCustomAttribute<EnumMemberAttribute>();

            // Return the value specified in the EnumMember attribute
            return enumMemberAttribute?.Value ?? string.Empty;
        }
    }
}
