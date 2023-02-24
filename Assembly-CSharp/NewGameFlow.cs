using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B37 RID: 2871
[AddComponentMenu("KMonoBehaviour/scripts/NewGameFlow")]
public class NewGameFlow : KMonoBehaviour
{
	// Token: 0x060058EC RID: 22764 RVA: 0x00203A0C File Offset: 0x00201C0C
	public void BeginFlow()
	{
		this.currentScreenIndex = -1;
		this.Next();
	}

	// Token: 0x060058ED RID: 22765 RVA: 0x00203A1B File Offset: 0x00201C1B
	private void Next()
	{
		this.ClearCurrentScreen();
		this.currentScreenIndex++;
		this.ActivateCurrentScreen();
	}

	// Token: 0x060058EE RID: 22766 RVA: 0x00203A37 File Offset: 0x00201C37
	private void Previous()
	{
		this.ClearCurrentScreen();
		this.currentScreenIndex--;
		this.ActivateCurrentScreen();
	}

	// Token: 0x060058EF RID: 22767 RVA: 0x00203A53 File Offset: 0x00201C53
	private void ClearCurrentScreen()
	{
		if (this.currentScreen != null)
		{
			this.currentScreen.Deactivate();
			this.currentScreen = null;
		}
	}

	// Token: 0x060058F0 RID: 22768 RVA: 0x00203A78 File Offset: 0x00201C78
	private void ActivateCurrentScreen()
	{
		if (this.currentScreenIndex >= 0 && this.currentScreenIndex < this.newGameFlowScreens.Count)
		{
			NewGameFlowScreen newGameFlowScreen = Util.KInstantiateUI<NewGameFlowScreen>(this.newGameFlowScreens[this.currentScreenIndex].gameObject, base.transform.parent.gameObject, true);
			newGameFlowScreen.OnNavigateForward += this.Next;
			newGameFlowScreen.OnNavigateBackward += this.Previous;
			if (!newGameFlowScreen.IsActive() && !newGameFlowScreen.activateOnSpawn)
			{
				newGameFlowScreen.Activate();
			}
			this.currentScreen = newGameFlowScreen;
		}
	}

	// Token: 0x04003C05 RID: 15365
	public List<NewGameFlowScreen> newGameFlowScreens;

	// Token: 0x04003C06 RID: 15366
	private int currentScreenIndex = -1;

	// Token: 0x04003C07 RID: 15367
	private NewGameFlowScreen currentScreen;
}
