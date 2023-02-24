using System;

namespace NodeEditorFramework
{
	// Token: 0x0200048E RID: 1166
	public struct NodeData
	{
		// Token: 0x06003217 RID: 12823 RVA: 0x0006664C File Offset: 0x0006484C
		public NodeData(string name, Type[] types)
		{
			this.adress = name;
			this.typeOfNodeCanvas = types;
		}

		// Token: 0x04001166 RID: 4454
		public string adress;

		// Token: 0x04001167 RID: 4455
		public Type[] typeOfNodeCanvas;
	}
}
