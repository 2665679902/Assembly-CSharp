using System;

namespace Satsuma
{
	// Token: 0x0200025C RID: 604
	public struct Node : IEquatable<Node>
	{
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x00047B1D File Offset: 0x00045D1D
		// (set) Token: 0x0600125C RID: 4700 RVA: 0x00047B25 File Offset: 0x00045D25
		public long Id { readonly get; private set; }

		// Token: 0x0600125D RID: 4701 RVA: 0x00047B2E File Offset: 0x00045D2E
		public Node(long id)
		{
			this = default(Node);
			this.Id = id;
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x00047B3E File Offset: 0x00045D3E
		public static Node Invalid
		{
			get
			{
				return new Node(0L);
			}
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00047B47 File Offset: 0x00045D47
		public bool Equals(Node other)
		{
			return this.Id == other.Id;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00047B58 File Offset: 0x00045D58
		public override bool Equals(object obj)
		{
			return obj is Node && this.Equals((Node)obj);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00047B70 File Offset: 0x00045D70
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00047B8C File Offset: 0x00045D8C
		public override string ToString()
		{
			return "#" + this.Id.ToString();
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00047BB1 File Offset: 0x00045DB1
		public static bool operator ==(Node a, Node b)
		{
			return a.Equals(b);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00047BBB File Offset: 0x00045DBB
		public static bool operator !=(Node a, Node b)
		{
			return !(a == b);
		}
	}
}
