using System;
using System.Collections.Generic;
using System.Text;

namespace PlayerOne.Helpers
{
    internal static class IdFormatter
    {
        public static string Platform(int id) => $"PLT-{id:D3}";
        public static string Genre(int id) => $"GNR-{id:D3}";
        public static string Game(int id) => $"GAM-{id:D3}";
        public static string User(int id) => $"USR-{id:D3}";
    }
}
