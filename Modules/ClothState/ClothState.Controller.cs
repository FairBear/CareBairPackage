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
			"Top",
			"Bottom",
			"Bra",
			"Underwear",
			"Gloves",
			"Stockings",
			"Socks",
			"Shoes"
		};

		static readonly ADMSheet sheet = new ADMSheet(
			"Cloth State",
			null,
			Condition,
			"Change the current state of the girl's clothes."
		);

		static IntReactiveProperty _selectedID;
		static int selectedID;
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
			if (!Enabled.Value || !Map.IsInstance() || Map.Instance.Player == null)
				return false;

			PlayerActor player = Map.Instance.Player;

			if (player.Controller.State is AIProject.Player.Communication && player.CommCompanion != null)
			{
				if (selectedID != 0)
				{
					selectedID = 0;
					current = player.CommCompanion.ChaControl;

					RefreshSheets();
				}

				return true;
			}

			StatusUI ui = MapUIContainer.SystemMenuUI.StatusUI;

			if (ui != null && ui.EnabledInput)
			{
				if (_selectedID == null)
					_selectedID = Traverse.Create(ui).Field("_selectedID").GetValue<IntReactiveProperty>();

				if (_selectedID != null && _selectedID.Value != 0)
				{
					if (_selectedID.Value != selectedID)
					{
						selectedID = _selectedID.Value;
						current = Map.Instance.AgentTable[selectedID - 1].ChaControl;

						RefreshSheets();
					}

					return true;
				}
			}
			else if (_selectedID != null)
				_selectedID = null;

			selectedID = -1;
			return false;
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
					(flag, slider) =>
					{
						if (flag)
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
