using AIProject;
using Manager;

namespace CareBairPackage
{
	public static partial class ProfileInspector
	{
		public static void AgentProfile()
		{
			Batch<float>(
				Resources.Instance.AgentProfile,
				"AgentProfile",
				"_actionPointNearDistance"
			);
		}
	}
}
