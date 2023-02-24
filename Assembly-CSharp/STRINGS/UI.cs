using System;
using System.Collections.Generic;

namespace STRINGS
{
	// Token: 0x02000D3B RID: 3387
	public class UI
	{
		// Token: 0x0600681A RID: 26650 RVA: 0x0028950A File Offset: 0x0028770A
		public static string FormatAsBuildMenuTab(string text)
		{
			return "<b>" + text + "</b>";
		}

		// Token: 0x0600681B RID: 26651 RVA: 0x0028951C File Offset: 0x0028771C
		public static string FormatAsBuildMenuTab(string text, string hotkey)
		{
			return "<b>" + text + "</b> " + UI.FormatAsHotkey(hotkey);
		}

		// Token: 0x0600681C RID: 26652 RVA: 0x00289534 File Offset: 0x00287734
		public static string FormatAsBuildMenuTab(string text, global::Action a)
		{
			return "<b>" + text + "</b> " + UI.FormatAsHotKey(a);
		}

		// Token: 0x0600681D RID: 26653 RVA: 0x0028954C File Offset: 0x0028774C
		public static string FormatAsOverlay(string text)
		{
			return "<b>" + text + "</b>";
		}

		// Token: 0x0600681E RID: 26654 RVA: 0x0028955E File Offset: 0x0028775E
		public static string FormatAsOverlay(string text, string hotkey)
		{
			return "<b>" + text + "</b> " + UI.FormatAsHotkey(hotkey);
		}

		// Token: 0x0600681F RID: 26655 RVA: 0x00289576 File Offset: 0x00287776
		public static string FormatAsOverlay(string text, global::Action a)
		{
			return "<b>" + text + "</b> " + UI.FormatAsHotKey(a);
		}

		// Token: 0x06006820 RID: 26656 RVA: 0x0028958E File Offset: 0x0028778E
		public static string FormatAsManagementMenu(string text)
		{
			return "<b>" + text + "</b>";
		}

		// Token: 0x06006821 RID: 26657 RVA: 0x002895A0 File Offset: 0x002877A0
		public static string FormatAsManagementMenu(string text, string hotkey)
		{
			return "<b>" + text + "</b> " + UI.FormatAsHotkey(hotkey);
		}

		// Token: 0x06006822 RID: 26658 RVA: 0x002895B8 File Offset: 0x002877B8
		public static string FormatAsManagementMenu(string text, global::Action a)
		{
			return "<b>" + text + "</b> " + UI.FormatAsHotKey(a);
		}

		// Token: 0x06006823 RID: 26659 RVA: 0x002895D0 File Offset: 0x002877D0
		public static string FormatAsKeyWord(string text)
		{
			return UI.PRE_KEYWORD + text + UI.PST_KEYWORD;
		}

		// Token: 0x06006824 RID: 26660 RVA: 0x002895E2 File Offset: 0x002877E2
		public static string FormatAsHotkey(string text)
		{
			return "<b><color=#F44A4A>" + text + "</b></color>";
		}

		// Token: 0x06006825 RID: 26661 RVA: 0x002895F4 File Offset: 0x002877F4
		public static string FormatAsHotKey(global::Action a)
		{
			return "{Hotkey/" + a.ToString() + "}";
		}

		// Token: 0x06006826 RID: 26662 RVA: 0x00289612 File Offset: 0x00287812
		public static string FormatAsTool(string text, string hotkey)
		{
			return "<b>" + text + "</b> " + UI.FormatAsHotkey(hotkey);
		}

		// Token: 0x06006827 RID: 26663 RVA: 0x0028962A File Offset: 0x0028782A
		public static string FormatAsTool(string text, global::Action a)
		{
			return "<b>" + text + "</b> " + UI.FormatAsHotKey(a);
		}

		// Token: 0x06006828 RID: 26664 RVA: 0x00289642 File Offset: 0x00287842
		public static string FormatAsLink(string text, string linkID)
		{
			text = UI.StripLinkFormatting(text);
			linkID = CodexCache.FormatLinkID(linkID);
			return string.Concat(new string[] { "<link=\"", linkID, "\">", text, "</link>" });
		}

		// Token: 0x06006829 RID: 26665 RVA: 0x0028967F File Offset: 0x0028787F
		public static string FormatAsPositiveModifier(string text)
		{
			return UI.PRE_POS_MODIFIER + text + UI.PST_POS_MODIFIER;
		}

		// Token: 0x0600682A RID: 26666 RVA: 0x00289691 File Offset: 0x00287891
		public static string FormatAsNegativeModifier(string text)
		{
			return UI.PRE_NEG_MODIFIER + text + UI.PST_NEG_MODIFIER;
		}

		// Token: 0x0600682B RID: 26667 RVA: 0x002896A3 File Offset: 0x002878A3
		public static string FormatAsPositiveRate(string text)
		{
			return UI.PRE_RATE_POSITIVE + text + UI.PST_RATE;
		}

		// Token: 0x0600682C RID: 26668 RVA: 0x002896B5 File Offset: 0x002878B5
		public static string FormatAsNegativeRate(string text)
		{
			return UI.PRE_RATE_NEGATIVE + text + UI.PST_RATE;
		}

		// Token: 0x0600682D RID: 26669 RVA: 0x002896C7 File Offset: 0x002878C7
		public static string CLICK(UI.ClickType c)
		{
			return "(ClickType/" + c.ToString() + ")";
		}

		// Token: 0x0600682E RID: 26670 RVA: 0x002896E5 File Offset: 0x002878E5
		public static string FormatAsAutomationState(string text, UI.AutomationState state)
		{
			if (state == UI.AutomationState.Active)
			{
				return UI.PRE_AUTOMATION_ACTIVE + text + UI.PST_AUTOMATION;
			}
			return UI.PRE_AUTOMATION_STANDBY + text + UI.PST_AUTOMATION;
		}

		// Token: 0x0600682F RID: 26671 RVA: 0x0028970B File Offset: 0x0028790B
		public static string FormatAsCaps(string text)
		{
			return text.ToUpper();
		}

		// Token: 0x06006830 RID: 26672 RVA: 0x00289714 File Offset: 0x00287914
		public static string ExtractLinkID(string text)
		{
			string text2 = text;
			int num = text2.IndexOf("<link=");
			if (num != -1)
			{
				int num2 = num + 7;
				int num3 = text2.IndexOf(">") - 1;
				text2 = text.Substring(num2, num3 - num2);
			}
			return text2;
		}

		// Token: 0x06006831 RID: 26673 RVA: 0x00289754 File Offset: 0x00287954
		public static string StripLinkFormatting(string text)
		{
			string text2 = text;
			try
			{
				while (text2.Contains("<link="))
				{
					int num = text2.IndexOf("</link>");
					if (num > -1)
					{
						text2 = text2.Remove(num, 7);
					}
					else
					{
						Debug.LogWarningFormat("String has no closing link tag: {0}", Array.Empty<object>());
					}
					int num2 = text2.IndexOf("<link=");
					if (num2 != -1)
					{
						text2 = text2.Remove(num2, 7);
					}
					else
					{
						Debug.LogWarningFormat("String has no open link tag: {0}", Array.Empty<object>());
					}
					int num3 = text2.IndexOf("\">");
					if (num3 != -1)
					{
						text2 = text2.Remove(num2, num3 - num2 + 2);
					}
					else
					{
						Debug.LogWarningFormat("String has no open link tag: {0}", Array.Empty<object>());
					}
				}
			}
			catch
			{
				Debug.Log("STRIP LINK FORMATTING FAILED ON: " + text);
				text2 = text;
			}
			return text2;
		}

		// Token: 0x04004DA7 RID: 19879
		public static string PRE_KEYWORD = "<style=\"KKeyword\">";

		// Token: 0x04004DA8 RID: 19880
		public static string PST_KEYWORD = "</style>";

		// Token: 0x04004DA9 RID: 19881
		public static string PRE_POS_MODIFIER = "<b>";

		// Token: 0x04004DAA RID: 19882
		public static string PST_POS_MODIFIER = "</b>";

		// Token: 0x04004DAB RID: 19883
		public static string PRE_NEG_MODIFIER = "<b>";

		// Token: 0x04004DAC RID: 19884
		public static string PST_NEG_MODIFIER = "</b>";

		// Token: 0x04004DAD RID: 19885
		public static string PRE_RATE_NEGATIVE = "<style=\"consumed\">";

		// Token: 0x04004DAE RID: 19886
		public static string PRE_RATE_POSITIVE = "<style=\"produced\">";

		// Token: 0x04004DAF RID: 19887
		public static string PST_RATE = "</style>";

		// Token: 0x04004DB0 RID: 19888
		public static string PRE_AUTOMATION_ACTIVE = "<b><style=\"logic_on\">";

		// Token: 0x04004DB1 RID: 19889
		public static string PRE_AUTOMATION_STANDBY = "<b><style=\"logic_off\">";

		// Token: 0x04004DB2 RID: 19890
		public static string PST_AUTOMATION = "</style></b>";

		// Token: 0x04004DB3 RID: 19891
		public static string YELLOW_PREFIX = "<color=#ffff00ff>";

		// Token: 0x04004DB4 RID: 19892
		public static string COLOR_SUFFIX = "</color>";

		// Token: 0x04004DB5 RID: 19893
		public static string HORIZONTAL_RULE = "------------------";

		// Token: 0x04004DB6 RID: 19894
		public static string HORIZONTAL_BR_RULE = "\n" + UI.HORIZONTAL_RULE + "\n";

		// Token: 0x04004DB7 RID: 19895
		public static LocString POS_INFINITY = "Infinity";

		// Token: 0x04004DB8 RID: 19896
		public static LocString NEG_INFINITY = "-Infinity";

		// Token: 0x04004DB9 RID: 19897
		public static LocString PROCEED_BUTTON = "PROCEED";

		// Token: 0x04004DBA RID: 19898
		public static LocString COPY_BUILDING = "Copy";

		// Token: 0x04004DBB RID: 19899
		public static LocString COPY_BUILDING_TOOLTIP = "Create new build orders using the most recent building selection as a template. {Hotkey}";

		// Token: 0x04004DBC RID: 19900
		public static LocString NAME_WITH_UNITS = "{0} x {1}";

		// Token: 0x04004DBD RID: 19901
		public static LocString NA = "N/A";

		// Token: 0x04004DBE RID: 19902
		public static LocString POSITIVE_FORMAT = "+{0}";

		// Token: 0x04004DBF RID: 19903
		public static LocString NEGATIVE_FORMAT = "-{0}";

		// Token: 0x04004DC0 RID: 19904
		public static LocString FILTER = "Filter";

		// Token: 0x04004DC1 RID: 19905
		public static LocString SPEED_SLOW = "SLOW";

		// Token: 0x04004DC2 RID: 19906
		public static LocString SPEED_MEDIUM = "MEDIUM";

		// Token: 0x04004DC3 RID: 19907
		public static LocString SPEED_FAST = "FAST";

		// Token: 0x04004DC4 RID: 19908
		public static LocString RED_ALERT = "RED ALERT";

		// Token: 0x04004DC5 RID: 19909
		public static LocString JOBS = "PRIORITIES";

		// Token: 0x04004DC6 RID: 19910
		public static LocString CONSUMABLES = "CONSUMABLES";

		// Token: 0x04004DC7 RID: 19911
		public static LocString VITALS = "VITALS";

		// Token: 0x04004DC8 RID: 19912
		public static LocString RESEARCH = "RESEARCH";

		// Token: 0x04004DC9 RID: 19913
		public static LocString ROLES = "JOB ASSIGNMENTS";

		// Token: 0x04004DCA RID: 19914
		public static LocString RESEARCHPOINTS = "Research points";

		// Token: 0x04004DCB RID: 19915
		public static LocString SCHEDULE = "SCHEDULE";

		// Token: 0x04004DCC RID: 19916
		public static LocString REPORT = "REPORTS";

		// Token: 0x04004DCD RID: 19917
		public static LocString SKILLS = "SKILLS";

		// Token: 0x04004DCE RID: 19918
		public static LocString OVERLAYSTITLE = "OVERLAYS";

		// Token: 0x04004DCF RID: 19919
		public static LocString ALERTS = "ALERTS";

		// Token: 0x04004DD0 RID: 19920
		public static LocString MESSAGES = "MESSAGES";

		// Token: 0x04004DD1 RID: 19921
		public static LocString ACTIONS = "ACTIONS";

		// Token: 0x04004DD2 RID: 19922
		public static LocString QUEUE = "Queue";

		// Token: 0x04004DD3 RID: 19923
		public static LocString BASECOUNT = "Base {0}";

		// Token: 0x04004DD4 RID: 19924
		public static LocString CHARACTERCONTAINER_SKILLS_TITLE = "ATTRIBUTES";

		// Token: 0x04004DD5 RID: 19925
		public static LocString CHARACTERCONTAINER_TRAITS_TITLE = "TRAITS";

		// Token: 0x04004DD6 RID: 19926
		public static LocString CHARACTERCONTAINER_APTITUDES_TITLE = "INTERESTS";

		// Token: 0x04004DD7 RID: 19927
		public static LocString CHARACTERCONTAINER_APTITUDES_TITLE_TOOLTIP = "A Duplicant's starting Attributes are determined by their Interests\n\nLearning Skills related to their Interests will give Duplicants a Morale Boost";

		// Token: 0x04004DD8 RID: 19928
		public static LocString CHARACTERCONTAINER_EXPECTATIONS_TITLE = "ADDITIONAL INFORMATION";

		// Token: 0x04004DD9 RID: 19929
		public static LocString CHARACTERCONTAINER_SKILL_VALUE = " {0} {1}";

		// Token: 0x04004DDA RID: 19930
		public static LocString CHARACTERCONTAINER_NEED = "{0}: {1}";

		// Token: 0x04004DDB RID: 19931
		public static LocString CHARACTERCONTAINER_STRESSTRAIT = "Stress Reaction: {0}";

		// Token: 0x04004DDC RID: 19932
		public static LocString CHARACTERCONTAINER_JOYTRAIT = "Overjoyed Response: {0}";

		// Token: 0x04004DDD RID: 19933
		public static LocString CHARACTERCONTAINER_CONGENITALTRAIT = "Genetic Trait: {0}";

		// Token: 0x04004DDE RID: 19934
		public static LocString CHARACTERCONTAINER_NOARCHETYPESELECTED = "Random";

		// Token: 0x04004DDF RID: 19935
		public static LocString CHARACTERCONTAINER_ARCHETYPESELECT_TOOLTIP = "Influence what type of Duplicant the reroll button will produce";

		// Token: 0x04004DE0 RID: 19936
		public static LocString CAREPACKAGECONTAINER_INFORMATION_TITLE = "CARE PACKAGE";

		// Token: 0x04004DE1 RID: 19937
		public static LocString CHARACTERCONTAINER_ATTRIBUTEMODIFIER_INCREASED = "Increased <b>{0}</b>";

		// Token: 0x04004DE2 RID: 19938
		public static LocString CHARACTERCONTAINER_ATTRIBUTEMODIFIER_DECREASED = "Decreased <b>{0}</b>";

		// Token: 0x04004DE3 RID: 19939
		public static LocString PRODUCTINFO_SELECTMATERIAL = "Select {0}:";

		// Token: 0x04004DE4 RID: 19940
		public static LocString PRODUCTINFO_RESEARCHREQUIRED = "Research required...";

		// Token: 0x04004DE5 RID: 19941
		public static LocString PRODUCTINFO_REQUIRESRESEARCHDESC = "Requires {0} Research";

		// Token: 0x04004DE6 RID: 19942
		public static LocString PRODUCTINFO_APPLICABLERESOURCES = "Required resources:";

		// Token: 0x04004DE7 RID: 19943
		public static LocString PRODUCTINFO_MISSINGRESOURCES_TITLE = "Requires {0}: {1}";

		// Token: 0x04004DE8 RID: 19944
		public static LocString PRODUCTINFO_MISSINGRESOURCES_HOVER = "Missing resources";

		// Token: 0x04004DE9 RID: 19945
		public static LocString PRODUCTINFO_MISSINGRESOURCES_DESC = "{0} has yet to be discovered";

		// Token: 0x04004DEA RID: 19946
		public static LocString PRODUCTINFO_UNIQUE_PER_WORLD = "Limit one per " + UI.CLUSTERMAP.PLANETOID_KEYWORD;

		// Token: 0x04004DEB RID: 19947
		public static LocString PRODUCTINFO_ROCKET_INTERIOR = "Rocket interior only";

		// Token: 0x04004DEC RID: 19948
		public static LocString PRODUCTINFO_ROCKET_NOT_INTERIOR = "Cannot build inside rocket";

		// Token: 0x04004DED RID: 19949
		public static LocString BUILDTOOL_ROTATE = "Rotate this building";

		// Token: 0x04004DEE RID: 19950
		public static LocString BUILDTOOL_ROTATE_CURRENT_DEGREES = "Currently rotated {Degrees} degrees";

		// Token: 0x04004DEF RID: 19951
		public static LocString BUILDTOOL_ROTATE_CURRENT_LEFT = "Currently facing left";

		// Token: 0x04004DF0 RID: 19952
		public static LocString BUILDTOOL_ROTATE_CURRENT_RIGHT = "Currently facing right";

		// Token: 0x04004DF1 RID: 19953
		public static LocString BUILDTOOL_ROTATE_CURRENT_UP = "Currently facing up";

		// Token: 0x04004DF2 RID: 19954
		public static LocString BUILDTOOL_ROTATE_CURRENT_DOWN = "Currently facing down";

		// Token: 0x04004DF3 RID: 19955
		public static LocString BUILDTOOL_ROTATE_CURRENT_UPRIGHT = "Currently upright";

		// Token: 0x04004DF4 RID: 19956
		public static LocString BUILDTOOL_ROTATE_CURRENT_ON_SIDE = "Currently on its side";

		// Token: 0x04004DF5 RID: 19957
		public static LocString BUILDTOOL_CANT_ROTATE = "This building cannot be rotated";

		// Token: 0x04004DF6 RID: 19958
		public static LocString EQUIPMENTTAB_OWNED = "Owned Items";

		// Token: 0x04004DF7 RID: 19959
		public static LocString EQUIPMENTTAB_HELD = "Held Items";

		// Token: 0x04004DF8 RID: 19960
		public static LocString EQUIPMENTTAB_ROOM = "Assigned Rooms";

		// Token: 0x04004DF9 RID: 19961
		public static LocString JOBSCREEN_PRIORITY = "Priority";

		// Token: 0x04004DFA RID: 19962
		public static LocString JOBSCREEN_HIGH = "High";

		// Token: 0x04004DFB RID: 19963
		public static LocString JOBSCREEN_LOW = "Low";

		// Token: 0x04004DFC RID: 19964
		public static LocString JOBSCREEN_EVERYONE = "Everyone";

		// Token: 0x04004DFD RID: 19965
		public static LocString JOBSCREEN_DEFAULT = "New Duplicants";

		// Token: 0x04004DFE RID: 19966
		public static LocString BUILD_REQUIRES_SKILL = "Skill: {Skill}";

		// Token: 0x04004DFF RID: 19967
		public static LocString BUILD_REQUIRES_SKILL_TOOLTIP = "At least one Duplicant must have the {Skill} Skill to construct this building";

		// Token: 0x04004E00 RID: 19968
		public static LocString VITALSSCREEN_NAME = "Name";

		// Token: 0x04004E01 RID: 19969
		public static LocString VITALSSCREEN_STRESS = "Stress";

		// Token: 0x04004E02 RID: 19970
		public static LocString VITALSSCREEN_HEALTH = "Health";

		// Token: 0x04004E03 RID: 19971
		public static LocString VITALSSCREEN_SICKNESS = "Disease";

		// Token: 0x04004E04 RID: 19972
		public static LocString VITALSSCREEN_CALORIES = "Fullness";

		// Token: 0x04004E05 RID: 19973
		public static LocString VITALSSCREEN_RATIONS = "Calories / Cycle";

		// Token: 0x04004E06 RID: 19974
		public static LocString VITALSSCREEN_EATENTODAY = "Eaten Today";

		// Token: 0x04004E07 RID: 19975
		public static LocString VITALSSCREEN_RATIONS_TOOLTIP = "Set how many calories this Duplicant may consume daily";

		// Token: 0x04004E08 RID: 19976
		public static LocString VITALSSCREEN_EATENTODAY_TOOLTIP = "The amount of food this Duplicant has eaten this cycle";

		// Token: 0x04004E09 RID: 19977
		public static LocString VITALSSCREEN_UNTIL_FULL = "Until Full";

		// Token: 0x04004E0A RID: 19978
		public static LocString RESEARCHSCREEN_UNLOCKSTOOLTIP = "Unlocks: {0}";

		// Token: 0x04004E0B RID: 19979
		public static LocString RESEARCHSCREEN_FILTER = "Search Tech";

		// Token: 0x04004E0C RID: 19980
		public static LocString ATTRIBUTELEVEL = "Expertise: Level {0} {1}";

		// Token: 0x04004E0D RID: 19981
		public static LocString ATTRIBUTELEVEL_SHORT = "Level {0} {1}";

		// Token: 0x04004E0E RID: 19982
		public static LocString NEUTRONIUMMASS = "Immeasurable";

		// Token: 0x04004E0F RID: 19983
		public static LocString CALCULATING = "Calculating...";

		// Token: 0x04004E10 RID: 19984
		public static LocString FORMATDAY = "{0} cycles";

		// Token: 0x04004E11 RID: 19985
		public static LocString FORMATSECONDS = "{0}s";

		// Token: 0x04004E12 RID: 19986
		public static LocString DELIVERED = "Delivered: {0} {1}";

		// Token: 0x04004E13 RID: 19987
		public static LocString PICKEDUP = "Picked Up: {0} {1}";

		// Token: 0x04004E14 RID: 19988
		public static LocString COPIED_SETTINGS = "Settings Applied";

		// Token: 0x04004E15 RID: 19989
		public static LocString WELCOMEMESSAGETITLE = "- ALERT -";

		// Token: 0x04004E16 RID: 19990
		public static LocString WELCOMEMESSAGEBODY = "I've awoken at the target location, but colonization efforts have already hit a hitch. I was supposed to land on the planet's surface, but became trapped many miles underground instead.\n\nAlthough the conditions are not ideal, it's imperative that I establish a colony here and begin mounting efforts to escape.";

		// Token: 0x04004E17 RID: 19991
		public static LocString WELCOMEMESSAGEBODY_SPACEDOUT = "The asteroid we call home has collided with an anomalous planet, decimating our colony. Rebuilding it is of the utmost importance.\n\nI've detected a new cluster of material-rich planetoids in nearby space. If I can guide the Duplicants through the perils of space travel, we could build a colony even bigger and better than before.";

		// Token: 0x04004E18 RID: 19992
		public static LocString WELCOMEMESSAGEBEGIN = "BEGIN";

		// Token: 0x04004E19 RID: 19993
		public static LocString VIEWDUPLICANTS = "Choose a Blueprint";

		// Token: 0x04004E1A RID: 19994
		public static LocString DUPLICANTPRINTING = "Duplicant Printing";

		// Token: 0x04004E1B RID: 19995
		public static LocString ASSIGNDUPLICANT = "Assign Duplicant";

		// Token: 0x04004E1C RID: 19996
		public static LocString CRAFT = "ADD TO QUEUE";

		// Token: 0x04004E1D RID: 19997
		public static LocString CLEAR_COMPLETED = "CLEAR COMPLETED ORDERS";

		// Token: 0x04004E1E RID: 19998
		public static LocString CRAFT_CONTINUOUS = "CONTINUOUS";

		// Token: 0x04004E1F RID: 19999
		public static LocString INCUBATE_CONTINUOUS_TOOLTIP = "When checked, this building will continuously incubate eggs of the selected type";

		// Token: 0x04004E20 RID: 20000
		public static LocString PLACEINRECEPTACLE = "Plant";

		// Token: 0x04004E21 RID: 20001
		public static LocString REMOVEFROMRECEPTACLE = "Uproot";

		// Token: 0x04004E22 RID: 20002
		public static LocString CANCELPLACEINRECEPTACLE = "Cancel";

		// Token: 0x04004E23 RID: 20003
		public static LocString CANCELREMOVALFROMRECEPTACLE = "Cancel";

		// Token: 0x04004E24 RID: 20004
		public static LocString CHANGEPERSECOND = "Change per second: {0}";

		// Token: 0x04004E25 RID: 20005
		public static LocString CHANGEPERCYCLE = "Change per cycle: {0}";

		// Token: 0x04004E26 RID: 20006
		public static LocString MODIFIER_ITEM_TEMPLATE = "    • {0}: {1}";

		// Token: 0x04004E27 RID: 20007
		public static LocString LISTENTRYSTRING = "     {0}\n";

		// Token: 0x04004E28 RID: 20008
		public static LocString LISTENTRYSTRINGNOLINEBREAK = "     {0}";

		// Token: 0x02001C70 RID: 7280
		public static class PLATFORMS
		{
			// Token: 0x04007FAD RID: 32685
			public static LocString UNKNOWN = "Your game client";

			// Token: 0x04007FAE RID: 32686
			public static LocString STEAM = "Steam";

			// Token: 0x04007FAF RID: 32687
			public static LocString EPIC = "Epic Games Store";

			// Token: 0x04007FB0 RID: 32688
			public static LocString WEGAME = "Wegame";
		}

		// Token: 0x02001C71 RID: 7281
		private enum KeywordType
		{
			// Token: 0x04007FB2 RID: 32690
			Hotkey,
			// Token: 0x04007FB3 RID: 32691
			BuildMenu,
			// Token: 0x04007FB4 RID: 32692
			Attribute,
			// Token: 0x04007FB5 RID: 32693
			Generic
		}

		// Token: 0x02001C72 RID: 7282
		public enum ClickType
		{
			// Token: 0x04007FB7 RID: 32695
			Click,
			// Token: 0x04007FB8 RID: 32696
			Clicked,
			// Token: 0x04007FB9 RID: 32697
			Clicking,
			// Token: 0x04007FBA RID: 32698
			Clickable,
			// Token: 0x04007FBB RID: 32699
			Clicks,
			// Token: 0x04007FBC RID: 32700
			click,
			// Token: 0x04007FBD RID: 32701
			clicked,
			// Token: 0x04007FBE RID: 32702
			clicking,
			// Token: 0x04007FBF RID: 32703
			clickable,
			// Token: 0x04007FC0 RID: 32704
			clicks,
			// Token: 0x04007FC1 RID: 32705
			CLICK,
			// Token: 0x04007FC2 RID: 32706
			CLICKED,
			// Token: 0x04007FC3 RID: 32707
			CLICKING,
			// Token: 0x04007FC4 RID: 32708
			CLICKABLE,
			// Token: 0x04007FC5 RID: 32709
			CLICKS
		}

		// Token: 0x02001C73 RID: 7283
		public enum AutomationState
		{
			// Token: 0x04007FC7 RID: 32711
			Active,
			// Token: 0x04007FC8 RID: 32712
			Standby
		}

		// Token: 0x02001C74 RID: 7284
		public class VANILLA
		{
			// Token: 0x04007FC9 RID: 32713
			public static LocString NAME = "base game";

			// Token: 0x04007FCA RID: 32714
			public static LocString NAME_ITAL = "<i>" + UI.VANILLA.NAME + "</i>";
		}

		// Token: 0x02001C75 RID: 7285
		public class DLC1
		{
			// Token: 0x04007FCB RID: 32715
			public static LocString NAME = "Spaced Out!";

			// Token: 0x04007FCC RID: 32716
			public static LocString NAME_ITAL = "<i>" + UI.DLC1.NAME + "</i>";
		}

		// Token: 0x02001C76 RID: 7286
		public class DIAGNOSTICS_SCREEN
		{
			// Token: 0x04007FCD RID: 32717
			public static LocString TITLE = "Diagnostics";

			// Token: 0x04007FCE RID: 32718
			public static LocString DIAGNOSTIC = "Diagnostic";

			// Token: 0x04007FCF RID: 32719
			public static LocString TOTAL = "Total";

			// Token: 0x04007FD0 RID: 32720
			public static LocString RESERVED = "Reserved";

			// Token: 0x04007FD1 RID: 32721
			public static LocString STATUS = "Status";

			// Token: 0x04007FD2 RID: 32722
			public static LocString SEARCH = "Search";

			// Token: 0x04007FD3 RID: 32723
			public static LocString CRITERIA_HEADER_TOOLTIP = "Expand or collapse diagnostic criteria panel";

			// Token: 0x04007FD4 RID: 32724
			public static LocString SEE_ALL = "+ See All ({0})";

			// Token: 0x04007FD5 RID: 32725
			public static LocString CRITERIA_TOOLTIP = "Toggle the <b>{0}</b> diagnostics evaluation of the <b>{1}</b> criteria.";

			// Token: 0x04007FD6 RID: 32726
			public static LocString CRITERIA_ENABLED_COUNT = "{0}/{1} criteria enabled";

			// Token: 0x020023C4 RID: 9156
			public class CLICK_TOGGLE_MESSAGE
			{
				// Token: 0x04009C21 RID: 39969
				public static LocString ALWAYS = UI.CLICK(UI.ClickType.Click) + " to pin this diagnostic to the sidebar - Current State: <b>Visible On Alert Only</b>";

				// Token: 0x04009C22 RID: 39970
				public static LocString ALERT_ONLY = UI.CLICK(UI.ClickType.Click) + " to subscribe to this diagnostic - Current State: <b>Never Visible</b>";

				// Token: 0x04009C23 RID: 39971
				public static LocString NEVER = UI.CLICK(UI.ClickType.Click) + " to mute this diagnostic on the sidebar - Current State: <b>Always Visible</b>";

				// Token: 0x04009C24 RID: 39972
				public static LocString TUTORIAL_DISABLED = UI.CLICK(UI.ClickType.Click) + " to enable this diagnostic -  Current State: <b>Temporarily disabled</b>";
			}
		}

		// Token: 0x02001C77 RID: 7287
		public class WORLD_SELECTOR_SCREEN
		{
			// Token: 0x04007FD7 RID: 32727
			public static LocString TITLE = UI.CLUSTERMAP.PLANETOID;
		}

		// Token: 0x02001C78 RID: 7288
		public class COLONY_DIAGNOSTICS
		{
			// Token: 0x04007FD8 RID: 32728
			public static LocString NO_MINIONS = "    • There are no Duplicants on this {0}";

			// Token: 0x04007FD9 RID: 32729
			public static LocString ROCKET = "rocket";

			// Token: 0x04007FDA RID: 32730
			public static LocString NO_MINIONS_REQUESTED = "    • Crew must be requested to update this diagnostic";

			// Token: 0x04007FDB RID: 32731
			public static LocString NO_DATA = "    • Not enough data for evaluation";

			// Token: 0x04007FDC RID: 32732
			public static LocString NO_DATA_SHORT = "    • No data";

			// Token: 0x04007FDD RID: 32733
			public static LocString MUTE_TUTORIAL = "Diagnostic can be muted in the <b><color=#E5B000>See All</color></b> panel";

			// Token: 0x04007FDE RID: 32734
			public static LocString GENERIC_STATUS_NORMAL = "All values nominal";

			// Token: 0x04007FDF RID: 32735
			public static LocString PLACEHOLDER_CRITERIA_NAME = "Placeholder Criteria Name";

			// Token: 0x04007FE0 RID: 32736
			public static LocString GENERIC_CRITERIA_PASS = "Criteria met";

			// Token: 0x04007FE1 RID: 32737
			public static LocString GENERIC_CRITERIA_FAIL = "Criteria not met";

			// Token: 0x020023C5 RID: 9157
			public class GENERIC_CRITERIA
			{
				// Token: 0x04009C25 RID: 39973
				public static LocString CHECKWORLDHASMINIONS = "Check world has Duplicants";
			}

			// Token: 0x020023C6 RID: 9158
			public class IDLEDIAGNOSTIC
			{
				// Token: 0x04009C26 RID: 39974
				public static LocString ALL_NAME = "Idleness";

				// Token: 0x04009C27 RID: 39975
				public static LocString TOOLTIP_NAME = "<b>Idleness</b>";

				// Token: 0x04009C28 RID: 39976
				public static LocString NORMAL = "    • All Duplicants currently have tasks";

				// Token: 0x04009C29 RID: 39977
				public static LocString IDLE = "    • One or more Duplicants are idle";

				// Token: 0x02002E0B RID: 11787
				public static class CRITERIA
				{
					// Token: 0x0400BB0E RID: 47886
					public static LocString CHECKIDLE = "Check idle";
				}
			}

			// Token: 0x020023C7 RID: 9159
			public class CHOREGROUPDIAGNOSTIC
			{
				// Token: 0x04009C2A RID: 39978
				public static LocString ALL_NAME = UI.COLONY_DIAGNOSTICS.ALLCHORESDIAGNOSTIC.ALL_NAME;

				// Token: 0x02002E0C RID: 11788
				public static class CRITERIA
				{
				}
			}

			// Token: 0x020023C8 RID: 9160
			public class ALLCHORESDIAGNOSTIC
			{
				// Token: 0x04009C2B RID: 39979
				public static LocString ALL_NAME = "Errands";

				// Token: 0x04009C2C RID: 39980
				public static LocString TOOLTIP_NAME = "<b>Errands</b>";

				// Token: 0x04009C2D RID: 39981
				public static LocString NORMAL = "    • {0} errands pending or in progress";

				// Token: 0x02002E0D RID: 11789
				public static class CRITERIA
				{
				}
			}

			// Token: 0x020023C9 RID: 9161
			public class WORKTIMEDIAGNOSTIC
			{
				// Token: 0x04009C2E RID: 39982
				public static LocString ALL_NAME = UI.COLONY_DIAGNOSTICS.ALLCHORESDIAGNOSTIC.ALL_NAME;

				// Token: 0x02002E0E RID: 11790
				public static class CRITERIA
				{
				}
			}

			// Token: 0x020023CA RID: 9162
			public class ALLWORKTIMEDIAGNOSTIC
			{
				// Token: 0x04009C2F RID: 39983
				public static LocString ALL_NAME = "Work Time";

				// Token: 0x04009C30 RID: 39984
				public static LocString TOOLTIP_NAME = "<b>Work Time</b>";

				// Token: 0x04009C31 RID: 39985
				public static LocString NORMAL = "    • {0} of Duplicant time spent working";

				// Token: 0x02002E0F RID: 11791
				public static class CRITERIA
				{
				}
			}

			// Token: 0x020023CB RID: 9163
			public class TRAVEL_TIME
			{
				// Token: 0x04009C32 RID: 39986
				public static LocString ALL_NAME = "Travel Time";

				// Token: 0x04009C33 RID: 39987
				public static LocString TOOLTIP_NAME = "<b>Travel Time</b>";

				// Token: 0x04009C34 RID: 39988
				public static LocString NORMAL = "    • {0} of Duplicant time spent traveling between errands";

				// Token: 0x02002E10 RID: 11792
				public static class CRITERIA
				{
				}
			}

			// Token: 0x020023CC RID: 9164
			public class TRAPPEDDUPLICANTDIAGNOSTIC
			{
				// Token: 0x04009C35 RID: 39989
				public static LocString ALL_NAME = "Trapped";

				// Token: 0x04009C36 RID: 39990
				public static LocString TOOLTIP_NAME = "<b>Trapped</b>";

				// Token: 0x04009C37 RID: 39991
				public static LocString NORMAL = "    • No Duplicants are trapped";

				// Token: 0x04009C38 RID: 39992
				public static LocString STUCK = "    • One or more Duplicants are trapped";

				// Token: 0x02002E11 RID: 11793
				public static class CRITERIA
				{
					// Token: 0x0400BB0F RID: 47887
					public static LocString CHECKTRAPPED = "Check Trapped";
				}
			}

			// Token: 0x020023CD RID: 9165
			public class BREATHABILITYDIAGNOSTIC
			{
				// Token: 0x04009C39 RID: 39993
				public static LocString ALL_NAME = "Breathability";

				// Token: 0x04009C3A RID: 39994
				public static LocString TOOLTIP_NAME = "<b>Breathability</b>";

				// Token: 0x04009C3B RID: 39995
				public static LocString NORMAL = "    • Oxygen levels are satisfactory";

				// Token: 0x04009C3C RID: 39996
				public static LocString POOR = "    • Oxygen is becoming scarce or low pressure";

				// Token: 0x04009C3D RID: 39997
				public static LocString SUFFOCATING = "    • One or more Duplicants are suffocating";

				// Token: 0x02002E12 RID: 11794
				public static class CRITERIA
				{
					// Token: 0x0400BB10 RID: 47888
					public static LocString CHECKSUFFOCATION = "Check suffocation";

					// Token: 0x0400BB11 RID: 47889
					public static LocString CHECKLOWBREATHABILITY = "Check low breathability";
				}
			}

			// Token: 0x020023CE RID: 9166
			public class STRESSDIAGNOSTIC
			{
				// Token: 0x04009C3E RID: 39998
				public static LocString ALL_NAME = "Max Stress";

				// Token: 0x04009C3F RID: 39999
				public static LocString TOOLTIP_NAME = "<b>Max Stress</b>";

				// Token: 0x04009C40 RID: 40000
				public static LocString HIGH_STRESS = "    • One or more Duplicants is suffering high stress";

				// Token: 0x04009C41 RID: 40001
				public static LocString NORMAL = "    • Duplicants have acceptable stress levels";

				// Token: 0x02002E13 RID: 11795
				public static class CRITERIA
				{
					// Token: 0x0400BB12 RID: 47890
					public static LocString CHECKSTRESSED = "Check stressed";
				}
			}

			// Token: 0x020023CF RID: 9167
			public class DECORDIAGNOSTIC
			{
				// Token: 0x04009C42 RID: 40002
				public static LocString ALL_NAME = "Decor";

				// Token: 0x04009C43 RID: 40003
				public static LocString TOOLTIP_NAME = "<b>Decor</b>";

				// Token: 0x04009C44 RID: 40004
				public static LocString LOW = "    • Decor levels are low";

				// Token: 0x04009C45 RID: 40005
				public static LocString NORMAL = "    • Decor levels are satisfactory";

				// Token: 0x02002E14 RID: 11796
				public static class CRITERIA
				{
					// Token: 0x0400BB13 RID: 47891
					public static LocString CHECKDECOR = "Check decor";
				}
			}

			// Token: 0x020023D0 RID: 9168
			public class TOILETDIAGNOSTIC
			{
				// Token: 0x04009C46 RID: 40006
				public static LocString ALL_NAME = "Toilets";

				// Token: 0x04009C47 RID: 40007
				public static LocString TOOLTIP_NAME = "<b>Toilets</b>";

				// Token: 0x04009C48 RID: 40008
				public static LocString NO_TOILETS = "    • Colony has no toilets";

				// Token: 0x04009C49 RID: 40009
				public static LocString NO_WORKING_TOILETS = "    • Colony has no working toilets";

				// Token: 0x04009C4A RID: 40010
				public static LocString TOILET_URGENT = "    • Duplicants urgently need to use a toilet";

				// Token: 0x04009C4B RID: 40011
				public static LocString FEW_TOILETS = "    • Toilet-to-Duplicant ratio is low";

				// Token: 0x04009C4C RID: 40012
				public static LocString INOPERATIONAL = "    • One or more toilets are out of order";

				// Token: 0x04009C4D RID: 40013
				public static LocString NORMAL = "    • Colony has adequate working toilets";

				// Token: 0x02002E15 RID: 11797
				public static class CRITERIA
				{
					// Token: 0x0400BB14 RID: 47892
					public static LocString CHECKHASANYTOILETS = "Check has any toilets";

					// Token: 0x0400BB15 RID: 47893
					public static LocString CHECKENOUGHTOILETS = "Check enough toilets";

					// Token: 0x0400BB16 RID: 47894
					public static LocString CHECKBLADDERS = "Check Duplicants really need to use the toilet";
				}
			}

			// Token: 0x020023D1 RID: 9169
			public class BEDDIAGNOSTIC
			{
				// Token: 0x04009C4E RID: 40014
				public static LocString ALL_NAME = "Beds";

				// Token: 0x04009C4F RID: 40015
				public static LocString TOOLTIP_NAME = "<b>Beds</b>";

				// Token: 0x04009C50 RID: 40016
				public static LocString NORMAL = "    • Colony has adequate bedding";

				// Token: 0x04009C51 RID: 40017
				public static LocString NOT_ENOUGH_BEDS = "    • One or more Duplicants are missing a bed";

				// Token: 0x04009C52 RID: 40018
				public static LocString MISSING_ASSIGNMENT = "    • One or more Duplicants don't have an assigned bed";

				// Token: 0x02002E16 RID: 11798
				public static class CRITERIA
				{
					// Token: 0x0400BB17 RID: 47895
					public static LocString CHECKENOUGHBEDS = "Check enough beds";
				}
			}

			// Token: 0x020023D2 RID: 9170
			public class FOODDIAGNOSTIC
			{
				// Token: 0x04009C53 RID: 40019
				public static LocString ALL_NAME = "Food";

				// Token: 0x04009C54 RID: 40020
				public static LocString TOOLTIP_NAME = "<b>Food</b>";

				// Token: 0x04009C55 RID: 40021
				public static LocString NORMAL = "    • Food supply is currently adequate";

				// Token: 0x04009C56 RID: 40022
				public static LocString LOW_CALORIES = "    • Food-to-Duplicant ratio is low";

				// Token: 0x04009C57 RID: 40023
				public static LocString HUNGRY = "    • One or more Duplicants are very hungry";

				// Token: 0x04009C58 RID: 40024
				public static LocString NO_FOOD = "    • Duplicants have no food";

				// Token: 0x02002E17 RID: 11799
				public class CRITERIA_HAS_FOOD
				{
					// Token: 0x0400BB18 RID: 47896
					public static LocString PASS = "    • Duplicants have food";

					// Token: 0x0400BB19 RID: 47897
					public static LocString FAIL = "    • Duplicants have no food";
				}

				// Token: 0x02002E18 RID: 11800
				public static class CRITERIA
				{
					// Token: 0x0400BB1A RID: 47898
					public static LocString CHECKENOUGHFOOD = "Check enough food";

					// Token: 0x0400BB1B RID: 47899
					public static LocString CHECKSTARVATION = "Check starvation";
				}
			}

			// Token: 0x020023D3 RID: 9171
			public class FARMDIAGNOSTIC
			{
				// Token: 0x04009C59 RID: 40025
				public static LocString ALL_NAME = "Crops";

				// Token: 0x04009C5A RID: 40026
				public static LocString TOOLTIP_NAME = "<b>Crops</b>";

				// Token: 0x04009C5B RID: 40027
				public static LocString NORMAL = "    • Crops are being grown in sufficient quantity";

				// Token: 0x04009C5C RID: 40028
				public static LocString NONE = "    • No farm plots";

				// Token: 0x04009C5D RID: 40029
				public static LocString NONE_PLANTED = "    • No crops planted";

				// Token: 0x04009C5E RID: 40030
				public static LocString WILTING = "    • One or more crops are wilting";

				// Token: 0x04009C5F RID: 40031
				public static LocString INOPERATIONAL = "    • One or more farm plots are inoperable";

				// Token: 0x02002E19 RID: 11801
				public static class CRITERIA
				{
					// Token: 0x0400BB1C RID: 47900
					public static LocString CHECKHASFARMS = "Check colony has farms";

					// Token: 0x0400BB1D RID: 47901
					public static LocString CHECKPLANTED = "Check farms are planted";

					// Token: 0x0400BB1E RID: 47902
					public static LocString CHECKWILTING = "Check crops wilting";

					// Token: 0x0400BB1F RID: 47903
					public static LocString CHECKOPERATIONAL = "Check farm plots operational";
				}
			}

			// Token: 0x020023D4 RID: 9172
			public class POWERUSEDIAGNOSTIC
			{
				// Token: 0x04009C60 RID: 40032
				public static LocString ALL_NAME = "Power use";

				// Token: 0x04009C61 RID: 40033
				public static LocString TOOLTIP_NAME = "<b>Power use</b>";

				// Token: 0x04009C62 RID: 40034
				public static LocString NORMAL = "    • Power supply is satisfactory";

				// Token: 0x04009C63 RID: 40035
				public static LocString OVERLOADED = "    • One or more power grids are damaged";

				// Token: 0x04009C64 RID: 40036
				public static LocString SIGNIFICANT_POWER_CHANGE_DETECTED = "Significant power use change detected. (Average:{0}, Current:{1})";

				// Token: 0x04009C65 RID: 40037
				public static LocString CIRCUIT_OVER_CAPACITY = "Circuit overloaded {0}/{1}";

				// Token: 0x02002E1A RID: 11802
				public static class CRITERIA
				{
					// Token: 0x0400BB20 RID: 47904
					public static LocString CHECKOVERWATTAGE = "Check circuit overloaded";

					// Token: 0x0400BB21 RID: 47905
					public static LocString CHECKPOWERUSECHANGE = "Check power use change";
				}
			}

			// Token: 0x020023D5 RID: 9173
			public class HEATDIAGNOSTIC
			{
				// Token: 0x04009C66 RID: 40038
				public static LocString ALL_NAME = UI.COLONY_DIAGNOSTICS.BATTERYDIAGNOSTIC.ALL_NAME;

				// Token: 0x02002E1B RID: 11803
				public static class CRITERIA
				{
					// Token: 0x0400BB22 RID: 47906
					public static LocString CHECKHEAT = "Check heat";
				}
			}

			// Token: 0x020023D6 RID: 9174
			public class BATTERYDIAGNOSTIC
			{
				// Token: 0x04009C67 RID: 40039
				public static LocString ALL_NAME = "Battery";

				// Token: 0x04009C68 RID: 40040
				public static LocString TOOLTIP_NAME = "<b>Battery</b>";

				// Token: 0x04009C69 RID: 40041
				public static LocString NORMAL = "    • All batteries functional";

				// Token: 0x04009C6A RID: 40042
				public static LocString NONE = "    • No batteries are connected to a power grid";

				// Token: 0x04009C6B RID: 40043
				public static LocString DEAD_BATTERY = "    • One or more batteries have died";

				// Token: 0x04009C6C RID: 40044
				public static LocString LIMITED_CAPACITY = "    • Low battery capacity relative to power use";

				// Token: 0x02002E1C RID: 11804
				public class CRITERIA_CHECK_CAPACITY
				{
					// Token: 0x0400BB23 RID: 47907
					public static LocString PASS = "";

					// Token: 0x0400BB24 RID: 47908
					public static LocString FAIL = "";
				}

				// Token: 0x02002E1D RID: 11805
				public static class CRITERIA
				{
					// Token: 0x0400BB25 RID: 47909
					public static LocString CHECKCAPACITY = "Check capacity";

					// Token: 0x0400BB26 RID: 47910
					public static LocString CHECKDEAD = "Check dead";
				}
			}

			// Token: 0x020023D7 RID: 9175
			public class RADIATIONDIAGNOSTIC
			{
				// Token: 0x04009C6D RID: 40045
				public static LocString ALL_NAME = "Radiation";

				// Token: 0x04009C6E RID: 40046
				public static LocString TOOLTIP_NAME = "<b>Radiation</b>";

				// Token: 0x04009C6F RID: 40047
				public static LocString NORMAL = "    • No Radiation concerns";

				// Token: 0x04009C70 RID: 40048
				public static LocString AVERAGE_RADS = "Avg. {0}";

				// Token: 0x02002E1E RID: 11806
				public class CRITERIA_RADIATION_SICKNESS
				{
					// Token: 0x0400BB27 RID: 47911
					public static LocString PASS = "Healthy";

					// Token: 0x0400BB28 RID: 47912
					public static LocString FAIL = "Sick";
				}

				// Token: 0x02002E1F RID: 11807
				public class CRITERIA_RADIATION_EXPOSURE
				{
					// Token: 0x0400BB29 RID: 47913
					public static LocString PASS = "Safe exposure levels";

					// Token: 0x0400BB2A RID: 47914
					public static LocString FAIL_CONCERN = "Exposure levels are above safe limits for one or more Duplicants";

					// Token: 0x0400BB2B RID: 47915
					public static LocString FAIL_WARNING = "One or more Duplicants are being exposed to extreme levels of radiation";
				}

				// Token: 0x02002E20 RID: 11808
				public static class CRITERIA
				{
					// Token: 0x0400BB2C RID: 47916
					public static LocString CHECKSICK = "Check sick";

					// Token: 0x0400BB2D RID: 47917
					public static LocString CHECKEXPOSED = "Check exposed";
				}
			}

			// Token: 0x020023D8 RID: 9176
			public class ENTOMBEDDIAGNOSTIC
			{
				// Token: 0x04009C71 RID: 40049
				public static LocString ALL_NAME = "Entombed";

				// Token: 0x04009C72 RID: 40050
				public static LocString TOOLTIP_NAME = "<b>Entombed</b>";

				// Token: 0x04009C73 RID: 40051
				public static LocString NORMAL = "    • No buildings are entombed";

				// Token: 0x04009C74 RID: 40052
				public static LocString BUILDING_ENTOMBED = "    • One or more buildings are entombed";

				// Token: 0x02002E21 RID: 11809
				public static class CRITERIA
				{
					// Token: 0x0400BB2E RID: 47918
					public static LocString CHECKENTOMBED = "Check entombed";
				}
			}

			// Token: 0x020023D9 RID: 9177
			public class ROCKETFUELDIAGNOSTIC
			{
				// Token: 0x04009C75 RID: 40053
				public static LocString ALL_NAME = "Rocket Fuel";

				// Token: 0x04009C76 RID: 40054
				public static LocString TOOLTIP_NAME = "<b>Rocket Fuel</b>";

				// Token: 0x04009C77 RID: 40055
				public static LocString NORMAL = "    • This rocket has sufficient fuel";

				// Token: 0x04009C78 RID: 40056
				public static LocString WARNING = "    • This rocket has no fuel";

				// Token: 0x02002E22 RID: 11810
				public static class CRITERIA
				{
				}
			}

			// Token: 0x020023DA RID: 9178
			public class ROCKETOXIDIZERDIAGNOSTIC
			{
				// Token: 0x04009C79 RID: 40057
				public static LocString ALL_NAME = "Rocket Oxidizer";

				// Token: 0x04009C7A RID: 40058
				public static LocString TOOLTIP_NAME = "<b>Rocket Oxidizer</b>";

				// Token: 0x04009C7B RID: 40059
				public static LocString NORMAL = "    • This rocket has sufficient oxidizer";

				// Token: 0x04009C7C RID: 40060
				public static LocString WARNING = "    • This rocket has insufficient oxidizer";

				// Token: 0x02002E23 RID: 11811
				public static class CRITERIA
				{
				}
			}

			// Token: 0x020023DB RID: 9179
			public class REACTORDIAGNOSTIC
			{
				// Token: 0x04009C7D RID: 40061
				public static LocString ALL_NAME = BUILDINGS.PREFABS.NUCLEARREACTOR.NAME;

				// Token: 0x04009C7E RID: 40062
				public static LocString TOOLTIP_NAME = BUILDINGS.PREFABS.NUCLEARREACTOR.NAME;

				// Token: 0x04009C7F RID: 40063
				public static LocString NORMAL = "    • Safe";

				// Token: 0x04009C80 RID: 40064
				public static LocString CRITERIA_TEMPERATURE_WARNING = "    • Temperature dangerously high";

				// Token: 0x04009C81 RID: 40065
				public static LocString CRITERIA_COOLANT_WARNING = "    • Coolant tank low";

				// Token: 0x02002E24 RID: 11812
				public static class CRITERIA
				{
					// Token: 0x0400BB2F RID: 47919
					public static LocString CHECKTEMPERATURE = "Check temperature";

					// Token: 0x0400BB30 RID: 47920
					public static LocString CHECKCOOLANT = "Check coolant";
				}
			}

			// Token: 0x020023DC RID: 9180
			public class FLOATINGROCKETDIAGNOSTIC
			{
				// Token: 0x04009C82 RID: 40066
				public static LocString ALL_NAME = "Flight Status";

				// Token: 0x04009C83 RID: 40067
				public static LocString TOOLTIP_NAME = "<b>Flight Status</b>";

				// Token: 0x04009C84 RID: 40068
				public static LocString NORMAL_FLIGHT = "    • This rocket is in flight towards its destination";

				// Token: 0x04009C85 RID: 40069
				public static LocString NORMAL_UTILITY = "    • This rocket is performing a task at its destination";

				// Token: 0x04009C86 RID: 40070
				public static LocString NORMAL_LANDED = "    • This rocket is currently landed on a " + UI.PRE_KEYWORD + "Rocket Platform" + UI.PST_KEYWORD;

				// Token: 0x04009C87 RID: 40071
				public static LocString WARNING_NO_DESTINATION = "    • This rocket is suspended in space with no set destination";

				// Token: 0x04009C88 RID: 40072
				public static LocString WARNING_NO_SPEED = "    • This rocket's flight has been halted";

				// Token: 0x02002E25 RID: 11813
				public static class CRITERIA
				{
				}
			}

			// Token: 0x020023DD RID: 9181
			public class ROCKETINORBITDIAGNOSTIC
			{
				// Token: 0x04009C89 RID: 40073
				public static LocString ALL_NAME = "Rockets in Orbit";

				// Token: 0x04009C8A RID: 40074
				public static LocString TOOLTIP_NAME = "<b>Rockets in Orbit</b>";

				// Token: 0x04009C8B RID: 40075
				public static LocString NORMAL_ONE_IN_ORBIT = "    • {0} is in orbit waiting to land";

				// Token: 0x04009C8C RID: 40076
				public static LocString NORMAL_IN_ORBIT = "    • There are {0} rockets in orbit waiting to land";

				// Token: 0x04009C8D RID: 40077
				public static LocString WARNING_ONE_ROCKETS_STRANDED = "    • No " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + " present. {0} stranded";

				// Token: 0x04009C8E RID: 40078
				public static LocString WARNING_ROCKETS_STRANDED = "    • No " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + " present. {0} rockets stranded";

				// Token: 0x04009C8F RID: 40079
				public static LocString NORMAL_NO_ROCKETS = "    • No rockets waiting to land";

				// Token: 0x02002E26 RID: 11814
				public static class CRITERIA
				{
					// Token: 0x0400BB31 RID: 47921
					public static LocString CHECKORBIT = "Check Orbiting Rockets";
				}
			}
		}

		// Token: 0x02001C79 RID: 7289
		public class TRACKERS
		{
			// Token: 0x04007FE2 RID: 32738
			public static LocString BREATHABILITY = "Breathability";

			// Token: 0x04007FE3 RID: 32739
			public static LocString FOOD = "Food";

			// Token: 0x04007FE4 RID: 32740
			public static LocString STRESS = "Max Stress";

			// Token: 0x04007FE5 RID: 32741
			public static LocString IDLE = "Idle Duplicants";
		}

		// Token: 0x02001C7A RID: 7290
		public class CONTROLS
		{
			// Token: 0x04007FE6 RID: 32742
			public static LocString PRESS = "Press";

			// Token: 0x04007FE7 RID: 32743
			public static LocString PRESSLOWER = "press";

			// Token: 0x04007FE8 RID: 32744
			public static LocString PRESSUPPER = "PRESS";

			// Token: 0x04007FE9 RID: 32745
			public static LocString PRESSING = "Pressing";

			// Token: 0x04007FEA RID: 32746
			public static LocString PRESSINGLOWER = "pressing";

			// Token: 0x04007FEB RID: 32747
			public static LocString PRESSINGUPPER = "PRESSING";

			// Token: 0x04007FEC RID: 32748
			public static LocString PRESSED = "Pressed";

			// Token: 0x04007FED RID: 32749
			public static LocString PRESSEDLOWER = "pressed";

			// Token: 0x04007FEE RID: 32750
			public static LocString PRESSEDUPPER = "PRESSED";

			// Token: 0x04007FEF RID: 32751
			public static LocString PRESSES = "Presses";

			// Token: 0x04007FF0 RID: 32752
			public static LocString PRESSESLOWER = "presses";

			// Token: 0x04007FF1 RID: 32753
			public static LocString PRESSESUPPER = "PRESSES";

			// Token: 0x04007FF2 RID: 32754
			public static LocString PRESSABLE = "Pressable";

			// Token: 0x04007FF3 RID: 32755
			public static LocString PRESSABLELOWER = "pressable";

			// Token: 0x04007FF4 RID: 32756
			public static LocString PRESSABLEUPPER = "PRESSABLE";

			// Token: 0x04007FF5 RID: 32757
			public static LocString CLICK = "Click";

			// Token: 0x04007FF6 RID: 32758
			public static LocString CLICKLOWER = "click";

			// Token: 0x04007FF7 RID: 32759
			public static LocString CLICKUPPER = "CLICK";

			// Token: 0x04007FF8 RID: 32760
			public static LocString CLICKING = "Clicking";

			// Token: 0x04007FF9 RID: 32761
			public static LocString CLICKINGLOWER = "clicking";

			// Token: 0x04007FFA RID: 32762
			public static LocString CLICKINGUPPER = "CLICKING";

			// Token: 0x04007FFB RID: 32763
			public static LocString CLICKED = "Clicked";

			// Token: 0x04007FFC RID: 32764
			public static LocString CLICKEDLOWER = "clicked";

			// Token: 0x04007FFD RID: 32765
			public static LocString CLICKEDUPPER = "CLICKED";

			// Token: 0x04007FFE RID: 32766
			public static LocString CLICKS = "Clicks";

			// Token: 0x04007FFF RID: 32767
			public static LocString CLICKSLOWER = "clicks";

			// Token: 0x04008000 RID: 32768
			public static LocString CLICKSUPPER = "CLICKS";

			// Token: 0x04008001 RID: 32769
			public static LocString CLICKABLE = "Clickable";

			// Token: 0x04008002 RID: 32770
			public static LocString CLICKABLELOWER = "clickable";

			// Token: 0x04008003 RID: 32771
			public static LocString CLICKABLEUPPER = "CLICKABLE";
		}

		// Token: 0x02001C7B RID: 7291
		public class MATH_PICTURES
		{
			// Token: 0x020023DE RID: 9182
			public class AXIS_LABELS
			{
				// Token: 0x04009C90 RID: 40080
				public static LocString CYCLES = "Cycles";
			}
		}

		// Token: 0x02001C7C RID: 7292
		public class SPACEDESTINATIONS
		{
			// Token: 0x020023DF RID: 9183
			public class WORMHOLE
			{
				// Token: 0x04009C91 RID: 40081
				public static LocString NAME = "Temporal Tear";

				// Token: 0x04009C92 RID: 40082
				public static LocString DESCRIPTION = "The source of our misfortune, though it may also be our shot at freedom. Traces of Neutronium are detectable in my readings.";
			}

			// Token: 0x020023E0 RID: 9184
			public class RESEARCHDESTINATION
			{
				// Token: 0x04009C93 RID: 40083
				public static LocString NAME = "Alluring Anomaly";

				// Token: 0x04009C94 RID: 40084
				public static LocString DESCRIPTION = "Our researchers would have a field day with this if they could only get close enough.";
			}

			// Token: 0x020023E1 RID: 9185
			public class DEBRIS
			{
				// Token: 0x02002E27 RID: 11815
				public class SATELLITE
				{
					// Token: 0x0400BB32 RID: 47922
					public static LocString NAME = "Satellite";

					// Token: 0x0400BB33 RID: 47923
					public static LocString DESCRIPTION = "An artificial construct that has escaped its orbit. It no longer appears to be monitored.";
				}
			}

			// Token: 0x020023E2 RID: 9186
			public class NONE
			{
				// Token: 0x04009C95 RID: 40085
				public static LocString NAME = "Unselected";
			}

			// Token: 0x020023E3 RID: 9187
			public class ORBIT
			{
				// Token: 0x04009C96 RID: 40086
				public static LocString NAME_FMT = "Orbiting {Name}";
			}

			// Token: 0x020023E4 RID: 9188
			public class EMPTY_SPACE
			{
				// Token: 0x04009C97 RID: 40087
				public static LocString NAME = "Empty Space";
			}

			// Token: 0x020023E5 RID: 9189
			public class FOG_OF_WAR_SPACE
			{
				// Token: 0x04009C98 RID: 40088
				public static LocString NAME = "Unexplored Space";
			}

			// Token: 0x020023E6 RID: 9190
			public class ARTIFACT_POI
			{
				// Token: 0x02002E28 RID: 11816
				public class GRAVITASSPACESTATION1
				{
					// Token: 0x0400BB34 RID: 47924
					public static LocString NAME = "Destroyed Satellite";

					// Token: 0x0400BB35 RID: 47925
					public static LocString DESC = "The remnants of a bygone era, lost in time.\n\nHarvesting space junk requires a rocket equipped with a " + UI.FormatAsLink("Drillcone", "NOSECONEHARVEST") + ".";
				}

				// Token: 0x02002E29 RID: 11817
				public class GRAVITASSPACESTATION2
				{
					// Token: 0x0400BB36 RID: 47926
					public static LocString NAME = "Demolished Rocket";

					// Token: 0x0400BB37 RID: 47927
					public static LocString DESC = "A defunct rocket from a corporation that vanished long ago.\n\nHarvesting space junk requires a rocket equipped with a " + UI.FormatAsLink("Drillcone", "NOSECONEHARVEST") + ".";
				}

				// Token: 0x02002E2A RID: 11818
				public class GRAVITASSPACESTATION3
				{
					// Token: 0x0400BB38 RID: 47928
					public static LocString NAME = "Ruined Rocket";

					// Token: 0x0400BB39 RID: 47929
					public static LocString DESC = "The ruins of a rocket that stopped functioning ages ago.\n\nHarvesting space junk requires a rocket equipped with a " + UI.FormatAsLink("Drillcone", "NOSECONEHARVEST") + ".";
				}

				// Token: 0x02002E2B RID: 11819
				public class GRAVITASSPACESTATION4
				{
					// Token: 0x0400BB3A RID: 47930
					public static LocString NAME = "Retired Planetary Excursion Module";

					// Token: 0x0400BB3B RID: 47931
					public static LocString DESC = "A rocket part from a society that has been wiped out.\n\nHarvesting space junk requires a rocket equipped with a " + UI.FormatAsLink("Drillcone", "NOSECONEHARVEST") + ".";
				}

				// Token: 0x02002E2C RID: 11820
				public class GRAVITASSPACESTATION5
				{
					// Token: 0x0400BB3C RID: 47932
					public static LocString NAME = "Destroyed Satellite";

					// Token: 0x0400BB3D RID: 47933
					public static LocString DESC = "A destroyed Gravitas satellite.\n\nHarvesting space junk requires a rocket equipped with a " + UI.FormatAsLink("Drillcone", "NOSECONEHARVEST") + ".";
				}

				// Token: 0x02002E2D RID: 11821
				public class GRAVITASSPACESTATION6
				{
					// Token: 0x0400BB3E RID: 47934
					public static LocString NAME = "Annihilated Satellite";

					// Token: 0x0400BB3F RID: 47935
					public static LocString DESC = "The remains of a satellite made some time in the past.\n\nHarvesting space junk requires a rocket equipped with a " + UI.FormatAsLink("Drillcone", "NOSECONEHARVEST") + ".";
				}

				// Token: 0x02002E2E RID: 11822
				public class GRAVITASSPACESTATION7
				{
					// Token: 0x0400BB40 RID: 47936
					public static LocString NAME = "Wrecked Space Shuttle";

					// Token: 0x0400BB41 RID: 47937
					public static LocString DESC = "A defunct space shuttle that floats through space unattended.\n\nHarvesting space junk requires a rocket equipped with a " + UI.FormatAsLink("Drillcone", "NOSECONEHARVEST") + ".";
				}

				// Token: 0x02002E2F RID: 11823
				public class GRAVITASSPACESTATION8
				{
					// Token: 0x0400BB42 RID: 47938
					public static LocString NAME = "Obsolete Space Station Module";

					// Token: 0x0400BB43 RID: 47939
					public static LocString DESC = "The module from a space station that ceased to exist ages ago.\n\nHarvesting space junk requires a rocket equipped with a " + UI.FormatAsLink("Drillcone", "NOSECONEHARVEST") + ".";
				}

				// Token: 0x02002E30 RID: 11824
				public class RUSSELLSTEAPOT
				{
					// Token: 0x0400BB44 RID: 47940
					public static LocString NAME = "Russell's Teapot";

					// Token: 0x0400BB45 RID: 47941
					public static LocString DESC = "Has never been disproven to not exist.";
				}
			}

			// Token: 0x020023E7 RID: 9191
			public class HARVESTABLE_POI
			{
				// Token: 0x04009C99 RID: 40089
				public static LocString POI_PRODUCTION = "{0}";

				// Token: 0x04009C9A RID: 40090
				public static LocString POI_PRODUCTION_TOOLTIP = "{0}";

				// Token: 0x02002E31 RID: 11825
				public class CARBONASTEROIDFIELD
				{
					// Token: 0x0400BB46 RID: 47942
					public static LocString NAME = "Carbon Asteroid Field";

					// Token: 0x0400BB47 RID: 47943
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid containing ",
						UI.FormatAsLink("Refined Carbon", "REFINEDCARBON"),
						" and ",
						UI.FormatAsLink("Coal", "CARBON"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02002E32 RID: 11826
				public class METALLICASTEROIDFIELD
				{
					// Token: 0x0400BB48 RID: 47944
					public static LocString NAME = "Metallic Asteroid Field";

					// Token: 0x0400BB49 RID: 47945
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Iron", "IRON"),
						", ",
						UI.FormatAsLink("Copper", "COPPER"),
						" and ",
						UI.FormatAsLink("Obsidian", "OBSIDIAN"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02002E33 RID: 11827
				public class SATELLITEFIELD
				{
					// Token: 0x0400BB4A RID: 47946
					public static LocString NAME = "Space Debris";

					// Token: 0x0400BB4B RID: 47947
					public static LocString DESC = "Space junk from a forgotten age.\n\nHarvesting resources requires a rocket equipped with a " + UI.FormatAsLink("Drillcone", "NOSECONEHARVEST") + ".";
				}

				// Token: 0x02002E34 RID: 11828
				public class ROCKYASTEROIDFIELD
				{
					// Token: 0x0400BB4C RID: 47948
					public static LocString NAME = "Rocky Asteroid Field";

					// Token: 0x0400BB4D RID: 47949
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Copper Ore", "CUPRITE"),
						", ",
						UI.FormatAsLink("Sedimentary Rock", "SEDIMENTARYROCK"),
						" and ",
						UI.FormatAsLink("Igneous Rock", "IGNEOUSROCK"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02002E35 RID: 11829
				public class INTERSTELLARICEFIELD
				{
					// Token: 0x0400BB4E RID: 47950
					public static LocString NAME = "Ice Asteroid Field";

					// Token: 0x0400BB4F RID: 47951
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Ice", "ICE"),
						", ",
						UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
						" and ",
						UI.FormatAsLink("Oxygen", "OXYGEN"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02002E36 RID: 11830
				public class ORGANICMASSFIELD
				{
					// Token: 0x0400BB50 RID: 47952
					public static LocString NAME = "Organic Mass Field";

					// Token: 0x0400BB51 RID: 47953
					public static LocString DESC = string.Concat(new string[]
					{
						"A mass of harvestable resources containing ",
						UI.FormatAsLink("Algae", "ALGAE"),
						", ",
						UI.FormatAsLink("Slime", "SLIMEMOLD"),
						", ",
						UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN"),
						" and ",
						UI.FormatAsLink("Dirt", "DIRT"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02002E37 RID: 11831
				public class ICEASTEROIDFIELD
				{
					// Token: 0x0400BB52 RID: 47954
					public static LocString NAME = "Exploded Ice Giant";

					// Token: 0x0400BB53 RID: 47955
					public static LocString DESC = string.Concat(new string[]
					{
						"A cloud of planetary remains containing ",
						UI.FormatAsLink("Ice", "ICE"),
						", ",
						UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
						", ",
						UI.FormatAsLink("Oxygen", "OXYGEN"),
						" and ",
						UI.FormatAsLink("Methane", "METHANE"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02002E38 RID: 11832
				public class GASGIANTCLOUD
				{
					// Token: 0x0400BB54 RID: 47956
					public static LocString NAME = "Exploded Gas Giant";

					// Token: 0x0400BB55 RID: 47957
					public static LocString DESC = string.Concat(new string[]
					{
						"The harvestable remains of a planet containing ",
						UI.FormatAsLink("Hydrogen", "HYDROGEN"),
						" in ",
						UI.FormatAsLink("gas", "ELEMENTS_GAS"),
						" form, and ",
						UI.FormatAsLink("Methane", "SOLIDMETHANE"),
						" in ",
						UI.FormatAsLink("solid", "ELEMENTS_SOLID"),
						" and ",
						UI.FormatAsLink("liquid", "ELEMENTS_LIQUID"),
						" form.\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02002E39 RID: 11833
				public class CHLORINECLOUD
				{
					// Token: 0x0400BB56 RID: 47958
					public static LocString NAME = "Chlorine Cloud";

					// Token: 0x0400BB57 RID: 47959
					public static LocString DESC = string.Concat(new string[]
					{
						"A cloud of harvestable debris containing ",
						UI.FormatAsLink("Chlorine", "CHLORINEGAS"),
						" and ",
						UI.FormatAsLink("Bleach Stone", "BLEACHSTONE"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02002E3A RID: 11834
				public class GILDEDASTEROIDFIELD
				{
					// Token: 0x0400BB58 RID: 47960
					public static LocString NAME = "Gilded Asteroid Field";

					// Token: 0x0400BB59 RID: 47961
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Gold", "GOLD"),
						", ",
						UI.FormatAsLink("Fullerene", "FULLERENE"),
						", ",
						UI.FormatAsLink("Regolith", "REGOLITH"),
						" and more.\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02002E3B RID: 11835
				public class GLIMMERINGASTEROIDFIELD
				{
					// Token: 0x0400BB5A RID: 47962
					public static LocString NAME = "Glimmering Asteroid Field";

					// Token: 0x0400BB5B RID: 47963
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Tungsten", "TUNGSTEN"),
						", ",
						UI.FormatAsLink("Wolframite", "WOLFRAMITE"),
						" and more.\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02002E3C RID: 11836
				public class HELIUMCLOUD
				{
					// Token: 0x0400BB5C RID: 47964
					public static LocString NAME = "Helium Cloud";

					// Token: 0x0400BB5D RID: 47965
					public static LocString DESC = string.Concat(new string[]
					{
						"A cloud of resources containing ",
						UI.FormatAsLink("Water", "WATER"),
						" and ",
						UI.FormatAsLink("Hydrogen", "HYDROGEN"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02002E3D RID: 11837
				public class OILYASTEROIDFIELD
				{
					// Token: 0x0400BB5E RID: 47966
					public static LocString NAME = "Oily Asteroid Field";

					// Token: 0x0400BB5F RID: 47967
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Methane", "SOLIDMETHANE"),
						", ",
						UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
						" and ",
						UI.FormatAsLink("Crude Oil", "CRUDEOIL"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02002E3E RID: 11838
				public class OXIDIZEDASTEROIDFIELD
				{
					// Token: 0x0400BB60 RID: 47968
					public static LocString NAME = "Oxidized Asteroid Field";

					// Token: 0x0400BB61 RID: 47969
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
						" and ",
						UI.FormatAsLink("Rust", "RUST"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02002E3F RID: 11839
				public class SALTYASTEROIDFIELD
				{
					// Token: 0x0400BB62 RID: 47970
					public static LocString NAME = "Salty Asteroid Field";

					// Token: 0x0400BB63 RID: 47971
					public static LocString DESC = string.Concat(new string[]
					{
						"A field of harvestable resources containing ",
						UI.FormatAsLink("Salt Water", "SALTWATER"),
						",",
						UI.FormatAsLink("Brine", "BRINE"),
						" and ",
						UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02002E40 RID: 11840
				public class FROZENOREFIELD
				{
					// Token: 0x0400BB64 RID: 47972
					public static LocString NAME = "Frozen Ore Asteroid Field";

					// Token: 0x0400BB65 RID: 47973
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Polluted Ice", "DIRTYICE"),
						", ",
						UI.FormatAsLink("Ice", "ICE"),
						", ",
						UI.FormatAsLink("Snow", "SNOW"),
						" and ",
						UI.FormatAsLink("Aluminum Ore", "ALUMINUMORE"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02002E41 RID: 11841
				public class FORESTYOREFIELD
				{
					// Token: 0x0400BB66 RID: 47974
					public static LocString NAME = "Forested Ore Field";

					// Token: 0x0400BB67 RID: 47975
					public static LocString DESC = string.Concat(new string[]
					{
						"A field of harvestable resources containing ",
						UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
						", ",
						UI.FormatAsLink("Igneous Rock", "IGNEOUSROCK"),
						" and ",
						UI.FormatAsLink("Aluminum Ore", "ALUMINUMORE"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02002E42 RID: 11842
				public class SWAMPYOREFIELD
				{
					// Token: 0x0400BB68 RID: 47976
					public static LocString NAME = "Swampy Ore Field";

					// Token: 0x0400BB69 RID: 47977
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Mud", "MUD"),
						", ",
						UI.FormatAsLink("Polluted Dirt", "TOXICSAND"),
						" and ",
						UI.FormatAsLink("Cobalt Ore", "COBALTITE"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02002E43 RID: 11843
				public class SANDYOREFIELD
				{
					// Token: 0x0400BB6A RID: 47978
					public static LocString NAME = "Sandy Ore Field";

					// Token: 0x0400BB6B RID: 47979
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Sandstone", "SANDSTONE"),
						", ",
						UI.FormatAsLink("Algae", "ALGAE"),
						", ",
						UI.FormatAsLink("Copper Ore", "CUPRITE"),
						" and ",
						UI.FormatAsLink("Sand", "SAND"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02002E44 RID: 11844
				public class RADIOACTIVEGASCLOUD
				{
					// Token: 0x0400BB6C RID: 47980
					public static LocString NAME = "Radioactive Gas Cloud";

					// Token: 0x0400BB6D RID: 47981
					public static LocString DESC = string.Concat(new string[]
					{
						"A cloud of resources containing ",
						UI.FormatAsLink("Chlorine", "CHLORINEGAS"),
						", ",
						UI.FormatAsLink("Uranium Ore", "URANIUMORE"),
						" and ",
						UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02002E45 RID: 11845
				public class RADIOACTIVEASTEROIDFIELD
				{
					// Token: 0x0400BB6E RID: 47982
					public static LocString NAME = "Radioactive Asteroid Field";

					// Token: 0x0400BB6F RID: 47983
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Bleach Stone", "BLEACHSTONE"),
						", ",
						UI.FormatAsLink("Rust", "RUST"),
						", ",
						UI.FormatAsLink("Uranium Ore", "URANIUMORE"),
						" and ",
						UI.FormatAsLink("Sulfur", "SULFUR"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02002E46 RID: 11846
				public class OXYGENRICHASTEROIDFIELD
				{
					// Token: 0x0400BB70 RID: 47984
					public static LocString NAME = "Oxygen Rich Asteroid Field";

					// Token: 0x0400BB71 RID: 47985
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Ice", "ICE"),
						", ",
						UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN"),
						" and ",
						UI.FormatAsLink("Water", "WATER"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02002E47 RID: 11847
				public class INTERSTELLAROCEAN
				{
					// Token: 0x0400BB72 RID: 47986
					public static LocString NAME = "Interstellar Ocean";

					// Token: 0x0400BB73 RID: 47987
					public static LocString DESC = string.Concat(new string[]
					{
						"An interplanetary body that consists of ",
						UI.FormatAsLink("Salt Water", "SALTWATER"),
						", ",
						UI.FormatAsLink("Brine", "BRINE"),
						", ",
						UI.FormatAsLink("Salt", "SALT"),
						" and ",
						UI.FormatAsLink("Ice", "ICE"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}
			}

			// Token: 0x020023E8 RID: 9192
			public class GRAVITAS_SPACE_POI
			{
				// Token: 0x04009C9B RID: 40091
				public static LocString STATION = "Destroyed Gravitas Space Station";
			}

			// Token: 0x020023E9 RID: 9193
			public class TELESCOPE_TARGET
			{
				// Token: 0x04009C9C RID: 40092
				public static LocString NAME = "Telescope Target";
			}

			// Token: 0x020023EA RID: 9194
			public class ASTEROIDS
			{
				// Token: 0x02002E48 RID: 11848
				public class ROCKYASTEROID
				{
					// Token: 0x0400BB74 RID: 47988
					public static LocString NAME = "Rocky Asteroid";

					// Token: 0x0400BB75 RID: 47989
					public static LocString DESCRIPTION = "A minor mineral planet. Unlike a comet, it does not possess a tail.";
				}

				// Token: 0x02002E49 RID: 11849
				public class METALLICASTEROID
				{
					// Token: 0x0400BB76 RID: 47990
					public static LocString NAME = "Metallic Asteroid";

					// Token: 0x0400BB77 RID: 47991
					public static LocString DESCRIPTION = "A shimmering conglomerate of various metals.";
				}

				// Token: 0x02002E4A RID: 11850
				public class CARBONACEOUSASTEROID
				{
					// Token: 0x0400BB78 RID: 47992
					public static LocString NAME = "Carbon Asteroid";

					// Token: 0x0400BB79 RID: 47993
					public static LocString DESCRIPTION = "A common asteroid containing several useful resources.";
				}

				// Token: 0x02002E4B RID: 11851
				public class OILYASTEROID
				{
					// Token: 0x0400BB7A RID: 47994
					public static LocString NAME = "Oily Asteroid";

					// Token: 0x0400BB7B RID: 47995
					public static LocString DESCRIPTION = "A viscous asteroid that is only loosely held together. Contains fossil fuel resources.";
				}

				// Token: 0x02002E4C RID: 11852
				public class GOLDASTEROID
				{
					// Token: 0x0400BB7C RID: 47996
					public static LocString NAME = "Gilded Asteroid";

					// Token: 0x0400BB7D RID: 47997
					public static LocString DESCRIPTION = "A rich asteroid with thin gold coating and veins of gold deposits throughout.";
				}
			}

			// Token: 0x020023EB RID: 9195
			public class COMETS
			{
				// Token: 0x02002E4D RID: 11853
				public class ROCKCOMET
				{
					// Token: 0x0400BB7E RID: 47998
					public static LocString NAME = "Rock Comet";
				}

				// Token: 0x02002E4E RID: 11854
				public class DUSTCOMET
				{
					// Token: 0x0400BB7F RID: 47999
					public static LocString NAME = "Dust Comet";
				}

				// Token: 0x02002E4F RID: 11855
				public class IRONCOMET
				{
					// Token: 0x0400BB80 RID: 48000
					public static LocString NAME = "Iron Comet";
				}

				// Token: 0x02002E50 RID: 11856
				public class COPPERCOMET
				{
					// Token: 0x0400BB81 RID: 48001
					public static LocString NAME = "Copper Comet";
				}

				// Token: 0x02002E51 RID: 11857
				public class GOLDCOMET
				{
					// Token: 0x0400BB82 RID: 48002
					public static LocString NAME = "Gold Comet";
				}

				// Token: 0x02002E52 RID: 11858
				public class FULLERENECOMET
				{
					// Token: 0x0400BB83 RID: 48003
					public static LocString NAME = "Fullerene Comet";
				}

				// Token: 0x02002E53 RID: 11859
				public class URANIUMORECOMET
				{
					// Token: 0x0400BB84 RID: 48004
					public static LocString NAME = "Unanium Comet";
				}

				// Token: 0x02002E54 RID: 11860
				public class NUCLEAR_WASTE
				{
					// Token: 0x0400BB85 RID: 48005
					public static LocString NAME = "Radioactive Comet";
				}

				// Token: 0x02002E55 RID: 11861
				public class SATELLITE
				{
					// Token: 0x0400BB86 RID: 48006
					public static LocString NAME = "Defunct Satellite";
				}

				// Token: 0x02002E56 RID: 11862
				public class FOODCOMET
				{
					// Token: 0x0400BB87 RID: 48007
					public static LocString NAME = "Snack Bomb";
				}

				// Token: 0x02002E57 RID: 11863
				public class GASSYMOOCOMET
				{
					// Token: 0x0400BB88 RID: 48008
					public static LocString NAME = "Gassy Mooteor";
				}
			}

			// Token: 0x020023EC RID: 9196
			public class DWARFPLANETS
			{
				// Token: 0x02002E58 RID: 11864
				public class ICYDWARF
				{
					// Token: 0x0400BB89 RID: 48009
					public static LocString NAME = "Interstellar Ice";

					// Token: 0x0400BB8A RID: 48010
					public static LocString DESCRIPTION = "A terrestrial destination, frozen completely solid.";
				}

				// Token: 0x02002E59 RID: 11865
				public class ORGANICDWARF
				{
					// Token: 0x0400BB8B RID: 48011
					public static LocString NAME = "Organic Mass";

					// Token: 0x0400BB8C RID: 48012
					public static LocString DESCRIPTION = "A mass of organic material similar to the ooze used to print Duplicants. This sample is heavily degraded.";
				}

				// Token: 0x02002E5A RID: 11866
				public class DUSTYDWARF
				{
					// Token: 0x0400BB8D RID: 48013
					public static LocString NAME = "Dusty Dwarf";

					// Token: 0x0400BB8E RID: 48014
					public static LocString DESCRIPTION = "A loosely held together composite of minerals.";
				}

				// Token: 0x02002E5B RID: 11867
				public class SALTDWARF
				{
					// Token: 0x0400BB8F RID: 48015
					public static LocString NAME = "Salty Dwarf";

					// Token: 0x0400BB90 RID: 48016
					public static LocString DESCRIPTION = "A dwarf planet with unusually high sodium concentrations.";
				}

				// Token: 0x02002E5C RID: 11868
				public class REDDWARF
				{
					// Token: 0x0400BB91 RID: 48017
					public static LocString NAME = "Red Dwarf";

					// Token: 0x0400BB92 RID: 48018
					public static LocString DESCRIPTION = "An M-class star orbited by clusters of extractable aluminum and methane.";
				}
			}

			// Token: 0x020023ED RID: 9197
			public class PLANETS
			{
				// Token: 0x02002E5D RID: 11869
				public class TERRAPLANET
				{
					// Token: 0x0400BB93 RID: 48019
					public static LocString NAME = "Terrestrial Planet";

					// Token: 0x0400BB94 RID: 48020
					public static LocString DESCRIPTION = "A planet with a walkable surface, though it does not possess the resources to sustain long-term life.";
				}

				// Token: 0x02002E5E RID: 11870
				public class VOLCANOPLANET
				{
					// Token: 0x0400BB95 RID: 48021
					public static LocString NAME = "Volcanic Planet";

					// Token: 0x0400BB96 RID: 48022
					public static LocString DESCRIPTION = "A large terrestrial object composed mainly of molten rock.";
				}

				// Token: 0x02002E5F RID: 11871
				public class SHATTEREDPLANET
				{
					// Token: 0x0400BB97 RID: 48023
					public static LocString NAME = "Shattered Planet";

					// Token: 0x0400BB98 RID: 48024
					public static LocString DESCRIPTION = "A once-habitable planet that has sustained massive damage.\n\nA powerful containment field prevents our rockets from traveling to its surface.";
				}

				// Token: 0x02002E60 RID: 11872
				public class RUSTPLANET
				{
					// Token: 0x0400BB99 RID: 48025
					public static LocString NAME = "Oxidized Asteroid";

					// Token: 0x0400BB9A RID: 48026
					public static LocString DESCRIPTION = "A small planet covered in large swathes of brown rust.";
				}

				// Token: 0x02002E61 RID: 11873
				public class FORESTPLANET
				{
					// Token: 0x0400BB9B RID: 48027
					public static LocString NAME = "Living Planet";

					// Token: 0x0400BB9C RID: 48028
					public static LocString DESCRIPTION = "A small green planet displaying several markers of primitive life.";
				}

				// Token: 0x02002E62 RID: 11874
				public class SHINYPLANET
				{
					// Token: 0x0400BB9D RID: 48029
					public static LocString NAME = "Glimmering Planet";

					// Token: 0x0400BB9E RID: 48030
					public static LocString DESCRIPTION = "A planet composed of rare, shimmering minerals. From the distance, it looks like gem in the sky.";
				}

				// Token: 0x02002E63 RID: 11875
				public class CHLORINEPLANET
				{
					// Token: 0x0400BB9F RID: 48031
					public static LocString NAME = "Chlorine Planet";

					// Token: 0x0400BBA0 RID: 48032
					public static LocString DESCRIPTION = "A noxious planet permeated by unbreathable chlorine.";
				}

				// Token: 0x02002E64 RID: 11876
				public class SALTDESERTPLANET
				{
					// Token: 0x0400BBA1 RID: 48033
					public static LocString NAME = "Arid Planet";

					// Token: 0x0400BBA2 RID: 48034
					public static LocString DESCRIPTION = "A sweltering, desert-like planet covered in surface salt deposits.";
				}
			}

			// Token: 0x020023EE RID: 9198
			public class GIANTS
			{
				// Token: 0x02002E65 RID: 11877
				public class GASGIANT
				{
					// Token: 0x0400BBA3 RID: 48035
					public static LocString NAME = "Gas Giant";

					// Token: 0x0400BBA4 RID: 48036
					public static LocString DESCRIPTION = "A massive volume of " + UI.FormatAsLink("Hydrogen", "HYDROGEN") + " formed around a small solid center.";
				}

				// Token: 0x02002E66 RID: 11878
				public class ICEGIANT
				{
					// Token: 0x0400BBA5 RID: 48037
					public static LocString NAME = "Ice Giant";

					// Token: 0x0400BBA6 RID: 48038
					public static LocString DESCRIPTION = "A massive volume of frozen material, primarily composed of " + UI.FormatAsLink("Ice", "ICE") + ".";
				}

				// Token: 0x02002E67 RID: 11879
				public class HYDROGENGIANT
				{
					// Token: 0x0400BBA7 RID: 48039
					public static LocString NAME = "Helium Giant";

					// Token: 0x0400BBA8 RID: 48040
					public static LocString DESCRIPTION = "A massive volume of " + UI.FormatAsLink("Helium", "HELIUM") + " formed around a small solid center.";
				}
			}
		}

		// Token: 0x02001C7D RID: 7293
		public class SPACEARTIFACTS
		{
			// Token: 0x020023EF RID: 9199
			public class ARTIFACTTIERS
			{
				// Token: 0x04009C9D RID: 40093
				public static LocString TIER_NONE = "Nothing";

				// Token: 0x04009C9E RID: 40094
				public static LocString TIER0 = "Rarity 0";

				// Token: 0x04009C9F RID: 40095
				public static LocString TIER1 = "Rarity 1";

				// Token: 0x04009CA0 RID: 40096
				public static LocString TIER2 = "Rarity 2";

				// Token: 0x04009CA1 RID: 40097
				public static LocString TIER3 = "Rarity 3";

				// Token: 0x04009CA2 RID: 40098
				public static LocString TIER4 = "Rarity 4";

				// Token: 0x04009CA3 RID: 40099
				public static LocString TIER5 = "Rarity 5";
			}

			// Token: 0x020023F0 RID: 9200
			public class PACUPERCOLATOR
			{
				// Token: 0x04009CA4 RID: 40100
				public static LocString NAME = "Percolator";

				// Token: 0x04009CA5 RID: 40101
				public static LocString DESCRIPTION = "Don't drink from it! There was a pacu... IN the percolator!";

				// Token: 0x04009CA6 RID: 40102
				public static LocString ARTIFACT = "A coffee percolator with the remnants of a blend of coffee that was a personal favorite of Dr. Hassan Aydem.\n\nHe would specifically reserve the consumption of this particular blend for when he was reviewing research papers on Sunday afternoons.";
			}

			// Token: 0x020023F1 RID: 9201
			public class ROBOTARM
			{
				// Token: 0x04009CA7 RID: 40103
				public static LocString NAME = "Robot Arm";

				// Token: 0x04009CA8 RID: 40104
				public static LocString DESCRIPTION = "It's not functional. Just cool.";

				// Token: 0x04009CA9 RID: 40105
				public static LocString ARTIFACT = "A commercially available robot arm that has had a significant amount of modifications made to it.\n\nThe initials B.A. appear on one of the fingers.";
			}

			// Token: 0x020023F2 RID: 9202
			public class HATCHFOSSIL
			{
				// Token: 0x04009CAA RID: 40106
				public static LocString NAME = "Pristine Fossil";

				// Token: 0x04009CAB RID: 40107
				public static LocString DESCRIPTION = "The preserved bones of an early species of Hatch.";

				// Token: 0x04009CAC RID: 40108
				public static LocString ARTIFACT = "The preservation of this skeleton occurred artificially using a technique called the \"The Ali Method\".\n\nIt should be noted that this fossilization technique was pioneered by one Dr. Ashkan Seyed Ali, an employee of Gravitas.";
			}

			// Token: 0x020023F3 RID: 9203
			public class MODERNART
			{
				// Token: 0x04009CAD RID: 40109
				public static LocString NAME = "Modern Art";

				// Token: 0x04009CAE RID: 40110
				public static LocString DESCRIPTION = "I don't get it.";

				// Token: 0x04009CAF RID: 40111
				public static LocString ARTIFACT = "A sculpture of the Neoplastism movement of Modern Art.\n\nGravitas records show that this piece was once used in a presentation called 'Form and Function in Corporate Aesthetic'.";
			}

			// Token: 0x020023F4 RID: 9204
			public class EGGROCK
			{
				// Token: 0x04009CB0 RID: 40112
				public static LocString NAME = "Egg-Shaped Rock";

				// Token: 0x04009CB1 RID: 40113
				public static LocString DESCRIPTION = "It's unclear whether this is its naturally occurring shape, or if its appearance as been sculpted.";

				// Token: 0x04009CB2 RID: 40114
				public static LocString ARTIFACT = "The words \"Happy Farters Day Dad. Love Macy\" appear on the bottom of this rock, written in a childlish scrawl.";
			}

			// Token: 0x020023F5 RID: 9205
			public class RAINBOWEGGROCK
			{
				// Token: 0x04009CB3 RID: 40115
				public static LocString NAME = "Egg-Shaped Rock";

				// Token: 0x04009CB4 RID: 40116
				public static LocString DESCRIPTION = "It's unclear whether this is its naturally occurring shape, or if its appearance as been sculpted.\n\nThis one is rainbow colored.";

				// Token: 0x04009CB5 RID: 40117
				public static LocString ARTIFACT = "The words \"Happy Father's Day, Dad. Love you!\" appear on the bottom of this rock, written in very neat handwriting. The words are surrounded by four hearts drawn in what appears to be a pink gel pen.";
			}

			// Token: 0x020023F6 RID: 9206
			public class OKAYXRAY
			{
				// Token: 0x04009CB6 RID: 40118
				public static LocString NAME = "Old X-Ray";

				// Token: 0x04009CB7 RID: 40119
				public static LocString DESCRIPTION = "Ew, weird. It has five fingers!";

				// Token: 0x04009CB8 RID: 40120
				public static LocString ARTIFACT = "The description on this X-ray indicates that it was taken in the Gravitas Medical Facility.\n\nMost likely this X-ray was performed while investigating an injury that occurred within the facility.";
			}

			// Token: 0x020023F7 RID: 9207
			public class SHIELDGENERATOR
			{
				// Token: 0x04009CB9 RID: 40121
				public static LocString NAME = "Shield Generator";

				// Token: 0x04009CBA RID: 40122
				public static LocString DESCRIPTION = "A mechanical prototype capable of producing a small section of shielding.";

				// Token: 0x04009CBB RID: 40123
				public static LocString ARTIFACT = "The energy field produced by this shield generator completely ignores those light behaviors which are wave-like and focuses instead on its particle behaviors.\n\nThis seemingly paradoxical state is possible when light is slowed down to the point at which it stops entirely.";
			}

			// Token: 0x020023F8 RID: 9208
			public class TEAPOT
			{
				// Token: 0x04009CBC RID: 40124
				public static LocString NAME = "Encrusted Teapot";

				// Token: 0x04009CBD RID: 40125
				public static LocString DESCRIPTION = "A teapot from the depths of space, coated in a thick layer of Neutronium.";

				// Token: 0x04009CBE RID: 40126
				public static LocString ARTIFACT = "The amount of Neutronium present in this teapot suggests that it has crossed the threshold of the spacetime continuum on countless occasions, floating through many multiple universes over a plethora of times and spaces.\n\nThough there are, theoretically, an infinite amount of outcomes to any one event over many multi-verses, the homogeneity of the still relatively young multiverse suggests that this is then not the only teapot which has crossed into multiple universes. Despite the infinite possible outcomes of infinite multiverses it appears one high probability constant is that there is, or once was, a teapot floating somewhere in space within every universe.";
			}

			// Token: 0x020023F9 RID: 9209
			public class DNAMODEL
			{
				// Token: 0x04009CBF RID: 40127
				public static LocString NAME = "Double Helix Model";

				// Token: 0x04009CC0 RID: 40128
				public static LocString DESCRIPTION = "An educational model of genetic information.";

				// Token: 0x04009CC1 RID: 40129
				public static LocString ARTIFACT = "A physical representation of the building blocks of life.\n\nThis one contains trace amounts of a Genetic Ooze prototype that was once used by Gravitas.";
			}

			// Token: 0x020023FA RID: 9210
			public class SANDSTONE
			{
				// Token: 0x04009CC2 RID: 40130
				public static LocString NAME = "Sandstone";

				// Token: 0x04009CC3 RID: 40131
				public static LocString DESCRIPTION = "A beautiful rock composed of multiple layers of sediment.";

				// Token: 0x04009CC4 RID: 40132
				public static LocString ARTIFACT = "This sample of sandstone appears to have been processed by the Gravitas Mining Gun that was made available to the general public.\n\nNote: The Gravitas public Mining Gun model is different than ones used by Duplicants in its larger size, and extra precautionary features added in order to be compliant with national safety standards.";
			}

			// Token: 0x020023FB RID: 9211
			public class MAGMALAMP
			{
				// Token: 0x04009CC5 RID: 40133
				public static LocString NAME = "Magma Lamp";

				// Token: 0x04009CC6 RID: 40134
				public static LocString DESCRIPTION = "The sequel to \"Lava Lamp\".";

				// Token: 0x04009CC7 RID: 40135
				public static LocString ARTIFACT = "Molten lava and obsidian combined in a way that allows the lava to maintain just enough heat to remain in liquid form.\n\nPlans of this lamp found in the Gravitas archives have been attributed to one Robin Nisbet, PhD.";
			}

			// Token: 0x020023FC RID: 9212
			public class OBELISK
			{
				// Token: 0x04009CC8 RID: 40136
				public static LocString NAME = "Small Obelisk";

				// Token: 0x04009CC9 RID: 40137
				public static LocString DESCRIPTION = "A rectangular stone piece.\n\nIts function is unclear.";

				// Token: 0x04009CCA RID: 40138
				public static LocString ARTIFACT = "On close inspection this rectangle is actually a stone box built with a covert, almost seamless, lid, housing a tiny key.\n\nIt is still unclear what the key unlocks.";
			}

			// Token: 0x020023FD RID: 9213
			public class RUBIKSCUBE
			{
				// Token: 0x04009CCB RID: 40139
				public static LocString NAME = "Rubik's Cube";

				// Token: 0x04009CCC RID: 40140
				public static LocString DESCRIPTION = "This mystery of the universe has already been solved.";

				// Token: 0x04009CCD RID: 40141
				public static LocString ARTIFACT = "A well-used, competition-compliant version of the popular puzzle cube.\n\nIt's worth noting that Dr. Dylan 'Nails' Winslow was once a regional Rubik's Cube champion.";
			}

			// Token: 0x020023FE RID: 9214
			public class OFFICEMUG
			{
				// Token: 0x04009CCE RID: 40142
				public static LocString NAME = "Office Mug";

				// Token: 0x04009CCF RID: 40143
				public static LocString DESCRIPTION = "An intermediary place to store espresso before you move it to your mouth.";

				// Token: 0x04009CD0 RID: 40144
				public static LocString ARTIFACT = "An office mug with the Gravitas logo on it. Though their office mugs were all emblazoned with the same logo, Gravitas colored their mugs differently to distinguish between their various departments.\n\nThis one is from the AI department.";
			}

			// Token: 0x020023FF RID: 9215
			public class AMELIASWATCH
			{
				// Token: 0x04009CD1 RID: 40145
				public static LocString NAME = "Wrist Watch";

				// Token: 0x04009CD2 RID: 40146
				public static LocString DESCRIPTION = "It was discovered in a package labeled \"To be entrusted to Dr. Walker\".";

				// Token: 0x04009CD3 RID: 40147
				public static LocString ARTIFACT = "This watch once belonged to pioneering aviator Amelia Earhart and travelled to space via astronaut Dr. Shannon Walker.\n\nHow it came to be floating in space is a matter of speculation, but perhaps the adventurous spirit of its original stewards became infused within the fabric of this timepiece and compelled the universe to launch it into the great unknown.";
			}

			// Token: 0x02002400 RID: 9216
			public class MOONMOONMOON
			{
				// Token: 0x04009CD4 RID: 40148
				public static LocString NAME = "Moonmoonmoon";

				// Token: 0x04009CD5 RID: 40149
				public static LocString DESCRIPTION = "A moon's moon's moon. It's very small.";

				// Token: 0x04009CD6 RID: 40150
				public static LocString ARTIFACT = "In contrast to most moons, this object's glowing properties do not come from reflecting an external source of light, but rather from an internal glow of mysterious origin.\n\nThe glow of this object also grants an extraordinary amount of Decor bonus to nearby Duplicants, almost as if it was designed that way.";
			}

			// Token: 0x02002401 RID: 9217
			public class BIOLUMINESCENTROCK
			{
				// Token: 0x04009CD7 RID: 40151
				public static LocString NAME = "Bioluminescent Rock";

				// Token: 0x04009CD8 RID: 40152
				public static LocString DESCRIPTION = "A thriving colony of tiny, microscopic organisms is responsible for giving it its bluish glow.";

				// Token: 0x04009CD9 RID: 40153
				public static LocString ARTIFACT = "The microscopic organisms within this rock are of a unique variety whose genetic code shows many tell-tale signs of being genetically engineered within a lab.\n\nFurther analysis reveals they share 99.999% of their genetic code with Shine Bugs.";
			}

			// Token: 0x02002402 RID: 9218
			public class PLASMALAMP
			{
				// Token: 0x04009CDA RID: 40154
				public static LocString NAME = "Plasma Lamp";

				// Token: 0x04009CDB RID: 40155
				public static LocString DESCRIPTION = "No space colony is complete without one.";

				// Token: 0x04009CDC RID: 40156
				public static LocString ARTIFACT = "The bottom of this lamp contains the words 'Property of the Atmospheric Sciences Department'.\n\nIt's worth noting that the Gravitas Atmospheric Sciences Department once simulated an experiment testing the feasibility of survival in an environment filled with noble gasses, similar to the ones contained within this device.";
			}

			// Token: 0x02002403 RID: 9219
			public class MOLDAVITE
			{
				// Token: 0x04009CDD RID: 40157
				public static LocString NAME = "Moldavite";

				// Token: 0x04009CDE RID: 40158
				public static LocString DESCRIPTION = "A unique green stone formed from the impact of a meteorite.";

				// Token: 0x04009CDF RID: 40159
				public static LocString ARTIFACT = "This extremely rare, museum grade moldavite once sat on the desk of Dr. Ren Sato, but it was stolen by some unknown person.\n\nDr. Sato suspected the perpetrator was none other than Director Stern, but was never able to confirm this theory.";
			}

			// Token: 0x02002404 RID: 9220
			public class BRICKPHONE
			{
				// Token: 0x04009CE0 RID: 40160
				public static LocString NAME = "Strange Brick";

				// Token: 0x04009CE1 RID: 40161
				public static LocString DESCRIPTION = "It still works.";

				// Token: 0x04009CE2 RID: 40162
				public static LocString ARTIFACT = "This cordless phone once held a direct line to an unknown location in which strange distant voices can be heard but not understood, nor interacted with.\n\nThough Gravitas spent a lot of money and years of study dedicated to discovering its secret, the mystery was never solved.";
			}

			// Token: 0x02002405 RID: 9221
			public class SOLARSYSTEM
			{
				// Token: 0x04009CE3 RID: 40163
				public static LocString NAME = "Self-Contained System";

				// Token: 0x04009CE4 RID: 40164
				public static LocString DESCRIPTION = "A marvel of the cosmos, inside this display is an entirely self-contained solar system.";

				// Token: 0x04009CE5 RID: 40165
				public static LocString ARTIFACT = "This marvel of a device was built using parts from an old Tornado-in-a-Box science fair project.\n\nVery faint, faded letters are still visible on the display bottom that read 'Camille P. Grade 5'.";
			}

			// Token: 0x02002406 RID: 9222
			public class SINK
			{
				// Token: 0x04009CE6 RID: 40166
				public static LocString NAME = "Sink";

				// Token: 0x04009CE7 RID: 40167
				public static LocString DESCRIPTION = "No collection is complete without it.";

				// Token: 0x04009CE8 RID: 40168
				public static LocString ARTIFACT = "A small trace of encrusted soap on this sink strongly suggests it was installed in a personal bathroom, rather than a public one which would have used a soap dispenser.\n\nThe soap sliver is light blue and contains a manufactured blueberry fragrance.";
			}

			// Token: 0x02002407 RID: 9223
			public class ROCKTORNADO
			{
				// Token: 0x04009CE9 RID: 40169
				public static LocString NAME = "Tornado Rock";

				// Token: 0x04009CEA RID: 40170
				public static LocString DESCRIPTION = "It's unclear how it formed, although I'm glad it did.";

				// Token: 0x04009CEB RID: 40171
				public static LocString ARTIFACT = "Speculations about the origin of this rock include a paper written by one Harold P. Moreson, Ph.D. in which he theorized it could be a rare form of hollow geode which failed to form any crystals inside.\n\nThis paper appears in the Gravitas archives, and in all probability, was one of the factors in the hiring of Moreson into the Geology department of the company.";
			}

			// Token: 0x02002408 RID: 9224
			public class BLENDER
			{
				// Token: 0x04009CEC RID: 40172
				public static LocString NAME = "Blender";

				// Token: 0x04009CED RID: 40173
				public static LocString DESCRIPTION = "Equipment used to conduct experiments answering the age-old question, \"Could that blend\"?";

				// Token: 0x04009CEE RID: 40174
				public static LocString ARTIFACT = "Trace amounts of edible foodstuffs present in this blender indicate that it was probably used to emulsify the ingredients of a mush bar.\n\nIt is also very likely that it was employed at least once in the production of a peanut butter and banana smoothie.";
			}

			// Token: 0x02002409 RID: 9225
			public class SAXOPHONE
			{
				// Token: 0x04009CEF RID: 40175
				public static LocString NAME = "Mangled Saxophone";

				// Token: 0x04009CF0 RID: 40176
				public static LocString DESCRIPTION = "The name \"Pesquet\" is barely legible on the inside.";

				// Token: 0x04009CF1 RID: 40177
				public static LocString ARTIFACT = "Though it is often remarked that \"in space, no one can hear you scream\", Thomas Pesquet proved the same cannot be said for the smooth jazzy sounds of a saxophone.\n\nAlthough this instrument once belonged to the eminent French Astronaut its current bumped and bent shape suggests it has seen many adventures beyond that of just being used to perform an out-of-this-world saxophone solo.";
			}

			// Token: 0x0200240A RID: 9226
			public class STETHOSCOPE
			{
				// Token: 0x04009CF2 RID: 40178
				public static LocString NAME = "Stethoscope";

				// Token: 0x04009CF3 RID: 40179
				public static LocString DESCRIPTION = "Listens to Duplicant heartbeats, or gurgly tummies.";

				// Token: 0x04009CF4 RID: 40180
				public static LocString ARTIFACT = "The size and shape of this stethescope suggests it was not intended to be used by neither a human-sized nor a Duplicant-sized person but something half-way in between the two beings.";
			}

			// Token: 0x0200240B RID: 9227
			public class VHS
			{
				// Token: 0x04009CF5 RID: 40181
				public static LocString NAME = "Archaic Tech";

				// Token: 0x04009CF6 RID: 40182
				public static LocString DESCRIPTION = "Be kind when you handle it. It's very fragile.";

				// Token: 0x04009CF7 RID: 40183
				public static LocString ARTIFACT = "The label on this VHS tape reads \"Jackie and Olivia's House Warming Party\".\n\nUnfortunately, a device with which to play this recording no longer exists in this universe.";
			}

			// Token: 0x0200240C RID: 9228
			public class REACTORMODEL
			{
				// Token: 0x04009CF8 RID: 40184
				public static LocString NAME = "Model Nuclear Power Plant";

				// Token: 0x04009CF9 RID: 40185
				public static LocString DESCRIPTION = "It's pronounced nu-clear.";

				// Token: 0x04009CFA RID: 40186
				public static LocString ARTIFACT = "Though this Nuclear Power Plant was never built, this model exists as an artifact to a time early in the life of Gravitas when it was researching all alternatives to solving the global energy problem.\n\nUltimately, the idea of building a Nuclear Power Plant was abandoned in favor of the \"much safer\" alternative of developing the Temporal Bow.";
			}

			// Token: 0x0200240D RID: 9229
			public class MOODRING
			{
				// Token: 0x04009CFB RID: 40187
				public static LocString NAME = "Radiation Mood Ring";

				// Token: 0x04009CFC RID: 40188
				public static LocString DESCRIPTION = "How radioactive are you feeling?";

				// Token: 0x04009CFD RID: 40189
				public static LocString ARTIFACT = "A wholly unique ring not found anywhere outside of the Gravitas Laboratory.\n\nThough it can't be determined for sure who worked on this extraordinary curiousity it's worth noting that, for his Ph.D. thesis, Dr. Travaldo Farrington wrote a paper entitled \"Novelty Uses for Radiochromatic Dyes\".";
			}

			// Token: 0x0200240E RID: 9230
			public class ORACLE
			{
				// Token: 0x04009CFE RID: 40190
				public static LocString NAME = "Useless Machine";

				// Token: 0x04009CFF RID: 40191
				public static LocString DESCRIPTION = "What does it do?";

				// Token: 0x04009D00 RID: 40192
				public static LocString ARTIFACT = "All of the parts for this contraption are recycled from projects abandoned by the Robotics department.\n\nThe design is very close to one published in an amateur DIY magazine that once sat in the lobby of the 'Employees Only' area of Gravitas' facilities.";
			}

			// Token: 0x0200240F RID: 9231
			public class GRUBSTATUE
			{
				// Token: 0x04009D01 RID: 40193
				public static LocString NAME = "Grubgrub Statue";

				// Token: 0x04009D02 RID: 40194
				public static LocString DESCRIPTION = "A moving tribute to a tiny plant hugger.";

				// Token: 0x04009D03 RID: 40195
				public static LocString ARTIFACT = "It's very likely this statue was placed in a hidden, secluded place in the Gravitas laboratory since the creation of Grubgrubs was a closely held secret that the general public was not privy to.\n\nThis is a shame since the artistic quality of this statue is really quite accomplished.";
			}

			// Token: 0x02002410 RID: 9232
			public class HONEYJAR
			{
				// Token: 0x04009D04 RID: 40196
				public static LocString NAME = "Honey Jar";

				// Token: 0x04009D05 RID: 40197
				public static LocString DESCRIPTION = "Sweet golden liquid with just a touch of uranium.";

				// Token: 0x04009D06 RID: 40198
				public static LocString ARTIFACT = "Records from the Genetics and Biology Lab of the Gravitas facility show that several early iterations of a radioactive Bee would continue to produce honey and that this honey was once accidentally stored in the employee kitchen which resulted in several incidents of minor radiation poisoning when it was erroneously labled as a sweetener for tea.\n\nEmployees who used this product reported that it was the \"sweetest honey they'd ever tasted\" and expressed no regret at the mix-up.";
			}
		}

		// Token: 0x02001C7E RID: 7294
		public class KEEPSAKES
		{
			// Token: 0x02002411 RID: 9233
			public class CRITTER_MANIPULATOR
			{
				// Token: 0x04009D07 RID: 40199
				public static LocString NAME = "Ceramic Morb";

				// Token: 0x04009D08 RID: 40200
				public static LocString DESCRIPTION = "A pottery project produced in an HR-mandated art therapy class.\n\nIt's glazed with a substance that once landed a curious lab technician in the ER.";
			}

			// Token: 0x02002412 RID: 9234
			public class MEGA_BRAIN
			{
				// Token: 0x04009D09 RID: 40201
				public static LocString NAME = "Model Plane";

				// Token: 0x04009D0A RID: 40202
				public static LocString DESCRIPTION = "A treasured souvenir that was once a common accompaniment to children's meals during commercial flights. There's a hole in the bottom from when Dr. Holland had it mounted on a stand.";
			}

			// Token: 0x02002413 RID: 9235
			public class LONELY_MINION
			{
				// Token: 0x04009D0B RID: 40203
				public static LocString NAME = "Rusty Toolbox";

				// Token: 0x04009D0C RID: 40204
				public static LocString DESCRIPTION = "On the inside of the lid, someone used a screwdriver to carve a drawing of a group of smiling Duplicants gathered around a massive crater.";
			}
		}

		// Token: 0x02001C7F RID: 7295
		public class SANDBOXTOOLS
		{
			// Token: 0x02002414 RID: 9236
			public class SETTINGS
			{
				// Token: 0x02002E68 RID: 11880
				public class INSTANT_BUILD
				{
					// Token: 0x0400BBA9 RID: 48041
					public static LocString NAME = "Instant build mode ON";

					// Token: 0x0400BBAA RID: 48042
					public static LocString TOOLTIP = "Toggle between placing construction plans and fully built buildings";
				}

				// Token: 0x02002E69 RID: 11881
				public class BRUSH_SIZE
				{
					// Token: 0x0400BBAB RID: 48043
					public static LocString NAME = "Size";

					// Token: 0x0400BBAC RID: 48044
					public static LocString TOOLTIP = "Adjust brush size";
				}

				// Token: 0x02002E6A RID: 11882
				public class BRUSH_NOISE_SCALE
				{
					// Token: 0x0400BBAD RID: 48045
					public static LocString NAME = "Noise A";

					// Token: 0x0400BBAE RID: 48046
					public static LocString TOOLTIP = "Adjust brush noisiness A";
				}

				// Token: 0x02002E6B RID: 11883
				public class BRUSH_NOISE_DENSITY
				{
					// Token: 0x0400BBAF RID: 48047
					public static LocString NAME = "Noise B";

					// Token: 0x0400BBB0 RID: 48048
					public static LocString TOOLTIP = "Adjust brush noisiness B";
				}

				// Token: 0x02002E6C RID: 11884
				public class TEMPERATURE
				{
					// Token: 0x0400BBB1 RID: 48049
					public static LocString NAME = "Temperature";

					// Token: 0x0400BBB2 RID: 48050
					public static LocString TOOLTIP = "Adjust absolute temperature";
				}

				// Token: 0x02002E6D RID: 11885
				public class TEMPERATURE_ADDITIVE
				{
					// Token: 0x0400BBB3 RID: 48051
					public static LocString NAME = "Temperature";

					// Token: 0x0400BBB4 RID: 48052
					public static LocString TOOLTIP = "Adjust additive temperature";
				}

				// Token: 0x02002E6E RID: 11886
				public class RADIATION
				{
					// Token: 0x0400BBB5 RID: 48053
					public static LocString NAME = "Absolute radiation";

					// Token: 0x0400BBB6 RID: 48054
					public static LocString TOOLTIP = "Adjust absolute radiation";
				}

				// Token: 0x02002E6F RID: 11887
				public class RADIATION_ADDITIVE
				{
					// Token: 0x0400BBB7 RID: 48055
					public static LocString NAME = "Additive radiation";

					// Token: 0x0400BBB8 RID: 48056
					public static LocString TOOLTIP = "Adjust additive radiation";
				}

				// Token: 0x02002E70 RID: 11888
				public class STRESS_ADDITIVE
				{
					// Token: 0x0400BBB9 RID: 48057
					public static LocString NAME = "Reduce Stress";

					// Token: 0x0400BBBA RID: 48058
					public static LocString TOOLTIP = "Adjust stress reduction";
				}

				// Token: 0x02002E71 RID: 11889
				public class MORALE
				{
					// Token: 0x0400BBBB RID: 48059
					public static LocString NAME = "Adjust Morale";

					// Token: 0x0400BBBC RID: 48060
					public static LocString TOOLTIP = "Bonus Morale adjustment";
				}

				// Token: 0x02002E72 RID: 11890
				public class MASS
				{
					// Token: 0x0400BBBD RID: 48061
					public static LocString NAME = "Mass";

					// Token: 0x0400BBBE RID: 48062
					public static LocString TOOLTIP = "Adjust mass";
				}

				// Token: 0x02002E73 RID: 11891
				public class DISEASE
				{
					// Token: 0x0400BBBF RID: 48063
					public static LocString NAME = "Germ";

					// Token: 0x0400BBC0 RID: 48064
					public static LocString TOOLTIP = "Adjust type of germ";
				}

				// Token: 0x02002E74 RID: 11892
				public class DISEASE_COUNT
				{
					// Token: 0x0400BBC1 RID: 48065
					public static LocString NAME = "Germs";

					// Token: 0x0400BBC2 RID: 48066
					public static LocString TOOLTIP = "Adjust germ count";
				}

				// Token: 0x02002E75 RID: 11893
				public class BRUSH
				{
					// Token: 0x0400BBC3 RID: 48067
					public static LocString NAME = "Brush";

					// Token: 0x0400BBC4 RID: 48068
					public static LocString TOOLTIP = "Paint elements into the world simulation {Hotkey}";
				}

				// Token: 0x02002E76 RID: 11894
				public class ELEMENT
				{
					// Token: 0x0400BBC5 RID: 48069
					public static LocString NAME = "Element";

					// Token: 0x0400BBC6 RID: 48070
					public static LocString TOOLTIP = "Adjust type of element";
				}

				// Token: 0x02002E77 RID: 11895
				public class SPRINKLE
				{
					// Token: 0x0400BBC7 RID: 48071
					public static LocString NAME = "Sprinkle";

					// Token: 0x0400BBC8 RID: 48072
					public static LocString TOOLTIP = "Paint elements into the simulation using noise {Hotkey}";
				}

				// Token: 0x02002E78 RID: 11896
				public class FLOOD
				{
					// Token: 0x0400BBC9 RID: 48073
					public static LocString NAME = "Fill";

					// Token: 0x0400BBCA RID: 48074
					public static LocString TOOLTIP = "Fill a section of the simulation with the chosen element {Hotkey}";
				}

				// Token: 0x02002E79 RID: 11897
				public class SAMPLE
				{
					// Token: 0x0400BBCB RID: 48075
					public static LocString NAME = "Sample";

					// Token: 0x0400BBCC RID: 48076
					public static LocString TOOLTIP = "Copy the settings from a cell to use with brush tools {Hotkey}";
				}

				// Token: 0x02002E7A RID: 11898
				public class HEATGUN
				{
					// Token: 0x0400BBCD RID: 48077
					public static LocString NAME = "Heat Gun";

					// Token: 0x0400BBCE RID: 48078
					public static LocString TOOLTIP = "Inject thermal energy into the simulation {Hotkey}";
				}

				// Token: 0x02002E7B RID: 11899
				public class RADSTOOL
				{
					// Token: 0x0400BBCF RID: 48079
					public static LocString NAME = "Radiation Tool";

					// Token: 0x0400BBD0 RID: 48080
					public static LocString TOOLTIP = "Inject or remove radiation from the simulation {Hotkey}";
				}

				// Token: 0x02002E7C RID: 11900
				public class SPAWNER
				{
					// Token: 0x0400BBD1 RID: 48081
					public static LocString NAME = "Spawner";

					// Token: 0x0400BBD2 RID: 48082
					public static LocString TOOLTIP = "Spawn critters, food, equipment, and other entities {Hotkey}";
				}

				// Token: 0x02002E7D RID: 11901
				public class STRESS
				{
					// Token: 0x0400BBD3 RID: 48083
					public static LocString NAME = "Stress";

					// Token: 0x0400BBD4 RID: 48084
					public static LocString TOOLTIP = "Manage Duplicants' stress levels {Hotkey}";
				}

				// Token: 0x02002E7E RID: 11902
				public class CLEAR_FLOOR
				{
					// Token: 0x0400BBD5 RID: 48085
					public static LocString NAME = "Clear Debris";

					// Token: 0x0400BBD6 RID: 48086
					public static LocString TOOLTIP = "Delete loose items cluttering the floor {Hotkey}";
				}

				// Token: 0x02002E7F RID: 11903
				public class DESTROY
				{
					// Token: 0x0400BBD7 RID: 48087
					public static LocString NAME = "Destroy";

					// Token: 0x0400BBD8 RID: 48088
					public static LocString TOOLTIP = "Delete everything in the selected cell(s) {Hotkey}";
				}

				// Token: 0x02002E80 RID: 11904
				public class SPAWN_ENTITY
				{
					// Token: 0x0400BBD9 RID: 48089
					public static LocString NAME = "Spawn";
				}

				// Token: 0x02002E81 RID: 11905
				public class FOW
				{
					// Token: 0x0400BBDA RID: 48090
					public static LocString NAME = "Reveal";

					// Token: 0x0400BBDB RID: 48091
					public static LocString TOOLTIP = "Dispel the Fog of War shrouding the map {Hotkey}";
				}

				// Token: 0x02002E82 RID: 11906
				public class CRITTER
				{
					// Token: 0x0400BBDC RID: 48092
					public static LocString NAME = "Critter Removal";

					// Token: 0x0400BBDD RID: 48093
					public static LocString TOOLTIP = "Remove Critters! {Hotkey}";
				}
			}

			// Token: 0x02002415 RID: 9237
			public class FILTERS
			{
				// Token: 0x04009D0D RID: 40205
				public static LocString BACK = "Back";

				// Token: 0x04009D0E RID: 40206
				public static LocString COMMON = "Common Substances";

				// Token: 0x04009D0F RID: 40207
				public static LocString SOLID = "Solids";

				// Token: 0x04009D10 RID: 40208
				public static LocString LIQUID = "Liquids";

				// Token: 0x04009D11 RID: 40209
				public static LocString GAS = "Gases";

				// Token: 0x02002E83 RID: 11907
				public class ENTITIES
				{
					// Token: 0x0400BBDE RID: 48094
					public static LocString SPECIAL = "Special";

					// Token: 0x0400BBDF RID: 48095
					public static LocString GRAVITAS = "Gravitas";

					// Token: 0x0400BBE0 RID: 48096
					public static LocString PLANTS = "Plants";

					// Token: 0x0400BBE1 RID: 48097
					public static LocString SEEDS = "Seeds";

					// Token: 0x0400BBE2 RID: 48098
					public static LocString CREATURE = "Critters";

					// Token: 0x0400BBE3 RID: 48099
					public static LocString CREATURE_EGG = "Eggs";

					// Token: 0x0400BBE4 RID: 48100
					public static LocString FOOD = "Foods";

					// Token: 0x0400BBE5 RID: 48101
					public static LocString EQUIPMENT = "Equipment";

					// Token: 0x0400BBE6 RID: 48102
					public static LocString GEYSERS = "Geysers";

					// Token: 0x0400BBE7 RID: 48103
					public static LocString EXPERIMENTS = "Experimental";

					// Token: 0x0400BBE8 RID: 48104
					public static LocString INDUSTRIAL_PRODUCTS = "Industrial";

					// Token: 0x0400BBE9 RID: 48105
					public static LocString COMETS = "Comets";

					// Token: 0x0400BBEA RID: 48106
					public static LocString ARTIFACTS = "Artifacts";

					// Token: 0x0400BBEB RID: 48107
					public static LocString STORYTRAITS = "Story Traits";
				}
			}

			// Token: 0x02002416 RID: 9238
			public class CLEARFLOOR
			{
				// Token: 0x04009D12 RID: 40210
				public static LocString DELETED = "Deleted";
			}
		}

		// Token: 0x02001C80 RID: 7296
		public class RETIRED_COLONY_INFO_SCREEN
		{
			// Token: 0x04008004 RID: 32772
			public static LocString SECONDS = "Seconds";

			// Token: 0x04008005 RID: 32773
			public static LocString CYCLES = "Cycles";

			// Token: 0x04008006 RID: 32774
			public static LocString CYCLE_COUNT = "Cycle Count: {0}";

			// Token: 0x04008007 RID: 32775
			public static LocString DUPLICANT_AGE = "Age: {0} cycles";

			// Token: 0x04008008 RID: 32776
			public static LocString SKILL_LEVEL = "Skill Level: {0}";

			// Token: 0x04008009 RID: 32777
			public static LocString BUILDING_COUNT = "Count: {0}";

			// Token: 0x0400800A RID: 32778
			public static LocString PREVIEW_UNAVAILABLE = "Preview\nUnavailable";

			// Token: 0x0400800B RID: 32779
			public static LocString TIMELAPSE_UNAVAILABLE = "Timelapse\nUnavailable";

			// Token: 0x0400800C RID: 32780
			public static LocString SEARCH = "SEARCH...";

			// Token: 0x02002417 RID: 9239
			public class BUTTONS
			{
				// Token: 0x04009D13 RID: 40211
				public static LocString RETURN_TO_GAME = "RETURN TO GAME";

				// Token: 0x04009D14 RID: 40212
				public static LocString VIEW_OTHER_COLONIES = "BACK";

				// Token: 0x04009D15 RID: 40213
				public static LocString QUIT_TO_MENU = "QUIT TO MAIN MENU";

				// Token: 0x04009D16 RID: 40214
				public static LocString CLOSE = "CLOSE";
			}

			// Token: 0x02002418 RID: 9240
			public class TITLES
			{
				// Token: 0x04009D17 RID: 40215
				public static LocString EXPLORER_HEADER = "COLONIES";

				// Token: 0x04009D18 RID: 40216
				public static LocString RETIRED_COLONIES = "Colony Summaries";

				// Token: 0x04009D19 RID: 40217
				public static LocString COLONY_STATISTICS = "Colony Statistics";

				// Token: 0x04009D1A RID: 40218
				public static LocString DUPLICANTS = "Duplicants";

				// Token: 0x04009D1B RID: 40219
				public static LocString BUILDINGS = "Buildings";

				// Token: 0x04009D1C RID: 40220
				public static LocString CHEEVOS = "Colony Achievements";

				// Token: 0x04009D1D RID: 40221
				public static LocString ACHIEVEMENT_HEADER = "ACHIEVEMENTS";

				// Token: 0x04009D1E RID: 40222
				public static LocString TIMELAPSE = "Timelapse";
			}

			// Token: 0x02002419 RID: 9241
			public class STATS
			{
				// Token: 0x04009D1F RID: 40223
				public static LocString OXYGEN_CREATED = "Total Oxygen Produced";

				// Token: 0x04009D20 RID: 40224
				public static LocString OXYGEN_CONSUMED = "Total Oxygen Consumed";

				// Token: 0x04009D21 RID: 40225
				public static LocString POWER_CREATED = "Average Power Produced";

				// Token: 0x04009D22 RID: 40226
				public static LocString POWER_WASTED = "Average Power Wasted";

				// Token: 0x04009D23 RID: 40227
				public static LocString TRAVEL_TIME = "Total Travel Time";

				// Token: 0x04009D24 RID: 40228
				public static LocString WORK_TIME = "Total Work Time";

				// Token: 0x04009D25 RID: 40229
				public static LocString AVERAGE_TRAVEL_TIME = "Average Travel Time";

				// Token: 0x04009D26 RID: 40230
				public static LocString AVERAGE_WORK_TIME = "Average Work Time";

				// Token: 0x04009D27 RID: 40231
				public static LocString CALORIES_CREATED = "Calorie Generation";

				// Token: 0x04009D28 RID: 40232
				public static LocString CALORIES_CONSUMED = "Calorie Consumption";

				// Token: 0x04009D29 RID: 40233
				public static LocString LIVE_DUPLICANTS = "Duplicants";

				// Token: 0x04009D2A RID: 40234
				public static LocString AVERAGE_STRESS_CREATED = "Average Stress Created";

				// Token: 0x04009D2B RID: 40235
				public static LocString AVERAGE_STRESS_REMOVED = "Average Stress Removed";

				// Token: 0x04009D2C RID: 40236
				public static LocString NUMBER_DOMESTICATED_CRITTERS = "Domesticated Critters";

				// Token: 0x04009D2D RID: 40237
				public static LocString NUMBER_WILD_CRITTERS = "Wild Critters";

				// Token: 0x04009D2E RID: 40238
				public static LocString AVERAGE_GERMS = "Average Germs";

				// Token: 0x04009D2F RID: 40239
				public static LocString ROCKET_MISSIONS = "Rocket Missions Underway";
			}
		}

		// Token: 0x02001C81 RID: 7297
		public class DROPDOWN
		{
			// Token: 0x0400800D RID: 32781
			public static LocString NONE = "Unassigned";
		}

		// Token: 0x02001C82 RID: 7298
		public class FRONTEND
		{
			// Token: 0x0400800E RID: 32782
			public static LocString GAME_VERSION = "Game Version: ";

			// Token: 0x0400800F RID: 32783
			public static LocString LOADING = "Loading...";

			// Token: 0x04008010 RID: 32784
			public static LocString DONE_BUTTON = "DONE";

			// Token: 0x0200241A RID: 9242
			public class DEMO_OVER_SCREEN
			{
				// Token: 0x04009D30 RID: 40240
				public static LocString TITLE = "Thanks for playing!";

				// Token: 0x04009D31 RID: 40241
				public static LocString BODY = "Thank you for playing the demo for Oxygen Not Included!\n\nThis game is still in development.\n\nGo to kleigames.com/o2 or ask one of us if you'd like more information.";

				// Token: 0x04009D32 RID: 40242
				public static LocString BUTTON_EXIT_TO_MENU = "EXIT TO MENU";
			}

			// Token: 0x0200241B RID: 9243
			public class CUSTOMGAMESETTINGSSCREEN
			{
				// Token: 0x02002E84 RID: 11908
				public class SETTINGS
				{
					// Token: 0x020030B5 RID: 12469
					public class SANDBOXMODE
					{
						// Token: 0x0400C1E7 RID: 49639
						public static LocString NAME = "Sandbox Mode";

						// Token: 0x0400C1E8 RID: 49640
						public static LocString TOOLTIP = "Manipulate and customize the simulation with tools that ignore regular game constraints";

						// Token: 0x0200312C RID: 12588
						public static class LEVELS
						{
							// Token: 0x02003141 RID: 12609
							public static class DISABLED
							{
								// Token: 0x0400C318 RID: 49944
								public static LocString NAME = "Disabled";

								// Token: 0x0400C319 RID: 49945
								public static LocString TOOLTIP = "Unchecked: Sandbox Mode is turned off (Default)";
							}

							// Token: 0x02003142 RID: 12610
							public static class ENABLED
							{
								// Token: 0x0400C31A RID: 49946
								public static LocString NAME = "Enabled";

								// Token: 0x0400C31B RID: 49947
								public static LocString TOOLTIP = "Checked: Sandbox Mode is turned on";
							}
						}
					}

					// Token: 0x020030B6 RID: 12470
					public class FASTWORKERSMODE
					{
						// Token: 0x0400C1E9 RID: 49641
						public static LocString NAME = "Fast Workers Mode";

						// Token: 0x0400C1EA RID: 49642
						public static LocString TOOLTIP = "Dupes will finish most work immediately and require little sleep";

						// Token: 0x0200312D RID: 12589
						public static class LEVELS
						{
							// Token: 0x02003143 RID: 12611
							public static class DISABLED
							{
								// Token: 0x0400C31C RID: 49948
								public static LocString NAME = "Disabled";

								// Token: 0x0400C31D RID: 49949
								public static LocString TOOLTIP = "Unchecked: Fast Workers Mode is turned off (Default)";
							}

							// Token: 0x02003144 RID: 12612
							public static class ENABLED
							{
								// Token: 0x0400C31E RID: 49950
								public static LocString NAME = "Enabled";

								// Token: 0x0400C31F RID: 49951
								public static LocString TOOLTIP = "Checked: Fast Workers Mode is turned on";
							}
						}
					}

					// Token: 0x020030B7 RID: 12471
					public class EXPANSION1ACTIVE
					{
						// Token: 0x0400C1EB RID: 49643
						public static LocString NAME = UI.DLC1.NAME_ITAL + " Content Enabled";

						// Token: 0x0400C1EC RID: 49644
						public static LocString TOOLTIP = "If checked, content from the " + UI.DLC1.NAME_ITAL + " Expansion will be available";

						// Token: 0x0200312E RID: 12590
						public static class LEVELS
						{
							// Token: 0x02003145 RID: 12613
							public static class DISABLED
							{
								// Token: 0x0400C320 RID: 49952
								public static LocString NAME = "Disabled";

								// Token: 0x0400C321 RID: 49953
								public static LocString TOOLTIP = "Unchecked: " + UI.DLC1.NAME_ITAL + " Content is turned off (Default)";
							}

							// Token: 0x02003146 RID: 12614
							public static class ENABLED
							{
								// Token: 0x0400C322 RID: 49954
								public static LocString NAME = "Enabled";

								// Token: 0x0400C323 RID: 49955
								public static LocString TOOLTIP = "Checked: " + UI.DLC1.NAME_ITAL + " Content is turned on";
							}
						}
					}

					// Token: 0x020030B8 RID: 12472
					public class SAVETOCLOUD
					{
						// Token: 0x0400C1ED RID: 49645
						public static LocString NAME = "Save To Cloud";

						// Token: 0x0400C1EE RID: 49646
						public static LocString TOOLTIP = "This colony will be created in the cloud saves folder, and synced by the game platform.";

						// Token: 0x0400C1EF RID: 49647
						public static LocString TOOLTIP_LOCAL = "This colony will be created in the local saves folder. It will not be a cloud save and will not be synced by the game platform.";

						// Token: 0x0400C1F0 RID: 49648
						public static LocString TOOLTIP_EXTRA = "This can be changed later with the colony management options in the load screen, from the main menu.";

						// Token: 0x0200312F RID: 12591
						public static class LEVELS
						{
							// Token: 0x02003147 RID: 12615
							public static class DISABLED
							{
								// Token: 0x0400C324 RID: 49956
								public static LocString NAME = "Disabled";

								// Token: 0x0400C325 RID: 49957
								public static LocString TOOLTIP = "Unchecked: This colony will be a local save";
							}

							// Token: 0x02003148 RID: 12616
							public static class ENABLED
							{
								// Token: 0x0400C326 RID: 49958
								public static LocString NAME = "Enabled";

								// Token: 0x0400C327 RID: 49959
								public static LocString TOOLTIP = "Checked: This colony will be a cloud save (Default)";
							}
						}
					}

					// Token: 0x020030B9 RID: 12473
					public class CAREPACKAGES
					{
						// Token: 0x0400C1F1 RID: 49649
						public static LocString NAME = "Care Packages";

						// Token: 0x0400C1F2 RID: 49650
						public static LocString TOOLTIP = "Affects what resources can be printed from the Printing Pod";

						// Token: 0x02003130 RID: 12592
						public static class LEVELS
						{
							// Token: 0x02003149 RID: 12617
							public static class NORMAL
							{
								// Token: 0x0400C328 RID: 49960
								public static LocString NAME = "All";

								// Token: 0x0400C329 RID: 49961
								public static LocString TOOLTIP = "Checked: The Printing Pod will offer both Duplicant blueprints and care packages (Default)";
							}

							// Token: 0x0200314A RID: 12618
							public static class DUPLICANTS_ONLY
							{
								// Token: 0x0400C32A RID: 49962
								public static LocString NAME = "Duplicants Only";

								// Token: 0x0400C32B RID: 49963
								public static LocString TOOLTIP = "Unchecked: The Printing Pod will only offer Duplicant blueprints";
							}
						}
					}

					// Token: 0x020030BA RID: 12474
					public class IMMUNESYSTEM
					{
						// Token: 0x0400C1F3 RID: 49651
						public static LocString NAME = "Disease";

						// Token: 0x0400C1F4 RID: 49652
						public static LocString TOOLTIP = "Affects Duplicants' chances of contracting a disease after germ exposure";

						// Token: 0x02003131 RID: 12593
						public static class LEVELS
						{
							// Token: 0x0200314B RID: 12619
							public static class COMPROMISED
							{
								// Token: 0x0400C32C RID: 49964
								public static LocString NAME = "Outbreak Prone";

								// Token: 0x0400C32D RID: 49965
								public static LocString TOOLTIP = "The whole colony will be ravaged by plague if a Duplicant so much as sneezes funny";

								// Token: 0x0400C32E RID: 49966
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Outbreak Prone (Highest Difficulty)";
							}

							// Token: 0x0200314C RID: 12620
							public static class WEAK
							{
								// Token: 0x0400C32F RID: 49967
								public static LocString NAME = "Germ Susceptible";

								// Token: 0x0400C330 RID: 49968
								public static LocString TOOLTIP = "These Duplicants have an increased chance of contracting diseases from germ exposure";

								// Token: 0x0400C331 RID: 49969
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Germ Susceptibility (Difficulty Up)";
							}

							// Token: 0x0200314D RID: 12621
							public static class DEFAULT
							{
								// Token: 0x0400C332 RID: 49970
								public static LocString NAME = "Default";

								// Token: 0x0400C333 RID: 49971
								public static LocString TOOLTIP = "Default disease chance";
							}

							// Token: 0x0200314E RID: 12622
							public static class STRONG
							{
								// Token: 0x0400C334 RID: 49972
								public static LocString NAME = "Germ Resistant";

								// Token: 0x0400C335 RID: 49973
								public static LocString TOOLTIP = "These Duplicants have a decreased chance of contracting diseases from germ exposure";

								// Token: 0x0400C336 RID: 49974
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Germ Resistance (Difficulty Down)";
							}

							// Token: 0x0200314F RID: 12623
							public static class INVINCIBLE
							{
								// Token: 0x0400C337 RID: 49975
								public static LocString NAME = "Total Immunity";

								// Token: 0x0400C338 RID: 49976
								public static LocString TOOLTIP = "Like diplomatic immunity, but without the diplomacy. These Duplicants will never get sick";

								// Token: 0x0400C339 RID: 49977
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Total Immunity (No Disease)";
							}
						}
					}

					// Token: 0x020030BB RID: 12475
					public class MORALE
					{
						// Token: 0x0400C1F5 RID: 49653
						public static LocString NAME = "Morale";

						// Token: 0x0400C1F6 RID: 49654
						public static LocString TOOLTIP = "Adjusts the minimum morale Duplicants must maintain to avoid gaining stress";

						// Token: 0x02003132 RID: 12594
						public static class LEVELS
						{
							// Token: 0x02003150 RID: 12624
							public static class VERYHARD
							{
								// Token: 0x0400C33A RID: 49978
								public static LocString NAME = "Draconian";

								// Token: 0x0400C33B RID: 49979
								public static LocString TOOLTIP = "The finest of the finest can barely keep up with these Duplicants' stringent demands";

								// Token: 0x0400C33C RID: 49980
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Draconian (Highest Difficulty)";
							}

							// Token: 0x02003151 RID: 12625
							public static class HARD
							{
								// Token: 0x0400C33D RID: 49981
								public static LocString NAME = "A Bit Persnickety";

								// Token: 0x0400C33E RID: 49982
								public static LocString TOOLTIP = "Duplicants require higher morale than usual to fend off stress";

								// Token: 0x0400C33F RID: 49983
								public static LocString ATTRIBUTE_MODIFIER_NAME = "A Bit Persnickety (Difficulty Up)";
							}

							// Token: 0x02003152 RID: 12626
							public static class DEFAULT
							{
								// Token: 0x0400C340 RID: 49984
								public static LocString NAME = "Default";

								// Token: 0x0400C341 RID: 49985
								public static LocString TOOLTIP = "Default morale needs";
							}

							// Token: 0x02003153 RID: 12627
							public static class EASY
							{
								// Token: 0x0400C342 RID: 49986
								public static LocString NAME = "Chill";

								// Token: 0x0400C343 RID: 49987
								public static LocString TOOLTIP = "Duplicants require lower morale than usual to fend off stress";

								// Token: 0x0400C344 RID: 49988
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Chill (Difficulty Down)";
							}

							// Token: 0x02003154 RID: 12628
							public static class DISABLED
							{
								// Token: 0x0400C345 RID: 49989
								public static LocString NAME = "Totally Blasé";

								// Token: 0x0400C346 RID: 49990
								public static LocString TOOLTIP = "These Duplicants have zero standards and will never gain stress, regardless of their morale";

								// Token: 0x0400C347 RID: 49991
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Totally Blasé (No Morale)";
							}
						}
					}

					// Token: 0x020030BC RID: 12476
					public class CALORIE_BURN
					{
						// Token: 0x0400C1F7 RID: 49655
						public static LocString NAME = "Hunger";

						// Token: 0x0400C1F8 RID: 49656
						public static LocString TOOLTIP = "Affects how quickly Duplicants burn calories and become hungry";

						// Token: 0x02003133 RID: 12595
						public static class LEVELS
						{
							// Token: 0x02003155 RID: 12629
							public static class VERYHARD
							{
								// Token: 0x0400C348 RID: 49992
								public static LocString NAME = "Ravenous";

								// Token: 0x0400C349 RID: 49993
								public static LocString TOOLTIP = "Your Duplicants are on a see-food diet... They see food and they eat it";

								// Token: 0x0400C34A RID: 49994
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Ravenous (Highest Difficulty)";
							}

							// Token: 0x02003156 RID: 12630
							public static class HARD
							{
								// Token: 0x0400C34B RID: 49995
								public static LocString NAME = "Rumbly Tummies";

								// Token: 0x0400C34C RID: 49996
								public static LocString TOOLTIP = "Duplicants burn calories quickly and require more feeding than usual";

								// Token: 0x0400C34D RID: 49997
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Rumbly Tummies (Difficulty Up)";
							}

							// Token: 0x02003157 RID: 12631
							public static class DEFAULT
							{
								// Token: 0x0400C34E RID: 49998
								public static LocString NAME = "Default";

								// Token: 0x0400C34F RID: 49999
								public static LocString TOOLTIP = "Default calorie burn rate";
							}

							// Token: 0x02003158 RID: 12632
							public static class EASY
							{
								// Token: 0x0400C350 RID: 50000
								public static LocString NAME = "Fasting";

								// Token: 0x0400C351 RID: 50001
								public static LocString TOOLTIP = "Duplicants burn calories slowly and get by with fewer meals";

								// Token: 0x0400C352 RID: 50002
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Fasting (Difficulty Down)";
							}

							// Token: 0x02003159 RID: 12633
							public static class DISABLED
							{
								// Token: 0x0400C353 RID: 50003
								public static LocString NAME = "Tummyless";

								// Token: 0x0400C354 RID: 50004
								public static LocString TOOLTIP = "These Duplicants were printed without tummies and need no food at all";

								// Token: 0x0400C355 RID: 50005
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Tummyless (No Hunger)";
							}
						}
					}

					// Token: 0x020030BD RID: 12477
					public class WORLD_CHOICE
					{
						// Token: 0x0400C1F9 RID: 49657
						public static LocString NAME = "World";

						// Token: 0x0400C1FA RID: 49658
						public static LocString TOOLTIP = "New worlds added by mods can be selected here";
					}

					// Token: 0x020030BE RID: 12478
					public class CLUSTER_CHOICE
					{
						// Token: 0x0400C1FB RID: 49659
						public static LocString NAME = "Asteroid Belt";

						// Token: 0x0400C1FC RID: 49660
						public static LocString TOOLTIP = "New asteroid belts added by mods can be selected here";
					}

					// Token: 0x020030BF RID: 12479
					public class STORY_TRAIT_COUNT
					{
						// Token: 0x0400C1FD RID: 49661
						public static LocString NAME = "Story Traits";

						// Token: 0x0400C1FE RID: 49662
						public static LocString TOOLTIP = "Determines the number of story traits spawned";

						// Token: 0x02003134 RID: 12596
						public static class LEVELS
						{
							// Token: 0x0200315A RID: 12634
							public static class NONE
							{
								// Token: 0x0400C356 RID: 50006
								public static LocString NAME = "Zilch";

								// Token: 0x0400C357 RID: 50007
								public static LocString TOOLTIP = "Zero story traits. Zip. Nada. None";
							}

							// Token: 0x0200315B RID: 12635
							public static class FEW
							{
								// Token: 0x0400C358 RID: 50008
								public static LocString NAME = "Stingy";

								// Token: 0x0400C359 RID: 50009
								public static LocString TOOLTIP = "Not zero, but not a lot";
							}

							// Token: 0x0200315C RID: 12636
							public static class LOTS
							{
								// Token: 0x0400C35A RID: 50010
								public static LocString NAME = "Oodles";

								// Token: 0x0400C35B RID: 50011
								public static LocString TOOLTIP = "Plenty of story traits to go around";
							}
						}
					}

					// Token: 0x020030C0 RID: 12480
					public class DURABILITY
					{
						// Token: 0x0400C1FF RID: 49663
						public static LocString NAME = "Durability";

						// Token: 0x0400C200 RID: 49664
						public static LocString TOOLTIP = "Affects how quickly equippable suits wear out";

						// Token: 0x02003135 RID: 12597
						public static class LEVELS
						{
							// Token: 0x0200315D RID: 12637
							public static class INDESTRUCTIBLE
							{
								// Token: 0x0400C35C RID: 50012
								public static LocString NAME = "Indestructible";

								// Token: 0x0400C35D RID: 50013
								public static LocString TOOLTIP = "Duplicants have perfected clothes manufacturing and are able to make suits that last forever";

								// Token: 0x0400C35E RID: 50014
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Indestructible Suits (No Durability)";
							}

							// Token: 0x0200315E RID: 12638
							public static class REINFORCED
							{
								// Token: 0x0400C35F RID: 50015
								public static LocString NAME = "Reinforced";

								// Token: 0x0400C360 RID: 50016
								public static LocString TOOLTIP = "Suits are more durable than usual";

								// Token: 0x0400C361 RID: 50017
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Reinforced Suits (Difficulty Down)";
							}

							// Token: 0x0200315F RID: 12639
							public static class DEFAULT
							{
								// Token: 0x0400C362 RID: 50018
								public static LocString NAME = "Default";

								// Token: 0x0400C363 RID: 50019
								public static LocString TOOLTIP = "Default suit durability";
							}

							// Token: 0x02003160 RID: 12640
							public static class FLIMSY
							{
								// Token: 0x0400C364 RID: 50020
								public static LocString NAME = "Flimsy";

								// Token: 0x0400C365 RID: 50021
								public static LocString TOOLTIP = "Suits wear out faster than usual";

								// Token: 0x0400C366 RID: 50022
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Flimsy Suits (Difficulty Up)";
							}

							// Token: 0x02003161 RID: 12641
							public static class THREADBARE
							{
								// Token: 0x0400C367 RID: 50023
								public static LocString NAME = "Threadbare";

								// Token: 0x0400C368 RID: 50024
								public static LocString TOOLTIP = "These Duplicants are no tailors - suits wear out much faster than usual";

								// Token: 0x0400C369 RID: 50025
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Threadbare Suits (Highest Difficulty)";
							}
						}
					}

					// Token: 0x020030C1 RID: 12481
					public class RADIATION
					{
						// Token: 0x0400C201 RID: 49665
						public static LocString NAME = "Radiation";

						// Token: 0x0400C202 RID: 49666
						public static LocString TOOLTIP = "Affects how susceptible Duplicants are to radiation sickness";

						// Token: 0x02003136 RID: 12598
						public static class LEVELS
						{
							// Token: 0x02003162 RID: 12642
							public static class HARDEST
							{
								// Token: 0x0400C36A RID: 50026
								public static LocString NAME = "Critical Mass";

								// Token: 0x0400C36B RID: 50027
								public static LocString TOOLTIP = "Duplicants feel ill at the merest mention of radiation...and may never truly recover";

								// Token: 0x0400C36C RID: 50028
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Super Radiation (Highest Difficulty)";
							}

							// Token: 0x02003163 RID: 12643
							public static class HARDER
							{
								// Token: 0x0400C36D RID: 50029
								public static LocString NAME = "Toxic Positivity";

								// Token: 0x0400C36E RID: 50030
								public static LocString TOOLTIP = "Duplicants are more sensitive to radiation exposure than usual";

								// Token: 0x0400C36F RID: 50031
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Radiation Vulnerable (Difficulty Up)";
							}

							// Token: 0x02003164 RID: 12644
							public static class DEFAULT
							{
								// Token: 0x0400C370 RID: 50032
								public static LocString NAME = "Default";

								// Token: 0x0400C371 RID: 50033
								public static LocString TOOLTIP = "Default radiation settings";
							}

							// Token: 0x02003165 RID: 12645
							public static class EASIER
							{
								// Token: 0x0400C372 RID: 50034
								public static LocString NAME = "Healthy Glow";

								// Token: 0x0400C373 RID: 50035
								public static LocString TOOLTIP = "Duplicants are more resistant to radiation exposure than usual";

								// Token: 0x0400C374 RID: 50036
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Radiation Shielded (Difficulty Down)";
							}

							// Token: 0x02003166 RID: 12646
							public static class EASIEST
							{
								// Token: 0x0400C375 RID: 50037
								public static LocString NAME = "Nuke-Proof";

								// Token: 0x0400C376 RID: 50038
								public static LocString TOOLTIP = "Duplicants could bathe in radioactive waste and not even notice";

								// Token: 0x0400C377 RID: 50039
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Radiation Protection (Lowest Difficulty)";
							}
						}
					}

					// Token: 0x020030C2 RID: 12482
					public class STRESS
					{
						// Token: 0x0400C203 RID: 49667
						public static LocString NAME = "Stress";

						// Token: 0x0400C204 RID: 49668
						public static LocString TOOLTIP = "Affects how quickly Duplicant stress rises";

						// Token: 0x02003137 RID: 12599
						public static class LEVELS
						{
							// Token: 0x02003167 RID: 12647
							public static class INDOMITABLE
							{
								// Token: 0x0400C378 RID: 50040
								public static LocString NAME = "Cloud Nine";

								// Token: 0x0400C379 RID: 50041
								public static LocString TOOLTIP = "A strong emotional support system makes these Duplicants impervious to all stress";

								// Token: 0x0400C37A RID: 50042
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Cloud Nine (No Stress)";
							}

							// Token: 0x02003168 RID: 12648
							public static class OPTIMISTIC
							{
								// Token: 0x0400C37B RID: 50043
								public static LocString NAME = "Chipper";

								// Token: 0x0400C37C RID: 50044
								public static LocString TOOLTIP = "Duplicants gain stress slower than usual";

								// Token: 0x0400C37D RID: 50045
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Chipper (Difficulty Down)";
							}

							// Token: 0x02003169 RID: 12649
							public static class DEFAULT
							{
								// Token: 0x0400C37E RID: 50046
								public static LocString NAME = "Default";

								// Token: 0x0400C37F RID: 50047
								public static LocString TOOLTIP = "Default stress change rate";
							}

							// Token: 0x0200316A RID: 12650
							public static class PESSIMISTIC
							{
								// Token: 0x0400C380 RID: 50048
								public static LocString NAME = "Glum";

								// Token: 0x0400C381 RID: 50049
								public static LocString TOOLTIP = "Duplicants gain stress more quickly than usual";

								// Token: 0x0400C382 RID: 50050
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Glum (Difficulty Up)";
							}

							// Token: 0x0200316B RID: 12651
							public static class DOOMED
							{
								// Token: 0x0400C383 RID: 50051
								public static LocString NAME = "Frankly Depressing";

								// Token: 0x0400C384 RID: 50052
								public static LocString TOOLTIP = "These Duplicants were never taught coping mechanisms... they're devastated by stress as a result";

								// Token: 0x0400C385 RID: 50053
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Frankly Depressing (Highest Difficulty)";
							}
						}
					}

					// Token: 0x020030C3 RID: 12483
					public class STRESS_BREAKS
					{
						// Token: 0x0400C205 RID: 49669
						public static LocString NAME = "Stress Reactions";

						// Token: 0x0400C206 RID: 49670
						public static LocString TOOLTIP = "Determines whether Duplicants wreak havoc on the colony when they reach maximum stress";

						// Token: 0x02003138 RID: 12600
						public static class LEVELS
						{
							// Token: 0x0200316C RID: 12652
							public static class DEFAULT
							{
								// Token: 0x0400C386 RID: 50054
								public static LocString NAME = "Enabled";

								// Token: 0x0400C387 RID: 50055
								public static LocString TOOLTIP = "Checked: Duplicants will wreak havoc when they reach 100% stress (Default)";
							}

							// Token: 0x0200316D RID: 12653
							public static class DISABLED
							{
								// Token: 0x0400C388 RID: 50056
								public static LocString NAME = "Disabled";

								// Token: 0x0400C389 RID: 50057
								public static LocString TOOLTIP = "Unchecked: Duplicants will not wreak havoc at maximum stress";
							}
						}
					}

					// Token: 0x020030C4 RID: 12484
					public class WORLDGEN_SEED
					{
						// Token: 0x0400C207 RID: 49671
						public static LocString NAME = "Worldgen Seed";

						// Token: 0x0400C208 RID: 49672
						public static LocString TOOLTIP = "This number chooses the procedural parameters that create your unique map\n\nWorldgen seeds can be copied and pasted so others can play a replica of your world configuration";
					}

					// Token: 0x020030C5 RID: 12485
					public class TELEPORTERS
					{
						// Token: 0x0400C209 RID: 49673
						public static LocString NAME = "Teleporters";

						// Token: 0x0400C20A RID: 49674
						public static LocString TOOLTIP = "Determines whether teleporters will be spawned during Worldgen";

						// Token: 0x02003139 RID: 12601
						public static class LEVELS
						{
							// Token: 0x0200316E RID: 12654
							public static class ENABLED
							{
								// Token: 0x0400C38A RID: 50058
								public static LocString NAME = "Enabled";

								// Token: 0x0400C38B RID: 50059
								public static LocString TOOLTIP = "Checked: Teleporters will spawn during Worldgen (Default)";
							}

							// Token: 0x0200316F RID: 12655
							public static class DISABLED
							{
								// Token: 0x0400C38C RID: 50060
								public static LocString NAME = "Disabled";

								// Token: 0x0400C38D RID: 50061
								public static LocString TOOLTIP = "Unchecked: No Teleporters will spawn during Worldgen";
							}
						}
					}
				}
			}

			// Token: 0x0200241C RID: 9244
			public class MAINMENU
			{
				// Token: 0x04009D33 RID: 40243
				public static LocString STARTDEMO = "START DEMO";

				// Token: 0x04009D34 RID: 40244
				public static LocString NEWGAME = "NEW GAME";

				// Token: 0x04009D35 RID: 40245
				public static LocString RESUMEGAME = "RESUME GAME";

				// Token: 0x04009D36 RID: 40246
				public static LocString LOADGAME = "LOAD GAME";

				// Token: 0x04009D37 RID: 40247
				public static LocString RETIREDCOLONIES = "COLONY SUMMARIES";

				// Token: 0x04009D38 RID: 40248
				public static LocString KLEIINVENTORY = "KLEI INVENTORY";

				// Token: 0x04009D39 RID: 40249
				public static LocString LOCKERMENU = "SUPPLY CLOSET";

				// Token: 0x04009D3A RID: 40250
				public static LocString SCENARIOS = "SCENARIOS";

				// Token: 0x04009D3B RID: 40251
				public static LocString TRANSLATIONS = "TRANSLATIONS";

				// Token: 0x04009D3C RID: 40252
				public static LocString OPTIONS = "OPTIONS";

				// Token: 0x04009D3D RID: 40253
				public static LocString QUITTODESKTOP = "QUIT";

				// Token: 0x04009D3E RID: 40254
				public static LocString RESTARTCONFIRM = "Should I really quit?\nAll unsaved progress will be lost.";

				// Token: 0x04009D3F RID: 40255
				public static LocString QUITCONFIRM = "Should I quit to the main menu?\nAll unsaved progress will be lost.";

				// Token: 0x04009D40 RID: 40256
				public static LocString RETIRECONFIRM = "Should I surrender under the soul-crushing weight of this universe's entropy and retire my colony?";

				// Token: 0x04009D41 RID: 40257
				public static LocString DESKTOPQUITCONFIRM = "Should I really quit?\nAll unsaved progress will be lost.";

				// Token: 0x04009D42 RID: 40258
				public static LocString RESUMEBUTTON_BASENAME = "{0}: Cycle {1}";

				// Token: 0x02002E85 RID: 11909
				public class DLC
				{
					// Token: 0x0400BBEC RID: 48108
					public static LocString ACTIVATE_EXPANSION1 = "ACTIVATE DLC";

					// Token: 0x0400BBED RID: 48109
					public static LocString ACTIVATE_EXPANSION1_DESC = "The game will need to restart in order to activate <i>Spaced Out!</i>";

					// Token: 0x0400BBEE RID: 48110
					public static LocString ACTIVATE_EXPANSION1_RAIL_DESC = "<i>Spaced Out!</i> will be activated the next time you launch the game. The game will now close.";

					// Token: 0x0400BBEF RID: 48111
					public static LocString DEACTIVATE_EXPANSION1 = "DEACTIVATE DLC";

					// Token: 0x0400BBF0 RID: 48112
					public static LocString DEACTIVATE_EXPANSION1_DESC = "The game will need to restart in order to activate the <i>Oxygen Not Included</i> base game.";

					// Token: 0x0400BBF1 RID: 48113
					public static LocString DEACTIVATE_EXPANSION1_RAIL_DESC = "<i>Spaced Out!</i> will be deactivated the next time you launch the game. The game will now close.";

					// Token: 0x0400BBF2 RID: 48114
					public static LocString AD_DLC1 = "Spaced Out! DLC";
				}
			}

			// Token: 0x0200241D RID: 9245
			public class DEVTOOLS
			{
				// Token: 0x04009D43 RID: 40259
				public static LocString TITLE = "About Dev Tools";

				// Token: 0x04009D44 RID: 40260
				public static LocString WARNING = "DANGER!!\n\nDev Tools are intended for developer use only. Using them may result in your save becoming unplayable, unstable, or severely damaged.\n\nThese tools are completely unsupported and may contain bugs. Are you sure you want to continue?";

				// Token: 0x04009D45 RID: 40261
				public static LocString DONTSHOW = "Do not show this message again";

				// Token: 0x04009D46 RID: 40262
				public static LocString BUTTON = "Show Dev Tools";
			}

			// Token: 0x0200241E RID: 9246
			public class NEWGAMESETTINGS
			{
				// Token: 0x04009D47 RID: 40263
				public static LocString HEADER = "GAME SETTINGS";

				// Token: 0x02002E86 RID: 11910
				public class BUTTONS
				{
					// Token: 0x0400BBF3 RID: 48115
					public static LocString STANDARDGAME = "Standard Game";

					// Token: 0x0400BBF4 RID: 48116
					public static LocString CUSTOMGAME = "Custom Game";

					// Token: 0x0400BBF5 RID: 48117
					public static LocString CANCEL = "Cancel";

					// Token: 0x0400BBF6 RID: 48118
					public static LocString STARTGAME = "Start Game";
				}
			}

			// Token: 0x0200241F RID: 9247
			public class COLONYDESTINATIONSCREEN
			{
				// Token: 0x04009D48 RID: 40264
				public static LocString TITLE = "CHOOSE A DESTINATION";

				// Token: 0x04009D49 RID: 40265
				public static LocString GENTLE_ZONE = "Habitable Zone";

				// Token: 0x04009D4A RID: 40266
				public static LocString DETAILS = "Destination Details";

				// Token: 0x04009D4B RID: 40267
				public static LocString START_SITE = "Immediate Surroundings";

				// Token: 0x04009D4C RID: 40268
				public static LocString COORDINATE = "Coordinates:";

				// Token: 0x04009D4D RID: 40269
				public static LocString CANCEL = "Back";

				// Token: 0x04009D4E RID: 40270
				public static LocString CUSTOMIZE = "Game Settings";

				// Token: 0x04009D4F RID: 40271
				public static LocString START_GAME = "Start Game";

				// Token: 0x04009D50 RID: 40272
				public static LocString SHUFFLE = "Shuffle";

				// Token: 0x04009D51 RID: 40273
				public static LocString SHUFFLETOOLTIP = "Reroll World Seed\n\nThis will shuffle the layout of your world and the geographical traits listed below";

				// Token: 0x04009D52 RID: 40274
				public static LocString HEADER_ASTEROID_STARTING = "Starting Asteroid";

				// Token: 0x04009D53 RID: 40275
				public static LocString HEADER_ASTEROID_NEARBY = "Nearby Asteroids";

				// Token: 0x04009D54 RID: 40276
				public static LocString HEADER_ASTEROID_DISTANT = "Distant Asteroids";

				// Token: 0x04009D55 RID: 40277
				public static LocString TRAITS_HEADER = "World Traits";

				// Token: 0x04009D56 RID: 40278
				public static LocString STORY_TRAITS_HEADER = "Story Traits";

				// Token: 0x04009D57 RID: 40279
				public static LocString NO_TRAITS = "No Traits";

				// Token: 0x04009D58 RID: 40280
				public static LocString SINGLE_TRAIT = "1 Trait";

				// Token: 0x04009D59 RID: 40281
				public static LocString TRAIT_COUNT = "{0} Traits";

				// Token: 0x04009D5A RID: 40282
				public static LocString TOO_MANY_TRAITS_WARNING = UI.YELLOW_PREFIX + "Too many!" + UI.COLOR_SUFFIX;

				// Token: 0x04009D5B RID: 40283
				public static LocString TOO_MANY_TRAITS_WARNING_TOOLTIP = UI.YELLOW_PREFIX + "Squeezing this many story traits into this asteroid may cause world gen to fail\n\nConsider lowering the number of story traits or changing the selected asteroid" + UI.COLOR_SUFFIX;

				// Token: 0x04009D5C RID: 40284
				public static LocString SHUFFLE_STORY_TRAITS_TOOLTIP = "Randomize Story Traits\n\nThis will select a comfortable number of story traits for the starting asteroid";

				// Token: 0x04009D5D RID: 40285
				public static LocString SELECTED_CLUSTER_TRAITS_HEADER = "Target Details";
			}

			// Token: 0x02002420 RID: 9248
			public class MODESELECTSCREEN
			{
				// Token: 0x04009D5E RID: 40286
				public static LocString HEADER = "GAME MODE";

				// Token: 0x04009D5F RID: 40287
				public static LocString BLANK_DESC = "Select a playstyle...";

				// Token: 0x04009D60 RID: 40288
				public static LocString SURVIVAL_TITLE = "SURVIVAL";

				// Token: 0x04009D61 RID: 40289
				public static LocString SURVIVAL_DESC = "Stay on your toes and one step ahead of this unforgiving world. One slip up could bring your colony crashing down.";

				// Token: 0x04009D62 RID: 40290
				public static LocString NOSWEAT_TITLE = "NO SWEAT";

				// Token: 0x04009D63 RID: 40291
				public static LocString NOSWEAT_DESC = "When disaster strikes (and it inevitably will), take a deep breath and stay calm. You have ample time to find a solution.";
			}

			// Token: 0x02002421 RID: 9249
			public class CLUSTERCATEGORYSELECTSCREEN
			{
				// Token: 0x04009D64 RID: 40292
				public static LocString HEADER = "ASTEROID STYLE";

				// Token: 0x04009D65 RID: 40293
				public static LocString BLANK_DESC = "Select an asteroid style...";

				// Token: 0x04009D66 RID: 40294
				public static LocString VANILLA_TITLE = "Classic";

				// Token: 0x04009D67 RID: 40295
				public static LocString VANILLA_DESC = "Scenarios similar to the <b>classic Oxygen Not Included</b> experience. Large starting asteroids with many resources.\nLess emphasis on space travel.";

				// Token: 0x04009D68 RID: 40296
				public static LocString SPACEDOUT_TITLE = "Spaced Out!";

				// Token: 0x04009D69 RID: 40297
				public static LocString SPACEDOUT_DESC = "Scenarios designed for the <b>Spaced Out! DLC</b>.\nSmaller starting asteroids with resources distributed across the starmap. More emphasis on space travel.";
			}

			// Token: 0x02002422 RID: 9250
			public class PATCHNOTESSCREEN
			{
				// Token: 0x04009D6A RID: 40298
				public static LocString HEADER = "IMPORTANT UPDATE NOTES";

				// Token: 0x04009D6B RID: 40299
				public static LocString OK_BUTTON = "OK";

				// Token: 0x04009D6C RID: 40300
				public static LocString FULLPATCHNOTES_TOOLTIP = "View the full patch notes online";
			}

			// Token: 0x02002423 RID: 9251
			public class MOTD
			{
				// Token: 0x04009D6D RID: 40301
				public static LocString IMAGE_HEADER = "FEB 2023 QOL";

				// Token: 0x04009D6E RID: 40302
				public static LocString NEWS_HEADER = "JOIN THE DISCUSSION";

				// Token: 0x04009D6F RID: 40303
				public static LocString NEWS_BODY = "Stay up to date by joining our mailing list, or head on over to the forums and join the discussion.";

				// Token: 0x04009D70 RID: 40304
				public static LocString PATCH_NOTES_SUMMARY = "This update includes:\n\n•<indent=20px>New search function and subcategories view for the build menu.</indent>\n•<indent=20px>New Disconnect tool.</indent>\n•<indent=20px>New Supply Closet music.</indent>\n•<indent=20px>Bug fixes and quality of life improvements.</indent>\n\n   Check out the full patch notes for more details!";

				// Token: 0x04009D71 RID: 40305
				public static LocString UPDATE_TEXT = "LAUNCHED!";

				// Token: 0x04009D72 RID: 40306
				public static LocString UPDATE_TEXT_EXPANSION1 = "LAUNCHED!";
			}

			// Token: 0x02002424 RID: 9252
			public class LOADSCREEN
			{
				// Token: 0x04009D73 RID: 40307
				public static LocString TITLE = "LOAD GAME";

				// Token: 0x04009D74 RID: 40308
				public static LocString TITLE_INSPECT = "LOAD GAME";

				// Token: 0x04009D75 RID: 40309
				public static LocString DELETEBUTTON = "DELETE";

				// Token: 0x04009D76 RID: 40310
				public static LocString BACKBUTTON = "< BACK";

				// Token: 0x04009D77 RID: 40311
				public static LocString CONFIRMDELETE = "Are you sure you want to delete {0}?\nYou cannot undo this action.";

				// Token: 0x04009D78 RID: 40312
				public static LocString SAVEDETAILS = "<b>File:</b> {0}\n\n<b>Save Date:</b>\n{1}\n\n<b>Base Name:</b> {2}\n<b>Duplicants Alive:</b> {3}\n<b>Cycle(s) Survived:</b> {4}";

				// Token: 0x04009D79 RID: 40313
				public static LocString AUTOSAVEWARNING = "<color=#ff0000>Autosave: This file will get deleted as new autosaves are created</color>";

				// Token: 0x04009D7A RID: 40314
				public static LocString CORRUPTEDSAVE = "<b><color=#ff0000>Could not load file {0}. Its data may be corrupted.</color></b>";

				// Token: 0x04009D7B RID: 40315
				public static LocString SAVE_FROM_SPACED_OUT = "<b><color=#ff0000>This save is from <i>Spaced Out!</i> Activate the DLC to play it! (v{2}/v{4})</color></b>";

				// Token: 0x04009D7C RID: 40316
				public static LocString SAVE_FROM_SPACED_OUT_TOOLTIP = "This save was created in the <i>Spaced Out!</i> DLC and can't be loaded in the base game.";

				// Token: 0x04009D7D RID: 40317
				public static LocString SAVE_TOO_NEW = "<b><color=#ff0000>Could not load file {0}. File is using build {1}, v{2}. This build is {3}, v{4}.</color></b>";

				// Token: 0x04009D7E RID: 40318
				public static LocString SAVE_MISSING_CONTENT = "<b><color=#ff0000>Could not load file {0}. File was saved with content that is not currently installed.</color></b>";

				// Token: 0x04009D7F RID: 40319
				public static LocString UNSUPPORTED_SAVE_VERSION = "<b><color=#ff0000>This save file is from a previous version of the game and is no longer supported.</color></b>";

				// Token: 0x04009D80 RID: 40320
				public static LocString MORE_INFO = "More Info";

				// Token: 0x04009D81 RID: 40321
				public static LocString NEWEST_SAVE = "Newest Save";

				// Token: 0x04009D82 RID: 40322
				public static LocString BASE_NAME = "Base Name";

				// Token: 0x04009D83 RID: 40323
				public static LocString CYCLES_SURVIVED = "Cycles Survived";

				// Token: 0x04009D84 RID: 40324
				public static LocString DUPLICANTS_ALIVE = "Duplicants Alive";

				// Token: 0x04009D85 RID: 40325
				public static LocString WORLD_NAME = "Asteroid Type";

				// Token: 0x04009D86 RID: 40326
				public static LocString NO_FILE_SELECTED = "No file selected";

				// Token: 0x04009D87 RID: 40327
				public static LocString COLONY_INFO_FMT = "{0}: {1}";

				// Token: 0x04009D88 RID: 40328
				public static LocString VANILLA_RESTART = "Loading this colony will require restarting the game with " + UI.DLC1.NAME_ITAL + " content disabled";

				// Token: 0x04009D89 RID: 40329
				public static LocString EXPANSION1_RESTART = "Loading this colony will require restarting the game with " + UI.DLC1.NAME_ITAL + " content enabled";

				// Token: 0x04009D8A RID: 40330
				public static LocString UNSUPPORTED_VANILLA_TEMP = "<b><color=#ff0000>This save file is from the base version of the game and currently cannot be loaded while " + UI.DLC1.NAME_ITAL + " is installed.</color></b>";

				// Token: 0x04009D8B RID: 40331
				public static LocString CONTENT = "Content";

				// Token: 0x04009D8C RID: 40332
				public static LocString VANILLA_CONTENT = "Vanilla FIXME";

				// Token: 0x04009D8D RID: 40333
				public static LocString EXPANSION1_CONTENT = UI.DLC1.NAME_ITAL + " Expansion FIXME";

				// Token: 0x04009D8E RID: 40334
				public static LocString SAVE_INFO = "{0} saves  {1} autosaves  {2}";

				// Token: 0x04009D8F RID: 40335
				public static LocString COLONIES_TITLE = "Colony View";

				// Token: 0x04009D90 RID: 40336
				public static LocString COLONY_TITLE = "Viewing colony '{0}'";

				// Token: 0x04009D91 RID: 40337
				public static LocString COLONY_FILE_SIZE = "Size: {0}";

				// Token: 0x04009D92 RID: 40338
				public static LocString COLONY_FILE_NAME = "File: '{0}'";

				// Token: 0x04009D93 RID: 40339
				public static LocString NO_PREVIEW = "NO PREVIEW";

				// Token: 0x04009D94 RID: 40340
				public static LocString LOCAL_SAVE = "local";

				// Token: 0x04009D95 RID: 40341
				public static LocString CLOUD_SAVE = "cloud";

				// Token: 0x04009D96 RID: 40342
				public static LocString CONVERT_COLONY = "CONVERT COLONY";

				// Token: 0x04009D97 RID: 40343
				public static LocString CONVERT_ALL_COLONIES = "CONVERT ALL";

				// Token: 0x04009D98 RID: 40344
				public static LocString CONVERT_ALL_WARNING = UI.PRE_KEYWORD + "\nWarning:" + UI.PST_KEYWORD + " Converting all colonies may take some time.";

				// Token: 0x04009D99 RID: 40345
				public static LocString SAVE_INFO_DIALOG_TITLE = "SAVE INFORMATION";

				// Token: 0x04009D9A RID: 40346
				public static LocString SAVE_INFO_DIALOG_TEXT = "Access your save files using the options below.";

				// Token: 0x04009D9B RID: 40347
				public static LocString SAVE_INFO_DIALOG_TOOLTIP = "Access your save file locations from here.";

				// Token: 0x04009D9C RID: 40348
				public static LocString CONVERT_ERROR_TITLE = "SAVE CONVERSION UNSUCCESSFUL";

				// Token: 0x04009D9D RID: 40349
				public static LocString CONVERT_ERROR = string.Concat(new string[]
				{
					"Converting the colony ",
					UI.PRE_KEYWORD,
					"{Colony}",
					UI.PST_KEYWORD,
					" was unsuccessful!\nThe error was:\n\n<b>{Error}</b>\n\nPlease try again, or post a bug in the forums if this problem keeps happening."
				});

				// Token: 0x04009D9E RID: 40350
				public static LocString CONVERT_TO_CLOUD = "CONVERT TO CLOUD SAVES";

				// Token: 0x04009D9F RID: 40351
				public static LocString CONVERT_TO_LOCAL = "CONVERT TO LOCAL SAVES";

				// Token: 0x04009DA0 RID: 40352
				public static LocString CONVERT_COLONY_TO_CLOUD = "Convert colony to use cloud saves";

				// Token: 0x04009DA1 RID: 40353
				public static LocString CONVERT_COLONY_TO_LOCAL = "Convert to colony to use local saves";

				// Token: 0x04009DA2 RID: 40354
				public static LocString CONVERT_ALL_TO_CLOUD = "Convert <b>all</b> colonies below to use cloud saves";

				// Token: 0x04009DA3 RID: 40355
				public static LocString CONVERT_ALL_TO_LOCAL = "Convert <b>all</b> colonies below to use local saves";

				// Token: 0x04009DA4 RID: 40356
				public static LocString CONVERT_ALL_TO_CLOUD_SUCCESS = string.Concat(new string[]
				{
					UI.PRE_KEYWORD,
					"SUCCESS!",
					UI.PST_KEYWORD,
					"\nAll existing colonies have been converted into ",
					UI.PRE_KEYWORD,
					"cloud",
					UI.PST_KEYWORD,
					" saves.\nNew colonies will use ",
					UI.PRE_KEYWORD,
					"cloud",
					UI.PST_KEYWORD,
					" saves by default.\n\n{Client} may take longer than usual to sync the next time you exit the game as a result of this change."
				});

				// Token: 0x04009DA5 RID: 40357
				public static LocString CONVERT_ALL_TO_LOCAL_SUCCESS = string.Concat(new string[]
				{
					UI.PRE_KEYWORD,
					"SUCCESS!",
					UI.PST_KEYWORD,
					"\nAll existing colonies have been converted into ",
					UI.PRE_KEYWORD,
					"local",
					UI.PST_KEYWORD,
					" saves.\nNew colonies will use ",
					UI.PRE_KEYWORD,
					"local",
					UI.PST_KEYWORD,
					" saves by default.\n\n{Client} may take longer than usual to sync the next time you exit the game as a result of this change."
				});

				// Token: 0x04009DA6 RID: 40358
				public static LocString CONVERT_TO_CLOUD_DETAILS = "Converting a colony to use cloud saves will move all of the save files for that colony into the cloud saves folder.\n\nThis allows your game platform to sync this colony to the cloud for your account, so it can be played on multiple machines.";

				// Token: 0x04009DA7 RID: 40359
				public static LocString CONVERT_TO_LOCAL_DETAILS = "Converting a colony to NOT use cloud saves will move all of the save files for that colony into the local saves folder.\n\n" + UI.PRE_KEYWORD + "These save files will no longer be synced to the cloud." + UI.PST_KEYWORD;

				// Token: 0x04009DA8 RID: 40360
				public static LocString OPEN_SAVE_FOLDER = "LOCAL SAVES";

				// Token: 0x04009DA9 RID: 40361
				public static LocString OPEN_CLOUDSAVE_FOLDER = "CLOUD SAVES";

				// Token: 0x04009DAA RID: 40362
				public static LocString MIGRATE_TITLE = "SAVE FILE MIGRATION";

				// Token: 0x04009DAB RID: 40363
				public static LocString MIGRATE_SAVE_FILES = "MIGRATE SAVE FILES";

				// Token: 0x04009DAC RID: 40364
				public static LocString MIGRATE_COUNT = string.Concat(new string[]
				{
					"\nFound ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" saves and ",
					UI.PRE_KEYWORD,
					"{1}",
					UI.PST_KEYWORD,
					" autosaves that require migration."
				});

				// Token: 0x04009DAD RID: 40365
				public static LocString MIGRATE_RESULT = string.Concat(new string[]
				{
					UI.PRE_KEYWORD,
					"SUCCESS!",
					UI.PST_KEYWORD,
					"\nMigration moved ",
					UI.PRE_KEYWORD,
					"{0}/{1}",
					UI.PST_KEYWORD,
					" saves and ",
					UI.PRE_KEYWORD,
					"{2}/{3}",
					UI.PST_KEYWORD,
					" autosaves",
					UI.PST_KEYWORD,
					"."
				});

				// Token: 0x04009DAE RID: 40366
				public static LocString MIGRATE_RESULT_FAILURES = string.Concat(new string[]
				{
					UI.PRE_KEYWORD,
					"<b>WARNING:</b> Not all saves could be migrated.",
					UI.PST_KEYWORD,
					"\nMigration moved ",
					UI.PRE_KEYWORD,
					"{0}/{1}",
					UI.PST_KEYWORD,
					" saves and ",
					UI.PRE_KEYWORD,
					"{2}/{3}",
					UI.PST_KEYWORD,
					" autosaves.\n\nThe file ",
					UI.PRE_KEYWORD,
					"{ErrorColony}",
					UI.PST_KEYWORD,
					" encountered this error:\n\n<b>{ErrorMessage}</b>"
				});

				// Token: 0x04009DAF RID: 40367
				public static LocString MIGRATE_RESULT_FAILURES_MORE_INFO_TITLE = "MIGRATION INCOMPLETE";

				// Token: 0x04009DB0 RID: 40368
				public static LocString MIGRATE_RESULT_FAILURES_MORE_INFO_PRE = "<b>The game was unable to move all save files to their new location.\nTo fix this, please:</b>\n\n";

				// Token: 0x04009DB1 RID: 40369
				public static LocString MIGRATE_RESULT_FAILURES_MORE_INFO_ITEM1 = "    1. Try temporarily disabling virus scanners and malware\n         protection programs.";

				// Token: 0x04009DB2 RID: 40370
				public static LocString MIGRATE_RESULT_FAILURES_MORE_INFO_ITEM2 = "    2. Turn off file sync services such as OneDrive and DropBox.";

				// Token: 0x04009DB3 RID: 40371
				public static LocString MIGRATE_RESULT_FAILURES_MORE_INFO_ITEM3 = "    3. Restart the game to retry file migration.";

				// Token: 0x04009DB4 RID: 40372
				public static LocString MIGRATE_RESULT_FAILURES_MORE_INFO_POST = "\n<b>If this still doesn't solve the problem, please post a bug in the forums and we will attempt to assist with your issue.</b>";

				// Token: 0x04009DB5 RID: 40373
				public static LocString MIGRATE_INFO = "We've changed how save files are organized!\nPlease " + UI.CLICK(UI.ClickType.click) + " the button below to automatically update your save file storage.";

				// Token: 0x04009DB6 RID: 40374
				public static LocString MIGRATE_DONE = "CONTINUE";

				// Token: 0x04009DB7 RID: 40375
				public static LocString MIGRATE_FAILURES_FORUM_BUTTON = "VISIT FORUMS";

				// Token: 0x04009DB8 RID: 40376
				public static LocString MIGRATE_FAILURES_DONE = "MORE INFO";

				// Token: 0x04009DB9 RID: 40377
				public static LocString CLOUD_TUTORIAL_BOUNCER = "Upload Saves to Cloud";
			}

			// Token: 0x02002425 RID: 9253
			public class SAVESCREEN
			{
				// Token: 0x04009DBA RID: 40378
				public static LocString TITLE = "SAVE SLOTS";

				// Token: 0x04009DBB RID: 40379
				public static LocString NEWSAVEBUTTON = "New Save";

				// Token: 0x04009DBC RID: 40380
				public static LocString OVERWRITEMESSAGE = "Are you sure you want to overwrite {0}?";

				// Token: 0x04009DBD RID: 40381
				public static LocString SAVENAMETITLE = "SAVE NAME";

				// Token: 0x04009DBE RID: 40382
				public static LocString CONFIRMNAME = "Confirm";

				// Token: 0x04009DBF RID: 40383
				public static LocString CANCELNAME = "Cancel";

				// Token: 0x04009DC0 RID: 40384
				public static LocString IO_ERROR = "An error occurred trying to save your game. Please ensure there is sufficient disk space.\n\n{0}";

				// Token: 0x04009DC1 RID: 40385
				public static LocString REPORT_BUG = "Report Bug";
			}

			// Token: 0x02002426 RID: 9254
			public class RAILFORCEQUIT
			{
				// Token: 0x04009DC2 RID: 40386
				public static LocString SAVE_EXIT = "Play time has expired and the game is exiting. Would you like to overwrite {0}?";

				// Token: 0x04009DC3 RID: 40387
				public static LocString WARN_EXIT = "Play time has expired and the game will now exit.";

				// Token: 0x04009DC4 RID: 40388
				public static LocString DLC_NOT_PURCHASED = "The <i>Spaced Out!</i> DLC has not yet been purchased in the WeGame store. Purchase <i>Spaced Out!</i> to support <i>Oxygen Not Included</i> and enjoy the new content!";
			}

			// Token: 0x02002427 RID: 9255
			public class MOD_ERRORS
			{
				// Token: 0x04009DC5 RID: 40389
				public static LocString TITLE = "MOD ERRORS";

				// Token: 0x04009DC6 RID: 40390
				public static LocString DETAILS = "DETAILS";

				// Token: 0x04009DC7 RID: 40391
				public static LocString CLOSE = "CLOSE";
			}

			// Token: 0x02002428 RID: 9256
			public class MODS
			{
				// Token: 0x04009DC8 RID: 40392
				public static LocString TITLE = "MODS";

				// Token: 0x04009DC9 RID: 40393
				public static LocString MANAGE = "Subscription";

				// Token: 0x04009DCA RID: 40394
				public static LocString MANAGE_LOCAL = "Browse";

				// Token: 0x04009DCB RID: 40395
				public static LocString WORKSHOP = "STEAM WORKSHOP";

				// Token: 0x04009DCC RID: 40396
				public static LocString ENABLE_ALL = "ENABLE ALL";

				// Token: 0x04009DCD RID: 40397
				public static LocString DISABLE_ALL = "DISABLE ALL";

				// Token: 0x04009DCE RID: 40398
				public static LocString DRAG_TO_REORDER = "Drag to reorder";

				// Token: 0x04009DCF RID: 40399
				public static LocString REQUIRES_RESTART = "Mod changes require restart";

				// Token: 0x04009DD0 RID: 40400
				public static LocString FAILED_TO_LOAD = "A mod failed to load and is being disabled:\n\n{0}: {1}\n\n{2}";

				// Token: 0x04009DD1 RID: 40401
				public static LocString DB_CORRUPT = "An error occurred trying to load the Mod Database.\n\n{0}";

				// Token: 0x02002E87 RID: 11911
				public class CONTENT_FAILURE
				{
					// Token: 0x0400BBF7 RID: 48119
					public static LocString DISABLED_CONTENT = " - <b>Not compatible with <i>{Content}</i></b>";

					// Token: 0x0400BBF8 RID: 48120
					public static LocString NO_CONTENT = " - <b>No compatible mod found</b>";

					// Token: 0x0400BBF9 RID: 48121
					public static LocString OLD_API = " - <b>Mod out-of-date</b>";
				}

				// Token: 0x02002E88 RID: 11912
				public class TOOLTIPS
				{
					// Token: 0x0400BBFA RID: 48122
					public static LocString ENABLED = "Enabled";

					// Token: 0x0400BBFB RID: 48123
					public static LocString DISABLED = "Disabled";

					// Token: 0x0400BBFC RID: 48124
					public static LocString MANAGE_STEAM_SUBSCRIPTION = "Manage Steam Subscription";

					// Token: 0x0400BBFD RID: 48125
					public static LocString MANAGE_RAIL_SUBSCRIPTION = "Manage Subscription";

					// Token: 0x0400BBFE RID: 48126
					public static LocString MANAGE_LOCAL_MOD = "Manage Local Mod";
				}

				// Token: 0x02002E89 RID: 11913
				public class RAILMODUPLOAD
				{
					// Token: 0x0400BBFF RID: 48127
					public static LocString TITLE = "Upload Mod";

					// Token: 0x0400BC00 RID: 48128
					public static LocString NAME = "Mod Name";

					// Token: 0x0400BC01 RID: 48129
					public static LocString DESCRIPTION = "Mod Description";

					// Token: 0x0400BC02 RID: 48130
					public static LocString VERSION = "Version Number";

					// Token: 0x0400BC03 RID: 48131
					public static LocString PREVIEW_IMAGE = "Preview Image Path";

					// Token: 0x0400BC04 RID: 48132
					public static LocString CONTENT_FOLDER = "Content Folder Path";

					// Token: 0x0400BC05 RID: 48133
					public static LocString SHARE_TYPE = "Share Type";

					// Token: 0x0400BC06 RID: 48134
					public static LocString SUBMIT = "Submit";

					// Token: 0x0400BC07 RID: 48135
					public static LocString SUBMIT_READY = "This mod is ready to submit";

					// Token: 0x0400BC08 RID: 48136
					public static LocString SUBMIT_NOT_READY = "The mod cannot be submitted. Check that all fields are properly entered and that the paths are valid.";

					// Token: 0x020030C6 RID: 12486
					public static class MOD_SHARE_TYPE
					{
						// Token: 0x0400C20B RID: 49675
						public static LocString PRIVATE = "Private";

						// Token: 0x0400C20C RID: 49676
						public static LocString TOOLTIP_PRIVATE = "This mod will only be visible to its creator";

						// Token: 0x0400C20D RID: 49677
						public static LocString FRIEND = "Friend";

						// Token: 0x0400C20E RID: 49678
						public static LocString TOOLTIP_FRIEND = "Friend";

						// Token: 0x0400C20F RID: 49679
						public static LocString PUBLIC = "Public";

						// Token: 0x0400C210 RID: 49680
						public static LocString TOOLTIP_PUBLIC = "This mod will be available to all players after publishing. It may be subject to review before being allowed to be published.";
					}

					// Token: 0x020030C7 RID: 12487
					public static class MOD_UPLOAD_RESULT
					{
						// Token: 0x0400C211 RID: 49681
						public static LocString SUCCESS = "Mod upload succeeded.";

						// Token: 0x0400C212 RID: 49682
						public static LocString FAILURE = "Mod upload failed.";
					}
				}
			}

			// Token: 0x02002429 RID: 9257
			public class MOD_EVENTS
			{
				// Token: 0x04009DD2 RID: 40402
				public static LocString REQUIRED = "REQUIRED";

				// Token: 0x04009DD3 RID: 40403
				public static LocString NOT_FOUND = "NOT FOUND";

				// Token: 0x04009DD4 RID: 40404
				public static LocString INSTALL_INFO_INACCESSIBLE = "INACCESSIBLE";

				// Token: 0x04009DD5 RID: 40405
				public static LocString OUT_OF_ORDER = "ORDERING CHANGED";

				// Token: 0x04009DD6 RID: 40406
				public static LocString ACTIVE_DURING_CRASH = "ACTIVE DURING CRASH";

				// Token: 0x04009DD7 RID: 40407
				public static LocString EXPECTED_ENABLED = "NOT ENABLED";

				// Token: 0x04009DD8 RID: 40408
				public static LocString EXPECTED_DISABLED = "NOT DISABLED";

				// Token: 0x04009DD9 RID: 40409
				public static LocString VERSION_UPDATE = "VERSION UPDATE";

				// Token: 0x04009DDA RID: 40410
				public static LocString AVAILABLE_CONTENT_CHANGED = "CONTENT CHANGED";

				// Token: 0x04009DDB RID: 40411
				public static LocString INSTALL_FAILED = "INSTALL FAILED";

				// Token: 0x04009DDC RID: 40412
				public static LocString INSTALLED = "INSTALLED";

				// Token: 0x04009DDD RID: 40413
				public static LocString UNINSTALLED = "UNINSTALLED";

				// Token: 0x04009DDE RID: 40414
				public static LocString REQUIRES_RESTART = "RESTART REQUIRED";

				// Token: 0x04009DDF RID: 40415
				public static LocString BAD_WORLD_GEN = "LOAD FAILED";

				// Token: 0x04009DE0 RID: 40416
				public static LocString DEACTIVATED = "DEACTIVATED";

				// Token: 0x04009DE1 RID: 40417
				public static LocString ALL_MODS_DISABLED_EARLY_ACCESS = "DEACTIVATED";

				// Token: 0x02002E8A RID: 11914
				public class TOOLTIPS
				{
					// Token: 0x0400BC09 RID: 48137
					public static LocString REQUIRED = "The current save game couldn't load this mod. Unexpected things may happen!";

					// Token: 0x0400BC0A RID: 48138
					public static LocString NOT_FOUND = "This mod isn't installed";

					// Token: 0x0400BC0B RID: 48139
					public static LocString INSTALL_INFO_INACCESSIBLE = "Mod files are inaccessible";

					// Token: 0x0400BC0C RID: 48140
					public static LocString OUT_OF_ORDER = "Active mod has changed order with respect to some other active mod";

					// Token: 0x0400BC0D RID: 48141
					public static LocString ACTIVE_DURING_CRASH = "Mod was active during a crash and may be the cause";

					// Token: 0x0400BC0E RID: 48142
					public static LocString EXPECTED_ENABLED = "This mod needs to be enabled";

					// Token: 0x0400BC0F RID: 48143
					public static LocString EXPECTED_DISABLED = "This mod needs to be disabled";

					// Token: 0x0400BC10 RID: 48144
					public static LocString VERSION_UPDATE = "New version detected";

					// Token: 0x0400BC11 RID: 48145
					public static LocString AVAILABLE_CONTENT_CHANGED = "Content added or removed";

					// Token: 0x0400BC12 RID: 48146
					public static LocString INSTALL_FAILED = "Installation failed";

					// Token: 0x0400BC13 RID: 48147
					public static LocString INSTALLED = "Installation succeeded";

					// Token: 0x0400BC14 RID: 48148
					public static LocString UNINSTALLED = "Uninstalled";

					// Token: 0x0400BC15 RID: 48149
					public static LocString BAD_WORLD_GEN = "Encountered an error while loading file";

					// Token: 0x0400BC16 RID: 48150
					public static LocString DEACTIVATED = "Deactivated due to errors";

					// Token: 0x0400BC17 RID: 48151
					public static LocString ALL_MODS_DISABLED_EARLY_ACCESS = "Deactivated due to Early Access for " + UI.DLC1.NAME_ITAL;
				}
			}

			// Token: 0x0200242A RID: 9258
			public class MOD_DIALOGS
			{
				// Token: 0x04009DE2 RID: 40418
				public static LocString ADDITIONAL_MOD_EVENTS = "(...additional entries omitted)";

				// Token: 0x02002E8B RID: 11915
				public class INSTALL_INFO_INACCESSIBLE
				{
					// Token: 0x0400BC18 RID: 48152
					public static LocString TITLE = "STEAM CONTENT ERROR";

					// Token: 0x0400BC19 RID: 48153
					public static LocString MESSAGE = "Failed to access local Steam files for mod {0}.\nTry restarting Oxygen not Included.\nIf that doesn't work, try re-subscribing to the mod via Steam.";
				}

				// Token: 0x02002E8C RID: 11916
				public class STEAM_SUBSCRIBED
				{
					// Token: 0x0400BC1A RID: 48154
					public static LocString TITLE = "STEAM MOD SUBSCRIBED";

					// Token: 0x0400BC1B RID: 48155
					public static LocString MESSAGE = "Subscribed to Steam mod: {0}";
				}

				// Token: 0x02002E8D RID: 11917
				public class STEAM_UPDATED
				{
					// Token: 0x0400BC1C RID: 48156
					public static LocString TITLE = "STEAM MOD UPDATE";

					// Token: 0x0400BC1D RID: 48157
					public static LocString MESSAGE = "Updating version of Steam mod: {0}";
				}

				// Token: 0x02002E8E RID: 11918
				public class STEAM_UNSUBSCRIBED
				{
					// Token: 0x0400BC1E RID: 48158
					public static LocString TITLE = "STEAM MOD UNSUBSCRIBED";

					// Token: 0x0400BC1F RID: 48159
					public static LocString MESSAGE = "Unsubscribed from Steam mod: {0}";
				}

				// Token: 0x02002E8F RID: 11919
				public class STEAM_REFRESH
				{
					// Token: 0x0400BC20 RID: 48160
					public static LocString TITLE = "STEAM MODS REFRESHED";

					// Token: 0x0400BC21 RID: 48161
					public static LocString MESSAGE = "Refreshed Steam mods:\n{0}";
				}

				// Token: 0x02002E90 RID: 11920
				public class ALL_MODS_DISABLED_EARLY_ACCESS
				{
					// Token: 0x0400BC22 RID: 48162
					public static LocString TITLE = "ALL MODS DISABLED";

					// Token: 0x0400BC23 RID: 48163
					public static LocString MESSAGE = "Mod support is temporarily suspended for the initial launch of " + UI.DLC1.NAME_ITAL + " into Early Access:\n{0}";
				}

				// Token: 0x02002E91 RID: 11921
				public class LOAD_FAILURE
				{
					// Token: 0x0400BC24 RID: 48164
					public static LocString TITLE = "LOAD FAILURE";

					// Token: 0x0400BC25 RID: 48165
					public static LocString MESSAGE = "Failed to load one or more mods:\n{0}\nThey will be re-installed when the game is restarted.\nGame may be unstable until then.";
				}

				// Token: 0x02002E92 RID: 11922
				public class SAVE_GAME_MODS_DIFFER
				{
					// Token: 0x0400BC26 RID: 48166
					public static LocString TITLE = "MOD DIFFERENCES";

					// Token: 0x0400BC27 RID: 48167
					public static LocString MESSAGE = "Save game mods differ from currently active mods:\n{0}";
				}

				// Token: 0x02002E93 RID: 11923
				public class MOD_ERRORS_ON_BOOT
				{
					// Token: 0x0400BC28 RID: 48168
					public static LocString TITLE = "MOD ERRORS";

					// Token: 0x0400BC29 RID: 48169
					public static LocString MESSAGE = "An error occurred during start-up with mods active.\nAll mods have been disabled to ensure a clean restart.\n{0}";

					// Token: 0x0400BC2A RID: 48170
					public static LocString DEV_MESSAGE = "An error occurred during start-up with mods active.\n{0}\nDisable all mods and restart, or continue in an unstable state?";
				}

				// Token: 0x02002E94 RID: 11924
				public class MODS_SCREEN_CHANGES
				{
					// Token: 0x0400BC2B RID: 48171
					public static LocString TITLE = "MODS CHANGED";

					// Token: 0x0400BC2C RID: 48172
					public static LocString MESSAGE = "Previous config:\n{0}\nRestart required to reload mods.\nGame may be unstable until then.";
				}

				// Token: 0x02002E95 RID: 11925
				public class MOD_EVENTS
				{
					// Token: 0x0400BC2D RID: 48173
					public static LocString TITLE = "MOD EVENTS";

					// Token: 0x0400BC2E RID: 48174
					public static LocString MESSAGE = "{0}";

					// Token: 0x0400BC2F RID: 48175
					public static LocString DEV_MESSAGE = "{0}\nCheck Player.log for details.";
				}

				// Token: 0x02002E96 RID: 11926
				public class RESTART
				{
					// Token: 0x0400BC30 RID: 48176
					public static LocString OK = "RESTART";

					// Token: 0x0400BC31 RID: 48177
					public static LocString CANCEL = "CONTINUE";

					// Token: 0x0400BC32 RID: 48178
					public static LocString MESSAGE = "{0}\nRestart required.";

					// Token: 0x0400BC33 RID: 48179
					public static LocString DEV_MESSAGE = "{0}\nRestart required.\nGame may be unstable until then.";
				}
			}

			// Token: 0x0200242B RID: 9259
			public class PAUSE_SCREEN
			{
				// Token: 0x04009DE3 RID: 40419
				public static LocString TITLE = "PAUSED";

				// Token: 0x04009DE4 RID: 40420
				public static LocString RESUME = "Resume";

				// Token: 0x04009DE5 RID: 40421
				public static LocString LOGBOOK = "Logbook";

				// Token: 0x04009DE6 RID: 40422
				public static LocString OPTIONS = "Options";

				// Token: 0x04009DE7 RID: 40423
				public static LocString SAVE = "Save";

				// Token: 0x04009DE8 RID: 40424
				public static LocString SAVEAS = "Save As";

				// Token: 0x04009DE9 RID: 40425
				public static LocString COLONY_SUMMARY = "Colony Summary";

				// Token: 0x04009DEA RID: 40426
				public static LocString LOCKERMENU = "Supply Closet";

				// Token: 0x04009DEB RID: 40427
				public static LocString LOAD = "Load";

				// Token: 0x04009DEC RID: 40428
				public static LocString QUIT = "Main Menu";

				// Token: 0x04009DED RID: 40429
				public static LocString DESKTOPQUIT = "Quit to Desktop";

				// Token: 0x04009DEE RID: 40430
				public static LocString WORLD_SEED = "Coordinates: {0}";

				// Token: 0x04009DEF RID: 40431
				public static LocString WORLD_SEED_TOOLTIP = "Share coordinates with a friend and they can start a colony on an identical asteroid!\n\n{0} - The asteroid\n\n{1} - The world seed\n\n{2} - Difficulty and Custom settings\n\n{3} - Story Trait settings";

				// Token: 0x04009DF0 RID: 40432
				public static LocString WORLD_SEED_COPY_TOOLTIP = "Copy Coordinates to clipboard\n\nShare coordinates with a friend and they can start a colony on an identical asteroid!";

				// Token: 0x04009DF1 RID: 40433
				public static LocString MANAGEMENT_BUTTON = "Pause Menu";
			}

			// Token: 0x0200242C RID: 9260
			public class OPTIONS_SCREEN
			{
				// Token: 0x04009DF2 RID: 40434
				public static LocString TITLE = "OPTIONS";

				// Token: 0x04009DF3 RID: 40435
				public static LocString GRAPHICS = "Graphics";

				// Token: 0x04009DF4 RID: 40436
				public static LocString AUDIO = "Audio";

				// Token: 0x04009DF5 RID: 40437
				public static LocString GAME = "Game";

				// Token: 0x04009DF6 RID: 40438
				public static LocString CONTROLS = "Controls";

				// Token: 0x04009DF7 RID: 40439
				public static LocString UNITS = "Temperature Units";

				// Token: 0x04009DF8 RID: 40440
				public static LocString METRICS = "Data Communication";

				// Token: 0x04009DF9 RID: 40441
				public static LocString LANGUAGE = "Change Language";

				// Token: 0x04009DFA RID: 40442
				public static LocString WORLD_GEN = "World Generation Key";

				// Token: 0x04009DFB RID: 40443
				public static LocString RESET_TUTORIAL = "Reset Tutorial Messages";

				// Token: 0x04009DFC RID: 40444
				public static LocString RESET_TUTORIAL_WARNING = "All tutorial messages will be reset, and\nwill show up again the next time you play the game.";

				// Token: 0x04009DFD RID: 40445
				public static LocString FEEDBACK = "Feedback";

				// Token: 0x04009DFE RID: 40446
				public static LocString CREDITS = "Credits";

				// Token: 0x04009DFF RID: 40447
				public static LocString BACK = "Done";

				// Token: 0x04009E00 RID: 40448
				public static LocString UNLOCK_SANDBOX = "Unlock Sandbox Mode";

				// Token: 0x04009E01 RID: 40449
				public static LocString MODS = "MODS";

				// Token: 0x04009E02 RID: 40450
				public static LocString SAVE_OPTIONS = "Save Options";

				// Token: 0x02002E97 RID: 11927
				public class TOGGLE_SANDBOX_SCREEN
				{
					// Token: 0x0400BC34 RID: 48180
					public static LocString UNLOCK_SANDBOX_WARNING = "Sandbox Mode will be enabled for this save file";

					// Token: 0x0400BC35 RID: 48181
					public static LocString CONFIRM = "Enable Sandbox Mode";

					// Token: 0x0400BC36 RID: 48182
					public static LocString CANCEL = "Cancel";

					// Token: 0x0400BC37 RID: 48183
					public static LocString CONFIRM_SAVE_BACKUP = "Enable Sandbox Mode, but save a backup first";

					// Token: 0x0400BC38 RID: 48184
					public static LocString BACKUP_SAVE_GAME_APPEND = " (BACKUP)";
				}
			}

			// Token: 0x0200242D RID: 9261
			public class INPUT_BINDINGS_SCREEN
			{
				// Token: 0x04009E03 RID: 40451
				public static LocString TITLE = "CUSTOMIZE KEYS";

				// Token: 0x04009E04 RID: 40452
				public static LocString RESET = "Reset";

				// Token: 0x04009E05 RID: 40453
				public static LocString APPLY = "Done";

				// Token: 0x04009E06 RID: 40454
				public static LocString DUPLICATE = "{0} was already bound to {1} and is now unbound.";

				// Token: 0x04009E07 RID: 40455
				public static LocString UNBOUND_ACTION = "{0} is unbound. Are you sure you want to continue?";

				// Token: 0x04009E08 RID: 40456
				public static LocString MULTIPLE_UNBOUND_ACTIONS = "You have multiple unbound actions, this may result in difficulty playing the game. Are you sure you want to continue?";

				// Token: 0x04009E09 RID: 40457
				public static LocString WAITING_FOR_INPUT = "???";
			}

			// Token: 0x0200242E RID: 9262
			public class TRANSLATIONS_SCREEN
			{
				// Token: 0x04009E0A RID: 40458
				public static LocString TITLE = "TRANSLATIONS";

				// Token: 0x04009E0B RID: 40459
				public static LocString UNINSTALL = "Uninstall";

				// Token: 0x04009E0C RID: 40460
				public static LocString PREINSTALLED_HEADER = "Preinstalled Language Packs";

				// Token: 0x04009E0D RID: 40461
				public static LocString UGC_HEADER = "Subscribed Workshop Language Packs";

				// Token: 0x04009E0E RID: 40462
				public static LocString UGC_MOD_TITLE_FORMAT = "{0} (workshop)";

				// Token: 0x04009E0F RID: 40463
				public static LocString ARE_YOU_SURE = "Are you sure you want to uninstall this language pack?";

				// Token: 0x04009E10 RID: 40464
				public static LocString PLEASE_REBOOT = "Please restart your game for these changes to take effect.";

				// Token: 0x04009E11 RID: 40465
				public static LocString NO_PACKS = "Steam Workshop";

				// Token: 0x04009E12 RID: 40466
				public static LocString DOWNLOAD = "Start Download";

				// Token: 0x04009E13 RID: 40467
				public static LocString INSTALL = "Install";

				// Token: 0x04009E14 RID: 40468
				public static LocString INSTALLED = "Installed";

				// Token: 0x04009E15 RID: 40469
				public static LocString NO_STEAM = "Unable to retrieve language list from Steam";

				// Token: 0x04009E16 RID: 40470
				public static LocString RESTART = "RESTART";

				// Token: 0x04009E17 RID: 40471
				public static LocString CANCEL = "CANCEL";

				// Token: 0x04009E18 RID: 40472
				public static LocString MISSING_LANGUAGE_PACK = "Selected language pack ({0}) not found.\nReverting to default language.";

				// Token: 0x04009E19 RID: 40473
				public static LocString UNKNOWN = "Unknown";

				// Token: 0x02002E98 RID: 11928
				public class PREINSTALLED_LANGUAGES
				{
					// Token: 0x0400BC39 RID: 48185
					public static LocString EN = "English (Klei)";

					// Token: 0x0400BC3A RID: 48186
					public static LocString ZH_KLEI = "Chinese (Klei)";

					// Token: 0x0400BC3B RID: 48187
					public static LocString KO_KLEI = "Korean (Klei)";

					// Token: 0x0400BC3C RID: 48188
					public static LocString RU_KLEI = "Russian (Klei)";
				}
			}

			// Token: 0x0200242F RID: 9263
			public class SCENARIOS_MENU
			{
				// Token: 0x04009E1A RID: 40474
				public static LocString TITLE = "Scenarios";

				// Token: 0x04009E1B RID: 40475
				public static LocString UNSUBSCRIBE = "Unsubscribe";

				// Token: 0x04009E1C RID: 40476
				public static LocString UNSUBSCRIBE_CONFIRM = "Are you sure you want to unsubscribe from this scenario?";

				// Token: 0x04009E1D RID: 40477
				public static LocString LOAD_SCENARIO_CONFIRM = "Load the \"{SCENARIO_NAME}\" scenario?";

				// Token: 0x04009E1E RID: 40478
				public static LocString LOAD_CONFIRM_TITLE = "LOAD";

				// Token: 0x04009E1F RID: 40479
				public static LocString SCENARIO_NAME = "Name:";

				// Token: 0x04009E20 RID: 40480
				public static LocString SCENARIO_DESCRIPTION = "Description";

				// Token: 0x04009E21 RID: 40481
				public static LocString BUTTON_DONE = "Done";

				// Token: 0x04009E22 RID: 40482
				public static LocString BUTTON_LOAD = "Load";

				// Token: 0x04009E23 RID: 40483
				public static LocString BUTTON_WORKSHOP = "Steam Workshop";

				// Token: 0x04009E24 RID: 40484
				public static LocString NO_SCENARIOS_AVAILABLE = "No scenarios available.\n\nSubscribe to some in the Steam Workshop.";
			}

			// Token: 0x02002430 RID: 9264
			public class AUDIO_OPTIONS_SCREEN
			{
				// Token: 0x04009E25 RID: 40485
				public static LocString TITLE = "AUDIO OPTIONS";

				// Token: 0x04009E26 RID: 40486
				public static LocString HEADER_VOLUME = "VOLUME";

				// Token: 0x04009E27 RID: 40487
				public static LocString HEADER_SETTINGS = "SETTINGS";

				// Token: 0x04009E28 RID: 40488
				public static LocString DONE_BUTTON = "Done";

				// Token: 0x04009E29 RID: 40489
				public static LocString MUSIC_EVERY_CYCLE = "Play background music each morning";

				// Token: 0x04009E2A RID: 40490
				public static LocString MUSIC_EVERY_CYCLE_TOOLTIP = "If enabled, background music will play every cycle instead of every few cycles";

				// Token: 0x04009E2B RID: 40491
				public static LocString AUTOMATION_SOUNDS_ALWAYS = "Always play automation sounds";

				// Token: 0x04009E2C RID: 40492
				public static LocString AUTOMATION_SOUNDS_ALWAYS_TOOLTIP = "If enabled, automation sound effects will play even when outside of the " + UI.FormatAsOverlay("Automation Overlay");

				// Token: 0x04009E2D RID: 40493
				public static LocString MUTE_ON_FOCUS_LOST = "Mute when unfocused";

				// Token: 0x04009E2E RID: 40494
				public static LocString MUTE_ON_FOCUS_LOST_TOOLTIP = "If enabled, the game will be muted while minimized or if the application loses focus";

				// Token: 0x04009E2F RID: 40495
				public static LocString AUDIO_BUS_MASTER = "Master";

				// Token: 0x04009E30 RID: 40496
				public static LocString AUDIO_BUS_SFX = "SFX";

				// Token: 0x04009E31 RID: 40497
				public static LocString AUDIO_BUS_MUSIC = "Music";

				// Token: 0x04009E32 RID: 40498
				public static LocString AUDIO_BUS_AMBIENCE = "Ambience";

				// Token: 0x04009E33 RID: 40499
				public static LocString AUDIO_BUS_UI = "UI";
			}

			// Token: 0x02002431 RID: 9265
			public class GAME_OPTIONS_SCREEN
			{
				// Token: 0x04009E34 RID: 40500
				public static LocString TITLE = "GAME OPTIONS";

				// Token: 0x04009E35 RID: 40501
				public static LocString GENERAL_GAME_OPTIONS = "GENERAL";

				// Token: 0x04009E36 RID: 40502
				public static LocString DISABLED_WARNING = "More options available in-game";

				// Token: 0x04009E37 RID: 40503
				public static LocString DEFAULT_TO_CLOUD_SAVES = "Default to cloud saves";

				// Token: 0x04009E38 RID: 40504
				public static LocString DEFAULT_TO_CLOUD_SAVES_TOOLTIP = "When a new colony is created, this controls whether it will be saved into the cloud saves folder for syncing or not.";

				// Token: 0x04009E39 RID: 40505
				public static LocString RESET_TUTORIAL_DESCRIPTION = "Mark all tutorial messages \"unread\"";

				// Token: 0x04009E3A RID: 40506
				public static LocString SANDBOX_DESCRIPTION = "Enable sandbox tools";

				// Token: 0x04009E3B RID: 40507
				public static LocString CONTROLS_DESCRIPTION = "Change key bindings";

				// Token: 0x04009E3C RID: 40508
				public static LocString TEMPERATURE_UNITS = "TEMPERATURE UNITS";

				// Token: 0x04009E3D RID: 40509
				public static LocString SAVE_OPTIONS = "SAVE";

				// Token: 0x04009E3E RID: 40510
				public static LocString CAMERA_SPEED_LABEL = "Camera Pan Speed: {0}%";
			}

			// Token: 0x02002432 RID: 9266
			public class METRIC_OPTIONS_SCREEN
			{
				// Token: 0x04009E3F RID: 40511
				public static LocString TITLE = "DATA COMMUNICATION";

				// Token: 0x04009E40 RID: 40512
				public static LocString HEADER_METRICS = "USER DATA";
			}

			// Token: 0x02002433 RID: 9267
			public class COLONY_SAVE_OPTIONS_SCREEN
			{
				// Token: 0x04009E41 RID: 40513
				public static LocString TITLE = "COLONY SAVE OPTIONS";

				// Token: 0x04009E42 RID: 40514
				public static LocString DESCRIPTION = "Note: These values are configured per save file";

				// Token: 0x04009E43 RID: 40515
				public static LocString AUTOSAVE_FREQUENCY = "Autosave frequency:";

				// Token: 0x04009E44 RID: 40516
				public static LocString AUTOSAVE_FREQUENCY_DESCRIPTION = "Every: {0} cycle(s)";

				// Token: 0x04009E45 RID: 40517
				public static LocString AUTOSAVE_NEVER = "Never";

				// Token: 0x04009E46 RID: 40518
				public static LocString TIMELAPSE_RESOLUTION = "Timelapse resolution:";

				// Token: 0x04009E47 RID: 40519
				public static LocString TIMELAPSE_RESOLUTION_DESCRIPTION = "{0}x{1}";

				// Token: 0x04009E48 RID: 40520
				public static LocString TIMELAPSE_DISABLED_DESCRIPTION = "Disabled";
			}

			// Token: 0x02002434 RID: 9268
			public class FEEDBACK_SCREEN
			{
				// Token: 0x04009E49 RID: 40521
				public static LocString TITLE = "FEEDBACK";

				// Token: 0x04009E4A RID: 40522
				public static LocString HEADER = "We would love to hear from you!";

				// Token: 0x04009E4B RID: 40523
				public static LocString DESCRIPTION = "Let us know if you encounter any problems or how we can improve your Oxygen Not Included experience.\n\nWhen reporting a bug, please include your log and colony save file. The buttons to the right will help you find those files on your local drive.\n\nThank you for being part of the Oxygen Not Included community!";

				// Token: 0x04009E4C RID: 40524
				public static LocString ALT_DESCRIPTION = "Let us know if you encounter any problems or how we can improve your Oxygen Not Included experience.\n\nWhen reporting a bug, please include your log and colony save file.\n\nThank you for being part of the Oxygen Not Included community!";

				// Token: 0x04009E4D RID: 40525
				public static LocString BUG_FORUMS_BUTTON = "Report a Bug";

				// Token: 0x04009E4E RID: 40526
				public static LocString SUGGESTION_FORUMS_BUTTON = "Suggestions Forum";

				// Token: 0x04009E4F RID: 40527
				public static LocString LOGS_DIRECTORY_BUTTON = "Browse Log Files";

				// Token: 0x04009E50 RID: 40528
				public static LocString SAVE_FILES_DIRECTORY_BUTTON = "Browse Save Files";
			}

			// Token: 0x02002435 RID: 9269
			public class WORLD_GEN_OPTIONS_SCREEN
			{
				// Token: 0x04009E51 RID: 40529
				public static LocString TITLE = "WORLD GENERATION OPTIONS";

				// Token: 0x04009E52 RID: 40530
				public static LocString USE_SEED = "Set World Gen Seed";

				// Token: 0x04009E53 RID: 40531
				public static LocString DONE_BUTTON = "Done";

				// Token: 0x04009E54 RID: 40532
				public static LocString RANDOM_BUTTON = "Randomize";

				// Token: 0x04009E55 RID: 40533
				public static LocString RANDOM_BUTTON_TOOLTIP = "Randomize a new world gen seed";

				// Token: 0x04009E56 RID: 40534
				public static LocString TOOLTIP = "This will override the current world gen seed";
			}

			// Token: 0x02002436 RID: 9270
			public class METRICS_OPTIONS_SCREEN
			{
				// Token: 0x04009E57 RID: 40535
				public static LocString TITLE = "DATA COMMUNICATION OPTIONS";

				// Token: 0x04009E58 RID: 40536
				public static LocString ENABLE_BUTTON = "Enable Data Communication";

				// Token: 0x04009E59 RID: 40537
				public static LocString DESCRIPTION = "Collecting user data helps us improve the game.\n\nPlayers who opt out of data communication will no longer send us crash reports and play data.\n\nThey will also be unable to receive new item unlocks from our servers, though existing unlocked items will continue to function.\n\nFor more details on our privacy policy and how we use the data we collect, please visit our <color=#ECA6C9><u><b>privacy center</b></u></color>.";

				// Token: 0x04009E5A RID: 40538
				public static LocString DONE_BUTTON = "Done";

				// Token: 0x04009E5B RID: 40539
				public static LocString RESTART_BUTTON = "Restart Game";

				// Token: 0x04009E5C RID: 40540
				public static LocString TOOLTIP = "Uncheck to disable data communication";

				// Token: 0x04009E5D RID: 40541
				public static LocString RESTART_WARNING = "A game restart is required to apply settings.";
			}

			// Token: 0x02002437 RID: 9271
			public class UNIT_OPTIONS_SCREEN
			{
				// Token: 0x04009E5E RID: 40542
				public static LocString TITLE = "TEMPERATURE UNITS";

				// Token: 0x04009E5F RID: 40543
				public static LocString CELSIUS = "Celsius";

				// Token: 0x04009E60 RID: 40544
				public static LocString CELSIUS_TOOLTIP = "Change temperature unit to Celsius (°C)";

				// Token: 0x04009E61 RID: 40545
				public static LocString KELVIN = "Kelvin";

				// Token: 0x04009E62 RID: 40546
				public static LocString KELVIN_TOOLTIP = "Change temperature unit to Kelvin (K)";

				// Token: 0x04009E63 RID: 40547
				public static LocString FAHRENHEIT = "Fahrenheit";

				// Token: 0x04009E64 RID: 40548
				public static LocString FAHRENHEIT_TOOLTIP = "Change temperature unit to Fahrenheit (°F)";
			}

			// Token: 0x02002438 RID: 9272
			public class GRAPHICS_OPTIONS_SCREEN
			{
				// Token: 0x04009E65 RID: 40549
				public static LocString TITLE = "GRAPHICS OPTIONS";

				// Token: 0x04009E66 RID: 40550
				public static LocString FULLSCREEN = "Fullscreen";

				// Token: 0x04009E67 RID: 40551
				public static LocString RESOLUTION = "Resolution:";

				// Token: 0x04009E68 RID: 40552
				public static LocString LOWRES = "Low Resolution Textures";

				// Token: 0x04009E69 RID: 40553
				public static LocString APPLYBUTTON = "Apply";

				// Token: 0x04009E6A RID: 40554
				public static LocString REVERTBUTTON = "Revert";

				// Token: 0x04009E6B RID: 40555
				public static LocString DONE_BUTTON = "Done";

				// Token: 0x04009E6C RID: 40556
				public static LocString UI_SCALE = "UI Scale";

				// Token: 0x04009E6D RID: 40557
				public static LocString HEADER_DISPLAY = "DISPLAY";

				// Token: 0x04009E6E RID: 40558
				public static LocString HEADER_UI = "INTERFACE";

				// Token: 0x04009E6F RID: 40559
				public static LocString COLORMODE = "Color Mode:";

				// Token: 0x04009E70 RID: 40560
				public static LocString COLOR_MODE_DEFAULT = "Default";

				// Token: 0x04009E71 RID: 40561
				public static LocString COLOR_MODE_PROTANOPIA = "Protanopia";

				// Token: 0x04009E72 RID: 40562
				public static LocString COLOR_MODE_DEUTERANOPIA = "Deuteranopia";

				// Token: 0x04009E73 RID: 40563
				public static LocString COLOR_MODE_TRITANOPIA = "Tritanopia";

				// Token: 0x04009E74 RID: 40564
				public static LocString ACCEPT_CHANGES = "Accept Changes?";

				// Token: 0x04009E75 RID: 40565
				public static LocString ACCEPT_CHANGES_STRING_COLOR = "Interface changes will be visible immediately, but applying color changes to in-game text will require a restart.\n\nAccept Changes?";

				// Token: 0x04009E76 RID: 40566
				public static LocString COLORBLIND_FEEDBACK = "Color blindness options are currently in progress.\n\nIf you would benefit from an alternative color mode or have had difficulties with any of the default colors, please visit the forums and let us know about your experiences.\n\nYour feedback is extremely helpful to us!";

				// Token: 0x04009E77 RID: 40567
				public static LocString COLORBLIND_FEEDBACK_BUTTON = "Provide Feedback";
			}

			// Token: 0x02002439 RID: 9273
			public class WORLDGENSCREEN
			{
				// Token: 0x04009E78 RID: 40568
				public static LocString TITLE = "NEW GAME";

				// Token: 0x04009E79 RID: 40569
				public static LocString GENERATINGWORLD = "GENERATING WORLD";

				// Token: 0x04009E7A RID: 40570
				public static LocString SELECTSIZEPROMPT = "A new world is about to be created. Please select its size.";

				// Token: 0x04009E7B RID: 40571
				public static LocString LOADINGGAME = "LOADING WORLD...";

				// Token: 0x02002E99 RID: 11929
				public class SIZES
				{
					// Token: 0x0400BC3D RID: 48189
					public static LocString TINY = "Tiny";

					// Token: 0x0400BC3E RID: 48190
					public static LocString SMALL = "Small";

					// Token: 0x0400BC3F RID: 48191
					public static LocString STANDARD = "Standard";

					// Token: 0x0400BC40 RID: 48192
					public static LocString LARGE = "Big";

					// Token: 0x0400BC41 RID: 48193
					public static LocString HUGE = "Colossal";
				}
			}

			// Token: 0x0200243A RID: 9274
			public class MINSPECSCREEN
			{
				// Token: 0x04009E7C RID: 40572
				public static LocString TITLE = "WARNING!";

				// Token: 0x04009E7D RID: 40573
				public static LocString SIMFAILEDTOLOAD = "A problem occurred loading Oxygen Not Included. This is usually caused by the Visual Studio C++ 2015 runtime being improperly installed on the system. Please exit the game, run Windows Update, and try re-launching Oxygen Not Included.";

				// Token: 0x04009E7E RID: 40574
				public static LocString BODY = "We've detected that this computer does not meet the minimum requirements to run Oxygen Not Included. While you may continue with your current specs, the game might not run smoothly for you.\n\nPlease be aware that your experience may suffer as a result.";

				// Token: 0x04009E7F RID: 40575
				public static LocString OKBUTTON = "Okay, thanks!";

				// Token: 0x04009E80 RID: 40576
				public static LocString QUITBUTTON = "Quit";
			}

			// Token: 0x0200243B RID: 9275
			public class SUPPORTWARNINGS
			{
				// Token: 0x04009E81 RID: 40577
				public static LocString AUDIO_DRIVERS = "A problem occurred initializing your audio device.\nSorry about that!\n\nThis is usually caused by outdated audio drivers.\n\nPlease visit your audio device manufacturer's website to download the latest drivers.";

				// Token: 0x04009E82 RID: 40578
				public static LocString AUDIO_DRIVERS_MORE_INFO = "More Info";

				// Token: 0x04009E83 RID: 40579
				public static LocString DUPLICATE_KEY_BINDINGS = "<b>Duplicate key bindings were detected.\nThis may be because your custom key bindings conflicted with a new feature's default key.\nPlease visit the controls screen to ensure your key bindings are set how you like them.</b>\n{0}";

				// Token: 0x04009E84 RID: 40580
				public static LocString SAVE_DIRECTORY_READ_ONLY = "A problem occurred while accessing your save directory.\nThis may be because your directory is set to read-only.\n\nPlease ensure your save directory is readable as well as writable and re-launch the game.\n{0}";

				// Token: 0x04009E85 RID: 40581
				public static LocString SAVE_DIRECTORY_INSUFFICIENT_SPACE = "There is insufficient disk space to write to your save directory.\n\nPlease free at least 15 MB to give your saves some room to breathe.\n{0}";

				// Token: 0x04009E86 RID: 40582
				public static LocString WORLD_GEN_FILES = "A problem occurred while accessing certain game files that will prevent starting new games.\n\nPlease ensure that the directory and files are readable as well as writable and re-launch the game:\n\n{0}";

				// Token: 0x04009E87 RID: 40583
				public static LocString WORLD_GEN_FAILURE = "A problem occurred while generating a world from this seed:\n{0}.\n\nUnfortunately, not all seeds germinate. Please try again with a different seed.";

				// Token: 0x04009E88 RID: 40584
				public static LocString WORLD_GEN_FAILURE_STORY = "A problem occurred while generating a world from this seed:\n{0}.\n\nNot all story traits were able to be placed. Please try again with a different seed or fewer story traits.";

				// Token: 0x04009E89 RID: 40585
				public static LocString PLAYER_PREFS_CORRUPTED = "A problem occurred while loading your game options.\nThey have been reset to their default settings.\n\n";

				// Token: 0x04009E8A RID: 40586
				public static LocString IO_UNAUTHORIZED = "An Unauthorized Access Error occurred when trying to write to disk.\nPlease check that you have permissions to write to:\n{0}\n\nThis may prevent the game from saving.";

				// Token: 0x04009E8B RID: 40587
				public static LocString IO_SUFFICIENT_SPACE = "An Insufficient Space Error occurred when trying to write to disk. \n\nPlease free up some space.\n{0}";

				// Token: 0x04009E8C RID: 40588
				public static LocString IO_UNKNOWN = "An unknown error occurred when trying to write or access a file.\n{0}";

				// Token: 0x04009E8D RID: 40589
				public static LocString MORE_INFO_BUTTON = "More Info";
			}

			// Token: 0x0200243C RID: 9276
			public class SAVEUPGRADEWARNINGS
			{
				// Token: 0x04009E8E RID: 40590
				public static LocString SUDDENMORALEHELPER_TITLE = "MORALE CHANGES";

				// Token: 0x04009E8F RID: 40591
				public static LocString SUDDENMORALEHELPER = "Welcome to the Expressive Upgrade! This update introduces a new Morale system that replaces Food and Decor Expectations that were found in previous versions of the game.\n\nThe game you are trying to load was created before this system was introduced, and will need to be updated. You may either:\n\n\n1) Enable the new Morale system in this save, removing Food and Decor Expectations. It's possible that when you load your save your old colony won't meet your Duplicants' new Morale needs, so they'll receive a 5 cycle Morale boost to give you time to adjust.\n\n2) Disable Morale in this save. The new Morale mechanics will still be visible, but won't affect your Duplicants' stress. Food and Decor expectations will no longer exist in this save.";

				// Token: 0x04009E90 RID: 40592
				public static LocString SUDDENMORALEHELPER_BUFF = "1) Bring on Morale!";

				// Token: 0x04009E91 RID: 40593
				public static LocString SUDDENMORALEHELPER_DISABLE = "2) Disable Morale";

				// Token: 0x04009E92 RID: 40594
				public static LocString NEWAUTOMATIONWARNING_TITLE = "AUTOMATION CHANGES";

				// Token: 0x04009E93 RID: 40595
				public static LocString NEWAUTOMATIONWARNING = "The following buildings have acquired new automation ports!\n\nTake a moment to check whether these buildings in your colony are now unintentionally connected to existing " + BUILDINGS.PREFABS.LOGICWIRE.NAME + "s.";

				// Token: 0x04009E94 RID: 40596
				public static LocString MERGEDOWNCHANGES_TITLE = "BREATH OF FRESH AIR UPDATE CHANGES";

				// Token: 0x04009E95 RID: 40597
				public static LocString MERGEDOWNCHANGES = "Oxygen Not Included has had a <b>major update</b> since this save file was created! In addition to the <b>multitude of bug fixes and quality-of-life features</b>, please pay attention to these changes which may affect your existing colony:";

				// Token: 0x04009E96 RID: 40598
				public static LocString MERGEDOWNCHANGES_FOOD = "•<indent=20px>Fridges are more effective for early-game food storage</indent>\n•<indent=20px><b>Both</b> freezing temperatures and a sterile gas are needed for <b>total food preservation</b>.</indent>";

				// Token: 0x04009E97 RID: 40599
				public static LocString MERGEDOWNCHANGES_AIRFILTER = "•<indent=20px>" + BUILDINGS.PREFABS.AIRFILTER.NAME + " now requires <b>5w Power</b>.</indent>\n•<indent=20px>Duplicants will get <b>Stinging Eyes</b> from gasses such as chlorine and hydrogen.</indent>";

				// Token: 0x04009E98 RID: 40600
				public static LocString MERGEDOWNCHANGES_SIMULATION = "•<indent=20px>Many <b>simulation bugs</b> have been fixed.</indent>\n•<indent=20px>This may <b>change the effectiveness</b> of certain contraptions and " + BUILDINGS.PREFABS.STEAMTURBINE2.NAME + " setups.</indent>";

				// Token: 0x04009E99 RID: 40601
				public static LocString MERGEDOWNCHANGES_BUILDINGS = "•<indent=20px>The <b>" + BUILDINGS.PREFABS.OXYGENMASKSTATION.NAME + "</b> has been added to aid early-game exploration.</indent>\n•<indent=20px>Use the new <b>Meter Valves</b> for precise control of resources in pipes.</indent>";
			}
		}

		// Token: 0x02001C83 RID: 7299
		public class SANDBOX_TOGGLE
		{
			// Token: 0x04008011 RID: 32785
			public static LocString TOOLTIP_LOCKED = "<b>Sandbox Mode</b> must be unlocked in the options menu before it can be used. {Hotkey}";

			// Token: 0x04008012 RID: 32786
			public static LocString TOOLTIP_UNLOCKED = "Toggle <b>Sandbox Mode</b> {Hotkey}";
		}

		// Token: 0x02001C84 RID: 7300
		public class SKILLS_SCREEN
		{
			// Token: 0x04008013 RID: 32787
			public static LocString CURRENT_MORALE = "Current Morale: {0}\nMorale Need: {1}";

			// Token: 0x04008014 RID: 32788
			public static LocString SORT_BY_DUPLICANT = "Duplicants";

			// Token: 0x04008015 RID: 32789
			public static LocString SORT_BY_MORALE = "Morale";

			// Token: 0x04008016 RID: 32790
			public static LocString SORT_BY_EXPERIENCE = "Skill Points";

			// Token: 0x04008017 RID: 32791
			public static LocString SORT_BY_SKILL_AVAILABLE = "Skill Points";

			// Token: 0x04008018 RID: 32792
			public static LocString SORT_BY_HAT = "Hat";

			// Token: 0x04008019 RID: 32793
			public static LocString SELECT_HAT = "<b>SELECT HAT</b>";

			// Token: 0x0400801A RID: 32794
			public static LocString POINTS_AVAILABLE = "<b>SKILL POINTS AVAILABLE</b>";

			// Token: 0x0400801B RID: 32795
			public static LocString MORALE = "<b>Morale</b>";

			// Token: 0x0400801C RID: 32796
			public static LocString MORALE_EXPECTATION = "<b>Morale Need</b>";

			// Token: 0x0400801D RID: 32797
			public static LocString EXPERIENCE = "EXPERIENCE TO NEXT LEVEL";

			// Token: 0x0400801E RID: 32798
			public static LocString EXPERIENCE_TOOLTIP = "{0}exp to next Skill Point";

			// Token: 0x0400801F RID: 32799
			public static LocString NOT_AVAILABLE = "Not available";

			// Token: 0x0200243D RID: 9277
			public class ASSIGNMENT_REQUIREMENTS
			{
				// Token: 0x04009E9A RID: 40602
				public static LocString EXPECTATION_TARGET_SKILL = "Current Morale: {0}\nSkill Morale Needs: {1}";

				// Token: 0x04009E9B RID: 40603
				public static LocString EXPECTATION_ALERT_TARGET_SKILL = "{2}'s Current: {0} Morale\n{3} Minimum Morale: {1}";

				// Token: 0x04009E9C RID: 40604
				public static LocString EXPECTATION_ALERT_DESC_EXPECTATION = "This Duplicant's Morale is too low to handle the rigors of this position, which will cause them Stress over time.";

				// Token: 0x02002E9A RID: 11930
				public class SKILLGROUP_ENABLED
				{
					// Token: 0x0400BC42 RID: 48194
					public static LocString NAME = "Can perform {0}";

					// Token: 0x0400BC43 RID: 48195
					public static LocString DESCRIPTION = "Capable of performing <b>{0}</b> skills";
				}

				// Token: 0x02002E9B RID: 11931
				public class MASTERY
				{
					// Token: 0x0400BC44 RID: 48196
					public static LocString CAN_MASTER = "{0} <b>can learn</b> {1}";

					// Token: 0x0400BC45 RID: 48197
					public static LocString HAS_MASTERED = "{0} has <b>already learned</b> {1}";

					// Token: 0x0400BC46 RID: 48198
					public static LocString CANNOT_MASTER = "{0} <b>cannot learn</b> {1}";

					// Token: 0x0400BC47 RID: 48199
					public static LocString STRESS_WARNING_MESSAGE = string.Concat(new string[]
					{
						"Learning {0} will put {1} into a ",
						UI.PRE_KEYWORD,
						"Morale",
						UI.PST_KEYWORD,
						" deficit and cause unnecessary ",
						UI.PRE_KEYWORD,
						"Stress",
						UI.PST_KEYWORD,
						"!"
					});

					// Token: 0x0400BC48 RID: 48200
					public static LocString REQUIRES_MORE_SKILL_POINTS = "    • Not enough " + UI.PRE_KEYWORD + "Skill Points" + UI.PST_KEYWORD;

					// Token: 0x0400BC49 RID: 48201
					public static LocString REQUIRES_PREVIOUS_SKILLS = "    • Missing prerequisite " + UI.PRE_KEYWORD + "Skill" + UI.PST_KEYWORD;

					// Token: 0x0400BC4A RID: 48202
					public static LocString PREVENTED_BY_TRAIT = string.Concat(new string[]
					{
						"    • This Duplicant possesses the ",
						UI.PRE_KEYWORD,
						"{0}",
						UI.PST_KEYWORD,
						" Trait and cannot learn this Skill"
					});

					// Token: 0x0400BC4B RID: 48203
					public static LocString SKILL_APTITUDE = string.Concat(new string[]
					{
						"{0} is interested in {1} and will receive a ",
						UI.PRE_KEYWORD,
						"Morale",
						UI.PST_KEYWORD,
						" bonus for learning it!"
					});

					// Token: 0x0400BC4C RID: 48204
					public static LocString SKILL_GRANTED = "{0} has been granted {1} by a Trait, but does not have increased " + UI.FormatAsKeyWord("Morale Requirements") + " from learning it";
				}
			}
		}

		// Token: 0x02001C85 RID: 7301
		public class KLEI_INVENTORY_SCREEN
		{
			// Token: 0x04008020 RID: 32800
			public static LocString OPEN_INVENTORY_BUTTON = "Open Klei Inventory";

			// Token: 0x04008021 RID: 32801
			public static LocString ITEM_FACADE_FOR = "This blueprint works with any {ConfigProperName}.";

			// Token: 0x04008022 RID: 32802
			public static LocString ARTABLE_ITEM_FACADE_FOR = "This blueprint works with any {ConfigProperName} of {ArtableQuality} quality.";

			// Token: 0x04008023 RID: 32803
			public static LocString CLOTHING_ITEM_FACADE_FOR = "This blueprint can be used in any outfit.";

			// Token: 0x04008024 RID: 32804
			public static LocString BALLOON_ARTIST_FACADE_FOR = "This blueprint can be used by any Balloon Artist.";

			// Token: 0x04008025 RID: 32805
			public static LocString ITEM_RARITY_DETAILS = "{RarityName} quality.";

			// Token: 0x04008026 RID: 32806
			public static LocString ITEM_PLAYER_OWNED_AMOUNT = "My colony has {OwnedCount} of these blueprints.";

			// Token: 0x04008027 RID: 32807
			public static LocString ITEM_PLAYER_OWN_NONE = "My colony doesn't have any of these yet.";

			// Token: 0x04008028 RID: 32808
			public static LocString ITEM_PLAYER_OWNED_AMOUNT_ICON = "x{OwnedCount}";

			// Token: 0x04008029 RID: 32809
			public static LocString ITEM_PLAYER_UNLOCKED_BUT_UNOWNABLE = "This blueprint is part of my colony's permanent collection.";

			// Token: 0x0400802A RID: 32810
			public static LocString ITEM_UNKNOWN_NAME = "Uh oh!";

			// Token: 0x0400802B RID: 32811
			public static LocString ITEM_UNKNOWN_DESCRIPTION = "Hmm. Looks like this blueprint is missing from the supply closet. Perhaps due to a temporal anomaly...";

			// Token: 0x0200243E RID: 9278
			public static class CATEGORIES
			{
				// Token: 0x04009E9D RID: 40605
				public static LocString EQUIPMENT = "Equipment";

				// Token: 0x04009E9E RID: 40606
				public static LocString DUPE_TOPS = "Tops & Onesies";

				// Token: 0x04009E9F RID: 40607
				public static LocString DUPE_BOTTOMS = "Bottoms";

				// Token: 0x04009EA0 RID: 40608
				public static LocString DUPE_GLOVES = "Gloves";

				// Token: 0x04009EA1 RID: 40609
				public static LocString DUPE_SHOES = "Footwear";

				// Token: 0x04009EA2 RID: 40610
				public static LocString DUPE_HATS = "Headgear";

				// Token: 0x04009EA3 RID: 40611
				public static LocString DUPE_ACCESSORIES = "Accessories";

				// Token: 0x04009EA4 RID: 40612
				public static LocString PRIMOGARB = "Primo Garb";

				// Token: 0x04009EA5 RID: 40613
				public static LocString ATMOSUITS = "Atmo Suits";

				// Token: 0x04009EA6 RID: 40614
				public static LocString BUILDINGS = "Buildings";

				// Token: 0x04009EA7 RID: 40615
				public static LocString CRITTERS = "Critters";

				// Token: 0x04009EA8 RID: 40616
				public static LocString SWEEPYS = "Sweepys";

				// Token: 0x04009EA9 RID: 40617
				public static LocString DUPLICANTS = "Duplicants";

				// Token: 0x04009EAA RID: 40618
				public static LocString ARTWORKS = "Artwork";

				// Token: 0x04009EAB RID: 40619
				public static LocString MONUMENTPARTS = "Monuments";

				// Token: 0x04009EAC RID: 40620
				public static LocString JOY_RESPONSE = "Overjoyed Responses";

				// Token: 0x02002E9C RID: 11932
				public static class JOY_RESPONSES
				{
					// Token: 0x0400BC4D RID: 48205
					public static LocString BALLOON_ARTIST = "Balloon Artist";
				}
			}

			// Token: 0x0200243F RID: 9279
			public static class COLUMN_HEADERS
			{
				// Token: 0x04009EAD RID: 40621
				public static LocString CATEGORY_HEADER = "BLUEPRINTS";

				// Token: 0x04009EAE RID: 40622
				public static LocString ITEMS_HEADER = "Items";

				// Token: 0x04009EAF RID: 40623
				public static LocString DETAILS_HEADER = "Details";
			}
		}

		// Token: 0x02001C86 RID: 7302
		public class ITEM_DROP_SCREEN
		{
			// Token: 0x0400802C RID: 32812
			public static LocString THANKS_FOR_PLAYING = "Thanks for keeping this colony alive!";

			// Token: 0x02002440 RID: 9280
			public static class ACTIONS
			{
				// Token: 0x04009EB0 RID: 40624
				public static LocString ACCEPT_ITEM = "Print Gift";
			}
		}

		// Token: 0x02001C87 RID: 7303
		public class OUTFIT_BROWSER_SCREEN
		{
			// Token: 0x0400802D RID: 32813
			public static LocString BUTTON_ADD_OUTFIT = "New Outfit";

			// Token: 0x0400802E RID: 32814
			public static LocString BUTTON_PICK_OUTFIT = "Assign Outfit";

			// Token: 0x0400802F RID: 32815
			public static LocString TOOLTIP_PICK_OUTFIT_ERROR_LOCKED = "Cannot assign this outfit to {MinionName} because my colony doesn't have all of these blueprints yet";

			// Token: 0x04008030 RID: 32816
			public static LocString BUTTON_EDIT_OUTFIT = "Restyle Outfit";

			// Token: 0x04008031 RID: 32817
			public static LocString BUTTON_COPY_OUTFIT = "Copy Outfit";

			// Token: 0x04008032 RID: 32818
			public static LocString TOOLTIP_DELETE_OUTFIT = "Delete Outfit";

			// Token: 0x04008033 RID: 32819
			public static LocString TOOLTIP_DELETE_OUTFIT_ERROR_READONLY = "This outfit cannot be deleted";

			// Token: 0x04008034 RID: 32820
			public static LocString TOOLTIP_RENAME_OUTFIT = "Rename Outfit";

			// Token: 0x04008035 RID: 32821
			public static LocString TOOLTIP_RENAME_OUTFIT_ERROR_READONLY = "This outfit cannot be renamed";

			// Token: 0x02002441 RID: 9281
			public static class COLUMN_HEADERS
			{
				// Token: 0x04009EB1 RID: 40625
				public static LocString GALLERY_HEADER = "OUTFITS";

				// Token: 0x04009EB2 RID: 40626
				public static LocString MINION_GALLERY_HEADER = "WARDROBE";

				// Token: 0x04009EB3 RID: 40627
				public static LocString DETAILS_HEADER = "Preview";
			}

			// Token: 0x02002442 RID: 9282
			public class DELETE_WARNING_POPUP
			{
				// Token: 0x04009EB4 RID: 40628
				public static LocString HEADER = "Delete \"{OutfitName}\"?";

				// Token: 0x04009EB5 RID: 40629
				public static LocString BODY = "Are you sure you want to delete \"{OutfitName}\"?\n\nAny Duplicants assigned to wear this outfit on spawn will be printed wearing their default outfit instead. Existing Duplicants in saved games won't be affected.\n\nThis <b>cannot</b> be undone.";

				// Token: 0x04009EB6 RID: 40630
				public static LocString BUTTON_YES_DELETE = "Yes, delete outfit";

				// Token: 0x04009EB7 RID: 40631
				public static LocString BUTTON_DONT_DELETE = "Cancel";
			}

			// Token: 0x02002443 RID: 9283
			public class RENAME_POPUP
			{
				// Token: 0x04009EB8 RID: 40632
				public static LocString HEADER = "RENAME OUTFIT";
			}
		}

		// Token: 0x02001C88 RID: 7304
		public class LOCKER_MENU
		{
			// Token: 0x04008036 RID: 32822
			public static LocString TITLE = "SUPPLY CLOSET";

			// Token: 0x04008037 RID: 32823
			public static LocString BUTTON_INVENTORY = "All";

			// Token: 0x04008038 RID: 32824
			public static LocString BUTTON_INVENTORY_DESCRIPTION = "View all of my colony's blueprints";

			// Token: 0x04008039 RID: 32825
			public static LocString BUTTON_DUPLICANTS = "Duplicants";

			// Token: 0x0400803A RID: 32826
			public static LocString BUTTON_DUPLICANTS_DESCRIPTION = "Manage individual Duplicants' outfits";

			// Token: 0x0400803B RID: 32827
			public static LocString BUTTON_OUTFITS = "Wardrobe";

			// Token: 0x0400803C RID: 32828
			public static LocString BUTTON_OUTFITS_DESCRIPTION = "Manage my colony's collection of outfits";

			// Token: 0x0400803D RID: 32829
			public static LocString DEFAULT_DESCRIPTION = "Select a screen";

			// Token: 0x0400803E RID: 32830
			public static LocString BUTTON_CLAIM = "Check Shipments";

			// Token: 0x0400803F RID: 32831
			public static LocString BUTTON_CLAIM_DESCRIPTION = "Check for available blueprints on the Klei Rewards website";

			// Token: 0x04008040 RID: 32832
			public static LocString UNOPENED_ITEMS_TOOLTIP = "You may have blueprints available to claim on the Klei Rewards website";
		}

		// Token: 0x02001C89 RID: 7305
		public class LOCKER_NAVIGATOR
		{
			// Token: 0x04008041 RID: 32833
			public static LocString BUTTON_BACK = "BACK";

			// Token: 0x04008042 RID: 32834
			public static LocString BUTTON_CLOSE = "CLOSE";

			// Token: 0x02002444 RID: 9284
			public class DATA_COLLECTION_WARNING_POPUP
			{
				// Token: 0x04009EB9 RID: 40633
				public static LocString HEADER = "Data Communication is Disabled";

				// Token: 0x04009EBA RID: 40634
				public static LocString BODY = "Data Communication must be enabled in order to access newly unlocked items. This setting can be found in the Options menu.\n\nExisting item unlocks can still be used while Data Communication is disabled.";

				// Token: 0x04009EBB RID: 40635
				public static LocString BUTTON_OK = "Continue";

				// Token: 0x04009EBC RID: 40636
				public static LocString BUTTON_OPEN_SETTINGS = "Options Menu";
			}
		}

		// Token: 0x02001C8A RID: 7306
		public class JOY_RESPONSE_DESIGNER_SCREEN
		{
			// Token: 0x04008043 RID: 32835
			public static LocString CATEGORY_HEADER = "OVERJOYED RESPONSES";

			// Token: 0x04008044 RID: 32836
			public static LocString BUTTON_APPLY_TO_MINION = "Assign to {MinionName}";

			// Token: 0x04008045 RID: 32837
			public static LocString TOOLTIP_NO_FACADES_FOR_JOY_TRAIT = "There aren't any blueprints for {JoyResponseType} Duplicants yet";

			// Token: 0x04008046 RID: 32838
			public static LocString TOOLTIP_PICK_JOY_RESPONSE_ERROR_LOCKED = "Cannot assign this overjoyed response to {MinionName} because my colony doesn't have it's blueprint yet";

			// Token: 0x02002445 RID: 9285
			public class CHANGES_NOT_SAVED_WARNING_POPUP
			{
				// Token: 0x04009EBD RID: 40637
				public static LocString HEADER = "Discard changes to {MinionName}'s overjoyed response?";
			}
		}

		// Token: 0x02001C8B RID: 7307
		public class OUTFIT_DESIGNER_SCREEN
		{
			// Token: 0x04008047 RID: 32839
			public static LocString CATEGORY_HEADER = "CLOTHING";

			// Token: 0x02002446 RID: 9286
			public class MINION_INSTANCE
			{
				// Token: 0x04009EBE RID: 40638
				public static LocString BUTTON_APPLY_TO_MINION = "Assign to {MinionName}";

				// Token: 0x04009EBF RID: 40639
				public static LocString BUTTON_APPLY_TO_TEMPLATE = "Apply to Template";

				// Token: 0x02002E9D RID: 11933
				public class APPLY_TEMPLATE_POPUP
				{
					// Token: 0x0400BC4E RID: 48206
					public static LocString HEADER = "SAVE AS TEMPLATE";

					// Token: 0x0400BC4F RID: 48207
					public static LocString DESC_SAVE_EXISTING = "\"{OutfitName}\" will be updated and applied to {MinionName} on save.";

					// Token: 0x0400BC50 RID: 48208
					public static LocString DESC_SAVE_NEW = "A new outfit named \"{OutfitName}\" will be created and assigned to {MinionName} on save.";

					// Token: 0x0400BC51 RID: 48209
					public static LocString BUTTON_SAVE_EXISTING = "Update Outfit";

					// Token: 0x0400BC52 RID: 48210
					public static LocString BUTTON_SAVE_NEW = "Save New Outfit";
				}
			}

			// Token: 0x02002447 RID: 9287
			public class OUTFIT_TEMPLATE
			{
				// Token: 0x04009EC0 RID: 40640
				public static LocString BUTTON_SAVE = "Save Template";

				// Token: 0x04009EC1 RID: 40641
				public static LocString BUTTON_COPY = "Save a Copy";

				// Token: 0x04009EC2 RID: 40642
				public static LocString TOOLTIP_SAVE_ERROR_LOCKED = "Cannot save this outfit because my colony doesn't have all of its blueprints yet";

				// Token: 0x04009EC3 RID: 40643
				public static LocString TOOLTIP_SAVE_ERROR_READONLY = "This wardrobe staple cannot be altered\n\nMake a copy to save your changes";
			}

			// Token: 0x02002448 RID: 9288
			public class CHANGES_NOT_SAVED_WARNING_POPUP
			{
				// Token: 0x04009EC4 RID: 40644
				public static LocString HEADER = "Discard changes to \"{OutfitName}\"?";

				// Token: 0x04009EC5 RID: 40645
				public static LocString BODY = "There are unsaved changes which will be lost if you exit now.\n\nAre you sure you want to discard your changes?";

				// Token: 0x04009EC6 RID: 40646
				public static LocString BUTTON_DISCARD = "Yes, discard changes";

				// Token: 0x04009EC7 RID: 40647
				public static LocString BUTTON_RETURN = "Cancel";
			}

			// Token: 0x02002449 RID: 9289
			public class COPY_POPUP
			{
				// Token: 0x04009EC8 RID: 40648
				public static LocString HEADER = "RENAME COPY";
			}
		}

		// Token: 0x02001C8C RID: 7308
		public class OUTFIT_NAME
		{
			// Token: 0x04008048 RID: 32840
			public static LocString NEW = "Custom Outfit";

			// Token: 0x04008049 RID: 32841
			public static LocString COPY_OF = "Copy of {OutfitName}";

			// Token: 0x0400804A RID: 32842
			public static LocString RESOLVE_CONFLICT = "{OutfitName} ({ConflictNumber})";

			// Token: 0x0400804B RID: 32843
			public static LocString ERROR_NAME_EXISTS = "There's already an outfit named \"{OutfitName}\"";

			// Token: 0x0400804C RID: 32844
			public static LocString MINIONS_OUTFIT = "{MinionName}'s Current Outfit";

			// Token: 0x0400804D RID: 32845
			public static LocString NONE = "Default Outfit";

			// Token: 0x0400804E RID: 32846
			public static LocString NONE_JOY_RESPONSE = "Default Overjoyed Blueprint";
		}

		// Token: 0x02001C8D RID: 7309
		public class OUTFIT_DESCRIPTION
		{
			// Token: 0x0400804F RID: 32847
			public static LocString CONTAINS_NON_OWNED_ITEMS = "This outfit cannot be worn because my colony doesn't have all of its blueprints yet.";

			// Token: 0x04008050 RID: 32848
			public static LocString NO_DUPE_TOPS = "Default Top";

			// Token: 0x04008051 RID: 32849
			public static LocString NO_DUPE_BOTTOMS = "Default Bottom";

			// Token: 0x04008052 RID: 32850
			public static LocString NO_DUPE_GLOVES = "Default Gloves";

			// Token: 0x04008053 RID: 32851
			public static LocString NO_DUPE_SHOES = "Default Footwear";

			// Token: 0x04008054 RID: 32852
			public static LocString NO_DUPE_HATS = "Default Headgear";

			// Token: 0x04008055 RID: 32853
			public static LocString NO_DUPE_ACCESSORIES = "Default Accessory";

			// Token: 0x04008056 RID: 32854
			public static LocString NO_JOY_RESPONSE = "Default Overjoyed Response";
		}

		// Token: 0x02001C8E RID: 7310
		public class MINION_BROWSER_SCREEN
		{
			// Token: 0x04008057 RID: 32855
			public static LocString CATEGORY_HEADER = "DUPLICANTS";

			// Token: 0x04008058 RID: 32856
			public static LocString BUTTON_CHANGE_OUTFIT = "Open Wardrobe";

			// Token: 0x04008059 RID: 32857
			public static LocString BUTTON_EDIT_OUTFIT_ITEMS = "Restyle Outfit";

			// Token: 0x0400805A RID: 32858
			public static LocString BUTTON_EDIT_JOY_RESPONSE = "Restyle Overjoyed Response";

			// Token: 0x0400805B RID: 32859
			public static LocString OUTFIT_TYPE_CLOTHING = "CLOTHING";

			// Token: 0x0400805C RID: 32860
			public static LocString OUTFIT_TYPE_JOY_RESPONSE = "OVERJOYED RESPONSE";
		}

		// Token: 0x02001C8F RID: 7311
		public class PERMIT_RARITY
		{
			// Token: 0x0400805D RID: 32861
			public static readonly LocString UNKNOWN = "Unknown";

			// Token: 0x0400805E RID: 32862
			public static readonly LocString UNIVERSAL = "Universal";

			// Token: 0x0400805F RID: 32863
			public static readonly LocString LOYALTY = "Loyalty";

			// Token: 0x04008060 RID: 32864
			public static readonly LocString COMMON = "Common";

			// Token: 0x04008061 RID: 32865
			public static readonly LocString DECENT = "Decent";

			// Token: 0x04008062 RID: 32866
			public static readonly LocString NIFTY = "Nifty";

			// Token: 0x04008063 RID: 32867
			public static readonly LocString SPLENDID = "Splendid";
		}

		// Token: 0x02001C90 RID: 7312
		public class OUTFITS
		{
			// Token: 0x0200244A RID: 9290
			public class BASIC_BLACK
			{
				// Token: 0x04009EC9 RID: 40649
				public static LocString NAME = "Basic Black Outfit";
			}

			// Token: 0x0200244B RID: 9291
			public class BASIC_WHITE
			{
				// Token: 0x04009ECA RID: 40650
				public static LocString NAME = "Basic White Outfit";
			}

			// Token: 0x0200244C RID: 9292
			public class BASIC_RED
			{
				// Token: 0x04009ECB RID: 40651
				public static LocString NAME = "Basic Red Outfit";
			}

			// Token: 0x0200244D RID: 9293
			public class BASIC_ORANGE
			{
				// Token: 0x04009ECC RID: 40652
				public static LocString NAME = "Basic Orange Outfit";
			}

			// Token: 0x0200244E RID: 9294
			public class BASIC_YELLOW
			{
				// Token: 0x04009ECD RID: 40653
				public static LocString NAME = "Basic Yellow Outfit";
			}

			// Token: 0x0200244F RID: 9295
			public class BASIC_GREEN
			{
				// Token: 0x04009ECE RID: 40654
				public static LocString NAME = "Basic Green Outfit";
			}

			// Token: 0x02002450 RID: 9296
			public class BASIC_AQUA
			{
				// Token: 0x04009ECF RID: 40655
				public static LocString NAME = "Basic Aqua Outfit";
			}

			// Token: 0x02002451 RID: 9297
			public class BASIC_PURPLE
			{
				// Token: 0x04009ED0 RID: 40656
				public static LocString NAME = "Basic Purple Outfit";
			}

			// Token: 0x02002452 RID: 9298
			public class BASIC_PINK_ORCHID
			{
				// Token: 0x04009ED1 RID: 40657
				public static LocString NAME = "Basic Bubblegum Outfit";
			}

			// Token: 0x02002453 RID: 9299
			public class BASIC_DEEPRED
			{
				// Token: 0x04009ED2 RID: 40658
				public static LocString NAME = "Team Captain Outfit";
			}

			// Token: 0x02002454 RID: 9300
			public class BASIC_BLUE_COBALT
			{
				// Token: 0x04009ED3 RID: 40659
				public static LocString NAME = "True Blue Outfit";
			}

			// Token: 0x02002455 RID: 9301
			public class BASIC_PINK_FLAMINGO
			{
				// Token: 0x04009ED4 RID: 40660
				public static LocString NAME = "Pep Rally Outfit";
			}

			// Token: 0x02002456 RID: 9302
			public class BASIC_GREEN_KELLY
			{
				// Token: 0x04009ED5 RID: 40661
				public static LocString NAME = "Go Team! Outfit";
			}

			// Token: 0x02002457 RID: 9303
			public class BASIC_GREY_CHARCOAL
			{
				// Token: 0x04009ED6 RID: 40662
				public static LocString NAME = "Underdog Outfit";
			}

			// Token: 0x02002458 RID: 9304
			public class BASIC_LEMON
			{
				// Token: 0x04009ED7 RID: 40663
				public static LocString NAME = "Team Hype Outfit";
			}

			// Token: 0x02002459 RID: 9305
			public class BASIC_SATSUMA
			{
				// Token: 0x04009ED8 RID: 40664
				public static LocString NAME = "Superfan Outfit";
			}
		}

		// Token: 0x02001C91 RID: 7313
		public class ROLES_SCREEN
		{
			// Token: 0x04008064 RID: 32868
			public static LocString MANAGEMENT_BUTTON = "JOBS";

			// Token: 0x04008065 RID: 32869
			public static LocString ROLE_PROGRESS = "<b>Job Experience: {0}/{1}</b>\nDuplicants can become eligible for specialized jobs by maxing their current job experience";

			// Token: 0x04008066 RID: 32870
			public static LocString NO_JOB_STATION_WARNING = string.Concat(new string[]
			{
				"Build a ",
				UI.PRE_KEYWORD,
				"Printing Pod",
				UI.PST_KEYWORD,
				" to unlock this menu\n\nThe ",
				UI.PRE_KEYWORD,
				"Printing Pod",
				UI.PST_KEYWORD,
				" can be found in the ",
				UI.FormatAsBuildMenuTab("Base Tab", global::Action.Plan1),
				" of the Build Menu"
			});

			// Token: 0x04008067 RID: 32871
			public static LocString AUTO_PRIORITIZE = "Auto-Prioritize:";

			// Token: 0x04008068 RID: 32872
			public static LocString AUTO_PRIORITIZE_ENABLED = "Duplicant priorities are automatically reconfigured when they are assigned a new job";

			// Token: 0x04008069 RID: 32873
			public static LocString AUTO_PRIORITIZE_DISABLED = "Duplicant priorities can only be changed manually";

			// Token: 0x0400806A RID: 32874
			public static LocString EXPECTATION_ALERT_EXPECTATION = "Current Morale: {0}\nJob Morale Needs: {1}";

			// Token: 0x0400806B RID: 32875
			public static LocString EXPECTATION_ALERT_JOB = "Current Morale: {0}\n{2} Minimum Morale: {1}";

			// Token: 0x0400806C RID: 32876
			public static LocString EXPECTATION_ALERT_TARGET_JOB = "{2}'s Current: {0} Morale\n{3} Minimum Morale: {1}";

			// Token: 0x0400806D RID: 32877
			public static LocString EXPECTATION_ALERT_DESC_EXPECTATION = "This Duplicant's Morale is too low to handle the rigors of this position, which will cause them Stress over time.";

			// Token: 0x0400806E RID: 32878
			public static LocString EXPECTATION_ALERT_DESC_JOB = "This Duplicant's Morale is too low to handle the assigned job, which will cause them Stress over time.";

			// Token: 0x0400806F RID: 32879
			public static LocString EXPECTATION_ALERT_DESC_TARGET_JOB = "This Duplicant's Morale is too low to handle the rigors of this position, which will cause them Stress over time.";

			// Token: 0x04008070 RID: 32880
			public static LocString HIGHEST_EXPECTATIONS_TIER = "<b>Highest Expectations</b>";

			// Token: 0x04008071 RID: 32881
			public static LocString ADDED_EXPECTATIONS_AMOUNT = " (+{0} Expectation)";

			// Token: 0x0200245A RID: 9306
			public class WIDGET
			{
				// Token: 0x04009ED9 RID: 40665
				public static LocString NUMBER_OF_MASTERS_TOOLTIP = "<b>Duplicants who have mastered this job:</b>{0}";

				// Token: 0x04009EDA RID: 40666
				public static LocString NO_MASTERS_TOOLTIP = "<b>No Duplicants have mastered this job</b>";
			}

			// Token: 0x0200245B RID: 9307
			public class TIER_NAMES
			{
				// Token: 0x04009EDB RID: 40667
				public static LocString ZERO = "Tier 0";

				// Token: 0x04009EDC RID: 40668
				public static LocString ONE = "Tier 1";

				// Token: 0x04009EDD RID: 40669
				public static LocString TWO = "Tier 2";

				// Token: 0x04009EDE RID: 40670
				public static LocString THREE = "Tier 3";

				// Token: 0x04009EDF RID: 40671
				public static LocString FOUR = "Tier 4";

				// Token: 0x04009EE0 RID: 40672
				public static LocString FIVE = "Tier 5";

				// Token: 0x04009EE1 RID: 40673
				public static LocString SIX = "Tier 6";

				// Token: 0x04009EE2 RID: 40674
				public static LocString SEVEN = "Tier 7";

				// Token: 0x04009EE3 RID: 40675
				public static LocString EIGHT = "Tier 8";

				// Token: 0x04009EE4 RID: 40676
				public static LocString NINE = "Tier 9";
			}

			// Token: 0x0200245C RID: 9308
			public class SLOTS
			{
				// Token: 0x04009EE5 RID: 40677
				public static LocString UNASSIGNED = "Vacant Position";

				// Token: 0x04009EE6 RID: 40678
				public static LocString UNASSIGNED_TOOLTIP = UI.CLICK(UI.ClickType.Click) + " to assign a Duplicant to this job opening";

				// Token: 0x04009EE7 RID: 40679
				public static LocString NOSLOTS = "No slots available";

				// Token: 0x04009EE8 RID: 40680
				public static LocString NO_ELIGIBLE_DUPLICANTS = "No Duplicants meet the requirements for this job";

				// Token: 0x04009EE9 RID: 40681
				public static LocString ASSIGNMENT_PENDING = "(Pending)";

				// Token: 0x04009EEA RID: 40682
				public static LocString PICK_JOB = "No Job";

				// Token: 0x04009EEB RID: 40683
				public static LocString PICK_DUPLICANT = "None";
			}

			// Token: 0x0200245D RID: 9309
			public class DROPDOWN
			{
				// Token: 0x04009EEC RID: 40684
				public static LocString NAME_AND_ROLE = "{0} <color=#F44A47FF>({1})</color>";

				// Token: 0x04009EED RID: 40685
				public static LocString ALREADY_ROLE = "(Currently {0})";
			}

			// Token: 0x0200245E RID: 9310
			public class SIDEBAR
			{
				// Token: 0x04009EEE RID: 40686
				public static LocString ASSIGNED_DUPLICANTS = "Assigned Duplicants";

				// Token: 0x04009EEF RID: 40687
				public static LocString UNASSIGNED_DUPLICANTS = "Unassigned Duplicants";

				// Token: 0x04009EF0 RID: 40688
				public static LocString UNASSIGN = "Unassign job";
			}

			// Token: 0x0200245F RID: 9311
			public class PRIORITY
			{
				// Token: 0x04009EF1 RID: 40689
				public static LocString TITLE = "Job Priorities";

				// Token: 0x04009EF2 RID: 40690
				public static LocString DESCRIPTION = "{0}s prioritize these work errands: ";

				// Token: 0x04009EF3 RID: 40691
				public static LocString NO_EFFECT = "This job does not affect errand prioritization";
			}

			// Token: 0x02002460 RID: 9312
			public class RESUME
			{
				// Token: 0x04009EF4 RID: 40692
				public static LocString TITLE = "Qualifications";

				// Token: 0x04009EF5 RID: 40693
				public static LocString PREVIOUS_ROLES = "PREVIOUS DUTIES";

				// Token: 0x04009EF6 RID: 40694
				public static LocString UNASSIGNED = "Unassigned";

				// Token: 0x04009EF7 RID: 40695
				public static LocString NO_SELECTION = "No Duplicant selected";
			}

			// Token: 0x02002461 RID: 9313
			public class PERKS
			{
				// Token: 0x04009EF8 RID: 40696
				public static LocString TITLE_BASICTRAINING = "Basic Job Training";

				// Token: 0x04009EF9 RID: 40697
				public static LocString TITLE_MORETRAINING = "Additional Job Training";

				// Token: 0x04009EFA RID: 40698
				public static LocString NO_PERKS = "This job comes with no training";

				// Token: 0x04009EFB RID: 40699
				public static LocString ATTRIBUTE_EFFECT_FMT = "<b>{0}</b> " + UI.PRE_KEYWORD + "{1}" + UI.PST_KEYWORD;

				// Token: 0x02002E9E RID: 11934
				public class CAN_DIG_VERY_FIRM
				{
					// Token: 0x0400BC53 RID: 48211
					public static LocString DESCRIPTION = UI.FormatAsLink(ELEMENTS.HARDNESS.HARDNESS_DESCRIPTOR.VERYFIRM + " Material", "HARDNESS") + " Mining";
				}

				// Token: 0x02002E9F RID: 11935
				public class CAN_DIG_NEARLY_IMPENETRABLE
				{
					// Token: 0x0400BC54 RID: 48212
					public static LocString DESCRIPTION = UI.FormatAsLink("Abyssalite", "KATAIRITE") + " Mining";
				}

				// Token: 0x02002EA0 RID: 11936
				public class CAN_DIG_SUPER_SUPER_HARD
				{
					// Token: 0x0400BC55 RID: 48213
					public static LocString DESCRIPTION = UI.FormatAsLink("Diamond", "DIAMOND") + " and " + UI.FormatAsLink("Obsidian", "OBSIDIAN") + " Mining";
				}

				// Token: 0x02002EA1 RID: 11937
				public class CAN_DIG_RADIOACTIVE_MATERIALS
				{
					// Token: 0x0400BC56 RID: 48214
					public static LocString DESCRIPTION = UI.FormatAsLink("Corium", "CORIUM") + " Mining";
				}

				// Token: 0x02002EA2 RID: 11938
				public class CAN_DIG_UNOBTANIUM
				{
					// Token: 0x0400BC57 RID: 48215
					public static LocString DESCRIPTION = UI.FormatAsLink("Neutronium", "UNOBTANIUM") + " Mining";
				}

				// Token: 0x02002EA3 RID: 11939
				public class CAN_ART
				{
					// Token: 0x0400BC58 RID: 48216
					public static LocString DESCRIPTION = "Can produce artwork using " + BUILDINGS.PREFABS.CANVAS.NAME + " and " + BUILDINGS.PREFABS.SCULPTURE.NAME;
				}

				// Token: 0x02002EA4 RID: 11940
				public class CAN_ART_UGLY
				{
					// Token: 0x0400BC59 RID: 48217
					public static LocString DESCRIPTION = UI.PRE_KEYWORD + "Crude" + UI.PST_KEYWORD + " artwork quality";
				}

				// Token: 0x02002EA5 RID: 11941
				public class CAN_ART_OKAY
				{
					// Token: 0x0400BC5A RID: 48218
					public static LocString DESCRIPTION = UI.PRE_KEYWORD + "Mediocre" + UI.PST_KEYWORD + " artwork quality";
				}

				// Token: 0x02002EA6 RID: 11942
				public class CAN_ART_GREAT
				{
					// Token: 0x0400BC5B RID: 48219
					public static LocString DESCRIPTION = UI.PRE_KEYWORD + "Master" + UI.PST_KEYWORD + " artwork quality";
				}

				// Token: 0x02002EA7 RID: 11943
				public class CAN_FARM_TINKER
				{
					// Token: 0x0400BC5C RID: 48220
					public static LocString DESCRIPTION = UI.FormatAsLink("Crop Tending", "PLANTS") + " and " + ITEMS.INDUSTRIAL_PRODUCTS.FARM_STATION_TOOLS.NAME + " Crafting";
				}

				// Token: 0x02002EA8 RID: 11944
				public class CAN_IDENTIFY_MUTANT_SEEDS
				{
					// Token: 0x0400BC5D RID: 48221
					public static LocString DESCRIPTION = string.Concat(new string[]
					{
						"Can identify ",
						UI.PRE_KEYWORD,
						"Mutant Seeds",
						UI.PST_KEYWORD,
						" at the ",
						BUILDINGS.PREFABS.GENETICANALYSISSTATION.NAME
					});
				}

				// Token: 0x02002EA9 RID: 11945
				public class CAN_WRANGLE_CREATURES
				{
					// Token: 0x0400BC5E RID: 48222
					public static LocString DESCRIPTION = "Critter Wrangling";
				}

				// Token: 0x02002EAA RID: 11946
				public class CAN_USE_RANCH_STATION
				{
					// Token: 0x0400BC5F RID: 48223
					public static LocString DESCRIPTION = "Grooming Station Usage";
				}

				// Token: 0x02002EAB RID: 11947
				public class CAN_POWER_TINKER
				{
					// Token: 0x0400BC60 RID: 48224
					public static LocString DESCRIPTION = UI.FormatAsLink("Generator Tuning", "POWER") + " usage and " + ITEMS.INDUSTRIAL_PRODUCTS.POWER_STATION_TOOLS.NAME + " Crafting";
				}

				// Token: 0x02002EAC RID: 11948
				public class CAN_ELECTRIC_GRILL
				{
					// Token: 0x0400BC61 RID: 48225
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.COOKINGSTATION.NAME + " Usage";
				}

				// Token: 0x02002EAD RID: 11949
				public class CAN_SPICE_GRINDER
				{
					// Token: 0x0400BC62 RID: 48226
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.SPICEGRINDER.NAME + " Usage";
				}

				// Token: 0x02002EAE RID: 11950
				public class ADVANCED_RESEARCH
				{
					// Token: 0x0400BC63 RID: 48227
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.ADVANCEDRESEARCHCENTER.NAME + " Usage";
				}

				// Token: 0x02002EAF RID: 11951
				public class INTERSTELLAR_RESEARCH
				{
					// Token: 0x0400BC64 RID: 48228
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.COSMICRESEARCHCENTER.NAME + " Usage";
				}

				// Token: 0x02002EB0 RID: 11952
				public class NUCLEAR_RESEARCH
				{
					// Token: 0x0400BC65 RID: 48229
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.NUCLEARRESEARCHCENTER.NAME + " Usage";
				}

				// Token: 0x02002EB1 RID: 11953
				public class ORBITAL_RESEARCH
				{
					// Token: 0x0400BC66 RID: 48230
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.DLC1COSMICRESEARCHCENTER.NAME + " Usage";
				}

				// Token: 0x02002EB2 RID: 11954
				public class GEYSER_TUNING
				{
					// Token: 0x0400BC67 RID: 48231
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.GEOTUNER.NAME + " Usage";
				}

				// Token: 0x02002EB3 RID: 11955
				public class CAN_CLOTHING_ALTERATION
				{
					// Token: 0x0400BC68 RID: 48232
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.CLOTHINGALTERATIONSTATION.NAME + " Usage";
				}

				// Token: 0x02002EB4 RID: 11956
				public class CAN_STUDY_WORLD_OBJECTS
				{
					// Token: 0x0400BC69 RID: 48233
					public static LocString DESCRIPTION = "Geographical Analysis";
				}

				// Token: 0x02002EB5 RID: 11957
				public class CAN_STUDY_ARTIFACTS
				{
					// Token: 0x0400BC6A RID: 48234
					public static LocString DESCRIPTION = "Artifact Analysis";
				}

				// Token: 0x02002EB6 RID: 11958
				public class CAN_USE_CLUSTER_TELESCOPE
				{
					// Token: 0x0400BC6B RID: 48235
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.CLUSTERTELESCOPE.NAME + " Usage";
				}

				// Token: 0x02002EB7 RID: 11959
				public class EXOSUIT_EXPERTISE
				{
					// Token: 0x0400BC6C RID: 48236
					public static LocString DESCRIPTION = UI.FormatAsLink("Exosuit", "EXOSUIT") + " Penalty Reduction";
				}

				// Token: 0x02002EB8 RID: 11960
				public class EXOSUIT_DURABILITY
				{
					// Token: 0x0400BC6D RID: 48237
					public static LocString DESCRIPTION = "Slows " + UI.FormatAsLink("Exosuit", "EXOSUIT") + " Durability Damage";
				}

				// Token: 0x02002EB9 RID: 11961
				public class CONVEYOR_BUILD
				{
					// Token: 0x0400BC6E RID: 48238
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.SOLIDCONDUIT.NAME + " Construction";
				}

				// Token: 0x02002EBA RID: 11962
				public class CAN_DO_PLUMBING
				{
					// Token: 0x0400BC6F RID: 48239
					public static LocString DESCRIPTION = "Pipe Emptying";
				}

				// Token: 0x02002EBB RID: 11963
				public class CAN_USE_ROCKETS
				{
					// Token: 0x0400BC70 RID: 48240
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.COMMANDMODULE.NAME + " Usage";
				}

				// Token: 0x02002EBC RID: 11964
				public class CAN_DO_ASTRONAUT_TRAINING
				{
					// Token: 0x0400BC71 RID: 48241
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.ASTRONAUTTRAININGCENTER.NAME + " Usage";
				}

				// Token: 0x02002EBD RID: 11965
				public class CAN_MISSION_CONTROL
				{
					// Token: 0x0400BC72 RID: 48242
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.MISSIONCONTROL.NAME + " Usage";
				}

				// Token: 0x02002EBE RID: 11966
				public class CAN_PILOT_ROCKET
				{
					// Token: 0x0400BC73 RID: 48243
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.ROCKETCONTROLSTATION.NAME + " Usage";
				}

				// Token: 0x02002EBF RID: 11967
				public class CAN_COMPOUND
				{
					// Token: 0x0400BC74 RID: 48244
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.APOTHECARY.NAME + " Usage";
				}

				// Token: 0x02002EC0 RID: 11968
				public class CAN_DOCTOR
				{
					// Token: 0x0400BC75 RID: 48245
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.DOCTORSTATION.NAME + " Usage";
				}

				// Token: 0x02002EC1 RID: 11969
				public class CAN_ADVANCED_MEDICINE
				{
					// Token: 0x0400BC76 RID: 48246
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.ADVANCEDDOCTORSTATION.NAME + " Usage";
				}

				// Token: 0x02002EC2 RID: 11970
				public class CAN_DEMOLISH
				{
					// Token: 0x0400BC77 RID: 48247
					public static LocString DESCRIPTION = "Demolish Gravitas Buildings";
				}
			}

			// Token: 0x02002462 RID: 9314
			public class ASSIGNMENT_REQUIREMENTS
			{
				// Token: 0x04009EFC RID: 40700
				public static LocString TITLE = "Qualifications";

				// Token: 0x04009EFD RID: 40701
				public static LocString NONE = "This position has no qualification requirements";

				// Token: 0x04009EFE RID: 40702
				public static LocString ALREADY_IS_ROLE = "{0} <b>is already</b> assigned to the {1} position";

				// Token: 0x04009EFF RID: 40703
				public static LocString ALREADY_IS_JOBLESS = "{0} <b>is already</b> unemployed";

				// Token: 0x04009F00 RID: 40704
				public static LocString MASTERED = "{0} has mastered the {1} position";

				// Token: 0x04009F01 RID: 40705
				public static LocString WILL_BE_UNASSIGNED = "Note: Assigning {0} to {1} will <color=#F44A47FF>unassign</color> them from {2}";

				// Token: 0x04009F02 RID: 40706
				public static LocString RELEVANT_ATTRIBUTES = "Relevant skills:";

				// Token: 0x04009F03 RID: 40707
				public static LocString APTITUDES = "Interests";

				// Token: 0x04009F04 RID: 40708
				public static LocString RELEVANT_APTITUDES = "Relevant Interests:";

				// Token: 0x04009F05 RID: 40709
				public static LocString NO_APTITUDE = "None";

				// Token: 0x02002EC3 RID: 11971
				public class ELIGIBILITY
				{
					// Token: 0x0400BC78 RID: 48248
					public static LocString ELIGIBLE = "{0} is qualified for the {1} position";

					// Token: 0x0400BC79 RID: 48249
					public static LocString INELIGIBLE = "{0} is <color=#F44A47FF>not qualified</color> for the {1} position";
				}

				// Token: 0x02002EC4 RID: 11972
				public class UNEMPLOYED
				{
					// Token: 0x0400BC7A RID: 48250
					public static LocString NAME = "Unassigned";

					// Token: 0x0400BC7B RID: 48251
					public static LocString DESCRIPTION = "Duplicant must not already have a job assignment";
				}

				// Token: 0x02002EC5 RID: 11973
				public class HAS_COLONY_LEADER
				{
					// Token: 0x0400BC7C RID: 48252
					public static LocString NAME = "Has colony leader";

					// Token: 0x0400BC7D RID: 48253
					public static LocString DESCRIPTION = "A colony leader must be assigned";
				}

				// Token: 0x02002EC6 RID: 11974
				public class HAS_ATTRIBUTE_DIGGING_BASIC
				{
					// Token: 0x0400BC7E RID: 48254
					public static LocString NAME = "Basic Digging";

					// Token: 0x0400BC7F RID: 48255
					public static LocString DESCRIPTION = "Must have at least {0} digging skill";
				}

				// Token: 0x02002EC7 RID: 11975
				public class HAS_ATTRIBUTE_COOKING_BASIC
				{
					// Token: 0x0400BC80 RID: 48256
					public static LocString NAME = "Basic Cooking";

					// Token: 0x0400BC81 RID: 48257
					public static LocString DESCRIPTION = "Must have at least {0} cooking skill";
				}

				// Token: 0x02002EC8 RID: 11976
				public class HAS_ATTRIBUTE_LEARNING_BASIC
				{
					// Token: 0x0400BC82 RID: 48258
					public static LocString NAME = "Basic Learning";

					// Token: 0x0400BC83 RID: 48259
					public static LocString DESCRIPTION = "Must have at least {0} learning skill";
				}

				// Token: 0x02002EC9 RID: 11977
				public class HAS_ATTRIBUTE_LEARNING_MEDIUM
				{
					// Token: 0x0400BC84 RID: 48260
					public static LocString NAME = "Medium Learning";

					// Token: 0x0400BC85 RID: 48261
					public static LocString DESCRIPTION = "Must have at least {0} learning skill";
				}

				// Token: 0x02002ECA RID: 11978
				public class HAS_EXPERIENCE
				{
					// Token: 0x0400BC86 RID: 48262
					public static LocString NAME = "{0} Experience";

					// Token: 0x0400BC87 RID: 48263
					public static LocString DESCRIPTION = "Mastery of the <b>{0}</b> job";
				}

				// Token: 0x02002ECB RID: 11979
				public class HAS_COMPLETED_ANY_OTHER_ROLE
				{
					// Token: 0x0400BC88 RID: 48264
					public static LocString NAME = "General Experience";

					// Token: 0x0400BC89 RID: 48265
					public static LocString DESCRIPTION = "Mastery of <b>at least one</b> job";
				}

				// Token: 0x02002ECC RID: 11980
				public class CHOREGROUP_ENABLED
				{
					// Token: 0x0400BC8A RID: 48266
					public static LocString NAME = "Can perform {0}";

					// Token: 0x0400BC8B RID: 48267
					public static LocString DESCRIPTION = "Capable of performing <b>{0}</b> jobs";
				}
			}

			// Token: 0x02002463 RID: 9315
			public class EXPECTATIONS
			{
				// Token: 0x04009F06 RID: 40710
				public static LocString TITLE = "Special Provisions Request";

				// Token: 0x04009F07 RID: 40711
				public static LocString NO_EXPECTATIONS = "No additional provisions are required to perform this job";

				// Token: 0x02002ECD RID: 11981
				public class PRIVATE_ROOM
				{
					// Token: 0x0400BC8C RID: 48268
					public static LocString NAME = "Private Bedroom";

					// Token: 0x0400BC8D RID: 48269
					public static LocString DESCRIPTION = "Duplicants in this job would appreciate their own place to unwind";
				}

				// Token: 0x02002ECE RID: 11982
				public class FOOD_QUALITY
				{
					// Token: 0x020030C8 RID: 12488
					public class MINOR
					{
						// Token: 0x0400C213 RID: 49683
						public static LocString NAME = "Standard Food";

						// Token: 0x0400C214 RID: 49684
						public static LocString DESCRIPTION = "Duplicants employed in this Tier desire food that meets basic living standards";
					}

					// Token: 0x020030C9 RID: 12489
					public class MEDIUM
					{
						// Token: 0x0400C215 RID: 49685
						public static LocString NAME = "Good Food";

						// Token: 0x0400C216 RID: 49686
						public static LocString DESCRIPTION = "Duplicants employed in this Tier desire decent food for their efforts";
					}

					// Token: 0x020030CA RID: 12490
					public class HIGH
					{
						// Token: 0x0400C217 RID: 49687
						public static LocString NAME = "Great Food";

						// Token: 0x0400C218 RID: 49688
						public static LocString DESCRIPTION = "Duplicants employed in this Tier desire better than average food";
					}

					// Token: 0x020030CB RID: 12491
					public class VERY_HIGH
					{
						// Token: 0x0400C219 RID: 49689
						public static LocString NAME = "Superb Food";

						// Token: 0x0400C21A RID: 49690
						public static LocString DESCRIPTION = "Duplicants employed in this Tier have a refined taste for food";
					}

					// Token: 0x020030CC RID: 12492
					public class EXCEPTIONAL
					{
						// Token: 0x0400C21B RID: 49691
						public static LocString NAME = "Ambrosial Food";

						// Token: 0x0400C21C RID: 49692
						public static LocString DESCRIPTION = "Duplicants employed in this Tier expect only the best cuisine";
					}
				}

				// Token: 0x02002ECF RID: 11983
				public class DECOR
				{
					// Token: 0x020030CD RID: 12493
					public class MINOR
					{
						// Token: 0x0400C21D RID: 49693
						public static LocString NAME = "Minor Decor";

						// Token: 0x0400C21E RID: 49694
						public static LocString DESCRIPTION = "Duplicants employed in this Tier desire slightly improved colony decor";
					}

					// Token: 0x020030CE RID: 12494
					public class MEDIUM
					{
						// Token: 0x0400C21F RID: 49695
						public static LocString NAME = "Medium Decor";

						// Token: 0x0400C220 RID: 49696
						public static LocString DESCRIPTION = "Duplicants employed in this Tier desire reasonably improved colony decor";
					}

					// Token: 0x020030CF RID: 12495
					public class HIGH
					{
						// Token: 0x0400C221 RID: 49697
						public static LocString NAME = "High Decor";

						// Token: 0x0400C222 RID: 49698
						public static LocString DESCRIPTION = "Duplicants employed in this Tier desire a decent increase in colony decor";
					}

					// Token: 0x020030D0 RID: 12496
					public class VERY_HIGH
					{
						// Token: 0x0400C223 RID: 49699
						public static LocString NAME = "Superb Decor";

						// Token: 0x0400C224 RID: 49700
						public static LocString DESCRIPTION = "Duplicants employed in this Tier desire majorly improved colony decor";
					}

					// Token: 0x020030D1 RID: 12497
					public class UNREASONABLE
					{
						// Token: 0x0400C225 RID: 49701
						public static LocString NAME = "Decadent Decor";

						// Token: 0x0400C226 RID: 49702
						public static LocString DESCRIPTION = "Duplicants employed in this Tier desire unrealistically luxurious improvements to decor";
					}
				}

				// Token: 0x02002ED0 RID: 11984
				public class QUALITYOFLIFE
				{
					// Token: 0x020030D2 RID: 12498
					public class TIER0
					{
						// Token: 0x0400C227 RID: 49703
						public static LocString NAME = "Morale Requirements";

						// Token: 0x0400C228 RID: 49704
						public static LocString DESCRIPTION = "Tier 0";
					}

					// Token: 0x020030D3 RID: 12499
					public class TIER1
					{
						// Token: 0x0400C229 RID: 49705
						public static LocString NAME = "Morale Requirements";

						// Token: 0x0400C22A RID: 49706
						public static LocString DESCRIPTION = "Tier 1";
					}

					// Token: 0x020030D4 RID: 12500
					public class TIER2
					{
						// Token: 0x0400C22B RID: 49707
						public static LocString NAME = "Morale Requirements";

						// Token: 0x0400C22C RID: 49708
						public static LocString DESCRIPTION = "Tier 2";
					}

					// Token: 0x020030D5 RID: 12501
					public class TIER3
					{
						// Token: 0x0400C22D RID: 49709
						public static LocString NAME = "Morale Requirements";

						// Token: 0x0400C22E RID: 49710
						public static LocString DESCRIPTION = "Tier 3";
					}

					// Token: 0x020030D6 RID: 12502
					public class TIER4
					{
						// Token: 0x0400C22F RID: 49711
						public static LocString NAME = "Morale Requirements";

						// Token: 0x0400C230 RID: 49712
						public static LocString DESCRIPTION = "Tier 4";
					}

					// Token: 0x020030D7 RID: 12503
					public class TIER5
					{
						// Token: 0x0400C231 RID: 49713
						public static LocString NAME = "Morale Requirements";

						// Token: 0x0400C232 RID: 49714
						public static LocString DESCRIPTION = "Tier 5";
					}

					// Token: 0x020030D8 RID: 12504
					public class TIER6
					{
						// Token: 0x0400C233 RID: 49715
						public static LocString NAME = "Morale Requirements";

						// Token: 0x0400C234 RID: 49716
						public static LocString DESCRIPTION = "Tier 6";
					}

					// Token: 0x020030D9 RID: 12505
					public class TIER7
					{
						// Token: 0x0400C235 RID: 49717
						public static LocString NAME = "Morale Requirements";

						// Token: 0x0400C236 RID: 49718
						public static LocString DESCRIPTION = "Tier 7";
					}

					// Token: 0x020030DA RID: 12506
					public class TIER8
					{
						// Token: 0x0400C237 RID: 49719
						public static LocString NAME = "Morale Requirements";

						// Token: 0x0400C238 RID: 49720
						public static LocString DESCRIPTION = "Tier 8";
					}
				}
			}
		}

		// Token: 0x02001C92 RID: 7314
		public class GAMEPLAY_EVENT_INFO_SCREEN
		{
			// Token: 0x04008072 RID: 32882
			public static LocString WHERE = "WHERE: {0}";

			// Token: 0x04008073 RID: 32883
			public static LocString WHEN = "WHEN: {0}";
		}

		// Token: 0x02001C93 RID: 7315
		public class DEBUG_TOOLS
		{
			// Token: 0x04008074 RID: 32884
			public static LocString ENTER_TEXT = "";

			// Token: 0x04008075 RID: 32885
			public static LocString DEBUG_ACTIVE = "Debug tools active";

			// Token: 0x04008076 RID: 32886
			public static LocString INVALID_LOCATION = "Invalid Location";

			// Token: 0x02002464 RID: 9316
			public class PAINT_ELEMENTS_SCREEN
			{
				// Token: 0x04009F08 RID: 40712
				public static LocString TITLE = "CELL PAINTER";

				// Token: 0x04009F09 RID: 40713
				public static LocString ELEMENT = "Element";

				// Token: 0x04009F0A RID: 40714
				public static LocString MASS_KG = "Mass (kg)";

				// Token: 0x04009F0B RID: 40715
				public static LocString TEMPERATURE_KELVIN = "Temperature (K)";

				// Token: 0x04009F0C RID: 40716
				public static LocString DISEASE = "Disease";

				// Token: 0x04009F0D RID: 40717
				public static LocString DISEASE_COUNT = "Disease Count";

				// Token: 0x04009F0E RID: 40718
				public static LocString BUILDINGS = "Buildings:";

				// Token: 0x04009F0F RID: 40719
				public static LocString CELLS = "Cells:";

				// Token: 0x04009F10 RID: 40720
				public static LocString ADD_FOW_MASK = "Prevent FoW Reveal";

				// Token: 0x04009F11 RID: 40721
				public static LocString REMOVE_FOW_MASK = "Allow FoW Reveal";

				// Token: 0x04009F12 RID: 40722
				public static LocString PAINT = "Paint";

				// Token: 0x04009F13 RID: 40723
				public static LocString SAMPLE = "Sample";

				// Token: 0x04009F14 RID: 40724
				public static LocString STORE = "Store";

				// Token: 0x04009F15 RID: 40725
				public static LocString FILL = "Fill";

				// Token: 0x04009F16 RID: 40726
				public static LocString SPAWN_ALL = "Spawn All (Slow)";
			}

			// Token: 0x02002465 RID: 9317
			public class SAVE_BASE_TEMPLATE
			{
				// Token: 0x04009F17 RID: 40727
				public static LocString TITLE = "Base and World Tools";

				// Token: 0x04009F18 RID: 40728
				public static LocString SAVE_TITLE = "Save Selection";

				// Token: 0x04009F19 RID: 40729
				public static LocString CLEAR_BUTTON = "Clear Floor";

				// Token: 0x04009F1A RID: 40730
				public static LocString DESTROY_BUTTON = "Destroy";

				// Token: 0x04009F1B RID: 40731
				public static LocString DECONSTRUCT_BUTTON = "Deconstruct";

				// Token: 0x04009F1C RID: 40732
				public static LocString CLEAR_SELECTION_BUTTON = "Clear Selection";

				// Token: 0x04009F1D RID: 40733
				public static LocString DEFAULT_SAVE_NAME = "TemplateSaveName";

				// Token: 0x04009F1E RID: 40734
				public static LocString MORE = "More";

				// Token: 0x04009F1F RID: 40735
				public static LocString BASE_GAME_FOLDER_NAME = "Base Game";

				// Token: 0x02002ED1 RID: 11985
				public class SELECTION_INFO_PANEL
				{
					// Token: 0x0400BC8E RID: 48270
					public static LocString TOTAL_MASS = "Total mass: {0}";

					// Token: 0x0400BC8F RID: 48271
					public static LocString AVERAGE_MASS = "Average cell mass: {0}";

					// Token: 0x0400BC90 RID: 48272
					public static LocString AVERAGE_TEMPERATURE = "Average temperature: {0}";

					// Token: 0x0400BC91 RID: 48273
					public static LocString TOTAL_JOULES = "Total joules: {0}";

					// Token: 0x0400BC92 RID: 48274
					public static LocString JOULES_PER_KILOGRAM = "Joules per kilogram: {0}";

					// Token: 0x0400BC93 RID: 48275
					public static LocString TOTAL_RADS = "Total rads: {0}";

					// Token: 0x0400BC94 RID: 48276
					public static LocString AVERAGE_RADS = "Average rads: {0}";
				}
			}
		}

		// Token: 0x02001C94 RID: 7316
		public class WORLDGEN
		{
			// Token: 0x04008077 RID: 32887
			public static LocString NOHEADERS = "";

			// Token: 0x04008078 RID: 32888
			public static LocString COMPLETE = "Success! Space adventure awaits.";

			// Token: 0x04008079 RID: 32889
			public static LocString FAILED = "Goodness, has this ever gone terribly wrong!";

			// Token: 0x0400807A RID: 32890
			public static LocString RESTARTING = "Rebooting...";

			// Token: 0x0400807B RID: 32891
			public static LocString LOADING = "Loading world...";

			// Token: 0x0400807C RID: 32892
			public static LocString GENERATINGWORLD = "The Galaxy Synthesizer";

			// Token: 0x0400807D RID: 32893
			public static LocString CHOOSEWORLDSIZE = "Select the magnitude of your new galaxy.";

			// Token: 0x0400807E RID: 32894
			public static LocString USING_PLAYER_SEED = "Using selected worldgen seed: {0}";

			// Token: 0x0400807F RID: 32895
			public static LocString CLEARINGLEVEL = "Staring into the void...";

			// Token: 0x04008080 RID: 32896
			public static LocString RETRYCOUNT = "Oh dear, let's try that again.";

			// Token: 0x04008081 RID: 32897
			public static LocString GENERATESOLARSYSTEM = "Catalyzing Big Bang...";

			// Token: 0x04008082 RID: 32898
			public static LocString GENERATESOLARSYSTEM1 = "Catalyzing Big Bang...";

			// Token: 0x04008083 RID: 32899
			public static LocString GENERATESOLARSYSTEM2 = "Catalyzing Big Bang...";

			// Token: 0x04008084 RID: 32900
			public static LocString GENERATESOLARSYSTEM3 = "Catalyzing Big Bang...";

			// Token: 0x04008085 RID: 32901
			public static LocString GENERATESOLARSYSTEM4 = "Catalyzing Big Bang...";

			// Token: 0x04008086 RID: 32902
			public static LocString GENERATESOLARSYSTEM5 = "Catalyzing Big Bang...";

			// Token: 0x04008087 RID: 32903
			public static LocString GENERATESOLARSYSTEM6 = "Approaching event horizon...";

			// Token: 0x04008088 RID: 32904
			public static LocString GENERATESOLARSYSTEM7 = "Approaching event horizon...";

			// Token: 0x04008089 RID: 32905
			public static LocString GENERATESOLARSYSTEM8 = "Approaching event horizon...";

			// Token: 0x0400808A RID: 32906
			public static LocString GENERATESOLARSYSTEM9 = "Approaching event horizon...";

			// Token: 0x0400808B RID: 32907
			public static LocString SETUPNOISE = "BANG!";

			// Token: 0x0400808C RID: 32908
			public static LocString BUILDNOISESOURCE = "Sorting quadrillions of atoms...";

			// Token: 0x0400808D RID: 32909
			public static LocString BUILDNOISESOURCE1 = "Sorting quadrillions of atoms...";

			// Token: 0x0400808E RID: 32910
			public static LocString BUILDNOISESOURCE2 = "Sorting quadrillions of atoms...";

			// Token: 0x0400808F RID: 32911
			public static LocString BUILDNOISESOURCE3 = "Ironing the fabric of creation...";

			// Token: 0x04008090 RID: 32912
			public static LocString BUILDNOISESOURCE4 = "Ironing the fabric of creation...";

			// Token: 0x04008091 RID: 32913
			public static LocString BUILDNOISESOURCE5 = "Ironing the fabric of creation...";

			// Token: 0x04008092 RID: 32914
			public static LocString BUILDNOISESOURCE6 = "Taking hot meteor shower...";

			// Token: 0x04008093 RID: 32915
			public static LocString BUILDNOISESOURCE7 = "Tightening asteroid belts...";

			// Token: 0x04008094 RID: 32916
			public static LocString BUILDNOISESOURCE8 = "Tightening asteroid belts...";

			// Token: 0x04008095 RID: 32917
			public static LocString BUILDNOISESOURCE9 = "Tightening asteroid belts...";

			// Token: 0x04008096 RID: 32918
			public static LocString GENERATENOISE = "Baking igneous rock...";

			// Token: 0x04008097 RID: 32919
			public static LocString GENERATENOISE1 = "Multilayering sediment...";

			// Token: 0x04008098 RID: 32920
			public static LocString GENERATENOISE2 = "Multilayering sediment...";

			// Token: 0x04008099 RID: 32921
			public static LocString GENERATENOISE3 = "Multilayering sediment...";

			// Token: 0x0400809A RID: 32922
			public static LocString GENERATENOISE4 = "Superheating gases...";

			// Token: 0x0400809B RID: 32923
			public static LocString GENERATENOISE5 = "Superheating gases...";

			// Token: 0x0400809C RID: 32924
			public static LocString GENERATENOISE6 = "Superheating gases...";

			// Token: 0x0400809D RID: 32925
			public static LocString GENERATENOISE7 = "Vacuuming out vacuums...";

			// Token: 0x0400809E RID: 32926
			public static LocString GENERATENOISE8 = "Vacuuming out vacuums...";

			// Token: 0x0400809F RID: 32927
			public static LocString GENERATENOISE9 = "Vacuuming out vacuums...";

			// Token: 0x040080A0 RID: 32928
			public static LocString NORMALISENOISE = "Interpolating suffocating gas...";

			// Token: 0x040080A1 RID: 32929
			public static LocString WORLDLAYOUT = "Freezing ice formations...";

			// Token: 0x040080A2 RID: 32930
			public static LocString WORLDLAYOUT1 = "Freezing ice formations...";

			// Token: 0x040080A3 RID: 32931
			public static LocString WORLDLAYOUT2 = "Freezing ice formations...";

			// Token: 0x040080A4 RID: 32932
			public static LocString WORLDLAYOUT3 = "Freezing ice formations...";

			// Token: 0x040080A5 RID: 32933
			public static LocString WORLDLAYOUT4 = "Melting magma...";

			// Token: 0x040080A6 RID: 32934
			public static LocString WORLDLAYOUT5 = "Melting magma...";

			// Token: 0x040080A7 RID: 32935
			public static LocString WORLDLAYOUT6 = "Melting magma...";

			// Token: 0x040080A8 RID: 32936
			public static LocString WORLDLAYOUT7 = "Sprinkling sand...";

			// Token: 0x040080A9 RID: 32937
			public static LocString WORLDLAYOUT8 = "Sprinkling sand...";

			// Token: 0x040080AA RID: 32938
			public static LocString WORLDLAYOUT9 = "Sprinkling sand...";

			// Token: 0x040080AB RID: 32939
			public static LocString WORLDLAYOUT10 = "Sprinkling sand...";

			// Token: 0x040080AC RID: 32940
			public static LocString COMPLETELAYOUT = "Cooling glass...";

			// Token: 0x040080AD RID: 32941
			public static LocString COMPLETELAYOUT1 = "Cooling glass...";

			// Token: 0x040080AE RID: 32942
			public static LocString COMPLETELAYOUT2 = "Cooling glass...";

			// Token: 0x040080AF RID: 32943
			public static LocString COMPLETELAYOUT3 = "Cooling glass...";

			// Token: 0x040080B0 RID: 32944
			public static LocString COMPLETELAYOUT4 = "Digging holes...";

			// Token: 0x040080B1 RID: 32945
			public static LocString COMPLETELAYOUT5 = "Digging holes...";

			// Token: 0x040080B2 RID: 32946
			public static LocString COMPLETELAYOUT6 = "Digging holes...";

			// Token: 0x040080B3 RID: 32947
			public static LocString COMPLETELAYOUT7 = "Adding buckets of dirt...";

			// Token: 0x040080B4 RID: 32948
			public static LocString COMPLETELAYOUT8 = "Adding buckets of dirt...";

			// Token: 0x040080B5 RID: 32949
			public static LocString COMPLETELAYOUT9 = "Adding buckets of dirt...";

			// Token: 0x040080B6 RID: 32950
			public static LocString COMPLETELAYOUT10 = "Adding buckets of dirt...";

			// Token: 0x040080B7 RID: 32951
			public static LocString PROCESSRIVERS = "Pouring rivers...";

			// Token: 0x040080B8 RID: 32952
			public static LocString CONVERTTERRAINCELLSTOEDGES = "Hardening diamonds...";

			// Token: 0x040080B9 RID: 32953
			public static LocString PROCESSING = "Embedding metals...";

			// Token: 0x040080BA RID: 32954
			public static LocString PROCESSING1 = "Embedding metals...";

			// Token: 0x040080BB RID: 32955
			public static LocString PROCESSING2 = "Embedding metals...";

			// Token: 0x040080BC RID: 32956
			public static LocString PROCESSING3 = "Burying precious ore...";

			// Token: 0x040080BD RID: 32957
			public static LocString PROCESSING4 = "Burying precious ore...";

			// Token: 0x040080BE RID: 32958
			public static LocString PROCESSING5 = "Burying precious ore...";

			// Token: 0x040080BF RID: 32959
			public static LocString PROCESSING6 = "Burying precious ore...";

			// Token: 0x040080C0 RID: 32960
			public static LocString PROCESSING7 = "Excavating tunnels...";

			// Token: 0x040080C1 RID: 32961
			public static LocString PROCESSING8 = "Excavating tunnels...";

			// Token: 0x040080C2 RID: 32962
			public static LocString PROCESSING9 = "Excavating tunnels...";

			// Token: 0x040080C3 RID: 32963
			public static LocString BORDERS = "Just adding water...";

			// Token: 0x040080C4 RID: 32964
			public static LocString BORDERS1 = "Just adding water...";

			// Token: 0x040080C5 RID: 32965
			public static LocString BORDERS2 = "Staring at the void...";

			// Token: 0x040080C6 RID: 32966
			public static LocString BORDERS3 = "Staring at the void...";

			// Token: 0x040080C7 RID: 32967
			public static LocString BORDERS4 = "Staring at the void...";

			// Token: 0x040080C8 RID: 32968
			public static LocString BORDERS5 = "Avoiding awkward eye contact with the void...";

			// Token: 0x040080C9 RID: 32969
			public static LocString BORDERS6 = "Avoiding awkward eye contact with the void...";

			// Token: 0x040080CA RID: 32970
			public static LocString BORDERS7 = "Avoiding awkward eye contact with the void...";

			// Token: 0x040080CB RID: 32971
			public static LocString BORDERS8 = "Avoiding awkward eye contact with the void...";

			// Token: 0x040080CC RID: 32972
			public static LocString BORDERS9 = "Avoiding awkward eye contact with the void...";

			// Token: 0x040080CD RID: 32973
			public static LocString DRAWWORLDBORDER = "Establishing personal boundaries...";

			// Token: 0x040080CE RID: 32974
			public static LocString PLACINGTEMPLATES = "Generating interest...";

			// Token: 0x040080CF RID: 32975
			public static LocString SETTLESIM = "Infusing oxygen...";

			// Token: 0x040080D0 RID: 32976
			public static LocString SETTLESIM1 = "Infusing oxygen...";

			// Token: 0x040080D1 RID: 32977
			public static LocString SETTLESIM2 = "Too much oxygen. Removing...";

			// Token: 0x040080D2 RID: 32978
			public static LocString SETTLESIM3 = "Too much oxygen. Removing...";

			// Token: 0x040080D3 RID: 32979
			public static LocString SETTLESIM4 = "Ideal oxygen levels achieved...";

			// Token: 0x040080D4 RID: 32980
			public static LocString SETTLESIM5 = "Ideal oxygen levels achieved...";

			// Token: 0x040080D5 RID: 32981
			public static LocString SETTLESIM6 = "Planting space flora...";

			// Token: 0x040080D6 RID: 32982
			public static LocString SETTLESIM7 = "Planting space flora...";

			// Token: 0x040080D7 RID: 32983
			public static LocString SETTLESIM8 = "Releasing wildlife...";

			// Token: 0x040080D8 RID: 32984
			public static LocString SETTLESIM9 = "Releasing wildlife...";

			// Token: 0x040080D9 RID: 32985
			public static LocString ANALYZINGWORLD = "Shuffling DNA Blueprints...";

			// Token: 0x040080DA RID: 32986
			public static LocString ANALYZINGWORLDCOMPLETE = "Tidying up for the Duplicants...";

			// Token: 0x040080DB RID: 32987
			public static LocString PLACINGCREATURES = "Building the suspense...";
		}

		// Token: 0x02001C95 RID: 7317
		public class TOOLTIPS
		{
			// Token: 0x040080DC RID: 32988
			public static LocString MANAGEMENTMENU_JOBS = string.Concat(new string[]
			{
				"Manage my Duplicant Priorities {Hotkey}\n\nDuplicant Priorities",
				UI.PST_KEYWORD,
				" are calculated <i>before</i> the ",
				UI.PRE_KEYWORD,
				"Building Priorities",
				UI.PST_KEYWORD,
				" set by the ",
				UI.FormatAsTool("Priority Tool", global::Action.Prioritize)
			});

			// Token: 0x040080DD RID: 32989
			public static LocString MANAGEMENTMENU_CONSUMABLES = "Manage my Duplicants' diets and medications {Hotkey}";

			// Token: 0x040080DE RID: 32990
			public static LocString MANAGEMENTMENU_VITALS = "View my Duplicants' vitals {Hotkey}";

			// Token: 0x040080DF RID: 32991
			public static LocString MANAGEMENTMENU_RESEARCH = "View the Research Tree {Hotkey}";

			// Token: 0x040080E0 RID: 32992
			public static LocString MANAGEMENTMENU_REQUIRES_RESEARCH = string.Concat(new string[]
			{
				"Build a Research Station to unlock this menu\n\nThe ",
				BUILDINGS.PREFABS.RESEARCHCENTER.NAME,
				" can be found in the ",
				UI.FormatAsBuildMenuTab("Stations Tab", global::Action.Plan10),
				" of the Build Menu"
			});

			// Token: 0x040080E1 RID: 32993
			public static LocString MANAGEMENTMENU_DAILYREPORT = "View each cycle's Colony Report {Hotkey}";

			// Token: 0x040080E2 RID: 32994
			public static LocString MANAGEMENTMENU_CODEX = "Browse entries in my Database {Hotkey}";

			// Token: 0x040080E3 RID: 32995
			public static LocString MANAGEMENTMENU_SCHEDULE = "Adjust the colony's time usage {Hotkey}";

			// Token: 0x040080E4 RID: 32996
			public static LocString MANAGEMENTMENU_STARMAP = "Manage astronaut rocket missions {Hotkey}";

			// Token: 0x040080E5 RID: 32997
			public static LocString MANAGEMENTMENU_REQUIRES_TELESCOPE = string.Concat(new string[]
			{
				"Build a Telescope to unlock this menu\n\nThe ",
				BUILDINGS.PREFABS.TELESCOPE.NAME,
				" can be found in the ",
				UI.FormatAsBuildMenuTab("Stations Tab", global::Action.Plan10),
				" of the Build Menu"
			});

			// Token: 0x040080E6 RID: 32998
			public static LocString MANAGEMENTMENU_REQUIRES_TELESCOPE_CLUSTER = string.Concat(new string[]
			{
				"Build a Telescope to unlock this menu\n\nThe ",
				BUILDINGS.PREFABS.TELESCOPE.NAME,
				" can be found in the ",
				UI.FormatAsBuildMenuTab("Rocketry Tab", global::Action.Plan14),
				" of the Build Menu"
			});

			// Token: 0x040080E7 RID: 32999
			public static LocString MANAGEMENTMENU_SKILLS = "Manage Duplicants' Skill assignments {Hotkey}";

			// Token: 0x040080E8 RID: 33000
			public static LocString MANAGEMENTMENU_REQUIRES_SKILL_STATION = string.Concat(new string[]
			{
				"Build a Printing Pod to unlock this menu\n\nThe ",
				BUILDINGS.PREFABS.HEADQUARTERSCOMPLETE.NAME,
				" can be found in the ",
				UI.FormatAsBuildMenuTab("Base Tab", global::Action.Plan1),
				" of the Build Menu"
			});

			// Token: 0x040080E9 RID: 33001
			public static LocString MANAGEMENTMENU_PAUSEMENU = "Open the game menu {Hotkey}";

			// Token: 0x040080EA RID: 33002
			public static LocString MANAGEMENTMENU_RESOURCES = "Open the resource management screen {Hotkey}";

			// Token: 0x040080EB RID: 33003
			public static LocString OPEN_CODEX_ENTRY = "View full entry in database";

			// Token: 0x040080EC RID: 33004
			public static LocString NO_CODEX_ENTRY = "No database entry available";

			// Token: 0x040080ED RID: 33005
			public static LocString CHANGE_OUTFIT = "Change this Duplicant's outfit";

			// Token: 0x040080EE RID: 33006
			public static LocString METERSCREEN_AVGSTRESS = "Highest Stress: {0}";

			// Token: 0x040080EF RID: 33007
			public static LocString METERSCREEN_MEALHISTORY = "Calories Available: {0}";

			// Token: 0x040080F0 RID: 33008
			public static LocString METERSCREEN_POPULATION = "Population: {0}";

			// Token: 0x040080F1 RID: 33009
			public static LocString METERSCREEN_POPULATION_CLUSTER = UI.PRE_KEYWORD + "{0}" + UI.PST_KEYWORD + " Population: {1}\nTotal Population: {2}";

			// Token: 0x040080F2 RID: 33010
			public static LocString METERSCREEN_SICK_DUPES = "Sick Duplicants: {0}";

			// Token: 0x040080F3 RID: 33011
			public static LocString METERSCREEN_INVALID_FOOD_TYPE = "Invalid Food Type: {0}";

			// Token: 0x040080F4 RID: 33012
			public static LocString PLAYBUTTON = "Start";

			// Token: 0x040080F5 RID: 33013
			public static LocString PAUSEBUTTON = "Pause";

			// Token: 0x040080F6 RID: 33014
			public static LocString PAUSE = "Pause {Hotkey}";

			// Token: 0x040080F7 RID: 33015
			public static LocString UNPAUSE = "Unpause {Hotkey}";

			// Token: 0x040080F8 RID: 33016
			public static LocString SPEEDBUTTON_SLOW = "Slow speed {Hotkey}";

			// Token: 0x040080F9 RID: 33017
			public static LocString SPEEDBUTTON_MEDIUM = "Medium speed {Hotkey}";

			// Token: 0x040080FA RID: 33018
			public static LocString SPEEDBUTTON_FAST = "Fast speed {Hotkey}";

			// Token: 0x040080FB RID: 33019
			public static LocString RED_ALERT_TITLE = "Toggle Red Alert";

			// Token: 0x040080FC RID: 33020
			public static LocString RED_ALERT_CONTENT = "Duplicants will work, ignoring schedules and their basic needs\n\nUse in case of emergency";

			// Token: 0x040080FD RID: 33021
			public static LocString DISINFECTBUTTON = "Disinfect buildings {Hotkey}";

			// Token: 0x040080FE RID: 33022
			public static LocString MOPBUTTON = "Mop liquid spills {Hotkey}";

			// Token: 0x040080FF RID: 33023
			public static LocString DIGBUTTON = "Set dig errands {Hotkey}";

			// Token: 0x04008100 RID: 33024
			public static LocString CANCELBUTTON = "Cancel errands {Hotkey}";

			// Token: 0x04008101 RID: 33025
			public static LocString DECONSTRUCTBUTTON = "Demolish buildings {Hotkey}";

			// Token: 0x04008102 RID: 33026
			public static LocString ATTACKBUTTON = "Attack poor, wild critters {Hotkey}";

			// Token: 0x04008103 RID: 33027
			public static LocString CAPTUREBUTTON = "Capture critters {Hotkey}";

			// Token: 0x04008104 RID: 33028
			public static LocString CLEARBUTTON = "Move debris into storage {Hotkey}";

			// Token: 0x04008105 RID: 33029
			public static LocString HARVESTBUTTON = "Harvest plants {Hotkey}";

			// Token: 0x04008106 RID: 33030
			public static LocString PRIORITIZEMAINBUTTON = "";

			// Token: 0x04008107 RID: 33031
			public static LocString PRIORITIZEBUTTON = string.Concat(new string[]
			{
				"Set Building Priority {Hotkey}\n\nDuplicant Priorities",
				UI.PST_KEYWORD,
				" ",
				UI.FormatAsHotKey(global::Action.ManagePriorities),
				" are calculated <i>before</i> the ",
				UI.PRE_KEYWORD,
				"Building Priorities",
				UI.PST_KEYWORD,
				" set by this tool"
			});

			// Token: 0x04008108 RID: 33032
			public static LocString CLEANUPMAINBUTTON = "Mop and sweep messy floors {Hotkey}";

			// Token: 0x04008109 RID: 33033
			public static LocString CANCELDECONSTRUCTIONBUTTON = "Cancel queued orders or deconstruct existing buildings {Hotkey}";

			// Token: 0x0400810A RID: 33034
			public static LocString HELP_ROTATE_KEY = "Press " + UI.FormatAsHotKey(global::Action.RotateBuilding) + " to Rotate";

			// Token: 0x0400810B RID: 33035
			public static LocString HELP_BUILDLOCATION_INVALID_CELL = "Invalid Cell";

			// Token: 0x0400810C RID: 33036
			public static LocString HELP_BUILDLOCATION_MISSING_TELEPAD = "World has no " + BUILDINGS.PREFABS.HEADQUARTERSCOMPLETE.NAME + " or " + BUILDINGS.PREFABS.EXOBASEHEADQUARTERS.NAME;

			// Token: 0x0400810D RID: 33037
			public static LocString HELP_BUILDLOCATION_FLOOR = "Must be built on solid ground";

			// Token: 0x0400810E RID: 33038
			public static LocString HELP_BUILDLOCATION_WALL = "Must be built against a wall";

			// Token: 0x0400810F RID: 33039
			public static LocString HELP_BUILDLOCATION_FLOOR_OR_ATTACHPOINT = "Must be built on solid ground or overlapping an {0}";

			// Token: 0x04008110 RID: 33040
			public static LocString HELP_BUILDLOCATION_OCCUPIED = "Must be built in unoccupied space";

			// Token: 0x04008111 RID: 33041
			public static LocString HELP_BUILDLOCATION_CEILING = "Must be built on the ceiling";

			// Token: 0x04008112 RID: 33042
			public static LocString HELP_BUILDLOCATION_INSIDEGROUND = "Must be built in the ground";

			// Token: 0x04008113 RID: 33043
			public static LocString HELP_BUILDLOCATION_ATTACHPOINT = "Must be built overlapping a {0}";

			// Token: 0x04008114 RID: 33044
			public static LocString HELP_BUILDLOCATION_SPACE = "Must be built on the surface in space";

			// Token: 0x04008115 RID: 33045
			public static LocString HELP_BUILDLOCATION_CORNER = "Must be built in a corner";

			// Token: 0x04008116 RID: 33046
			public static LocString HELP_BUILDLOCATION_CORNER_FLOOR = "Must be built in a corner on the ground";

			// Token: 0x04008117 RID: 33047
			public static LocString HELP_BUILDLOCATION_BELOWROCKETCEILING = "Must be placed further from the edge of space";

			// Token: 0x04008118 RID: 33048
			public static LocString HELP_BUILDLOCATION_ONROCKETENVELOPE = "Must be built on the interior wall of a rocket";

			// Token: 0x04008119 RID: 33049
			public static LocString HELP_BUILDLOCATION_LIQUID_CONDUIT_FORBIDDEN = "Obstructed by a building";

			// Token: 0x0400811A RID: 33050
			public static LocString HELP_BUILDLOCATION_NOT_IN_TILES = "Cannot be built inside tile";

			// Token: 0x0400811B RID: 33051
			public static LocString HELP_BUILDLOCATION_GASPORTS_OVERLAP = "Gas ports cannot overlap";

			// Token: 0x0400811C RID: 33052
			public static LocString HELP_BUILDLOCATION_LIQUIDPORTS_OVERLAP = "Liquid ports cannot overlap";

			// Token: 0x0400811D RID: 33053
			public static LocString HELP_BUILDLOCATION_SOLIDPORTS_OVERLAP = "Solid ports cannot overlap";

			// Token: 0x0400811E RID: 33054
			public static LocString HELP_BUILDLOCATION_LOGIC_PORTS_OBSTRUCTED = "Automation ports cannot overlap";

			// Token: 0x0400811F RID: 33055
			public static LocString HELP_BUILDLOCATION_WIRECONNECTORS_OVERLAP = "Power connectors cannot overlap";

			// Token: 0x04008120 RID: 33056
			public static LocString HELP_BUILDLOCATION_HIGHWATT_NOT_IN_TILE = "Heavi-Watt connectors cannot be built inside tile";

			// Token: 0x04008121 RID: 33057
			public static LocString HELP_BUILDLOCATION_WIRE_OBSTRUCTION = "Obstructed by Heavi-Watt Wire";

			// Token: 0x04008122 RID: 33058
			public static LocString HELP_BUILDLOCATION_BACK_WALL = "Obstructed by back wall";

			// Token: 0x04008123 RID: 33059
			public static LocString HELP_TUBELOCATION_NO_UTURNS = "Can't U-Turn";

			// Token: 0x04008124 RID: 33060
			public static LocString HELP_TUBELOCATION_STRAIGHT_BRIDGES = "Can't Turn Here";

			// Token: 0x04008125 RID: 33061
			public static LocString HELP_REQUIRES_ROOM = "Must be in a " + UI.PRE_KEYWORD + "Room" + UI.PST_KEYWORD;

			// Token: 0x04008126 RID: 33062
			public static LocString OXYGENOVERLAYSTRING = "Displays ambient oxygen density {Hotkey}";

			// Token: 0x04008127 RID: 33063
			public static LocString POWEROVERLAYSTRING = "Displays power grid components {Hotkey}";

			// Token: 0x04008128 RID: 33064
			public static LocString TEMPERATUREOVERLAYSTRING = "Displays ambient temperature {Hotkey}";

			// Token: 0x04008129 RID: 33065
			public static LocString HEATFLOWOVERLAYSTRING = "Displays comfortable temperatures for Duplicants {Hotkey}";

			// Token: 0x0400812A RID: 33066
			public static LocString SUITOVERLAYSTRING = "Displays Exosuits and related buildings {Hotkey}";

			// Token: 0x0400812B RID: 33067
			public static LocString LOGICOVERLAYSTRING = "Displays automation grid components {Hotkey}";

			// Token: 0x0400812C RID: 33068
			public static LocString ROOMSOVERLAYSTRING = "Displays special purpose rooms and bonuses {Hotkey}";

			// Token: 0x0400812D RID: 33069
			public static LocString JOULESOVERLAYSTRING = "Displays the thermal energy in each cell";

			// Token: 0x0400812E RID: 33070
			public static LocString LIGHTSOVERLAYSTRING = "Displays the visibility radius of light sources {Hotkey}";

			// Token: 0x0400812F RID: 33071
			public static LocString LIQUIDVENTOVERLAYSTRING = "Displays liquid pipe system components {Hotkey}";

			// Token: 0x04008130 RID: 33072
			public static LocString GASVENTOVERLAYSTRING = "Displays gas pipe system components {Hotkey}";

			// Token: 0x04008131 RID: 33073
			public static LocString DECOROVERLAYSTRING = "Displays areas with Morale-boosting decor values {Hotkey}";

			// Token: 0x04008132 RID: 33074
			public static LocString PRIORITIESOVERLAYSTRING = "Displays work priority values {Hotkey}";

			// Token: 0x04008133 RID: 33075
			public static LocString DISEASEOVERLAYSTRING = "Displays areas of disease risk {Hotkey}";

			// Token: 0x04008134 RID: 33076
			public static LocString NOISE_POLLUTION_OVERLAY_STRING = "Displays ambient noise levels {Hotkey}";

			// Token: 0x04008135 RID: 33077
			public static LocString CROPS_OVERLAY_STRING = "Displays plant growth progress {Hotkey}";

			// Token: 0x04008136 RID: 33078
			public static LocString CONVEYOR_OVERLAY_STRING = "Displays conveyor transport components {Hotkey}";

			// Token: 0x04008137 RID: 33079
			public static LocString TILEMODE_OVERLAY_STRING = "Displays material information {Hotkey}";

			// Token: 0x04008138 RID: 33080
			public static LocString REACHABILITYOVERLAYSTRING = "Displays areas accessible by Duplicants";

			// Token: 0x04008139 RID: 33081
			public static LocString RADIATIONOVERLAYSTRING = "Displays radiation levels {Hotkey}";

			// Token: 0x0400813A RID: 33082
			public static LocString ENERGYREQUIRED = UI.FormatAsLink("Power", "POWER") + " Required";

			// Token: 0x0400813B RID: 33083
			public static LocString ENERGYGENERATED = UI.FormatAsLink("Power", "POWER") + " Produced";

			// Token: 0x0400813C RID: 33084
			public static LocString INFOPANEL = "The Info Panel contains an overview of the basic information about my Duplicant";

			// Token: 0x0400813D RID: 33085
			public static LocString VITALSPANEL = "The Vitals Panel monitors the status and well being of my Duplicant";

			// Token: 0x0400813E RID: 33086
			public static LocString STRESSPANEL = "The Stress Panel offers a detailed look at what is affecting my Duplicant psychologically";

			// Token: 0x0400813F RID: 33087
			public static LocString STATSPANEL = "The Stats Panel gives me an overview of my Duplicant's individual stats";

			// Token: 0x04008140 RID: 33088
			public static LocString ITEMSPANEL = "The Items Panel displays everything this Duplicant is in possession of";

			// Token: 0x04008141 RID: 33089
			public static LocString STRESSDESCRIPTION = string.Concat(new string[]
			{
				"Accommodate my Duplicant's needs to manage their ",
				UI.FormatAsLink("Stress", "STRESS"),
				".\n\nLow ",
				UI.FormatAsLink("Stress", "STRESS"),
				" can provide a productivity boost, while high ",
				UI.FormatAsLink("Stress", "STRESS"),
				" can impair production or even lead to a nervous breakdown."
			});

			// Token: 0x04008142 RID: 33090
			public static LocString ALERTSTOOLTIP = "Alerts provide important information about what's happening in the colony right now";

			// Token: 0x04008143 RID: 33091
			public static LocString MESSAGESTOOLTIP = "Messages are events that have happened and tips to help me manage my colony";

			// Token: 0x04008144 RID: 33092
			public static LocString NEXTMESSAGESTOOLTIP = "Next message";

			// Token: 0x04008145 RID: 33093
			public static LocString CLOSETOOLTIP = "Close";

			// Token: 0x04008146 RID: 33094
			public static LocString DISMISSMESSAGE = "Dismiss message";

			// Token: 0x04008147 RID: 33095
			public static LocString RECIPE_QUEUE = "Queue {0} for continuous fabrication";

			// Token: 0x04008148 RID: 33096
			public static LocString RED_ALERT_BUTTON_ON = "Enable Red Alert";

			// Token: 0x04008149 RID: 33097
			public static LocString RED_ALERT_BUTTON_OFF = "Disable Red Alert";

			// Token: 0x0400814A RID: 33098
			public static LocString JOBSSCREEN_PRIORITY = "High priority tasks are always performed before low priority tasks.\n\nHowever, a busy Duplicant will continue to work on their current work errand until it's complete, even if a more important errand becomes available.";

			// Token: 0x0400814B RID: 33099
			public static LocString JOBSSCREEN_ATTRIBUTES = "The following attributes affect a Duplicant's efficiency at this errand:";

			// Token: 0x0400814C RID: 33100
			public static LocString JOBSSCREEN_CANNOTPERFORMTASK = "{0} cannot perform this errand.";

			// Token: 0x0400814D RID: 33101
			public static LocString JOBSSCREEN_RELEVANT_ATTRIBUTES = "Relevant Attributes:";

			// Token: 0x0400814E RID: 33102
			public static LocString SORTCOLUMN = UI.CLICK(UI.ClickType.Click) + " to sort";

			// Token: 0x0400814F RID: 33103
			public static LocString NOMATERIAL = "Not enough materials";

			// Token: 0x04008150 RID: 33104
			public static LocString SELECTAMATERIAL = "There are insufficient materials to construct this building";

			// Token: 0x04008151 RID: 33105
			public static LocString EDITNAME = "Give this Duplicant a new name";

			// Token: 0x04008152 RID: 33106
			public static LocString RANDOMIZENAME = "Randomize this Duplicant's name";

			// Token: 0x04008153 RID: 33107
			public static LocString EDITNAMEGENERIC = "Rename {0}";

			// Token: 0x04008154 RID: 33108
			public static LocString EDITNAMEROCKET = "Rename this rocket";

			// Token: 0x04008155 RID: 33109
			public static LocString BASE_VALUE = "Base Value";

			// Token: 0x04008156 RID: 33110
			public static LocString MATIERIAL_MOD = "Made out of {0}";

			// Token: 0x04008157 RID: 33111
			public static LocString VITALS_CHECKBOX_TEMPERATURE = string.Concat(new string[]
			{
				"This plant's internal ",
				UI.PRE_KEYWORD,
				"Temperature",
				UI.PST_KEYWORD,
				" is <b>{temperature}</b>"
			});

			// Token: 0x04008158 RID: 33112
			public static LocString VITALS_CHECKBOX_PRESSURE = string.Concat(new string[]
			{
				"The current ",
				UI.PRE_KEYWORD,
				"Gas",
				UI.PST_KEYWORD,
				" pressure is <b>{pressure}</b>"
			});

			// Token: 0x04008159 RID: 33113
			public static LocString VITALS_CHECKBOX_ATMOSPHERE = "This plant is immersed in {element}";

			// Token: 0x0400815A RID: 33114
			public static LocString VITALS_CHECKBOX_ILLUMINATION_DARK = "This plant is currently in the dark";

			// Token: 0x0400815B RID: 33115
			public static LocString VITALS_CHECKBOX_ILLUMINATION_LIGHT = "This plant is currently lit";

			// Token: 0x0400815C RID: 33116
			public static LocString VITALS_CHECKBOX_FERTILIZER = string.Concat(new string[]
			{
				"<b>{mass}</b> of ",
				UI.PRE_KEYWORD,
				"Fertilizer",
				UI.PST_KEYWORD,
				" is currently available"
			});

			// Token: 0x0400815D RID: 33117
			public static LocString VITALS_CHECKBOX_IRRIGATION = string.Concat(new string[]
			{
				"<b>{mass}</b> of ",
				UI.PRE_KEYWORD,
				"Liquid",
				UI.PST_KEYWORD,
				" is currently available"
			});

			// Token: 0x0400815E RID: 33118
			public static LocString VITALS_CHECKBOX_SUBMERGED_TRUE = "This plant is fully submerged in " + UI.PRE_KEYWORD + "Liquid" + UI.PRE_KEYWORD;

			// Token: 0x0400815F RID: 33119
			public static LocString VITALS_CHECKBOX_SUBMERGED_FALSE = "This plant must be submerged in " + UI.PRE_KEYWORD + "Liquid" + UI.PST_KEYWORD;

			// Token: 0x04008160 RID: 33120
			public static LocString VITALS_CHECKBOX_DROWNING_TRUE = "This plant is not drowning";

			// Token: 0x04008161 RID: 33121
			public static LocString VITALS_CHECKBOX_DROWNING_FALSE = "This plant is drowning in " + UI.PRE_KEYWORD + "Liquid" + UI.PST_KEYWORD;

			// Token: 0x04008162 RID: 33122
			public static LocString VITALS_CHECKBOX_RECEPTACLE_OPERATIONAL = "This plant is housed in an operational farm plot";

			// Token: 0x04008163 RID: 33123
			public static LocString VITALS_CHECKBOX_RECEPTACLE_INOPERATIONAL = "This plant is not housed in an operational farm plot";

			// Token: 0x04008164 RID: 33124
			public static LocString VITALS_CHECKBOX_RADIATION = string.Concat(new string[]
			{
				"This plant is sitting in <b>{rads}</b> of ambient ",
				UI.PRE_KEYWORD,
				"Radiation",
				UI.PST_KEYWORD,
				". It needs at between {minRads} and {maxRads} to grow"
			});

			// Token: 0x04008165 RID: 33125
			public static LocString VITALS_CHECKBOX_RADIATION_NO_MIN = string.Concat(new string[]
			{
				"This plant is sitting in <b>{rads}</b> of ambient ",
				UI.PRE_KEYWORD,
				"Radiation",
				UI.PST_KEYWORD,
				". It needs less than {maxRads} to grow"
			});
		}

		// Token: 0x02001C96 RID: 7318
		public class CLUSTERMAP
		{
			// Token: 0x04008166 RID: 33126
			public static LocString PLANETOID = "Planetoid";

			// Token: 0x04008167 RID: 33127
			public static LocString PLANETOID_KEYWORD = UI.PRE_KEYWORD + UI.CLUSTERMAP.PLANETOID + UI.PST_KEYWORD;

			// Token: 0x04008168 RID: 33128
			public static LocString TITLE = "STARMAP";

			// Token: 0x04008169 RID: 33129
			public static LocString LANDING_SITES = "LANDING SITES";

			// Token: 0x0400816A RID: 33130
			public static LocString DESTINATION = "DESTINATION";

			// Token: 0x0400816B RID: 33131
			public static LocString OCCUPANTS = "CREW";

			// Token: 0x0400816C RID: 33132
			public static LocString ELEMENTS = "ELEMENTS";

			// Token: 0x0400816D RID: 33133
			public static LocString UNKNOWN_DESTINATION = "Unknown";

			// Token: 0x0400816E RID: 33134
			public static LocString TILES = "Tiles";

			// Token: 0x0400816F RID: 33135
			public static LocString TILES_PER_CYCLE = "Tiles per cycle";

			// Token: 0x04008170 RID: 33136
			public static LocString CHANGE_DESTINATION = UI.CLICK(UI.ClickType.Click) + " to change destination";

			// Token: 0x04008171 RID: 33137
			public static LocString SELECT_DESTINATION = "Select a new destination on the map";

			// Token: 0x04008172 RID: 33138
			public static LocString TOOLTIP_INVALID_DESTINATION_FOG_OF_WAR = "Rockets cannot travel to this hex until it has been analyzed\n\nSpace can be analyzed with a " + BUILDINGS.PREFABS.CLUSTERTELESCOPE.NAME + " or " + BUILDINGS.PREFABS.SCANNERMODULE.NAME;

			// Token: 0x04008173 RID: 33139
			public static LocString TOOLTIP_INVALID_DESTINATION_NO_PATH = string.Concat(new string[]
			{
				"There is no navigable rocket path to this ",
				UI.CLUSTERMAP.PLANETOID_KEYWORD,
				"\n\nSpace can be analyzed with a ",
				BUILDINGS.PREFABS.CLUSTERTELESCOPE.NAME,
				" or ",
				BUILDINGS.PREFABS.SCANNERMODULE.NAME,
				" to clear the way"
			});

			// Token: 0x04008174 RID: 33140
			public static LocString TOOLTIP_INVALID_DESTINATION_NO_LAUNCH_PAD = string.Concat(new string[]
			{
				"There is no ",
				BUILDINGS.PREFABS.LAUNCHPAD.NAME,
				" on this ",
				UI.CLUSTERMAP.PLANETOID_KEYWORD,
				" for a rocket to land on\n\nUse a ",
				BUILDINGS.PREFABS.PIONEERMODULE.NAME,
				" or ",
				BUILDINGS.PREFABS.SCOUTMODULE.NAME,
				" to deploy a scout and make first contact"
			});

			// Token: 0x04008175 RID: 33141
			public static LocString TOOLTIP_INVALID_DESTINATION_REQUIRE_ASTEROID = "Must select a " + UI.CLUSTERMAP.PLANETOID_KEYWORD + " destination";

			// Token: 0x04008176 RID: 33142
			public static LocString TOOLTIP_INVALID_DESTINATION_OUT_OF_RANGE = "This destination is further away than the rocket's maximum range of {0}.";

			// Token: 0x04008177 RID: 33143
			public static LocString TOOLTIP_HIDDEN_HEX = "???";

			// Token: 0x04008178 RID: 33144
			public static LocString TOOLTIP_PEEKED_HEX_WITH_OBJECT = "UNKNOWN OBJECT DETECTED!";

			// Token: 0x04008179 RID: 33145
			public static LocString TOOLTIP_EMPTY_HEX = "EMPTY SPACE";

			// Token: 0x0400817A RID: 33146
			public static LocString TOOLTIP_PATH_LENGTH = "Trip Distance: {0}/{1}";

			// Token: 0x0400817B RID: 33147
			public static LocString TOOLTIP_PATH_LENGTH_RETURN = "Trip Distance: {0}/{1} (Return Trip)";

			// Token: 0x02002466 RID: 9318
			public class STATUS
			{
				// Token: 0x04009F20 RID: 40736
				public static LocString NORMAL = "Normal";

				// Token: 0x02002ED2 RID: 11986
				public class ROCKET
				{
					// Token: 0x0400BC95 RID: 48277
					public static LocString GROUNDED = "Normal";

					// Token: 0x0400BC96 RID: 48278
					public static LocString TRAVELING = "Traveling";

					// Token: 0x0400BC97 RID: 48279
					public static LocString STRANDED = "Stranded";

					// Token: 0x0400BC98 RID: 48280
					public static LocString IDLE = "Idle";
				}
			}

			// Token: 0x02002467 RID: 9319
			public class ASTEROIDS
			{
				// Token: 0x02002ED3 RID: 11987
				public class ELEMENT_AMOUNTS
				{
					// Token: 0x0400BC99 RID: 48281
					public static LocString LOTS = "Plentiful";

					// Token: 0x0400BC9A RID: 48282
					public static LocString SOME = "Significant amount";

					// Token: 0x0400BC9B RID: 48283
					public static LocString LITTLE = "Small amount";

					// Token: 0x0400BC9C RID: 48284
					public static LocString VERY_LITTLE = "Trace amount";
				}

				// Token: 0x02002ED4 RID: 11988
				public class SURFACE_CONDITIONS
				{
					// Token: 0x0400BC9D RID: 48285
					public static LocString LIGHT = "Peak Light";

					// Token: 0x0400BC9E RID: 48286
					public static LocString RADIATION = "Cosmic Radiation";
				}
			}

			// Token: 0x02002468 RID: 9320
			public class POI
			{
				// Token: 0x04009F21 RID: 40737
				public static LocString TITLE = "POINT OF INTEREST";

				// Token: 0x04009F22 RID: 40738
				public static LocString MASS_REMAINING = "<b>Total Mass Remaining</b>";

				// Token: 0x04009F23 RID: 40739
				public static LocString ROCKETS_AT_THIS_LOCATION = "<b>Rockets at this location</b>";

				// Token: 0x04009F24 RID: 40740
				public static LocString ARTIFACTS = "Artifact";

				// Token: 0x04009F25 RID: 40741
				public static LocString ARTIFACTS_AVAILABLE = "Available";

				// Token: 0x04009F26 RID: 40742
				public static LocString ARTIFACTS_DEPLETED = "Collected\nRecharge: {0}";
			}

			// Token: 0x02002469 RID: 9321
			public class ROCKETS
			{
				// Token: 0x02002ED5 RID: 11989
				public class SPEED
				{
					// Token: 0x0400BC9F RID: 48287
					public static LocString NAME = "Rocket Speed: ";

					// Token: 0x0400BCA0 RID: 48288
					public static LocString TOOLTIP = "<b>Rocket Speed</b> is calculated by dividing <b>Engine Power</b> by <b>Burden</b>.\n\nRockets operating on autopilot will have a reduced speed.\n\nRocket speed can be further increased by the skill of the Duplicant flying the rocket.";
				}

				// Token: 0x02002ED6 RID: 11990
				public class FUEL_REMAINING
				{
					// Token: 0x0400BCA1 RID: 48289
					public static LocString NAME = "Fuel Remaining: ";

					// Token: 0x0400BCA2 RID: 48290
					public static LocString TOOLTIP = "This rocket has {0} fuel in its tank";
				}

				// Token: 0x02002ED7 RID: 11991
				public class OXIDIZER_REMAINING
				{
					// Token: 0x0400BCA3 RID: 48291
					public static LocString NAME = "Oxidizer Power Remaining: ";

					// Token: 0x0400BCA4 RID: 48292
					public static LocString TOOLTIP = "This rocket has enough oxidizer in its tank for {0} of fuel";
				}

				// Token: 0x02002ED8 RID: 11992
				public class RANGE
				{
					// Token: 0x0400BCA5 RID: 48293
					public static LocString NAME = "Range Remaining: ";

					// Token: 0x0400BCA6 RID: 48294
					public static LocString TOOLTIP = "<b>Range remaining</b> is calculated by dividing the lesser of <b>fuel remaining</b> and <b>oxidizer power remaining</b> by <b>fuel consumed per tile</b>";
				}

				// Token: 0x02002ED9 RID: 11993
				public class FUEL_PER_HEX
				{
					// Token: 0x0400BCA7 RID: 48295
					public static LocString NAME = "Fuel consumed per Tile: {0}";

					// Token: 0x0400BCA8 RID: 48296
					public static LocString TOOLTIP = "This rocket can travel one tile per {0} of fuel";
				}

				// Token: 0x02002EDA RID: 11994
				public class BURDEN_TOTAL
				{
					// Token: 0x0400BCA9 RID: 48297
					public static LocString NAME = "Rocket burden: ";

					// Token: 0x0400BCAA RID: 48298
					public static LocString TOOLTIP = "The combined burden of all the modules in this rocket";
				}

				// Token: 0x02002EDB RID: 11995
				public class BURDEN_MODULE
				{
					// Token: 0x0400BCAB RID: 48299
					public static LocString NAME = "Module Burden: ";

					// Token: 0x0400BCAC RID: 48300
					public static LocString TOOLTIP = "The selected module adds {0} to the rocket's total " + DUPLICANTS.ATTRIBUTES.ROCKETBURDEN.NAME;
				}

				// Token: 0x02002EDC RID: 11996
				public class POWER_TOTAL
				{
					// Token: 0x0400BCAD RID: 48301
					public static LocString NAME = "Rocket engine power: ";

					// Token: 0x0400BCAE RID: 48302
					public static LocString TOOLTIP = "The total engine power added by all the modules in this rocket";
				}

				// Token: 0x02002EDD RID: 11997
				public class POWER_MODULE
				{
					// Token: 0x0400BCAF RID: 48303
					public static LocString NAME = "Module Engine Power: ";

					// Token: 0x0400BCB0 RID: 48304
					public static LocString TOOLTIP = "The selected module adds {0} to the rocket's total " + DUPLICANTS.ATTRIBUTES.ROCKETENGINEPOWER.NAME;
				}

				// Token: 0x02002EDE RID: 11998
				public class MODULE_STATS
				{
					// Token: 0x0400BCB1 RID: 48305
					public static LocString NAME = "Module Stats: ";

					// Token: 0x0400BCB2 RID: 48306
					public static LocString NAME_HEADER = "Module Stats";

					// Token: 0x0400BCB3 RID: 48307
					public static LocString TOOLTIP = "Properties of the selected module";
				}

				// Token: 0x02002EDF RID: 11999
				public class MAX_MODULES
				{
					// Token: 0x0400BCB4 RID: 48308
					public static LocString NAME = "Max Modules: ";

					// Token: 0x0400BCB5 RID: 48309
					public static LocString TOOLTIP = "The {0} can support {1} rocket modules, plus itself";
				}

				// Token: 0x02002EE0 RID: 12000
				public class MAX_HEIGHT
				{
					// Token: 0x0400BCB6 RID: 48310
					public static LocString NAME = "Height: {0}/{1}";

					// Token: 0x0400BCB7 RID: 48311
					public static LocString NAME_RAW = "Height: ";

					// Token: 0x0400BCB8 RID: 48312
					public static LocString NAME_MAX_SUPPORTED = "Maximum supported rocket height: ";

					// Token: 0x0400BCB9 RID: 48313
					public static LocString TOOLTIP = "The {0} can support a total rocket height {1}";
				}

				// Token: 0x02002EE1 RID: 12001
				public class ARTIFACT_MODULE
				{
					// Token: 0x0400BCBA RID: 48314
					public static LocString EMPTY = "Empty";
				}
			}
		}

		// Token: 0x02001C97 RID: 7319
		public class STARMAP
		{
			// Token: 0x0400817C RID: 33148
			public static LocString TITLE = "STARMAP";

			// Token: 0x0400817D RID: 33149
			public static LocString MANAGEMENT_BUTTON = "STARMAP";

			// Token: 0x0400817E RID: 33150
			public static LocString SUBROW = "•  {0}";

			// Token: 0x0400817F RID: 33151
			public static LocString UNKNOWN_DESTINATION = "Destination Unknown";

			// Token: 0x04008180 RID: 33152
			public static LocString ANALYSIS_AMOUNT = "Analysis {0} Complete";

			// Token: 0x04008181 RID: 33153
			public static LocString ANALYSIS_COMPLETE = "ANALYSIS COMPLETE";

			// Token: 0x04008182 RID: 33154
			public static LocString NO_ANALYZABLE_DESTINATION_SELECTED = "No destination selected";

			// Token: 0x04008183 RID: 33155
			public static LocString UNKNOWN_TYPE = "Type Unknown";

			// Token: 0x04008184 RID: 33156
			public static LocString DISTANCE = "{0} km";

			// Token: 0x04008185 RID: 33157
			public static LocString MODULE_MASS = "+ {0} t";

			// Token: 0x04008186 RID: 33158
			public static LocString MODULE_STORAGE = "{0} / {1}";

			// Token: 0x04008187 RID: 33159
			public static LocString ANALYSIS_DESCRIPTION = "Use a Telescope to analyze space destinations.\n\nCompleting analysis on an object will unlock rocket missions to that destination.";

			// Token: 0x04008188 RID: 33160
			public static LocString RESEARCH_DESCRIPTION = "Gather Interstellar Research Data using Research Modules.";

			// Token: 0x04008189 RID: 33161
			public static LocString ROCKET_RENAME_BUTTON_TOOLTIP = "Rename this rocket";

			// Token: 0x0400818A RID: 33162
			public static LocString NO_ROCKETS_HELP_TEXT = "Rockets allow you to visit nearby celestial bodies.\n\nEach rocket must have a Command Module, an Engine, and Fuel.\n\nYou can also carry other modules that allow you to gather specific resources from the places you visit.\n\nRemember the more weight a rocket has, the more limited it'll be on the distance it can travel. You can add more fuel to fix that, but fuel will add weight as well.";

			// Token: 0x0400818B RID: 33163
			public static LocString CONTAINER_REQUIRED = "{0} installation required to retrieve material";

			// Token: 0x0400818C RID: 33164
			public static LocString CAN_CARRY_ELEMENT = "Gathered by: {1}";

			// Token: 0x0400818D RID: 33165
			public static LocString CANT_CARRY_ELEMENT = "{0} installation required to retrieve material";

			// Token: 0x0400818E RID: 33166
			public static LocString STATUS = "SELECTED";

			// Token: 0x0400818F RID: 33167
			public static LocString DISTANCE_OVERLAY = "TOO FAR FOR THIS ROCKET";

			// Token: 0x04008190 RID: 33168
			public static LocString COMPOSITION_UNDISCOVERED = "?????????";

			// Token: 0x04008191 RID: 33169
			public static LocString COMPOSITION_UNDISCOVERED_TOOLTIP = "Further research required to identify resource\n\nSend a Research Module to this destination for more information";

			// Token: 0x04008192 RID: 33170
			public static LocString COMPOSITION_UNDISCOVERED_AMOUNT = "???";

			// Token: 0x04008193 RID: 33171
			public static LocString COMPOSITION_SMALL_AMOUNT = "Trace Amount";

			// Token: 0x04008194 RID: 33172
			public static LocString CURRENT_MASS = "Current Mass";

			// Token: 0x04008195 RID: 33173
			public static LocString CURRENT_MASS_TOOLTIP = "Warning: Missions to this destination will not return a full cargo load to avoid depleting the destination for future explorations\n\nDestination: {0} Resources Available\nRocket Capacity: {1}";

			// Token: 0x04008196 RID: 33174
			public static LocString MAXIMUM_MASS = "Maximum Mass";

			// Token: 0x04008197 RID: 33175
			public static LocString MINIMUM_MASS = "Minimum Mass";

			// Token: 0x04008198 RID: 33176
			public static LocString MINIMUM_MASS_TOOLTIP = "This destination must retain at least this much mass in order to prevent depletion and allow the future regeneration of resources.\n\nDuplicants will always maintain a destination's minimum mass requirements, potentially returning with less cargo than their rocket can hold";

			// Token: 0x04008199 RID: 33177
			public static LocString REPLENISH_RATE = "Replenished/Cycle:";

			// Token: 0x0400819A RID: 33178
			public static LocString REPLENISH_RATE_TOOLTIP = "The rate at which this destination regenerates resources";

			// Token: 0x0400819B RID: 33179
			public static LocString ROCKETLIST = "Rocket Hangar";

			// Token: 0x0400819C RID: 33180
			public static LocString NO_ROCKETS_TITLE = "NO ROCKETS";

			// Token: 0x0400819D RID: 33181
			public static LocString ROCKET_COUNT = "ROCKETS: {0}";

			// Token: 0x0400819E RID: 33182
			public static LocString LAUNCH_MISSION = "LAUNCH MISSION";

			// Token: 0x0400819F RID: 33183
			public static LocString CANT_LAUNCH_MISSION = "CANNOT LAUNCH";

			// Token: 0x040081A0 RID: 33184
			public static LocString LAUNCH_ROCKET = "Launch Rocket";

			// Token: 0x040081A1 RID: 33185
			public static LocString LAND_ROCKET = "Land Rocket";

			// Token: 0x040081A2 RID: 33186
			public static LocString SEE_ROCKETS_LIST = "See Rockets List";

			// Token: 0x040081A3 RID: 33187
			public static LocString DEFAULT_NAME = "Rocket";

			// Token: 0x040081A4 RID: 33188
			public static LocString ANALYZE_DESTINATION = "ANALYZE OBJECT";

			// Token: 0x040081A5 RID: 33189
			public static LocString SUSPEND_DESTINATION_ANALYSIS = "PAUSE ANALYSIS";

			// Token: 0x040081A6 RID: 33190
			public static LocString DESTINATIONTITLE = "Destination Status";

			// Token: 0x0200246A RID: 9322
			public class DESTINATIONSTUDY
			{
				// Token: 0x04009F27 RID: 40743
				public static LocString UPPERATMO = "Study upper atmosphere";

				// Token: 0x04009F28 RID: 40744
				public static LocString LOWERATMO = "Study lower atmosphere";

				// Token: 0x04009F29 RID: 40745
				public static LocString MAGNETICFIELD = "Study magnetic field";

				// Token: 0x04009F2A RID: 40746
				public static LocString SURFACE = "Study surface";

				// Token: 0x04009F2B RID: 40747
				public static LocString SUBSURFACE = "Study subsurface";
			}

			// Token: 0x0200246B RID: 9323
			public class COMPONENT
			{
				// Token: 0x04009F2C RID: 40748
				public static LocString FUEL_TANK = "Fuel Tank";

				// Token: 0x04009F2D RID: 40749
				public static LocString ROCKET_ENGINE = "Rocket Engine";

				// Token: 0x04009F2E RID: 40750
				public static LocString CARGO_BAY = "Cargo Bay";

				// Token: 0x04009F2F RID: 40751
				public static LocString OXIDIZER_TANK = "Oxidizer Tank";
			}

			// Token: 0x0200246C RID: 9324
			public class MISSION_STATUS
			{
				// Token: 0x04009F30 RID: 40752
				public static LocString GROUNDED = "Grounded";

				// Token: 0x04009F31 RID: 40753
				public static LocString LAUNCHING = "Launching";

				// Token: 0x04009F32 RID: 40754
				public static LocString WAITING_TO_LAND = "Waiting To Land";

				// Token: 0x04009F33 RID: 40755
				public static LocString LANDING = "Landing";

				// Token: 0x04009F34 RID: 40756
				public static LocString UNDERWAY = "Underway";

				// Token: 0x04009F35 RID: 40757
				public static LocString UNDERWAY_BOOSTED = "Underway <color=#5FDB37FF>(Boosted)</color>";

				// Token: 0x04009F36 RID: 40758
				public static LocString DESTROYED = "Destroyed";

				// Token: 0x04009F37 RID: 40759
				public static LocString GO = "ALL SYSTEMS GO";
			}

			// Token: 0x0200246D RID: 9325
			public class LISTTITLES
			{
				// Token: 0x04009F38 RID: 40760
				public static LocString MISSIONSTATUS = "Mission Status";

				// Token: 0x04009F39 RID: 40761
				public static LocString LAUNCHCHECKLIST = "Launch Checklist";

				// Token: 0x04009F3A RID: 40762
				public static LocString MAXRANGE = "Max Range";

				// Token: 0x04009F3B RID: 40763
				public static LocString MASS = "Mass";

				// Token: 0x04009F3C RID: 40764
				public static LocString STORAGE = "Storage";

				// Token: 0x04009F3D RID: 40765
				public static LocString FUEL = "Fuel";

				// Token: 0x04009F3E RID: 40766
				public static LocString OXIDIZER = "Oxidizer";

				// Token: 0x04009F3F RID: 40767
				public static LocString PASSENGERS = "Passengers";

				// Token: 0x04009F40 RID: 40768
				public static LocString RESEARCH = "Research";

				// Token: 0x04009F41 RID: 40769
				public static LocString ARTIFACTS = "Artifacts";

				// Token: 0x04009F42 RID: 40770
				public static LocString ANALYSIS = "Analysis";

				// Token: 0x04009F43 RID: 40771
				public static LocString WORLDCOMPOSITION = "World Composition";

				// Token: 0x04009F44 RID: 40772
				public static LocString RESOURCES = "Resources";

				// Token: 0x04009F45 RID: 40773
				public static LocString MODULES = "Modules";

				// Token: 0x04009F46 RID: 40774
				public static LocString TYPE = "Type";

				// Token: 0x04009F47 RID: 40775
				public static LocString DISTANCE = "Distance";

				// Token: 0x04009F48 RID: 40776
				public static LocString DESTINATION_MASS = "World Mass Available";

				// Token: 0x04009F49 RID: 40777
				public static LocString STORAGECAPACITY = "Storage Capacity";
			}

			// Token: 0x0200246E RID: 9326
			public class ROCKETWEIGHT
			{
				// Token: 0x04009F4A RID: 40778
				public static LocString MASS = "Mass: ";

				// Token: 0x04009F4B RID: 40779
				public static LocString MASSPENALTY = "Mass Penalty: ";

				// Token: 0x04009F4C RID: 40780
				public static LocString CURRENTMASS = "Current Rocket Mass: ";

				// Token: 0x04009F4D RID: 40781
				public static LocString CURRENTMASSPENALTY = "Current Weight Penalty: ";
			}

			// Token: 0x0200246F RID: 9327
			public class DESTINATIONSELECTION
			{
				// Token: 0x04009F4E RID: 40782
				public static LocString REACHABLE = "Destination set";

				// Token: 0x04009F4F RID: 40783
				public static LocString UNREACHABLE = "Destination set";

				// Token: 0x04009F50 RID: 40784
				public static LocString NOTSELECTED = "Destination set";
			}

			// Token: 0x02002470 RID: 9328
			public class DESTINATIONSELECTION_TOOLTIP
			{
				// Token: 0x04009F51 RID: 40785
				public static LocString REACHABLE = "Viable destination selected, ready for launch";

				// Token: 0x04009F52 RID: 40786
				public static LocString UNREACHABLE = "The selected destination is beyond rocket reach";

				// Token: 0x04009F53 RID: 40787
				public static LocString NOTSELECTED = "Select the rocket's Command Module to set a destination";
			}

			// Token: 0x02002471 RID: 9329
			public class HASFOOD
			{
				// Token: 0x04009F54 RID: 40788
				public static LocString NAME = "Food Loaded";

				// Token: 0x04009F55 RID: 40789
				public static LocString TOOLTIP = "Sufficient food stores have been loaded, ready for launch";
			}

			// Token: 0x02002472 RID: 9330
			public class HASSUIT
			{
				// Token: 0x04009F56 RID: 40790
				public static LocString NAME = "Has " + EQUIPMENT.PREFABS.ATMO_SUIT.NAME;

				// Token: 0x04009F57 RID: 40791
				public static LocString TOOLTIP = "An " + EQUIPMENT.PREFABS.ATMO_SUIT.NAME + " has been loaded";
			}

			// Token: 0x02002473 RID: 9331
			public class NOSUIT
			{
				// Token: 0x04009F58 RID: 40792
				public static LocString NAME = "Missing " + EQUIPMENT.PREFABS.ATMO_SUIT.NAME;

				// Token: 0x04009F59 RID: 40793
				public static LocString TOOLTIP = "Rocket cannot launch without an " + EQUIPMENT.PREFABS.ATMO_SUIT.NAME + " loaded";
			}

			// Token: 0x02002474 RID: 9332
			public class NOFOOD
			{
				// Token: 0x04009F5A RID: 40794
				public static LocString NAME = "Insufficient Food";

				// Token: 0x04009F5B RID: 40795
				public static LocString TOOLTIP = "Rocket cannot launch without adequate food stores for passengers";
			}

			// Token: 0x02002475 RID: 9333
			public class CARGOEMPTY
			{
				// Token: 0x04009F5C RID: 40796
				public static LocString NAME = "Emptied Cargo Bay";

				// Token: 0x04009F5D RID: 40797
				public static LocString TOOLTIP = "Cargo Bays must be emptied of all materials before launch";
			}

			// Token: 0x02002476 RID: 9334
			public class LAUNCHCHECKLIST
			{
				// Token: 0x04009F5E RID: 40798
				public static LocString ASTRONAUT_TITLE = "Astronaut";

				// Token: 0x04009F5F RID: 40799
				public static LocString HASASTRONAUT = "Astronaut ready for liftoff";

				// Token: 0x04009F60 RID: 40800
				public static LocString ASTRONAUGHT = "No Astronaut assigned";

				// Token: 0x04009F61 RID: 40801
				public static LocString INSTALLED = "Installed";

				// Token: 0x04009F62 RID: 40802
				public static LocString INSTALLED_TOOLTIP = "A suitable {0} has been installed";

				// Token: 0x04009F63 RID: 40803
				public static LocString REQUIRED = "Required";

				// Token: 0x04009F64 RID: 40804
				public static LocString REQUIRED_TOOLTIP = "A {0} must be installed before launch";

				// Token: 0x04009F65 RID: 40805
				public static LocString MISSING_TOOLTIP = "No {0} installed\n\nThis rocket cannot launch without a completed {0}";

				// Token: 0x04009F66 RID: 40806
				public static LocString NO_DESTINATION = "No destination selected";

				// Token: 0x04009F67 RID: 40807
				public static LocString MINIMUM_MASS = "Resources available {0}";

				// Token: 0x04009F68 RID: 40808
				public static LocString RESOURCE_MASS_TOOLTIP = "{0} has {1} resources available\nThis rocket has capacity for {2}";

				// Token: 0x04009F69 RID: 40809
				public static LocString INSUFFICENT_MASS_TOOLTIP = "Launching to this destination will not return a full cargo load";

				// Token: 0x02002EE2 RID: 12002
				public class CONSTRUCTION_COMPLETE
				{
					// Token: 0x020030DB RID: 12507
					public class STATUS
					{
						// Token: 0x0400C239 RID: 49721
						public static LocString READY = "No active construction";

						// Token: 0x0400C23A RID: 49722
						public static LocString FAILURE = "No active construction";

						// Token: 0x0400C23B RID: 49723
						public static LocString WARNING = "No active construction";
					}

					// Token: 0x020030DC RID: 12508
					public class TOOLTIP
					{
						// Token: 0x0400C23C RID: 49724
						public static LocString READY = "Construction of all modules is complete";

						// Token: 0x0400C23D RID: 49725
						public static LocString FAILURE = "In-progress module construction is preventing takeoff";

						// Token: 0x0400C23E RID: 49726
						public static LocString WARNING = "Construction warning";
					}
				}

				// Token: 0x02002EE3 RID: 12003
				public class PILOT_BOARDED
				{
					// Token: 0x0400BCBB RID: 48315
					public static LocString READY = "Pilot boarded";

					// Token: 0x0400BCBC RID: 48316
					public static LocString FAILURE = "Pilot boarded";

					// Token: 0x0400BCBD RID: 48317
					public static LocString WARNING = "Pilot boarded";

					// Token: 0x020030DD RID: 12509
					public class TOOLTIP
					{
						// Token: 0x0400C23F RID: 49727
						public static LocString READY = "A Duplicant with the " + DUPLICANTS.ROLES.ROCKETPILOT.NAME + " skill is currently onboard";

						// Token: 0x0400C240 RID: 49728
						public static LocString FAILURE = "At least one crew member aboard the rocket must possess the " + DUPLICANTS.ROLES.ROCKETPILOT.NAME + " skill to launch\n\nQualified Duplicants must be assigned to the rocket crew, and have access to the module's hatch";

						// Token: 0x0400C241 RID: 49729
						public static LocString WARNING = "Pilot warning";
					}
				}

				// Token: 0x02002EE4 RID: 12004
				public class CREW_BOARDED
				{
					// Token: 0x0400BCBE RID: 48318
					public static LocString READY = "All crew boarded";

					// Token: 0x0400BCBF RID: 48319
					public static LocString FAILURE = "All crew boarded";

					// Token: 0x0400BCC0 RID: 48320
					public static LocString WARNING = "All crew boarded";

					// Token: 0x020030DE RID: 12510
					public class TOOLTIP
					{
						// Token: 0x0400C242 RID: 49730
						public static LocString READY = "All Duplicants assigned to the rocket crew are boarded and ready for launch\n\n    • {0}/{1} Boarded";

						// Token: 0x0400C243 RID: 49731
						public static LocString FAILURE = "No crew members have boarded this rocket\n\nDuplicants must be assigned to the rocket crew and have access to the module's hatch to board\n\n    • {0}/{1} Boarded";

						// Token: 0x0400C244 RID: 49732
						public static LocString WARNING = "Some Duplicants assigned to this rocket crew have not yet boarded\n\n    • {0}/{1} Boarded";

						// Token: 0x0400C245 RID: 49733
						public static LocString NONE = "There are no Duplicants assigned to this rocket crew\n\n    • {0}/{1} Boarded";
					}
				}

				// Token: 0x02002EE5 RID: 12005
				public class NO_EXTRA_PASSENGERS
				{
					// Token: 0x0400BCC1 RID: 48321
					public static LocString READY = "Non-crew exited";

					// Token: 0x0400BCC2 RID: 48322
					public static LocString FAILURE = "Non-crew exited";

					// Token: 0x0400BCC3 RID: 48323
					public static LocString WARNING = "Non-crew exited";

					// Token: 0x020030DF RID: 12511
					public class TOOLTIP
					{
						// Token: 0x0400C246 RID: 49734
						public static LocString READY = "All non-crew Duplicants have disembarked";

						// Token: 0x0400C247 RID: 49735
						public static LocString FAILURE = "Non-crew Duplicants must exit the rocket before launch";

						// Token: 0x0400C248 RID: 49736
						public static LocString WARNING = "Non-crew warning";
					}
				}

				// Token: 0x02002EE6 RID: 12006
				public class FLIGHT_PATH_CLEAR
				{
					// Token: 0x020030E0 RID: 12512
					public class STATUS
					{
						// Token: 0x0400C249 RID: 49737
						public static LocString READY = "Clear launch path";

						// Token: 0x0400C24A RID: 49738
						public static LocString FAILURE = "Clear launch path";

						// Token: 0x0400C24B RID: 49739
						public static LocString WARNING = "Clear launch path";
					}

					// Token: 0x020030E1 RID: 12513
					public class TOOLTIP
					{
						// Token: 0x0400C24C RID: 49740
						public static LocString READY = "The rocket's launch path is clear for takeoff";

						// Token: 0x0400C24D RID: 49741
						public static LocString FAILURE = "This rocket does not have a clear line of sight to space, preventing launch\n\nThe rocket's launch path can be cleared by excavating undug tiles and deconstructing any buildings above the rocket";

						// Token: 0x0400C24E RID: 49742
						public static LocString WARNING = "";
					}
				}

				// Token: 0x02002EE7 RID: 12007
				public class HAS_FUEL_TANK
				{
					// Token: 0x020030E2 RID: 12514
					public class STATUS
					{
						// Token: 0x0400C24F RID: 49743
						public static LocString READY = "Fuel Tank";

						// Token: 0x0400C250 RID: 49744
						public static LocString FAILURE = "Fuel Tank";

						// Token: 0x0400C251 RID: 49745
						public static LocString WARNING = "Fuel Tank";
					}

					// Token: 0x020030E3 RID: 12515
					public class TOOLTIP
					{
						// Token: 0x0400C252 RID: 49746
						public static LocString READY = "A fuel tank has been installed";

						// Token: 0x0400C253 RID: 49747
						public static LocString FAILURE = "No fuel tank installed\n\nThis rocket cannot launch without a completed fuel tank";

						// Token: 0x0400C254 RID: 49748
						public static LocString WARNING = "Fuel tank warning";
					}
				}

				// Token: 0x02002EE8 RID: 12008
				public class HAS_ENGINE
				{
					// Token: 0x020030E4 RID: 12516
					public class STATUS
					{
						// Token: 0x0400C255 RID: 49749
						public static LocString READY = "Engine";

						// Token: 0x0400C256 RID: 49750
						public static LocString FAILURE = "Engine";

						// Token: 0x0400C257 RID: 49751
						public static LocString WARNING = "Engine";
					}

					// Token: 0x020030E5 RID: 12517
					public class TOOLTIP
					{
						// Token: 0x0400C258 RID: 49752
						public static LocString READY = "A suitable engine has been installed";

						// Token: 0x0400C259 RID: 49753
						public static LocString FAILURE = "No engine installed\n\nThis rocket cannot launch without a completed engine";

						// Token: 0x0400C25A RID: 49754
						public static LocString WARNING = "Engine warning";
					}
				}

				// Token: 0x02002EE9 RID: 12009
				public class HAS_NOSECONE
				{
					// Token: 0x020030E6 RID: 12518
					public class STATUS
					{
						// Token: 0x0400C25B RID: 49755
						public static LocString READY = "Nosecone";

						// Token: 0x0400C25C RID: 49756
						public static LocString FAILURE = "Nosecone";

						// Token: 0x0400C25D RID: 49757
						public static LocString WARNING = "Nosecone";
					}

					// Token: 0x020030E7 RID: 12519
					public class TOOLTIP
					{
						// Token: 0x0400C25E RID: 49758
						public static LocString READY = "A suitable nosecone has been installed";

						// Token: 0x0400C25F RID: 49759
						public static LocString FAILURE = "No nosecone installed\n\nThis rocket cannot launch without a completed nosecone";

						// Token: 0x0400C260 RID: 49760
						public static LocString WARNING = "Nosecone warning";
					}
				}

				// Token: 0x02002EEA RID: 12010
				public class HAS_CONTROLSTATION
				{
					// Token: 0x020030E8 RID: 12520
					public class STATUS
					{
						// Token: 0x0400C261 RID: 49761
						public static LocString READY = "Control Station";

						// Token: 0x0400C262 RID: 49762
						public static LocString FAILURE = "Control Station";

						// Token: 0x0400C263 RID: 49763
						public static LocString WARNING = "Control Station";
					}

					// Token: 0x020030E9 RID: 12521
					public class TOOLTIP
					{
						// Token: 0x0400C264 RID: 49764
						public static LocString READY = "The control station is installed and waiting for the pilot";

						// Token: 0x0400C265 RID: 49765
						public static LocString FAILURE = "No Control Station\n\nA new Rocket Control Station must be installed inside the rocket";

						// Token: 0x0400C266 RID: 49766
						public static LocString WARNING = "Control Station warning";
					}
				}

				// Token: 0x02002EEB RID: 12011
				public class LOADING_COMPLETE
				{
					// Token: 0x020030EA RID: 12522
					public class STATUS
					{
						// Token: 0x0400C267 RID: 49767
						public static LocString READY = "Cargo Loading Complete";

						// Token: 0x0400C268 RID: 49768
						public static LocString FAILURE = "";

						// Token: 0x0400C269 RID: 49769
						public static LocString WARNING = "Cargo Loading Complete";
					}

					// Token: 0x020030EB RID: 12523
					public class TOOLTIP
					{
						// Token: 0x0400C26A RID: 49770
						public static LocString READY = "All possible loading and unloading has been completed";

						// Token: 0x0400C26B RID: 49771
						public static LocString FAILURE = "";

						// Token: 0x0400C26C RID: 49772
						public static LocString WARNING = "The " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + " could still transfer cargo to or from this rocket";
					}
				}

				// Token: 0x02002EEC RID: 12012
				public class CARGO_TRANSFER_COMPLETE
				{
					// Token: 0x020030EC RID: 12524
					public class STATUS
					{
						// Token: 0x0400C26D RID: 49773
						public static LocString READY = "Cargo Transfer Complete";

						// Token: 0x0400C26E RID: 49774
						public static LocString FAILURE = "";

						// Token: 0x0400C26F RID: 49775
						public static LocString WARNING = "Cargo Transfer Complete";
					}

					// Token: 0x020030ED RID: 12525
					public class TOOLTIP
					{
						// Token: 0x0400C270 RID: 49776
						public static LocString READY = "All possible loading and unloading has been completed";

						// Token: 0x0400C271 RID: 49777
						public static LocString FAILURE = "";

						// Token: 0x0400C272 RID: 49778
						public static LocString WARNING = "The " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + " could still transfer cargo to or from this rocket";
					}
				}

				// Token: 0x02002EED RID: 12013
				public class INTERNAL_CONSTRUCTION_COMPLETE
				{
					// Token: 0x020030EE RID: 12526
					public class STATUS
					{
						// Token: 0x0400C273 RID: 49779
						public static LocString READY = "Landers Ready";

						// Token: 0x0400C274 RID: 49780
						public static LocString FAILURE = "Landers Ready";

						// Token: 0x0400C275 RID: 49781
						public static LocString WARNING = "";
					}

					// Token: 0x020030EF RID: 12527
					public class TOOLTIP
					{
						// Token: 0x0400C276 RID: 49782
						public static LocString READY = "All requested landers have been built and are ready for deployment";

						// Token: 0x0400C277 RID: 49783
						public static LocString FAILURE = "Additional landers must be constructed to fulfill the lander requests of this rocket";

						// Token: 0x0400C278 RID: 49784
						public static LocString WARNING = "";
					}
				}

				// Token: 0x02002EEE RID: 12014
				public class MAX_MODULES
				{
					// Token: 0x020030F0 RID: 12528
					public class STATUS
					{
						// Token: 0x0400C279 RID: 49785
						public static LocString READY = "Module limit";

						// Token: 0x0400C27A RID: 49786
						public static LocString FAILURE = "Module limit";

						// Token: 0x0400C27B RID: 49787
						public static LocString WARNING = "Module limit";
					}

					// Token: 0x020030F1 RID: 12529
					public class TOOLTIP
					{
						// Token: 0x0400C27C RID: 49788
						public static LocString READY = "The rocket's engine can support the number of installed rocket modules";

						// Token: 0x0400C27D RID: 49789
						public static LocString FAILURE = "The number of installed modules exceeds the engine's module limit\n\nExcess modules must be removed";

						// Token: 0x0400C27E RID: 49790
						public static LocString WARNING = "Module limit warning";
					}
				}

				// Token: 0x02002EEF RID: 12015
				public class HAS_RESOURCE
				{
					// Token: 0x020030F2 RID: 12530
					public class STATUS
					{
						// Token: 0x0400C27F RID: 49791
						public static LocString READY = "{0} {1} supplied";

						// Token: 0x0400C280 RID: 49792
						public static LocString FAILURE = "{0} missing {1}";

						// Token: 0x0400C281 RID: 49793
						public static LocString WARNING = "{0} missing {1}";
					}

					// Token: 0x020030F3 RID: 12531
					public class TOOLTIP
					{
						// Token: 0x0400C282 RID: 49794
						public static LocString READY = "{0} {1} supplied";

						// Token: 0x0400C283 RID: 49795
						public static LocString FAILURE = "{0} has less than {1} {2}";

						// Token: 0x0400C284 RID: 49796
						public static LocString WARNING = "{0} has less than {1} {2}";
					}
				}

				// Token: 0x02002EF0 RID: 12016
				public class MAX_HEIGHT
				{
					// Token: 0x020030F4 RID: 12532
					public class STATUS
					{
						// Token: 0x0400C285 RID: 49797
						public static LocString READY = "Height limit";

						// Token: 0x0400C286 RID: 49798
						public static LocString FAILURE = "Height limit";

						// Token: 0x0400C287 RID: 49799
						public static LocString WARNING = "Height limit";
					}

					// Token: 0x020030F5 RID: 12533
					public class TOOLTIP
					{
						// Token: 0x0400C288 RID: 49800
						public static LocString READY = "The rocket's engine can support the height of the rocket";

						// Token: 0x0400C289 RID: 49801
						public static LocString FAILURE = "The height of the rocket exceeds the engine's limit\n\nExcess modules must be removed";

						// Token: 0x0400C28A RID: 49802
						public static LocString WARNING = "Height limit warning";
					}
				}

				// Token: 0x02002EF1 RID: 12017
				public class PROPERLY_FUELED
				{
					// Token: 0x020030F6 RID: 12534
					public class STATUS
					{
						// Token: 0x0400C28B RID: 49803
						public static LocString READY = "Fueled";

						// Token: 0x0400C28C RID: 49804
						public static LocString FAILURE = "Fueled";

						// Token: 0x0400C28D RID: 49805
						public static LocString WARNING = "Fueled";
					}

					// Token: 0x020030F7 RID: 12535
					public class TOOLTIP
					{
						// Token: 0x0400C28E RID: 49806
						public static LocString READY = "The rocket is sufficiently fueled for a roundtrip to its destination and back";

						// Token: 0x0400C28F RID: 49807
						public static LocString READY_NO_DESTINATION = "This rocket's fuel tanks have been filled to capacity, but it has no destination";

						// Token: 0x0400C290 RID: 49808
						public static LocString FAILURE = "This rocket does not have enough fuel to reach its destination\n\nIf the tanks are full, a different Fuel Tank Module may be required";

						// Token: 0x0400C291 RID: 49809
						public static LocString WARNING = "The rocket has enough fuel for a one-way trip to its destination, but will not be able to make it back";
					}
				}

				// Token: 0x02002EF2 RID: 12018
				public class SUFFICIENT_OXIDIZER
				{
					// Token: 0x020030F8 RID: 12536
					public class STATUS
					{
						// Token: 0x0400C292 RID: 49810
						public static LocString READY = "Sufficient Oxidizer";

						// Token: 0x0400C293 RID: 49811
						public static LocString FAILURE = "Sufficient Oxidizer";

						// Token: 0x0400C294 RID: 49812
						public static LocString WARNING = "Warning: Limited oxidizer";
					}

					// Token: 0x020030F9 RID: 12537
					public class TOOLTIP
					{
						// Token: 0x0400C295 RID: 49813
						public static LocString READY = "This rocket has sufficient oxidizer for a roundtrip to its destination and back";

						// Token: 0x0400C296 RID: 49814
						public static LocString FAILURE = "This rocket does not have enough oxidizer to reach its destination\n\nIf the oxidizer tanks are full, a different Oxidizer Tank Module may be required";

						// Token: 0x0400C297 RID: 49815
						public static LocString WARNING = "The rocket has enough oxidizer for a one-way trip to its destination, but will not be able to make it back";
					}
				}

				// Token: 0x02002EF3 RID: 12019
				public class ON_LAUNCHPAD
				{
					// Token: 0x020030FA RID: 12538
					public class STATUS
					{
						// Token: 0x0400C298 RID: 49816
						public static LocString READY = "On a launch pad";

						// Token: 0x0400C299 RID: 49817
						public static LocString FAILURE = "Not on a launch pad";

						// Token: 0x0400C29A RID: 49818
						public static LocString WARNING = "No launch pad";
					}

					// Token: 0x020030FB RID: 12539
					public class TOOLTIP
					{
						// Token: 0x0400C29B RID: 49819
						public static LocString READY = "On a launch pad";

						// Token: 0x0400C29C RID: 49820
						public static LocString FAILURE = "Not on a launch pad";

						// Token: 0x0400C29D RID: 49821
						public static LocString WARNING = "No launch pad";
					}
				}
			}

			// Token: 0x02002477 RID: 9335
			public class FULLTANK
			{
				// Token: 0x04009F6A RID: 40810
				public static LocString NAME = "Fuel Tank full";

				// Token: 0x04009F6B RID: 40811
				public static LocString TOOLTIP = "Tank is full, ready for launch";
			}

			// Token: 0x02002478 RID: 9336
			public class EMPTYTANK
			{
				// Token: 0x04009F6C RID: 40812
				public static LocString NAME = "Fuel Tank not full";

				// Token: 0x04009F6D RID: 40813
				public static LocString TOOLTIP = "Fuel tank must be filled before launch";
			}

			// Token: 0x02002479 RID: 9337
			public class FULLOXIDIZERTANK
			{
				// Token: 0x04009F6E RID: 40814
				public static LocString NAME = "Oxidizer Tank full";

				// Token: 0x04009F6F RID: 40815
				public static LocString TOOLTIP = "Tank is full, ready for launch";
			}

			// Token: 0x0200247A RID: 9338
			public class EMPTYOXIDIZERTANK
			{
				// Token: 0x04009F70 RID: 40816
				public static LocString NAME = "Oxidizer Tank not full";

				// Token: 0x04009F71 RID: 40817
				public static LocString TOOLTIP = "Oxidizer tank must be filled before launch";
			}

			// Token: 0x0200247B RID: 9339
			public class ROCKETSTATUS
			{
				// Token: 0x04009F72 RID: 40818
				public static LocString STATUS_TITLE = "Rocket Status";

				// Token: 0x04009F73 RID: 40819
				public static LocString NONE = "NONE";

				// Token: 0x04009F74 RID: 40820
				public static LocString SELECTED = "SELECTED";

				// Token: 0x04009F75 RID: 40821
				public static LocString LOCKEDIN = "LOCKED IN";

				// Token: 0x04009F76 RID: 40822
				public static LocString NODESTINATION = "No destination selected";

				// Token: 0x04009F77 RID: 40823
				public static LocString DESTINATIONVALUE = "None";

				// Token: 0x04009F78 RID: 40824
				public static LocString NOPASSENGERS = "No passengers";

				// Token: 0x04009F79 RID: 40825
				public static LocString STATUS = "Status";

				// Token: 0x04009F7A RID: 40826
				public static LocString TOTAL = "Total";

				// Token: 0x04009F7B RID: 40827
				public static LocString WEIGHTPENALTY = "Weight Penalty";

				// Token: 0x04009F7C RID: 40828
				public static LocString TIMEREMAINING = "Time Remaining";

				// Token: 0x04009F7D RID: 40829
				public static LocString BOOSTED_TIME_MODIFIER = "Less Than ";
			}

			// Token: 0x0200247C RID: 9340
			public class ROCKETSTATS
			{
				// Token: 0x04009F7E RID: 40830
				public static LocString TOTAL_OXIDIZABLE_FUEL = "Total oxidizable fuel";

				// Token: 0x04009F7F RID: 40831
				public static LocString TOTAL_OXIDIZER = "Total oxidizer";

				// Token: 0x04009F80 RID: 40832
				public static LocString TOTAL_FUEL = "Total fuel";

				// Token: 0x04009F81 RID: 40833
				public static LocString NO_ENGINE = "NO ENGINE";

				// Token: 0x04009F82 RID: 40834
				public static LocString ENGINE_EFFICIENCY = "Main engine efficiency";

				// Token: 0x04009F83 RID: 40835
				public static LocString OXIDIZER_EFFICIENCY = "Average oxidizer efficiency";

				// Token: 0x04009F84 RID: 40836
				public static LocString SOLID_BOOSTER = "Solid boosters";

				// Token: 0x04009F85 RID: 40837
				public static LocString TOTAL_THRUST = "Total thrust";

				// Token: 0x04009F86 RID: 40838
				public static LocString TOTAL_RANGE = "Total range";

				// Token: 0x04009F87 RID: 40839
				public static LocString DRY_MASS = "Dry mass";

				// Token: 0x04009F88 RID: 40840
				public static LocString WET_MASS = "Wet mass";
			}

			// Token: 0x0200247D RID: 9341
			public class STORAGESTATS
			{
				// Token: 0x04009F89 RID: 40841
				public static LocString STORAGECAPACITY = "{0} / {1}";
			}
		}

		// Token: 0x02001C98 RID: 7320
		public class RESEARCHSCREEN
		{
			// Token: 0x0200247E RID: 9342
			public class FILTER_BUTTONS
			{
				// Token: 0x04009F8A RID: 40842
				public static LocString HEADER = "Preset Filters";

				// Token: 0x04009F8B RID: 40843
				public static LocString ALL = "All";

				// Token: 0x04009F8C RID: 40844
				public static LocString AVAILABLE = "Next";

				// Token: 0x04009F8D RID: 40845
				public static LocString COMPLETED = "Completed";

				// Token: 0x04009F8E RID: 40846
				public static LocString OXYGEN = "Oxygen";

				// Token: 0x04009F8F RID: 40847
				public static LocString FOOD = "Food";

				// Token: 0x04009F90 RID: 40848
				public static LocString WATER = "Water";

				// Token: 0x04009F91 RID: 40849
				public static LocString POWER = "Power";

				// Token: 0x04009F92 RID: 40850
				public static LocString MORALE = "Morale";

				// Token: 0x04009F93 RID: 40851
				public static LocString RANCHING = "Ranching";

				// Token: 0x04009F94 RID: 40852
				public static LocString FILTER = "Filters";

				// Token: 0x04009F95 RID: 40853
				public static LocString TILE = "Tiles";

				// Token: 0x04009F96 RID: 40854
				public static LocString TRANSPORT = "Transport";

				// Token: 0x04009F97 RID: 40855
				public static LocString AUTOMATION = "Automation";

				// Token: 0x04009F98 RID: 40856
				public static LocString MEDICINE = "Medicine";

				// Token: 0x04009F99 RID: 40857
				public static LocString ROCKET = "Rockets";

				// Token: 0x04009F9A RID: 40858
				public static LocString RADIATION = "Radiation";
			}
		}

		// Token: 0x02001C99 RID: 7321
		public class CODEX
		{
			// Token: 0x040081A7 RID: 33191
			public static LocString SEARCH_HEADER = "Search Database";

			// Token: 0x040081A8 RID: 33192
			public static LocString BACK_BUTTON = "Back ({0})";

			// Token: 0x040081A9 RID: 33193
			public static LocString TIPS = "Tips";

			// Token: 0x040081AA RID: 33194
			public static LocString GAME_SYSTEMS = "Systems";

			// Token: 0x040081AB RID: 33195
			public static LocString DETAILS = "Details";

			// Token: 0x040081AC RID: 33196
			public static LocString RECIPE_ITEM = "{0} x {1}{2}";

			// Token: 0x040081AD RID: 33197
			public static LocString RECIPE_FABRICATOR = "{1} ({0} seconds)";

			// Token: 0x040081AE RID: 33198
			public static LocString RECIPE_FABRICATOR_HEADER = "Produced by";

			// Token: 0x040081AF RID: 33199
			public static LocString BACK_BUTTON_TOOLTIP = UI.CLICK(UI.ClickType.Click) + " to go back:\n{0}";

			// Token: 0x040081B0 RID: 33200
			public static LocString BACK_BUTTON_NO_HISTORY_TOOLTIP = UI.CLICK(UI.ClickType.Click) + " to go back:\nN/A";

			// Token: 0x040081B1 RID: 33201
			public static LocString FORWARD_BUTTON_TOOLTIP = UI.CLICK(UI.ClickType.Click) + " to go forward:\n{0}";

			// Token: 0x040081B2 RID: 33202
			public static LocString FORWARD_BUTTON_NO_HISTORY_TOOLTIP = UI.CLICK(UI.ClickType.Click) + " to go forward:\nN/A";

			// Token: 0x040081B3 RID: 33203
			public static LocString TITLE = "DATABASE";

			// Token: 0x040081B4 RID: 33204
			public static LocString MANAGEMENT_BUTTON = "DATABASE";

			// Token: 0x0200247F RID: 9343
			public class CODEX_DISCOVERED_MESSAGE
			{
				// Token: 0x04009F9B RID: 40859
				public static LocString TITLE = "New Log Entry";

				// Token: 0x04009F9C RID: 40860
				public static LocString BODY = "I've added a new entry to my log: {codex}\n";
			}

			// Token: 0x02002480 RID: 9344
			public class SUBWORLDS
			{
				// Token: 0x04009F9D RID: 40861
				public static LocString ELEMENTS = "Elements";

				// Token: 0x04009F9E RID: 40862
				public static LocString PLANTS = "Plants";

				// Token: 0x04009F9F RID: 40863
				public static LocString CRITTERS = "Critters";

				// Token: 0x04009FA0 RID: 40864
				public static LocString NONE = "None";
			}

			// Token: 0x02002481 RID: 9345
			public class GEYSERS
			{
				// Token: 0x04009FA1 RID: 40865
				public static LocString DESC = "Geysers and Fumaroles emit elements at variable intervals. They provide a sustainable source of material, albeit in typically low volumes.\n\nThe variable factors of a geyser are:\n\n    • Emission element \n    • Emission temperature \n    • Emission mass \n    • Cycle length \n    • Dormancy duration \n    • Disease emitted";
			}

			// Token: 0x02002482 RID: 9346
			public class EQUIPMENT
			{
				// Token: 0x04009FA2 RID: 40866
				public static LocString DESC = "Equipment description";
			}

			// Token: 0x02002483 RID: 9347
			public class FOOD
			{
				// Token: 0x04009FA3 RID: 40867
				public static LocString QUALITY = "Quality: {0}";

				// Token: 0x04009FA4 RID: 40868
				public static LocString CALORIES = "Calories: {0}";

				// Token: 0x04009FA5 RID: 40869
				public static LocString SPOILPROPERTIES = "Refrigeration temperature: {0}\nDeep Freeze temperature: {1}\nSpoil time: {2}";

				// Token: 0x04009FA6 RID: 40870
				public static LocString NON_PERISHABLE = "Spoil time: Never";
			}

			// Token: 0x02002484 RID: 9348
			public class CATEGORYNAMES
			{
				// Token: 0x04009FA7 RID: 40871
				public static LocString ROOT = UI.FormatAsLink("Index", "HOME");

				// Token: 0x04009FA8 RID: 40872
				public static LocString PLANTS = UI.FormatAsLink("Plants", "PLANTS");

				// Token: 0x04009FA9 RID: 40873
				public static LocString CREATURES = UI.FormatAsLink("Critters", "CREATURES");

				// Token: 0x04009FAA RID: 40874
				public static LocString EMAILS = UI.FormatAsLink("E-mail", "EMAILS");

				// Token: 0x04009FAB RID: 40875
				public static LocString JOURNALS = UI.FormatAsLink("Journals", "JOURNALS");

				// Token: 0x04009FAC RID: 40876
				public static LocString MYLOG = UI.FormatAsLink("My Log", "MYLOG");

				// Token: 0x04009FAD RID: 40877
				public static LocString INVESTIGATIONS = UI.FormatAsLink("Investigations", "Investigations");

				// Token: 0x04009FAE RID: 40878
				public static LocString RESEARCHNOTES = UI.FormatAsLink("Research Notes", "RESEARCHNOTES");

				// Token: 0x04009FAF RID: 40879
				public static LocString NOTICES = UI.FormatAsLink("Notices", "NOTICES");

				// Token: 0x04009FB0 RID: 40880
				public static LocString FOOD = UI.FormatAsLink("Food", "FOOD");

				// Token: 0x04009FB1 RID: 40881
				public static LocString MINION_MODIFIERS = UI.FormatAsLink("Duplicant Effects (EDITOR ONLY)", "MINION_MODIFIERS");

				// Token: 0x04009FB2 RID: 40882
				public static LocString BUILDINGS = UI.FormatAsLink("Buildings", "BUILDINGS");

				// Token: 0x04009FB3 RID: 40883
				public static LocString ROOMS = UI.FormatAsLink("Rooms", "ROOMS");

				// Token: 0x04009FB4 RID: 40884
				public static LocString TECH = UI.FormatAsLink("Research", "TECH");

				// Token: 0x04009FB5 RID: 40885
				public static LocString TIPS = UI.FormatAsLink("Lessons", "LESSONS");

				// Token: 0x04009FB6 RID: 40886
				public static LocString EQUIPMENT = UI.FormatAsLink("Equipment", "EQUIPMENT");

				// Token: 0x04009FB7 RID: 40887
				public static LocString BIOMES = UI.FormatAsLink("Biomes", "BIOMES");

				// Token: 0x04009FB8 RID: 40888
				public static LocString STORYTRAITS = UI.FormatAsLink("Story Traits", "STORYTRAITS");

				// Token: 0x04009FB9 RID: 40889
				public static LocString VIDEOS = UI.FormatAsLink("Videos", "VIDEOS");

				// Token: 0x04009FBA RID: 40890
				public static LocString MISCELLANEOUSTIPS = UI.FormatAsLink("Tips", "MISCELLANEOUSTIPS");

				// Token: 0x04009FBB RID: 40891
				public static LocString MISCELLANEOUSITEMS = UI.FormatAsLink("Items", "MISCELLANEOUSITEMS");

				// Token: 0x04009FBC RID: 40892
				public static LocString ELEMENTS = UI.FormatAsLink("Elements", "ELEMENTS");

				// Token: 0x04009FBD RID: 40893
				public static LocString ELEMENTSSOLID = UI.FormatAsLink("Solids", "ELEMENTS_SOLID");

				// Token: 0x04009FBE RID: 40894
				public static LocString ELEMENTSGAS = UI.FormatAsLink("Gases", "ELEMENTS_GAS");

				// Token: 0x04009FBF RID: 40895
				public static LocString ELEMENTSLIQUID = UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID");

				// Token: 0x04009FC0 RID: 40896
				public static LocString ELEMENTSOTHER = UI.FormatAsLink("Other", "ELEMENTS_OTHER");

				// Token: 0x04009FC1 RID: 40897
				public static LocString ELEMENTSCLASSES = UI.FormatAsLink("Classes", "ELEMENTS_CLASSES");

				// Token: 0x04009FC2 RID: 40898
				public static LocString INDUSTRIALINGREDIENTS = UI.FormatAsLink("Industrial Ingredients", "INDUSTRIALINGREDIENTS");

				// Token: 0x04009FC3 RID: 40899
				public static LocString GEYSERS = UI.FormatAsLink("Geysers", "GEYSERS");

				// Token: 0x04009FC4 RID: 40900
				public static LocString SYSTEMS = UI.FormatAsLink("Systems", "SYSTEMS");

				// Token: 0x04009FC5 RID: 40901
				public static LocString ROLES = UI.FormatAsLink("Duplicant Skills", "ROLES");

				// Token: 0x04009FC6 RID: 40902
				public static LocString DISEASE = UI.FormatAsLink("Disease", "DISEASE");

				// Token: 0x04009FC7 RID: 40903
				public static LocString SICKNESS = UI.FormatAsLink("Sickness", "SICKNESS");

				// Token: 0x04009FC8 RID: 40904
				public static LocString MEDIA = UI.FormatAsLink("Media", "MEDIA");
			}
		}

		// Token: 0x02001C9A RID: 7322
		public class DEVELOPMENTBUILDS
		{
			// Token: 0x040081B5 RID: 33205
			public static LocString WATERMARK = "BUILD: {0}";

			// Token: 0x040081B6 RID: 33206
			public static LocString TESTING_WATERMARK = "TESTING BUILD: {0}";

			// Token: 0x040081B7 RID: 33207
			public static LocString TESTING_TOOLTIP = "This game is currently running a Test version.\n\n" + UI.CLICK(UI.ClickType.Click) + " for more info.";

			// Token: 0x040081B8 RID: 33208
			public static LocString TESTING_MESSAGE_TITLE = "TESTING BUILD";

			// Token: 0x040081B9 RID: 33209
			public static LocString TESTING_MESSAGE = "This game is running a Test version of Oxygen Not Included. This means that some features may be in development or buggier than normal, and require more testing before they can be moved into the Release build.\n\nIf you encounter any bugs or strange behavior, please add a report to the bug forums. We appreciate it!";

			// Token: 0x040081BA RID: 33210
			public static LocString TESTING_MORE_INFO = "BUG FORUMS";

			// Token: 0x040081BB RID: 33211
			public static LocString FULL_PATCH_NOTES = "Full Patch Notes";

			// Token: 0x040081BC RID: 33212
			public static LocString PREVIOUS_VERSION = "Previous Version";

			// Token: 0x02002485 RID: 9349
			public class ALPHA
			{
				// Token: 0x02002EF4 RID: 12020
				public class MESSAGES
				{
					// Token: 0x0400BCC4 RID: 48324
					public static LocString FORUMBUTTON = "FORUMS";

					// Token: 0x0400BCC5 RID: 48325
					public static LocString MAILINGLIST = "MAILING LIST";

					// Token: 0x0400BCC6 RID: 48326
					public static LocString PATCHNOTES = "PATCH NOTES";

					// Token: 0x0400BCC7 RID: 48327
					public static LocString FEEDBACK = "FEEDBACK";
				}

				// Token: 0x02002EF5 RID: 12021
				public class LOADING
				{
					// Token: 0x0400BCC8 RID: 48328
					public static LocString TITLE = "<b>Welcome to Oxygen Not Included!</b>";

					// Token: 0x0400BCC9 RID: 48329
					public static LocString BODY = "This game is in the early stages of development which means you're likely to encounter strange, amusing, and occasionally just downright frustrating bugs.\n\nDuring this time Oxygen Not Included will be receiving regular updates to fix bugs, add features, and introduce additional content, so if you encounter issues or just have suggestions to share, please let us know on our forums: <u>http://forums.kleientertainment.com</u>\n\nA special thanks to those who joined us during our time in Alpha. We value your feedback and thank you for joining us in the development process. We couldn't do this without you.\n\nEnjoy your time in deep space!\n\n- Klei";

					// Token: 0x0400BCCA RID: 48330
					public static LocString BODY_NOLINKS = "This DLC is currently in active development, which means you're likely to encounter strange, amusing, and occasionally just downright frustrating bugs.\n\n During this time Spaced Out! will be receiving regular updates to fix bugs, add features, and introduce additional content.\n\n We've got lots of content old and new to add to this DLC before it's ready, and we're happy to have you along with us. Enjoy your time in deep space!\n\n - The Team at Klei";

					// Token: 0x0400BCCB RID: 48331
					public static LocString FORUMBUTTON = "Visit Forums";
				}

				// Token: 0x02002EF6 RID: 12022
				public class HEALTHY_MESSAGE
				{
					// Token: 0x0400BCCC RID: 48332
					public static LocString CONTINUEBUTTON = "Thanks!";
				}
			}

			// Token: 0x02002486 RID: 9350
			public class PREVIOUS_UPDATE
			{
				// Token: 0x04009FC9 RID: 40905
				public static LocString TITLE = "<b>Welcome to Oxygen Not Included!</b>";

				// Token: 0x04009FCA RID: 40906
				public static LocString BODY = "Whoops!\n\nYou're about to opt in to the <b>Previous Update branch</b>. That means opting out of all new features, fixes and content from the live branch.\n\nThis branch is temporary. It will be replaced when the next update is released. It's also completely unsupported—please don't report bugs or issues you find here.\n\nAre you sure you want to opt in?";

				// Token: 0x04009FCB RID: 40907
				public static LocString CONTINUEBUTTON = "Play Old Version";

				// Token: 0x04009FCC RID: 40908
				public static LocString FORUMBUTTON = "More Information";

				// Token: 0x04009FCD RID: 40909
				public static LocString QUITBUTTON = "Quit";
			}

			// Token: 0x02002487 RID: 9351
			public class UPDATES
			{
				// Token: 0x04009FCE RID: 40910
				public static LocString UPDATES_HEADER = "NEXT UPGRADE LIVE IN";

				// Token: 0x04009FCF RID: 40911
				public static LocString NOW = "Less than a day";

				// Token: 0x04009FD0 RID: 40912
				public static LocString TWENTY_FOUR_HOURS = "Less than a day";

				// Token: 0x04009FD1 RID: 40913
				public static LocString FINAL_WEEK = "{0} days";

				// Token: 0x04009FD2 RID: 40914
				public static LocString BIGGER_TIMES = "{1} weeks {0} days";
			}
		}

		// Token: 0x02001C9B RID: 7323
		public class UNITSUFFIXES
		{
			// Token: 0x040081BD RID: 33213
			public static LocString SECOND = " s";

			// Token: 0x040081BE RID: 33214
			public static LocString PERSECOND = "/s";

			// Token: 0x040081BF RID: 33215
			public static LocString PERCYCLE = "/cycle";

			// Token: 0x040081C0 RID: 33216
			public static LocString UNIT = " unit";

			// Token: 0x040081C1 RID: 33217
			public static LocString UNITS = " units";

			// Token: 0x040081C2 RID: 33218
			public static LocString PERCENT = "%";

			// Token: 0x040081C3 RID: 33219
			public static LocString DEGREES = " degrees";

			// Token: 0x040081C4 RID: 33220
			public static LocString CRITTERS = " critters";

			// Token: 0x040081C5 RID: 33221
			public static LocString GROWTH = "growth";

			// Token: 0x040081C6 RID: 33222
			public static LocString SECONDS = "Seconds";

			// Token: 0x040081C7 RID: 33223
			public static LocString DUPLICANTS = "Duplicants";

			// Token: 0x040081C8 RID: 33224
			public static LocString GERMS = "Germs";

			// Token: 0x040081C9 RID: 33225
			public static LocString ROCKET_MISSIONS = "Missions";

			// Token: 0x02002488 RID: 9352
			public class MASS
			{
				// Token: 0x04009FD3 RID: 40915
				public static LocString TONNE = " t";

				// Token: 0x04009FD4 RID: 40916
				public static LocString KILOGRAM = " kg";

				// Token: 0x04009FD5 RID: 40917
				public static LocString GRAM = " g";

				// Token: 0x04009FD6 RID: 40918
				public static LocString MILLIGRAM = " mg";

				// Token: 0x04009FD7 RID: 40919
				public static LocString MICROGRAM = " mcg";

				// Token: 0x04009FD8 RID: 40920
				public static LocString POUND = " lb";

				// Token: 0x04009FD9 RID: 40921
				public static LocString DRACHMA = " dr";

				// Token: 0x04009FDA RID: 40922
				public static LocString GRAIN = " gr";
			}

			// Token: 0x02002489 RID: 9353
			public class TEMPERATURE
			{
				// Token: 0x04009FDB RID: 40923
				public static LocString CELSIUS = " " + 'º'.ToString() + "C";

				// Token: 0x04009FDC RID: 40924
				public static LocString FAHRENHEIT = " " + 'º'.ToString() + "F";

				// Token: 0x04009FDD RID: 40925
				public static LocString KELVIN = " K";
			}

			// Token: 0x0200248A RID: 9354
			public class CALORIES
			{
				// Token: 0x04009FDE RID: 40926
				public static LocString CALORIE = " cal";

				// Token: 0x04009FDF RID: 40927
				public static LocString KILOCALORIE = " kcal";
			}

			// Token: 0x0200248B RID: 9355
			public class ELECTRICAL
			{
				// Token: 0x04009FE0 RID: 40928
				public static LocString JOULE = " J";

				// Token: 0x04009FE1 RID: 40929
				public static LocString KILOJOULE = " kJ";

				// Token: 0x04009FE2 RID: 40930
				public static LocString MEGAJOULE = " MJ";

				// Token: 0x04009FE3 RID: 40931
				public static LocString WATT = " W";

				// Token: 0x04009FE4 RID: 40932
				public static LocString KILOWATT = " kW";
			}

			// Token: 0x0200248C RID: 9356
			public class HEAT
			{
				// Token: 0x04009FE5 RID: 40933
				public static LocString DTU = " DTU";

				// Token: 0x04009FE6 RID: 40934
				public static LocString KDTU = " kDTU";

				// Token: 0x04009FE7 RID: 40935
				public static LocString DTU_S = " DTU/s";

				// Token: 0x04009FE8 RID: 40936
				public static LocString KDTU_S = " kDTU/s";
			}

			// Token: 0x0200248D RID: 9357
			public class DISTANCE
			{
				// Token: 0x04009FE9 RID: 40937
				public static LocString METER = " m";

				// Token: 0x04009FEA RID: 40938
				public static LocString KILOMETER = " km";
			}

			// Token: 0x0200248E RID: 9358
			public class DISEASE
			{
				// Token: 0x04009FEB RID: 40939
				public static LocString UNITS = " germs";
			}

			// Token: 0x0200248F RID: 9359
			public class NOISE
			{
				// Token: 0x04009FEC RID: 40940
				public static LocString UNITS = " dB";
			}

			// Token: 0x02002490 RID: 9360
			public class INFORMATION
			{
				// Token: 0x04009FED RID: 40941
				public static LocString BYTE = "B";

				// Token: 0x04009FEE RID: 40942
				public static LocString KILOBYTE = "kB";

				// Token: 0x04009FEF RID: 40943
				public static LocString MEGABYTE = "MB";

				// Token: 0x04009FF0 RID: 40944
				public static LocString GIGABYTE = "GB";

				// Token: 0x04009FF1 RID: 40945
				public static LocString TERABYTE = "TB";
			}

			// Token: 0x02002491 RID: 9361
			public class LIGHT
			{
				// Token: 0x04009FF2 RID: 40946
				public static LocString LUX = " lux";
			}

			// Token: 0x02002492 RID: 9362
			public class RADIATION
			{
				// Token: 0x04009FF3 RID: 40947
				public static LocString RADS = " rads";
			}

			// Token: 0x02002493 RID: 9363
			public class HIGHENERGYPARTICLES
			{
				// Token: 0x04009FF4 RID: 40948
				public static LocString PARTRICLE = " Radbolt";

				// Token: 0x04009FF5 RID: 40949
				public static LocString PARTRICLES = " Radbolts";
			}
		}

		// Token: 0x02001C9C RID: 7324
		public class OVERLAYS
		{
			// Token: 0x02002494 RID: 9364
			public class TILEMODE
			{
				// Token: 0x04009FF6 RID: 40950
				public static LocString NAME = "MATERIALS OVERLAY";

				// Token: 0x04009FF7 RID: 40951
				public static LocString BUTTON = "Materials Overlay";
			}

			// Token: 0x02002495 RID: 9365
			public class OXYGEN
			{
				// Token: 0x04009FF8 RID: 40952
				public static LocString NAME = "OXYGEN OVERLAY";

				// Token: 0x04009FF9 RID: 40953
				public static LocString BUTTON = "Oxygen Overlay";

				// Token: 0x04009FFA RID: 40954
				public static LocString LEGEND1 = "Very Breathable";

				// Token: 0x04009FFB RID: 40955
				public static LocString LEGEND2 = "Breathable";

				// Token: 0x04009FFC RID: 40956
				public static LocString LEGEND3 = "Barely Breathable";

				// Token: 0x04009FFD RID: 40957
				public static LocString LEGEND4 = "Unbreathable";

				// Token: 0x04009FFE RID: 40958
				public static LocString LEGEND5 = "Barely Breathable";

				// Token: 0x04009FFF RID: 40959
				public static LocString LEGEND6 = "Unbreathable";

				// Token: 0x02002EF7 RID: 12023
				public class TOOLTIPS
				{
					// Token: 0x0400BCCD RID: 48333
					public static LocString LEGEND1 = string.Concat(new string[]
					{
						"<b>Very Breathable</b>\nHigh ",
						UI.PRE_KEYWORD,
						"Oxygen",
						UI.PST_KEYWORD,
						" concentrations"
					});

					// Token: 0x0400BCCE RID: 48334
					public static LocString LEGEND2 = string.Concat(new string[]
					{
						"<b>Breathable</b>\nSufficient ",
						UI.PRE_KEYWORD,
						"Oxygen",
						UI.PST_KEYWORD,
						" concentrations"
					});

					// Token: 0x0400BCCF RID: 48335
					public static LocString LEGEND3 = string.Concat(new string[]
					{
						"<b>Barely Breathable</b>\nLow ",
						UI.PRE_KEYWORD,
						"Oxygen",
						UI.PST_KEYWORD,
						" concentrations"
					});

					// Token: 0x0400BCD0 RID: 48336
					public static LocString LEGEND4 = string.Concat(new string[]
					{
						"<b>Unbreathable</b>\nExtremely low or absent ",
						UI.PRE_KEYWORD,
						"Oxygen",
						UI.PST_KEYWORD,
						" concentrations\n\nDuplicants will suffocate if trapped in these areas"
					});

					// Token: 0x0400BCD1 RID: 48337
					public static LocString LEGEND5 = "<b>Slightly Toxic</b>\nHarmful gas concentration";

					// Token: 0x0400BCD2 RID: 48338
					public static LocString LEGEND6 = "<b>Very Toxic</b>\nLethal gas concentration";
				}
			}

			// Token: 0x02002496 RID: 9366
			public class ELECTRICAL
			{
				// Token: 0x0400A000 RID: 40960
				public static LocString NAME = "POWER OVERLAY";

				// Token: 0x0400A001 RID: 40961
				public static LocString BUTTON = "Power Overlay";

				// Token: 0x0400A002 RID: 40962
				public static LocString LEGEND1 = "<b>BUILDING POWER</b>";

				// Token: 0x0400A003 RID: 40963
				public static LocString LEGEND2 = "Consumer";

				// Token: 0x0400A004 RID: 40964
				public static LocString LEGEND3 = "Producer";

				// Token: 0x0400A005 RID: 40965
				public static LocString LEGEND4 = "<b>CIRCUIT POWER HEALTH</b>";

				// Token: 0x0400A006 RID: 40966
				public static LocString LEGEND5 = "Inactive";

				// Token: 0x0400A007 RID: 40967
				public static LocString LEGEND6 = "Safe";

				// Token: 0x0400A008 RID: 40968
				public static LocString LEGEND7 = "Strained";

				// Token: 0x0400A009 RID: 40969
				public static LocString LEGEND8 = "Overloaded";

				// Token: 0x0400A00A RID: 40970
				public static LocString DIAGRAM_HEADER = "Energy from the <b>Left Outlet</b> is used by the <b>Right Outlet</b>";

				// Token: 0x0400A00B RID: 40971
				public static LocString LEGEND_SWITCH = "Switch";

				// Token: 0x02002EF8 RID: 12024
				public class TOOLTIPS
				{
					// Token: 0x0400BCD3 RID: 48339
					public static LocString LEGEND1 = "Displays whether buildings use or generate " + UI.FormatAsLink("Power", "POWER");

					// Token: 0x0400BCD4 RID: 48340
					public static LocString LEGEND2 = "<b>Consumer</b>\nThese buildings draw power from a circuit";

					// Token: 0x0400BCD5 RID: 48341
					public static LocString LEGEND3 = "<b>Producer</b>\nThese buildings generate power for a circuit";

					// Token: 0x0400BCD6 RID: 48342
					public static LocString LEGEND4 = "Displays the health of wire systems";

					// Token: 0x0400BCD7 RID: 48343
					public static LocString LEGEND5 = "<b>Inactive</b>\nThere is no power activity on these circuits";

					// Token: 0x0400BCD8 RID: 48344
					public static LocString LEGEND6 = "<b>Safe</b>\nThese circuits are not in danger of overloading";

					// Token: 0x0400BCD9 RID: 48345
					public static LocString LEGEND7 = "<b>Strained</b>\nThese circuits are close to consuming more power than their wires support";

					// Token: 0x0400BCDA RID: 48346
					public static LocString LEGEND8 = "<b>Overloaded</b>\nThese circuits are consuming more power than their wires support";

					// Token: 0x0400BCDB RID: 48347
					public static LocString LEGEND_SWITCH = "<b>Switch</b>\nActivates or deactivates connected circuits";
				}
			}

			// Token: 0x02002497 RID: 9367
			public class TEMPERATURE
			{
				// Token: 0x0400A00C RID: 40972
				public static LocString NAME = "TEMPERATURE OVERLAY";

				// Token: 0x0400A00D RID: 40973
				public static LocString BUTTON = "Temperature Overlay";

				// Token: 0x0400A00E RID: 40974
				public static LocString EXTREMECOLD = "Absolute Zero";

				// Token: 0x0400A00F RID: 40975
				public static LocString VERYCOLD = "Cold";

				// Token: 0x0400A010 RID: 40976
				public static LocString COLD = "Chilled";

				// Token: 0x0400A011 RID: 40977
				public static LocString TEMPERATE = "Temperate";

				// Token: 0x0400A012 RID: 40978
				public static LocString HOT = "Warm";

				// Token: 0x0400A013 RID: 40979
				public static LocString VERYHOT = "Hot";

				// Token: 0x0400A014 RID: 40980
				public static LocString EXTREMEHOT = "Scorching";

				// Token: 0x0400A015 RID: 40981
				public static LocString MAXHOT = "Molten";

				// Token: 0x02002EF9 RID: 12025
				public class TOOLTIPS
				{
					// Token: 0x0400BCDC RID: 48348
					public static LocString TEMPERATURE = "Temperatures reaching {0}";
				}
			}

			// Token: 0x02002498 RID: 9368
			public class STATECHANGE
			{
				// Token: 0x0400A016 RID: 40982
				public static LocString LOWPOINT = "Low energy state change";

				// Token: 0x0400A017 RID: 40983
				public static LocString STABLE = "Stable";

				// Token: 0x0400A018 RID: 40984
				public static LocString HIGHPOINT = "High energy state change";

				// Token: 0x02002EFA RID: 12026
				public class TOOLTIPS
				{
					// Token: 0x0400BCDD RID: 48349
					public static LocString LOWPOINT = "Nearing a low energy state change";

					// Token: 0x0400BCDE RID: 48350
					public static LocString STABLE = "Not near any state changes";

					// Token: 0x0400BCDF RID: 48351
					public static LocString HIGHPOINT = "Nearing high energy state change";
				}
			}

			// Token: 0x02002499 RID: 9369
			public class HEATFLOW
			{
				// Token: 0x0400A019 RID: 40985
				public static LocString NAME = "THERMAL TOLERANCE OVERLAY";

				// Token: 0x0400A01A RID: 40986
				public static LocString HOVERTITLE = "THERMAL TOLERANCE";

				// Token: 0x0400A01B RID: 40987
				public static LocString BUTTON = "Thermal Tolerance Overlay";

				// Token: 0x0400A01C RID: 40988
				public static LocString COOLING = "Body Heat Loss";

				// Token: 0x0400A01D RID: 40989
				public static LocString NEUTRAL = "Comfort Zone";

				// Token: 0x0400A01E RID: 40990
				public static LocString HEATING = "Body Heat Retention";

				// Token: 0x02002EFB RID: 12027
				public class TOOLTIPS
				{
					// Token: 0x0400BCE0 RID: 48352
					public static LocString COOLING = "<b>Body Heat Loss</b>\nUncomfortably cold\n\nDuplicants lose more heat in these areas than they can absorb\n* Warm Sweaters help Duplicants retain body heat";

					// Token: 0x0400BCE1 RID: 48353
					public static LocString NEUTRAL = "<b>Comfort Zone</b>\nComfortable area\n\nDuplicants can regulate their internal temperatures in these areas";

					// Token: 0x0400BCE2 RID: 48354
					public static LocString HEATING = "<b>Body Heat Retention</b>\nUncomfortably warm\n\nDuplicants absorb more heat in these areas than they can release\n* Cool Vests help Duplicants shed excess body heat";
				}
			}

			// Token: 0x0200249A RID: 9370
			public class ROOMS
			{
				// Token: 0x0400A01F RID: 40991
				public static LocString NAME = "ROOM OVERLAY";

				// Token: 0x0400A020 RID: 40992
				public static LocString BUTTON = "Room Overlay";

				// Token: 0x0400A021 RID: 40993
				public static LocString ROOM = "Room {0}";

				// Token: 0x0400A022 RID: 40994
				public static LocString HOVERTITLE = "ROOMS";

				// Token: 0x02002EFC RID: 12028
				public static class NOROOM
				{
					// Token: 0x0400BCE3 RID: 48355
					public static LocString HEADER = "No Room";

					// Token: 0x0400BCE4 RID: 48356
					public static LocString DESC = "Enclose this space with walls and doors to make a room";

					// Token: 0x0400BCE5 RID: 48357
					public static LocString TOO_BIG = "<color=#F44A47FF>    • Size: {0} Tiles\n    • Maximum room size: {1} Tiles</color>";
				}

				// Token: 0x02002EFD RID: 12029
				public class TOOLTIPS
				{
					// Token: 0x0400BCE6 RID: 48358
					public static LocString ROOM = "Completed Duplicant bedrooms";

					// Token: 0x0400BCE7 RID: 48359
					public static LocString NOROOMS = "Duplicants have nowhere to sleep";
				}
			}

			// Token: 0x0200249B RID: 9371
			public class JOULES
			{
				// Token: 0x0400A023 RID: 40995
				public static LocString NAME = "JOULES";

				// Token: 0x0400A024 RID: 40996
				public static LocString HOVERTITLE = "JOULES";

				// Token: 0x0400A025 RID: 40997
				public static LocString BUTTON = "Joules Overlay";
			}

			// Token: 0x0200249C RID: 9372
			public class LIGHTING
			{
				// Token: 0x0400A026 RID: 40998
				public static LocString NAME = "LIGHT OVERLAY";

				// Token: 0x0400A027 RID: 40999
				public static LocString BUTTON = "Light Overlay";

				// Token: 0x0400A028 RID: 41000
				public static LocString LITAREA = "Lit Area";

				// Token: 0x0400A029 RID: 41001
				public static LocString DARK = "Unlit Area";

				// Token: 0x0400A02A RID: 41002
				public static LocString HOVERTITLE = "LIGHT";

				// Token: 0x0400A02B RID: 41003
				public static LocString DESC = "{0} Lux";

				// Token: 0x02002EFE RID: 12030
				public class RANGES
				{
					// Token: 0x0400BCE8 RID: 48360
					public static LocString NO_LIGHT = "Pitch Black";

					// Token: 0x0400BCE9 RID: 48361
					public static LocString VERY_LOW_LIGHT = "Dark";

					// Token: 0x0400BCEA RID: 48362
					public static LocString LOW_LIGHT = "Dim";

					// Token: 0x0400BCEB RID: 48363
					public static LocString MEDIUM_LIGHT = "Well Lit";

					// Token: 0x0400BCEC RID: 48364
					public static LocString HIGH_LIGHT = "Bright";

					// Token: 0x0400BCED RID: 48365
					public static LocString VERY_HIGH_LIGHT = "Brilliant";

					// Token: 0x0400BCEE RID: 48366
					public static LocString MAX_LIGHT = "Blinding";
				}

				// Token: 0x02002EFF RID: 12031
				public class TOOLTIPS
				{
					// Token: 0x0400BCEF RID: 48367
					public static LocString NAME = "LIGHT OVERLAY";

					// Token: 0x0400BCF0 RID: 48368
					public static LocString LITAREA = "<b>Lit Area</b>\nWorking in well-lit areas improves Duplicant " + UI.PRE_KEYWORD + "Morale" + UI.PST_KEYWORD;

					// Token: 0x0400BCF1 RID: 48369
					public static LocString DARK = "<b>Unlit Area</b>\nWorking in the dark has no effect on Duplicants";
				}
			}

			// Token: 0x0200249D RID: 9373
			public class CROP
			{
				// Token: 0x0400A02C RID: 41004
				public static LocString NAME = "FARMING OVERLAY";

				// Token: 0x0400A02D RID: 41005
				public static LocString BUTTON = "Farming Overlay";

				// Token: 0x0400A02E RID: 41006
				public static LocString GROWTH_HALTED = "Halted Growth";

				// Token: 0x0400A02F RID: 41007
				public static LocString GROWING = "Growing";

				// Token: 0x0400A030 RID: 41008
				public static LocString FULLY_GROWN = "Fully Grown";

				// Token: 0x02002F00 RID: 12032
				public class TOOLTIPS
				{
					// Token: 0x0400BCF2 RID: 48370
					public static LocString GROWTH_HALTED = "<b>Halted Growth</b>\nSubstandard conditions prevent these plants from growing";

					// Token: 0x0400BCF3 RID: 48371
					public static LocString GROWING = "<b>Growing</b>\nThese plants are thriving in their current conditions";

					// Token: 0x0400BCF4 RID: 48372
					public static LocString FULLY_GROWN = "<b>Fully Grown</b>\nThese plants have reached maturation\n\nSelect the " + UI.FormatAsTool("Harvest Tool", global::Action.Harvest) + " to batch harvest";
				}
			}

			// Token: 0x0200249E RID: 9374
			public class LIQUIDPLUMBING
			{
				// Token: 0x0400A031 RID: 41009
				public static LocString NAME = "PLUMBING OVERLAY";

				// Token: 0x0400A032 RID: 41010
				public static LocString BUTTON = "Plumbing Overlay";

				// Token: 0x0400A033 RID: 41011
				public static LocString CONSUMER = "Output Pipe";

				// Token: 0x0400A034 RID: 41012
				public static LocString FILTERED = "Filtered Output Pipe";

				// Token: 0x0400A035 RID: 41013
				public static LocString PRODUCER = "Building Intake";

				// Token: 0x0400A036 RID: 41014
				public static LocString CONNECTED = "Connected";

				// Token: 0x0400A037 RID: 41015
				public static LocString DISCONNECTED = "Disconnected";

				// Token: 0x0400A038 RID: 41016
				public static LocString NETWORK = "Liquid Network {0}";

				// Token: 0x0400A039 RID: 41017
				public static LocString DIAGRAM_BEFORE_ARROW = "Liquid flows from <b>Output Pipe</b>";

				// Token: 0x0400A03A RID: 41018
				public static LocString DIAGRAM_AFTER_ARROW = "<b>Building Intake</b>";

				// Token: 0x02002F01 RID: 12033
				public class TOOLTIPS
				{
					// Token: 0x0400BCF5 RID: 48373
					public static LocString CONNECTED = "Connected to a " + UI.FormatAsLink("Liquid Pipe", "LIQUIDCONDUIT");

					// Token: 0x0400BCF6 RID: 48374
					public static LocString DISCONNECTED = "Not connected to a " + UI.FormatAsLink("Liquid Pipe", "LIQUIDCONDUIT");

					// Token: 0x0400BCF7 RID: 48375
					public static LocString CONSUMER = "<b>Output Pipe</b>\nOutputs send liquid into pipes\n\nMust be on the same network as at least one " + UI.FormatAsLink("Intake", "LIQUIDPIPING");

					// Token: 0x0400BCF8 RID: 48376
					public static LocString FILTERED = "<b>Filtered Output Pipe</b>\nFiltered Outputs send filtered liquid into pipes\n\nMust be on the same network as at least one " + UI.FormatAsLink("Intake", "LIQUIDPIPING");

					// Token: 0x0400BCF9 RID: 48377
					public static LocString PRODUCER = "<b>Building Intake</b>\nIntakes send liquid into buildings\n\nMust be on the same network as at least one " + UI.FormatAsLink("Output", "LIQUIDPIPING");

					// Token: 0x0400BCFA RID: 48378
					public static LocString NETWORK = "Liquid network {0}";
				}
			}

			// Token: 0x0200249F RID: 9375
			public class GASPLUMBING
			{
				// Token: 0x0400A03B RID: 41019
				public static LocString NAME = "VENTILATION OVERLAY";

				// Token: 0x0400A03C RID: 41020
				public static LocString BUTTON = "Ventilation Overlay";

				// Token: 0x0400A03D RID: 41021
				public static LocString CONSUMER = "Output Pipe";

				// Token: 0x0400A03E RID: 41022
				public static LocString FILTERED = "Filtered Output Pipe";

				// Token: 0x0400A03F RID: 41023
				public static LocString PRODUCER = "Building Intake";

				// Token: 0x0400A040 RID: 41024
				public static LocString CONNECTED = "Connected";

				// Token: 0x0400A041 RID: 41025
				public static LocString DISCONNECTED = "Disconnected";

				// Token: 0x0400A042 RID: 41026
				public static LocString NETWORK = "Gas Network {0}";

				// Token: 0x0400A043 RID: 41027
				public static LocString DIAGRAM_BEFORE_ARROW = "Gas flows from <b>Output Pipe</b>";

				// Token: 0x0400A044 RID: 41028
				public static LocString DIAGRAM_AFTER_ARROW = "<b>Building Intake</b>";

				// Token: 0x02002F02 RID: 12034
				public class TOOLTIPS
				{
					// Token: 0x0400BCFB RID: 48379
					public static LocString CONNECTED = "Connected to a " + UI.FormatAsLink("Gas Pipe", "GASPIPING");

					// Token: 0x0400BCFC RID: 48380
					public static LocString DISCONNECTED = "Not connected to a " + UI.FormatAsLink("Gas Pipe", "GASPIPING");

					// Token: 0x0400BCFD RID: 48381
					public static LocString CONSUMER = string.Concat(new string[]
					{
						"<b>Output Pipe</b>\nOutputs send ",
						UI.PRE_KEYWORD,
						"Gas",
						UI.PST_KEYWORD,
						" into ",
						UI.PRE_KEYWORD,
						"Pipes",
						UI.PST_KEYWORD,
						"\n\nMust be on the same network as at least one ",
						UI.FormatAsLink("Intake", "GASPIPING")
					});

					// Token: 0x0400BCFE RID: 48382
					public static LocString FILTERED = string.Concat(new string[]
					{
						"<b>Filtered Output Pipe</b>\nFiltered Outputs send filtered ",
						UI.PRE_KEYWORD,
						"Gas",
						UI.PST_KEYWORD,
						" into ",
						UI.PRE_KEYWORD,
						"Pipes",
						UI.PST_KEYWORD,
						"\n\nMust be on the same network as at least one ",
						UI.FormatAsLink("Intake", "GASPIPING")
					});

					// Token: 0x0400BCFF RID: 48383
					public static LocString PRODUCER = "<b>Building Intake</b>\nIntakes send gas into buildings\n\nMust be on the same network as at least one " + UI.FormatAsLink("Output", "GASPIPING");

					// Token: 0x0400BD00 RID: 48384
					public static LocString NETWORK = "Gas network {0}";
				}
			}

			// Token: 0x020024A0 RID: 9376
			public class SUIT
			{
				// Token: 0x0400A045 RID: 41029
				public static LocString NAME = "EXOSUIT OVERLAY";

				// Token: 0x0400A046 RID: 41030
				public static LocString BUTTON = "Exosuit Overlay";

				// Token: 0x0400A047 RID: 41031
				public static LocString SUIT_ICON = "Exosuit";

				// Token: 0x0400A048 RID: 41032
				public static LocString SUIT_ICON_TOOLTIP = "<b>Exosuit</b>\nHighlights the current location of equippable exosuits";
			}

			// Token: 0x020024A1 RID: 9377
			public class LOGIC
			{
				// Token: 0x0400A049 RID: 41033
				public static LocString NAME = "AUTOMATION OVERLAY";

				// Token: 0x0400A04A RID: 41034
				public static LocString BUTTON = "Automation Overlay";

				// Token: 0x0400A04B RID: 41035
				public static LocString INPUT = "Input Port";

				// Token: 0x0400A04C RID: 41036
				public static LocString OUTPUT = "Output Port";

				// Token: 0x0400A04D RID: 41037
				public static LocString RIBBON_INPUT = "Ribbon Input Port";

				// Token: 0x0400A04E RID: 41038
				public static LocString RIBBON_OUTPUT = "Ribbon Output Port";

				// Token: 0x0400A04F RID: 41039
				public static LocString RESET_UPDATE = "Reset Port";

				// Token: 0x0400A050 RID: 41040
				public static LocString CONTROL_INPUT = "Control Port";

				// Token: 0x0400A051 RID: 41041
				public static LocString CIRCUIT_STATUS_HEADER = "GRID STATUS";

				// Token: 0x0400A052 RID: 41042
				public static LocString ONE = "Green";

				// Token: 0x0400A053 RID: 41043
				public static LocString ZERO = "Red";

				// Token: 0x0400A054 RID: 41044
				public static LocString DISCONNECTED = "DISCONNECTED";

				// Token: 0x02002F03 RID: 12035
				public abstract class TOOLTIPS
				{
					// Token: 0x0400BD01 RID: 48385
					public static LocString INPUT = "<b>Input Port</b>\nReceives a signal from an automation grid";

					// Token: 0x0400BD02 RID: 48386
					public static LocString OUTPUT = "<b>Output Port</b>\nSends a signal out to an automation grid";

					// Token: 0x0400BD03 RID: 48387
					public static LocString RIBBON_INPUT = "<b>Ribbon Input Port</b>\nReceives a 4-bit signal from an automation grid";

					// Token: 0x0400BD04 RID: 48388
					public static LocString RIBBON_OUTPUT = "<b>Ribbon Output Port</b>\nSends a 4-bit signal out to an automation grid";

					// Token: 0x0400BD05 RID: 48389
					public static LocString RESET_UPDATE = "<b>Reset Port</b>\nReset a " + BUILDINGS.PREFABS.LOGICMEMORY.NAME + "'s internal Memory to " + UI.FormatAsAutomationState("Red", UI.AutomationState.Standby);

					// Token: 0x0400BD06 RID: 48390
					public static LocString CONTROL_INPUT = "<b>Control Port</b>\nControl the signal selection of a " + BUILDINGS.PREFABS.LOGICGATEMULTIPLEXER.NAME + " or " + BUILDINGS.PREFABS.LOGICGATEDEMULTIPLEXER.NAME;

					// Token: 0x0400BD07 RID: 48391
					public static LocString ONE = "<b>Green</b>\nThis port is currently " + UI.FormatAsAutomationState("Green", UI.AutomationState.Active);

					// Token: 0x0400BD08 RID: 48392
					public static LocString ZERO = "<b>Red</b>\nThis port is currently " + UI.FormatAsAutomationState("Red", UI.AutomationState.Standby);

					// Token: 0x0400BD09 RID: 48393
					public static LocString DISCONNECTED = "<b>Disconnected</b>\nThis port is not connected to an automation grid";
				}
			}

			// Token: 0x020024A2 RID: 9378
			public class CONVEYOR
			{
				// Token: 0x0400A055 RID: 41045
				public static LocString NAME = "CONVEYOR OVERLAY";

				// Token: 0x0400A056 RID: 41046
				public static LocString BUTTON = "Conveyor Overlay";

				// Token: 0x0400A057 RID: 41047
				public static LocString OUTPUT = "Loader";

				// Token: 0x0400A058 RID: 41048
				public static LocString INPUT = "Receptacle";

				// Token: 0x02002F04 RID: 12036
				public abstract class TOOLTIPS
				{
					// Token: 0x0400BD0A RID: 48394
					public static LocString OUTPUT = string.Concat(new string[]
					{
						"<b>Loader</b>\nLoads material onto a ",
						UI.PRE_KEYWORD,
						"Conveyor Rail",
						UI.PST_KEYWORD,
						" for transport to Receptacles"
					});

					// Token: 0x0400BD0B RID: 48395
					public static LocString INPUT = string.Concat(new string[]
					{
						"<b>Receptacle</b>\nReceives material from a ",
						UI.PRE_KEYWORD,
						"Conveyor Rail",
						UI.PST_KEYWORD,
						" and stores it for Duplicant use"
					});
				}
			}

			// Token: 0x020024A3 RID: 9379
			public class DECOR
			{
				// Token: 0x0400A059 RID: 41049
				public static LocString NAME = "DECOR OVERLAY";

				// Token: 0x0400A05A RID: 41050
				public static LocString BUTTON = "Decor Overlay";

				// Token: 0x0400A05B RID: 41051
				public static LocString TOTAL = "Total Decor: ";

				// Token: 0x0400A05C RID: 41052
				public static LocString ENTRY = "{0} {1} {2}";

				// Token: 0x0400A05D RID: 41053
				public static LocString COUNT = "({0})";

				// Token: 0x0400A05E RID: 41054
				public static LocString VALUE = "{0}{1}";

				// Token: 0x0400A05F RID: 41055
				public static LocString VALUE_ZERO = "{0}{1}";

				// Token: 0x0400A060 RID: 41056
				public static LocString HEADER_POSITIVE = "Positive Value:";

				// Token: 0x0400A061 RID: 41057
				public static LocString HEADER_NEGATIVE = "Negative Value:";

				// Token: 0x0400A062 RID: 41058
				public static LocString LOWDECOR = "Negative Decor";

				// Token: 0x0400A063 RID: 41059
				public static LocString HIGHDECOR = "Positive Decor";

				// Token: 0x0400A064 RID: 41060
				public static LocString CLUTTER = "Debris";

				// Token: 0x0400A065 RID: 41061
				public static LocString LIGHTING = "Lighting";

				// Token: 0x0400A066 RID: 41062
				public static LocString CLOTHING = "{0}'s Outfit";

				// Token: 0x0400A067 RID: 41063
				public static LocString CLOTHING_TRAIT_DECORUP = "{0}'s Outfit (Innately Stylish)";

				// Token: 0x0400A068 RID: 41064
				public static LocString CLOTHING_TRAIT_DECORDOWN = "{0}'s Outfit (Shabby Dresser)";

				// Token: 0x0400A069 RID: 41065
				public static LocString HOVERTITLE = "DECOR";

				// Token: 0x0400A06A RID: 41066
				public static LocString MAXIMUM_DECOR = "{0}{1} (Maximum Decor)";

				// Token: 0x02002F05 RID: 12037
				public class TOOLTIPS
				{
					// Token: 0x0400BD0C RID: 48396
					public static LocString LOWDECOR = string.Concat(new string[]
					{
						"<b>Negative Decor</b>\nArea with insufficient ",
						UI.PRE_KEYWORD,
						"Decor",
						UI.PST_KEYWORD,
						" values\n* Resources on the floor are considered \"debris\" and will decrease decor"
					});

					// Token: 0x0400BD0D RID: 48397
					public static LocString HIGHDECOR = string.Concat(new string[]
					{
						"<b>Positive Decor</b>\nArea with sufficient ",
						UI.PRE_KEYWORD,
						"Decor",
						UI.PST_KEYWORD,
						" values\n* Lighting and aesthetically pleasing buildings increase decor"
					});
				}
			}

			// Token: 0x020024A4 RID: 9380
			public class PRIORITIES
			{
				// Token: 0x0400A06B RID: 41067
				public static LocString NAME = "PRIORITY OVERLAY";

				// Token: 0x0400A06C RID: 41068
				public static LocString BUTTON = "Priority Overlay";

				// Token: 0x0400A06D RID: 41069
				public static LocString ONE = "1 (Low Urgency)";

				// Token: 0x0400A06E RID: 41070
				public static LocString ONE_TOOLTIP = "Priority 1";

				// Token: 0x0400A06F RID: 41071
				public static LocString TWO = "2";

				// Token: 0x0400A070 RID: 41072
				public static LocString TWO_TOOLTIP = "Priority 2";

				// Token: 0x0400A071 RID: 41073
				public static LocString THREE = "3";

				// Token: 0x0400A072 RID: 41074
				public static LocString THREE_TOOLTIP = "Priority 3";

				// Token: 0x0400A073 RID: 41075
				public static LocString FOUR = "4";

				// Token: 0x0400A074 RID: 41076
				public static LocString FOUR_TOOLTIP = "Priority 4";

				// Token: 0x0400A075 RID: 41077
				public static LocString FIVE = "5";

				// Token: 0x0400A076 RID: 41078
				public static LocString FIVE_TOOLTIP = "Priority 5";

				// Token: 0x0400A077 RID: 41079
				public static LocString SIX = "6";

				// Token: 0x0400A078 RID: 41080
				public static LocString SIX_TOOLTIP = "Priority 6";

				// Token: 0x0400A079 RID: 41081
				public static LocString SEVEN = "7";

				// Token: 0x0400A07A RID: 41082
				public static LocString SEVEN_TOOLTIP = "Priority 7";

				// Token: 0x0400A07B RID: 41083
				public static LocString EIGHT = "8";

				// Token: 0x0400A07C RID: 41084
				public static LocString EIGHT_TOOLTIP = "Priority 8";

				// Token: 0x0400A07D RID: 41085
				public static LocString NINE = "9 (High Urgency)";

				// Token: 0x0400A07E RID: 41086
				public static LocString NINE_TOOLTIP = "Priority 9";
			}

			// Token: 0x020024A5 RID: 9381
			public class DISEASE
			{
				// Token: 0x0400A07F RID: 41087
				public static LocString NAME = "GERM OVERLAY";

				// Token: 0x0400A080 RID: 41088
				public static LocString BUTTON = "Germ Overlay";

				// Token: 0x0400A081 RID: 41089
				public static LocString HOVERTITLE = "Germ";

				// Token: 0x0400A082 RID: 41090
				public static LocString INFECTION_SOURCE = "Germ Source";

				// Token: 0x0400A083 RID: 41091
				public static LocString INFECTION_SOURCE_TOOLTIP = "<b>Germ Source</b>\nAreas where germs are produced\n* Placing Wash Basins or Hand Sanitizers near these areas may prevent disease spread";

				// Token: 0x0400A084 RID: 41092
				public static LocString NO_DISEASE = "Zero surface germs";

				// Token: 0x0400A085 RID: 41093
				public static LocString DISEASE_NAME_FORMAT = "{0}<color=#{1}></color>";

				// Token: 0x0400A086 RID: 41094
				public static LocString DISEASE_NAME_FORMAT_NO_COLOR = "{0}";

				// Token: 0x0400A087 RID: 41095
				public static LocString DISEASE_FORMAT = "{1} [{0}]<color=#{2}></color>";

				// Token: 0x0400A088 RID: 41096
				public static LocString DISEASE_FORMAT_NO_COLOR = "{1} [{0}]";

				// Token: 0x0400A089 RID: 41097
				public static LocString CONTAINER_FORMAT = "\n    {0}: {1}";

				// Token: 0x02002F06 RID: 12038
				public class DISINFECT_THRESHOLD_DIAGRAM
				{
					// Token: 0x0400BD0E RID: 48398
					public static LocString UNITS = "Germs";

					// Token: 0x0400BD0F RID: 48399
					public static LocString MIN_LABEL = "0";

					// Token: 0x0400BD10 RID: 48400
					public static LocString MAX_LABEL = "1m";

					// Token: 0x0400BD11 RID: 48401
					public static LocString THRESHOLD_PREFIX = "Disinfect At:";

					// Token: 0x0400BD12 RID: 48402
					public static LocString TOOLTIP = "Automatically disinfect any building with more than {NumberOfGerms} germs.";

					// Token: 0x0400BD13 RID: 48403
					public static LocString TOOLTIP_DISABLED = "Automatic building disinfection disabled.";
				}
			}

			// Token: 0x020024A6 RID: 9382
			public class CROPS
			{
				// Token: 0x0400A08A RID: 41098
				public static LocString NAME = "FARMING OVERLAY";

				// Token: 0x0400A08B RID: 41099
				public static LocString BUTTON = "Farming Overlay";
			}

			// Token: 0x020024A7 RID: 9383
			public class POWER
			{
				// Token: 0x0400A08C RID: 41100
				public static LocString WATTS_GENERATED = "Watts Generated";

				// Token: 0x0400A08D RID: 41101
				public static LocString WATTS_CONSUMED = "Watts Consumed";
			}

			// Token: 0x020024A8 RID: 9384
			public class RADIATION
			{
				// Token: 0x0400A08E RID: 41102
				public static LocString NAME = "RADIATION";

				// Token: 0x0400A08F RID: 41103
				public static LocString BUTTON = "Radiation Overlay";

				// Token: 0x0400A090 RID: 41104
				public static LocString DESC = "{rads} per cycle ({description})";

				// Token: 0x0400A091 RID: 41105
				public static LocString SHIELDING_DESC = "Radiation Blocking: {radiationAbsorptionFactor}";

				// Token: 0x0400A092 RID: 41106
				public static LocString HOVERTITLE = "RADIATION";

				// Token: 0x02002F07 RID: 12039
				public class RANGES
				{
					// Token: 0x0400BD14 RID: 48404
					public static LocString NONE = "Completely Safe";

					// Token: 0x0400BD15 RID: 48405
					public static LocString VERY_LOW = "Mostly Safe";

					// Token: 0x0400BD16 RID: 48406
					public static LocString LOW = "Barely Safe";

					// Token: 0x0400BD17 RID: 48407
					public static LocString MEDIUM = "Slight Hazard";

					// Token: 0x0400BD18 RID: 48408
					public static LocString HIGH = "Significant Hazard";

					// Token: 0x0400BD19 RID: 48409
					public static LocString VERY_HIGH = "Extreme Hazard";

					// Token: 0x0400BD1A RID: 48410
					public static LocString MAX = "Maximum Hazard";

					// Token: 0x0400BD1B RID: 48411
					public static LocString INPUTPORT = "Radbolt Input Port";

					// Token: 0x0400BD1C RID: 48412
					public static LocString OUTPUTPORT = "Radbolt Output Port";
				}

				// Token: 0x02002F08 RID: 12040
				public class TOOLTIPS
				{
					// Token: 0x0400BD1D RID: 48413
					public static LocString NONE = "Completely Safe";

					// Token: 0x0400BD1E RID: 48414
					public static LocString VERY_LOW = "Mostly Safe";

					// Token: 0x0400BD1F RID: 48415
					public static LocString LOW = "Barely Safe";

					// Token: 0x0400BD20 RID: 48416
					public static LocString MEDIUM = "Slight Hazard";

					// Token: 0x0400BD21 RID: 48417
					public static LocString HIGH = "Significant Hazard";

					// Token: 0x0400BD22 RID: 48418
					public static LocString VERY_HIGH = "Extreme Hazard";

					// Token: 0x0400BD23 RID: 48419
					public static LocString MAX = "Maximum Hazard";

					// Token: 0x0400BD24 RID: 48420
					public static LocString INPUTPORT = "Radbolt Input Port";

					// Token: 0x0400BD25 RID: 48421
					public static LocString OUTPUTPORT = "Radbolt Output Port";
				}
			}
		}

		// Token: 0x02001C9D RID: 7325
		public class TABLESCREENS
		{
			// Token: 0x040081CA RID: 33226
			public static LocString DUPLICANT_PROPERNAME = "<b>{0}</b>";

			// Token: 0x040081CB RID: 33227
			public static LocString SELECT_DUPLICANT_BUTTON = UI.CLICK(UI.ClickType.Click) + " to select <b>{0}</b>";

			// Token: 0x040081CC RID: 33228
			public static LocString GOTO_DUPLICANT_BUTTON = "Double-" + UI.CLICK(UI.ClickType.click) + " to go to <b>{0}</b>";

			// Token: 0x040081CD RID: 33229
			public static LocString COLUMN_SORT_BY_NAME = "Sort by <b>Name</b>";

			// Token: 0x040081CE RID: 33230
			public static LocString COLUMN_SORT_BY_STRESS = "Sort by <b>Stress</b>";

			// Token: 0x040081CF RID: 33231
			public static LocString COLUMN_SORT_BY_HITPOINTS = "Sort by <b>Health</b>";

			// Token: 0x040081D0 RID: 33232
			public static LocString COLUMN_SORT_BY_SICKNESSES = "Sort by <b>Disease</b>";

			// Token: 0x040081D1 RID: 33233
			public static LocString COLUMN_SORT_BY_FULLNESS = "Sort by <b>Fullness</b>";

			// Token: 0x040081D2 RID: 33234
			public static LocString COLUMN_SORT_BY_EATEN_TODAY = "Sort by number of <b>Calories</b> consumed today";

			// Token: 0x040081D3 RID: 33235
			public static LocString COLUMN_SORT_BY_EXPECTATIONS = "Sort by <b>Morale</b>";

			// Token: 0x040081D4 RID: 33236
			public static LocString NA = "N/A";

			// Token: 0x040081D5 RID: 33237
			public static LocString INFORMATION_NOT_AVAILABLE_TOOLTIP = "Information is not available because {1} is in {0}";

			// Token: 0x040081D6 RID: 33238
			public static LocString NOBODY_HERE = "Nobody here...";
		}

		// Token: 0x02001C9E RID: 7326
		public class CONSUMABLESSCREEN
		{
			// Token: 0x040081D7 RID: 33239
			public static LocString TITLE = "CONSUMABLES";

			// Token: 0x040081D8 RID: 33240
			public static LocString TOOLTIP_TOGGLE_ALL = "Toggle <b>all</b> food permissions <b>colonywide</b>";

			// Token: 0x040081D9 RID: 33241
			public static LocString TOOLTIP_TOGGLE_COLUMN = "Toggle colonywide <b>{0}</b> permission";

			// Token: 0x040081DA RID: 33242
			public static LocString TOOLTIP_TOGGLE_ROW = "Toggle <b>all consumable permissions</b> for <b>{0}</b>";

			// Token: 0x040081DB RID: 33243
			public static LocString NEW_MINIONS_TOOLTIP_TOGGLE_ROW = "Toggle <b>all consumable permissions</b> for <b>New Duplicants</b>";

			// Token: 0x040081DC RID: 33244
			public static LocString NEW_MINIONS_FOOD_PERMISSION_ON = string.Concat(new string[]
			{
				"<b>New Duplicants</b> are <b>allowed</b> to eat \n",
				UI.PRE_KEYWORD,
				"{0}",
				UI.PST_KEYWORD,
				"</b> by default"
			});

			// Token: 0x040081DD RID: 33245
			public static LocString NEW_MINIONS_FOOD_PERMISSION_OFF = string.Concat(new string[]
			{
				"<b>New Duplicants</b> are <b>not allowed</b> to eat \n",
				UI.PRE_KEYWORD,
				"{0}",
				UI.PST_KEYWORD,
				" by default"
			});

			// Token: 0x040081DE RID: 33246
			public static LocString FOOD_PERMISSION_ON = "<b>{0}</b> is <b>allowed</b> to eat " + UI.PRE_KEYWORD + "{1}" + UI.PST_KEYWORD;

			// Token: 0x040081DF RID: 33247
			public static LocString FOOD_PERMISSION_OFF = "<b>{0}</b> is <b>not allowed</b> to eat " + UI.PRE_KEYWORD + "{1}" + UI.PST_KEYWORD;

			// Token: 0x040081E0 RID: 33248
			public static LocString FOOD_CANT_CONSUME = "<b>{0}</b> <b>physically cannot</b> eat\n" + UI.PRE_KEYWORD + "{1}" + UI.PST_KEYWORD;

			// Token: 0x040081E1 RID: 33249
			public static LocString FOOD_REFUSE = "<b>{0}</b> <b>refuses</b> to eat\n" + UI.PRE_KEYWORD + "{1}" + UI.PST_KEYWORD;

			// Token: 0x040081E2 RID: 33250
			public static LocString FOOD_AVAILABLE = "Available: {0}";

			// Token: 0x040081E3 RID: 33251
			public static LocString FOOD_QUALITY = UI.PRE_KEYWORD + "Morale" + UI.PST_KEYWORD + ": {0}";

			// Token: 0x040081E4 RID: 33252
			public static LocString FOOD_QUALITY_VS_EXPECTATION = string.Concat(new string[]
			{
				"\nThis food will give ",
				UI.PRE_KEYWORD,
				"Morale",
				UI.PST_KEYWORD,
				" <b>{0}</b> if {1} eats it"
			});

			// Token: 0x040081E5 RID: 33253
			public static LocString CANNOT_ADJUST_PERMISSIONS = "Cannot adjust consumable permissions because they're in {0}";
		}

		// Token: 0x02001C9F RID: 7327
		public class JOBSSCREEN
		{
			// Token: 0x040081E6 RID: 33254
			public static LocString TITLE = "MANAGE DUPLICANT PRIORITIES";

			// Token: 0x040081E7 RID: 33255
			public static LocString TOOLTIP_TOGGLE_ALL = "Set priority of all Errand Types colonywide";

			// Token: 0x040081E8 RID: 33256
			public static LocString HEADER_TOOLTIP = string.Concat(new string[]
			{
				"<size=16>{Job} Errand Type</size>\n\n{Details}\n\nDuplicants will first choose what ",
				UI.PRE_KEYWORD,
				"Errand Type",
				UI.PST_KEYWORD,
				" to perform based on ",
				UI.PRE_KEYWORD,
				"Duplicant Priorities",
				UI.PST_KEYWORD,
				",\nthen they will choose individual tasks within that type using ",
				UI.PRE_KEYWORD,
				"Building Priorities",
				UI.PST_KEYWORD,
				" set by the ",
				UI.FormatAsLink("Priority Tool", "PRIORITIES"),
				" ",
				UI.FormatAsHotKey(global::Action.ManagePriorities)
			});

			// Token: 0x040081E9 RID: 33257
			public static LocString HEADER_DETAILS_TOOLTIP = "{Description}\n\nAffected errands: {ChoreList}";

			// Token: 0x040081EA RID: 33258
			public static LocString HEADER_CHANGE_TOOLTIP = string.Concat(new string[]
			{
				"Set the priority for the ",
				UI.PRE_KEYWORD,
				"{Job}",
				UI.PST_KEYWORD,
				" Errand Type colonywide\n"
			});

			// Token: 0x040081EB RID: 33259
			public static LocString NEW_MINION_ITEM_TOOLTIP = string.Concat(new string[]
			{
				"The ",
				UI.PRE_KEYWORD,
				"{Job}",
				UI.PST_KEYWORD,
				" Errand Type is automatically a {Priority} ",
				UI.PRE_KEYWORD,
				"Priority",
				UI.PST_KEYWORD,
				" for <b>Arriving Duplicants</b>"
			});

			// Token: 0x040081EC RID: 33260
			public static LocString ITEM_TOOLTIP = UI.PRE_KEYWORD + "{Job}" + UI.PST_KEYWORD + " Priority for {Name}:\n<b>{Priority} Priority ({PriorityValue})</b>";

			// Token: 0x040081ED RID: 33261
			public static LocString MINION_SKILL_TOOLTIP = string.Concat(new string[]
			{
				"{Name}'s ",
				UI.PRE_KEYWORD,
				"{Attribute}",
				UI.PST_KEYWORD,
				" Skill: "
			});

			// Token: 0x040081EE RID: 33262
			public static LocString TRAIT_DISABLED = string.Concat(new string[]
			{
				"{Name} possesses the ",
				UI.PRE_KEYWORD,
				"{Trait}",
				UI.PST_KEYWORD,
				" trait and <b>cannot</b> do ",
				UI.PRE_KEYWORD,
				"{Job}",
				UI.PST_KEYWORD,
				" Errands"
			});

			// Token: 0x040081EF RID: 33263
			public static LocString INCREASE_ROW_PRIORITY_NEW_MINION_TOOLTIP = string.Concat(new string[]
			{
				"Prioritize ",
				UI.PRE_KEYWORD,
				"All Errands",
				UI.PST_KEYWORD,
				" for <b>New Duplicants</b>"
			});

			// Token: 0x040081F0 RID: 33264
			public static LocString DECREASE_ROW_PRIORITY_NEW_MINION_TOOLTIP = string.Concat(new string[]
			{
				"Deprioritize ",
				UI.PRE_KEYWORD,
				"All Errands",
				UI.PST_KEYWORD,
				" for <b>New Duplicants</b>"
			});

			// Token: 0x040081F1 RID: 33265
			public static LocString INCREASE_ROW_PRIORITY_MINION_TOOLTIP = string.Concat(new string[]
			{
				"Prioritize ",
				UI.PRE_KEYWORD,
				"All Errands",
				UI.PST_KEYWORD,
				" for <b>{Name}</b>"
			});

			// Token: 0x040081F2 RID: 33266
			public static LocString DECREASE_ROW_PRIORITY_MINION_TOOLTIP = string.Concat(new string[]
			{
				"Deprioritize ",
				UI.PRE_KEYWORD,
				"All Errands",
				UI.PST_KEYWORD,
				" for <b>{Name}</b>"
			});

			// Token: 0x040081F3 RID: 33267
			public static LocString INCREASE_PRIORITY_TUTORIAL = "{Hotkey} Increase Priority";

			// Token: 0x040081F4 RID: 33268
			public static LocString DECREASE_PRIORITY_TUTORIAL = "{Hotkey} Decrease Priority";

			// Token: 0x040081F5 RID: 33269
			public static LocString CANNOT_ADJUST_PRIORITY = string.Concat(new string[]
			{
				"Priorities for ",
				UI.PRE_KEYWORD,
				"{0}",
				UI.PST_KEYWORD,
				" cannot be adjusted currently because they're in {1}"
			});

			// Token: 0x040081F6 RID: 33270
			public static LocString SORT_TOOLTIP = string.Concat(new string[]
			{
				"Sort by the ",
				UI.PRE_KEYWORD,
				"{Job}",
				UI.PST_KEYWORD,
				" Errand Type"
			});

			// Token: 0x040081F7 RID: 33271
			public static LocString DISABLED_TOOLTIP = string.Concat(new string[]
			{
				"{Name} may not perform ",
				UI.PRE_KEYWORD,
				"{Job}",
				UI.PST_KEYWORD,
				" Errands"
			});

			// Token: 0x040081F8 RID: 33272
			public static LocString OPTIONS = "Options";

			// Token: 0x040081F9 RID: 33273
			public static LocString TOGGLE_ADVANCED_MODE = "Enable Proximity";

			// Token: 0x040081FA RID: 33274
			public static LocString TOGGLE_ADVANCED_MODE_TOOLTIP = "<b>Errand Proximity Settings</b>\n\nEnabling Proximity settings tells my Duplicants to always choose the closest, most urgent errand to perform.\n\nWhen disabled, Duplicants will choose between two high priority errands based on a hidden priority hierarchy instead.\n\nEnabling Proximity helps cut down on travel time in areas with lots of high priority errands, and is useful for large colonies.";

			// Token: 0x040081FB RID: 33275
			public static LocString RESET_SETTINGS = "Reset Priorities";

			// Token: 0x040081FC RID: 33276
			public static LocString RESET_SETTINGS_TOOLTIP = "<b>Reset Priorities</b>\n\nReturns all priorities to their default values.\n\nProximity Enabled: Priorities will be adjusted high-to-low.\n\nProximity Disabled: All priorities will be reset to neutral.";

			// Token: 0x020024A9 RID: 9385
			public class PRIORITY
			{
				// Token: 0x0400A093 RID: 41107
				public static LocString VERYHIGH = "Very High";

				// Token: 0x0400A094 RID: 41108
				public static LocString HIGH = "High";

				// Token: 0x0400A095 RID: 41109
				public static LocString STANDARD = "Standard";

				// Token: 0x0400A096 RID: 41110
				public static LocString LOW = "Low";

				// Token: 0x0400A097 RID: 41111
				public static LocString VERYLOW = "Very Low";

				// Token: 0x0400A098 RID: 41112
				public static LocString DISABLED = "Disallowed";
			}

			// Token: 0x020024AA RID: 9386
			public class PRIORITY_CLASS
			{
				// Token: 0x0400A099 RID: 41113
				public static LocString IDLE = "Idle";

				// Token: 0x0400A09A RID: 41114
				public static LocString BASIC = "Normal";

				// Token: 0x0400A09B RID: 41115
				public static LocString HIGH = "Urgent";

				// Token: 0x0400A09C RID: 41116
				public static LocString PERSONAL_NEEDS = "Personal Needs";

				// Token: 0x0400A09D RID: 41117
				public static LocString EMERGENCY = "Emergency";

				// Token: 0x0400A09E RID: 41118
				public static LocString COMPULSORY = "Involuntary";
			}
		}

		// Token: 0x02001CA0 RID: 7328
		public class VITALSSCREEN
		{
			// Token: 0x040081FD RID: 33277
			public static LocString HEALTH = "Health";

			// Token: 0x040081FE RID: 33278
			public static LocString SICKNESS = "Diseases";

			// Token: 0x040081FF RID: 33279
			public static LocString NO_SICKNESSES = "No diseases";

			// Token: 0x04008200 RID: 33280
			public static LocString MULTIPLE_SICKNESSES = "Multiple diseases ({0})";

			// Token: 0x04008201 RID: 33281
			public static LocString SICKNESS_REMAINING = "{0}\n({1})";

			// Token: 0x04008202 RID: 33282
			public static LocString STRESS = "Stress";

			// Token: 0x04008203 RID: 33283
			public static LocString EXPECTATIONS = "Expectations";

			// Token: 0x04008204 RID: 33284
			public static LocString CALORIES = "Fullness";

			// Token: 0x04008205 RID: 33285
			public static LocString EATEN_TODAY = "Eaten Today";

			// Token: 0x04008206 RID: 33286
			public static LocString EATEN_TODAY_TOOLTIP = "Consumed {0} of food this cycle";

			// Token: 0x04008207 RID: 33287
			public static LocString ATMOSPHERE_CONDITION = "Atmosphere:";

			// Token: 0x04008208 RID: 33288
			public static LocString SUBMERSION = "Liquid Level";

			// Token: 0x04008209 RID: 33289
			public static LocString NOT_DROWNING = "Liquid Level";

			// Token: 0x0400820A RID: 33290
			public static LocString FOOD_EXPECTATIONS = "Food Expectation";

			// Token: 0x0400820B RID: 33291
			public static LocString FOOD_EXPECTATIONS_TOOLTIP = "This Duplicant desires food that is {0} quality or better";

			// Token: 0x0400820C RID: 33292
			public static LocString DECOR_EXPECTATIONS = "Decor Expectation";

			// Token: 0x0400820D RID: 33293
			public static LocString DECOR_EXPECTATIONS_TOOLTIP = "This Duplicant desires decor that is {0} or higher";

			// Token: 0x0400820E RID: 33294
			public static LocString QUALITYOFLIFE_EXPECTATIONS = "Morale";

			// Token: 0x0400820F RID: 33295
			public static LocString QUALITYOFLIFE_EXPECTATIONS_TOOLTIP = "This Duplicant requires " + UI.FormatAsLink("{0} Morale", "MORALE") + ".\n\nCurrent Morale:";

			// Token: 0x020024AB RID: 9387
			public class CONDITIONS_GROWING
			{
				// Token: 0x02002F09 RID: 12041
				public class WILD
				{
					// Token: 0x0400BD26 RID: 48422
					public static LocString BASE = "<b>Wild Growth\n[Life Cycle: {0}]</b>";

					// Token: 0x0400BD27 RID: 48423
					public static LocString TOOLTIP = "This plant will take {0} to grow in the wild";
				}

				// Token: 0x02002F0A RID: 12042
				public class DOMESTIC
				{
					// Token: 0x0400BD28 RID: 48424
					public static LocString BASE = "<b>Domestic Growth\n[Life Cycle: {0}]</b>";

					// Token: 0x0400BD29 RID: 48425
					public static LocString TOOLTIP = "This plant will take {0} to grow domestically";
				}

				// Token: 0x02002F0B RID: 12043
				public class ADDITIONAL_DOMESTIC
				{
					// Token: 0x0400BD2A RID: 48426
					public static LocString BASE = "<b>Additional Domestic Growth\n[Life Cycle: {0}]</b>";

					// Token: 0x0400BD2B RID: 48427
					public static LocString TOOLTIP = "This plant will take {0} to grow domestically";
				}

				// Token: 0x02002F0C RID: 12044
				public class WILD_DECOR
				{
					// Token: 0x0400BD2C RID: 48428
					public static LocString BASE = "<b>Wild Growth</b>";

					// Token: 0x0400BD2D RID: 48429
					public static LocString TOOLTIP = "This plant must have these requirements met to grow in the wild";
				}

				// Token: 0x02002F0D RID: 12045
				public class WILD_INSTANT
				{
					// Token: 0x0400BD2E RID: 48430
					public static LocString BASE = "<b>Wild Growth\n[{0}% Throughput]</b>";

					// Token: 0x0400BD2F RID: 48431
					public static LocString TOOLTIP = "This plant must have these requirements met to grow in the wild";
				}

				// Token: 0x02002F0E RID: 12046
				public class ADDITIONAL_DOMESTIC_INSTANT
				{
					// Token: 0x0400BD30 RID: 48432
					public static LocString BASE = "<b>Domestic Growth\n[{0}% Throughput]</b>";

					// Token: 0x0400BD31 RID: 48433
					public static LocString TOOLTIP = "This plant must have these requirements met to grow domestically";
				}
			}
		}

		// Token: 0x02001CA1 RID: 7329
		public class SCHEDULESCREEN
		{
			// Token: 0x04008210 RID: 33296
			public static LocString SCHEDULE_EDITOR = "Schedule Editor";

			// Token: 0x04008211 RID: 33297
			public static LocString SCHEDULE_NAME_DEFAULT = "Default Schedule";

			// Token: 0x04008212 RID: 33298
			public static LocString SCHEDULE_NAME_FORMAT = "Schedule {0}";

			// Token: 0x04008213 RID: 33299
			public static LocString SCHEDULE_DROPDOWN_ASSIGNED = "{0} (Assigned)";

			// Token: 0x04008214 RID: 33300
			public static LocString SCHEDULE_DROPDOWN_BLANK = "<i>Move Duplicant...</i>";

			// Token: 0x04008215 RID: 33301
			public static LocString SCHEDULE_DOWNTIME_MORALE = "Duplicants will receive {0} Morale from the scheduled Downtime shifts";

			// Token: 0x04008216 RID: 33302
			public static LocString RENAME_BUTTON_TOOLTIP = "Rename custom schedule";

			// Token: 0x04008217 RID: 33303
			public static LocString ALARM_BUTTON_ON_TOOLTIP = "Toggle Notifications\n\nSounds and notifications will play when shifts change for this schedule.\n\nENABLED\n" + UI.CLICK(UI.ClickType.Click) + " to disable";

			// Token: 0x04008218 RID: 33304
			public static LocString ALARM_BUTTON_OFF_TOOLTIP = "Toggle Notifications\n\nNo sounds or notifications will play for this schedule.\n\nDISABLED\n" + UI.CLICK(UI.ClickType.Click) + " to enable";

			// Token: 0x04008219 RID: 33305
			public static LocString DELETE_BUTTON_TOOLTIP = "Delete Schedule";

			// Token: 0x0400821A RID: 33306
			public static LocString PAINT_TOOLS = "Paint Tools:";

			// Token: 0x0400821B RID: 33307
			public static LocString ADD_SCHEDULE = "Add New Schedule";

			// Token: 0x0400821C RID: 33308
			public static LocString POO = "dar";

			// Token: 0x0400821D RID: 33309
			public static LocString DOWNTIME_MORALE = "Downtime Morale: {0}";

			// Token: 0x0400821E RID: 33310
			public static LocString ALARM_TITLE_ENABLED = "Alarm On";

			// Token: 0x0400821F RID: 33311
			public static LocString ALARM_TITLE_DISABLED = "Alarm Off";

			// Token: 0x04008220 RID: 33312
			public static LocString SETTINGS = "Settings";

			// Token: 0x04008221 RID: 33313
			public static LocString ALARM_BUTTON = "Shift Alarms";

			// Token: 0x04008222 RID: 33314
			public static LocString RESET_SETTINGS = "Reset Shifts";

			// Token: 0x04008223 RID: 33315
			public static LocString RESET_SETTINGS_TOOLTIP = "Restore this schedule to default shifts";

			// Token: 0x04008224 RID: 33316
			public static LocString DELETE_SCHEDULE = "Delete Schedule";

			// Token: 0x04008225 RID: 33317
			public static LocString DELETE_SCHEDULE_TOOLTIP = "Remove this schedule and unassign all Duplicants from it";

			// Token: 0x04008226 RID: 33318
			public static LocString DUPLICANT_NIGHTOWL_TOOLTIP = string.Concat(new string[]
			{
				DUPLICANTS.TRAITS.NIGHTOWL.NAME,
				"\n• All ",
				UI.PRE_KEYWORD,
				"Attributes",
				UI.PST_KEYWORD,
				" <b>+3</b> at night"
			});

			// Token: 0x04008227 RID: 33319
			public static LocString DUPLICANT_EARLYBIRD_TOOLTIP = string.Concat(new string[]
			{
				DUPLICANTS.TRAITS.EARLYBIRD.NAME,
				"\n• All ",
				UI.PRE_KEYWORD,
				"Attributes",
				UI.PST_KEYWORD,
				" <b>+2</b> in the morning"
			});
		}

		// Token: 0x02001CA2 RID: 7330
		public class COLONYLOSTSCREEN
		{
			// Token: 0x04008228 RID: 33320
			public static LocString COLONYLOST = "COLONY LOST";

			// Token: 0x04008229 RID: 33321
			public static LocString COLONYLOSTDESCRIPTION = "All Duplicants are dead or incapacitated.";

			// Token: 0x0400822A RID: 33322
			public static LocString RESTARTPROMPT = "Press <color=#F44A47><b>[ESC]</b></color> to return to a previous colony, or begin a new one.";

			// Token: 0x0400822B RID: 33323
			public static LocString DISMISSBUTTON = "DISMISS";

			// Token: 0x0400822C RID: 33324
			public static LocString QUITBUTTON = "MAIN MENU";
		}

		// Token: 0x02001CA3 RID: 7331
		public class VICTORYSCREEN
		{
			// Token: 0x0400822D RID: 33325
			public static LocString HEADER = "SUCCESS: IMPERATIVE ACHIEVED!";

			// Token: 0x0400822E RID: 33326
			public static LocString DESCRIPTION = "I have fulfilled the conditions of one of my Hardwired Imperatives";

			// Token: 0x0400822F RID: 33327
			public static LocString RESTARTPROMPT = "Press <color=#F44A47><b>[ESC]</b></color> to retire the colony and begin anew.";

			// Token: 0x04008230 RID: 33328
			public static LocString DISMISSBUTTON = "DISMISS";

			// Token: 0x04008231 RID: 33329
			public static LocString RETIREBUTTON = "RETIRE COLONY";
		}

		// Token: 0x02001CA4 RID: 7332
		public class GENESHUFFLERMESSAGE
		{
			// Token: 0x04008232 RID: 33330
			public static LocString HEADER = "NEURAL VACILLATION COMPLETE";

			// Token: 0x04008233 RID: 33331
			public static LocString BODY_SUCCESS = "Whew! <b>{0}'s</b> brain is still vibrating, but they've never felt better!\n\n<b>{0}</b> acquired the <b>{1}</b> trait.\n\n<b>{1}:</b>\n{2}";

			// Token: 0x04008234 RID: 33332
			public static LocString BODY_FAILURE = "The machine attempted to alter this Duplicant, but there's no improving on perfection.\n\n<b>{0}</b> already has all positive traits!";

			// Token: 0x04008235 RID: 33333
			public static LocString DISMISSBUTTON = "DISMISS";
		}

		// Token: 0x02001CA5 RID: 7333
		public class CRASHSCREEN
		{
			// Token: 0x04008236 RID: 33334
			public static LocString TITLE = "\"Whoops! We're sorry, but it seems your game has encountered an error. It's okay though - these errors are how we find and fix problems to make our game more fun for everyone. If you use the box below to submit a crash report to us, we can use this information to get the issue sorted out.\"";

			// Token: 0x04008237 RID: 33335
			public static LocString TITLE_MODS = "\"Oops-a-daisy! We're sorry, but it seems your game has encountered an error. If you uncheck all of the mods below, we will be able to help the next time this happens. Any mods that could be related to this error have already been unchecked.\"";

			// Token: 0x04008238 RID: 33336
			public static LocString HEADER = "OPTIONAL CRASH DESCRIPTION";

			// Token: 0x04008239 RID: 33337
			public static LocString HEADER_MODS = "ACTIVE MODS";

			// Token: 0x0400823A RID: 33338
			public static LocString BODY = "Help! A black hole ate my game!";

			// Token: 0x0400823B RID: 33339
			public static LocString THANKYOU = "Thank you!\n\nYou're making our game better, one crash at a time.";

			// Token: 0x0400823C RID: 33340
			public static LocString UPLOADINFO = "UPLOAD ADDITIONAL INFO ({0})";

			// Token: 0x0400823D RID: 33341
			public static LocString REPORTBUTTON = "REPORT CRASH";

			// Token: 0x0400823E RID: 33342
			public static LocString REPORTING = "REPORTING, PLEASE WAIT...";

			// Token: 0x0400823F RID: 33343
			public static LocString CONTINUEBUTTON = "CONTINUE GAME";

			// Token: 0x04008240 RID: 33344
			public static LocString MOREINFOBUTTON = "MORE INFO";

			// Token: 0x04008241 RID: 33345
			public static LocString COPYTOCLIPBOARDBUTTON = "COPY TO CLIPBOARD";

			// Token: 0x04008242 RID: 33346
			public static LocString QUITBUTTON = "QUIT TO DESKTOP";

			// Token: 0x04008243 RID: 33347
			public static LocString SAVEFAILED = "Save Failed: {0}";

			// Token: 0x04008244 RID: 33348
			public static LocString LOADFAILED = "Load Failed: {0}\nSave Version: {1}\nExpected: {2}";

			// Token: 0x04008245 RID: 33349
			public static LocString REPORTEDERROR = "Reported Error";
		}

		// Token: 0x02001CA6 RID: 7334
		public class DEMOOVERSCREEN
		{
			// Token: 0x04008246 RID: 33350
			public static LocString TIMEREMAINING = "Demo time remaining:";

			// Token: 0x04008247 RID: 33351
			public static LocString TIMERTOOLTIP = "Demo time remaining";

			// Token: 0x04008248 RID: 33352
			public static LocString TIMERINACTIVE = "Timer inactive";

			// Token: 0x04008249 RID: 33353
			public static LocString DEMOOVER = "END OF DEMO";

			// Token: 0x0400824A RID: 33354
			public static LocString DESCRIPTION = "Thank you for playing <color=#F44A47>Oxygen Not Included</color>!";

			// Token: 0x0400824B RID: 33355
			public static LocString DESCRIPTION_2 = "";

			// Token: 0x0400824C RID: 33356
			public static LocString QUITBUTTON = "RESET";
		}

		// Token: 0x02001CA7 RID: 7335
		public class CREDITSSCREEN
		{
			// Token: 0x0400824D RID: 33357
			public static LocString TITLE = "CREDITS";

			// Token: 0x0400824E RID: 33358
			public static LocString CLOSEBUTTON = "CLOSE";

			// Token: 0x020024AC RID: 9388
			public class THIRD_PARTY
			{
				// Token: 0x0400A09F RID: 41119
				public static LocString FMOD = "FMOD Sound System\nCopyright Firelight Technologies";

				// Token: 0x0400A0A0 RID: 41120
				public static LocString HARMONY = "Harmony by Andreas Pardeike";
			}
		}

		// Token: 0x02001CA8 RID: 7336
		public class ALLRESOURCESSCREEN
		{
			// Token: 0x0400824F RID: 33359
			public static LocString RESOURCES_TITLE = "RESOURCES";

			// Token: 0x04008250 RID: 33360
			public static LocString RESOURCES = "Resources";

			// Token: 0x04008251 RID: 33361
			public static LocString SEARCH = "Search";

			// Token: 0x04008252 RID: 33362
			public static LocString NAME = "Resource";

			// Token: 0x04008253 RID: 33363
			public static LocString TOTAL = "Total";

			// Token: 0x04008254 RID: 33364
			public static LocString AVAILABLE = "Available";

			// Token: 0x04008255 RID: 33365
			public static LocString RESERVED = "Reserved";

			// Token: 0x04008256 RID: 33366
			public static LocString SEARCH_PLACEHODLER = "Enter text...";

			// Token: 0x04008257 RID: 33367
			public static LocString FIRST_FRAME_NO_DATA = "...";

			// Token: 0x04008258 RID: 33368
			public static LocString PIN_TOOLTIP = "Check to pin resource to side panel";

			// Token: 0x04008259 RID: 33369
			public static LocString UNPIN_TOOLTIP = "Unpin resource";
		}

		// Token: 0x02001CA9 RID: 7337
		public class PRIORITYSCREEN
		{
			// Token: 0x0400825A RID: 33370
			public static LocString BASIC = "Set the order in which specific pending errands should be done\n\n1: Least Urgent\n9: Most Urgent";

			// Token: 0x0400825B RID: 33371
			public static LocString HIGH = "";

			// Token: 0x0400825C RID: 33372
			public static LocString TOP_PRIORITY = "Top Priority\n\nThis priority will override all other priorities and set the colony on Yellow Alert until the errand is completed";

			// Token: 0x0400825D RID: 33373
			public static LocString HIGH_TOGGLE = "";

			// Token: 0x0400825E RID: 33374
			public static LocString OPEN_JOBS_SCREEN = string.Concat(new string[]
			{
				UI.CLICK(UI.ClickType.Click),
				" to open the Priorities Screen\n\nDuplicants will first decide what to work on based on their ",
				UI.PRE_KEYWORD,
				"Duplicant Priorities",
				UI.PST_KEYWORD,
				", and then decide where to work based on ",
				UI.PRE_KEYWORD,
				"Building Priorities",
				UI.PST_KEYWORD
			});

			// Token: 0x0400825F RID: 33375
			public static LocString DIAGRAM = string.Concat(new string[]
			{
				"Duplicants will first choose what ",
				UI.PRE_KEYWORD,
				"Errand Type",
				UI.PST_KEYWORD,
				" to perform using their ",
				UI.PRE_KEYWORD,
				"Duplicant Priorities",
				UI.PST_KEYWORD,
				" ",
				UI.FormatAsHotKey(global::Action.ManagePriorities),
				"\n\nThey will then choose one ",
				UI.PRE_KEYWORD,
				"Errand",
				UI.PST_KEYWORD,
				" from within that type using the ",
				UI.PRE_KEYWORD,
				"Building Priorities",
				UI.PST_KEYWORD,
				" set by this tool"
			});

			// Token: 0x04008260 RID: 33376
			public static LocString DIAGRAM_TITLE = "BUILDING PRIORITY";
		}

		// Token: 0x02001CAA RID: 7338
		public class RESOURCESCREEN
		{
			// Token: 0x04008261 RID: 33377
			public static LocString HEADER = "RESOURCES";

			// Token: 0x04008262 RID: 33378
			public static LocString CATEGORY_TOOLTIP = "Counts all unallocated resources within reach\n\n" + UI.CLICK(UI.ClickType.Click) + " to expand";

			// Token: 0x04008263 RID: 33379
			public static LocString AVAILABLE_TOOLTIP = "Available: <b>{0}</b>\n({1} of {2} allocated to pending errands)";

			// Token: 0x04008264 RID: 33380
			public static LocString TREND_TOOLTIP = "The available amount of this resource has {0} {1} in the last cycle";

			// Token: 0x04008265 RID: 33381
			public static LocString TREND_TOOLTIP_NO_CHANGE = "The available amount of this resource has NOT CHANGED in the last cycle";

			// Token: 0x04008266 RID: 33382
			public static LocString FLAT_STR = "<b>NOT CHANGED</b>";

			// Token: 0x04008267 RID: 33383
			public static LocString INCREASING_STR = "<color=" + Constants.POSITIVE_COLOR_STR + ">INCREASED</color>";

			// Token: 0x04008268 RID: 33384
			public static LocString DECREASING_STR = "<color=" + Constants.NEGATIVE_COLOR_STR + ">DECREASED</color>";

			// Token: 0x04008269 RID: 33385
			public static LocString CLEAR_NEW_RESOURCES = "Clear New";

			// Token: 0x0400826A RID: 33386
			public static LocString CLEAR_ALL = "Unpin all resources";

			// Token: 0x0400826B RID: 33387
			public static LocString SEE_ALL = "+ See All ({0})";

			// Token: 0x0400826C RID: 33388
			public static LocString NEW_TAG = "NEW";
		}

		// Token: 0x02001CAB RID: 7339
		public class CONFIRMDIALOG
		{
			// Token: 0x0400826D RID: 33389
			public static LocString OK = "OK";

			// Token: 0x0400826E RID: 33390
			public static LocString CANCEL = "CANCEL";

			// Token: 0x0400826F RID: 33391
			public static LocString DIALOG_HEADER = "MESSAGE";
		}

		// Token: 0x02001CAC RID: 7340
		public class FACADE_SELECTION_PANEL
		{
			// Token: 0x04008270 RID: 33392
			public static LocString HEADER = "Select Blueprint";

			// Token: 0x04008271 RID: 33393
			public static LocString STORE_BUTTON_TOOLTIP = "More Blueprints";
		}

		// Token: 0x02001CAD RID: 7341
		public class FILE_NAME_DIALOG
		{
			// Token: 0x04008272 RID: 33394
			public static LocString ENTER_TEXT = "Enter Text...";
		}

		// Token: 0x02001CAE RID: 7342
		public class MINION_IDENTITY_SORT
		{
			// Token: 0x04008273 RID: 33395
			public static LocString TITLE = "Sort By";

			// Token: 0x04008274 RID: 33396
			public static LocString NAME = "Duplicant";

			// Token: 0x04008275 RID: 33397
			public static LocString ROLE = "Role";

			// Token: 0x04008276 RID: 33398
			public static LocString PERMISSION = "Permission";
		}

		// Token: 0x02001CAF RID: 7343
		public class UISIDESCREENS
		{
			// Token: 0x020024AD RID: 9389
			public class ARTABLESELECTIONSIDESCREEN
			{
				// Token: 0x0400A0A1 RID: 41121
				public static LocString TITLE = "Style Selection";

				// Token: 0x0400A0A2 RID: 41122
				public static LocString BUTTON = "Repaint";

				// Token: 0x0400A0A3 RID: 41123
				public static LocString BUTTON_TOOLTIP = "Clears current artwork\n\nCreates errand for a skilled Duplicant to paint selected style";

				// Token: 0x0400A0A4 RID: 41124
				public static LocString CLEAR_BUTTON_TOOLTIP = "Clears current artwork\n\nAllows a skilled Duplicant to create artwork of their choice";
			}

			// Token: 0x020024AE RID: 9390
			public class ARTIFACTANALYSISSIDESCREEN
			{
				// Token: 0x0400A0A5 RID: 41125
				public static LocString NO_ARTIFACTS_DISCOVERED = "No artifacts analyzed";

				// Token: 0x0400A0A6 RID: 41126
				public static LocString NO_ARTIFACTS_DISCOVERED_TOOLTIP = "Analyzing artifacts requires a Duplicant with the Masterworks skill";
			}

			// Token: 0x020024AF RID: 9391
			public class BUTTONMENUSIDESCREEN
			{
				// Token: 0x0400A0A7 RID: 41127
				public static LocString TITLE = "Building Menu";

				// Token: 0x0400A0A8 RID: 41128
				public static LocString ALLOW_INTERNAL_CONSTRUCTOR = "Enable Auto-Delivery";

				// Token: 0x0400A0A9 RID: 41129
				public static LocString ALLOW_INTERNAL_CONSTRUCTOR_TOOLTIP = "Order Duplicants to deliver {0}" + UI.FormatAsLink("s", "NONE") + " to this building automatically when they need replacing";

				// Token: 0x0400A0AA RID: 41130
				public static LocString DISALLOW_INTERNAL_CONSTRUCTOR = "Cancel Auto-Delivery";

				// Token: 0x0400A0AB RID: 41131
				public static LocString DISALLOW_INTERNAL_CONSTRUCTOR_TOOLTIP = "Cancel automatic {0} deliveries to this building";
			}

			// Token: 0x020024B0 RID: 9392
			public class CONFIGURECONSUMERSIDESCREEN
			{
				// Token: 0x0400A0AC RID: 41132
				public static LocString TITLE = "Configure Building";

				// Token: 0x0400A0AD RID: 41133
				public static LocString SELECTION_DESCRIPTION_HEADER = "Description";
			}

			// Token: 0x020024B1 RID: 9393
			public class TREEFILTERABLESIDESCREEN
			{
				// Token: 0x0400A0AE RID: 41134
				public static LocString TITLE = "Element Filter";

				// Token: 0x0400A0AF RID: 41135
				public static LocString TITLE_CRITTER = "Critter Filter";

				// Token: 0x0400A0B0 RID: 41136
				public static LocString ALLBUTTON = "All";

				// Token: 0x0400A0B1 RID: 41137
				public static LocString ALLBUTTONTOOLTIP = "Allow storage of all resource categories in this container";

				// Token: 0x0400A0B2 RID: 41138
				public static LocString CATEGORYBUTTONTOOLTIP = "Allow storage of anything in the {0} resource category";

				// Token: 0x0400A0B3 RID: 41139
				public static LocString MATERIALBUTTONTOOLTIP = "Add or remove this material from storage";

				// Token: 0x0400A0B4 RID: 41140
				public static LocString ONLYALLOWTRANSPORTITEMSBUTTON = "Sweep Only";

				// Token: 0x0400A0B5 RID: 41141
				public static LocString ONLYALLOWTRANSPORTITEMSBUTTONTOOLTIP = "Only store objects marked Sweep <color=#F44A47><b>[K]</b></color> in this container";

				// Token: 0x0400A0B6 RID: 41142
				public static LocString ONLYALLOWSPICEDITEMSBUTTON = "Spiced Food Only";

				// Token: 0x0400A0B7 RID: 41143
				public static LocString ONLYALLOWSPICEDITEMSBUTTONTOOLTIP = "Only store foods that have been spiced at the " + UI.PRE_KEYWORD + "Spice Grinder" + UI.PST_KEYWORD;
			}

			// Token: 0x020024B2 RID: 9394
			public class TELESCOPESIDESCREEN
			{
				// Token: 0x0400A0B8 RID: 41144
				public static LocString TITLE = "Telescope Configuration";

				// Token: 0x0400A0B9 RID: 41145
				public static LocString NO_SELECTED_ANALYSIS_TARGET = "No analysis focus selected\nOpen the " + UI.FormatAsManagementMenu("Starmap", global::Action.ManageStarmap) + " to selected a focus";

				// Token: 0x0400A0BA RID: 41146
				public static LocString ANALYSIS_TARGET_SELECTED = "Object focus selected\nAnalysis underway";

				// Token: 0x0400A0BB RID: 41147
				public static LocString OPENSTARMAPBUTTON = "OPEN STARMAP";

				// Token: 0x0400A0BC RID: 41148
				public static LocString ANALYSIS_TARGET_HEADER = "Object Analysis";
			}

			// Token: 0x020024B3 RID: 9395
			public class TEMPORALTEARSIDESCREEN
			{
				// Token: 0x0400A0BD RID: 41149
				public static LocString TITLE = "Temporal Tear";

				// Token: 0x0400A0BE RID: 41150
				public static LocString BUTTON_OPEN = "Enter Tear";

				// Token: 0x0400A0BF RID: 41151
				public static LocString BUTTON_CLOSED = "Tear Closed";

				// Token: 0x0400A0C0 RID: 41152
				public static LocString BUTTON_LABEL = "Enter Temporal Tear";

				// Token: 0x0400A0C1 RID: 41153
				public static LocString CONFIRM_POPUP_MESSAGE = "Are you sure you want to fire this?";

				// Token: 0x0400A0C2 RID: 41154
				public static LocString CONFIRM_POPUP_CONFIRM = "Yes, I'm ready for a meteor shower.";

				// Token: 0x0400A0C3 RID: 41155
				public static LocString CONFIRM_POPUP_CANCEL = "No, I need more time to prepare.";

				// Token: 0x0400A0C4 RID: 41156
				public static LocString CONFIRM_POPUP_TITLE = "Temporal Tear Opener";
			}

			// Token: 0x020024B4 RID: 9396
			public class RAILGUNSIDESCREEN
			{
				// Token: 0x0400A0C5 RID: 41157
				public static LocString TITLE = "Launcher Configuration";

				// Token: 0x0400A0C6 RID: 41158
				public static LocString NO_SELECTED_LAUNCH_TARGET = "No destination selected\nOpen the " + UI.FormatAsManagementMenu("Starmap", global::Action.ManageStarmap) + " to set a course";

				// Token: 0x0400A0C7 RID: 41159
				public static LocString LAUNCH_TARGET_SELECTED = "Launcher destination {0} set";

				// Token: 0x0400A0C8 RID: 41160
				public static LocString OPENSTARMAPBUTTON = "OPEN STARMAP";

				// Token: 0x0400A0C9 RID: 41161
				public static LocString LAUNCH_RESOURCES_HEADER = "Launch Resources:";

				// Token: 0x0400A0CA RID: 41162
				public static LocString MINIMUM_PAYLOAD_MASS = "Minimum launch mass:";
			}

			// Token: 0x020024B5 RID: 9397
			public class CLUSTERWORLDSIDESCREEN
			{
				// Token: 0x0400A0CB RID: 41163
				public static LocString TITLE = UI.CLUSTERMAP.PLANETOID;

				// Token: 0x0400A0CC RID: 41164
				public static LocString VIEW_WORLD = "Oversee " + UI.CLUSTERMAP.PLANETOID;

				// Token: 0x0400A0CD RID: 41165
				public static LocString VIEW_WORLD_DISABLE_TOOLTIP = "Cannot view " + UI.CLUSTERMAP.PLANETOID;

				// Token: 0x0400A0CE RID: 41166
				public static LocString VIEW_WORLD_TOOLTIP = "View this " + UI.CLUSTERMAP.PLANETOID + "'s surface";
			}

			// Token: 0x020024B6 RID: 9398
			public class ROCKETMODULESIDESCREEN
			{
				// Token: 0x0400A0CF RID: 41167
				public static LocString TITLE = "Rocket Module";

				// Token: 0x0400A0D0 RID: 41168
				public static LocString CHANGEMODULEPANEL = "Add or Change Module";

				// Token: 0x0400A0D1 RID: 41169
				public static LocString ENGINE_MAX_HEIGHT = "This engine allows a <b>Maximum Rocket Height</b> of {0}";

				// Token: 0x02002F0F RID: 12047
				public class MODULESTATCHANGE
				{
					// Token: 0x0400BD32 RID: 48434
					public static LocString TITLE = "Rocket stats on construction:";

					// Token: 0x0400BD33 RID: 48435
					public static LocString BURDEN = "    • " + DUPLICANTS.ATTRIBUTES.ROCKETBURDEN.NAME + ": {0} ({1})";

					// Token: 0x0400BD34 RID: 48436
					public static LocString RANGE = string.Concat(new string[]
					{
						"    • Potential ",
						DUPLICANTS.ATTRIBUTES.FUELRANGEPERKILOGRAM.NAME,
						": {0}/1",
						UI.UNITSUFFIXES.MASS.KILOGRAM,
						" Fuel ({1})"
					});

					// Token: 0x0400BD35 RID: 48437
					public static LocString SPEED = "    • Speed: {0} ({1})";

					// Token: 0x0400BD36 RID: 48438
					public static LocString ENGINEPOWER = "    • " + DUPLICANTS.ATTRIBUTES.ROCKETENGINEPOWER.NAME + ": {0} ({1})";

					// Token: 0x0400BD37 RID: 48439
					public static LocString HEIGHT = "    • " + DUPLICANTS.ATTRIBUTES.HEIGHT.NAME + ": {0}/{2} ({1})";

					// Token: 0x0400BD38 RID: 48440
					public static LocString HEIGHT_NOMAX = "    • " + DUPLICANTS.ATTRIBUTES.HEIGHT.NAME + ": {0} ({1})";

					// Token: 0x0400BD39 RID: 48441
					public static LocString POSITIVEDELTA = UI.FormatAsPositiveModifier("{0}");

					// Token: 0x0400BD3A RID: 48442
					public static LocString NEGATIVEDELTA = UI.FormatAsNegativeModifier("{0}");
				}

				// Token: 0x02002F10 RID: 12048
				public class BUTTONSWAPMODULEUP
				{
					// Token: 0x0400BD3B RID: 48443
					public static LocString DESC = "Swap this rocket module with the one above";

					// Token: 0x0400BD3C RID: 48444
					public static LocString INVALID = "No module above may be swapped.\n\n    • A module above may be unable to have modules placed above it.\n    • A module above may be unable to fit into the space below it.\n    • This module may be unable to fit into the space above it.";
				}

				// Token: 0x02002F11 RID: 12049
				public class BUTTONVIEWINTERIOR
				{
					// Token: 0x0400BD3D RID: 48445
					public static LocString LABEL = "View Interior";

					// Token: 0x0400BD3E RID: 48446
					public static LocString DESC = "What's goin' on in there?";

					// Token: 0x0400BD3F RID: 48447
					public static LocString INVALID = "This module does not have an interior view";
				}

				// Token: 0x02002F12 RID: 12050
				public class BUTTONVIEWEXTERIOR
				{
					// Token: 0x0400BD40 RID: 48448
					public static LocString LABEL = "View Exterior";

					// Token: 0x0400BD41 RID: 48449
					public static LocString DESC = "Switch to external world view";

					// Token: 0x0400BD42 RID: 48450
					public static LocString INVALID = "Not available in flight";
				}

				// Token: 0x02002F13 RID: 12051
				public class BUTTONSWAPMODULEDOWN
				{
					// Token: 0x0400BD43 RID: 48451
					public static LocString DESC = "Swap this rocket module with the one below";

					// Token: 0x0400BD44 RID: 48452
					public static LocString INVALID = "No module below may be swapped.\n\n    • A module below may be unable to have modules placed below it.\n    • A module below may be unable to fit into the space above it.\n    • This module may be unable to fit into the space below it.";
				}

				// Token: 0x02002F14 RID: 12052
				public class BUTTONCHANGEMODULE
				{
					// Token: 0x0400BD45 RID: 48453
					public static LocString DESC = "Swap this module for a different module";

					// Token: 0x0400BD46 RID: 48454
					public static LocString INVALID = "This module cannot be changed to a different type";
				}

				// Token: 0x02002F15 RID: 12053
				public class BUTTONREMOVEMODULE
				{
					// Token: 0x0400BD47 RID: 48455
					public static LocString DESC = "Remove this module";

					// Token: 0x0400BD48 RID: 48456
					public static LocString INVALID = "This module cannot be removed";
				}

				// Token: 0x02002F16 RID: 12054
				public class ADDMODULE
				{
					// Token: 0x0400BD49 RID: 48457
					public static LocString DESC = "Add a new module above this one";

					// Token: 0x0400BD4A RID: 48458
					public static LocString INVALID = "Modules cannot be added above this module, or there is no room above to add a module";
				}
			}

			// Token: 0x020024B7 RID: 9399
			public class CLUSTERLOCATIONFILTERSIDESCREEN
			{
				// Token: 0x0400A0D2 RID: 41170
				public static LocString TITLE = "Location Filter";

				// Token: 0x0400A0D3 RID: 41171
				public static LocString HEADER = "Send Green signal at locations";

				// Token: 0x0400A0D4 RID: 41172
				public static LocString EMPTY_SPACE_ROW = "In Space";
			}

			// Token: 0x020024B8 RID: 9400
			public class DISPENSERSIDESCREEN
			{
				// Token: 0x0400A0D5 RID: 41173
				public static LocString TITLE = "Dispenser";

				// Token: 0x0400A0D6 RID: 41174
				public static LocString BUTTON_CANCEL = "Cancel order";

				// Token: 0x0400A0D7 RID: 41175
				public static LocString BUTTON_DISPENSE = "Dispense item";
			}

			// Token: 0x020024B9 RID: 9401
			public class ROCKETRESTRICTIONSIDESCREEN
			{
				// Token: 0x0400A0D8 RID: 41176
				public static LocString TITLE = "Rocket Restrictions";

				// Token: 0x0400A0D9 RID: 41177
				public static LocString BUILDING_RESTRICTIONS_LABEL = "Interior Building Restrictions";

				// Token: 0x0400A0DA RID: 41178
				public static LocString NONE_RESTRICTION_BUTTON = "None";

				// Token: 0x0400A0DB RID: 41179
				public static LocString NONE_RESTRICTION_BUTTON_TOOLTIP = "There are no restrictions on buildings inside this rocket";

				// Token: 0x0400A0DC RID: 41180
				public static LocString GROUNDED_RESTRICTION_BUTTON = "Grounded";

				// Token: 0x0400A0DD RID: 41181
				public static LocString GROUNDED_RESTRICTION_BUTTON_TOOLTIP = "Buildings with their access restricted cannot be operated while grounded, though they can still be filled";

				// Token: 0x0400A0DE RID: 41182
				public static LocString AUTOMATION = "Automation Controlled";

				// Token: 0x0400A0DF RID: 41183
				public static LocString AUTOMATION_TOOLTIP = "Building restrictions are managed by automation\n\nBuildings with their access restricted cannot be operated, though they can still be filled";
			}

			// Token: 0x020024BA RID: 9402
			public class HABITATMODULESIDESCREEN
			{
				// Token: 0x0400A0E0 RID: 41184
				public static LocString TITLE = "Spacefarer Module";

				// Token: 0x0400A0E1 RID: 41185
				public static LocString VIEW_BUTTON = "View Interior";

				// Token: 0x0400A0E2 RID: 41186
				public static LocString VIEW_BUTTON_TOOLTIP = "What's goin' on in there?";
			}

			// Token: 0x020024BB RID: 9403
			public class HARVESTMODULESIDESCREEN
			{
				// Token: 0x0400A0E3 RID: 41187
				public static LocString TITLE = "Resource Gathering";

				// Token: 0x0400A0E4 RID: 41188
				public static LocString MINING_IN_PROGRESS = "Drilling...";

				// Token: 0x0400A0E5 RID: 41189
				public static LocString MINING_STOPPED = "Not drilling";

				// Token: 0x0400A0E6 RID: 41190
				public static LocString ENABLE = "Enable Drill";

				// Token: 0x0400A0E7 RID: 41191
				public static LocString DISABLE = "Disable Drill";
			}

			// Token: 0x020024BC RID: 9404
			public class SELECTMODULESIDESCREEN
			{
				// Token: 0x0400A0E8 RID: 41192
				public static LocString TITLE = "Select Module";

				// Token: 0x0400A0E9 RID: 41193
				public static LocString BUILDBUTTON = "Build";

				// Token: 0x02002F17 RID: 12055
				public class CONSTRAINTS
				{
					// Token: 0x020030FC RID: 12540
					public class RESEARCHED
					{
						// Token: 0x0400C29E RID: 49822
						public static LocString COMPLETE = "Research Completed";

						// Token: 0x0400C29F RID: 49823
						public static LocString FAILED = "Research Incomplete";
					}

					// Token: 0x020030FD RID: 12541
					public class MATERIALS_AVAILABLE
					{
						// Token: 0x0400C2A0 RID: 49824
						public static LocString COMPLETE = "Materials available";

						// Token: 0x0400C2A1 RID: 49825
						public static LocString FAILED = "• Materials unavailable";
					}

					// Token: 0x020030FE RID: 12542
					public class ONE_COMMAND_PER_ROCKET
					{
						// Token: 0x0400C2A2 RID: 49826
						public static LocString COMPLETE = "";

						// Token: 0x0400C2A3 RID: 49827
						public static LocString FAILED = "• Command module already installed";
					}

					// Token: 0x020030FF RID: 12543
					public class ONE_ENGINE_PER_ROCKET
					{
						// Token: 0x0400C2A4 RID: 49828
						public static LocString COMPLETE = "";

						// Token: 0x0400C2A5 RID: 49829
						public static LocString FAILED = "• Engine module already installed";
					}

					// Token: 0x02003100 RID: 12544
					public class ENGINE_AT_BOTTOM
					{
						// Token: 0x0400C2A6 RID: 49830
						public static LocString COMPLETE = "";

						// Token: 0x0400C2A7 RID: 49831
						public static LocString FAILED = "• Must install at bottom of rocket";
					}

					// Token: 0x02003101 RID: 12545
					public class TOP_ONLY
					{
						// Token: 0x0400C2A8 RID: 49832
						public static LocString COMPLETE = "";

						// Token: 0x0400C2A9 RID: 49833
						public static LocString FAILED = "• Must install at top of rocket";
					}

					// Token: 0x02003102 RID: 12546
					public class SPACE_AVAILABLE
					{
						// Token: 0x0400C2AA RID: 49834
						public static LocString COMPLETE = "";

						// Token: 0x0400C2AB RID: 49835
						public static LocString FAILED = "• Space above rocket blocked";
					}

					// Token: 0x02003103 RID: 12547
					public class PASSENGER_MODULE_AVAILABLE
					{
						// Token: 0x0400C2AC RID: 49836
						public static LocString COMPLETE = "";

						// Token: 0x0400C2AD RID: 49837
						public static LocString FAILED = "• Max number of passenger modules installed";
					}

					// Token: 0x02003104 RID: 12548
					public class MAX_MODULES
					{
						// Token: 0x0400C2AE RID: 49838
						public static LocString COMPLETE = "";

						// Token: 0x0400C2AF RID: 49839
						public static LocString FAILED = "• Max module limit of engine reached";
					}

					// Token: 0x02003105 RID: 12549
					public class MAX_HEIGHT
					{
						// Token: 0x0400C2B0 RID: 49840
						public static LocString COMPLETE = "";

						// Token: 0x0400C2B1 RID: 49841
						public static LocString FAILED = "• Engine's height limit reached or exceeded";

						// Token: 0x0400C2B2 RID: 49842
						public static LocString FAILED_NO_ENGINE = "• Rocket requires space for an engine";
					}
				}
			}

			// Token: 0x020024BD RID: 9405
			public class FILTERSIDESCREEN
			{
				// Token: 0x0400A0EA RID: 41194
				public static LocString TITLE = "Filter Outputs";

				// Token: 0x0400A0EB RID: 41195
				public static LocString NO_SELECTION = "None";

				// Token: 0x0400A0EC RID: 41196
				public static LocString OUTPUTELEMENTHEADER = "Output 1";

				// Token: 0x0400A0ED RID: 41197
				public static LocString SELECTELEMENTHEADER = "Output 2";

				// Token: 0x0400A0EE RID: 41198
				public static LocString OUTPUTRED = "Output Red";

				// Token: 0x0400A0EF RID: 41199
				public static LocString OUTPUTGREEN = "Output Green";

				// Token: 0x0400A0F0 RID: 41200
				public static LocString NOELEMENTSELECTED = "No element selected";

				// Token: 0x02002F18 RID: 12056
				public static class UNFILTEREDELEMENTS
				{
					// Token: 0x0400BD4B RID: 48459
					public static LocString GAS = "Gas Output:\nAll";

					// Token: 0x0400BD4C RID: 48460
					public static LocString LIQUID = "Liquid Output:\nAll";

					// Token: 0x0400BD4D RID: 48461
					public static LocString SOLID = "Solid Output:\nAll";
				}

				// Token: 0x02002F19 RID: 12057
				public static class FILTEREDELEMENT
				{
					// Token: 0x0400BD4E RID: 48462
					public static LocString GAS = "Filtered Gas Output:\n{0}";

					// Token: 0x0400BD4F RID: 48463
					public static LocString LIQUID = "Filtered Liquid Output:\n{0}";

					// Token: 0x0400BD50 RID: 48464
					public static LocString SOLID = "Filtered Solid Output:\n{0}";
				}
			}

			// Token: 0x020024BE RID: 9406
			public class LOGICBROADCASTCHANNELSIDESCREEN
			{
				// Token: 0x0400A0F1 RID: 41201
				public static LocString TITLE = "Channel Selector";

				// Token: 0x0400A0F2 RID: 41202
				public static LocString HEADER = "Channel Selector";

				// Token: 0x0400A0F3 RID: 41203
				public static LocString IN_RANGE = "In Range";

				// Token: 0x0400A0F4 RID: 41204
				public static LocString OUT_OF_RANGE = "Out of Range";

				// Token: 0x0400A0F5 RID: 41205
				public static LocString NO_SENDERS = "No Channels Available";

				// Token: 0x0400A0F6 RID: 41206
				public static LocString NO_SENDERS_DESC = "Build a " + BUILDINGS.PREFABS.LOGICINTERASTEROIDSENDER.NAME + " to transmit a signal.";
			}

			// Token: 0x020024BF RID: 9407
			public class CONDITIONLISTSIDESCREEN
			{
				// Token: 0x0400A0F7 RID: 41207
				public static LocString TITLE = "Condition List";
			}

			// Token: 0x020024C0 RID: 9408
			public class FABRICATORSIDESCREEN
			{
				// Token: 0x0400A0F8 RID: 41208
				public static LocString TITLE = "{0} Production Orders";

				// Token: 0x0400A0F9 RID: 41209
				public static LocString SUBTITLE = "Recipes";

				// Token: 0x0400A0FA RID: 41210
				public static LocString NORECIPEDISCOVERED = "No discovered recipes";

				// Token: 0x0400A0FB RID: 41211
				public static LocString NORECIPEDISCOVERED_BODY = "Discover new ingredients or research new technology to unlock some recipes.";

				// Token: 0x0400A0FC RID: 41212
				public static LocString NORECIPESELECTED = "No recipe selected";

				// Token: 0x0400A0FD RID: 41213
				public static LocString SELECTRECIPE = "Select a recipe to fabricate.";

				// Token: 0x0400A0FE RID: 41214
				public static LocString COST = "<b>Ingredients:</b>\n";

				// Token: 0x0400A0FF RID: 41215
				public static LocString RESULTREQUIREMENTS = "<b>Requirements:</b>";

				// Token: 0x0400A100 RID: 41216
				public static LocString RESULTEFFECTS = "<b>Effects:</b>";

				// Token: 0x0400A101 RID: 41217
				public static LocString KG = "- {0}: {1}\n";

				// Token: 0x0400A102 RID: 41218
				public static LocString INFORMATION = "INFORMATION";

				// Token: 0x0400A103 RID: 41219
				public static LocString CANCEL = "Cancel";

				// Token: 0x0400A104 RID: 41220
				public static LocString RECIPERQUIREMENT = "{0}: {1} / {2}";

				// Token: 0x0400A105 RID: 41221
				public static LocString RECIPEPRODUCT = "{0}: {1}";

				// Token: 0x0400A106 RID: 41222
				public static LocString UNITS_AND_CALS = "{0} [{1}]";

				// Token: 0x0400A107 RID: 41223
				public static LocString CALS = "{0}";

				// Token: 0x0400A108 RID: 41224
				public static LocString QUEUED_MISSING_INGREDIENTS_TOOLTIP = "Missing {0} of {1}\n";

				// Token: 0x0400A109 RID: 41225
				public static LocString CURRENT_ORDER = "Current order: {0}";

				// Token: 0x0400A10A RID: 41226
				public static LocString NEXT_ORDER = "Next order: {0}";

				// Token: 0x0400A10B RID: 41227
				public static LocString NO_WORKABLE_ORDER = "No workable order";

				// Token: 0x0400A10C RID: 41228
				public static LocString RECIPE_DETAILS = "Recipe Details";

				// Token: 0x0400A10D RID: 41229
				public static LocString RECIPE_QUEUE = "Order Production Quantity:";

				// Token: 0x0400A10E RID: 41230
				public static LocString RECIPE_FOREVER = "Forever";

				// Token: 0x0400A10F RID: 41231
				public static LocString INGREDIENTS = "<b>Ingredients:</b>";

				// Token: 0x0400A110 RID: 41232
				public static LocString RECIPE_EFFECTS = "<b>Effects:</b>";

				// Token: 0x0400A111 RID: 41233
				public static LocString ALLOW_MUTANT_SEED_INGREDIENTS = "Building accepts mutant seeds";

				// Token: 0x0400A112 RID: 41234
				public static LocString ALLOW_MUTANT_SEED_INGREDIENTS_TOOLTIP = "Toggle whether Duplicants will deliver mutant seed species to this building as recipe ingredients.";

				// Token: 0x02002F1A RID: 12058
				public class TOOLTIPS
				{
					// Token: 0x0400BD51 RID: 48465
					public static LocString RECIPERQUIREMENT_SUFFICIENT = "This recipe consumes {1} of an available {2} of {0}";

					// Token: 0x0400BD52 RID: 48466
					public static LocString RECIPERQUIREMENT_INSUFFICIENT = "This recipe requires {1} {0}\nAvailable: {2}";

					// Token: 0x0400BD53 RID: 48467
					public static LocString RECIPEPRODUCT = "This recipe produces {1} {0}";
				}

				// Token: 0x02002F1B RID: 12059
				public class EFFECTS
				{
					// Token: 0x0400BD54 RID: 48468
					public static LocString OXYGEN_TANK = STRINGS.EQUIPMENT.PREFABS.OXYGEN_TANK.NAME + " ({0})";

					// Token: 0x0400BD55 RID: 48469
					public static LocString OXYGEN_TANK_UNDERWATER = STRINGS.EQUIPMENT.PREFABS.OXYGEN_TANK_UNDERWATER.NAME + " ({0})";

					// Token: 0x0400BD56 RID: 48470
					public static LocString JETSUIT_TANK = STRINGS.EQUIPMENT.PREFABS.JET_SUIT.TANK_EFFECT_NAME + " ({0})";

					// Token: 0x0400BD57 RID: 48471
					public static LocString LEADSUIT_BATTERY = STRINGS.EQUIPMENT.PREFABS.LEAD_SUIT.BATTERY_EFFECT_NAME + " ({0})";

					// Token: 0x0400BD58 RID: 48472
					public static LocString COOL_VEST = STRINGS.EQUIPMENT.PREFABS.COOL_VEST.NAME + " ({0})";

					// Token: 0x0400BD59 RID: 48473
					public static LocString WARM_VEST = STRINGS.EQUIPMENT.PREFABS.WARM_VEST.NAME + " ({0})";

					// Token: 0x0400BD5A RID: 48474
					public static LocString FUNKY_VEST = STRINGS.EQUIPMENT.PREFABS.FUNKY_VEST.NAME + " ({0})";

					// Token: 0x0400BD5B RID: 48475
					public static LocString RESEARCHPOINT = "{0}: +1";
				}

				// Token: 0x02002F1C RID: 12060
				public class RECIPE_CATEGORIES
				{
					// Token: 0x0400BD5C RID: 48476
					public static LocString ATMO_SUIT_FACADES = "Atmo Suit Styles";

					// Token: 0x0400BD5D RID: 48477
					public static LocString JET_SUIT_FACADES = "Jet Suit Styles";

					// Token: 0x0400BD5E RID: 48478
					public static LocString LEAD_SUIT_FACADES = "Lead Suit Styles";

					// Token: 0x0400BD5F RID: 48479
					public static LocString PRIMO_GARB_FACADES = "Primo Garb Styles";
				}
			}

			// Token: 0x020024C1 RID: 9409
			public class ASSIGNMENTGROUPCONTROLLER
			{
				// Token: 0x0400A113 RID: 41235
				public static LocString TITLE = "Duplicant Assignment";

				// Token: 0x0400A114 RID: 41236
				public static LocString PILOT = "Pilot";

				// Token: 0x0400A115 RID: 41237
				public static LocString OFFWORLD = "Offworld";

				// Token: 0x02002F1D RID: 12061
				public class TOOLTIPS
				{
					// Token: 0x0400BD60 RID: 48480
					public static LocString DIFFERENT_WORLD = "This Duplicant is on a different " + UI.CLUSTERMAP.PLANETOID;

					// Token: 0x0400BD61 RID: 48481
					public static LocString ASSIGN = "<b>Add</b> this Duplicant to rocket crew";

					// Token: 0x0400BD62 RID: 48482
					public static LocString UNASSIGN = "<b>Remove</b> this Duplicant from rocket crew";
				}
			}

			// Token: 0x020024C2 RID: 9410
			public class LAUNCHPADSIDESCREEN
			{
				// Token: 0x0400A116 RID: 41238
				public static LocString TITLE = "Rocket Platform";

				// Token: 0x0400A117 RID: 41239
				public static LocString WAITING_TO_LAND_PANEL = "Waiting to land";

				// Token: 0x0400A118 RID: 41240
				public static LocString NO_ROCKETS_WAITING = "No rockets in orbit";

				// Token: 0x0400A119 RID: 41241
				public static LocString IN_ORBIT_ABOVE_PANEL = "Rockets in orbit";

				// Token: 0x0400A11A RID: 41242
				public static LocString NEW_ROCKET_BUTTON = "NEW ROCKET";

				// Token: 0x0400A11B RID: 41243
				public static LocString LAND_BUTTON = "LAND HERE";

				// Token: 0x0400A11C RID: 41244
				public static LocString CANCEL_LAND_BUTTON = "CANCEL";

				// Token: 0x0400A11D RID: 41245
				public static LocString LAUNCH_BUTTON = "BEGIN LAUNCH SEQUENCE";

				// Token: 0x0400A11E RID: 41246
				public static LocString LAUNCH_BUTTON_DEBUG = "BEGIN LAUNCH SEQUENCE (DEBUG ENABLED)";

				// Token: 0x0400A11F RID: 41247
				public static LocString LAUNCH_BUTTON_TOOLTIP = "Blast off!";

				// Token: 0x0400A120 RID: 41248
				public static LocString LAUNCH_BUTTON_NOT_READY_TOOLTIP = "This rocket is <b>not</b> ready to launch\n\n<b>Review the Launch Checklist in the status panel for more detail</b>";

				// Token: 0x0400A121 RID: 41249
				public static LocString LAUNCH_WARNINGS_BUTTON = "ACKNOWLEDGE WARNINGS";

				// Token: 0x0400A122 RID: 41250
				public static LocString LAUNCH_WARNINGS_BUTTON_TOOLTIP = "Some items in the Launch Checklist require attention\n\n<b>" + UI.CLICK(UI.ClickType.Click) + " to ignore warnings and proceed with launch</b>";

				// Token: 0x0400A123 RID: 41251
				public static LocString LAUNCH_REQUESTED_BUTTON = "CANCEL LAUNCH";

				// Token: 0x0400A124 RID: 41252
				public static LocString LAUNCH_REQUESTED_BUTTON_TOOLTIP = "This rocket will take off as soon as a Duplicant takes the controls\n\n<b>" + UI.CLICK(UI.ClickType.Click) + " to cancel launch</b>";

				// Token: 0x0400A125 RID: 41253
				public static LocString LAUNCH_AUTOMATION_CONTROLLED = "AUTOMATION CONTROLLED";

				// Token: 0x0400A126 RID: 41254
				public static LocString LAUNCH_AUTOMATION_CONTROLLED_TOOLTIP = "This " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + "'s launch operation is controlled by automation signals";

				// Token: 0x02002F1E RID: 12062
				public class STATUS
				{
					// Token: 0x0400BD63 RID: 48483
					public static LocString STILL_PREPPING = "Launch Checklist Incomplete";

					// Token: 0x0400BD64 RID: 48484
					public static LocString READY_FOR_LAUNCH = "Ready to Launch";

					// Token: 0x0400BD65 RID: 48485
					public static LocString LOADING_CREW = "Loading crew...";

					// Token: 0x0400BD66 RID: 48486
					public static LocString UNLOADING_PASSENGERS = "Unloading non-crew...";

					// Token: 0x0400BD67 RID: 48487
					public static LocString WAITING_FOR_PILOT = "Pilot requested at control station...";

					// Token: 0x0400BD68 RID: 48488
					public static LocString COUNTING_DOWN = "5... 4... 3... 2... 1...";

					// Token: 0x0400BD69 RID: 48489
					public static LocString TAKING_OFF = "Liftoff!!";
				}
			}

			// Token: 0x020024C3 RID: 9411
			public class AUTOPLUMBERSIDESCREEN
			{
				// Token: 0x0400A127 RID: 41255
				public static LocString TITLE = "Automatic Building Configuration";

				// Token: 0x02002F1F RID: 12063
				public class BUTTONS
				{
					// Token: 0x02003106 RID: 12550
					public class POWER
					{
						// Token: 0x0400C2B3 RID: 49843
						public static LocString TOOLTIP = "Add Dev Generator and Electrical Wires";
					}

					// Token: 0x02003107 RID: 12551
					public class PIPES
					{
						// Token: 0x0400C2B4 RID: 49844
						public static LocString TOOLTIP = "Add Dev Pumps and Pipes";
					}

					// Token: 0x02003108 RID: 12552
					public class SOLIDS
					{
						// Token: 0x0400C2B5 RID: 49845
						public static LocString TOOLTIP = "Spawn solid resources for a relevant recipe or conversions";
					}

					// Token: 0x02003109 RID: 12553
					public class MINION
					{
						// Token: 0x0400C2B6 RID: 49846
						public static LocString TOOLTIP = "Spawn a Duplicant in front of the building";
					}

					// Token: 0x0200310A RID: 12554
					public class FACADE
					{
						// Token: 0x0400C2B7 RID: 49847
						public static LocString TOOLTIP = "Toggle the building blueprint";
					}
				}
			}

			// Token: 0x020024C4 RID: 9412
			public class SELFDESTRUCTSIDESCREEN
			{
				// Token: 0x0400A128 RID: 41256
				public static LocString TITLE = "Self Destruct";

				// Token: 0x0400A129 RID: 41257
				public static LocString MESSAGE_TEXT = "EMERGENCY PROCEDURES";

				// Token: 0x0400A12A RID: 41258
				public static LocString BUTTON_TEXT = "ABANDON SHIP!";

				// Token: 0x0400A12B RID: 41259
				public static LocString BUTTON_TEXT_CONFIRM = "CONFIRM ABANDON SHIP";

				// Token: 0x0400A12C RID: 41260
				public static LocString BUTTON_TOOLTIP = "This rocket is equipped with an emergency escape system.\n\nThe rocket's self-destruct sequence can be triggered to destroy it and propel fragments of the ship towards the nearest planetoid.\n\nAny Duplicants on board will be safely delivered in escape pods.";

				// Token: 0x0400A12D RID: 41261
				public static LocString BUTTON_TOOLTIP_CONFIRM = "<b>This will eject any passengers and destroy the rocket.<b>\n\nThe rocket's self-destruct sequence can be triggered to destroy it and propel fragments of the ship towards the nearest planetoid.\n\nAny Duplicants on board will be safely delivered in escape pods.";
			}

			// Token: 0x020024C5 RID: 9413
			public class GENESHUFFLERSIDESREEN
			{
				// Token: 0x0400A12E RID: 41262
				public static LocString TITLE = "Neural Vacillator";

				// Token: 0x0400A12F RID: 41263
				public static LocString COMPLETE = "Something feels different.";

				// Token: 0x0400A130 RID: 41264
				public static LocString UNDERWAY = "Neural Vacillation in progress.";

				// Token: 0x0400A131 RID: 41265
				public static LocString CONSUMED = "There are no charges left in this Vacillator.";

				// Token: 0x0400A132 RID: 41266
				public static LocString CONSUMED_WAITING = "Recharge requested, awaiting delivery by Duplicant.";

				// Token: 0x0400A133 RID: 41267
				public static LocString BUTTON = "Complete Neural Process";

				// Token: 0x0400A134 RID: 41268
				public static LocString BUTTON_RECHARGE = "Recharge";

				// Token: 0x0400A135 RID: 41269
				public static LocString BUTTON_RECHARGE_CANCEL = "Cancel Recharge";
			}

			// Token: 0x020024C6 RID: 9414
			public class MINIONTODOSIDESCREEN
			{
				// Token: 0x0400A136 RID: 41270
				public static LocString CURRENT_TITLE = "Current Errand";

				// Token: 0x0400A137 RID: 41271
				public static LocString LIST_TITLE = "\"To Do\" List";

				// Token: 0x0400A138 RID: 41272
				public static LocString CURRENT_SCHEDULE_BLOCK = "Schedule Block: {0}";

				// Token: 0x0400A139 RID: 41273
				public static LocString CHORE_TARGET = "{Target}";

				// Token: 0x0400A13A RID: 41274
				public static LocString CHORE_TARGET_AND_GROUP = "{Target} -- {Groups}";

				// Token: 0x0400A13B RID: 41275
				public static LocString SELF_LABEL = "Self";

				// Token: 0x0400A13C RID: 41276
				public static LocString TRUNCATED_CHORES = "{0} more";

				// Token: 0x0400A13D RID: 41277
				public static LocString TOOLTIP_IDLE = string.Concat(new string[]
				{
					"{IdleDescription}\n\nDuplicants will only <b>{Errand}</b> when there is nothing else for them to do\n\nTotal ",
					UI.PRE_KEYWORD,
					"Priority",
					UI.PST_KEYWORD,
					": {TotalPriority}\n    • ",
					UI.JOBSSCREEN.PRIORITY_CLASS.IDLE,
					": {ClassPriority}\n    • All {BestGroup} Errands: {TypePriority}"
				});

				// Token: 0x0400A13E RID: 41278
				public static LocString TOOLTIP_NORMAL = string.Concat(new string[]
				{
					"{Description}\n\nErrand Type: {Groups}\n\nTotal ",
					UI.PRE_KEYWORD,
					"Priority",
					UI.PST_KEYWORD,
					": {TotalPriority}\n    • {Name}'s {BestGroup} Priority: {PersonalPriorityValue} ({PersonalPriority})\n    • This {Building}'s Priority: {BuildingPriority}\n    • All {BestGroup} Errands: {TypePriority}"
				});

				// Token: 0x0400A13F RID: 41279
				public static LocString TOOLTIP_PERSONAL = string.Concat(new string[]
				{
					"{Description}\n\n<b>{Errand}</b> is a ",
					UI.JOBSSCREEN.PRIORITY_CLASS.PERSONAL_NEEDS,
					" errand and so will be performed before all Regular errands\n\nTotal ",
					UI.PRE_KEYWORD,
					"Priority",
					UI.PST_KEYWORD,
					": {TotalPriority}\n    • ",
					UI.JOBSSCREEN.PRIORITY_CLASS.PERSONAL_NEEDS,
					": {ClassPriority}\n    • All {BestGroup} Errands: {TypePriority}"
				});

				// Token: 0x0400A140 RID: 41280
				public static LocString TOOLTIP_EMERGENCY = string.Concat(new string[]
				{
					"{Description}\n\n<b>{Errand}</b> is an ",
					UI.JOBSSCREEN.PRIORITY_CLASS.EMERGENCY,
					" errand and so will be performed before all Regular and Personal errands\n\nTotal ",
					UI.PRE_KEYWORD,
					"Priority",
					UI.PST_KEYWORD,
					": {TotalPriority}\n    • ",
					UI.JOBSSCREEN.PRIORITY_CLASS.EMERGENCY,
					" : {ClassPriority}\n    • This {Building}'s Priority: {BuildingPriority}\n    • All {BestGroup} Errands: {TypePriority}"
				});

				// Token: 0x0400A141 RID: 41281
				public static LocString TOOLTIP_COMPULSORY = string.Concat(new string[]
				{
					"{Description}\n\n<b>{Errand}</b> is a ",
					UI.JOBSSCREEN.PRIORITY_CLASS.COMPULSORY,
					" action and so will occur immediately\n\nTotal ",
					UI.PRE_KEYWORD,
					"Priority",
					UI.PST_KEYWORD,
					": {TotalPriority}\n    • ",
					UI.JOBSSCREEN.PRIORITY_CLASS.COMPULSORY,
					": {ClassPriority}\n    • All {BestGroup} Errands: {TypePriority}"
				});

				// Token: 0x0400A142 RID: 41282
				public static LocString TOOLTIP_DESC_ACTIVE = "{Name}'s Current Errand: <b>{Errand}</b>";

				// Token: 0x0400A143 RID: 41283
				public static LocString TOOLTIP_DESC_INACTIVE = "{Name} could work on <b>{Errand}</b>, but it's not their top priority right now";

				// Token: 0x0400A144 RID: 41284
				public static LocString TOOLTIP_IDLEDESC_ACTIVE = "{Name} is currently <b>Idle</b>";

				// Token: 0x0400A145 RID: 41285
				public static LocString TOOLTIP_IDLEDESC_INACTIVE = "{Name} could become <b>Idle</b> when all other errands are canceled or completed";

				// Token: 0x0400A146 RID: 41286
				public static LocString TOOLTIP_NA = "--";

				// Token: 0x0400A147 RID: 41287
				public static LocString CHORE_GROUP_SEPARATOR = " or ";
			}

			// Token: 0x020024C7 RID: 9415
			public class MODULEFLIGHTUTILITYSIDESCREEN
			{
				// Token: 0x0400A148 RID: 41288
				public static LocString TITLE = "Deployables";

				// Token: 0x0400A149 RID: 41289
				public static LocString DEPLOY_BUTTON = "Deploy";

				// Token: 0x0400A14A RID: 41290
				public static LocString DEPLOY_BUTTON_TOOLTIP = "Send this module's contents to the surface of the currently orbited " + UI.CLUSTERMAP.PLANETOID_KEYWORD + "\n\nA specific deploy location may need to be chosen for certain modules";

				// Token: 0x0400A14B RID: 41291
				public static LocString REPEAT_BUTTON_TOOLTIP = "Automatically deploy this module's contents when a destination orbit is reached";

				// Token: 0x0400A14C RID: 41292
				public static LocString SELECT_DUPLICANT = "Select Duplicant";

				// Token: 0x0400A14D RID: 41293
				public static LocString PILOT_FMT = "{0} - Pilot";
			}

			// Token: 0x020024C8 RID: 9416
			public class HIGHENERGYPARTICLEDIRECTIONSIDESCREEN
			{
				// Token: 0x0400A14E RID: 41294
				public static LocString TITLE = "Emitting Particle Direction";

				// Token: 0x0400A14F RID: 41295
				public static LocString SELECTED_DIRECTION = "Selected direction: {0}";

				// Token: 0x0400A150 RID: 41296
				public static LocString DIRECTION_N = "N";

				// Token: 0x0400A151 RID: 41297
				public static LocString DIRECTION_NE = "NE";

				// Token: 0x0400A152 RID: 41298
				public static LocString DIRECTION_E = "E";

				// Token: 0x0400A153 RID: 41299
				public static LocString DIRECTION_SE = "SE";

				// Token: 0x0400A154 RID: 41300
				public static LocString DIRECTION_S = "S";

				// Token: 0x0400A155 RID: 41301
				public static LocString DIRECTION_SW = "SW";

				// Token: 0x0400A156 RID: 41302
				public static LocString DIRECTION_W = "W";

				// Token: 0x0400A157 RID: 41303
				public static LocString DIRECTION_NW = "NW";
			}

			// Token: 0x020024C9 RID: 9417
			public class MONUMENTSIDESCREEN
			{
				// Token: 0x0400A158 RID: 41304
				public static LocString TITLE = "Great Monument";

				// Token: 0x0400A159 RID: 41305
				public static LocString FLIP_FACING_BUTTON = UI.CLICK(UI.ClickType.CLICK) + " TO ROTATE";
			}

			// Token: 0x020024CA RID: 9418
			public class PLANTERSIDESCREEN
			{
				// Token: 0x0400A15A RID: 41306
				public static LocString TITLE = "{0} Seeds";

				// Token: 0x0400A15B RID: 41307
				public static LocString INFORMATION = "INFORMATION";

				// Token: 0x0400A15C RID: 41308
				public static LocString AWAITINGREQUEST = "PLANT: {0}";

				// Token: 0x0400A15D RID: 41309
				public static LocString AWAITINGDELIVERY = "AWAITING DELIVERY: {0}";

				// Token: 0x0400A15E RID: 41310
				public static LocString AWAITINGREMOVAL = "AWAITING DIGGING UP: {0}";

				// Token: 0x0400A15F RID: 41311
				public static LocString ENTITYDEPOSITED = "PLANTED: {0}";

				// Token: 0x0400A160 RID: 41312
				public static LocString MUTATIONS_HEADER = "Mutations";

				// Token: 0x0400A161 RID: 41313
				public static LocString DEPOSIT = "Plant";

				// Token: 0x0400A162 RID: 41314
				public static LocString CANCELDEPOSIT = "Cancel";

				// Token: 0x0400A163 RID: 41315
				public static LocString REMOVE = "Uproot";

				// Token: 0x0400A164 RID: 41316
				public static LocString CANCELREMOVAL = "Cancel";

				// Token: 0x0400A165 RID: 41317
				public static LocString SELECT_TITLE = "SELECT";

				// Token: 0x0400A166 RID: 41318
				public static LocString SELECT_DESC = "Select a seed to plant.";

				// Token: 0x0400A167 RID: 41319
				public static LocString LIFECYCLE = "<b>Life Cycle</b>:";

				// Token: 0x0400A168 RID: 41320
				public static LocString PLANTREQUIREMENTS = "<b>Growth Requirements</b>:";

				// Token: 0x0400A169 RID: 41321
				public static LocString PLANTEFFECTS = "<b>Effects</b>:";

				// Token: 0x0400A16A RID: 41322
				public static LocString NUMBEROFHARVESTS = "Harvests: {0}";

				// Token: 0x0400A16B RID: 41323
				public static LocString YIELD = "{0}: {1} ";

				// Token: 0x0400A16C RID: 41324
				public static LocString YIELD_NONFOOD = "{0}: {1} ";

				// Token: 0x0400A16D RID: 41325
				public static LocString YIELD_SINGLE = "{0}";

				// Token: 0x0400A16E RID: 41326
				public static LocString YIELDPERHARVEST = "{0} {1} per harvest";

				// Token: 0x0400A16F RID: 41327
				public static LocString TOTALHARVESTCALORIESWITHPERUNIT = "{0} [{1} / unit]";

				// Token: 0x0400A170 RID: 41328
				public static LocString TOTALHARVESTCALORIES = "{0}";

				// Token: 0x0400A171 RID: 41329
				public static LocString BONUS_SEEDS = "Base " + UI.FormatAsLink("Seed", "PLANTS") + " Harvest Chance: {0}";

				// Token: 0x0400A172 RID: 41330
				public static LocString YIELD_SEED = "{1} {0}";

				// Token: 0x0400A173 RID: 41331
				public static LocString YIELD_SEED_SINGLE = "{0}";

				// Token: 0x0400A174 RID: 41332
				public static LocString YIELD_SEED_FINAL_HARVEST = "{1} {0} - Final harvest only";

				// Token: 0x0400A175 RID: 41333
				public static LocString YIELD_SEED_SINGLE_FINAL_HARVEST = "{0} - Final harvest only";

				// Token: 0x0400A176 RID: 41334
				public static LocString ROTATION_NEED_FLOOR = "<b>Requires upward plot orientation.</b>";

				// Token: 0x0400A177 RID: 41335
				public static LocString ROTATION_NEED_WALL = "<b>Requires sideways plot orientation.</b>";

				// Token: 0x0400A178 RID: 41336
				public static LocString ROTATION_NEED_CEILING = "<b>Requires downward plot orientation.</b>";

				// Token: 0x0400A179 RID: 41337
				public static LocString NO_SPECIES_SELECTED = "Select a seed species above...";

				// Token: 0x0400A17A RID: 41338
				public static LocString DISEASE_DROPPER_BURST = "{Disease} Burst: {DiseaseAmount}";

				// Token: 0x0400A17B RID: 41339
				public static LocString DISEASE_DROPPER_CONSTANT = "{Disease}: {DiseaseAmount}";

				// Token: 0x0400A17C RID: 41340
				public static LocString DISEASE_ON_HARVEST = "{Disease} on crop: {DiseaseAmount}";

				// Token: 0x0400A17D RID: 41341
				public static LocString AUTO_SELF_HARVEST = "Self-Harvest On Grown";

				// Token: 0x02002F20 RID: 12064
				public class TOOLTIPS
				{
					// Token: 0x0400BD6A RID: 48490
					public static LocString PLANTLIFECYCLE = "Duration and number of harvests produced by this plant in a lifetime";

					// Token: 0x0400BD6B RID: 48491
					public static LocString PLANTREQUIREMENTS = "Minimum conditions for basic plant growth";

					// Token: 0x0400BD6C RID: 48492
					public static LocString PLANTEFFECTS = "Additional attributes of this plant";

					// Token: 0x0400BD6D RID: 48493
					public static LocString YIELD = UI.FormatAsLink("{2}", "KCAL") + " produced [" + UI.FormatAsLink("{1}", "KCAL") + " / unit]";

					// Token: 0x0400BD6E RID: 48494
					public static LocString YIELD_NONFOOD = "{0} produced per harvest";

					// Token: 0x0400BD6F RID: 48495
					public static LocString NUMBEROFHARVESTS = "This plant can mature {0} times before the end of its life cycle";

					// Token: 0x0400BD70 RID: 48496
					public static LocString YIELD_SEED = "Sow to grow more of this plant";

					// Token: 0x0400BD71 RID: 48497
					public static LocString YIELD_SEED_FINAL_HARVEST = "{0}\n\nProduced in the final harvest of the plant's life cycle";

					// Token: 0x0400BD72 RID: 48498
					public static LocString BONUS_SEEDS = "This plant has a {0} chance to produce new seeds when harvested";

					// Token: 0x0400BD73 RID: 48499
					public static LocString DISEASE_DROPPER_BURST = "At certain points in this plant's lifecycle, it will emit a burst of {DiseaseAmount} {Disease}.";

					// Token: 0x0400BD74 RID: 48500
					public static LocString DISEASE_DROPPER_CONSTANT = "This plant emits {DiseaseAmount} {Disease} while it is alive.";

					// Token: 0x0400BD75 RID: 48501
					public static LocString DISEASE_ON_HARVEST = "The {Crop} produced by this plant will have {DiseaseAmount} {Disease} on it.";

					// Token: 0x0400BD76 RID: 48502
					public static LocString AUTO_SELF_HARVEST = "This plant will instantly drop its crop and begin regrowing when it is matured.";
				}
			}

			// Token: 0x020024CB RID: 9419
			public class EGGINCUBATOR
			{
				// Token: 0x0400A17E RID: 41342
				public static LocString TITLE = "Critter Eggs";

				// Token: 0x0400A17F RID: 41343
				public static LocString AWAITINGREQUEST = "INCUBATE: {0}";

				// Token: 0x0400A180 RID: 41344
				public static LocString AWAITINGDELIVERY = "AWAITING DELIVERY: {0}";

				// Token: 0x0400A181 RID: 41345
				public static LocString AWAITINGREMOVAL = "AWAITING REMOVAL: {0}";

				// Token: 0x0400A182 RID: 41346
				public static LocString ENTITYDEPOSITED = "INCUBATING: {0}";

				// Token: 0x0400A183 RID: 41347
				public static LocString DEPOSIT = "Incubate";

				// Token: 0x0400A184 RID: 41348
				public static LocString CANCELDEPOSIT = "Cancel";

				// Token: 0x0400A185 RID: 41349
				public static LocString REMOVE = "Remove";

				// Token: 0x0400A186 RID: 41350
				public static LocString CANCELREMOVAL = "Cancel";

				// Token: 0x0400A187 RID: 41351
				public static LocString SELECT_TITLE = "SELECT";

				// Token: 0x0400A188 RID: 41352
				public static LocString SELECT_DESC = "Select an egg to incubate.";
			}

			// Token: 0x020024CC RID: 9420
			public class BASICRECEPTACLE
			{
				// Token: 0x0400A189 RID: 41353
				public static LocString TITLE = "Displayed Object";

				// Token: 0x0400A18A RID: 41354
				public static LocString AWAITINGREQUEST = "SELECT: {0}";

				// Token: 0x0400A18B RID: 41355
				public static LocString AWAITINGDELIVERY = "AWAITING DELIVERY: {0}";

				// Token: 0x0400A18C RID: 41356
				public static LocString AWAITINGREMOVAL = "AWAITING REMOVAL: {0}";

				// Token: 0x0400A18D RID: 41357
				public static LocString ENTITYDEPOSITED = "DISPLAYING: {0}";

				// Token: 0x0400A18E RID: 41358
				public static LocString DEPOSIT = "Select";

				// Token: 0x0400A18F RID: 41359
				public static LocString CANCELDEPOSIT = "Cancel";

				// Token: 0x0400A190 RID: 41360
				public static LocString REMOVE = "Remove";

				// Token: 0x0400A191 RID: 41361
				public static LocString CANCELREMOVAL = "Cancel";

				// Token: 0x0400A192 RID: 41362
				public static LocString SELECT_TITLE = "SELECT OBJECT";

				// Token: 0x0400A193 RID: 41363
				public static LocString SELECT_DESC = "Select an object to display here.";
			}

			// Token: 0x020024CD RID: 9421
			public class LURE
			{
				// Token: 0x0400A194 RID: 41364
				public static LocString TITLE = "Select Bait";

				// Token: 0x0400A195 RID: 41365
				public static LocString INFORMATION = "INFORMATION";

				// Token: 0x0400A196 RID: 41366
				public static LocString AWAITINGREQUEST = "PLANT: {0}";

				// Token: 0x0400A197 RID: 41367
				public static LocString AWAITINGDELIVERY = "AWAITING DELIVERY: {0}";

				// Token: 0x0400A198 RID: 41368
				public static LocString AWAITINGREMOVAL = "AWAITING DIGGING UP: {0}";

				// Token: 0x0400A199 RID: 41369
				public static LocString ENTITYDEPOSITED = "PLANTED: {0}";

				// Token: 0x0400A19A RID: 41370
				public static LocString ATTRACTS = "Attract {1}s";
			}

			// Token: 0x020024CE RID: 9422
			public class ROLESTATION
			{
				// Token: 0x0400A19B RID: 41371
				public static LocString TITLE = "Duplicant Skills";

				// Token: 0x0400A19C RID: 41372
				public static LocString OPENROLESBUTTON = "SKILLS";
			}

			// Token: 0x020024CF RID: 9423
			public class RESEARCHSIDESCREEN
			{
				// Token: 0x0400A19D RID: 41373
				public static LocString TITLE = "Select Research";

				// Token: 0x0400A19E RID: 41374
				public static LocString CURRENTLYRESEARCHING = "Currently Researching";

				// Token: 0x0400A19F RID: 41375
				public static LocString NOSELECTEDRESEARCH = "No Research selected";

				// Token: 0x0400A1A0 RID: 41376
				public static LocString OPENRESEARCHBUTTON = "RESEARCH";
			}

			// Token: 0x020024D0 RID: 9424
			public class REFINERYSIDESCREEN
			{
				// Token: 0x0400A1A1 RID: 41377
				public static LocString RECIPE_FROM_TO = "{0} to {1}";

				// Token: 0x0400A1A2 RID: 41378
				public static LocString RECIPE_WITH = "{1} ({0})";

				// Token: 0x0400A1A3 RID: 41379
				public static LocString RECIPE_FROM_TO_WITH_NEWLINES = "{0}\nto\n{1}";

				// Token: 0x0400A1A4 RID: 41380
				public static LocString RECIPE_FROM_TO_COMPOSITE = "{0} to {1} and {2}";

				// Token: 0x0400A1A5 RID: 41381
				public static LocString RECIPE_FROM_TO_HEP = "{0} to " + UI.FormatAsLink("Radbolts", "RADIATION") + " and {1}";

				// Token: 0x0400A1A6 RID: 41382
				public static LocString RECIPE_SIMPLE_INCLUDE_AMOUNTS = "{0} {1}";

				// Token: 0x0400A1A7 RID: 41383
				public static LocString RECIPE_FROM_TO_INCLUDE_AMOUNTS = "{2} {0} to {3} {1}";

				// Token: 0x0400A1A8 RID: 41384
				public static LocString RECIPE_WITH_INCLUDE_AMOUNTS = "{3} {1} ({2} {0})";

				// Token: 0x0400A1A9 RID: 41385
				public static LocString RECIPE_FROM_TO_COMPOSITE_INCLUDE_AMOUNTS = "{3} {0} to {4} {1} and {5} {2}";

				// Token: 0x0400A1AA RID: 41386
				public static LocString RECIPE_FROM_TO_HEP_INCLUDE_AMOUNTS = "{2} {0} to {3} " + UI.FormatAsLink("Radbolts", "RADIATION") + " and {4} {1}";
			}

			// Token: 0x020024D1 RID: 9425
			public class SEALEDDOORSIDESCREEN
			{
				// Token: 0x0400A1AB RID: 41387
				public static LocString TITLE = "Sealed Door";

				// Token: 0x0400A1AC RID: 41388
				public static LocString LABEL = "This door requires a sample to unlock.";

				// Token: 0x0400A1AD RID: 41389
				public static LocString BUTTON = "SUBMIT BIOSCAN";

				// Token: 0x0400A1AE RID: 41390
				public static LocString AWAITINGBUTTON = "AWAITING BIOSCAN";
			}

			// Token: 0x020024D2 RID: 9426
			public class ENCRYPTEDLORESIDESCREEN
			{
				// Token: 0x0400A1AF RID: 41391
				public static LocString TITLE = "Encrypted File";

				// Token: 0x0400A1B0 RID: 41392
				public static LocString LABEL = "This computer contains encrypted files.";

				// Token: 0x0400A1B1 RID: 41393
				public static LocString BUTTON = "ATTEMPT DECRYPTION";

				// Token: 0x0400A1B2 RID: 41394
				public static LocString AWAITINGBUTTON = "AWAITING DECRYPTION";
			}

			// Token: 0x020024D3 RID: 9427
			public class ACCESS_CONTROL_SIDE_SCREEN
			{
				// Token: 0x0400A1B3 RID: 41395
				public static LocString TITLE = "Door Access Control";

				// Token: 0x0400A1B4 RID: 41396
				public static LocString DOOR_DEFAULT = "Default";

				// Token: 0x0400A1B5 RID: 41397
				public static LocString MINION_ACCESS = "Duplicant Access Permissions";

				// Token: 0x0400A1B6 RID: 41398
				public static LocString GO_LEFT_ENABLED = "Passing Left through this door is permitted\n\n" + UI.CLICK(UI.ClickType.Click) + " to revoke permission";

				// Token: 0x0400A1B7 RID: 41399
				public static LocString GO_LEFT_DISABLED = "Passing Left through this door is not permitted\n\n" + UI.CLICK(UI.ClickType.Click) + " to grant permission";

				// Token: 0x0400A1B8 RID: 41400
				public static LocString GO_RIGHT_ENABLED = "Passing Right through this door is permitted\n\n" + UI.CLICK(UI.ClickType.Click) + " to revoke permission";

				// Token: 0x0400A1B9 RID: 41401
				public static LocString GO_RIGHT_DISABLED = "Passing Right through this door is not permitted\n\n" + UI.CLICK(UI.ClickType.Click) + " to grant permission";

				// Token: 0x0400A1BA RID: 41402
				public static LocString GO_UP_ENABLED = "Passing Up through this door is permitted\n\n" + UI.CLICK(UI.ClickType.Click) + " to revoke permission";

				// Token: 0x0400A1BB RID: 41403
				public static LocString GO_UP_DISABLED = "Passing Up through this door is not permitted\n\n" + UI.CLICK(UI.ClickType.Click) + " to grant permission";

				// Token: 0x0400A1BC RID: 41404
				public static LocString GO_DOWN_ENABLED = "Passing Down through this door is permitted\n\n" + UI.CLICK(UI.ClickType.Click) + " to revoke permission";

				// Token: 0x0400A1BD RID: 41405
				public static LocString GO_DOWN_DISABLED = "Passing Down through this door is not permitted\n\n" + UI.CLICK(UI.ClickType.Click) + " to grant permission";

				// Token: 0x0400A1BE RID: 41406
				public static LocString SET_TO_DEFAULT = UI.CLICK(UI.ClickType.Click) + " to clear custom permissions";

				// Token: 0x0400A1BF RID: 41407
				public static LocString SET_TO_CUSTOM = UI.CLICK(UI.ClickType.Click) + " to assign custom permissions";

				// Token: 0x0400A1C0 RID: 41408
				public static LocString USING_DEFAULT = "Default Access";

				// Token: 0x0400A1C1 RID: 41409
				public static LocString USING_CUSTOM = "Custom Access";
			}

			// Token: 0x020024D4 RID: 9428
			public class ASSIGNABLESIDESCREEN
			{
				// Token: 0x0400A1C2 RID: 41410
				public static LocString TITLE = "Assign {0}";

				// Token: 0x0400A1C3 RID: 41411
				public static LocString ASSIGNED = "Assigned";

				// Token: 0x0400A1C4 RID: 41412
				public static LocString UNASSIGNED = "-";

				// Token: 0x0400A1C5 RID: 41413
				public static LocString DISABLED = "Ineligible";

				// Token: 0x0400A1C6 RID: 41414
				public static LocString SORT_BY_DUPLICANT = "Duplicant";

				// Token: 0x0400A1C7 RID: 41415
				public static LocString SORT_BY_ASSIGNMENT = "Assignment";

				// Token: 0x0400A1C8 RID: 41416
				public static LocString ASSIGN_TO_TOOLTIP = "Assign to {0}";

				// Token: 0x0400A1C9 RID: 41417
				public static LocString UNASSIGN_TOOLTIP = "Assigned to {0}";

				// Token: 0x0400A1CA RID: 41418
				public static LocString DISABLED_TOOLTIP = "{0} is ineligible for this skill assignment";

				// Token: 0x0400A1CB RID: 41419
				public static LocString PUBLIC = "Public";
			}

			// Token: 0x020024D5 RID: 9429
			public class COMETDETECTORSIDESCREEN
			{
				// Token: 0x0400A1CC RID: 41420
				public static LocString TITLE = "Space Scanner";

				// Token: 0x0400A1CD RID: 41421
				public static LocString HEADER = "Sends automation signal when selected object is detected";

				// Token: 0x0400A1CE RID: 41422
				public static LocString ASSIGNED = "Assigned";

				// Token: 0x0400A1CF RID: 41423
				public static LocString UNASSIGNED = "-";

				// Token: 0x0400A1D0 RID: 41424
				public static LocString DISABLED = "Ineligible";

				// Token: 0x0400A1D1 RID: 41425
				public static LocString SORT_BY_DUPLICANT = "Duplicant";

				// Token: 0x0400A1D2 RID: 41426
				public static LocString SORT_BY_ASSIGNMENT = "Assignment";

				// Token: 0x0400A1D3 RID: 41427
				public static LocString ASSIGN_TO_TOOLTIP = "Scanning for {0}";

				// Token: 0x0400A1D4 RID: 41428
				public static LocString UNASSIGN_TOOLTIP = "Scanning for {0}";

				// Token: 0x0400A1D5 RID: 41429
				public static LocString NOTHING = "Nothing";

				// Token: 0x0400A1D6 RID: 41430
				public static LocString COMETS = "Meteor Showers";

				// Token: 0x0400A1D7 RID: 41431
				public static LocString ROCKETS = "Rocket Landing Ping";

				// Token: 0x0400A1D8 RID: 41432
				public static LocString DUPEMADE = "Dupe-made Ballistics";
			}

			// Token: 0x020024D6 RID: 9430
			public class GEOTUNERSIDESCREEN
			{
				// Token: 0x0400A1D9 RID: 41433
				public static LocString TITLE = "Select Geyser";

				// Token: 0x0400A1DA RID: 41434
				public static LocString DESCRIPTION = "Select an analyzed geyser to transmit amplification data to.";

				// Token: 0x0400A1DB RID: 41435
				public static LocString NOTHING = "No geyser selected";

				// Token: 0x0400A1DC RID: 41436
				public static LocString UNSTUDIED_TOOLTIP = "This geyser must be analyzed before it can be selected\n\nDouble-click to view this geyser";

				// Token: 0x0400A1DD RID: 41437
				public static LocString STUDIED_TOOLTIP = string.Concat(new string[]
				{
					"Increase this geyser's ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" and output"
				});

				// Token: 0x0400A1DE RID: 41438
				public static LocString GEOTUNER_LIMIT_TOOLTIP = "This geyser cannot be targeted by more " + UI.PRE_KEYWORD + "Geotuners" + UI.PST_KEYWORD;

				// Token: 0x0400A1DF RID: 41439
				public static LocString STUDIED_TOOLTIP_MATERIAL = "Required resource: {MATERIAL}";

				// Token: 0x0400A1E0 RID: 41440
				public static LocString STUDIED_TOOLTIP_POTENTIAL_OUTPUT = "Potential Output {POTENTIAL_OUTPUT}";

				// Token: 0x0400A1E1 RID: 41441
				public static LocString STUDIED_TOOLTIP_BASE_TEMP = "Base {BASE}";

				// Token: 0x0400A1E2 RID: 41442
				public static LocString STUDIED_TOOLTIP_VISIT_GEYSER = "Double-click to view this geyser";

				// Token: 0x0400A1E3 RID: 41443
				public static LocString STUDIED_TOOLTIP_GEOTUNER_MODIFIER_ROW_TITLE = "Geotuned ";

				// Token: 0x0400A1E4 RID: 41444
				public static LocString STUDIED_TOOLTIP_NUMBER_HOVERED = "This geyser is targeted by {0} Geotuners";
			}

			// Token: 0x020024D7 RID: 9431
			public class COMMAND_MODULE_SIDE_SCREEN
			{
				// Token: 0x0400A1E5 RID: 41445
				public static LocString TITLE = "Launch Conditions";

				// Token: 0x0400A1E6 RID: 41446
				public static LocString DESTINATION_BUTTON = "Show Starmap";

				// Token: 0x0400A1E7 RID: 41447
				public static LocString DESTINATION_BUTTON_EXPANSION = "Show Starmap";
			}

			// Token: 0x020024D8 RID: 9432
			public class CLUSTERDESTINATIONSIDESCREEN
			{
				// Token: 0x0400A1E8 RID: 41448
				public static LocString TITLE = "Destination";

				// Token: 0x0400A1E9 RID: 41449
				public static LocString FIRSTAVAILABLE = "Any " + BUILDINGS.PREFABS.LAUNCHPAD.NAME;

				// Token: 0x0400A1EA RID: 41450
				public static LocString NONEAVAILABLE = "No landing site";

				// Token: 0x0400A1EB RID: 41451
				public static LocString NO_TALL_SITES_AVAILABLE = "No landing sites fit the height of this rocket";

				// Token: 0x0400A1EC RID: 41452
				public static LocString DROPDOWN_TOOLTIP_VALID_SITE = "Land at {0} when the site is clear";

				// Token: 0x0400A1ED RID: 41453
				public static LocString DROPDOWN_TOOLTIP_FIRST_AVAILABLE = "Select the first available landing site";

				// Token: 0x0400A1EE RID: 41454
				public static LocString DROPDOWN_TOOLTIP_TOO_SHORT = "This rocket's height exceeds the space available in this landing site";

				// Token: 0x0400A1EF RID: 41455
				public static LocString DROPDOWN_TOOLTIP_PATH_OBSTRUCTED = "Landing path obstructed";

				// Token: 0x0400A1F0 RID: 41456
				public static LocString DROPDOWN_TOOLTIP_SITE_OBSTRUCTED = "Landing position on the platform is obstructed";

				// Token: 0x0400A1F1 RID: 41457
				public static LocString DROPDOWN_TOOLTIP_PAD_DISABLED = BUILDINGS.PREFABS.LAUNCHPAD.NAME + " is disabled";

				// Token: 0x0400A1F2 RID: 41458
				public static LocString CHANGE_DESTINATION_BUTTON = "Change";

				// Token: 0x0400A1F3 RID: 41459
				public static LocString CHANGE_DESTINATION_BUTTON_TOOLTIP = "Select a new destination for this rocket";

				// Token: 0x0400A1F4 RID: 41460
				public static LocString CLEAR_DESTINATION_BUTTON = "Clear";

				// Token: 0x0400A1F5 RID: 41461
				public static LocString CLEAR_DESTINATION_BUTTON_TOOLTIP = "Clear this rocket's selected destination";

				// Token: 0x0400A1F6 RID: 41462
				public static LocString LOOP_BUTTON_TOOLTIP = "Toggle a roundtrip flight between this rocket's destination and its original takeoff location";

				// Token: 0x02002F21 RID: 12065
				public class ASSIGNMENTSTATUS
				{
					// Token: 0x0400BD77 RID: 48503
					public static LocString LOCAL = "Current";

					// Token: 0x0400BD78 RID: 48504
					public static LocString DESTINATION = "Destination";
				}
			}

			// Token: 0x020024D9 RID: 9433
			public class EQUIPPABLESIDESCREEN
			{
				// Token: 0x0400A1F7 RID: 41463
				public static LocString TITLE = "Equip {0}";

				// Token: 0x0400A1F8 RID: 41464
				public static LocString ASSIGNEDTO = "Assigned to: {Assignee}";

				// Token: 0x0400A1F9 RID: 41465
				public static LocString UNASSIGNED = "Unassigned";

				// Token: 0x0400A1FA RID: 41466
				public static LocString GENERAL_CURRENTASSIGNED = "(Owner)";
			}

			// Token: 0x020024DA RID: 9434
			public class EQUIPPABLE_SIDE_SCREEN
			{
				// Token: 0x0400A1FB RID: 41467
				public static LocString TITLE = "Assign To Duplicant";

				// Token: 0x0400A1FC RID: 41468
				public static LocString CURRENTLY_EQUIPPED = "Currently Equipped:\n{0}";

				// Token: 0x0400A1FD RID: 41469
				public static LocString NONE_EQUIPPED = "None";

				// Token: 0x0400A1FE RID: 41470
				public static LocString EQUIP_BUTTON = "Equip";

				// Token: 0x0400A1FF RID: 41471
				public static LocString DROP_BUTTON = "Drop";

				// Token: 0x0400A200 RID: 41472
				public static LocString SWAP_BUTTON = "Swap";
			}

			// Token: 0x020024DB RID: 9435
			public class TELEPADSIDESCREEN
			{
				// Token: 0x0400A201 RID: 41473
				public static LocString TITLE = "Printables";

				// Token: 0x0400A202 RID: 41474
				public static LocString NEXTPRODUCTION = "Next Production: {0}";

				// Token: 0x0400A203 RID: 41475
				public static LocString GAMEOVER = "Colony Lost";

				// Token: 0x0400A204 RID: 41476
				public static LocString VICTORY_CONDITIONS = "Hardwired Imperatives";

				// Token: 0x0400A205 RID: 41477
				public static LocString SUMMARY_TITLE = "Colony Summary";

				// Token: 0x0400A206 RID: 41478
				public static LocString SKILLS_BUTTON = "Duplicant Skills";
			}

			// Token: 0x020024DC RID: 9436
			public class VALVESIDESCREEN
			{
				// Token: 0x0400A207 RID: 41479
				public static LocString TITLE = "Flow Control";
			}

			// Token: 0x020024DD RID: 9437
			public class LIMIT_VALVE_SIDE_SCREEN
			{
				// Token: 0x0400A208 RID: 41480
				public static LocString TITLE = "Meter Control";

				// Token: 0x0400A209 RID: 41481
				public static LocString AMOUNT = "Amount: {0}";

				// Token: 0x0400A20A RID: 41482
				public static LocString LIMIT = "Limit:";

				// Token: 0x0400A20B RID: 41483
				public static LocString RESET_BUTTON = "Reset Amount";

				// Token: 0x0400A20C RID: 41484
				public static LocString SLIDER_TOOLTIP_UNITS = "The amount of Units or Mass passing through the sensor.";
			}

			// Token: 0x020024DE RID: 9438
			public class NUCLEAR_REACTOR_SIDE_SCREEN
			{
				// Token: 0x0400A20D RID: 41485
				public static LocString TITLE = "Reaction Mass Target";

				// Token: 0x0400A20E RID: 41486
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Duplicants will attempt to keep the reactor supplied with ",
					UI.PRE_KEYWORD,
					"{0}{1}",
					UI.PST_KEYWORD,
					" of ",
					UI.PRE_KEYWORD,
					"{2}",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x020024DF RID: 9439
			public class MANUALGENERATORSIDESCREEN
			{
				// Token: 0x0400A20F RID: 41487
				public static LocString TITLE = "Battery Recharge Threshold";

				// Token: 0x0400A210 RID: 41488
				public static LocString CURRENT_THRESHOLD = "Current Threshold: {0}%";

				// Token: 0x0400A211 RID: 41489
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Duplicants will be requested to operate this generator when the total charge of the connected ",
					UI.PRE_KEYWORD,
					"Batteries",
					UI.PST_KEYWORD,
					" falls below <b>{0}%</b>"
				});
			}

			// Token: 0x020024E0 RID: 9440
			public class MANUALDELIVERYGENERATORSIDESCREEN
			{
				// Token: 0x0400A212 RID: 41490
				public static LocString TITLE = "Fuel Request Threshold";

				// Token: 0x0400A213 RID: 41491
				public static LocString CURRENT_THRESHOLD = "Current Threshold: {0}%";

				// Token: 0x0400A214 RID: 41492
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Duplicants will be requested to deliver ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" when the total charge of the connected ",
					UI.PRE_KEYWORD,
					"Batteries",
					UI.PST_KEYWORD,
					" falls below <b>{1}%</b>"
				});
			}

			// Token: 0x020024E1 RID: 9441
			public class TIME_OF_DAY_SIDE_SCREEN
			{
				// Token: 0x0400A215 RID: 41493
				public static LocString TITLE = "Time-of-Day Sensor";

				// Token: 0x0400A216 RID: 41494
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" after the selected Turn On time, and a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" after the selected Turn Off time"
				});

				// Token: 0x0400A217 RID: 41495
				public static LocString START = "Turn On";

				// Token: 0x0400A218 RID: 41496
				public static LocString STOP = "Turn Off";
			}

			// Token: 0x020024E2 RID: 9442
			public class CRITTER_COUNT_SIDE_SCREEN
			{
				// Token: 0x0400A219 RID: 41497
				public static LocString TITLE = "Critter Count Sensor";

				// Token: 0x0400A21A RID: 41498
				public static LocString TOOLTIP_ABOVE = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if there are more than <b>{0}</b> ",
					UI.PRE_KEYWORD,
					"Critters",
					UI.PST_KEYWORD,
					" or ",
					UI.PRE_KEYWORD,
					"Eggs",
					UI.PST_KEYWORD,
					" in the room"
				});

				// Token: 0x0400A21B RID: 41499
				public static LocString TOOLTIP_BELOW = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if there are fewer than <b>{0}</b> ",
					UI.PRE_KEYWORD,
					"Critters",
					UI.PST_KEYWORD,
					" or ",
					UI.PRE_KEYWORD,
					"Eggs",
					UI.PST_KEYWORD,
					" in the room"
				});

				// Token: 0x0400A21C RID: 41500
				public static LocString START = "Turn On";

				// Token: 0x0400A21D RID: 41501
				public static LocString STOP = "Turn Off";

				// Token: 0x0400A21E RID: 41502
				public static LocString VALUE_NAME = "Count";
			}

			// Token: 0x020024E3 RID: 9443
			public class OIL_WELL_CAP_SIDE_SCREEN
			{
				// Token: 0x0400A21F RID: 41503
				public static LocString TITLE = "Backpressure Release Threshold";

				// Token: 0x0400A220 RID: 41504
				public static LocString TOOLTIP = "Duplicants will be requested to release backpressure buildup when it exceeds <b>{0}%</b>";
			}

			// Token: 0x020024E4 RID: 9444
			public class MODULAR_CONDUIT_PORT_SIDE_SCREEN
			{
				// Token: 0x0400A221 RID: 41505
				public static LocString TITLE = "Pump Control";

				// Token: 0x0400A222 RID: 41506
				public static LocString LABEL_UNLOAD = "Unload Only";

				// Token: 0x0400A223 RID: 41507
				public static LocString LABEL_BOTH = "Load/Unload";

				// Token: 0x0400A224 RID: 41508
				public static LocString LABEL_LOAD = "Load Only";

				// Token: 0x0400A225 RID: 41509
				public static readonly List<LocString> LABELS = new List<LocString>
				{
					UI.UISIDESCREENS.MODULAR_CONDUIT_PORT_SIDE_SCREEN.LABEL_UNLOAD,
					UI.UISIDESCREENS.MODULAR_CONDUIT_PORT_SIDE_SCREEN.LABEL_BOTH,
					UI.UISIDESCREENS.MODULAR_CONDUIT_PORT_SIDE_SCREEN.LABEL_LOAD
				};

				// Token: 0x0400A226 RID: 41510
				public static LocString TOOLTIP_UNLOAD = "This pump will attempt to <b>Unload</b> cargo from the landed rocket, but not attempt to load new cargo";

				// Token: 0x0400A227 RID: 41511
				public static LocString TOOLTIP_BOTH = "This pump will both <b>Load</b> and <b>Unload</b> cargo from the landed rocket";

				// Token: 0x0400A228 RID: 41512
				public static LocString TOOLTIP_LOAD = "This pump will attempt to <b>Load</b> cargo onto the landed rocket, but will not unload it";

				// Token: 0x0400A229 RID: 41513
				public static readonly List<LocString> TOOLTIPS = new List<LocString>
				{
					UI.UISIDESCREENS.MODULAR_CONDUIT_PORT_SIDE_SCREEN.TOOLTIP_UNLOAD,
					UI.UISIDESCREENS.MODULAR_CONDUIT_PORT_SIDE_SCREEN.TOOLTIP_BOTH,
					UI.UISIDESCREENS.MODULAR_CONDUIT_PORT_SIDE_SCREEN.TOOLTIP_LOAD
				};

				// Token: 0x0400A22A RID: 41514
				public static LocString DESCRIPTION = "";
			}

			// Token: 0x020024E5 RID: 9445
			public class LOGIC_BUFFER_SIDE_SCREEN
			{
				// Token: 0x0400A22B RID: 41515
				public static LocString TITLE = "Buffer Time";

				// Token: 0x0400A22C RID: 41516
				public static LocString TOOLTIP = "Will continue to send a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " for <b>{0} seconds</b> after receiving a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x020024E6 RID: 9446
			public class LOGIC_FILTER_SIDE_SCREEN
			{
				// Token: 0x0400A22D RID: 41517
				public static LocString TITLE = "Filter Time";

				// Token: 0x0400A22E RID: 41518
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Will only send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if it receives ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					" for longer than <b>{0} seconds</b>"
				});
			}

			// Token: 0x020024E7 RID: 9447
			public class TIME_RANGE_SIDE_SCREEN
			{
				// Token: 0x0400A22F RID: 41519
				public static LocString TITLE = "Time Schedule";

				// Token: 0x0400A230 RID: 41520
				public static LocString ON = "Activation Time";

				// Token: 0x0400A231 RID: 41521
				public static LocString ON_TOOLTIP = string.Concat(new string[]
				{
					"Activation time determines the time of day this sensor should begin sending a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					"\n\nThis sensor sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" {0} through the day"
				});

				// Token: 0x0400A232 RID: 41522
				public static LocString DURATION = "Active Duration";

				// Token: 0x0400A233 RID: 41523
				public static LocString DURATION_TOOLTIP = string.Concat(new string[]
				{
					"Active duration determines what percentage of the day this sensor will spend sending a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					"\n\nThis sensor will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" for {0} of the day"
				});
			}

			// Token: 0x020024E8 RID: 9448
			public class TIMER_SIDE_SCREEN
			{
				// Token: 0x0400A234 RID: 41524
				public static LocString TITLE = "Timer";

				// Token: 0x0400A235 RID: 41525
				public static LocString ON = "Green Duration";

				// Token: 0x0400A236 RID: 41526
				public static LocString GREEN_DURATION_TOOLTIP = string.Concat(new string[]
				{
					"Green duration determines the amount of time this sensor should send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					"\n\nThis sensor sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" for {0}"
				});

				// Token: 0x0400A237 RID: 41527
				public static LocString OFF = "Red Duration";

				// Token: 0x0400A238 RID: 41528
				public static LocString RED_DURATION_TOOLTIP = string.Concat(new string[]
				{
					"Red duration determines the amount of time this sensor should send a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					"\n\nThis sensor will send a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" for {0}"
				});

				// Token: 0x0400A239 RID: 41529
				public static LocString CURRENT_TIME = "{0}/{1}";

				// Token: 0x0400A23A RID: 41530
				public static LocString MODE_LABEL_SECONDS = "Mode: Seconds";

				// Token: 0x0400A23B RID: 41531
				public static LocString MODE_LABEL_CYCLES = "Mode: Cycles";

				// Token: 0x0400A23C RID: 41532
				public static LocString RESET_BUTTON = "Reset Timer";

				// Token: 0x0400A23D RID: 41533
				public static LocString DISABLED = "Timer Disabled";
			}

			// Token: 0x020024E9 RID: 9449
			public class COUNTER_SIDE_SCREEN
			{
				// Token: 0x0400A23E RID: 41534
				public static LocString TITLE = "Counter";

				// Token: 0x0400A23F RID: 41535
				public static LocString RESET_BUTTON = "Reset Counter";

				// Token: 0x0400A240 RID: 41536
				public static LocString DESCRIPTION = "Send " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when count is reached:";

				// Token: 0x0400A241 RID: 41537
				public static LocString INCREMENT_MODE = "Mode: Increment";

				// Token: 0x0400A242 RID: 41538
				public static LocString DECREMENT_MODE = "Mode: Decrement";

				// Token: 0x0400A243 RID: 41539
				public static LocString ADVANCED_MODE = "Advanced Mode";

				// Token: 0x0400A244 RID: 41540
				public static LocString CURRENT_COUNT_SIMPLE = "{0} of ";

				// Token: 0x0400A245 RID: 41541
				public static LocString CURRENT_COUNT_ADVANCED = "{0} % ";

				// Token: 0x02002F22 RID: 12066
				public class TOOLTIPS
				{
					// Token: 0x0400BD79 RID: 48505
					public static LocString ADVANCED_MODE = string.Concat(new string[]
					{
						"In Advanced Mode, the ",
						BUILDINGS.PREFABS.LOGICCOUNTER.NAME,
						" will count from <b>0</b> rather than <b>1</b>. It will reset when the max is reached, and send a ",
						UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
						" as a brief pulse rather than continuously."
					});
				}
			}

			// Token: 0x020024EA RID: 9450
			public class PASSENGERMODULESIDESCREEN
			{
				// Token: 0x0400A246 RID: 41542
				public static LocString REQUEST_CREW = "Crew";

				// Token: 0x0400A247 RID: 41543
				public static LocString REQUEST_CREW_TOOLTIP = "Crew may not leave the module, non crew-must exit";

				// Token: 0x0400A248 RID: 41544
				public static LocString AUTO_CREW = "Auto";

				// Token: 0x0400A249 RID: 41545
				public static LocString AUTO_CREW_TOOLTIP = "All Duplicants may enter and exit the module freely until the rocket is ready for launch\n\nBefore launch the crew will automatically be requested";

				// Token: 0x0400A24A RID: 41546
				public static LocString RELEASE_CREW = "All";

				// Token: 0x0400A24B RID: 41547
				public static LocString RELEASE_CREW_TOOLTIP = "All Duplicants may enter and exit the module freely";

				// Token: 0x0400A24C RID: 41548
				public static LocString REQUIRE_SUIT_LABEL = "Atmosuit Required";

				// Token: 0x0400A24D RID: 41549
				public static LocString REQUIRE_SUIT_LABEL_TOOLTIP = "If checked, Duplicants will be required to wear an Atmo Suit when entering this rocket";

				// Token: 0x0400A24E RID: 41550
				public static LocString CHANGE_CREW_BUTTON = "Change crew";

				// Token: 0x0400A24F RID: 41551
				public static LocString CHANGE_CREW_BUTTON_TOOLTIP = "Assign Duplicants to crew this rocket's missions";

				// Token: 0x0400A250 RID: 41552
				public static LocString ASSIGNED_TO_CREW = "Assigned to crew";

				// Token: 0x0400A251 RID: 41553
				public static LocString UNASSIGNED = "Unassigned";
			}

			// Token: 0x020024EB RID: 9451
			public class TIMEDSWITCHSIDESCREEN
			{
				// Token: 0x0400A252 RID: 41554
				public static LocString TITLE = "Time Schedule";

				// Token: 0x0400A253 RID: 41555
				public static LocString ONTIME = "On Time:";

				// Token: 0x0400A254 RID: 41556
				public static LocString OFFTIME = "Off Time:";

				// Token: 0x0400A255 RID: 41557
				public static LocString TIMETODEACTIVATE = "Time until deactivation: {0}";

				// Token: 0x0400A256 RID: 41558
				public static LocString TIMETOACTIVATE = "Time until activation: {0}";

				// Token: 0x0400A257 RID: 41559
				public static LocString WARNING = "Switch must be connected to a " + UI.FormatAsLink("Power", "POWER") + " grid";

				// Token: 0x0400A258 RID: 41560
				public static LocString CURRENTSTATE = "Current State:";

				// Token: 0x0400A259 RID: 41561
				public static LocString ON = "On";

				// Token: 0x0400A25A RID: 41562
				public static LocString OFF = "Off";
			}

			// Token: 0x020024EC RID: 9452
			public class CAPTURE_POINT_SIDE_SCREEN
			{
				// Token: 0x0400A25B RID: 41563
				public static LocString TITLE = "Stable Management";

				// Token: 0x0400A25C RID: 41564
				public static LocString AUTOWRANGLE = "Auto-Wrangle Surplus";

				// Token: 0x0400A25D RID: 41565
				public static LocString AUTOWRANGLE_TOOLTIP = "A Duplicant will automatically wrangle any critters that exceed the population limit or that do not belong in this stable\n\nDuplicants must possess the Critter Ranching Skill in order to wrangle critters";

				// Token: 0x0400A25E RID: 41566
				public static LocString LIMIT_TOOLTIP = "Critters exceeding this population limit will automatically be wrangled:";

				// Token: 0x0400A25F RID: 41567
				public static LocString UNITS_SUFFIX = " Critters";
			}

			// Token: 0x020024ED RID: 9453
			public class TEMPERATURESWITCHSIDESCREEN
			{
				// Token: 0x0400A260 RID: 41568
				public static LocString TITLE = "Temperature Threshold";

				// Token: 0x0400A261 RID: 41569
				public static LocString CURRENT_TEMPERATURE = "Current Temperature:\n{0}";

				// Token: 0x0400A262 RID: 41570
				public static LocString ACTIVATE_IF = "Send " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if:";

				// Token: 0x0400A263 RID: 41571
				public static LocString COLDER_BUTTON = "Below";

				// Token: 0x0400A264 RID: 41572
				public static LocString WARMER_BUTTON = "Above";
			}

			// Token: 0x020024EE RID: 9454
			public class RADIATIONSWITCHSIDESCREEN
			{
				// Token: 0x0400A265 RID: 41573
				public static LocString TITLE = "Radiation Threshold";

				// Token: 0x0400A266 RID: 41574
				public static LocString CURRENT_TEMPERATURE = "Current Radiation:\n{0}/cycle";

				// Token: 0x0400A267 RID: 41575
				public static LocString ACTIVATE_IF = "Send " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if:";

				// Token: 0x0400A268 RID: 41576
				public static LocString COLDER_BUTTON = "Below";

				// Token: 0x0400A269 RID: 41577
				public static LocString WARMER_BUTTON = "Above";
			}

			// Token: 0x020024EF RID: 9455
			public class WATTAGESWITCHSIDESCREEN
			{
				// Token: 0x0400A26A RID: 41578
				public static LocString TITLE = "Wattage Threshold";

				// Token: 0x0400A26B RID: 41579
				public static LocString CURRENT_TEMPERATURE = "Current Wattage:\n{0}";

				// Token: 0x0400A26C RID: 41580
				public static LocString ACTIVATE_IF = "Send " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if:";

				// Token: 0x0400A26D RID: 41581
				public static LocString COLDER_BUTTON = "Below";

				// Token: 0x0400A26E RID: 41582
				public static LocString WARMER_BUTTON = "Above";
			}

			// Token: 0x020024F0 RID: 9456
			public class HEPSWITCHSIDESCREEN
			{
				// Token: 0x0400A26F RID: 41583
				public static LocString TITLE = "Radbolt Threshold";
			}

			// Token: 0x020024F1 RID: 9457
			public class THRESHOLD_SWITCH_SIDESCREEN
			{
				// Token: 0x0400A270 RID: 41584
				public static LocString TITLE = "Pressure";

				// Token: 0x0400A271 RID: 41585
				public static LocString THRESHOLD_SUBTITLE = "Threshold:";

				// Token: 0x0400A272 RID: 41586
				public static LocString CURRENT_VALUE = "Current {0}:\n{1}";

				// Token: 0x0400A273 RID: 41587
				public static LocString ACTIVATE_IF = "Send " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if:";

				// Token: 0x0400A274 RID: 41588
				public static LocString ABOVE_BUTTON = "Above";

				// Token: 0x0400A275 RID: 41589
				public static LocString BELOW_BUTTON = "Below";

				// Token: 0x0400A276 RID: 41590
				public static LocString STATUS_ACTIVE = "Switch Active";

				// Token: 0x0400A277 RID: 41591
				public static LocString STATUS_INACTIVE = "Switch Inactive";

				// Token: 0x0400A278 RID: 41592
				public static LocString PRESSURE = "Ambient Pressure";

				// Token: 0x0400A279 RID: 41593
				public static LocString PRESSURE_TOOLTIP_ABOVE = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ",
					UI.PRE_KEYWORD,
					"Pressure",
					UI.PST_KEYWORD,
					" is above <b>{0}</b>"
				});

				// Token: 0x0400A27A RID: 41594
				public static LocString PRESSURE_TOOLTIP_BELOW = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ",
					UI.PRE_KEYWORD,
					"Pressure",
					UI.PST_KEYWORD,
					" is below <b>{0}</b>"
				});

				// Token: 0x0400A27B RID: 41595
				public static LocString TEMPERATURE = "Ambient Temperature";

				// Token: 0x0400A27C RID: 41596
				public static LocString TEMPERATURE_TOOLTIP_ABOVE = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ambient ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" is above <b>{0}</b>"
				});

				// Token: 0x0400A27D RID: 41597
				public static LocString TEMPERATURE_TOOLTIP_BELOW = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ambient ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" is below <b>{0}</b>"
				});

				// Token: 0x0400A27E RID: 41598
				public static LocString WATTAGE = "Wattage Reading";

				// Token: 0x0400A27F RID: 41599
				public static LocString WATTAGE_TOOLTIP_ABOVE = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ",
					UI.PRE_KEYWORD,
					"Wattage",
					UI.PST_KEYWORD,
					" consumed is above <b>{0}</b>"
				});

				// Token: 0x0400A280 RID: 41600
				public static LocString WATTAGE_TOOLTIP_BELOW = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ",
					UI.PRE_KEYWORD,
					"Wattage",
					UI.PST_KEYWORD,
					" consumed is below <b>{0}</b>"
				});

				// Token: 0x0400A281 RID: 41601
				public static LocString DISEASE_TITLE = "Germ Threshold";

				// Token: 0x0400A282 RID: 41602
				public static LocString DISEASE = "Ambient Germs";

				// Token: 0x0400A283 RID: 41603
				public static LocString DISEASE_TOOLTIP_ABOVE = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the number of ",
					UI.PRE_KEYWORD,
					"Germs",
					UI.PST_KEYWORD,
					" is above <b>{0}</b>"
				});

				// Token: 0x0400A284 RID: 41604
				public static LocString DISEASE_TOOLTIP_BELOW = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the number of ",
					UI.PRE_KEYWORD,
					"Germs",
					UI.PST_KEYWORD,
					" is below <b>{0}</b>"
				});

				// Token: 0x0400A285 RID: 41605
				public static LocString DISEASE_UNITS = "";

				// Token: 0x0400A286 RID: 41606
				public static LocString RADIATION = "Ambient Radiation";

				// Token: 0x0400A287 RID: 41607
				public static LocString RADIATION_TOOLTIP_ABOVE = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ambient ",
					UI.PRE_KEYWORD,
					"Radiation",
					UI.PST_KEYWORD,
					" is above <b>{0}</b>"
				});

				// Token: 0x0400A288 RID: 41608
				public static LocString RADIATION_TOOLTIP_BELOW = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ambient ",
					UI.PRE_KEYWORD,
					"Radiation",
					UI.PST_KEYWORD,
					" is below <b>{0}</b>"
				});

				// Token: 0x0400A289 RID: 41609
				public static LocString HEPS = "Radbolt Reading";

				// Token: 0x0400A28A RID: 41610
				public static LocString HEPS_TOOLTIP_ABOVE = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ",
					UI.PRE_KEYWORD,
					"Radbolts",
					UI.PST_KEYWORD,
					" is above <b>{0}</b>"
				});

				// Token: 0x0400A28B RID: 41611
				public static LocString HEPS_TOOLTIP_BELOW = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ",
					UI.PRE_KEYWORD,
					"Radbolts",
					UI.PST_KEYWORD,
					" is below <b>{0}</b>"
				});
			}

			// Token: 0x020024F2 RID: 9458
			public class CAPACITY_CONTROL_SIDE_SCREEN
			{
				// Token: 0x0400A28C RID: 41612
				public static LocString TITLE = "Automated Storage Capacity";

				// Token: 0x0400A28D RID: 41613
				public static LocString MAX_LABEL = "Max:";
			}

			// Token: 0x020024F3 RID: 9459
			public class DOOR_TOGGLE_SIDE_SCREEN
			{
				// Token: 0x0400A28E RID: 41614
				public static LocString TITLE = "Door Setting";

				// Token: 0x0400A28F RID: 41615
				public static LocString OPEN = "Door is open.";

				// Token: 0x0400A290 RID: 41616
				public static LocString AUTO = "Door is on auto.";

				// Token: 0x0400A291 RID: 41617
				public static LocString CLOSE = "Door is locked.";

				// Token: 0x0400A292 RID: 41618
				public static LocString PENDING_FORMAT = "{0} {1}";

				// Token: 0x0400A293 RID: 41619
				public static LocString OPEN_PENDING = "Awaiting Duplicant to open door.";

				// Token: 0x0400A294 RID: 41620
				public static LocString AUTO_PENDING = "Awaiting Duplicant to automate door.";

				// Token: 0x0400A295 RID: 41621
				public static LocString CLOSE_PENDING = "Awaiting Duplicant to lock door.";

				// Token: 0x0400A296 RID: 41622
				public static LocString ACCESS_FORMAT = "{0}\n\n{1}";

				// Token: 0x0400A297 RID: 41623
				public static LocString ACCESS_OFFLINE = "Emergency Access Permissions:\nAll Duplicants are permitted to use this door until " + UI.FormatAsLink("Power", "POWER") + " is restored.";

				// Token: 0x0400A298 RID: 41624
				public static LocString POI_INTERNAL = "This door cannot be manually controlled.";
			}

			// Token: 0x020024F4 RID: 9460
			public class ACTIVATION_RANGE_SIDE_SCREEN
			{
				// Token: 0x0400A299 RID: 41625
				public static LocString NAME = "Breaktime Policy";

				// Token: 0x0400A29A RID: 41626
				public static LocString ACTIVATE = "Break starts at:";

				// Token: 0x0400A29B RID: 41627
				public static LocString DEACTIVATE = "Break ends at:";
			}

			// Token: 0x020024F5 RID: 9461
			public class CAPACITY_SIDE_SCREEN
			{
				// Token: 0x0400A29C RID: 41628
				public static LocString TOOLTIP = "Adjust the maximum amount that can be stored here";
			}

			// Token: 0x020024F6 RID: 9462
			public class SUIT_SIDE_SCREEN
			{
				// Token: 0x0400A29D RID: 41629
				public static LocString TITLE = "Dock Inventory";

				// Token: 0x0400A29E RID: 41630
				public static LocString CONFIGURATION_REQUIRED = "Configuration Required:";

				// Token: 0x0400A29F RID: 41631
				public static LocString CONFIG_REQUEST_SUIT = "Deliver Suit";

				// Token: 0x0400A2A0 RID: 41632
				public static LocString CONFIG_REQUEST_SUIT_TOOLTIP = "Duplicants will immediately deliver and dock the nearest unequipped suit";

				// Token: 0x0400A2A1 RID: 41633
				public static LocString CONFIG_NO_SUIT = "Leave Empty";

				// Token: 0x0400A2A2 RID: 41634
				public static LocString CONFIG_NO_SUIT_TOOLTIP = "The next suited Duplicant to pass by will unequip their suit and dock it here";

				// Token: 0x0400A2A3 RID: 41635
				public static LocString CONFIG_CANCEL_REQUEST = "Cancel Request";

				// Token: 0x0400A2A4 RID: 41636
				public static LocString CONFIG_CANCEL_REQUEST_TOOLTIP = "Cancel this suit delivery";

				// Token: 0x0400A2A5 RID: 41637
				public static LocString CONFIG_DROP_SUIT = "Undock Suit";

				// Token: 0x0400A2A6 RID: 41638
				public static LocString CONFIG_DROP_SUIT_TOOLTIP = "Disconnect this suit, dropping it on the ground";

				// Token: 0x0400A2A7 RID: 41639
				public static LocString CONFIG_DROP_SUIT_NO_SUIT_TOOLTIP = "There is no suit in this building to undock";
			}

			// Token: 0x020024F7 RID: 9463
			public class AUTOMATABLE_SIDE_SCREEN
			{
				// Token: 0x0400A2A8 RID: 41640
				public static LocString TITLE = "Automatable Storage";

				// Token: 0x0400A2A9 RID: 41641
				public static LocString ALLOWMANUALBUTTON = "Allow Manual Use";

				// Token: 0x0400A2AA RID: 41642
				public static LocString ALLOWMANUALBUTTONTOOLTIP = "Allow Duplicants to manually manage these storage materials";
			}

			// Token: 0x020024F8 RID: 9464
			public class STUDYABLE_SIDE_SCREEN
			{
				// Token: 0x0400A2AB RID: 41643
				public static LocString TITLE = "Analyze Natural Feature";

				// Token: 0x0400A2AC RID: 41644
				public static LocString STUDIED_STATUS = "Researchers have completed their analysis and compiled their findings.";

				// Token: 0x0400A2AD RID: 41645
				public static LocString STUDIED_BUTTON = "ANALYSIS COMPLETE";

				// Token: 0x0400A2AE RID: 41646
				public static LocString SEND_STATUS = "Send a researcher to gather data here.\n\nAnalyzing a feature takes time, but yields useful results.";

				// Token: 0x0400A2AF RID: 41647
				public static LocString SEND_BUTTON = "ANALYZE";

				// Token: 0x0400A2B0 RID: 41648
				public static LocString PENDING_STATUS = "A researcher is in the process of studying this feature.";

				// Token: 0x0400A2B1 RID: 41649
				public static LocString PENDING_BUTTON = "CANCEL ANALYSIS";
			}

			// Token: 0x020024F9 RID: 9465
			public class MEDICALCOTSIDESCREEN
			{
				// Token: 0x0400A2B2 RID: 41650
				public static LocString TITLE = "Severity Requirement";

				// Token: 0x0400A2B3 RID: 41651
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A Duplicant may not use this cot until their ",
					UI.PRE_KEYWORD,
					"Health",
					UI.PST_KEYWORD,
					" falls below <b>{0}%</b>"
				});
			}

			// Token: 0x020024FA RID: 9466
			public class WARPPORTALSIDESCREEN
			{
				// Token: 0x0400A2B4 RID: 41652
				public static LocString TITLE = "Teleporter";

				// Token: 0x0400A2B5 RID: 41653
				public static LocString IDLE = "Teleporter online.\nPlease select a passenger:";

				// Token: 0x0400A2B6 RID: 41654
				public static LocString WAITING = "Ready to transmit passenger.";

				// Token: 0x0400A2B7 RID: 41655
				public static LocString COMPLETE = "Passenger transmitted!";

				// Token: 0x0400A2B8 RID: 41656
				public static LocString UNDERWAY = "Transmitting passenger...";

				// Token: 0x0400A2B9 RID: 41657
				public static LocString CONSUMED = "Teleporter recharging:";

				// Token: 0x0400A2BA RID: 41658
				public static LocString BUTTON = "Teleport!";

				// Token: 0x0400A2BB RID: 41659
				public static LocString CANCELBUTTON = "Cancel";
			}

			// Token: 0x020024FB RID: 9467
			public class RADBOLTTHRESHOLDSIDESCREEN
			{
				// Token: 0x0400A2BC RID: 41660
				public static LocString TITLE = "Radbolt Threshold";

				// Token: 0x0400A2BD RID: 41661
				public static LocString CURRENT_THRESHOLD = "Current Threshold: {0}%";

				// Token: 0x0400A2BE RID: 41662
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Releases a ",
					UI.PRE_KEYWORD,
					"Radbolt",
					UI.PST_KEYWORD,
					" when stored Radbolts exceed <b>{0}</b>"
				});

				// Token: 0x0400A2BF RID: 41663
				public static LocString PROGRESS_BAR_LABEL = "Radbolt Generation";

				// Token: 0x0400A2C0 RID: 41664
				public static LocString PROGRESS_BAR_TOOLTIP = string.Concat(new string[]
				{
					"The building will emit a ",
					UI.PRE_KEYWORD,
					"Radbolt",
					UI.PST_KEYWORD,
					" in the chosen direction when fully charged"
				});
			}

			// Token: 0x020024FC RID: 9468
			public class LOGICBITSELECTORSIDESCREEN
			{
				// Token: 0x0400A2C1 RID: 41665
				public static LocString RIBBON_READER_TITLE = "Ribbon Reader";

				// Token: 0x0400A2C2 RID: 41666
				public static LocString RIBBON_READER_DESCRIPTION = "Selected <b>Bit's Signal</b> will be read by the <b>Output Port</b>";

				// Token: 0x0400A2C3 RID: 41667
				public static LocString RIBBON_WRITER_TITLE = "Ribbon Writer";

				// Token: 0x0400A2C4 RID: 41668
				public static LocString RIBBON_WRITER_DESCRIPTION = "Received <b>Signal</b> will be written to selected <b>Bit</b>";

				// Token: 0x0400A2C5 RID: 41669
				public static LocString BIT = "Bit {0}";

				// Token: 0x0400A2C6 RID: 41670
				public static LocString STATE_ACTIVE = "Green";

				// Token: 0x0400A2C7 RID: 41671
				public static LocString STATE_INACTIVE = "Red";
			}

			// Token: 0x020024FD RID: 9469
			public class LOGICALARMSIDESCREEN
			{
				// Token: 0x0400A2C8 RID: 41672
				public static LocString TITLE = "Notification Designer";

				// Token: 0x0400A2C9 RID: 41673
				public static LocString DESCRIPTION = "Notification will be sent upon receiving a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + "\n\nMaking modifications will clear any existing notifications being sent by this building.";

				// Token: 0x0400A2CA RID: 41674
				public static LocString NAME = "<b>Name:</b>";

				// Token: 0x0400A2CB RID: 41675
				public static LocString NAME_DEFAULT = "Notification";

				// Token: 0x0400A2CC RID: 41676
				public static LocString TOOLTIP = "<b>Tooltip:</b>";

				// Token: 0x0400A2CD RID: 41677
				public static LocString TOOLTIP_DEFAULT = "Tooltip";

				// Token: 0x0400A2CE RID: 41678
				public static LocString TYPE = "<b>Type:</b>";

				// Token: 0x0400A2CF RID: 41679
				public static LocString PAUSE = "<b>Pause:</b>";

				// Token: 0x0400A2D0 RID: 41680
				public static LocString ZOOM = "<b>Zoom:</b>";

				// Token: 0x02002F23 RID: 12067
				public class TOOLTIPS
				{
					// Token: 0x0400BD7A RID: 48506
					public static LocString NAME = "Select notification text";

					// Token: 0x0400BD7B RID: 48507
					public static LocString TOOLTIP = "Select notification hover text";

					// Token: 0x0400BD7C RID: 48508
					public static LocString TYPE = "Select the visual and aural style of the notification";

					// Token: 0x0400BD7D RID: 48509
					public static LocString PAUSE = "Time will pause upon notification when checked";

					// Token: 0x0400BD7E RID: 48510
					public static LocString ZOOM = "The view will zoom to this building upon notification when checked";

					// Token: 0x0400BD7F RID: 48511
					public static LocString BAD = "\"Boing boing!\"";

					// Token: 0x0400BD80 RID: 48512
					public static LocString NEUTRAL = "\"Pop!\"";

					// Token: 0x0400BD81 RID: 48513
					public static LocString DUPLICANT_THREATENING = "AHH!";
				}
			}

			// Token: 0x020024FE RID: 9470
			public class GENETICANALYSISSIDESCREEN
			{
				// Token: 0x0400A2D1 RID: 41681
				public static LocString TITLE = "Genetic Analysis";

				// Token: 0x0400A2D2 RID: 41682
				public static LocString NONE_DISCOVERED = "No mutant seeds have been found.";

				// Token: 0x0400A2D3 RID: 41683
				public static LocString SELECT_SEEDS = "Select which seed types to analyze:";

				// Token: 0x0400A2D4 RID: 41684
				public static LocString SEED_NO_MUTANTS = "</i>No mutants found</i>";

				// Token: 0x0400A2D5 RID: 41685
				public static LocString SEED_FORBIDDEN = "</i>Won't analyze</i>";

				// Token: 0x0400A2D6 RID: 41686
				public static LocString SEED_ALLOWED = "</i>Will analyze</i>";
			}
		}

		// Token: 0x02001CB0 RID: 7344
		public class USERMENUACTIONS
		{
			// Token: 0x020024FF RID: 9471
			public class CLEANTOILET
			{
				// Token: 0x0400A2D7 RID: 41687
				public static LocString NAME = "Clean Toilet";

				// Token: 0x0400A2D8 RID: 41688
				public static LocString TOOLTIP = "Empty waste from this toilet";
			}

			// Token: 0x02002500 RID: 9472
			public class CANCELCLEANTOILET
			{
				// Token: 0x0400A2D9 RID: 41689
				public static LocString NAME = "Cancel Clean";

				// Token: 0x0400A2DA RID: 41690
				public static LocString TOOLTIP = "Cancel this cleaning order";
			}

			// Token: 0x02002501 RID: 9473
			public class EMPTYBEEHIVE
			{
				// Token: 0x0400A2DB RID: 41691
				public static LocString NAME = "Enable Autoharvest";

				// Token: 0x0400A2DC RID: 41692
				public static LocString TOOLTIP = "Automatically harvest this hive when full";
			}

			// Token: 0x02002502 RID: 9474
			public class CANCELEMPTYBEEHIVE
			{
				// Token: 0x0400A2DD RID: 41693
				public static LocString NAME = "Disable Autoharvest";

				// Token: 0x0400A2DE RID: 41694
				public static LocString TOOLTIP = "Do not automatically harvest this hive";
			}

			// Token: 0x02002503 RID: 9475
			public class EMPTYDESALINATOR
			{
				// Token: 0x0400A2DF RID: 41695
				public static LocString NAME = "Empty Desalinator";

				// Token: 0x0400A2E0 RID: 41696
				public static LocString TOOLTIP = "Empty salt from this desalinator";
			}

			// Token: 0x02002504 RID: 9476
			public class CHANGE_ROOM
			{
				// Token: 0x0400A2E1 RID: 41697
				public static LocString REQUEST_OUTFIT = "Request Outfit";

				// Token: 0x0400A2E2 RID: 41698
				public static LocString REQUEST_OUTFIT_TOOLTIP = "Request outfit to be delivered to this change room";

				// Token: 0x0400A2E3 RID: 41699
				public static LocString CANCEL_REQUEST = "Cancel Request";

				// Token: 0x0400A2E4 RID: 41700
				public static LocString CANCEL_REQUEST_TOOLTIP = "Cancel outfit request";

				// Token: 0x0400A2E5 RID: 41701
				public static LocString DROP_OUTFIT = "Drop Outfit";

				// Token: 0x0400A2E6 RID: 41702
				public static LocString DROP_OUTFIT_TOOLTIP = "Drop outfit on floor";
			}

			// Token: 0x02002505 RID: 9477
			public class DUMP
			{
				// Token: 0x0400A2E7 RID: 41703
				public static LocString NAME = "Empty";

				// Token: 0x0400A2E8 RID: 41704
				public static LocString TOOLTIP = "Dump bottle contents onto the floor";

				// Token: 0x0400A2E9 RID: 41705
				public static LocString NAME_OFF = "Cancel Empty";

				// Token: 0x0400A2EA RID: 41706
				public static LocString TOOLTIP_OFF = "Cancel this empty order";
			}

			// Token: 0x02002506 RID: 9478
			public class TAGFILTER
			{
				// Token: 0x0400A2EB RID: 41707
				public static LocString NAME = "Filter Settings";

				// Token: 0x0400A2EC RID: 41708
				public static LocString TOOLTIP = "Assign materials to storage";
			}

			// Token: 0x02002507 RID: 9479
			public class CANCELCONSTRUCTION
			{
				// Token: 0x0400A2ED RID: 41709
				public static LocString NAME = "Cancel Build";

				// Token: 0x0400A2EE RID: 41710
				public static LocString TOOLTIP = "Cancel this build order";
			}

			// Token: 0x02002508 RID: 9480
			public class DIG
			{
				// Token: 0x0400A2EF RID: 41711
				public static LocString NAME = "Dig";

				// Token: 0x0400A2F0 RID: 41712
				public static LocString TOOLTIP = "Dig out this cell";

				// Token: 0x0400A2F1 RID: 41713
				public static LocString TOOLTIP_OFF = "Cancel this dig order";
			}

			// Token: 0x02002509 RID: 9481
			public class CANCELMOP
			{
				// Token: 0x0400A2F2 RID: 41714
				public static LocString NAME = "Cancel Mop";

				// Token: 0x0400A2F3 RID: 41715
				public static LocString TOOLTIP = "Cancel this mop order";
			}

			// Token: 0x0200250A RID: 9482
			public class CANCELDIG
			{
				// Token: 0x0400A2F4 RID: 41716
				public static LocString NAME = "Cancel Dig";

				// Token: 0x0400A2F5 RID: 41717
				public static LocString TOOLTIP = "Cancel this dig order";
			}

			// Token: 0x0200250B RID: 9483
			public class UPROOT
			{
				// Token: 0x0400A2F6 RID: 41718
				public static LocString NAME = "Uproot";

				// Token: 0x0400A2F7 RID: 41719
				public static LocString TOOLTIP = "Convert this plant into a seed";
			}

			// Token: 0x0200250C RID: 9484
			public class CANCELUPROOT
			{
				// Token: 0x0400A2F8 RID: 41720
				public static LocString NAME = "Cancel Uproot";

				// Token: 0x0400A2F9 RID: 41721
				public static LocString TOOLTIP = "Cancel this uproot order";
			}

			// Token: 0x0200250D RID: 9485
			public class HARVEST_WHEN_READY
			{
				// Token: 0x0400A2FA RID: 41722
				public static LocString NAME = "Enable Autoharvest";

				// Token: 0x0400A2FB RID: 41723
				public static LocString TOOLTIP = "Automatically harvest this plant when it matures";
			}

			// Token: 0x0200250E RID: 9486
			public class CANCEL_HARVEST_WHEN_READY
			{
				// Token: 0x0400A2FC RID: 41724
				public static LocString NAME = "Disable Autoharvest";

				// Token: 0x0400A2FD RID: 41725
				public static LocString TOOLTIP = "Do not automatically harvest this plant";
			}

			// Token: 0x0200250F RID: 9487
			public class HARVEST
			{
				// Token: 0x0400A2FE RID: 41726
				public static LocString NAME = "Harvest";

				// Token: 0x0400A2FF RID: 41727
				public static LocString TOOLTIP = "Harvest materials from this plant";

				// Token: 0x0400A300 RID: 41728
				public static LocString TOOLTIP_DISABLED = "This plant has nothing to harvest";
			}

			// Token: 0x02002510 RID: 9488
			public class CANCELHARVEST
			{
				// Token: 0x0400A301 RID: 41729
				public static LocString NAME = "Cancel Harvest";

				// Token: 0x0400A302 RID: 41730
				public static LocString TOOLTIP = "Cancel this harvest order";
			}

			// Token: 0x02002511 RID: 9489
			public class ATTACK
			{
				// Token: 0x0400A303 RID: 41731
				public static LocString NAME = "Attack";

				// Token: 0x0400A304 RID: 41732
				public static LocString TOOLTIP = "Attack this critter";
			}

			// Token: 0x02002512 RID: 9490
			public class CANCELATTACK
			{
				// Token: 0x0400A305 RID: 41733
				public static LocString NAME = "Cancel Attack";

				// Token: 0x0400A306 RID: 41734
				public static LocString TOOLTIP = "Cancel this attack order";
			}

			// Token: 0x02002513 RID: 9491
			public class CAPTURE
			{
				// Token: 0x0400A307 RID: 41735
				public static LocString NAME = "Wrangle";

				// Token: 0x0400A308 RID: 41736
				public static LocString TOOLTIP = "Capture this critter alive";
			}

			// Token: 0x02002514 RID: 9492
			public class CANCELCAPTURE
			{
				// Token: 0x0400A309 RID: 41737
				public static LocString NAME = "Cancel Wrangle";

				// Token: 0x0400A30A RID: 41738
				public static LocString TOOLTIP = "Cancel this wrangle order";
			}

			// Token: 0x02002515 RID: 9493
			public class RELEASEELEMENT
			{
				// Token: 0x0400A30B RID: 41739
				public static LocString NAME = "Empty Building";

				// Token: 0x0400A30C RID: 41740
				public static LocString TOOLTIP = "Refund all resources currently in use by this building";
			}

			// Token: 0x02002516 RID: 9494
			public class DECONSTRUCT
			{
				// Token: 0x0400A30D RID: 41741
				public static LocString NAME = "Deconstruct";

				// Token: 0x0400A30E RID: 41742
				public static LocString TOOLTIP = "Deconstruct this building and refund all resources";

				// Token: 0x0400A30F RID: 41743
				public static LocString NAME_OFF = "Cancel Deconstruct";

				// Token: 0x0400A310 RID: 41744
				public static LocString TOOLTIP_OFF = "Cancel this deconstruct order";
			}

			// Token: 0x02002517 RID: 9495
			public class DEMOLISH
			{
				// Token: 0x0400A311 RID: 41745
				public static LocString NAME = "Demolish";

				// Token: 0x0400A312 RID: 41746
				public static LocString TOOLTIP = "Demolish this building";

				// Token: 0x0400A313 RID: 41747
				public static LocString NAME_OFF = "Cancel Demolition";

				// Token: 0x0400A314 RID: 41748
				public static LocString TOOLTIP_OFF = "Cancel this demolition order";
			}

			// Token: 0x02002518 RID: 9496
			public class ROCKETUSAGERESTRICTION
			{
				// Token: 0x0400A315 RID: 41749
				public static LocString NAME_UNCONTROLLED = "Uncontrolled";

				// Token: 0x0400A316 RID: 41750
				public static LocString TOOLTIP_UNCONTROLLED = "Do not allow this building to be controlled by a " + BUILDINGS.PREFABS.ROCKETCONTROLSTATION.NAME;

				// Token: 0x0400A317 RID: 41751
				public static LocString NAME_CONTROLLED = "Controlled";

				// Token: 0x0400A318 RID: 41752
				public static LocString TOOLTIP_CONTROLLED = "Allow this building's operation to be controlled by a " + BUILDINGS.PREFABS.ROCKETCONTROLSTATION.NAME;
			}

			// Token: 0x02002519 RID: 9497
			public class MANUAL_DELIVERY
			{
				// Token: 0x0400A319 RID: 41753
				public static LocString NAME = "Disable Delivery";

				// Token: 0x0400A31A RID: 41754
				public static LocString TOOLTIP = "Do not deliver materials to this building";

				// Token: 0x0400A31B RID: 41755
				public static LocString NAME_OFF = "Enable Delivery";

				// Token: 0x0400A31C RID: 41756
				public static LocString TOOLTIP_OFF = "Deliver materials to this building";
			}

			// Token: 0x0200251A RID: 9498
			public class SELECTRESEARCH
			{
				// Token: 0x0400A31D RID: 41757
				public static LocString NAME = "Select Research";

				// Token: 0x0400A31E RID: 41758
				public static LocString TOOLTIP = "Choose a technology from the " + UI.FormatAsManagementMenu("Research Tree", global::Action.ManageResearch);
			}

			// Token: 0x0200251B RID: 9499
			public class RELOCATE
			{
				// Token: 0x0400A31F RID: 41759
				public static LocString NAME = "Relocate";

				// Token: 0x0400A320 RID: 41760
				public static LocString TOOLTIP = "Move this building to a new location\n\nCosts no additional resources";

				// Token: 0x0400A321 RID: 41761
				public static LocString NAME_OFF = "Cancel Relocation";

				// Token: 0x0400A322 RID: 41762
				public static LocString TOOLTIP_OFF = "Cancel this relocation order";
			}

			// Token: 0x0200251C RID: 9500
			public class ENABLEBUILDING
			{
				// Token: 0x0400A323 RID: 41763
				public static LocString NAME = "Disable Building";

				// Token: 0x0400A324 RID: 41764
				public static LocString TOOLTIP = "Halt the use of this building {Hotkey}\n\nDisabled buildings consume no energy or resources";

				// Token: 0x0400A325 RID: 41765
				public static LocString NAME_OFF = "Enable Building";

				// Token: 0x0400A326 RID: 41766
				public static LocString TOOLTIP_OFF = "Resume the use of this building {Hotkey}";
			}

			// Token: 0x0200251D RID: 9501
			public class READLORE
			{
				// Token: 0x0400A327 RID: 41767
				public static LocString NAME = "Inspect";

				// Token: 0x0400A328 RID: 41768
				public static LocString ALREADYINSPECTED = "Already inspected";

				// Token: 0x0400A329 RID: 41769
				public static LocString TOOLTIP = "Recover files from this structure";

				// Token: 0x0400A32A RID: 41770
				public static LocString TOOLTIP_ALREADYINSPECTED = "This structure has already been inspected";

				// Token: 0x0400A32B RID: 41771
				public static LocString GOTODATABASE = "View Entry";

				// Token: 0x0400A32C RID: 41772
				public static LocString SEARCH_DISPLAY = "The display is still functional. I copy its message into my database.\n\nNew Database Entry discovered.";

				// Token: 0x0400A32D RID: 41773
				public static LocString SEARCH_ELLIESDESK = "All I find on the machine is a curt e-mail from a disgruntled employee.\n\nNew Database Entry discovered.";

				// Token: 0x0400A32E RID: 41774
				public static LocString SEARCH_POD = "I search my incoming message history and find a single entry. I move the odd message into my database.\n\nNew Database Entry discovered.";

				// Token: 0x0400A32F RID: 41775
				public static LocString ALREADY_SEARCHED = "I already took everything of interest from this. I can check the Database to re-read what I found.";

				// Token: 0x0400A330 RID: 41776
				public static LocString SEARCH_CABINET = "One intact document remains - an old yellowing newspaper clipping. It won't be of much use, but I add it to my database nonetheless.\n\nNew Database Entry discovered.";

				// Token: 0x0400A331 RID: 41777
				public static LocString SEARCH_STERNSDESK = "There's an old magazine article from a publication called the \"Nucleoid\" tucked in the top drawer. I add it to my database.\n\nNew Database Entry discovered.";

				// Token: 0x0400A332 RID: 41778
				public static LocString ALREADY_SEARCHED_STERNSDESK = "The desk is eerily empty inside.";

				// Token: 0x0400A333 RID: 41779
				public static LocString SEARCH_TELEPORTER_SENDER = "While scanning the antiquated computer code of this machine I uncovered some research notes. I add them to my database.\n\nNew Database Entry discovered.";

				// Token: 0x0400A334 RID: 41780
				public static LocString SEARCH_TELEPORTER_RECEIVER = "Incongruously placed research notes are hidden within the operating instructions of this device. I add them to my database.\n\nNew Database Entry discovered.";

				// Token: 0x0400A335 RID: 41781
				public static LocString SEARCH_CRYO_TANK = "There are some safety instructions included in the operating instructions of this Cryotank. I add them to my database.\n\nNew Database Entry discovered.";

				// Token: 0x0400A336 RID: 41782
				public static LocString SEARCH_PROPGRAVITASCREATUREPOSTER = "There's a handwritten note taped to the back of this poster. I add it to my database.\n\nNew Database Entry discovered.";

				// Token: 0x02002F24 RID: 12068
				public class SEARCH_COMPUTER_PODIUM
				{
					// Token: 0x0400BD82 RID: 48514
					public static LocString SEARCH1 = "I search through the computer's database and find an unredacted e-mail.\n\nNew Database Entry unlocked.";
				}

				// Token: 0x02002F25 RID: 12069
				public class SEARCH_COMPUTER_SUCCESS
				{
					// Token: 0x0400BD83 RID: 48515
					public static LocString SEARCH1 = "After searching through the computer's database, I managed to piece together some files that piqued my interest.\n\nNew Database Entry unlocked.";

					// Token: 0x0400BD84 RID: 48516
					public static LocString SEARCH2 = "Searching through the computer, I find some recoverable files that are still readable.\n\nNew Database Entry unlocked.";

					// Token: 0x0400BD85 RID: 48517
					public static LocString SEARCH3 = "The computer looks pristine on the outside, but is corrupted internally. Still, I managed to find one uncorrupted file, and have added it to my database.\n\nNew Database Entry unlocked.";

					// Token: 0x0400BD86 RID: 48518
					public static LocString SEARCH4 = "The computer was wiped almost completely clean, except for one file hidden in the recycle bin.\n\nNew Database Entry unlocked.";

					// Token: 0x0400BD87 RID: 48519
					public static LocString SEARCH5 = "I search the computer, storing what useful data I can find in my own memory.\n\nNew Database Entry unlocked.";

					// Token: 0x0400BD88 RID: 48520
					public static LocString SEARCH6 = "This computer is broken and requires some finessing to get working. Still, I recover a handful of interesting files.\n\nNew Database Entry unlocked.";
				}

				// Token: 0x02002F26 RID: 12070
				public class SEARCH_COMPUTER_FAIL
				{
					// Token: 0x0400BD89 RID: 48521
					public static LocString SEARCH1 = "Unfortunately, the computer's hard drive is irreparably corrupted.";

					// Token: 0x0400BD8A RID: 48522
					public static LocString SEARCH2 = "The computer was wiped clean before I got here. There is nothing to recover.";

					// Token: 0x0400BD8B RID: 48523
					public static LocString SEARCH3 = "Some intact files are available on the computer, but nothing I haven't already discovered elsewhere. I find nothing else.";

					// Token: 0x0400BD8C RID: 48524
					public static LocString SEARCH4 = "The computer has nothing of import.";

					// Token: 0x0400BD8D RID: 48525
					public static LocString SEARCH5 = "Someone's left a solitaire game up. There's nothing else of interest on the computer.\n\nAlso, it looks as though they were about to lose.";

					// Token: 0x0400BD8E RID: 48526
					public static LocString SEARCH6 = "The background on this computer depicts two kittens hugging in a field of daisies. There is nothing else of import to be found.";

					// Token: 0x0400BD8F RID: 48527
					public static LocString SEARCH7 = "The user alphabetized the shortcuts on their desktop. There is nothing else of import to be found.";

					// Token: 0x0400BD90 RID: 48528
					public static LocString SEARCH8 = "The background is a picture of a golden retriever in a science lab. It looks very confused. There is nothing else of import to be found.";

					// Token: 0x0400BD91 RID: 48529
					public static LocString SEARCH9 = "This user never changed their default background. There is nothing else of import to be found. How dull.";
				}

				// Token: 0x02002F27 RID: 12071
				public class SEARCH_TECHNOLOGY_SUCCESS
				{
					// Token: 0x0400BD92 RID: 48530
					public static LocString SEARCH1 = "I scour the internal systems and find something of interest.\n\nNew Database Entry discovered.";

					// Token: 0x0400BD93 RID: 48531
					public static LocString SEARCH2 = "I see if I can salvage anything from the electronics. I add what I find to my database.\n\nNew Database Entry discovered.";

					// Token: 0x0400BD94 RID: 48532
					public static LocString SEARCH3 = "I look for anything of interest within the abandoned machinery and add what I find to my database.\n\nNew Database Entry discovered.";
				}

				// Token: 0x02002F28 RID: 12072
				public class SEARCH_OBJECT_SUCCESS
				{
					// Token: 0x0400BD95 RID: 48533
					public static LocString SEARCH1 = "I look around and recover an old file.\n\nNew Database Entry discovered.";

					// Token: 0x0400BD96 RID: 48534
					public static LocString SEARCH2 = "There's a three-ringed binder inside. I scan the surviving documents.\n\nNew Database Entry discovered.";

					// Token: 0x0400BD97 RID: 48535
					public static LocString SEARCH3 = "A discarded journal inside remains mostly intact. I scan the pages of use.\n\nNew Database Entry discovered.";

					// Token: 0x0400BD98 RID: 48536
					public static LocString SEARCH4 = "A single page of a long printout remains legible. I scan it and add it to my database.\n\nNew Database Entry discovered.";

					// Token: 0x0400BD99 RID: 48537
					public static LocString SEARCH5 = "A few loose papers can be found inside. I scan the ones that look interesting.\n\nNew Database Entry discovered.";

					// Token: 0x0400BD9A RID: 48538
					public static LocString SEARCH6 = "I find a memory stick inside and copy its data into my database.\n\nNew Database Entry discovered.";
				}

				// Token: 0x02002F29 RID: 12073
				public class SEARCH_OBJECT_FAIL
				{
					// Token: 0x0400BD9B RID: 48539
					public static LocString SEARCH1 = "I look around but find nothing of interest.";
				}

				// Token: 0x02002F2A RID: 12074
				public class SEARCH_SPACEPOI_SUCCESS
				{
					// Token: 0x0400BD9C RID: 48540
					public static LocString SEARCH1 = "A quick analysis of the hardware of this debris has uncovered some searchable files within.\n\nNew Database Entry unlocked.";

					// Token: 0x0400BD9D RID: 48541
					public static LocString SEARCH2 = "There's an archaic interface I can interact with on this device.\n\nNew Database Entry unlocked.";

					// Token: 0x0400BD9E RID: 48542
					public static LocString SEARCH3 = "While investigating the software of this wreckage, a compelling file comes to my attention.\n\nNew Database Entry unlocked.";

					// Token: 0x0400BD9F RID: 48543
					public static LocString SEARCH4 = "Not much remains of the software that once ran this spacecraft except for one file that piques my interest.\n\nNew Database Entry unlocked.";

					// Token: 0x0400BDA0 RID: 48544
					public static LocString SEARCH5 = "I find some noteworthy data hidden amongst the system files of this space junk.\n\nNew Database Entry unlocked.";

					// Token: 0x0400BDA1 RID: 48545
					public static LocString SEARCH6 = "Despite being subjected to years of degradation, there are still recoverable files in this machinery.\n\nNew Database Entry unlocked.";
				}

				// Token: 0x02002F2B RID: 12075
				public class SEARCH_SPACEPOI_FAIL
				{
					// Token: 0x0400BDA2 RID: 48546
					public static LocString SEARCH1 = "There's nothing of interest left in this old space junk.";

					// Token: 0x0400BDA3 RID: 48547
					public static LocString SEARCH2 = "I've salvaged everything I can from this vehicle.";

					// Token: 0x0400BDA4 RID: 48548
					public static LocString SEARCH3 = "Years of neglect and radioactive decay have destroyed all the useful data from this derelict spacecraft.";
				}
			}

			// Token: 0x0200251E RID: 9502
			public class OPENPOI
			{
				// Token: 0x0400A337 RID: 41783
				public static LocString NAME = "Rummage";

				// Token: 0x0400A338 RID: 41784
				public static LocString TOOLTIP = "Scrounge for usable materials";

				// Token: 0x0400A339 RID: 41785
				public static LocString NAME_OFF = "Cancel Rummage";

				// Token: 0x0400A33A RID: 41786
				public static LocString TOOLTIP_OFF = "Cancel this rummage order";
			}

			// Token: 0x0200251F RID: 9503
			public class EMPTYSTORAGE
			{
				// Token: 0x0400A33B RID: 41787
				public static LocString NAME = "Empty Storage";

				// Token: 0x0400A33C RID: 41788
				public static LocString TOOLTIP = "Eject all resources from this container";

				// Token: 0x0400A33D RID: 41789
				public static LocString NAME_OFF = "Cancel Empty";

				// Token: 0x0400A33E RID: 41790
				public static LocString TOOLTIP_OFF = "Cancel this empty order";
			}

			// Token: 0x02002520 RID: 9504
			public class COPY_BUILDING_SETTINGS
			{
				// Token: 0x0400A33F RID: 41791
				public static LocString NAME = "Copy Settings";

				// Token: 0x0400A340 RID: 41792
				public static LocString TOOLTIP = "Apply the settings and priorities of this building to other buildings of the same type {Hotkey}";
			}

			// Token: 0x02002521 RID: 9505
			public class CLEAR
			{
				// Token: 0x0400A341 RID: 41793
				public static LocString NAME = "Sweep";

				// Token: 0x0400A342 RID: 41794
				public static LocString TOOLTIP = "Put this object away in the nearest storage container";

				// Token: 0x0400A343 RID: 41795
				public static LocString NAME_OFF = "Cancel Sweeping";

				// Token: 0x0400A344 RID: 41796
				public static LocString TOOLTIP_OFF = "Cancel this sweep order";
			}

			// Token: 0x02002522 RID: 9506
			public class COMPOST
			{
				// Token: 0x0400A345 RID: 41797
				public static LocString NAME = "Compost";

				// Token: 0x0400A346 RID: 41798
				public static LocString TOOLTIP = "Mark this object for compost";

				// Token: 0x0400A347 RID: 41799
				public static LocString NAME_OFF = "Cancel Compost";

				// Token: 0x0400A348 RID: 41800
				public static LocString TOOLTIP_OFF = "Cancel this compost order";
			}

			// Token: 0x02002523 RID: 9507
			public class UNEQUIP
			{
				// Token: 0x0400A349 RID: 41801
				public static LocString NAME = "Unequip {0}";

				// Token: 0x0400A34A RID: 41802
				public static LocString TOOLTIP = "Take off and drop this equipment";
			}

			// Token: 0x02002524 RID: 9508
			public class QUARANTINE
			{
				// Token: 0x0400A34B RID: 41803
				public static LocString NAME = "Quarantine";

				// Token: 0x0400A34C RID: 41804
				public static LocString TOOLTIP = "Isolate this Duplicant\nThe Duplicant will return to their assigned Cot";

				// Token: 0x0400A34D RID: 41805
				public static LocString TOOLTIP_DISABLED = "No quarantine zone assigned";

				// Token: 0x0400A34E RID: 41806
				public static LocString NAME_OFF = "Cancel Quarantine";

				// Token: 0x0400A34F RID: 41807
				public static LocString TOOLTIP_OFF = "Cancel this quarantine order";
			}

			// Token: 0x02002525 RID: 9509
			public class DRAWPATHS
			{
				// Token: 0x0400A350 RID: 41808
				public static LocString NAME = "Show Navigation";

				// Token: 0x0400A351 RID: 41809
				public static LocString TOOLTIP = "Show all areas within this Duplicant's reach";

				// Token: 0x0400A352 RID: 41810
				public static LocString NAME_OFF = "Hide Navigation";

				// Token: 0x0400A353 RID: 41811
				public static LocString TOOLTIP_OFF = "Hide areas within this Duplicant's reach";
			}

			// Token: 0x02002526 RID: 9510
			public class MOVETOLOCATION
			{
				// Token: 0x0400A354 RID: 41812
				public static LocString NAME = "Move To";

				// Token: 0x0400A355 RID: 41813
				public static LocString TOOLTIP = "Move this Duplicant to a specific location";
			}

			// Token: 0x02002527 RID: 9511
			public class FOLLOWCAM
			{
				// Token: 0x0400A356 RID: 41814
				public static LocString NAME = "Follow Cam";

				// Token: 0x0400A357 RID: 41815
				public static LocString TOOLTIP = "Track this Duplicant with the camera";
			}

			// Token: 0x02002528 RID: 9512
			public class WORKABLE_DIRECTION_BOTH
			{
				// Token: 0x0400A358 RID: 41816
				public static LocString NAME = " Set Direction: Both";

				// Token: 0x0400A359 RID: 41817
				public static LocString TOOLTIP = "Select to make Duplicants wash when passing by in either direction";
			}

			// Token: 0x02002529 RID: 9513
			public class WORKABLE_DIRECTION_LEFT
			{
				// Token: 0x0400A35A RID: 41818
				public static LocString NAME = "Set Direction: Left";

				// Token: 0x0400A35B RID: 41819
				public static LocString TOOLTIP = "Select to make Duplicants wash when passing by from right to left";
			}

			// Token: 0x0200252A RID: 9514
			public class WORKABLE_DIRECTION_RIGHT
			{
				// Token: 0x0400A35C RID: 41820
				public static LocString NAME = "Set Direction: Right";

				// Token: 0x0400A35D RID: 41821
				public static LocString TOOLTIP = "Select to make Duplicants wash when passing by from left to right";
			}

			// Token: 0x0200252B RID: 9515
			public class MANUAL_PUMP_DELIVERY
			{
				// Token: 0x02002F2C RID: 12076
				public static class ALLOWED
				{
					// Token: 0x0400BDA5 RID: 48549
					public static LocString NAME = "Enable Auto-Bottle";

					// Token: 0x0400BDA6 RID: 48550
					public static LocString TOOLTIP = "If enabled, Duplicants will deliver bottled liquids to this building directly from Pitcher Pumps";
				}

				// Token: 0x02002F2D RID: 12077
				public static class DENIED
				{
					// Token: 0x0400BDA7 RID: 48551
					public static LocString NAME = "Disable Auto-Bottle";

					// Token: 0x0400BDA8 RID: 48552
					public static LocString TOOLTIP = "If disabled, Duplicants will no longer deliver bottled liquids directly from Pitcher Pumps";
				}

				// Token: 0x02002F2E RID: 12078
				public static class ALLOWED_GAS
				{
					// Token: 0x0400BDA9 RID: 48553
					public static LocString NAME = "Enable Auto-Bottle";

					// Token: 0x0400BDAA RID: 48554
					public static LocString TOOLTIP = "If enabled, Duplicants will deliver gas canisters to this building directly from Canister Fillers";
				}

				// Token: 0x02002F2F RID: 12079
				public static class DENIED_GAS
				{
					// Token: 0x0400BDAB RID: 48555
					public static LocString NAME = "Disable Auto-Bottle";

					// Token: 0x0400BDAC RID: 48556
					public static LocString TOOLTIP = "If disabled, Duplicants will no longer deliver gas canisters directly from Canister Fillers";
				}
			}

			// Token: 0x0200252C RID: 9516
			public class SUIT_MARKER_TRAVERSAL
			{
				// Token: 0x02002F30 RID: 12080
				public static class ONLY_WHEN_ROOM_AVAILABLE
				{
					// Token: 0x0400BDAD RID: 48557
					public static LocString NAME = "Clearance: Vacancy";

					// Token: 0x0400BDAE RID: 48558
					public static LocString TOOLTIP = "Suited Duplicants may only pass if there is an available dock to store their suit";
				}

				// Token: 0x02002F31 RID: 12081
				public static class ALWAYS
				{
					// Token: 0x0400BDAF RID: 48559
					public static LocString NAME = "Clearance: Always";

					// Token: 0x0400BDB0 RID: 48560
					public static LocString TOOLTIP = "Suited Duplicants may pass even if there is no room to store their suits\n\nWhen all available docks are full, Duplicants will unequip their suits and drop them on the floor";
				}
			}

			// Token: 0x0200252D RID: 9517
			public class ACTIVATEBUILDING
			{
				// Token: 0x0400A35E RID: 41822
				public static LocString ACTIVATE = "Activate";

				// Token: 0x0400A35F RID: 41823
				public static LocString TOOLTIP_ACTIVATE = "Request a Duplicant to activate this building";

				// Token: 0x0400A360 RID: 41824
				public static LocString TOOLTIP_ACTIVATED = "This building has already been activated";

				// Token: 0x0400A361 RID: 41825
				public static LocString ACTIVATE_CANCEL = "Cancel Activation";

				// Token: 0x0400A362 RID: 41826
				public static LocString ACTIVATED = "Activated";

				// Token: 0x0400A363 RID: 41827
				public static LocString TOOLTIP_CANCEL = "Cancel activation of this building";
			}

			// Token: 0x0200252E RID: 9518
			public class ACCEPT_MUTANT_SEEDS
			{
				// Token: 0x0400A364 RID: 41828
				public static LocString ACCEPT = "Allow Mutants";

				// Token: 0x0400A365 RID: 41829
				public static LocString REJECT = "Forbid Mutants";

				// Token: 0x0400A366 RID: 41830
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Toggle whether or not this building will accept ",
					UI.PRE_KEYWORD,
					"Mutant Seeds",
					UI.PST_KEYWORD,
					" for recipes that could use them"
				});

				// Token: 0x0400A367 RID: 41831
				public static LocString FISH_FEEDER_TOOLTIP = string.Concat(new string[]
				{
					"Toggle whether or not this feeder will accept ",
					UI.PRE_KEYWORD,
					"Mutant Seeds",
					UI.PST_KEYWORD,
					" for critters who eat them"
				});
			}
		}

		// Token: 0x02001CB1 RID: 7345
		public class BUILDCATEGORIES
		{
			// Token: 0x0200252F RID: 9519
			public static class BASE
			{
				// Token: 0x0400A368 RID: 41832
				public static LocString NAME = UI.FormatAsLink("Base", "BUILDCATEGORYBASE");

				// Token: 0x0400A369 RID: 41833
				public static LocString TOOLTIP = "Maintain the colony's infrastructure with these homebase basics. {Hotkey}";
			}

			// Token: 0x02002530 RID: 9520
			public static class CONVEYANCE
			{
				// Token: 0x0400A36A RID: 41834
				public static LocString NAME = UI.FormatAsLink("Shipping", "BUILDCATEGORYCONVEYANCE");

				// Token: 0x0400A36B RID: 41835
				public static LocString TOOLTIP = "Transport ore and solid materials around my base. {Hotkey}";
			}

			// Token: 0x02002531 RID: 9521
			public static class OXYGEN
			{
				// Token: 0x0400A36C RID: 41836
				public static LocString NAME = UI.FormatAsLink("Oxygen", "BUILDCATEGORYOXYGEN");

				// Token: 0x0400A36D RID: 41837
				public static LocString TOOLTIP = "Everything I need to keep the colony breathing. {Hotkey}";
			}

			// Token: 0x02002532 RID: 9522
			public static class POWER
			{
				// Token: 0x0400A36E RID: 41838
				public static LocString NAME = UI.FormatAsLink("Power", "BUILDCATEGORYPOWER");

				// Token: 0x0400A36F RID: 41839
				public static LocString TOOLTIP = "Need to power the colony? Here's how to do it! {Hotkey}";
			}

			// Token: 0x02002533 RID: 9523
			public static class FOOD
			{
				// Token: 0x0400A370 RID: 41840
				public static LocString NAME = UI.FormatAsLink("Food", "BUILDCATEGORYFOOD");

				// Token: 0x0400A371 RID: 41841
				public static LocString TOOLTIP = "Keep my Duplicants' spirits high and their bellies full. {Hotkey}";
			}

			// Token: 0x02002534 RID: 9524
			public static class UTILITIES
			{
				// Token: 0x0400A372 RID: 41842
				public static LocString NAME = UI.FormatAsLink("Utilities", "BUILDCATEGORYUTILITIES");

				// Token: 0x0400A373 RID: 41843
				public static LocString TOOLTIP = "Heat up and cool down. {Hotkey}";
			}

			// Token: 0x02002535 RID: 9525
			public static class PLUMBING
			{
				// Token: 0x0400A374 RID: 41844
				public static LocString NAME = UI.FormatAsLink("Plumbing", "BUILDCATEGORYPLUMBING");

				// Token: 0x0400A375 RID: 41845
				public static LocString TOOLTIP = "Get the colony's water running and its sewage flowing. {Hotkey}";
			}

			// Token: 0x02002536 RID: 9526
			public static class HVAC
			{
				// Token: 0x0400A376 RID: 41846
				public static LocString NAME = UI.FormatAsLink("Ventilation", "BUILDCATEGORYHVAC");

				// Token: 0x0400A377 RID: 41847
				public static LocString TOOLTIP = "Control the flow of gas in the base. {Hotkey}";
			}

			// Token: 0x02002537 RID: 9527
			public static class REFINING
			{
				// Token: 0x0400A378 RID: 41848
				public static LocString NAME = UI.FormatAsLink("Refinement", "BUILDCATEGORYREFINING");

				// Token: 0x0400A379 RID: 41849
				public static LocString TOOLTIP = "Use the resources I want, filter the ones I don't. {Hotkey}";
			}

			// Token: 0x02002538 RID: 9528
			public static class ROCKETRY
			{
				// Token: 0x0400A37A RID: 41850
				public static LocString NAME = UI.FormatAsLink("Rocketry", "BUILDCATEGORYROCKETRY");

				// Token: 0x0400A37B RID: 41851
				public static LocString TOOLTIP = "With rockets, the sky's no longer the limit! {Hotkey}";
			}

			// Token: 0x02002539 RID: 9529
			public static class MEDICAL
			{
				// Token: 0x0400A37C RID: 41852
				public static LocString NAME = UI.FormatAsLink("Medicine", "BUILDCATEGORYMEDICAL");

				// Token: 0x0400A37D RID: 41853
				public static LocString TOOLTIP = "A cure for everything but the common cold. {Hotkey}";
			}

			// Token: 0x0200253A RID: 9530
			public static class FURNITURE
			{
				// Token: 0x0400A37E RID: 41854
				public static LocString NAME = UI.FormatAsLink("Furniture", "BUILDCATEGORYFURNITURE");

				// Token: 0x0400A37F RID: 41855
				public static LocString TOOLTIP = "Amenities to keep my Duplicants happy, comfy and efficient. {Hotkey}";
			}

			// Token: 0x0200253B RID: 9531
			public static class EQUIPMENT
			{
				// Token: 0x0400A380 RID: 41856
				public static LocString NAME = UI.FormatAsLink("Stations", "BUILDCATEGORYEQUIPMENT");

				// Token: 0x0400A381 RID: 41857
				public static LocString TOOLTIP = "Unlock new technologies through the power of science! {Hotkey}";
			}

			// Token: 0x0200253C RID: 9532
			public static class MISC
			{
				// Token: 0x0400A382 RID: 41858
				public static LocString NAME = UI.FormatAsLink("Decor", "BUILDCATEGORYMISC");

				// Token: 0x0400A383 RID: 41859
				public static LocString TOOLTIP = "Spruce up my colony with some lovely interior decorating. {Hotkey}";
			}

			// Token: 0x0200253D RID: 9533
			public static class AUTOMATION
			{
				// Token: 0x0400A384 RID: 41860
				public static LocString NAME = UI.FormatAsLink("Automation", "BUILDCATEGORYAUTOMATION");

				// Token: 0x0400A385 RID: 41861
				public static LocString TOOLTIP = "Automate my base with a wide range of sensors. {Hotkey}";
			}

			// Token: 0x0200253E RID: 9534
			public static class HEP
			{
				// Token: 0x0400A386 RID: 41862
				public static LocString NAME = UI.FormatAsLink("Radiation", "BUILDCATEGORYHEP");

				// Token: 0x0400A387 RID: 41863
				public static LocString TOOLTIP = "Here's where things get rad. {Hotkey}";
			}
		}

		// Token: 0x02001CB2 RID: 7346
		public class NEWBUILDCATEGORIES
		{
			// Token: 0x0200253F RID: 9535
			public static class BASE
			{
				// Token: 0x0400A388 RID: 41864
				public static LocString NAME = UI.FormatAsLink("Base", "BUILD_CATEGORY_BASE");

				// Token: 0x0400A389 RID: 41865
				public static LocString TOOLTIP = "Maintain the colony's infrastructure with these homebase basics. {Hotkey}";
			}

			// Token: 0x02002540 RID: 9536
			public static class INFRASTRUCTURE
			{
				// Token: 0x0400A38A RID: 41866
				public static LocString NAME = UI.FormatAsLink("Utilities", "BUILD_CATEGORY_INFRASTRUCTURE");

				// Token: 0x0400A38B RID: 41867
				public static LocString TOOLTIP = "Power, plumbing, and ventilation can all be found here. {Hotkey}";
			}

			// Token: 0x02002541 RID: 9537
			public static class FOODANDAGRICULTURE
			{
				// Token: 0x0400A38C RID: 41868
				public static LocString NAME = UI.FormatAsLink("Food", "BUILD_CATEGORY_FOODANDAGRICULTURE");

				// Token: 0x0400A38D RID: 41869
				public static LocString TOOLTIP = "Keep my Duplicants' spirits high and their bellies full. {Hotkey}";
			}

			// Token: 0x02002542 RID: 9538
			public static class LOGISTICS
			{
				// Token: 0x0400A38E RID: 41870
				public static LocString NAME = UI.FormatAsLink("Logistics", "BUILD_CATEGORY_LOGISTICS");

				// Token: 0x0400A38F RID: 41871
				public static LocString TOOLTIP = "Devices for base automation and material transport. {Hotkey}";
			}

			// Token: 0x02002543 RID: 9539
			public static class HEALTHANDHAPPINESS
			{
				// Token: 0x0400A390 RID: 41872
				public static LocString NAME = UI.FormatAsLink("Accommodation", "BUILD_CATEGORY_HEALTHANDHAPPINESS");

				// Token: 0x0400A391 RID: 41873
				public static LocString TOOLTIP = "Everything a Duplicant needs to stay happy, healthy, and fulfilled. {Hotkey}";
			}

			// Token: 0x02002544 RID: 9540
			public static class INDUSTRIAL
			{
				// Token: 0x0400A392 RID: 41874
				public static LocString NAME = UI.FormatAsLink("Industrials", "BUILD_CATEGORY_INDUSTRIAL");

				// Token: 0x0400A393 RID: 41875
				public static LocString TOOLTIP = "Machinery for oxygen production, heat management, and material refinement. {Hotkey}";
			}

			// Token: 0x02002545 RID: 9541
			public static class LADDERS
			{
				// Token: 0x0400A394 RID: 41876
				public static LocString NAME = "Ladders";

				// Token: 0x0400A395 RID: 41877
				public static LocString BUILDMENUTITLE = "Ladders";

				// Token: 0x0400A396 RID: 41878
				public static LocString TOOLTIP = "";

				// Token: 0x0400A397 RID: 41879
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002546 RID: 9542
			public static class TILES
			{
				// Token: 0x0400A398 RID: 41880
				public static LocString NAME = "Tiles and Drywall";

				// Token: 0x0400A399 RID: 41881
				public static LocString BUILDMENUTITLE = "Tiles and Drywall";

				// Token: 0x0400A39A RID: 41882
				public static LocString TOOLTIP = "";

				// Token: 0x0400A39B RID: 41883
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002547 RID: 9543
			public static class PRINTINGPODS
			{
				// Token: 0x0400A39C RID: 41884
				public static LocString NAME = "Printing Pods";

				// Token: 0x0400A39D RID: 41885
				public static LocString BUILDMENUTITLE = "Printing Pods";

				// Token: 0x0400A39E RID: 41886
				public static LocString TOOLTIP = "";

				// Token: 0x0400A39F RID: 41887
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002548 RID: 9544
			public static class DOORS
			{
				// Token: 0x0400A3A0 RID: 41888
				public static LocString NAME = "Doors";

				// Token: 0x0400A3A1 RID: 41889
				public static LocString BUILDMENUTITLE = "Doors";

				// Token: 0x0400A3A2 RID: 41890
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3A3 RID: 41891
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002549 RID: 9545
			public static class STORAGE
			{
				// Token: 0x0400A3A4 RID: 41892
				public static LocString NAME = "Storage";

				// Token: 0x0400A3A5 RID: 41893
				public static LocString BUILDMENUTITLE = "Storage";

				// Token: 0x0400A3A6 RID: 41894
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3A7 RID: 41895
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200254A RID: 9546
			public static class TRANSPORT
			{
				// Token: 0x0400A3A8 RID: 41896
				public static LocString NAME = "Transit Tubes";

				// Token: 0x0400A3A9 RID: 41897
				public static LocString BUILDMENUTITLE = "Transit Tubes";

				// Token: 0x0400A3AA RID: 41898
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3AB RID: 41899
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200254B RID: 9547
			public static class PRODUCERS
			{
				// Token: 0x0400A3AC RID: 41900
				public static LocString NAME = "Production";

				// Token: 0x0400A3AD RID: 41901
				public static LocString BUILDMENUTITLE = "Production";

				// Token: 0x0400A3AE RID: 41902
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3AF RID: 41903
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200254C RID: 9548
			public static class SCRUBBERS
			{
				// Token: 0x0400A3B0 RID: 41904
				public static LocString NAME = "Purification";

				// Token: 0x0400A3B1 RID: 41905
				public static LocString BUILDMENUTITLE = "Purification";

				// Token: 0x0400A3B2 RID: 41906
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3B3 RID: 41907
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200254D RID: 9549
			public static class BATTERIES
			{
				// Token: 0x0400A3B4 RID: 41908
				public static LocString NAME = "Batteries";

				// Token: 0x0400A3B5 RID: 41909
				public static LocString BUILDMENUTITLE = "Batteries";

				// Token: 0x0400A3B6 RID: 41910
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3B7 RID: 41911
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200254E RID: 9550
			public static class SWITCHES
			{
				// Token: 0x0400A3B8 RID: 41912
				public static LocString NAME = "Switches";

				// Token: 0x0400A3B9 RID: 41913
				public static LocString BUILDMENUTITLE = "Switches";

				// Token: 0x0400A3BA RID: 41914
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3BB RID: 41915
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200254F RID: 9551
			public static class COOKING
			{
				// Token: 0x0400A3BC RID: 41916
				public static LocString NAME = "Cooking";

				// Token: 0x0400A3BD RID: 41917
				public static LocString BUILDMENUTITLE = "Cooking";

				// Token: 0x0400A3BE RID: 41918
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3BF RID: 41919
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002550 RID: 9552
			public static class FARMING
			{
				// Token: 0x0400A3C0 RID: 41920
				public static LocString NAME = "Farming";

				// Token: 0x0400A3C1 RID: 41921
				public static LocString BUILDMENUTITLE = "Farming";

				// Token: 0x0400A3C2 RID: 41922
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3C3 RID: 41923
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002551 RID: 9553
			public static class RANCHING
			{
				// Token: 0x0400A3C4 RID: 41924
				public static LocString NAME = "Ranching";

				// Token: 0x0400A3C5 RID: 41925
				public static LocString BUILDMENUTITLE = "Ranching";

				// Token: 0x0400A3C6 RID: 41926
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3C7 RID: 41927
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002552 RID: 9554
			public static class WASHROOM
			{
				// Token: 0x0400A3C8 RID: 41928
				public static LocString NAME = "Washroom";

				// Token: 0x0400A3C9 RID: 41929
				public static LocString BUILDMENUTITLE = "Washroom";

				// Token: 0x0400A3CA RID: 41930
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3CB RID: 41931
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002553 RID: 9555
			public static class VALVES
			{
				// Token: 0x0400A3CC RID: 41932
				public static LocString NAME = "Valves";

				// Token: 0x0400A3CD RID: 41933
				public static LocString BUILDMENUTITLE = "Valves";

				// Token: 0x0400A3CE RID: 41934
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3CF RID: 41935
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002554 RID: 9556
			public static class PUMPS
			{
				// Token: 0x0400A3D0 RID: 41936
				public static LocString NAME = "Pumps";

				// Token: 0x0400A3D1 RID: 41937
				public static LocString BUILDMENUTITLE = "Pumps";

				// Token: 0x0400A3D2 RID: 41938
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3D3 RID: 41939
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002555 RID: 9557
			public static class SENSORS
			{
				// Token: 0x0400A3D4 RID: 41940
				public static LocString NAME = "Sensors";

				// Token: 0x0400A3D5 RID: 41941
				public static LocString BUILDMENUTITLE = "Sensors";

				// Token: 0x0400A3D6 RID: 41942
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3D7 RID: 41943
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002556 RID: 9558
			public static class PORTS
			{
				// Token: 0x0400A3D8 RID: 41944
				public static LocString NAME = "Ports";

				// Token: 0x0400A3D9 RID: 41945
				public static LocString BUILDMENUTITLE = "Ports";

				// Token: 0x0400A3DA RID: 41946
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3DB RID: 41947
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002557 RID: 9559
			public static class MATERIALS
			{
				// Token: 0x0400A3DC RID: 41948
				public static LocString NAME = "Materials";

				// Token: 0x0400A3DD RID: 41949
				public static LocString BUILDMENUTITLE = "Materials";

				// Token: 0x0400A3DE RID: 41950
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3DF RID: 41951
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002558 RID: 9560
			public static class OIL
			{
				// Token: 0x0400A3E0 RID: 41952
				public static LocString NAME = "Oil";

				// Token: 0x0400A3E1 RID: 41953
				public static LocString BUILDMENUTITLE = "Oil";

				// Token: 0x0400A3E2 RID: 41954
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3E3 RID: 41955
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002559 RID: 9561
			public static class ADVANCED
			{
				// Token: 0x0400A3E4 RID: 41956
				public static LocString NAME = "Advanced";

				// Token: 0x0400A3E5 RID: 41957
				public static LocString BUILDMENUTITLE = "Advanced";

				// Token: 0x0400A3E6 RID: 41958
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3E7 RID: 41959
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200255A RID: 9562
			public static class BEDS
			{
				// Token: 0x0400A3E8 RID: 41960
				public static LocString NAME = "Beds";

				// Token: 0x0400A3E9 RID: 41961
				public static LocString BUILDMENUTITLE = "Beds";

				// Token: 0x0400A3EA RID: 41962
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3EB RID: 41963
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200255B RID: 9563
			public static class LIGHTS
			{
				// Token: 0x0400A3EC RID: 41964
				public static LocString NAME = "Lights";

				// Token: 0x0400A3ED RID: 41965
				public static LocString BUILDMENUTITLE = "Lights";

				// Token: 0x0400A3EE RID: 41966
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3EF RID: 41967
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200255C RID: 9564
			public static class DINING
			{
				// Token: 0x0400A3F0 RID: 41968
				public static LocString NAME = "Dining";

				// Token: 0x0400A3F1 RID: 41969
				public static LocString BUILDMENUTITLE = "Dining";

				// Token: 0x0400A3F2 RID: 41970
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3F3 RID: 41971
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200255D RID: 9565
			public static class MANUFACTURING
			{
				// Token: 0x0400A3F4 RID: 41972
				public static LocString NAME = "Manufacturing";

				// Token: 0x0400A3F5 RID: 41973
				public static LocString BUILDMENUTITLE = "Manufacturing";

				// Token: 0x0400A3F6 RID: 41974
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3F7 RID: 41975
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200255E RID: 9566
			public static class TEMPERATURE
			{
				// Token: 0x0400A3F8 RID: 41976
				public static LocString NAME = "Temperature";

				// Token: 0x0400A3F9 RID: 41977
				public static LocString BUILDMENUTITLE = "Temperature";

				// Token: 0x0400A3FA RID: 41978
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3FB RID: 41979
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200255F RID: 9567
			public static class RESEARCH
			{
				// Token: 0x0400A3FC RID: 41980
				public static LocString NAME = "Research";

				// Token: 0x0400A3FD RID: 41981
				public static LocString BUILDMENUTITLE = "Research";

				// Token: 0x0400A3FE RID: 41982
				public static LocString TOOLTIP = "";

				// Token: 0x0400A3FF RID: 41983
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002560 RID: 9568
			public static class GENERATORS
			{
				// Token: 0x0400A400 RID: 41984
				public static LocString NAME = "Generators";

				// Token: 0x0400A401 RID: 41985
				public static LocString BUILDMENUTITLE = "Generators";

				// Token: 0x0400A402 RID: 41986
				public static LocString TOOLTIP = "";

				// Token: 0x0400A403 RID: 41987
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002561 RID: 9569
			public static class WIRES
			{
				// Token: 0x0400A404 RID: 41988
				public static LocString NAME = "Wires";

				// Token: 0x0400A405 RID: 41989
				public static LocString BUILDMENUTITLE = "Wires";

				// Token: 0x0400A406 RID: 41990
				public static LocString TOOLTIP = "";

				// Token: 0x0400A407 RID: 41991
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002562 RID: 9570
			public static class LOGICGATES
			{
				// Token: 0x0400A408 RID: 41992
				public static LocString NAME = "Gates";

				// Token: 0x0400A409 RID: 41993
				public static LocString BUILDMENUTITLE = "Gates";

				// Token: 0x0400A40A RID: 41994
				public static LocString TOOLTIP = "";

				// Token: 0x0400A40B RID: 41995
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002563 RID: 9571
			public static class TRANSMISSIONS
			{
				// Token: 0x0400A40C RID: 41996
				public static LocString NAME = "Transmissions";

				// Token: 0x0400A40D RID: 41997
				public static LocString BUILDMENUTITLE = "Transmissions";

				// Token: 0x0400A40E RID: 41998
				public static LocString TOOLTIP = "";

				// Token: 0x0400A40F RID: 41999
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002564 RID: 9572
			public static class LOGICMANAGER
			{
				// Token: 0x0400A410 RID: 42000
				public static LocString NAME = "Monitoring";

				// Token: 0x0400A411 RID: 42001
				public static LocString BUILDMENUTITLE = "Monitoring";

				// Token: 0x0400A412 RID: 42002
				public static LocString TOOLTIP = "";

				// Token: 0x0400A413 RID: 42003
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002565 RID: 9573
			public static class LOGICAUDIO
			{
				// Token: 0x0400A414 RID: 42004
				public static LocString NAME = "Ambience";

				// Token: 0x0400A415 RID: 42005
				public static LocString BUILDMENUTITLE = "Ambience";

				// Token: 0x0400A416 RID: 42006
				public static LocString TOOLTIP = "";

				// Token: 0x0400A417 RID: 42007
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002566 RID: 9574
			public static class CONVEYANCESTRUCTURES
			{
				// Token: 0x0400A418 RID: 42008
				public static LocString NAME = "Structural";

				// Token: 0x0400A419 RID: 42009
				public static LocString BUILDMENUTITLE = "Structural";

				// Token: 0x0400A41A RID: 42010
				public static LocString TOOLTIP = "";

				// Token: 0x0400A41B RID: 42011
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002567 RID: 9575
			public static class BUILDMENUPORTS
			{
				// Token: 0x0400A41C RID: 42012
				public static LocString NAME = "Ports";

				// Token: 0x0400A41D RID: 42013
				public static LocString BUILDMENUTITLE = "Ports";

				// Token: 0x0400A41E RID: 42014
				public static LocString TOOLTIP = "";

				// Token: 0x0400A41F RID: 42015
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002568 RID: 9576
			public static class POWERCONTROL
			{
				// Token: 0x0400A420 RID: 42016
				public static LocString NAME = "Power\nRegulation";

				// Token: 0x0400A421 RID: 42017
				public static LocString BUILDMENUTITLE = "Power Regulation";

				// Token: 0x0400A422 RID: 42018
				public static LocString TOOLTIP = "";

				// Token: 0x0400A423 RID: 42019
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002569 RID: 9577
			public static class PLUMBINGSTRUCTURES
			{
				// Token: 0x0400A424 RID: 42020
				public static LocString NAME = "Plumbing";

				// Token: 0x0400A425 RID: 42021
				public static LocString BUILDMENUTITLE = "Plumbing";

				// Token: 0x0400A426 RID: 42022
				public static LocString TOOLTIP = "Get the colony's water running and its sewage flowing. {Hotkey}";

				// Token: 0x0400A427 RID: 42023
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200256A RID: 9578
			public static class PIPES
			{
				// Token: 0x0400A428 RID: 42024
				public static LocString NAME = "Pipes";

				// Token: 0x0400A429 RID: 42025
				public static LocString BUILDMENUTITLE = "Pipes";

				// Token: 0x0400A42A RID: 42026
				public static LocString TOOLTIP = "";

				// Token: 0x0400A42B RID: 42027
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200256B RID: 9579
			public static class VENTILATIONSTRUCTURES
			{
				// Token: 0x0400A42C RID: 42028
				public static LocString NAME = "Ventilation";

				// Token: 0x0400A42D RID: 42029
				public static LocString BUILDMENUTITLE = "Ventilation";

				// Token: 0x0400A42E RID: 42030
				public static LocString TOOLTIP = "Control the flow of gas in your base. {Hotkey}";

				// Token: 0x0400A42F RID: 42031
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200256C RID: 9580
			public static class CONVEYANCE
			{
				// Token: 0x0400A430 RID: 42032
				public static LocString NAME = "Ore\nTransport";

				// Token: 0x0400A431 RID: 42033
				public static LocString BUILDMENUTITLE = "Ore Transport";

				// Token: 0x0400A432 RID: 42034
				public static LocString TOOLTIP = "Transport ore and solid materials around my base. {Hotkey}";

				// Token: 0x0400A433 RID: 42035
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200256D RID: 9581
			public static class HYGIENE
			{
				// Token: 0x0400A434 RID: 42036
				public static LocString NAME = "Hygiene";

				// Token: 0x0400A435 RID: 42037
				public static LocString BUILDMENUTITLE = "Hygiene";

				// Token: 0x0400A436 RID: 42038
				public static LocString TOOLTIP = "Keeps my Duplicants clean.";

				// Token: 0x0400A437 RID: 42039
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200256E RID: 9582
			public static class MEDICAL
			{
				// Token: 0x0400A438 RID: 42040
				public static LocString NAME = "Medical";

				// Token: 0x0400A439 RID: 42041
				public static LocString BUILDMENUTITLE = "Medical";

				// Token: 0x0400A43A RID: 42042
				public static LocString TOOLTIP = "A cure for everything but the common cold. {Hotkey}";

				// Token: 0x0400A43B RID: 42043
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200256F RID: 9583
			public static class WELLNESS
			{
				// Token: 0x0400A43C RID: 42044
				public static LocString NAME = "Wellness";

				// Token: 0x0400A43D RID: 42045
				public static LocString BUILDMENUTITLE = "Wellness";

				// Token: 0x0400A43E RID: 42046
				public static LocString TOOLTIP = "";

				// Token: 0x0400A43F RID: 42047
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002570 RID: 9584
			public static class RECREATION
			{
				// Token: 0x0400A440 RID: 42048
				public static LocString NAME = "Recreation";

				// Token: 0x0400A441 RID: 42049
				public static LocString BUILDMENUTITLE = "Recreation";

				// Token: 0x0400A442 RID: 42050
				public static LocString TOOLTIP = "Everything needed to reduce stress and increase fun.";

				// Token: 0x0400A443 RID: 42051
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002571 RID: 9585
			public static class FURNITURE
			{
				// Token: 0x0400A444 RID: 42052
				public static LocString NAME = "Furniture";

				// Token: 0x0400A445 RID: 42053
				public static LocString BUILDMENUTITLE = "Furniture";

				// Token: 0x0400A446 RID: 42054
				public static LocString TOOLTIP = "Amenities to keep my Duplicants happy, comfy and efficient. {Hotkey}";

				// Token: 0x0400A447 RID: 42055
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002572 RID: 9586
			public static class DECOR
			{
				// Token: 0x0400A448 RID: 42056
				public static LocString NAME = "Decor";

				// Token: 0x0400A449 RID: 42057
				public static LocString BUILDMENUTITLE = "Decor";

				// Token: 0x0400A44A RID: 42058
				public static LocString TOOLTIP = "Spruce up your colony with some lovely interior decorating. {Hotkey}";

				// Token: 0x0400A44B RID: 42059
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002573 RID: 9587
			public static class OXYGEN
			{
				// Token: 0x0400A44C RID: 42060
				public static LocString NAME = "Oxygen";

				// Token: 0x0400A44D RID: 42061
				public static LocString BUILDMENUTITLE = "Oxygen";

				// Token: 0x0400A44E RID: 42062
				public static LocString TOOLTIP = "Everything I need to keep my colony breathing. {Hotkey}";

				// Token: 0x0400A44F RID: 42063
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002574 RID: 9588
			public static class UTILITIES
			{
				// Token: 0x0400A450 RID: 42064
				public static LocString NAME = "Temperature";

				// Token: 0x0400A451 RID: 42065
				public static LocString BUILDMENUTITLE = "Temperature";

				// Token: 0x0400A452 RID: 42066
				public static LocString TOOLTIP = "";

				// Token: 0x0400A453 RID: 42067
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002575 RID: 9589
			public static class REFINING
			{
				// Token: 0x0400A454 RID: 42068
				public static LocString NAME = "Refinement";

				// Token: 0x0400A455 RID: 42069
				public static LocString BUILDMENUTITLE = "Refinement";

				// Token: 0x0400A456 RID: 42070
				public static LocString TOOLTIP = "Use the resources you want, filter the ones you don't. {Hotkey}";

				// Token: 0x0400A457 RID: 42071
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002576 RID: 9590
			public static class EQUIPMENT
			{
				// Token: 0x0400A458 RID: 42072
				public static LocString NAME = "Equipment";

				// Token: 0x0400A459 RID: 42073
				public static LocString BUILDMENUTITLE = "Equipment";

				// Token: 0x0400A45A RID: 42074
				public static LocString TOOLTIP = "Unlock new technologies through the power of science! {Hotkey}";

				// Token: 0x0400A45B RID: 42075
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002577 RID: 9591
			public static class ARCHAEOLOGY
			{
				// Token: 0x0400A45C RID: 42076
				public static LocString NAME = "Archaeology";

				// Token: 0x0400A45D RID: 42077
				public static LocString BUILDMENUTITLE = "Archaeology";

				// Token: 0x0400A45E RID: 42078
				public static LocString TOOLTIP = "";

				// Token: 0x0400A45F RID: 42079
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002578 RID: 9592
			public static class INDUSTRIALSTATION
			{
				// Token: 0x0400A460 RID: 42080
				public static LocString NAME = "Industrial";

				// Token: 0x0400A461 RID: 42081
				public static LocString BUILDMENUTITLE = "Industrial";

				// Token: 0x0400A462 RID: 42082
				public static LocString TOOLTIP = "";

				// Token: 0x0400A463 RID: 42083
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002579 RID: 9593
			public static class TELESCOPES
			{
				// Token: 0x0400A464 RID: 42084
				public static LocString NAME = "Telescopes";

				// Token: 0x0400A465 RID: 42085
				public static LocString BUILDMENUTITLE = "Telescopes";

				// Token: 0x0400A466 RID: 42086
				public static LocString TOOLTIP = "Unlock new technologies through the power of science! {Hotkey}";

				// Token: 0x0400A467 RID: 42087
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200257A RID: 9594
			public static class FITTINGS
			{
				// Token: 0x0400A468 RID: 42088
				public static LocString NAME = "Fittings";

				// Token: 0x0400A469 RID: 42089
				public static LocString BUILDMENUTITLE = "Fittings";

				// Token: 0x0400A46A RID: 42090
				public static LocString TOOLTIP = "";

				// Token: 0x0400A46B RID: 42091
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200257B RID: 9595
			public static class SANITATION
			{
				// Token: 0x0400A46C RID: 42092
				public static LocString NAME = "Sanitation";

				// Token: 0x0400A46D RID: 42093
				public static LocString BUILDMENUTITLE = "Sanitation";

				// Token: 0x0400A46E RID: 42094
				public static LocString TOOLTIP = "";

				// Token: 0x0400A46F RID: 42095
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200257C RID: 9596
			public static class AUTOMATED
			{
				// Token: 0x0400A470 RID: 42096
				public static LocString NAME = "Automated";

				// Token: 0x0400A471 RID: 42097
				public static LocString BUILDMENUTITLE = "Automated";

				// Token: 0x0400A472 RID: 42098
				public static LocString TOOLTIP = "";

				// Token: 0x0400A473 RID: 42099
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200257D RID: 9597
			public static class ROCKETSTRUCTURES
			{
				// Token: 0x0400A474 RID: 42100
				public static LocString NAME = "Structural";

				// Token: 0x0400A475 RID: 42101
				public static LocString BUILDMENUTITLE = "Structural";

				// Token: 0x0400A476 RID: 42102
				public static LocString TOOLTIP = "";

				// Token: 0x0400A477 RID: 42103
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200257E RID: 9598
			public static class ROCKETNAV
			{
				// Token: 0x0400A478 RID: 42104
				public static LocString NAME = "Navigation";

				// Token: 0x0400A479 RID: 42105
				public static LocString BUILDMENUTITLE = "Navigation";

				// Token: 0x0400A47A RID: 42106
				public static LocString TOOLTIP = "";

				// Token: 0x0400A47B RID: 42107
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x0200257F RID: 9599
			public static class CONDUITSENSORS
			{
				// Token: 0x0400A47C RID: 42108
				public static LocString NAME = "Pipe Sensors";

				// Token: 0x0400A47D RID: 42109
				public static LocString BUILDMENUTITLE = "Pipe Sensors";

				// Token: 0x0400A47E RID: 42110
				public static LocString TOOLTIP = "";

				// Token: 0x0400A47F RID: 42111
				public static LocString INVISIBLETAG = "";
			}

			// Token: 0x02002580 RID: 9600
			public static class ROCKETRY
			{
				// Token: 0x0400A480 RID: 42112
				public static LocString NAME = "Rocketry";

				// Token: 0x0400A481 RID: 42113
				public static LocString BUILDMENUTITLE = "Rocketry";

				// Token: 0x0400A482 RID: 42114
				public static LocString TOOLTIP = "Rocketry {Hotkey}";

				// Token: 0x0400A483 RID: 42115
				public static LocString INVISIBLETAG = "";
			}
		}

		// Token: 0x02001CB3 RID: 7347
		public class TOOLS
		{
			// Token: 0x04008277 RID: 33399
			public static LocString TOOL_AREA_FMT = "{0} x {1}\n{2} tiles";

			// Token: 0x04008278 RID: 33400
			public static LocString TOOL_LENGTH_FMT = "{0}";

			// Token: 0x04008279 RID: 33401
			public static LocString FILTER_HOVERCARD_HEADER = "   <style=\"hovercard_element\">({0})</style>";

			// Token: 0x02002581 RID: 9601
			public class SANDBOX
			{
				// Token: 0x02002F32 RID: 12082
				public class SANDBOX_TOGGLE
				{
					// Token: 0x0400BDB1 RID: 48561
					public static LocString NAME = "SANDBOX";
				}

				// Token: 0x02002F33 RID: 12083
				public class BRUSH
				{
					// Token: 0x0400BDB2 RID: 48562
					public static LocString NAME = "Brush";

					// Token: 0x0400BDB3 RID: 48563
					public static LocString HOVERACTION = "PAINT SIM";
				}

				// Token: 0x02002F34 RID: 12084
				public class SPRINKLE
				{
					// Token: 0x0400BDB4 RID: 48564
					public static LocString NAME = "Sprinkle";

					// Token: 0x0400BDB5 RID: 48565
					public static LocString HOVERACTION = "SPRINKLE SIM";
				}

				// Token: 0x02002F35 RID: 12085
				public class FLOOD
				{
					// Token: 0x0400BDB6 RID: 48566
					public static LocString NAME = "Fill";

					// Token: 0x0400BDB7 RID: 48567
					public static LocString HOVERACTION = "PAINT SECTION";
				}

				// Token: 0x02002F36 RID: 12086
				public class MARQUEE
				{
					// Token: 0x0400BDB8 RID: 48568
					public static LocString NAME = "Marquee";
				}

				// Token: 0x02002F37 RID: 12087
				public class SAMPLE
				{
					// Token: 0x0400BDB9 RID: 48569
					public static LocString NAME = "Sample";

					// Token: 0x0400BDBA RID: 48570
					public static LocString HOVERACTION = "COPY SELECTION";
				}

				// Token: 0x02002F38 RID: 12088
				public class HEATGUN
				{
					// Token: 0x0400BDBB RID: 48571
					public static LocString NAME = "Heat Gun";

					// Token: 0x0400BDBC RID: 48572
					public static LocString HOVERACTION = "PAINT HEAT";
				}

				// Token: 0x02002F39 RID: 12089
				public class RADSTOOL
				{
					// Token: 0x0400BDBD RID: 48573
					public static LocString NAME = "Radiation Tool";

					// Token: 0x0400BDBE RID: 48574
					public static LocString HOVERACTION = "PAINT RADS";
				}

				// Token: 0x02002F3A RID: 12090
				public class STRESSTOOL
				{
					// Token: 0x0400BDBF RID: 48575
					public static LocString NAME = "Happy Tool";

					// Token: 0x0400BDC0 RID: 48576
					public static LocString HOVERACTION = "PAINT CALM";
				}

				// Token: 0x02002F3B RID: 12091
				public class SPAWNER
				{
					// Token: 0x0400BDC1 RID: 48577
					public static LocString NAME = "Spawner";

					// Token: 0x0400BDC2 RID: 48578
					public static LocString HOVERACTION = "SPAWN";
				}

				// Token: 0x02002F3C RID: 12092
				public class CLEAR_FLOOR
				{
					// Token: 0x0400BDC3 RID: 48579
					public static LocString NAME = "Clear Floor";

					// Token: 0x0400BDC4 RID: 48580
					public static LocString HOVERACTION = "DELETE DEBRIS";
				}

				// Token: 0x02002F3D RID: 12093
				public class DESTROY
				{
					// Token: 0x0400BDC5 RID: 48581
					public static LocString NAME = "Destroy";

					// Token: 0x0400BDC6 RID: 48582
					public static LocString HOVERACTION = "DELETE";
				}

				// Token: 0x02002F3E RID: 12094
				public class SPAWN_ENTITY
				{
					// Token: 0x0400BDC7 RID: 48583
					public static LocString NAME = "Spawn";
				}

				// Token: 0x02002F3F RID: 12095
				public class FOW
				{
					// Token: 0x0400BDC8 RID: 48584
					public static LocString NAME = "Reveal";

					// Token: 0x0400BDC9 RID: 48585
					public static LocString HOVERACTION = "DE-FOG";
				}

				// Token: 0x02002F40 RID: 12096
				public class CRITTER
				{
					// Token: 0x0400BDCA RID: 48586
					public static LocString NAME = "Critter Removal";

					// Token: 0x0400BDCB RID: 48587
					public static LocString HOVERACTION = "DELETE CRITTERS";
				}
			}

			// Token: 0x02002582 RID: 9602
			public class GENERIC
			{
				// Token: 0x0400A484 RID: 42116
				public static LocString BACK = "Back";

				// Token: 0x0400A485 RID: 42117
				public static LocString UNKNOWN = "UNKNOWN";

				// Token: 0x0400A486 RID: 42118
				public static LocString BUILDING_HOVER_NAME_FMT = "{Name}    <style=\"hovercard_element\">({Element})</style>";

				// Token: 0x0400A487 RID: 42119
				public static LocString LOGIC_INPUT_HOVER_FMT = "{Port}    <style=\"hovercard_element\">({Name})</style>";

				// Token: 0x0400A488 RID: 42120
				public static LocString LOGIC_OUTPUT_HOVER_FMT = "{Port}    <style=\"hovercard_element\">({Name})</style>";

				// Token: 0x0400A489 RID: 42121
				public static LocString LOGIC_MULTI_INPUT_HOVER_FMT = "{Port}    <style=\"hovercard_element\">({Name})</style>";

				// Token: 0x0400A48A RID: 42122
				public static LocString LOGIC_MULTI_OUTPUT_HOVER_FMT = "{Port}    <style=\"hovercard_element\">({Name})</style>";
			}

			// Token: 0x02002583 RID: 9603
			public class ATTACK
			{
				// Token: 0x0400A48B RID: 42123
				public static LocString NAME = "Attack";

				// Token: 0x0400A48C RID: 42124
				public static LocString TOOLNAME = "Attack tool";

				// Token: 0x0400A48D RID: 42125
				public static LocString TOOLACTION = "DRAG";
			}

			// Token: 0x02002584 RID: 9604
			public class CAPTURE
			{
				// Token: 0x0400A48E RID: 42126
				public static LocString NAME = "Wrangle";

				// Token: 0x0400A48F RID: 42127
				public static LocString TOOLNAME = "Wrangle tool";

				// Token: 0x0400A490 RID: 42128
				public static LocString TOOLACTION = "DRAG";

				// Token: 0x0400A491 RID: 42129
				public static LocString NOT_CAPTURABLE = "Cannot Wrangle";
			}

			// Token: 0x02002585 RID: 9605
			public class BUILD
			{
				// Token: 0x0400A492 RID: 42130
				public static LocString NAME = "Build {0}";

				// Token: 0x0400A493 RID: 42131
				public static LocString TOOLNAME = "Build tool";

				// Token: 0x0400A494 RID: 42132
				public static LocString TOOLACTION = UI.CLICK(UI.ClickType.CLICK) + " TO BUILD";

				// Token: 0x0400A495 RID: 42133
				public static LocString TOOLACTION_DRAG = "DRAG";
			}

			// Token: 0x02002586 RID: 9606
			public class PLACE
			{
				// Token: 0x0400A496 RID: 42134
				public static LocString NAME = "Place {0}";

				// Token: 0x0400A497 RID: 42135
				public static LocString TOOLNAME = "Place tool";

				// Token: 0x0400A498 RID: 42136
				public static LocString TOOLACTION = UI.CLICK(UI.ClickType.CLICK) + " TO PLACE";

				// Token: 0x02002F41 RID: 12097
				public class REASONS
				{
					// Token: 0x0400BDCC RID: 48588
					public static LocString CAN_OCCUPY_AREA = "Location blocked";

					// Token: 0x0400BDCD RID: 48589
					public static LocString ON_FOUNDATION = "Must place on the ground";

					// Token: 0x0400BDCE RID: 48590
					public static LocString VISIBLE_TO_SPACE = "Must have a clear path to space";

					// Token: 0x0400BDCF RID: 48591
					public static LocString RESTRICT_TO_WORLD = "Incorrect " + UI.CLUSTERMAP.PLANETOID;
				}
			}

			// Token: 0x02002587 RID: 9607
			public class MOVETOLOCATION
			{
				// Token: 0x0400A499 RID: 42137
				public static LocString NAME = "Move";

				// Token: 0x0400A49A RID: 42138
				public static LocString TOOLNAME = "Move Here";

				// Token: 0x0400A49B RID: 42139
				public static LocString TOOLACTION = UI.CLICK(UI.ClickType.CLICK) ?? "";

				// Token: 0x0400A49C RID: 42140
				public static LocString UNREACHABLE = "UNREACHABLE";
			}

			// Token: 0x02002588 RID: 9608
			public class COPYSETTINGS
			{
				// Token: 0x0400A49D RID: 42141
				public static LocString NAME = "Paste Settings";

				// Token: 0x0400A49E RID: 42142
				public static LocString TOOLNAME = "Paste Settings Tool";

				// Token: 0x0400A49F RID: 42143
				public static LocString TOOLACTION = "DRAG";
			}

			// Token: 0x02002589 RID: 9609
			public class DIG
			{
				// Token: 0x0400A4A0 RID: 42144
				public static LocString NAME = "Dig";

				// Token: 0x0400A4A1 RID: 42145
				public static LocString TOOLNAME = "Dig tool";

				// Token: 0x0400A4A2 RID: 42146
				public static LocString TOOLACTION = "DRAG";
			}

			// Token: 0x0200258A RID: 9610
			public class DISINFECT
			{
				// Token: 0x0400A4A3 RID: 42147
				public static LocString NAME = "Disinfect";

				// Token: 0x0400A4A4 RID: 42148
				public static LocString TOOLNAME = "Disinfect tool";

				// Token: 0x0400A4A5 RID: 42149
				public static LocString TOOLACTION = "DRAG";
			}

			// Token: 0x0200258B RID: 9611
			public class DISCONNECT
			{
				// Token: 0x0400A4A6 RID: 42150
				public static LocString NAME = "Disconnect";

				// Token: 0x0400A4A7 RID: 42151
				public static LocString TOOLTIP = "Sever conduits and connectors {Hotkey}";

				// Token: 0x0400A4A8 RID: 42152
				public static LocString TOOLNAME = "Disconnect tool";

				// Token: 0x0400A4A9 RID: 42153
				public static LocString TOOLACTION = "DRAG";
			}

			// Token: 0x0200258C RID: 9612
			public class CANCEL
			{
				// Token: 0x0400A4AA RID: 42154
				public static LocString NAME = "Cancel";

				// Token: 0x0400A4AB RID: 42155
				public static LocString TOOLNAME = "Cancel tool";

				// Token: 0x0400A4AC RID: 42156
				public static LocString TOOLACTION = "DRAG";
			}

			// Token: 0x0200258D RID: 9613
			public class DECONSTRUCT
			{
				// Token: 0x0400A4AD RID: 42157
				public static LocString NAME = "Deconstruct";

				// Token: 0x0400A4AE RID: 42158
				public static LocString TOOLNAME = "Deconstruct tool";

				// Token: 0x0400A4AF RID: 42159
				public static LocString TOOLACTION = "DRAG";
			}

			// Token: 0x0200258E RID: 9614
			public class CLEANUPCATEGORY
			{
				// Token: 0x0400A4B0 RID: 42160
				public static LocString NAME = "Clean";

				// Token: 0x0400A4B1 RID: 42161
				public static LocString TOOLNAME = "Clean Up tools";
			}

			// Token: 0x0200258F RID: 9615
			public class PRIORITIESCATEGORY
			{
				// Token: 0x0400A4B2 RID: 42162
				public static LocString NAME = "Priority";
			}

			// Token: 0x02002590 RID: 9616
			public class MARKFORSTORAGE
			{
				// Token: 0x0400A4B3 RID: 42163
				public static LocString NAME = "Sweep";

				// Token: 0x0400A4B4 RID: 42164
				public static LocString TOOLNAME = "Sweep tool";

				// Token: 0x0400A4B5 RID: 42165
				public static LocString TOOLACTION = "DRAG";
			}

			// Token: 0x02002591 RID: 9617
			public class MOP
			{
				// Token: 0x0400A4B6 RID: 42166
				public static LocString NAME = "Mop";

				// Token: 0x0400A4B7 RID: 42167
				public static LocString TOOLNAME = "Mop tool";

				// Token: 0x0400A4B8 RID: 42168
				public static LocString TOOLACTION = "DRAG";

				// Token: 0x0400A4B9 RID: 42169
				public static LocString TOO_MUCH_LIQUID = "Too Much Liquid";

				// Token: 0x0400A4BA RID: 42170
				public static LocString NOT_ON_FLOOR = "Not On Floor";
			}

			// Token: 0x02002592 RID: 9618
			public class HARVEST
			{
				// Token: 0x0400A4BB RID: 42171
				public static LocString NAME = "Harvest";

				// Token: 0x0400A4BC RID: 42172
				public static LocString TOOLNAME = "Harvest tool";

				// Token: 0x0400A4BD RID: 42173
				public static LocString TOOLACTION = "DRAG";
			}

			// Token: 0x02002593 RID: 9619
			public class PRIORITIZE
			{
				// Token: 0x0400A4BE RID: 42174
				public static LocString NAME = "Priority";

				// Token: 0x0400A4BF RID: 42175
				public static LocString TOOLNAME = "Priority tool";

				// Token: 0x0400A4C0 RID: 42176
				public static LocString TOOLACTION = "DRAG";

				// Token: 0x0400A4C1 RID: 42177
				public static LocString SPECIFIC_PRIORITY = "Set Priority: {0}";
			}

			// Token: 0x02002594 RID: 9620
			public class EMPTY_PIPE
			{
				// Token: 0x0400A4C2 RID: 42178
				public static LocString NAME = "Empty Pipe";

				// Token: 0x0400A4C3 RID: 42179
				public static LocString TOOLTIP = "Extract pipe contents {Hotkey}";

				// Token: 0x0400A4C4 RID: 42180
				public static LocString TOOLNAME = "Empty Pipe tool";

				// Token: 0x0400A4C5 RID: 42181
				public static LocString TOOLACTION = "DRAG";
			}

			// Token: 0x02002595 RID: 9621
			public class FILTERSCREEN
			{
				// Token: 0x0400A4C6 RID: 42182
				public static LocString OPTIONS = "Tool Filter";
			}

			// Token: 0x02002596 RID: 9622
			public class FILTERLAYERS
			{
				// Token: 0x0400A4C7 RID: 42183
				public static LocString BUILDINGS = "Buildings";

				// Token: 0x0400A4C8 RID: 42184
				public static LocString TILES = "Tiles";

				// Token: 0x0400A4C9 RID: 42185
				public static LocString WIRES = "Power Wires";

				// Token: 0x0400A4CA RID: 42186
				public static LocString LIQUIDPIPES = "Liquid Pipes";

				// Token: 0x0400A4CB RID: 42187
				public static LocString GASPIPES = "Gas Pipes";

				// Token: 0x0400A4CC RID: 42188
				public static LocString SOLIDCONDUITS = "Conveyor Rails";

				// Token: 0x0400A4CD RID: 42189
				public static LocString DIGPLACER = "Dig Orders";

				// Token: 0x0400A4CE RID: 42190
				public static LocString CLEANANDCLEAR = "Sweep & Mop Orders";

				// Token: 0x0400A4CF RID: 42191
				public static LocString ALL = "All";

				// Token: 0x0400A4D0 RID: 42192
				public static LocString HARVEST_WHEN_READY = "Enable Harvest";

				// Token: 0x0400A4D1 RID: 42193
				public static LocString DO_NOT_HARVEST = "Disable Harvest";

				// Token: 0x0400A4D2 RID: 42194
				public static LocString ATTACK = "Attack";

				// Token: 0x0400A4D3 RID: 42195
				public static LocString LOGIC = "Automation";

				// Token: 0x0400A4D4 RID: 42196
				public static LocString BACKWALL = "Background Buildings";

				// Token: 0x0400A4D5 RID: 42197
				public static LocString METAL = "Metal";

				// Token: 0x0400A4D6 RID: 42198
				public static LocString BUILDABLE = "Mineral";

				// Token: 0x0400A4D7 RID: 42199
				public static LocString FILTER = "Filtration Medium";

				// Token: 0x0400A4D8 RID: 42200
				public static LocString LIQUIFIABLE = "Liquefiable";

				// Token: 0x0400A4D9 RID: 42201
				public static LocString LIQUID = "Liquid";

				// Token: 0x0400A4DA RID: 42202
				public static LocString GAS = "Gas";

				// Token: 0x0400A4DB RID: 42203
				public static LocString CONSUMABLEORE = "Consumable Ore";

				// Token: 0x0400A4DC RID: 42204
				public static LocString ORGANICS = "Organic";

				// Token: 0x0400A4DD RID: 42205
				public static LocString FARMABLE = "Cultivable Soil";

				// Token: 0x0400A4DE RID: 42206
				public static LocString BREATHABLE = "Breathable Gas";

				// Token: 0x0400A4DF RID: 42207
				public static LocString UNBREATHABLE = "Unbreathable Gas";

				// Token: 0x0400A4E0 RID: 42208
				public static LocString AGRICULTURE = "Agriculture";

				// Token: 0x0400A4E1 RID: 42209
				public static LocString ABSOLUTETEMPERATURE = "Temperature";

				// Token: 0x0400A4E2 RID: 42210
				public static LocString ADAPTIVETEMPERATURE = "Adapt. Temperature";

				// Token: 0x0400A4E3 RID: 42211
				public static LocString HEATFLOW = "Thermal Tolerance";

				// Token: 0x0400A4E4 RID: 42212
				public static LocString STATECHANGE = "State Change";

				// Token: 0x0400A4E5 RID: 42213
				public static LocString CONSTRUCTION = "Construction";

				// Token: 0x0400A4E6 RID: 42214
				public static LocString DIG = "Digging";

				// Token: 0x0400A4E7 RID: 42215
				public static LocString CLEAN = "Cleaning";

				// Token: 0x0400A4E8 RID: 42216
				public static LocString OPERATE = "Duties";
			}
		}

		// Token: 0x02001CB4 RID: 7348
		public class DETAILTABS
		{
			// Token: 0x02002597 RID: 9623
			public class STATS
			{
				// Token: 0x0400A4E9 RID: 42217
				public static LocString NAME = "Skills";

				// Token: 0x0400A4EA RID: 42218
				public static LocString TOOLTIP = "View this Duplicant's attributes, traits, and daily stress";

				// Token: 0x0400A4EB RID: 42219
				public static LocString GROUPNAME_ATTRIBUTES = "ATTRIBUTES";

				// Token: 0x0400A4EC RID: 42220
				public static LocString GROUPNAME_STRESS = "TODAY'S STRESS";

				// Token: 0x0400A4ED RID: 42221
				public static LocString GROUPNAME_EXPECTATIONS = "EXPECTATIONS";

				// Token: 0x0400A4EE RID: 42222
				public static LocString GROUPNAME_TRAITS = "TRAITS";
			}

			// Token: 0x02002598 RID: 9624
			public class SIMPLEINFO
			{
				// Token: 0x0400A4EF RID: 42223
				public static LocString NAME = "Status";

				// Token: 0x0400A4F0 RID: 42224
				public static LocString TOOLTIP = "View the current status of the selected object";

				// Token: 0x0400A4F1 RID: 42225
				public static LocString GROUPNAME_STATUS = "STATUS";

				// Token: 0x0400A4F2 RID: 42226
				public static LocString GROUPNAME_DESCRIPTION = "INFORMATION";

				// Token: 0x0400A4F3 RID: 42227
				public static LocString GROUPNAME_CONDITION = "CONDITION";

				// Token: 0x0400A4F4 RID: 42228
				public static LocString GROUPNAME_REQUIREMENTS = "REQUIREMENTS";

				// Token: 0x0400A4F5 RID: 42229
				public static LocString GROUPNAME_RESEARCH = "RESEARCH";

				// Token: 0x0400A4F6 RID: 42230
				public static LocString GROUPNAME_LORE = "RECOVERED FILES";

				// Token: 0x0400A4F7 RID: 42231
				public static LocString GROUPNAME_FERTILITY = "EGG CHANCES";

				// Token: 0x0400A4F8 RID: 42232
				public static LocString GROUPNAME_ROCKET = "ROCKETRY";

				// Token: 0x0400A4F9 RID: 42233
				public static LocString GROUPNAME_CARGOBAY = "CARGO BAYS";

				// Token: 0x0400A4FA RID: 42234
				public static LocString GROUPNAME_ELEMENTS = "RESOURCES";

				// Token: 0x0400A4FB RID: 42235
				public static LocString GROUPNAME_LIFE = "LIFEFORMS";

				// Token: 0x0400A4FC RID: 42236
				public static LocString GROUPNAME_BIOMES = "BIOMES";

				// Token: 0x0400A4FD RID: 42237
				public static LocString GROUPNAME_GEYSERS = "GEYSERS";

				// Token: 0x0400A4FE RID: 42238
				public static LocString GROUPNAME_WORLDTRAITS = "WORLD TRAITS";

				// Token: 0x0400A4FF RID: 42239
				public static LocString GROUPNAME_CLUSTER_POI = "POINT OF INTEREST";

				// Token: 0x0400A500 RID: 42240
				public static LocString NO_GEYSERS = "No geysers detected";

				// Token: 0x0400A501 RID: 42241
				public static LocString UNKNOWN_GEYSERS = "Unknown Geysers ({num})";
			}

			// Token: 0x02002599 RID: 9625
			public class DETAILS
			{
				// Token: 0x0400A502 RID: 42242
				public static LocString NAME = "Properties";

				// Token: 0x0400A503 RID: 42243
				public static LocString MINION_NAME = "About";

				// Token: 0x0400A504 RID: 42244
				public static LocString TOOLTIP = "More information";

				// Token: 0x0400A505 RID: 42245
				public static LocString MINION_TOOLTIP = "More information";

				// Token: 0x0400A506 RID: 42246
				public static LocString GROUPNAME_DETAILS = "DETAILS";

				// Token: 0x0400A507 RID: 42247
				public static LocString GROUPNAME_CONTENTS = "CONTENTS";

				// Token: 0x0400A508 RID: 42248
				public static LocString GROUPNAME_MINION_CONTENTS = "CARRIED ITEMS";

				// Token: 0x0400A509 RID: 42249
				public static LocString STORAGE_EMPTY = "None";

				// Token: 0x0400A50A RID: 42250
				public static LocString CONTENTS_MASS = "{0}: {1}";

				// Token: 0x0400A50B RID: 42251
				public static LocString CONTENTS_TEMPERATURE = "{0} at {1}";

				// Token: 0x0400A50C RID: 42252
				public static LocString CONTENTS_ROTTABLE = "\n • {0}";

				// Token: 0x0400A50D RID: 42253
				public static LocString CONTENTS_DISEASED = "\n • {0}";

				// Token: 0x0400A50E RID: 42254
				public static LocString NET_STRESS = "<b>Today's Net Stress: {0}%</b>";

				// Token: 0x02002F42 RID: 12098
				public class RADIATIONABSORPTIONFACTOR
				{
					// Token: 0x0400BDD0 RID: 48592
					public static LocString NAME = "Radiation Blocking: {0}";

					// Token: 0x0400BDD1 RID: 48593
					public static LocString TOOLTIP = "This object will block approximately {0} of radiation.";
				}
			}

			// Token: 0x0200259A RID: 9626
			public class PERSONALITY
			{
				// Token: 0x0400A50F RID: 42255
				public static LocString NAME = "Bio";

				// Token: 0x0400A510 RID: 42256
				public static LocString TOOLTIP = "View this Duplicant's personality, resume, and amenities";

				// Token: 0x0400A511 RID: 42257
				public static LocString GROUPNAME_BIO = "ABOUT";

				// Token: 0x0400A512 RID: 42258
				public static LocString GROUPNAME_RESUME = "{0}'S RESUME";

				// Token: 0x02002F43 RID: 12099
				public class RESUME
				{
					// Token: 0x0400BDD2 RID: 48594
					public static LocString MASTERED_SKILLS = "<b><size=13>Learned Skills:</size></b>";

					// Token: 0x0400BDD3 RID: 48595
					public static LocString MASTERED_SKILLS_TOOLTIP = string.Concat(new string[]
					{
						"All ",
						UI.PRE_KEYWORD,
						"Traits",
						UI.PST_KEYWORD,
						" and ",
						UI.PRE_KEYWORD,
						"Morale Needs",
						UI.PST_KEYWORD,
						" become permanent once a Duplicant has learned a new ",
						UI.PRE_KEYWORD,
						"Skill",
						UI.PST_KEYWORD,
						"\n\n",
						BUILDINGS.PREFABS.RESETSKILLSSTATION.NAME,
						"s can be built from the ",
						UI.FormatAsBuildMenuTab("Stations Tab", global::Action.Plan10),
						" to completely reset a Duplicant's learned ",
						UI.PRE_KEYWORD,
						"Skills",
						UI.PST_KEYWORD,
						", refunding all ",
						UI.PRE_KEYWORD,
						"Skill Points",
						UI.PST_KEYWORD
					});

					// Token: 0x0400BDD4 RID: 48596
					public static LocString JOBTRAINING_TOOLTIP = string.Concat(new string[]
					{
						"{0} learned this ",
						UI.PRE_KEYWORD,
						"Skill",
						UI.PST_KEYWORD,
						" while working as a {1}"
					});

					// Token: 0x0200310B RID: 12555
					public class APTITUDES
					{
						// Token: 0x0400C2B8 RID: 49848
						public static LocString NAME = "<b><size=13>Personal Interests:</size></b>";

						// Token: 0x0400C2B9 RID: 49849
						public static LocString TOOLTIP = "{0} enjoys these types of work";
					}

					// Token: 0x0200310C RID: 12556
					public class PERKS
					{
						// Token: 0x0400C2BA RID: 49850
						public static LocString NAME = "<b><size=13>Skill Training:</size></b>";

						// Token: 0x0400C2BB RID: 49851
						public static LocString TOOLTIP = "These are permanent skills {0} gained from learned skills";
					}

					// Token: 0x0200310D RID: 12557
					public class CURRENT_ROLE
					{
						// Token: 0x0400C2BC RID: 49852
						public static LocString NAME = "<size=13><b>Current Job:</b> {0}</size>";

						// Token: 0x0400C2BD RID: 49853
						public static LocString TOOLTIP = "{0} is currently working as a {1}";

						// Token: 0x0400C2BE RID: 49854
						public static LocString NOJOB_TOOLTIP = "This {0} is... \"between jobs\" at present";
					}

					// Token: 0x0200310E RID: 12558
					public class NO_MASTERED_SKILLS
					{
						// Token: 0x0400C2BF RID: 49855
						public static LocString NAME = "None";

						// Token: 0x0400C2C0 RID: 49856
						public static LocString TOOLTIP = string.Concat(new string[]
						{
							"{0} has not learned any ",
							UI.PRE_KEYWORD,
							"Skills",
							UI.PST_KEYWORD,
							" yet"
						});
					}
				}

				// Token: 0x02002F44 RID: 12100
				public class EQUIPMENT
				{
					// Token: 0x0400BDD5 RID: 48597
					public static LocString GROUPNAME_ROOMS = "AMENITIES";

					// Token: 0x0400BDD6 RID: 48598
					public static LocString GROUPNAME_OWNABLE = "EQUIPMENT";

					// Token: 0x0400BDD7 RID: 48599
					public static LocString NO_ASSIGNABLES = "None";

					// Token: 0x0400BDD8 RID: 48600
					public static LocString NO_ASSIGNABLES_TOOLTIP = "{0} has not been assigned any buildings of their own";

					// Token: 0x0400BDD9 RID: 48601
					public static LocString UNASSIGNED = "Unassigned";

					// Token: 0x0400BDDA RID: 48602
					public static LocString UNASSIGNED_TOOLTIP = "This Duplicant has not been assigned a {0}";

					// Token: 0x0400BDDB RID: 48603
					public static LocString ASSIGNED_TOOLTIP = "{2} has been assigned a {0}\n\nEffects: {1}";

					// Token: 0x0400BDDC RID: 48604
					public static LocString NOEQUIPMENT = "None";

					// Token: 0x0400BDDD RID: 48605
					public static LocString NOEQUIPMENT_TOOLTIP = "{0}'s wearing their Printday Suit and nothing more";
				}
			}

			// Token: 0x0200259B RID: 9627
			public class ENERGYCONSUMER
			{
				// Token: 0x0400A513 RID: 42259
				public static LocString NAME = "Energy";

				// Token: 0x0400A514 RID: 42260
				public static LocString TOOLTIP = "View how much power this building consumes";
			}

			// Token: 0x0200259C RID: 9628
			public class ENERGYWIRE
			{
				// Token: 0x0400A515 RID: 42261
				public static LocString NAME = "Energy";

				// Token: 0x0400A516 RID: 42262
				public static LocString TOOLTIP = "View this wire's network";
			}

			// Token: 0x0200259D RID: 9629
			public class ENERGYGENERATOR
			{
				// Token: 0x0400A517 RID: 42263
				public static LocString NAME = "Energy";

				// Token: 0x0400A518 RID: 42264
				public static LocString TOOLTIP = "Monitor the power this building is generating";

				// Token: 0x0400A519 RID: 42265
				public static LocString CIRCUITOVERVIEW = "CIRCUIT OVERVIEW";

				// Token: 0x0400A51A RID: 42266
				public static LocString GENERATORS = "POWER GENERATORS";

				// Token: 0x0400A51B RID: 42267
				public static LocString CONSUMERS = "POWER CONSUMERS";

				// Token: 0x0400A51C RID: 42268
				public static LocString BATTERIES = "BATTERIES";

				// Token: 0x0400A51D RID: 42269
				public static LocString DISCONNECTED = "Not connected to an electrical circuit";

				// Token: 0x0400A51E RID: 42270
				public static LocString NOGENERATORS = "No generators on this circuit";

				// Token: 0x0400A51F RID: 42271
				public static LocString NOCONSUMERS = "No consumers on this circuit";

				// Token: 0x0400A520 RID: 42272
				public static LocString NOBATTERIES = "No batteries on this circuit";

				// Token: 0x0400A521 RID: 42273
				public static LocString AVAILABLE_JOULES = UI.FormatAsLink("Power", "POWER") + " stored: {0}";

				// Token: 0x0400A522 RID: 42274
				public static LocString AVAILABLE_JOULES_TOOLTIP = "Amount of power stored in batteries";

				// Token: 0x0400A523 RID: 42275
				public static LocString WATTAGE_GENERATED = UI.FormatAsLink("Power", "POWER") + " produced: {0}";

				// Token: 0x0400A524 RID: 42276
				public static LocString WATTAGE_GENERATED_TOOLTIP = "The total amount of power generated by this circuit";

				// Token: 0x0400A525 RID: 42277
				public static LocString WATTAGE_CONSUMED = UI.FormatAsLink("Power", "POWER") + " consumed: {0}";

				// Token: 0x0400A526 RID: 42278
				public static LocString WATTAGE_CONSUMED_TOOLTIP = "The total amount of power used by this circuit";

				// Token: 0x0400A527 RID: 42279
				public static LocString POTENTIAL_WATTAGE_CONSUMED = "Potential power consumed: {0}";

				// Token: 0x0400A528 RID: 42280
				public static LocString POTENTIAL_WATTAGE_CONSUMED_TOOLTIP = "The total amount of power that can be used by this circuit if all connected buildings are active";

				// Token: 0x0400A529 RID: 42281
				public static LocString MAX_SAFE_WATTAGE = "Maximum safe wattage: {0}";

				// Token: 0x0400A52A RID: 42282
				public static LocString MAX_SAFE_WATTAGE_TOOLTIP = "Exceeding this value will overload the circuit and can result in damage to wiring and buildings";
			}

			// Token: 0x0200259E RID: 9630
			public class DISEASE
			{
				// Token: 0x0400A52B RID: 42283
				public static LocString NAME = "Germs";

				// Token: 0x0400A52C RID: 42284
				public static LocString TOOLTIP = "View the disease risk presented by the selected object";

				// Token: 0x0400A52D RID: 42285
				public static LocString DISEASE_SOURCE = "DISEASE SOURCE";

				// Token: 0x0400A52E RID: 42286
				public static LocString IMMUNE_SYSTEM = "GERM HOST";

				// Token: 0x0400A52F RID: 42287
				public static LocString CONTRACTION_RATES = "CONTRACTION RATES";

				// Token: 0x0400A530 RID: 42288
				public static LocString CURRENT_GERMS = "SURFACE GERMS";

				// Token: 0x0400A531 RID: 42289
				public static LocString NO_CURRENT_GERMS = "SURFACE GERMS";

				// Token: 0x0400A532 RID: 42290
				public static LocString GERMS_INFO = "GERM LIFE CYCLE";

				// Token: 0x0400A533 RID: 42291
				public static LocString INFECTION_INFO = "INFECTION DETAILS";

				// Token: 0x0400A534 RID: 42292
				public static LocString DISEASE_INFO_POPUP_HEADER = "DISEASE INFO: {0}";

				// Token: 0x0400A535 RID: 42293
				public static LocString DISEASE_INFO_POPUP_BUTTON = "FULL INFO";

				// Token: 0x0400A536 RID: 42294
				public static LocString DISEASE_INFO_POPUP_TOOLTIP = "View detailed germ and infection info for {0}";

				// Token: 0x02002F45 RID: 12101
				public class DETAILS
				{
					// Token: 0x0400BDDE RID: 48606
					public static LocString NODISEASE = "No surface germs";

					// Token: 0x0400BDDF RID: 48607
					public static LocString NODISEASE_TOOLTIP = "There are no germs present on this object";

					// Token: 0x0400BDE0 RID: 48608
					public static LocString DISEASE_AMOUNT = "{0}: {1}";

					// Token: 0x0400BDE1 RID: 48609
					public static LocString DISEASE_AMOUNT_TOOLTIP = "{0} are present on the surface of the selected object";

					// Token: 0x0400BDE2 RID: 48610
					public static LocString DEATH_FORMAT = "{0} dead/cycle";

					// Token: 0x0400BDE3 RID: 48611
					public static LocString DEATH_FORMAT_TOOLTIP = "Germ count is being reduced by {0}/cycle";

					// Token: 0x0400BDE4 RID: 48612
					public static LocString GROWTH_FORMAT = "{0} spawned/cycle";

					// Token: 0x0400BDE5 RID: 48613
					public static LocString GROWTH_FORMAT_TOOLTIP = "Germ count is being increased by {0}/cycle";

					// Token: 0x0400BDE6 RID: 48614
					public static LocString NEUTRAL_FORMAT = "No change";

					// Token: 0x0400BDE7 RID: 48615
					public static LocString NEUTRAL_FORMAT_TOOLTIP = "Germ count is static";

					// Token: 0x0200310F RID: 12559
					public class GROWTH_FACTORS
					{
						// Token: 0x0400C2C1 RID: 49857
						public static LocString TITLE = "\nGrowth factors:";

						// Token: 0x0400C2C2 RID: 49858
						public static LocString TOOLTIP = "These conditions are contributing to the multiplication of germs";

						// Token: 0x0400C2C3 RID: 49859
						public static LocString RATE_OF_CHANGE = "Change rate: {0}";

						// Token: 0x0400C2C4 RID: 49860
						public static LocString RATE_OF_CHANGE_TOOLTIP = "Germ count is fluctuating at a rate of {0}";

						// Token: 0x0400C2C5 RID: 49861
						public static LocString HALF_LIFE_NEG = "Half life: {0}";

						// Token: 0x0400C2C6 RID: 49862
						public static LocString HALF_LIFE_NEG_TOOLTIP = "In {0} the germ count on this object will be halved";

						// Token: 0x0400C2C7 RID: 49863
						public static LocString HALF_LIFE_POS = "Doubling time: {0}";

						// Token: 0x0400C2C8 RID: 49864
						public static LocString HALF_LIFE_POS_TOOLTIP = "In {0} the germ count on this object will be doubled";

						// Token: 0x0400C2C9 RID: 49865
						public static LocString HALF_LIFE_NEUTRAL = "Static";

						// Token: 0x0400C2CA RID: 49866
						public static LocString HALF_LIFE_NEUTRAL_TOOLTIP = "The germ count is neither increasing nor decreasing";

						// Token: 0x0200313A RID: 12602
						public class SUBSTRATE
						{
							// Token: 0x0400C303 RID: 49923
							public static LocString GROW = "    • Growing on {0}: {1}";

							// Token: 0x0400C304 RID: 49924
							public static LocString GROW_TOOLTIP = "Contact with this substance is causing germs to multiply";

							// Token: 0x0400C305 RID: 49925
							public static LocString NEUTRAL = "    • No change on {0}";

							// Token: 0x0400C306 RID: 49926
							public static LocString NEUTRAL_TOOLTIP = "Contact with this substance has no effect on germ count";

							// Token: 0x0400C307 RID: 49927
							public static LocString DIE = "    • Dying on {0}: {1}";

							// Token: 0x0400C308 RID: 49928
							public static LocString DIE_TOOLTIP = "Contact with this substance is causing germs to die off";
						}

						// Token: 0x0200313B RID: 12603
						public class ENVIRONMENT
						{
							// Token: 0x0400C309 RID: 49929
							public static LocString TITLE = "    • Surrounded by {0}: {1}";

							// Token: 0x0400C30A RID: 49930
							public static LocString GROW_TOOLTIP = "This atmosphere is causing germs to multiply";

							// Token: 0x0400C30B RID: 49931
							public static LocString DIE_TOOLTIP = "This atmosphere is causing germs to die off";
						}

						// Token: 0x0200313C RID: 12604
						public class TEMPERATURE
						{
							// Token: 0x0400C30C RID: 49932
							public static LocString TITLE = "    • Current temperature {0}: {1}";

							// Token: 0x0400C30D RID: 49933
							public static LocString GROW_TOOLTIP = "This temperature is allowing germs to multiply";

							// Token: 0x0400C30E RID: 49934
							public static LocString DIE_TOOLTIP = "This temperature is causing germs to die off";
						}

						// Token: 0x0200313D RID: 12605
						public class PRESSURE
						{
							// Token: 0x0400C30F RID: 49935
							public static LocString TITLE = "    • Current pressure {0}: {1}";

							// Token: 0x0400C310 RID: 49936
							public static LocString GROW_TOOLTIP = "Atmospheric pressure is causing germs to multiply";

							// Token: 0x0400C311 RID: 49937
							public static LocString DIE_TOOLTIP = "Atmospheric pressure is causing germs to die off";
						}

						// Token: 0x0200313E RID: 12606
						public class RADIATION
						{
							// Token: 0x0400C312 RID: 49938
							public static LocString TITLE = "    • Exposed to {0} Rads: {1}";

							// Token: 0x0400C313 RID: 49939
							public static LocString DIE_TOOLTIP = "Radiation exposure is causing germs to die off";
						}

						// Token: 0x0200313F RID: 12607
						public class DYING_OFF
						{
							// Token: 0x0400C314 RID: 49940
							public static LocString TITLE = "    • <b>Dying off: {0}</b>";

							// Token: 0x0400C315 RID: 49941
							public static LocString TOOLTIP = "Low germ count in this area is causing germs to die rapidly\n\nFewer than {0} are on this {1} of material.\n({2} germs/" + UI.UNITSUFFIXES.MASS.KILOGRAM + ")";
						}

						// Token: 0x02003140 RID: 12608
						public class OVERPOPULATED
						{
							// Token: 0x0400C316 RID: 49942
							public static LocString TITLE = "    • <b>Overpopulated: {0}</b>";

							// Token: 0x0400C317 RID: 49943
							public static LocString TOOLTIP = "Too many germs are present in this area, resulting in rapid die-off until the population stabilizes\n\nA maximum of {0} can be on this {1} of material.\n({2} germs/" + UI.UNITSUFFIXES.MASS.KILOGRAM + ")";
						}
					}
				}
			}

			// Token: 0x0200259F RID: 9631
			public class NEEDS
			{
				// Token: 0x0400A537 RID: 42295
				public static LocString NAME = "Stress";

				// Token: 0x0400A538 RID: 42296
				public static LocString TOOLTIP = "View this Duplicant's psychological status";

				// Token: 0x0400A539 RID: 42297
				public static LocString CURRENT_STRESS_LEVEL = "Current " + UI.FormatAsLink("Stress", "STRESS") + " Level: {0}";

				// Token: 0x0400A53A RID: 42298
				public static LocString OVERVIEW = "Overview";

				// Token: 0x0400A53B RID: 42299
				public static LocString STRESS_CREATORS = UI.FormatAsLink("Stress", "STRESS") + " Creators";

				// Token: 0x0400A53C RID: 42300
				public static LocString STRESS_RELIEVERS = UI.FormatAsLink("Stress", "STRESS") + " Relievers";

				// Token: 0x0400A53D RID: 42301
				public static LocString CURRENT_NEED_LEVEL = "Current Level: {0}";

				// Token: 0x0400A53E RID: 42302
				public static LocString NEXT_NEED_LEVEL = "Next Level: {0}";
			}

			// Token: 0x020025A0 RID: 9632
			public class EGG_CHANCES
			{
				// Token: 0x0400A53F RID: 42303
				public static LocString CHANCE_FORMAT = "{0}: {1}";

				// Token: 0x0400A540 RID: 42304
				public static LocString CHANCE_FORMAT_TOOLTIP = "This critter has a {1} chance of laying {0}s.\n\nThis probability increases when the creature:\n{2}";

				// Token: 0x0400A541 RID: 42305
				public static LocString CHANCE_MOD_FORMAT = "    • {0}\n";

				// Token: 0x0400A542 RID: 42306
				public static LocString CHANCE_FORMAT_TOOLTIP_NOMOD = "This critter has a {1} chance of laying {0}s.";
			}

			// Token: 0x020025A1 RID: 9633
			public class BUILDING_CHORES
			{
				// Token: 0x0400A543 RID: 42307
				public static LocString NAME = "Errands";

				// Token: 0x0400A544 RID: 42308
				public static LocString TOOLTIP = "See what errands this building can perform and view its current queue";

				// Token: 0x0400A545 RID: 42309
				public static LocString CHORE_TYPE_TOOLTIP = "Errand Type: {0}";

				// Token: 0x0400A546 RID: 42310
				public static LocString AVAILABLE_CHORES = "AVAILABLE ERRANDS";

				// Token: 0x0400A547 RID: 42311
				public static LocString DUPE_TOOLTIP_FAILED = "{Name} cannot currently {Errand}\n\nReason:\n{FailedPrecondition}";

				// Token: 0x0400A548 RID: 42312
				public static LocString DUPE_TOOLTIP_SUCCEEDED = "{Description}\n\n{Errand}'s Type: {Groups}\n\n{Name}'s {BestGroup} Priority: {PersonalPriorityValue} ({PersonalPriority})\n{Building} Priority: {BuildingPriority}\nAll {BestGroup} Errands: {TypePriority}\n\nTotal Priority: {TotalPriority}";

				// Token: 0x0400A549 RID: 42313
				public static LocString DUPE_TOOLTIP_DESC_ACTIVE = "{Name} is currently busy: \"{Errand}\"";

				// Token: 0x0400A54A RID: 42314
				public static LocString DUPE_TOOLTIP_DESC_INACTIVE = "\"{Errand}\" is #{Rank} on {Name}'s To Do list, after they finish their current errand";
			}

			// Token: 0x020025A2 RID: 9634
			public class PROCESS_CONDITIONS
			{
				// Token: 0x0400A54B RID: 42315
				public static LocString NAME = "LAUNCH CHECKLIST";

				// Token: 0x0400A54C RID: 42316
				public static LocString ROCKETPREP = "Rocket Construction";

				// Token: 0x0400A54D RID: 42317
				public static LocString ROCKETPREP_TOOLTIP = "It is recommended that all boxes on the Rocket Construction checklist be ticked before launching";

				// Token: 0x0400A54E RID: 42318
				public static LocString ROCKETSTORAGE = "Cargo Manifest";

				// Token: 0x0400A54F RID: 42319
				public static LocString ROCKETSTORAGE_TOOLTIP = "It is recommended that all boxes on the Cargo Manifest checklist be ticked before launching";

				// Token: 0x0400A550 RID: 42320
				public static LocString ROCKETFLIGHT = "Flight Route";

				// Token: 0x0400A551 RID: 42321
				public static LocString ROCKETFLIGHT_TOOLTIP = "A rocket requires a clear path to a set destination to conduct a mission";

				// Token: 0x0400A552 RID: 42322
				public static LocString ROCKETBOARD = "Crew Manifest";

				// Token: 0x0400A553 RID: 42323
				public static LocString ROCKETBOARD_TOOLTIP = "It is recommended that all boxes on the Crew Manifest checklist be ticked before launching";

				// Token: 0x0400A554 RID: 42324
				public static LocString ALL = "Requirements";

				// Token: 0x0400A555 RID: 42325
				public static LocString ALL_TOOLTIP = "These conditions must be fulfilled in order to launch a rocket mission";
			}
		}

		// Token: 0x02001CB5 RID: 7349
		public class BUILDMENU
		{
			// Token: 0x0400827A RID: 33402
			public static LocString GRID_VIEW_TOGGLE_TOOLTIP = "Toggle Grid View";

			// Token: 0x0400827B RID: 33403
			public static LocString LIST_VIEW_TOGGLE_TOOLTIP = "Toggle List View";

			// Token: 0x0400827C RID: 33404
			public static LocString NO_SEARCH_RESULTS = "NO RESULTS FOUND";

			// Token: 0x0400827D RID: 33405
			public static LocString SEARCH_RESULTS_HEADER = "SEARCH RESULTS";

			// Token: 0x0400827E RID: 33406
			public static LocString SEARCH_TEXT_PLACEHOLDER = "Search all buildings...";

			// Token: 0x0400827F RID: 33407
			public static LocString CLEAR_SEARCH_TOOLTIP = "Clear search";
		}

		// Token: 0x02001CB6 RID: 7350
		public class BUILDINGEFFECTS
		{
			// Token: 0x04008280 RID: 33408
			public static LocString OPERATIONREQUIREMENTS = "<b>Requirements:</b>";

			// Token: 0x04008281 RID: 33409
			public static LocString REQUIRESPOWER = UI.FormatAsLink("Power", "POWER") + ": {0}";

			// Token: 0x04008282 RID: 33410
			public static LocString REQUIRESELEMENT = "Supply of {0}";

			// Token: 0x04008283 RID: 33411
			public static LocString REQUIRESLIQUIDINPUT = UI.FormatAsLink("Liquid Intake Pipe", "LIQUIDPIPING");

			// Token: 0x04008284 RID: 33412
			public static LocString REQUIRESLIQUIDOUTPUT = UI.FormatAsLink("Liquid Output Pipe", "LIQUIDPIPING");

			// Token: 0x04008285 RID: 33413
			public static LocString REQUIRESLIQUIDOUTPUTS = "Two " + UI.FormatAsLink("Liquid Output Pipes", "LIQUIDPIPING");

			// Token: 0x04008286 RID: 33414
			public static LocString REQUIRESGASINPUT = UI.FormatAsLink("Gas Intake Pipe", "GASPIPING");

			// Token: 0x04008287 RID: 33415
			public static LocString REQUIRESGASOUTPUT = UI.FormatAsLink("Gas Output Pipe", "GASPIPING");

			// Token: 0x04008288 RID: 33416
			public static LocString REQUIRESGASOUTPUTS = "Two " + UI.FormatAsLink("Gas Output Pipes", "GASPIPING");

			// Token: 0x04008289 RID: 33417
			public static LocString REQUIRESMANUALOPERATION = "Duplicant operation";

			// Token: 0x0400828A RID: 33418
			public static LocString REQUIRESCREATIVITY = "Duplicant " + UI.FormatAsLink("Creativity", "ARTIST");

			// Token: 0x0400828B RID: 33419
			public static LocString REQUIRESPOWERGENERATOR = UI.FormatAsLink("Power", "POWER") + " generator";

			// Token: 0x0400828C RID: 33420
			public static LocString REQUIRESSEED = "1 Unplanted " + UI.FormatAsLink("Seed", "PLANTS");

			// Token: 0x0400828D RID: 33421
			public static LocString PREFERS_ROOM = "Preferred Room: {0}";

			// Token: 0x0400828E RID: 33422
			public static LocString REQUIRESROOM = "Dedicated Room: {0}";

			// Token: 0x0400828F RID: 33423
			public static LocString ALLOWS_FERTILIZER = "Plant " + UI.FormatAsLink("Fertilization", "WILTCONDITIONS");

			// Token: 0x04008290 RID: 33424
			public static LocString ALLOWS_IRRIGATION = "Plant " + UI.FormatAsLink("Liquid", "WILTCONDITIONS");

			// Token: 0x04008291 RID: 33425
			public static LocString ASSIGNEDDUPLICANT = "Duplicant assignment";

			// Token: 0x04008292 RID: 33426
			public static LocString CONSUMESANYELEMENT = "Any Element";

			// Token: 0x04008293 RID: 33427
			public static LocString ENABLESDOMESTICGROWTH = "Enables " + UI.FormatAsLink("Plant Domestication", "PLANTS");

			// Token: 0x04008294 RID: 33428
			public static LocString TRANSFORMER_INPUT_WIRE = "Input " + UI.FormatAsLink("Power Wire", "WIRE");

			// Token: 0x04008295 RID: 33429
			public static LocString TRANSFORMER_OUTPUT_WIRE = "Output " + UI.FormatAsLink("Power Wire", "WIRE") + " (Limited to {0})";

			// Token: 0x04008296 RID: 33430
			public static LocString OPERATIONEFFECTS = "<b>Effects:</b>";

			// Token: 0x04008297 RID: 33431
			public static LocString BATTERYCAPACITY = UI.FormatAsLink("Power", "POWER") + " capacity: {0}";

			// Token: 0x04008298 RID: 33432
			public static LocString BATTERYLEAK = UI.FormatAsLink("Power", "POWER") + " leak: {0}";

			// Token: 0x04008299 RID: 33433
			public static LocString STORAGECAPACITY = "Storage capacity: {0}";

			// Token: 0x0400829A RID: 33434
			public static LocString ELEMENTEMITTED_INPUTTEMP = "{0}: {1}";

			// Token: 0x0400829B RID: 33435
			public static LocString ELEMENTEMITTED_ENTITYTEMP = "{0}: {1}";

			// Token: 0x0400829C RID: 33436
			public static LocString ELEMENTEMITTED_MINORENTITYTEMP = "{0}: {1}";

			// Token: 0x0400829D RID: 33437
			public static LocString ELEMENTEMITTED_MINTEMP = "{0}: {1}";

			// Token: 0x0400829E RID: 33438
			public static LocString ELEMENTEMITTED_FIXEDTEMP = "{0}: {1}";

			// Token: 0x0400829F RID: 33439
			public static LocString ELEMENTCONSUMED = "{0}: {1}";

			// Token: 0x040082A0 RID: 33440
			public static LocString ELEMENTEMITTED_TOILET = "{0}: {1} per use";

			// Token: 0x040082A1 RID: 33441
			public static LocString ELEMENTEMITTEDPERUSE = "{0}: {1} per use";

			// Token: 0x040082A2 RID: 33442
			public static LocString DISEASEEMITTEDPERUSE = "{0}: {1} per use";

			// Token: 0x040082A3 RID: 33443
			public static LocString DISEASECONSUMEDPERUSE = "All Diseases: -{0} per use";

			// Token: 0x040082A4 RID: 33444
			public static LocString ELEMENTCONSUMEDPERUSE = "{0}: {1} per use";

			// Token: 0x040082A5 RID: 33445
			public static LocString ENERGYCONSUMED = UI.FormatAsLink("Power", "POWER") + " consumed: {0}";

			// Token: 0x040082A6 RID: 33446
			public static LocString ENERGYGENERATED = UI.FormatAsLink("Power", "POWER") + ": +{0}";

			// Token: 0x040082A7 RID: 33447
			public static LocString HEATGENERATED = UI.FormatAsLink("Heat", "HEAT") + ": +{0}/s";

			// Token: 0x040082A8 RID: 33448
			public static LocString HEATCONSUMED = UI.FormatAsLink("Heat", "HEAT") + ": -{0}/s";

			// Token: 0x040082A9 RID: 33449
			public static LocString HEATER_TARGETTEMPERATURE = "Target " + UI.FormatAsLink("Temperature", "HEAT") + ": {0}";

			// Token: 0x040082AA RID: 33450
			public static LocString HEATGENERATED_AIRCONDITIONER = UI.FormatAsLink("Heat", "HEAT") + ": +{0} (Approximate Value)";

			// Token: 0x040082AB RID: 33451
			public static LocString HEATGENERATED_LIQUIDCONDITIONER = UI.FormatAsLink("Heat", "HEAT") + ": +{0} (Approximate Value)";

			// Token: 0x040082AC RID: 33452
			public static LocString FABRICATES = "Fabricates";

			// Token: 0x040082AD RID: 33453
			public static LocString FABRICATEDITEM = "{1}";

			// Token: 0x040082AE RID: 33454
			public static LocString PROCESSES = "Refines:";

			// Token: 0x040082AF RID: 33455
			public static LocString PROCESSEDITEM = "{1} {0}";

			// Token: 0x040082B0 RID: 33456
			public static LocString PLANTERBOX_PENTALTY = "Planter box penalty";

			// Token: 0x040082B1 RID: 33457
			public static LocString DECORPROVIDED = UI.FormatAsLink("Decor", "DECOR") + ": {1} (Radius: {2} tiles)";

			// Token: 0x040082B2 RID: 33458
			public static LocString OVERHEAT_TEMP = "Overheat " + UI.FormatAsLink("Temperature", "HEAT") + ": {0}";

			// Token: 0x040082B3 RID: 33459
			public static LocString MINIMUM_TEMP = "Freeze " + UI.FormatAsLink("Temperature", "HEAT") + ": {0}";

			// Token: 0x040082B4 RID: 33460
			public static LocString OVER_PRESSURE_MASS = "Overpressure: {0}";

			// Token: 0x040082B5 RID: 33461
			public static LocString REFILLOXYGENTANK = "Refills Exosuit " + STRINGS.EQUIPMENT.PREFABS.OXYGEN_TANK.NAME;

			// Token: 0x040082B6 RID: 33462
			public static LocString DUPLICANTMOVEMENTBOOST = "Runspeed: {0}";

			// Token: 0x040082B7 RID: 33463
			public static LocString STRESSREDUCEDPERMINUTE = UI.FormatAsLink("Stress", "STRESS") + ": {0} per minute";

			// Token: 0x040082B8 RID: 33464
			public static LocString REMOVESEFFECTSUBTITLE = "Cures";

			// Token: 0x040082B9 RID: 33465
			public static LocString REMOVEDEFFECT = "{0}";

			// Token: 0x040082BA RID: 33466
			public static LocString ADDED_EFFECT = "Added Effect: {0}";

			// Token: 0x040082BB RID: 33467
			public static LocString GASCOOLING = UI.FormatAsLink("Cooling factor", "HEAT") + ": {0}";

			// Token: 0x040082BC RID: 33468
			public static LocString LIQUIDCOOLING = UI.FormatAsLink("Cooling factor", "HEAT") + ": {0}";

			// Token: 0x040082BD RID: 33469
			public static LocString MAX_WATTAGE = "Max " + UI.FormatAsLink("Power", "POWER") + ": {0}";

			// Token: 0x040082BE RID: 33470
			public static LocString MAX_BITS = UI.FormatAsLink("Bit", "LOGIC") + " Depth: {0}";

			// Token: 0x040082BF RID: 33471
			public static LocString RESEARCH_MATERIALS = "{0}: {1} per " + UI.FormatAsLink("Research", "RESEARCH") + " point";

			// Token: 0x040082C0 RID: 33472
			public static LocString PRODUCES_RESEARCH_POINTS = "{0}";

			// Token: 0x040082C1 RID: 33473
			public static LocString HIT_POINTS_PER_CYCLE = UI.FormatAsLink("Health", "Health") + " per cycle: {0}";

			// Token: 0x040082C2 RID: 33474
			public static LocString KCAL_PER_CYCLE = UI.FormatAsLink("KCal", "FOOD") + " per cycle: {0}";

			// Token: 0x040082C3 RID: 33475
			public static LocString REMOVES_DISEASE = "Kills germs";

			// Token: 0x040082C4 RID: 33476
			public static LocString DOCTORING = "Doctoring";

			// Token: 0x040082C5 RID: 33477
			public static LocString RECREATION = "Recreation";

			// Token: 0x040082C6 RID: 33478
			public static LocString COOLANT = "Coolant: {1} {0}";

			// Token: 0x040082C7 RID: 33479
			public static LocString REFINEMENT_ENERGY = "Heat: {0}";

			// Token: 0x040082C8 RID: 33480
			public static LocString IMPROVED_BUILDINGS = "Improved Buildings";

			// Token: 0x040082C9 RID: 33481
			public static LocString IMPROVED_BUILDINGS_ITEM = "{0}";

			// Token: 0x040082CA RID: 33482
			public static LocString GEYSER_PRODUCTION = "{0}: {1} at {2}";

			// Token: 0x040082CB RID: 33483
			public static LocString GEYSER_DISEASE = "Germs: {0}";

			// Token: 0x040082CC RID: 33484
			public static LocString GEYSER_PERIOD = "Eruption Period: {0} every {1}";

			// Token: 0x040082CD RID: 33485
			public static LocString GEYSER_YEAR_UNSTUDIED = "Active Period: (Requires Analysis)";

			// Token: 0x040082CE RID: 33486
			public static LocString GEYSER_YEAR_PERIOD = "Active Period: {0} every {1}";

			// Token: 0x040082CF RID: 33487
			public static LocString GEYSER_YEAR_NEXT_ACTIVE = "Next Activity: {0}";

			// Token: 0x040082D0 RID: 33488
			public static LocString GEYSER_YEAR_NEXT_DORMANT = "Next Dormancy: {0}";

			// Token: 0x040082D1 RID: 33489
			public static LocString GEYSER_YEAR_AVR_OUTPUT_UNSTUDIED = "Average Output: (Requires Analysis)";

			// Token: 0x040082D2 RID: 33490
			public static LocString GEYSER_YEAR_AVR_OUTPUT = "Average Output: {0}";

			// Token: 0x040082D3 RID: 33491
			public static LocString CAPTURE_METHOD_WRANGLE = "Capture Method: Wrangling";

			// Token: 0x040082D4 RID: 33492
			public static LocString CAPTURE_METHOD_LURE = "Capture Method: Lures";

			// Token: 0x040082D5 RID: 33493
			public static LocString CAPTURE_METHOD_TRAP = "Capture Method: Traps";

			// Token: 0x040082D6 RID: 33494
			public static LocString DIET_HEADER = "Digestion:";

			// Token: 0x040082D7 RID: 33495
			public static LocString DIET_CONSUMED = "    • Diet: {Foodlist}";

			// Token: 0x040082D8 RID: 33496
			public static LocString DIET_STORED = "    • Stores: {Foodlist}";

			// Token: 0x040082D9 RID: 33497
			public static LocString DIET_CONSUMED_ITEM = "{Food}: {Amount}";

			// Token: 0x040082DA RID: 33498
			public static LocString DIET_PRODUCED = "    • Excretion: {Items}";

			// Token: 0x040082DB RID: 33499
			public static LocString DIET_PRODUCED_ITEM = "{Item}: {Percent} of consumed mass";

			// Token: 0x040082DC RID: 33500
			public static LocString DIET_PRODUCED_ITEM_FROM_PLANT = "{Item}: {Amount} when properly fed";

			// Token: 0x040082DD RID: 33501
			public static LocString SCALE_GROWTH = "Shearable {Item}: {Amount} per {Time}";

			// Token: 0x040082DE RID: 33502
			public static LocString SCALE_GROWTH_ATMO = "Shearable {Item}: {Amount} per {Time} ({Atmosphere})";

			// Token: 0x040082DF RID: 33503
			public static LocString SCALE_GROWTH_TEMP = "Shearable {Item}: {Amount} per {Time} ({TempMin}-{TempMax})";

			// Token: 0x040082E0 RID: 33504
			public static LocString ACCESS_CONTROL = "Duplicant Access Permissions";

			// Token: 0x040082E1 RID: 33505
			public static LocString ROCKETRESTRICTION_HEADER = "Restriction Control:";

			// Token: 0x040082E2 RID: 33506
			public static LocString ROCKETRESTRICTION_BUILDINGS = "    • Buildings: {buildinglist}";

			// Token: 0x040082E3 RID: 33507
			public static LocString ITEM_TEMPERATURE_ADJUST = "Stored " + UI.FormatAsLink("Temperature", "HEAT") + ": {0}";

			// Token: 0x040082E4 RID: 33508
			public static LocString NOISE_CREATED = UI.FormatAsLink("Noise", "SOUND") + ": {0} dB (Radius: {1} tiles)";

			// Token: 0x040082E5 RID: 33509
			public static LocString MESS_TABLE_SALT = "Table Salt: +{0}";

			// Token: 0x040082E6 RID: 33510
			public static LocString ACTIVE_PARTICLE_CONSUMPTION = "Radbolts: {Rate}";

			// Token: 0x040082E7 RID: 33511
			public static LocString PARTICLE_PORT_INPUT = "Radbolt Input Port";

			// Token: 0x040082E8 RID: 33512
			public static LocString PARTICLE_PORT_OUTPUT = "Radbolt Output Port";

			// Token: 0x040082E9 RID: 33513
			public static LocString IN_ORBIT_REQUIRED = "Active In Space";

			// Token: 0x020025A3 RID: 9635
			public class TOOLTIPS
			{
				// Token: 0x0400A556 RID: 42326
				public static LocString OPERATIONREQUIREMENTS = "All requirements must be met in order for this building to operate";

				// Token: 0x0400A557 RID: 42327
				public static LocString REQUIRESPOWER = string.Concat(new string[]
				{
					"Must be connected to a power grid with at least ",
					UI.FormatAsNegativeRate("{0}"),
					" of available ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD
				});

				// Token: 0x0400A558 RID: 42328
				public static LocString REQUIRESELEMENT = string.Concat(new string[]
				{
					"Must receive deliveries of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" to function"
				});

				// Token: 0x0400A559 RID: 42329
				public static LocString REQUIRESLIQUIDINPUT = string.Concat(new string[]
				{
					"Must receive ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" from a ",
					BUILDINGS.PREFABS.LIQUIDCONDUIT.NAME,
					" system"
				});

				// Token: 0x0400A55A RID: 42330
				public static LocString REQUIRESLIQUIDOUTPUT = string.Concat(new string[]
				{
					"Must expel ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" through a ",
					BUILDINGS.PREFABS.LIQUIDCONDUIT.NAME,
					" system"
				});

				// Token: 0x0400A55B RID: 42331
				public static LocString REQUIRESLIQUIDOUTPUTS = string.Concat(new string[]
				{
					"Must expel ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" through a ",
					BUILDINGS.PREFABS.LIQUIDCONDUIT.NAME,
					" system"
				});

				// Token: 0x0400A55C RID: 42332
				public static LocString REQUIRESGASINPUT = string.Concat(new string[]
				{
					"Must receive ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" from a ",
					BUILDINGS.PREFABS.GASCONDUIT.NAME,
					" system"
				});

				// Token: 0x0400A55D RID: 42333
				public static LocString REQUIRESGASOUTPUT = string.Concat(new string[]
				{
					"Must expel ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" through a ",
					BUILDINGS.PREFABS.GASCONDUIT.NAME,
					" system"
				});

				// Token: 0x0400A55E RID: 42334
				public static LocString REQUIRESGASOUTPUTS = string.Concat(new string[]
				{
					"Must expel ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" through a ",
					BUILDINGS.PREFABS.GASCONDUIT.NAME,
					" system"
				});

				// Token: 0x0400A55F RID: 42335
				public static LocString REQUIRESMANUALOPERATION = "A Duplicant must be present to run this building";

				// Token: 0x0400A560 RID: 42336
				public static LocString REQUIRESCREATIVITY = "A Duplicant must work on this object to create " + UI.PRE_KEYWORD + "Art" + UI.PST_KEYWORD;

				// Token: 0x0400A561 RID: 42337
				public static LocString REQUIRESPOWERGENERATOR = string.Concat(new string[]
				{
					"Must be connected to a ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" producing generator to function"
				});

				// Token: 0x0400A562 RID: 42338
				public static LocString REQUIRESSEED = "Must receive a plant " + UI.PRE_KEYWORD + "Seed" + UI.PST_KEYWORD;

				// Token: 0x0400A563 RID: 42339
				public static LocString PREFERS_ROOM = "This building gains additional effects or functionality when built inside a " + UI.PRE_KEYWORD + "{0}" + UI.PST_KEYWORD;

				// Token: 0x0400A564 RID: 42340
				public static LocString REQUIRESROOM = string.Concat(new string[]
				{
					"Must be built within a dedicated ",
					UI.PRE_KEYWORD,
					"Room",
					UI.PST_KEYWORD,
					"\n\n",
					UI.PRE_KEYWORD,
					"Room",
					UI.PST_KEYWORD,
					" will become a ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" after construction"
				});

				// Token: 0x0400A565 RID: 42341
				public static LocString ALLOWS_FERTILIZER = string.Concat(new string[]
				{
					"Allows ",
					UI.PRE_KEYWORD,
					"Fertilizer",
					UI.PST_KEYWORD,
					" to be delivered to plants"
				});

				// Token: 0x0400A566 RID: 42342
				public static LocString ALLOWS_IRRIGATION = string.Concat(new string[]
				{
					"Allows ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" to be delivered to plants"
				});

				// Token: 0x0400A567 RID: 42343
				public static LocString ALLOWS_IRRIGATION_PIPE = string.Concat(new string[]
				{
					"Allows irrigation ",
					UI.PRE_KEYWORD,
					"Pipe",
					UI.PST_KEYWORD,
					" connection"
				});

				// Token: 0x0400A568 RID: 42344
				public static LocString ASSIGNEDDUPLICANT = "This amenity may only be used by the Duplicant it is assigned to";

				// Token: 0x0400A569 RID: 42345
				public static LocString OPERATIONEFFECTS = "The building will produce these effects when its requirements are met";

				// Token: 0x0400A56A RID: 42346
				public static LocString BATTERYCAPACITY = string.Concat(new string[]
				{
					"Can hold <b>{0}</b> of ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" when connected to a ",
					UI.PRE_KEYWORD,
					"Generator",
					UI.PST_KEYWORD
				});

				// Token: 0x0400A56B RID: 42347
				public static LocString BATTERYLEAK = string.Concat(new string[]
				{
					UI.FormatAsNegativeRate("{0}"),
					" of this battery's charge will be lost as ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD
				});

				// Token: 0x0400A56C RID: 42348
				public static LocString STORAGECAPACITY = "Holds up to <b>{0}</b> of material";

				// Token: 0x0400A56D RID: 42349
				public static LocString ELEMENTEMITTED_INPUTTEMP = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsPositiveRate("{1}"),
					" of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" when in use\n\nIt will be the combined ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" of the input materials."
				});

				// Token: 0x0400A56E RID: 42350
				public static LocString ELEMENTEMITTED_ENTITYTEMP = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsPositiveRate("{1}"),
					" of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" when in use\n\nIt will be the ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" of the building at the time of production"
				});

				// Token: 0x0400A56F RID: 42351
				public static LocString ELEMENTEMITTED_MINORENTITYTEMP = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsPositiveRate("{1}"),
					" of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" when in use\n\nIt will be at least <b>{2}</b>, or hotter if the building is hotter."
				});

				// Token: 0x0400A570 RID: 42352
				public static LocString ELEMENTEMITTED_MINTEMP = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsPositiveRate("{1}"),
					" of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" when in use\n\nIt will be at least <b>{2}</b>, or hotter if the input materials are hotter."
				});

				// Token: 0x0400A571 RID: 42353
				public static LocString ELEMENTEMITTED_FIXEDTEMP = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsPositiveRate("{1}"),
					" of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" when in use\n\nIt will be produced at <b>{2}</b>."
				});

				// Token: 0x0400A572 RID: 42354
				public static LocString ELEMENTCONSUMED = string.Concat(new string[]
				{
					"Consumes ",
					UI.FormatAsNegativeRate("{1}"),
					" of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" when in use"
				});

				// Token: 0x0400A573 RID: 42355
				public static LocString ELEMENTEMITTED_TOILET = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsPositiveRate("{1}"),
					" of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" per use\n\nDuplicant waste is emitted at <b>{2}</b>."
				});

				// Token: 0x0400A574 RID: 42356
				public static LocString ELEMENTEMITTEDPERUSE = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsPositiveRate("{1}"),
					" of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" per use\n\nIt will be the ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" of the input materials."
				});

				// Token: 0x0400A575 RID: 42357
				public static LocString DISEASEEMITTEDPERUSE = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsPositiveRate("{1}"),
					" of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" per use"
				});

				// Token: 0x0400A576 RID: 42358
				public static LocString DISEASECONSUMEDPERUSE = "Removes " + UI.FormatAsNegativeRate("{0}") + " per use";

				// Token: 0x0400A577 RID: 42359
				public static LocString ELEMENTCONSUMEDPERUSE = string.Concat(new string[]
				{
					"Consumes ",
					UI.FormatAsNegativeRate("{1}"),
					" of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" per use"
				});

				// Token: 0x0400A578 RID: 42360
				public static LocString ENERGYCONSUMED = string.Concat(new string[]
				{
					"Draws ",
					UI.FormatAsNegativeRate("{0}"),
					" from the ",
					UI.PRE_KEYWORD,
					"Power Grid",
					UI.PST_KEYWORD,
					" it's connected to"
				});

				// Token: 0x0400A579 RID: 42361
				public static LocString ENERGYGENERATED = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsPositiveRate("{0}"),
					" for the ",
					UI.PRE_KEYWORD,
					"Power Grid",
					UI.PST_KEYWORD,
					" it's connected to"
				});

				// Token: 0x0400A57A RID: 42362
				public static LocString ENABLESDOMESTICGROWTH = string.Concat(new string[]
				{
					"Accelerates ",
					UI.PRE_KEYWORD,
					"Plant",
					UI.PST_KEYWORD,
					" growth and maturation"
				});

				// Token: 0x0400A57B RID: 42363
				public static LocString HEATGENERATED = string.Concat(new string[]
				{
					"Generates ",
					UI.FormatAsPositiveRate("{0}"),
					" per second\n\nSum ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" change is affected by the material attributes of the heated substance:\n    • mass\n    • specific heat capacity\n    • surface area\n    • insulation thickness\n    • thermal conductivity"
				});

				// Token: 0x0400A57C RID: 42364
				public static LocString HEATCONSUMED = string.Concat(new string[]
				{
					"Dissipates ",
					UI.FormatAsNegativeRate("{0}"),
					" per second\n\nSum ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" change can be affected by the material attributes of the cooled substance:\n    • mass\n    • specific heat capacity\n    • surface area\n    • insulation thickness\n    • thermal conductivity"
				});

				// Token: 0x0400A57D RID: 42365
				public static LocString HEATER_TARGETTEMPERATURE = string.Concat(new string[]
				{
					"Stops heating when the surrounding average ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" is above <b>{0}</b>"
				});

				// Token: 0x0400A57E RID: 42366
				public static LocString FABRICATES = "Fabrication is the production of items and equipment";

				// Token: 0x0400A57F RID: 42367
				public static LocString PROCESSES = "Processes raw materials into refined materials";

				// Token: 0x0400A580 RID: 42368
				public static LocString PROCESSEDITEM = "Refining this material produces " + UI.PRE_KEYWORD + "{0}" + UI.PST_KEYWORD;

				// Token: 0x0400A581 RID: 42369
				public static LocString PLANTERBOX_PENTALTY = "Plants grow more slowly when contained within boxes";

				// Token: 0x0400A582 RID: 42370
				public static LocString DECORPROVIDED = string.Concat(new string[]
				{
					"Improves ",
					UI.PRE_KEYWORD,
					"Decor",
					UI.PST_KEYWORD,
					" values by ",
					UI.FormatAsPositiveModifier("<b>{0}</b>"),
					" in a <b>{1}</b> tile radius"
				});

				// Token: 0x0400A583 RID: 42371
				public static LocString DECORDECREASED = string.Concat(new string[]
				{
					"Decreases ",
					UI.PRE_KEYWORD,
					"Decor",
					UI.PST_KEYWORD,
					" values by ",
					UI.FormatAsNegativeModifier("<b>{0}</b>"),
					" in a <b>{1}</b> tile radius"
				});

				// Token: 0x0400A584 RID: 42372
				public static LocString OVERHEAT_TEMP = "Begins overheating at <b>{0}</b>";

				// Token: 0x0400A585 RID: 42373
				public static LocString MINIMUM_TEMP = "Ceases to function when temperatures fall below <b>{0}</b>";

				// Token: 0x0400A586 RID: 42374
				public static LocString OVER_PRESSURE_MASS = "Ceases to function when the surrounding mass is above <b>{0}</b>";

				// Token: 0x0400A587 RID: 42375
				public static LocString REFILLOXYGENTANK = string.Concat(new string[]
				{
					"Refills ",
					UI.PRE_KEYWORD,
					"Exosuit",
					UI.PST_KEYWORD,
					" Oxygen tanks with ",
					UI.PRE_KEYWORD,
					"Oxygen",
					UI.PST_KEYWORD,
					" for reuse"
				});

				// Token: 0x0400A588 RID: 42376
				public static LocString DUPLICANTMOVEMENTBOOST = "Duplicants walk <b>{0}</b> faster on this tile";

				// Token: 0x0400A589 RID: 42377
				public static LocString STRESSREDUCEDPERMINUTE = string.Concat(new string[]
				{
					"Removes <b>{0}</b> of Duplicants' ",
					UI.PRE_KEYWORD,
					"Stress",
					UI.PST_KEYWORD,
					" for every uninterrupted minute of use"
				});

				// Token: 0x0400A58A RID: 42378
				public static LocString REMOVESEFFECTSUBTITLE = "Use of this building will remove the listed effects";

				// Token: 0x0400A58B RID: 42379
				public static LocString REMOVEDEFFECT = "{0}";

				// Token: 0x0400A58C RID: 42380
				public static LocString ADDED_EFFECT = "Effect being applied:\n\n{0}\n{1}";

				// Token: 0x0400A58D RID: 42381
				public static LocString GASCOOLING = string.Concat(new string[]
				{
					"Reduces the ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" of piped ",
					UI.PRE_KEYWORD,
					"Gases",
					UI.PST_KEYWORD,
					" by <b>{0}</b>"
				});

				// Token: 0x0400A58E RID: 42382
				public static LocString LIQUIDCOOLING = string.Concat(new string[]
				{
					"Reduces the ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" of piped ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" by <b>{0}</b>"
				});

				// Token: 0x0400A58F RID: 42383
				public static LocString MAX_WATTAGE = string.Concat(new string[]
				{
					"Drawing more than the maximum allowed ",
					UI.PRE_KEYWORD,
					"Watts",
					UI.PST_KEYWORD,
					" can result in damage to the circuit"
				});

				// Token: 0x0400A590 RID: 42384
				public static LocString MAX_BITS = string.Concat(new string[]
				{
					"Sending an ",
					UI.PRE_KEYWORD,
					"Automation Signal",
					UI.PST_KEYWORD,
					" with a higher ",
					UI.PRE_KEYWORD,
					"Bit Depth",
					UI.PST_KEYWORD,
					" than the connected ",
					UI.PRE_KEYWORD,
					"Logic Wire",
					UI.PST_KEYWORD,
					" can result in damage to the circuit"
				});

				// Token: 0x0400A591 RID: 42385
				public static LocString RESEARCH_MATERIALS = string.Concat(new string[]
				{
					"This research station consumes ",
					UI.FormatAsNegativeRate("{1}"),
					" of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" for each ",
					UI.PRE_KEYWORD,
					"Research Point",
					UI.PST_KEYWORD,
					" produced"
				});

				// Token: 0x0400A592 RID: 42386
				public static LocString PRODUCES_RESEARCH_POINTS = string.Concat(new string[]
				{
					"Produces ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" research"
				});

				// Token: 0x0400A593 RID: 42387
				public static LocString REMOVES_DISEASE = string.Concat(new string[]
				{
					"The cooking process kills all ",
					UI.PRE_KEYWORD,
					"Germs",
					UI.PST_KEYWORD,
					" present in the ingredients, removing the ",
					UI.PRE_KEYWORD,
					"Disease",
					UI.PST_KEYWORD,
					" risk when eating the product"
				});

				// Token: 0x0400A594 RID: 42388
				public static LocString DOCTORING = "Doctoring increases existing health benefits and can allow the treatment of otherwise stubborn " + UI.PRE_KEYWORD + "Diseases" + UI.PST_KEYWORD;

				// Token: 0x0400A595 RID: 42389
				public static LocString RECREATION = string.Concat(new string[]
				{
					"Improves Duplicant ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" during scheduled ",
					UI.PRE_KEYWORD,
					"Downtime",
					UI.PST_KEYWORD
				});

				// Token: 0x0400A596 RID: 42390
				public static LocString HEATGENERATED_AIRCONDITIONER = string.Concat(new string[]
				{
					"Generates ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					" based on the ",
					UI.PRE_KEYWORD,
					"Volume",
					UI.PST_KEYWORD,
					" and ",
					UI.PRE_KEYWORD,
					"Specific Heat Capacity",
					UI.PST_KEYWORD,
					" of the pumped ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					"\n\nCooling 1",
					UI.UNITSUFFIXES.MASS.KILOGRAM,
					" of ",
					ELEMENTS.OXYGEN.NAME,
					" the entire <b>{1}</b> will output <b>{0}</b>"
				});

				// Token: 0x0400A597 RID: 42391
				public static LocString HEATGENERATED_LIQUIDCONDITIONER = string.Concat(new string[]
				{
					"Generates ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					" based on the ",
					UI.PRE_KEYWORD,
					"Volume",
					UI.PST_KEYWORD,
					" and ",
					UI.PRE_KEYWORD,
					"Specific Heat Capacity",
					UI.PST_KEYWORD,
					" of the pumped ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					"\n\nCooling 10",
					UI.UNITSUFFIXES.MASS.KILOGRAM,
					" of ",
					ELEMENTS.WATER.NAME,
					" the entire <b>{1}</b> will output <b>{0}</b>"
				});

				// Token: 0x0400A598 RID: 42392
				public static LocString MOVEMENT_BONUS = "Increases the Runspeed of Duplicants";

				// Token: 0x0400A599 RID: 42393
				public static LocString COOLANT = string.Concat(new string[]
				{
					"<b>{1}</b> of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" coolant is required to cool off an item produced by this building\n\nCoolant ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" increase is variable and dictated by the amount of energy needed to cool the produced item"
				});

				// Token: 0x0400A59A RID: 42394
				public static LocString REFINEMENT_ENERGY_HAS_COOLANT = string.Concat(new string[]
				{
					UI.FormatAsPositiveRate("{0}"),
					" of ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					" will be produced to cool off the fabricated item\n\nThis will raise the ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" of the contained ",
					UI.PRE_KEYWORD,
					"{1}",
					UI.PST_KEYWORD,
					" by ",
					UI.FormatAsPositiveModifier("{2}"),
					", and heat the containing building"
				});

				// Token: 0x0400A59B RID: 42395
				public static LocString REFINEMENT_ENERGY_NO_COOLANT = string.Concat(new string[]
				{
					UI.FormatAsPositiveRate("{0}"),
					" of ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					" will be produced to cool off the fabricated item\n\nIf ",
					UI.PRE_KEYWORD,
					"{1}",
					UI.PST_KEYWORD,
					" is used for coolant, its ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" will be raised by ",
					UI.FormatAsPositiveModifier("{2}"),
					", and will heat the containing building"
				});

				// Token: 0x0400A59C RID: 42396
				public static LocString IMPROVED_BUILDINGS = UI.PRE_KEYWORD + "Tune Ups" + UI.PST_KEYWORD + " will improve these buildings:";

				// Token: 0x0400A59D RID: 42397
				public static LocString IMPROVED_BUILDINGS_ITEM = "{0}";

				// Token: 0x0400A59E RID: 42398
				public static LocString GEYSER_PRODUCTION = string.Concat(new string[]
				{
					"While erupting, this geyser will produce ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" at a rate of ",
					UI.FormatAsPositiveRate("{1}"),
					", and at a ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" of <b>{2}</b>"
				});

				// Token: 0x0400A59F RID: 42399
				public static LocString GEYSER_PRODUCTION_GEOTUNED = string.Concat(new string[]
				{
					"While erupting, this geyser will produce ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" at a rate of ",
					UI.FormatAsPositiveRate("{1}"),
					", and at a ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" of <b>{2}</b>"
				});

				// Token: 0x0400A5A0 RID: 42400
				public static LocString GEYSER_PRODUCTION_GEOTUNED_COUNT = "<b>{0}</b> of <b>{1}</b> Geotuners targeting this geyser are amplifying it";

				// Token: 0x0400A5A1 RID: 42401
				public static LocString GEYSER_PRODUCTION_GEOTUNED_TOTAL = "Total geotuning: {0} {1}";

				// Token: 0x0400A5A2 RID: 42402
				public static LocString GEYSER_PRODUCTION_GEOTUNED_TOTAL_ROW_TITLE = "Geotuned ";

				// Token: 0x0400A5A3 RID: 42403
				public static LocString GEYSER_DISEASE = UI.PRE_KEYWORD + "{0}" + UI.PST_KEYWORD + " germs are present in the output of this geyser";

				// Token: 0x0400A5A4 RID: 42404
				public static LocString GEYSER_PERIOD = "This geyser will produce for <b>{0}</b> of every <b>{1}</b>";

				// Token: 0x0400A5A5 RID: 42405
				public static LocString GEYSER_YEAR_UNSTUDIED = "A researcher must analyze this geyser to determine its geoactive period";

				// Token: 0x0400A5A6 RID: 42406
				public static LocString GEYSER_YEAR_PERIOD = "This geyser will be active for <b>{0}</b> out of every <b>{1}</b>\n\nIt will be dormant the rest of the time";

				// Token: 0x0400A5A7 RID: 42407
				public static LocString GEYSER_YEAR_NEXT_ACTIVE = "This geyser will become active in <b>{0}</b>";

				// Token: 0x0400A5A8 RID: 42408
				public static LocString GEYSER_YEAR_NEXT_DORMANT = "This geyser will become dormant in <b>{0}</b>";

				// Token: 0x0400A5A9 RID: 42409
				public static LocString GEYSER_YEAR_AVR_OUTPUT_UNSTUDIED = "A researcher must analyze this geyser to determine its average output rate";

				// Token: 0x0400A5AA RID: 42410
				public static LocString GEYSER_YEAR_AVR_OUTPUT = "This geyser emits an average of {average} of {element} during its lifetime\n\nThis includes its dormant period";

				// Token: 0x0400A5AB RID: 42411
				public static LocString GEYSER_YEAR_AVR_OUTPUT_BREAKDOWN_TITLE = "Total Geotuning ";

				// Token: 0x0400A5AC RID: 42412
				public static LocString GEYSER_YEAR_AVR_OUTPUT_BREAKDOWN_ROW = "Geotuned ";

				// Token: 0x0400A5AD RID: 42413
				public static LocString CAPTURE_METHOD_WRANGLE = string.Concat(new string[]
				{
					"This critter can be captured\n\nMark critters for capture using the ",
					UI.FormatAsTool("Wrangle Tool", global::Action.Capture),
					"\n\nDuplicants must possess the ",
					UI.PRE_KEYWORD,
					"Critter Ranching",
					UI.PST_KEYWORD,
					" Skill in order to wrangle critters"
				});

				// Token: 0x0400A5AE RID: 42414
				public static LocString CAPTURE_METHOD_LURE = "This critter can be moved using an " + BUILDINGS.PREFABS.AIRBORNECREATURELURE.NAME;

				// Token: 0x0400A5AF RID: 42415
				public static LocString CAPTURE_METHOD_TRAP = "This critter can be captured using a " + BUILDINGS.PREFABS.CREATURETRAP.NAME;

				// Token: 0x0400A5B0 RID: 42416
				public static LocString NOISE_POLLUTION_INCREASE = "Produces noise at <b>{0} dB</b> in a <b>{1}</b> tile radius";

				// Token: 0x0400A5B1 RID: 42417
				public static LocString NOISE_POLLUTION_DECREASE = "Dampens noise at <b>{0} dB</b> in a <b>{1}</b> tile radius";

				// Token: 0x0400A5B2 RID: 42418
				public static LocString ITEM_TEMPERATURE_ADJUST = string.Concat(new string[]
				{
					"Stored items will reach a ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" of <b>{0}</b> over time"
				});

				// Token: 0x0400A5B3 RID: 42419
				public static LocString DIET_HEADER = "Creatures will eat and digest only specific materials";

				// Token: 0x0400A5B4 RID: 42420
				public static LocString DIET_CONSUMED = "This critter can typically consume these materials at the following rates:\n\n{Foodlist}";

				// Token: 0x0400A5B5 RID: 42421
				public static LocString DIET_PRODUCED = "This critter will \"produce\" the following materials:\n\n{Items}";

				// Token: 0x0400A5B6 RID: 42422
				public static LocString ROCKETRESTRICTION_HEADER = "Controls whether a building is operational within a rocket interior";

				// Token: 0x0400A5B7 RID: 42423
				public static LocString ROCKETRESTRICTION_BUILDINGS = "This station controls the operational status of the following buildings:\n\n{buildinglist}";

				// Token: 0x0400A5B8 RID: 42424
				public static LocString SCALE_GROWTH = string.Concat(new string[]
				{
					"This critter can be sheared every <b>{Time}</b> to produce ",
					UI.FormatAsPositiveModifier("{Amount}"),
					" of ",
					UI.PRE_KEYWORD,
					"{Item}",
					UI.PST_KEYWORD
				});

				// Token: 0x0400A5B9 RID: 42425
				public static LocString SCALE_GROWTH_ATMO = string.Concat(new string[]
				{
					"This critter can be sheared every <b>{Time}</b> to produce ",
					UI.FormatAsPositiveRate("{Amount}"),
					" of ",
					UI.PRE_KEYWORD,
					"{Item}",
					UI.PST_KEYWORD,
					"\n\nIt must be kept in ",
					UI.PRE_KEYWORD,
					"{Atmosphere}",
					UI.PST_KEYWORD,
					"-rich environments to regrow sheared ",
					UI.PRE_KEYWORD,
					"{Item}",
					UI.PST_KEYWORD
				});

				// Token: 0x0400A5BA RID: 42426
				public static LocString SCALE_GROWTH_TEMP = string.Concat(new string[]
				{
					"This critter can be sheared every <b>{Time}</b> to produce ",
					UI.FormatAsPositiveRate("{Amount}"),
					" of ",
					UI.PRE_KEYWORD,
					"{Item}",
					UI.PST_KEYWORD,
					"\n\nIt must eat food between {TempMin}-{TempMax} to regrow sheared ",
					UI.PRE_KEYWORD,
					"{Item}",
					UI.PST_KEYWORD
				});

				// Token: 0x0400A5BB RID: 42427
				public static LocString MESS_TABLE_SALT = string.Concat(new string[]
				{
					"Duplicants gain ",
					UI.FormatAsPositiveModifier("+{0}"),
					" ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" when using ",
					UI.PRE_KEYWORD,
					"Table Salt",
					UI.PST_KEYWORD,
					" with their food at a ",
					BUILDINGS.PREFABS.DININGTABLE.NAME
				});

				// Token: 0x0400A5BC RID: 42428
				public static LocString ACCESS_CONTROL = "Settings to allow or restrict Duplicants from passing through the door.";

				// Token: 0x0400A5BD RID: 42429
				public static LocString TRANSFORMER_INPUT_WIRE = string.Concat(new string[]
				{
					"Connect a ",
					UI.PRE_KEYWORD,
					"Wire",
					UI.PST_KEYWORD,
					" to the large ",
					UI.PRE_KEYWORD,
					"Input",
					UI.PST_KEYWORD,
					" with any amount of ",
					UI.PRE_KEYWORD,
					"Watts",
					UI.PST_KEYWORD,
					"."
				});

				// Token: 0x0400A5BE RID: 42430
				public static LocString TRANSFORMER_OUTPUT_WIRE = string.Concat(new string[]
				{
					"The ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" provided by the the small ",
					UI.PRE_KEYWORD,
					"Output",
					UI.PST_KEYWORD,
					" will be limited to {0}."
				});

				// Token: 0x0400A5BF RID: 42431
				public static LocString FABRICATOR_INGREDIENTS = "Ingredients:\n{0}";

				// Token: 0x0400A5C0 RID: 42432
				public static LocString ACTIVE_PARTICLE_CONSUMPTION = string.Concat(new string[]
				{
					"This building requires ",
					UI.PRE_KEYWORD,
					"Radbolts",
					UI.PST_KEYWORD,
					" to function, consuming them at a rate of {Rate} while in use"
				});

				// Token: 0x0400A5C1 RID: 42433
				public static LocString PARTICLE_PORT_INPUT = "A Radbolt Port on this building allows it to receive " + UI.PRE_KEYWORD + "Radbolts" + UI.PST_KEYWORD;

				// Token: 0x0400A5C2 RID: 42434
				public static LocString PARTICLE_PORT_OUTPUT = string.Concat(new string[]
				{
					"This building has a configurable Radbolt Port for ",
					UI.PRE_KEYWORD,
					"Radbolt",
					UI.PST_KEYWORD,
					" emission"
				});

				// Token: 0x0400A5C3 RID: 42435
				public static LocString IN_ORBIT_REQUIRED = "This building is only operational while its parent rocket is in flight";
			}
		}

		// Token: 0x02001CB7 RID: 7351
		public class LOGIC_PORTS
		{
			// Token: 0x040082EA RID: 33514
			public static LocString INPUT_PORTS = UI.FormatAsLink("Auto Inputs", "LOGIC");

			// Token: 0x040082EB RID: 33515
			public static LocString INPUT_PORTS_TOOLTIP = "Input ports change a state on this building when a signal is received";

			// Token: 0x040082EC RID: 33516
			public static LocString OUTPUT_PORTS = UI.FormatAsLink("Auto Outputs", "LOGIC");

			// Token: 0x040082ED RID: 33517
			public static LocString OUTPUT_PORTS_TOOLTIP = "Output ports send a signal when this building changes state";

			// Token: 0x040082EE RID: 33518
			public static LocString INPUT_PORT_TOOLTIP = "Input Behavior:\n• {0}\n• {1}";

			// Token: 0x040082EF RID: 33519
			public static LocString OUTPUT_PORT_TOOLTIP = "Output Behavior:\n• {0}\n• {1}";

			// Token: 0x040082F0 RID: 33520
			public static LocString CONTROL_OPERATIONAL = "Enable/Disable";

			// Token: 0x040082F1 RID: 33521
			public static LocString CONTROL_OPERATIONAL_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Enable building";

			// Token: 0x040082F2 RID: 33522
			public static LocString CONTROL_OPERATIONAL_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Disable building";

			// Token: 0x040082F3 RID: 33523
			public static LocString PORT_INPUT_DEFAULT_NAME = "INPUT";

			// Token: 0x040082F4 RID: 33524
			public static LocString PORT_OUTPUT_DEFAULT_NAME = "OUTPUT";

			// Token: 0x040082F5 RID: 33525
			public static LocString GATE_MULTI_INPUT_ONE_NAME = "INPUT A";

			// Token: 0x040082F6 RID: 33526
			public static LocString GATE_MULTI_INPUT_ONE_ACTIVE = "Green Signal";

			// Token: 0x040082F7 RID: 33527
			public static LocString GATE_MULTI_INPUT_ONE_INACTIVE = "Red Signal";

			// Token: 0x040082F8 RID: 33528
			public static LocString GATE_MULTI_INPUT_TWO_NAME = "INPUT B";

			// Token: 0x040082F9 RID: 33529
			public static LocString GATE_MULTI_INPUT_TWO_ACTIVE = "Green Signal";

			// Token: 0x040082FA RID: 33530
			public static LocString GATE_MULTI_INPUT_TWO_INACTIVE = "Red Signal";

			// Token: 0x040082FB RID: 33531
			public static LocString GATE_MULTI_INPUT_THREE_NAME = "INPUT C";

			// Token: 0x040082FC RID: 33532
			public static LocString GATE_MULTI_INPUT_THREE_ACTIVE = "Green Signal";

			// Token: 0x040082FD RID: 33533
			public static LocString GATE_MULTI_INPUT_THREE_INACTIVE = "Red Signal";

			// Token: 0x040082FE RID: 33534
			public static LocString GATE_MULTI_INPUT_FOUR_NAME = "INPUT D";

			// Token: 0x040082FF RID: 33535
			public static LocString GATE_MULTI_INPUT_FOUR_ACTIVE = "Green Signal";

			// Token: 0x04008300 RID: 33536
			public static LocString GATE_MULTI_INPUT_FOUR_INACTIVE = "Red Signal";

			// Token: 0x04008301 RID: 33537
			public static LocString GATE_SINGLE_INPUT_ONE_NAME = "INPUT";

			// Token: 0x04008302 RID: 33538
			public static LocString GATE_SINGLE_INPUT_ONE_ACTIVE = "Green Signal";

			// Token: 0x04008303 RID: 33539
			public static LocString GATE_SINGLE_INPUT_ONE_INACTIVE = "Red Signal";

			// Token: 0x04008304 RID: 33540
			public static LocString GATE_MULTI_OUTPUT_ONE_NAME = "OUTPUT A";

			// Token: 0x04008305 RID: 33541
			public static LocString GATE_MULTI_OUTPUT_ONE_ACTIVE = "Green Signal";

			// Token: 0x04008306 RID: 33542
			public static LocString GATE_MULTI_OUTPUT_ONE_INACTIVE = "Red Signal";

			// Token: 0x04008307 RID: 33543
			public static LocString GATE_MULTI_OUTPUT_TWO_NAME = "OUTPUT B";

			// Token: 0x04008308 RID: 33544
			public static LocString GATE_MULTI_OUTPUT_TWO_ACTIVE = "Green Signal";

			// Token: 0x04008309 RID: 33545
			public static LocString GATE_MULTI_OUTPUT_TWO_INACTIVE = "Red Signal";

			// Token: 0x0400830A RID: 33546
			public static LocString GATE_MULTI_OUTPUT_THREE_NAME = "OUTPUT C";

			// Token: 0x0400830B RID: 33547
			public static LocString GATE_MULTI_OUTPUT_THREE_ACTIVE = "Green Signal";

			// Token: 0x0400830C RID: 33548
			public static LocString GATE_MULTI_OUTPUT_THREE_INACTIVE = "Red Signal";

			// Token: 0x0400830D RID: 33549
			public static LocString GATE_MULTI_OUTPUT_FOUR_NAME = "OUTPUT D";

			// Token: 0x0400830E RID: 33550
			public static LocString GATE_MULTI_OUTPUT_FOUR_ACTIVE = "Green Signal";

			// Token: 0x0400830F RID: 33551
			public static LocString GATE_MULTI_OUTPUT_FOUR_INACTIVE = "Red Signal";

			// Token: 0x04008310 RID: 33552
			public static LocString GATE_SINGLE_OUTPUT_ONE_NAME = "OUTPUT";

			// Token: 0x04008311 RID: 33553
			public static LocString GATE_SINGLE_OUTPUT_ONE_ACTIVE = "Green Signal";

			// Token: 0x04008312 RID: 33554
			public static LocString GATE_SINGLE_OUTPUT_ONE_INACTIVE = "Red Signal";

			// Token: 0x04008313 RID: 33555
			public static LocString GATE_MULTIPLEXER_CONTROL_ONE_NAME = "CONTROL A";

			// Token: 0x04008314 RID: 33556
			public static LocString GATE_MULTIPLEXER_CONTROL_ONE_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Set signal path to <b>down</b> position";

			// Token: 0x04008315 RID: 33557
			public static LocString GATE_MULTIPLEXER_CONTROL_ONE_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Set signal path to <b>up</b> position";

			// Token: 0x04008316 RID: 33558
			public static LocString GATE_MULTIPLEXER_CONTROL_TWO_NAME = "CONTROL B";

			// Token: 0x04008317 RID: 33559
			public static LocString GATE_MULTIPLEXER_CONTROL_TWO_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Set signal path to <b>down</b> position";

			// Token: 0x04008318 RID: 33560
			public static LocString GATE_MULTIPLEXER_CONTROL_TWO_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Set signal path to <b>up</b> position";
		}

		// Token: 0x02001CB8 RID: 7352
		public class GAMEOBJECTEFFECTS
		{
			// Token: 0x04008319 RID: 33561
			public static LocString CALORIES = "+{0}";

			// Token: 0x0400831A RID: 33562
			public static LocString FOOD_QUALITY = "Quality: {0}";

			// Token: 0x0400831B RID: 33563
			public static LocString FORGAVEATTACKER = "Forgiveness";

			// Token: 0x0400831C RID: 33564
			public static LocString COLDBREATHER = UI.FormatAsLink("Cooling Effect", "HEAT");

			// Token: 0x0400831D RID: 33565
			public static LocString LIFECYCLETITLE = "Growth:";

			// Token: 0x0400831E RID: 33566
			public static LocString GROWTHTIME_SIMPLE = "Life Cycle: {0}";

			// Token: 0x0400831F RID: 33567
			public static LocString GROWTHTIME_REGROWTH = "Domestic growth: {0} / {1}";

			// Token: 0x04008320 RID: 33568
			public static LocString GROWTHTIME = "Growth: {0}";

			// Token: 0x04008321 RID: 33569
			public static LocString INITIALGROWTHTIME = "Initial Growth: {0}";

			// Token: 0x04008322 RID: 33570
			public static LocString REGROWTHTIME = "Regrowth: {0}";

			// Token: 0x04008323 RID: 33571
			public static LocString REQUIRES_LIGHT = UI.FormatAsLink("Light", "LIGHT") + ": {Lux}";

			// Token: 0x04008324 RID: 33572
			public static LocString REQUIRES_DARKNESS = UI.FormatAsLink("Darkness", "LIGHT");

			// Token: 0x04008325 RID: 33573
			public static LocString REQUIRESFERTILIZER = "{0}: {1}";

			// Token: 0x04008326 RID: 33574
			public static LocString IDEAL_FERTILIZER = "{0}: {1}";

			// Token: 0x04008327 RID: 33575
			public static LocString EQUIPMENT_MODS = "{Attribute} {Value}";

			// Token: 0x04008328 RID: 33576
			public static LocString ROTTEN = "Rotten";

			// Token: 0x04008329 RID: 33577
			public static LocString REQUIRES_ATMOSPHERE = UI.FormatAsLink("Atmosphere", "ATMOSPHERE") + ": {0}";

			// Token: 0x0400832A RID: 33578
			public static LocString REQUIRES_PRESSURE = UI.FormatAsLink("Air", "ATMOSPHERE") + " Pressure: {0} minimum";

			// Token: 0x0400832B RID: 33579
			public static LocString IDEAL_PRESSURE = UI.FormatAsLink("Air", "ATMOSPHERE") + " Pressure: {0}";

			// Token: 0x0400832C RID: 33580
			public static LocString REQUIRES_TEMPERATURE = UI.FormatAsLink("Temperature", "HEAT") + ": {0} to {1}";

			// Token: 0x0400832D RID: 33581
			public static LocString IDEAL_TEMPERATURE = UI.FormatAsLink("Temperature", "HEAT") + ": {0} to {1}";

			// Token: 0x0400832E RID: 33582
			public static LocString REQUIRES_SUBMERSION = UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " Submersion";

			// Token: 0x0400832F RID: 33583
			public static LocString FOOD_EFFECTS = "Effects:";

			// Token: 0x04008330 RID: 33584
			public static LocString EMITS_LIGHT = UI.FormatAsLink("Light Range", "LIGHT") + ": {0} tiles";

			// Token: 0x04008331 RID: 33585
			public static LocString EMITS_LIGHT_LUX = UI.FormatAsLink("Brightness", "LIGHT") + ": {0} Lux";

			// Token: 0x04008332 RID: 33586
			public static LocString AMBIENT_RADIATION = "Ambient Radiation";

			// Token: 0x04008333 RID: 33587
			public static LocString AMBIENT_RADIATION_FMT = "{minRads} - {maxRads}";

			// Token: 0x04008334 RID: 33588
			public static LocString AMBIENT_NO_MIN_RADIATION_FMT = "Less than {maxRads}";

			// Token: 0x04008335 RID: 33589
			public static LocString REQUIRES_NO_MIN_RADIATION = "Maximum " + UI.FormatAsLink("Radiation", "RADIATION") + ": {MaxRads}";

			// Token: 0x04008336 RID: 33590
			public static LocString REQUIRES_RADIATION = UI.FormatAsLink("Radiation", "RADIATION") + ": {MinRads} to {MaxRads}";

			// Token: 0x04008337 RID: 33591
			public static LocString MUTANT_STERILE = "Doesn't Drop " + UI.FormatAsLink("Seeds", "PLANTS");

			// Token: 0x04008338 RID: 33592
			public static LocString DARKNESS = "Darkness";

			// Token: 0x04008339 RID: 33593
			public static LocString LIGHT = "Light";

			// Token: 0x0400833A RID: 33594
			public static LocString SEED_PRODUCTION_DIG_ONLY = "Consumes 1 " + UI.FormatAsLink("Seed", "PLANTS");

			// Token: 0x0400833B RID: 33595
			public static LocString SEED_PRODUCTION_HARVEST = "Harvest yields " + UI.FormatAsLink("Seeds", "PLANTS");

			// Token: 0x0400833C RID: 33596
			public static LocString SEED_PRODUCTION_FINAL_HARVEST = "Final harvest yields " + UI.FormatAsLink("Seeds", "PLANTS");

			// Token: 0x0400833D RID: 33597
			public static LocString SEED_PRODUCTION_FRUIT = "Fruit produces " + UI.FormatAsLink("Seeds", "PLANTS");

			// Token: 0x0400833E RID: 33598
			public static LocString SEED_REQUIREMENT_CEILING = "Plot Orientation: Downward";

			// Token: 0x0400833F RID: 33599
			public static LocString SEED_REQUIREMENT_WALL = "Plot Orientation: Sideways";

			// Token: 0x04008340 RID: 33600
			public static LocString REQUIRES_RECEPTACLE = "Farm Plot";

			// Token: 0x04008341 RID: 33601
			public static LocString PLANT_MARK_FOR_HARVEST = "Autoharvest Enabled";

			// Token: 0x04008342 RID: 33602
			public static LocString PLANT_DO_NOT_HARVEST = "Autoharvest Disabled";

			// Token: 0x020025A4 RID: 9636
			public class INSULATED
			{
				// Token: 0x0400A5C4 RID: 42436
				public static LocString NAME = "Insulated";

				// Token: 0x0400A5C5 RID: 42437
				public static LocString TOOLTIP = "Proper insulation drastically reduces thermal conductivity";
			}

			// Token: 0x020025A5 RID: 9637
			public class TOOLTIPS
			{
				// Token: 0x0400A5C6 RID: 42438
				public static LocString CALORIES = "+{0}";

				// Token: 0x0400A5C7 RID: 42439
				public static LocString FOOD_QUALITY = "Quality: {0}";

				// Token: 0x0400A5C8 RID: 42440
				public static LocString COLDBREATHER = "Lowers ambient air temperature";

				// Token: 0x0400A5C9 RID: 42441
				public static LocString GROWTHTIME_SIMPLE = "This plant takes <b>{0}</b> to grow";

				// Token: 0x0400A5CA RID: 42442
				public static LocString GROWTHTIME_REGROWTH = "This plant initially takes <b>{0}</b> to grow, but only <b>{1}</b> to mature after first harvest";

				// Token: 0x0400A5CB RID: 42443
				public static LocString GROWTHTIME = "This plant takes <b>{0}</b> to grow";

				// Token: 0x0400A5CC RID: 42444
				public static LocString INITIALGROWTHTIME = "This plant takes <b>{0}</b> to mature again once replanted";

				// Token: 0x0400A5CD RID: 42445
				public static LocString REGROWTHTIME = "This plant takes <b>{0}</b> to mature again once harvested";

				// Token: 0x0400A5CE RID: 42446
				public static LocString EQUIPMENT_MODS = "{Attribute} {Value}";

				// Token: 0x0400A5CF RID: 42447
				public static LocString REQUIRESFERTILIZER = string.Concat(new string[]
				{
					"This plant requires <b>{1}</b> ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" for basic growth"
				});

				// Token: 0x0400A5D0 RID: 42448
				public static LocString IDEAL_FERTILIZER = string.Concat(new string[]
				{
					"This plant requires <b>{1}</b> of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" for basic growth"
				});

				// Token: 0x0400A5D1 RID: 42449
				public static LocString REQUIRES_LIGHT = string.Concat(new string[]
				{
					"This plant requires a ",
					UI.PRE_KEYWORD,
					"Light",
					UI.PST_KEYWORD,
					" source bathing it in at least {Lux}"
				});

				// Token: 0x0400A5D2 RID: 42450
				public static LocString REQUIRES_DARKNESS = "This plant requires complete darkness";

				// Token: 0x0400A5D3 RID: 42451
				public static LocString REQUIRES_ATMOSPHERE = "This plant must be submerged in one of the following gases: {0}";

				// Token: 0x0400A5D4 RID: 42452
				public static LocString REQUIRES_ATMOSPHERE_LIQUID = "This plant must be submerged in one of the following liquids: {0}";

				// Token: 0x0400A5D5 RID: 42453
				public static LocString REQUIRES_ATMOSPHERE_MIXED = "This plant must be submerged in one of the following gases or liquids: {0}";

				// Token: 0x0400A5D6 RID: 42454
				public static LocString REQUIRES_PRESSURE = string.Concat(new string[]
				{
					"Ambient ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" pressure must be at least <b>{0}</b> for basic growth"
				});

				// Token: 0x0400A5D7 RID: 42455
				public static LocString IDEAL_PRESSURE = string.Concat(new string[]
				{
					"This plant requires ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" pressures above <b>{0}</b> for basic growth"
				});

				// Token: 0x0400A5D8 RID: 42456
				public static LocString REQUIRES_TEMPERATURE = string.Concat(new string[]
				{
					"Internal ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" must be between <b>{0}</b> and <b>{1}</b> for basic growth"
				});

				// Token: 0x0400A5D9 RID: 42457
				public static LocString IDEAL_TEMPERATURE = string.Concat(new string[]
				{
					"This plant requires internal ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" between <b>{0}</b> and <b>{1}</b> for basic growth"
				});

				// Token: 0x0400A5DA RID: 42458
				public static LocString REQUIRES_SUBMERSION = string.Concat(new string[]
				{
					"This plant must be fully submerged in ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" for basic growth"
				});

				// Token: 0x0400A5DB RID: 42459
				public static LocString FOOD_EFFECTS = "Duplicants will gain the following effects from eating this food: {0}";

				// Token: 0x0400A5DC RID: 42460
				public static LocString REQUIRES_RECEPTACLE = string.Concat(new string[]
				{
					"This plant must be housed in a ",
					UI.FormatAsLink("Planter Box", "PLANTERBOX"),
					", ",
					UI.FormatAsLink("Farm Tile", "FARMTILE"),
					", or ",
					UI.FormatAsLink("Hydroponic Farm", "HYDROPONICFARM"),
					" farm to grow domestically"
				});

				// Token: 0x0400A5DD RID: 42461
				public static LocString EMITS_LIGHT = string.Concat(new string[]
				{
					"Emits ",
					UI.PRE_KEYWORD,
					"Light",
					UI.PST_KEYWORD,
					"\n\nDuplicants can operate buildings more quickly when they're well lit"
				});

				// Token: 0x0400A5DE RID: 42462
				public static LocString EMITS_LIGHT_LUX = string.Concat(new string[]
				{
					"Emits ",
					UI.PRE_KEYWORD,
					"Light",
					UI.PST_KEYWORD,
					"\n\nDuplicants can operate buildings more quickly when they're well lit"
				});

				// Token: 0x0400A5DF RID: 42463
				public static LocString SEED_PRODUCTION_DIG_ONLY = "May be replanted, but will produce no further " + UI.PRE_KEYWORD + "Seeds" + UI.PST_KEYWORD;

				// Token: 0x0400A5E0 RID: 42464
				public static LocString SEED_PRODUCTION_HARVEST = "Harvesting this plant will yield new " + UI.PRE_KEYWORD + "Seeds" + UI.PST_KEYWORD;

				// Token: 0x0400A5E1 RID: 42465
				public static LocString SEED_PRODUCTION_FINAL_HARVEST = string.Concat(new string[]
				{
					"Yields new ",
					UI.PRE_KEYWORD,
					"Seeds",
					UI.PST_KEYWORD,
					" on the final harvest of its life cycle"
				});

				// Token: 0x0400A5E2 RID: 42466
				public static LocString SEED_PRODUCTION_FRUIT = "Consuming this plant's fruit will yield new " + UI.PRE_KEYWORD + "Seeds" + UI.PST_KEYWORD;

				// Token: 0x0400A5E3 RID: 42467
				public static LocString SEED_REQUIREMENT_CEILING = "This seed must be planted in a downward facing plot\n\nPress " + UI.FormatAsKeyWord("[O]") + " while building farm plots to rotate them";

				// Token: 0x0400A5E4 RID: 42468
				public static LocString SEED_REQUIREMENT_WALL = "This seed must be planted in a side facing plot\n\nPress " + UI.FormatAsKeyWord("[O]") + " while building farm plots to rotate them";

				// Token: 0x0400A5E5 RID: 42469
				public static LocString REQUIRES_NO_MIN_RADIATION = "This plant will stop growing if exposed to more than {MaxRads} of " + UI.FormatAsLink("Radiation", "RADIATION");

				// Token: 0x0400A5E6 RID: 42470
				public static LocString REQUIRES_RADIATION = "This plant will only grow if it has between {MinRads} and {MaxRads} of " + UI.FormatAsLink("Radiation", "RADIATION");

				// Token: 0x0400A5E7 RID: 42471
				public static LocString MUTANT_SEED_TOOLTIP = "\n\nGrowing near its maximum radiation increases the chance of mutant seeds being produced";

				// Token: 0x0400A5E8 RID: 42472
				public static LocString MUTANT_STERILE = "This plant will not produce seeds of its own due to changes to its DNA";
			}

			// Token: 0x020025A6 RID: 9638
			public class DAMAGE_POPS
			{
				// Token: 0x0400A5E9 RID: 42473
				public static LocString OVERHEAT = "Overheat Damage";

				// Token: 0x0400A5EA RID: 42474
				public static LocString CORROSIVE_ELEMENT = "Corrosive Element Damage";

				// Token: 0x0400A5EB RID: 42475
				public static LocString WRONG_ELEMENT = "Wrong Element Damage";

				// Token: 0x0400A5EC RID: 42476
				public static LocString CIRCUIT_OVERLOADED = "Overload Damage";

				// Token: 0x0400A5ED RID: 42477
				public static LocString LOGIC_CIRCUIT_OVERLOADED = "Signal Overload Damage";

				// Token: 0x0400A5EE RID: 42478
				public static LocString LIQUID_PRESSURE = "Pressure Damage";

				// Token: 0x0400A5EF RID: 42479
				public static LocString MINION_DESTRUCTION = "Tantrum Damage";

				// Token: 0x0400A5F0 RID: 42480
				public static LocString CONDUIT_CONTENTS_FROZE = "Cold Damage";

				// Token: 0x0400A5F1 RID: 42481
				public static LocString CONDUIT_CONTENTS_BOILED = "Heat Damage";

				// Token: 0x0400A5F2 RID: 42482
				public static LocString MICROMETEORITE = "Micrometeorite Damage";

				// Token: 0x0400A5F3 RID: 42483
				public static LocString COMET = "Meteor Damage";

				// Token: 0x0400A5F4 RID: 42484
				public static LocString ROCKET = "Rocket Thruster Damage";
			}
		}

		// Token: 0x02001CB9 RID: 7353
		public class ASTEROIDCLOCK
		{
			// Token: 0x04008343 RID: 33603
			public static LocString CYCLE = "Cycle";

			// Token: 0x04008344 RID: 33604
			public static LocString CYCLES_OLD = "This Colony is {0} Cycle(s) Old";

			// Token: 0x04008345 RID: 33605
			public static LocString TIME_PLAYED = "Time Played: {0} hours";

			// Token: 0x04008346 RID: 33606
			public static LocString SCHEDULE_BUTTON_TOOLTIP = "Manage Schedule";
		}

		// Token: 0x02001CBA RID: 7354
		public class ENDOFDAYREPORT
		{
			// Token: 0x04008347 RID: 33607
			public static LocString REPORT_TITLE = "DAILY REPORTS";

			// Token: 0x04008348 RID: 33608
			public static LocString DAY_TITLE = "Cycle {0}";

			// Token: 0x04008349 RID: 33609
			public static LocString DAY_TITLE_TODAY = "Cycle {0} - Today";

			// Token: 0x0400834A RID: 33610
			public static LocString DAY_TITLE_YESTERDAY = "Cycle {0} - Yesterday";

			// Token: 0x0400834B RID: 33611
			public static LocString NOTIFICATION_TITLE = "Cycle {0} report ready";

			// Token: 0x0400834C RID: 33612
			public static LocString NOTIFICATION_TOOLTIP = "The daily report for Cycle {0} is ready to view";

			// Token: 0x0400834D RID: 33613
			public static LocString NEXT = "Next";

			// Token: 0x0400834E RID: 33614
			public static LocString PREV = "Prev";

			// Token: 0x0400834F RID: 33615
			public static LocString ADDED = "Added";

			// Token: 0x04008350 RID: 33616
			public static LocString REMOVED = "Removed";

			// Token: 0x04008351 RID: 33617
			public static LocString NET = "Net";

			// Token: 0x04008352 RID: 33618
			public static LocString DUPLICANT_DETAILS_HEADER = "Duplicant Details:";

			// Token: 0x04008353 RID: 33619
			public static LocString TIME_DETAILS_HEADER = "Total Time Details:";

			// Token: 0x04008354 RID: 33620
			public static LocString BASE_DETAILS_HEADER = "Base Details:";

			// Token: 0x04008355 RID: 33621
			public static LocString AVERAGE_TIME_DETAILS_HEADER = "Average Time Details:";

			// Token: 0x04008356 RID: 33622
			public static LocString MY_COLONY = "my colony";

			// Token: 0x04008357 RID: 33623
			public static LocString NONE = "None";

			// Token: 0x020025A7 RID: 9639
			public class OXYGEN_CREATED
			{
				// Token: 0x0400A5F5 RID: 42485
				public static LocString NAME = UI.FormatAsLink("Oxygen", "OXYGEN") + " Generation:";

				// Token: 0x0400A5F6 RID: 42486
				public static LocString POSITIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Oxygen", "OXYGEN") + " was produced by {1} over the course of the day";

				// Token: 0x0400A5F7 RID: 42487
				public static LocString NEGATIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Oxygen", "OXYGEN") + " was consumed by {1} over the course of the day";
			}

			// Token: 0x020025A8 RID: 9640
			public class CALORIES_CREATED
			{
				// Token: 0x0400A5F8 RID: 42488
				public static LocString NAME = "Calorie Generation:";

				// Token: 0x0400A5F9 RID: 42489
				public static LocString POSITIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Food", "FOOD") + " was produced by {1} over the course of the day";

				// Token: 0x0400A5FA RID: 42490
				public static LocString NEGATIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Food", "FOOD") + " was consumed by {1} over the course of the day";
			}

			// Token: 0x020025A9 RID: 9641
			public class NUMBER_OF_DOMESTICATED_CRITTERS
			{
				// Token: 0x0400A5FB RID: 42491
				public static LocString NAME = "Domesticated Critters:";

				// Token: 0x0400A5FC RID: 42492
				public static LocString POSITIVE_TOOLTIP = "{0} domestic critters live in {1}";

				// Token: 0x0400A5FD RID: 42493
				public static LocString NEGATIVE_TOOLTIP = "{0} domestic critters live in {1}";
			}

			// Token: 0x020025AA RID: 9642
			public class NUMBER_OF_WILD_CRITTERS
			{
				// Token: 0x0400A5FE RID: 42494
				public static LocString NAME = "Wild Critters:";

				// Token: 0x0400A5FF RID: 42495
				public static LocString POSITIVE_TOOLTIP = "{0} wild critters live in {1}";

				// Token: 0x0400A600 RID: 42496
				public static LocString NEGATIVE_TOOLTIP = "{0} wild critters live in {1}";
			}

			// Token: 0x020025AB RID: 9643
			public class ROCKETS_IN_FLIGHT
			{
				// Token: 0x0400A601 RID: 42497
				public static LocString NAME = "Rocket Missions Underway:";

				// Token: 0x0400A602 RID: 42498
				public static LocString POSITIVE_TOOLTIP = "{0} rockets are currently flying missions for {1}";

				// Token: 0x0400A603 RID: 42499
				public static LocString NEGATIVE_TOOLTIP = "{0} rockets are currently flying missions for {1}";
			}

			// Token: 0x020025AC RID: 9644
			public class STRESS_DELTA
			{
				// Token: 0x0400A604 RID: 42500
				public static LocString NAME = UI.FormatAsLink("Stress", "STRESS") + " Change:";

				// Token: 0x0400A605 RID: 42501
				public static LocString POSITIVE_TOOLTIP = UI.FormatAsLink("Stress", "STRESS") + " increased by a total of {0} for {1}";

				// Token: 0x0400A606 RID: 42502
				public static LocString NEGATIVE_TOOLTIP = UI.FormatAsLink("Stress", "STRESS") + " decreased by a total of {0} for {1}";
			}

			// Token: 0x020025AD RID: 9645
			public class TRAVELTIMEWARNING
			{
				// Token: 0x0400A607 RID: 42503
				public static LocString WARNING_TITLE = "Long Commutes";

				// Token: 0x0400A608 RID: 42504
				public static LocString WARNING_MESSAGE = "My Duplicants are spending a significant amount of time traveling between their errands (> {0})";
			}

			// Token: 0x020025AE RID: 9646
			public class TRAVEL_TIME
			{
				// Token: 0x0400A609 RID: 42505
				public static LocString NAME = "Travel Time:";

				// Token: 0x0400A60A RID: 42506
				public static LocString POSITIVE_TOOLTIP = "On average, {1} spent {0} of their time traveling between tasks";
			}

			// Token: 0x020025AF RID: 9647
			public class WORK_TIME
			{
				// Token: 0x0400A60B RID: 42507
				public static LocString NAME = "Work Time:";

				// Token: 0x0400A60C RID: 42508
				public static LocString POSITIVE_TOOLTIP = "On average, {0} of {1}'s time was spent working";
			}

			// Token: 0x020025B0 RID: 9648
			public class IDLE_TIME
			{
				// Token: 0x0400A60D RID: 42509
				public static LocString NAME = "Idle Time:";

				// Token: 0x0400A60E RID: 42510
				public static LocString POSITIVE_TOOLTIP = "On average, {0} of {1}'s time was spent idling";
			}

			// Token: 0x020025B1 RID: 9649
			public class PERSONAL_TIME
			{
				// Token: 0x0400A60F RID: 42511
				public static LocString NAME = "Personal Time:";

				// Token: 0x0400A610 RID: 42512
				public static LocString POSITIVE_TOOLTIP = "On average, {0} of {1}'s time was spent tending to personal needs";
			}

			// Token: 0x020025B2 RID: 9650
			public class ENERGY_USAGE
			{
				// Token: 0x0400A611 RID: 42513
				public static LocString NAME = UI.FormatAsLink("Power", "POWER") + " Usage:";

				// Token: 0x0400A612 RID: 42514
				public static LocString POSITIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Power", "POWER") + " was created by {1} over the course of the day";

				// Token: 0x0400A613 RID: 42515
				public static LocString NEGATIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Power", "POWER") + " was consumed by {1} over the course of the day";
			}

			// Token: 0x020025B3 RID: 9651
			public class ENERGY_WASTED
			{
				// Token: 0x0400A614 RID: 42516
				public static LocString NAME = UI.FormatAsLink("Power", "POWER") + " Wasted:";

				// Token: 0x0400A615 RID: 42517
				public static LocString NEGATIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Power", "POWER") + " was lost today due to battery runoff and overproduction in {1}";
			}

			// Token: 0x020025B4 RID: 9652
			public class LEVEL_UP
			{
				// Token: 0x0400A616 RID: 42518
				public static LocString NAME = "Skill Increases:";

				// Token: 0x0400A617 RID: 42519
				public static LocString TOOLTIP = "Today {1} gained a total of {0} skill levels";
			}

			// Token: 0x020025B5 RID: 9653
			public class TOILET_INCIDENT
			{
				// Token: 0x0400A618 RID: 42520
				public static LocString NAME = "Restroom Accidents:";

				// Token: 0x0400A619 RID: 42521
				public static LocString TOOLTIP = "{0} Duplicants couldn't quite reach the toilet in time today";
			}

			// Token: 0x020025B6 RID: 9654
			public class DISEASE_ADDED
			{
				// Token: 0x0400A61A RID: 42522
				public static LocString NAME = UI.FormatAsLink("Diseases", "DISEASE") + " Contracted:";

				// Token: 0x0400A61B RID: 42523
				public static LocString POSITIVE_TOOLTIP = "{0} " + UI.FormatAsLink("Disease", "DISEASE") + " were contracted by {1}";

				// Token: 0x0400A61C RID: 42524
				public static LocString NEGATIVE_TOOLTIP = "{0} " + UI.FormatAsLink("Disease", "DISEASE") + " were cured by {1}";
			}

			// Token: 0x020025B7 RID: 9655
			public class CONTAMINATED_OXYGEN_FLATULENCE
			{
				// Token: 0x0400A61D RID: 42525
				public static LocString NAME = UI.FormatAsLink("Flatulence", "CONTAMINATEDOXYGEN") + " Generation:";

				// Token: 0x0400A61E RID: 42526
				public static LocString POSITIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN") + " was generated by {1} over the course of the day";

				// Token: 0x0400A61F RID: 42527
				public static LocString NEGATIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN") + " was consumed by {1} over the course of the day";
			}

			// Token: 0x020025B8 RID: 9656
			public class CONTAMINATED_OXYGEN_TOILET
			{
				// Token: 0x0400A620 RID: 42528
				public static LocString NAME = UI.FormatAsLink("Toilet Emissions: ", "CONTAMINATEDOXYGEN");

				// Token: 0x0400A621 RID: 42529
				public static LocString POSITIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN") + " was generated by {1} over the course of the day";

				// Token: 0x0400A622 RID: 42530
				public static LocString NEGATIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN") + " was consumed by {1} over the course of the day";
			}

			// Token: 0x020025B9 RID: 9657
			public class CONTAMINATED_OXYGEN_SUBLIMATION
			{
				// Token: 0x0400A623 RID: 42531
				public static LocString NAME = UI.FormatAsLink("Sublimation", "CONTAMINATEDOXYGEN") + ":";

				// Token: 0x0400A624 RID: 42532
				public static LocString POSITIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN") + " was generated by {1} over the course of the day";

				// Token: 0x0400A625 RID: 42533
				public static LocString NEGATIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN") + " was consumed by {1} over the course of the day";
			}

			// Token: 0x020025BA RID: 9658
			public class DISEASE_STATUS
			{
				// Token: 0x0400A626 RID: 42534
				public static LocString NAME = "Disease Status:";

				// Token: 0x0400A627 RID: 42535
				public static LocString TOOLTIP = "There are {0} covering {1}";
			}

			// Token: 0x020025BB RID: 9659
			public class CHORE_STATUS
			{
				// Token: 0x0400A628 RID: 42536
				public static LocString NAME = "Errands:";

				// Token: 0x0400A629 RID: 42537
				public static LocString POSITIVE_TOOLTIP = "{0} errands are queued for {1}";

				// Token: 0x0400A62A RID: 42538
				public static LocString NEGATIVE_TOOLTIP = "{0} errands were completed over the course of the day by {1}";
			}

			// Token: 0x020025BC RID: 9660
			public class NOTES
			{
				// Token: 0x0400A62B RID: 42539
				public static LocString NOTE_ENTRY_LINE_ITEM = "{0}\n{1}: {2}";

				// Token: 0x0400A62C RID: 42540
				public static LocString BUTCHERED = "Butchered for {0}";

				// Token: 0x0400A62D RID: 42541
				public static LocString BUTCHERED_CONTEXT = "Butchered";

				// Token: 0x0400A62E RID: 42542
				public static LocString CRAFTED = "Crafted a {0}";

				// Token: 0x0400A62F RID: 42543
				public static LocString CRAFTED_USED = "{0} used as ingredient";

				// Token: 0x0400A630 RID: 42544
				public static LocString CRAFTED_CONTEXT = "Crafted";

				// Token: 0x0400A631 RID: 42545
				public static LocString HARVESTED = "Harvested {0}";

				// Token: 0x0400A632 RID: 42546
				public static LocString HARVESTED_CONTEXT = "Harvested";

				// Token: 0x0400A633 RID: 42547
				public static LocString EATEN = "{0} eaten";

				// Token: 0x0400A634 RID: 42548
				public static LocString ROTTED = "Rotten {0}";

				// Token: 0x0400A635 RID: 42549
				public static LocString ROTTED_CONTEXT = "Rotted";

				// Token: 0x0400A636 RID: 42550
				public static LocString GERMS = "On {0}";

				// Token: 0x0400A637 RID: 42551
				public static LocString TIME_SPENT = "{0}";

				// Token: 0x0400A638 RID: 42552
				public static LocString WORK_TIME = "{0}";

				// Token: 0x0400A639 RID: 42553
				public static LocString PERSONAL_TIME = "{0}";

				// Token: 0x0400A63A RID: 42554
				public static LocString FOODFIGHT_CONTEXT = "{0} ingested in food fight";
			}
		}

		// Token: 0x02001CBB RID: 7355
		public static class SCHEDULEBLOCKTYPES
		{
			// Token: 0x020025BD RID: 9661
			public static class EAT
			{
				// Token: 0x0400A63B RID: 42555
				public static LocString NAME = "Mealtime";

				// Token: 0x0400A63C RID: 42556
				public static LocString DESCRIPTION = "EAT:\nDuring Mealtime Duplicants will head to their assigned mess halls and eat.";
			}

			// Token: 0x020025BE RID: 9662
			public static class SLEEP
			{
				// Token: 0x0400A63D RID: 42557
				public static LocString NAME = "Sleep";

				// Token: 0x0400A63E RID: 42558
				public static LocString DESCRIPTION = "SLEEP:\nWhen it's time to sleep, Duplicants will head to their assigned rooms and rest.";
			}

			// Token: 0x020025BF RID: 9663
			public static class WORK
			{
				// Token: 0x0400A63F RID: 42559
				public static LocString NAME = "Work";

				// Token: 0x0400A640 RID: 42560
				public static LocString DESCRIPTION = "WORK:\nDuring Work hours Duplicants will perform any pending errands in the colony.";
			}

			// Token: 0x020025C0 RID: 9664
			public static class RECREATION
			{
				// Token: 0x0400A641 RID: 42561
				public static LocString NAME = "Recreation";

				// Token: 0x0400A642 RID: 42562
				public static LocString DESCRIPTION = "HAMMER TIME:\nDuring Hammer Time, Duplicants will relieve their " + UI.FormatAsLink("Stress", "STRESS") + " through dance. Please be aware that no matter how hard my Duplicants try, they will absolutely not be able to touch this.";
			}

			// Token: 0x020025C1 RID: 9665
			public static class HYGIENE
			{
				// Token: 0x0400A643 RID: 42563
				public static LocString NAME = "Hygiene";

				// Token: 0x0400A644 RID: 42564
				public static LocString DESCRIPTION = "HYGIENE:\nDuring " + UI.FormatAsLink("Hygiene", "HYGIENE") + " hours Duplicants will head to their assigned washrooms to get cleaned up.";
			}
		}

		// Token: 0x02001CBC RID: 7356
		public static class SCHEDULEGROUPS
		{
			// Token: 0x04008358 RID: 33624
			public static LocString TOOLTIP_FORMAT = "{0}\n\n{1}";

			// Token: 0x04008359 RID: 33625
			public static LocString MISSINGBLOCKS = "Warning: Scheduling Issues ({0})";

			// Token: 0x0400835A RID: 33626
			public static LocString NOTIME = "No {0} shifts allotted";

			// Token: 0x020025C2 RID: 9666
			public static class HYGENE
			{
				// Token: 0x0400A645 RID: 42565
				public static LocString NAME = "Bathtime";

				// Token: 0x0400A646 RID: 42566
				public static LocString DESCRIPTION = "During Bathtime shifts my Duplicants will take care of their hygienic needs, such as going to the bathroom, using the shower or washing their hands.\n\nOnce they're all caught up on personal hygiene, Duplicants will head back to work.";

				// Token: 0x0400A647 RID: 42567
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"During ",
					UI.PRE_KEYWORD,
					"Bathtime",
					UI.PST_KEYWORD,
					" shifts my Duplicants will take care of their hygienic needs, such as going to the bathroom, using the shower or washing their hands."
				});
			}

			// Token: 0x020025C3 RID: 9667
			public static class WORKTIME
			{
				// Token: 0x0400A648 RID: 42568
				public static LocString NAME = "Work";

				// Token: 0x0400A649 RID: 42569
				public static LocString DESCRIPTION = "During Work shifts my Duplicants must perform the errands I have placed for them throughout the colony.\n\nIt's important when scheduling to maintain a good work-life balance for my Duplicants to maintain their health and prevent Morale loss.";

				// Token: 0x0400A64A RID: 42570
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"During ",
					UI.PRE_KEYWORD,
					"Work",
					UI.PST_KEYWORD,
					" shifts my Duplicants must perform the errands I've placed for them throughout the colony."
				});
			}

			// Token: 0x020025C4 RID: 9668
			public static class RECREATION
			{
				// Token: 0x0400A64B RID: 42571
				public static LocString NAME = "Downtime";

				// Token: 0x0400A64C RID: 42572
				public static LocString DESCRIPTION = "During Downtime my Duplicants they may do as they please.\n\nThis may include personal matters like bathroom visits or snacking, or they may choose to engage in leisure activities like socializing with friends.\n\nDowntime increases Duplicant Morale.";

				// Token: 0x0400A64D RID: 42573
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"During ",
					UI.PRE_KEYWORD,
					"Downtime",
					UI.PST_KEYWORD,
					" shifts my Duplicants they may do as they please."
				});
			}

			// Token: 0x020025C5 RID: 9669
			public static class SLEEP
			{
				// Token: 0x0400A64E RID: 42574
				public static LocString NAME = "Bedtime";

				// Token: 0x0400A64F RID: 42575
				public static LocString DESCRIPTION = "My Duplicants use Bedtime shifts to rest up after a hard day's work.\n\nScheduling too few bedtime shifts may prevent my Duplicants from regaining enough Stamina to make it through the following day.";

				// Token: 0x0400A650 RID: 42576
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"My Duplicants use ",
					UI.PRE_KEYWORD,
					"Bedtime",
					UI.PST_KEYWORD,
					" shifts to rest up after a hard day's work."
				});
			}
		}

		// Token: 0x02001CBD RID: 7357
		public class ELEMENTAL
		{
			// Token: 0x020025C6 RID: 9670
			public class AGE
			{
				// Token: 0x0400A651 RID: 42577
				public static LocString NAME = "Age: {0}";

				// Token: 0x0400A652 RID: 42578
				public static LocString TOOLTIP = "The selected object is {0} cycles old";

				// Token: 0x0400A653 RID: 42579
				public static LocString UNKNOWN = "Unknown";

				// Token: 0x0400A654 RID: 42580
				public static LocString UNKNOWN_TOOLTIP = "The age of the selected object is unknown";
			}

			// Token: 0x020025C7 RID: 9671
			public class UPTIME
			{
				// Token: 0x0400A655 RID: 42581
				public static LocString NAME = "Uptime:\n{0}{1}: {2}\n{0}{3}: {4}\n{0}{5}: {6}";

				// Token: 0x0400A656 RID: 42582
				public static LocString THIS_CYCLE = "This Cycle";

				// Token: 0x0400A657 RID: 42583
				public static LocString LAST_CYCLE = "Last Cycle";

				// Token: 0x0400A658 RID: 42584
				public static LocString LAST_X_CYCLES = "Last {0} Cycles";
			}

			// Token: 0x020025C8 RID: 9672
			public class PRIMARYELEMENT
			{
				// Token: 0x0400A659 RID: 42585
				public static LocString NAME = "Primary Element: {0}";

				// Token: 0x0400A65A RID: 42586
				public static LocString TOOLTIP = "The selected object is primarily composed of {0}";
			}

			// Token: 0x020025C9 RID: 9673
			public class UNITS
			{
				// Token: 0x0400A65B RID: 42587
				public static LocString NAME = "Stack Units: {0}";

				// Token: 0x0400A65C RID: 42588
				public static LocString TOOLTIP = "This stack contains {0} units of {1}";
			}

			// Token: 0x020025CA RID: 9674
			public class MASS
			{
				// Token: 0x0400A65D RID: 42589
				public static LocString NAME = "Mass: {0}";

				// Token: 0x0400A65E RID: 42590
				public static LocString TOOLTIP = "The selected object has a mass of {0}";
			}

			// Token: 0x020025CB RID: 9675
			public class TEMPERATURE
			{
				// Token: 0x0400A65F RID: 42591
				public static LocString NAME = "Temperature: {0}";

				// Token: 0x0400A660 RID: 42592
				public static LocString TOOLTIP = "The selected object's current temperature is {0}";
			}

			// Token: 0x020025CC RID: 9676
			public class DISEASE
			{
				// Token: 0x0400A661 RID: 42593
				public static LocString NAME = "Disease: {0}";

				// Token: 0x0400A662 RID: 42594
				public static LocString TOOLTIP = "There are {0} on the selected object";
			}

			// Token: 0x020025CD RID: 9677
			public class SHC
			{
				// Token: 0x0400A663 RID: 42595
				public static LocString NAME = "Specific Heat Capacity: {0}";

				// Token: 0x0400A664 RID: 42596
				public static LocString TOOLTIP = "{SPECIFIC_HEAT_CAPACITY} is required to heat 1 g of the selected object by 1 {TEMPERATURE_UNIT}";
			}

			// Token: 0x020025CE RID: 9678
			public class THERMALCONDUCTIVITY
			{
				// Token: 0x0400A665 RID: 42597
				public static LocString NAME = "Thermal Conductivity: {0}";

				// Token: 0x0400A666 RID: 42598
				public static LocString TOOLTIP = "This object can conduct heat to other materials at a rate of {THERMAL_CONDUCTIVITY} W for each degree {TEMPERATURE_UNIT} difference\n\nBetween two objects, the rate of heat transfer will be determined by the object with the lowest Thermal Conductivity";

				// Token: 0x02002F46 RID: 12102
				public class ADJECTIVES
				{
					// Token: 0x0400BDE8 RID: 48616
					public static LocString VALUE_WITH_ADJECTIVE = "{0} ({1})";

					// Token: 0x0400BDE9 RID: 48617
					public static LocString VERY_LOW_CONDUCTIVITY = "Highly Insulating";

					// Token: 0x0400BDEA RID: 48618
					public static LocString LOW_CONDUCTIVITY = "Insulating";

					// Token: 0x0400BDEB RID: 48619
					public static LocString MEDIUM_CONDUCTIVITY = "Conductive";

					// Token: 0x0400BDEC RID: 48620
					public static LocString HIGH_CONDUCTIVITY = "Highly Conductive";

					// Token: 0x0400BDED RID: 48621
					public static LocString VERY_HIGH_CONDUCTIVITY = "Extremely Conductive";
				}
			}

			// Token: 0x020025CF RID: 9679
			public class CONDUCTIVITYBARRIER
			{
				// Token: 0x0400A667 RID: 42599
				public static LocString NAME = "Insulation Thickness: {0}";

				// Token: 0x0400A668 RID: 42600
				public static LocString TOOLTIP = "Thick insulation reduces an object's Thermal Conductivity";
			}

			// Token: 0x020025D0 RID: 9680
			public class VAPOURIZATIONPOINT
			{
				// Token: 0x0400A669 RID: 42601
				public static LocString NAME = "Vaporization Point: {0}";

				// Token: 0x0400A66A RID: 42602
				public static LocString TOOLTIP = "The selected object will evaporate into a gas at {0}";
			}

			// Token: 0x020025D1 RID: 9681
			public class MELTINGPOINT
			{
				// Token: 0x0400A66B RID: 42603
				public static LocString NAME = "Melting Point: {0}";

				// Token: 0x0400A66C RID: 42604
				public static LocString TOOLTIP = "The selected object will melt into a liquid at {0}";
			}

			// Token: 0x020025D2 RID: 9682
			public class OVERHEATPOINT
			{
				// Token: 0x0400A66D RID: 42605
				public static LocString NAME = "Overheat Modifier: {0}";

				// Token: 0x0400A66E RID: 42606
				public static LocString TOOLTIP = "This building will overheat and take damage if its temperature reaches {0}\n\nBuilding with better building materials can increase overheat temperature";
			}

			// Token: 0x020025D3 RID: 9683
			public class FREEZEPOINT
			{
				// Token: 0x0400A66F RID: 42607
				public static LocString NAME = "Freeze Point: {0}";

				// Token: 0x0400A670 RID: 42608
				public static LocString TOOLTIP = "The selected object will cool into a solid at {0}";
			}

			// Token: 0x020025D4 RID: 9684
			public class DEWPOINT
			{
				// Token: 0x0400A671 RID: 42609
				public static LocString NAME = "Condensation Point: {0}";

				// Token: 0x0400A672 RID: 42610
				public static LocString TOOLTIP = "The selected object will condense into a liquid at {0}";
			}
		}

		// Token: 0x02001CBE RID: 7358
		public class IMMIGRANTSCREEN
		{
			// Token: 0x0400835B RID: 33627
			public static LocString IMMIGRANTSCREENTITLE = "Select a Blueprint";

			// Token: 0x0400835C RID: 33628
			public static LocString PROCEEDBUTTON = "Print";

			// Token: 0x0400835D RID: 33629
			public static LocString CANCELBUTTON = "Cancel";

			// Token: 0x0400835E RID: 33630
			public static LocString REJECTALL = "Reject All";

			// Token: 0x0400835F RID: 33631
			public static LocString EMBARK = "EMBARK";

			// Token: 0x04008360 RID: 33632
			public static LocString SELECTDUPLICANTS = "Select {0} Duplicants";

			// Token: 0x04008361 RID: 33633
			public static LocString SELECTYOURCREW = "CHOOSE THREE DUPLICANTS TO BEGIN";

			// Token: 0x04008362 RID: 33634
			public static LocString SHUFFLE = "REROLL";

			// Token: 0x04008363 RID: 33635
			public static LocString SHUFFLETOOLTIP = "Reroll for a different Duplicant";

			// Token: 0x04008364 RID: 33636
			public static LocString BACK = "BACK";

			// Token: 0x04008365 RID: 33637
			public static LocString CONFIRMATIONTITLE = "Reject All Printables?";

			// Token: 0x04008366 RID: 33638
			public static LocString CONFIRMATIONBODY = "The Printing Pod will need time to recharge if I reject these Printables.";

			// Token: 0x04008367 RID: 33639
			public static LocString NAME_YOUR_COLONY = "NAME THE COLONY";

			// Token: 0x04008368 RID: 33640
			public static LocString CARE_PACKAGE_ELEMENT_QUANTITY = "{0} of {1}";

			// Token: 0x04008369 RID: 33641
			public static LocString CARE_PACKAGE_ELEMENT_COUNT = "{0} x {1}";

			// Token: 0x0400836A RID: 33642
			public static LocString CARE_PACKAGE_ELEMENT_COUNT_ONLY = "x {0}";

			// Token: 0x0400836B RID: 33643
			public static LocString CARE_PACKAGE_CURRENT_AMOUNT = "Available: {0}";

			// Token: 0x0400836C RID: 33644
			public static LocString DUPLICATE_COLONY_NAME = "A colony named \"{0}\" already exists";
		}

		// Token: 0x02001CBF RID: 7359
		public class METERS
		{
			// Token: 0x020025D5 RID: 9685
			public class HEALTH
			{
				// Token: 0x0400A673 RID: 42611
				public static LocString TOOLTIP = "Health";
			}

			// Token: 0x020025D6 RID: 9686
			public class BREATH
			{
				// Token: 0x0400A674 RID: 42612
				public static LocString TOOLTIP = "Oxygen";
			}

			// Token: 0x020025D7 RID: 9687
			public class FUEL
			{
				// Token: 0x0400A675 RID: 42613
				public static LocString TOOLTIP = "Fuel";
			}

			// Token: 0x020025D8 RID: 9688
			public class BATTERY
			{
				// Token: 0x0400A676 RID: 42614
				public static LocString TOOLTIP = "Battery Charge";
			}
		}
	}
}
