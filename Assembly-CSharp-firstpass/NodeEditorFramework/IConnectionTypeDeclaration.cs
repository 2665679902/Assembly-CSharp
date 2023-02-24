using System;
using UnityEngine;

namespace NodeEditorFramework
{
	// Token: 0x02000474 RID: 1140
	public interface IConnectionTypeDeclaration
	{
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600310B RID: 12555
		string Identifier { get; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x0600310C RID: 12556
		Type Type { get; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x0600310D RID: 12557
		Color Color { get; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x0600310E RID: 12558
		string InKnobTex { get; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x0600310F RID: 12559
		string OutKnobTex { get; }
	}
}
