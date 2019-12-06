using AIProject;
using Manager;

namespace CareBairPackage
{
	public static partial class ProfileInspector
	{
		public static void PlayerProfile()
		{
			Batch<int>(
				Resources.Instance.PlayerProfile,
				"PlayerProfile",
				"_defaultInventoryMaxSlot"
			);
		}
	}
}
