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

		private Dictionary<long, Session> gateClientSessions=new Dictionary<long, Session>();
		private int count = 0;
		private const int MaxCount = 10000;
		public Dictionary<long, Session> GetAllClientSession() {
			if (count > MaxCount) {
				List<long> ls = new List<long>();
				foreach (long s in gateClientSessions.Keys)
				{
					if (gateClientSessions[s].IsDisposed) ls.Add(s);
				}
				foreach (long s in ls)
				{
					gateClientSessions.Remove(s);
				}
				count = 0;
			}
			count++;
			return gateClientSessions;
		}
		public void AddClientSession(Session s) {
			if (gateClientSessions.ContainsKey(s.InstanceId))
				gateClientSessions[s.InstanceId] = s;
			else
				gateClientSessions.Add(s.InstanceId, s);
		}

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
			gateClientSessions.Clear();
			Instance = null;
		}
	}
}
