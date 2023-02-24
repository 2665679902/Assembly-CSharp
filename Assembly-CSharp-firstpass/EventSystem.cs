using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000098 RID: 152
public class EventSystem
{
	// Token: 0x060005D7 RID: 1495 RVA: 0x0001B4E0 File Offset: 0x000196E0
	public EventSystem()
	{
		this.log = new LoggerFIO("Events", 35);
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x0001B510 File Offset: 0x00019710
	public void Trigger(GameObject go, int hash, object data = null)
	{
		if (App.IsExiting)
		{
			return;
		}
		this.currentlyTriggering++;
		for (int num = 0; num != this.intraObjectRoutes.size; num++)
		{
			if (this.intraObjectRoutes[num].eventHash == hash)
			{
				EventSystem.intraObjectDispatcher[hash][this.intraObjectRoutes[num].handlerIndex].Trigger(go, data);
			}
		}
		List<EventSystem.Entry> list;
		if (this.entryMap.TryGetValue(hash, out list))
		{
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				if (list[i].hash == hash && list[i].handler != null)
				{
					list[i].handler(data);
				}
			}
		}
		this.currentlyTriggering--;
		if (this.dirty && this.currentlyTriggering == 0)
		{
			this.dirty = false;
			List<EventSystem.Entry> list2;
			if (this.entryMap.TryGetValue(hash, out list2))
			{
				list2.RemoveAll((EventSystem.Entry x) => x.handler == null);
			}
			this.intraObjectRoutes.RemoveAllSwap((EventSystem.IntraObjectRoute route) => !route.IsValid());
		}
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x0001B660 File Offset: 0x00019860
	public void OnCleanUp()
	{
		for (int i = this.subscribedEvents.size - 1; i >= 0; i--)
		{
			EventSystem.SubscribedEntry subscribedEntry = this.subscribedEvents[i];
			if (subscribedEntry.go != null)
			{
				this.Unsubscribe(subscribedEntry.go, subscribedEntry.hash, subscribedEntry.handler);
			}
		}
		foreach (KeyValuePair<int, List<EventSystem.Entry>> keyValuePair in this.entryMap)
		{
			List<EventSystem.Entry> value = keyValuePair.Value;
			for (int j = 0; j < value.Count; j++)
			{
				EventSystem.Entry entry = value[j];
				entry.handler = null;
				value[j] = entry;
			}
			value.Clear();
		}
		this.entryMap.Clear();
		this.subscribedEvents.Clear();
		this.intraObjectRoutes.Clear();
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x0001B75C File Offset: 0x0001995C
	public void UnregisterEvent(GameObject target, int eventName, Action<object> handler)
	{
		for (int i = 0; i < this.subscribedEvents.size; i++)
		{
			if (this.subscribedEvents[i].hash == eventName && this.subscribedEvents[i].handler == handler && this.subscribedEvents[i].go == target)
			{
				this.subscribedEvents.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x0001B7D2 File Offset: 0x000199D2
	public void RegisterEvent(GameObject target, int eventName, Action<object> handler)
	{
		this.subscribedEvents.Add(new EventSystem.SubscribedEntry(target, eventName, handler));
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x0001B7E8 File Offset: 0x000199E8
	public int Subscribe(int hash, Action<object> handler)
	{
		int num = this.nextId + 1;
		this.nextId = num;
		EventSystem.Entry entry = new EventSystem.Entry(hash, handler, num);
		List<EventSystem.Entry> list;
		if (!this.entryMap.TryGetValue(hash, out list))
		{
			list = new List<EventSystem.Entry>();
			this.entryMap.Add(hash, list);
		}
		list.Add(entry);
		this.idMap.Add(entry.id, entry.hash);
		return this.nextId;
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x0001B858 File Offset: 0x00019A58
	public void Unsubscribe(int hash, Action<object> handler)
	{
		List<EventSystem.Entry> list;
		if (this.entryMap.TryGetValue(hash, out list))
		{
			int i = 0;
			while (i < list.Count)
			{
				if (list[i].hash == hash && list[i].handler == handler)
				{
					if (this.currentlyTriggering == 0)
					{
						list.RemoveAt(i);
						return;
					}
					this.dirty = true;
					EventSystem.Entry entry = list[i];
					entry.handler = null;
					list[i] = entry;
					return;
				}
				else
				{
					i++;
				}
			}
		}
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x0001B8DC File Offset: 0x00019ADC
	public void Unsubscribe(int id)
	{
		int num = -1;
		List<EventSystem.Entry> list;
		if (this.idMap.TryGetValue(id, out num) && this.entryMap.TryGetValue(num, out list))
		{
			int i = 0;
			while (i < list.Count)
			{
				if (list[i].id == id)
				{
					if (this.currentlyTriggering == 0)
					{
						list.RemoveAt(i);
						break;
					}
					this.dirty = true;
					EventSystem.Entry entry = list[i];
					entry.handler = null;
					list[i] = entry;
					break;
				}
				else
				{
					i++;
				}
			}
		}
		this.idMap.Remove(id);
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x0001B96A File Offset: 0x00019B6A
	public int Subscribe(GameObject target, int eventName, Action<object> handler)
	{
		this.RegisterEvent(target, eventName, handler);
		return KObjectManager.Instance.GetOrCreateObject(target).GetEventSystem().Subscribe(eventName, handler);
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x0001B98C File Offset: 0x00019B8C
	public int Subscribe<ComponentType>(int eventName, EventSystem.IntraObjectHandler<ComponentType> handler) where ComponentType : Component
	{
		List<EventSystem.IntraObjectHandlerBase> list;
		if (!EventSystem.intraObjectDispatcher.TryGetValue(eventName, out list))
		{
			list = new List<EventSystem.IntraObjectHandlerBase>();
			EventSystem.intraObjectDispatcher.Add(eventName, list);
		}
		int num = list.IndexOf(handler);
		if (num == -1)
		{
			list.Add(handler);
			num = list.Count - 1;
		}
		this.intraObjectRoutes.Add(new EventSystem.IntraObjectRoute(eventName, num));
		return num;
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x0001B9EA File Offset: 0x00019BEA
	public void Unsubscribe(GameObject target, int eventName, Action<object> handler)
	{
		this.UnregisterEvent(target, eventName, handler);
		if (target == null)
		{
			return;
		}
		KObjectManager.Instance.GetOrCreateObject(target).GetEventSystem().Unsubscribe(eventName, handler);
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x0001BA18 File Offset: 0x00019C18
	public void Unsubscribe(int eventName, int subscribeHandle, bool suppressWarnings = false)
	{
		int num = this.intraObjectRoutes.FindIndex((EventSystem.IntraObjectRoute route) => route.eventHash == eventName && route.handlerIndex == subscribeHandle);
		if (num == -1)
		{
			if (!suppressWarnings)
			{
				global::Debug.LogWarning("Failed to Unsubscribe event handler: " + EventSystem.intraObjectDispatcher[eventName][subscribeHandle].ToString() + "\nNot subscribed to event");
			}
			return;
		}
		if (this.currentlyTriggering == 0)
		{
			this.intraObjectRoutes.RemoveAtSwap(num);
			return;
		}
		this.dirty = true;
		this.intraObjectRoutes[num] = default(EventSystem.IntraObjectRoute);
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x0001BAC0 File Offset: 0x00019CC0
	public void Unsubscribe<ComponentType>(int eventName, EventSystem.IntraObjectHandler<ComponentType> handler, bool suppressWarnings) where ComponentType : Component
	{
		List<EventSystem.IntraObjectHandlerBase> list;
		if (!EventSystem.intraObjectDispatcher.TryGetValue(eventName, out list))
		{
			if (!suppressWarnings)
			{
				global::Debug.LogWarning(string.Format("Failed to Unsubscribe event handler: {0}\nNo subscriptions have been made to event {1}", handler.ToString(), eventName));
			}
			return;
		}
		int num = list.IndexOf(handler);
		if (num == -1)
		{
			if (!suppressWarnings)
			{
				global::Debug.LogWarning(string.Format("Failed to Unsubscribe event handler: {0}\nNot subscribed to event {1}", handler.ToString(), eventName));
			}
			return;
		}
		this.Unsubscribe(eventName, num, suppressWarnings);
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x0001BB30 File Offset: 0x00019D30
	public void Unsubscribe(string[] eventNames, Action<object> handler)
	{
		for (int i = 0; i < eventNames.Length; i++)
		{
			int num = Hash.SDBMLower(eventNames[i]);
			this.Unsubscribe(num, handler);
		}
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x0001BB5E File Offset: 0x00019D5E
	[Conditional("ENABLE_DETAILED_EVENT_PROFILE_INFO")]
	private static void BeginDetailedSample(string region_name)
	{
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x0001BB60 File Offset: 0x00019D60
	[Conditional("ENABLE_DETAILED_EVENT_PROFILE_INFO")]
	private static void EndDetailedSample(string region_name)
	{
	}

	// Token: 0x0400058B RID: 1419
	private int nextId;

	// Token: 0x0400058C RID: 1420
	private int currentlyTriggering;

	// Token: 0x0400058D RID: 1421
	private bool dirty;

	// Token: 0x0400058E RID: 1422
	private ArrayRef<EventSystem.SubscribedEntry> subscribedEvents;

	// Token: 0x0400058F RID: 1423
	private Dictionary<int, List<EventSystem.Entry>> entryMap = new Dictionary<int, List<EventSystem.Entry>>();

	// Token: 0x04000590 RID: 1424
	private Dictionary<int, int> idMap = new Dictionary<int, int>();

	// Token: 0x04000591 RID: 1425
	private ArrayRef<EventSystem.IntraObjectRoute> intraObjectRoutes;

	// Token: 0x04000592 RID: 1426
	private static Dictionary<int, List<EventSystem.IntraObjectHandlerBase>> intraObjectDispatcher = new Dictionary<int, List<EventSystem.IntraObjectHandlerBase>>();

	// Token: 0x04000593 RID: 1427
	private LoggerFIO log;

	// Token: 0x020009D6 RID: 2518
	private struct Entry
	{
		// Token: 0x06005397 RID: 21399 RVA: 0x0009C0D4 File Offset: 0x0009A2D4
		public Entry(int hash, Action<object> handler, int id)
		{
			this.handler = handler;
			this.hash = hash;
			this.id = id;
		}

		// Token: 0x0400220E RID: 8718
		public Action<object> handler;

		// Token: 0x0400220F RID: 8719
		public int hash;

		// Token: 0x04002210 RID: 8720
		public int id;
	}

	// Token: 0x020009D7 RID: 2519
	private struct SubscribedEntry
	{
		// Token: 0x06005398 RID: 21400 RVA: 0x0009C0EB File Offset: 0x0009A2EB
		public SubscribedEntry(GameObject go, int hash, Action<object> handler)
		{
			this.go = go;
			this.hash = hash;
			this.handler = handler;
		}

		// Token: 0x04002211 RID: 8721
		public Action<object> handler;

		// Token: 0x04002212 RID: 8722
		public int hash;

		// Token: 0x04002213 RID: 8723
		public GameObject go;
	}

	// Token: 0x020009D8 RID: 2520
	private struct IntraObjectRoute
	{
		// Token: 0x06005399 RID: 21401 RVA: 0x0009C102 File Offset: 0x0009A302
		public IntraObjectRoute(int eventHash, int handlerIndex)
		{
			this.eventHash = eventHash;
			this.handlerIndex = handlerIndex;
		}

		// Token: 0x0600539A RID: 21402 RVA: 0x0009C112 File Offset: 0x0009A312
		public bool IsValid()
		{
			return this.eventHash != 0;
		}

		// Token: 0x04002214 RID: 8724
		public int eventHash;

		// Token: 0x04002215 RID: 8725
		public int handlerIndex;
	}

	// Token: 0x020009D9 RID: 2521
	public abstract class IntraObjectHandlerBase
	{
		// Token: 0x0600539B RID: 21403
		public abstract void Trigger(GameObject gameObject, object eventData);
	}

	// Token: 0x020009DA RID: 2522
	public class IntraObjectHandler<ComponentType> : EventSystem.IntraObjectHandlerBase where ComponentType : Component
	{
		// Token: 0x0600539D RID: 21405 RVA: 0x0009C125 File Offset: 0x0009A325
		public static bool IsStatic(Delegate del)
		{
			return del.Target == null || del.Target.GetType().GetCustomAttributes(false).OfType<CompilerGeneratedAttribute>()
				.Any<CompilerGeneratedAttribute>();
		}

		// Token: 0x0600539E RID: 21406 RVA: 0x0009C14C File Offset: 0x0009A34C
		public IntraObjectHandler(Action<ComponentType, object> handler)
		{
			global::Debug.Assert(EventSystem.IntraObjectHandler<ComponentType>.IsStatic(handler), "IntraObjectHandler method must be static to avoid allocations");
			this.handler = handler;
		}

		// Token: 0x0600539F RID: 21407 RVA: 0x0009C16B File Offset: 0x0009A36B
		public static implicit operator EventSystem.IntraObjectHandler<ComponentType>(Action<ComponentType, object> handler)
		{
			return new EventSystem.IntraObjectHandler<ComponentType>(handler);
		}

		// Token: 0x060053A0 RID: 21408 RVA: 0x0009C174 File Offset: 0x0009A374
		public override void Trigger(GameObject gameObject, object eventData)
		{
			ListPool<ComponentType, EventSystem.IntraObjectHandler<ComponentType>>.PooledList pooledList = ListPool<ComponentType, EventSystem.IntraObjectHandler<ComponentType>>.Allocate();
			gameObject.GetComponents<ComponentType>(pooledList);
			foreach (ComponentType componentType in pooledList)
			{
				this.handler(componentType, eventData);
			}
			pooledList.Recycle();
		}

		// Token: 0x060053A1 RID: 21409 RVA: 0x0009C1DC File Offset: 0x0009A3DC
		public override string ToString()
		{
			return ((this.handler.Target != null) ? this.handler.Target.GetType().ToString() : "STATIC") + "." + this.handler.Method.ToString();
		}

		// Token: 0x04002216 RID: 8726
		private Action<ComponentType, object> handler;
	}
}
