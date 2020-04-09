using UnityEngine;
using UnityEditor;
using ETModel;
namespace ETHotfix {
    public class CodeExplain
    {
        public const string CODE0 = "0 成功";
        public const string ERRORCODE_1 = "-1 账号或密码错误";
        public const string ERRORCODE_99 = "-99 失败";

        public static string GetExplain(int code) {
            switch (code) {
                case 0:
                    return CODE0;
                case -1:
                    return ERRORCODE_1;
                case -99:
                    return ERRORCODE_99;
                case ErrorCode.ERR_MyErrorCode://待补充
                    break;
                default:
                    break;
            }
            return string.Empty;
        }
    }
}