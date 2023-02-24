using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;

namespace KMod
{
	// Token: 0x02000D04 RID: 3332
	public class UserMod2
	{
		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06006731 RID: 26417 RVA: 0x0027D4DA File Offset: 0x0027B6DA
		// (set) Token: 0x06006732 RID: 26418 RVA: 0x0027D4E2 File Offset: 0x0027B6E2
		public Assembly assembly { get; set; }

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06006733 RID: 26419 RVA: 0x0027D4EB File Offset: 0x0027B6EB
		// (set) Token: 0x06006734 RID: 26420 RVA: 0x0027D4F3 File Offset: 0x0027B6F3
		public string path { get; set; }

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06006735 RID: 26421 RVA: 0x0027D4FC File Offset: 0x0027B6FC
		// (set) Token: 0x06006736 RID: 26422 RVA: 0x0027D504 File Offset: 0x0027B704
		public Mod mod { get; set; }

		// Token: 0x06006737 RID: 26423 RVA: 0x0027D50D File Offset: 0x0027B70D
		public virtual void OnLoad(Harmony harmony)
		{
			harmony.PatchAll(this.assembly);
		}

		// Token: 0x06006738 RID: 26424 RVA: 0x0027D51B File Offset: 0x0027B71B
		public virtual void OnAllModsLoaded(Harmony harmony, IReadOnlyList<Mod> mods)
		{
		}
	}
}
