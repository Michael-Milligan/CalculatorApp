using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
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
		public static event EventHandler LanguageChanged;

		public static List<CultureInfo> Languages { get; } = new List<CultureInfo>();

        public App()
		{
			Languages.Clear();
			Languages.Add(new CultureInfo("en-US"));
			Languages.Add(new CultureInfo("ru-RU"));
		}

        public static CultureInfo Language { 
            get 
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set 
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == Language) return;

                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

				ResourceDictionary dict = new ResourceDictionary();
				switch (value.Name)
				{
					case "ru-RU":
						dict.Source = new Uri(("Resources/lang.ru-RU.xaml"), UriKind.Relative);
						break;
					default:
						dict.Source = new Uri("Resources/lang.xaml", UriKind.Relative);
						break;
				}

				ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
											  where d.Source != null && d.Source.OriginalString.StartsWith("Resources/lang.")
											  select d).First();
				if (oldDict != null)
				{
					int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
					Application.Current.Resources.MergedDictionaries.Remove(oldDict);
					Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
				}
				else
				{
					Application.Current.Resources.MergedDictionaries.Add(dict);
				}

				new Model().LanguageChanged(Application.Current, new EventArgs());
			}
		}
	}
}
