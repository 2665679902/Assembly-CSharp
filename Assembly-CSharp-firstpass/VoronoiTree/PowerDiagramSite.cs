using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Delaunay.Geo;
using KSerialization;
using MIConvexHull;
using UnityEngine;

namespace VoronoiTree
{
	// Token: 0x020004B1 RID: 1201
	[SerializationConfig(MemberSerialization.OptIn)]
	public class PowerDiagramSite : TriangulationCell<PowerDiagram.DualSite2d, PowerDiagramSite>
	{
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06003341 RID: 13121 RVA: 0x0006CB88 File Offset: 0x0006AD88
		// (set) Token: 0x06003342 RID: 13122 RVA: 0x0006CB90 File Offset: 0x0006AD90
		public bool dummy { get; set; }

		// Token: 0x06003343 RID: 13123 RVA: 0x0006CB99 File Offset: 0x0006AD99
		public PowerDiagramSite()
		{
			this.dummy = true;
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x0006CBAF File Offset: 0x0006ADAF
		public PowerDiagramSite(Vector2 pos)
		{
			this.position = pos;
			this.weight = Mathf.Epsilon;
			this.dummy = false;
			this.previousWeightAdaption = 1f;
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x0006CBE2 File Offset: 0x0006ADE2
		public PowerDiagramSite(float x, float y)
			: this(new Vector2(x, y))
		{
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x0006CBF4 File Offset: 0x0006ADF4
		public PowerDiagramSite(uint siteId, Vector2 pos, float siteWeight = 1f)
		{
			this.dummy = false;
			this.neighbours = new List<PowerDiagramSite>();
			this.id = (int)siteId;
			this.position = pos;
			this.weight = siteWeight;
			this.currentWeight = this.weight;
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x0006CC41 File Offset: 0x0006AE41
		[OnDeserializing]
		internal void OnDeserializingMethod()
		{
			this.neighbours = new List<PowerDiagramSite>();
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x0006CC50 File Offset: 0x0006AE50
		public PowerDiagram.DualSite3d ToDualSite()
		{
			return new PowerDiagram.DualSite3d((double)this.position.x, (double)this.position.y, (double)(this.position.x * this.position.x + this.position.y * this.position.y - this.currentWeight), this);
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x0006CCB4 File Offset: 0x0006AEB4
		private double Det(double[,] m)
		{
			return m[0, 0] * (m[1, 1] * m[2, 2] - m[2, 1] * m[1, 2]) - m[0, 1] * (m[1, 0] * m[2, 2] - m[2, 0] * m[1, 2]) + m[0, 2] * (m[1, 0] * m[2, 1] - m[2, 0] * m[1, 1]);
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x0006CD48 File Offset: 0x0006AF48
		private double LengthSquared(double[] v)
		{
			double num = 0.0;
			foreach (double num2 in v)
			{
				num += num2 * num2;
			}
			return num;
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x0006CD78 File Offset: 0x0006AF78
		private Vector2 GetCircumcenter()
		{
			PowerDiagram.DualSite2d[] vertices = base.Vertices;
			double[,] array = new double[3, 3];
			for (int i = 0; i < 3; i++)
			{
				array[i, 0] = vertices[i].Position[0];
				array[i, 1] = vertices[i].Position[1];
				array[i, 2] = 1.0;
			}
			double num = this.Det(array);
			double num2 = -1.0 / (2.0 * num);
			for (int j = 0; j < 3; j++)
			{
				array[j, 0] = this.LengthSquared(vertices[j].Position);
			}
			double num3 = -this.Det(array);
			for (int k = 0; k < 3; k++)
			{
				array[k, 1] = vertices[k].Position[0];
			}
			double num4 = this.Det(array);
			return new Vector2((float)(num2 * num3), (float)(num2 * num4));
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x0006CE6C File Offset: 0x0006B06C
		private Vector2 GetCentroid()
		{
			return new Vector2((float)base.Vertices.Select((PowerDiagram.DualSite2d v) => v.Position[0]).Average(), (float)base.Vertices.Select((PowerDiagram.DualSite2d v) => v.Position[1]).Average());
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x0600334D RID: 13133 RVA: 0x0006CEE0 File Offset: 0x0006B0E0
		public Vector2 Circumcenter
		{
			get
			{
				this.circumCenter = new Vector2?(this.circumCenter ?? this.GetCircumcenter());
				return this.circumCenter.Value;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x0600334E RID: 13134 RVA: 0x0006CF24 File Offset: 0x0006B124
		public Vector2 Centroid
		{
			get
			{
				if (this.poly != null)
				{
					return this.poly.Centroid();
				}
				this.centroid = new Vector2?(this.centroid ?? this.GetCentroid());
				return this.centroid.Value;
			}
		}

		// Token: 0x040011F5 RID: 4597
		public float weight;

		// Token: 0x040011F6 RID: 4598
		public float currentWeight;

		// Token: 0x040011F7 RID: 4599
		public float previousWeightAdaption;

		// Token: 0x040011F9 RID: 4601
		[Serialize]
		public int id = -1;

		// Token: 0x040011FA RID: 4602
		[Serialize]
		public Vector2 position;

		// Token: 0x040011FB RID: 4603
		[Serialize]
		public Polygon poly;

		// Token: 0x040011FC RID: 4604
		[Serialize]
		public List<PowerDiagramSite> neighbours;

		// Token: 0x040011FD RID: 4605
		private Vector2? circumCenter;

		// Token: 0x040011FE RID: 4606
		private Vector2? centroid;
	}
}
