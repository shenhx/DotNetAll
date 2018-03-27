using System;
using System.Collections.Generic;
using System.Windows;

namespace FarsiLibrary.FX.Test
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        #region Theme Names

        public const string Theme_Generic = "Generic";
        public const string Theme_Aero = "Aero.NormalColor";
        public const string Theme_Classic = "Classic";
        public const string Theme_LunaNormal = "Luna.NormalColor";
        public const string Theme_LunaHomestead = "Luna.Homestead";
        public const string Theme_LunaMetallic = "Luna.Metallic";
        public const string Theme_Royale = "Royale.NormalColor";

        #endregion

        #region Fields

        private readonly Dictionary<string, ThemeInfo> _themeList;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public App()
        {
            _themeList = new Dictionary<string, ThemeInfo>();
        }

        #endregion

        #region Props

        public Dictionary<string, ThemeInfo> AvailableThemes
        {
            get { return _themeList; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Application Startup
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            _themeList.Add(Theme_Aero, GetTheme(Theme_Aero));
            _themeList.Add(Theme_Classic, GetTheme(Theme_Classic));
            _themeList.Add(Theme_LunaNormal, GetTheme(Theme_LunaNormal));
            _themeList.Add(Theme_LunaHomestead, GetTheme(Theme_LunaHomestead));
            _themeList.Add(Theme_LunaMetallic, GetTheme(Theme_LunaMetallic));
            _themeList.Add(Theme_Royale, GetTheme(Theme_Royale));

            base.OnStartup(e);
        }

        private ResourceDictionary GetSystemTheme(string name)
        {
            Uri uri = null;

            switch (name)
            {
                case Theme_Aero:
                    uri = new Uri("/PresentationFramework.Aero;component/themes/Aero.NormalColor.xaml", UriKind.Relative);
                    break;

                case Theme_Classic:
                    uri = new Uri("/PresentationFramework.Classic;component/themes/Classic.xaml", UriKind.Relative);
                    break;

                case Theme_LunaNormal:
                    uri = new Uri("/PresentationFramework.Luna;component/themes/Luna.NormalColor.xaml", UriKind.Relative);
                    break;

                case Theme_LunaHomestead:
                    uri = new Uri("/PresentationFramework.Luna;component/themes/Luna.Homestead.xaml", UriKind.Relative);
                    break;

                case Theme_LunaMetallic:
                    uri = new Uri("/PresentationFramework.Luna;component/themes/Luna.Metallic.xaml", UriKind.Relative);
                    break;

                case Theme_Royale:
                    uri = new Uri("/PresentationFramework.Royale;component/themes/Royale.NormalColor.xaml", UriKind.Relative);
                    break;
            }

            if (uri != null)
                return LoadComponent(uri) as ResourceDictionary;

            return null;
        }

        private ThemeInfo GetTheme(string themeName)
        {
            Uri uri = new Uri(string.Format(@"FarsiLibrary.FX.Win;;;component/themes/{0}.xaml", themeName), UriKind.Relative);
            ResourceDictionary skinTheme = LoadComponent(uri) as ResourceDictionary;
            ResourceDictionary systemTheme = GetSystemTheme(themeName);

            return new ThemeInfo(themeName, skinTheme, systemTheme);
        }

        #endregion
    }

    #region ThemeInfo

    public class ThemeInfo
    {
        public ResourceDictionary SkinTheme;
        public ResourceDictionary SystemTheme;
        public string Name;

        public ThemeInfo(string name, ResourceDictionary skinTheme, ResourceDictionary systemTheme)
        {
            SkinTheme = skinTheme;
            SystemTheme = systemTheme;
            Name = name;
        }
    }

    #endregion
}
