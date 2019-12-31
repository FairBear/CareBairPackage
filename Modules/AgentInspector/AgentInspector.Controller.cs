using AIProject;
using AIProject.UI;
using HarmonyLib;
using Manager;
using UniRx;

namespace CareBairPackage
{
	public static partial class AgentInspector
	{
		// -2 = None; -1 = Girl via Comm; 0 = Player; 1 = Girl 1; ...; 4 = Girl 4
		static IntReactiveProperty _selectedID;
		public static int SelectedID { get; private set; }
		public static Actor Current { get; private set; }

		public static void Update()
		{
			if (!Enabled.Value || !Map.IsInstance() || Map.Instance.Player == null)
				return;

			PlayerActor player = Map.Instance.Player;

			if (player.Controller.State is AIProject.Player.Communication && player.CommCompanion != null)
			{
				if (SelectedID != -1)
				{
					SelectedID = -1;
					Current = player.CommCompanion;
				}

				return;
			}

			StatusUI ui = MapUIContainer.SystemMenuUI.StatusUI;

			if (ui != null && ui.EnabledInput)
			{
				if (_selectedID == null)
					_selectedID = Traverse.Create(ui).Field("_selectedID").GetValue<IntReactiveProperty>();

				if (_selectedID != null)
				{
					if (_selectedID.Value != SelectedID)
					{
						SelectedID = _selectedID.Value;
						Current =
							SelectedID == 0 ?
								(Actor)Map.Instance.Player :
								Map.Instance.AgentTable[SelectedID - 1];
					}

					return;
				}
			}
			else if (_selectedID != null)
				_selectedID = null;

			SelectedID = -2;
			Current = null;
		}
	}
}
