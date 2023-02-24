using System;
using UnityEngine;

// Token: 0x02000B64 RID: 2916
public class QuickLayout : KMonoBehaviour
{
	// Token: 0x06005AFE RID: 23294 RVA: 0x0021081C File Offset: 0x0020EA1C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.ForceUpdate();
	}

	// Token: 0x06005AFF RID: 23295 RVA: 0x0021082A File Offset: 0x0020EA2A
	private void OnEnable()
	{
		this.ForceUpdate();
	}

	// Token: 0x06005B00 RID: 23296 RVA: 0x00210832 File Offset: 0x0020EA32
	private void LateUpdate()
	{
		this.Run(false);
	}

	// Token: 0x06005B01 RID: 23297 RVA: 0x0021083B File Offset: 0x0020EA3B
	public void ForceUpdate()
	{
		this.Run(true);
	}

	// Token: 0x06005B02 RID: 23298 RVA: 0x00210844 File Offset: 0x0020EA44
	private void Run(bool forceUpdate = false)
	{
		forceUpdate = forceUpdate || this._elementSize != this.elementSize;
		forceUpdate = forceUpdate || this._spacing != this.spacing;
		forceUpdate = forceUpdate || this._layoutDirection != this.layoutDirection;
		forceUpdate = forceUpdate || this._offset != this.offset;
		if (forceUpdate)
		{
			this._elementSize = this.elementSize;
			this._spacing = this.spacing;
			this._layoutDirection = this.layoutDirection;
			this._offset = this.offset;
		}
		int num = 0;
		for (int i = 0; i < base.transform.childCount; i++)
		{
			if (base.transform.GetChild(i).gameObject.activeInHierarchy)
			{
				num++;
			}
		}
		if (num != this.oldActiveChildCount || forceUpdate)
		{
			this.Layout();
			this.oldActiveChildCount = num;
		}
	}

	// Token: 0x06005B03 RID: 23299 RVA: 0x0021093C File Offset: 0x0020EB3C
	public void Layout()
	{
		Vector3 vector = this._offset;
		bool flag = false;
		for (int i = 0; i < base.transform.childCount; i++)
		{
			if (base.transform.GetChild(i).gameObject.activeInHierarchy)
			{
				flag = true;
				base.transform.GetChild(i).rectTransform().anchoredPosition = vector;
				vector += (float)(this._elementSize + this._spacing) * this.GetDirectionVector();
			}
		}
		if (this.driveParentRectSize != null)
		{
			if (!flag)
			{
				if (this._layoutDirection == QuickLayout.LayoutDirection.BottomToTop || this._layoutDirection == QuickLayout.LayoutDirection.TopToBottom)
				{
					this.driveParentRectSize.sizeDelta = new Vector2(Mathf.Abs(this.driveParentRectSize.sizeDelta.x), 0f);
					return;
				}
				if (this._layoutDirection == QuickLayout.LayoutDirection.LeftToRight || this._layoutDirection == QuickLayout.LayoutDirection.LeftToRight)
				{
					this.driveParentRectSize.sizeDelta = new Vector2(0f, Mathf.Abs(this.driveParentRectSize.sizeDelta.y));
					return;
				}
			}
			else
			{
				if (this._layoutDirection == QuickLayout.LayoutDirection.BottomToTop || this._layoutDirection == QuickLayout.LayoutDirection.TopToBottom)
				{
					this.driveParentRectSize.sizeDelta = new Vector2(this.driveParentRectSize.sizeDelta.x, Mathf.Abs(vector.y));
					return;
				}
				if (this._layoutDirection == QuickLayout.LayoutDirection.LeftToRight || this._layoutDirection == QuickLayout.LayoutDirection.LeftToRight)
				{
					this.driveParentRectSize.sizeDelta = new Vector2(Mathf.Abs(vector.x), this.driveParentRectSize.sizeDelta.y);
				}
			}
		}
	}

	// Token: 0x06005B04 RID: 23300 RVA: 0x00210AD4 File Offset: 0x0020ECD4
	private Vector2 GetDirectionVector()
	{
		Vector2 vector = Vector3.zero;
		switch (this._layoutDirection)
		{
		case QuickLayout.LayoutDirection.TopToBottom:
			vector = Vector2.down;
			break;
		case QuickLayout.LayoutDirection.BottomToTop:
			vector = Vector2.up;
			break;
		case QuickLayout.LayoutDirection.LeftToRight:
			vector = Vector2.right;
			break;
		case QuickLayout.LayoutDirection.RightToLeft:
			vector = Vector2.left;
			break;
		}
		return vector;
	}

	// Token: 0x04003DA6 RID: 15782
	[Header("Configuration")]
	[SerializeField]
	private int elementSize;

	// Token: 0x04003DA7 RID: 15783
	[SerializeField]
	private int spacing;

	// Token: 0x04003DA8 RID: 15784
	[SerializeField]
	private QuickLayout.LayoutDirection layoutDirection;

	// Token: 0x04003DA9 RID: 15785
	[SerializeField]
	private Vector2 offset;

	// Token: 0x04003DAA RID: 15786
	[SerializeField]
	private RectTransform driveParentRectSize;

	// Token: 0x04003DAB RID: 15787
	private int _elementSize;

	// Token: 0x04003DAC RID: 15788
	private int _spacing;

	// Token: 0x04003DAD RID: 15789
	private QuickLayout.LayoutDirection _layoutDirection;

	// Token: 0x04003DAE RID: 15790
	private Vector2 _offset;

	// Token: 0x04003DAF RID: 15791
	private int oldActiveChildCount;

	// Token: 0x02001A0A RID: 6666
	private enum LayoutDirection
	{
		// Token: 0x04007650 RID: 30288
		TopToBottom,
		// Token: 0x04007651 RID: 30289
		BottomToTop,
		// Token: 0x04007652 RID: 30290
		LeftToRight,
		// Token: 0x04007653 RID: 30291
		RightToLeft
	}
}
