using System;
using System.Collections.Generic;
using Delaunay.Geo;
using UnityEngine;

namespace VoronoiTree
{
	// Token: 0x020004B6 RID: 1206
	public class Tree : Node
	{
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x0600338C RID: 13196 RVA: 0x0006F584 File Offset: 0x0006D784
		// (set) Token: 0x0600338D RID: 13197 RVA: 0x0006F58C File Offset: 0x0006D78C
		public SeededRandom myRandom { get; private set; }

		// Token: 0x0600338E RID: 13198 RVA: 0x0006F595 File Offset: 0x0006D795
		public Tree()
			: base(Node.NodeType.Internal)
		{
			this.children = new List<Node>();
			this.SetSeed(0);
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x0006F5B0 File Offset: 0x0006D7B0
		public Tree(int seed = 0)
			: base(Node.NodeType.Internal)
		{
			this.children = new List<Node>();
			this.SetSeed(seed);
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x0006F5CB File Offset: 0x0006D7CB
		public Tree(Diagram.Site site, Tree parent, int seed = 0)
			: base(site, Node.NodeType.Internal, parent)
		{
			this.children = new List<Node>();
			this.SetSeed(seed);
		}

		// Token: 0x06003391 RID: 13201 RVA: 0x0006F5E8 File Offset: 0x0006D7E8
		public Tree(Diagram.Site site, List<Node> children, Tree parent, int seed = 0)
			: base(site, Node.NodeType.Internal, parent)
		{
			if (children == null)
			{
				children = new List<Node>();
			}
			this.children = children;
			this.SetSeed(seed);
		}

		// Token: 0x06003392 RID: 13202 RVA: 0x0006F60C File Offset: 0x0006D80C
		public void SetSeed(int seed)
		{
			this.myRandom = new SeededRandom(seed);
		}

		// Token: 0x06003393 RID: 13203 RVA: 0x0006F61C File Offset: 0x0006D81C
		public Node GetChildByID(uint id)
		{
			return this.children.Find((Node s) => s.site.id == id);
		}

		// Token: 0x06003394 RID: 13204 RVA: 0x0006F64D File Offset: 0x0006D84D
		public int ChildCount()
		{
			if (this.children == null)
			{
				return 0;
			}
			return this.children.Count;
		}

		// Token: 0x06003395 RID: 13205 RVA: 0x0006F664 File Offset: 0x0006D864
		public Tree GetChildContainingLeaf(Leaf leaf)
		{
			Vector2 vector = leaf.site.poly.Centroid();
			for (int i = 0; i < this.children.Count; i++)
			{
				Tree tree = this.children[i] as Tree;
				if (tree != null && tree.site.poly.Contains(vector))
				{
					return tree;
				}
			}
			return null;
		}

		// Token: 0x06003396 RID: 13206 RVA: 0x0006F6C3 File Offset: 0x0006D8C3
		public Node GetChild(int childIndex)
		{
			if (childIndex < this.children.Count)
			{
				return this.children[childIndex];
			}
			return null;
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x0006F6E1 File Offset: 0x0006D8E1
		public void AddChild(Node child)
		{
			if (child.site.id > Node.maxIndex)
			{
				Node.maxIndex = child.site.id;
			}
			this.children.Add(child);
			child.SetParent(this);
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x0006F718 File Offset: 0x0006D918
		public Node AddSite(Diagram.Site site, Node.NodeType type)
		{
			Node node;
			if (type == Node.NodeType.Internal)
			{
				node = new Tree(site, this, this.myRandom.seed + this.ChildCount());
			}
			else
			{
				node = new Leaf(site, this);
			}
			this.AddChild(node);
			return node;
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x0006F758 File Offset: 0x0006D958
		public bool ComputeChildrenRecursive(int depth, bool pd = false)
		{
			if (depth > Node.maxDepth || this.site.poly == null || this.children == null)
			{
				return false;
			}
			List<Diagram.Site> list = new List<Diagram.Site>();
			for (int i = 0; i < this.children.Count; i++)
			{
				list.Add(this.children[i].site);
			}
			base.PlaceSites(list, depth);
			if (pd)
			{
				for (int j = 0; j < list.Count; j++)
				{
					if (!this.site.poly.Contains(list[j].position))
					{
						global::Debug.LogErrorFormat("Cant feed points [{0}] to powerdiagram that are outside its area [{1}] ", new object[]
						{
							list[j].id,
							list[j].position
						});
					}
				}
				if (base.ComputeNodePD(list, 500, 0.2f))
				{
					for (int k = 0; k < this.children.Count; k++)
					{
						if (this.children[k].type == Node.NodeType.Internal && !(this.children[k] as Tree).ComputeChildrenRecursive(depth + 1, pd))
						{
							return false;
						}
					}
				}
			}
			else if (base.ComputeNode(list))
			{
				for (int l = 0; l < this.children.Count; l++)
				{
					if (this.children[l].type == Node.NodeType.Internal && !(this.children[l] as Tree).ComputeChildrenRecursive(depth + 1, false))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600339A RID: 13210 RVA: 0x0006F8E4 File Offset: 0x0006DAE4
		public bool ComputeChildren(int seed, bool place = false, bool pd = false)
		{
			if (this.site.poly == null || this.children == null)
			{
				return false;
			}
			List<Diagram.Site> list = new List<Diagram.Site>();
			for (int i = 0; i < this.children.Count; i++)
			{
				if (place || !this.site.poly.Contains(this.children[i].site.position))
				{
					global::Debug.LogErrorFormat("Cant feed points [{0}] to powerdiagram that are outside its area [{1}] ", new object[]
					{
						this.children[i].site.id,
						this.children[i].site.position
					});
				}
				list.Add(this.children[i].site);
			}
			if (place)
			{
				base.PlaceSites(list, seed);
			}
			if (pd)
			{
				base.ComputeNodePD(list, 500, 0.2f);
			}
			else
			{
				base.ComputeNode(list);
			}
			return true;
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x0006F9E4 File Offset: 0x0006DBE4
		public int Count()
		{
			if (this.children == null || this.children.Count == 0)
			{
				return 0;
			}
			int num = this.children.Count;
			for (int i = 0; i < this.children.Count; i++)
			{
				if (this.children[i].type == Node.NodeType.Internal)
				{
					Tree tree = this.children[i] as Tree;
					num += tree.Count();
				}
			}
			return num;
		}

		// Token: 0x0600339C RID: 13212 RVA: 0x0006FA5C File Offset: 0x0006DC5C
		public void Reset()
		{
			if (this.children != null)
			{
				for (int i = 0; i < this.children.Count; i++)
				{
					if (this.children[i].type == Node.NodeType.Internal)
					{
						(this.children[i] as Tree).Reset();
					}
				}
			}
			base.Reset(null);
		}

		// Token: 0x0600339D RID: 13213 RVA: 0x0006FAB8 File Offset: 0x0006DCB8
		public int MaxDepth(int depth = 0)
		{
			if (this.children == null || this.children.Count == 0)
			{
				return depth;
			}
			int num = depth + 1;
			int num2 = num;
			for (int i = 0; i < this.children.Count; i++)
			{
				int num3 = num2 + 1;
				if (this.children[i].type == Node.NodeType.Internal)
				{
					num3 = (this.children[i] as Tree).MaxDepth(num2);
				}
				if (num3 > num)
				{
					num = num3;
				}
			}
			return num;
		}

		// Token: 0x0600339E RID: 13214 RVA: 0x0006FB30 File Offset: 0x0006DD30
		public void RelaxRecursive(int depth, int iterations = -1, float minEnergy = 1f, bool pd = false)
		{
			if (this.dontRelaxChildren || this.site.poly == null || this.children == null || this.children.Count == 0)
			{
				this.visited = Node.VisitedType.MissingData;
				return;
			}
			List<Diagram.Site> list = new List<Diagram.Site>();
			for (int i = 0; i < this.children.Count; i++)
			{
				list.Add(this.children[i].site);
			}
			float num = float.MaxValue;
			int num2 = 0;
			while (num2 < iterations && num > minEnergy)
			{
				float num3 = 0f;
				for (int j = 0; j < this.children.Count; j++)
				{
					num3 += Vector2.Distance(this.children[j].site.position, list[j].poly.Centroid());
					this.children[j].site.position = list[j].poly.Centroid();
				}
				num = num3;
				base.PlaceSites(list, depth);
				if (pd)
				{
					if (!base.ComputeNodePD(list, 500, 0.2f))
					{
						this.visited = Node.VisitedType.Error;
						return;
					}
				}
				else if (!base.ComputeNode(list))
				{
					this.visited = Node.VisitedType.Error;
					return;
				}
				num2++;
			}
			for (int k = 0; k < this.children.Count; k++)
			{
				if (this.children[k].type == Node.NodeType.Internal)
				{
					Tree tree = this.children[k] as Tree;
					if (tree.ComputeChildren(depth, false, false))
					{
						tree.RelaxRecursive(depth + 1, iterations, minEnergy, false);
					}
				}
			}
			this.visited = Node.VisitedType.VisitedSuccess;
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x0006FCDC File Offset: 0x0006DEDC
		public float Relax(int depth, int relaxDepth, bool pd = false)
		{
			if (this.dontRelaxChildren || depth > Node.maxDepth || depth > relaxDepth || this.site.poly == null || this.children == null || this.children.Count == 0)
			{
				return 0f;
			}
			float num = 0f;
			if (depth < relaxDepth)
			{
				for (int i = 0; i < this.children.Count; i++)
				{
					if (this.children[i].type == Node.NodeType.Internal)
					{
						Tree tree = this.children[i] as Tree;
						num += tree.Relax(depth + 1, relaxDepth, false);
					}
				}
				return num;
			}
			if (depth == relaxDepth)
			{
				List<Diagram.Site> list = new List<Diagram.Site>();
				for (int j = 0; j < this.children.Count; j++)
				{
					list.Add(this.children[j].site);
				}
				if (pd)
				{
					if (!base.ComputeNodePD(list, 500, 0.2f))
					{
						return 0f;
					}
				}
				else
				{
					base.PlaceSites(list, depth);
					if (!base.ComputeNode(list))
					{
						return 0f;
					}
				}
				for (int k = 0; k < this.children.Count; k++)
				{
					num += Vector2.Distance(this.children[k].site.position, list[k].poly.Centroid());
					this.children[k].site.position = list[k].poly.Centroid();
					if (this.children[k].type == Node.NodeType.Internal && !(this.children[k] as Tree).ComputeChildren(depth, false, false))
					{
						return 0f;
					}
				}
			}
			return num;
		}

		// Token: 0x060033A0 RID: 13216 RVA: 0x0006FEA0 File Offset: 0x0006E0A0
		public Node GetNodeForPoint(Vector2 point, bool stopAtFirstChild = false)
		{
			if (this.site.poly == null)
			{
				return null;
			}
			if (this.children == null || this.children.Count == 0)
			{
				return this;
			}
			int i = 0;
			while (i < this.children.Count)
			{
				if (this.children[i].site.poly.Contains(point))
				{
					if (this.children[i].type != Node.NodeType.Internal)
					{
						return this.children[i];
					}
					Tree tree = this.children[i] as Tree;
					if (stopAtFirstChild)
					{
						return this.children[i];
					}
					return tree.GetNodeForPoint(point, false);
				}
				else
				{
					i++;
				}
			}
			if (this.site.poly.Contains(point))
			{
				return this;
			}
			return null;
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x0006FF6C File Offset: 0x0006E16C
		public Node GetNodeForSite(Diagram.Site target)
		{
			if (this.site == target)
			{
				return this;
			}
			if (this.site.poly == null || this.children == null || this.children.Count == 0)
			{
				return null;
			}
			int i = 0;
			while (i < this.children.Count)
			{
				if (this.children[i].site == target)
				{
					return this.children[i];
				}
				if (this.children[i].site.poly.Contains(target.position))
				{
					if (this.children[i].type == Node.NodeType.Internal)
					{
						return (this.children[i] as Tree).GetNodeForSite(target);
					}
					return this.children[i];
				}
				else
				{
					i++;
				}
			}
			return null;
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x00070044 File Offset: 0x0006E244
		public void GetIntersectingLeafSites(LineSegment edge, List<Diagram.Site> intersectingSites)
		{
			LineSegment lineSegment = new LineSegment(null, null);
			if (!(this.site.poly.Contains(edge.p0.Value) | this.site.poly.Contains(edge.p1.Value)) && !this.site.poly.ClipSegment(edge, ref lineSegment))
			{
				return;
			}
			if (this.children.Count == 0)
			{
				intersectingSites.Add(this.site);
				return;
			}
			for (int i = 0; i < this.children.Count; i++)
			{
				if (this.children[i].type == Node.NodeType.Internal)
				{
					((Tree)this.children[i]).GetIntersectingLeafSites(edge, intersectingSites);
				}
				else
				{
					((Leaf)this.children[i]).GetIntersectingSites(edge, intersectingSites);
				}
			}
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x00070130 File Offset: 0x0006E330
		public void GetIntersectingLeafNodes(LineSegment edge, List<Leaf> intersectingNodes)
		{
			LineSegment lineSegment = new LineSegment(null, null);
			for (int i = 0; i < this.children.Count; i++)
			{
				if ((this.children[i].site.poly.Contains(edge.p0.Value) | this.children[i].site.poly.Contains(edge.p1.Value)) || this.children[i].site.poly.ClipSegment(edge, ref lineSegment))
				{
					if (this.children[i].type == Node.NodeType.Internal)
					{
						((Tree)this.children[i]).GetIntersectingLeafNodes(edge, intersectingNodes);
					}
					else
					{
						intersectingNodes.Add((Leaf)this.children[i]);
					}
				}
			}
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x00070228 File Offset: 0x0006E428
		public Tree ReplaceLeafWithTree(Leaf leaf)
		{
			for (int i = 0; i < this.children.Count; i++)
			{
				if (this.children[i] == leaf)
				{
					this.children[i] = new Tree(leaf.site, this, this.myRandom.seed + i);
					this.children[i].log = leaf.log;
					if (leaf.tags != null)
					{
						this.children[i].SetTags(leaf.tags);
					}
					return this.children[i] as Tree;
				}
			}
			return null;
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x000702D0 File Offset: 0x0006E4D0
		public Leaf ReplaceTreeWithLeaf(Tree tree)
		{
			for (int i = 0; i < this.children.Count; i++)
			{
				if (this.children[i] == tree)
				{
					this.children[i] = new Leaf(tree.site, this);
					this.children[i].log = tree.log;
					if (tree.tags != null)
					{
						this.children[i].SetTags(tree.tags);
					}
					return this.children[i] as Leaf;
				}
			}
			return null;
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x00070368 File Offset: 0x0006E568
		public void ForceLowestToLeaf()
		{
			List<Node> list = new List<Node>();
			this.GetLeafNodes(list, null);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].type == Node.NodeType.Internal)
				{
					list[i].parent.ReplaceTreeWithLeaf(list[i] as Tree);
				}
			}
		}

		// Token: 0x060033A7 RID: 13223 RVA: 0x000703C4 File Offset: 0x0006E5C4
		public void Collapse()
		{
			for (int i = 0; i < this.children.Count; i++)
			{
				if (this.children[i] != null && this.children[i].type == Node.NodeType.Internal)
				{
					Tree tree = (Tree)this.children[i];
					if (tree.ChildCount() > 1)
					{
						tree.Collapse();
					}
					else
					{
						Node node = this.children[i];
						this.children[i] = new Leaf(tree.site, this);
						this.children[i].log = node.log;
						if (tree.tags != null)
						{
							this.children[i].SetTags(tree.tags);
						}
					}
				}
			}
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x00070490 File Offset: 0x0006E690
		public void VisitAll(Action<Node> action)
		{
			action(this);
			for (int i = 0; i < this.children.Count; i++)
			{
				if (this.children[i].type == Node.NodeType.Internal)
				{
					(this.children[i] as Tree).VisitAll(action);
				}
				else
				{
					action(this.children[i]);
				}
			}
		}

		// Token: 0x060033A9 RID: 13225 RVA: 0x000704F9 File Offset: 0x0006E6F9
		public List<Node> ImmediateChildren()
		{
			return new List<Node>(this.children);
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x00070508 File Offset: 0x0006E708
		public void GetLeafNodes(List<Node> nodes, Tree.LeafNodeTest test = null)
		{
			for (int i = 0; i < this.children.Count; i++)
			{
				if (this.children[i].type == Node.NodeType.Internal)
				{
					Tree tree = (Tree)this.children[i];
					if (tree.ChildCount() > 0)
					{
						tree.GetLeafNodes(nodes, test);
					}
					else if (test == null || test(this.children[i]))
					{
						nodes.Add(this.children[i]);
					}
				}
				else if (test == null || test(this.children[i]))
				{
					nodes.Add(this.children[i]);
				}
			}
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x000705C0 File Offset: 0x0006E7C0
		public void GetInternalNodes(List<Tree> nodes)
		{
			for (int i = 0; i < this.children.Count; i++)
			{
				if (this.children[i].type == Node.NodeType.Internal)
				{
					Tree tree = (Tree)this.children[i];
					nodes.Add(tree);
					if (tree.ChildCount() > 0)
					{
						tree.GetInternalNodes(nodes);
					}
				}
			}
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x00070620 File Offset: 0x0006E820
		public void ResetParentPointer()
		{
			for (int i = 0; i < this.children.Count; i++)
			{
				if (this.children[i].type == Node.NodeType.Internal)
				{
					Tree tree = (Tree)this.children[i];
					if (tree.ChildCount() > 0)
					{
						tree.ResetParentPointer();
					}
				}
				this.children[i].SetParent(this);
			}
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x0007068C File Offset: 0x0006E88C
		public void AddTagToChildren(Tag tag)
		{
			for (int i = 0; i < this.children.Count; i++)
			{
				this.children[i].AddTag(tag);
			}
		}

		// Token: 0x060033AE RID: 13230 RVA: 0x000706C4 File Offset: 0x0006E8C4
		public void GetNodesWithTag(Tag tag, List<Node> nodes)
		{
			if (this.children.Count == 0 && this.tags.Contains(tag))
			{
				nodes.Add(this);
				return;
			}
			for (int i = 0; i < this.children.Count; i++)
			{
				if (this.children[i].type == Node.NodeType.Internal)
				{
					((Tree)this.children[i]).GetNodesWithTag(tag, nodes);
				}
				else if (this.children[i].tags.Contains(tag))
				{
					nodes.Add(this.children[i]);
				}
			}
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x00070764 File Offset: 0x0006E964
		public void GetNodesWithoutTag(Tag tag, List<Node> nodes)
		{
			if (this.children.Count == 0 && !this.tags.Contains(tag))
			{
				nodes.Add(this);
				return;
			}
			for (int i = 0; i < this.children.Count; i++)
			{
				if (this.children[i].type == Node.NodeType.Internal)
				{
					((Tree)this.children[i]).GetNodesWithoutTag(tag, nodes);
				}
				else if (!this.children[i].tags.Contains(tag))
				{
					nodes.Add(this.children[i]);
				}
			}
		}

		// Token: 0x0400121C RID: 4636
		protected List<Node> children;

		// Token: 0x0400121D RID: 4637
		public bool dontRelaxChildren;

		// Token: 0x02000AE4 RID: 2788
		// (Invoke) Token: 0x060057C2 RID: 22466
		public delegate bool LeafNodeTest(Node node);
	}
}
