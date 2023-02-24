using System;
using UnityEngine;

// Token: 0x02000066 RID: 102
[ExecuteInEditMode]
public class KSnap : MonoBehaviour
{
	// Token: 0x06000435 RID: 1077 RVA: 0x0001536A File Offset: 0x0001356A
	public void SetTarget(GameObject newtarget)
	{
		this.target = newtarget;
		this.Update();
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x0001537C File Offset: 0x0001357C
	private void Update()
	{
		if (this.target != null)
		{
			RectTransform rectTransform = this.target.rectTransform();
			if (rectTransform != null)
			{
				rectTransform.GetWorldCorners(this.corners);
				Vector3 vector = this.corners[2];
				Vector3 vector2 = this.corners[0];
				Vector3 position = base.transform.GetPosition();
				if (this.horizontal == KSnap.LeftRight.Left)
				{
					position.x = vector2.x + this.offset.x;
				}
				else if (this.horizontal == KSnap.LeftRight.Right)
				{
					position.x = vector.x + this.offset.x;
				}
				else if (this.horizontal == KSnap.LeftRight.Middle)
				{
					position.x = vector.x + (vector2.x - vector.x) / 2f + this.offset.x;
				}
				if (this.vertical == KSnap.TopBottom.Top)
				{
					position.y = vector.y + this.offset.y;
				}
				else if (this.vertical == KSnap.TopBottom.Bottom)
				{
					position.y = vector2.y + this.offset.y;
				}
				else if (this.vertical == KSnap.TopBottom.Middle)
				{
					position.y = vector2.y + (vector.y - vector2.y) / 2f + this.offset.y;
				}
				base.transform.SetPosition(position);
				this.KeepOnScreen();
			}
		}
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x000154F4 File Offset: 0x000136F4
	private void KeepOnScreen()
	{
		if (!this.keepOnScreen)
		{
			return;
		}
		base.transform.rectTransform().GetWorldCorners(this.corners);
		Vector3 zero = Vector3.zero;
		foreach (Vector3 vector in this.corners)
		{
			if (vector.x < 0f)
			{
				zero.x = Mathf.Max(zero.x, -vector.x);
			}
			if (vector.x > (float)Screen.width)
			{
				zero.x = Mathf.Min(zero.x, (float)Screen.width - vector.x);
			}
			if (vector.y < 0f)
			{
				zero.y = Mathf.Max(zero.y, -vector.y);
			}
			if (vector.y > (float)Screen.height)
			{
				zero.y = Mathf.Min(zero.y, (float)Screen.height - vector.y);
			}
		}
		base.transform.SetPosition(base.transform.GetPosition() + zero);
	}

	// Token: 0x0400049B RID: 1179
	public GameObject target;

	// Token: 0x0400049C RID: 1180
	public KSnap.LeftRight horizontal;

	// Token: 0x0400049D RID: 1181
	public KSnap.TopBottom vertical;

	// Token: 0x0400049E RID: 1182
	public Vector2 offset;

	// Token: 0x0400049F RID: 1183
	[SerializeField]
	private bool keepOnScreen;

	// Token: 0x040004A0 RID: 1184
	private Vector3[] corners = new Vector3[4];

	// Token: 0x020009B7 RID: 2487
	public enum LeftRight
	{
		// Token: 0x040021B8 RID: 8632
		None,
		// Token: 0x040021B9 RID: 8633
		Left,
		// Token: 0x040021BA RID: 8634
		Middle,
		// Token: 0x040021BB RID: 8635
		Right
	}

	// Token: 0x020009B8 RID: 2488
	public enum TopBottom
	{
		// Token: 0x040021BD RID: 8637
		None,
		// Token: 0x040021BE RID: 8638
		Top,
		// Token: 0x040021BF RID: 8639
		Middle,
		// Token: 0x040021C0 RID: 8640
		Bottom
	}
}
