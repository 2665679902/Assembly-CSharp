using System;
using System.Collections.Generic;

namespace TemplateClasses
{
	// Token: 0x02000C5B RID: 3163
	[Serializable]
	public class Prefab
	{
		// Token: 0x06006480 RID: 25728 RVA: 0x0025A98B File Offset: 0x00258B8B
		public Prefab()
		{
			this.type = Prefab.Type.Other;
		}

		// Token: 0x06006481 RID: 25729 RVA: 0x0025A99C File Offset: 0x00258B9C
		public Prefab(string _id, Prefab.Type _type, int loc_x, int loc_y, SimHashes _element, float _temperature = -1f, float _units = 1f, string _disease = null, int _disease_count = 0, Orientation _rotation = Orientation.Neutral, Prefab.template_amount_value[] _amount_values = null, Prefab.template_amount_value[] _other_values = null, int _connections = 0)
		{
			this.id = _id;
			this.type = _type;
			this.location_x = loc_x;
			this.location_y = loc_y;
			this.connections = _connections;
			this.element = _element;
			this.temperature = _temperature;
			this.units = _units;
			this.diseaseName = _disease;
			this.diseaseCount = _disease_count;
			this.rotationOrientation = _rotation;
			if (_amount_values != null && _amount_values.Length != 0)
			{
				this.amounts = _amount_values;
			}
			if (_other_values != null && _other_values.Length != 0)
			{
				this.other_values = _other_values;
			}
		}

		// Token: 0x06006482 RID: 25730 RVA: 0x0025AA28 File Offset: 0x00258C28
		public Prefab Clone(Vector2I offset)
		{
			Prefab prefab = new Prefab(this.id, this.type, offset.x + this.location_x, offset.y + this.location_y, this.element, this.temperature, this.units, this.diseaseName, this.diseaseCount, this.rotationOrientation, this.amounts, this.other_values, this.connections);
			if (this.rottable != null)
			{
				prefab.rottable = new Rottable();
				prefab.rottable.rotAmount = this.rottable.rotAmount;
			}
			if (this.storage != null && this.storage.Count > 0)
			{
				prefab.storage = new List<StorageItem>();
				foreach (StorageItem storageItem in this.storage)
				{
					prefab.storage.Add(storageItem.Clone());
				}
			}
			return prefab;
		}

		// Token: 0x06006483 RID: 25731 RVA: 0x0025AB34 File Offset: 0x00258D34
		public void AssignStorage(StorageItem _storage)
		{
			if (this.storage == null)
			{
				this.storage = new List<StorageItem>();
			}
			this.storage.Add(_storage);
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06006484 RID: 25732 RVA: 0x0025AB55 File Offset: 0x00258D55
		// (set) Token: 0x06006485 RID: 25733 RVA: 0x0025AB5D File Offset: 0x00258D5D
		public string id { get; set; }

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06006486 RID: 25734 RVA: 0x0025AB66 File Offset: 0x00258D66
		// (set) Token: 0x06006487 RID: 25735 RVA: 0x0025AB6E File Offset: 0x00258D6E
		public int location_x { get; set; }

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06006488 RID: 25736 RVA: 0x0025AB77 File Offset: 0x00258D77
		// (set) Token: 0x06006489 RID: 25737 RVA: 0x0025AB7F File Offset: 0x00258D7F
		public int location_y { get; set; }

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x0600648A RID: 25738 RVA: 0x0025AB88 File Offset: 0x00258D88
		// (set) Token: 0x0600648B RID: 25739 RVA: 0x0025AB90 File Offset: 0x00258D90
		public SimHashes element { get; set; }

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x0600648C RID: 25740 RVA: 0x0025AB99 File Offset: 0x00258D99
		// (set) Token: 0x0600648D RID: 25741 RVA: 0x0025ABA1 File Offset: 0x00258DA1
		public float temperature { get; set; }

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x0600648E RID: 25742 RVA: 0x0025ABAA File Offset: 0x00258DAA
		// (set) Token: 0x0600648F RID: 25743 RVA: 0x0025ABB2 File Offset: 0x00258DB2
		public float units { get; set; }

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06006490 RID: 25744 RVA: 0x0025ABBB File Offset: 0x00258DBB
		// (set) Token: 0x06006491 RID: 25745 RVA: 0x0025ABC3 File Offset: 0x00258DC3
		public string diseaseName { get; set; }

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06006492 RID: 25746 RVA: 0x0025ABCC File Offset: 0x00258DCC
		// (set) Token: 0x06006493 RID: 25747 RVA: 0x0025ABD4 File Offset: 0x00258DD4
		public int diseaseCount { get; set; }

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06006494 RID: 25748 RVA: 0x0025ABDD File Offset: 0x00258DDD
		// (set) Token: 0x06006495 RID: 25749 RVA: 0x0025ABE5 File Offset: 0x00258DE5
		public Orientation rotationOrientation { get; set; }

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06006496 RID: 25750 RVA: 0x0025ABEE File Offset: 0x00258DEE
		// (set) Token: 0x06006497 RID: 25751 RVA: 0x0025ABF6 File Offset: 0x00258DF6
		public List<StorageItem> storage { get; set; }

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06006498 RID: 25752 RVA: 0x0025ABFF File Offset: 0x00258DFF
		// (set) Token: 0x06006499 RID: 25753 RVA: 0x0025AC07 File Offset: 0x00258E07
		public Prefab.Type type { get; set; }

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x0600649A RID: 25754 RVA: 0x0025AC10 File Offset: 0x00258E10
		// (set) Token: 0x0600649B RID: 25755 RVA: 0x0025AC18 File Offset: 0x00258E18
		public int connections { get; set; }

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x0600649C RID: 25756 RVA: 0x0025AC21 File Offset: 0x00258E21
		// (set) Token: 0x0600649D RID: 25757 RVA: 0x0025AC29 File Offset: 0x00258E29
		public Rottable rottable { get; set; }

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x0600649E RID: 25758 RVA: 0x0025AC32 File Offset: 0x00258E32
		// (set) Token: 0x0600649F RID: 25759 RVA: 0x0025AC3A File Offset: 0x00258E3A
		public Prefab.template_amount_value[] amounts { get; set; }

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060064A0 RID: 25760 RVA: 0x0025AC43 File Offset: 0x00258E43
		// (set) Token: 0x060064A1 RID: 25761 RVA: 0x0025AC4B File Offset: 0x00258E4B
		public Prefab.template_amount_value[] other_values { get; set; }

		// Token: 0x02001B05 RID: 6917
		public enum Type
		{
			// Token: 0x0400796E RID: 31086
			Building,
			// Token: 0x0400796F RID: 31087
			Ore,
			// Token: 0x04007970 RID: 31088
			Pickupable,
			// Token: 0x04007971 RID: 31089
			Other
		}

		// Token: 0x02001B06 RID: 6918
		[Serializable]
		public class template_amount_value
		{
			// Token: 0x06009497 RID: 38039 RVA: 0x0031CED6 File Offset: 0x0031B0D6
			public template_amount_value()
			{
			}

			// Token: 0x06009498 RID: 38040 RVA: 0x0031CEDE File Offset: 0x0031B0DE
			public template_amount_value(string id, float value)
			{
				this.id = id;
				this.value = value;
			}

			// Token: 0x170009E2 RID: 2530
			// (get) Token: 0x06009499 RID: 38041 RVA: 0x0031CEF4 File Offset: 0x0031B0F4
			// (set) Token: 0x0600949A RID: 38042 RVA: 0x0031CEFC File Offset: 0x0031B0FC
			public string id { get; set; }

			// Token: 0x170009E3 RID: 2531
			// (get) Token: 0x0600949B RID: 38043 RVA: 0x0031CF05 File Offset: 0x0031B105
			// (set) Token: 0x0600949C RID: 38044 RVA: 0x0031CF0D File Offset: 0x0031B10D
			public float value { get; set; }
		}
	}
}
