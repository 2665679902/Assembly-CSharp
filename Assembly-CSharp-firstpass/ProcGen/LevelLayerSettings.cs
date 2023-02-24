using System;

namespace ProcGen
{
	// Token: 0x020004D9 RID: 1241
	public class LevelLayerSettings : IMerge<LevelLayerSettings>
	{
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06003554 RID: 13652 RVA: 0x00075290 File Offset: 0x00073490
		// (set) Token: 0x06003555 RID: 13653 RVA: 0x00075298 File Offset: 0x00073498
		public LevelLayer LevelLayers { get; private set; }

		// Token: 0x06003556 RID: 13654 RVA: 0x000752A1 File Offset: 0x000734A1
		public LevelLayerSettings()
		{
			this.LevelLayers = new LevelLayer();
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x000752B4 File Offset: 0x000734B4
		public LevelLayerSettings Merge(LevelLayerSettings other)
		{
			this.LevelLayers.Merge(other.LevelLayers);
			return this;
		}
	}
}
