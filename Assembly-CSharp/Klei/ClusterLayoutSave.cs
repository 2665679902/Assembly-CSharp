using System;
using System.Collections.Generic;

namespace Klei
{
	// Token: 0x02000D58 RID: 3416
	public class ClusterLayoutSave
	{
		// Token: 0x06006858 RID: 26712 RVA: 0x0028A65B File Offset: 0x0028885B
		public ClusterLayoutSave()
		{
			this.worlds = new List<ClusterLayoutSave.World>();
		}

		// Token: 0x04004E9D RID: 20125
		public string ID;

		// Token: 0x04004E9E RID: 20126
		public Vector2I version;

		// Token: 0x04004E9F RID: 20127
		public List<ClusterLayoutSave.World> worlds;

		// Token: 0x04004EA0 RID: 20128
		public Vector2I size;

		// Token: 0x04004EA1 RID: 20129
		public int currentWorldIdx;

		// Token: 0x04004EA2 RID: 20130
		public int numRings;

		// Token: 0x04004EA3 RID: 20131
		public Dictionary<ClusterLayoutSave.POIType, List<AxialI>> poiLocations = new Dictionary<ClusterLayoutSave.POIType, List<AxialI>>();

		// Token: 0x04004EA4 RID: 20132
		public Dictionary<AxialI, string> poiPlacements = new Dictionary<AxialI, string>();

		// Token: 0x02001E3C RID: 7740
		public class World
		{
			// Token: 0x0400881A RID: 34842
			public Dictionary<string, object> stats = new Dictionary<string, object>();

			// Token: 0x0400881B RID: 34843
			public Data data = new Data();

			// Token: 0x0400881C RID: 34844
			public string name = string.Empty;

			// Token: 0x0400881D RID: 34845
			public bool isDiscovered;

			// Token: 0x0400881E RID: 34846
			public List<string> traits = new List<string>();

			// Token: 0x0400881F RID: 34847
			public List<string> storyTraits = new List<string>();
		}

		// Token: 0x02001E3D RID: 7741
		public enum POIType
		{
			// Token: 0x04008821 RID: 34849
			TemporalTear,
			// Token: 0x04008822 RID: 34850
			ResearchDestination
		}
	}
}
