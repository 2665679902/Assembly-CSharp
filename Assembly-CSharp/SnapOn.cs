using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004CE RID: 1230
[AddComponentMenu("KMonoBehaviour/scripts/SnapOn")]
public class SnapOn : KMonoBehaviour
{
	// Token: 0x06001C81 RID: 7297 RVA: 0x00097DD2 File Offset: 0x00095FD2
	protected override void OnPrefabInit()
	{
		this.kanimController = base.GetComponent<KAnimControllerBase>();
	}

	// Token: 0x06001C82 RID: 7298 RVA: 0x00097DE0 File Offset: 0x00095FE0
	protected override void OnSpawn()
	{
		foreach (SnapOn.SnapPoint snapPoint in this.snapPoints)
		{
			if (snapPoint.automatic)
			{
				this.DoAttachSnapOn(snapPoint);
			}
		}
	}

	// Token: 0x06001C83 RID: 7299 RVA: 0x00097E3C File Offset: 0x0009603C
	public void AttachSnapOnByName(string name)
	{
		foreach (SnapOn.SnapPoint snapPoint in this.snapPoints)
		{
			if (snapPoint.pointName == name)
			{
				HashedString context = base.GetComponent<AnimEventHandler>().GetContext();
				if (!context.IsValid || !snapPoint.context.IsValid || context == snapPoint.context)
				{
					this.DoAttachSnapOn(snapPoint);
				}
			}
		}
	}

	// Token: 0x06001C84 RID: 7300 RVA: 0x00097ED0 File Offset: 0x000960D0
	public void DetachSnapOnByName(string name)
	{
		foreach (SnapOn.SnapPoint snapPoint in this.snapPoints)
		{
			if (snapPoint.pointName == name)
			{
				HashedString context = base.GetComponent<AnimEventHandler>().GetContext();
				if (!context.IsValid || !snapPoint.context.IsValid || context == snapPoint.context)
				{
					base.GetComponent<SymbolOverrideController>().RemoveSymbolOverride(snapPoint.overrideSymbol, 5);
					this.kanimController.SetSymbolVisiblity(snapPoint.overrideSymbol, false);
					break;
				}
			}
		}
	}

	// Token: 0x06001C85 RID: 7301 RVA: 0x00097F88 File Offset: 0x00096188
	private void DoAttachSnapOn(SnapOn.SnapPoint point)
	{
		SnapOn.OverrideEntry overrideEntry = null;
		KAnimFile kanimFile = point.buildFile;
		string text = "";
		if (this.overrideMap.TryGetValue(point.pointName, out overrideEntry))
		{
			kanimFile = overrideEntry.buildFile;
			text = overrideEntry.symbolName;
		}
		KAnim.Build.Symbol symbol = SnapOn.GetSymbol(kanimFile, text);
		base.GetComponent<SymbolOverrideController>().AddSymbolOverride(point.overrideSymbol, symbol, 5);
		this.kanimController.SetSymbolVisiblity(point.overrideSymbol, true);
	}

	// Token: 0x06001C86 RID: 7302 RVA: 0x00097FFC File Offset: 0x000961FC
	private static KAnim.Build.Symbol GetSymbol(KAnimFile anim_file, string symbol_name)
	{
		KAnim.Build.Symbol symbol = anim_file.GetData().build.symbols[0];
		KAnimHashedString kanimHashedString = new KAnimHashedString(symbol_name);
		foreach (KAnim.Build.Symbol symbol2 in anim_file.GetData().build.symbols)
		{
			if (symbol2.hash == kanimHashedString)
			{
				symbol = symbol2;
				break;
			}
		}
		return symbol;
	}

	// Token: 0x06001C87 RID: 7303 RVA: 0x0009805D File Offset: 0x0009625D
	public void AddOverride(string point_name, KAnimFile build_override, string symbol_name)
	{
		this.overrideMap[point_name] = new SnapOn.OverrideEntry
		{
			buildFile = build_override,
			symbolName = symbol_name
		};
	}

	// Token: 0x06001C88 RID: 7304 RVA: 0x0009807E File Offset: 0x0009627E
	public void RemoveOverride(string point_name)
	{
		this.overrideMap.Remove(point_name);
	}

	// Token: 0x0400100B RID: 4107
	private KAnimControllerBase kanimController;

	// Token: 0x0400100C RID: 4108
	public List<SnapOn.SnapPoint> snapPoints = new List<SnapOn.SnapPoint>();

	// Token: 0x0400100D RID: 4109
	private Dictionary<string, SnapOn.OverrideEntry> overrideMap = new Dictionary<string, SnapOn.OverrideEntry>();

	// Token: 0x02001111 RID: 4369
	[Serializable]
	public class SnapPoint
	{
		// Token: 0x040059B0 RID: 22960
		public string pointName;

		// Token: 0x040059B1 RID: 22961
		public bool automatic = true;

		// Token: 0x040059B2 RID: 22962
		public HashedString context;

		// Token: 0x040059B3 RID: 22963
		public KAnimFile buildFile;

		// Token: 0x040059B4 RID: 22964
		public HashedString overrideSymbol;
	}

	// Token: 0x02001112 RID: 4370
	public class OverrideEntry
	{
		// Token: 0x040059B5 RID: 22965
		public KAnimFile buildFile;

		// Token: 0x040059B6 RID: 22966
		public string symbolName;
	}
}
