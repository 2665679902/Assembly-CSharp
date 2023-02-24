using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization.Converters;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization.NodeDeserializers;
using YamlDotNet.Serialization.NodeTypeResolvers;
using YamlDotNet.Serialization.ObjectFactories;
using YamlDotNet.Serialization.TypeInspectors;
using YamlDotNet.Serialization.TypeResolvers;
using YamlDotNet.Serialization.Utilities;
using YamlDotNet.Serialization.ValueDeserializers;

namespace YamlDotNet.Serialization
{
	// Token: 0x02000177 RID: 375
	public sealed class Deserializer
	{
		// Token: 0x06000C92 RID: 3218 RVA: 0x00036C23 File Offset: 0x00034E23
		private void ThrowUnlessInBackwardsCompatibleMode()
		{
			if (this.backwardsCompatibleConfiguration == null)
			{
				throw new InvalidOperationException("This method / property exists for backwards compatibility reasons, but the Deserializer was created using the new configuration mechanism. To configure the Deserializer, use the DeserializerBuilder.");
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x00036C38 File Offset: 0x00034E38
		[Obsolete("Please use DeserializerBuilder to customize the Deserializer. This property will be removed in future releases.")]
		public IList<INodeDeserializer> NodeDeserializers
		{
			get
			{
				this.ThrowUnlessInBackwardsCompatibleMode();
				return this.backwardsCompatibleConfiguration.NodeDeserializers;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x00036C4B File Offset: 0x00034E4B
		[Obsolete("Please use DeserializerBuilder to customize the Deserializer. This property will be removed in future releases.")]
		public IList<INodeTypeResolver> TypeResolvers
		{
			get
			{
				this.ThrowUnlessInBackwardsCompatibleMode();
				return this.backwardsCompatibleConfiguration.TypeResolvers;
			}
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00036C5E File Offset: 0x00034E5E
		[Obsolete("Please use DeserializerBuilder to customize the Deserializer. This constructor will be removed in future releases.")]
		public Deserializer(IObjectFactory objectFactory = null, INamingConvention namingConvention = null, bool ignoreUnmatched = false, YamlAttributeOverrides overrides = null)
		{
			this.backwardsCompatibleConfiguration = new Deserializer.BackwardsCompatibleConfiguration(objectFactory, namingConvention, ignoreUnmatched, overrides);
			this.valueDeserializer = this.backwardsCompatibleConfiguration.valueDeserializer;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00036C87 File Offset: 0x00034E87
		[Obsolete("Please use DeserializerBuilder to customize the Deserializer. This method will be removed in future releases.")]
		public void RegisterTagMapping(string tag, Type type)
		{
			this.ThrowUnlessInBackwardsCompatibleMode();
			this.backwardsCompatibleConfiguration.RegisterTagMapping(tag, type);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00036C9C File Offset: 0x00034E9C
		[Obsolete("Please use DeserializerBuilder to customize the Deserializer. This method will be removed in future releases.")]
		public void RegisterTypeConverter(IYamlTypeConverter typeConverter)
		{
			this.ThrowUnlessInBackwardsCompatibleMode();
			this.backwardsCompatibleConfiguration.RegisterTypeConverter(typeConverter);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00036CB0 File Offset: 0x00034EB0
		public Deserializer()
		{
			this.backwardsCompatibleConfiguration = new Deserializer.BackwardsCompatibleConfiguration(null, null, false, null);
			this.valueDeserializer = this.backwardsCompatibleConfiguration.valueDeserializer;
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00036CD8 File Offset: 0x00034ED8
		private Deserializer(IValueDeserializer valueDeserializer)
		{
			if (valueDeserializer == null)
			{
				throw new ArgumentNullException("valueDeserializer");
			}
			this.valueDeserializer = valueDeserializer;
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00036CF5 File Offset: 0x00034EF5
		public static Deserializer FromValueDeserializer(IValueDeserializer valueDeserializer)
		{
			return new Deserializer(valueDeserializer);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00036D00 File Offset: 0x00034F00
		public T Deserialize<T>(string input)
		{
			T t;
			using (StringReader stringReader = new StringReader(input))
			{
				t = (T)((object)this.Deserialize(stringReader, typeof(T)));
			}
			return t;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00036D48 File Offset: 0x00034F48
		public T Deserialize<T>(TextReader input)
		{
			return (T)((object)this.Deserialize(input, typeof(T)));
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00036D60 File Offset: 0x00034F60
		public object Deserialize(TextReader input)
		{
			return this.Deserialize(input, typeof(object));
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00036D74 File Offset: 0x00034F74
		public object Deserialize(string input, Type type)
		{
			object obj;
			using (StringReader stringReader = new StringReader(input))
			{
				obj = this.Deserialize(stringReader, type);
			}
			return obj;
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00036DB0 File Offset: 0x00034FB0
		public object Deserialize(TextReader input, Type type)
		{
			return this.Deserialize(new Parser(input), type);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00036DBF File Offset: 0x00034FBF
		public T Deserialize<T>(IParser parser)
		{
			return (T)((object)this.Deserialize(parser, typeof(T)));
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00036DD7 File Offset: 0x00034FD7
		public object Deserialize(IParser parser)
		{
			return this.Deserialize(parser, typeof(object));
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00036DEC File Offset: 0x00034FEC
		public object Deserialize(IParser parser, Type type)
		{
			if (parser == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			bool flag = parser.Allow<StreamStart>() != null;
			bool flag2 = parser.Allow<DocumentStart>() != null;
			object obj = null;
			if (!parser.Accept<DocumentEnd>() && !parser.Accept<StreamEnd>())
			{
				using (SerializerState serializerState = new SerializerState())
				{
					obj = this.valueDeserializer.DeserializeValue(parser, type, serializerState, this.valueDeserializer);
					serializerState.OnDeserialization();
				}
			}
			if (flag2)
			{
				parser.Expect<DocumentEnd>();
			}
			if (flag)
			{
				parser.Expect<StreamEnd>();
			}
			return obj;
		}

		// Token: 0x040007DA RID: 2010
		private readonly Deserializer.BackwardsCompatibleConfiguration backwardsCompatibleConfiguration;

		// Token: 0x040007DB RID: 2011
		private readonly IValueDeserializer valueDeserializer;

		// Token: 0x02000A20 RID: 2592
		private class BackwardsCompatibleConfiguration
		{
			// Token: 0x17000E51 RID: 3665
			// (get) Token: 0x0600546E RID: 21614 RVA: 0x0009D1F4 File Offset: 0x0009B3F4
			// (set) Token: 0x0600546F RID: 21615 RVA: 0x0009D1FC File Offset: 0x0009B3FC
			public IList<INodeDeserializer> NodeDeserializers { get; private set; }

			// Token: 0x17000E52 RID: 3666
			// (get) Token: 0x06005470 RID: 21616 RVA: 0x0009D205 File Offset: 0x0009B405
			// (set) Token: 0x06005471 RID: 21617 RVA: 0x0009D20D File Offset: 0x0009B40D
			public IList<INodeTypeResolver> TypeResolvers { get; private set; }

			// Token: 0x06005472 RID: 21618 RVA: 0x0009D218 File Offset: 0x0009B418
			public BackwardsCompatibleConfiguration(IObjectFactory objectFactory, INamingConvention namingConvention, bool ignoreUnmatched, YamlAttributeOverrides overrides)
			{
				objectFactory = objectFactory ?? new DefaultObjectFactory();
				namingConvention = namingConvention ?? new NullNamingConvention();
				this.typeDescriptor.TypeDescriptor = new CachedTypeInspector(new NamingConventionTypeInspector(new YamlAttributesTypeInspector(new YamlAttributeOverridesInspector(new ReadableAndWritablePropertiesTypeInspector(new ReadablePropertiesTypeInspector(new StaticTypeResolver())), overrides)), namingConvention));
				this.converters = new List<IYamlTypeConverter>();
				this.converters.Add(new GuidConverter(false));
				this.NodeDeserializers = new List<INodeDeserializer>();
				this.NodeDeserializers.Add(new YamlConvertibleNodeDeserializer(objectFactory));
				this.NodeDeserializers.Add(new YamlSerializableNodeDeserializer(objectFactory));
				this.NodeDeserializers.Add(new TypeConverterNodeDeserializer(this.converters));
				this.NodeDeserializers.Add(new NullNodeDeserializer());
				this.NodeDeserializers.Add(new ScalarNodeDeserializer());
				this.NodeDeserializers.Add(new ArrayNodeDeserializer());
				this.NodeDeserializers.Add(new DictionaryNodeDeserializer(objectFactory));
				this.NodeDeserializers.Add(new CollectionNodeDeserializer(objectFactory));
				this.NodeDeserializers.Add(new EnumerableNodeDeserializer());
				this.NodeDeserializers.Add(new ObjectNodeDeserializer(objectFactory, this.typeDescriptor, ignoreUnmatched, null));
				this.tagMappings = new Dictionary<string, Type>(Deserializer.BackwardsCompatibleConfiguration.predefinedTagMappings);
				this.TypeResolvers = new List<INodeTypeResolver>();
				this.TypeResolvers.Add(new YamlConvertibleTypeResolver());
				this.TypeResolvers.Add(new YamlSerializableTypeResolver());
				this.TypeResolvers.Add(new TagNodeTypeResolver(this.tagMappings));
				this.TypeResolvers.Add(new TypeNameInTagNodeTypeResolver());
				this.TypeResolvers.Add(new DefaultContainersNodeTypeResolver());
				this.valueDeserializer = new AliasValueDeserializer(new NodeValueDeserializer(this.NodeDeserializers, this.TypeResolvers));
			}

			// Token: 0x06005473 RID: 21619 RVA: 0x0009D3E6 File Offset: 0x0009B5E6
			public void RegisterTagMapping(string tag, Type type)
			{
				this.tagMappings.Add(tag, type);
			}

			// Token: 0x06005474 RID: 21620 RVA: 0x0009D3F5 File Offset: 0x0009B5F5
			public void RegisterTypeConverter(IYamlTypeConverter typeConverter)
			{
				this.converters.Insert(0, typeConverter);
			}

			// Token: 0x040022A4 RID: 8868
			private static readonly Dictionary<string, Type> predefinedTagMappings = new Dictionary<string, Type>
			{
				{
					"tag:yaml.org,2002:map",
					typeof(Dictionary<object, object>)
				},
				{
					"tag:yaml.org,2002:bool",
					typeof(bool)
				},
				{
					"tag:yaml.org,2002:float",
					typeof(double)
				},
				{
					"tag:yaml.org,2002:int",
					typeof(int)
				},
				{
					"tag:yaml.org,2002:str",
					typeof(string)
				},
				{
					"tag:yaml.org,2002:timestamp",
					typeof(DateTime)
				}
			};

			// Token: 0x040022A5 RID: 8869
			private readonly Dictionary<string, Type> tagMappings;

			// Token: 0x040022A6 RID: 8870
			private readonly List<IYamlTypeConverter> converters;

			// Token: 0x040022A7 RID: 8871
			private Deserializer.BackwardsCompatibleConfiguration.TypeDescriptorProxy typeDescriptor = new Deserializer.BackwardsCompatibleConfiguration.TypeDescriptorProxy();

			// Token: 0x040022A8 RID: 8872
			public IValueDeserializer valueDeserializer;

			// Token: 0x02000B49 RID: 2889
			private class TypeDescriptorProxy : ITypeInspector
			{
				// Token: 0x060058B5 RID: 22709 RVA: 0x000A4E3B File Offset: 0x000A303B
				public IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container)
				{
					return this.TypeDescriptor.GetProperties(type, container);
				}

				// Token: 0x060058B6 RID: 22710 RVA: 0x000A4E4A File Offset: 0x000A304A
				public IPropertyDescriptor GetProperty(Type type, object container, string name, bool ignoreUnmatched)
				{
					return this.TypeDescriptor.GetProperty(type, container, name, ignoreUnmatched);
				}

				// Token: 0x0400269F RID: 9887
				public ITypeInspector TypeDescriptor;
			}
		}
	}
}
