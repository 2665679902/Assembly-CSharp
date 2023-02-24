using System;
using System.Collections.Generic;
using KSerialization;
using ProcGen;
using UnityEngine;

// Token: 0x02000794 RID: 1940
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/GermExposureTracker")]
public class GermExposureTracker : KMonoBehaviour
{
	// Token: 0x06003664 RID: 13924 RVA: 0x0012DB08 File Offset: 0x0012BD08
	protected override void OnPrefabInit()
	{
		global::Debug.Assert(GermExposureTracker.Instance == null);
		GermExposureTracker.Instance = this;
	}

	// Token: 0x06003665 RID: 13925 RVA: 0x0012DB20 File Offset: 0x0012BD20
	protected override void OnSpawn()
	{
		this.rng = new SeededRandom(GameClock.Instance.GetCycle());
	}

	// Token: 0x06003666 RID: 13926 RVA: 0x0012DB37 File Offset: 0x0012BD37
	protected override void OnForcedCleanUp()
	{
		GermExposureTracker.Instance = null;
	}

	// Token: 0x06003667 RID: 13927 RVA: 0x0012DB40 File Offset: 0x0012BD40
	public void AddExposure(ExposureType exposure_type, float amount)
	{
		float num;
		this.accumulation.TryGetValue(exposure_type.germ_id, out num);
		float num2 = num + amount;
		if (num2 > 1f)
		{
			using (List<MinionIdentity>.Enumerator enumerator = Components.LiveMinionIdentities.Items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MinionIdentity minionIdentity = enumerator.Current;
					GermExposureMonitor.Instance smi = minionIdentity.GetSMI<GermExposureMonitor.Instance>();
					if (smi.GetExposureState(exposure_type.germ_id) == GermExposureMonitor.ExposureState.Exposed)
					{
						float exposureWeight = minionIdentity.GetSMI<GermExposureMonitor.Instance>().GetExposureWeight(exposure_type.germ_id);
						if (exposureWeight > 0f)
						{
							this.exposure_candidates.Add(new GermExposureTracker.WeightedExposure
							{
								weight = exposureWeight,
								monitor = smi
							});
						}
					}
				}
				goto IL_F8;
			}
			IL_AF:
			num2 -= 1f;
			if (this.exposure_candidates.Count > 0)
			{
				GermExposureTracker.WeightedExposure weightedExposure = WeightedRandom.Choose<GermExposureTracker.WeightedExposure>(this.exposure_candidates, this.rng);
				this.exposure_candidates.Remove(weightedExposure);
				weightedExposure.monitor.ContractGerms(exposure_type.germ_id);
			}
			IL_F8:
			if (num2 > 1f)
			{
				goto IL_AF;
			}
		}
		this.accumulation[exposure_type.germ_id] = num2;
		this.exposure_candidates.Clear();
	}

	// Token: 0x04002444 RID: 9284
	public static GermExposureTracker Instance;

	// Token: 0x04002445 RID: 9285
	[Serialize]
	private Dictionary<HashedString, float> accumulation = new Dictionary<HashedString, float>();

	// Token: 0x04002446 RID: 9286
	private SeededRandom rng;

	// Token: 0x04002447 RID: 9287
	private List<GermExposureTracker.WeightedExposure> exposure_candidates = new List<GermExposureTracker.WeightedExposure>();

	// Token: 0x020014CF RID: 5327
	private class WeightedExposure : IWeighted
	{
		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x0600820C RID: 33292 RVA: 0x002E428A File Offset: 0x002E248A
		// (set) Token: 0x0600820D RID: 33293 RVA: 0x002E4292 File Offset: 0x002E2492
		public float weight { get; set; }

		// Token: 0x040064F7 RID: 25847
		public GermExposureMonitor.Instance monitor;
	}
}
