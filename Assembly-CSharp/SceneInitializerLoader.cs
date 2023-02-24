using System;
using UnityEngine;

// Token: 0x02000902 RID: 2306
public class SceneInitializerLoader : MonoBehaviour
{
	// Token: 0x06004314 RID: 17172 RVA: 0x0017B384 File Offset: 0x00179584
	private void Awake()
	{
		Camera[] array = UnityEngine.Object.FindObjectsOfType<Camera>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = false;
		}
		KMonoBehaviour.isLoadingScene = false;
		Singleton<StateMachineManager>.Instance.Clear();
		Util.KInstantiate(this.sceneInitializer, null, null);
		if (SceneInitializerLoader.ReportDeferredError != null && SceneInitializerLoader.deferred_error.IsValid)
		{
			SceneInitializerLoader.ReportDeferredError(SceneInitializerLoader.deferred_error);
			SceneInitializerLoader.deferred_error = default(SceneInitializerLoader.DeferredError);
		}
	}

	// Token: 0x04002CB8 RID: 11448
	public SceneInitializer sceneInitializer;

	// Token: 0x04002CB9 RID: 11449
	public static SceneInitializerLoader.DeferredError deferred_error;

	// Token: 0x04002CBA RID: 11450
	public static SceneInitializerLoader.DeferredErrorDelegate ReportDeferredError;

	// Token: 0x020016E4 RID: 5860
	public struct DeferredError
	{
		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06008901 RID: 35073 RVA: 0x002F7823 File Offset: 0x002F5A23
		public bool IsValid
		{
			get
			{
				return !string.IsNullOrEmpty(this.msg);
			}
		}

		// Token: 0x04006B50 RID: 27472
		public string msg;

		// Token: 0x04006B51 RID: 27473
		public string stack_trace;
	}

	// Token: 0x020016E5 RID: 5861
	// (Invoke) Token: 0x06008903 RID: 35075
	public delegate void DeferredErrorDelegate(SceneInitializerLoader.DeferredError deferred_error);
}
