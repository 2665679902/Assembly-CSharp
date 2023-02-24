using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;

// Token: 0x02000767 RID: 1895
[SerializationConfig(MemberSerialization.OptIn)]
public class EventLogger<EventInstanceType, EventType> : KMonoBehaviour, ISaveLoadable where EventInstanceType : EventInstanceBase where EventType : EventBase
{
	// Token: 0x06003403 RID: 13315 RVA: 0x00117F42 File Offset: 0x00116142
	public IEnumerator<EventInstanceType> GetEnumerator()
	{
		return this.EventInstances.GetEnumerator();
	}

	// Token: 0x06003404 RID: 13316 RVA: 0x00117F54 File Offset: 0x00116154
	public EventType AddEvent(EventType ev)
	{
		for (int i = 0; i < this.Events.Count; i++)
		{
			if (this.Events[i].hash == ev.hash)
			{
				this.Events[i] = ev;
				return this.Events[i];
			}
		}
		this.Events.Add(ev);
		return ev;
	}

	// Token: 0x06003405 RID: 13317 RVA: 0x00117FC1 File Offset: 0x001161C1
	public EventInstanceType Add(EventInstanceType ev)
	{
		if (this.EventInstances.Count > 10000)
		{
			this.EventInstances.RemoveAt(0);
		}
		this.EventInstances.Add(ev);
		return ev;
	}

	// Token: 0x06003406 RID: 13318 RVA: 0x00117FF0 File Offset: 0x001161F0
	[OnDeserialized]
	protected internal void OnDeserialized()
	{
		if (this.EventInstances.Count > 10000)
		{
			this.EventInstances.RemoveRange(0, this.EventInstances.Count - 10000);
		}
		for (int i = 0; i < this.EventInstances.Count; i++)
		{
			for (int j = 0; j < this.Events.Count; j++)
			{
				if (this.Events[j].hash == this.EventInstances[i].eventHash)
				{
					this.EventInstances[i].ev = this.Events[j];
					break;
				}
			}
		}
	}

	// Token: 0x06003407 RID: 13319 RVA: 0x001180AF File Offset: 0x001162AF
	public void Clear()
	{
		this.EventInstances.Clear();
	}

	// Token: 0x04002021 RID: 8225
	private const int MAX_NUM_EVENTS = 10000;

	// Token: 0x04002022 RID: 8226
	private List<EventType> Events = new List<EventType>();

	// Token: 0x04002023 RID: 8227
	[Serialize]
	private List<EventInstanceType> EventInstances = new List<EventInstanceType>();
}
