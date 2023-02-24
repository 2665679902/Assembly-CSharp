using System;
using System.Globalization;
using System.Linq;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization.Converters
{
	// Token: 0x020001DB RID: 475
	public class DateTimeConverter : IYamlTypeConverter
	{
		// Token: 0x06000E5F RID: 3679 RVA: 0x0003B71B File Offset: 0x0003991B
		public DateTimeConverter(DateTimeKind kind = DateTimeKind.Utc, IFormatProvider provider = null, params string[] formats)
		{
			this.kind = ((kind == DateTimeKind.Unspecified) ? DateTimeKind.Utc : kind);
			this.provider = provider ?? CultureInfo.InvariantCulture;
			this.formats = formats.DefaultIfEmpty("G").ToArray<string>();
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0003B756 File Offset: 0x00039956
		public bool Accepts(Type type)
		{
			return type == typeof(DateTime);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0003B768 File Offset: 0x00039968
		public object ReadYaml(IParser parser, Type type)
		{
			string value = ((Scalar)parser.Current).Value;
			DateTimeStyles dateTimeStyles = ((this.kind == DateTimeKind.Local) ? DateTimeStyles.AssumeLocal : DateTimeStyles.AssumeUniversal);
			DateTime dateTime = DateTimeConverter.EnsureDateTimeKind(DateTime.ParseExact(value, this.formats, this.provider, dateTimeStyles), this.kind);
			parser.MoveNext();
			return dateTime;
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0003B7C0 File Offset: 0x000399C0
		public void WriteYaml(IEmitter emitter, object value, Type type)
		{
			DateTime dateTime = (DateTime)value;
			string text = ((this.kind == DateTimeKind.Local) ? dateTime.ToLocalTime() : dateTime.ToUniversalTime()).ToString(this.formats.First<string>(), this.provider);
			emitter.Emit(new Scalar(null, null, text, ScalarStyle.Any, true, false));
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0003B818 File Offset: 0x00039A18
		private static DateTime EnsureDateTimeKind(DateTime dt, DateTimeKind kind)
		{
			if (dt.Kind == DateTimeKind.Local && kind == DateTimeKind.Utc)
			{
				return dt.ToUniversalTime();
			}
			if (dt.Kind == DateTimeKind.Utc && kind == DateTimeKind.Local)
			{
				return dt.ToLocalTime();
			}
			return dt;
		}

		// Token: 0x04000842 RID: 2114
		private readonly DateTimeKind kind;

		// Token: 0x04000843 RID: 2115
		private readonly IFormatProvider provider;

		// Token: 0x04000844 RID: 2116
		private readonly string[] formats;
	}
}
