using System;
using KSerialization;
using UnityEngine;

namespace ProcGen.Map
{
	// Token: 0x020004F7 RID: 1271
	[SerializationConfig(MemberSerialization.OptIn)]
	public class Corner
	{
		// Token: 0x060036E0 RID: 14048 RVA: 0x00078ABB File Offset: 0x00076CBB
		public Corner(Vector2 position)
		{
			this.position = position;
		}

		// Token: 0x040013A6 RID: 5030
		public Vector2 position;
	}
}
