using System;
using System.Collections.Generic;

namespace ClipperLib
{
	// Token: 0x02000158 RID: 344
	public class PolyTree : PolyNode
	{
		// Token: 0x06000B97 RID: 2967 RVA: 0x0002E2E8 File Offset: 0x0002C4E8
		~PolyTree()
		{
			this.Clear();
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0002E314 File Offset: 0x0002C514
		public void Clear()
		{
			for (int i = 0; i < this.m_AllPolys.Count; i++)
			{
				this.m_AllPolys[i] = null;
			}
			this.m_AllPolys.Clear();
			this.m_Childs.Clear();
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002E35A File Offset: 0x0002C55A
		public PolyNode GetFirst()
		{
			if (this.m_Childs.Count > 0)
			{
				return this.m_Childs[0];
			}
			return null;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0002E378 File Offset: 0x0002C578
		public int Total
		{
			get
			{
				int num = this.m_AllPolys.Count;
				if (num > 0 && this.m_Childs[0] != this.m_AllPolys[0])
				{
					num--;
				}
				return num;
			}
		}

		// Token: 0x0400074E RID: 1870
		internal List<PolyNode> m_AllPolys = new List<PolyNode>();
	}
}
