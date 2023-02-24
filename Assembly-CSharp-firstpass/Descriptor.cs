using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000094 RID: 148
[DebuggerDisplay("{text}")]
public struct Descriptor
{
	// Token: 0x060005A7 RID: 1447 RVA: 0x0001AB0F File Offset: 0x00018D0F
	public Descriptor(string txt, string tooltip, Descriptor.DescriptorType descriptorType = Descriptor.DescriptorType.Effect, bool only_for_simple_info_screen = false)
	{
		this.indent = 0;
		this.text = txt;
		this.tooltipText = tooltip;
		this.type = descriptorType;
		this.onlyForSimpleInfoScreen = only_for_simple_info_screen;
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x0001AB35 File Offset: 0x00018D35
	public void SetupDescriptor(string txt, string tooltip, Descriptor.DescriptorType descriptorType = Descriptor.DescriptorType.Effect)
	{
		this.text = txt;
		this.tooltipText = tooltip;
		this.type = descriptorType;
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x0001AB4C File Offset: 0x00018D4C
	public Descriptor IncreaseIndent()
	{
		this.indent++;
		return this;
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x0001AB62 File Offset: 0x00018D62
	public Descriptor DecreaseIndent()
	{
		this.indent = Mathf.Max(this.indent - 1, 0);
		return this;
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x0001AB80 File Offset: 0x00018D80
	public string IndentedText()
	{
		string text = this.text;
		for (int i = 0; i < this.indent; i++)
		{
			text = "    " + text;
		}
		return text;
	}

	// Token: 0x04000578 RID: 1400
	public string text;

	// Token: 0x04000579 RID: 1401
	public string tooltipText;

	// Token: 0x0400057A RID: 1402
	public int indent;

	// Token: 0x0400057B RID: 1403
	public Descriptor.DescriptorType type;

	// Token: 0x0400057C RID: 1404
	public bool onlyForSimpleInfoScreen;

	// Token: 0x020009D5 RID: 2517
	public enum DescriptorType
	{
		// Token: 0x04002206 RID: 8710
		Requirement,
		// Token: 0x04002207 RID: 8711
		Effect,
		// Token: 0x04002208 RID: 8712
		Lifecycle,
		// Token: 0x04002209 RID: 8713
		Information,
		// Token: 0x0400220A RID: 8714
		DiseaseSource,
		// Token: 0x0400220B RID: 8715
		Detail,
		// Token: 0x0400220C RID: 8716
		Symptom,
		// Token: 0x0400220D RID: 8717
		SymptomAidable
	}
}
