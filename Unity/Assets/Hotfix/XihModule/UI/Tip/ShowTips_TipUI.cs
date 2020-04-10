using UnityEngine;
using UnityEditor;
using ETModel;

namespace ETHotfix {
    [Event(EventIdType.ShowTip)]
    public class ShowTips_TipUI : AEvent<string>
    {
        public override void Run(string a)
        {
            UI ui = UITipFactory.Create(a);
            //if (!Game.Scene.GetComponent<UIComponent>().uis.ContainsKey(ui.Name))
            //     Game.Scene.GetComponent<UIComponent>().Add(ui);
        }
    }
}