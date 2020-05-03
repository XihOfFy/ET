using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETHotfix
{
    [MessageHandler]
	public class ChatMsg_Handler : AMHandler<ChatMsg>
	{
		protected async override ETTask Run(ETModel.Session session, ChatMsg message)
		{
			ChatInfos cs = Game.Scene.GetComponent<ChatComponent>().GetChatInfo("world");
			cs.Chat.Add(new ChatMsg() { Name= message.Name,Msg= message.Msg});
			await ETTask.CompletedTask;
		}
	}
}