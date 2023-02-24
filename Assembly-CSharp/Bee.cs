using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000452 RID: 1106
public class Bee : KMonoBehaviour
{
	// Token: 0x060017E8 RID: 6120 RVA: 0x0007D194 File Offset: 0x0007B394
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<Bee>(-739654666, Bee.OnAttackDelegate);
		base.Subscribe<Bee>(-1283701846, Bee.OnSleepDelegate);
		base.Subscribe<Bee>(-2090444759, Bee.OnWakeUpDelegate);
		base.Subscribe<Bee>(1623392196, Bee.OnDeathDelegate);
		base.Subscribe<Bee>(1890751808, Bee.OnHappyDelegate);
		base.Subscribe<Bee>(-647798969, Bee.OnUnhappyDelegate);
		base.GetComponent<KBatchedAnimController>().SetSymbolVisiblity("tag", false);
		base.GetComponent<KBatchedAnimController>().SetSymbolVisiblity("snapto_tag", false);
		this.StopSleep();
	}

	// Token: 0x060017E9 RID: 6121 RVA: 0x0007D240 File Offset: 0x0007B440
	private void OnDeath(object data)
	{
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		Storage component2 = base.GetComponent<Storage>();
		byte index = Db.Get().Diseases.GetIndex(Db.Get().Diseases.RadiationPoisoning.id);
		component2.AddOre(SimHashes.NuclearWaste, BeeTuning.WASTE_DROPPED_ON_DEATH, component.Temperature, index, BeeTuning.GERMS_DROPPED_ON_DEATH, false, true);
		component2.DropAll(base.transform.position, true, true, default(Vector3), true, null);
	}

	// Token: 0x060017EA RID: 6122 RVA: 0x0007D2BA File Offset: 0x0007B4BA
	private void StartSleep()
	{
		this.RemoveRadiationMod(this.awakeRadiationModKey);
		base.GetComponent<ElementConsumer>().EnableConsumption(true);
	}

	// Token: 0x060017EB RID: 6123 RVA: 0x0007D2D4 File Offset: 0x0007B4D4
	private void StopSleep()
	{
		this.AddRadiationModifier(this.awakeRadiationModKey, this.awakeRadiationMod);
		base.GetComponent<ElementConsumer>().EnableConsumption(false);
	}

	// Token: 0x060017EC RID: 6124 RVA: 0x0007D2F4 File Offset: 0x0007B4F4
	private void AddRadiationModifier(HashedString name, float mod)
	{
		this.radiationModifiers.Add(name, mod);
		this.RefreshRadiationOutput();
	}

	// Token: 0x060017ED RID: 6125 RVA: 0x0007D309 File Offset: 0x0007B509
	private void RemoveRadiationMod(HashedString name)
	{
		this.radiationModifiers.Remove(name);
		this.RefreshRadiationOutput();
	}

	// Token: 0x060017EE RID: 6126 RVA: 0x0007D320 File Offset: 0x0007B520
	public void RefreshRadiationOutput()
	{
		float num = this.radiationOutputAmount;
		foreach (KeyValuePair<HashedString, float> keyValuePair in this.radiationModifiers)
		{
			num *= keyValuePair.Value;
		}
		RadiationEmitter component = base.GetComponent<RadiationEmitter>();
		component.SetEmitting(true);
		component.emitRads = num;
		component.Refresh();
	}

	// Token: 0x060017EF RID: 6127 RVA: 0x0007D398 File Offset: 0x0007B598
	private void OnAttack(object data)
	{
		if ((Tag)data == GameTags.Creatures.Attack)
		{
			base.GetComponent<Health>().Damage(base.GetComponent<Health>().hitPoints);
		}
	}

	// Token: 0x060017F0 RID: 6128 RVA: 0x0007D3C4 File Offset: 0x0007B5C4
	public KPrefabID FindHiveInRoom()
	{
		List<BeeHive.StatesInstance> list = new List<BeeHive.StatesInstance>();
		Room roomOfGameObject = Game.Instance.roomProber.GetRoomOfGameObject(base.gameObject);
		foreach (BeeHive.StatesInstance statesInstance in Components.BeeHives.Items)
		{
			if (Game.Instance.roomProber.GetRoomOfGameObject(statesInstance.gameObject) == roomOfGameObject)
			{
				list.Add(statesInstance);
			}
		}
		int num = int.MaxValue;
		KPrefabID kprefabID = null;
		foreach (BeeHive.StatesInstance statesInstance2 in list)
		{
			int navigationCost = base.gameObject.GetComponent<Navigator>().GetNavigationCost(Grid.PosToCell(statesInstance2.transform.GetLocalPosition()));
			if (navigationCost < num)
			{
				num = navigationCost;
				kprefabID = statesInstance2.GetComponent<KPrefabID>();
			}
		}
		return kprefabID;
	}

	// Token: 0x04000D42 RID: 3394
	public float radiationOutputAmount;

	// Token: 0x04000D43 RID: 3395
	private Dictionary<HashedString, float> radiationModifiers = new Dictionary<HashedString, float>();

	// Token: 0x04000D44 RID: 3396
	private float unhappyRadiationMod = 0.1f;

	// Token: 0x04000D45 RID: 3397
	private float awakeRadiationMod;

	// Token: 0x04000D46 RID: 3398
	private HashedString unhappyRadiationModKey = "UNHAPPY";

	// Token: 0x04000D47 RID: 3399
	private HashedString awakeRadiationModKey = "AWAKE";

	// Token: 0x04000D48 RID: 3400
	private static readonly EventSystem.IntraObjectHandler<Bee> OnAttackDelegate = new EventSystem.IntraObjectHandler<Bee>(delegate(Bee component, object data)
	{
		component.OnAttack(data);
	});

	// Token: 0x04000D49 RID: 3401
	private static readonly EventSystem.IntraObjectHandler<Bee> OnSleepDelegate = new EventSystem.IntraObjectHandler<Bee>(delegate(Bee component, object data)
	{
		component.StartSleep();
	});

	// Token: 0x04000D4A RID: 3402
	private static readonly EventSystem.IntraObjectHandler<Bee> OnWakeUpDelegate = new EventSystem.IntraObjectHandler<Bee>(delegate(Bee component, object data)
	{
		component.StopSleep();
	});

	// Token: 0x04000D4B RID: 3403
	private static readonly EventSystem.IntraObjectHandler<Bee> OnDeathDelegate = new EventSystem.IntraObjectHandler<Bee>(delegate(Bee component, object data)
	{
		component.OnDeath(data);
	});

	// Token: 0x04000D4C RID: 3404
	private static readonly EventSystem.IntraObjectHandler<Bee> OnHappyDelegate = new EventSystem.IntraObjectHandler<Bee>(delegate(Bee component, object data)
	{
		component.RemoveRadiationMod(component.unhappyRadiationModKey);
	});

	// Token: 0x04000D4D RID: 3405
	private static readonly EventSystem.IntraObjectHandler<Bee> OnUnhappyDelegate = new EventSystem.IntraObjectHandler<Bee>(delegate(Bee component, object data)
	{
		component.AddRadiationModifier(component.unhappyRadiationModKey, component.unhappyRadiationMod);
	});
}
