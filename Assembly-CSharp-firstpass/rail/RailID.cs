using System;

namespace rail
{
	// Token: 0x02000312 RID: 786
	public class RailID : RailComparableID
	{
		// Token: 0x06002DEF RID: 11759 RVA: 0x000600E6 File Offset: 0x0005E2E6
		public RailID()
		{
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x000600EE File Offset: 0x0005E2EE
		public RailID(ulong id)
			: base(id)
		{
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x000600F7 File Offset: 0x0005E2F7
		public EnumRailIDDomain GetDomain()
		{
			if ((int)(this.id_ >> 56) == 1)
			{
				return EnumRailIDDomain.kRailIDDomainPublic;
			}
			return EnumRailIDDomain.kRailIDDomainInvalid;
		}
	}
}
