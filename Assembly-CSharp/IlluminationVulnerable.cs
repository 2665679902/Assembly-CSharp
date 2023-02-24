using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020006DB RID: 1755
[SkipSaveFileSerialization]
public class IlluminationVulnerable : StateMachineComponent<IlluminationVulnerable.StatesInstance>, IGameObjectEffectDescriptor, IWiltCause
{
	// Token: 0x1700035D RID: 861
	// (get) Token: 0x06002FC2 RID: 12226 RVA: 0x000FC3AA File Offset: 0x000FA5AA
	public int LightIntensityThreshold
	{
		get
		{
			if (this.minLuxAttributeInstance != null)
			{
				return Mathf.RoundToInt(this.minLuxAttributeInstance.GetTotalValue());
			}
			return Mathf.RoundToInt(base.GetComponent<Modifiers>().GetPreModifiedAttributeValue(Db.Get().PlantAttributes.MinLightLux));
		}
	}

	// Token: 0x1700035E RID: 862
	// (get) Token: 0x06002FC3 RID: 12227 RVA: 0x000FC3E4 File Offset: 0x000FA5E4
	private OccupyArea occupyArea
	{
		get
		{
			if (this._occupyArea == null)
			{
				this._occupyArea = base.GetComponent<OccupyArea>();
			}
			return this._occupyArea;
		}
	}

	// Token: 0x06002FC4 RID: 12228 RVA: 0x000FC408 File Offset: 0x000FA608
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.gameObject.GetAmounts().Add(new AmountInstance(Db.Get().Amounts.Illumination, base.gameObject));
		this.minLuxAttributeInstance = base.gameObject.GetAttributes().Add(Db.Get().PlantAttributes.MinLightLux);
	}

	// Token: 0x06002FC5 RID: 12229 RVA: 0x000FC46B File Offset: 0x000FA66B
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06002FC6 RID: 12230 RVA: 0x000FC47E File Offset: 0x000FA67E
	public void SetPrefersDarkness(bool prefersDarkness = false)
	{
		this.prefersDarkness = prefersDarkness;
	}

	// Token: 0x06002FC7 RID: 12231 RVA: 0x000FC487 File Offset: 0x000FA687
	protected override void OnCleanUp()
	{
		this.handle.ClearScheduler();
		base.OnCleanUp();
	}

	// Token: 0x06002FC8 RID: 12232 RVA: 0x000FC49A File Offset: 0x000FA69A
	public bool IsCellSafe(int cell)
	{
		if (!Grid.IsValidCell(cell))
		{
			return false;
		}
		if (this.prefersDarkness)
		{
			return Grid.LightIntensity[cell] == 0;
		}
		return Grid.LightIntensity[cell] > this.LightIntensityThreshold;
	}

	// Token: 0x1700035F RID: 863
	// (get) Token: 0x06002FC9 RID: 12233 RVA: 0x000FC4D0 File Offset: 0x000FA6D0
	WiltCondition.Condition[] IWiltCause.Conditions
	{
		get
		{
			return new WiltCondition.Condition[]
			{
				WiltCondition.Condition.Darkness,
				WiltCondition.Condition.IlluminationComfort
			};
		}
	}

	// Token: 0x17000360 RID: 864
	// (get) Token: 0x06002FCA RID: 12234 RVA: 0x000FC4E0 File Offset: 0x000FA6E0
	public string WiltStateString
	{
		get
		{
			if (base.smi.IsInsideState(base.smi.sm.too_bright))
			{
				return Db.Get().CreatureStatusItems.Crop_Too_Bright.GetName(this);
			}
			if (base.smi.IsInsideState(base.smi.sm.too_dark))
			{
				return Db.Get().CreatureStatusItems.Crop_Too_Dark.GetName(this);
			}
			return "";
		}
	}

	// Token: 0x06002FCB RID: 12235 RVA: 0x000FC558 File Offset: 0x000FA758
	public bool IsComfortable()
	{
		return base.smi.IsInsideState(base.smi.sm.comfortable);
	}

	// Token: 0x06002FCC RID: 12236 RVA: 0x000FC578 File Offset: 0x000FA778
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		if (this.prefersDarkness)
		{
			return new List<Descriptor>
			{
				new Descriptor(UI.GAMEOBJECTEFFECTS.REQUIRES_DARKNESS, UI.GAMEOBJECTEFFECTS.TOOLTIPS.REQUIRES_DARKNESS, Descriptor.DescriptorType.Requirement, false)
			};
		}
		return new List<Descriptor>
		{
			new Descriptor(UI.GAMEOBJECTEFFECTS.REQUIRES_LIGHT.Replace("{Lux}", GameUtil.GetFormattedLux(this.LightIntensityThreshold)), UI.GAMEOBJECTEFFECTS.TOOLTIPS.REQUIRES_LIGHT.Replace("{Lux}", GameUtil.GetFormattedLux(this.LightIntensityThreshold)), Descriptor.DescriptorType.Requirement, false)
		};
	}

	// Token: 0x04001CC5 RID: 7365
	private OccupyArea _occupyArea;

	// Token: 0x04001CC6 RID: 7366
	private SchedulerHandle handle;

	// Token: 0x04001CC7 RID: 7367
	public bool prefersDarkness;

	// Token: 0x04001CC8 RID: 7368
	private AttributeInstance minLuxAttributeInstance;

	// Token: 0x020013DE RID: 5086
	public class StatesInstance : GameStateMachine<IlluminationVulnerable.States, IlluminationVulnerable.StatesInstance, IlluminationVulnerable, object>.GameInstance
	{
		// Token: 0x06007F56 RID: 32598 RVA: 0x002DC20A File Offset: 0x002DA40A
		public StatesInstance(IlluminationVulnerable master)
			: base(master)
		{
		}

		// Token: 0x040061D8 RID: 25048
		public bool hasMaturity;
	}

	// Token: 0x020013DF RID: 5087
	public class States : GameStateMachine<IlluminationVulnerable.States, IlluminationVulnerable.StatesInstance, IlluminationVulnerable>
	{
		// Token: 0x06007F57 RID: 32599 RVA: 0x002DC214 File Offset: 0x002DA414
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.comfortable;
			this.root.Update("Illumination", delegate(IlluminationVulnerable.StatesInstance smi, float dt)
			{
				int num = Grid.PosToCell(smi.master.gameObject);
				if (Grid.IsValidCell(num))
				{
					smi.master.GetAmounts().Get(Db.Get().Amounts.Illumination).SetValue((float)Grid.LightCount[num]);
					return;
				}
				smi.master.GetAmounts().Get(Db.Get().Amounts.Illumination).SetValue(0f);
			}, UpdateRate.SIM_1000ms, false);
			this.comfortable.Update("Illumination.Comfortable", delegate(IlluminationVulnerable.StatesInstance smi, float dt)
			{
				int num2 = Grid.PosToCell(smi.master.gameObject);
				if (!smi.master.IsCellSafe(num2))
				{
					GameStateMachine<IlluminationVulnerable.States, IlluminationVulnerable.StatesInstance, IlluminationVulnerable, object>.State state = (smi.master.prefersDarkness ? this.too_bright : this.too_dark);
					smi.GoTo(state);
				}
			}, UpdateRate.SIM_1000ms, false).Enter(delegate(IlluminationVulnerable.StatesInstance smi)
			{
				smi.master.Trigger(1113102781, null);
			});
			this.too_dark.TriggerOnEnter(GameHashes.IlluminationDiscomfort, null).Update("Illumination.too_dark", delegate(IlluminationVulnerable.StatesInstance smi, float dt)
			{
				int num3 = Grid.PosToCell(smi.master.gameObject);
				if (smi.master.IsCellSafe(num3))
				{
					smi.GoTo(this.comfortable);
				}
			}, UpdateRate.SIM_1000ms, false);
			this.too_bright.TriggerOnEnter(GameHashes.IlluminationDiscomfort, null).Update("Illumination.too_bright", delegate(IlluminationVulnerable.StatesInstance smi, float dt)
			{
				int num4 = Grid.PosToCell(smi.master.gameObject);
				if (smi.master.IsCellSafe(num4))
				{
					smi.GoTo(this.comfortable);
				}
			}, UpdateRate.SIM_1000ms, false);
		}

		// Token: 0x040061D9 RID: 25049
		public StateMachine<IlluminationVulnerable.States, IlluminationVulnerable.StatesInstance, IlluminationVulnerable, object>.BoolParameter illuminated;

		// Token: 0x040061DA RID: 25050
		public GameStateMachine<IlluminationVulnerable.States, IlluminationVulnerable.StatesInstance, IlluminationVulnerable, object>.State comfortable;

		// Token: 0x040061DB RID: 25051
		public GameStateMachine<IlluminationVulnerable.States, IlluminationVulnerable.StatesInstance, IlluminationVulnerable, object>.State too_dark;

		// Token: 0x040061DC RID: 25052
		public GameStateMachine<IlluminationVulnerable.States, IlluminationVulnerable.StatesInstance, IlluminationVulnerable, object>.State too_bright;
	}
}
