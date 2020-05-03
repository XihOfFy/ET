using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETHotfix
{
    [ObjectSystem]
	public class ChatComponentSystem : AwakeSystem<ChatComponent>
	{
		public override void Awake(ChatComponent self)
		{
			self.Awake();
		}
	}

	public class ChatComponent : Component
	{
		public Dictionary<string, ChatInfos> channelChat=new Dictionary<string, ChatInfos>();
		public void Awake()
		{
		}
		public void AddChannel(string channel, ChatInfos info) {
			if (channelChat.ContainsKey(channel))
				channelChat[channel] = info;
			else
				channelChat.Add(channel, info);
		}
		public ChatInfos GetChatInfo(string channel) {
			if (channelChat.ContainsKey(channel))
				return channelChat[channel];
			else
				return new ChatInfos();
		}
	}
}
