using UnityEngine;
using UnityEditor;
using ETModel;
using System;

namespace ETHotfix
{
    public static class UITipFactory
    {
        public static UI Create(bool isCodeTranslate, string info, int code)
        {
            try
            {
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                resourcesComponent.LoadBundle(UIType.UITip.StringToAB());
                GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset(UIType.UITip.StringToAB(), UIType.UITip);
                GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);

                UI ui = ComponentFactory.Create<UI, string, GameObject>(UIType.UITip, gameObject, false);

                ui.AddComponent<UITipComponent, bool, string, int>(isCodeTranslate,info,code);
                return ui;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }
    }
}