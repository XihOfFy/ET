using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using ETModel;

namespace ETHotfix {
    [ObjectSystem]
    public class UITipComponentAwakeSystem : AwakeSystem<UITipComponent,bool,string,int>
    {
        public override void Awake(UITipComponent self, bool a, string b, int c)
        {
            self.Awake(a,b,c);
        }
    }
    public class UITipComponent : Component
    {
        Text tipInfo;
        RectTransform bg;
        public void Awake(bool isCodeTranslate, string info, int code)
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            this.tipInfo = rc.Get<GameObject>("Tips").GetComponent<Text>();
            this.bg = rc.Get<GameObject>("Bg").GetComponent<RectTransform>();
            if(isCodeTranslate)
                tipInfo.text = CodeExplain.GetExplain(code);
            else
                tipInfo.text = info;
            StartAsync().Coroutine();
        }
        public async ETVoid StartAsync()
        {
            TimerComponent timerComponent = ETModel.Game.Scene.GetComponent<TimerComponent>();
            int time = 0;
            float x = bg.position.x;
            float z = bg.position.z;
            float py = bg.position.y;
            float delta = py / 128.0f;
            while (time < 1000){
                await timerComponent.WaitAsync(10);
                time += 10;
                if (null == bg) break;
                bg.position = new Vector3(x, py,z);
                py = py + delta;
            }
            Game.EventSystem.Run(EventIdType.DestoryTip,this.Parent);
        }
    }
}