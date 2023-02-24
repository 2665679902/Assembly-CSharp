using System;
using UnityEngine;

namespace Rendering.World
{
	// Token: 0x02000C4B RID: 3147
	public class DynamicMesh
	{
		// Token: 0x06006413 RID: 25619 RVA: 0x00258180 File Offset: 0x00256380
		public DynamicMesh(string name, Bounds bounds)
		{
			this.Name = name;
			this.Bounds = bounds;
		}

		// Token: 0x06006414 RID: 25620 RVA: 0x002581A4 File Offset: 0x002563A4
		public void Reserve(int vertex_count, int triangle_count)
		{
			if (vertex_count > this.VertexCount)
			{
				this.SetUVs = true;
			}
			else
			{
				this.SetUVs = false;
			}
			if (this.TriangleCount != triangle_count)
			{
				this.SetTriangles = true;
			}
			else
			{
				this.SetTriangles = false;
			}
			int num = (int)Mathf.Ceil((float)triangle_count / (float)DynamicMesh.TrianglesPerMesh);
			if (num != this.Meshes.Length)
			{
				this.Meshes = new DynamicSubMesh[num];
				for (int i = 0; i < this.Meshes.Length; i++)
				{
					int num2 = -i * DynamicMesh.VerticesPerMesh;
					this.Meshes[i] = new DynamicSubMesh(this.Name, this.Bounds, num2);
				}
				this.SetUVs = true;
				this.SetTriangles = true;
			}
			for (int j = 0; j < this.Meshes.Length; j++)
			{
				if (j == this.Meshes.Length - 1)
				{
					this.Meshes[j].Reserve(vertex_count % DynamicMesh.VerticesPerMesh, triangle_count % DynamicMesh.TrianglesPerMesh);
				}
				else
				{
					this.Meshes[j].Reserve(DynamicMesh.VerticesPerMesh, DynamicMesh.TrianglesPerMesh);
				}
			}
			this.VertexCount = vertex_count;
			this.TriangleCount = triangle_count;
		}

		// Token: 0x06006415 RID: 25621 RVA: 0x002582B0 File Offset: 0x002564B0
		public void Commit()
		{
			DynamicSubMesh[] meshes = this.Meshes;
			for (int i = 0; i < meshes.Length; i++)
			{
				meshes[i].Commit();
			}
			this.TriangleMeshIdx = 0;
			this.UVMeshIdx = 0;
			this.VertexMeshIdx = 0;
		}

		// Token: 0x06006416 RID: 25622 RVA: 0x002582F0 File Offset: 0x002564F0
		public void AddTriangle(int triangle)
		{
			if (this.Meshes[this.TriangleMeshIdx].AreTrianglesFull())
			{
				DynamicSubMesh[] meshes = this.Meshes;
				int num = this.TriangleMeshIdx + 1;
				this.TriangleMeshIdx = num;
				object obj = meshes[num];
			}
			this.Meshes[this.TriangleMeshIdx].AddTriangle(triangle);
		}

		// Token: 0x06006417 RID: 25623 RVA: 0x00258340 File Offset: 0x00256540
		public void AddUV(Vector2 uv)
		{
			DynamicSubMesh dynamicSubMesh = this.Meshes[this.UVMeshIdx];
			if (dynamicSubMesh.AreUVsFull())
			{
				DynamicSubMesh[] meshes = this.Meshes;
				int num = this.UVMeshIdx + 1;
				this.UVMeshIdx = num;
				dynamicSubMesh = meshes[num];
			}
			dynamicSubMesh.AddUV(uv);
		}

		// Token: 0x06006418 RID: 25624 RVA: 0x00258384 File Offset: 0x00256584
		public void AddVertex(Vector3 vertex)
		{
			DynamicSubMesh dynamicSubMesh = this.Meshes[this.VertexMeshIdx];
			if (dynamicSubMesh.AreVerticesFull())
			{
				DynamicSubMesh[] meshes = this.Meshes;
				int num = this.VertexMeshIdx + 1;
				this.VertexMeshIdx = num;
				dynamicSubMesh = meshes[num];
			}
			dynamicSubMesh.AddVertex(vertex);
		}

		// Token: 0x06006419 RID: 25625 RVA: 0x002583C8 File Offset: 0x002565C8
		public void Render(Vector3 position, Quaternion rotation, Material material, int layer, MaterialPropertyBlock property_block)
		{
			DynamicSubMesh[] meshes = this.Meshes;
			for (int i = 0; i < meshes.Length; i++)
			{
				meshes[i].Render(position, rotation, material, layer, property_block);
			}
		}

		// Token: 0x0400454D RID: 17741
		private static int TrianglesPerMesh = 65004;

		// Token: 0x0400454E RID: 17742
		private static int VerticesPerMesh = 4 * DynamicMesh.TrianglesPerMesh / 6;

		// Token: 0x0400454F RID: 17743
		public bool SetUVs;

		// Token: 0x04004550 RID: 17744
		public bool SetTriangles;

		// Token: 0x04004551 RID: 17745
		public string Name;

		// Token: 0x04004552 RID: 17746
		public Bounds Bounds;

		// Token: 0x04004553 RID: 17747
		public DynamicSubMesh[] Meshes = new DynamicSubMesh[0];

		// Token: 0x04004554 RID: 17748
		private int VertexCount;

		// Token: 0x04004555 RID: 17749
		private int TriangleCount;

		// Token: 0x04004556 RID: 17750
		private int VertexIdx;

		// Token: 0x04004557 RID: 17751
		private int UVIdx;

		// Token: 0x04004558 RID: 17752
		private int TriangleIdx;

		// Token: 0x04004559 RID: 17753
		private int TriangleMeshIdx;

		// Token: 0x0400455A RID: 17754
		private int VertexMeshIdx;

		// Token: 0x0400455B RID: 17755
		private int UVMeshIdx;
	}
}
