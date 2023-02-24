using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200070E RID: 1806
public class FarmDiagnostic : ColonyDiagnostic
{
	// Token: 0x0600318F RID: 12687 RVA: 0x00108468 File Offset: 0x00106668
	public FarmDiagnostic(int worldID)
		: base(worldID, UI.COLONY_DIAGNOSTICS.FARMDIAGNOSTIC.ALL_NAME)
	{
		this.icon = "icon_errand_farm";
		base.AddCriterion("CheckHasFarms", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.FARMDIAGNOSTIC.CRITERIA.CHECKHASFARMS, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckHasFarms)));
		base.AddCriterion("CheckPlanted", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.FARMDIAGNOSTIC.CRITERIA.CHECKPLANTED, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckPlanted)));
		base.AddCriterion("CheckWilting", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.FARMDIAGNOSTIC.CRITERIA.CHECKWILTING, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckWilting)));
		base.AddCriterion("CheckOperational", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.FARMDIAGNOSTIC.CRITERIA.CHECKOPERATIONAL, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckOperational)));
	}

	// Token: 0x06003190 RID: 12688 RVA: 0x00108529 File Offset: 0x00106729
	private void RefreshPlots()
	{
		this.plots = Components.PlantablePlots.GetItems(base.worldID).FindAll((PlantablePlot match) => match.HasDepositTag(GameTags.CropSeed));
	}

	// Token: 0x06003191 RID: 12689 RVA: 0x00108568 File Offset: 0x00106768
	private ColonyDiagnostic.DiagnosticResult CheckHasFarms()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		if (this.plots.Count == 0)
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
			diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.FARMDIAGNOSTIC.NONE;
		}
		return diagnosticResult;
	}

	// Token: 0x06003192 RID: 12690 RVA: 0x001085B0 File Offset: 0x001067B0
	private ColonyDiagnostic.DiagnosticResult CheckPlanted()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		bool flag = false;
		using (List<PlantablePlot>.Enumerator enumerator = this.plots.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.plant != null)
				{
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
			diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.FARMDIAGNOSTIC.NONE_PLANTED;
		}
		return diagnosticResult;
	}

	// Token: 0x06003193 RID: 12691 RVA: 0x00108640 File Offset: 0x00106840
	private ColonyDiagnostic.DiagnosticResult CheckWilting()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		foreach (PlantablePlot plantablePlot in this.plots)
		{
			if (plantablePlot.plant != null && plantablePlot.plant.HasTag(GameTags.Wilting))
			{
				StandardCropPlant component = plantablePlot.plant.GetComponent<StandardCropPlant>();
				if (component != null && component.smi.IsInsideState(component.smi.sm.alive.wilting) && component.smi.timeinstate > 15f)
				{
					diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
					diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.FARMDIAGNOSTIC.WILTING;
					diagnosticResult.clickThroughTarget = new global::Tuple<Vector3, GameObject>(plantablePlot.transform.position, plantablePlot.gameObject);
					break;
				}
			}
		}
		return diagnosticResult;
	}

	// Token: 0x06003194 RID: 12692 RVA: 0x00108750 File Offset: 0x00106950
	private ColonyDiagnostic.DiagnosticResult CheckOperational()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		foreach (PlantablePlot plantablePlot in this.plots)
		{
			if (plantablePlot.plant != null && !plantablePlot.HasTag(GameTags.Operational))
			{
				diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
				diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.FARMDIAGNOSTIC.INOPERATIONAL;
				diagnosticResult.clickThroughTarget = new global::Tuple<Vector3, GameObject>(plantablePlot.transform.position, plantablePlot.gameObject);
				break;
			}
		}
		return diagnosticResult;
	}

	// Token: 0x06003195 RID: 12693 RVA: 0x00108804 File Offset: 0x00106A04
	public override string GetAverageValueString()
	{
		if (this.plots == null)
		{
			this.RefreshPlots();
		}
		return TrackerTool.Instance.GetWorldTracker<CropTracker>(base.worldID).GetCurrentValue().ToString() + "/" + this.plots.Count.ToString();
	}

	// Token: 0x06003196 RID: 12694 RVA: 0x0010885C File Offset: 0x00106A5C
	public override ColonyDiagnostic.DiagnosticResult Evaluate()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.NO_MINIONS, null);
		if (ColonyDiagnosticUtility.IgnoreRocketsWithNoCrewRequested(base.worldID, out diagnosticResult))
		{
			return diagnosticResult;
		}
		this.RefreshPlots();
		return base.Evaluate();
	}

	// Token: 0x04001E3C RID: 7740
	private List<PlantablePlot> plots;
}
