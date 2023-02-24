using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization.Converters
{
	// Token: 0x020001DC RID: 476
	public class GuidConverter : IYamlTypeConverter
	{
		// Token: 0x06000E64 RID: 3684 RVA: 0x0003B847 File Offset: 0x00039A47
		public GuidConverter(bool jsonCompatible)
		{
			this.jsonCompatible = jsonCompatible;
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0003B856 File Offset: 0x00039A56
		public bool Accepts(Type type)
		{
			return type == typeof(Guid);
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x0003B868 File Offset: 0x00039A68
		public object ReadYaml(IParser parser, Type type)
		{
			string value = ((Scalar)parser.Current).Value;
			parser.MoveNext();
			return new Guid(value);
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0003B88C File Offset: 0x00039A8C
		public void WriteYaml(IEmitter emitter, object value, Type type)
		{
			emitter.Emit(new Scalar(null, null, ((Guid)value).ToString("D"), this.jsonCompatible ? ScalarStyle.DoubleQuoted : ScalarStyle.Any, true, false));
		}

		// Token: 0x04000845 RID: 2117
		private readonly bool jsonCompatible;
	}
}
