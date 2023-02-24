using System;

namespace NodeEditorFramework
{
	// Token: 0x0200047A RID: 1146
	public class NodeCanvasTypeAttribute : Attribute
	{
		// Token: 0x0600314B RID: 12619 RVA: 0x00062B08 File Offset: 0x00060D08
		public NodeCanvasTypeAttribute(string displayName)
		{
			this.Name = displayName;
		}

		// Token: 0x04001103 RID: 4355
		public string Name;
	}
}
