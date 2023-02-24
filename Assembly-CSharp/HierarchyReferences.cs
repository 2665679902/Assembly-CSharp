using System;
using UnityEngine;

// Token: 0x02000AB4 RID: 2740
[AddComponentMenu("KMonoBehaviour/scripts/HierarchyReferences")]
public class HierarchyReferences : KMonoBehaviour
{
	// Token: 0x060053E8 RID: 21480 RVA: 0x001E8130 File Offset: 0x001E6330
	public bool HasReference(string name)
	{
		ElementReference[] array = this.references;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Name == name)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060053E9 RID: 21481 RVA: 0x001E816C File Offset: 0x001E636C
	public SpecifiedType GetReference<SpecifiedType>(string name) where SpecifiedType : Component
	{
		foreach (ElementReference elementReference in this.references)
		{
			if (elementReference.Name == name)
			{
				if (elementReference.behaviour is SpecifiedType)
				{
					return (SpecifiedType)((object)elementReference.behaviour);
				}
				global::Debug.LogError(string.Format("Behavior is not specified type", Array.Empty<object>()));
			}
		}
		global::Debug.LogError(string.Format("Could not find UI reference '{0}' or convert to specified type)", name));
		return default(SpecifiedType);
	}

	// Token: 0x060053EA RID: 21482 RVA: 0x001E81EC File Offset: 0x001E63EC
	public Component GetReference(string name)
	{
		foreach (ElementReference elementReference in this.references)
		{
			if (elementReference.Name == name)
			{
				return elementReference.behaviour;
			}
		}
		global::Debug.LogWarning("Couldn't find reference to object named {0} Make sure the name matches the field in the inspector.");
		return null;
	}

	// Token: 0x0400390B RID: 14603
	public ElementReference[] references;
}
