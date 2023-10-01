﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Wpf.Ui.Mvvm.Contracts;

using CommunityToolkit.Mvvm.Input;

using Roforge.UI.Elements.Menu.Pages;

namespace Roforge.UI.ViewModels.Menu
{
    public class FastFlagsViewModel : NotifyPropertyChangedViewModel
    {
        private readonly Page _page;

        public FastFlagsViewModel(Page page) 
        { 
            _page = page;
        }

        private void OpenFastFlagEditor()
        {
            if (Window.GetWindow(_page) is INavigationWindow window)
                window.Navigate(typeof(FastFlagEditorPage));
        }

        public ICommand OpenFastFlagEditorCommand => new RelayCommand(OpenFastFlagEditor);

        public Visibility ShowDebugFlags => App.Settings.Prop.OhHeyYouFoundMe ? Visibility.Visible : Visibility.Collapsed;

        public bool HttpRequestLogging
        {
            get => App.FastFlags.GetPreset("HTTP.Log") is not null;
            set => App.FastFlags.SetPreset("HTTP.Log", value ? 12 : null);
        }

        public string HttpRequestProxy
        {
            get => App.FastFlags.GetPreset("HTTP.Proxy.Address.1") ?? "";

            set
            {
                App.FastFlags.SetPreset("HTTP.Proxy.Enable", String.IsNullOrEmpty(value) ? null : true);
                App.FastFlags.SetPreset("HTTP.Proxy.Address", String.IsNullOrEmpty(value) ? null : value);
            }
        }

        public string StateOverlayFlags
        {
            get => App.FastFlags.GetPreset("UI.FlagState") ?? "";
            set => App.FastFlags.SetPreset("UI.FlagState", String.IsNullOrEmpty(value) ? null : value);
        }

        public int FramerateLimit
        {
            get => int.TryParse(App.FastFlags.GetPreset("Rendering.Framerate"), out int x) ? x : 60;
            set => App.FastFlags.SetPreset("Rendering.Framerate", value);
        }

        public IReadOnlyDictionary<string, string> RenderingModes => FastFlagManager.RenderingModes;

        public string SelectedRenderingMode
        {
            get => App.FastFlags.GetPresetEnum(RenderingModes, "Rendering.Mode", "True");
            set
            {
                App.FastFlags.SetPresetEnum("Rendering.Mode", RenderingModes[value], "True");
                App.FastFlags.CheckManualFullscreenPreset();
            }
        }

        public bool FixDisplayScaling
        {
            get => App.FastFlags.GetPreset("Rendering.DisableScaling") == "True";
            set => App.FastFlags.SetPreset("Rendering.DisableScaling", value ? "True" : null);
        }

        public bool AlternateGraphicsSelectorEnabled
        {
            get => App.FastFlags.GetPreset("UI.Menu.GraphicsSlider") == "True";
            set => App.FastFlags.SetPreset("UI.Menu.GraphicsSlider", value ? "True" : null);
        }

        public bool InGameChromeEnabled
        { 
            get => App.FastFlags.GetPreset("UI.Menu.InGameChrome") == "True";
            set => App.FastFlags.SetPreset("UI.Menu.InGameChrome", value ? "True" : null);
        }

        public bool InfiniteJump
        {
            get => App.FastFlags.GetPreset("Physics.InfiniteJump") == "1";
            set => App.FastFlags.SetPreset("Physics.InfiniteJump", value ? "1" : null);
        }

        public bool DynamicHeadsDisabled
        {
            get => App.FastFlags.GetPreset("Rendering.DisableDynamicHeads") == "False";
            set => App.FastFlags.SetPreset("Rendering.DisableDynamicHeads", value ? "False" : null);
        }

        public bool AccessibilityEnabled
        {
            get => App.FastFlags.GetPreset("UI.Menu.Accessibility.EnableAccessibility") == "True";
            set => App.FastFlags.SetPreset("UI.Menu.Accessibility", value ? "True" : null);
        }

        public bool Pre2022TexturesEnabled
        {
            get => App.FastFlags.GetPreset("Rendering.TexturePack") == FastFlagManager.OldTexturesFlagValue;
            set => App.FastFlags.SetPreset("Rendering.TexturePack", value ? FastFlagManager.OldTexturesFlagValue : null);
        }

        public int GrassRenderLimit
        {
            get => int.TryParse(App.FastFlags.GetPreset("Rendering.GrassLimit"), out int x) ? x : 99999;
            set => App.FastFlags.SetPreset("Rendering.GrassLimit", value);
        }

        public IReadOnlyDictionary<string, Dictionary<string, string?>> IGMenuVersions => FastFlagManager.IGMenuVersions;

        public string SelectedIGMenuVersion
        {
            get
            {
                // yeah this kinda sucks
                foreach (var version in IGMenuVersions)
                {
                    bool flagsMatch = true;

                    foreach (var flag in version.Value)
                    {
                        foreach (var presetFlag in FastFlagManager.PresetFlags.Where(x => x.Key.StartsWith($"UI.Menu.Style.{flag.Key}")))
                        { 
                            if (App.FastFlags.GetValue(presetFlag.Value) != flag.Value)
                                flagsMatch = false;
                        }
                    }

                    if (flagsMatch)
                        return version.Key;
                }

                return IGMenuVersions.First().Key;
            }

            set
            {
                foreach (var flag in IGMenuVersions[value])
                    App.FastFlags.SetPreset($"UI.Menu.Style.{flag.Key}", flag.Value);
            }
        }

        public IReadOnlyDictionary<string, string> LightingModes => FastFlagManager.LightingModes;

        public IReadOnlyDictionary<string, string> TextureQualities => FastFlagManager.TextureQualities;

        public string SelectedLightingMode
        {
            get => App.FastFlags.GetPresetEnum(LightingModes, "Rendering.Lighting", "True");
            set => App.FastFlags.SetPresetEnum("Rendering.Lighting", LightingModes[value], "True");
        }

        public string SelectedTextureQuality
        {

            get
            {
                if (App.FastFlags.GetPreset("Rendering.TextureLevel.Quality") is null)
                    return TextureQualities.First().Key;

                return TextureQualities.FirstOrDefault(x => x.Value == App.FastFlags.GetPreset("Rendering.TextureLevel.Quality")).Key;
            }
            
            set
            {
                App.FastFlags.SetPreset("Rendering.TextureLevel.Quality", TextureQualities[value]);
                App.FastFlags.SetPreset("Rendering.TextureLevel.Enabled", true);
            }

        }

        public bool GuiHidingEnabled
        {
            get => App.FastFlags.GetPreset("UI.Hide") == "32380007";
            set => App.FastFlags.SetPreset("UI.Hide", value ? "32380007" : null);
        }
    }
}
