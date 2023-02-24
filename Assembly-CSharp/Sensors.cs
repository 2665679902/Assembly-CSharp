using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003F2 RID: 1010
[AddComponentMenu("KMonoBehaviour/scripts/Sensors")]
public class Sensors : KMonoBehaviour
{
	// Token: 0x060014ED RID: 5357 RVA: 0x0006DA1D File Offset: 0x0006BC1D
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.GetComponent<Brain>().onPreUpdate += this.OnBrainPreUpdate;
	}

	// Token: 0x060014EE RID: 5358 RVA: 0x0006DA3C File Offset: 0x0006BC3C
	public SensorType GetSensor<SensorType>() where SensorType : Sensor
	{
		foreach (Sensor sensor in this.sensors)
		{
			if (typeof(SensorType).IsAssignableFrom(sensor.GetType()))
			{
				return (SensorType)((object)sensor);
			}
		}
		global::Debug.LogError("Missing sensor of type: " + typeof(SensorType).Name);
		return default(SensorType);
	}

	// Token: 0x060014EF RID: 5359 RVA: 0x0006DAD4 File Offset: 0x0006BCD4
	public void Add(Sensor sensor)
	{
		this.sensors.Add(sensor);
		sensor.Update();
	}

	// Token: 0x060014F0 RID: 5360 RVA: 0x0006DAE8 File Offset: 0x0006BCE8
	public void UpdateSensors()
	{
		foreach (Sensor sensor in this.sensors)
		{
			sensor.Update();
		}
	}

	// Token: 0x060014F1 RID: 5361 RVA: 0x0006DB38 File Offset: 0x0006BD38
	private void OnBrainPreUpdate()
	{
		this.UpdateSensors();
	}

	// Token: 0x060014F2 RID: 5362 RVA: 0x0006DB40 File Offset: 0x0006BD40
	public void ShowEditor()
	{
		foreach (Sensor sensor in this.sensors)
		{
			sensor.ShowEditor();
		}
	}

	// Token: 0x04000BBA RID: 3002
	public List<Sensor> sensors = new List<Sensor>();
}
