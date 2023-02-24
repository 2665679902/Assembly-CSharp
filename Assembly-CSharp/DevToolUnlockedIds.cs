using System;
using System.Collections.Generic;
using System.Linq;
using ImGuiNET;
using UnityEngine;

// Token: 0x02000530 RID: 1328
public class DevToolUnlockedIds : DevTool
{
	// Token: 0x06001FDF RID: 8159 RVA: 0x000ADF63 File Offset: 0x000AC163
	public DevToolUnlockedIds()
	{
		this.RequiresGameRunning = true;
	}

	// Token: 0x06001FE0 RID: 8160 RVA: 0x000ADF88 File Offset: 0x000AC188
	protected override void RenderTo(DevPanel panel)
	{
		bool flag;
		DevToolUnlockedIds.UnlocksWrapper unlocksWrapper;
		this.GetUnlocks().Deconstruct(out flag, out unlocksWrapper);
		bool flag2 = flag;
		DevToolUnlockedIds.UnlocksWrapper unlocksWrapper2 = unlocksWrapper;
		if (!flag2)
		{
			ImGui.Text("Couldn't access global unlocks");
			return;
		}
		if (ImGui.TreeNode("Help"))
		{
			ImGui.TextWrapped("This is a list of global unlocks that are persistant across saves. Changes made here will be saved to disk immediately.");
			ImGui.Spacing();
			ImGui.TextWrapped("NOTE: It may be necessary to relaunch the game after modifying unlocks in order for systems to respond.");
			ImGui.TreePop();
		}
		ImGui.Spacing();
		ImGuiEx.InputFilter("Filter", ref this.filterForUnlockIds, 50U);
		ImGuiTableFlags imGuiTableFlags = ImGuiTableFlags.RowBg | ImGuiTableFlags.BordersInnerH | ImGuiTableFlags.BordersOuterH | ImGuiTableFlags.BordersInnerV | ImGuiTableFlags.BordersOuterV | ImGuiTableFlags.ScrollY;
		if (ImGui.BeginTable("ID_unlockIds", 2, imGuiTableFlags))
		{
			ImGui.TableSetupScrollFreeze(2, 2);
			ImGui.TableSetupColumn("Unlock ID");
			ImGui.TableSetupColumn("Actions", ImGuiTableColumnFlags.WidthFixed);
			ImGui.TableHeadersRow();
			ImGui.PushID("ID_row_add_new");
			ImGui.TableNextRow();
			ImGui.TableSetColumnIndex(0);
			ImGui.InputText("", ref this.unlockIdToAdd, 50U);
			ImGui.TableSetColumnIndex(1);
			if (ImGui.Button("Add"))
			{
				unlocksWrapper2.AddId(this.unlockIdToAdd);
				global::Debug.Log("[Added unlock id] " + this.unlockIdToAdd);
				this.unlockIdToAdd = "";
			}
			ImGui.PopID();
			int num = 0;
			foreach (string text in unlocksWrapper2.GetAllIds())
			{
				string text2 = ((text == null) ? "<<null>>" : ("\"" + text + "\""));
				if (text2.ToLower().Contains(this.filterForUnlockIds.ToLower()))
				{
					ImGui.TableNextRow();
					ImGui.PushID(string.Format("ID_row_{0}", num++));
					ImGui.TableSetColumnIndex(0);
					ImGui.Text(text2);
					ImGui.TableSetColumnIndex(1);
					if (ImGui.Button("Copy"))
					{
						GUIUtility.systemCopyBuffer = text;
						global::Debug.Log("[Copied to clipboard] " + text);
					}
					ImGui.SameLine();
					if (ImGui.Button("Remove"))
					{
						unlocksWrapper2.RemoveId(text);
						global::Debug.Log("[Removed unlock id] " + text);
					}
					ImGui.PopID();
				}
			}
			ImGui.EndTable();
		}
	}

	// Token: 0x06001FE1 RID: 8161 RVA: 0x000AE1B4 File Offset: 0x000AC3B4
	private Option<DevToolUnlockedIds.UnlocksWrapper> GetUnlocks()
	{
		if (App.IsExiting)
		{
			return Option.None;
		}
		if (Game.Instance == null || !Game.Instance)
		{
			return Option.None;
		}
		if (Game.Instance.unlocks == null)
		{
			return Option.None;
		}
		return Option.Some<DevToolUnlockedIds.UnlocksWrapper>(new DevToolUnlockedIds.UnlocksWrapper(Game.Instance.unlocks));
	}

	// Token: 0x0400123D RID: 4669
	private string filterForUnlockIds = "";

	// Token: 0x0400123E RID: 4670
	private string unlockIdToAdd = "";

	// Token: 0x0200115E RID: 4446
	public readonly struct UnlocksWrapper
	{
		// Token: 0x06007646 RID: 30278 RVA: 0x002B7612 File Offset: 0x002B5812
		public UnlocksWrapper(Unlocks unlocks)
		{
			this.unlocks = unlocks;
		}

		// Token: 0x06007647 RID: 30279 RVA: 0x002B761B File Offset: 0x002B581B
		public void AddId(string unlockId)
		{
			this.unlocks.Unlock(unlockId, true);
		}

		// Token: 0x06007648 RID: 30280 RVA: 0x002B762A File Offset: 0x002B582A
		public void RemoveId(string unlockId)
		{
			this.unlocks.Lock(unlockId);
		}

		// Token: 0x06007649 RID: 30281 RVA: 0x002B7638 File Offset: 0x002B5838
		public IEnumerable<string> GetAllIds()
		{
			return from s in this.unlocks.GetAllUnlockedIds()
				orderby s
				select s;
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x0600764A RID: 30282 RVA: 0x002B7669 File Offset: 0x002B5869
		public int Count
		{
			get
			{
				return this.unlocks.GetAllUnlockedIds().Count;
			}
		}

		// Token: 0x04005AA3 RID: 23203
		public readonly Unlocks unlocks;
	}
}
