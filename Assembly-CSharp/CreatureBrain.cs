using System;

// Token: 0x0200036D RID: 877
public class CreatureBrain : Brain
{
	// Token: 0x060011F5 RID: 4597 RVA: 0x0005E9E8 File Offset: 0x0005CBE8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Navigator component = base.GetComponent<Navigator>();
		if (component != null)
		{
			component.SetAbilities(new CreaturePathFinderAbilities(component));
		}
	}

	// Token: 0x040009A0 RID: 2464
	public string symbolPrefix;

	// Token: 0x040009A1 RID: 2465
	public Tag species;
}
