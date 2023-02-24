using System;
using System.Globalization;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization.EventEmitters
{
	// Token: 0x020001D8 RID: 472
	public sealed class JsonEventEmitter : ChainedEventEmitter
	{
		// Token: 0x06000E4E RID: 3662 RVA: 0x0003B21D File Offset: 0x0003941D
		public JsonEventEmitter(IEventEmitter nextEmitter)
			: base(nextEmitter)
		{
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0003B226 File Offset: 0x00039426
		public override void Emit(AliasEventInfo eventInfo, IEmitter emitter)
		{
			throw new NotSupportedException("Aliases are not supported in JSON");
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0003B234 File Offset: 0x00039434
		public override void Emit(ScalarEventInfo eventInfo, IEmitter emitter)
		{
			eventInfo.IsPlainImplicit = true;
			eventInfo.Style = ScalarStyle.Plain;
			TypeCode typeCode = ((eventInfo.Source.Value != null) ? eventInfo.Source.Type.GetTypeCode() : TypeCode.Empty);
			switch (typeCode)
			{
			case TypeCode.Empty:
				eventInfo.RenderedValue = "null";
				goto IL_14C;
			case TypeCode.Boolean:
				eventInfo.RenderedValue = YamlFormatter.FormatBoolean(eventInfo.Source.Value);
				goto IL_14C;
			case TypeCode.Char:
			case TypeCode.String:
				eventInfo.RenderedValue = eventInfo.Source.Value.ToString();
				eventInfo.Style = ScalarStyle.DoubleQuoted;
				goto IL_14C;
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
			case TypeCode.Single:
			case TypeCode.Double:
			case TypeCode.Decimal:
				eventInfo.RenderedValue = YamlFormatter.FormatNumber(eventInfo.Source.Value);
				goto IL_14C;
			case TypeCode.DateTime:
				eventInfo.RenderedValue = YamlFormatter.FormatDateTime(eventInfo.Source.Value);
				goto IL_14C;
			}
			if (!(eventInfo.Source.Type == typeof(TimeSpan)))
			{
				throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "TypeCode.{0} is not supported.", typeCode));
			}
			eventInfo.RenderedValue = YamlFormatter.FormatTimeSpan(eventInfo.Source.Value);
			IL_14C:
			base.Emit(eventInfo, emitter);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0003B395 File Offset: 0x00039595
		public override void Emit(MappingStartEventInfo eventInfo, IEmitter emitter)
		{
			eventInfo.Style = MappingStyle.Flow;
			base.Emit(eventInfo, emitter);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0003B3A6 File Offset: 0x000395A6
		public override void Emit(SequenceStartEventInfo eventInfo, IEmitter emitter)
		{
			eventInfo.Style = SequenceStyle.Flow;
			base.Emit(eventInfo, emitter);
		}
	}
}
