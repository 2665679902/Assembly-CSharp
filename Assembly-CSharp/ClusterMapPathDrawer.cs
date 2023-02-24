using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000A54 RID: 2644
public class ClusterMapPathDrawer : MonoBehaviour
{
	// Token: 0x0600504D RID: 20557 RVA: 0x001CBE7C File Offset: 0x001CA07C
	public ClusterMapPath AddPath()
	{
		ClusterMapPath clusterMapPath = UnityEngine.Object.Instantiate<ClusterMapPath>(this.pathPrefab, this.pathContainer);
		clusterMapPath.Init();
		return clusterMapPath;
	}

	// Token: 0x0600504E RID: 20558 RVA: 0x001CBE95 File Offset: 0x001CA095
	public static List<Vector2> GetDrawPathList(Vector2 startLocation, List<AxialI> pathPoints)
	{
		List<Vector2> list = new List<Vector2>();
		list.Add(startLocation);
		list.AddRange(pathPoints.Select((AxialI point) => point.ToWorld2D()));
		return list;
	}

	// Token: 0x040035F4 RID: 13812
	public ClusterMapPath pathPrefab;

	// Token: 0x040035F5 RID: 13813
	public Transform pathContainer;
}
