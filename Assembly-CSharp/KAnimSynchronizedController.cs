using System;
using UnityEngine;

// Token: 0x02000418 RID: 1048
public class KAnimSynchronizedController
{
	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x06001637 RID: 5687 RVA: 0x000727F4 File Offset: 0x000709F4
	// (set) Token: 0x06001638 RID: 5688 RVA: 0x000727FC File Offset: 0x000709FC
	public string Postfix
	{
		get
		{
			return this.postfix;
		}
		set
		{
			this.postfix = value;
		}
	}

	// Token: 0x06001639 RID: 5689 RVA: 0x00072808 File Offset: 0x00070A08
	public KAnimSynchronizedController(KAnimControllerBase controller, Grid.SceneLayer layer, string postfix)
	{
		this.controller = controller;
		this.Postfix = postfix;
		GameObject gameObject = Util.KInstantiate(EntityPrefabs.Instance.ForegroundLayer, controller.gameObject, null);
		gameObject.name = controller.name + postfix;
		this.synchronizedController = gameObject.GetComponent<KAnimControllerBase>();
		this.synchronizedController.AnimFiles = controller.AnimFiles;
		gameObject.SetActive(true);
		this.synchronizedController.initialAnim = controller.initialAnim + postfix;
		this.synchronizedController.defaultAnim = this.synchronizedController.initialAnim;
		Vector3 vector = new Vector3(0f, 0f, Grid.GetLayerZ(layer) - 0.1f);
		gameObject.transform.SetLocalPosition(vector);
		this.link = new KAnimLink(controller, this.synchronizedController);
		this.Dirty();
		KAnimSynchronizer synchronizer = controller.GetSynchronizer();
		synchronizer.Add(this);
		synchronizer.SyncController(this);
	}

	// Token: 0x0600163A RID: 5690 RVA: 0x000728F8 File Offset: 0x00070AF8
	public void Enable(bool enable)
	{
		this.synchronizedController.enabled = enable;
	}

	// Token: 0x0600163B RID: 5691 RVA: 0x00072906 File Offset: 0x00070B06
	public void Play(HashedString anim_name, KAnim.PlayMode mode = KAnim.PlayMode.Once, float speed = 1f, float time_offset = 0f)
	{
		if (this.synchronizedController.enabled && this.synchronizedController.HasAnimation(anim_name))
		{
			this.synchronizedController.Play(anim_name, mode, speed, time_offset);
		}
	}

	// Token: 0x0600163C RID: 5692 RVA: 0x00072934 File Offset: 0x00070B34
	public void Dirty()
	{
		if (this.synchronizedController == null)
		{
			return;
		}
		this.synchronizedController.Offset = this.controller.Offset;
		this.synchronizedController.Pivot = this.controller.Pivot;
		this.synchronizedController.Rotation = this.controller.Rotation;
		this.synchronizedController.FlipX = this.controller.FlipX;
		this.synchronizedController.FlipY = this.controller.FlipY;
	}

	// Token: 0x04000C5C RID: 3164
	private KAnimControllerBase controller;

	// Token: 0x04000C5D RID: 3165
	public KAnimControllerBase synchronizedController;

	// Token: 0x04000C5E RID: 3166
	private KAnimLink link;

	// Token: 0x04000C5F RID: 3167
	private string postfix;
}
