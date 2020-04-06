using UnityEngine;
using UnityEditor;
using ETModel;
using System;

namespace ETHotfix
{
    public static class UITipFactory
    {
        public static UI Create(string info)
        {
            try
            {
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                resourcesComponent.LoadBundle(UIType.UITip.StringToAB());
                GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset(UIType.UITip.StringToAB(), UIType.UITip);
                GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);

                UI ui = ComponentFactory.Create<UI, string, GameObject>(UIType.UITip, gameObject, false);

                ui.AddComponent<UITipComponent, string>(info);
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