using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class HandlerC2R_CheckUpdate : AMRpcHandler<C2R_CheckUpdate, R2C_CheckUpdate>
    {
        protected override async ETTask Run(Session session, C2R_CheckUpdate request, R2C_CheckUpdate response, Action reply)
        {
            if (request.Version < 2)
            {
                // 随机分配一个Gate
                response.Message = "本次更新祭天程序员为：老王";
                response.Url = "https://www.download.com";
            }
            reply();
            await ETTask.CompletedTask;
        }
    }
}
