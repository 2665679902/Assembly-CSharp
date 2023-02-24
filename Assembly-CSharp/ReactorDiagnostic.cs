using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000715 RID: 1813
public class ReactorDiagnostic : ColonyDiagnostic
{
	// Token: 0x060031AE RID: 12718 RVA: 0x00109634 File Offset: 0x00107834
	public ReactorDiagnostic(int worldID)
		: base(worldID, UI.COLONY_DIAGNOSTICS.REACTORDIAGNOSTIC.ALL_NAME)
	{
		this.icon = "overlay_radiation";
		base.AddCriterion("CheckTemperature", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.REACTORDIAGNOSTIC.CRITERIA.CHECKTEMPERATURE, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckTemperature)));
		base.AddCriterion("CheckCoolant", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.REACTORDIAGNOSTIC.CRITERIA.CHECKCOOLANT, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckCoolant)));
	}

	// Token: 0x060031AF RID: 12719 RVA: 0x001096A9 File Offset: 0x001078A9
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060031B0 RID: 12720 RVA: 0x001096B0 File Offset: 0x001078B0
	private ColonyDiagnostic.DiagnosticResult CheckTemperature()
	{
		List<Reactor> worldItems = Components.NuclearReactors.GetWorldItems(base.worldID, false);
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
		diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.REACTORDIAGNOSTIC.NORMAL;
		foreach (Reactor reactor in worldItems)
		{
			if (reactor.FuelTemperature > 1254.8625f)
			{
				diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Warning;
				diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.REACTORDIAGNOSTIC.CRITERIA_TEMPERATURE_WARNING;
				diagnosticResult.clickThroughTarget = new global::Tuple<Vector3, GameObject>(reactor.gameObject.transform.position, reactor.gameObject);
			}
		}
		return diagnosticResult;
	}

	// Token: 0x060031B1 RID: 12721 RVA: 0x0010977C File Offset: 0x0010797C
	private ColonyDiagnostic.DiagnosticResult CheckCoolant()
	{
		List<Reactor> worldItems = Components.NuclearReactors.GetWorldItems(base.worldID, false);
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
		diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.REACTORDIAGNOSTIC.NORMAL;
		foreach (Reactor reactor in worldItems)
		{
			if (reactor.On && reactor.ReserveCoolantMass <= 45f)
			{
				diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
				diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.REACTORDIAGNOSTIC.CRITERIA_COOLANT_WARNING;
				diagnosticResult.clickThroughTarget = new global::Tuple<Vector3, GameObject>(reactor.gameObject.transform.position, reactor.gameObject);
			}
		}
		return diagnosticResult;
	}
}
