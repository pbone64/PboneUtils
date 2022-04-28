using PboneLib.CustomLoading;
using PboneLib.CustomLoading.Content;
using PboneLib.CustomLoading.Content.Implementations.Misc;
using PboneLib.CustomLoading.Localization;
using Terraria.ModLoader;

namespace PboneUtils
{
	public sealed class PboneUtils : Mod
	{
		public PboneUtils()
		{
			ContentAutoloadingEnabled = false;
		}

        public override void Load()
        {
			CustomModLoader modLoader = new(this);

			CustomLocalizationLoader localizationLoader = new(LocalizationLoader.AddTranslation);

			CustomContentLoader configLoader = new(new(content => {
				PConfig config = content.AsLoadable as PConfig;
				AddConfig(config.GetName(), config);
			}));
			configLoader.Settings.TryToLoadConditions = CustomContentLoader.ContentLoaderSettings.PresetTryToLoadConfigConditions;
			modLoader.Add(configLoader);

			CustomContentLoader contentLoader = new(new(content => {
				AddContent(content.AsLoadable);
            }));
			configLoader.Settings.TryToLoadConditions = CustomContentLoader.ContentLoaderSettings.PresetNormalTryToLoadConditions;
			modLoader.Add(contentLoader);

			modLoader.Load();
		}
	}
}