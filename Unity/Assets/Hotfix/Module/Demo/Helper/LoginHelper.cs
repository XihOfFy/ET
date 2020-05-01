using System;
using ETModel;

namespace ETHotfix
{
    public static class LoginHelper
    {
        public static async ETVoid OnLoginAsync(string account,string password )
        {
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password)) {
                Game.EventSystem.Run(EventIdType.ShowTip, CodeExplain.GetExplain(ErrorCode.ERRORCODE_3));
                return;
            }
            try
            {
                // 创建一个ETModel层的Session
                ETModel.Session session = ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(GlobalConfigComponent.Instance.GlobalProto.Address);
                // 创建一个ETHotfix层的Session, ETHotfix的Session会通过ETModel层的Session发送消息
                Session realmSession = ComponentFactory.Create<Session, ETModel.Session>(session);
                R2C_Login r2CLogin = (R2C_Login) await realmSession.Call(new C2R_Login() { Account = account, Password = password });
                realmSession.Dispose();
                switch (r2CLogin.Error) {
                    case ErrorCode.CODE0:
                    case ErrorCode.CODE1:
                        //创建一个ETModel层的Session,并且保存到ETModel.SessionComponent中
                        ETModel.Session gateSession = ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(r2CLogin.Address);
                        ETModel.Game.Scene.AddComponent<ETModel.SessionComponent>().Session = gateSession;
                        // 创建一个ETHotfix层的Session, 并且保存到ETHotfix.SessionComponent中
                        Game.Scene.AddComponent<SessionComponent>().Session = ComponentFactory.Create<Session, ETModel.Session>(gateSession);
                        G2C_LoginGate g2CLoginGate = (G2C_LoginGate)await SessionComponent.Instance.Session.Call(new C2G_LoginGate() { Key = r2CLogin.Key });
                        switch (g2CLoginGate.Error) {
                            case ErrorCode.CODE0:
                                Game.EventSystem.Run(EventIdType.ShowTip, CodeExplain.GetExplain(r2CLogin.Error));
                                //TODO 完成玩家数据拉取
                                Game.MyUser =new User(g2CLoginGate.PlayerId.ToString());
                                Log.Debug("TODO 完成玩家数据拉取");
                                // 创建Player
                                Player player = ETModel.ComponentFactory.CreateWithId<Player>(g2CLoginGate.PlayerId);
                                PlayerComponent playerComponent = ETModel.Game.Scene.GetComponent<PlayerComponent>();
                                playerComponent.MyPlayer = player;
                                Game.EventSystem.Run(EventIdType.LoginFinish);
                                // 测试消息有成员是class类型
                                //G2C_PlayerInfo g2CPlayerInfo = (G2C_PlayerInfo)await SessionComponent.Instance.Session.Call(new C2G_PlayerInfo());
                                break;
                            default:
                                break;
                        }
                        break;
                    default:break;
                }
            }
            catch (Exception e)
            {
                Game.EventSystem.Run(EventIdType.ShowTip, CodeExplain.GetExplain(ErrorCode.ERRORCODE_999));
                Log.Error(e);
            }
        } 
    }
}