using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000702 RID: 1794
[Serializable]
public class Def : ScriptableObject
{
	// Token: 0x06003157 RID: 12631 RVA: 0x00106C9E File Offset: 0x00104E9E
	public virtual void InitDef()
	{
		this.Tag = TagManager.Create(this.PrefabID);
	}

	// Token: 0x17000392 RID: 914
	// (get) Token: 0x06003158 RID: 12632 RVA: 0x00106CB1 File Offset: 0x00104EB1
	public virtual string Name
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06003159 RID: 12633 RVA: 0x00106CB4 File Offset: 0x00104EB4
	public static global::Tuple<Sprite, Color> GetUISprite(object item, string animName = "ui", bool centered = false)
	{
		if (item is Substance)
		{
			return Def.GetUISprite(ElementLoader.FindElementByHash((item as Substance).elementID), animName, centered);
		}
		if (item is Element)
		{
			if ((item as Element).IsSolid)
			{
				return new global::Tuple<Sprite, Color>(Def.GetUISpriteFromMultiObjectAnim((item as Element).substance.anim, animName, centered, ""), Color.white);
			}
			if ((item as Element).IsLiquid)
			{
				return new global::Tuple<Sprite, Color>(Assets.GetSprite("element_liquid"), (item as Element).substance.uiColour);
			}
			if ((item as Element).IsGas)
			{
				return new global::Tuple<Sprite, Color>(Assets.GetSprite("element_gas"), (item as Element).substance.uiColour);
			}
			return new global::Tuple<Sprite, Color>(null, Color.clear);
		}
		else
		{
			if (item is AsteroidGridEntity)
			{
				return new global::Tuple<Sprite, Color>(((AsteroidGridEntity)item).GetUISprite(), Color.white);
			}
			if (item is GameObject)
			{
				GameObject gameObject = item as GameObject;
				if (ElementLoader.GetElement(gameObject.PrefabID()) != null)
				{
					return Def.GetUISprite(ElementLoader.GetElement(gameObject.PrefabID()), animName, centered);
				}
				CreatureBrain creatureBrain = gameObject.GetComponent<CreatureBrain>();
				if (creatureBrain != null)
				{
					animName = creatureBrain.symbolPrefix + "ui";
				}
				SpaceArtifact component = gameObject.GetComponent<SpaceArtifact>();
				if (component != null)
				{
					animName = component.GetUIAnim();
				}
				if (gameObject.HasTag(GameTags.Egg))
				{
					IncubationMonitor.Def def = gameObject.GetDef<IncubationMonitor.Def>();
					if (def != null)
					{
						GameObject prefab = Assets.GetPrefab(def.spawnedCreature);
						if (prefab)
						{
							creatureBrain = prefab.GetComponent<CreatureBrain>();
							if (creatureBrain && !string.IsNullOrEmpty(creatureBrain.symbolPrefix))
							{
								animName = creatureBrain.symbolPrefix + animName;
							}
						}
					}
				}
				if (gameObject.HasTag(GameTags.MoltShell))
				{
					animName = gameObject.GetComponent<SimpleMassStatusItem>().symbolPrefix + animName;
				}
				KBatchedAnimController component2 = gameObject.GetComponent<KBatchedAnimController>();
				if (component2)
				{
					Sprite uispriteFromMultiObjectAnim = Def.GetUISpriteFromMultiObjectAnim(component2.AnimFiles[0], animName, centered, "");
					return new global::Tuple<Sprite, Color>(uispriteFromMultiObjectAnim, (uispriteFromMultiObjectAnim != null) ? Color.white : Color.clear);
				}
				if (gameObject.GetComponent<Building>() != null)
				{
					Sprite uisprite = gameObject.GetComponent<Building>().Def.GetUISprite(animName, centered);
					return new global::Tuple<Sprite, Color>(uisprite, (uisprite != null) ? Color.white : Color.clear);
				}
				global::Debug.LogWarningFormat("Can't get sprite for type {0} (no KBatchedAnimController)", new object[] { item.ToString() });
				return new global::Tuple<Sprite, Color>(Assets.GetSprite("unknown"), Color.grey);
			}
			else
			{
				if (!(item is string))
				{
					if (item is Tag)
					{
						if (ElementLoader.GetElement((Tag)item) != null)
						{
							return Def.GetUISprite(ElementLoader.GetElement((Tag)item), animName, centered);
						}
						if (Assets.GetPrefab((Tag)item) != null)
						{
							return Def.GetUISprite(Assets.GetPrefab((Tag)item), animName, centered);
						}
						if (Assets.GetSprite(((Tag)item).Name) != null)
						{
							return new global::Tuple<Sprite, Color>(Assets.GetSprite(((Tag)item).Name), Color.white);
						}
					}
					DebugUtil.DevAssertArgs(false, new object[]
					{
						"Can't get sprite for type ",
						item.ToString()
					});
					return new global::Tuple<Sprite, Color>(Assets.GetSprite("unknown"), Color.grey);
				}
				if (Db.Get().Amounts.Exists(item as string))
				{
					return new global::Tuple<Sprite, Color>(Assets.GetSprite(Db.Get().Amounts.Get(item as string).uiSprite), Color.white);
				}
				if (Db.Get().Attributes.Exists(item as string))
				{
					return new global::Tuple<Sprite, Color>(Assets.GetSprite(Db.Get().Attributes.Get(item as string).uiSprite), Color.white);
				}
				return Def.GetUISprite((item as string).ToTag(), animName, centered);
			}
		}
	}

	// Token: 0x0600315A RID: 12634 RVA: 0x001070C6 File Offset: 0x001052C6
	public static global::Tuple<Sprite, Color> GetUISprite(Tag prefabID, string facadeID)
	{
		if (Assets.GetPrefab(prefabID).GetComponent<Equippable>() != null && !facadeID.IsNullOrWhiteSpace())
		{
			return Db.GetEquippableFacades().Get(facadeID).GetUISprite();
		}
		return Def.GetUISprite(prefabID, "ui", false);
	}

	// Token: 0x0600315B RID: 12635 RVA: 0x00107105 File Offset: 0x00105305
	public static Sprite GetFacadeUISprite(string facadeID)
	{
		return Def.GetUISpriteFromMultiObjectAnim(Assets.GetAnim(Db.GetBuildingFacades().Get(facadeID).AnimFile), "ui", false, "");
	}

	// Token: 0x0600315C RID: 12636 RVA: 0x00107134 File Offset: 0x00105334
	public static Sprite GetUISpriteFromMultiObjectAnim(KAnimFile animFile, string animName = "ui", bool centered = false, string symbolName = "")
	{
		global::Tuple<KAnimFile, string, bool> tuple = new global::Tuple<KAnimFile, string, bool>(animFile, animName, centered);
		if (Def.knownUISprites.ContainsKey(tuple))
		{
			return Def.knownUISprites[tuple];
		}
		if (animFile == null)
		{
			DebugUtil.LogWarningArgs(new object[] { animName, "missing Anim File" });
			return Assets.GetSprite("unknown");
		}
		KAnimFileData data = animFile.GetData();
		if (data == null)
		{
			DebugUtil.LogWarningArgs(new object[] { animName, "KAnimFileData is null" });
			return Assets.GetSprite("unknown");
		}
		if (data.build == null)
		{
			return Assets.GetSprite("unknown");
		}
		KAnim.Anim.Frame frame = KAnim.Anim.Frame.InvalidFrame;
		for (int i = 0; i < data.animCount; i++)
		{
			KAnim.Anim anim = data.GetAnim(i);
			if (anim.name == animName)
			{
				frame = anim.GetFrame(data.batchTag, 0);
			}
		}
		if (!frame.IsValid())
		{
			DebugUtil.LogWarningArgs(new object[] { string.Format("missing '{0}' anim in '{1}'", animName, animFile) });
			return Assets.GetSprite("unknown");
		}
		if (data.elementCount == 0)
		{
			return Assets.GetSprite("unknown");
		}
		KAnim.Anim.FrameElement frameElement = default(KAnim.Anim.FrameElement);
		if (string.IsNullOrEmpty(symbolName))
		{
			symbolName = animName;
		}
		KAnimHashedString kanimHashedString = new KAnimHashedString(symbolName);
		KAnim.Build.Symbol symbol = data.build.GetSymbol(kanimHashedString);
		if (symbol == null)
		{
			DebugUtil.LogWarningArgs(new object[] { animFile.name, animName, "placeSymbol [", frameElement.symbol, "] is missing" });
			return Assets.GetSprite("unknown");
		}
		int frame2 = frameElement.frame;
		KAnim.Build.SymbolFrame symbolFrame = symbol.GetFrame(frame2).symbolFrame;
		if (symbolFrame == null)
		{
			DebugUtil.LogWarningArgs(new object[] { animName, "SymbolFrame [", frameElement.frame, "] is missing" });
			return Assets.GetSprite("unknown");
		}
		Texture2D texture = data.build.GetTexture(0);
		global::Debug.Assert(texture != null, "Invalid texture on " + animFile.name);
		float x = symbolFrame.uvMin.x;
		float x2 = symbolFrame.uvMax.x;
		float y = symbolFrame.uvMax.y;
		float y2 = symbolFrame.uvMin.y;
		int num = (int)((float)texture.width * Mathf.Abs(x2 - x));
		int num2 = (int)((float)texture.height * Mathf.Abs(y2 - y));
		float num3 = Mathf.Abs(symbolFrame.bboxMax.x - symbolFrame.bboxMin.x);
		Rect rect = default(Rect);
		rect.width = (float)num;
		rect.height = (float)num2;
		rect.x = (float)((int)((float)texture.width * x));
		rect.y = (float)((int)((float)texture.height * y));
		float num4 = 100f;
		if (num != 0)
		{
			num4 = 100f / (num3 / (float)num);
		}
		Sprite sprite = Sprite.Create(texture, rect, centered ? new Vector2(0.5f, 0.5f) : Vector2.zero, num4, 0U, SpriteMeshType.FullRect);
		sprite.name = string.Format("{0}:{1}:{2}:{3}", new object[]
		{
			texture.name,
			animName,
			frameElement.frame.ToString(),
			centered
		});
		Def.knownUISprites[tuple] = sprite;
		return sprite;
	}

	// Token: 0x04001E24 RID: 7716
	public string PrefabID;

	// Token: 0x04001E25 RID: 7717
	public Tag Tag;

	// Token: 0x04001E26 RID: 7718
	private static Dictionary<global::Tuple<KAnimFile, string, bool>, Sprite> knownUISprites = new Dictionary<global::Tuple<KAnimFile, string, bool>, Sprite>();

	// Token: 0x04001E27 RID: 7719
	public const string DEFAULT_SPRITE = "unknown";
}
