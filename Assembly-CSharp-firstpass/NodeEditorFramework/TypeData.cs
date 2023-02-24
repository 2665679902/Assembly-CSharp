using System;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace NodeEditorFramework
{
	// Token: 0x02000473 RID: 1139
	public class TypeData
	{
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060030FE RID: 12542 RVA: 0x0006187C File Offset: 0x0005FA7C
		// (set) Token: 0x060030FF RID: 12543 RVA: 0x00061884 File Offset: 0x0005FA84
		public string Identifier { get; private set; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06003100 RID: 12544 RVA: 0x0006188D File Offset: 0x0005FA8D
		// (set) Token: 0x06003101 RID: 12545 RVA: 0x00061895 File Offset: 0x0005FA95
		public Type Type { get; private set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06003102 RID: 12546 RVA: 0x0006189E File Offset: 0x0005FA9E
		// (set) Token: 0x06003103 RID: 12547 RVA: 0x000618A6 File Offset: 0x0005FAA6
		public Color Color { get; private set; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06003104 RID: 12548 RVA: 0x000618AF File Offset: 0x0005FAAF
		// (set) Token: 0x06003105 RID: 12549 RVA: 0x000618B7 File Offset: 0x0005FAB7
		public Texture2D InKnobTex { get; private set; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06003106 RID: 12550 RVA: 0x000618C0 File Offset: 0x0005FAC0
		// (set) Token: 0x06003107 RID: 12551 RVA: 0x000618C8 File Offset: 0x0005FAC8
		public Texture2D OutKnobTex { get; private set; }

		// Token: 0x06003108 RID: 12552 RVA: 0x000618D4 File Offset: 0x0005FAD4
		internal TypeData(IConnectionTypeDeclaration typeDecl)
		{
			this.Identifier = typeDecl.Identifier;
			this.declaration = typeDecl;
			this.Type = this.declaration.Type;
			this.Color = this.declaration.Color;
			this.InKnobTex = ResourceManager.GetTintedTexture(this.declaration.InKnobTex, this.Color);
			this.OutKnobTex = ResourceManager.GetTintedTexture(this.declaration.OutKnobTex, this.Color);
			if (!this.isValid())
			{
				throw new DataMisalignedException("Type Declaration " + typeDecl.Identifier + " contains invalid data!");
			}
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x00061978 File Offset: 0x0005FB78
		public TypeData(Type type)
		{
			this.Identifier = type.Name;
			this.declaration = null;
			this.Type = type;
			this.Color = Color.white;
			byte[] bytes = BitConverter.GetBytes(type.GetHashCode());
			this.Color = new Color(Mathf.Pow((float)bytes[0] / 255f, 0.5f), Mathf.Pow((float)bytes[1] / 255f, 0.5f), Mathf.Pow((float)bytes[2] / 255f, 0.5f));
			this.InKnobTex = ResourceManager.GetTintedTexture("Textures/In_Knob.png", this.Color);
			this.OutKnobTex = ResourceManager.GetTintedTexture("Textures/Out_Knob.png", this.Color);
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x00061A2F File Offset: 0x0005FC2F
		public bool isValid()
		{
			return this.Type != null && this.InKnobTex != null && this.OutKnobTex != null;
		}

		// Token: 0x040010EE RID: 4334
		private IConnectionTypeDeclaration declaration;
	}
}
