using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AAF RID: 2735
public class GridLayouter
{
	// Token: 0x060053CC RID: 21452 RVA: 0x001E76E8 File Offset: 0x001E58E8
	[Conditional("UNITY_EDITOR")]
	private void ValidateImportantFieldsAreSet()
	{
		global::Debug.Assert(this.minCellSize >= 0f, string.Format("[{0} Error] Minimum cell size is invalid. Given: {1}", "GridLayouter", this.minCellSize));
		global::Debug.Assert(this.maxCellSize >= 0f, string.Format("[{0} Error] Maximum cell size is invalid. Given: {1}", "GridLayouter", this.maxCellSize));
		global::Debug.Assert(this.targetGridLayout != null && this.targetGridLayout, string.Format("[{0} Error] Target grid layout is invalid. Given: {1}", "GridLayouter", this.targetGridLayout));
	}

	// Token: 0x060053CD RID: 21453 RVA: 0x001E778C File Offset: 0x001E598C
	public void CheckIfShouldResizeGrid()
	{
		Vector2 vector = new Vector2((float)Screen.width, (float)Screen.height);
		if (vector != this.oldScreenSize)
		{
			this.RequestGridResize();
		}
		this.oldScreenSize = vector;
		float @float = KPlayerPrefs.GetFloat(KCanvasScaler.UIScalePrefKey);
		if (@float != this.oldScreenScale)
		{
			this.RequestGridResize();
		}
		this.oldScreenScale = @float;
		this.ResizeGridIfRequested();
	}

	// Token: 0x060053CE RID: 21454 RVA: 0x001E77EE File Offset: 0x001E59EE
	public void RequestGridResize()
	{
		this.framesLeftToResizeGrid = 3;
	}

	// Token: 0x060053CF RID: 21455 RVA: 0x001E77F7 File Offset: 0x001E59F7
	private void ResizeGridIfRequested()
	{
		if (this.framesLeftToResizeGrid > 0)
		{
			this.ImmediateSizeGridToScreenResolution();
			this.framesLeftToResizeGrid--;
		}
	}

	// Token: 0x060053D0 RID: 21456 RVA: 0x001E7818 File Offset: 0x001E5A18
	public void ImmediateSizeGridToScreenResolution()
	{
		float num = this.targetGridLayout.transform.parent.rectTransform().rect.size.x - (float)this.targetGridLayout.padding.left - (float)this.targetGridLayout.padding.right;
		float x = this.targetGridLayout.spacing.x;
		int num2 = GridLayouter.<ImmediateSizeGridToScreenResolution>g__GetCellCountToFit|10_1(this.maxCellSize, x, num) + 1;
		float num3;
		for (num3 = GridLayouter.<ImmediateSizeGridToScreenResolution>g__GetCellSize|10_0(num, x, num2); num3 < this.minCellSize; num3 = Mathf.Min(this.maxCellSize, GridLayouter.<ImmediateSizeGridToScreenResolution>g__GetCellSize|10_0(num, x, num2)))
		{
			num2--;
			if (num2 <= 0)
			{
				num2 = 1;
				num3 = this.minCellSize;
				break;
			}
		}
		this.targetGridLayout.childAlignment = ((num2 == 1) ? TextAnchor.UpperCenter : TextAnchor.UpperLeft);
		this.targetGridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
		this.targetGridLayout.constraintCount = num2;
		this.targetGridLayout.cellSize = Vector2.one * num3;
	}

	// Token: 0x060053D2 RID: 21458 RVA: 0x001E792D File Offset: 0x001E5B2D
	[CompilerGenerated]
	internal static float <ImmediateSizeGridToScreenResolution>g__GetCellSize|10_0(float workingWidth, float spacingSize, int count)
	{
		return (workingWidth - (spacingSize * (float)count - 1f)) / (float)count;
	}

	// Token: 0x060053D3 RID: 21459 RVA: 0x001E7940 File Offset: 0x001E5B40
	[CompilerGenerated]
	internal static int <ImmediateSizeGridToScreenResolution>g__GetCellCountToFit|10_1(float cellSize, float spacingSize, float workingWidth)
	{
		int num = 0;
		for (float num2 = cellSize; num2 < workingWidth; num2 += cellSize + spacingSize)
		{
			num++;
		}
		return num;
	}

	// Token: 0x040038E9 RID: 14569
	public float minCellSize = -1f;

	// Token: 0x040038EA RID: 14570
	public float maxCellSize = -1f;

	// Token: 0x040038EB RID: 14571
	public GridLayoutGroup targetGridLayout;

	// Token: 0x040038EC RID: 14572
	private Vector2 oldScreenSize;

	// Token: 0x040038ED RID: 14573
	private float oldScreenScale;

	// Token: 0x040038EE RID: 14574
	private int framesLeftToResizeGrid;
}
