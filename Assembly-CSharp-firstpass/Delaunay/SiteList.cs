using System;
using System.Collections.Generic;
using Delaunay.Geo;
using Delaunay.Utils;
using UnityEngine;

namespace Delaunay
{
	// Token: 0x0200014C RID: 332
	public sealed class SiteList : Delaunay.Utils.IDisposable
	{
		// Token: 0x06000B23 RID: 2851 RVA: 0x0002B71E File Offset: 0x0002991E
		public SiteList()
		{
			this._sites = new List<Site>();
			this._sorted = false;
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0002B738 File Offset: 0x00029938
		public void Dispose()
		{
			if (this._sites != null)
			{
				for (int i = 0; i < this._sites.Count; i++)
				{
					this._sites[i].Dispose();
				}
				this._sites.Clear();
				this._sites = null;
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0002B786 File Offset: 0x00029986
		public int Add(Site site)
		{
			this._sorted = false;
			this._sites.Add(site);
			return this._sites.Count;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0002B7A6 File Offset: 0x000299A6
		public int Count
		{
			get
			{
				return this._sites.Count;
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0002B7B4 File Offset: 0x000299B4
		public Site Next()
		{
			if (!this._sorted)
			{
				UnityEngine.Debug.LogError("SiteList::next():  sites have not been sorted");
			}
			if (this._currentIndex < this._sites.Count)
			{
				List<Site> sites = this._sites;
				int currentIndex = this._currentIndex;
				this._currentIndex = currentIndex + 1;
				return sites[currentIndex];
			}
			return null;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0002B804 File Offset: 0x00029A04
		internal Rect GetSitesBounds()
		{
			if (!this._sorted)
			{
				Site.SortSites(this._sites);
				this._currentIndex = 0;
				this._sorted = true;
			}
			if (this._sites.Count == 0)
			{
				return new Rect(0f, 0f, 0f, 0f);
			}
			float num = float.MaxValue;
			float num2 = float.MinValue;
			for (int i = 0; i < this._sites.Count; i++)
			{
				Site site = this._sites[i];
				if (site.x < num)
				{
					num = site.x;
				}
				if (site.x > num2)
				{
					num2 = site.x;
				}
			}
			float y = this._sites[0].y;
			float y2 = this._sites[this._sites.Count - 1].y;
			return new Rect(num, y, num2 - num, y2 - y);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0002B8F0 File Offset: 0x00029AF0
		public List<uint> SiteColors()
		{
			List<uint> list = new List<uint>();
			for (int i = 0; i < this._sites.Count; i++)
			{
				list.Add(this._sites[i].color);
			}
			return list;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0002B934 File Offset: 0x00029B34
		public List<Vector2> SiteCoords()
		{
			List<Vector2> list = new List<Vector2>();
			for (int i = 0; i < this._sites.Count; i++)
			{
				list.Add(this._sites[i].Coord);
			}
			return list;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0002B978 File Offset: 0x00029B78
		public void ScaleWeight(float scale)
		{
			for (int i = 0; i < this._sites.Count; i++)
			{
				this._sites[i].scaled_weight = this._sites[i].weight * scale;
			}
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0002B9C0 File Offset: 0x00029BC0
		public List<Circle> Circles()
		{
			List<Circle> list = new List<Circle>();
			for (int i = 0; i < this._sites.Count; i++)
			{
				float num = 0f;
				Edge edge = this._sites[i].NearestEdge();
				if (!edge.IsPartOfConvexHull())
				{
					num = edge.SitesDistance() * 0.5f;
				}
				list.Add(new Circle(this._sites[i].x, this._sites[i].y, num));
			}
			return list;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002BA48 File Offset: 0x00029C48
		public List<List<Vector2>> Regions(Rect plotBounds)
		{
			List<List<Vector2>> list = new List<List<Vector2>>();
			for (int i = 0; i < this._sites.Count; i++)
			{
				Site site = this._sites[i];
				list.Add(site.Region(plotBounds));
			}
			return list;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0002BA8C File Offset: 0x00029C8C
		public List<List<Vector2>> Regions(Polygon plotBounds)
		{
			List<List<Vector2>> list = new List<List<Vector2>>();
			for (int i = 0; i < this._sites.Count; i++)
			{
				Site site = this._sites[i];
				list.Add(site.Region(plotBounds));
			}
			return list;
		}

		// Token: 0x04000729 RID: 1833
		private List<Site> _sites;

		// Token: 0x0400072A RID: 1834
		private int _currentIndex;

		// Token: 0x0400072B RID: 1835
		private bool _sorted;
	}
}
