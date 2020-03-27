using System;
using UnityEngine;

namespace ETModel
{
    [Event(EventIdType.BeginCheckUpdate)]
    public class EventBenginCheckUpdate : AEvent
    {
        public override void Run()
        {
			try
			{
				GameObject bundleGameObject = ((GameObject)ResourcesHelper.Load("KV")).Get<GameObject>(UIType.UICheckUpdate);
				GameObject go = UnityEngine.Object.Instantiate(bundleGameObject);
				go.layer = LayerMask.NameToLayer(LayerNames.UI);
				UI ui = ComponentFactory.Create<UI, string, GameObject>(UIType.UICheckUpdate, go, false);
				ui.AddComponent<UICheckUpdateComponent>();
				Game.Scene.GetComponent<UIComponent>().Add(ui);
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
        }
    }
}
