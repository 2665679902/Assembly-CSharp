using System;
using System.Collections.Generic;
using Klei.AI;

// Token: 0x02000A3A RID: 2618
public interface IAttributeFormatter
{
	// Token: 0x170005CF RID: 1487
	// (get) Token: 0x06004F7D RID: 20349
	// (set) Token: 0x06004F7E RID: 20350
	GameUtil.TimeSlice DeltaTimeSlice { get; set; }

	// Token: 0x06004F7F RID: 20351
	string GetFormattedAttribute(AttributeInstance instance);

	// Token: 0x06004F80 RID: 20352
	string GetFormattedModifier(AttributeModifier modifier);

	// Token: 0x06004F81 RID: 20353
	string GetFormattedValue(float value, GameUtil.TimeSlice timeSlice);

	// Token: 0x06004F82 RID: 20354
	string GetTooltip(Klei.AI.Attribute master, AttributeInstance instance);

	// Token: 0x06004F83 RID: 20355
	string GetTooltip(Klei.AI.Attribute master, List<AttributeModifier> modifiers, AttributeConverters converters);
}
