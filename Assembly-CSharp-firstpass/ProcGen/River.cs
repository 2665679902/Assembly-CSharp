using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

namespace ProcGen
{
	// Token: 0x020004BC RID: 1212
	[SerializationConfig(MemberSerialization.OptOut)]
	public class River : Path
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060033E8 RID: 13288 RVA: 0x0007163E File Offset: 0x0006F83E
		// (set) Token: 0x060033E9 RID: 13289 RVA: 0x00071646 File Offset: 0x0006F846
		public string element { get; set; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060033EA RID: 13290 RVA: 0x0007164F File Offset: 0x0006F84F
		// (set) Token: 0x060033EB RID: 13291 RVA: 0x00071657 File Offset: 0x0006F857
		public string backgroundElement { get; set; }

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060033EC RID: 13292 RVA: 0x00071660 File Offset: 0x0006F860
		// (set) Token: 0x060033ED RID: 13293 RVA: 0x00071668 File Offset: 0x0006F868
		public float widthCenter { get; set; }

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060033EE RID: 13294 RVA: 0x00071671 File Offset: 0x0006F871
		// (set) Token: 0x060033EF RID: 13295 RVA: 0x00071679 File Offset: 0x0006F879
		public float widthBorder { get; set; }

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060033F0 RID: 13296 RVA: 0x00071682 File Offset: 0x0006F882
		// (set) Token: 0x060033F1 RID: 13297 RVA: 0x0007168A File Offset: 0x0006F88A
		public float temperature { get; set; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060033F2 RID: 13298 RVA: 0x00071693 File Offset: 0x0006F893
		// (set) Token: 0x060033F3 RID: 13299 RVA: 0x0007169B File Offset: 0x0006F89B
		public float maxMass { get; set; }

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060033F4 RID: 13300 RVA: 0x000716A4 File Offset: 0x0006F8A4
		// (set) Token: 0x060033F5 RID: 13301 RVA: 0x000716AC File Offset: 0x0006F8AC
		public float flowIn { get; set; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060033F6 RID: 13302 RVA: 0x000716B5 File Offset: 0x0006F8B5
		// (set) Token: 0x060033F7 RID: 13303 RVA: 0x000716BD File Offset: 0x0006F8BD
		public float flowOut { get; set; }

		// Token: 0x060033F8 RID: 13304 RVA: 0x000716C6 File Offset: 0x0006F8C6
		public River()
		{
			this.pathElements = new List<Segment>();
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x000716DC File Offset: 0x0006F8DC
		public River(Node t0, Node t1, string element = "Water", string backgroundElement = "Granite", float temperature = 373f, float maxMass = 2000f, float flowIn = 1000f, float flowOut = 100f, float widthCenter = 1.5f, float widthBorder = 1.5f)
			: this()
		{
			this.element = element;
			this.backgroundElement = backgroundElement;
			this.AddSection(t0, t1);
			this.temperature = temperature;
			this.maxMass = maxMass;
			this.flowIn = flowIn;
			this.flowOut = flowOut;
			this.widthCenter = widthCenter;
			this.widthBorder = widthBorder;
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x00071738 File Offset: 0x0006F938
		public River(River other, bool copySections = true)
		{
			if (copySections)
			{
				this.pathElements = new List<Segment>(other.pathElements);
			}
			this.element = this.element;
			this.backgroundElement = this.backgroundElement;
			this.temperature = other.temperature;
			this.maxMass = other.maxMass;
			this.flowIn = other.flowIn;
			this.flowOut = other.flowOut;
			this.widthCenter = other.widthCenter;
			this.widthBorder = other.widthBorder;
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x000717BF File Offset: 0x0006F9BF
		public void AddSection(Node t0, Node t1)
		{
			this.pathElements.Add(new Segment(t0.position, t1.position));
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x000717DD File Offset: 0x0006F9DD
		public Vector2 SourcePosition()
		{
			return this.pathElements[0].e0;
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x000717F0 File Offset: 0x0006F9F0
		public Vector2 SinkPosition()
		{
			return this.pathElements[this.pathElements.Count - 1].e1;
		}
	}
}
