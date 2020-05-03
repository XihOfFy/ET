using ETModel;
using System;
namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class ChatMsg_Handler : AMHandler<ChatMsg>
    {
        protected async override ETTask Run(Session session, ChatMsg message)
        {
            ChatHelper.SendMsg(message);
            await ETTask.CompletedTask;
        }
    }
}
