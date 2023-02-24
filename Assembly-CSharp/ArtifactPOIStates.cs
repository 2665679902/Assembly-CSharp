using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000936 RID: 2358
[AddComponentMenu("KMonoBehaviour/scripts/ArtifactPOIStates")]
public class ArtifactPOIStates : GameStateMachine<ArtifactPOIStates, ArtifactPOIStates.Instance, IStateMachineTarget, ArtifactPOIStates.Def>
{
	// Token: 0x06004509 RID: 17673 RVA: 0x00185540 File Offset: 0x00183740
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.idle;
		this.root.Enter(delegate(ArtifactPOIStates.Instance smi)
		{
			if (smi.configuration == null || smi.configuration.typeId == HashedString.Invalid)
			{
				smi.configuration = smi.GetComponent<ArtifactPOIConfigurator>().MakeConfiguration();
				smi.PickNewArtifactToHarvest();
				smi.poiCharge = 1f;
			}
		});
		this.idle.ParamTransition<float>(this.poiCharge, this.recharging, (ArtifactPOIStates.Instance smi, float f) => f < 1f);
		this.recharging.ParamTransition<float>(this.poiCharge, this.idle, (ArtifactPOIStates.Instance smi, float f) => f >= 1f).EventHandler(GameHashes.NewDay, (ArtifactPOIStates.Instance smi) => GameClock.Instance, delegate(ArtifactPOIStates.Instance smi)
		{
			smi.RechargePOI(600f);
		});
	}

	// Token: 0x04002E10 RID: 11792
	public GameStateMachine<ArtifactPOIStates, ArtifactPOIStates.Instance, IStateMachineTarget, ArtifactPOIStates.Def>.State idle;

	// Token: 0x04002E11 RID: 11793
	public GameStateMachine<ArtifactPOIStates, ArtifactPOIStates.Instance, IStateMachineTarget, ArtifactPOIStates.Def>.State recharging;

	// Token: 0x04002E12 RID: 11794
	public StateMachine<ArtifactPOIStates, ArtifactPOIStates.Instance, IStateMachineTarget, ArtifactPOIStates.Def>.FloatParameter poiCharge = new StateMachine<ArtifactPOIStates, ArtifactPOIStates.Instance, IStateMachineTarget, ArtifactPOIStates.Def>.FloatParameter(1f);

	// Token: 0x02001720 RID: 5920
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001721 RID: 5921
	public new class Instance : GameStateMachine<ArtifactPOIStates, ArtifactPOIStates.Instance, IStateMachineTarget, ArtifactPOIStates.Def>.GameInstance, IGameObjectEffectDescriptor
	{
		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060089A8 RID: 35240 RVA: 0x002F97EF File Offset: 0x002F79EF
		// (set) Token: 0x060089A9 RID: 35241 RVA: 0x002F97F7 File Offset: 0x002F79F7
		public float poiCharge
		{
			get
			{
				return this._poiCharge;
			}
			set
			{
				this._poiCharge = value;
				base.smi.sm.poiCharge.Set(value, base.smi, false);
			}
		}

		// Token: 0x060089AA RID: 35242 RVA: 0x002F981E File Offset: 0x002F7A1E
		public Instance(IStateMachineTarget target, ArtifactPOIStates.Def def)
			: base(target, def)
		{
		}

		// Token: 0x060089AB RID: 35243 RVA: 0x002F9828 File Offset: 0x002F7A28
		public void PickNewArtifactToHarvest()
		{
			if (this.numHarvests <= 0 && !string.IsNullOrEmpty(this.configuration.GetArtifactID()))
			{
				this.artifactToHarvest = this.configuration.GetArtifactID();
				ArtifactSelector.Instance.ReserveArtifactID(this.artifactToHarvest, ArtifactType.Any);
				return;
			}
			this.artifactToHarvest = ArtifactSelector.Instance.GetUniqueArtifactID(ArtifactType.Space);
		}

		// Token: 0x060089AC RID: 35244 RVA: 0x002F9884 File Offset: 0x002F7A84
		public string GetArtifactToHarvest()
		{
			if (this.CanHarvestArtifact())
			{
				if (string.IsNullOrEmpty(this.artifactToHarvest))
				{
					this.PickNewArtifactToHarvest();
				}
				return this.artifactToHarvest;
			}
			return null;
		}

		// Token: 0x060089AD RID: 35245 RVA: 0x002F98A9 File Offset: 0x002F7AA9
		public void HarvestArtifact()
		{
			if (this.CanHarvestArtifact())
			{
				this.numHarvests++;
				this.poiCharge = 0f;
				this.artifactToHarvest = null;
				this.PickNewArtifactToHarvest();
			}
		}

		// Token: 0x060089AE RID: 35246 RVA: 0x002F98DC File Offset: 0x002F7ADC
		public void RechargePOI(float dt)
		{
			float num = dt / this.configuration.GetRechargeTime();
			this.DeltaPOICharge(num);
		}

		// Token: 0x060089AF RID: 35247 RVA: 0x002F98FE File Offset: 0x002F7AFE
		public float RechargeTimeRemaining()
		{
			return (float)Mathf.CeilToInt((this.configuration.GetRechargeTime() - this.configuration.GetRechargeTime() * this.poiCharge) / 600f) * 600f;
		}

		// Token: 0x060089B0 RID: 35248 RVA: 0x002F9930 File Offset: 0x002F7B30
		public void DeltaPOICharge(float delta)
		{
			this.poiCharge += delta;
			this.poiCharge = Mathf.Min(1f, this.poiCharge);
		}

		// Token: 0x060089B1 RID: 35249 RVA: 0x002F9956 File Offset: 0x002F7B56
		public bool CanHarvestArtifact()
		{
			return this.poiCharge >= 1f;
		}

		// Token: 0x060089B2 RID: 35250 RVA: 0x002F9968 File Offset: 0x002F7B68
		public List<Descriptor> GetDescriptors(GameObject go)
		{
			return new List<Descriptor>();
		}

		// Token: 0x04006C10 RID: 27664
		[Serialize]
		public ArtifactPOIConfigurator.ArtifactPOIInstanceConfiguration configuration;

		// Token: 0x04006C11 RID: 27665
		[Serialize]
		private float _poiCharge;

		// Token: 0x04006C12 RID: 27666
		[Serialize]
		public string artifactToHarvest;

		// Token: 0x04006C13 RID: 27667
		[Serialize]
		private int numHarvests;
	}
}
