using System;
using System.Threading;
using UnityEngine;

namespace ETModel
{
	public class Init : MonoBehaviour
	{
		private void Start()
		{
			this.StartAsync().Coroutine();
		}
		
		private async ETVoid StartAsync()
		{
			try
			{
				SynchronizationContext.SetSynchronizationContext(OneThreadSynchronizationContext.Instance);

				DontDestroyOnLoad(gameObject);
				Game.EventSystem.Add(DLLType.Model, typeof(Init).Assembly);

				Game.Scene.AddComponent<TimerComponent>();
				Game.Scene.AddComponent<GlobalConfigComponent>();
				Game.Scene.AddComponent<NetOuterComponent>();
				Game.Scene.AddComponent<ResourcesComponent>();
				Game.Scene.AddComponent<PlayerComponent>();//考虑放hotfix层
				Game.Scene.AddComponent<UnitComponent>();//考虑放hotfix层
				Game.Scene.AddComponent<UIComponent>();
				Game.Scene.AddComponent<OpcodeTypeComponent>();
				Game.Scene.AddComponent<MessageDispatcherComponent>();


				Session session = Game.Scene.GetComponent<NetOuterComponent>().Create(GlobalConfigComponent.Instance.GlobalProto.Address);
				//await session.Call(); 依据返回消息 Game.EventSystem.Run(EventIdType.TestHotfixSubscribMonoEvent, "ToLoad/Notice"); 如果要更新则跳过加载资源到hotfixUI去显示要下载最新版
				session.Dispose();

				// 下载ab包
				await BundleHelper.DownloadBundle();

				Game.Hotfix.LoadHotfixAssembly();

				// 加载配置 中间与上下这3行同生共死可以不用如果现在不用，因为热更会再次加载
				//AddComponent<ConfigComponent> 将所有M的配置加载，需要先load相关资源，不然加载没找到文件会报错
				//目前"config.unity3d" 的资源在M没有用到，因为M没有类继承资源配置，所以ConfigComponent内容为空
				Game.Scene.GetComponent<ResourcesComponent>().LoadBundle("config.unity3d");
				Game.Scene.AddComponent<ConfigComponent>();
				Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle("config.unity3d");
				//Game.Scene.AddComponent<OpcodeTypeComponent>();
				//Game.Scene.AddComponent<MessageDispatcherComponent>();
				
				Game.Hotfix.GotoHotfix();

				Game.EventSystem.Run(EventIdType.TestHotfixSubscribMonoEvent, "TestHotfixSubscribMonoEvent");
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}

		private void Update()
		{
			OneThreadSynchronizationContext.Instance.Update();
			Game.Hotfix.Update?.Invoke();
			Game.EventSystem.Update();
		}

		private void LateUpdate()
		{
			Game.Hotfix.LateUpdate?.Invoke();
			Game.EventSystem.LateUpdate();
		}

		private void OnApplicationQuit()
		{
			Game.Hotfix.OnApplicationQuit?.Invoke();
			Game.Close();
		}
	}
}