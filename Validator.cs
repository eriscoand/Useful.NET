using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Data.Entity.Validation;

public static class Validator
{
    public static string ToString(DbEntityValidationException ev)
    {
        var returnText = "Error: ";
        foreach (var eve in ev.EntityValidationErrors)
        {
            foreach (var ve in eve.ValidationErrors)
            {
                returnText += ve.ErrorMessage + " ";
            }
        }
        return returnText;
    }
}