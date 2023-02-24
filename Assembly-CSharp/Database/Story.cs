using System;
using ProcGen;

namespace Database
{
	// Token: 0x02000CC0 RID: 3264
	public class Story : Resource, IComparable<Story>
	{
		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x0600661F RID: 26143 RVA: 0x00275733 File Offset: 0x00273933
		// (set) Token: 0x06006620 RID: 26144 RVA: 0x0027573B File Offset: 0x0027393B
		public int HashId { get; private set; }

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06006621 RID: 26145 RVA: 0x00275744 File Offset: 0x00273944
		public WorldTrait StoryTrait
		{
			get
			{
				if (this._cachedStoryTrait == null)
				{
					this._cachedStoryTrait = SettingsCache.GetCachedStoryTrait(this.worldgenStoryTraitKey, false);
				}
				return this._cachedStoryTrait;
			}
		}

		// Token: 0x06006622 RID: 26146 RVA: 0x00275766 File Offset: 0x00273966
		public Story(string id, string worldgenStoryTraitKey, int displayOrder)
		{
			this.Id = id;
			this.worldgenStoryTraitKey = worldgenStoryTraitKey;
			this.displayOrder = displayOrder;
			this.kleiUseOnlyCoordinateOffset = -1;
			this.updateNumber = -1;
			this.HashId = Hash.SDBMLower(id);
		}

		// Token: 0x06006623 RID: 26147 RVA: 0x002757A0 File Offset: 0x002739A0
		public Story(string id, string worldgenStoryTraitKey, int displayOrder, int kleiUseOnlyCoordinateOffset, int updateNumber)
		{
			this.Id = id;
			this.worldgenStoryTraitKey = worldgenStoryTraitKey;
			this.displayOrder = displayOrder;
			this.updateNumber = updateNumber;
			DebugUtil.Assert(kleiUseOnlyCoordinateOffset < 20, "More than 19 stories is unsupported!");
			this.kleiUseOnlyCoordinateOffset = kleiUseOnlyCoordinateOffset;
			this.HashId = Hash.SDBMLower(id);
		}

		// Token: 0x06006624 RID: 26148 RVA: 0x002757F4 File Offset: 0x002739F4
		public int CompareTo(Story other)
		{
			return this.displayOrder.CompareTo(other.displayOrder);
		}

		// Token: 0x06006625 RID: 26149 RVA: 0x00275815 File Offset: 0x00273A15
		public bool IsNew()
		{
			return this.updateNumber == LaunchInitializer.UpdateNumber();
		}

		// Token: 0x06006626 RID: 26150 RVA: 0x00275824 File Offset: 0x00273A24
		public Story AutoStart()
		{
			this.autoStart = true;
			return this;
		}

		// Token: 0x06006627 RID: 26151 RVA: 0x0027582E File Offset: 0x00273A2E
		public Story SetKeepsake(string prefabId)
		{
			this.keepsakePrefabId = prefabId;
			return this;
		}

		// Token: 0x04004AB8 RID: 19128
		public const int MODDED_STORY = -1;

		// Token: 0x04004AB9 RID: 19129
		public int kleiUseOnlyCoordinateOffset;

		// Token: 0x04004ABB RID: 19131
		public bool autoStart;

		// Token: 0x04004ABC RID: 19132
		public string keepsakePrefabId;

		// Token: 0x04004ABD RID: 19133
		public readonly string worldgenStoryTraitKey;

		// Token: 0x04004ABE RID: 19134
		private readonly int displayOrder;

		// Token: 0x04004ABF RID: 19135
		private readonly int updateNumber;

		// Token: 0x04004AC0 RID: 19136
		private WorldTrait _cachedStoryTrait;
	}
}
