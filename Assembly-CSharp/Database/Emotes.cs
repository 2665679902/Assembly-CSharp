using System;
using Klei.AI;

namespace Database
{
	// Token: 0x02000C92 RID: 3218
	public class Emotes : ResourceSet<Resource>
	{
		// Token: 0x0600657E RID: 25982 RVA: 0x0026AAE8 File Offset: 0x00268CE8
		public Emotes(ResourceSet parent)
			: base("Emotes", parent)
		{
			this.Minion = new Emotes.MinionEmotes(this);
			this.Critter = new Emotes.CritterEmotes(this);
		}

		// Token: 0x0600657F RID: 25983 RVA: 0x0026AB10 File Offset: 0x00268D10
		public void ResetProblematicReferences()
		{
			for (int i = 0; i < this.Minion.resources.Count; i++)
			{
				Emote emote = this.Minion.resources[i];
				for (int j = 0; j < emote.StepCount; j++)
				{
					emote[j].UnregisterAllCallbacks();
				}
			}
			for (int k = 0; k < this.Critter.resources.Count; k++)
			{
				Emote emote2 = this.Critter.resources[k];
				for (int l = 0; l < emote2.StepCount; l++)
				{
					emote2[l].UnregisterAllCallbacks();
				}
			}
		}

		// Token: 0x040048DB RID: 18651
		public Emotes.MinionEmotes Minion;

		// Token: 0x040048DC RID: 18652
		public Emotes.CritterEmotes Critter;

		// Token: 0x02001B20 RID: 6944
		public class MinionEmotes : ResourceSet<Emote>
		{
			// Token: 0x06009585 RID: 38277 RVA: 0x00320C86 File Offset: 0x0031EE86
			public MinionEmotes(ResourceSet parent)
				: base("Minion", parent)
			{
				this.InitializeCelebrations();
				this.InitializePhysicalStatus();
				this.InitializeEmotionalStatus();
				this.InitializeGreetings();
			}

			// Token: 0x06009586 RID: 38278 RVA: 0x00320CAC File Offset: 0x0031EEAC
			public void InitializeCelebrations()
			{
				this.ClapCheer = new Emote(this, "ClapCheer", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "clapcheer_pre"
					},
					new EmoteStep
					{
						anim = "clapcheer_loop"
					},
					new EmoteStep
					{
						anim = "clapcheer_pst"
					}
				}, "anim_clapcheer_kanim");
				this.Cheer = new Emote(this, "Cheer", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "cheer_pre"
					},
					new EmoteStep
					{
						anim = "cheer_loop"
					},
					new EmoteStep
					{
						anim = "cheer_pst"
					}
				}, "anim_cheer_kanim");
				this.ProductiveCheer = new Emote(this, "Productive Cheer", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "productive"
					}
				}, "anim_productive_kanim");
				this.ResearchComplete = new Emote(this, "ResearchComplete", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_react_research_complete_kanim");
				this.ThumbsUp = new Emote(this, "ThumbsUp", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_react_thumbsup_kanim");
			}

			// Token: 0x06009587 RID: 38279 RVA: 0x00320DEC File Offset: 0x0031EFEC
			private void InitializePhysicalStatus()
			{
				this.CloseCall_Fall = new Emote(this, "Near Fall", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_react_floor_missing_kanim");
				this.Cold = new Emote(this, "Cold", Emotes.MinionEmotes.DEFAULT_IDLE_STEPS, "andim_idle_cold_kanim");
				this.Cough = new Emote(this, "Cough", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_slimelungcough_kanim");
				this.Cough_Small = new Emote(this, "Small Cough", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "react_small"
					}
				}, "anim_slimelungcough_kanim");
				this.FoodPoisoning = new Emote(this, "Food Poisoning", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_react_contaminated_food_kanim");
				this.Hot = new Emote(this, "Hot", Emotes.MinionEmotes.DEFAULT_IDLE_STEPS, "anim_idle_hot_kanim");
				this.IritatedEyes = new Emote(this, "Irritated Eyes", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "irritated_eyes"
					}
				}, "anim_irritated_eyes_kanim");
				this.MorningStretch = new Emote(this, "Morning Stretch", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_react_morning_stretch_kanim");
				this.Radiation_Glare = new Emote(this, "Radiation Glare", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "react_radiation_glare"
					}
				}, "anim_react_radiation_kanim");
				this.Radiation_Itch = new Emote(this, "Radiation Itch", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "react_radiation_itch"
					}
				}, "anim_react_radiation_kanim");
				this.Sick = new Emote(this, "Sick", Emotes.MinionEmotes.DEFAULT_IDLE_STEPS, "anim_idle_sick_kanim");
				this.Sneeze = new Emote(this, "Sneeze", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "sneeze"
					},
					new EmoteStep
					{
						anim = "sneeze_pst"
					}
				}, "anim_sneeze_kanim");
				this.Sneeze_Short = new Emote(this, "Short Sneeze", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "sneeze_short"
					},
					new EmoteStep
					{
						anim = "sneeze_short_pst"
					}
				}, "anim_sneeze_kanim");
			}

			// Token: 0x06009588 RID: 38280 RVA: 0x00321020 File Offset: 0x0031F220
			private void InitializeEmotionalStatus()
			{
				this.Concern = new Emote(this, "Concern", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_react_concern_kanim");
				this.Cringe = new Emote(this, "Cringe", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "cringe_pre"
					},
					new EmoteStep
					{
						anim = "cringe_loop"
					},
					new EmoteStep
					{
						anim = "cringe_pst"
					}
				}, "anim_cringe_kanim");
				this.Disappointed = new Emote(this, "Disappointed", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_disappointed_kanim");
				this.Shock = new Emote(this, "Shock", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_react_shock_kanim");
				this.Sing = new Emote(this, "Sing", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_react_singer_kanim");
			}

			// Token: 0x06009589 RID: 38281 RVA: 0x00321100 File Offset: 0x0031F300
			private void InitializeGreetings()
			{
				this.FingerGuns = new Emote(this, "Finger Guns", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_react_fingerguns_kanim");
				this.Wave = new Emote(this, "Wave", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_react_wave_kanim");
				this.Wave_Shy = new Emote(this, "Shy Wave", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_react_wave_shy_kanim");
			}

			// Token: 0x04007A60 RID: 31328
			private static EmoteStep[] DEFAULT_STEPS = new EmoteStep[]
			{
				new EmoteStep
				{
					anim = "react"
				}
			};

			// Token: 0x04007A61 RID: 31329
			private static EmoteStep[] DEFAULT_IDLE_STEPS = new EmoteStep[]
			{
				new EmoteStep
				{
					anim = "idle_pre"
				},
				new EmoteStep
				{
					anim = "idle_default"
				},
				new EmoteStep
				{
					anim = "idle_pst"
				}
			};

			// Token: 0x04007A62 RID: 31330
			public Emote ClapCheer;

			// Token: 0x04007A63 RID: 31331
			public Emote Cheer;

			// Token: 0x04007A64 RID: 31332
			public Emote ProductiveCheer;

			// Token: 0x04007A65 RID: 31333
			public Emote ResearchComplete;

			// Token: 0x04007A66 RID: 31334
			public Emote ThumbsUp;

			// Token: 0x04007A67 RID: 31335
			public Emote CloseCall_Fall;

			// Token: 0x04007A68 RID: 31336
			public Emote Cold;

			// Token: 0x04007A69 RID: 31337
			public Emote Cough;

			// Token: 0x04007A6A RID: 31338
			public Emote Cough_Small;

			// Token: 0x04007A6B RID: 31339
			public Emote FoodPoisoning;

			// Token: 0x04007A6C RID: 31340
			public Emote Hot;

			// Token: 0x04007A6D RID: 31341
			public Emote IritatedEyes;

			// Token: 0x04007A6E RID: 31342
			public Emote MorningStretch;

			// Token: 0x04007A6F RID: 31343
			public Emote Radiation_Glare;

			// Token: 0x04007A70 RID: 31344
			public Emote Radiation_Itch;

			// Token: 0x04007A71 RID: 31345
			public Emote Sick;

			// Token: 0x04007A72 RID: 31346
			public Emote Sneeze;

			// Token: 0x04007A73 RID: 31347
			public Emote Sneeze_Short;

			// Token: 0x04007A74 RID: 31348
			public Emote Concern;

			// Token: 0x04007A75 RID: 31349
			public Emote Cringe;

			// Token: 0x04007A76 RID: 31350
			public Emote Disappointed;

			// Token: 0x04007A77 RID: 31351
			public Emote Shock;

			// Token: 0x04007A78 RID: 31352
			public Emote Sing;

			// Token: 0x04007A79 RID: 31353
			public Emote FingerGuns;

			// Token: 0x04007A7A RID: 31354
			public Emote Wave;

			// Token: 0x04007A7B RID: 31355
			public Emote Wave_Shy;
		}

		// Token: 0x02001B21 RID: 6945
		public class CritterEmotes : ResourceSet<Emote>
		{
			// Token: 0x0600958B RID: 38283 RVA: 0x003211E3 File Offset: 0x0031F3E3
			public CritterEmotes(ResourceSet parent)
				: base("Critter", parent)
			{
				this.InitializePhysicalState();
				this.InitializeEmotionalState();
			}

			// Token: 0x0600958C RID: 38284 RVA: 0x00321200 File Offset: 0x0031F400
			private void InitializePhysicalState()
			{
				this.Hungry = new Emote(this, "Hungry", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "react_hungry"
					}
				}, null);
			}

			// Token: 0x0600958D RID: 38285 RVA: 0x00321240 File Offset: 0x0031F440
			private void InitializeEmotionalState()
			{
				this.Angry = new Emote(this, "Angry", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "react_angry"
					}
				}, null);
				this.Happy = new Emote(this, "Happy", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "react_happy"
					}
				}, null);
				this.Idle = new Emote(this, "Idle", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "react_idle"
					}
				}, null);
				this.Sad = new Emote(this, "Sad", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "react_sad"
					}
				}, null);
			}

			// Token: 0x04007A7C RID: 31356
			public Emote Hungry;

			// Token: 0x04007A7D RID: 31357
			public Emote Angry;

			// Token: 0x04007A7E RID: 31358
			public Emote Happy;

			// Token: 0x04007A7F RID: 31359
			public Emote Idle;

			// Token: 0x04007A80 RID: 31360
			public Emote Sad;
		}
	}
}
