using System;
using System.Globalization;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization.EventEmitters
{
	// Token: 0x020001D9 RID: 473
	public sealed class TypeAssigningEventEmitter : ChainedEventEmitter
	{
		// Token: 0x06000E53 RID: 3667 RVA: 0x0003B3B7 File Offset: 0x000395B7
		public TypeAssigningEventEmitter(IEventEmitter nextEmitter, bool assignTypeWhenDifferent)
			: base(nextEmitter)
		{
			this._assignTypeWhenDifferent = assignTypeWhenDifferent;
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0003B3C8 File Offset: 0x000395C8
		public override void Emit(ScalarEventInfo eventInfo, IEmitter emitter)
		{
			ScalarStyle scalarStyle = ScalarStyle.Plain;
			TypeCode typeCode = ((eventInfo.Source.Value != null) ? eventInfo.Source.Type.GetTypeCode() : TypeCode.Empty);
			switch (typeCode)
			{
			case TypeCode.Empty:
				eventInfo.Tag = "tag:yaml.org,2002:null";
				eventInfo.RenderedValue = "";
				goto IL_1F4;
			case TypeCode.Boolean:
				eventInfo.Tag = "tag:yaml.org,2002:bool";
				eventInfo.RenderedValue = YamlFormatter.FormatBoolean(eventInfo.Source.Value);
				goto IL_1F4;
			case TypeCode.Char:
			case TypeCode.String:
				eventInfo.Tag = "tag:yaml.org,2002:str";
				eventInfo.RenderedValue = eventInfo.Source.Value.ToString();
				scalarStyle = ScalarStyle.Any;
				goto IL_1F4;
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
				eventInfo.Tag = "tag:yaml.org,2002:int";
				eventInfo.RenderedValue = YamlFormatter.FormatNumber(eventInfo.Source.Value);
				goto IL_1F4;
			case TypeCode.Single:
				eventInfo.Tag = "tag:yaml.org,2002:float";
				eventInfo.RenderedValue = YamlFormatter.FormatNumber((float)eventInfo.Source.Value);
				goto IL_1F4;
			case TypeCode.Double:
				eventInfo.Tag = "tag:yaml.org,2002:float";
				eventInfo.RenderedValue = YamlFormatter.FormatNumber((double)eventInfo.Source.Value);
				goto IL_1F4;
			case TypeCode.Decimal:
				eventInfo.Tag = "tag:yaml.org,2002:float";
				eventInfo.RenderedValue = YamlFormatter.FormatNumber(eventInfo.Source.Value);
				goto IL_1F4;
			case TypeCode.DateTime:
				eventInfo.Tag = "tag:yaml.org,2002:timestamp";
				eventInfo.RenderedValue = YamlFormatter.FormatDateTime(eventInfo.Source.Value);
				goto IL_1F4;
			}
			if (!(eventInfo.Source.Type == typeof(TimeSpan)))
			{
				throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "TypeCode.{0} is not supported.", typeCode));
			}
			eventInfo.RenderedValue = YamlFormatter.FormatTimeSpan(eventInfo.Source.Value);
			IL_1F4:
			eventInfo.IsPlainImplicit = true;
			if (eventInfo.Style == ScalarStyle.Any)
			{
				eventInfo.Style = scalarStyle;
			}
			base.Emit(eventInfo, emitter);
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0003B5E7 File Offset: 0x000397E7
		public override void Emit(MappingStartEventInfo eventInfo, IEmitter emitter)
		{
			this.AssignTypeIfDifferent(eventInfo);
			base.Emit(eventInfo, emitter);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0003B5F8 File Offset: 0x000397F8
		public override void Emit(SequenceStartEventInfo eventInfo, IEmitter emitter)
		{
			this.AssignTypeIfDifferent(eventInfo);
			base.Emit(eventInfo, emitter);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0003B60C File Offset: 0x0003980C
		private void AssignTypeIfDifferent(ObjectEventInfo eventInfo)
		{
			if (this._assignTypeWhenDifferent && eventInfo.Source.Value != null && eventInfo.Source.Type != eventInfo.Source.StaticType)
			{
				eventInfo.Tag = "!" + eventInfo.Source.Type.AssemblyQualifiedName;
			}
		}

		// Token: 0x04000841 RID: 2113
		private readonly bool _assignTypeWhenDifferent;
	}
}
