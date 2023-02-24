using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C04 RID: 3076
[AddComponentMenu("KMonoBehaviour/scripts/StarmapPlanet")]
public class StarmapPlanet : KMonoBehaviour
{
	// Token: 0x06006151 RID: 24913 RVA: 0x0023C8F4 File Offset: 0x0023AAF4
	public void SetSprite(Sprite sprite, Color color)
	{
		foreach (StarmapPlanetVisualizer starmapPlanetVisualizer in this.visualizers)
		{
			starmapPlanetVisualizer.image.sprite = sprite;
			starmapPlanetVisualizer.image.color = color;
		}
	}

	// Token: 0x06006152 RID: 24914 RVA: 0x0023C958 File Offset: 0x0023AB58
	public void SetFillAmount(float amount)
	{
		foreach (StarmapPlanetVisualizer starmapPlanetVisualizer in this.visualizers)
		{
			starmapPlanetVisualizer.image.fillAmount = amount;
		}
	}

	// Token: 0x06006153 RID: 24915 RVA: 0x0023C9B0 File Offset: 0x0023ABB0
	public void SetUnknownBGActive(bool active, Color color)
	{
		foreach (StarmapPlanetVisualizer starmapPlanetVisualizer in this.visualizers)
		{
			starmapPlanetVisualizer.unknownBG.gameObject.SetActive(active);
			starmapPlanetVisualizer.unknownBG.color = color;
		}
	}

	// Token: 0x06006154 RID: 24916 RVA: 0x0023CA18 File Offset: 0x0023AC18
	public void SetSelectionActive(bool active)
	{
		foreach (StarmapPlanetVisualizer starmapPlanetVisualizer in this.visualizers)
		{
			starmapPlanetVisualizer.selection.gameObject.SetActive(active);
		}
	}

	// Token: 0x06006155 RID: 24917 RVA: 0x0023CA74 File Offset: 0x0023AC74
	public void SetAnalysisActive(bool active)
	{
		foreach (StarmapPlanetVisualizer starmapPlanetVisualizer in this.visualizers)
		{
			starmapPlanetVisualizer.analysisSelection.SetActive(active);
		}
	}

	// Token: 0x06006156 RID: 24918 RVA: 0x0023CACC File Offset: 0x0023ACCC
	public void SetLabel(string text)
	{
		foreach (StarmapPlanetVisualizer starmapPlanetVisualizer in this.visualizers)
		{
			starmapPlanetVisualizer.label.text = text;
			this.ShowLabel(false);
		}
	}

	// Token: 0x06006157 RID: 24919 RVA: 0x0023CB2C File Offset: 0x0023AD2C
	public void ShowLabel(bool show)
	{
		foreach (StarmapPlanetVisualizer starmapPlanetVisualizer in this.visualizers)
		{
			starmapPlanetVisualizer.label.gameObject.SetActive(show);
		}
	}

	// Token: 0x06006158 RID: 24920 RVA: 0x0023CB88 File Offset: 0x0023AD88
	public void SetOnClick(System.Action del)
	{
		foreach (StarmapPlanetVisualizer starmapPlanetVisualizer in this.visualizers)
		{
			starmapPlanetVisualizer.button.onClick = del;
		}
	}

	// Token: 0x06006159 RID: 24921 RVA: 0x0023CBE0 File Offset: 0x0023ADE0
	public void SetOnEnter(System.Action del)
	{
		foreach (StarmapPlanetVisualizer starmapPlanetVisualizer in this.visualizers)
		{
			starmapPlanetVisualizer.button.onEnter = del;
		}
	}

	// Token: 0x0600615A RID: 24922 RVA: 0x0023CC38 File Offset: 0x0023AE38
	public void SetOnExit(System.Action del)
	{
		foreach (StarmapPlanetVisualizer starmapPlanetVisualizer in this.visualizers)
		{
			starmapPlanetVisualizer.button.onExit = del;
		}
	}

	// Token: 0x0600615B RID: 24923 RVA: 0x0023CC90 File Offset: 0x0023AE90
	public void AnimateSelector(float time)
	{
		foreach (StarmapPlanetVisualizer starmapPlanetVisualizer in this.visualizers)
		{
			starmapPlanetVisualizer.selection.anchoredPosition = new Vector2(0f, 25f + Mathf.Sin(time * 4f) * 5f);
		}
	}

	// Token: 0x0600615C RID: 24924 RVA: 0x0023CD08 File Offset: 0x0023AF08
	public void ShowAsCurrentRocketDestination(bool show)
	{
		foreach (StarmapPlanetVisualizer starmapPlanetVisualizer in this.visualizers)
		{
			RectTransform rectTransform = starmapPlanetVisualizer.rocketIconContainer.rectTransform();
			if (rectTransform.childCount > 0)
			{
				rectTransform.GetChild(rectTransform.childCount - 1).GetComponent<HierarchyReferences>().GetReference<Image>("fg")
					.color = (show ? new Color(0.11764706f, 0.8627451f, 0.3137255f) : Color.white);
			}
		}
	}

	// Token: 0x0600615D RID: 24925 RVA: 0x0023CDA8 File Offset: 0x0023AFA8
	public void SetRocketIcons(int numRockets, GameObject iconPrefab)
	{
		foreach (StarmapPlanetVisualizer starmapPlanetVisualizer in this.visualizers)
		{
			RectTransform rectTransform = starmapPlanetVisualizer.rocketIconContainer.rectTransform();
			for (int i = rectTransform.childCount; i < numRockets; i++)
			{
				Util.KInstantiateUI(iconPrefab, starmapPlanetVisualizer.rocketIconContainer, true);
			}
			for (int j = rectTransform.childCount; j > numRockets; j--)
			{
				UnityEngine.Object.Destroy(rectTransform.GetChild(j - 1).gameObject);
			}
			int num = 0;
			foreach (object obj in rectTransform)
			{
				((RectTransform)obj).anchoredPosition = new Vector2((float)num * -10f, 0f);
				num++;
			}
		}
	}

	// Token: 0x0400430F RID: 17167
	public List<StarmapPlanetVisualizer> visualizers;
}
