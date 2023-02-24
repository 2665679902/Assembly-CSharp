using System;
using UnityEngine;

// Token: 0x02000AAB RID: 2731
[RequireComponent(typeof(GraphBase))]
[AddComponentMenu("KMonoBehaviour/scripts/GraphLayer")]
public class GraphLayer : KMonoBehaviour
{
	// Token: 0x17000633 RID: 1587
	// (get) Token: 0x060053B0 RID: 21424 RVA: 0x001E6207 File Offset: 0x001E4407
	public GraphBase graph
	{
		get
		{
			if (this.graph_base == null)
			{
				this.graph_base = base.GetComponent<GraphBase>();
			}
			return this.graph_base;
		}
	}

	// Token: 0x040038D4 RID: 14548
	[MyCmpReq]
	private GraphBase graph_base;
}
