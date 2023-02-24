using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A2E RID: 2606
public class AlertVignette : KMonoBehaviour
{
	// Token: 0x06004F18 RID: 20248 RVA: 0x001C2441 File Offset: 0x001C0641
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06004F19 RID: 20249 RVA: 0x001C244C File Offset: 0x001C064C
	private void Update()
	{
		Color color = this.image.color;
		if (ClusterManager.Instance.GetWorld(this.worldID) == null)
		{
			color = Color.clear;
			this.image.color = color;
			return;
		}
		if (ClusterManager.Instance.GetWorld(this.worldID).IsRedAlert())
		{
			if (color.r != Vignette.Instance.redAlertColor.r || color.g != Vignette.Instance.redAlertColor.g || color.b != Vignette.Instance.redAlertColor.b)
			{
				color = Vignette.Instance.redAlertColor;
			}
		}
		else if (ClusterManager.Instance.GetWorld(this.worldID).IsYellowAlert())
		{
			if (color.r != Vignette.Instance.yellowAlertColor.r || color.g != Vignette.Instance.yellowAlertColor.g || color.b != Vignette.Instance.yellowAlertColor.b)
			{
				color = Vignette.Instance.yellowAlertColor;
			}
		}
		else
		{
			color = Color.clear;
		}
		if (color != Color.clear)
		{
			color.a = 0.2f + (0.5f + Mathf.Sin(Time.unscaledTime * 4f - 1f) / 2f) * 0.5f;
		}
		if (this.image.color != color)
		{
			this.image.color = color;
		}
	}

	// Token: 0x04003527 RID: 13607
	public Image image;

	// Token: 0x04003528 RID: 13608
	public int worldID;
}
