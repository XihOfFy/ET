using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    public sealed class UserInfo: ComponentWithId
    {
        public string Account { get; set; }
        public string Password { get; set; }
    }
}
