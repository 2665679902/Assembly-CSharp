using System;

// Token: 0x02000099 RID: 153
internal class OilFloaterMovementSound : KMonoBehaviour
{
	// Token: 0x060002A6 RID: 678 RVA: 0x000150F0 File Offset: 0x000132F0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.sound = GlobalAssets.GetSound(this.sound, false);
		base.Subscribe<OilFloaterMovementSound>(1027377649, OilFloaterMovementSound.OnObjectMovementStateChangedDelegate);
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new System.Action(this.OnCellChanged), "OilFloaterMovementSound");
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x00015148 File Offset: 0x00013348
	private void OnObjectMovementStateChanged(object data)
	{
		GameHashes gameHashes = (GameHashes)data;
		this.isMoving = gameHashes == GameHashes.ObjectMovementWakeUp;
		this.UpdateSound();
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x00015170 File Offset: 0x00013370
	private void OnCellChanged()
	{
		this.UpdateSound();
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x00015178 File Offset: 0x00013378
	private void UpdateSound()
	{
		bool flag = this.isMoving && base.GetComponent<Navigator>().CurrentNavType != NavType.Swim;
		if (flag == this.isPlayingSound)
		{
			return;
		}
		LoopingSounds component = base.GetComponent<LoopingSounds>();
		if (flag)
		{
			component.StartSound(this.sound);
		}
		else
		{
			component.StopSound(this.sound);
		}
		this.isPlayingSound = flag;
	}

	// Token: 0x060002AA RID: 682 RVA: 0x000151D8 File Offset: 0x000133D8
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new System.Action(this.OnCellChanged));
	}

	// Token: 0x040001B1 RID: 433
	public string sound;

	// Token: 0x040001B2 RID: 434
	public bool isPlayingSound;

	// Token: 0x040001B3 RID: 435
	public bool isMoving;

	// Token: 0x040001B4 RID: 436
	private static readonly EventSystem.IntraObjectHandler<OilFloaterMovementSound> OnObjectMovementStateChangedDelegate = new EventSystem.IntraObjectHandler<OilFloaterMovementSound>(delegate(OilFloaterMovementSound component, object data)
	{
		component.OnObjectMovementStateChanged(data);
	});
}
