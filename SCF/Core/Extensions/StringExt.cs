using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Peyton.Core;
using Peyton.Core.Common;
using Peyton.Core.Enterprise;
using Peyton.Core.Repository;

// ReSharper disable once CheckNamespace

namespace System
{
    public static class StringExt
    {
        public static Peyton.Core.Common.Version ToVersion(this string val)
        {
            var vals = val.Split(".");
            var result = new Peyton.Core.Common.Version();
            var n = vals.Count();
            if (n > 0)
                result.No = vals[0].ToInt();
            if (n > 1)
                result.Major = vals[1].ToInt();
            if (n > 2)
                result.Minor = vals[2].ToInt();
            if (n > 3)
                result.Build = vals[3].ToInt();
            return result;
        }
        public static string Percentage(double val)
        {
            return string.Format("{0}%", (int) (val*100));
        }
        public static string Percentage(double? val)
        {
            return val.HasValue? string.Format("{0}%", (int)(val * 100)) : "--%";
        }
        public static string HtmlResolve(this string val)
        {
            return val.Replace("\n", "<br />");
        }

        public static string Trim(this string val)
        {
            if (IsEmpty(val))
                return string.Empty;
            return val.Trim();
        }

        public static string TrimLower(this string val)
        {
            if (IsEmpty(val))
                return string.Empty;
            return val.Trim().ToLower();
        }

        public static string Get(params string[] vals)
        {
            foreach (var val in vals)
            {
                if (!IsEmpty(val))
                    return val.Trim();
            }
            return string.Empty;
        }

        public static MvcHtmlString ToHtml(this string val)
        {
            return new MvcHtmlString(val);
        }

        public static bool IsEmpty(this string val)
        {
            return string.IsNullOrWhiteSpace(val);
        }

        public static long ToLong(this string val)
        {
            long result;
            long.TryParse(val, out result);
            return result;
        }

        public static int ToInt(this string val)
        {
            int result;
            int.TryParse(val, out result);
            return result;
        }

        public static double ToDouble(this string val)
        {
            double result;
            double.TryParse(val, out result);
            return result;
        }

        public static bool EqualEx(this string val, string par)
        {
            var a = Trim(val);
            var b = Trim(par);
            return a.Equals(b, StringComparison.OrdinalIgnoreCase);
        }

        public static Dictionary<string, string> LogChanges(this Dictionary<string, string> vals, string format,
            Dictionary<string, string> newVals)
        {
            var result = new Dictionary<string, string>();
            foreach (var key in vals.Keys)
            {
                var val = Trim(vals[key]);
                var newVal = Trim(newVals[key]);
                if (!val.EqualEx(newVal))
                    result[key] = string.Format(format, key, val, newVal);
            }
            return result;
        }

        public static string Combine(params string[] vals)
        {
            return Combine(vals, " ");
        }

        public static string Combine(this IEnumerable<string> vals, string separator)
        {
            vals = vals.Trim();
            return string.Join(separator, vals);
        }

        public static string ToSha1(this string val)
        {
            val = Trim(val);
            var algorithm = SHA1.Create();
            var code = algorithm.ComputeHash(Encoding.UTF8.GetBytes(val));
            return Convert.ToBase64String(code);
        }

        public static string[] Split(this string val, string separator,
            StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries)
        {
            if (val == null)
                return new string[0];
            else
            {
                var s = val.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
                var n = s.Count();
                for (var i = 0; i < n; i++)
                {
                    s[i] = s[i].Trim();
                }
                return s;
            }

        }

        public static bool ToBoolean(this string val)
        {
            if (val == null)
                return false;
            if (val.ToUpper() == "TRUE")
                return true;
            if (val == "1")
                return true;
            return false;
        }




        public static bool IsEmail(this string s)
        {
            using (var context = new DbContext())
            {
                var emailRegex = context.Get<Setting>("EmailRegex");
                if (string.IsNullOrWhiteSpace(s))
                    return true;
                var regex = new Regex(emailRegex.Description);
                return regex.IsMatch(s);
            }

        }

        public static Email ParseEmail(this string email)
        {
            var result = new Email();
            if (string.IsNullOrWhiteSpace(email))
                return result;

            var s =
                email.Split(new[] {"<", ">", " ", "[", "]"}, StringSplitOptions.RemoveEmptyEntries).ToList();
            result.Adress = s.LastOrDefault();
            if (s.Count <= 1) return result;
            s.Remove(result.Adress);
            result.Name = s.Combine(" ");
            return result;
        }

        public static string TripleDesDecrypt(this string encrypted, string password)
        {
            using (var hashmd5 = new MD5CryptoServiceProvider())
            using (var des = new TripleDESCryptoServiceProvider())
            {
                var pwdhash = hashmd5.ComputeHash(Encoding.ASCII.GetBytes(password));
                des.Key = pwdhash;
                des.Mode = CipherMode.ECB; //CBC, CFB
                var buff = Convert.FromBase64String(encrypted);
                return Encoding.ASCII.GetString(des.CreateDecryptor().TransformFinalBlock(buff, 0, buff.Length));
            }
        }

        public static string TripleDesEncrypt(this string original, string password)
        {
            using (var hashmd5 = new MD5CryptoServiceProvider())
            using (var des = new TripleDESCryptoServiceProvider())
            {
                var pwdhash = hashmd5.ComputeHash(Encoding.ASCII.GetBytes(password));
                des.Key = pwdhash;
                des.Mode = CipherMode.ECB; //CBC, CFB
                var buff = Encoding.ASCII.GetBytes(original);
                return Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buff, 0, buff.Length));
            }
        }

        public static string Simplify(this string orginal)
        {
            return Regex.Replace(orginal, @"\W+", "");
        }

        public static string Format(string format, double? val)
        {
            return string.Format(format, val.HasValue ? val.Value.ToString() : "--");
        }
    }
}