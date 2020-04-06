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
        public void Awake(string info)
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            this.tipInfo = rc.Get<GameObject>("Tips").GetComponent<Text>();
            tipInfo.text = info;
            StartAsync().Coroutine();
        }
        public async ETVoid StartAsync()
        {
            TimerComponent timerComponent = ETModel.Game.Scene.GetComponent<TimerComponent>();
            int time = 0;
            float x = tipInfo.rectTransform.localPosition.x;
            float z = tipInfo.rectTransform.localPosition.z;
            float py = tipInfo.rectTransform.localPosition.y;
            float delta = py / 32.0f;
            while (time < 2000){
                await timerComponent.WaitAsync(10);
                time += 10;
                tipInfo.rectTransform.localPosition= new Vector3(x, py,z);
                py = py - delta;
            }
            Game.EventSystem.Run(EventIdType.DestoryTip,this.Parent);
        }
    }
}