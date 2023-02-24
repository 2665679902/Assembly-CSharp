using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200070D RID: 1805
public class EntombedDiagnostic : ColonyDiagnostic
{
	// Token: 0x0600318C RID: 12684 RVA: 0x0010831C File Offset: 0x0010651C
	public EntombedDiagnostic(int worldID)
		: base(worldID, UI.COLONY_DIAGNOSTICS.ENTOMBEDDIAGNOSTIC.ALL_NAME)
	{
		this.icon = "icon_action_dig";
		base.AddCriterion("CheckEntombed", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.ENTOMBEDDIAGNOSTIC.CRITERIA.CHECKENTOMBED, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckEntombed)));
	}

	// Token: 0x0600318D RID: 12685 RVA: 0x0010836C File Offset: 0x0010656C
	private ColonyDiagnostic.DiagnosticResult CheckEntombed()
	{
		List<BuildingComplete> worldItems = Components.EntombedBuildings.GetWorldItems(base.worldID, false);
		this.m_entombedCount = 0;
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
		diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.ENTOMBEDDIAGNOSTIC.NORMAL;
		foreach (BuildingComplete buildingComplete in worldItems)
		{
			if (!buildingComplete.IsNullOrDestroyed() && buildingComplete.HasTag(GameTags.Entombed))
			{
				diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Bad;
				diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.ENTOMBEDDIAGNOSTIC.BUILDING_ENTOMBED;
				diagnosticResult.clickThroughTarget = new global::Tuple<Vector3, GameObject>(buildingComplete.gameObject.transform.position, buildingComplete.gameObject);
				this.m_entombedCount++;
			}
		}
		return diagnosticResult;
	}

	// Token: 0x0600318E RID: 12686 RVA: 0x00108458 File Offset: 0x00106658
	public override string GetAverageValueString()
	{
		return this.m_entombedCount.ToString();
	}

	// Token: 0x04001E3B RID: 7739
	private int m_entombedCount;
}
