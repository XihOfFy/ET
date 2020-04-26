using System;
using System.Collections.Generic;
using System.Net;
using ETModel;

namespace ETHotfix
{
	[MessageHandler(AppType.Realm)]
	public class C2R_LoginHandler : AMRpcHandler<C2R_Login, R2C_Login>
	{
		protected override async ETTask Run(Session session, C2R_Login request, R2C_Login response, Action reply)
		{
            if (string.IsNullOrEmpty(request.Account) || string.IsNullOrEmpty(request.Password)) {
                response.Error = ErrorCode.ERRORCODE_3;
                reply();
                return;
            }
            response.Error = ErrorCode.CODE1;
#if DB
            List<ComponentWithId> infos = await Game.Scene.GetComponent<DBProxyComponent>().Query<UserInfo>((user) => user.Account == request.Account && user.Password == request.Password);
            if (infos.Count <= 0)
            {
                response.Error = ErrorCode.CODE0; //0 新账号 1 登录成功 >1 查询到多个匹配
                await Game.Scene.GetComponent<DBProxyComponent>().Save(new UserInfo() { Account = request.Account, Password = request.Password });
            }
            else if (infos.Count == 1) {
                response.Error = ErrorCode.CODE1; //0 新账号 1 登录成功 >1 查询到多个匹配
            }
            else
            {
                response.Error = ErrorCode.ERRORCODE_2; //0 新账号 1 登录成功 -2 查询到多个匹配
                reply();
                return;
            }
#endif
            // 随机分配一个Gate
            StartConfig config = Game.Scene.GetComponent<RealmGateAddressComponent>().GetAddress();
			//Log.Debug($"gate address: {MongoHelper.ToJson(config)}");
			IPEndPoint innerAddress = config.GetComponent<InnerConfig>().IPEndPoint;
			Session gateSession = Game.Scene.GetComponent<NetInnerComponent>().Get(innerAddress);

			// 向gate请求一个key,客户端可以拿着这个key连接gate
			G2R_GetLoginKey g2RGetLoginKey = (G2R_GetLoginKey)await gateSession.Call(new R2G_GetLoginKey() {Account = request.Account});

			string outerAddress = config.GetComponent<OuterConfig>().Address2;

			response.Address = outerAddress;
			response.Key = g2RGetLoginKey.Key;
			reply();
		}
	}
}