using System.Reflection;
using System.Runtime.Serialization;

namespace ApiAggregation.Models.Enums
{
    public static class EnumExtensions
    {
        public static string GetEnumMemberValue<T>(this T enumValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            // Retrieve the member info of the enum
            var memberInfo = typeof(T).GetMember(enumValue.ToString()).FirstOrDefault();

            // Get the EnumMemberAttribute data
            var enumMemberAttribute = memberInfo?.GetCustomAttribute<EnumMemberAttribute>();

            // Return the value specified in the EnumMember attribute
            return enumMemberAttribute?.Value;
        }
    }
}
