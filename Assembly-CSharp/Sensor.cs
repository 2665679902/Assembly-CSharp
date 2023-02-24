using System;
using UnityEngine;

// Token: 0x020003F1 RID: 1009
public class Sensor
{
	// Token: 0x17000092 RID: 146
	// (get) Token: 0x060014E4 RID: 5348 RVA: 0x0006D9B2 File Offset: 0x0006BBB2
	// (set) Token: 0x060014E5 RID: 5349 RVA: 0x0006D9BA File Offset: 0x0006BBBA
	public string Name { get; private set; }

	// Token: 0x060014E6 RID: 5350 RVA: 0x0006D9C3 File Offset: 0x0006BBC3
	public Sensor(Sensors sensors)
	{
		this.sensors = sensors;
		this.Name = base.GetType().Name;
	}

	// Token: 0x060014E7 RID: 5351 RVA: 0x0006D9E3 File Offset: 0x0006BBE3
	public ComponentType GetComponent<ComponentType>()
	{
		return this.sensors.GetComponent<ComponentType>();
	}

	// Token: 0x17000093 RID: 147
	// (get) Token: 0x060014E8 RID: 5352 RVA: 0x0006D9F0 File Offset: 0x0006BBF0
	public GameObject gameObject
	{
		get
		{
			return this.sensors.gameObject;
		}
	}

	// Token: 0x17000094 RID: 148
	// (get) Token: 0x060014E9 RID: 5353 RVA: 0x0006D9FD File Offset: 0x0006BBFD
	public Transform transform
	{
		get
		{
			return this.gameObject.transform;
		}
	}

	// Token: 0x060014EA RID: 5354 RVA: 0x0006DA0A File Offset: 0x0006BC0A
	public void Trigger(int hash, object data = null)
	{
		this.sensors.Trigger(hash, data);
	}

	// Token: 0x060014EB RID: 5355 RVA: 0x0006DA19 File Offset: 0x0006BC19
	public virtual void Update()
	{
	}

	// Token: 0x060014EC RID: 5356 RVA: 0x0006DA1B File Offset: 0x0006BC1B
	public virtual void ShowEditor()
	{
	}

	// Token: 0x04000BB8 RID: 3000
	protected Sensors sensors;
}
