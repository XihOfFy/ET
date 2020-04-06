﻿using System;
using System.Net;
using ETModel;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace ETHotfix
{
	[ObjectSystem]
	public class UiLoginComponentSystem : AwakeSystem<UILoginComponent>
	{
		public override void Awake(UILoginComponent self)
		{
			self.Awake();
		}
	}
	
	public class UILoginComponent: Component
	{
		private GameObject account;
		private GameObject loginBtn;

		public void Awake()
		{
			ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
			loginBtn = rc.Get<GameObject>("LoginBtn");
			loginBtn.GetComponent<Button>().onClick.Add(OnLogin);
			this.account = rc.Get<GameObject>("Account");
		}

		public void OnLogin()
		{
            Game.EventSystem.Run(EventIdType.ShowTip, "tips"+Random.Range(1000,9999));
            //LoginHelper.OnLoginAsync(this.account.GetComponent<InputField>().text).Coroutine();
		}
	}
}
