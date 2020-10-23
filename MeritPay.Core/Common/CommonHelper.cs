using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MeritPay.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Sso.UMProxy;

namespace MeritPay.Core.Common
{
    public static class CommonHelper
    {

       // public static Microsoft.AspNetCore.Http.HttpContext Current => new HttpContextAccessor().HttpContext;
        //public static string CurenUserRole
        //{
        //    get
        //    {
        //        string label = String.Empty;
        //        var user = CurrentUser;
        //        if (user == null) return label;
        //        try
        //        {
        //            var role = CurrentUser.Position.FirstOrDefault();
        //            if (role != null)
        //            {
        //                label = role.Code;
        //            }

        //        }
        //        catch
        //        {
        //            label = String.Empty;
        //        }
        //        return String.Format("{0}", label);
        //    }
        //}
        public static int CurrentUserDepCode
        {
            get
            {
                //var user = CurrentUser;

                //if (user == null) return -1;
                //return (int)(long.Parse(user.Corporates.DescendantsAndSelf("l").ToArray()[0].Attribute("i").Value) % 100000);

                var user = CurrentUser;
                //#if DEBUG
                //                return 136;
                //#endif
                //if (user == null || !user.HasPermission(UserPermission.PejvakBranchAccess.ToString())) return -1;
                //return (int)(long.Parse(user.Corporates.DescendantsAndSelf("l").ToArray()[0].Attribute("i").Value) % 100000);

                if (user == null) return -1;

                long n = long.Parse(user.Corporates.DescendantsAndSelf("l").ToArray()[0].Attribute("i").Value);
                var ayandeh = Branches.FirstOrDefault(p => p.GhesmatkhedmatID == n);
                var result = ayandeh == null ? (int)(long.Parse(user.Corporates.DescendantsAndSelf("l").ToArray()[0].Attribute("i").Value) % 100000) : (int)ayandeh.Branch;
                return result;
            }
        }
       
        public static string GetCurrentDate()
        {
            string[] array = Faranam.Utils.PersianDate.Today(true, false).Split('/');
            var currentmonth = Int32.Parse(array[1]);
            var today = "";
            switch (currentmonth)
            {
                case 1:
                    today = "فروردین";
                    break;
                case 2:
                    today = "اردیبهشت";
                    break;
                case 3:
                    today = "خرداد";
                    break;
                case 4:
                    today = "تیر";
                    break;
                case 5:
                    today = "مرداد";
                    break;
                case 6:
                    today = "شهریور";
                    break;
                case 7:
                    today = "مهر";
                    break;
                case 8:
                    today = "آبان";
                    break;
                case 9:
                    today = "آذر";
                    break;
                case 10:
                    today = "دی";
                    break;
                case 11:
                    today = "بهمن";
                    break;
                case 12:
                    today = "اسفند";
                    break;
            }
            var currentDate = array[2] + " " + today + " " + array[0];
            return currentDate;
        }
        #region Date Convertion
        public static string ChangepersianFormatToDate(string date)
        {
            string[] dateresult = date.Split('/');
            var endresult = dateresult[0] + "/" + string.Format("{0:00}", dateresult[1]) + "/" + string.Format("{0:00}", dateresult[2]);
            return endresult;
        }
        public static int CalcuLateleftDay(DateTime date)
        {
            //var currentDate = GetPersianDate(DateTime.Now);
            //var insertdate = GetPersianDate(date);
            //var currentDateTime = Int32.Parse((currentDate.Substring(currentDate.LastIndexOf('/') + 1)));
            //var insertDatetime = Int32.Parse(insertdate.Substring(currentDate.LastIndexOf('/') + 1));
            var result = Convert.ToInt32((DateTime.Now - date).TotalDays);
            return result;
        }
        #endregion
        #  region User
        public static bool CurrentUserHasPermission(UserPermission p)
        {
            var user =  CurrentUser;
            if (user == null) return false;
            return user.HasPermission(p.ToString());
        }

        private static Sso.UMProxy.AppSettings UMLSettings = new Sso.UMProxy.AppSettings();
        public static void SetSetting()
        {
            UMLSettings.SSOApplicationName = Contracts.AppSettings.SSOApplicationName;
            UMLSettings.SSOApplicationId = Contracts.AppSettings.SSOApplicationId;
            UMLSettings.SSOdomainName = Contracts.AppSettings.SSOdomainName;
            UMLSettings.SSOUrl = Contracts.AppSettings.SSOUrl;
        }


        public static SsoUser CurrentUser;

        #endregion
        public static IEnumerable<string> SplitByLength(this string s, int length)
        {
            for (int i = 0; i < s.Length; i += length)
            {
                if (i + length <= s.Length)
                {
                    yield return s.Substring(i, length);
                }
                else
                {
                    yield return s.Substring(i);
                }
            }
        }
        //private static string ToString(this UserPermission p)
        //{
        //    return ((int)p).ToString();
        //}
        public static int UserDepartmentCode
        {
            get
            {
                var user = CurrentUser;

                if (user == null) return -1;
                // return (int)(long.Parse(user.Corporates.DescendantsAndSelf("l").ToArray()[0].Attribute("i").Value) % 100000);
                long n = long.Parse(user.Corporates.DescendantsAndSelf("l").ToArray()[0].Attribute("i").Value);
                var ayandeh = Branches.FirstOrDefault(p => p.GhesmatkhedmatID == n);
                var result = ayandeh == null ? -1 : (int)ayandeh.Branch;
                if (result == -1 && !user.UserGroups.Any(d => d.Name == "admin")) return -2;

                return result;
            }
        }
        public static IEnumerable<BranchViewModel> Branches;
        public static int CurrentUserBranchCode
        {
            get
            {
                var user = CurrentUser;
                //#if DEBUG
                //                return 136;
                //#endif
                //if (user == null || !user.HasPermission(UserPermission.PejvakBranchAccess.ToString())) return -1;
                //return (int)(long.Parse(user.Corporates.DescendantsAndSelf("l").ToArray()[0].Attribute("i").Value) % 100000);

                if (user == null) return -1;

                long n = long.Parse(user.Corporates.DescendantsAndSelf("l").ToArray()[0].Attribute("i").Value);
                var ayandeh = Branches.FirstOrDefault(p => p.GhesmatkhedmatID == n);
                var result = ayandeh == null ? (int)(long.Parse(user.Corporates.DescendantsAndSelf("l").ToArray()[0].Attribute("i").Value) % 100000) : (int)ayandeh.Branch;
                return result;
            }
        }
        public static string CurrentUserBranchName
        {
            get
            {
                var user = CurrentUser;

                if (user == null || !user.HasPermission(UserPermission.FullAccess.ToString())) return "-";
                return ((user.Corporates.DescendantsAndSelf("l").ToArray()[0].Attribute("n").Value)).ToString(); ;
            }
        }
        public static int CurrentUserAreaCode
        {
            get
            {
                var user = CurrentUser;
                if (user == null || user.HasPermission(UserPermission.FullAccess.ToString())) return -1;
                try
                {
                    return (int)(long.Parse(user.Corporates.DescendantsAndSelf("l").ToArray()[2].Attribute("i").Value));
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }
        public static int CurrentUserZoneCode
        {
            get
            {
                var user = CurrentUser;
                if (user == null ||
                    (
                        !user.HasPermission(UserPermission.FullAccess.ToString())
                    )
                ) return -1;
                return (int)(long.Parse(user.Corporates.DescendantsAndSelf("l").ToArray()[2].Attribute("i").Value));
            }
        }
        public static string CurrentUserShowLabel
        {
            get
            {   
                string label = string.Empty;
                try
                {
                var user = CurrentUser;
                if (user == null) return label;
                
                    var role = CurrentUser.UserGroups.FirstOrDefault();
                    if (role != null)
                        label = role.Name;
                return string.Format("{0} - {1}", user.ShowName, label);

                }
                catch
                {
                    label = string.Empty;
                    return string.Format("{0}", label);
                }

            }
        }
        public static string BuildLineforString(string obj, int index, int maxlentgh, int length)
        {
            int count = 0;
            string result = "";
            if (obj != null)
            {
                count = (maxlentgh) - (obj.Length);
                count = (count < 0) ? -count : count;
                //var a = 250 - 5;
                obj += new string(' ', count);
                obj = obj.Substring(index, length - 1).Trim();
                result = (obj != "") ? obj : "-";
                return result;
            }
            else
            {
                return "-";
            }
        }
        
        public static string ReplaceValue(this string str, string key, string value, string defaultValue = "")
        {
            return str.Replace("#" + key + "#", string.IsNullOrEmpty(value) ? defaultValue : value);
        }
        /// <summary>
        /// this function change fromate 13931011 to 1393/10/11  the main function in sepam 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ChangeFormateTodate(string date)
        {

            var result = string.Empty;
            var newdate = date.Splitevery(2);
            int i = newdate.Count();
            var count = 0;
            while (i > 0)
            {
                if (count >= 2)
                {
                    result = newdate[i - 1] + result;
                    i--;
                    count++;
                }
                else
                    if (count < 2)
                    {
                        result = "/" + newdate[i - 1] + result;
                        i--;
                        count++;
                    }
            }
            return result;
        }
        private static List<string> Splitevery(this string s, int n)
        {
            var list = new List<string>();
            if (s != null)
            {
                var entity = s.Select((c, i) => i % n == 0 ? s.Substring(i, n) : "|").Where(x => x != "|").ToList();
                return entity;
            }
            else
            {
                return new List<string>();
            }
        }

        public static string GetnumberChars(string str)
        {
            string result = "";
	        int Count = str.Length;

	        while (Count >= 0)
            {
                int dig = 0;
                result += int.TryParse(str.Substring(@Count,1),out dig) ? dig.ToString() : "";
		        Count--;
            }
            return result;
        }
    }
}
