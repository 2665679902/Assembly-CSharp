using System;

namespace MIConvexHull
{
	// Token: 0x020004A6 RID: 1190
	internal class ObjectManager
	{
		// Token: 0x06003312 RID: 13074 RVA: 0x0006C5C8 File Offset: 0x0006A7C8
		public ObjectManager(ConvexHullAlgorithm hull)
		{
			this.Dimension = hull.NumOfDimensions;
			this.Hull = hull;
			this.FacePool = hull.FacePool;
			this.FacePoolSize = 0;
			this.FacePoolCapacity = hull.FacePool.Length;
			this.FreeFaceIndices = new IndexBuffer();
			this.EmptyBufferStack = new SimpleList<IndexBuffer>();
			this.DeferredFaceStack = new SimpleList<DeferredFace>();
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x0006C630 File Offset: 0x0006A830
		public void DepositFace(int faceIndex)
		{
			int[] adjacentFaces = this.FacePool[faceIndex].AdjacentFaces;
			for (int i = 0; i < adjacentFaces.Length; i++)
			{
				adjacentFaces[i] = -1;
			}
			this.FreeFaceIndices.Push(faceIndex);
		}

		// Token: 0x06003314 RID: 13076 RVA: 0x0006C66C File Offset: 0x0006A86C
		private void ReallocateFacePool()
		{
			ConvexFaceInternal[] array = new ConvexFaceInternal[2 * this.FacePoolCapacity];
			bool[] array2 = new bool[2 * this.FacePoolCapacity];
			Array.Copy(this.FacePool, array, this.FacePoolCapacity);
			Buffer.BlockCopy(this.Hull.AffectedFaceFlags, 0, array2, 0, this.FacePoolCapacity);
			this.FacePoolCapacity = 2 * this.FacePoolCapacity;
			this.Hull.FacePool = array;
			this.FacePool = array;
			this.Hull.AffectedFaceFlags = array2;
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x0006C6F0 File Offset: 0x0006A8F0
		private int CreateFace()
		{
			int facePoolSize = this.FacePoolSize;
			ConvexFaceInternal convexFaceInternal = new ConvexFaceInternal(this.Dimension, facePoolSize, this.GetVertexBuffer());
			this.FacePoolSize++;
			if (this.FacePoolSize > this.FacePoolCapacity)
			{
				this.ReallocateFacePool();
			}
			this.FacePool[facePoolSize] = convexFaceInternal;
			return facePoolSize;
		}

		// Token: 0x06003316 RID: 13078 RVA: 0x0006C743 File Offset: 0x0006A943
		public int GetFace()
		{
			if (this.FreeFaceIndices.Count > 0)
			{
				return this.FreeFaceIndices.Pop();
			}
			return this.CreateFace();
		}

		// Token: 0x06003317 RID: 13079 RVA: 0x0006C765 File Offset: 0x0006A965
		public void DepositConnector(FaceConnector connector)
		{
			if (this.ConnectorStack == null)
			{
				connector.Next = null;
				this.ConnectorStack = connector;
				return;
			}
			connector.Next = this.ConnectorStack;
			this.ConnectorStack = connector;
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x0006C791 File Offset: 0x0006A991
		public FaceConnector GetConnector()
		{
			if (this.ConnectorStack == null)
			{
				return new FaceConnector(this.Dimension);
			}
			FaceConnector connectorStack = this.ConnectorStack;
			this.ConnectorStack = this.ConnectorStack.Next;
			connectorStack.Next = null;
			return connectorStack;
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x0006C7C5 File Offset: 0x0006A9C5
		public void DepositVertexBuffer(IndexBuffer buffer)
		{
			buffer.Clear();
			this.EmptyBufferStack.Push(buffer);
		}

		// Token: 0x0600331A RID: 13082 RVA: 0x0006C7D9 File Offset: 0x0006A9D9
		public IndexBuffer GetVertexBuffer()
		{
			if (this.EmptyBufferStack.Count == 0)
			{
				return new IndexBuffer();
			}
			return this.EmptyBufferStack.Pop();
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x0006C7F9 File Offset: 0x0006A9F9
		public void DepositDeferredFace(DeferredFace face)
		{
			this.DeferredFaceStack.Push(face);
		}

		// Token: 0x0600331C RID: 13084 RVA: 0x0006C807 File Offset: 0x0006AA07
		public DeferredFace GetDeferredFace()
		{
			if (this.DeferredFaceStack.Count == 0)
			{
				return new DeferredFace();
			}
			return this.DeferredFaceStack.Pop();
		}

		// Token: 0x040011E6 RID: 4582
		private readonly int Dimension;

		// Token: 0x040011E7 RID: 4583
		private FaceConnector ConnectorStack;

		// Token: 0x040011E8 RID: 4584
		private readonly SimpleList<DeferredFace> DeferredFaceStack;

		// Token: 0x040011E9 RID: 4585
		private readonly SimpleList<IndexBuffer> EmptyBufferStack;

		// Token: 0x040011EA RID: 4586
		private ConvexFaceInternal[] FacePool;

		// Token: 0x040011EB RID: 4587
		private int FacePoolSize;

		// Token: 0x040011EC RID: 4588
		private int FacePoolCapacity;

		// Token: 0x040011ED RID: 4589
		private readonly IndexBuffer FreeFaceIndices;

		// Token: 0x040011EE RID: 4590
		private readonly ConvexHullAlgorithm Hull;
	}
}
