using System;
using System.Collections.Generic;
using Database;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C1A RID: 3098
public class UIMinionOrMannequin : KMonoBehaviour
{
	// Token: 0x170006BE RID: 1726
	// (get) Token: 0x06006218 RID: 25112 RVA: 0x00244028 File Offset: 0x00242228
	// (set) Token: 0x06006219 RID: 25113 RVA: 0x00244030 File Offset: 0x00242230
	public UIMinionOrMannequin.ITarget current { get; private set; }

	// Token: 0x0600621A RID: 25114 RVA: 0x00244039 File Offset: 0x00242239
	protected override void OnSpawn()
	{
		this.TrySpawn();
	}

	// Token: 0x0600621B RID: 25115 RVA: 0x00244044 File Offset: 0x00242244
	public bool TrySpawn()
	{
		bool flag = false;
		if (this.mannequin.IsNullOrDestroyed())
		{
			GameObject gameObject = new GameObject("UIMannequin");
			gameObject.AddOrGet<RectTransform>().Fill(Padding.All(10f));
			gameObject.transform.SetParent(base.transform, false);
			AspectRatioFitter aspectRatioFitter = gameObject.AddOrGet<AspectRatioFitter>();
			aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
			aspectRatioFitter.aspectRatio = 1f;
			this.mannequin = gameObject.AddOrGet<UIMannequin>();
			this.mannequin.TrySpawn();
			gameObject.SetActive(false);
			flag = true;
		}
		if (this.minion.IsNullOrDestroyed())
		{
			GameObject gameObject2 = new GameObject("UIMinion");
			gameObject2.AddOrGet<RectTransform>().Fill(Padding.All(10f));
			gameObject2.transform.SetParent(base.transform, false);
			AspectRatioFitter aspectRatioFitter2 = gameObject2.AddOrGet<AspectRatioFitter>();
			aspectRatioFitter2.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
			aspectRatioFitter2.aspectRatio = 1f;
			this.minion = gameObject2.AddOrGet<UIMinion>();
			this.minion.TrySpawn();
			gameObject2.SetActive(false);
			flag = true;
		}
		if (flag)
		{
			this.SetAsMannequin();
		}
		return flag;
	}

	// Token: 0x0600621C RID: 25116 RVA: 0x0024414C File Offset: 0x0024234C
	public UIMinionOrMannequin.ITarget SetFrom(Option<Personality> personality)
	{
		if (personality.IsSome())
		{
			return this.SetAsMinion(personality.Unwrap());
		}
		return this.SetAsMannequin();
	}

	// Token: 0x0600621D RID: 25117 RVA: 0x0024416C File Offset: 0x0024236C
	public UIMinion SetAsMinion(Personality personality)
	{
		this.mannequin.gameObject.SetActive(false);
		this.minion.gameObject.SetActive(true);
		this.minion.SetMinion(personality);
		this.current = this.minion;
		return this.minion;
	}

	// Token: 0x0600621E RID: 25118 RVA: 0x002441B9 File Offset: 0x002423B9
	public UIMannequin SetAsMannequin()
	{
		this.minion.gameObject.SetActive(false);
		this.mannequin.gameObject.SetActive(true);
		this.current = this.mannequin;
		return this.mannequin;
	}

	// Token: 0x0600621F RID: 25119 RVA: 0x002441F0 File Offset: 0x002423F0
	public MinionVoice GetMinionVoice()
	{
		return MinionVoice.ByObject(this.current.SpawnedAvatar).UnwrapOr(MinionVoice.Random(), null);
	}

	// Token: 0x040043E1 RID: 17377
	private UIMinion minion;

	// Token: 0x040043E2 RID: 17378
	private UIMannequin mannequin;

	// Token: 0x02001AAD RID: 6829
	public interface ITarget
	{
		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x060093CD RID: 37837
		GameObject SpawnedAvatar { get; }

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x060093CE RID: 37838
		Option<Personality> Personality { get; }

		// Token: 0x060093CF RID: 37839
		void SetOutfit(IEnumerable<ClothingItemResource> clothingItems);

		// Token: 0x060093D0 RID: 37840
		void React(UIMinionOrMannequinReactSource source);
	}
}
