using System;
using UnityEngine;

// Token: 0x02000812 RID: 2066
[AddComponentMenu("KMonoBehaviour/scripts/Meter")]
public class Meter : KMonoBehaviour
{
	// Token: 0x0200156D RID: 5485
	public enum Offset
	{
		// Token: 0x040066B3 RID: 26291
		Infront,
		// Token: 0x040066B4 RID: 26292
		Behind,
		// Token: 0x040066B5 RID: 26293
		UserSpecified,
		// Token: 0x040066B6 RID: 26294
		NoChange
	}
}
