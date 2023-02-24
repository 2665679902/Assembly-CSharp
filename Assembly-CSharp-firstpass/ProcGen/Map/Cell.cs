using System;
using KSerialization;

namespace ProcGen.Map
{
	// Token: 0x020004F6 RID: 1270
	[SerializationConfig(MemberSerialization.OptIn)]
	public class Cell : Node
	{
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x060036DE RID: 14046 RVA: 0x00078A98 File Offset: 0x00076C98
		public long NodeId
		{
			get
			{
				return base.node.Id;
			}
		}
	}
}
