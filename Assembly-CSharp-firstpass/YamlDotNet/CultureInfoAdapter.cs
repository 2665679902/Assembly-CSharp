using System;
using System.Globalization;

namespace YamlDotNet
{
	// Token: 0x02000172 RID: 370
	internal sealed class CultureInfoAdapter : CultureInfo
	{
		// Token: 0x06000C75 RID: 3189 RVA: 0x0003688F File Offset: 0x00034A8F
		public CultureInfoAdapter(CultureInfo baseCulture, IFormatProvider provider)
			: base(baseCulture.LCID)
		{
			this._provider = provider;
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x000368A4 File Offset: 0x00034AA4
		public override object GetFormat(Type formatType)
		{
			return this._provider.GetFormat(formatType);
		}

		// Token: 0x040007D4 RID: 2004
		private readonly IFormatProvider _provider;
	}
}
