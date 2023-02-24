using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000C11 RID: 3089
[AddComponentMenu("KMonoBehaviour/scripts/ToolParameterMenu")]
public class ToolParameterMenu : KMonoBehaviour
{
	// Token: 0x1400002D RID: 45
	// (add) Token: 0x060061E1 RID: 25057 RVA: 0x00242C5C File Offset: 0x00240E5C
	// (remove) Token: 0x060061E2 RID: 25058 RVA: 0x00242C94 File Offset: 0x00240E94
	public event System.Action onParametersChanged;

	// Token: 0x060061E3 RID: 25059 RVA: 0x00242CC9 File Offset: 0x00240EC9
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.ClearMenu();
	}

	// Token: 0x060061E4 RID: 25060 RVA: 0x00242CD8 File Offset: 0x00240ED8
	public void PopulateMenu(Dictionary<string, ToolParameterMenu.ToggleState> parameters)
	{
		this.ClearMenu();
		this.currentParameters = parameters;
		foreach (KeyValuePair<string, ToolParameterMenu.ToggleState> keyValuePair in parameters)
		{
			GameObject gameObject = Util.KInstantiateUI(this.widgetPrefab, this.widgetContainer, true);
			gameObject.GetComponentInChildren<LocText>().text = Strings.Get("STRINGS.UI.TOOLS.FILTERLAYERS." + keyValuePair.Key);
			this.widgets.Add(keyValuePair.Key, gameObject);
			MultiToggle toggle = gameObject.GetComponentInChildren<MultiToggle>();
			ToolParameterMenu.ToggleState value = keyValuePair.Value;
			if (value == ToolParameterMenu.ToggleState.Disabled)
			{
				toggle.ChangeState(2);
			}
			else if (value == ToolParameterMenu.ToggleState.On)
			{
				toggle.ChangeState(1);
				this.lastEnabledFilter = keyValuePair.Key;
			}
			else
			{
				toggle.ChangeState(0);
			}
			MultiToggle toggle2 = toggle;
			toggle2.onClick = (System.Action)Delegate.Combine(toggle2.onClick, new System.Action(delegate
			{
				foreach (KeyValuePair<string, GameObject> keyValuePair2 in this.widgets)
				{
					if (keyValuePair2.Value == toggle.transform.parent.gameObject)
					{
						if (this.currentParameters[keyValuePair2.Key] == ToolParameterMenu.ToggleState.Disabled)
						{
							break;
						}
						this.ChangeToSetting(keyValuePair2.Key);
						this.OnChange();
						break;
					}
				}
			}));
		}
		this.content.SetActive(true);
	}

	// Token: 0x060061E5 RID: 25061 RVA: 0x00242E14 File Offset: 0x00241014
	public void ClearMenu()
	{
		this.content.SetActive(false);
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.widgets)
		{
			Util.KDestroyGameObject(keyValuePair.Value);
		}
		this.widgets.Clear();
	}

	// Token: 0x060061E6 RID: 25062 RVA: 0x00242E84 File Offset: 0x00241084
	private void ChangeToSetting(string key)
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.widgets)
		{
			if (this.currentParameters[keyValuePair.Key] != ToolParameterMenu.ToggleState.Disabled)
			{
				this.currentParameters[keyValuePair.Key] = ToolParameterMenu.ToggleState.Off;
			}
		}
		this.currentParameters[key] = ToolParameterMenu.ToggleState.On;
	}

	// Token: 0x060061E7 RID: 25063 RVA: 0x00242F08 File Offset: 0x00241108
	private void OnChange()
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.widgets)
		{
			switch (this.currentParameters[keyValuePair.Key])
			{
			case ToolParameterMenu.ToggleState.On:
				keyValuePair.Value.GetComponentInChildren<MultiToggle>().ChangeState(1);
				this.lastEnabledFilter = keyValuePair.Key;
				break;
			case ToolParameterMenu.ToggleState.Off:
				keyValuePair.Value.GetComponentInChildren<MultiToggle>().ChangeState(0);
				break;
			case ToolParameterMenu.ToggleState.Disabled:
				keyValuePair.Value.GetComponentInChildren<MultiToggle>().ChangeState(2);
				break;
			}
		}
		if (this.onParametersChanged != null)
		{
			this.onParametersChanged();
		}
	}

	// Token: 0x060061E8 RID: 25064 RVA: 0x00242FD8 File Offset: 0x002411D8
	public string GetLastEnabledFilter()
	{
		return this.lastEnabledFilter;
	}

	// Token: 0x040043B3 RID: 17331
	public GameObject content;

	// Token: 0x040043B4 RID: 17332
	public GameObject widgetContainer;

	// Token: 0x040043B5 RID: 17333
	public GameObject widgetPrefab;

	// Token: 0x040043B7 RID: 17335
	private Dictionary<string, GameObject> widgets = new Dictionary<string, GameObject>();

	// Token: 0x040043B8 RID: 17336
	private Dictionary<string, ToolParameterMenu.ToggleState> currentParameters;

	// Token: 0x040043B9 RID: 17337
	private string lastEnabledFilter;

	// Token: 0x02001AA9 RID: 6825
	public class FILTERLAYERS
	{
		// Token: 0x04007830 RID: 30768
		public static string BUILDINGS = "BUILDINGS";

		// Token: 0x04007831 RID: 30769
		public static string TILES = "TILES";

		// Token: 0x04007832 RID: 30770
		public static string WIRES = "WIRES";

		// Token: 0x04007833 RID: 30771
		public static string LIQUIDCONDUIT = "LIQUIDPIPES";

		// Token: 0x04007834 RID: 30772
		public static string GASCONDUIT = "GASPIPES";

		// Token: 0x04007835 RID: 30773
		public static string SOLIDCONDUIT = "SOLIDCONDUITS";

		// Token: 0x04007836 RID: 30774
		public static string CLEANANDCLEAR = "CLEANANDCLEAR";

		// Token: 0x04007837 RID: 30775
		public static string DIGPLACER = "DIGPLACER";

		// Token: 0x04007838 RID: 30776
		public static string LOGIC = "LOGIC";

		// Token: 0x04007839 RID: 30777
		public static string BACKWALL = "BACKWALL";

		// Token: 0x0400783A RID: 30778
		public static string CONSTRUCTION = "CONSTRUCTION";

		// Token: 0x0400783B RID: 30779
		public static string DIG = "DIG";

		// Token: 0x0400783C RID: 30780
		public static string CLEAN = "CLEAN";

		// Token: 0x0400783D RID: 30781
		public static string OPERATE = "OPERATE";

		// Token: 0x0400783E RID: 30782
		public static string METAL = "METAL";

		// Token: 0x0400783F RID: 30783
		public static string BUILDABLE = "BUILDABLE";

		// Token: 0x04007840 RID: 30784
		public static string FILTER = "FILTER";

		// Token: 0x04007841 RID: 30785
		public static string LIQUIFIABLE = "LIQUIFIABLE";

		// Token: 0x04007842 RID: 30786
		public static string LIQUID = "LIQUID";

		// Token: 0x04007843 RID: 30787
		public static string CONSUMABLEORE = "CONSUMABLEORE";

		// Token: 0x04007844 RID: 30788
		public static string ORGANICS = "ORGANICS";

		// Token: 0x04007845 RID: 30789
		public static string FARMABLE = "FARMABLE";

		// Token: 0x04007846 RID: 30790
		public static string GAS = "GAS";

		// Token: 0x04007847 RID: 30791
		public static string HEATFLOW = "HEATFLOW";

		// Token: 0x04007848 RID: 30792
		public static string ABSOLUTETEMPERATURE = "ABSOLUTETEMPERATURE";

		// Token: 0x04007849 RID: 30793
		public static string ADAPTIVETEMPERATURE = "ADAPTIVETEMPERATURE";

		// Token: 0x0400784A RID: 30794
		public static string STATECHANGE = "STATECHANGE";

		// Token: 0x0400784B RID: 30795
		public static string ALL = "ALL";
	}

	// Token: 0x02001AAA RID: 6826
	public enum ToggleState
	{
		// Token: 0x0400784D RID: 30797
		On,
		// Token: 0x0400784E RID: 30798
		Off,
		// Token: 0x0400784F RID: 30799
		Disabled
	}
}
