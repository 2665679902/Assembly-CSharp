using System;
using UnityEngine;

// Token: 0x02000C32 RID: 3122
public class ScreenResolutionMonitor : MonoBehaviour
{
	// Token: 0x060062BF RID: 25279 RVA: 0x00247784 File Offset: 0x00245984
	private void Awake()
	{
		this.previousSize = new Vector2((float)Screen.width, (float)Screen.height);
	}

	// Token: 0x060062C0 RID: 25280 RVA: 0x002477A0 File Offset: 0x002459A0
	private void Update()
	{
		if ((this.previousSize.x != (float)Screen.width || this.previousSize.y != (float)Screen.height) && Game.Instance != null)
		{
			Game.Instance.Trigger(445618876, null);
			this.previousSize.x = (float)Screen.width;
			this.previousSize.y = (float)Screen.height;
		}
		this.UpdateShouldUseGamepadUIMode();
	}

	// Token: 0x060062C1 RID: 25281 RVA: 0x00247818 File Offset: 0x00245A18
	public static bool UsingGamepadUIMode()
	{
		return ScreenResolutionMonitor.previousGamepadUIMode;
	}

	// Token: 0x060062C2 RID: 25282 RVA: 0x00247820 File Offset: 0x00245A20
	private void UpdateShouldUseGamepadUIMode()
	{
		bool flag = (Screen.dpi > 130f && Screen.height < 900) || KInputManager.currentControllerIsGamepad;
		if (flag != ScreenResolutionMonitor.previousGamepadUIMode)
		{
			ScreenResolutionMonitor.previousGamepadUIMode = flag;
			if (Game.Instance == null)
			{
				return;
			}
			Game.Instance.Trigger(-442024484, null);
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound(flag ? "ControllerType_ToggleOn" : "ControllerType_ToggleOff", false));
		}
	}

	// Token: 0x04004498 RID: 17560
	[SerializeField]
	private Vector2 previousSize;

	// Token: 0x04004499 RID: 17561
	private static bool previousGamepadUIMode;

	// Token: 0x0400449A RID: 17562
	private const float HIGH_DPI = 130f;
}
