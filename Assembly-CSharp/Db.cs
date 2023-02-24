using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Database;
using Klei;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000507 RID: 1287
public class Db : EntityModifierSet
{
	// Token: 0x06001EBC RID: 7868 RVA: 0x000A4BD4 File Offset: 0x000A2DD4
	public static string GetPath(string dlcId, string folder)
	{
		string text;
		if (dlcId == "")
		{
			text = FileSystem.Normalize(Path.Combine(Application.streamingAssetsPath, folder));
		}
		else
		{
			string contentDirectoryName = DlcManager.GetContentDirectoryName(dlcId);
			text = FileSystem.Normalize(Path.Combine(Application.streamingAssetsPath, "dlc", contentDirectoryName, folder));
		}
		return text;
	}

	// Token: 0x06001EBD RID: 7869 RVA: 0x000A4C20 File Offset: 0x000A2E20
	public static Db Get()
	{
		if (Db._Instance == null)
		{
			Db._Instance = Resources.Load<Db>("Db");
			Db._Instance.Initialize();
		}
		return Db._Instance;
	}

	// Token: 0x06001EBE RID: 7870 RVA: 0x000A4C4D File Offset: 0x000A2E4D
	public static BuildingFacades GetBuildingFacades()
	{
		return Db.Get().Permits.BuildingFacades;
	}

	// Token: 0x06001EBF RID: 7871 RVA: 0x000A4C5E File Offset: 0x000A2E5E
	public static ArtableStages GetArtableStages()
	{
		return Db.Get().Permits.ArtableStages;
	}

	// Token: 0x06001EC0 RID: 7872 RVA: 0x000A4C6F File Offset: 0x000A2E6F
	public static EquippableFacades GetEquippableFacades()
	{
		return Db.Get().Permits.EquippableFacades;
	}

	// Token: 0x06001EC1 RID: 7873 RVA: 0x000A4C80 File Offset: 0x000A2E80
	public static StickerBombs GetStickerBombs()
	{
		return Db.Get().Permits.StickerBombs;
	}

	// Token: 0x06001EC2 RID: 7874 RVA: 0x000A4C91 File Offset: 0x000A2E91
	public static MonumentParts GetMonumentParts()
	{
		return Db.Get().Permits.MonumentParts;
	}

	// Token: 0x06001EC3 RID: 7875 RVA: 0x000A4CA4 File Offset: 0x000A2EA4
	public override void Initialize()
	{
		base.Initialize();
		this.Urges = new Urges();
		this.AssignableSlots = new AssignableSlots();
		this.StateMachineCategories = new StateMachineCategories();
		this.Personalities = new Personalities();
		this.Faces = new Faces();
		this.Shirts = new Shirts();
		this.Expressions = new Expressions(this.Root);
		this.Emotes = new Emotes(this.Root);
		this.Thoughts = new Thoughts(this.Root);
		this.Dreams = new Dreams(this.Root);
		this.Deaths = new Deaths(this.Root);
		this.StatusItemCategories = new StatusItemCategories(this.Root);
		this.TechTreeTitles = new TechTreeTitles(this.Root);
		this.TechTreeTitles.Load(DlcManager.IsExpansion1Active() ? this.researchTreeFileExpansion1 : this.researchTreeFileVanilla);
		this.Techs = new Techs(this.Root);
		this.TechItems = new TechItems(this.Root);
		this.Techs.Init();
		this.Techs.Load(DlcManager.IsExpansion1Active() ? this.researchTreeFileExpansion1 : this.researchTreeFileVanilla);
		this.TechItems.Init();
		this.Accessories = new Accessories(this.Root);
		this.AccessorySlots = new AccessorySlots(this.Root);
		this.ScheduleBlockTypes = new ScheduleBlockTypes(this.Root);
		this.ScheduleGroups = new ScheduleGroups(this.Root);
		this.RoomTypeCategories = new RoomTypeCategories(this.Root);
		this.RoomTypes = new RoomTypes(this.Root);
		this.ArtifactDropRates = new ArtifactDropRates(this.Root);
		this.SpaceDestinationTypes = new SpaceDestinationTypes(this.Root);
		this.Diseases = new Diseases(this.Root, false);
		this.Sicknesses = new Database.Sicknesses(this.Root);
		this.SkillPerks = new SkillPerks(this.Root);
		this.SkillGroups = new SkillGroups(this.Root);
		this.Skills = new Skills(this.Root);
		this.ColonyAchievements = new ColonyAchievements(this.Root);
		this.MiscStatusItems = new MiscStatusItems(this.Root);
		this.CreatureStatusItems = new CreatureStatusItems(this.Root);
		this.BuildingStatusItems = new BuildingStatusItems(this.Root);
		this.RobotStatusItems = new RobotStatusItems(this.Root);
		this.ChoreTypes = new ChoreTypes(this.Root);
		this.Quests = new Quests(this.Root);
		this.GameplayEvents = new GameplayEvents(this.Root);
		this.GameplaySeasons = new GameplaySeasons(this.Root);
		this.Stories = new Stories(this.Root);
		if (DlcManager.FeaturePlantMutationsEnabled())
		{
			this.PlantMutations = new PlantMutations(this.Root);
		}
		this.OrbitalTypeCategories = new OrbitalTypeCategories(this.Root);
		this.ArtableStatuses = new ArtableStatuses(this.Root);
		this.Permits = new PermitResources(this.Root);
		Effect effect = new Effect("CenterOfAttention", DUPLICANTS.MODIFIERS.CENTEROFATTENTION.NAME, DUPLICANTS.MODIFIERS.CENTEROFATTENTION.TOOLTIP, 0f, true, true, false, null, -1f, 0f, null, "");
		effect.Add(new AttributeModifier("StressDelta", -0.008333334f, DUPLICANTS.MODIFIERS.CENTEROFATTENTION.NAME, false, false, true));
		this.effects.Add(effect);
		this.Spices = new Spices(this.Root);
		this.CollectResources(this.Root, this.ResourceTable);
	}

	// Token: 0x06001EC4 RID: 7876 RVA: 0x000A5045 File Offset: 0x000A3245
	public void PostProcess()
	{
		this.Techs.PostProcess();
		this.Permits.PostProcess();
	}

	// Token: 0x06001EC5 RID: 7877 RVA: 0x000A5060 File Offset: 0x000A3260
	private void CollectResources(Resource resource, List<Resource> resource_table)
	{
		if (resource.Guid != null)
		{
			resource_table.Add(resource);
		}
		ResourceSet resourceSet = resource as ResourceSet;
		if (resourceSet != null)
		{
			for (int i = 0; i < resourceSet.Count; i++)
			{
				this.CollectResources(resourceSet.GetResource(i), resource_table);
			}
		}
	}

	// Token: 0x06001EC6 RID: 7878 RVA: 0x000A50AC File Offset: 0x000A32AC
	public ResourceType GetResource<ResourceType>(ResourceGuid guid) where ResourceType : Resource
	{
		Resource resource = this.ResourceTable.FirstOrDefault((Resource s) => s.Guid == guid);
		if (resource == null)
		{
			string text = "Could not find resource: ";
			ResourceGuid guid2 = guid;
			global::Debug.LogWarning(text + ((guid2 != null) ? guid2.ToString() : null));
			return default(ResourceType);
		}
		ResourceType resourceType = (ResourceType)((object)resource);
		if (resourceType == null)
		{
			global::Debug.LogError(string.Concat(new string[]
			{
				"Resource type mismatch for resource: ",
				resource.Id,
				"\nExpecting Type: ",
				typeof(ResourceType).Name,
				"\nGot Type: ",
				resource.GetType().Name
			}));
			return default(ResourceType);
		}
		return resourceType;
	}

	// Token: 0x06001EC7 RID: 7879 RVA: 0x000A5177 File Offset: 0x000A3377
	public void ResetProblematicDbs()
	{
		this.Emotes.ResetProblematicReferences();
	}

	// Token: 0x04001143 RID: 4419
	private static Db _Instance;

	// Token: 0x04001144 RID: 4420
	public TextAsset researchTreeFileVanilla;

	// Token: 0x04001145 RID: 4421
	public TextAsset researchTreeFileExpansion1;

	// Token: 0x04001146 RID: 4422
	public Diseases Diseases;

	// Token: 0x04001147 RID: 4423
	public Database.Sicknesses Sicknesses;

	// Token: 0x04001148 RID: 4424
	public Urges Urges;

	// Token: 0x04001149 RID: 4425
	public AssignableSlots AssignableSlots;

	// Token: 0x0400114A RID: 4426
	public StateMachineCategories StateMachineCategories;

	// Token: 0x0400114B RID: 4427
	public Personalities Personalities;

	// Token: 0x0400114C RID: 4428
	public Faces Faces;

	// Token: 0x0400114D RID: 4429
	public Shirts Shirts;

	// Token: 0x0400114E RID: 4430
	public Expressions Expressions;

	// Token: 0x0400114F RID: 4431
	public Emotes Emotes;

	// Token: 0x04001150 RID: 4432
	public Thoughts Thoughts;

	// Token: 0x04001151 RID: 4433
	public Dreams Dreams;

	// Token: 0x04001152 RID: 4434
	public BuildingStatusItems BuildingStatusItems;

	// Token: 0x04001153 RID: 4435
	public MiscStatusItems MiscStatusItems;

	// Token: 0x04001154 RID: 4436
	public CreatureStatusItems CreatureStatusItems;

	// Token: 0x04001155 RID: 4437
	public RobotStatusItems RobotStatusItems;

	// Token: 0x04001156 RID: 4438
	public StatusItemCategories StatusItemCategories;

	// Token: 0x04001157 RID: 4439
	public Deaths Deaths;

	// Token: 0x04001158 RID: 4440
	public ChoreTypes ChoreTypes;

	// Token: 0x04001159 RID: 4441
	public TechItems TechItems;

	// Token: 0x0400115A RID: 4442
	public AccessorySlots AccessorySlots;

	// Token: 0x0400115B RID: 4443
	public Accessories Accessories;

	// Token: 0x0400115C RID: 4444
	public ScheduleBlockTypes ScheduleBlockTypes;

	// Token: 0x0400115D RID: 4445
	public ScheduleGroups ScheduleGroups;

	// Token: 0x0400115E RID: 4446
	public RoomTypeCategories RoomTypeCategories;

	// Token: 0x0400115F RID: 4447
	public RoomTypes RoomTypes;

	// Token: 0x04001160 RID: 4448
	public ArtifactDropRates ArtifactDropRates;

	// Token: 0x04001161 RID: 4449
	public SpaceDestinationTypes SpaceDestinationTypes;

	// Token: 0x04001162 RID: 4450
	public SkillPerks SkillPerks;

	// Token: 0x04001163 RID: 4451
	public SkillGroups SkillGroups;

	// Token: 0x04001164 RID: 4452
	public Skills Skills;

	// Token: 0x04001165 RID: 4453
	public ColonyAchievements ColonyAchievements;

	// Token: 0x04001166 RID: 4454
	public Quests Quests;

	// Token: 0x04001167 RID: 4455
	public GameplayEvents GameplayEvents;

	// Token: 0x04001168 RID: 4456
	public GameplaySeasons GameplaySeasons;

	// Token: 0x04001169 RID: 4457
	public PlantMutations PlantMutations;

	// Token: 0x0400116A RID: 4458
	public Spices Spices;

	// Token: 0x0400116B RID: 4459
	public Techs Techs;

	// Token: 0x0400116C RID: 4460
	public TechTreeTitles TechTreeTitles;

	// Token: 0x0400116D RID: 4461
	public OrbitalTypeCategories OrbitalTypeCategories;

	// Token: 0x0400116E RID: 4462
	public PermitResources Permits;

	// Token: 0x0400116F RID: 4463
	public ArtableStatuses ArtableStatuses;

	// Token: 0x04001170 RID: 4464
	public Stories Stories;

	// Token: 0x02001147 RID: 4423
	[Serializable]
	public class SlotInfo : Resource
	{
	}
}
