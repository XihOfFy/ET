using ETModel;
using System.Collections.Generic;
using System.Linq;

namespace ETHotfix
{
   public class ChatHelper
    {
        public static void SendMsg(ChatMsg message) {
            //List<Session> ls = GateSessionComponent.Instance.GetAllClientSession().Values.ToList();
            /*Dictionary<long, Session> ls = GateSessionComponent.Instance.GetAllSessions();
            foreach (KeyValuePair<long, Session> s in ls)
            {
                if (s.Value.IsDisposed) continue;
                s.Value.Send(new G2C_TestHotfixMessage() {Info = $"{s.Key}_{ls.Count}:{msg}-->{s.Value.InstanceId}:{s.Value}"});
            }*/
            foreach (Session s in GateSessionComponent.Instance.GetAllClientSession())
            {
                if (s.IsDisposed) continue;
                s.Send(message);
            }
        }
    }
}
