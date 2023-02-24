using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x0200044D RID: 1101
[AddComponentMenu("KMonoBehaviour/Workable/AttackableBase")]
public class AttackableBase : Workable, IApproachable
{
	// Token: 0x060017C5 RID: 6085 RVA: 0x0007C944 File Offset: 0x0007AB44
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.attributeConverter = Db.Get().AttributeConverters.AttackDamage;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.BARELY_EVER_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Mining.Id;
		this.skillExperienceMultiplier = SKILLS.BARELY_EVER_EXPERIENCE;
		this.SetupScenePartitioner(null);
		base.Subscribe<AttackableBase>(1088554450, AttackableBase.OnCellChangedDelegate);
		GameUtil.SubscribeToTags<AttackableBase>(this, AttackableBase.OnDeadTagAddedDelegate, true);
		base.Subscribe<AttackableBase>(-1506500077, AttackableBase.OnDefeatedDelegate);
		base.Subscribe<AttackableBase>(-1256572400, AttackableBase.SetupScenePartitionerDelegate);
	}

	// Token: 0x060017C6 RID: 6086 RVA: 0x0007C9E4 File Offset: 0x0007ABE4
	public float GetDamageMultiplier()
	{
		if (this.attributeConverter != null && base.worker != null)
		{
			AttributeConverterInstance converter = base.worker.GetComponent<AttributeConverters>().GetConverter(this.attributeConverter.Id);
			return Mathf.Max(1f + converter.Evaluate(), 0.1f);
		}
		return 1f;
	}

	// Token: 0x060017C7 RID: 6087 RVA: 0x0007CA40 File Offset: 0x0007AC40
	private void SetupScenePartitioner(object data = null)
	{
		Extents extents = new Extents(Grid.PosToXY(base.transform.GetPosition()).x, Grid.PosToXY(base.transform.GetPosition()).y, 1, 1);
		this.scenePartitionerEntry = GameScenePartitioner.Instance.Add(base.gameObject.name, base.GetComponent<FactionAlignment>(), extents, GameScenePartitioner.Instance.attackableEntitiesLayer, null);
	}

	// Token: 0x060017C8 RID: 6088 RVA: 0x0007CAAD File Offset: 0x0007ACAD
	private void OnDefeated(object data = null)
	{
		GameScenePartitioner.Instance.Free(ref this.scenePartitionerEntry);
	}

	// Token: 0x060017C9 RID: 6089 RVA: 0x0007CABF File Offset: 0x0007ACBF
	public override float GetEfficiencyMultiplier(Worker worker)
	{
		return 1f;
	}

	// Token: 0x060017CA RID: 6090 RVA: 0x0007CAC8 File Offset: 0x0007ACC8
	protected override void OnCleanUp()
	{
		base.Unsubscribe<AttackableBase>(1088554450, AttackableBase.OnCellChangedDelegate, false);
		GameUtil.UnsubscribeToTags<AttackableBase>(this, AttackableBase.OnDeadTagAddedDelegate);
		base.Unsubscribe<AttackableBase>(-1506500077, AttackableBase.OnDefeatedDelegate, false);
		base.Unsubscribe<AttackableBase>(-1256572400, AttackableBase.SetupScenePartitionerDelegate, false);
		GameScenePartitioner.Instance.Free(ref this.scenePartitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x04000D2C RID: 3372
	private HandleVector<int>.Handle scenePartitionerEntry;

	// Token: 0x04000D2D RID: 3373
	private static readonly EventSystem.IntraObjectHandler<AttackableBase> OnDeadTagAddedDelegate = GameUtil.CreateHasTagHandler<AttackableBase>(GameTags.Dead, delegate(AttackableBase component, object data)
	{
		component.OnDefeated(data);
	});

	// Token: 0x04000D2E RID: 3374
	private static readonly EventSystem.IntraObjectHandler<AttackableBase> OnDefeatedDelegate = new EventSystem.IntraObjectHandler<AttackableBase>(delegate(AttackableBase component, object data)
	{
		component.OnDefeated(data);
	});

	// Token: 0x04000D2F RID: 3375
	private static readonly EventSystem.IntraObjectHandler<AttackableBase> SetupScenePartitionerDelegate = new EventSystem.IntraObjectHandler<AttackableBase>(delegate(AttackableBase component, object data)
	{
		component.SetupScenePartitioner(data);
	});

	// Token: 0x04000D30 RID: 3376
	private static readonly EventSystem.IntraObjectHandler<AttackableBase> OnCellChangedDelegate = new EventSystem.IntraObjectHandler<AttackableBase>(delegate(AttackableBase component, object data)
	{
		GameScenePartitioner.Instance.UpdatePosition(component.scenePartitionerEntry, Grid.PosToCell(component.gameObject));
	});
}
