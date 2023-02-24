using System;
using System.Collections.Generic;
using ProcGen.Map;
using ProcGenGame;
using VoronoiTree;

namespace Klei
{
	// Token: 0x02000D5A RID: 3418
	public class TerrainCellLogged : TerrainCell
	{
		// Token: 0x06006868 RID: 26728 RVA: 0x0028AA5A File Offset: 0x00288C5A
		public TerrainCellLogged()
		{
		}

		// Token: 0x06006869 RID: 26729 RVA: 0x0028AA62 File Offset: 0x00288C62
		public TerrainCellLogged(Cell node, Diagram.Site site, Dictionary<Tag, int> distancesToTags)
			: base(node, site, distancesToTags)
		{
		}

		// Token: 0x0600686A RID: 26730 RVA: 0x0028AA6D File Offset: 0x00288C6D
		public override void LogInfo(string evt, string param, float value)
		{
		}
	}
}
