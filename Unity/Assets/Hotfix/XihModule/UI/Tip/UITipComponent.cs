using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using ETModel;

namespace ETHotfix {
    [ObjectSystem]
    public class UITipComponentAwakeSystem : AwakeSystem<UITipComponent,string>
    {
        public override void Awake(UITipComponent self, string info)
        {
            self.Awake(info);
        }
    }
    public class UITipComponent : Component
    {
        Text tipInfo;
        RectTransform bg;
        public void Awake(string info)
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            this.tipInfo = rc.Get<GameObject>("Tips").GetComponent<Text>();
            this.bg = rc.Get<GameObject>("Bg").GetComponent<RectTransform>();
            tipInfo.text = info;
            StartAsync().Coroutine();
        }
        public async ETVoid StartAsync()
        {
            TimerComponent timerComponent = ETModel.Game.Scene.GetComponent<TimerComponent>();
            int time = 0;
            float x = bg.localPosition.x;
            float z = bg.localPosition.z;
            float py = bg.localPosition.y;
            float delta = py / 32.0f;
            while (time < 2000){
                await timerComponent.WaitAsync(10);
                time += 10;
                bg.localPosition= new Vector3(x, py,z);
                py = py - delta;
            }
            Game.EventSystem.Run(EventIdType.DestoryTip,this.Parent);
        }
    }
}