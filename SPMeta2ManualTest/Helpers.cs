
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.SharePoint.Client;

using SPMeta2.Definitions;
using SPMeta2.Models;
using SPMeta2.Syntax.Default;

using File = System.IO.File;
using Microsoft.SharePoint.Client.Taxonomy;
using SPMeta2.Utils;

namespace SPMeta2ManualTest
{


    public static class Helpers
    {

        /// <summary>
        /// Helper to return the password
        /// </summary>
        /// <returns>SecureString representing the password</returns>
        public static SecureString GetPassword()
        {
            SecureString securePassword = new SecureString();

            try
            {
                Console.Write("SharePoint Password: ");

                for (ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    keyInfo.Key != ConsoleKey.Enter;
                    keyInfo = Console.ReadKey(true))
                {
                    if (keyInfo.Key == ConsoleKey.Backspace)
                    {
                        if (securePassword.Length > 0)
                        {
                            securePassword.RemoveAt(securePassword.Length - 1);
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            Console.Write(" ");
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        }
                    }
                    else if (keyInfo.Key != ConsoleKey.Enter)
                    {
                        Console.Write("*");
                        securePassword.AppendChar(keyInfo.KeyChar);
                    }

                }
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                securePassword = null;
                Console.WriteLine(ex.Message);
            }

            return securePassword;
        }

        /// <summary>
        /// Helper to return the User name.
        /// </summary>
        /// <returns></returns>
        public static string GetUserName()
        {
            string userName;
            try
            {
                Console.Write("SharePoint Username: ");
                userName = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                userName = String.Empty;
            }
            return userName;
        }

        public static void EnsureProperties<T>(this T obj, params string[] propertyNames) where T : ClientObject
        {
            if (propertyNames.Any(x => !obj.IsPropertyAvailable(x)))
            {
                List<Expression<Func<T, object>>> funcsList = new List<Expression<Func<T, object>>>();
                foreach (var propertyName in propertyNames)
                {
                    ParameterExpression p = Expression.Parameter(typeof (T), "p");
                    Expression body = Expression.Property(p, propertyName);
                    var expr = Expression.Lambda<Func<T, object>>(body, p);
                    funcsList.Add(expr);
                }
                obj.Context.Load(obj, funcsList.ToArray());
                obj.Context.ExecuteQuery();
            }
        }

        public static bool IsNullOrServerObjectIsNull<T>(this T obj) where T : ClientObject
        {
            return ((obj == null) || (obj.ServerObjectIsNull.GetValueOrDefault(false)));
        }

        public static string CombineUrl(params string[] args)
        {
            return SPMeta2.Utils.UrlUtility.CombineUrl(args);
        }


    }
}
