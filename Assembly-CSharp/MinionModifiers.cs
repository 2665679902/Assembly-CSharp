using System;
using System.IO;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x02000817 RID: 2071
[SerializationConfig(MemberSerialization.OptIn)]
public class MinionModifiers : Modifiers, ISaveLoadable
{
	// Token: 0x06003C15 RID: 15381 RVA: 0x0014D9F4 File Offset: 0x0014BBF4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (this.addBaseTraits)
		{
			foreach (Klei.AI.Attribute attribute in Db.Get().Attributes.resources)
			{
				if (this.attributes.Get(attribute) == null)
				{
					this.attributes.Add(attribute);
				}
			}
			foreach (Disease disease in Db.Get().Diseases.resources)
			{
				AmountInstance amountInstance = this.AddAmount(disease.amount);
				this.attributes.Add(disease.cureSpeedBase);
				amountInstance.SetValue(0f);
			}
			ChoreConsumer component = base.GetComponent<ChoreConsumer>();
			if (component != null)
			{
				component.AddProvider(GlobalChoreProvider.Instance);
				base.gameObject.AddComponent<QualityOfLifeNeed>();
			}
		}
	}

	// Token: 0x06003C16 RID: 15382 RVA: 0x0014DB0C File Offset: 0x0014BD0C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (base.GetComponent<ChoreConsumer>() != null)
		{
			base.Subscribe<MinionModifiers>(1623392196, MinionModifiers.OnDeathDelegate);
			base.Subscribe<MinionModifiers>(-1506069671, MinionModifiers.OnAttachFollowCamDelegate);
			base.Subscribe<MinionModifiers>(-485480405, MinionModifiers.OnDetachFollowCamDelegate);
			base.Subscribe<MinionModifiers>(-1988963660, MinionModifiers.OnBeginChoreDelegate);
			AmountInstance amountInstance = this.GetAmounts().Get("Calories");
			amountInstance.OnMaxValueReached = (System.Action)Delegate.Combine(amountInstance.OnMaxValueReached, new System.Action(this.OnMaxCaloriesReached));
			Vector3 position = base.transform.GetPosition();
			position.z = Grid.GetLayerZ(Grid.SceneLayer.Move);
			base.transform.SetPosition(position);
			base.gameObject.layer = LayerMask.NameToLayer("Default");
			this.SetupDependentAttribute(Db.Get().Attributes.CarryAmount, Db.Get().AttributeConverters.CarryAmountFromStrength);
		}
	}

	// Token: 0x06003C17 RID: 15383 RVA: 0x0014DC08 File Offset: 0x0014BE08
	private AmountInstance AddAmount(Amount amount)
	{
		AmountInstance amountInstance = new AmountInstance(amount, base.gameObject);
		return this.amounts.Add(amountInstance);
	}

	// Token: 0x06003C18 RID: 15384 RVA: 0x0014DC30 File Offset: 0x0014BE30
	private void SetupDependentAttribute(Klei.AI.Attribute targetAttribute, AttributeConverter attributeConverter)
	{
		Klei.AI.Attribute attribute = attributeConverter.attribute;
		AttributeInstance attributeInstance = attribute.Lookup(this);
		AttributeModifier target_modifier = new AttributeModifier(targetAttribute.Id, attributeConverter.Lookup(this).Evaluate(), attribute.Name, false, false, false);
		this.GetAttributes().Add(target_modifier);
		attributeInstance.OnDirty = (System.Action)Delegate.Combine(attributeInstance.OnDirty, new System.Action(delegate
		{
			target_modifier.SetValue(attributeConverter.Lookup(this).Evaluate());
		}));
	}

	// Token: 0x06003C19 RID: 15385 RVA: 0x0014DCC4 File Offset: 0x0014BEC4
	private void OnDeath(object data)
	{
		global::Debug.LogFormat("OnDeath {0} -- {1} has died!", new object[] { data, base.name });
		foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.Items)
		{
			minionIdentity.GetComponent<Effects>().Add("Mourning", true);
		}
	}

	// Token: 0x06003C1A RID: 15386 RVA: 0x0014DD44 File Offset: 0x0014BF44
	private void OnMaxCaloriesReached()
	{
		base.GetComponent<Effects>().Add("WellFed", true);
	}

	// Token: 0x06003C1B RID: 15387 RVA: 0x0014DD58 File Offset: 0x0014BF58
	private void OnBeginChore(object data)
	{
		Storage component = base.GetComponent<Storage>();
		if (component != null)
		{
			component.DropAll(false, false, default(Vector3), true, null);
		}
	}

	// Token: 0x06003C1C RID: 15388 RVA: 0x0014DD88 File Offset: 0x0014BF88
	public override void OnSerialize(BinaryWriter writer)
	{
		base.OnSerialize(writer);
	}

	// Token: 0x06003C1D RID: 15389 RVA: 0x0014DD91 File Offset: 0x0014BF91
	public override void OnDeserialize(IReader reader)
	{
		base.OnDeserialize(reader);
	}

	// Token: 0x06003C1E RID: 15390 RVA: 0x0014DD9A File Offset: 0x0014BF9A
	private void OnAttachFollowCam(object data)
	{
		base.GetComponent<Effects>().Add("CenterOfAttention", false);
	}

	// Token: 0x06003C1F RID: 15391 RVA: 0x0014DDAE File Offset: 0x0014BFAE
	private void OnDetachFollowCam(object data)
	{
		base.GetComponent<Effects>().Remove("CenterOfAttention");
	}

	// Token: 0x0400271A RID: 10010
	public bool addBaseTraits = true;

	// Token: 0x0400271B RID: 10011
	private static readonly EventSystem.IntraObjectHandler<MinionModifiers> OnDeathDelegate = new EventSystem.IntraObjectHandler<MinionModifiers>(delegate(MinionModifiers component, object data)
	{
		component.OnDeath(data);
	});

	// Token: 0x0400271C RID: 10012
	private static readonly EventSystem.IntraObjectHandler<MinionModifiers> OnAttachFollowCamDelegate = new EventSystem.IntraObjectHandler<MinionModifiers>(delegate(MinionModifiers component, object data)
	{
		component.OnAttachFollowCam(data);
	});

	// Token: 0x0400271D RID: 10013
	private static readonly EventSystem.IntraObjectHandler<MinionModifiers> OnDetachFollowCamDelegate = new EventSystem.IntraObjectHandler<MinionModifiers>(delegate(MinionModifiers component, object data)
	{
		component.OnDetachFollowCam(data);
	});

	// Token: 0x0400271E RID: 10014
	private static readonly EventSystem.IntraObjectHandler<MinionModifiers> OnBeginChoreDelegate = new EventSystem.IntraObjectHandler<MinionModifiers>(delegate(MinionModifiers component, object data)
	{
		component.OnBeginChore(data);
	});
}
