using System;
using ETModel;

namespace ETHotfix
{
	[MessageHandler(AppType.Gate)]
	public class C2G_LoginGateHandler : AMRpcHandler<C2G_LoginGate, G2C_LoginGate>
	{
		protected override async ETTask Run(Session session, C2G_LoginGate request, G2C_LoginGate response, Action reply)
		{
			string account = Game.Scene.GetComponent<GateSessionKeyComponent>().Get(request.Key);
			if (string.IsNullOrEmpty(account))
			{
				response.Error = ErrorCode.ERR_ConnectGateKeyError;
				response.Message = "Gate key验证失败!";
				reply();
				return;
			}
			response.Error = ErrorCode.CODE0;
			GateSessionComponent.Instance.AddClientSession(session);
			Player player = ComponentFactory.Create<Player, string>(account);
			Game.Scene.GetComponent<PlayerComponent>().Add(player);
			session.AddComponent<SessionPlayerComponent>().Player = player;
			session.AddComponent<MailBoxComponent, string>(MailboxType.GateSession);

			response.PlayerId = player.Id;
			reply();
			ChatHelper.SendMsg(session.Id.ToString(),$"let us chat!");
			//session.Send(new G2C_TestHotfixMessage() { Info = "recv hotfix message success" });
			await ETTask.CompletedTask;
		}
	}
}