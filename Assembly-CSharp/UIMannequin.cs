using System;
using System.Collections.Generic;
using Database;
using UnityEngine;

// Token: 0x02000C17 RID: 3095
public class UIMannequin : KMonoBehaviour, UIMinionOrMannequin.ITarget
{
	// Token: 0x170006BA RID: 1722
	// (get) Token: 0x06006207 RID: 25095 RVA: 0x00243B48 File Offset: 0x00241D48
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

	// Token: 0x170006BB RID: 1723
	// (get) Token: 0x06006208 RID: 25096 RVA: 0x00243B64 File Offset: 0x00241D64
	public Option<Personality> Personality
	{
		get
		{
			return default(Option<Personality>);
		}
	}

	// Token: 0x06006209 RID: 25097 RVA: 0x00243B7A File Offset: 0x00241D7A
	protected override void OnSpawn()
	{
		this.TrySpawn();
	}

	// Token: 0x0600620A RID: 25098 RVA: 0x00243B84 File Offset: 0x00241D84
	public void TrySpawn()
	{
		if (this.animController == null)
		{
			this.animController = Util.KInstantiateUI(Assets.GetPrefab(MannequinUIPortrait.ID), base.gameObject, false).GetComponent<KBatchedAnimController>();
			this.animController.gameObject.SetActive(true);
			this.animController.animScale = 0.38f;
			this.animController.Play("idle", KAnim.PlayMode.Paused, 1f, 0f);
			this.spawn = this.animController.gameObject;
			MinionConfig.ConfigureSymbols(this.spawn, false);
			base.gameObject.AddOrGet<MinionVoiceProviderMB>().voice = Option.None;
		}
	}

	// Token: 0x0600620B RID: 25099 RVA: 0x00243C40 File Offset: 0x00241E40
	public void SetOutfit(IEnumerable<ClothingItemResource> outfit)
	{
		MinionConfig.ConfigureSymbols(this.SpawnedAvatar, false);
		SymbolOverrideController component = this.SpawnedAvatar.GetComponent<SymbolOverrideController>();
		foreach (ClothingItemResource clothingItemResource in outfit)
		{
			KAnim.Build build = clothingItemResource.AnimFile.GetData().build;
			if (build != null)
			{
				for (int i = 0; i < build.symbols.Length; i++)
				{
					string text = HashCache.Get().Get(build.symbols[i].hash);
					component.AddSymbolOverride(text, build.symbols[i], 0);
					this.animController.SetSymbolVisiblity(text, true);
				}
			}
		}
	}

	// Token: 0x0600620C RID: 25100 RVA: 0x00243D04 File Offset: 0x00241F04
	public void React(UIMinionOrMannequinReactSource source)
	{
		this.animController.Play("idle", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x040043D1 RID: 17361
	public const float ANIM_SCALE = 0.38f;

	// Token: 0x040043D2 RID: 17362
	private KBatchedAnimController animController;

	// Token: 0x040043D3 RID: 17363
	private GameObject spawn;
}
