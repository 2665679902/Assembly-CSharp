using System;

namespace YamlDotNet.Core
{
	// Token: 0x02000218 RID: 536
	[Serializable]
	public class Version
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x00043DDB File Offset: 0x00041FDB
		// (set) Token: 0x06001094 RID: 4244 RVA: 0x00043DE3 File Offset: 0x00041FE3
		public int Major { get; private set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x00043DEC File Offset: 0x00041FEC
		// (set) Token: 0x06001096 RID: 4246 RVA: 0x00043DF4 File Offset: 0x00041FF4
		public int Minor { get; private set; }

		// Token: 0x06001097 RID: 4247 RVA: 0x00043DFD File Offset: 0x00041FFD
		public Version(int major, int minor)
		{
			this.Major = major;
			this.Minor = minor;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00043E14 File Offset: 0x00042014
		public override bool Equals(object obj)
		{
			Version version = obj as Version;
			return version != null && this.Major == version.Major && this.Minor == version.Minor;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00043E4C File Offset: 0x0004204C
		public override int GetHashCode()
		{
			return this.Major.GetHashCode() ^ this.Minor.GetHashCode();
		}
	}
}
