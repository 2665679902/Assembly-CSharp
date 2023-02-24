using System;
using UnityEngine;

// Token: 0x02000BA8 RID: 2984
public interface IConfigurableConsumerOption
{
	// Token: 0x06005DE2 RID: 24034
	Tag GetID();

	// Token: 0x06005DE3 RID: 24035
	string GetName();

	// Token: 0x06005DE4 RID: 24036
	string GetDetailedDescription();

	// Token: 0x06005DE5 RID: 24037
	string GetDescription();

	// Token: 0x06005DE6 RID: 24038
	Sprite GetIcon();

	// Token: 0x06005DE7 RID: 24039
	IConfigurableConsumerIngredient[] GetIngredients();
}
