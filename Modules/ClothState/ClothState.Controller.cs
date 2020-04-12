using AIChara;
using AIProject;
using AIProject.UI;
using HarmonyLib;
using Manager;
using System.Collections.Generic;
using UniRx;

namespace CareBairPackage
{
	public static partial class ClothState
	{
		static readonly string[] CATEGORY = new string[] {
			//"Top",
			//"Bottom",
			//"Bra",
			//"Underwear",
			//"Gloves",
			//"Stockings",
			//"Socks",
			//"Shoes"
			"上衣",
			"下衣",
			"胸罩",
			"内裤",
			"手套",
			"连袜裤",
			"袜子",
			"鞋子"
		};

		static readonly ADMSheet sheet = new ADMSheet(
			/*"Cloth State"*/
			"衣服状态",
			null,
			Condition,
			//"Change the current state of the agent's clothes."
			"改变当前属性界面所选角色的衣服状态"
		);

		static ChaControl current;

		public static void Update()
		{
			if (AltDialogMenu.IsSheet(sheet) || !Condition())
				return;

			//RefreshSheets();
			AltDialogMenu.AddSheet(sheet);
		}

		public static bool Condition()
		{
			if (AgentInspector.Current == null)
				return false;

			ChaControl next = AgentInspector.Current.ChaControl;

			if (current != next)
			{
				current = next;

				RefreshSheets();
			}

			return true;
		}

		public static void RefreshSheets()
		{
			List<ADMSheet> sheets = new List<ADMSheet>();

			for (int i = 0; i < CATEGORY.Length; i++)
			{
				int n = i;

				sheets.Add(new ADMSheet(
					CATEGORY[i],
					current.fileStatus.clothesState[i],
					0,
					2,
					(next, slider) =>
					{
						if (next == 0)
							return;

						if (next > 0)
							current.SetClothesStateNext(n);
						else
							current.SetClothesStatePrev(n);

						for (int c = 0; c < sheets.Count; c++)
							(sheets[c].value as ADMSheetSlider).value = current.fileStatus.clothesState[c];
					}
				));
			}

			sheet.SetSheets(sheets);
		}
	}
}
