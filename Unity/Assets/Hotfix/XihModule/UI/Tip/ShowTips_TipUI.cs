using UnityEngine;
using UnityEditor;
using ETModel;

namespace ETHotfix {
    [Event(EventIdType.ShowTip)]
    public class ShowTips_TipUI : AEvent<bool, string, int>
    {
        public override void Run(bool isCodeTranslate, string info, int code)
        {
            UI ui = UITipFactory.Create(isCodeTranslate, info, code);
            //if (!Game.Scene.GetComponent<UIComponent>().uis.ContainsKey(ui.Name))
            //     Game.Scene.GetComponent<UIComponent>().Add(ui);
        }
    }
}