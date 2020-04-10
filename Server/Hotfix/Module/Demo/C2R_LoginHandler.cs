using System;
using System.Net;
using ETModel;

namespace ETHotfix
{
	[MessageHandler(AppType.Realm)]
	public class C2R_LoginHandler : AMRpcHandler<C2R_Login, R2C_Login>
	{
		protected override async ETTask Run(Session session, C2R_Login request, R2C_Login response, Action reply)
		{
            //if (message.Account != "abcdef" || message.Password != "111111")
            //{
            //	response.Error = ErrorCode.ERR_AccountOrPasswordError;
            //	reply(response);
            //	return;
            //}

            // await DBProxyComponentEx.Query<UserInfo>(Game.Scene.GetComponent<DBProxyComponent>(),(user)=>user.Account== request.Account&& user.Password==request.Password);
            await Game.Scene.GetComponent<DBProxyComponent>().Save(new UserInfo() { Account = request.Account,Password="111"});
            await Game.Scene.GetComponent<DBProxyComponent>().Query<UserInfo>((user) => user.Account == request.Account && user.Password == request.Password);
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