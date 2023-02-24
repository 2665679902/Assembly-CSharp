using System;
using System.Globalization;

namespace YamlDotNet.Core.Events
{
	// Token: 0x0200023C RID: 572
	public class Scalar : NodeEvent
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x000446FA File Offset: 0x000428FA
		internal override EventType Type
		{
			get
			{
				return EventType.Scalar;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x000446FD File Offset: 0x000428FD
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x00044705 File Offset: 0x00042905
		public ScalarStyle Style
		{
			get
			{
				return this.style;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x0004470D File Offset: 0x0004290D
		public bool IsPlainImplicit
		{
			get
			{
				return this.isPlainImplicit;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x00044715 File Offset: 0x00042915
		public bool IsQuotedImplicit
		{
			get
			{
				return this.isQuotedImplicit;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x0004471D File Offset: 0x0004291D
		public override bool IsCanonical
		{
			get
			{
				return !this.isPlainImplicit && !this.isQuotedImplicit;
			}
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00044732 File Offset: 0x00042932
		public Scalar(string anchor, string tag, string value, ScalarStyle style, bool isPlainImplicit, bool isQuotedImplicit, Mark start, Mark end)
			: base(anchor, tag, start, end)
		{
			this.value = value;
			this.style = style;
			this.isPlainImplicit = isPlainImplicit;
			this.isQuotedImplicit = isQuotedImplicit;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00044760 File Offset: 0x00042960
		public Scalar(string anchor, string tag, string value, ScalarStyle style, bool isPlainImplicit, bool isQuotedImplicit)
			: this(anchor, tag, value, style, isPlainImplicit, isQuotedImplicit, Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00044788 File Offset: 0x00042988
		public Scalar(string value)
			: this(null, null, value, ScalarStyle.Any, true, true, Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x000447AC File Offset: 0x000429AC
		public Scalar(string tag, string value)
			: this(null, tag, value, ScalarStyle.Any, true, true, Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x000447D0 File Offset: 0x000429D0
		public Scalar(string anchor, string tag, string value)
			: this(anchor, tag, value, ScalarStyle.Any, true, true, Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x000447F4 File Offset: 0x000429F4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Scalar [anchor = {0}, tag = {1}, value = {2}, style = {3}, isPlainImplicit = {4}, isQuotedImplicit = {5}]", new object[] { base.Anchor, base.Tag, this.value, this.style, this.isPlainImplicit, this.isQuotedImplicit });
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0004485B File Offset: 0x00042A5B
		public override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x04000939 RID: 2361
		private readonly string value;

		// Token: 0x0400093A RID: 2362
		private readonly ScalarStyle style;

		// Token: 0x0400093B RID: 2363
		private readonly bool isPlainImplicit;

		// Token: 0x0400093C RID: 2364
		private readonly bool isQuotedImplicit;
	}
}
