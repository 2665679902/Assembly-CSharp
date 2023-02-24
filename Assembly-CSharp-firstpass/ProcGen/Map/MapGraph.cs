using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

namespace ProcGen.Map
{
	// Token: 0x020004F9 RID: 1273
	[SerializationConfig(MemberSerialization.OptIn)]
	public class MapGraph : Graph<Cell, Edge>
	{
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060036E5 RID: 14053 RVA: 0x00078B7B File Offset: 0x00076D7B
		public List<Corner> corners
		{
			get
			{
				return this.cornerList;
			}
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x00078B83 File Offset: 0x00076D83
		public MapGraph(int seed)
			: base(seed)
		{
			this.cornerList = new List<Corner>();
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x00078B97 File Offset: 0x00076D97
		public Edge GetEdge(Cell site0, Cell site1)
		{
			return base.GetArc(site0, site1);
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x00078BA1 File Offset: 0x00076DA1
		public Edge AddEdge(Cell site0, Cell site1, Corner corner0, Corner corner1)
		{
			Edge edge = base.AddArc(site0, site1, "Edge");
			edge.SetCorners(corner0, corner1);
			return edge;
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x00078BBC File Offset: 0x00076DBC
		public Edge AddOrGetEdge(Cell site0, Cell site1, Corner corner0, Corner corner1)
		{
			Edge edge = base.GetArc(site0, site1);
			if (edge != null)
			{
				return edge;
			}
			edge = base.AddArc(site0, site1, "Edge");
			edge.SetCorners(corner0, corner1);
			return edge;
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x00078BF0 File Offset: 0x00076DF0
		public Corner AddOrGetCorner(Vector2 position)
		{
			Corner corner = this.cornerList.Find(delegate(Corner c)
			{
				Vector2 vector = c.position - position;
				return vector.x < 1f && vector.x > -1f && vector.y < 1f && vector.y > -1f;
			});
			if (corner == null)
			{
				corner = new Corner(position);
				this.cornerList.Add(corner);
			}
			return corner;
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x00078C3E File Offset: 0x00076E3E
		public List<Edge> GetEdgesWithTag(Tag tag)
		{
			return base.GetArcsWithTag(tag);
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x00078C48 File Offset: 0x00076E48
		public void ClearEdgesAndCorners()
		{
			foreach (Edge edge in this.arcList)
			{
				base.baseGraph.DeleteArc(edge.arc);
			}
			this.arcList.Clear();
			this.cornerList.Clear();
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x00078CBC File Offset: 0x00076EBC
		public void ClearTags()
		{
			foreach (Cell cell in this.nodeList)
			{
				cell.tags.Clear();
			}
			foreach (Edge edge in this.arcList)
			{
				edge.tags.Clear();
			}
		}

		// Token: 0x060036EE RID: 14062 RVA: 0x00078D58 File Offset: 0x00076F58
		public void Validate()
		{
			for (int i = 0; i < this.nodeList.Count; i++)
			{
				for (int j = 0; j < this.nodeList.Count; j++)
				{
					if (j != i)
					{
						if (this.nodeList[i] == this.nodeList[j])
						{
							global::Debug.LogError("Duplicate cell (instance)");
							return;
						}
						if (this.nodeList[i].position == this.nodeList[j].position)
						{
							global::Debug.LogError("Duplicate cell (position)");
							return;
						}
						if (this.nodeList[i].node == this.nodeList[j].node)
						{
							global::Debug.LogError("Duplicate cell (node)");
							return;
						}
					}
				}
			}
			for (int k = 0; k < this.cornerList.Count; k++)
			{
				for (int l = 0; l < this.cornerList.Count; l++)
				{
					if (l != k)
					{
						if (this.cornerList[k] == this.cornerList[l])
						{
							global::Debug.LogError("Duplicate corner (instance)");
							return;
						}
						if (this.cornerList[k].position == this.cornerList[l].position)
						{
							global::Debug.LogError("Duplicate corner (position)");
							return;
						}
					}
				}
			}
			for (int m = 0; m < this.arcList.Count; m++)
			{
				for (int n = 0; n < this.arcList.Count; n++)
				{
					if (n != m)
					{
						Edge edge = this.arcList[m];
						Edge edge2 = this.arcList[n];
						if (edge == edge2)
						{
							global::Debug.LogError("Duplicate edge (instance)");
							return;
						}
						if (edge.arc == edge2.arc)
						{
							global::Debug.LogError(string.Concat(new string[]
							{
								"Duplicate EDGE [",
								edge.arc.ToString(),
								"] & [",
								edge2.arc.ToString(),
								"] - (ARC)"
							}));
							return;
						}
						if (edge.corner0 == edge2.corner0 && edge.corner1 == edge2.corner1)
						{
							global::Debug.LogError("Duplicate edge (corner same order)");
							return;
						}
						if (edge.corner0 == edge2.corner1 && edge.corner1 == edge2.corner0)
						{
							global::Debug.LogError("Duplicate edge (corner different order)");
							return;
						}
						List<Cell> nodes = base.GetNodes(edge);
						List<Cell> nodes2 = base.GetNodes(edge2);
						if (nodes[0] == nodes2[0] && nodes[1] == nodes2[1])
						{
							global::Debug.LogError("Duplicate edge (site same order)");
							return;
						}
						if (nodes[0] == nodes2[1] && nodes[1] == nodes2[0])
						{
							global::Debug.LogError("Duplicate Edge (site differnt order)");
							return;
						}
						if (nodes[0].node == nodes2[0].node && nodes[1].node == nodes2[1].node)
						{
							global::Debug.LogError("Duplicate edge (site node same order)");
							return;
						}
						if (nodes[0].node == nodes2[1].node && nodes[1].node == nodes2[0].node)
						{
							global::Debug.LogError("Duplicate edge (site node differnt order)");
							return;
						}
					}
				}
			}
		}

		// Token: 0x040013A9 RID: 5033
		[Serialize]
		public List<Corner> cornerList;
	}
}
