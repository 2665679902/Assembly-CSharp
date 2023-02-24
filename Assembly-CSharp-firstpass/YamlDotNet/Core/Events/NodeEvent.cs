using System;
using System.Text.RegularExpressions;

namespace YamlDotNet.Core.Events
{
	// Token: 0x0200023A RID: 570
	public abstract class NodeEvent : ParsingEvent
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x00044621 File Offset: 0x00042821
		public string Anchor
		{
			get
			{
				return this.anchor;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x00044629 File Offset: 0x00042829
		public string Tag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06001123 RID: 4387
		public abstract bool IsCanonical { get; }

		// Token: 0x06001124 RID: 4388 RVA: 0x00044634 File Offset: 0x00042834
		protected NodeEvent(string anchor, string tag, Mark start, Mark end)
			: base(start, end)
		{
			if (anchor != null)
			{
				if (anchor.Length == 0)
				{
					throw new ArgumentException("Anchor value must not be empty.", "anchor");
				}
				if (!NodeEvent.anchorValidator.IsMatch(anchor))
				{
					throw new ArgumentException("Anchor value must contain alphanumerical characters only.", "anchor");
				}
			}
			if (tag != null && tag.Length == 0)
			{
				throw new ArgumentException("Tag value must not be empty.", "tag");
			}
			this.anchor = anchor;
			this.tag = tag;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x000446AB File Offset: 0x000428AB
		protected NodeEvent(string anchor, string tag)
			: this(anchor, tag, Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x04000934 RID: 2356
		internal static readonly Regex anchorValidator = new Regex("^[0-9a-zA-Z_\\-]+$", RegexOptions.None);

		// Token: 0x04000935 RID: 2357
		private readonly string anchor;

		// Token: 0x04000936 RID: 2358
		private readonly string tag;
	}
}
