using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

namespace Database
{
	// Token: 0x02000C89 RID: 3209
	public class ClothingItemResource : PermitResource
	{
		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x0600655F RID: 25951 RVA: 0x0026755E File Offset: 0x0026575E
		// (set) Token: 0x06006560 RID: 25952 RVA: 0x00267566 File Offset: 0x00265766
		public string animFilename { get; private set; }

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06006561 RID: 25953 RVA: 0x0026756F File Offset: 0x0026576F
		// (set) Token: 0x06006562 RID: 25954 RVA: 0x00267577 File Offset: 0x00265777
		public KAnimFile AnimFile { get; private set; }

		// Token: 0x06006563 RID: 25955 RVA: 0x00267580 File Offset: 0x00265780
		public ClothingItemResource(string id, string name, string desc, PermitCategory category, PermitRarity rarity, string animFile)
			: base(id, name, desc, category, rarity)
		{
			this.AnimFile = Assets.GetAnim(animFile);
			this.animFilename = animFile;
		}

		// Token: 0x06006564 RID: 25956 RVA: 0x002675AC File Offset: 0x002657AC
		public global::Tuple<Sprite, Color> GetUISprite()
		{
			if (this.AnimFile == null)
			{
				global::Debug.LogError("Clothing AnimFile is null: " + this.animFilename);
			}
			Sprite uisprite = ClothingItemResource.GetUISprite(this.AnimFile);
			return new global::Tuple<Sprite, Color>(uisprite, (uisprite != null) ? Color.white : Color.clear);
		}

		// Token: 0x06006565 RID: 25957 RVA: 0x00267604 File Offset: 0x00265804
		public override PermitPresentationInfo GetPermitPresentationInfo()
		{
			PermitPresentationInfo permitPresentationInfo = default(PermitPresentationInfo);
			permitPresentationInfo.sprite = this.GetUISprite().first;
			permitPresentationInfo.SetFacadeForText(UI.KLEI_INVENTORY_SCREEN.CLOTHING_ITEM_FACADE_FOR);
			return permitPresentationInfo;
		}

		// Token: 0x06006566 RID: 25958 RVA: 0x00267640 File Offset: 0x00265840
		public static Sprite GetUISprite(KAnimFile animFile)
		{
			if (ClothingItemResource.knownNoSpriteAvailble.Contains(animFile))
			{
				return Assets.GetSprite("unknown");
			}
			Sprite sprite;
			if (ClothingItemResource.knownUISprites.TryGetValue(animFile, out sprite))
			{
				return sprite;
			}
			Option<Sprite> option = ClothingItemResource.MaybeGenerateUISprite(animFile);
			if (option.HasValue)
			{
				ClothingItemResource.knownUISprites.Add(animFile, option.Value);
				return option.Value;
			}
			ClothingItemResource.knownNoSpriteAvailble.Add(animFile);
			return Assets.GetSprite("unknown");
		}

		// Token: 0x06006567 RID: 25959 RVA: 0x002676C0 File Offset: 0x002658C0
		public static Option<Sprite> MaybeGenerateUISprite(KAnimFile animFile)
		{
			if (animFile == null)
			{
				return default(Option<Sprite>);
			}
			if (animFile.GetData() == null)
			{
				return default(Option<Sprite>);
			}
			KAnim.Build build = animFile.GetData().build;
			if (build.textureCount == 0)
			{
				return default(Option<Sprite>);
			}
			Texture2D texture = build.GetTexture(0);
			if (texture == null || !texture)
			{
				return default(Option<Sprite>);
			}
			KAnim.Build.Symbol symbol = build.GetSymbol("ui");
			if (symbol == null)
			{
				return default(Option<Sprite>);
			}
			int firstFrameIdx = symbol.firstFrameIdx;
			if (firstFrameIdx < 0 || firstFrameIdx >= build.frames.Length)
			{
				return default(Option<Sprite>);
			}
			KAnim.Build.SymbolFrame symbolFrame = build.frames[firstFrameIdx];
			float x = symbolFrame.uvMin.x;
			float x2 = symbolFrame.uvMax.x;
			float y = symbolFrame.uvMax.y;
			float y2 = symbolFrame.uvMin.y;
			Rect rect = new Rect
			{
				x = (float)((int)((float)texture.width * x)),
				y = (float)((int)((float)texture.height * y)),
				width = (float)((int)((float)texture.width * Mathf.Abs(x2 - x))),
				height = (float)((int)((float)texture.height * Mathf.Abs(y2 - y)))
			};
			float num = 100f;
			if (rect.width != 0f)
			{
				float num2 = Mathf.Abs(symbolFrame.bboxMax.x - symbolFrame.bboxMin.x);
				num = 100f / (num2 / rect.width);
			}
			Sprite sprite = Sprite.Create(texture, rect, Vector2.zero, num, 0U, SpriteMeshType.FullRect);
			sprite.name = string.Format("{0}:{1}:{2}:{3}", new object[] { texture.name, animFile.name, symbolFrame.sourceFrameNum, false });
			return sprite;
		}

		// Token: 0x04004813 RID: 18451
		private static Dictionary<KAnimFile, Sprite> knownUISprites = new Dictionary<KAnimFile, Sprite>();

		// Token: 0x04004814 RID: 18452
		private static HashSet<KAnimFile> knownNoSpriteAvailble = new HashSet<KAnimFile>();
	}
}
