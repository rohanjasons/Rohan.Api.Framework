﻿using System.Text.RegularExpressions;

namespace Api.Framework.Tests.Helper
{
    public static class RegexHelpers
    {
        public static string RegexHelper(this string value)
        {
            return Regex.Replace(value, @"(\s+|@|&|'|\(|\)|<|>|#|{|}|:|;|\"")", "");
        }
    }
}