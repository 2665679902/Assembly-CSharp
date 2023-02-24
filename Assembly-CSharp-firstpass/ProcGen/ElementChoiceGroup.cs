using System;
using System.Collections.Generic;
using KSerialization.Converters;

namespace ProcGen
{
	// Token: 0x020004BF RID: 1215
	[Serializable]
	public class ElementChoiceGroup<T>
	{
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06003443 RID: 13379 RVA: 0x00071C54 File Offset: 0x0006FE54
		// (set) Token: 0x06003444 RID: 13380 RVA: 0x00071C5C File Offset: 0x0006FE5C
		[StringEnumConverter]
		public Room.Selection selectionMethod { get; private set; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06003445 RID: 13381 RVA: 0x00071C65 File Offset: 0x0006FE65
		// (set) Token: 0x06003446 RID: 13382 RVA: 0x00071C6D File Offset: 0x0006FE6D
		public List<T> choices { get; private set; }

		// Token: 0x06003447 RID: 13383 RVA: 0x00071C76 File Offset: 0x0006FE76
		public ElementChoiceGroup()
		{
			this.choices = new List<T>();
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x00071C89 File Offset: 0x0006FE89
		public ElementChoiceGroup(List<T> choices, Room.Selection selectionMethod)
		{
			this.choices = choices;
			this.selectionMethod = selectionMethod;
		}
	}
}
