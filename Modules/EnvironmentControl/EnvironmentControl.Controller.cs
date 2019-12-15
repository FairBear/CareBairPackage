namespace CareBairPackage
{
	public static partial class EnvironmentControl
	{
		static bool shouldUpdate = false;
		static bool visible = false;

		static void Update()
		{
			if (!Manager.Map.IsInstance() ||
				Manager.Map.Instance.Simulator == null)
			{
				if (visible)
					visible = false;

				return;
			}

			if (Key.Value.IsDown())
			{
				if (!visible)
					shouldUpdate = true;

				visible = !visible;
			}
		}
	}
}
