using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

public static class CleanStrings
{
    public static string friendlyURL(string s)
    {
        string str = RemoveAccent(s).ToLower();
        str = ToAlphaNumericOnly(str); // Remove all non valid chars          
        str = System.Text.RegularExpressions.Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space  
        str = System.Text.RegularExpressions.Regex.Replace(str, @"\s", "-"); // //Replace spaces by dashes
        return str;
    }
    
    public static string ToAlphaNumericOnly(string input, bool toUpper = false)
    {
        if (input == null) return "";

        Regex rgx = new Regex("[^a-zA-Z0-9]");

        if (toUpper)  return rgx.Replace(input.ToUpper(), "");
        
        return rgx.Replace(input, "");
        
    }

    public static string ToAlphaOnly(string input)
    {
        Regex rgx = new Regex("[^a-zA-Z]");
        return rgx.Replace(input, "");
    }

    public static string ToNumericOnly(string input)
    {
        Regex rgx = new Regex("[^0-9]");
        return rgx.Replace(input, "");
    }

    public static string RemoveAccent(string txt)
    {
        byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
        return System.Text.Encoding.ASCII.GetString(bytes);
    }

}