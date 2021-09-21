using UnityEngine;

namespace General
{
    public static class Utilities
    {
        public static bool IntToBool(int value)
        {
            return value switch
            {
                1 => true,
                0 => false,
                _ => false
            };
        }
        
        public static int BoolToInt(bool value)
        {
            return value switch
            {
                true => 1,
                false => 0
            };
        }
    }
}