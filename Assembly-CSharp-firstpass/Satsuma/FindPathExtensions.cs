using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x02000253 RID: 595
	public static class FindPathExtensions
	{
		// Token: 0x06001213 RID: 4627 RVA: 0x00047070 File Offset: 0x00045270
		public static IPath FindPath(this IGraph graph, IEnumerable<Node> source, Func<Node, bool> target, Dfs.Direction direction)
		{
			FindPathExtensions.PathDfs pathDfs = new FindPathExtensions.PathDfs
			{
				PathDirection = direction,
				IsTarget = target
			};
			pathDfs.Run(graph, source);
			if (pathDfs.EndNode == Node.Invalid)
			{
				return null;
			}
			Path path = new Path(graph);
			path.Begin(pathDfs.StartNode);
			foreach (Arc arc in pathDfs.Path)
			{
				path.AddLast(arc);
			}
			return path;
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00047108 File Offset: 0x00045308
		public static IPath FindPath(this IGraph graph, Node source, Node target, Dfs.Direction direction)
		{
			return graph.FindPath(new Node[] { source }, (Node x) => x == target, direction);
		}

		// Token: 0x02000A86 RID: 2694
		private class PathDfs : Dfs
		{
			// Token: 0x0600562E RID: 22062 RVA: 0x000A0983 File Offset: 0x0009EB83
			protected override void Start(out Dfs.Direction direction)
			{
				direction = this.PathDirection;
				this.StartNode = Node.Invalid;
				this.Path = new List<Arc>();
				this.EndNode = Node.Invalid;
			}

			// Token: 0x0600562F RID: 22063 RVA: 0x000A09AE File Offset: 0x0009EBAE
			protected override bool NodeEnter(Node node, Arc arc)
			{
				if (arc == Arc.Invalid)
				{
					this.StartNode = node;
				}
				else
				{
					this.Path.Add(arc);
				}
				if (this.IsTarget(node))
				{
					this.EndNode = node;
					return false;
				}
				return true;
			}

			// Token: 0x06005630 RID: 22064 RVA: 0x000A09EA File Offset: 0x0009EBEA
			protected override bool NodeExit(Node node, Arc arc)
			{
				if (arc != Arc.Invalid && this.EndNode == Node.Invalid)
				{
					this.Path.RemoveAt(this.Path.Count - 1);
				}
				return true;
			}

			// Token: 0x040023F0 RID: 9200
			public Dfs.Direction PathDirection;

			// Token: 0x040023F1 RID: 9201
			public Func<Node, bool> IsTarget;

			// Token: 0x040023F2 RID: 9202
			public Node StartNode;

			// Token: 0x040023F3 RID: 9203
			public List<Arc> Path;

			// Token: 0x040023F4 RID: 9204
			public Node EndNode;
		}
	}
}
