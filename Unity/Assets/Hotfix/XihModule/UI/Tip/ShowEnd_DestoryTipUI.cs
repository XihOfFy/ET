using UnityEngine;
using UnityEditor;
using ETModel;

namespace ETHotfix
{
    [Event(EventIdType.DestoryTip)]
    public class ShowEnd_DestoryTipUI : AEvent<UI>
    {
        public override void Run(UI ui)
        {
            //if (Game.Scene.GetComponent<UIComponent>().uis.ContainsKey(ui.Name))
            //    Game.Scene.GetComponent<UIComponent>().Remove(ui.Name);
            //else
                ui.Dispose();
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.UITip.StringToAB());
        }
    }
}