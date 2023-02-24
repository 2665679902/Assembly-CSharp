using System;
using UnityEngine;

namespace Rendering.World
{
	// Token: 0x02000C4C RID: 3148
	public class DynamicSubMesh
	{
		// Token: 0x0600641B RID: 25627 RVA: 0x00258414 File Offset: 0x00256614
		public DynamicSubMesh(string name, Bounds bounds, int idx_offset)
		{
			this.IdxOffset = idx_offset;
			this.Mesh = new Mesh();
			this.Mesh.name = name;
			this.Mesh.bounds = bounds;
			this.Mesh.MarkDynamic();
		}

		// Token: 0x0600641C RID: 25628 RVA: 0x00258480 File Offset: 0x00256680
		public void Reserve(int vertex_count, int triangle_count)
		{
			if (vertex_count > this.Vertices.Length)
			{
				this.Vertices = new Vector3[vertex_count];
				this.UVs = new Vector2[vertex_count];
				this.SetUVs = true;
			}
			else
			{
				this.SetUVs = false;
			}
			if (this.Triangles.Length != triangle_count)
			{
				this.Triangles = new int[triangle_count];
				this.SetTriangles = true;
				return;
			}
			this.SetTriangles = false;
		}

		// Token: 0x0600641D RID: 25629 RVA: 0x002584E6 File Offset: 0x002566E6
		public bool AreTrianglesFull()
		{
			return this.Triangles.Length == this.TriangleIdx;
		}

		// Token: 0x0600641E RID: 25630 RVA: 0x002584F8 File Offset: 0x002566F8
		public bool AreVerticesFull()
		{
			return this.Vertices.Length == this.VertexIdx;
		}

		// Token: 0x0600641F RID: 25631 RVA: 0x0025850A File Offset: 0x0025670A
		public bool AreUVsFull()
		{
			return this.UVs.Length == this.UVIdx;
		}

		// Token: 0x06006420 RID: 25632 RVA: 0x0025851C File Offset: 0x0025671C
		public void Commit()
		{
			if (this.SetTriangles)
			{
				this.Mesh.Clear();
			}
			this.Mesh.vertices = this.Vertices;
			if (this.SetUVs || this.SetTriangles)
			{
				this.Mesh.uv = this.UVs;
			}
			if (this.SetTriangles)
			{
				this.Mesh.triangles = this.Triangles;
			}
			this.VertexIdx = 0;
			this.UVIdx = 0;
			this.TriangleIdx = 0;
		}

		// Token: 0x06006421 RID: 25633 RVA: 0x0025859C File Offset: 0x0025679C
		public void AddTriangle(int triangle)
		{
			int[] triangles = this.Triangles;
			int triangleIdx = this.TriangleIdx;
			this.TriangleIdx = triangleIdx + 1;
			triangles[triangleIdx] = triangle + this.IdxOffset;
		}

		// Token: 0x06006422 RID: 25634 RVA: 0x002585CC File Offset: 0x002567CC
		public void AddUV(Vector2 uv)
		{
			Vector2[] uvs = this.UVs;
			int uvidx = this.UVIdx;
			this.UVIdx = uvidx + 1;
			uvs[uvidx] = uv;
		}

		// Token: 0x06006423 RID: 25635 RVA: 0x002585F8 File Offset: 0x002567F8
		public void AddVertex(Vector3 vertex)
		{
			Vector3[] vertices = this.Vertices;
			int vertexIdx = this.VertexIdx;
			this.VertexIdx = vertexIdx + 1;
			vertices[vertexIdx] = vertex;
		}

		// Token: 0x06006424 RID: 25636 RVA: 0x00258624 File Offset: 0x00256824
		public void Render(Vector3 position, Quaternion rotation, Material material, int layer, MaterialPropertyBlock property_block)
		{
			Graphics.DrawMesh(this.Mesh, position, rotation, material, layer, null, 0, property_block, false, false);
		}

		// Token: 0x0400455C RID: 17756
		public Vector3[] Vertices = new Vector3[0];

		// Token: 0x0400455D RID: 17757
		public Vector2[] UVs = new Vector2[0];

		// Token: 0x0400455E RID: 17758
		public int[] Triangles = new int[0];

		// Token: 0x0400455F RID: 17759
		public Mesh Mesh;

		// Token: 0x04004560 RID: 17760
		public bool SetUVs;

		// Token: 0x04004561 RID: 17761
		public bool SetTriangles;

		// Token: 0x04004562 RID: 17762
		private int VertexIdx;

		// Token: 0x04004563 RID: 17763
		private int UVIdx;

		// Token: 0x04004564 RID: 17764
		private int TriangleIdx;

		// Token: 0x04004565 RID: 17765
		private int IdxOffset;
	}
}
