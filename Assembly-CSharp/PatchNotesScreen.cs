using System;
using UnityEngine;

// Token: 0x02000A16 RID: 2582
public class PatchNotesScreen : KModalScreen
{
	// Token: 0x06004DDB RID: 19931 RVA: 0x001B7CD8 File Offset: 0x001B5ED8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.changesLabel.text = PatchNotesScreen.m_patchNotesText;
		this.closeButton.onClick += this.MarkAsReadAndClose;
		this.closeButton.soundPlayer.widget_sound_events()[0].OverrideAssetName = "HUD_Click_Close";
		this.okButton.onClick += this.MarkAsReadAndClose;
		this.previousVersion.onClick += delegate
		{
			App.OpenWebURL("http://support.kleientertainment.com/customer/portal/articles/2776550");
		};
		this.fullPatchNotes.onClick += this.OnPatchNotesClick;
		PatchNotesScreen.instance = this;
	}

	// Token: 0x06004DDC RID: 19932 RVA: 0x001B7D90 File Offset: 0x001B5F90
	protected override void OnCleanUp()
	{
		PatchNotesScreen.instance = null;
	}

	// Token: 0x06004DDD RID: 19933 RVA: 0x001B7D98 File Offset: 0x001B5F98
	public static bool ShouldShowScreen()
	{
		return KPlayerPrefs.GetInt("PatchNotesVersion") < PatchNotesScreen.PatchNotesVersion;
	}

	// Token: 0x06004DDE RID: 19934 RVA: 0x001B7DAB File Offset: 0x001B5FAB
	private void MarkAsReadAndClose()
	{
		KPlayerPrefs.SetInt("PatchNotesVersion", PatchNotesScreen.PatchNotesVersion);
		this.Deactivate();
	}

	// Token: 0x06004DDF RID: 19935 RVA: 0x001B7DC2 File Offset: 0x001B5FC2
	public static void UpdatePatchNotes(string patchNotesSummary, string url)
	{
		PatchNotesScreen.m_patchNotesUrl = url;
		PatchNotesScreen.m_patchNotesText = patchNotesSummary;
		if (PatchNotesScreen.instance != null)
		{
			PatchNotesScreen.instance.changesLabel.text = PatchNotesScreen.m_patchNotesText;
		}
	}

	// Token: 0x06004DE0 RID: 19936 RVA: 0x001B7DF1 File Offset: 0x001B5FF1
	private void OnPatchNotesClick()
	{
		App.OpenWebURL(PatchNotesScreen.m_patchNotesUrl);
	}

	// Token: 0x06004DE1 RID: 19937 RVA: 0x001B7DFD File Offset: 0x001B5FFD
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight))
		{
			this.MarkAsReadAndClose();
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x0400336F RID: 13167
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04003370 RID: 13168
	[SerializeField]
	private KButton okButton;

	// Token: 0x04003371 RID: 13169
	[SerializeField]
	private KButton fullPatchNotes;

	// Token: 0x04003372 RID: 13170
	[SerializeField]
	private KButton previousVersion;

	// Token: 0x04003373 RID: 13171
	[SerializeField]
	private LocText changesLabel;

	// Token: 0x04003374 RID: 13172
	private static string m_patchNotesUrl;

	// Token: 0x04003375 RID: 13173
	private static string m_patchNotesText;

	// Token: 0x04003376 RID: 13174
	private static int PatchNotesVersion = 9;

	// Token: 0x04003377 RID: 13175
	private static PatchNotesScreen instance;
}
