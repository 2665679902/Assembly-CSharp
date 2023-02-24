using System;
using UnityEngine;

// Token: 0x02000B5E RID: 2910
public class PopIn : MonoBehaviour
{
	// Token: 0x06005ABA RID: 23226 RVA: 0x0020EF34 File Offset: 0x0020D134
	private void OnEnable()
	{
		this.StartPopIn(true);
	}

	// Token: 0x06005ABB RID: 23227 RVA: 0x0020EF40 File Offset: 0x0020D140
	private void Update()
	{
		float num = Mathf.Lerp(base.transform.localScale.x, this.targetScale, Time.unscaledDeltaTime * this.speed);
		base.transform.localScale = new Vector3(num, num, 1f);
	}

	// Token: 0x06005ABC RID: 23228 RVA: 0x0020EF8C File Offset: 0x0020D18C
	public void StartPopIn(bool force_reset = false)
	{
		if (force_reset)
		{
			base.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
		}
		this.targetScale = 1f;
	}

	// Token: 0x06005ABD RID: 23229 RVA: 0x0020EFBB File Offset: 0x0020D1BB
	public void StartPopOut()
	{
		this.targetScale = 0f;
	}

	// Token: 0x04003D73 RID: 15731
	private float targetScale;

	// Token: 0x04003D74 RID: 15732
	public float speed;
}
