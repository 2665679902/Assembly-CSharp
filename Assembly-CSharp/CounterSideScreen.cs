using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000BAA RID: 2986
public class CounterSideScreen : SideScreenContent, IRender200ms
{
	// Token: 0x06005DEA RID: 24042 RVA: 0x0022532D File Offset: 0x0022352D
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06005DEB RID: 24043 RVA: 0x00225338 File Offset: 0x00223538
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.resetButton.onClick += this.ResetCounter;
		this.incrementMaxButton.onClick += this.IncrementMaxCount;
		this.decrementMaxButton.onClick += this.DecrementMaxCount;
		this.incrementModeButton.onClick += this.ToggleMode;
		this.advancedModeToggle.onClick += this.ToggleAdvanced;
		this.maxCountInput.onEndEdit += delegate
		{
			this.UpdateMaxCountFromTextInput(this.maxCountInput.currentValue);
		};
		this.UpdateCurrentCountLabel(this.targetLogicCounter.currentCount);
	}

	// Token: 0x06005DEC RID: 24044 RVA: 0x002253E6 File Offset: 0x002235E6
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<LogicCounter>() != null;
	}

	// Token: 0x06005DED RID: 24045 RVA: 0x002253F4 File Offset: 0x002235F4
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.maxCountInput.minValue = 1f;
		this.maxCountInput.maxValue = 10f;
		this.targetLogicCounter = target.GetComponent<LogicCounter>();
		this.UpdateCurrentCountLabel(this.targetLogicCounter.currentCount);
		this.UpdateMaxCountLabel(this.targetLogicCounter.maxCount);
		this.advancedModeCheckmark.enabled = this.targetLogicCounter.advancedMode;
	}

	// Token: 0x06005DEE RID: 24046 RVA: 0x0022546C File Offset: 0x0022366C
	public void Render200ms(float dt)
	{
		if (this.targetLogicCounter == null)
		{
			return;
		}
		this.UpdateCurrentCountLabel(this.targetLogicCounter.currentCount);
	}

	// Token: 0x06005DEF RID: 24047 RVA: 0x00225490 File Offset: 0x00223690
	private void UpdateCurrentCountLabel(int value)
	{
		string text = value.ToString();
		if (value == this.targetLogicCounter.maxCount)
		{
			text = UI.FormatAsAutomationState(text, UI.AutomationState.Active);
		}
		else
		{
			text = UI.FormatAsAutomationState(text, UI.AutomationState.Standby);
		}
		this.currentCount.text = (this.targetLogicCounter.advancedMode ? string.Format(UI.UISIDESCREENS.COUNTER_SIDE_SCREEN.CURRENT_COUNT_ADVANCED, text) : string.Format(UI.UISIDESCREENS.COUNTER_SIDE_SCREEN.CURRENT_COUNT_SIMPLE, text));
	}

	// Token: 0x06005DF0 RID: 24048 RVA: 0x002254FF File Offset: 0x002236FF
	private void UpdateMaxCountLabel(int value)
	{
		this.maxCountInput.SetAmount((float)value);
	}

	// Token: 0x06005DF1 RID: 24049 RVA: 0x0022550E File Offset: 0x0022370E
	private void UpdateMaxCountFromTextInput(float newValue)
	{
		this.SetMaxCount((int)newValue);
	}

	// Token: 0x06005DF2 RID: 24050 RVA: 0x00225518 File Offset: 0x00223718
	private void IncrementMaxCount()
	{
		this.SetMaxCount(this.targetLogicCounter.maxCount + 1);
	}

	// Token: 0x06005DF3 RID: 24051 RVA: 0x0022552D File Offset: 0x0022372D
	private void DecrementMaxCount()
	{
		this.SetMaxCount(this.targetLogicCounter.maxCount - 1);
	}

	// Token: 0x06005DF4 RID: 24052 RVA: 0x00225544 File Offset: 0x00223744
	private void SetMaxCount(int newValue)
	{
		if (newValue > 10)
		{
			newValue = 1;
		}
		if (newValue < 1)
		{
			newValue = 10;
		}
		if (newValue < this.targetLogicCounter.currentCount)
		{
			this.targetLogicCounter.currentCount = newValue;
		}
		this.targetLogicCounter.maxCount = newValue;
		this.UpdateCounterStates();
		this.UpdateMaxCountLabel(newValue);
	}

	// Token: 0x06005DF5 RID: 24053 RVA: 0x00225594 File Offset: 0x00223794
	private void ResetCounter()
	{
		this.targetLogicCounter.ResetCounter();
	}

	// Token: 0x06005DF6 RID: 24054 RVA: 0x002255A1 File Offset: 0x002237A1
	private void UpdateCounterStates()
	{
		this.targetLogicCounter.SetCounterState();
		this.targetLogicCounter.UpdateLogicCircuit();
		this.targetLogicCounter.UpdateVisualState(true);
		this.targetLogicCounter.UpdateMeter();
	}

	// Token: 0x06005DF7 RID: 24055 RVA: 0x002255D0 File Offset: 0x002237D0
	private void ToggleMode()
	{
	}

	// Token: 0x06005DF8 RID: 24056 RVA: 0x002255D4 File Offset: 0x002237D4
	private void ToggleAdvanced()
	{
		this.targetLogicCounter.advancedMode = !this.targetLogicCounter.advancedMode;
		this.advancedModeCheckmark.enabled = this.targetLogicCounter.advancedMode;
		this.UpdateCurrentCountLabel(this.targetLogicCounter.currentCount);
		this.UpdateCounterStates();
	}

	// Token: 0x0400403B RID: 16443
	public LogicCounter targetLogicCounter;

	// Token: 0x0400403C RID: 16444
	public KButton resetButton;

	// Token: 0x0400403D RID: 16445
	public KButton incrementMaxButton;

	// Token: 0x0400403E RID: 16446
	public KButton decrementMaxButton;

	// Token: 0x0400403F RID: 16447
	public KButton incrementModeButton;

	// Token: 0x04004040 RID: 16448
	public KToggle advancedModeToggle;

	// Token: 0x04004041 RID: 16449
	public KImage advancedModeCheckmark;

	// Token: 0x04004042 RID: 16450
	public LocText currentCount;

	// Token: 0x04004043 RID: 16451
	[SerializeField]
	private KNumberInputField maxCountInput;
}
