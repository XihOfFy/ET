using UnityEngine;
using UnityEditor;
using ETModel;
using System.Collections.Generic;

namespace ETHotfix {
    public class CodeExplain
    {
        private static Dictionary<int, string> map = new Dictionary<int, string>();
        public const string CODE0 = "0 注册成功";
        public const string CODE1 = "1 登录成功";
        public const string ERRORCODE_1 = "-1 账号或密码错误";
        public const string ERRORCODE_2 = "-2 无法多账号登录";
        public const string ERRORCODE_3 = "-3 账号或密码不能为空";
        public const string ERRORCODE_99 = "-99 失败";
        public const string ERRORCODE_999 = "-999 网络错误";
        /// <summary>
        /// 以后放配置文件中
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetExplain(int code) {
            if (map.ContainsKey(code)) return map[code];
            string res= string.Empty;
            switch (code) {
                case ErrorCode.CODE0:
                    res= CODE0;
                    break;
                case ErrorCode.CODE1:
                    res = CODE1;
                    break;
                case ErrorCode.ERRORCODE_1:
                    res = ERRORCODE_1;
                    break;
                case ErrorCode.ERRORCODE_2:
                    res = ERRORCODE_2;
                    break;
                case ErrorCode.ERRORCODE_3:
                    res = ERRORCODE_3;
                    break;
                case ErrorCode.ERRORCODE_99:
                    res = ERRORCODE_99;
                    break;
                case ErrorCode.ERRORCODE_999:
                    res = ERRORCODE_999;
                    break;
                case ErrorCode.ERR_MyErrorCode://待补充
                    break;
                default:
                    break;
            }
            map.Add(code, res);
            return res;
        }
    }
}