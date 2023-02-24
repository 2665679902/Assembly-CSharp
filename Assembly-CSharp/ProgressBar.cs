using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B62 RID: 2914
[AddComponentMenu("KMonoBehaviour/scripts/ProgressBar")]
public class ProgressBar : KMonoBehaviour
{
	// Token: 0x1700066F RID: 1647
	// (get) Token: 0x06005AE7 RID: 23271 RVA: 0x00210352 File Offset: 0x0020E552
	// (set) Token: 0x06005AE8 RID: 23272 RVA: 0x0021035F File Offset: 0x0020E55F
	public Color barColor
	{
		get
		{
			return this.bar.color;
		}
		set
		{
			this.bar.color = value;
		}
	}

	// Token: 0x17000670 RID: 1648
	// (get) Token: 0x06005AE9 RID: 23273 RVA: 0x0021036D File Offset: 0x0020E56D
	// (set) Token: 0x06005AEA RID: 23274 RVA: 0x0021037A File Offset: 0x0020E57A
	public float PercentFull
	{
		get
		{
			return this.bar.fillAmount;
		}
		set
		{
			this.bar.fillAmount = value;
		}
	}

	// Token: 0x06005AEB RID: 23275 RVA: 0x00210388 File Offset: 0x0020E588
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.autoHide)
		{
			this.overlayUpdateHandle = Game.Instance.Subscribe(1798162660, new Action<object>(this.OnOverlayChanged));
			if (OverlayScreen.Instance != null && OverlayScreen.Instance.GetMode() != OverlayModes.None.ID)
			{
				base.gameObject.SetActive(false);
			}
		}
		Game.Instance.Subscribe(1983128072, new Action<object>(this.OnActiveWorldChanged));
		this.SetWorldActive(ClusterManager.Instance.activeWorldId);
		base.enabled = this.updatePercentFull != null;
	}

	// Token: 0x06005AEC RID: 23276 RVA: 0x00210430 File Offset: 0x0020E630
	private void OnActiveWorldChanged(object data)
	{
		global::Tuple<int, int> tuple = (global::Tuple<int, int>)data;
		this.SetWorldActive(tuple.first);
	}

	// Token: 0x06005AED RID: 23277 RVA: 0x00210450 File Offset: 0x0020E650
	private void SetWorldActive(int worldId)
	{
		base.gameObject.SetActive(this.GetMyWorldId() == worldId);
		if (this.updatePercentFull == null || this.updatePercentFull.Target.IsNullOrDestroyed())
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06005AEE RID: 23278 RVA: 0x0021048C File Offset: 0x0020E68C
	public void SetUpdateFunc(Func<float> func)
	{
		this.updatePercentFull = func;
		base.enabled = this.updatePercentFull != null;
	}

	// Token: 0x06005AEF RID: 23279 RVA: 0x002104A4 File Offset: 0x0020E6A4
	public virtual void Update()
	{
		if (this.updatePercentFull != null && !this.updatePercentFull.Target.IsNullOrDestroyed())
		{
			this.PercentFull = this.updatePercentFull();
		}
	}

	// Token: 0x06005AF0 RID: 23280 RVA: 0x002104D4 File Offset: 0x0020E6D4
	public virtual void OnOverlayChanged(object data = null)
	{
		if (!this.autoHide)
		{
			return;
		}
		if ((HashedString)data == OverlayModes.None.ID)
		{
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
				return;
			}
		}
		else if (base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06005AF1 RID: 23281 RVA: 0x00210530 File Offset: 0x0020E730
	public void Retarget(GameObject entity)
	{
		Vector3 vector = entity.transform.GetPosition() + Vector3.down * 0.5f;
		Building component = entity.GetComponent<Building>();
		if (component != null)
		{
			vector -= Vector3.right * 0.5f * (float)(component.Def.WidthInCells % 2);
		}
		else
		{
			vector -= Vector3.right * 0.5f;
		}
		base.transform.SetPosition(vector);
	}

	// Token: 0x06005AF2 RID: 23282 RVA: 0x002105BB File Offset: 0x0020E7BB
	protected override void OnCleanUp()
	{
		if (this.overlayUpdateHandle != -1)
		{
			Game.Instance.Unsubscribe(this.overlayUpdateHandle);
		}
		Game.Instance.Unsubscribe(1983128072, new Action<object>(this.OnActiveWorldChanged));
		base.OnCleanUp();
	}

	// Token: 0x06005AF3 RID: 23283 RVA: 0x002105F7 File Offset: 0x0020E7F7
	private void OnBecameInvisible()
	{
		base.enabled = false;
	}

	// Token: 0x06005AF4 RID: 23284 RVA: 0x00210600 File Offset: 0x0020E800
	private void OnBecameVisible()
	{
		base.enabled = true;
	}

	// Token: 0x06005AF5 RID: 23285 RVA: 0x0021060C File Offset: 0x0020E80C
	public static ProgressBar CreateProgressBar(GameObject entity, Func<float> updateFunc)
	{
		ProgressBar progressBar = Util.KInstantiateUI<ProgressBar>(ProgressBarsConfig.Instance.progressBarPrefab, null, false);
		progressBar.SetUpdateFunc(updateFunc);
		progressBar.transform.SetParent(GameScreenManager.Instance.worldSpaceCanvas.transform);
		progressBar.name = ((entity != null) ? (entity.name + "_") : "") + " ProgressBar";
		progressBar.transform.Find("Bar").GetComponent<Image>().color = ProgressBarsConfig.Instance.GetBarColor("ProgressBar");
		progressBar.Update();
		progressBar.Retarget(entity);
		return progressBar;
	}

	// Token: 0x04003D9C RID: 15772
	public Image bar;

	// Token: 0x04003D9D RID: 15773
	private Func<float> updatePercentFull;

	// Token: 0x04003D9E RID: 15774
	private int overlayUpdateHandle = -1;

	// Token: 0x04003D9F RID: 15775
	public bool autoHide = true;
}
