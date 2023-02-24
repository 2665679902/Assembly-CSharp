using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A46 RID: 2630
public class AsteroidClock : MonoBehaviour
{
	// Token: 0x06004FBD RID: 20413 RVA: 0x001C63A4 File Offset: 0x001C45A4
	private void Awake()
	{
		this.UpdateOverlay();
	}

	// Token: 0x06004FBE RID: 20414 RVA: 0x001C63AC File Offset: 0x001C45AC
	private void Start()
	{
	}

	// Token: 0x06004FBF RID: 20415 RVA: 0x001C63AE File Offset: 0x001C45AE
	private void Update()
	{
		if (GameClock.Instance != null)
		{
			this.rotationTransform.rotation = Quaternion.Euler(0f, 0f, 360f * -GameClock.Instance.GetCurrentCycleAsPercentage());
		}
	}

	// Token: 0x06004FC0 RID: 20416 RVA: 0x001C63E8 File Offset: 0x001C45E8
	private void UpdateOverlay()
	{
		float num = 0.125f;
		this.NightOverlay.fillAmount = num;
	}

	// Token: 0x0400356C RID: 13676
	public Transform rotationTransform;

	// Token: 0x0400356D RID: 13677
	public Image NightOverlay;
}
