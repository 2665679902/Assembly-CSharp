using System;

namespace MIConvexHull
{
	// Token: 0x0200049E RID: 1182
	internal sealed class ConnectorList
	{
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060032D2 RID: 13010 RVA: 0x00069AF1 File Offset: 0x00067CF1
		// (set) Token: 0x060032D3 RID: 13011 RVA: 0x00069AF9 File Offset: 0x00067CF9
		public FaceConnector First { get; private set; }

		// Token: 0x060032D4 RID: 13012 RVA: 0x00069B02 File Offset: 0x00067D02
		private void AddFirst(FaceConnector connector)
		{
			this.First.Previous = connector;
			connector.Next = this.First;
			this.First = connector;
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x00069B23 File Offset: 0x00067D23
		public void Add(FaceConnector element)
		{
			if (this.last != null)
			{
				this.last.Next = element;
			}
			element.Previous = this.last;
			this.last = element;
			if (this.First == null)
			{
				this.First = element;
			}
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x00069B5C File Offset: 0x00067D5C
		public void Remove(FaceConnector connector)
		{
			if (connector.Previous != null)
			{
				connector.Previous.Next = connector.Next;
			}
			else if (connector.Previous == null)
			{
				this.First = connector.Next;
			}
			if (connector.Next != null)
			{
				connector.Next.Previous = connector.Previous;
			}
			else if (connector.Next == null)
			{
				this.last = connector.Previous;
			}
			connector.Next = null;
			connector.Previous = null;
		}

		// Token: 0x040011A3 RID: 4515
		private FaceConnector last;
	}
}
