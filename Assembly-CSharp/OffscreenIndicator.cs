using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000872 RID: 2162
[AddComponentMenu("KMonoBehaviour/scripts/OffscreenIndicator")]
public class OffscreenIndicator : KMonoBehaviour
{
	// Token: 0x06003E26 RID: 15910 RVA: 0x0015A8FB File Offset: 0x00158AFB
	protected override void OnSpawn()
	{
		base.OnSpawn();
		OffscreenIndicator.Instance = this;
	}

	// Token: 0x06003E27 RID: 15911 RVA: 0x0015A909 File Offset: 0x00158B09
	protected override void OnForcedCleanUp()
	{
		OffscreenIndicator.Instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x06003E28 RID: 15912 RVA: 0x0015A918 File Offset: 0x00158B18
	private void Update()
	{
		foreach (KeyValuePair<GameObject, GameObject> keyValuePair in this.targets)
		{
			this.UpdateArrow(keyValuePair.Value, keyValuePair.Key);
		}
	}

	// Token: 0x06003E29 RID: 15913 RVA: 0x0015A978 File Offset: 0x00158B78
	public void ActivateIndicator(GameObject target)
	{
		if (!this.targets.ContainsKey(target))
		{
			global::Tuple<Sprite, Color> uisprite = Def.GetUISprite(target, "ui", false);
			if (uisprite != null)
			{
				this.ActivateIndicator(target, uisprite);
			}
		}
	}

	// Token: 0x06003E2A RID: 15914 RVA: 0x0015A9AC File Offset: 0x00158BAC
	public void ActivateIndicator(GameObject target, GameObject iconSource)
	{
		if (!this.targets.ContainsKey(target))
		{
			MinionIdentity component = iconSource.GetComponent<MinionIdentity>();
			if (component != null)
			{
				GameObject gameObject = Util.KInstantiateUI(this.IndicatorPrefab, this.IndicatorContainer, true);
				gameObject.GetComponent<HierarchyReferences>().GetReference<Image>("icon").gameObject.SetActive(false);
				CrewPortrait reference = gameObject.GetComponent<HierarchyReferences>().GetReference<CrewPortrait>("Portrait");
				reference.gameObject.SetActive(true);
				reference.SetIdentityObject(component, true);
				this.targets.Add(target, gameObject);
			}
		}
	}

	// Token: 0x06003E2B RID: 15915 RVA: 0x0015AA38 File Offset: 0x00158C38
	public void ActivateIndicator(GameObject target, global::Tuple<Sprite, Color> icon)
	{
		if (!this.targets.ContainsKey(target))
		{
			GameObject gameObject = Util.KInstantiateUI(this.IndicatorPrefab, this.IndicatorContainer, true);
			Image reference = gameObject.GetComponent<HierarchyReferences>().GetReference<Image>("icon");
			if (icon != null)
			{
				reference.sprite = icon.first;
				reference.color = icon.second;
				this.targets.Add(target, gameObject);
			}
		}
	}

	// Token: 0x06003E2C RID: 15916 RVA: 0x0015AA9F File Offset: 0x00158C9F
	public void DeactivateIndicator(GameObject target)
	{
		if (this.targets.ContainsKey(target))
		{
			UnityEngine.Object.Destroy(this.targets[target]);
			this.targets.Remove(target);
		}
	}

	// Token: 0x06003E2D RID: 15917 RVA: 0x0015AAD0 File Offset: 0x00158CD0
	private void UpdateArrow(GameObject arrow, GameObject target)
	{
		if (target == null)
		{
			UnityEngine.Object.Destroy(arrow);
			this.targets.Remove(target);
			return;
		}
		Vector3 vector = Camera.main.WorldToViewportPoint(target.transform.position);
		if ((double)vector.x > 0.3 && (double)vector.x < 0.7 && (double)vector.y > 0.3 && (double)vector.y < 0.7)
		{
			arrow.GetComponent<HierarchyReferences>().GetReference<CrewPortrait>("Portrait").SetIdentityObject(null, true);
			arrow.SetActive(false);
			return;
		}
		arrow.SetActive(true);
		arrow.rectTransform().SetLocalPosition(Vector3.zero);
		Vector3 vector2 = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
		vector2.z = target.transform.position.z;
		Vector3 normalized = (target.transform.position - vector2).normalized;
		arrow.transform.up = normalized;
		this.UpdateTargetIconPosition(target, arrow);
	}

	// Token: 0x06003E2E RID: 15918 RVA: 0x0015ABF4 File Offset: 0x00158DF4
	private void UpdateTargetIconPosition(GameObject goTarget, GameObject indicator)
	{
		Vector3 vector = goTarget.transform.position;
		vector = Camera.main.WorldToViewportPoint(vector);
		if (vector.z < 0f)
		{
			vector.x = 1f - vector.x;
			vector.y = 1f - vector.y;
			vector.z = 0f;
			vector = this.Vector3Maxamize(vector);
		}
		vector = Camera.main.ViewportToScreenPoint(vector);
		vector.x = Mathf.Clamp(vector.x, this.edgeInset, (float)Screen.width - this.edgeInset);
		vector.y = Mathf.Clamp(vector.y, this.edgeInset, (float)Screen.height - this.edgeInset);
		indicator.transform.position = vector;
		indicator.GetComponent<HierarchyReferences>().GetReference<Image>("icon").rectTransform.up = Vector3.up;
		indicator.GetComponent<HierarchyReferences>().GetReference<CrewPortrait>("Portrait").transform.up = Vector3.up;
	}

	// Token: 0x06003E2F RID: 15919 RVA: 0x0015AD00 File Offset: 0x00158F00
	public Vector3 Vector3Maxamize(Vector3 vector)
	{
		float num = 0f;
		num = ((vector.x > num) ? vector.x : num);
		num = ((vector.y > num) ? vector.y : num);
		num = ((vector.z > num) ? vector.z : num);
		return vector / num;
	}

	// Token: 0x040028AA RID: 10410
	public GameObject IndicatorPrefab;

	// Token: 0x040028AB RID: 10411
	public GameObject IndicatorContainer;

	// Token: 0x040028AC RID: 10412
	private Dictionary<GameObject, GameObject> targets = new Dictionary<GameObject, GameObject>();

	// Token: 0x040028AD RID: 10413
	public static OffscreenIndicator Instance;

	// Token: 0x040028AE RID: 10414
	[SerializeField]
	private float edgeInset = 25f;
}
