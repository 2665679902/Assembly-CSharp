using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	// Token: 0x02000179 RID: 377
	public sealed class EmissionPhaseObjectGraphVisitorArgs
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x00037693 File Offset: 0x00035893
		// (set) Token: 0x06000CC0 RID: 3264 RVA: 0x0003769B File Offset: 0x0003589B
		public IObjectGraphVisitor<IEmitter> InnerVisitor { get; private set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x000376A4 File Offset: 0x000358A4
		// (set) Token: 0x06000CC2 RID: 3266 RVA: 0x000376AC File Offset: 0x000358AC
		public IEventEmitter EventEmitter { get; private set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x000376B5 File Offset: 0x000358B5
		// (set) Token: 0x06000CC4 RID: 3268 RVA: 0x000376BD File Offset: 0x000358BD
		public ObjectSerializer NestedObjectSerializer { get; private set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x000376C6 File Offset: 0x000358C6
		// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x000376CE File Offset: 0x000358CE
		public IEnumerable<IYamlTypeConverter> TypeConverters { get; private set; }

		// Token: 0x06000CC7 RID: 3271 RVA: 0x000376D8 File Offset: 0x000358D8
		public EmissionPhaseObjectGraphVisitorArgs(IObjectGraphVisitor<IEmitter> innerVisitor, IEventEmitter eventEmitter, IEnumerable<IObjectGraphVisitor<Nothing>> preProcessingPhaseVisitors, IEnumerable<IYamlTypeConverter> typeConverters, ObjectSerializer nestedObjectSerializer)
		{
			if (innerVisitor == null)
			{
				throw new ArgumentNullException("innerVisitor");
			}
			this.InnerVisitor = innerVisitor;
			if (eventEmitter == null)
			{
				throw new ArgumentNullException("eventEmitter");
			}
			this.EventEmitter = eventEmitter;
			if (preProcessingPhaseVisitors == null)
			{
				throw new ArgumentNullException("preProcessingPhaseVisitors");
			}
			this.preProcessingPhaseVisitors = preProcessingPhaseVisitors;
			if (typeConverters == null)
			{
				throw new ArgumentNullException("typeConverters");
			}
			this.TypeConverters = typeConverters;
			if (nestedObjectSerializer == null)
			{
				throw new ArgumentNullException("nestedObjectSerializer");
			}
			this.NestedObjectSerializer = nestedObjectSerializer;
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00037758 File Offset: 0x00035958
		public T GetPreProcessingPhaseObjectGraphVisitor<T>() where T : IObjectGraphVisitor<Nothing>
		{
			return this.preProcessingPhaseVisitors.OfType<T>().Single<T>();
		}

		// Token: 0x040007E6 RID: 2022
		private readonly IEnumerable<IObjectGraphVisitor<Nothing>> preProcessingPhaseVisitors;
	}
}
