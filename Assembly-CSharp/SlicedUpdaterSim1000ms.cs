using System;
using System.Collections.Generic;
using KSerialization;

// Token: 0x02000925 RID: 2341
public abstract class SlicedUpdaterSim1000ms<T> : KMonoBehaviour, ISim200ms where T : KMonoBehaviour, ISlicedSim1000ms
{
	// Token: 0x06004463 RID: 17507 RVA: 0x00182397 File Offset: 0x00180597
	protected override void OnPrefabInit()
	{
		this.InitializeSlices();
		base.OnPrefabInit();
		SlicedUpdaterSim1000ms<T>.instance = this;
	}

	// Token: 0x06004464 RID: 17508 RVA: 0x001823AB File Offset: 0x001805AB
	protected override void OnForcedCleanUp()
	{
		SlicedUpdaterSim1000ms<T>.instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x06004465 RID: 17509 RVA: 0x001823BC File Offset: 0x001805BC
	private void InitializeSlices()
	{
		int num = SlicedUpdaterSim1000ms<T>.NUM_200MS_BUCKETS * this.numSlicesPer200ms;
		this.m_slices = new List<SlicedUpdaterSim1000ms<T>.Slice>();
		for (int i = 0; i < num; i++)
		{
			this.m_slices.Add(new SlicedUpdaterSim1000ms<T>.Slice());
		}
		this.m_nextSliceIdx = 0;
	}

	// Token: 0x06004466 RID: 17510 RVA: 0x00182404 File Offset: 0x00180604
	private int GetSliceIdx(T toBeUpdated)
	{
		return toBeUpdated.GetComponent<KPrefabID>().InstanceID % this.m_slices.Count;
	}

	// Token: 0x06004467 RID: 17511 RVA: 0x00182424 File Offset: 0x00180624
	public void RegisterUpdate1000ms(T toBeUpdated)
	{
		SlicedUpdaterSim1000ms<T>.Slice slice = this.m_slices[this.GetSliceIdx(toBeUpdated)];
		slice.Register(toBeUpdated);
		DebugUtil.DevAssert(slice.Count < this.maxUpdatesPer200ms, string.Format("The SlicedUpdaterSim1000ms for {0} wants to update no more than {1} instances per 200ms tick, but a slice has grown more than the SlicedUpdaterSim1000ms can support.", typeof(T).Name, this.maxUpdatesPer200ms), null);
	}

	// Token: 0x06004468 RID: 17512 RVA: 0x00182481 File Offset: 0x00180681
	public void UnregisterUpdate1000ms(T toBeUpdated)
	{
		this.m_slices[this.GetSliceIdx(toBeUpdated)].Unregister(toBeUpdated);
	}

	// Token: 0x06004469 RID: 17513 RVA: 0x0018249C File Offset: 0x0018069C
	public void Sim200ms(float dt)
	{
		foreach (SlicedUpdaterSim1000ms<T>.Slice slice in this.m_slices)
		{
			slice.IncrementDt(dt);
		}
		int num = 0;
		int i = 0;
		while (i < this.numSlicesPer200ms)
		{
			SlicedUpdaterSim1000ms<T>.Slice slice2 = this.m_slices[this.m_nextSliceIdx];
			num += slice2.Count;
			if (num > this.maxUpdatesPer200ms && i > 0)
			{
				break;
			}
			slice2.Update();
			i++;
			this.m_nextSliceIdx = (this.m_nextSliceIdx + 1) % this.m_slices.Count;
		}
	}

	// Token: 0x04002D99 RID: 11673
	private static int NUM_200MS_BUCKETS = 5;

	// Token: 0x04002D9A RID: 11674
	public static SlicedUpdaterSim1000ms<T> instance;

	// Token: 0x04002D9B RID: 11675
	[Serialize]
	public int maxUpdatesPer200ms = 300;

	// Token: 0x04002D9C RID: 11676
	[Serialize]
	public int numSlicesPer200ms = 3;

	// Token: 0x04002D9D RID: 11677
	private List<SlicedUpdaterSim1000ms<T>.Slice> m_slices;

	// Token: 0x04002D9E RID: 11678
	private int m_nextSliceIdx;

	// Token: 0x02001703 RID: 5891
	private class Slice
	{
		// Token: 0x06008937 RID: 35127 RVA: 0x002F814B File Offset: 0x002F634B
		public void Register(T toBeUpdated)
		{
			if (this.m_timeSinceLastUpdate == 0f)
			{
				this.m_updateList.Add(toBeUpdated);
				return;
			}
			this.m_recentlyAdded[toBeUpdated] = 0f;
		}

		// Token: 0x06008938 RID: 35128 RVA: 0x002F8178 File Offset: 0x002F6378
		public void Unregister(T toBeUpdated)
		{
			if (!this.m_updateList.Remove(toBeUpdated))
			{
				this.m_recentlyAdded.Remove(toBeUpdated);
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06008939 RID: 35129 RVA: 0x002F8195 File Offset: 0x002F6395
		public int Count
		{
			get
			{
				return this.m_updateList.Count + this.m_recentlyAdded.Count;
			}
		}

		// Token: 0x0600893A RID: 35130 RVA: 0x002F81AE File Offset: 0x002F63AE
		public List<T> GetUpdateList()
		{
			List<T> list = new List<T>();
			list.AddRange(this.m_updateList);
			list.AddRange(this.m_recentlyAdded.Keys);
			return list;
		}

		// Token: 0x0600893B RID: 35131 RVA: 0x002F81D4 File Offset: 0x002F63D4
		public void Update()
		{
			foreach (T t in this.m_updateList)
			{
				t.SlicedSim1000ms(this.m_timeSinceLastUpdate);
			}
			foreach (KeyValuePair<T, float> keyValuePair in this.m_recentlyAdded)
			{
				keyValuePair.Key.SlicedSim1000ms(keyValuePair.Value);
				this.m_updateList.Add(keyValuePair.Key);
			}
			this.m_recentlyAdded.Clear();
			this.m_timeSinceLastUpdate = 0f;
		}

		// Token: 0x0600893C RID: 35132 RVA: 0x002F82AC File Offset: 0x002F64AC
		public void IncrementDt(float dt)
		{
			this.m_timeSinceLastUpdate += dt;
			if (this.m_recentlyAdded.Count > 0)
			{
				foreach (T t in new List<T>(this.m_recentlyAdded.Keys))
				{
					Dictionary<T, float> recentlyAdded = this.m_recentlyAdded;
					T t2 = t;
					recentlyAdded[t2] += dt;
				}
			}
		}

		// Token: 0x04006BC0 RID: 27584
		private float m_timeSinceLastUpdate;

		// Token: 0x04006BC1 RID: 27585
		private List<T> m_updateList = new List<T>();

		// Token: 0x04006BC2 RID: 27586
		private Dictionary<T, float> m_recentlyAdded = new Dictionary<T, float>();
	}
}
