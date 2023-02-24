using System;

namespace TemplateClasses
{
	// Token: 0x02000C5C RID: 3164
	[Serializable]
	public class Cell
	{
		// Token: 0x060064A2 RID: 25762 RVA: 0x0025AC54 File Offset: 0x00258E54
		public Cell()
		{
		}

		// Token: 0x060064A3 RID: 25763 RVA: 0x0025AC5C File Offset: 0x00258E5C
		public Cell(int loc_x, int loc_y, SimHashes _element, float _temperature, float _mass, string _diseaseName, int _diseaseCount, bool _preventFoWReveal = false)
		{
			this.location_x = loc_x;
			this.location_y = loc_y;
			this.element = _element;
			this.temperature = _temperature;
			this.mass = _mass;
			this.diseaseName = _diseaseName;
			this.diseaseCount = _diseaseCount;
			this.preventFoWReveal = _preventFoWReveal;
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060064A4 RID: 25764 RVA: 0x0025ACAC File Offset: 0x00258EAC
		// (set) Token: 0x060064A5 RID: 25765 RVA: 0x0025ACB4 File Offset: 0x00258EB4
		public SimHashes element { get; set; }

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060064A6 RID: 25766 RVA: 0x0025ACBD File Offset: 0x00258EBD
		// (set) Token: 0x060064A7 RID: 25767 RVA: 0x0025ACC5 File Offset: 0x00258EC5
		public float mass { get; set; }

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060064A8 RID: 25768 RVA: 0x0025ACCE File Offset: 0x00258ECE
		// (set) Token: 0x060064A9 RID: 25769 RVA: 0x0025ACD6 File Offset: 0x00258ED6
		public float temperature { get; set; }

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060064AA RID: 25770 RVA: 0x0025ACDF File Offset: 0x00258EDF
		// (set) Token: 0x060064AB RID: 25771 RVA: 0x0025ACE7 File Offset: 0x00258EE7
		public string diseaseName { get; set; }

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060064AC RID: 25772 RVA: 0x0025ACF0 File Offset: 0x00258EF0
		// (set) Token: 0x060064AD RID: 25773 RVA: 0x0025ACF8 File Offset: 0x00258EF8
		public int diseaseCount { get; set; }

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060064AE RID: 25774 RVA: 0x0025AD01 File Offset: 0x00258F01
		// (set) Token: 0x060064AF RID: 25775 RVA: 0x0025AD09 File Offset: 0x00258F09
		public int location_x { get; set; }

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060064B0 RID: 25776 RVA: 0x0025AD12 File Offset: 0x00258F12
		// (set) Token: 0x060064B1 RID: 25777 RVA: 0x0025AD1A File Offset: 0x00258F1A
		public int location_y { get; set; }

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060064B2 RID: 25778 RVA: 0x0025AD23 File Offset: 0x00258F23
		// (set) Token: 0x060064B3 RID: 25779 RVA: 0x0025AD2B File Offset: 0x00258F2B
		public bool preventFoWReveal { get; set; }
	}
}
