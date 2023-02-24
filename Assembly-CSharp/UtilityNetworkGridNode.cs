using System;

// Token: 0x020009C3 RID: 2499
public struct UtilityNetworkGridNode : IEquatable<UtilityNetworkGridNode>
{
	// Token: 0x06004A2F RID: 18991 RVA: 0x0019F317 File Offset: 0x0019D517
	public bool Equals(UtilityNetworkGridNode other)
	{
		return this.connections == other.connections && this.networkIdx == other.networkIdx;
	}

	// Token: 0x06004A30 RID: 18992 RVA: 0x0019F338 File Offset: 0x0019D538
	public override bool Equals(object obj)
	{
		return ((UtilityNetworkGridNode)obj).Equals(this);
	}

	// Token: 0x06004A31 RID: 18993 RVA: 0x0019F359 File Offset: 0x0019D559
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	// Token: 0x06004A32 RID: 18994 RVA: 0x0019F36B File Offset: 0x0019D56B
	public static bool operator ==(UtilityNetworkGridNode x, UtilityNetworkGridNode y)
	{
		return x.Equals(y);
	}

	// Token: 0x06004A33 RID: 18995 RVA: 0x0019F375 File Offset: 0x0019D575
	public static bool operator !=(UtilityNetworkGridNode x, UtilityNetworkGridNode y)
	{
		return !x.Equals(y);
	}

	// Token: 0x040030AF RID: 12463
	public UtilityConnections connections;

	// Token: 0x040030B0 RID: 12464
	public int networkIdx;

	// Token: 0x040030B1 RID: 12465
	public const int InvalidNetworkIdx = -1;
}
