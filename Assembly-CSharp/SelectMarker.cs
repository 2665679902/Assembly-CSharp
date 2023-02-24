using System;
using UnityEngine;

// Token: 0x020007E0 RID: 2016
[AddComponentMenu("KMonoBehaviour/scripts/SelectMarker")]
public class SelectMarker : KMonoBehaviour
{
	// Token: 0x06003A04 RID: 14852 RVA: 0x00140C1D File Offset: 0x0013EE1D
	public void SetTargetTransform(Transform target_transform)
	{
		this.targetTransform = target_transform;
		this.LateUpdate();
	}

	// Token: 0x06003A05 RID: 14853 RVA: 0x00140C2C File Offset: 0x0013EE2C
	private void LateUpdate()
	{
		if (this.targetTransform == null)
		{
			base.gameObject.SetActive(false);
			return;
		}
		Vector3 position = this.targetTransform.GetPosition();
		KCollider2D component = this.targetTransform.GetComponent<KCollider2D>();
		if (component != null)
		{
			position.x = component.bounds.center.x;
			position.y = component.bounds.center.y + component.bounds.size.y / 2f + 0.1f;
		}
		else
		{
			position.y += 2f;
		}
		Vector3 vector = new Vector3(0f, (Mathf.Sin(Time.unscaledTime * 4f) + 1f) * this.animationOffset, 0f);
		base.transform.SetPosition(position + vector);
	}

	// Token: 0x04002622 RID: 9762
	public float animationOffset = 0.1f;

	// Token: 0x04002623 RID: 9763
	private Transform targetTransform;
}
