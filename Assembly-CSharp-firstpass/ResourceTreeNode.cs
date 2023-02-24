using System;
using System.Collections.Generic;
using NodeEditorFramework.Utilities;
using UnityEngine;

// Token: 0x020000ED RID: 237
public class ResourceTreeNode : Resource
{
	// Token: 0x170000E5 RID: 229
	// (get) Token: 0x06000878 RID: 2168 RVA: 0x00022420 File Offset: 0x00020620
	public Vector2 position
	{
		get
		{
			return new Vector2(this.nodeX, this.nodeY);
		}
	}

	// Token: 0x170000E6 RID: 230
	// (get) Token: 0x06000879 RID: 2169 RVA: 0x00022433 File Offset: 0x00020633
	public Vector2 center
	{
		get
		{
			return this.position + new Vector2(this.width / 2f, -this.height / 2f);
		}
	}

	// Token: 0x0400063F RID: 1599
	public float nodeX;

	// Token: 0x04000640 RID: 1600
	public float nodeY;

	// Token: 0x04000641 RID: 1601
	public float width;

	// Token: 0x04000642 RID: 1602
	public float height;

	// Token: 0x04000643 RID: 1603
	public List<ResourceTreeNode> references = new List<ResourceTreeNode>();

	// Token: 0x04000644 RID: 1604
	public List<ResourceTreeNode.Edge> edges = new List<ResourceTreeNode.Edge>();

	// Token: 0x020009F2 RID: 2546
	public class Edge
	{
		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x060053E7 RID: 21479 RVA: 0x0009C7FA File Offset: 0x0009A9FA
		// (set) Token: 0x060053E8 RID: 21480 RVA: 0x0009C802 File Offset: 0x0009AA02
		public ResourceTreeNode.Edge.EdgeType edgeType { get; private set; }

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x060053E9 RID: 21481 RVA: 0x0009C80B File Offset: 0x0009AA0B
		// (set) Token: 0x060053EA RID: 21482 RVA: 0x0009C813 File Offset: 0x0009AA13
		public ResourceTreeNode source { get; private set; }

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x060053EB RID: 21483 RVA: 0x0009C81C File Offset: 0x0009AA1C
		// (set) Token: 0x060053EC RID: 21484 RVA: 0x0009C824 File Offset: 0x0009AA24
		public ResourceTreeNode target { get; private set; }

		// Token: 0x060053ED RID: 21485 RVA: 0x0009C82D File Offset: 0x0009AA2D
		private Vector2 SourcePos()
		{
			return this.source.center + this.sourceOffset;
		}

		// Token: 0x060053EE RID: 21486 RVA: 0x0009C845 File Offset: 0x0009AA45
		private Vector2 TargetPos()
		{
			return this.target.center + this.targetOffset;
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x060053EF RID: 21487 RVA: 0x0009C85D File Offset: 0x0009AA5D
		public List<Vector2> SrcTarget
		{
			get
			{
				return new List<Vector2>
				{
					this.SourcePos(),
					this.TargetPos()
				};
			}
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x060053F0 RID: 21488 RVA: 0x0009C87C File Offset: 0x0009AA7C
		// (set) Token: 0x060053F1 RID: 21489 RVA: 0x0009C884 File Offset: 0x0009AA84
		public List<Vector2> path { get; private set; }

		// Token: 0x060053F2 RID: 21490 RVA: 0x0009C88D File Offset: 0x0009AA8D
		public Edge(ResourceTreeNode source, ResourceTreeNode target, ResourceTreeNode.Edge.EdgeType edgeType)
		{
			this.edgeType = edgeType;
			this.source = source;
			this.target = target;
			this.path = null;
		}

		// Token: 0x060053F3 RID: 21491 RVA: 0x0009C8CB File Offset: 0x0009AACB
		public void AddToPath(Vector2f point)
		{
			if (this.path == null)
			{
				this.path = new List<Vector2>();
			}
			this.path.Add(point);
		}

		// Token: 0x060053F4 RID: 21492 RVA: 0x0009C8F1 File Offset: 0x0009AAF1
		public void Render(Rect rect, float width, Color colour)
		{
			ResourceTreeNode.Edge.EdgeType edgeType = this.edgeType;
			RTEditorGUI.DrawLine(rect, this.SourcePos(), this.TargetPos(), colour, null, width);
		}

		// Token: 0x04002244 RID: 8772
		public Vector2f sourceOffset = new Vector2f(0, 0);

		// Token: 0x04002245 RID: 8773
		public Vector2f targetOffset = new Vector2f(0, 0);

		// Token: 0x02000B48 RID: 2888
		public enum EdgeType
		{
			// Token: 0x04002699 RID: 9881
			PolyLineEdge,
			// Token: 0x0400269A RID: 9882
			QuadCurveEdge,
			// Token: 0x0400269B RID: 9883
			ArcEdge,
			// Token: 0x0400269C RID: 9884
			SplineEdge,
			// Token: 0x0400269D RID: 9885
			BezierEdge,
			// Token: 0x0400269E RID: 9886
			GenericEdge
		}
	}
}
