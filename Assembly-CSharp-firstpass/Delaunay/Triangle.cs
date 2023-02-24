using System;
using System.Collections.Generic;
using Delaunay.Utils;

namespace Delaunay
{
	// Token: 0x0200014D RID: 333
	public sealed class Triangle : Delaunay.Utils.IDisposable
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0002BAD0 File Offset: 0x00029CD0
		public List<Site> sites
		{
			get
			{
				return this._sites;
			}
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0002BAD8 File Offset: 0x00029CD8
		public Triangle(Site a, Site b, Site c)
		{
			this._sites = new List<Site> { a, b, c };
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0002BB00 File Offset: 0x00029D00
		public void Dispose()
		{
			this._sites.Clear();
			this._sites = null;
		}

		// Token: 0x0400072C RID: 1836
		private List<Site> _sites;
	}
}
