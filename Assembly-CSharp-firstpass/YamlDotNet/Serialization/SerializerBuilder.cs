using System;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Serialization.Converters;
using YamlDotNet.Serialization.EventEmitters;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization.ObjectGraphTraversalStrategies;
using YamlDotNet.Serialization.ObjectGraphVisitors;
using YamlDotNet.Serialization.TypeInspectors;
using YamlDotNet.Serialization.TypeResolvers;

namespace YamlDotNet.Serialization
{
	// Token: 0x0200019F RID: 415
	public sealed class SerializerBuilder : BuilderSkeleton<SerializerBuilder>
	{
		// Token: 0x06000D5F RID: 3423 RVA: 0x00037E28 File Offset: 0x00036028
		public SerializerBuilder()
		{
			this.typeInspectorFactories.Add(typeof(CachedTypeInspector), (ITypeInspector inner) => new CachedTypeInspector(inner));
			this.typeInspectorFactories.Add(typeof(NamingConventionTypeInspector), delegate(ITypeInspector inner)
			{
				if (this.namingConvention == null)
				{
					return inner;
				}
				return new NamingConventionTypeInspector(inner, this.namingConvention);
			});
			this.typeInspectorFactories.Add(typeof(YamlAttributesTypeInspector), (ITypeInspector inner) => new YamlAttributesTypeInspector(inner));
			this.typeInspectorFactories.Add(typeof(YamlAttributeOverridesInspector), delegate(ITypeInspector inner)
			{
				if (this.overrides == null)
				{
					return inner;
				}
				return new YamlAttributeOverridesInspector(inner, this.overrides.Clone());
			});
			this.preProcessingPhaseObjectGraphVisitorFactories = new LazyComponentRegistrationList<IEnumerable<IYamlTypeConverter>, IObjectGraphVisitor<Nothing>>();
			this.preProcessingPhaseObjectGraphVisitorFactories.Add(typeof(AnchorAssigner), (IEnumerable<IYamlTypeConverter> typeConverters) => new AnchorAssigner(typeConverters));
			this.emissionPhaseObjectGraphVisitorFactories = new LazyComponentRegistrationList<EmissionPhaseObjectGraphVisitorArgs, IObjectGraphVisitor<IEmitter>>();
			this.emissionPhaseObjectGraphVisitorFactories.Add(typeof(CustomSerializationObjectGraphVisitor), (EmissionPhaseObjectGraphVisitorArgs args) => new CustomSerializationObjectGraphVisitor(args.InnerVisitor, args.TypeConverters, args.NestedObjectSerializer));
			this.emissionPhaseObjectGraphVisitorFactories.Add(typeof(AnchorAssigningObjectGraphVisitor), (EmissionPhaseObjectGraphVisitorArgs args) => new AnchorAssigningObjectGraphVisitor(args.InnerVisitor, args.EventEmitter, args.GetPreProcessingPhaseObjectGraphVisitor<AnchorAssigner>()));
			this.emissionPhaseObjectGraphVisitorFactories.Add(typeof(DefaultExclusiveObjectGraphVisitor), (EmissionPhaseObjectGraphVisitorArgs args) => new DefaultExclusiveObjectGraphVisitor(args.InnerVisitor));
			this.eventEmitterFactories = new LazyComponentRegistrationList<IEventEmitter, IEventEmitter>();
			this.eventEmitterFactories.Add(typeof(TypeAssigningEventEmitter), (IEventEmitter inner) => new TypeAssigningEventEmitter(inner, false));
			this.objectGraphTraversalStrategyFactory = (ITypeInspector typeInspector, ITypeResolver typeResolver, IEnumerable<IYamlTypeConverter> typeConverters) => new FullObjectGraphTraversalStrategy(typeInspector, typeResolver, 50, this.namingConvention ?? new NullNamingConvention());
			base.WithTypeResolver(new DynamicTypeResolver());
			this.WithEventEmitter<CustomTagEventEmitter>((IEventEmitter inner) => new CustomTagEventEmitter(inner, this.tagMappings));
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x00038046 File Offset: 0x00036246
		protected override SerializerBuilder Self
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00038049 File Offset: 0x00036249
		public SerializerBuilder WithEventEmitter<TEventEmitter>(Func<IEventEmitter, TEventEmitter> eventEmitterFactory) where TEventEmitter : IEventEmitter
		{
			return this.WithEventEmitter<TEventEmitter>(eventEmitterFactory, delegate(IRegistrationLocationSelectionSyntax<IEventEmitter> w)
			{
				w.OnTop();
			});
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00038074 File Offset: 0x00036274
		public SerializerBuilder WithEventEmitter<TEventEmitter>(Func<IEventEmitter, TEventEmitter> eventEmitterFactory, Action<IRegistrationLocationSelectionSyntax<IEventEmitter>> where) where TEventEmitter : IEventEmitter
		{
			if (eventEmitterFactory == null)
			{
				throw new ArgumentNullException("eventEmitterFactory");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(this.eventEmitterFactories.CreateRegistrationLocationSelector(typeof(TEventEmitter), (IEventEmitter inner) => eventEmitterFactory(inner)));
			return this.Self;
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x000380DC File Offset: 0x000362DC
		public SerializerBuilder WithEventEmitter<TEventEmitter>(WrapperFactory<IEventEmitter, IEventEmitter, TEventEmitter> eventEmitterFactory, Action<ITrackingRegistrationLocationSelectionSyntax<IEventEmitter>> where) where TEventEmitter : IEventEmitter
		{
			if (eventEmitterFactory == null)
			{
				throw new ArgumentNullException("eventEmitterFactory");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(this.eventEmitterFactories.CreateTrackingRegistrationLocationSelector(typeof(TEventEmitter), (IEventEmitter wrapped, IEventEmitter inner) => eventEmitterFactory(wrapped, inner)));
			return this.Self;
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00038144 File Offset: 0x00036344
		public SerializerBuilder WithoutEventEmitter<TEventEmitter>() where TEventEmitter : IEventEmitter
		{
			return this.WithoutEventEmitter(typeof(TEventEmitter));
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00038156 File Offset: 0x00036356
		public SerializerBuilder WithoutEventEmitter(Type eventEmitterType)
		{
			if (eventEmitterType == null)
			{
				throw new ArgumentNullException("eventEmitterType");
			}
			this.eventEmitterFactories.Remove(eventEmitterType);
			return this;
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0003817C File Offset: 0x0003637C
		public SerializerBuilder WithTagMapping(string tag, Type type)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			string text;
			if (this.tagMappings.TryGetValue(type, out text))
			{
				throw new ArgumentException(string.Format("Type already has a registered tag '{0}' for type '{1}'", text, type.FullName), "type");
			}
			this.tagMappings.Add(type, tag);
			return this;
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x000381E5 File Offset: 0x000363E5
		public SerializerBuilder WithoutTagMapping(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!this.tagMappings.Remove(type))
			{
				throw new KeyNotFoundException(string.Format("Tag for type '{0}' is not registered", type.FullName));
			}
			return this;
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00038220 File Offset: 0x00036420
		public SerializerBuilder EnsureRoundtrip()
		{
			this.objectGraphTraversalStrategyFactory = (ITypeInspector typeInspector, ITypeResolver typeResolver, IEnumerable<IYamlTypeConverter> typeConverters) => new RoundtripObjectGraphTraversalStrategy(typeConverters, typeInspector, typeResolver, 50);
			this.WithEventEmitter<TypeAssigningEventEmitter>((IEventEmitter inner) => new TypeAssigningEventEmitter(inner, true), delegate(IRegistrationLocationSelectionSyntax<IEventEmitter> loc)
			{
				loc.InsteadOf<TypeAssigningEventEmitter>();
			});
			return base.WithTypeInspector<ReadableAndWritablePropertiesTypeInspector>((ITypeInspector inner) => new ReadableAndWritablePropertiesTypeInspector(inner), delegate(IRegistrationLocationSelectionSyntax<ITypeInspector> loc)
			{
				loc.OnBottom();
			});
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x000382DB File Offset: 0x000364DB
		public SerializerBuilder DisableAliases()
		{
			this.preProcessingPhaseObjectGraphVisitorFactories.Remove(typeof(AnchorAssigner));
			this.emissionPhaseObjectGraphVisitorFactories.Remove(typeof(AnchorAssigningObjectGraphVisitor));
			return this;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00038308 File Offset: 0x00036508
		public SerializerBuilder EmitDefaults()
		{
			this.emissionPhaseObjectGraphVisitorFactories.Remove(typeof(DefaultExclusiveObjectGraphVisitor));
			return this;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00038320 File Offset: 0x00036520
		public SerializerBuilder JsonCompatible()
		{
			return base.WithTypeConverter(new GuidConverter(true), delegate(IRegistrationLocationSelectionSyntax<IYamlTypeConverter> w)
			{
				w.InsteadOf<GuidConverter>();
			}).WithEventEmitter<JsonEventEmitter>((IEventEmitter inner) => new JsonEventEmitter(inner), delegate(IRegistrationLocationSelectionSyntax<IEventEmitter> loc)
			{
				loc.InsteadOf<TypeAssigningEventEmitter>();
			});
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0003839B File Offset: 0x0003659B
		public SerializerBuilder WithPreProcessingPhaseObjectGraphVisitor<TObjectGraphVisitor>(TObjectGraphVisitor objectGraphVisitor) where TObjectGraphVisitor : IObjectGraphVisitor<Nothing>
		{
			return this.WithPreProcessingPhaseObjectGraphVisitor<TObjectGraphVisitor>(objectGraphVisitor, delegate(IRegistrationLocationSelectionSyntax<IObjectGraphVisitor<Nothing>> w)
			{
				w.OnTop();
			});
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x000383C4 File Offset: 0x000365C4
		public SerializerBuilder WithPreProcessingPhaseObjectGraphVisitor<TObjectGraphVisitor>(TObjectGraphVisitor objectGraphVisitor, Action<IRegistrationLocationSelectionSyntax<IObjectGraphVisitor<Nothing>>> where) where TObjectGraphVisitor : IObjectGraphVisitor<Nothing>
		{
			if (objectGraphVisitor == null)
			{
				throw new ArgumentNullException("objectGraphVisitor");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(this.preProcessingPhaseObjectGraphVisitorFactories.CreateRegistrationLocationSelector(typeof(TObjectGraphVisitor), (IEnumerable<IYamlTypeConverter> _) => objectGraphVisitor));
			return this;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0003842C File Offset: 0x0003662C
		public SerializerBuilder WithPreProcessingPhaseObjectGraphVisitor<TObjectGraphVisitor>(WrapperFactory<IObjectGraphVisitor<Nothing>, TObjectGraphVisitor> objectGraphVisitorFactory, Action<ITrackingRegistrationLocationSelectionSyntax<IObjectGraphVisitor<Nothing>>> where) where TObjectGraphVisitor : IObjectGraphVisitor<Nothing>
		{
			if (objectGraphVisitorFactory == null)
			{
				throw new ArgumentNullException("objectGraphVisitorFactory");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(this.preProcessingPhaseObjectGraphVisitorFactories.CreateTrackingRegistrationLocationSelector(typeof(TObjectGraphVisitor), (IObjectGraphVisitor<Nothing> wrapped, IEnumerable<IYamlTypeConverter> _) => objectGraphVisitorFactory(wrapped)));
			return this;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0003848F File Offset: 0x0003668F
		public SerializerBuilder WithoutPreProcessingPhaseObjectGraphVisitor<TObjectGraphVisitor>() where TObjectGraphVisitor : IObjectGraphVisitor<Nothing>
		{
			return this.WithoutPreProcessingPhaseObjectGraphVisitor(typeof(TObjectGraphVisitor));
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x000384A1 File Offset: 0x000366A1
		public SerializerBuilder WithoutPreProcessingPhaseObjectGraphVisitor(Type objectGraphVisitorType)
		{
			if (objectGraphVisitorType == null)
			{
				throw new ArgumentNullException("objectGraphVisitorType");
			}
			this.preProcessingPhaseObjectGraphVisitorFactories.Remove(objectGraphVisitorType);
			return this;
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x000384C4 File Offset: 0x000366C4
		public SerializerBuilder WithEmissionPhaseObjectGraphVisitor<TObjectGraphVisitor>(Func<EmissionPhaseObjectGraphVisitorArgs, TObjectGraphVisitor> objectGraphVisitorFactory) where TObjectGraphVisitor : IObjectGraphVisitor<IEmitter>
		{
			return this.WithEmissionPhaseObjectGraphVisitor<TObjectGraphVisitor>(objectGraphVisitorFactory, delegate(IRegistrationLocationSelectionSyntax<IObjectGraphVisitor<IEmitter>> w)
			{
				w.OnTop();
			});
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x000384EC File Offset: 0x000366EC
		public SerializerBuilder WithEmissionPhaseObjectGraphVisitor<TObjectGraphVisitor>(Func<EmissionPhaseObjectGraphVisitorArgs, TObjectGraphVisitor> objectGraphVisitorFactory, Action<IRegistrationLocationSelectionSyntax<IObjectGraphVisitor<IEmitter>>> where) where TObjectGraphVisitor : IObjectGraphVisitor<IEmitter>
		{
			if (objectGraphVisitorFactory == null)
			{
				throw new ArgumentNullException("objectGraphVisitorFactory");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(this.emissionPhaseObjectGraphVisitorFactories.CreateRegistrationLocationSelector(typeof(TObjectGraphVisitor), (EmissionPhaseObjectGraphVisitorArgs args) => objectGraphVisitorFactory(args)));
			return this;
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00038550 File Offset: 0x00036750
		public SerializerBuilder WithEmissionPhaseObjectGraphVisitor<TObjectGraphVisitor>(WrapperFactory<EmissionPhaseObjectGraphVisitorArgs, IObjectGraphVisitor<IEmitter>, TObjectGraphVisitor> objectGraphVisitorFactory, Action<ITrackingRegistrationLocationSelectionSyntax<IObjectGraphVisitor<IEmitter>>> where) where TObjectGraphVisitor : IObjectGraphVisitor<IEmitter>
		{
			if (objectGraphVisitorFactory == null)
			{
				throw new ArgumentNullException("objectGraphVisitorFactory");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(this.emissionPhaseObjectGraphVisitorFactories.CreateTrackingRegistrationLocationSelector(typeof(TObjectGraphVisitor), (IObjectGraphVisitor<IEmitter> wrapped, EmissionPhaseObjectGraphVisitorArgs args) => objectGraphVisitorFactory(wrapped, args)));
			return this;
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x000385B3 File Offset: 0x000367B3
		public SerializerBuilder WithoutEmissionPhaseObjectGraphVisitor<TObjectGraphVisitor>() where TObjectGraphVisitor : IObjectGraphVisitor<IEmitter>
		{
			return this.WithoutEmissionPhaseObjectGraphVisitor(typeof(TObjectGraphVisitor));
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x000385C5 File Offset: 0x000367C5
		public SerializerBuilder WithoutEmissionPhaseObjectGraphVisitor(Type objectGraphVisitorType)
		{
			if (objectGraphVisitorType == null)
			{
				throw new ArgumentNullException("objectGraphVisitorType");
			}
			this.emissionPhaseObjectGraphVisitorFactories.Remove(objectGraphVisitorType);
			return this;
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x000385E8 File Offset: 0x000367E8
		public Serializer Build()
		{
			return Serializer.FromValueSerializer(this.BuildValueSerializer());
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x000385F8 File Offset: 0x000367F8
		public IValueSerializer BuildValueSerializer()
		{
			IEnumerable<IYamlTypeConverter> enumerable = base.BuildTypeConverters();
			ITypeInspector typeInspector = base.BuildTypeInspector();
			IObjectGraphTraversalStrategy objectGraphTraversalStrategy = this.objectGraphTraversalStrategyFactory(typeInspector, this.typeResolver, enumerable);
			IEventEmitter eventEmitter = this.eventEmitterFactories.BuildComponentChain(new WriterEventEmitter());
			return new SerializerBuilder.ValueSerializer(objectGraphTraversalStrategy, eventEmitter, enumerable, this.preProcessingPhaseObjectGraphVisitorFactories.Clone(), this.emissionPhaseObjectGraphVisitorFactories.Clone());
		}

		// Token: 0x04000804 RID: 2052
		private Func<ITypeInspector, ITypeResolver, IEnumerable<IYamlTypeConverter>, IObjectGraphTraversalStrategy> objectGraphTraversalStrategyFactory;

		// Token: 0x04000805 RID: 2053
		private readonly LazyComponentRegistrationList<IEnumerable<IYamlTypeConverter>, IObjectGraphVisitor<Nothing>> preProcessingPhaseObjectGraphVisitorFactories;

		// Token: 0x04000806 RID: 2054
		private readonly LazyComponentRegistrationList<EmissionPhaseObjectGraphVisitorArgs, IObjectGraphVisitor<IEmitter>> emissionPhaseObjectGraphVisitorFactories;

		// Token: 0x04000807 RID: 2055
		private readonly LazyComponentRegistrationList<IEventEmitter, IEventEmitter> eventEmitterFactories;

		// Token: 0x04000808 RID: 2056
		private readonly IDictionary<Type, string> tagMappings = new Dictionary<Type, string>();

		// Token: 0x02000A31 RID: 2609
		private class ValueSerializer : IValueSerializer
		{
			// Token: 0x060054B4 RID: 21684 RVA: 0x0009DB76 File Offset: 0x0009BD76
			public ValueSerializer(IObjectGraphTraversalStrategy traversalStrategy, IEventEmitter eventEmitter, IEnumerable<IYamlTypeConverter> typeConverters, LazyComponentRegistrationList<IEnumerable<IYamlTypeConverter>, IObjectGraphVisitor<Nothing>> preProcessingPhaseObjectGraphVisitorFactories, LazyComponentRegistrationList<EmissionPhaseObjectGraphVisitorArgs, IObjectGraphVisitor<IEmitter>> emissionPhaseObjectGraphVisitorFactories)
			{
				this.traversalStrategy = traversalStrategy;
				this.eventEmitter = eventEmitter;
				this.typeConverters = typeConverters;
				this.preProcessingPhaseObjectGraphVisitorFactories = preProcessingPhaseObjectGraphVisitorFactories;
				this.emissionPhaseObjectGraphVisitorFactories = emissionPhaseObjectGraphVisitorFactories;
			}

			// Token: 0x060054B5 RID: 21685 RVA: 0x0009DBA4 File Offset: 0x0009BDA4
			public void SerializeValue(IEmitter emitter, object value, Type type)
			{
				Type type2 = ((type != null) ? type : ((value != null) ? value.GetType() : typeof(object)));
				Type type3 = type ?? typeof(object);
				ObjectDescriptor objectDescriptor = new ObjectDescriptor(value, type2, type3);
				List<IObjectGraphVisitor<Nothing>> preProcessingPhaseObjectGraphVisitors = this.preProcessingPhaseObjectGraphVisitorFactories.BuildComponentList(this.typeConverters);
				foreach (IObjectGraphVisitor<Nothing> objectGraphVisitor in preProcessingPhaseObjectGraphVisitors)
				{
					this.traversalStrategy.Traverse<Nothing>(objectDescriptor, objectGraphVisitor, null);
				}
				ObjectSerializer nestedObjectSerializer = delegate(object v, Type t)
				{
					this.SerializeValue(emitter, v, t);
				};
				IObjectGraphVisitor<IEmitter> objectGraphVisitor2 = this.emissionPhaseObjectGraphVisitorFactories.BuildComponentChain(new EmittingObjectGraphVisitor(this.eventEmitter), (IObjectGraphVisitor<IEmitter> inner) => new EmissionPhaseObjectGraphVisitorArgs(inner, this.eventEmitter, preProcessingPhaseObjectGraphVisitors, this.typeConverters, nestedObjectSerializer));
				this.traversalStrategy.Traverse<IEmitter>(objectDescriptor, objectGraphVisitor2, emitter);
			}

			// Token: 0x040022D7 RID: 8919
			private readonly IObjectGraphTraversalStrategy traversalStrategy;

			// Token: 0x040022D8 RID: 8920
			private readonly IEventEmitter eventEmitter;

			// Token: 0x040022D9 RID: 8921
			private readonly IEnumerable<IYamlTypeConverter> typeConverters;

			// Token: 0x040022DA RID: 8922
			private readonly LazyComponentRegistrationList<IEnumerable<IYamlTypeConverter>, IObjectGraphVisitor<Nothing>> preProcessingPhaseObjectGraphVisitorFactories;

			// Token: 0x040022DB RID: 8923
			private readonly LazyComponentRegistrationList<EmissionPhaseObjectGraphVisitorArgs, IObjectGraphVisitor<IEmitter>> emissionPhaseObjectGraphVisitorFactories;
		}
	}
}
