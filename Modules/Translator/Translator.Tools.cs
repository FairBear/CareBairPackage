using System.Collections.Generic;
using XUnity.AutoTranslator.Plugin.Core;

namespace CareBairPackage
{
	public static partial class Translator
	{
		static readonly Dictionary<string, string> list = new Dictionary<string, string>();

		public static string Translate(this string text)
		{
			if (Enabled.Value)
			{
				if (list.ContainsKey(text))
					return list[text] ?? text;

				try { Translate_Internal(text); } catch { }
			}

			return text;
		}

		static void Translate_Internal(string text)
		{
			if (list.ContainsKey(text))
				return;

			list[text] = null;

			AutoTranslator.Default.TranslateAsync(text, v =>
			{
				if (v.Succeeded)
					list[text] = v.TranslatedText;
				else
					list.Remove(text);
			});
		}
	}
}
