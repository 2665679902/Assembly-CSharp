using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000973 RID: 2419
public class CargoBayIsEmpty : ProcessCondition
{
	// Token: 0x060047DE RID: 18398 RVA: 0x00194668 File Offset: 0x00192868
	public CargoBayIsEmpty(CommandModule module)
	{
		this.commandModule = module;
	}

	// Token: 0x060047DF RID: 18399 RVA: 0x00194678 File Offset: 0x00192878
	public override ProcessCondition.Status EvaluateCondition()
	{
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(this.commandModule.GetComponent<AttachableBuilding>()))
		{
			CargoBay component = gameObject.GetComponent<CargoBay>();
			if (component != null && component.storage.MassStored() != 0f)
			{
				return ProcessCondition.Status.Failure;
			}
		}
		return ProcessCondition.Status.Ready;
	}

	// Token: 0x060047E0 RID: 18400 RVA: 0x001946F8 File Offset: 0x001928F8
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		return UI.STARMAP.CARGOEMPTY.NAME;
	}

	// Token: 0x060047E1 RID: 18401 RVA: 0x00194704 File Offset: 0x00192904
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		return UI.STARMAP.CARGOEMPTY.TOOLTIP;
	}

	// Token: 0x060047E2 RID: 18402 RVA: 0x00194710 File Offset: 0x00192910
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04002F73 RID: 12147
	private CommandModule commandModule;
}
