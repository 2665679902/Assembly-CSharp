using System;
using System.Collections.Generic;

namespace ClipperLib
{
	// Token: 0x02000159 RID: 345
	public class PolyNode
	{
		// Token: 0x06000B9C RID: 2972 RVA: 0x0002E3C8 File Offset: 0x0002C5C8
		private bool IsHoleNode()
		{
			bool flag = true;
			for (PolyNode polyNode = this.m_Parent; polyNode != null; polyNode = polyNode.m_Parent)
			{
				flag = !flag;
			}
			return flag;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x0002E3F0 File Offset: 0x0002C5F0
		public int ChildCount
		{
			get
			{
				return this.m_Childs.Count;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0002E3FD File Offset: 0x0002C5FD
		public List<IntPoint> Contour
		{
			get
			{
				return this.m_polygon;
			}
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0002E408 File Offset: 0x0002C608
		internal void AddChild(PolyNode Child)
		{
			int count = this.m_Childs.Count;
			this.m_Childs.Add(Child);
			Child.m_Parent = this;
			Child.m_Index = count;
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002E43B File Offset: 0x0002C63B
		public PolyNode GetNext()
		{
			if (this.m_Childs.Count > 0)
			{
				return this.m_Childs[0];
			}
			return this.GetNextSiblingUp();
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002E460 File Offset: 0x0002C660
		internal PolyNode GetNextSiblingUp()
		{
			if (this.m_Parent == null)
			{
				return null;
			}
			if (this.m_Index == this.m_Parent.m_Childs.Count - 1)
			{
				return this.m_Parent.GetNextSiblingUp();
			}
			return this.m_Parent.m_Childs[this.m_Index + 1];
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0002E4B5 File Offset: 0x0002C6B5
		public List<PolyNode> Childs
		{
			get
			{
				return this.m_Childs;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0002E4BD File Offset: 0x0002C6BD
		public PolyNode Parent
		{
			get
			{
				return this.m_Parent;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0002E4C5 File Offset: 0x0002C6C5
		public bool IsHole
		{
			get
			{
				return this.IsHoleNode();
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x0002E4CD File Offset: 0x0002C6CD
		// (set) Token: 0x06000BA6 RID: 2982 RVA: 0x0002E4D5 File Offset: 0x0002C6D5
		public bool IsOpen { get; set; }

		// Token: 0x0400074F RID: 1871
		internal PolyNode m_Parent;

		// Token: 0x04000750 RID: 1872
		internal List<IntPoint> m_polygon = new List<IntPoint>();

		// Token: 0x04000751 RID: 1873
		internal int m_Index;

		// Token: 0x04000752 RID: 1874
		internal JoinType m_jointype;

		// Token: 0x04000753 RID: 1875
		internal EndType m_endtype;

		// Token: 0x04000754 RID: 1876
		internal List<PolyNode> m_Childs = new List<PolyNode>();
	}
}
