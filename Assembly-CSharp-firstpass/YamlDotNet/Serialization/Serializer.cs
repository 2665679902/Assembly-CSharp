using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization.Converters;
using YamlDotNet.Serialization.EventEmitters;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization.ObjectGraphTraversalStrategies;
using YamlDotNet.Serialization.ObjectGraphVisitors;
using YamlDotNet.Serialization.TypeInspectors;
using YamlDotNet.Serialization.TypeResolvers;

namespace YamlDotNet.Serialization
{
	// Token: 0x0200019E RID: 414
	public sealed class Serializer
	{
		// Token: 0x06000D53 RID: 3411 RVA: 0x00037CAB File Offset: 0x00035EAB
		private void ThrowUnlessInBackwardsCompatibleMode()
		{
			if (this.backwardsCompatibleConfiguration == null)
			{
				throw new InvalidOperationException("This method / property exists for backwards compatibility reasons, but the Serializer was created using the new configuration mechanism. To configure the Serializer, use the SerializerBuilder.");
			}
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00037CC0 File Offset: 0x00035EC0
		[Obsolete("Please use SerializerBuilder to customize the Serializer. This constructor will be removed in future releases.")]
		public Serializer(SerializationOptions options = SerializationOptions.None, INamingConvention namingConvention = null, YamlAttributeOverrides overrides = null)
		{
			this.backwardsCompatibleConfiguration = new Serializer.BackwardsCompatibleConfiguration(options, namingConvention, overrides);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00037CD6 File Offset: 0x00035ED6
		[Obsolete("Please use SerializerBuilder to customize the Serializer. This method will be removed in future releases.")]
		public void RegisterTypeConverter(IYamlTypeConverter converter)
		{
			this.ThrowUnlessInBackwardsCompatibleMode();
			this.backwardsCompatibleConfiguration.Converters.Insert(0, converter);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00037CF0 File Offset: 0x00035EF0
		public Serializer()
		{
			this.backwardsCompatibleConfiguration = new Serializer.BackwardsCompatibleConfiguration(SerializationOptions.None, null, null);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00037D06 File Offset: 0x00035F06
		private Serializer(IValueSerializer valueSerializer)
		{
			if (valueSerializer == null)
			{
				throw new ArgumentNullException("valueSerializer");
			}
			this.valueSerializer = valueSerializer;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00037D23 File Offset: 0x00035F23
		public static Serializer FromValueSerializer(IValueSerializer valueSerializer)
		{
			return new Serializer(valueSerializer);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00037D2B File Offset: 0x00035F2B
		public void Serialize(TextWriter writer, object graph)
		{
			this.Serialize(new Emitter(writer), graph);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00037D3C File Offset: 0x00035F3C
		public string Serialize(object graph)
		{
			string text;
			using (StringWriter stringWriter = new StringWriter())
			{
				this.Serialize(stringWriter, graph);
				text = stringWriter.ToString();
			}
			return text;
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00037D7C File Offset: 0x00035F7C
		public void Serialize(TextWriter writer, object graph, Type type)
		{
			this.Serialize(new Emitter(writer), graph, type);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00037D8C File Offset: 0x00035F8C
		public void Serialize(IEmitter emitter, object graph)
		{
			if (emitter == null)
			{
				throw new ArgumentNullException("emitter");
			}
			this.EmitDocument(emitter, graph, null);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00037DA5 File Offset: 0x00035FA5
		public void Serialize(IEmitter emitter, object graph, Type type)
		{
			if (emitter == null)
			{
				throw new ArgumentNullException("emitter");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.EmitDocument(emitter, graph, type);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00037DD4 File Offset: 0x00035FD4
		private void EmitDocument(IEmitter emitter, object graph, Type type)
		{
			emitter.Emit(new StreamStart());
			emitter.Emit(new DocumentStart());
			IValueSerializer valueSerializer = this.backwardsCompatibleConfiguration;
			(valueSerializer ?? this.valueSerializer).SerializeValue(emitter, graph, type);
			emitter.Emit(new DocumentEnd(true));
			emitter.Emit(new StreamEnd());
		}

		// Token: 0x04000802 RID: 2050
		private readonly IValueSerializer valueSerializer;

		// Token: 0x04000803 RID: 2051
		private readonly Serializer.BackwardsCompatibleConfiguration backwardsCompatibleConfiguration;

		// Token: 0x02000A30 RID: 2608
		private class BackwardsCompatibleConfiguration : IValueSerializer
		{
			// Token: 0x17000E55 RID: 3669
			// (get) Token: 0x060054AC RID: 21676 RVA: 0x0009D943 File Offset: 0x0009BB43
			// (set) Token: 0x060054AD RID: 21677 RVA: 0x0009D94B File Offset: 0x0009BB4B
			public IList<IYamlTypeConverter> Converters { get; private set; }

			// Token: 0x060054AE RID: 21678 RVA: 0x0009D954 File Offset: 0x0009BB54
			public BackwardsCompatibleConfiguration(SerializationOptions options, INamingConvention namingConvention, YamlAttributeOverrides overrides)
			{
				this.options = options;
				this.namingConvention = namingConvention ?? new NullNamingConvention();
				this.overrides = overrides;
				this.Converters = new List<IYamlTypeConverter>();
				this.Converters.Add(new GuidConverter(this.IsOptionSet(SerializationOptions.JsonCompatible)));
				ITypeResolver typeResolver2;
				if (!this.IsOptionSet(SerializationOptions.DefaultToStaticType))
				{
					ITypeResolver typeResolver = new DynamicTypeResolver();
					typeResolver2 = typeResolver;
				}
				else
				{
					ITypeResolver typeResolver = new StaticTypeResolver();
					typeResolver2 = typeResolver;
				}
				this.typeResolver = typeResolver2;
			}

			// Token: 0x060054AF RID: 21679 RVA: 0x0009D9C7 File Offset: 0x0009BBC7
			public bool IsOptionSet(SerializationOptions option)
			{
				return (this.options & option) > SerializationOptions.None;
			}

			// Token: 0x060054B0 RID: 21680 RVA: 0x0009D9D4 File Offset: 0x0009BBD4
			private IObjectGraphVisitor<IEmitter> CreateEmittingVisitor(IEmitter emitter, IObjectGraphTraversalStrategy traversalStrategy, IEventEmitter eventEmitter, IObjectDescriptor graph)
			{
				IObjectGraphVisitor<IEmitter> objectGraphVisitor = new EmittingObjectGraphVisitor(eventEmitter);
				ObjectSerializer objectSerializer = delegate(object v, Type t)
				{
					this.SerializeValue(emitter, v, t);
				};
				objectGraphVisitor = new CustomSerializationObjectGraphVisitor(objectGraphVisitor, this.Converters, objectSerializer);
				if (!this.IsOptionSet(SerializationOptions.DisableAliases))
				{
					AnchorAssigner anchorAssigner = new AnchorAssigner(this.Converters);
					traversalStrategy.Traverse<Nothing>(graph, anchorAssigner, null);
					objectGraphVisitor = new AnchorAssigningObjectGraphVisitor(objectGraphVisitor, eventEmitter, anchorAssigner);
				}
				if (!this.IsOptionSet(SerializationOptions.EmitDefaults))
				{
					objectGraphVisitor = new DefaultExclusiveObjectGraphVisitor(objectGraphVisitor);
				}
				return objectGraphVisitor;
			}

			// Token: 0x060054B1 RID: 21681 RVA: 0x0009DA50 File Offset: 0x0009BC50
			private IEventEmitter CreateEventEmitter()
			{
				WriterEventEmitter writerEventEmitter = new WriterEventEmitter();
				if (this.IsOptionSet(SerializationOptions.JsonCompatible))
				{
					return new JsonEventEmitter(writerEventEmitter);
				}
				return new TypeAssigningEventEmitter(writerEventEmitter, this.IsOptionSet(SerializationOptions.Roundtrip));
			}

			// Token: 0x060054B2 RID: 21682 RVA: 0x0009DA80 File Offset: 0x0009BC80
			private IObjectGraphTraversalStrategy CreateTraversalStrategy()
			{
				ITypeInspector typeInspector = new ReadablePropertiesTypeInspector(this.typeResolver);
				if (this.IsOptionSet(SerializationOptions.Roundtrip))
				{
					typeInspector = new ReadableAndWritablePropertiesTypeInspector(typeInspector);
				}
				typeInspector = new YamlAttributeOverridesInspector(typeInspector, this.overrides);
				typeInspector = new YamlAttributesTypeInspector(typeInspector);
				typeInspector = new NamingConventionTypeInspector(typeInspector, this.namingConvention);
				if (this.IsOptionSet(SerializationOptions.DefaultToStaticType))
				{
					typeInspector = new CachedTypeInspector(typeInspector);
				}
				if (this.IsOptionSet(SerializationOptions.Roundtrip))
				{
					return new RoundtripObjectGraphTraversalStrategy(this.Converters, typeInspector, this.typeResolver, 50);
				}
				return new FullObjectGraphTraversalStrategy(typeInspector, this.typeResolver, 50, this.namingConvention);
			}

			// Token: 0x060054B3 RID: 21683 RVA: 0x0009DB10 File Offset: 0x0009BD10
			public void SerializeValue(IEmitter emitter, object value, Type type)
			{
				ObjectDescriptor objectDescriptor = ((type != null) ? new ObjectDescriptor(value, type, type) : new ObjectDescriptor(value, (value != null) ? value.GetType() : typeof(object), typeof(object)));
				IObjectGraphTraversalStrategy objectGraphTraversalStrategy = this.CreateTraversalStrategy();
				IObjectGraphVisitor<IEmitter> objectGraphVisitor = this.CreateEmittingVisitor(emitter, objectGraphTraversalStrategy, this.CreateEventEmitter(), objectDescriptor);
				objectGraphTraversalStrategy.Traverse<IEmitter>(objectDescriptor, objectGraphVisitor, emitter);
			}

			// Token: 0x040022D3 RID: 8915
			private readonly SerializationOptions options;

			// Token: 0x040022D4 RID: 8916
			private readonly INamingConvention namingConvention;

			// Token: 0x040022D5 RID: 8917
			private readonly ITypeResolver typeResolver;

			// Token: 0x040022D6 RID: 8918
			private readonly YamlAttributeOverrides overrides;
		}
	}
}
