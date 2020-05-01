using ETModel;
using System.Collections.Generic;
using System.Linq;

namespace ETHotfix
{
   public class ChatHelper
    {
        public static void SendMsg(string name,string msg="") {
            //List<Session> ls = GateSessionComponent.Instance.GetAllClientSession().Values.ToList();
            Dictionary<long, Session> ls = GateSessionComponent.Instance.GetAllClientSession();
            foreach (KeyValuePair<long,Session> s in ls)
            {
                if (s.Value.IsDisposed) continue;
                s.Value.Send(new G2C_TestHotfixMessage() {ActorId=s.Value.Id, Info = $"{name}_{ls.Count}:-->{s.Key}"});
            }
        }
    }
}
