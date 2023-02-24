using System;
using System.Collections.Generic;
using Database;
using UnityEngine;

// Token: 0x02000C18 RID: 3096
public class UIMinion : KMonoBehaviour, UIMinionOrMannequin.ITarget
{
	// Token: 0x170006BC RID: 1724
	// (get) Token: 0x0600620E RID: 25102 RVA: 0x00243D2E File Offset: 0x00241F2E
	public GameObject SpawnedAvatar
	{
		get
		{
			if (this.spawn == null)
			{
				this.TrySpawn();
			}
			return this.spawn;
		}
	}

	// Token: 0x170006BD RID: 1725
	// (get) Token: 0x0600620F RID: 25103 RVA: 0x00243D4A File Offset: 0x00241F4A
	// (set) Token: 0x06006210 RID: 25104 RVA: 0x00243D52 File Offset: 0x00241F52
	public Option<Personality> Personality { get; private set; }

	// Token: 0x06006211 RID: 25105 RVA: 0x00243D5B File Offset: 0x00241F5B
	protected override void OnSpawn()
	{
		this.TrySpawn();
	}

	// Token: 0x06006212 RID: 25106 RVA: 0x00243D64 File Offset: 0x00241F64
	public void TrySpawn()
	{
		if (this.animController == null)
		{
			this.animController = Util.KInstantiateUI(Assets.GetPrefab(MinionUIPortrait.ID), base.gameObject, false).GetComponent<KBatchedAnimController>();
			this.animController.gameObject.SetActive(true);
			this.animController.animScale = 0.38f;
			this.animController.Play("idle_default", KAnim.PlayMode.Loop, 1f, 0f);
			MinionConfig.ConfigureSymbols(this.animController.gameObject, true);
			this.spawn = this.animController.gameObject;
		}
	}

	// Token: 0x06006213 RID: 25107 RVA: 0x00243E0B File Offset: 0x0024200B
	public void SetMinion(Personality personality)
	{
		this.SpawnedAvatar.GetComponent<Accessorizer>().ApplyMinionPersonality(personality);
		this.Personality = personality;
		base.gameObject.AddOrGet<MinionVoiceProviderMB>().voice = MinionVoice.ByPersonality(personality);
	}

	// Token: 0x06006214 RID: 25108 RVA: 0x00243E45 File Offset: 0x00242045
	public void SetOutfit(IEnumerable<ClothingItemResource> outfit)
	{
		this.SpawnedAvatar.GetComponent<WearableAccessorizer>().ApplyClothingItems(outfit);
	}

	// Token: 0x06006215 RID: 25109 RVA: 0x00243E58 File Offset: 0x00242058
	public MinionVoice GetMinionVoice()
	{
		return MinionVoice.ByObject(this.SpawnedAvatar).UnwrapOr(MinionVoice.Random(), null);
	}

	// Token: 0x06006216 RID: 25110 RVA: 0x00243E80 File Offset: 0x00242080
	public void React(UIMinionOrMannequinReactSource source)
	{
		if (source != UIMinionOrMannequinReactSource.OnPersonalityChanged && this.lastReactSource == source)
		{
			KAnim.Anim currentAnim = this.animController.GetCurrentAnim();
			if (currentAnim != null && currentAnim.name != "idle_default")
			{
				return;
			}
		}
		switch (source)
		{
		case UIMinionOrMannequinReactSource.OnPersonalityChanged:
			this.animController.Play("react", KAnim.PlayMode.Once, 1f, 0f);
			goto IL_16C;
		case UIMinionOrMannequinReactSource.OnWholeOutfitChanged:
		case UIMinionOrMannequinReactSource.OnBottomChanged:
			this.animController.Play("react_bottoms", KAnim.PlayMode.Once, 1f, 0f);
			goto IL_16C;
		case UIMinionOrMannequinReactSource.OnTopChanged:
			this.animController.Play("react_tops", KAnim.PlayMode.Once, 1f, 0f);
			goto IL_16C;
		case UIMinionOrMannequinReactSource.OnGlovesChanged:
			this.animController.Play("react_gloves", KAnim.PlayMode.Once, 1f, 0f);
			goto IL_16C;
		case UIMinionOrMannequinReactSource.OnShoesChanged:
			this.animController.Play("react_shoes", KAnim.PlayMode.Once, 1f, 0f);
			goto IL_16C;
		}
		this.animController.Play("cheer_pre", KAnim.PlayMode.Once, 1f, 0f);
		this.animController.Queue("cheer_loop", KAnim.PlayMode.Once, 1f, 0f);
		this.animController.Queue("cheer_pst", KAnim.PlayMode.Once, 1f, 0f);
		IL_16C:
		this.animController.Queue("idle_default", KAnim.PlayMode.Loop, 1f, 0f);
		this.lastReactSource = source;
	}

	// Token: 0x040043D4 RID: 17364
	public const float ANIM_SCALE = 0.38f;

	// Token: 0x040043D5 RID: 17365
	private KBatchedAnimController animController;

	// Token: 0x040043D6 RID: 17366
	private GameObject spawn;

	// Token: 0x040043D8 RID: 17368
	private UIMinionOrMannequinReactSource lastReactSource;
}
