using System;
using System.Collections.Generic;
using System.Linq;

// Token: 0x020000F9 RID: 249
public class SimAndRenderScheduler
{
	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x06000885 RID: 2181 RVA: 0x0002247C File Offset: 0x0002067C
	public static SimAndRenderScheduler instance
	{
		get
		{
			if (SimAndRenderScheduler._instance == null)
			{
				SimAndRenderScheduler._instance = new SimAndRenderScheduler();
			}
			return SimAndRenderScheduler._instance;
		}
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x00022494 File Offset: 0x00020694
	public static void DestroyInstance()
	{
		SimAndRenderScheduler._instance = null;
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x0002249C File Offset: 0x0002069C
	private SimAndRenderScheduler()
	{
		this.availableInterfaces[typeof(IRenderEveryTick)] = UpdateRate.RENDER_EVERY_TICK;
		this.availableInterfaces[typeof(IRender200ms)] = UpdateRate.RENDER_200ms;
		this.availableInterfaces[typeof(IRender1000ms)] = UpdateRate.RENDER_1000ms;
		this.availableInterfaces[typeof(ISimEveryTick)] = UpdateRate.SIM_EVERY_TICK;
		this.availableInterfaces[typeof(ISim33ms)] = UpdateRate.SIM_33ms;
		this.availableInterfaces[typeof(ISim200ms)] = UpdateRate.SIM_200ms;
		this.availableInterfaces[typeof(ISim1000ms)] = UpdateRate.SIM_1000ms;
		this.availableInterfaces[typeof(ISim4000ms)] = UpdateRate.SIM_4000ms;
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x000225D8 File Offset: 0x000207D8
	private static string MakeBucketId(Type updater_type, UpdateRate update_rate)
	{
		return string.Format("{0} {1}", updater_type.Name, update_rate.ToString());
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x000225F8 File Offset: 0x000207F8
	private UpdateRate[] GetImplementedInterfaces(Type type)
	{
		UpdateRate[] array = null;
		if (!this.typeImplementedInterfaces.TryGetValue(type, out array))
		{
			ListPool<UpdateRate, SimAndRenderScheduler>.PooledList pooledList = ListPool<UpdateRate, SimAndRenderScheduler>.Allocate();
			foreach (KeyValuePair<Type, UpdateRate> keyValuePair in this.availableInterfaces)
			{
				if (keyValuePair.Key.IsAssignableFrom(type))
				{
					pooledList.Add(keyValuePair.Value);
				}
			}
			array = pooledList.ToArray();
			pooledList.Recycle();
			this.typeImplementedInterfaces[type] = array;
		}
		return array;
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x00022694 File Offset: 0x00020894
	public static Type GetUpdateInterface(UpdateRate update_rate)
	{
		switch (update_rate)
		{
		case UpdateRate.RENDER_EVERY_TICK:
			return typeof(IRenderEveryTick);
		case UpdateRate.RENDER_200ms:
			return typeof(IRender200ms);
		case UpdateRate.RENDER_1000ms:
			return typeof(IRender1000ms);
		case UpdateRate.SIM_EVERY_TICK:
			return typeof(ISimEveryTick);
		case UpdateRate.SIM_33ms:
			return typeof(ISim33ms);
		case UpdateRate.SIM_200ms:
			return typeof(ISim200ms);
		case UpdateRate.SIM_1000ms:
			return typeof(ISim1000ms);
		case UpdateRate.SIM_4000ms:
			return typeof(ISim4000ms);
		default:
			return null;
		}
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x00022724 File Offset: 0x00020924
	public UpdateRate GetUpdateRate(Type updater)
	{
		UpdateRate updateRate;
		if (!this.availableInterfaces.TryGetValue(updater, out updateRate))
		{
			Debug.Assert(false, "only call this with an update interface type");
		}
		return updateRate;
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x0002274D File Offset: 0x0002094D
	public UpdateRate GetUpdateRate<T>()
	{
		return this.GetUpdateRate(typeof(T));
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x00022760 File Offset: 0x00020960
	public void Add(object obj, bool load_balance = false)
	{
		UpdateRate[] implementedInterfaces = this.GetImplementedInterfaces(obj.GetType());
		for (int i = 0; i < implementedInterfaces.Length; i++)
		{
			switch (implementedInterfaces[i])
			{
			case UpdateRate.RENDER_EVERY_TICK:
				this.renderEveryTick.Add((IRenderEveryTick)obj, load_balance);
				break;
			case UpdateRate.RENDER_200ms:
				this.render200ms.Add((IRender200ms)obj, load_balance);
				break;
			case UpdateRate.RENDER_1000ms:
				this.render1000ms.Add((IRender1000ms)obj, load_balance);
				break;
			case UpdateRate.SIM_EVERY_TICK:
				this.simEveryTick.Add((ISimEveryTick)obj, load_balance);
				break;
			case UpdateRate.SIM_33ms:
				this.sim33ms.Add((ISim33ms)obj, load_balance);
				break;
			case UpdateRate.SIM_200ms:
				this.sim200ms.Add((ISim200ms)obj, load_balance);
				break;
			case UpdateRate.SIM_1000ms:
				this.sim1000ms.Add((ISim1000ms)obj, load_balance);
				break;
			case UpdateRate.SIM_4000ms:
				this.sim4000ms.Add((ISim4000ms)obj, load_balance);
				break;
			}
		}
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x00022860 File Offset: 0x00020A60
	public void Remove(object obj)
	{
		UpdateRate[] implementedInterfaces = this.GetImplementedInterfaces(obj.GetType());
		for (int i = 0; i < implementedInterfaces.Length; i++)
		{
			switch (implementedInterfaces[i])
			{
			case UpdateRate.RENDER_EVERY_TICK:
				this.renderEveryTick.Remove((IRenderEveryTick)obj);
				break;
			case UpdateRate.RENDER_200ms:
				this.render200ms.Remove((IRender200ms)obj);
				break;
			case UpdateRate.RENDER_1000ms:
				this.render1000ms.Remove((IRender1000ms)obj);
				break;
			case UpdateRate.SIM_EVERY_TICK:
				this.simEveryTick.Remove((ISimEveryTick)obj);
				break;
			case UpdateRate.SIM_33ms:
				this.sim33ms.Remove((ISim33ms)obj);
				break;
			case UpdateRate.SIM_200ms:
				this.sim200ms.Remove((ISim200ms)obj);
				break;
			case UpdateRate.SIM_1000ms:
				this.sim1000ms.Remove((ISim1000ms)obj);
				break;
			case UpdateRate.SIM_4000ms:
				this.sim4000ms.Remove((ISim4000ms)obj);
				break;
			}
		}
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x00022958 File Offset: 0x00020B58
	private SimAndRenderScheduler.Entry ManifestEntry<UpdateInterface>(string name, bool load_balance)
	{
		SimAndRenderScheduler.Entry entry;
		if (this.bucketTable.TryGetValue(name, out entry))
		{
			DebugUtil.DevAssertArgs(entry.buckets.Length == (load_balance ? Singleton<StateMachineUpdater>.Instance.GetFrameCount(this.GetUpdateRate<UpdateInterface>()) : 1), new object[] { "load_balance doesn't match previous registration...maybe load_balance erroneously on for a BatchUpdate type ", name, "?" });
			return entry;
		}
		entry = default(SimAndRenderScheduler.Entry);
		UpdateRate updateRate = this.GetUpdateRate<UpdateInterface>();
		int num = (load_balance ? Singleton<StateMachineUpdater>.Instance.GetFrameCount(updateRate) : 1);
		entry.buckets = new StateMachineUpdater.BaseUpdateBucket[num];
		for (int i = 0; i < num; i++)
		{
			entry.buckets[i] = new UpdateBucketWithUpdater<UpdateInterface>(name);
			Singleton<StateMachineUpdater>.Instance.AddBucket(updateRate, entry.buckets[i]);
		}
		return entry;
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x00022A14 File Offset: 0x00020C14
	public SimAndRenderScheduler.Handle Schedule<SimUpdateType>(string name, UpdateBucketWithUpdater<SimUpdateType>.IUpdater bucket_updater, UpdateRate update_rate, SimUpdateType updater, bool load_balance = false)
	{
		SimAndRenderScheduler.Entry entry = this.ManifestEntry<SimUpdateType>(name, load_balance);
		UpdateBucketWithUpdater<SimUpdateType> updateBucketWithUpdater = (UpdateBucketWithUpdater<SimUpdateType>)entry.buckets[entry.nextBucketIdx];
		SimAndRenderScheduler.Handle handle = default(SimAndRenderScheduler.Handle);
		handle.handle = updateBucketWithUpdater.Add(updater, Singleton<StateMachineUpdater>.Instance.GetFrameTime(update_rate, updateBucketWithUpdater.frame), bucket_updater);
		handle.bucket = updateBucketWithUpdater;
		entry.nextBucketIdx = (entry.nextBucketIdx + 1) % entry.buckets.Length;
		this.bucketTable[name] = entry;
		return handle;
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x00022A95 File Offset: 0x00020C95
	public void Reset()
	{
		SimAndRenderScheduler._instance = null;
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x00022AA0 File Offset: 0x00020CA0
	public void RegisterBatchUpdate<UpdateInterface, T>(UpdateBucketWithUpdater<UpdateInterface>.BatchUpdateDelegate batch_update)
	{
		string text = SimAndRenderScheduler.MakeBucketId(typeof(T), this.GetUpdateRate<UpdateInterface>());
		SimAndRenderScheduler.Entry entry = this.ManifestEntry<UpdateInterface>(text, false);
		DebugUtil.DevAssert(this.GetImplementedInterfaces(typeof(T)).Contains(this.GetUpdateRate<UpdateInterface>()), "T does not implement the UpdateInterface it is registering for BatchUpdate under", null);
		DebugUtil.DevAssert(entry.buckets.Length == 1, "don't do a batch update with load balancing because load balancing will produce many small batches which is inefficient", null);
		((UpdateBucketWithUpdater<UpdateInterface>)entry.buckets[0]).batch_update_delegate = batch_update;
		this.bucketTable[text] = entry;
	}

	// Token: 0x0400064E RID: 1614
	private static SimAndRenderScheduler _instance;

	// Token: 0x0400064F RID: 1615
	private Dictionary<string, SimAndRenderScheduler.Entry> bucketTable = new Dictionary<string, SimAndRenderScheduler.Entry>();

	// Token: 0x04000650 RID: 1616
	public SimAndRenderScheduler.RenderEveryTickUpdater renderEveryTick = new SimAndRenderScheduler.RenderEveryTickUpdater();

	// Token: 0x04000651 RID: 1617
	public SimAndRenderScheduler.Render200ms render200ms = new SimAndRenderScheduler.Render200ms();

	// Token: 0x04000652 RID: 1618
	public SimAndRenderScheduler.Render1000msUpdater render1000ms = new SimAndRenderScheduler.Render1000msUpdater();

	// Token: 0x04000653 RID: 1619
	public SimAndRenderScheduler.SimEveryTickUpdater simEveryTick = new SimAndRenderScheduler.SimEveryTickUpdater();

	// Token: 0x04000654 RID: 1620
	public SimAndRenderScheduler.Sim33msUpdater sim33ms = new SimAndRenderScheduler.Sim33msUpdater();

	// Token: 0x04000655 RID: 1621
	public SimAndRenderScheduler.Sim200msUpdater sim200ms = new SimAndRenderScheduler.Sim200msUpdater();

	// Token: 0x04000656 RID: 1622
	public SimAndRenderScheduler.Sim1000msUpdater sim1000ms = new SimAndRenderScheduler.Sim1000msUpdater();

	// Token: 0x04000657 RID: 1623
	public SimAndRenderScheduler.Sim4000msUpdater sim4000ms = new SimAndRenderScheduler.Sim4000msUpdater();

	// Token: 0x04000658 RID: 1624
	private Dictionary<Type, UpdateRate[]> typeImplementedInterfaces = new Dictionary<Type, UpdateRate[]>();

	// Token: 0x04000659 RID: 1625
	private Dictionary<Type, UpdateRate> availableInterfaces = new Dictionary<Type, UpdateRate>();

	// Token: 0x020009F3 RID: 2547
	public struct Handle
	{
		// Token: 0x060053F5 RID: 21493 RVA: 0x0009C911 File Offset: 0x0009AB11
		public bool IsValid()
		{
			return this.bucket != null;
		}

		// Token: 0x060053F6 RID: 21494 RVA: 0x0009C91C File Offset: 0x0009AB1C
		public void Release()
		{
			if (this.bucket != null)
			{
				this.bucket.Remove(this.handle);
				this.bucket = null;
			}
		}

		// Token: 0x04002247 RID: 8775
		public HandleVector<int>.Handle handle;

		// Token: 0x04002248 RID: 8776
		public StateMachineUpdater.BaseUpdateBucket bucket;
	}

	// Token: 0x020009F4 RID: 2548
	private struct Entry
	{
		// Token: 0x04002249 RID: 8777
		public StateMachineUpdater.BaseUpdateBucket[] buckets;

		// Token: 0x0400224A RID: 8778
		public int nextBucketIdx;
	}

	// Token: 0x020009F5 RID: 2549
	public class BaseUpdaterManager
	{
		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x060053F7 RID: 21495 RVA: 0x0009C93E File Offset: 0x0009AB3E
		// (set) Token: 0x060053F8 RID: 21496 RVA: 0x0009C946 File Offset: 0x0009AB46
		public UpdateRate updateRate { get; private set; }

		// Token: 0x060053F9 RID: 21497 RVA: 0x0009C94F File Offset: 0x0009AB4F
		protected BaseUpdaterManager(UpdateRate update_rate)
		{
			this.updateRate = update_rate;
		}
	}

	// Token: 0x020009F6 RID: 2550
	public class UpdaterManager<UpdaterType> : SimAndRenderScheduler.BaseUpdaterManager
	{
		// Token: 0x060053FA RID: 21498 RVA: 0x0009C95E File Offset: 0x0009AB5E
		public UpdaterManager(UpdateRate update_rate)
			: base(update_rate)
		{
		}

		// Token: 0x060053FB RID: 21499 RVA: 0x0009C980 File Offset: 0x0009AB80
		public void Add(UpdaterType updater, bool load_balance = false)
		{
			if (this.Contains(updater))
			{
				return;
			}
			string text = "";
			if (!this.bucketIds.TryGetValue(updater.GetType(), out text))
			{
				text = SimAndRenderScheduler.MakeBucketId(updater.GetType(), base.updateRate);
				this.bucketIds[updater.GetType()] = text;
			}
			SimAndRenderScheduler.Handle handle = SimAndRenderScheduler.instance.Schedule<UpdaterType>(text, (UpdateBucketWithUpdater<UpdaterType>.IUpdater)this, base.updateRate, updater, load_balance);
			this.updaterHandles[updater] = handle;
		}

		// Token: 0x060053FC RID: 21500 RVA: 0x0009CA14 File Offset: 0x0009AC14
		public void Remove(UpdaterType updater)
		{
			SimAndRenderScheduler.Handle handle;
			if (this.updaterHandles.TryGetValue(updater, out handle))
			{
				handle.Release();
				this.updaterHandles.Remove(updater);
			}
		}

		// Token: 0x060053FD RID: 21501 RVA: 0x0009CA45 File Offset: 0x0009AC45
		public bool Contains(UpdaterType updater)
		{
			return this.updaterHandles.ContainsKey(updater);
		}

		// Token: 0x0400224C RID: 8780
		private Dictionary<UpdaterType, SimAndRenderScheduler.Handle> updaterHandles = new Dictionary<UpdaterType, SimAndRenderScheduler.Handle>();

		// Token: 0x0400224D RID: 8781
		private Dictionary<Type, string> bucketIds = new Dictionary<Type, string>();
	}

	// Token: 0x020009F7 RID: 2551
	public class RenderEveryTickUpdater : SimAndRenderScheduler.UpdaterManager<IRenderEveryTick>, UpdateBucketWithUpdater<IRenderEveryTick>.IUpdater
	{
		// Token: 0x060053FE RID: 21502 RVA: 0x0009CA53 File Offset: 0x0009AC53
		public RenderEveryTickUpdater()
			: base(UpdateRate.RENDER_EVERY_TICK)
		{
		}

		// Token: 0x060053FF RID: 21503 RVA: 0x0009CA5C File Offset: 0x0009AC5C
		public void Update(IRenderEveryTick updater, float dt)
		{
			updater.RenderEveryTick(dt);
		}
	}

	// Token: 0x020009F8 RID: 2552
	public class Render200ms : SimAndRenderScheduler.UpdaterManager<IRender200ms>, UpdateBucketWithUpdater<IRender200ms>.IUpdater
	{
		// Token: 0x06005400 RID: 21504 RVA: 0x0009CA65 File Offset: 0x0009AC65
		public Render200ms()
			: base(UpdateRate.RENDER_200ms)
		{
		}

		// Token: 0x06005401 RID: 21505 RVA: 0x0009CA6E File Offset: 0x0009AC6E
		public void Update(IRender200ms updater, float dt)
		{
			updater.Render200ms(dt);
		}
	}

	// Token: 0x020009F9 RID: 2553
	public class Render1000msUpdater : SimAndRenderScheduler.UpdaterManager<IRender1000ms>, UpdateBucketWithUpdater<IRender1000ms>.IUpdater
	{
		// Token: 0x06005402 RID: 21506 RVA: 0x0009CA77 File Offset: 0x0009AC77
		public Render1000msUpdater()
			: base(UpdateRate.RENDER_1000ms)
		{
		}

		// Token: 0x06005403 RID: 21507 RVA: 0x0009CA80 File Offset: 0x0009AC80
		public void Update(IRender1000ms updater, float dt)
		{
			updater.Render1000ms(dt);
		}
	}

	// Token: 0x020009FA RID: 2554
	public class SimEveryTickUpdater : SimAndRenderScheduler.UpdaterManager<ISimEveryTick>, UpdateBucketWithUpdater<ISimEveryTick>.IUpdater
	{
		// Token: 0x06005404 RID: 21508 RVA: 0x0009CA89 File Offset: 0x0009AC89
		public SimEveryTickUpdater()
			: base(UpdateRate.SIM_EVERY_TICK)
		{
		}

		// Token: 0x06005405 RID: 21509 RVA: 0x0009CA92 File Offset: 0x0009AC92
		public void Update(ISimEveryTick updater, float dt)
		{
			updater.SimEveryTick(dt);
		}
	}

	// Token: 0x020009FB RID: 2555
	public class Sim33msUpdater : SimAndRenderScheduler.UpdaterManager<ISim33ms>, UpdateBucketWithUpdater<ISim33ms>.IUpdater
	{
		// Token: 0x06005406 RID: 21510 RVA: 0x0009CA9B File Offset: 0x0009AC9B
		public Sim33msUpdater()
			: base(UpdateRate.SIM_33ms)
		{
		}

		// Token: 0x06005407 RID: 21511 RVA: 0x0009CAA4 File Offset: 0x0009ACA4
		public void Update(ISim33ms updater, float dt)
		{
			updater.Sim33ms(dt);
		}
	}

	// Token: 0x020009FC RID: 2556
	public class Sim200msUpdater : SimAndRenderScheduler.UpdaterManager<ISim200ms>, UpdateBucketWithUpdater<ISim200ms>.IUpdater
	{
		// Token: 0x06005408 RID: 21512 RVA: 0x0009CAAD File Offset: 0x0009ACAD
		public Sim200msUpdater()
			: base(UpdateRate.SIM_200ms)
		{
		}

		// Token: 0x06005409 RID: 21513 RVA: 0x0009CAB6 File Offset: 0x0009ACB6
		public void Update(ISim200ms updater, float dt)
		{
			updater.Sim200ms(dt);
		}
	}

	// Token: 0x020009FD RID: 2557
	public class Sim1000msUpdater : SimAndRenderScheduler.UpdaterManager<ISim1000ms>, UpdateBucketWithUpdater<ISim1000ms>.IUpdater
	{
		// Token: 0x0600540A RID: 21514 RVA: 0x0009CABF File Offset: 0x0009ACBF
		public Sim1000msUpdater()
			: base(UpdateRate.SIM_1000ms)
		{
		}

		// Token: 0x0600540B RID: 21515 RVA: 0x0009CAC8 File Offset: 0x0009ACC8
		public void Update(ISim1000ms updater, float dt)
		{
			updater.Sim1000ms(dt);
		}
	}

	// Token: 0x020009FE RID: 2558
	public class Sim4000msUpdater : SimAndRenderScheduler.UpdaterManager<ISim4000ms>, UpdateBucketWithUpdater<ISim4000ms>.IUpdater
	{
		// Token: 0x0600540C RID: 21516 RVA: 0x0009CAD1 File Offset: 0x0009ACD1
		public Sim4000msUpdater()
			: base(UpdateRate.SIM_4000ms)
		{
		}

		// Token: 0x0600540D RID: 21517 RVA: 0x0009CADA File Offset: 0x0009ACDA
		public void Update(ISim4000ms updater, float dt)
		{
			updater.Sim4000ms(dt);
		}
	}
}
