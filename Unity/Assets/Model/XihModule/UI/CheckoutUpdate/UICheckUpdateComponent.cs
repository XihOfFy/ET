using System;
using UnityEngine;
using UnityEngine.UI;

namespace ETModel
{
	[ObjectSystem]
	public class UICheckUpdateAwakeSystem : AwakeSystem<UICheckUpdateComponent>
	{
		public override void Awake(UICheckUpdateComponent self)
		{
			self.Awake();
		}
	}

	[ObjectSystem]
	public class UICheckUpdateStartSystem : StartSystem<UICheckUpdateComponent>
	{
		public override void Start(UICheckUpdateComponent self)
		{
			StartAsync(self).Coroutine();
		}

		public async ETVoid StartAsync(UICheckUpdateComponent self)
		{
			// 创建一个ETModel层的Session
			R2C_CheckUpdate isUpdate = null;
			try {
				using (Session session = Game.Scene.GetComponent<NetOuterComponent>().Create(GlobalConfigComponent.Instance.GlobalProto.Address))
				isUpdate = (R2C_CheckUpdate)await session.Call(new C2R_CheckUpdate() { Version = GlobalConfigComponent.Instance.GlobalProto.Version }); //依据返回消息 Game.EventSystem.Run(EventIdType.TestHotfixSubscribMonoEvent, "ToLoad/Notice"); 如果要更新则跳过加载资源到hotfixUI去显示要下载最新版
			}
			catch{
				self.ui.SetActive(true);
				self.urlObj.SetActive(false);
				self.notice.text = "网络错误，是否重新尝试";
				self.exitOrRetry.onClick.RemoveAllListeners();
				self.exitOrRetry.GetComponentInChildren<Text>().text = "重新尝试";
				self.exitOrRetry.onClick.Add(() => { StartAsync(self).Coroutine(); });
				return;
			}
			
			if (!string.IsNullOrEmpty(isUpdate.Message))
			{
				self.exitOrRetry.onClick.RemoveAllListeners();
				self.exitOrRetry.GetComponentInChildren<Text>().text = "退出游戏";
				self.exitOrRetry.onClick.Add(() => { Application.Quit(); });
				self.ui.SetActive(true);
				self.notice.text = isUpdate.Message;
				if (!string.IsNullOrEmpty(isUpdate.Url))
				{
					self.urlObj.SetActive(true);
					self.url.text = isUpdate.Url;
				}
				else
				{
					self.urlObj.SetActive(false);
				}
				return;
			}
			RetryDownBundle();
			async void RetryDownBundle() {
				try
				{
					// 下载ab包
					await BundleHelper.DownloadBundle();
					Game.Hotfix.LoadHotfixAssembly();
					/*// 加载配置 中间与上下这3行同生共死可以不用如果现在不用，因为热更会再次加载
					//AddComponent<ConfigComponent> 将所有M的配置加载，需要先load相关资源，不然加载没找到文件会报错
					//目前"config.unity3d" 的资源在M没有用到，因为M没有类继承资源配置，所以ConfigComponent内容为空
					Game.Scene.GetComponent<ResourcesComponent>().LoadBundle("config.unity3d");
					Game.Scene.AddComponent<ConfigComponent>();
					Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle("config.unity3d");
					*/
					Game.Hotfix.GotoHotfix();
					//Game.EventSystem.Run(EventIdType.TestHotfixSubscribMonoEvent, "TestHotfixSubscribMonoEvent");
					//卸载自己
					Game.EventSystem.Run(EventIdType.EndCheckUpdate);
				}
				catch (Exception e)
				{
					Log.Warning(e.ToString());
					self.ui.SetActive(true);
					self.urlObj.SetActive(false);
					self.notice.text = "资源下载失败，是否重新尝试";
					self.exitOrRetry.onClick.RemoveAllListeners();
					self.exitOrRetry.GetComponentInChildren<Text>().text = "重新尝试";
					self.exitOrRetry.onClick.Add(() => { RetryDownBundle(); });
				}
			}
		}
	}

	public class UICheckUpdateComponent : Component
	{
		public Text notice;
		public Text url;
		private Button copy;
		public Button exitOrRetry;
		public GameObject ui;
		public GameObject urlObj;
		public void Awake()
		{
			ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

			ui = rc.Get<GameObject>("UICheckUpdate");
			notice = rc.Get<GameObject>("Notice").GetComponent<Text>();
			urlObj = rc.Get<GameObject>("Url");
			url = urlObj.GetComponent<Text>();
			copy = rc.Get<GameObject>("Copy").GetComponent<Button>();
			exitOrRetry = rc.Get<GameObject>("QuitOrRetry").GetComponent<Button>();
			copy.onClick.Add(CopyUrl);
			ui.SetActive(false);
		}

		private void CopyUrl()
		{
			TextEditor te = new TextEditor
			{
				text = url.text
			};
			te.SelectAll();
			te.Copy();
		}
	}
}