using System;
using System.Collections.Generic;
using System.Linq;

namespace MIConvexHull
{
	// Token: 0x020004A1 RID: 1185
	internal class ConvexHullAlgorithm
	{
		// Token: 0x060032E0 RID: 13024 RVA: 0x00069C20 File Offset: 0x00067E20
		internal static ConvexHull<TVertex, TFace> GetConvexHull<TVertex, TFace>(IList<TVertex> data, double PlaneDistanceTolerance) where TVertex : IVertex where TFace : ConvexFace<TVertex, TFace>, new()
		{
			ConvexHullAlgorithm convexHullAlgorithm = new ConvexHullAlgorithm(data.Cast<IVertex>().ToArray<IVertex>(), false, PlaneDistanceTolerance);
			convexHullAlgorithm.GetConvexHull();
			if (convexHullAlgorithm.NumOfDimensions == 2)
			{
				return convexHullAlgorithm.Return2DResultInOrder<TVertex, TFace>(data);
			}
			return new ConvexHull<TVertex, TFace>
			{
				Points = convexHullAlgorithm.GetHullVertices<TVertex>(data),
				Faces = convexHullAlgorithm.GetConvexFaces<TVertex, TFace>()
			};
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x00069C78 File Offset: 0x00067E78
		private ConvexHullAlgorithm(IVertex[] vertices, bool lift, double PlaneDistanceTolerance)
		{
			this.IsLifted = lift;
			this.Vertices = vertices;
			this.NumberOfVertices = vertices.Length;
			this.NumOfDimensions = this.DetermineDimension();
			if (this.IsLifted)
			{
				this.NumOfDimensions++;
			}
			if (this.NumOfDimensions < 2)
			{
				throw new InvalidOperationException("Dimension of the input must be 2 or greater.");
			}
			if (this.NumberOfVertices <= this.NumOfDimensions)
			{
				throw new ArgumentException("There are too few vertices (m) for the n-dimensional space. (m must be greater than the n, but m is " + this.NumberOfVertices.ToString() + " and n is " + this.NumOfDimensions.ToString());
			}
			this.PlaneDistanceTolerance = PlaneDistanceTolerance;
			this.UnprocessedFaces = new FaceList();
			this.ConvexFaces = new IndexBuffer();
			this.FacePool = new ConvexFaceInternal[(this.NumOfDimensions + 1) * 10];
			this.AffectedFaceFlags = new bool[(this.NumOfDimensions + 1) * 10];
			this.ObjectManager = new ObjectManager(this);
			this.Center = new double[this.NumOfDimensions];
			this.TraverseStack = new IndexBuffer();
			this.UpdateBuffer = new int[this.NumOfDimensions];
			this.UpdateIndices = new int[this.NumOfDimensions];
			this.EmptyBuffer = new IndexBuffer();
			this.AffectedFaceBuffer = new IndexBuffer();
			this.ConeFaceBuffer = new SimpleList<DeferredFace>();
			this.SingularVertices = new HashSet<int>();
			this.BeyondBuffer = new IndexBuffer();
			this.ConnectorTable = new ConnectorList[2017];
			for (int i = 0; i < 2017; i++)
			{
				this.ConnectorTable[i] = new ConnectorList();
			}
			this.VertexVisited = new bool[this.NumberOfVertices];
			this.Positions = new double[this.NumberOfVertices * this.NumOfDimensions];
			this.boundingBoxPoints = new List<int>[this.NumOfDimensions];
			this.minima = new double[this.NumOfDimensions];
			this.maxima = new double[this.NumOfDimensions];
			this.mathHelper = new MathHelper(this.NumOfDimensions, this.Positions);
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x00069E7C File Offset: 0x0006807C
		private int DetermineDimension()
		{
			Random random = new Random();
			List<int> list = new List<int>();
			for (int i = 0; i < 10; i++)
			{
				list.Add(this.Vertices[random.Next(this.NumberOfVertices)].Position.Length);
			}
			int num = list.Min();
			if (num != list.Max())
			{
				throw new ArgumentException("Invalid input data (non-uniform dimension).");
			}
			return num;
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x00069EDC File Offset: 0x000680DC
		private void GetConvexHull()
		{
			this.SerializeVerticesToPositions();
			this.FindBoundingBoxPoints();
			this.ShiftAndScalePositions();
			this.CreateInitialSimplex();
			while (this.UnprocessedFaces.First != null)
			{
				ConvexFaceInternal first = this.UnprocessedFaces.First;
				this.CurrentVertex = first.FurthestVertex;
				this.UpdateCenter();
				this.TagAffectedFaces(first);
				if (!this.SingularVertices.Contains(this.CurrentVertex) && this.CreateCone())
				{
					this.CommitCone();
				}
				else
				{
					this.HandleSingular();
				}
				int count = this.AffectedFaceBuffer.Count;
				for (int i = 0; i < count; i++)
				{
					this.AffectedFaceFlags[this.AffectedFaceBuffer[i]] = false;
				}
			}
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x00069F90 File Offset: 0x00068190
		private void SerializeVerticesToPositions()
		{
			int num = 0;
			if (this.IsLifted)
			{
				foreach (IVertex vertex in this.Vertices)
				{
					double num2 = 0.0;
					int num3 = this.NumOfDimensions - 1;
					for (int j = 0; j < num3; j++)
					{
						double num4 = vertex.Position[j];
						this.Positions[num++] = num4;
						num2 += num4 * num4;
					}
					this.Positions[num++] = num2;
				}
				return;
			}
			foreach (IVertex vertex2 in this.Vertices)
			{
				for (int k = 0; k < this.NumOfDimensions; k++)
				{
					this.Positions[num++] = vertex2.Position[k];
				}
			}
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x0006A060 File Offset: 0x00068260
		private void FindBoundingBoxPoints()
		{
			this.indexOfDimensionWithLeastExtremes = -1;
			int num = int.MaxValue;
			int i;
			int i2;
			for (i = 0; i < this.NumOfDimensions; i = i2 + 1)
			{
				List<int> list = new List<int>();
				List<int> list2 = new List<int>();
				double min = double.PositiveInfinity;
				double num2 = double.NegativeInfinity;
				Predicate<int> <>9__0;
				Predicate<int> <>9__1;
				for (int j = 0; j < this.NumberOfVertices; j++)
				{
					double coordinate = this.GetCoordinate(j, i);
					double num3 = min - coordinate;
					if (num3 >= this.PlaneDistanceTolerance)
					{
						min = coordinate;
						list.Clear();
						list.Add(j);
					}
					else if (num3 > 0.0)
					{
						min = coordinate;
						List<int> list3 = list;
						Predicate<int> predicate;
						if ((predicate = <>9__0) == null)
						{
							predicate = (<>9__0 = (int index) => min - this.GetCoordinate(index, i) > this.PlaneDistanceTolerance);
						}
						list3.RemoveAll(predicate);
						list.Add(j);
					}
					else if (num3 > -this.PlaneDistanceTolerance)
					{
						list.Add(j);
					}
					num3 = coordinate - num2;
					if (num3 >= this.PlaneDistanceTolerance)
					{
						num2 = coordinate;
						list2.Clear();
						list2.Add(j);
					}
					else if (num3 > 0.0)
					{
						num2 = coordinate;
						List<int> list4 = list2;
						Predicate<int> predicate2;
						if ((predicate2 = <>9__1) == null)
						{
							predicate2 = (<>9__1 = (int index) => min - this.GetCoordinate(index, i) > this.PlaneDistanceTolerance);
						}
						list4.RemoveAll(predicate2);
						list2.Add(j);
					}
					else if (num3 > -this.PlaneDistanceTolerance)
					{
						list2.Add(j);
					}
				}
				this.minima[i] = min;
				this.maxima[i] = num2;
				list.AddRange(list2);
				if (list.Count < num)
				{
					num = list.Count;
					this.indexOfDimensionWithLeastExtremes = i;
				}
				this.boundingBoxPoints[i] = list;
				i2 = i;
			}
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x0006A28C File Offset: 0x0006848C
		private void ShiftAndScalePositions()
		{
			int num = this.Positions.Length;
			if (this.IsLifted)
			{
				int num2 = this.NumOfDimensions - 1;
				double num3 = 2.0 / (this.minima.Sum((double x) => Math.Abs(x)) + this.maxima.Sum((double x) => Math.Abs(x)) - Math.Abs(this.maxima[num2]) - Math.Abs(this.minima[num2]));
				this.minima[num2] *= num3;
				this.maxima[num2] *= num3;
				for (int i = num2; i < num; i += this.NumOfDimensions)
				{
					this.Positions[i] *= num3;
				}
			}
			double[] array = new double[this.NumOfDimensions];
			for (int j = 0; j < this.NumOfDimensions; j++)
			{
				if (this.maxima[j] == this.minima[j])
				{
					array[j] = 0.0;
				}
				else
				{
					array[j] = this.maxima[j] - this.minima[j] - this.minima[j];
				}
			}
			for (int k = 0; k < num; k++)
			{
				this.Positions[k] += array[k % this.NumOfDimensions];
			}
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x0006A40C File Offset: 0x0006860C
		private void CreateInitialSimplex()
		{
			List<int> list = this.FindInitialPoints();
			int[] array = new int[this.NumOfDimensions + 1];
			for (int i = 0; i < this.NumOfDimensions + 1; i++)
			{
				int[] array2 = new int[this.NumOfDimensions];
				int j = 0;
				int num = 0;
				while (j <= this.NumOfDimensions)
				{
					if (i != j)
					{
						if (j == list.Count)
						{
						}
						int num2 = list[j];
						array2[num++] = num2;
					}
					j++;
				}
				ConvexFaceInternal convexFaceInternal = this.FacePool[this.ObjectManager.GetFace()];
				convexFaceInternal.Vertices = array2;
				Array.Sort<int>(array2);
				this.mathHelper.CalculateFacePlane(convexFaceInternal, this.Center);
				array[i] = convexFaceInternal.Index;
			}
			for (int k = 0; k < this.NumOfDimensions; k++)
			{
				for (int l = k + 1; l < this.NumOfDimensions + 1; l++)
				{
					this.UpdateAdjacency(this.FacePool[array[k]], this.FacePool[array[l]]);
				}
			}
			foreach (int num3 in array)
			{
				ConvexFaceInternal convexFaceInternal2 = this.FacePool[num3];
				this.FindBeyondVertices(convexFaceInternal2);
				if (convexFaceInternal2.VerticesBeyond.Count == 0)
				{
					this.ConvexFaces.Add(convexFaceInternal2.Index);
				}
				else
				{
					this.UnprocessedFaces.Add(convexFaceInternal2);
				}
			}
			foreach (int num4 in list)
			{
				this.VertexVisited[num4] = false;
			}
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x0006A5C4 File Offset: 0x000687C4
		private List<int> FindInitialPoints()
		{
			double num = this.maxima.Sum() * (double)this.NumOfDimensions * (double)this.NumberOfVertices;
			int num2 = this.boundingBoxPoints[this.indexOfDimensionWithLeastExtremes].First<int>();
			int num3 = this.boundingBoxPoints[this.indexOfDimensionWithLeastExtremes].Last<int>();
			this.boundingBoxPoints[this.indexOfDimensionWithLeastExtremes].RemoveAt(0);
			this.boundingBoxPoints[this.indexOfDimensionWithLeastExtremes].RemoveAt(this.boundingBoxPoints[this.indexOfDimensionWithLeastExtremes].Count - 1);
			List<int> list = new List<int> { num2, num3 };
			this.VertexVisited[num2] = (this.VertexVisited[num3] = true);
			this.CurrentVertex = num2;
			this.UpdateCenter();
			this.CurrentVertex = num3;
			this.UpdateCenter();
			double[][] array = new double[this.NumOfDimensions][];
			array[0] = this.mathHelper.VectorBetweenVertices(num3, num2);
			List<int> list2 = this.boundingBoxPoints.SelectMany((List<int> x) => x).ToList<int>();
			int num4 = 1;
			while (num4 < this.NumOfDimensions && list2.Any<int>())
			{
				int num5 = -1;
				double[] array2 = new double[0];
				double num6 = 0.0;
				for (int i = list2.Count - 1; i >= 0; i--)
				{
					int num7 = list2[i];
					if (list.Contains(num7))
					{
						list2.RemoveAt(i);
					}
					else
					{
						array[num4] = this.mathHelper.VectorBetweenVertices(num7, num2);
						double simplexVolume = this.mathHelper.GetSimplexVolume(array, num4, num);
						if (num6 < simplexVolume)
						{
							num6 = simplexVolume;
							num5 = num7;
							array2 = array[num4];
						}
					}
				}
				list2.Remove(num5);
				if (num5 == -1)
				{
					break;
				}
				list.Add(num5);
				array[num4++] = array2;
				this.CurrentVertex = num5;
				this.UpdateCenter();
			}
			if (list.Count <= this.NumOfDimensions)
			{
				List<int> list3 = Enumerable.Range(0, this.NumberOfVertices).ToList<int>();
				while (num4 < this.NumOfDimensions && list3.Any<int>())
				{
					int num8 = -1;
					double[] array3 = new double[0];
					double num9 = 0.0;
					for (int j = list3.Count - 1; j >= 0; j--)
					{
						int num10 = list3[j];
						if (list.Contains(num10))
						{
							list3.RemoveAt(j);
						}
						else
						{
							array[num4] = this.mathHelper.VectorBetweenVertices(num10, num2);
							double simplexVolume2 = this.mathHelper.GetSimplexVolume(array, num4, num);
							if (num9 < simplexVolume2)
							{
								num9 = simplexVolume2;
								num8 = num10;
								array3 = array[num4];
							}
						}
					}
					list3.Remove(num8);
					if (num8 == -1)
					{
						break;
					}
					list.Add(num8);
					array[num4++] = array3;
					this.CurrentVertex = num8;
					this.UpdateCenter();
				}
			}
			if (list.Count <= this.NumOfDimensions && this.IsLifted)
			{
				List<int> list4 = Enumerable.Range(0, this.NumberOfVertices).ToList<int>();
				while (num4 < this.NumOfDimensions && list4.Any<int>())
				{
					int num11 = -1;
					double[] array4 = new double[0];
					double num12 = 0.0;
					for (int k = list4.Count - 1; k >= 0; k--)
					{
						int num13 = list4[k];
						if (list.Contains(num13))
						{
							list4.RemoveAt(k);
						}
						else
						{
							this.mathHelper.RandomOffsetToLift(num13);
							array[num4] = this.mathHelper.VectorBetweenVertices(num13, num2);
							double simplexVolume3 = this.mathHelper.GetSimplexVolume(array, num4, num);
							if (num12 < simplexVolume3)
							{
								num12 = simplexVolume3;
								num11 = num13;
								array4 = array[num4];
							}
						}
					}
					list4.Remove(num11);
					if (num11 == -1)
					{
						break;
					}
					list.Add(num11);
					array[num4++] = array4;
					this.CurrentVertex = num11;
					this.UpdateCenter();
				}
			}
			if (list.Count <= this.NumOfDimensions && this.IsLifted)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"The input data is degenerate. It appears to exist in ",
					this.NumOfDimensions.ToString(),
					" dimensions, but it is a ",
					(this.NumOfDimensions - 1).ToString(),
					" dimensional set (i.e. the point of collinear, coplanar, or co-hyperplanar.)"
				}));
			}
			return list;
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x0006AA24 File Offset: 0x00068C24
		private void UpdateAdjacency(ConvexFaceInternal l, ConvexFaceInternal r)
		{
			int[] vertices = l.Vertices;
			int[] vertices2 = r.Vertices;
			int i;
			for (i = 0; i < vertices.Length; i++)
			{
				this.VertexVisited[vertices[i]] = false;
			}
			for (i = 0; i < vertices2.Length; i++)
			{
				this.VertexVisited[vertices2[i]] = true;
			}
			i = 0;
			while (i < vertices.Length && this.VertexVisited[vertices[i]])
			{
				i++;
			}
			if (i == this.NumOfDimensions)
			{
				return;
			}
			for (int j = i + 1; j < vertices.Length; j++)
			{
				if (!this.VertexVisited[vertices[j]])
				{
					return;
				}
			}
			l.AdjacentFaces[i] = r.Index;
			for (i = 0; i < vertices.Length; i++)
			{
				this.VertexVisited[vertices[i]] = false;
			}
			i = 0;
			while (i < vertices2.Length && !this.VertexVisited[vertices2[i]])
			{
				i++;
			}
			r.AdjacentFaces[i] = l.Index;
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x0006AB04 File Offset: 0x00068D04
		private void FindBeyondVertices(ConvexFaceInternal face)
		{
			IndexBuffer verticesBeyond = face.VerticesBeyond;
			this.MaxDistance = double.NegativeInfinity;
			this.FurthestVertex = 0;
			for (int i = 0; i < this.NumberOfVertices; i++)
			{
				if (!this.VertexVisited[i])
				{
					this.IsBeyond(face, verticesBeyond, i);
				}
			}
			face.FurthestVertex = this.FurthestVertex;
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x0006AB5E File Offset: 0x00068D5E
		private void TagAffectedFaces(ConvexFaceInternal currentFace)
		{
			this.AffectedFaceBuffer.Clear();
			this.AffectedFaceBuffer.Add(currentFace.Index);
			this.TraverseAffectedFaces(currentFace.Index);
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x0006AB88 File Offset: 0x00068D88
		private void TraverseAffectedFaces(int currentFace)
		{
			this.TraverseStack.Clear();
			this.TraverseStack.Push(currentFace);
			this.AffectedFaceFlags[currentFace] = true;
			while (this.TraverseStack.Count > 0)
			{
				ConvexFaceInternal convexFaceInternal = this.FacePool[this.TraverseStack.Pop()];
				for (int i = 0; i < this.NumOfDimensions; i++)
				{
					int num = convexFaceInternal.AdjacentFaces[i];
					if (!this.AffectedFaceFlags[num] && this.mathHelper.GetVertexDistance(this.CurrentVertex, this.FacePool[num]) >= this.PlaneDistanceTolerance)
					{
						this.AffectedFaceBuffer.Add(num);
						this.AffectedFaceFlags[num] = true;
						this.TraverseStack.Push(num);
					}
				}
			}
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x0006AC41 File Offset: 0x00068E41
		private DeferredFace MakeDeferredFace(ConvexFaceInternal face, int faceIndex, ConvexFaceInternal pivot, int pivotIndex, ConvexFaceInternal oldFace)
		{
			DeferredFace deferredFace = this.ObjectManager.GetDeferredFace();
			deferredFace.Face = face;
			deferredFace.FaceIndex = faceIndex;
			deferredFace.Pivot = pivot;
			deferredFace.PivotIndex = pivotIndex;
			deferredFace.OldFace = oldFace;
			return deferredFace;
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x0006AC74 File Offset: 0x00068E74
		private void ConnectFace(FaceConnector connector)
		{
			uint num = connector.HashCode % 2017U;
			ConnectorList connectorList = this.ConnectorTable[(int)num];
			for (FaceConnector faceConnector = connectorList.First; faceConnector != null; faceConnector = faceConnector.Next)
			{
				if (FaceConnector.AreConnectable(connector, faceConnector, this.NumOfDimensions))
				{
					connectorList.Remove(faceConnector);
					FaceConnector.Connect(faceConnector, connector);
					faceConnector.Face = null;
					connector.Face = null;
					this.ObjectManager.DepositConnector(faceConnector);
					this.ObjectManager.DepositConnector(connector);
					return;
				}
			}
			connectorList.Add(connector);
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x0006ACF8 File Offset: 0x00068EF8
		private bool CreateCone()
		{
			int currentVertex = this.CurrentVertex;
			this.ConeFaceBuffer.Clear();
			for (int i = 0; i < this.AffectedFaceBuffer.Count; i++)
			{
				int num = this.AffectedFaceBuffer[i];
				ConvexFaceInternal convexFaceInternal = this.FacePool[num];
				int num2 = 0;
				for (int j = 0; j < this.NumOfDimensions; j++)
				{
					int num3 = convexFaceInternal.AdjacentFaces[j];
					if (!this.AffectedFaceFlags[num3])
					{
						this.UpdateBuffer[num2] = num3;
						this.UpdateIndices[num2] = j;
						num2++;
					}
				}
				for (int k = 0; k < num2; k++)
				{
					ConvexFaceInternal convexFaceInternal2 = this.FacePool[this.UpdateBuffer[k]];
					int num4 = 0;
					int[] adjacentFaces = convexFaceInternal2.AdjacentFaces;
					for (int l = 0; l < adjacentFaces.Length; l++)
					{
						if (num == adjacentFaces[l])
						{
							num4 = l;
							break;
						}
					}
					int num5 = this.UpdateIndices[k];
					int face = this.ObjectManager.GetFace();
					ConvexFaceInternal convexFaceInternal3 = this.FacePool[face];
					int[] vertices = convexFaceInternal3.Vertices;
					for (int m = 0; m < this.NumOfDimensions; m++)
					{
						vertices[m] = convexFaceInternal.Vertices[m];
					}
					int num6 = vertices[num5];
					int num7;
					if (currentVertex < num6)
					{
						num7 = 0;
						for (int n = num5 - 1; n >= 0; n--)
						{
							if (vertices[n] <= currentVertex)
							{
								num7 = n + 1;
								break;
							}
							vertices[n + 1] = vertices[n];
						}
					}
					else
					{
						num7 = this.NumOfDimensions - 1;
						for (int num8 = num5 + 1; num8 < this.NumOfDimensions; num8++)
						{
							if (vertices[num8] >= currentVertex)
							{
								num7 = num8 - 1;
								break;
							}
							vertices[num8 - 1] = vertices[num8];
						}
					}
					vertices[num7] = this.CurrentVertex;
					if (!this.mathHelper.CalculateFacePlane(convexFaceInternal3, this.Center))
					{
						return false;
					}
					this.ConeFaceBuffer.Add(this.MakeDeferredFace(convexFaceInternal3, num7, convexFaceInternal2, num4, convexFaceInternal));
				}
			}
			return true;
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x0006AEF8 File Offset: 0x000690F8
		private void CommitCone()
		{
			for (int i = 0; i < this.ConeFaceBuffer.Count; i++)
			{
				DeferredFace deferredFace = this.ConeFaceBuffer[i];
				ConvexFaceInternal face = deferredFace.Face;
				ConvexFaceInternal pivot = deferredFace.Pivot;
				ConvexFaceInternal oldFace = deferredFace.OldFace;
				int faceIndex = deferredFace.FaceIndex;
				face.AdjacentFaces[faceIndex] = pivot.Index;
				pivot.AdjacentFaces[deferredFace.PivotIndex] = face.Index;
				for (int j = 0; j < this.NumOfDimensions; j++)
				{
					if (j != faceIndex)
					{
						FaceConnector connector = this.ObjectManager.GetConnector();
						connector.Update(face, j, this.NumOfDimensions);
						this.ConnectFace(connector);
					}
				}
				if (pivot.VerticesBeyond.Count == 0)
				{
					this.FindBeyondVertices(face, oldFace.VerticesBeyond);
				}
				else if (pivot.VerticesBeyond.Count < oldFace.VerticesBeyond.Count)
				{
					this.FindBeyondVertices(face, pivot.VerticesBeyond, oldFace.VerticesBeyond);
				}
				else
				{
					this.FindBeyondVertices(face, oldFace.VerticesBeyond, pivot.VerticesBeyond);
				}
				if (face.VerticesBeyond.Count == 0)
				{
					this.ConvexFaces.Add(face.Index);
					this.UnprocessedFaces.Remove(face);
					this.ObjectManager.DepositVertexBuffer(face.VerticesBeyond);
					face.VerticesBeyond = this.EmptyBuffer;
				}
				else
				{
					this.UnprocessedFaces.Add(face);
				}
				this.ObjectManager.DepositDeferredFace(deferredFace);
			}
			for (int k = 0; k < this.AffectedFaceBuffer.Count; k++)
			{
				int num = this.AffectedFaceBuffer[k];
				this.UnprocessedFaces.Remove(this.FacePool[num]);
				this.ObjectManager.DepositFace(num);
			}
		}

		// Token: 0x060032F1 RID: 13041 RVA: 0x0006B0BC File Offset: 0x000692BC
		private void IsBeyond(ConvexFaceInternal face, IndexBuffer beyondVertices, int v)
		{
			double vertexDistance = this.mathHelper.GetVertexDistance(v, face);
			if (vertexDistance >= this.PlaneDistanceTolerance)
			{
				if (vertexDistance > this.MaxDistance)
				{
					if (vertexDistance - this.MaxDistance < this.PlaneDistanceTolerance)
					{
						if (this.LexCompare(v, this.FurthestVertex) > 0)
						{
							this.MaxDistance = vertexDistance;
							this.FurthestVertex = v;
						}
					}
					else
					{
						this.MaxDistance = vertexDistance;
						this.FurthestVertex = v;
					}
				}
				beyondVertices.Add(v);
			}
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x0006B130 File Offset: 0x00069330
		private int LexCompare(int u, int v)
		{
			int num = u * this.NumOfDimensions;
			int num2 = v * this.NumOfDimensions;
			for (int i = 0; i < this.NumOfDimensions; i++)
			{
				double num3 = this.Positions[num + i];
				double num4 = this.Positions[num2 + i];
				int num5 = num3.CompareTo(num4);
				if (num5 != 0)
				{
					return num5;
				}
			}
			return 0;
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x0006B18C File Offset: 0x0006938C
		private void FindBeyondVertices(ConvexFaceInternal face, IndexBuffer beyond, IndexBuffer beyond1)
		{
			IndexBuffer beyondBuffer = this.BeyondBuffer;
			this.MaxDistance = double.NegativeInfinity;
			this.FurthestVertex = 0;
			for (int i = 0; i < beyond1.Count; i++)
			{
				this.VertexVisited[beyond1[i]] = true;
			}
			this.VertexVisited[this.CurrentVertex] = false;
			for (int j = 0; j < beyond.Count; j++)
			{
				int num = beyond[j];
				if (num != this.CurrentVertex)
				{
					this.VertexVisited[num] = false;
					this.IsBeyond(face, beyondBuffer, num);
				}
			}
			for (int k = 0; k < beyond1.Count; k++)
			{
				int num = beyond1[k];
				if (this.VertexVisited[num])
				{
					this.IsBeyond(face, beyondBuffer, num);
				}
			}
			face.FurthestVertex = this.FurthestVertex;
			IndexBuffer verticesBeyond = face.VerticesBeyond;
			face.VerticesBeyond = beyondBuffer;
			if (verticesBeyond.Count > 0)
			{
				verticesBeyond.Clear();
			}
			this.BeyondBuffer = verticesBeyond;
		}

		// Token: 0x060032F4 RID: 13044 RVA: 0x0006B280 File Offset: 0x00069480
		private void FindBeyondVertices(ConvexFaceInternal face, IndexBuffer beyond)
		{
			IndexBuffer beyondBuffer = this.BeyondBuffer;
			this.MaxDistance = double.NegativeInfinity;
			this.FurthestVertex = 0;
			for (int i = 0; i < beyond.Count; i++)
			{
				int num = beyond[i];
				if (num != this.CurrentVertex)
				{
					this.IsBeyond(face, beyondBuffer, num);
				}
			}
			face.FurthestVertex = this.FurthestVertex;
			IndexBuffer verticesBeyond = face.VerticesBeyond;
			face.VerticesBeyond = beyondBuffer;
			if (verticesBeyond.Count > 0)
			{
				verticesBeyond.Clear();
			}
			this.BeyondBuffer = verticesBeyond;
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x0006B308 File Offset: 0x00069508
		private void UpdateCenter()
		{
			for (int i = 0; i < this.NumOfDimensions; i++)
			{
				this.Center[i] *= (double)this.ConvexHullSize;
			}
			this.ConvexHullSize++;
			double num = 1.0 / (double)this.ConvexHullSize;
			int num2 = this.CurrentVertex * this.NumOfDimensions;
			for (int j = 0; j < this.NumOfDimensions; j++)
			{
				this.Center[j] = num * (this.Center[j] + this.Positions[num2 + j]);
			}
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x0006B39C File Offset: 0x0006959C
		private void RollbackCenter()
		{
			for (int i = 0; i < this.NumOfDimensions; i++)
			{
				this.Center[i] *= (double)this.ConvexHullSize;
			}
			this.ConvexHullSize--;
			double num = ((this.ConvexHullSize > 0) ? (1.0 / (double)this.ConvexHullSize) : 0.0);
			int num2 = this.CurrentVertex * this.NumOfDimensions;
			for (int j = 0; j < this.NumOfDimensions; j++)
			{
				this.Center[j] = num * (this.Center[j] - this.Positions[num2 + j]);
			}
		}

		// Token: 0x060032F7 RID: 13047 RVA: 0x0006B444 File Offset: 0x00069644
		private void HandleSingular()
		{
			this.RollbackCenter();
			this.SingularVertices.Add(this.CurrentVertex);
			for (int i = 0; i < this.AffectedFaceBuffer.Count; i++)
			{
				ConvexFaceInternal convexFaceInternal = this.FacePool[this.AffectedFaceBuffer[i]];
				IndexBuffer verticesBeyond = convexFaceInternal.VerticesBeyond;
				for (int j = 0; j < verticesBeyond.Count; j++)
				{
					this.SingularVertices.Add(verticesBeyond[j]);
				}
				this.ConvexFaces.Add(convexFaceInternal.Index);
				this.UnprocessedFaces.Remove(convexFaceInternal);
				this.ObjectManager.DepositVertexBuffer(convexFaceInternal.VerticesBeyond);
				convexFaceInternal.VerticesBeyond = this.EmptyBuffer;
			}
		}

		// Token: 0x060032F8 RID: 13048 RVA: 0x0006B4FB File Offset: 0x000696FB
		private double GetCoordinate(int vIndex, int dimension)
		{
			return this.Positions[vIndex * this.NumOfDimensions + dimension];
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x0006B510 File Offset: 0x00069710
		private TVertex[] GetHullVertices<TVertex>(IList<TVertex> data)
		{
			int count = this.ConvexFaces.Count;
			int num = 0;
			for (int i = 0; i < this.NumberOfVertices; i++)
			{
				this.VertexVisited[i] = false;
			}
			for (int j = 0; j < count; j++)
			{
				foreach (int num2 in this.FacePool[this.ConvexFaces[j]].Vertices)
				{
					if (!this.VertexVisited[num2])
					{
						this.VertexVisited[num2] = true;
						num++;
					}
				}
			}
			TVertex[] array = new TVertex[num];
			for (int l = 0; l < this.NumberOfVertices; l++)
			{
				if (this.VertexVisited[l])
				{
					array[--num] = data[l];
				}
			}
			return array;
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x0006B5E0 File Offset: 0x000697E0
		private TFace[] GetConvexFaces<TVertex, TFace>() where TVertex : IVertex where TFace : ConvexFace<TVertex, TFace>, new()
		{
			IndexBuffer convexFaces = this.ConvexFaces;
			int count = convexFaces.Count;
			TFace[] array = new TFace[count];
			for (int i = 0; i < count; i++)
			{
				ConvexFaceInternal convexFaceInternal = this.FacePool[convexFaces[i]];
				TVertex[] array2 = new TVertex[this.NumOfDimensions];
				for (int j = 0; j < this.NumOfDimensions; j++)
				{
					array2[j] = (TVertex)((object)this.Vertices[convexFaceInternal.Vertices[j]]);
				}
				TFace[] array3 = array;
				int num = i;
				TFace tface = new TFace();
				tface.Vertices = array2;
				tface.Adjacency = new TFace[this.NumOfDimensions];
				tface.Normal = (this.IsLifted ? null : convexFaceInternal.Normal);
				array3[num] = tface;
				convexFaceInternal.Tag = i;
			}
			for (int k = 0; k < count; k++)
			{
				ConvexFaceInternal convexFaceInternal2 = this.FacePool[convexFaces[k]];
				TFace tface2 = array[k];
				for (int l = 0; l < this.NumOfDimensions; l++)
				{
					if (convexFaceInternal2.AdjacentFaces[l] >= 0)
					{
						tface2.Adjacency[l] = array[this.FacePool[convexFaceInternal2.AdjacentFaces[l]].Tag];
					}
				}
				if (convexFaceInternal2.IsNormalFlipped)
				{
					TVertex tvertex = tface2.Vertices[0];
					tface2.Vertices[0] = tface2.Vertices[this.NumOfDimensions - 1];
					tface2.Vertices[this.NumOfDimensions - 1] = tvertex;
					TFace tface3 = tface2.Adjacency[0];
					tface2.Adjacency[0] = tface2.Adjacency[this.NumOfDimensions - 1];
					tface2.Adjacency[this.NumOfDimensions - 1] = tface3;
				}
			}
			return array;
		}

		// Token: 0x060032FB RID: 13051 RVA: 0x0006B7FC File Offset: 0x000699FC
		private ConvexHull<TVertex, TFace> Return2DResultInOrder<TVertex, TFace>(IList<TVertex> data) where TVertex : IVertex where TFace : ConvexFace<TVertex, TFace>, new()
		{
			TFace[] convexFaces = this.GetConvexFaces<TVertex, TFace>();
			int num = convexFaces.Length;
			Dictionary<TVertex, TFace> dictionary = new Dictionary<TVertex, TFace>();
			foreach (TFace tface in convexFaces)
			{
				dictionary.Add(tface.Vertices[1], tface);
			}
			TVertex tvertex = convexFaces[0].Vertices[1];
			TVertex tvertex2 = convexFaces[0].Vertices[0];
			List<TVertex> list = new List<TVertex>();
			list.Add(tvertex);
			List<TFace> list2 = new List<TFace>();
			list2.Add(convexFaces[1]);
			int num2 = 0;
			int num3 = 0;
			while (!tvertex2.Equals(tvertex))
			{
				list.Add(tvertex2);
				TFace tface2 = dictionary[tvertex2];
				list2.Add(tface2);
				double num4 = tvertex2.Position[0];
				TVertex tvertex3 = list[num2];
				if (num4 < tvertex3.Position[0])
				{
					goto IL_149;
				}
				double num5 = tvertex2.Position[0];
				tvertex3 = list[num2];
				if (num5 == tvertex3.Position[0])
				{
					double num6 = tvertex2.Position[1];
					tvertex3 = list[num2];
					if (num6 <= tvertex3.Position[1])
					{
						goto IL_149;
					}
				}
				IL_14D:
				num3++;
				tvertex2 = tface2.Vertices[0];
				continue;
				IL_149:
				num2 = num3;
				goto IL_14D;
			}
			TVertex[] array2 = new TVertex[num];
			for (int j = 0; j < num; j++)
			{
				int num7 = (j + num2) % num;
				array2[j] = list[num7];
				convexFaces[j] = list2[num7];
			}
			return new ConvexHull<TVertex, TFace>
			{
				Points = array2,
				Faces = convexFaces
			};
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x0006B9E0 File Offset: 0x00069BE0
		internal static TCell[] GetDelaunayTriangulation<TVertex, TCell>(IList<TVertex> data) where TVertex : IVertex where TCell : TriangulationCell<TVertex, TCell>, new()
		{
			ConvexHullAlgorithm convexHullAlgorithm = new ConvexHullAlgorithm(data.Cast<IVertex>().ToArray<IVertex>(), true, 1E-10);
			convexHullAlgorithm.GetConvexHull();
			convexHullAlgorithm.RemoveUpperFaces();
			return convexHullAlgorithm.GetConvexFaces<TVertex, TCell>();
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x0006BA10 File Offset: 0x00069C10
		private void RemoveUpperFaces()
		{
			IndexBuffer convexFaces = this.ConvexFaces;
			int num = this.NumOfDimensions - 1;
			for (int i = convexFaces.Count - 1; i >= 0; i--)
			{
				int num2 = convexFaces[i];
				ConvexFaceInternal convexFaceInternal = this.FacePool[num2];
				if (convexFaceInternal.Normal[num] >= 0.0)
				{
					for (int j = 0; j < convexFaceInternal.AdjacentFaces.Length; j++)
					{
						int num3 = convexFaceInternal.AdjacentFaces[j];
						if (num3 >= 0)
						{
							ConvexFaceInternal convexFaceInternal2 = this.FacePool[num3];
							for (int k = 0; k < convexFaceInternal2.AdjacentFaces.Length; k++)
							{
								if (convexFaceInternal2.AdjacentFaces[k] == num2)
								{
									convexFaceInternal2.AdjacentFaces[k] = -1;
								}
							}
						}
					}
					convexFaces[i] = convexFaces[convexFaces.Count - 1];
					convexFaces.Pop();
				}
			}
		}

		// Token: 0x040011A8 RID: 4520
		internal readonly int NumOfDimensions;

		// Token: 0x040011A9 RID: 4521
		private readonly bool IsLifted;

		// Token: 0x040011AA RID: 4522
		private readonly double PlaneDistanceTolerance;

		// Token: 0x040011AB RID: 4523
		private readonly IVertex[] Vertices;

		// Token: 0x040011AC RID: 4524
		private double[] Positions;

		// Token: 0x040011AD RID: 4525
		private readonly bool[] VertexVisited;

		// Token: 0x040011AE RID: 4526
		private readonly int NumberOfVertices;

		// Token: 0x040011AF RID: 4527
		internal ConvexFaceInternal[] FacePool;

		// Token: 0x040011B0 RID: 4528
		internal bool[] AffectedFaceFlags;

		// Token: 0x040011B1 RID: 4529
		private int ConvexHullSize;

		// Token: 0x040011B2 RID: 4530
		private readonly FaceList UnprocessedFaces;

		// Token: 0x040011B3 RID: 4531
		private readonly IndexBuffer ConvexFaces;

		// Token: 0x040011B4 RID: 4532
		private int CurrentVertex;

		// Token: 0x040011B5 RID: 4533
		private double MaxDistance;

		// Token: 0x040011B6 RID: 4534
		private int FurthestVertex;

		// Token: 0x040011B7 RID: 4535
		private readonly double[] Center;

		// Token: 0x040011B8 RID: 4536
		private readonly int[] UpdateBuffer;

		// Token: 0x040011B9 RID: 4537
		private readonly int[] UpdateIndices;

		// Token: 0x040011BA RID: 4538
		private readonly IndexBuffer TraverseStack;

		// Token: 0x040011BB RID: 4539
		private readonly IndexBuffer EmptyBuffer;

		// Token: 0x040011BC RID: 4540
		private IndexBuffer BeyondBuffer;

		// Token: 0x040011BD RID: 4541
		private readonly IndexBuffer AffectedFaceBuffer;

		// Token: 0x040011BE RID: 4542
		private readonly SimpleList<DeferredFace> ConeFaceBuffer;

		// Token: 0x040011BF RID: 4543
		private readonly HashSet<int> SingularVertices;

		// Token: 0x040011C0 RID: 4544
		private readonly ConnectorList[] ConnectorTable;

		// Token: 0x040011C1 RID: 4545
		private readonly ObjectManager ObjectManager;

		// Token: 0x040011C2 RID: 4546
		private readonly MathHelper mathHelper;

		// Token: 0x040011C3 RID: 4547
		private readonly List<int>[] boundingBoxPoints;

		// Token: 0x040011C4 RID: 4548
		private int indexOfDimensionWithLeastExtremes;

		// Token: 0x040011C5 RID: 4549
		private readonly double[] minima;

		// Token: 0x040011C6 RID: 4550
		private readonly double[] maxima;
	}
}
