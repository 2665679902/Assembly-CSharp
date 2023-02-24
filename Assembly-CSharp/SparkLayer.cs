using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AAE RID: 2734
public class SparkLayer : LineLayer
{
	// Token: 0x060053C5 RID: 21445 RVA: 0x001E7264 File Offset: 0x001E5464
	public void SetColor(ColonyDiagnostic.DiagnosticResult result)
	{
		switch (result.opinion)
		{
		case ColonyDiagnostic.DiagnosticResult.Opinion.DuplicantThreatening:
		case ColonyDiagnostic.DiagnosticResult.Opinion.Bad:
			this.SetColor(Constants.NEGATIVE_COLOR);
			return;
		case ColonyDiagnostic.DiagnosticResult.Opinion.Warning:
		case ColonyDiagnostic.DiagnosticResult.Opinion.Concern:
			this.SetColor(Constants.WARNING_COLOR);
			return;
		case ColonyDiagnostic.DiagnosticResult.Opinion.Suggestion:
		case ColonyDiagnostic.DiagnosticResult.Opinion.Tutorial:
		case ColonyDiagnostic.DiagnosticResult.Opinion.Normal:
			this.SetColor(Constants.NEUTRAL_COLOR);
			return;
		case ColonyDiagnostic.DiagnosticResult.Opinion.Good:
			this.SetColor(Constants.POSITIVE_COLOR);
			return;
		default:
			this.SetColor(Constants.NEUTRAL_COLOR);
			return;
		}
	}

	// Token: 0x060053C6 RID: 21446 RVA: 0x001E72DD File Offset: 0x001E54DD
	public void SetColor(Color color)
	{
		this.line_formatting[0].color = color;
	}

	// Token: 0x060053C7 RID: 21447 RVA: 0x001E72F4 File Offset: 0x001E54F4
	public override GraphedLine NewLine(global::Tuple<float, float>[] points, string ID = "")
	{
		Color positive_COLOR = Constants.POSITIVE_COLOR;
		Color neutral_COLOR = Constants.NEUTRAL_COLOR;
		Color negative_COLOR = Constants.NEGATIVE_COLOR;
		if (this.colorRules.setOwnColor)
		{
			if (points.Length > 2)
			{
				if (this.colorRules.zeroIsBad && points[points.Length - 1].second == 0f)
				{
					this.line_formatting[0].color = negative_COLOR;
				}
				else if (points[points.Length - 1].second > points[points.Length - 2].second)
				{
					this.line_formatting[0].color = (this.colorRules.positiveIsGood ? positive_COLOR : negative_COLOR);
				}
				else if (points[points.Length - 1].second < points[points.Length - 2].second)
				{
					this.line_formatting[0].color = (this.colorRules.positiveIsGood ? negative_COLOR : positive_COLOR);
				}
				else
				{
					this.line_formatting[0].color = neutral_COLOR;
				}
			}
			else
			{
				this.line_formatting[0].color = neutral_COLOR;
			}
		}
		this.ScaleToData(points);
		if (this.subZeroAreaFill != null)
		{
			this.subZeroAreaFill.color = new Color(this.line_formatting[0].color.r, this.line_formatting[0].color.g, this.line_formatting[0].color.b, this.fillAlphaMin);
		}
		return base.NewLine(points, ID);
	}

	// Token: 0x060053C8 RID: 21448 RVA: 0x001E747A File Offset: 0x001E567A
	public override void RefreshLine(global::Tuple<float, float>[] points, string ID)
	{
		this.SetColor(points);
		this.ScaleToData(points);
		base.RefreshLine(points, ID);
	}

	// Token: 0x060053C9 RID: 21449 RVA: 0x001E7494 File Offset: 0x001E5694
	private void SetColor(global::Tuple<float, float>[] points)
	{
		Color positive_COLOR = Constants.POSITIVE_COLOR;
		Color neutral_COLOR = Constants.NEUTRAL_COLOR;
		Color negative_COLOR = Constants.NEGATIVE_COLOR;
		if (this.colorRules.setOwnColor)
		{
			if (points.Length > 2)
			{
				if (this.colorRules.zeroIsBad && points[points.Length - 1].second == 0f)
				{
					this.line_formatting[0].color = negative_COLOR;
				}
				else if (points[points.Length - 1].second > points[points.Length - 2].second)
				{
					this.line_formatting[0].color = (this.colorRules.positiveIsGood ? positive_COLOR : negative_COLOR);
				}
				else if (points[points.Length - 1].second < points[points.Length - 2].second)
				{
					this.line_formatting[0].color = (this.colorRules.positiveIsGood ? negative_COLOR : positive_COLOR);
				}
				else
				{
					this.line_formatting[0].color = neutral_COLOR;
				}
			}
			else
			{
				this.line_formatting[0].color = neutral_COLOR;
			}
		}
		if (this.subZeroAreaFill != null)
		{
			this.subZeroAreaFill.color = new Color(this.line_formatting[0].color.r, this.line_formatting[0].color.g, this.line_formatting[0].color.b, this.fillAlphaMin);
		}
	}

	// Token: 0x060053CA RID: 21450 RVA: 0x001E760C File Offset: 0x001E580C
	private void ScaleToData(global::Tuple<float, float>[] points)
	{
		if (this.scaleWidthToData || this.scaleHeightToData)
		{
			Vector2 vector = base.CalculateMin(points);
			Vector2 vector2 = base.CalculateMax(points);
			if (this.scaleHeightToData)
			{
				base.graph.ClearHorizontalGuides();
				base.graph.axis_y.max_value = vector2.y;
				base.graph.axis_y.min_value = vector.y;
				base.graph.RefreshHorizontalGuides();
			}
			if (this.scaleWidthToData)
			{
				base.graph.ClearVerticalGuides();
				base.graph.axis_x.max_value = vector2.x;
				base.graph.axis_x.min_value = vector.x;
				base.graph.RefreshVerticalGuides();
			}
		}
	}

	// Token: 0x040038E4 RID: 14564
	public Image subZeroAreaFill;

	// Token: 0x040038E5 RID: 14565
	public SparkLayer.ColorRules colorRules;

	// Token: 0x040038E6 RID: 14566
	public bool debugMark;

	// Token: 0x040038E7 RID: 14567
	public bool scaleHeightToData = true;

	// Token: 0x040038E8 RID: 14568
	public bool scaleWidthToData = true;

	// Token: 0x02001932 RID: 6450
	[Serializable]
	public struct ColorRules
	{
		// Token: 0x0400739C RID: 29596
		public bool setOwnColor;

		// Token: 0x0400739D RID: 29597
		public bool positiveIsGood;

		// Token: 0x0400739E RID: 29598
		public bool zeroIsBad;
	}
}
