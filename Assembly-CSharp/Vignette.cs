using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C24 RID: 3108
public class Vignette : KMonoBehaviour
{
	// Token: 0x06006263 RID: 25187 RVA: 0x0024516F File Offset: 0x0024336F
	public static void DestroyInstance()
	{
		Vignette.Instance = null;
	}

	// Token: 0x06006264 RID: 25188 RVA: 0x00245178 File Offset: 0x00243378
	protected override void OnSpawn()
	{
		this.looping_sounds = base.GetComponent<LoopingSounds>();
		base.OnSpawn();
		Vignette.Instance = this;
		this.defaultColor = this.image.color;
		Game.Instance.Subscribe(1983128072, new Action<object>(this.Refresh));
		Game.Instance.Subscribe(1585324898, new Action<object>(this.Refresh));
		Game.Instance.Subscribe(-1393151672, new Action<object>(this.Refresh));
		Game.Instance.Subscribe(-741654735, new Action<object>(this.Refresh));
		Game.Instance.Subscribe(-2062778933, new Action<object>(this.Refresh));
	}

	// Token: 0x06006265 RID: 25189 RVA: 0x0024523A File Offset: 0x0024343A
	public void SetColor(Color color)
	{
		this.image.color = color;
	}

	// Token: 0x06006266 RID: 25190 RVA: 0x00245248 File Offset: 0x00243448
	public void Refresh(object data)
	{
		AlertStateManager.Instance alertManager = ClusterManager.Instance.activeWorld.AlertManager;
		if (alertManager == null)
		{
			return;
		}
		if (alertManager.IsYellowAlert())
		{
			this.SetColor(this.yellowAlertColor);
			if (!this.showingYellowAlert)
			{
				this.looping_sounds.StartSound(GlobalAssets.GetSound("YellowAlert_LP", false), true, false, true);
				this.showingYellowAlert = true;
			}
		}
		else
		{
			this.showingYellowAlert = false;
			this.looping_sounds.StopSound(GlobalAssets.GetSound("YellowAlert_LP", false));
		}
		if (alertManager.IsRedAlert())
		{
			this.SetColor(this.redAlertColor);
			if (!this.showingRedAlert)
			{
				this.looping_sounds.StartSound(GlobalAssets.GetSound("RedAlert_LP", false), true, false, true);
				this.showingRedAlert = true;
			}
		}
		else
		{
			this.showingRedAlert = false;
			this.looping_sounds.StopSound(GlobalAssets.GetSound("RedAlert_LP", false));
		}
		if (!this.showingRedAlert && !this.showingYellowAlert)
		{
			this.Reset();
		}
	}

	// Token: 0x06006267 RID: 25191 RVA: 0x00245338 File Offset: 0x00243538
	public void Reset()
	{
		this.SetColor(this.defaultColor);
		this.showingRedAlert = false;
		this.showingYellowAlert = false;
		this.looping_sounds.StopSound(GlobalAssets.GetSound("RedAlert_LP", false));
		this.looping_sounds.StopSound(GlobalAssets.GetSound("YellowAlert_LP", false));
	}

	// Token: 0x04004413 RID: 17427
	[SerializeField]
	private Image image;

	// Token: 0x04004414 RID: 17428
	public Color defaultColor;

	// Token: 0x04004415 RID: 17429
	public Color redAlertColor = new Color(1f, 0f, 0f, 0.3f);

	// Token: 0x04004416 RID: 17430
	public Color yellowAlertColor = new Color(1f, 1f, 0f, 0.3f);

	// Token: 0x04004417 RID: 17431
	public static Vignette Instance;

	// Token: 0x04004418 RID: 17432
	private LoopingSounds looping_sounds;

	// Token: 0x04004419 RID: 17433
	private bool showingRedAlert;

	// Token: 0x0400441A RID: 17434
	private bool showingYellowAlert;
}
