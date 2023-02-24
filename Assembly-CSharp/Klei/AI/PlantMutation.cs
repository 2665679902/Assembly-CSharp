using System;
using System.Collections.Generic;
using System.Text;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000DA1 RID: 3489
	public class PlantMutation : Modifier
	{
		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06006A3E RID: 27198 RVA: 0x0029413A File Offset: 0x0029233A
		public List<string> AdditionalSoundEvents
		{
			get
			{
				return this.additionalSoundEvents;
			}
		}

		// Token: 0x06006A3F RID: 27199 RVA: 0x00294144 File Offset: 0x00292344
		public PlantMutation(string id, string name, string desc)
			: base(id, name, desc)
		{
		}

		// Token: 0x06006A40 RID: 27200 RVA: 0x002941C8 File Offset: 0x002923C8
		public void ApplyTo(MutantPlant target)
		{
			this.ApplyFunctionalTo(target);
			if (!target.HasTag(GameTags.Seed) && !target.HasTag(GameTags.CropSeed) && !target.HasTag(GameTags.Compostable))
			{
				this.ApplyVisualTo(target);
			}
		}

		// Token: 0x06006A41 RID: 27201 RVA: 0x00294200 File Offset: 0x00292400
		private void ApplyFunctionalTo(MutantPlant target)
		{
			SeedProducer component = target.GetComponent<SeedProducer>();
			if (component != null && component.seedInfo.productionType == SeedProducer.ProductionType.Harvest)
			{
				component.Configure(component.seedInfo.seedId, SeedProducer.ProductionType.Sterile, 0);
			}
			if (this.bonusCropID.IsValid)
			{
				target.Subscribe(-1072826864, new Action<object>(this.OnHarvestBonusCrop));
			}
			if (!this.forcePrefersDarkness)
			{
				if (this.SelfModifiers.Find((AttributeModifier m) => m.AttributeId == Db.Get().PlantAttributes.MinLightLux.Id) == null)
				{
					goto IL_F0;
				}
			}
			IlluminationVulnerable illuminationVulnerable = target.GetComponent<IlluminationVulnerable>();
			if (illuminationVulnerable == null)
			{
				illuminationVulnerable = target.gameObject.AddComponent<IlluminationVulnerable>();
			}
			if (this.forcePrefersDarkness)
			{
				if (illuminationVulnerable != null)
				{
					illuminationVulnerable.SetPrefersDarkness(true);
				}
			}
			else
			{
				if (illuminationVulnerable != null)
				{
					illuminationVulnerable.SetPrefersDarkness(false);
				}
				target.GetComponent<Modifiers>().attributes.Add(Db.Get().PlantAttributes.MinLightLux);
			}
			IL_F0:
			byte b = this.droppedDiseaseID;
			if (this.harvestDiseaseID != 255)
			{
				target.Subscribe(35625290, new Action<object>(this.OnCropSpawnedAddDisease));
			}
			bool isValid = this.ensureIrrigationInfo.tag.IsValid;
			Attributes attributes = target.GetAttributes();
			this.AddTo(attributes);
		}

		// Token: 0x06006A42 RID: 27202 RVA: 0x00294350 File Offset: 0x00292550
		private void ApplyVisualTo(MutantPlant target)
		{
			KBatchedAnimController component = target.GetComponent<KBatchedAnimController>();
			if (this.symbolOverrideInfo != null && this.symbolOverrideInfo.Count > 0)
			{
				SymbolOverrideController component2 = target.GetComponent<SymbolOverrideController>();
				if (component2 != null)
				{
					foreach (PlantMutation.SymbolOverrideInfo symbolOverrideInfo in this.symbolOverrideInfo)
					{
						KAnim.Build.Symbol symbol = Assets.GetAnim(symbolOverrideInfo.sourceAnim).GetData().build.GetSymbol(symbolOverrideInfo.sourceSymbol);
						component2.AddSymbolOverride(symbolOverrideInfo.targetSymbolName, symbol, 0);
					}
				}
			}
			if (this.bGFXAnim != null)
			{
				PlantMutation.CreateFXObject(target, this.bGFXAnim, "_BGFX", 0.1f);
			}
			if (this.fGFXAnim != null)
			{
				PlantMutation.CreateFXObject(target, this.fGFXAnim, "_FGFX", -0.1f);
			}
			if (this.plantTint != Color.white)
			{
				component.TintColour = this.plantTint;
			}
			if (this.symbolTints.Count > 0)
			{
				for (int i = 0; i < this.symbolTints.Count; i++)
				{
					component.SetSymbolTint(this.symbolTintTargets[i], this.symbolTints[i]);
				}
			}
			if (this.symbolScales.Count > 0)
			{
				for (int j = 0; j < this.symbolScales.Count; j++)
				{
					component.SetSymbolScale(this.symbolScaleTargets[j], this.symbolScales[j]);
				}
			}
			if (this.additionalSoundEvents.Count > 0)
			{
				for (int k = 0; k < this.additionalSoundEvents.Count; k++)
				{
				}
			}
		}

		// Token: 0x06006A43 RID: 27203 RVA: 0x00294534 File Offset: 0x00292734
		private static void CreateFXObject(MutantPlant target, string anim, string nameSuffix, float offset)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Assets.GetPrefab(SimpleFXConfig.ID));
			gameObject.name = target.name + nameSuffix;
			gameObject.transform.parent = target.transform;
			gameObject.AddComponent<LoopingSounds>();
			gameObject.GetComponent<KPrefabID>().PrefabTag = new Tag(gameObject.name);
			Extents extents = target.GetComponent<OccupyArea>().GetExtents();
			Vector3 position = target.transform.GetPosition();
			position.x = (float)extents.x + (float)extents.width / 2f;
			position.y = (float)extents.y + (float)extents.height / 2f;
			position.z += offset;
			gameObject.transform.SetPosition(position);
			KBatchedAnimController component = gameObject.GetComponent<KBatchedAnimController>();
			component.AnimFiles = new KAnimFile[] { Assets.GetAnim(anim) };
			component.initialAnim = "idle";
			component.initialMode = KAnim.PlayMode.Loop;
			component.randomiseLoopedOffset = true;
			component.fgLayer = Grid.SceneLayer.NoLayer;
			if (target.HasTag(GameTags.Hanging))
			{
				component.Rotation = 180f;
			}
			gameObject.SetActive(true);
		}

		// Token: 0x06006A44 RID: 27204 RVA: 0x00294663 File Offset: 0x00292863
		private void OnHarvestBonusCrop(object data)
		{
			((Crop)data).SpawnSomeFruit(this.bonusCropID, this.bonusCropAmount);
		}

		// Token: 0x06006A45 RID: 27205 RVA: 0x0029467C File Offset: 0x0029287C
		private void OnCropSpawnedAddDisease(object data)
		{
			((GameObject)data).GetComponent<PrimaryElement>().AddDisease(this.harvestDiseaseID, this.harvestDiseaseAmount, this.Name);
		}

		// Token: 0x06006A46 RID: 27206 RVA: 0x002946A0 File Offset: 0x002928A0
		public string GetTooltip()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.desc);
			foreach (AttributeModifier attributeModifier in this.SelfModifiers)
			{
				Attribute attribute = Db.Get().Attributes.TryGet(attributeModifier.AttributeId);
				if (attribute == null)
				{
					attribute = Db.Get().PlantAttributes.Get(attributeModifier.AttributeId);
				}
				if (attribute.ShowInUI != Attribute.Display.Never)
				{
					stringBuilder.Append(DUPLICANTS.TRAITS.TRAIT_DESCRIPTION_LIST_ENTRY);
					stringBuilder.Append(string.Format(DUPLICANTS.TRAITS.ATTRIBUTE_MODIFIERS, attribute.Name, attributeModifier.GetFormattedString()));
				}
			}
			if (this.bonusCropID != null)
			{
				string text;
				if (GameTags.DisplayAsCalories.Contains(this.bonusCropID))
				{
					EdiblesManager.FoodInfo foodInfo = EdiblesManager.GetFoodInfo(this.bonusCropID.Name);
					DebugUtil.Assert(foodInfo != null, "Eeh? Trying to spawn a bonus crop that is caloric but isn't a food??", this.bonusCropID.ToString());
					text = GameUtil.GetFormattedCalories(this.bonusCropAmount * foodInfo.CaloriesPerUnit, GameUtil.TimeSlice.None, true);
				}
				else if (GameTags.DisplayAsUnits.Contains(this.bonusCropID))
				{
					text = GameUtil.GetFormattedUnits(this.bonusCropAmount, GameUtil.TimeSlice.None, false, "");
				}
				else
				{
					text = GameUtil.GetFormattedMass(this.bonusCropAmount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}");
				}
				stringBuilder.Append(DUPLICANTS.TRAITS.TRAIT_DESCRIPTION_LIST_ENTRY);
				stringBuilder.Append(CREATURES.PLANT_MUTATIONS.BONUS_CROP_FMT.Replace("{Crop}", this.bonusCropID.ProperName()).Replace("{Amount}", text));
			}
			if (this.droppedDiseaseID != 255)
			{
				if (this.droppedDiseaseOnGrowAmount > 0)
				{
					stringBuilder.Append(DUPLICANTS.TRAITS.TRAIT_DESCRIPTION_LIST_ENTRY);
					stringBuilder.Append(UI.UISIDESCREENS.PLANTERSIDESCREEN.DISEASE_DROPPER_BURST.Replace("{Disease}", GameUtil.GetFormattedDiseaseName(this.droppedDiseaseID, false)).Replace("{DiseaseAmount}", GameUtil.GetFormattedDiseaseAmount(this.droppedDiseaseOnGrowAmount, GameUtil.TimeSlice.None)));
				}
				if (this.droppedDiseaseContinuousAmount > 0)
				{
					stringBuilder.Append(DUPLICANTS.TRAITS.TRAIT_DESCRIPTION_LIST_ENTRY);
					stringBuilder.Append(UI.UISIDESCREENS.PLANTERSIDESCREEN.DISEASE_DROPPER_CONSTANT.Replace("{Disease}", GameUtil.GetFormattedDiseaseName(this.droppedDiseaseID, false)).Replace("{DiseaseAmount}", GameUtil.GetFormattedDiseaseAmount(this.droppedDiseaseContinuousAmount, GameUtil.TimeSlice.PerSecond)));
				}
			}
			if (this.harvestDiseaseID != 255)
			{
				stringBuilder.Append(DUPLICANTS.TRAITS.TRAIT_DESCRIPTION_LIST_ENTRY);
				stringBuilder.Append(UI.UISIDESCREENS.PLANTERSIDESCREEN.DISEASE_ON_HARVEST.Replace("{Disease}", GameUtil.GetFormattedDiseaseName(this.harvestDiseaseID, false)).Replace("{DiseaseAmount}", GameUtil.GetFormattedDiseaseAmount(this.harvestDiseaseAmount, GameUtil.TimeSlice.None)));
			}
			if (this.forcePrefersDarkness)
			{
				stringBuilder.Append(DUPLICANTS.TRAITS.TRAIT_DESCRIPTION_LIST_ENTRY);
				stringBuilder.Append(UI.GAMEOBJECTEFFECTS.REQUIRES_DARKNESS);
			}
			if (this.forceSelfHarvestOnGrown)
			{
				stringBuilder.Append(DUPLICANTS.TRAITS.TRAIT_DESCRIPTION_LIST_ENTRY);
				stringBuilder.Append(UI.UISIDESCREENS.PLANTERSIDESCREEN.AUTO_SELF_HARVEST);
			}
			if (this.ensureIrrigationInfo.tag.IsValid)
			{
				stringBuilder.Append(DUPLICANTS.TRAITS.TRAIT_DESCRIPTION_LIST_ENTRY);
				stringBuilder.Append(string.Format(UI.GAMEOBJECTEFFECTS.IDEAL_FERTILIZER, this.ensureIrrigationInfo.tag.ProperName(), GameUtil.GetFormattedMass(-this.ensureIrrigationInfo.massConsumptionRate, GameUtil.TimeSlice.PerCycle, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), true));
			}
			if (!this.originalMutation)
			{
				stringBuilder.Append(DUPLICANTS.TRAITS.TRAIT_DESCRIPTION_LIST_ENTRY);
				stringBuilder.Append(UI.GAMEOBJECTEFFECTS.MUTANT_STERILE);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006A47 RID: 27207 RVA: 0x00294A50 File Offset: 0x00292C50
		public void GetDescriptors(ref List<Descriptor> descriptors, GameObject go)
		{
			if (this.harvestDiseaseID != 255)
			{
				descriptors.Add(new Descriptor(UI.UISIDESCREENS.PLANTERSIDESCREEN.DISEASE_ON_HARVEST.Replace("{Disease}", GameUtil.GetFormattedDiseaseName(this.harvestDiseaseID, false)).Replace("{DiseaseAmount}", GameUtil.GetFormattedDiseaseAmount(this.harvestDiseaseAmount, GameUtil.TimeSlice.None)), UI.UISIDESCREENS.PLANTERSIDESCREEN.TOOLTIPS.DISEASE_ON_HARVEST.Replace("{Disease}", GameUtil.GetFormattedDiseaseName(this.harvestDiseaseID, false)).Replace("{DiseaseAmount}", GameUtil.GetFormattedDiseaseAmount(this.harvestDiseaseAmount, GameUtil.TimeSlice.None)), Descriptor.DescriptorType.Effect, false));
			}
			if (this.forceSelfHarvestOnGrown)
			{
				descriptors.Add(new Descriptor(UI.UISIDESCREENS.PLANTERSIDESCREEN.AUTO_SELF_HARVEST, UI.UISIDESCREENS.PLANTERSIDESCREEN.TOOLTIPS.AUTO_SELF_HARVEST, Descriptor.DescriptorType.Effect, false));
			}
		}

		// Token: 0x06006A48 RID: 27208 RVA: 0x00294B04 File Offset: 0x00292D04
		public PlantMutation Original()
		{
			this.originalMutation = true;
			return this;
		}

		// Token: 0x06006A49 RID: 27209 RVA: 0x00294B0E File Offset: 0x00292D0E
		public PlantMutation RequiredPrefabID(string requiredID)
		{
			this.requiredPrefabIDs.Add(requiredID);
			return this;
		}

		// Token: 0x06006A4A RID: 27210 RVA: 0x00294B1D File Offset: 0x00292D1D
		public PlantMutation RestrictPrefabID(string restrictedID)
		{
			this.restrictedPrefabIDs.Add(restrictedID);
			return this;
		}

		// Token: 0x06006A4B RID: 27211 RVA: 0x00294B2C File Offset: 0x00292D2C
		public PlantMutation AttributeModifier(Attribute attribute, float amount, bool multiplier = false)
		{
			DebugUtil.Assert(!this.forcePrefersDarkness || attribute != Db.Get().PlantAttributes.MinLightLux, "A plant mutation has both darkness and light defined!", this.Id);
			base.Add(new AttributeModifier(attribute.Id, amount, this.Name, multiplier, false, true));
			return this;
		}

		// Token: 0x06006A4C RID: 27212 RVA: 0x00294B85 File Offset: 0x00292D85
		public PlantMutation BonusCrop(Tag cropPrefabID, float bonucCropAmount)
		{
			this.bonusCropID = cropPrefabID;
			this.bonusCropAmount = bonucCropAmount;
			return this;
		}

		// Token: 0x06006A4D RID: 27213 RVA: 0x00294B96 File Offset: 0x00292D96
		public PlantMutation DiseaseDropper(byte diseaseID, int onGrowAmount, int continuousAmount)
		{
			this.droppedDiseaseID = diseaseID;
			this.droppedDiseaseOnGrowAmount = onGrowAmount;
			this.droppedDiseaseContinuousAmount = continuousAmount;
			return this;
		}

		// Token: 0x06006A4E RID: 27214 RVA: 0x00294BAE File Offset: 0x00292DAE
		public PlantMutation AddDiseaseToHarvest(byte diseaseID, int amount)
		{
			this.harvestDiseaseID = diseaseID;
			this.harvestDiseaseAmount = amount;
			return this;
		}

		// Token: 0x06006A4F RID: 27215 RVA: 0x00294BC0 File Offset: 0x00292DC0
		public PlantMutation ForcePrefersDarkness()
		{
			DebugUtil.Assert(this.SelfModifiers.Find((AttributeModifier m) => m.AttributeId == Db.Get().PlantAttributes.MinLightLux.Id) == null, "A plant mutation has both darkness and light defined!", this.Id);
			this.forcePrefersDarkness = true;
			return this;
		}

		// Token: 0x06006A50 RID: 27216 RVA: 0x00294C12 File Offset: 0x00292E12
		public PlantMutation ForceSelfHarvestOnGrown()
		{
			this.forceSelfHarvestOnGrown = true;
			this.AttributeModifier(Db.Get().Amounts.OldAge.maxAttribute, -0.999999f, true);
			return this;
		}

		// Token: 0x06006A51 RID: 27217 RVA: 0x00294C3D File Offset: 0x00292E3D
		public PlantMutation EnsureIrrigated(PlantElementAbsorber.ConsumeInfo consumeInfo)
		{
			this.ensureIrrigationInfo = consumeInfo;
			return this;
		}

		// Token: 0x06006A52 RID: 27218 RVA: 0x00294C48 File Offset: 0x00292E48
		public PlantMutation VisualTint(float r, float g, float b)
		{
			global::Debug.Assert(Mathf.Sign(r) == Mathf.Sign(g) && Mathf.Sign(r) == Mathf.Sign(b), "Vales for tints must be all positive or all negative for the shader to work correctly!");
			if (r < 0f)
			{
				this.plantTint = Color.white + new Color(r, g, b, 0f);
			}
			else
			{
				this.plantTint = new Color(r, g, b, 0f);
			}
			return this;
		}

		// Token: 0x06006A53 RID: 27219 RVA: 0x00294CBC File Offset: 0x00292EBC
		public PlantMutation VisualSymbolTint(string targetSymbolName, float r, float g, float b)
		{
			global::Debug.Assert(Mathf.Sign(r) == Mathf.Sign(g) && Mathf.Sign(r) == Mathf.Sign(b), "Vales for tints must be all positive or all negative for the shader to work correctly!");
			this.symbolTintTargets.Add(targetSymbolName);
			this.symbolTints.Add(Color.white + new Color(r, g, b, 0f));
			return this;
		}

		// Token: 0x06006A54 RID: 27220 RVA: 0x00294D23 File Offset: 0x00292F23
		public PlantMutation VisualSymbolOverride(string targetSymbolName, string sourceAnim, string sourceSymbol)
		{
			if (this.symbolOverrideInfo == null)
			{
				this.symbolOverrideInfo = new List<PlantMutation.SymbolOverrideInfo>();
			}
			this.symbolOverrideInfo.Add(new PlantMutation.SymbolOverrideInfo
			{
				targetSymbolName = targetSymbolName,
				sourceAnim = sourceAnim,
				sourceSymbol = sourceSymbol
			});
			return this;
		}

		// Token: 0x06006A55 RID: 27221 RVA: 0x00294D5E File Offset: 0x00292F5E
		public PlantMutation VisualSymbolScale(string targetSymbolName, float scale)
		{
			this.symbolScaleTargets.Add(targetSymbolName);
			this.symbolScales.Add(scale);
			return this;
		}

		// Token: 0x06006A56 RID: 27222 RVA: 0x00294D79 File Offset: 0x00292F79
		public PlantMutation VisualBGFX(string animName)
		{
			this.bGFXAnim = animName;
			return this;
		}

		// Token: 0x06006A57 RID: 27223 RVA: 0x00294D83 File Offset: 0x00292F83
		public PlantMutation VisualFGFX(string animName)
		{
			this.fGFXAnim = animName;
			return this;
		}

		// Token: 0x06006A58 RID: 27224 RVA: 0x00294D8D File Offset: 0x00292F8D
		public PlantMutation AddSoundEvent(string soundEventName)
		{
			this.additionalSoundEvents.Add(soundEventName);
			return this;
		}

		// Token: 0x04004FC3 RID: 20419
		public string desc;

		// Token: 0x04004FC4 RID: 20420
		public string animationSoundEvent;

		// Token: 0x04004FC5 RID: 20421
		public bool originalMutation;

		// Token: 0x04004FC6 RID: 20422
		public List<string> requiredPrefabIDs = new List<string>();

		// Token: 0x04004FC7 RID: 20423
		public List<string> restrictedPrefabIDs = new List<string>();

		// Token: 0x04004FC8 RID: 20424
		private Tag bonusCropID;

		// Token: 0x04004FC9 RID: 20425
		private float bonusCropAmount;

		// Token: 0x04004FCA RID: 20426
		private byte droppedDiseaseID = byte.MaxValue;

		// Token: 0x04004FCB RID: 20427
		private int droppedDiseaseOnGrowAmount;

		// Token: 0x04004FCC RID: 20428
		private int droppedDiseaseContinuousAmount;

		// Token: 0x04004FCD RID: 20429
		private byte harvestDiseaseID = byte.MaxValue;

		// Token: 0x04004FCE RID: 20430
		private int harvestDiseaseAmount;

		// Token: 0x04004FCF RID: 20431
		private bool forcePrefersDarkness;

		// Token: 0x04004FD0 RID: 20432
		private bool forceSelfHarvestOnGrown;

		// Token: 0x04004FD1 RID: 20433
		private PlantElementAbsorber.ConsumeInfo ensureIrrigationInfo;

		// Token: 0x04004FD2 RID: 20434
		private Color plantTint = Color.white;

		// Token: 0x04004FD3 RID: 20435
		private List<string> symbolTintTargets = new List<string>();

		// Token: 0x04004FD4 RID: 20436
		private List<Color> symbolTints = new List<Color>();

		// Token: 0x04004FD5 RID: 20437
		private List<PlantMutation.SymbolOverrideInfo> symbolOverrideInfo;

		// Token: 0x04004FD6 RID: 20438
		private List<string> symbolScaleTargets = new List<string>();

		// Token: 0x04004FD7 RID: 20439
		private List<float> symbolScales = new List<float>();

		// Token: 0x04004FD8 RID: 20440
		private string bGFXAnim;

		// Token: 0x04004FD9 RID: 20441
		private string fGFXAnim;

		// Token: 0x04004FDA RID: 20442
		private List<string> additionalSoundEvents = new List<string>();

		// Token: 0x02001E7C RID: 7804
		private class SymbolOverrideInfo
		{
			// Token: 0x040088F0 RID: 35056
			public string targetSymbolName;

			// Token: 0x040088F1 RID: 35057
			public string sourceAnim;

			// Token: 0x040088F2 RID: 35058
			public string sourceSymbol;
		}
	}
}
