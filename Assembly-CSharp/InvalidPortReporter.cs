using System;

// Token: 0x020006DD RID: 1757
public class InvalidPortReporter : KMonoBehaviour
{
	// Token: 0x06002FD9 RID: 12249 RVA: 0x000FCB55 File Offset: 0x000FAD55
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.OnTagsChanged(null);
		base.Subscribe<InvalidPortReporter>(-1582839653, InvalidPortReporter.OnTagsChangedDelegate);
	}

	// Token: 0x06002FDA RID: 12250 RVA: 0x000FCB75 File Offset: 0x000FAD75
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06002FDB RID: 12251 RVA: 0x000FCB80 File Offset: 0x000FAD80
	private void OnTagsChanged(object data)
	{
		bool flag = base.gameObject.HasTag(GameTags.HasInvalidPorts);
		Operational component = base.GetComponent<Operational>();
		if (component != null)
		{
			component.SetFlag(InvalidPortReporter.portsNotOverlapping, !flag);
		}
		KSelectable component2 = base.GetComponent<KSelectable>();
		if (component2 != null)
		{
			component2.ToggleStatusItem(Db.Get().BuildingStatusItems.InvalidPortOverlap, flag, base.gameObject);
		}
	}

	// Token: 0x04001CD3 RID: 7379
	public static readonly Operational.Flag portsNotOverlapping = new Operational.Flag("ports_not_overlapping", Operational.Flag.Type.Functional);

	// Token: 0x04001CD4 RID: 7380
	private static readonly EventSystem.IntraObjectHandler<InvalidPortReporter> OnTagsChangedDelegate = new EventSystem.IntraObjectHandler<InvalidPortReporter>(delegate(InvalidPortReporter component, object data)
	{
		component.OnTagsChanged(data);
	});
}
