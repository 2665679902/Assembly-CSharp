using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;

namespace KMod
{
	// Token: 0x02000D05 RID: 3333
	public class LoadedModData
	{
		// Token: 0x04004BCE RID: 19406
		public Harmony harmony;

		// Token: 0x04004BCF RID: 19407
		public Dictionary<Assembly, UserMod2> userMod2Instances;

		// Token: 0x04004BD0 RID: 19408
		public ICollection<Assembly> dlls;

		// Token: 0x04004BD1 RID: 19409
		public ICollection<MethodBase> patched_methods;
	}
}
