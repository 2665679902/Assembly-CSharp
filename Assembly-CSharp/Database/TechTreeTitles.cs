using System;
using UnityEngine;

namespace Database
{
	// Token: 0x02000CBC RID: 3260
	public class TechTreeTitles : ResourceSet<TechTreeTitle>
	{
		// Token: 0x06006612 RID: 26130 RVA: 0x00272E5C File Offset: 0x0027105C
		public TechTreeTitles(ResourceSet parent)
			: base("TreeTitles", parent)
		{
		}

		// Token: 0x06006613 RID: 26131 RVA: 0x00272E6C File Offset: 0x0027106C
		public void Load(TextAsset tree_file)
		{
			foreach (ResourceTreeNode resourceTreeNode in new ResourceTreeLoader<ResourceTreeNode>(tree_file))
			{
				if (string.Equals(resourceTreeNode.Id.Substring(0, 1), "_"))
				{
					new TechTreeTitle(resourceTreeNode.Id, this, Strings.Get("STRINGS.RESEARCH.TREES.TITLE" + resourceTreeNode.Id.ToUpper()), resourceTreeNode);
				}
			}
		}
	}
}
