using System;
using Klei.AI;
using UnityEngine;

// Token: 0x0200061F RID: 1567
[AddComponentMenu("KMonoBehaviour/scripts/ParkSign")]
public class ParkSign : KMonoBehaviour
{
	// Token: 0x0600290F RID: 10511 RVA: 0x000D90BA File Offset: 0x000D72BA
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<ParkSign>(-832141045, ParkSign.TriggerRoomEffectsDelegate);
	}

	// Token: 0x06002910 RID: 10512 RVA: 0x000D90D4 File Offset: 0x000D72D4
	private void TriggerRoomEffects(object data)
	{
		GameObject gameObject = (GameObject)data;
		Room roomOfGameObject = Game.Instance.roomProber.GetRoomOfGameObject(base.gameObject);
		if (roomOfGameObject != null)
		{
			roomOfGameObject.roomType.TriggerRoomEffects(base.gameObject.GetComponent<KPrefabID>(), gameObject.GetComponent<Effects>());
		}
	}

	// Token: 0x04001828 RID: 6184
	private static readonly EventSystem.IntraObjectHandler<ParkSign> TriggerRoomEffectsDelegate = new EventSystem.IntraObjectHandler<ParkSign>(delegate(ParkSign component, object data)
	{
		component.TriggerRoomEffects(data);
	});
}
