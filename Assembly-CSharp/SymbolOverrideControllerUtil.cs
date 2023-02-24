using System;
using UnityEngine;

// Token: 0x0200042B RID: 1067
public static class SymbolOverrideControllerUtil
{
	// Token: 0x060016F7 RID: 5879 RVA: 0x000770F4 File Offset: 0x000752F4
	public static SymbolOverrideController AddToPrefab(GameObject prefab)
	{
		SymbolOverrideController symbolOverrideController = prefab.AddComponent<SymbolOverrideController>();
		KBatchedAnimController component = prefab.GetComponent<KBatchedAnimController>();
		DebugUtil.Assert(component != null, "SymbolOverrideController must be added after a KBatchedAnimController component.");
		component.usingNewSymbolOverrideSystem = true;
		return symbolOverrideController;
	}

	// Token: 0x060016F8 RID: 5880 RVA: 0x0007711C File Offset: 0x0007531C
	public static void AddBuildOverride(this SymbolOverrideController symbol_override_controller, KAnimFileData anim_file_data, int priority = 0)
	{
		for (int i = 0; i < anim_file_data.build.symbols.Length; i++)
		{
			KAnim.Build.Symbol symbol = anim_file_data.build.symbols[i];
			symbol_override_controller.AddSymbolOverride(new HashedString(symbol.hash.HashValue), symbol, priority);
		}
	}

	// Token: 0x060016F9 RID: 5881 RVA: 0x00077168 File Offset: 0x00075368
	public static void RemoveBuildOverride(this SymbolOverrideController symbol_override_controller, KAnimFileData anim_file_data, int priority = 0)
	{
		for (int i = 0; i < anim_file_data.build.symbols.Length; i++)
		{
			KAnim.Build.Symbol symbol = anim_file_data.build.symbols[i];
			symbol_override_controller.RemoveSymbolOverride(new HashedString(symbol.hash.HashValue), priority);
		}
	}

	// Token: 0x060016FA RID: 5882 RVA: 0x000771B4 File Offset: 0x000753B4
	public static void TryRemoveBuildOverride(this SymbolOverrideController symbol_override_controller, KAnimFileData anim_file_data, int priority = 0)
	{
		for (int i = 0; i < anim_file_data.build.symbols.Length; i++)
		{
			KAnim.Build.Symbol symbol = anim_file_data.build.symbols[i];
			symbol_override_controller.TryRemoveSymbolOverride(new HashedString(symbol.hash.HashValue), priority);
		}
	}

	// Token: 0x060016FB RID: 5883 RVA: 0x000771FF File Offset: 0x000753FF
	public static bool TryRemoveSymbolOverride(this SymbolOverrideController symbol_override_controller, HashedString target_symbol, int priority = 0)
	{
		return symbol_override_controller.GetSymbolOverrideIdx(target_symbol, priority) >= 0 && symbol_override_controller.RemoveSymbolOverride(target_symbol, priority);
	}

	// Token: 0x060016FC RID: 5884 RVA: 0x00077218 File Offset: 0x00075418
	public static void ApplySymbolOverridesByAffix(this SymbolOverrideController symbol_override_controller, KAnimFile anim_file, string prefix = null, string postfix = null, int priority = 0)
	{
		for (int i = 0; i < anim_file.GetData().build.symbols.Length; i++)
		{
			KAnim.Build.Symbol symbol = anim_file.GetData().build.symbols[i];
			string text = HashCache.Get().Get(symbol.hash);
			if (prefix != null && postfix != null)
			{
				if (text.StartsWith(prefix) && text.EndsWith(postfix))
				{
					string text2 = text.Substring(prefix.Length, text.Length - prefix.Length);
					text2 = text2.Substring(0, text2.Length - postfix.Length);
					symbol_override_controller.AddSymbolOverride(text2, symbol, priority);
				}
			}
			else if (prefix != null && text.StartsWith(prefix))
			{
				symbol_override_controller.AddSymbolOverride(text.Substring(prefix.Length, text.Length - prefix.Length), symbol, priority);
			}
			else if (postfix != null && text.EndsWith(postfix))
			{
				symbol_override_controller.AddSymbolOverride(text.Substring(0, text.Length - postfix.Length), symbol, priority);
			}
		}
	}
}
