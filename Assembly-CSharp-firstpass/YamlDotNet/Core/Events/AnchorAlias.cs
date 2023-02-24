using System;
using System.Globalization;

namespace YamlDotNet.Core.Events
{
	// Token: 0x02000231 RID: 561
	public class AnchorAlias : ParsingEvent
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x0004435B File Offset: 0x0004255B
		internal override EventType Type
		{
			get
			{
				return EventType.Alias;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x0004435E File Offset: 0x0004255E
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00044366 File Offset: 0x00042566
		public AnchorAlias(string value, Mark start, Mark end)
			: base(start, end)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new YamlException(start, end, "Anchor value must not be empty.");
			}
			if (!NodeEvent.anchorValidator.IsMatch(value))
			{
				throw new YamlException(start, end, "Anchor value must contain alphanumerical characters only.");
			}
			this.value = value;
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x000443A6 File Offset: 0x000425A6
		public AnchorAlias(string value)
			: this(value, Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x000443B9 File Offset: 0x000425B9
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Alias [value = {0}]", this.value);
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x000443D0 File Offset: 0x000425D0
		public override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0400091A RID: 2330
		private readonly string value;
	}
}
