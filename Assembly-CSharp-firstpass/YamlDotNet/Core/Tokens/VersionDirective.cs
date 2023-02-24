using System;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x02000230 RID: 560
	[Serializable]
	public class VersionDirective : Token
	{
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x000442F7 File Offset: 0x000424F7
		public Version Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x000442FF File Offset: 0x000424FF
		public VersionDirective(Version version)
			: this(version, Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00044312 File Offset: 0x00042512
		public VersionDirective(Version version, Mark start, Mark end)
			: base(start, end)
		{
			this.version = version;
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00044324 File Offset: 0x00042524
		public override bool Equals(object obj)
		{
			VersionDirective versionDirective = obj as VersionDirective;
			return versionDirective != null && this.version.Equals(versionDirective.version);
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0004434E File Offset: 0x0004254E
		public override int GetHashCode()
		{
			return this.version.GetHashCode();
		}

		// Token: 0x04000919 RID: 2329
		private readonly Version version;
	}
}
