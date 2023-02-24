using System;

namespace MIConvexHull
{
	// Token: 0x020004A3 RID: 1187
	internal sealed class FaceConnector
	{
		// Token: 0x060032FF RID: 13055 RVA: 0x0006BAF7 File Offset: 0x00069CF7
		public FaceConnector(int dimension)
		{
			this.Vertices = new int[dimension - 1];
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x0006BB10 File Offset: 0x00069D10
		public void Update(ConvexFaceInternal face, int edgeIndex, int dim)
		{
			this.Face = face;
			this.EdgeIndex = edgeIndex;
			uint num = 23U;
			int[] vertices = face.Vertices;
			int num2 = 0;
			for (int i = 0; i < edgeIndex; i++)
			{
				this.Vertices[num2++] = vertices[i];
				num += 31U * num + (uint)vertices[i];
			}
			for (int i = edgeIndex + 1; i < vertices.Length; i++)
			{
				this.Vertices[num2++] = vertices[i];
				num += 31U * num + (uint)vertices[i];
			}
			this.HashCode = num;
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x0006BB90 File Offset: 0x00069D90
		public static bool AreConnectable(FaceConnector a, FaceConnector b, int dim)
		{
			if (a.HashCode != b.HashCode)
			{
				return false;
			}
			int[] vertices = a.Vertices;
			int[] vertices2 = b.Vertices;
			for (int i = 0; i < vertices.Length; i++)
			{
				if (vertices[i] != vertices2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x0006BBD4 File Offset: 0x00069DD4
		public static void Connect(FaceConnector a, FaceConnector b)
		{
			a.Face.AdjacentFaces[a.EdgeIndex] = b.Face.Index;
			b.Face.AdjacentFaces[b.EdgeIndex] = a.Face.Index;
		}

		// Token: 0x040011CC RID: 4556
		public int EdgeIndex;

		// Token: 0x040011CD RID: 4557
		public ConvexFaceInternal Face;

		// Token: 0x040011CE RID: 4558
		public uint HashCode;

		// Token: 0x040011CF RID: 4559
		public FaceConnector Next;

		// Token: 0x040011D0 RID: 4560
		public FaceConnector Previous;

		// Token: 0x040011D1 RID: 4561
		public int[] Vertices;
	}
}
