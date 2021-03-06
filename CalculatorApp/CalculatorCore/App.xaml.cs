﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		public static List<CultureInfo> Languages { get; } = new List<CultureInfo>();

        public App()
		{
			Languages.Clear();
			Languages.Add(new CultureInfo("en-US"));
			Languages.Add(new CultureInfo("ru-RU"));

			string defaultLanguage = File.ReadAllText(Model.Path);

			Language = new CultureInfo(defaultLanguage);
		}

        public static CultureInfo Language { 
            get 
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set 
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (value == Language) return;

                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

				ResourceDictionary dict = new ResourceDictionary();
				switch (value.Name)
				{
					case "ru-RU":
					case "en-US":
						dict.Source = new Uri(($"Resources/lang.{value.Name}.xaml"), UriKind.Relative);
						break;
					default:
						dict.Source = new Uri($"Resources/lang.{File.ReadAllText(Model.Path)}.xaml", 
							UriKind.Relative);
						break;
				}

				ResourceDictionary oldDict = (from d in Current.Resources.MergedDictionaries
											  where d.Source != null && d.Source.OriginalString.StartsWith("Resources/lang.")
											  select d).FirstOrDefault();
				if (oldDict != null)
				{
					int Index = Current.Resources.MergedDictionaries.IndexOf(oldDict);
					Current.Resources.MergedDictionaries.Remove(oldDict);
					Current.Resources.MergedDictionaries.Insert(Index, dict);
				}
				else
				{
					Current.Resources.MergedDictionaries.Add(dict);
				}
				new Model().App_LanguageChanged();
			}
		}
	}
}
