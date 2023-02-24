using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using ImGuiNET;
using UnityEngine;

// Token: 0x02000519 RID: 1305
public class DevToolChoreDebugger : DevTool
{
	// Token: 0x06001F5E RID: 8030 RVA: 0x000A7004 File Offset: 0x000A5204
	protected override void RenderTo(DevPanel panel)
	{
		this.Update();
	}

	// Token: 0x06001F5F RID: 8031 RVA: 0x000A700C File Offset: 0x000A520C
	public void Update()
	{
		if (!Application.isPlaying || SelectTool.Instance == null || SelectTool.Instance.selected == null || SelectTool.Instance.selected.gameObject == null)
		{
			return;
		}
		GameObject gameObject = SelectTool.Instance.selected.gameObject;
		if (this.Consumer == null || (!this.lockSelection && this.selectedGameObject != gameObject))
		{
			this.Consumer = gameObject.GetComponent<ChoreConsumer>();
			this.selectedGameObject = gameObject;
		}
		if (this.Consumer != null)
		{
			ImGui.InputText("Filter:", ref this.filter, 256U);
			this.DisplayAvailableChores();
			ImGui.Text("");
		}
	}

	// Token: 0x06001F60 RID: 8032 RVA: 0x000A70D4 File Offset: 0x000A52D4
	private void DisplayAvailableChores()
	{
		ImGui.Checkbox("Lock selection", ref this.lockSelection);
		ImGui.Checkbox("Show Last Successful Chore Selection", ref this.showLastSuccessfulPreconditionSnapshot);
		ImGui.Text("Available Chores:");
		ChoreConsumer.PreconditionSnapshot preconditionSnapshot = this.Consumer.GetLastPreconditionSnapshot();
		if (this.showLastSuccessfulPreconditionSnapshot)
		{
			preconditionSnapshot = this.Consumer.GetLastSuccessfulPreconditionSnapshot();
		}
		this.ShowChores(preconditionSnapshot);
	}

	// Token: 0x06001F61 RID: 8033 RVA: 0x000A7134 File Offset: 0x000A5334
	private void ShowChores(ChoreConsumer.PreconditionSnapshot target_snapshot)
	{
		ImGuiTableFlags imGuiTableFlags = ImGuiTableFlags.RowBg | ImGuiTableFlags.BordersInnerH | ImGuiTableFlags.BordersOuterH | ImGuiTableFlags.BordersInnerV | ImGuiTableFlags.BordersOuterV | ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.ScrollX | ImGuiTableFlags.ScrollY;
		this.rowIndex = 0;
		if (ImGui.BeginTable("Available Chores", this.columns.Count, imGuiTableFlags))
		{
			foreach (object obj in this.columns.Keys)
			{
				ImGui.TableSetupColumn(obj.ToString(), ImGuiTableColumnFlags.WidthFixed);
			}
			ImGui.TableHeadersRow();
			for (int i = target_snapshot.succeededContexts.Count - 1; i >= 0; i--)
			{
				this.ShowContext(target_snapshot.succeededContexts[i]);
			}
			if (target_snapshot.doFailedContextsNeedSorting)
			{
				target_snapshot.failedContexts.Sort();
				target_snapshot.doFailedContextsNeedSorting = false;
			}
			for (int j = target_snapshot.failedContexts.Count - 1; j >= 0; j--)
			{
				this.ShowContext(target_snapshot.failedContexts[j]);
			}
			ImGui.EndTable();
		}
	}

	// Token: 0x06001F62 RID: 8034 RVA: 0x000A7238 File Offset: 0x000A5438
	private void ShowContext(Chore.Precondition.Context context)
	{
		string text = "";
		Chore chore = context.chore;
		if (!context.IsSuccess())
		{
			text = context.chore.GetPreconditions()[context.failedPreconditionId].id;
		}
		string text2 = "";
		if (chore.driver != null)
		{
			text2 = chore.driver.name;
		}
		string text3 = "";
		if (chore.overrideTarget != null)
		{
			text3 = chore.overrideTarget.name;
		}
		string text4 = "";
		if (!chore.isNull)
		{
			text4 = chore.gameObject.name;
		}
		if (Chore.Precondition.Context.ShouldFilter(this.filter, chore.GetType().ToString()) && Chore.Precondition.Context.ShouldFilter(this.filter, chore.choreType.Id) && Chore.Precondition.Context.ShouldFilter(this.filter, text) && Chore.Precondition.Context.ShouldFilter(this.filter, text2) && Chore.Precondition.Context.ShouldFilter(this.filter, text3) && Chore.Precondition.Context.ShouldFilter(this.filter, text4))
		{
			return;
		}
		this.columns["BP"] = chore.debug;
		this.columns["Id"] = chore.id.ToString();
		this.columns["Class"] = chore.GetType().ToString().Replace("`1", "");
		this.columns["Type"] = chore.choreType.Id;
		this.columns["PriorityClass"] = context.masterPriority.priority_class.ToString();
		this.columns["PersonalPriority"] = context.personalPriority.ToString();
		this.columns["PriorityValue"] = context.masterPriority.priority_value.ToString();
		this.columns["Priority"] = context.priority.ToString();
		this.columns["PriorityMod"] = context.priorityMod.ToString();
		this.columns["ConsumerPriority"] = context.consumerPriority.ToString();
		this.columns["Cost"] = context.cost.ToString();
		this.columns["Interrupt"] = context.interruptPriority.ToString();
		this.columns["Precondition"] = text;
		this.columns["Override"] = text3;
		this.columns["Assigned To"] = text2;
		this.columns["Owner"] = text4;
		this.columns["Details"] = "";
		ImGui.TableNextRow();
		string text5 = "ID_row_{0}";
		int num = this.rowIndex;
		this.rowIndex = num + 1;
		ImGui.PushID(string.Format(text5, num));
		int i = 0;
		ImGui.PushID("debug");
		ImGui.TableSetColumnIndex(i++);
		ImGui.Checkbox("", ref chore.debug);
		ImGui.PopID();
		while (i < this.columns.Count)
		{
			ImGui.TableSetColumnIndex(i);
			ImGui.Text(this.columns[i].ToString());
			i++;
		}
		ImGui.PopID();
	}

	// Token: 0x06001F63 RID: 8035 RVA: 0x000A759E File Offset: 0x000A579E
	public void ConsumerDebugDisplayLog()
	{
	}

	// Token: 0x040011C5 RID: 4549
	private string filter = "";

	// Token: 0x040011C6 RID: 4550
	private bool showLastSuccessfulPreconditionSnapshot;

	// Token: 0x040011C7 RID: 4551
	private bool lockSelection;

	// Token: 0x040011C8 RID: 4552
	private ChoreConsumer Consumer;

	// Token: 0x040011C9 RID: 4553
	private GameObject selectedGameObject;

	// Token: 0x040011CA RID: 4554
	private OrderedDictionary columns = new OrderedDictionary
	{
		{ "BP", "" },
		{ "Id", "" },
		{ "Class", "" },
		{ "Type", "" },
		{ "PriorityClass", "" },
		{ "PersonalPriority", "" },
		{ "PriorityValue", "" },
		{ "Priority", "" },
		{ "PriorityMod", "" },
		{ "ConsumerPriority", "" },
		{ "Cost", "" },
		{ "Interrupt", "" },
		{ "Precondition", "" },
		{ "Override", "" },
		{ "Assigned To", "" },
		{ "Owner", "" },
		{ "Details", "" }
	};

	// Token: 0x040011CB RID: 4555
	private int rowIndex;

	// Token: 0x0200114D RID: 4429
	public class EditorPreconditionSnapshot
	{
		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06007611 RID: 30225 RVA: 0x002B724E File Offset: 0x002B544E
		// (set) Token: 0x06007612 RID: 30226 RVA: 0x002B7256 File Offset: 0x002B5456
		public List<DevToolChoreDebugger.EditorPreconditionSnapshot.EditorContext> SucceededContexts { get; set; }

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06007613 RID: 30227 RVA: 0x002B725F File Offset: 0x002B545F
		// (set) Token: 0x06007614 RID: 30228 RVA: 0x002B7267 File Offset: 0x002B5467
		public List<DevToolChoreDebugger.EditorPreconditionSnapshot.EditorContext> FailedContexts { get; set; }

		// Token: 0x02001F7E RID: 8062
		public struct EditorContext
		{
			// Token: 0x170009FB RID: 2555
			// (get) Token: 0x06009F19 RID: 40729 RVA: 0x0034031E File Offset: 0x0033E51E
			// (set) Token: 0x06009F1A RID: 40730 RVA: 0x00340326 File Offset: 0x0033E526
			public string Chore { readonly get; set; }

			// Token: 0x170009FC RID: 2556
			// (get) Token: 0x06009F1B RID: 40731 RVA: 0x0034032F File Offset: 0x0033E52F
			// (set) Token: 0x06009F1C RID: 40732 RVA: 0x00340337 File Offset: 0x0033E537
			public string ChoreType { readonly get; set; }

			// Token: 0x170009FD RID: 2557
			// (get) Token: 0x06009F1D RID: 40733 RVA: 0x00340340 File Offset: 0x0033E540
			// (set) Token: 0x06009F1E RID: 40734 RVA: 0x00340348 File Offset: 0x0033E548
			public string FailedPrecondition { readonly get; set; }

			// Token: 0x170009FE RID: 2558
			// (get) Token: 0x06009F1F RID: 40735 RVA: 0x00340351 File Offset: 0x0033E551
			// (set) Token: 0x06009F20 RID: 40736 RVA: 0x00340359 File Offset: 0x0033E559
			public int WorldId { readonly get; set; }
		}
	}
}
