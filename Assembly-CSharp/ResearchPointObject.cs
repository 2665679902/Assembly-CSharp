using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020008DA RID: 2266
[AddComponentMenu("KMonoBehaviour/scripts/ResearchPointObject")]
public class ResearchPointObject : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x06004145 RID: 16709 RVA: 0x0016E068 File Offset: 0x0016C268
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Research.Instance.AddResearchPoints(this.TypeID, 1f);
		ResearchType researchType = Research.Instance.GetResearchType(this.TypeID);
		PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Research, researchType.name, base.transform, 1.5f, false);
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x06004146 RID: 16710 RVA: 0x0016E0D4 File Offset: 0x0016C2D4
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		ResearchType researchType = Research.Instance.GetResearchType(this.TypeID);
		list.Add(new Descriptor(string.Format(UI.UISIDESCREENS.FABRICATORSIDESCREEN.EFFECTS.RESEARCHPOINT, researchType.name), string.Format(UI.UISIDESCREENS.FABRICATORSIDESCREEN.EFFECTS.RESEARCHPOINT, researchType.description), Descriptor.DescriptorType.Effect, false));
		return list;
	}

	// Token: 0x04002B8E RID: 11150
	public string TypeID = "";
}
