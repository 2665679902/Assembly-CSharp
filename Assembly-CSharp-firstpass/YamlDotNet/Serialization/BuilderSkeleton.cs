using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using YamlDotNet.Serialization.Converters;
using YamlDotNet.Serialization.TypeInspectors;

namespace YamlDotNet.Serialization
{
	// Token: 0x02000174 RID: 372
	public abstract class BuilderSkeleton<TBuilder> where TBuilder : BuilderSkeleton<TBuilder>
	{
		// Token: 0x06000C78 RID: 3192 RVA: 0x000368C4 File Offset: 0x00034AC4
		internal BuilderSkeleton()
		{
			this.overrides = new YamlAttributeOverrides();
			this.typeConverterFactories = new LazyComponentRegistrationList<Nothing, IYamlTypeConverter>();
			this.typeConverterFactories.Add(typeof(GuidConverter), (Nothing _) => new GuidConverter(false));
			this.typeInspectorFactories = new LazyComponentRegistrationList<ITypeInspector, ITypeInspector>();
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000C79 RID: 3193
		protected abstract TBuilder Self { get; }

		// Token: 0x06000C7A RID: 3194 RVA: 0x0003692C File Offset: 0x00034B2C
		internal ITypeInspector BuildTypeInspector()
		{
			return this.typeInspectorFactories.BuildComponentChain(new ReadablePropertiesTypeInspector(this.typeResolver));
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00036944 File Offset: 0x00034B44
		public TBuilder WithNamingConvention(INamingConvention namingConvention)
		{
			if (namingConvention == null)
			{
				throw new ArgumentNullException("namingConvention");
			}
			this.namingConvention = namingConvention;
			return this.Self;
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00036961 File Offset: 0x00034B61
		public TBuilder WithTypeResolver(ITypeResolver typeResolver)
		{
			if (typeResolver == null)
			{
				throw new ArgumentNullException("typeResolver");
			}
			this.typeResolver = typeResolver;
			return this.Self;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0003697E File Offset: 0x00034B7E
		public TBuilder WithAttributeOverride<TClass>(Expression<Func<TClass, object>> propertyAccessor, Attribute attribute)
		{
			this.overrides.Add<TClass>(propertyAccessor, attribute);
			return this.Self;
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00036993 File Offset: 0x00034B93
		public TBuilder WithAttributeOverride(Type type, string member, Attribute attribute)
		{
			this.overrides.Add(type, member, attribute);
			return this.Self;
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x000369A9 File Offset: 0x00034BA9
		public TBuilder WithTypeConverter(IYamlTypeConverter typeConverter)
		{
			return this.WithTypeConverter(typeConverter, delegate(IRegistrationLocationSelectionSyntax<IYamlTypeConverter> w)
			{
				w.OnTop();
			});
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x000369D4 File Offset: 0x00034BD4
		public TBuilder WithTypeConverter(IYamlTypeConverter typeConverter, Action<IRegistrationLocationSelectionSyntax<IYamlTypeConverter>> where)
		{
			if (typeConverter == null)
			{
				throw new ArgumentNullException("typeConverter");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(this.typeConverterFactories.CreateRegistrationLocationSelector(typeConverter.GetType(), (Nothing _) => typeConverter));
			return this.Self;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00036A40 File Offset: 0x00034C40
		public TBuilder WithTypeConverter<TYamlTypeConverter>(WrapperFactory<IYamlTypeConverter, IYamlTypeConverter> typeConverterFactory, Action<ITrackingRegistrationLocationSelectionSyntax<IYamlTypeConverter>> where) where TYamlTypeConverter : IYamlTypeConverter
		{
			if (typeConverterFactory == null)
			{
				throw new ArgumentNullException("typeConverterFactory");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(this.typeConverterFactories.CreateTrackingRegistrationLocationSelector(typeof(TYamlTypeConverter), (IYamlTypeConverter wrapped, Nothing _) => typeConverterFactory(wrapped)));
			return this.Self;
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00036AA8 File Offset: 0x00034CA8
		public TBuilder WithoutTypeConverter<TYamlTypeConverter>() where TYamlTypeConverter : IYamlTypeConverter
		{
			return this.WithoutTypeConverter(typeof(TYamlTypeConverter));
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00036ABA File Offset: 0x00034CBA
		public TBuilder WithoutTypeConverter(Type converterType)
		{
			if (converterType == null)
			{
				throw new ArgumentNullException("converterType");
			}
			this.typeConverterFactories.Remove(converterType);
			return this.Self;
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00036AE2 File Offset: 0x00034CE2
		public TBuilder WithTypeInspector<TTypeInspector>(Func<ITypeInspector, TTypeInspector> typeInspectorFactory) where TTypeInspector : ITypeInspector
		{
			return this.WithTypeInspector<TTypeInspector>(typeInspectorFactory, delegate(IRegistrationLocationSelectionSyntax<ITypeInspector> w)
			{
				w.OnTop();
			});
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00036B0C File Offset: 0x00034D0C
		public TBuilder WithTypeInspector<TTypeInspector>(Func<ITypeInspector, TTypeInspector> typeInspectorFactory, Action<IRegistrationLocationSelectionSyntax<ITypeInspector>> where) where TTypeInspector : ITypeInspector
		{
			if (typeInspectorFactory == null)
			{
				throw new ArgumentNullException("typeInspectorFactory");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(this.typeInspectorFactories.CreateRegistrationLocationSelector(typeof(TTypeInspector), (ITypeInspector inner) => typeInspectorFactory(inner)));
			return this.Self;
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00036B74 File Offset: 0x00034D74
		public TBuilder WithTypeInspector<TTypeInspector>(WrapperFactory<ITypeInspector, ITypeInspector, TTypeInspector> typeInspectorFactory, Action<ITrackingRegistrationLocationSelectionSyntax<ITypeInspector>> where) where TTypeInspector : ITypeInspector
		{
			if (typeInspectorFactory == null)
			{
				throw new ArgumentNullException("typeInspectorFactory");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(this.typeInspectorFactories.CreateTrackingRegistrationLocationSelector(typeof(TTypeInspector), (ITypeInspector wrapped, ITypeInspector inner) => typeInspectorFactory(wrapped, inner)));
			return this.Self;
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00036BDC File Offset: 0x00034DDC
		public TBuilder WithoutTypeInspector<TTypeInspector>() where TTypeInspector : ITypeInspector
		{
			return this.WithoutTypeInspector(typeof(ITypeInspector));
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00036BEE File Offset: 0x00034DEE
		public TBuilder WithoutTypeInspector(Type inspectorType)
		{
			if (inspectorType == null)
			{
				throw new ArgumentNullException("inspectorType");
			}
			this.typeInspectorFactories.Remove(inspectorType);
			return this.Self;
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00036C16 File Offset: 0x00034E16
		protected IEnumerable<IYamlTypeConverter> BuildTypeConverters()
		{
			return this.typeConverterFactories.BuildComponentList<IYamlTypeConverter>();
		}

		// Token: 0x040007D5 RID: 2005
		internal INamingConvention namingConvention;

		// Token: 0x040007D6 RID: 2006
		internal ITypeResolver typeResolver;

		// Token: 0x040007D7 RID: 2007
		internal readonly YamlAttributeOverrides overrides;

		// Token: 0x040007D8 RID: 2008
		internal readonly LazyComponentRegistrationList<Nothing, IYamlTypeConverter> typeConverterFactories;

		// Token: 0x040007D9 RID: 2009
		internal readonly LazyComponentRegistrationList<ITypeInspector, ITypeInspector> typeInspectorFactories;
	}
}
