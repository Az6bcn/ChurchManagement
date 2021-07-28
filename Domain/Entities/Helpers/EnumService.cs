using System;

namespace Domain.Entities.Helpers
{
    public class EnumService<T> where T: Enum
    {
        public static EnumValue GetValue(T enumType)
        {
            EnumService<T>.GetEnumValue(enumType, out var result);

            return result;
        }
        
        private static void GetEnumValue(T enumeration, out EnumValue value)
        {
            var stringValue = (T)Enum.Parse(typeof(T), enumeration.ToString());
            //
            var intValue = (int)Enum.Parse(typeof(T), enumeration.ToString());
            //
            //
            value = new()
            {
                Value = stringValue.ToString(),
                Id = intValue
            };
        }
    }

}