using System;
using System.Collections.Generic;
using LibNoiseDotNet.Graphics.Tools.Noise.Modifier;
using UnityEngine;

namespace ProcGen.Noise
{
	// Token: 0x020004E9 RID: 1257
	[Serializable]
	public class ControlPointList : NoiseBase
	{
		// Token: 0x06003639 RID: 13881 RVA: 0x00077045 File Offset: 0x00075245
		public override Type GetObjectType()
		{
			return typeof(ControlPointList);
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600363A RID: 13882 RVA: 0x00077051 File Offset: 0x00075251
		// (set) Token: 0x0600363B RID: 13883 RVA: 0x00077059 File Offset: 0x00075259
		[SerializeField]
		public List<ControlPointList.Control> points { get; set; }

		// Token: 0x0600363C RID: 13884 RVA: 0x00077062 File Offset: 0x00075262
		public ControlPointList()
		{
			this.points = new List<ControlPointList.Control>();
		}

		// Token: 0x0600363D RID: 13885 RVA: 0x00077078 File Offset: 0x00075278
		public List<ControlPoint> GetControls()
		{
			List<ControlPoint> list = new List<ControlPoint>();
			for (int i = 0; i < this.points.Count; i++)
			{
				list.Add(new ControlPoint(this.points[i].input, this.points[i].output));
			}
			return list;
		}

		// Token: 0x02000B12 RID: 2834
		public class Control
		{
			// Token: 0x17000EF7 RID: 3831
			// (get) Token: 0x06005853 RID: 22611 RVA: 0x000A45F8 File Offset: 0x000A27F8
			// (set) Token: 0x06005854 RID: 22612 RVA: 0x000A4600 File Offset: 0x000A2800
			public float input { get; set; }

			// Token: 0x17000EF8 RID: 3832
			// (get) Token: 0x06005855 RID: 22613 RVA: 0x000A4609 File Offset: 0x000A2809
			// (set) Token: 0x06005856 RID: 22614 RVA: 0x000A4611 File Offset: 0x000A2811
			public float output { get; set; }

			// Token: 0x06005857 RID: 22615 RVA: 0x000A461A File Offset: 0x000A281A
			public Control()
			{
				this.input = 0f;
				this.output = 0f;
			}

			// Token: 0x06005858 RID: 22616 RVA: 0x000A4638 File Offset: 0x000A2838
			public Control(float i, float o)
			{
				this.input = i;
				this.output = o;
			}
		}
	}
}
