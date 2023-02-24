using System;

namespace NodeEditorFramework
{
	// Token: 0x0200048F RID: 1167
	public class NodeAttribute : Attribute
	{
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06003218 RID: 12824 RVA: 0x0006665C File Offset: 0x0006485C
		// (set) Token: 0x06003219 RID: 12825 RVA: 0x00066664 File Offset: 0x00064864
		public bool hide { get; private set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600321A RID: 12826 RVA: 0x0006666D File Offset: 0x0006486D
		// (set) Token: 0x0600321B RID: 12827 RVA: 0x00066675 File Offset: 0x00064875
		public string contextText { get; private set; }

		// Token: 0x0600321C RID: 12828 RVA: 0x0006667E File Offset: 0x0006487E
		public NodeAttribute(bool HideNode, string ReplacedContextText, Type[] nodeCanvasTypes = null)
		{
			this.hide = HideNode;
			this.contextText = ReplacedContextText;
			Type[] array = nodeCanvasTypes;
			if (nodeCanvasTypes == null)
			{
				(array = new Type[1])[0] = typeof(NodeCanvas);
			}
			this.typeOfNodeCanvas = array;
		}

		// Token: 0x0400116A RID: 4458
		public Type[] typeOfNodeCanvas;
	}
}
