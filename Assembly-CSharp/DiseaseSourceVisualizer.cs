using System;
using Klei.AI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000723 RID: 1827
[AddComponentMenu("KMonoBehaviour/scripts/DiseaseSourceVisualizer")]
public class DiseaseSourceVisualizer : KMonoBehaviour
{
	// Token: 0x060031FB RID: 12795 RVA: 0x0010B931 File Offset: 0x00109B31
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.UpdateVisibility();
		Components.DiseaseSourceVisualizers.Add(this);
	}

	// Token: 0x060031FC RID: 12796 RVA: 0x0010B94C File Offset: 0x00109B4C
	protected override void OnCleanUp()
	{
		OverlayScreen instance = OverlayScreen.Instance;
		instance.OnOverlayChanged = (Action<HashedString>)Delegate.Remove(instance.OnOverlayChanged, new Action<HashedString>(this.OnViewModeChanged));
		base.OnCleanUp();
		Components.DiseaseSourceVisualizers.Remove(this);
		if (this.visualizer != null)
		{
			UnityEngine.Object.Destroy(this.visualizer);
			this.visualizer = null;
		}
	}

	// Token: 0x060031FD RID: 12797 RVA: 0x0010B9B0 File Offset: 0x00109BB0
	private void CreateVisualizer()
	{
		if (this.visualizer != null)
		{
			return;
		}
		if (GameScreenManager.Instance.worldSpaceCanvas == null)
		{
			return;
		}
		this.visualizer = Util.KInstantiate(Assets.UIPrefabs.ResourceVisualizer, GameScreenManager.Instance.worldSpaceCanvas, null);
	}

	// Token: 0x060031FE RID: 12798 RVA: 0x0010BA00 File Offset: 0x00109C00
	public void UpdateVisibility()
	{
		this.CreateVisualizer();
		if (string.IsNullOrEmpty(this.alwaysShowDisease))
		{
			this.visible = false;
		}
		else
		{
			Disease disease = Db.Get().Diseases.Get(this.alwaysShowDisease);
			if (disease != null)
			{
				this.SetVisibleDisease(disease);
			}
		}
		if (OverlayScreen.Instance != null)
		{
			this.Show(OverlayScreen.Instance.GetMode());
		}
	}

	// Token: 0x060031FF RID: 12799 RVA: 0x0010BA68 File Offset: 0x00109C68
	private void SetVisibleDisease(Disease disease)
	{
		Sprite overlaySprite = Assets.instance.DiseaseVisualization.overlaySprite;
		Color32 colorByName = GlobalAssets.Instance.colorSet.GetColorByName(disease.overlayColourName);
		Image component = this.visualizer.transform.GetChild(0).GetComponent<Image>();
		component.sprite = overlaySprite;
		component.color = colorByName;
		this.visible = true;
	}

	// Token: 0x06003200 RID: 12800 RVA: 0x0010BACA File Offset: 0x00109CCA
	private void Update()
	{
		if (this.visualizer == null)
		{
			return;
		}
		this.visualizer.transform.SetPosition(base.transform.GetPosition() + this.offset);
	}

	// Token: 0x06003201 RID: 12801 RVA: 0x0010BB02 File Offset: 0x00109D02
	private void OnViewModeChanged(HashedString mode)
	{
		this.Show(mode);
	}

	// Token: 0x06003202 RID: 12802 RVA: 0x0010BB0B File Offset: 0x00109D0B
	public void Show(HashedString mode)
	{
		base.enabled = this.visible && mode == OverlayModes.Disease.ID;
		if (this.visualizer != null)
		{
			this.visualizer.SetActive(base.enabled);
		}
	}

	// Token: 0x04001E58 RID: 7768
	[SerializeField]
	private Vector3 offset;

	// Token: 0x04001E59 RID: 7769
	private GameObject visualizer;

	// Token: 0x04001E5A RID: 7770
	private bool visible;

	// Token: 0x04001E5B RID: 7771
	public string alwaysShowDisease;
}
