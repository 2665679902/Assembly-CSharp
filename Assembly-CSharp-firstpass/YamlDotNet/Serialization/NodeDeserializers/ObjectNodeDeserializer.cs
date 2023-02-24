using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization.Utilities;

namespace YamlDotNet.Serialization.NodeDeserializers
{
	// Token: 0x020001CC RID: 460
	public sealed class ObjectNodeDeserializer : INodeDeserializer
	{
		// Token: 0x06000E2D RID: 3629 RVA: 0x0003AA76 File Offset: 0x00038C76
		public ObjectNodeDeserializer(IObjectFactory objectFactory, ITypeInspector typeDescriptor, bool ignoreUnmatched, Action<string> unmatchedLogFn = null)
		{
			this._objectFactory = objectFactory;
			this._typeDescriptor = typeDescriptor;
			this._ignoreUnmatched = ignoreUnmatched;
			this._unmatchedLogFn = unmatchedLogFn;
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0003AA9C File Offset: 0x00038C9C
		bool INodeDeserializer.Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object> nestedObjectDeserializer, out object value)
		{
			if (parser.Allow<MappingStart>() == null)
			{
				value = null;
				return false;
			}
			value = this._objectFactory.Create(expectedType);
			while (!parser.Accept<MappingEnd>())
			{
				Scalar scalar = parser.Expect<Scalar>();
				IPropertyDescriptor property = this._typeDescriptor.GetProperty(expectedType, null, scalar.Value, this._ignoreUnmatched);
				if (property == null)
				{
					if (this._unmatchedLogFn != null)
					{
						this._unmatchedLogFn(string.Format("Found a property '{0}' on a type '{1}', but that type doesn't have that property!", scalar.Value, expectedType.FullName));
					}
					parser.SkipThisAndNestedEvents();
				}
				else
				{
					object obj = nestedObjectDeserializer(parser, property.Type);
					IValuePromise valuePromise = obj as IValuePromise;
					if (valuePromise == null)
					{
						object obj2 = TypeConverter.ChangeType(obj, property.Type);
						property.Write(value, obj2);
					}
					else
					{
						object valueRef = value;
						valuePromise.ValueAvailable += delegate(object v)
						{
							object obj3 = TypeConverter.ChangeType(v, property.Type);
							property.Write(valueRef, obj3);
						};
					}
				}
			}
			parser.Expect<MappingEnd>();
			return true;
		}

		// Token: 0x04000836 RID: 2102
		private readonly IObjectFactory _objectFactory;

		// Token: 0x04000837 RID: 2103
		private readonly ITypeInspector _typeDescriptor;

		// Token: 0x04000838 RID: 2104
		private readonly bool _ignoreUnmatched;

		// Token: 0x04000839 RID: 2105
		private readonly Action<string> _unmatchedLogFn;
	}
}
