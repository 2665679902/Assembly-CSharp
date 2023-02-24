using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x0200010E RID: 270
[AddComponentMenu("KMonoBehaviour/Plugins/Traces")]
public class Traces : KMonoBehaviour
{
	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x0600092B RID: 2347 RVA: 0x0002481F File Offset: 0x00022A1F
	// (set) Token: 0x0600092C RID: 2348 RVA: 0x00024826 File Offset: 0x00022A26
	public static Traces Instance { get; private set; }

	// Token: 0x0600092D RID: 2349 RVA: 0x0002482E File Offset: 0x00022A2E
	public static void DestroyInstance()
	{
		Traces.Instance = null;
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x00024836 File Offset: 0x00022A36
	protected override void OnPrefabInit()
	{
		Traces.Instance = this;
	}

	// Token: 0x0600092F RID: 2351 RVA: 0x0002483E File Offset: 0x00022A3E
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Traces.Instance = null;
	}

	// Token: 0x06000930 RID: 2352 RVA: 0x0002484C File Offset: 0x00022A4C
	public void TraceDestroy(GameObject go, StackTrace stack_trace)
	{
		if (this.DestroyTraces.Count > 99)
		{
			this.DestroyTraces.RemoveAt(0);
		}
		Traces.Entry entry = new Traces.Entry
		{
			Name = string.Concat(new string[]
			{
				Time.frameCount.ToString(),
				" ",
				go.name,
				" [",
				go.GetInstanceID().ToString(),
				"]"
			}),
			StackTrace = stack_trace
		};
		this.DestroyTraces.Add(entry);
	}

	// Token: 0x04000685 RID: 1669
	public List<Traces.Entry> DestroyTraces = new List<Traces.Entry>();

	// Token: 0x02000A07 RID: 2567
	[Serializable]
	public class Entry
	{
		// Token: 0x0400225D RID: 8797
		public string Name;

		// Token: 0x0400225E RID: 8798
		public StackTrace StackTrace;

		// Token: 0x0400225F RID: 8799
		public bool Foldout;
	}
}
