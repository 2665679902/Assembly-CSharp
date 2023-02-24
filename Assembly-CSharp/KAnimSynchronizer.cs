using System;
using System.Collections.Generic;

// Token: 0x02000419 RID: 1049
public class KAnimSynchronizer
{
	// Token: 0x0600163D RID: 5693 RVA: 0x000729BE File Offset: 0x00070BBE
	public KAnimSynchronizer(KAnimControllerBase master_controller)
	{
		this.masterController = master_controller;
	}

	// Token: 0x0600163E RID: 5694 RVA: 0x000729E3 File Offset: 0x00070BE3
	private void Clear(KAnimControllerBase controller)
	{
		controller.Play(controller.defaultAnim, KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x0600163F RID: 5695 RVA: 0x00072A01 File Offset: 0x00070C01
	public void Add(KAnimControllerBase controller)
	{
		this.Targets.Add(controller);
	}

	// Token: 0x06001640 RID: 5696 RVA: 0x00072A0F File Offset: 0x00070C0F
	public void Remove(KAnimControllerBase controller)
	{
		this.Clear(controller);
		this.Targets.Remove(controller);
	}

	// Token: 0x06001641 RID: 5697 RVA: 0x00072A25 File Offset: 0x00070C25
	private void Clear(KAnimSynchronizedController controller)
	{
		controller.Play(controller.synchronizedController.defaultAnim, KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x06001642 RID: 5698 RVA: 0x00072A48 File Offset: 0x00070C48
	public void Add(KAnimSynchronizedController controller)
	{
		this.SyncedControllers.Add(controller);
	}

	// Token: 0x06001643 RID: 5699 RVA: 0x00072A56 File Offset: 0x00070C56
	public void Remove(KAnimSynchronizedController controller)
	{
		this.Clear(controller);
		this.SyncedControllers.Remove(controller);
	}

	// Token: 0x06001644 RID: 5700 RVA: 0x00072A6C File Offset: 0x00070C6C
	public void Clear()
	{
		foreach (KAnimControllerBase kanimControllerBase in this.Targets)
		{
			if (!(kanimControllerBase == null) && kanimControllerBase.AnimFiles != null)
			{
				this.Clear(kanimControllerBase);
			}
		}
		this.Targets.Clear();
		foreach (KAnimSynchronizedController kanimSynchronizedController in this.SyncedControllers)
		{
			if (!(kanimSynchronizedController.synchronizedController == null) && kanimSynchronizedController.synchronizedController.AnimFiles != null)
			{
				this.Clear(kanimSynchronizedController);
			}
		}
		this.SyncedControllers.Clear();
	}

	// Token: 0x06001645 RID: 5701 RVA: 0x00072B44 File Offset: 0x00070D44
	public void Sync(KAnimControllerBase controller)
	{
		if (this.masterController == null)
		{
			return;
		}
		if (controller == null)
		{
			return;
		}
		KAnim.Anim currentAnim = this.masterController.GetCurrentAnim();
		if (currentAnim != null && !string.IsNullOrEmpty(controller.defaultAnim) && !controller.HasAnimation(currentAnim.name))
		{
			controller.Play(controller.defaultAnim, KAnim.PlayMode.Loop, 1f, 0f);
			return;
		}
		if (currentAnim == null)
		{
			return;
		}
		KAnim.PlayMode mode = this.masterController.GetMode();
		float playSpeed = this.masterController.GetPlaySpeed();
		float elapsedTime = this.masterController.GetElapsedTime();
		controller.Play(currentAnim.name, mode, playSpeed, elapsedTime);
		Facing component = controller.GetComponent<Facing>();
		if (component != null)
		{
			float num = component.transform.GetPosition().x;
			num += (this.masterController.FlipX ? (-0.5f) : 0.5f);
			component.Face(num);
			return;
		}
		controller.FlipX = this.masterController.FlipX;
		controller.FlipY = this.masterController.FlipY;
	}

	// Token: 0x06001646 RID: 5702 RVA: 0x00072C64 File Offset: 0x00070E64
	public void SyncController(KAnimSynchronizedController controller)
	{
		if (this.masterController == null)
		{
			return;
		}
		if (controller == null)
		{
			return;
		}
		KAnim.Anim currentAnim = this.masterController.GetCurrentAnim();
		string text = ((currentAnim != null) ? (currentAnim.name + controller.Postfix) : string.Empty);
		if (!string.IsNullOrEmpty(controller.synchronizedController.defaultAnim) && !controller.synchronizedController.HasAnimation(text))
		{
			controller.Play(controller.synchronizedController.defaultAnim, KAnim.PlayMode.Loop, 1f, 0f);
			return;
		}
		if (currentAnim == null)
		{
			return;
		}
		KAnim.PlayMode mode = this.masterController.GetMode();
		float playSpeed = this.masterController.GetPlaySpeed();
		float elapsedTime = this.masterController.GetElapsedTime();
		controller.Play(text, mode, playSpeed, elapsedTime);
		Facing component = controller.synchronizedController.GetComponent<Facing>();
		if (component != null)
		{
			float num = component.transform.GetPosition().x;
			num += (this.masterController.FlipX ? (-0.5f) : 0.5f);
			component.Face(num);
			return;
		}
		controller.synchronizedController.FlipX = this.masterController.FlipX;
		controller.synchronizedController.FlipY = this.masterController.FlipY;
	}

	// Token: 0x06001647 RID: 5703 RVA: 0x00072DAC File Offset: 0x00070FAC
	public void Sync()
	{
		for (int i = 0; i < this.Targets.Count; i++)
		{
			KAnimControllerBase kanimControllerBase = this.Targets[i];
			this.Sync(kanimControllerBase);
		}
		for (int j = 0; j < this.SyncedControllers.Count; j++)
		{
			KAnimSynchronizedController kanimSynchronizedController = this.SyncedControllers[j];
			this.SyncController(kanimSynchronizedController);
		}
	}

	// Token: 0x06001648 RID: 5704 RVA: 0x00072E10 File Offset: 0x00071010
	public void SyncTime()
	{
		float elapsedTime = this.masterController.GetElapsedTime();
		for (int i = 0; i < this.Targets.Count; i++)
		{
			this.Targets[i].SetElapsedTime(elapsedTime);
		}
		for (int j = 0; j < this.SyncedControllers.Count; j++)
		{
			this.SyncedControllers[j].synchronizedController.SetElapsedTime(elapsedTime);
		}
	}

	// Token: 0x04000C60 RID: 3168
	private KAnimControllerBase masterController;

	// Token: 0x04000C61 RID: 3169
	private List<KAnimControllerBase> Targets = new List<KAnimControllerBase>();

	// Token: 0x04000C62 RID: 3170
	private List<KAnimSynchronizedController> SyncedControllers = new List<KAnimSynchronizedController>();
}
