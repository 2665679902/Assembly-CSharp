using System;

// Token: 0x0200095B RID: 2395
public class RocketCommandConditions : KMonoBehaviour
{
	// Token: 0x060046CB RID: 18123 RVA: 0x0018E6B0 File Offset: 0x0018C8B0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		RocketModule component = base.GetComponent<RocketModule>();
		this.reachable = (ConditionDestinationReachable)component.AddModuleCondition(ProcessCondition.ProcessConditionType.RocketPrep, new ConditionDestinationReachable(base.GetComponent<RocketModule>()));
		this.allModulesComplete = (ConditionAllModulesComplete)component.AddModuleCondition(ProcessCondition.ProcessConditionType.RocketPrep, new ConditionAllModulesComplete(base.GetComponent<ILaunchableRocket>()));
		if (base.GetComponent<ILaunchableRocket>().registerType == LaunchableRocketRegisterType.Spacecraft)
		{
			this.destHasResources = (ConditionHasMinimumMass)component.AddModuleCondition(ProcessCondition.ProcessConditionType.RocketStorage, new ConditionHasMinimumMass(base.GetComponent<CommandModule>()));
			this.hasAstronaut = (ConditionHasAstronaut)component.AddModuleCondition(ProcessCondition.ProcessConditionType.RocketPrep, new ConditionHasAstronaut(base.GetComponent<CommandModule>()));
			this.hasSuit = (ConditionHasAtmoSuit)component.AddModuleCondition(ProcessCondition.ProcessConditionType.RocketStorage, new ConditionHasAtmoSuit(base.GetComponent<CommandModule>()));
			this.cargoEmpty = (CargoBayIsEmpty)component.AddModuleCondition(ProcessCondition.ProcessConditionType.RocketStorage, new CargoBayIsEmpty(base.GetComponent<CommandModule>()));
		}
		else if (base.GetComponent<ILaunchableRocket>().registerType == LaunchableRocketRegisterType.Clustercraft)
		{
			this.hasEngine = (ConditionHasEngine)component.AddModuleCondition(ProcessCondition.ProcessConditionType.RocketPrep, new ConditionHasEngine(base.GetComponent<ILaunchableRocket>()));
			this.hasNosecone = (ConditionHasNosecone)component.AddModuleCondition(ProcessCondition.ProcessConditionType.RocketPrep, new ConditionHasNosecone(base.GetComponent<LaunchableRocketCluster>()));
			this.hasControlStation = (ConditionHasControlStation)component.AddModuleCondition(ProcessCondition.ProcessConditionType.RocketPrep, new ConditionHasControlStation(base.GetComponent<RocketModuleCluster>()));
			this.pilotOnBoard = (ConditionPilotOnBoard)component.AddModuleCondition(ProcessCondition.ProcessConditionType.RocketBoard, new ConditionPilotOnBoard(base.GetComponent<PassengerRocketModule>()));
			this.passengersOnBoard = (ConditionPassengersOnBoard)component.AddModuleCondition(ProcessCondition.ProcessConditionType.RocketBoard, new ConditionPassengersOnBoard(base.GetComponent<PassengerRocketModule>()));
			this.noExtraPassengers = (ConditionNoExtraPassengers)component.AddModuleCondition(ProcessCondition.ProcessConditionType.RocketBoard, new ConditionNoExtraPassengers(base.GetComponent<PassengerRocketModule>()));
			this.onLaunchPad = (ConditionOnLaunchPad)component.AddModuleCondition(ProcessCondition.ProcessConditionType.RocketPrep, new ConditionOnLaunchPad(base.GetComponent<RocketModuleCluster>().CraftInterface));
		}
		int num = 1;
		if (DlcManager.FeatureClusterSpaceEnabled())
		{
			num = 0;
		}
		this.flightPathIsClear = (ConditionFlightPathIsClear)component.AddModuleCondition(ProcessCondition.ProcessConditionType.RocketFlight, new ConditionFlightPathIsClear(base.gameObject, num));
	}

	// Token: 0x04002ECD RID: 11981
	public ConditionDestinationReachable reachable;

	// Token: 0x04002ECE RID: 11982
	public ConditionHasAstronaut hasAstronaut;

	// Token: 0x04002ECF RID: 11983
	public ConditionPilotOnBoard pilotOnBoard;

	// Token: 0x04002ED0 RID: 11984
	public ConditionPassengersOnBoard passengersOnBoard;

	// Token: 0x04002ED1 RID: 11985
	public ConditionNoExtraPassengers noExtraPassengers;

	// Token: 0x04002ED2 RID: 11986
	public ConditionHasAtmoSuit hasSuit;

	// Token: 0x04002ED3 RID: 11987
	public CargoBayIsEmpty cargoEmpty;

	// Token: 0x04002ED4 RID: 11988
	public ConditionHasMinimumMass destHasResources;

	// Token: 0x04002ED5 RID: 11989
	public ConditionAllModulesComplete allModulesComplete;

	// Token: 0x04002ED6 RID: 11990
	public ConditionHasControlStation hasControlStation;

	// Token: 0x04002ED7 RID: 11991
	public ConditionHasEngine hasEngine;

	// Token: 0x04002ED8 RID: 11992
	public ConditionHasNosecone hasNosecone;

	// Token: 0x04002ED9 RID: 11993
	public ConditionOnLaunchPad onLaunchPad;

	// Token: 0x04002EDA RID: 11994
	public ConditionFlightPathIsClear flightPathIsClear;
}
