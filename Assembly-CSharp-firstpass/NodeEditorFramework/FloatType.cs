using System;
using UnityEngine;

namespace NodeEditorFramework
{
	// Token: 0x02000475 RID: 1141
	public class FloatType : IConnectionTypeDeclaration
	{
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06003110 RID: 12560 RVA: 0x00061A5B File Offset: 0x0005FC5B
		public string Identifier
		{
			get
			{
				return "Float";
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06003111 RID: 12561 RVA: 0x00061A62 File Offset: 0x0005FC62
		public Type Type
		{
			get
			{
				return typeof(float);
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06003112 RID: 12562 RVA: 0x00061A6E File Offset: 0x0005FC6E
		public Color Color
		{
			get
			{
				return Color.cyan;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06003113 RID: 12563 RVA: 0x00061A75 File Offset: 0x0005FC75
		public string InKnobTex
		{
			get
			{
				return "Textures/In_Knob.png";
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06003114 RID: 12564 RVA: 0x00061A7C File Offset: 0x0005FC7C
		public string OutKnobTex
		{
			get
			{
				return "Textures/Out_Knob.png";
			}
		}
	}
}
