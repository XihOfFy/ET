using ETModel;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
	[ObjectSystem]
	public class UiLobbyComponentSystem : AwakeSystem<UILobbyComponent>
	{
		public override void Awake(UILobbyComponent self)
		{
			self.Awake();

		}
	}
	[ObjectSystem]
	public class UiLobbyComponentDestorySystem : DestroySystem<UILobbyComponent>
	{
        public override void Destroy(UILobbyComponent self)
        {
            self.Destory();
        }
	}

	public class UILobbyComponent : Component
	{
		private GameObject enterMap;
		private Text chatMsgs;
		private InputField sendMsg;
		private Button send;
        public static Action<string, ChatInfos> ChatEvent;

        private void ShowChatMsg(string channel, ChatInfos infos)
        {
            switch (channel) {
                case "world":
                    List<ChatMsg> info = infos.Chat;
                    if (info.Count > 50) info.RemoveRange(0, info.Count - 50);
                    StringBuilder sb = new StringBuilder();
                    foreach (ChatMsg s in info)
                    {
                        sb.AppendLine($"{s.Name}:{s.Msg}");
                    }
                    chatMsgs.text = sb.ToString();
                    break;
                default:break;
            }
        }
        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            enterMap = rc.Get<GameObject>("EnterMap");
			enterMap.GetComponent<Button>().onClick.Add(this.EnterMap);
			this.chatMsgs = rc.Get<GameObject>("ChatInfo").GetComponent<Text>();
			this.sendMsg = rc.Get<GameObject>("ChatMsg").GetComponent<InputField>();
			this.send = rc.Get<GameObject>("Send").GetComponent<Button>();
			this.send.onClick.Add(SendMsg);
            ChatEvent += ShowChatMsg;
        }
        public void Destory() {
            ChatEvent -= ShowChatMsg;
        }
        private void SendMsg() {
			SessionComponent.Instance.Session.Send(new ChatMsg() { Name = Game.MyUser.Name, Msg = sendMsg.text });
		}
		private void EnterMap()
		{
			MapHelper.EnterMapAsync().Coroutine();
		}
	}
}
