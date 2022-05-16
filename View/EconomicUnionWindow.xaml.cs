using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestEFAsyncWPF.Model.Countries;
using TestEFAsyncWPF.ViewModel;

namespace TestEFAsyncWPF.View
{
    /// <summary>
    /// Логика взаимодействия для EconomicUnionWindow.xaml
    /// </summary>
    public partial class EconomicUnionWindow : Window
    {
        public struct CountryUnionCheck
        {
            StackPanel StackPanel { get; set; }
            TextBlock TextBlock { get; set; }
            public CheckBox CheckBox { get; set; }
            //public bool IsChecked { get; set; }
            public Country Country { get; set; }
            public EconomicUnion EconomicUnion { get; set; }

            public CountryUnionCheck(EconomicUnion economicUnion,
                Country country,
                EconomicUnionWindow economicUnionWindow) : this()
            {
                StackPanel = new StackPanel();
                TextBlock = new TextBlock();
                CheckBox = new CheckBox();
                EconomicUnion = economicUnion;
                Country = country;
                if (EconomicUnion.Countries.Contains(Country))
                    CheckBox.IsChecked = true;
                else
                    CheckBox.IsChecked = false;

                TextBlock.Text = Country.Name;
                StackPanel.Orientation = Orientation.Horizontal;
                StackPanel.Children.Add(CheckBox);
                StackPanel.Children.Add(TextBlock);
                economicUnionWindow.checkCountries.Items.Add(StackPanel);

            }

        }

        public List<CountryUnionCheck> checks = new();
        public EconomicUnion EconomicUnion;

        public EconomicUnionWindow(EconomicUnion economicUnion, MainViewModel mainViewModel)
        {
            InitializeComponent();
            EconomicUnion = economicUnion;
            foreach (Country country in mainViewModel.Countries)
            {
                checks.Add(new CountryUnionCheck(EconomicUnion, country, this));

            }
            this.DataContext = checks;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            foreach(CountryUnionCheck check in checks)
            {
                System.Diagnostics.Debug.WriteLine("Check: "+check.Country.Name+" "+check.CheckBox.IsChecked.ToString());
                if (check.CheckBox.IsChecked == true)
                {
                    if (!EconomicUnion.Countries.Contains(check.Country))
                        EconomicUnion.Countries.Add(check.Country);
                }
                else
                    if(EconomicUnion.Countries.Contains(check.Country))
                        EconomicUnion.Countries.Remove(check.Country);
            }
            foreach (Country country in EconomicUnion.Countries)
            {
                System.Diagnostics.Debug.WriteLine("EUW Countries:");
                System.Diagnostics.Debug.WriteLine(country.Name);
            }
            this.DialogResult = true;
        }

    }
}
