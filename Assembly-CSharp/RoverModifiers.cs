using System;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x020006BA RID: 1722
[SerializationConfig(MemberSerialization.OptIn)]
public class RoverModifiers : Modifiers, ISaveLoadable
{
	// Token: 0x06002EDE RID: 11998 RVA: 0x000F8200 File Offset: 0x000F6400
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.attributes.Add(Db.Get().Attributes.Construction);
		this.attributes.Add(Db.Get().Attributes.Digging);
		this.attributes.Add(Db.Get().Attributes.Strength);
	}

	// Token: 0x06002EDF RID: 11999 RVA: 0x000F8264 File Offset: 0x000F6464
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (base.GetComponent<ChoreConsumer>() != null)
		{
			base.Subscribe<RoverModifiers>(-1988963660, RoverModifiers.OnBeginChoreDelegate);
			Vector3 position = base.transform.GetPosition();
			position.z = Grid.GetLayerZ(Grid.SceneLayer.Move);
			base.transform.SetPosition(position);
			base.gameObject.layer = LayerMask.NameToLayer("Default");
			this.SetupDependentAttribute(Db.Get().Attributes.CarryAmount, Db.Get().AttributeConverters.CarryAmountFromStrength);
		}
	}

	// Token: 0x06002EE0 RID: 12000 RVA: 0x000F82F8 File Offset: 0x000F64F8
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

	// Token: 0x06002EE1 RID: 12001 RVA: 0x000F838C File Offset: 0x000F658C
	private void OnBeginChore(object data)
	{
		Storage component = base.GetComponent<Storage>();
		if (component != null)
		{
			component.DropAll(false, false, default(Vector3), true, null);
		}
	}

	// Token: 0x04001C38 RID: 7224
	private static readonly EventSystem.IntraObjectHandler<RoverModifiers> OnBeginChoreDelegate = new EventSystem.IntraObjectHandler<RoverModifiers>(delegate(RoverModifiers component, object data)
	{
		component.OnBeginChore(data);
	});
}
