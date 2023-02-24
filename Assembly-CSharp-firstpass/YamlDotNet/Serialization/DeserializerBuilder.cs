using System;
using System.Collections.Generic;
using YamlDotNet.Serialization.NodeDeserializers;
using YamlDotNet.Serialization.NodeTypeResolvers;
using YamlDotNet.Serialization.ObjectFactories;
using YamlDotNet.Serialization.TypeInspectors;
using YamlDotNet.Serialization.TypeResolvers;
using YamlDotNet.Serialization.ValueDeserializers;

namespace YamlDotNet.Serialization
{
	// Token: 0x02000178 RID: 376
	public sealed class DeserializerBuilder : BuilderSkeleton<DeserializerBuilder>
	{
		// Token: 0x06000CA3 RID: 3235 RVA: 0x00036E94 File Offset: 0x00035094
		public DeserializerBuilder()
		{
			this.tagMappings = new Dictionary<string, Type>
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
			this.typeInspectorFactories.Add(typeof(ReadableAndWritablePropertiesTypeInspector), (ITypeInspector inner) => new ReadableAndWritablePropertiesTypeInspector(inner));
			LazyComponentRegistrationList<Nothing, INodeDeserializer> lazyComponentRegistrationList = new LazyComponentRegistrationList<Nothing, INodeDeserializer>();
			lazyComponentRegistrationList.Add(typeof(YamlConvertibleNodeDeserializer), (Nothing _) => new YamlConvertibleNodeDeserializer(this.objectFactory));
			lazyComponentRegistrationList.Add(typeof(YamlSerializableNodeDeserializer), (Nothing _) => new YamlSerializableNodeDeserializer(this.objectFactory));
			lazyComponentRegistrationList.Add(typeof(TypeConverterNodeDeserializer), (Nothing _) => new TypeConverterNodeDeserializer(base.BuildTypeConverters()));
			lazyComponentRegistrationList.Add(typeof(NullNodeDeserializer), (Nothing _) => new NullNodeDeserializer());
			lazyComponentRegistrationList.Add(typeof(ScalarNodeDeserializer), (Nothing _) => new ScalarNodeDeserializer());
			lazyComponentRegistrationList.Add(typeof(ArrayNodeDeserializer), (Nothing _) => new ArrayNodeDeserializer());
			lazyComponentRegistrationList.Add(typeof(DictionaryNodeDeserializer), (Nothing _) => new DictionaryNodeDeserializer(this.objectFactory));
			lazyComponentRegistrationList.Add(typeof(CollectionNodeDeserializer), (Nothing _) => new CollectionNodeDeserializer(this.objectFactory));
			lazyComponentRegistrationList.Add(typeof(EnumerableNodeDeserializer), (Nothing _) => new EnumerableNodeDeserializer());
			lazyComponentRegistrationList.Add(typeof(ObjectNodeDeserializer), (Nothing _) => new ObjectNodeDeserializer(this.objectFactory, base.BuildTypeInspector(), this.ignoreUnmatched, this.unmatchedLogFn));
			this.nodeDeserializerFactories = lazyComponentRegistrationList;
			LazyComponentRegistrationList<Nothing, INodeTypeResolver> lazyComponentRegistrationList2 = new LazyComponentRegistrationList<Nothing, INodeTypeResolver>();
			lazyComponentRegistrationList2.Add(typeof(YamlConvertibleTypeResolver), (Nothing _) => new YamlConvertibleTypeResolver());
			lazyComponentRegistrationList2.Add(typeof(YamlSerializableTypeResolver), (Nothing _) => new YamlSerializableTypeResolver());
			lazyComponentRegistrationList2.Add(typeof(TagNodeTypeResolver), (Nothing _) => new TagNodeTypeResolver(this.tagMappings));
			lazyComponentRegistrationList2.Add(typeof(TypeNameInTagNodeTypeResolver), (Nothing _) => new TypeNameInTagNodeTypeResolver());
			lazyComponentRegistrationList2.Add(typeof(DefaultContainersNodeTypeResolver), (Nothing _) => new DefaultContainersNodeTypeResolver());
			this.nodeTypeResolverFactories = lazyComponentRegistrationList2;
			base.WithTypeResolver(new StaticTypeResolver());
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x00037277 File Offset: 0x00035477
		protected override DeserializerBuilder Self
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0003727A File Offset: 0x0003547A
		public DeserializerBuilder WithObjectFactory(IObjectFactory objectFactory)
		{
			if (objectFactory == null)
			{
				throw new ArgumentNullException("objectFactory");
			}
			this.objectFactory = objectFactory;
			return this;
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00037292 File Offset: 0x00035492
		public DeserializerBuilder WithObjectFactory(Func<Type, object> objectFactory)
		{
			if (objectFactory == null)
			{
				throw new ArgumentNullException("objectFactory");
			}
			return this.WithObjectFactory(new LambdaObjectFactory(objectFactory));
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x000372AE File Offset: 0x000354AE
		public DeserializerBuilder WithNodeDeserializer(INodeDeserializer nodeDeserializer)
		{
			return this.WithNodeDeserializer(nodeDeserializer, delegate(IRegistrationLocationSelectionSyntax<INodeDeserializer> w)
			{
				w.OnTop();
			});
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x000372D8 File Offset: 0x000354D8
		public DeserializerBuilder WithNodeDeserializer(INodeDeserializer nodeDeserializer, Action<IRegistrationLocationSelectionSyntax<INodeDeserializer>> where)
		{
			if (nodeDeserializer == null)
			{
				throw new ArgumentNullException("nodeDeserializer");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(this.nodeDeserializerFactories.CreateRegistrationLocationSelector(nodeDeserializer.GetType(), (Nothing _) => nodeDeserializer));
			return this;
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0003733C File Offset: 0x0003553C
		public DeserializerBuilder WithNodeDeserializer<TNodeDeserializer>(WrapperFactory<INodeDeserializer, TNodeDeserializer> nodeDeserializerFactory, Action<ITrackingRegistrationLocationSelectionSyntax<INodeDeserializer>> where) where TNodeDeserializer : INodeDeserializer
		{
			if (nodeDeserializerFactory == null)
			{
				throw new ArgumentNullException("nodeDeserializerFactory");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(this.nodeDeserializerFactories.CreateTrackingRegistrationLocationSelector(typeof(TNodeDeserializer), (INodeDeserializer wrapped, Nothing _) => nodeDeserializerFactory(wrapped)));
			return this;
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0003739F File Offset: 0x0003559F
		public DeserializerBuilder WithoutNodeDeserializer<TNodeDeserializer>() where TNodeDeserializer : INodeDeserializer
		{
			return this.WithoutNodeDeserializer(typeof(TNodeDeserializer));
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x000373B1 File Offset: 0x000355B1
		public DeserializerBuilder WithoutNodeDeserializer(Type nodeDeserializerType)
		{
			if (nodeDeserializerType == null)
			{
				throw new ArgumentNullException("nodeDeserializerType");
			}
			this.nodeDeserializerFactories.Remove(nodeDeserializerType);
			return this;
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x000373D4 File Offset: 0x000355D4
		public DeserializerBuilder WithNodeTypeResolver(INodeTypeResolver nodeTypeResolver)
		{
			return this.WithNodeTypeResolver(nodeTypeResolver, delegate(IRegistrationLocationSelectionSyntax<INodeTypeResolver> w)
			{
				w.OnTop();
			});
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x000373FC File Offset: 0x000355FC
		public DeserializerBuilder WithNodeTypeResolver(INodeTypeResolver nodeTypeResolver, Action<IRegistrationLocationSelectionSyntax<INodeTypeResolver>> where)
		{
			if (nodeTypeResolver == null)
			{
				throw new ArgumentNullException("nodeTypeResolver");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(this.nodeTypeResolverFactories.CreateRegistrationLocationSelector(nodeTypeResolver.GetType(), (Nothing _) => nodeTypeResolver));
			return this;
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00037460 File Offset: 0x00035660
		public DeserializerBuilder WithNodeTypeResolver<TNodeTypeResolver>(WrapperFactory<INodeTypeResolver, TNodeTypeResolver> nodeTypeResolverFactory, Action<ITrackingRegistrationLocationSelectionSyntax<INodeTypeResolver>> where) where TNodeTypeResolver : INodeTypeResolver
		{
			if (nodeTypeResolverFactory == null)
			{
				throw new ArgumentNullException("nodeTypeResolverFactory");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(this.nodeTypeResolverFactories.CreateTrackingRegistrationLocationSelector(typeof(TNodeTypeResolver), (INodeTypeResolver wrapped, Nothing _) => nodeTypeResolverFactory(wrapped)));
			return this;
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x000374C3 File Offset: 0x000356C3
		public DeserializerBuilder WithoutNodeTypeResolver<TNodeTypeResolver>() where TNodeTypeResolver : INodeTypeResolver
		{
			return this.WithoutNodeTypeResolver(typeof(TNodeTypeResolver));
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x000374D5 File Offset: 0x000356D5
		public DeserializerBuilder WithoutNodeTypeResolver(Type nodeTypeResolverType)
		{
			if (nodeTypeResolverType == null)
			{
				throw new ArgumentNullException("nodeTypeResolverType");
			}
			this.nodeTypeResolverFactories.Remove(nodeTypeResolverType);
			return this;
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x000374F8 File Offset: 0x000356F8
		public DeserializerBuilder WithTagMapping(string tag, Type type)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Type type2;
			if (this.tagMappings.TryGetValue(tag, out type2))
			{
				throw new ArgumentException(string.Format("Type already has a registered type '{0}' for tag '{1}'", type2.FullName, tag), "tag");
			}
			this.tagMappings.Add(tag, type);
			return this;
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00037561 File Offset: 0x00035761
		public DeserializerBuilder WithoutTagMapping(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (!this.tagMappings.Remove(tag))
			{
				throw new KeyNotFoundException(string.Format("Tag '{0}' is not registered", tag));
			}
			return this;
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00037591 File Offset: 0x00035791
		public DeserializerBuilder IgnoreUnmatchedProperties(Action<string> unmatchedLogFn = null)
		{
			this.ignoreUnmatched = true;
			this.unmatchedLogFn = unmatchedLogFn;
			return this;
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x000375A2 File Offset: 0x000357A2
		public Deserializer Build()
		{
			return Deserializer.FromValueDeserializer(this.BuildValueDeserializer());
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x000375AF File Offset: 0x000357AF
		public IValueDeserializer BuildValueDeserializer()
		{
			return new AliasValueDeserializer(new NodeValueDeserializer(this.nodeDeserializerFactories.BuildComponentList<INodeDeserializer>(), this.nodeTypeResolverFactories.BuildComponentList<INodeTypeResolver>()));
		}

		// Token: 0x040007DC RID: 2012
		private IObjectFactory objectFactory = new DefaultObjectFactory();

		// Token: 0x040007DD RID: 2013
		private readonly LazyComponentRegistrationList<Nothing, INodeDeserializer> nodeDeserializerFactories;

		// Token: 0x040007DE RID: 2014
		private readonly LazyComponentRegistrationList<Nothing, INodeTypeResolver> nodeTypeResolverFactories;

		// Token: 0x040007DF RID: 2015
		private readonly Dictionary<string, Type> tagMappings;

		// Token: 0x040007E0 RID: 2016
		private bool ignoreUnmatched;

		// Token: 0x040007E1 RID: 2017
		private Action<string> unmatchedLogFn;
	}
}
