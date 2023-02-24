using System;

namespace TemplateClasses
{
	// Token: 0x02000C5D RID: 3165
	[Serializable]
	public class StorageItem
	{
		// Token: 0x060064B4 RID: 25780 RVA: 0x0025AD34 File Offset: 0x00258F34
		public StorageItem()
		{
			this.rottable = new Rottable();
		}

		// Token: 0x060064B5 RID: 25781 RVA: 0x0025AD48 File Offset: 0x00258F48
		public StorageItem(string _id, float _units, float _temp, SimHashes _element, string _disease, int _disease_count, bool _isOre)
		{
			this.rottable = new Rottable();
			this.id = _id;
			this.element = _element;
			this.units = _units;
			this.diseaseName = _disease;
			this.diseaseCount = _disease_count;
			this.isOre = _isOre;
			this.temperature = _temp;
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060064B6 RID: 25782 RVA: 0x0025AD9B File Offset: 0x00258F9B
		// (set) Token: 0x060064B7 RID: 25783 RVA: 0x0025ADA3 File Offset: 0x00258FA3
		public string id { get; set; }

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060064B8 RID: 25784 RVA: 0x0025ADAC File Offset: 0x00258FAC
		// (set) Token: 0x060064B9 RID: 25785 RVA: 0x0025ADB4 File Offset: 0x00258FB4
		public SimHashes element { get; set; }

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060064BA RID: 25786 RVA: 0x0025ADBD File Offset: 0x00258FBD
		// (set) Token: 0x060064BB RID: 25787 RVA: 0x0025ADC5 File Offset: 0x00258FC5
		public float units { get; set; }

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060064BC RID: 25788 RVA: 0x0025ADCE File Offset: 0x00258FCE
		// (set) Token: 0x060064BD RID: 25789 RVA: 0x0025ADD6 File Offset: 0x00258FD6
		public bool isOre { get; set; }

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060064BE RID: 25790 RVA: 0x0025ADDF File Offset: 0x00258FDF
		// (set) Token: 0x060064BF RID: 25791 RVA: 0x0025ADE7 File Offset: 0x00258FE7
		public float temperature { get; set; }

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060064C0 RID: 25792 RVA: 0x0025ADF0 File Offset: 0x00258FF0
		// (set) Token: 0x060064C1 RID: 25793 RVA: 0x0025ADF8 File Offset: 0x00258FF8
		public string diseaseName { get; set; }

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x060064C2 RID: 25794 RVA: 0x0025AE01 File Offset: 0x00259001
		// (set) Token: 0x060064C3 RID: 25795 RVA: 0x0025AE09 File Offset: 0x00259009
		public int diseaseCount { get; set; }

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060064C4 RID: 25796 RVA: 0x0025AE12 File Offset: 0x00259012
		// (set) Token: 0x060064C5 RID: 25797 RVA: 0x0025AE1A File Offset: 0x0025901A
		public Rottable rottable { get; set; }

		// Token: 0x060064C6 RID: 25798 RVA: 0x0025AE24 File Offset: 0x00259024
		public StorageItem Clone()
		{
			return new StorageItem(this.id, this.units, this.temperature, this.element, this.diseaseName, this.diseaseCount, this.isOre)
			{
				rottable = 
				{
					rotAmount = this.rottable.rotAmount
				}
			};
		}
	}
}
