using System;
using Klei.AI;
using UnityEngine;

// Token: 0x0200076A RID: 1898
public class AttributeModifierExpectation : Expectation
{
	// Token: 0x06003420 RID: 13344 RVA: 0x00118570 File Offset: 0x00116770
	public AttributeModifierExpectation(string id, string name, string description, AttributeModifier modifier, Sprite icon)
		: base(id, name, description, delegate(MinionResume resume)
		{
			resume.GetAttributes().Get(modifier.AttributeId).Add(modifier);
		}, delegate(MinionResume resume)
		{
			resume.GetAttributes().Get(modifier.AttributeId).Remove(modifier);
		})
	{
		this.modifier = modifier;
		this.icon = icon;
	}

	// Token: 0x04002036 RID: 8246
	public AttributeModifier modifier;

	// Token: 0x04002037 RID: 8247
	public Sprite icon;
}
