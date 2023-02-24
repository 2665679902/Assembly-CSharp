using System;
using UnityEngine;

namespace rendering
{
	// Token: 0x02000C53 RID: 3155
	public class BackWall : MonoBehaviour
	{
		// Token: 0x06006448 RID: 25672 RVA: 0x0025939F File Offset: 0x0025759F
		private void Awake()
		{
			this.backwallMaterial.SetTexture("images", this.array);
		}

		// Token: 0x0400458D RID: 17805
		[SerializeField]
		public Material backwallMaterial;

		// Token: 0x0400458E RID: 17806
		[SerializeField]
		public Texture2DArray array;
	}
}
