using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000797 RID: 1943
[SkipSaveFileSerialization]
public class GlowStick : StateMachineComponent<GlowStick.StatesInstance>
{
	// Token: 0x0600368C RID: 13964 RVA: 0x00130731 File Offset: 0x0012E931
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x020014D3 RID: 5331
	public class StatesInstance : GameStateMachine<GlowStick.States, GlowStick.StatesInstance, GlowStick, object>.GameInstance
	{
		// Token: 0x06008218 RID: 33304 RVA: 0x002E4310 File Offset: 0x002E2510
		public StatesInstance(GlowStick master)
			: base(master)
		{
			this._light2D.Color = Color.green;
			this._light2D.Range = 2f;
			this._light2D.Angle = 0f;
			this._light2D.Direction = LIGHT2D.DEFAULT_DIRECTION;
			this._light2D.Offset = new Vector2(0.05f, 0.5f);
			this._light2D.shape = global::LightShape.Circle;
			this._light2D.Lux = 500;
			this._radiationEmitter.emitRads = 100f;
			this._radiationEmitter.emitType = RadiationEmitter.RadiationEmitterType.Constant;
			this._radiationEmitter.emitRate = 0.5f;
			this._radiationEmitter.emitRadiusX = 3;
			this._radiationEmitter.emitRadiusY = 3;
			this.radiationResistance = new AttributeModifier(Db.Get().Attributes.RadiationResistance.Id, TRAITS.GLOWSTICK_RADIATION_RESISTANCE, DUPLICANTS.TRAITS.GLOWSTICK.NAME, false, false, true);
		}

		// Token: 0x040064FE RID: 25854
		[MyCmpAdd]
		private RadiationEmitter _radiationEmitter;

		// Token: 0x040064FF RID: 25855
		[MyCmpAdd]
		private Light2D _light2D;

		// Token: 0x04006500 RID: 25856
		public AttributeModifier radiationResistance;
	}

	// Token: 0x020014D4 RID: 5332
	public class States : GameStateMachine<GlowStick.States, GlowStick.StatesInstance, GlowStick>
	{
		// Token: 0x06008219 RID: 33305 RVA: 0x002E4410 File Offset: 0x002E2610
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			this.root.ToggleComponent<RadiationEmitter>(false).ToggleComponent<Light2D>(false).ToggleAttributeModifier("Radiation Resistance", (GlowStick.StatesInstance smi) => smi.radiationResistance, null);
		}
	}
}
