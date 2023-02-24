using System;
using UnityEngine;

namespace Database
{
	// Token: 0x02000C9C RID: 3228
	public class MonumentPartResource : PermitResource
	{
		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x060065A7 RID: 26023 RVA: 0x0026DC6F File Offset: 0x0026BE6F
		// (set) Token: 0x060065A8 RID: 26024 RVA: 0x0026DC77 File Offset: 0x0026BE77
		public KAnimFile AnimFile { get; private set; }

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060065A9 RID: 26025 RVA: 0x0026DC80 File Offset: 0x0026BE80
		// (set) Token: 0x060065AA RID: 26026 RVA: 0x0026DC88 File Offset: 0x0026BE88
		public string SymbolName { get; private set; }

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x060065AB RID: 26027 RVA: 0x0026DC91 File Offset: 0x0026BE91
		// (set) Token: 0x060065AC RID: 26028 RVA: 0x0026DC99 File Offset: 0x0026BE99
		public string State { get; private set; }

		// Token: 0x060065AD RID: 26029 RVA: 0x0026DCA2 File Offset: 0x0026BEA2
		public MonumentPartResource(string id, string animFilename, string state, string symbolName, MonumentPartResource.Part part)
			: base(id, "TODO:DbMonumentParts", "TODO:DbMonumentParts", PermitCategory.Artwork, PermitRarity.Unknown)
		{
			this.AnimFile = Assets.GetAnim(animFilename);
			this.SymbolName = symbolName;
			this.State = state;
			this.part = part;
		}

		// Token: 0x060065AE RID: 26030 RVA: 0x0026DCE0 File Offset: 0x0026BEE0
		public global::Tuple<Sprite, Color> GetUISprite()
		{
			Sprite sprite = Assets.GetSprite("unknown");
			return new global::Tuple<Sprite, Color>(sprite, (sprite != null) ? Color.white : Color.clear);
		}

		// Token: 0x060065AF RID: 26031 RVA: 0x0026DD0C File Offset: 0x0026BF0C
		public override PermitPresentationInfo GetPermitPresentationInfo()
		{
			PermitPresentationInfo permitPresentationInfo = default(PermitPresentationInfo);
			permitPresentationInfo.sprite = this.GetUISprite().first;
			permitPresentationInfo.SetFacadeForText("_monument part");
			return permitPresentationInfo;
		}

		// Token: 0x04004987 RID: 18823
		public MonumentPartResource.Part part;

		// Token: 0x02001B28 RID: 6952
		public enum Part
		{
			// Token: 0x04007AAB RID: 31403
			Bottom,
			// Token: 0x04007AAC RID: 31404
			Middle,
			// Token: 0x04007AAD RID: 31405
			Top
		}
	}
}
