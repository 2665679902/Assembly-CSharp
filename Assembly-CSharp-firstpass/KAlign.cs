using System;
using UnityEngine;

// Token: 0x02000055 RID: 85
[ExecuteInEditMode]
public class KAlign : MonoBehaviour
{
	// Token: 0x06000352 RID: 850 RVA: 0x00011A08 File Offset: 0x0000FC08
	public void SetTarget(GameObject newtarget)
	{
		this.target = newtarget;
		this.Update();
	}

	// Token: 0x06000353 RID: 851 RVA: 0x00011A17 File Offset: 0x0000FC17
	private void OnEnable()
	{
		this.Update();
	}

	// Token: 0x06000354 RID: 852 RVA: 0x00011A20 File Offset: 0x0000FC20
	private void Update()
	{
		if (this.target != null)
		{
			RectTransform rectTransform = this.target.rectTransform();
			if (rectTransform != null)
			{
				Vector3[] array = new Vector3[4];
				rectTransform.GetWorldCorners(array);
				Vector3 vector = array[1];
				Vector3 vector2 = array[3];
				Vector3 position = base.transform.GetPosition();
				Vector3[] array2 = new Vector3[4];
				this.rectTransform().GetWorldCorners(array2);
				Vector3 vector3 = array2[1];
				Vector3 vector4 = array2[3];
				float num = position.x;
				float num2 = position.y;
				if (this.targetHorizontal != KAlign.TargetLeftRight.None)
				{
					num = this.offset.x;
					if (this.sourceHorizontal == KAlign.SourceLeftRight.Left)
					{
						num += position.x - vector3.x;
					}
					else if (this.sourceHorizontal == KAlign.SourceLeftRight.Right)
					{
						num += position.x - vector4.x;
					}
					else if (this.sourceHorizontal == KAlign.SourceLeftRight.Middle)
					{
						num += vector3.x - position.x + (vector4.x - vector3.x) / 2f;
					}
					if (this.targetHorizontal == KAlign.TargetLeftRight.Right)
					{
						num += vector2.x;
					}
					else if (this.targetHorizontal == KAlign.TargetLeftRight.Left)
					{
						num += vector.x;
					}
					else if (this.targetHorizontal == KAlign.TargetLeftRight.Middle)
					{
						num += vector.x + (vector2.x - vector.x) / 2f;
					}
				}
				if (this.targetVertical != KAlign.TargetTopBottom.None)
				{
					num2 = this.offset.y;
					if (this.sourceVertical == KAlign.SourceTopBottom.Top)
					{
						num2 += position.y - vector3.y;
					}
					else if (this.sourceVertical == KAlign.SourceTopBottom.Bottom)
					{
						num2 += position.y - vector4.y;
					}
					else if (this.sourceVertical == KAlign.SourceTopBottom.Middle)
					{
						num2 += position.y - vector3.y + (vector3.y - vector4.y) / 2f;
					}
					if (this.targetVertical == KAlign.TargetTopBottom.Top)
					{
						num2 += vector.y;
					}
					else if (this.targetVertical == KAlign.TargetTopBottom.Bottom)
					{
						num2 += vector2.y;
					}
					else if (this.targetVertical == KAlign.TargetTopBottom.Middle)
					{
						num2 += vector2.y + (vector.y - vector2.y) / 2f;
					}
				}
				position.x = num;
				position.y = num2;
				base.transform.SetPosition(position);
			}
		}
	}

	// Token: 0x04000404 RID: 1028
	public GameObject target;

	// Token: 0x04000405 RID: 1029
	public KAlign.SourceLeftRight sourceHorizontal;

	// Token: 0x04000406 RID: 1030
	public KAlign.SourceTopBottom sourceVertical;

	// Token: 0x04000407 RID: 1031
	public KAlign.TargetLeftRight targetHorizontal;

	// Token: 0x04000408 RID: 1032
	public KAlign.TargetTopBottom targetVertical;

	// Token: 0x04000409 RID: 1033
	public Vector2 offset;

	// Token: 0x020009A8 RID: 2472
	public enum TargetLeftRight
	{
		// Token: 0x04002180 RID: 8576
		None,
		// Token: 0x04002181 RID: 8577
		Left,
		// Token: 0x04002182 RID: 8578
		Middle,
		// Token: 0x04002183 RID: 8579
		Right
	}

	// Token: 0x020009A9 RID: 2473
	public enum TargetTopBottom
	{
		// Token: 0x04002185 RID: 8581
		None,
		// Token: 0x04002186 RID: 8582
		Top,
		// Token: 0x04002187 RID: 8583
		Middle,
		// Token: 0x04002188 RID: 8584
		Bottom
	}

	// Token: 0x020009AA RID: 2474
	public enum SourceLeftRight
	{
		// Token: 0x0400218A RID: 8586
		Left,
		// Token: 0x0400218B RID: 8587
		Middle,
		// Token: 0x0400218C RID: 8588
		Right
	}

	// Token: 0x020009AB RID: 2475
	public enum SourceTopBottom
	{
		// Token: 0x0400218E RID: 8590
		Top,
		// Token: 0x0400218F RID: 8591
		Middle,
		// Token: 0x04002190 RID: 8592
		Bottom
	}
}
