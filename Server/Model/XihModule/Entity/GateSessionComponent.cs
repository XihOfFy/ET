using System.Collections.Generic;
using System.Net;

namespace ETModel
{
	[ObjectSystem]
	public class GateSessionComponentAwakeSystem : AwakeSystem<GateSessionComponent>
	{
		public override void Awake(GateSessionComponent self)
		{
			self.Awake();
		}
	}
	public class GateSessionComponent : Component
	{
		public static GateSessionComponent Instance;

		private HashSet<Session> gateClientSessions=new HashSet<Session>();
		//private Dictionary<long, Session> gateSessions=new Dictionary<long, Session>();
		private int count = 0;
		private const int MaxCount = 10000;
		public HashSet<Session> GetAllClientSession() {
			if (count > MaxCount) {
				List<Session> ls = new List<Session>();
				foreach (Session s in gateClientSessions)
				{
					if (s.IsDisposed) ls.Add(s);
				}
				foreach (Session s in ls)
				{
					gateClientSessions.Remove(s);
				}
				count = 0;
			}
			count++;
			return gateClientSessions;
		}
		public void AddClientSession(Session s) {
			if (!gateClientSessions.Contains(s))
				gateClientSessions.Add(s);
		}
		/*public Dictionary<long, Session> GetAllSessions() {
			if (count > MaxCount) {
				List<long> ls = new List<long>();
				foreach (KeyValuePair<long, Session> s in gateSessions)
				{
					if (s.Value.IsDisposed) ls.Add(s.Key);
				}
				foreach (long s in ls)
				{
					gateSessions.Remove(s);
				}
				count = 0;
			}
			count++;
			return gateSessions;
		}
		public void AddSession(long sid, Session s) {
			if (!gateSessions.ContainsKey(sid))
				gateSessions.Add(sid, s);
			else
				gateSessions[sid] =s;
		}*/

		public void Awake()
		{
			Instance = this;
		}
		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			base.Dispose();
			//gateSessions.Clear();
			gateClientSessions.Clear();
			Instance = null;
		}
	}
}
