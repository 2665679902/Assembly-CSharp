using System;
using UnityEngine;

// Token: 0x0200009F RID: 159
public class GarbageCollectionProfiler : MonoBehaviour
{
	// Token: 0x0600061F RID: 1567 RVA: 0x0001C18C File Offset: 0x0001A38C
	private void Update()
	{
		if (this._Items == null || this._Items.Length != this._ObjectCount)
		{
			this._Items = new GarbageCollectionProfiler.Test[this._ObjectCount];
			for (int i = 0; i < this._ObjectCount; i++)
			{
				this._Items[i] = new GarbageCollectionProfiler.DelegateWithSingleHandler();
			}
		}
		GC.Collect();
	}

	// Token: 0x0400059A RID: 1434
	public int _ObjectCount = 100000;

	// Token: 0x0400059B RID: 1435
	private GarbageCollectionProfiler.Test[] _Items;

	// Token: 0x020009DD RID: 2525
	private class Test
	{
	}

	// Token: 0x020009DE RID: 2526
	private class StringTest : GarbageCollectionProfiler.Test
	{
		// Token: 0x0400221C RID: 8732
		private string _String;
	}

	// Token: 0x020009DF RID: 2527
	private class ObjectTest : GarbageCollectionProfiler.Test
	{
		// Token: 0x0400221D RID: 8733
		private object _Object;
	}

	// Token: 0x020009E0 RID: 2528
	private class DelegateTest : GarbageCollectionProfiler.Test
	{
		// Token: 0x0400221E RID: 8734
		private System.Action _Delegate;
	}

	// Token: 0x020009E1 RID: 2529
	private class DelegateWithSingleHandler : GarbageCollectionProfiler.Test
	{
		// Token: 0x060053AC RID: 21420 RVA: 0x0009C29F File Offset: 0x0009A49F
		public DelegateWithSingleHandler()
		{
			this._Delegate = (System.Action)Delegate.Combine(this._Delegate, new System.Action(this.DoNothing));
		}

		// Token: 0x060053AD RID: 21421 RVA: 0x0009C2C9 File Offset: 0x0009A4C9
		private void DoNothing()
		{
		}

		// Token: 0x0400221F RID: 8735
		private System.Action _Delegate;
	}
}
