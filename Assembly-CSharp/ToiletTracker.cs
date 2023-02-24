using System;

// Token: 0x020004E7 RID: 1255
public class ToiletTracker : WorldTracker
{
	// Token: 0x06001DB3 RID: 7603 RVA: 0x0009E94F File Offset: 0x0009CB4F
	public ToiletTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06001DB4 RID: 7604 RVA: 0x0009E958 File Offset: 0x0009CB58
	public override void UpdateData()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06001DB5 RID: 7605 RVA: 0x0009E95F File Offset: 0x0009CB5F
	public override string FormatValueString(float value)
	{
		return value.ToString();
	}
}
