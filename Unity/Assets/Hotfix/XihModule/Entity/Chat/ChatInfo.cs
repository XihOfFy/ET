using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETHotfix
{
    public class ChatInfos : Entity
    {
        public List<ChatMsg> Chat { get; set; }
        public ChatInfos()
        {
            Chat = new List<ChatMsg>();
        }
    }
}