using System;
using System.Collections.Generic;
using ImGuiNET;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200052F RID: 1327
public class DevToolUI : DevTool
{
	// Token: 0x06001FD7 RID: 8151 RVA: 0x000ADCE6 File Offset: 0x000ABEE6
	protected override void RenderTo(DevPanel panel)
	{
		this.RepopulateRaycastHits();
		this.DrawPingObject();
		this.DrawRaycastHits();
	}

	// Token: 0x06001FD8 RID: 8152 RVA: 0x000ADCFC File Offset: 0x000ABEFC
	private void DrawPingObject()
	{
		if (this.m_last_pinged_hit != null)
		{
			GameObject gameObject = this.m_last_pinged_hit.Value.gameObject;
			if (gameObject != null && gameObject)
			{
				ImGui.Text("Last Pinged: \"" + DevToolUI.GetQualifiedName(gameObject) + "\"");
				ImGui.SameLine();
				if (ImGui.Button("Inspect"))
				{
					DevToolSceneInspector.Inspect(gameObject);
				}
				ImGui.Spacing();
				ImGui.Spacing();
			}
			else
			{
				this.m_last_pinged_hit = null;
			}
		}
		ImGui.Text("Press \",\" to ping the top hovered ui object");
		ImGui.Spacing();
		ImGui.Spacing();
	}

	// Token: 0x06001FD9 RID: 8153 RVA: 0x000ADD9B File Offset: 0x000ABF9B
	private void Internal_Ping(RaycastResult raycastResult)
	{
		GameObject gameObject = raycastResult.gameObject;
		this.m_last_pinged_hit = new RaycastResult?(raycastResult);
	}

	// Token: 0x06001FDA RID: 8154 RVA: 0x000ADDB4 File Offset: 0x000ABFB4
	public static void PingHoveredObject()
	{
		using (ListPool<RaycastResult, DevToolUI>.PooledList pooledList = PoolsFor<DevToolUI>.AllocateList<RaycastResult>())
		{
			UnityEngine.EventSystems.EventSystem current = UnityEngine.EventSystems.EventSystem.current;
			if (!(current == null) && current)
			{
				current.RaycastAll(new PointerEventData(current)
				{
					position = Input.mousePosition
				}, pooledList);
				DevToolManager.Instance.panels.AddOrGetDevTool<DevToolUI>().Internal_Ping(pooledList[0]);
			}
		}
	}

	// Token: 0x06001FDB RID: 8155 RVA: 0x000ADE34 File Offset: 0x000AC034
	private void DrawRaycastHits()
	{
		if (this.m_raycast_hits.Count <= 0)
		{
			ImGui.Text("Didn't hit any ui");
			return;
		}
		ImGui.Text("Raycast Hits:");
		ImGui.Indent();
		for (int i = 0; i < this.m_raycast_hits.Count; i++)
		{
			RaycastResult raycastResult = this.m_raycast_hits[i];
			ImGui.BulletText(string.Format("[{0}] {1}", i, DevToolUI.GetQualifiedName(raycastResult.gameObject)));
		}
		ImGui.Unindent();
	}

	// Token: 0x06001FDC RID: 8156 RVA: 0x000ADEB4 File Offset: 0x000AC0B4
	private void RepopulateRaycastHits()
	{
		this.m_raycast_hits.Clear();
		UnityEngine.EventSystems.EventSystem current = UnityEngine.EventSystems.EventSystem.current;
		if (current == null || !current)
		{
			return;
		}
		current.RaycastAll(new PointerEventData(current)
		{
			position = Input.mousePosition
		}, this.m_raycast_hits);
	}

	// Token: 0x06001FDD RID: 8157 RVA: 0x000ADF08 File Offset: 0x000AC108
	private static string GetQualifiedName(GameObject game_object)
	{
		KScreen componentInParent = game_object.GetComponentInParent<KScreen>();
		if (componentInParent != null)
		{
			return componentInParent.gameObject.name + " :: " + game_object.name;
		}
		return game_object.name ?? "";
	}

	// Token: 0x0400123B RID: 4667
	private List<RaycastResult> m_raycast_hits = new List<RaycastResult>();

	// Token: 0x0400123C RID: 4668
	private RaycastResult? m_last_pinged_hit;
}
