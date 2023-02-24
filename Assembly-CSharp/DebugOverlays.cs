using System;

// Token: 0x020009FB RID: 2555
public class DebugOverlays : KScreen
{
	// Token: 0x170005C2 RID: 1474
	// (get) Token: 0x06004CC2 RID: 19650 RVA: 0x001B02E7 File Offset: 0x001AE4E7
	// (set) Token: 0x06004CC3 RID: 19651 RVA: 0x001B02EE File Offset: 0x001AE4EE
	public static DebugOverlays instance { get; private set; }

	// Token: 0x06004CC4 RID: 19652 RVA: 0x001B02F8 File Offset: 0x001AE4F8
	protected override void OnPrefabInit()
	{
		DebugOverlays.instance = this;
		KPopupMenu componentInChildren = base.GetComponentInChildren<KPopupMenu>();
		componentInChildren.SetOptions(new string[] { "None", "Rooms", "Lighting", "Style", "Flow" });
		KPopupMenu kpopupMenu = componentInChildren;
		kpopupMenu.OnSelect = (Action<string, int>)Delegate.Combine(kpopupMenu.OnSelect, new Action<string, int>(this.OnSelect));
		base.gameObject.SetActive(false);
	}

	// Token: 0x06004CC5 RID: 19653 RVA: 0x001B0374 File Offset: 0x001AE574
	private void OnSelect(string str, int index)
	{
		if (str != null)
		{
			if (str == "None")
			{
				SimDebugView.Instance.SetMode(OverlayModes.None.ID);
				return;
			}
			if (str == "Flow")
			{
				SimDebugView.Instance.SetMode(SimDebugView.OverlayModes.Flow);
				return;
			}
			if (str == "Lighting")
			{
				SimDebugView.Instance.SetMode(OverlayModes.Light.ID);
				return;
			}
			if (str == "Rooms")
			{
				SimDebugView.Instance.SetMode(OverlayModes.Rooms.ID);
				return;
			}
		}
		Debug.LogError("Unknown debug view: " + str);
	}
}
