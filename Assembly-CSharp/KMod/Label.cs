using System;
using System.Diagnostics;
using System.IO;
using Klei;
using Newtonsoft.Json;

namespace KMod
{
	// Token: 0x02000D10 RID: 3344
	[JsonObject(MemberSerialization.Fields)]
	[DebuggerDisplay("{title}")]
	public struct Label
	{
		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x0600676A RID: 26474 RVA: 0x0027E5D4 File Offset: 0x0027C7D4
		[JsonIgnore]
		private string distribution_platform_name
		{
			get
			{
				return this.distribution_platform.ToString();
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x0600676B RID: 26475 RVA: 0x0027E5E7 File Offset: 0x0027C7E7
		[JsonIgnore]
		public string install_path
		{
			get
			{
				return FileSystem.Normalize(Path.Combine(Manager.GetDirectory(), this.distribution_platform_name, this.id));
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x0600676C RID: 26476 RVA: 0x0027E604 File Offset: 0x0027C804
		[JsonIgnore]
		public string defaultStaticID
		{
			get
			{
				return this.id + "." + this.distribution_platform.ToString();
			}
		}

		// Token: 0x0600676D RID: 26477 RVA: 0x0027E627 File Offset: 0x0027C827
		public override string ToString()
		{
			return this.title;
		}

		// Token: 0x0600676E RID: 26478 RVA: 0x0027E62F File Offset: 0x0027C82F
		public bool Match(Label rhs)
		{
			return this.id == rhs.id && this.distribution_platform == rhs.distribution_platform;
		}

		// Token: 0x04004BDF RID: 19423
		public Label.DistributionPlatform distribution_platform;

		// Token: 0x04004BE0 RID: 19424
		public string id;

		// Token: 0x04004BE1 RID: 19425
		public string title;

		// Token: 0x04004BE2 RID: 19426
		public long version;

		// Token: 0x02001B45 RID: 6981
		public enum DistributionPlatform
		{
			// Token: 0x04007B0E RID: 31502
			Local,
			// Token: 0x04007B0F RID: 31503
			Steam,
			// Token: 0x04007B10 RID: 31504
			Epic,
			// Token: 0x04007B11 RID: 31505
			Rail,
			// Token: 0x04007B12 RID: 31506
			Dev
		}
	}
}
