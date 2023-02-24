using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000851 RID: 2129
public class TubeTraveller : GameStateMachine<TubeTraveller, TubeTraveller.Instance>
{
	// Token: 0x06003D38 RID: 15672 RVA: 0x00156478 File Offset: 0x00154678
	public void InitModifiers()
	{
		this.modifiers.Add(new AttributeModifier(Db.Get().Attributes.Insulation.Id, (float)TUNING.EQUIPMENT.SUITS.ATMOSUIT_INSULATION, STRINGS.BUILDINGS.PREFABS.TRAVELTUBE.NAME, false, false, true));
		this.modifiers.Add(new AttributeModifier(Db.Get().Attributes.ThermalConductivityBarrier.Id, TUNING.EQUIPMENT.SUITS.ATMOSUIT_THERMAL_CONDUCTIVITY_BARRIER, STRINGS.BUILDINGS.PREFABS.TRAVELTUBE.NAME, false, false, true));
		this.modifiers.Add(new AttributeModifier(Db.Get().Amounts.Bladder.deltaAttribute.Id, TUNING.EQUIPMENT.SUITS.ATMOSUIT_BLADDER, STRINGS.BUILDINGS.PREFABS.TRAVELTUBE.NAME, false, false, true));
		this.modifiers.Add(new AttributeModifier(Db.Get().Attributes.ScaldingThreshold.Id, (float)TUNING.EQUIPMENT.SUITS.ATMOSUIT_SCALDING, STRINGS.BUILDINGS.PREFABS.TRAVELTUBE.NAME, false, false, true));
		this.immunities.Add(Db.Get().effects.Get("SoakingWet"));
		this.immunities.Add(Db.Get().effects.Get("WetFeet"));
		this.immunities.Add(Db.Get().effects.Get("PoppedEarDrums"));
		this.immunities.Add(Db.Get().effects.Get("MinorIrritation"));
		this.immunities.Add(Db.Get().effects.Get("MajorIrritation"));
	}

	// Token: 0x06003D39 RID: 15673 RVA: 0x001565FF File Offset: 0x001547FF
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		this.InitModifiers();
		default_state = this.root;
		this.root.DoNothing();
	}

	// Token: 0x06003D3A RID: 15674 RVA: 0x0015661B File Offset: 0x0015481B
	public void OnSetOxygenBreather(OxygenBreather oxygen_breather)
	{
	}

	// Token: 0x06003D3B RID: 15675 RVA: 0x0015661D File Offset: 0x0015481D
	public void OnClearOxygenBreather(OxygenBreather oxygen_breather)
	{
	}

	// Token: 0x06003D3C RID: 15676 RVA: 0x0015661F File Offset: 0x0015481F
	public bool ConsumeGas(OxygenBreather oxygen_breather, float amount)
	{
		return false;
	}

	// Token: 0x06003D3D RID: 15677 RVA: 0x00156622 File Offset: 0x00154822
	public bool ShouldEmitCO2()
	{
		return false;
	}

	// Token: 0x06003D3E RID: 15678 RVA: 0x00156625 File Offset: 0x00154825
	public bool ShouldStoreCO2()
	{
		return false;
	}

	// Token: 0x0400281B RID: 10267
	private List<Effect> immunities = new List<Effect>();

	// Token: 0x0400281C RID: 10268
	private List<AttributeModifier> modifiers = new List<AttributeModifier>();

	// Token: 0x02001602 RID: 5634
	public new class Instance : GameStateMachine<TubeTraveller, TubeTraveller.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06008659 RID: 34393 RVA: 0x002EEBAE File Offset: 0x002ECDAE
		public int prefabInstanceID
		{
			get
			{
				return base.GetComponent<Navigator>().gameObject.GetComponent<KPrefabID>().InstanceID;
			}
		}

		// Token: 0x0600865A RID: 34394 RVA: 0x002EEBC5 File Offset: 0x002ECDC5
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x0600865B RID: 34395 RVA: 0x002EEBD9 File Offset: 0x002ECDD9
		public void OnPathAdvanced(object data)
		{
			this.UnreserveEntrances();
			this.ReserveEntrances();
		}

		// Token: 0x0600865C RID: 34396 RVA: 0x002EEBE8 File Offset: 0x002ECDE8
		public void ReserveEntrances()
		{
			PathFinder.Path path = base.GetComponent<Navigator>().path;
			if (path.nodes == null)
			{
				return;
			}
			for (int i = 0; i < path.nodes.Count - 1; i++)
			{
				if (path.nodes[i].navType == NavType.Floor && path.nodes[i + 1].navType == NavType.Tube)
				{
					int cell = path.nodes[i].cell;
					if (Grid.HasUsableTubeEntrance(cell, this.prefabInstanceID))
					{
						GameObject gameObject = Grid.Objects[cell, 1];
						if (gameObject)
						{
							TravelTubeEntrance component = gameObject.GetComponent<TravelTubeEntrance>();
							if (component)
							{
								component.Reserve(this, this.prefabInstanceID);
								this.reservations.Add(component);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600865D RID: 34397 RVA: 0x002EECB4 File Offset: 0x002ECEB4
		public void UnreserveEntrances()
		{
			foreach (TravelTubeEntrance travelTubeEntrance in this.reservations)
			{
				if (!(travelTubeEntrance == null))
				{
					travelTubeEntrance.Unreserve(this, this.prefabInstanceID);
				}
			}
			this.reservations.Clear();
		}

		// Token: 0x0600865E RID: 34398 RVA: 0x002EED24 File Offset: 0x002ECF24
		public void ApplyEnteringTubeEffects()
		{
			Effects component = base.GetComponent<Effects>();
			Attributes attributes = base.gameObject.GetAttributes();
			base.gameObject.AddTag(GameTags.InTransitTube);
			string name = GameTags.InTransitTube.Name;
			foreach (Effect effect in base.sm.immunities)
			{
				component.AddImmunity(effect, name, true);
			}
			foreach (AttributeModifier attributeModifier in base.sm.modifiers)
			{
				attributes.Add(attributeModifier);
			}
			CreatureSimTemperatureTransfer component2 = base.gameObject.GetComponent<CreatureSimTemperatureTransfer>();
			if (component2 != null)
			{
				component2.RefreshRegistration();
			}
		}

		// Token: 0x0600865F RID: 34399 RVA: 0x002EEE18 File Offset: 0x002ED018
		public void ClearAllEffects()
		{
			Effects component = base.GetComponent<Effects>();
			Attributes attributes = base.gameObject.GetAttributes();
			base.gameObject.RemoveTag(GameTags.InTransitTube);
			string name = GameTags.InTransitTube.Name;
			foreach (Effect effect in base.sm.immunities)
			{
				component.RemoveImmunity(effect, name);
			}
			foreach (AttributeModifier attributeModifier in base.sm.modifiers)
			{
				attributes.Remove(attributeModifier);
			}
			CreatureSimTemperatureTransfer component2 = base.gameObject.GetComponent<CreatureSimTemperatureTransfer>();
			if (component2 != null)
			{
				component2.RefreshRegistration();
			}
		}

		// Token: 0x06008660 RID: 34400 RVA: 0x002EEF0C File Offset: 0x002ED10C
		public void OnTubeTransition(bool nowInTube)
		{
			if (nowInTube != this.inTube)
			{
				this.inTube = nowInTube;
				base.GetComponent<Effects>();
				base.gameObject.GetAttributes();
				if (nowInTube)
				{
					this.ApplyEnteringTubeEffects();
					return;
				}
				this.ClearAllEffects();
			}
		}

		// Token: 0x040068AA RID: 26794
		private List<TravelTubeEntrance> reservations = new List<TravelTubeEntrance>();

		// Token: 0x040068AB RID: 26795
		public bool inTube;
	}
}
