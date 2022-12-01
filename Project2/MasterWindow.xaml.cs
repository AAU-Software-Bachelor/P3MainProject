using System;
using System.Collections.Generic;
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

namespace Project2
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

        }
        public config CurrentConfig { get; set; }

        public void OnLoad(object sender, RoutedEventArgs e)
        {
            config CurrentConfig = new();


            majorTrait TestMT = new majorTrait(CurrentConfig.newUID("Ability"));
            TestMT.Name = "SMITE";
            TestMT.Description = "slaa gud modstander med hellig styrke";
            TestMT.Type = "Ability";
            TestMT.cost = 4;
            TestMT.CostTypes = new List<string>() { "Resource-/GUID4", "Resource-/GUID5" };
            TestMT.dependency = new List<List<string>>()
            {
                new List<string>(){ "Career-/GUID1", "Career-/GUID2"},
                new List<string>(){ "Ability-/GUID7", "Ability-/GUID6" },
                new List<string>(){ "Career-/GUID3", "Ability-/GUID9" }
            };
            TestMT.addDiscount("Career-/GUID2");
            TestMT.discounts[0].Amount = 2;
            TestMT.addDiscountType("Career-/GUID2", new List<string>() { "Resource-/GUID4", "Resource-/GUID5" });
            CurrentConfig.AbilList.Add(TestMT);

            TestMT = new majorTrait(CurrentConfig.newUID("Ability"));
            TestMT.Name = "Eldrich BLAST";
            TestMT.Description = "it's a BLAST to use";
            TestMT.Type = "Ability";
            TestMT.cost = 4;
            TestMT.CostTypes = new List<string>() { "Resource-/GUID6", "Resource-/GUID7" };
            TestMT.dependency = new List<List<string>>()
            {
                new List<string>(){ "Career-/GUID6", "Career-/GUID4"},
                new List<string>(){ "Ability-/GUID7", "Ability-/GUID6" },
                new List<string>(){ "Career-/GUID4", "Ability-/GUID9" }
            };
            TestMT.addDiscount("Career-/GUID2");
            TestMT.discounts[0].Amount = 2;
            TestMT.addDiscountType("Career-/GUID4", new List<string>() { "Resource-/GUID6", "Resource-/GUID7" });
            TestMT.addAffectedResources("Resource-/GUID3", 2);
            CurrentConfig.AbilList.Add(TestMT);

            TestMT = new majorTrait(CurrentConfig.newUID("Career"));
            TestMT.Name = "Warrior";
            TestMT.Description = "am gonna swing a sword";
            TestMT.Type = "Career";
            TestMT.addAffectedResources("Resource-/GUID1", 1);
            CurrentConfig.CarList.Add(TestMT);

            TestMT = new majorTrait(CurrentConfig.newUID("Career"));
            TestMT.Name = "Warlock";
            TestMT.Description = "-_-";
            TestMT.Type = "Career";
            TestMT.dependency = new List<List<string>>()
            {
                new List<string>(){ "Career-/GUID6"},
                new List<string>(){ "Career-/GUID4"}
            };
            TestMT.addAffectedResources("Resource-/GUID3", 2);
            CurrentConfig.CarList.Add(TestMT);


            TestMT = new majorTrait(CurrentConfig.newUID("Race"));
            TestMT.Name = "Elf";
            TestMT.Description = "the most pompius pricks of all races";
            TestMT.Type = "Race";
            TestMT.freeAbilities = new List<string>() { "Ability-/GUID5" };
            TestMT.addAffectedResources("Resource-/GUID1", 2);
            TestMT.addAffectedResources("Resource-/GUID3", 3);
            CurrentConfig.RacList.Add(TestMT);

            TestMT = new majorTrait(CurrentConfig.newUID("Race"));
            TestMT.Name = "Dwarf";
            TestMT.Description = "this be armor";
            TestMT.Type = "Race";
            TestMT.freeAbilities = new List<string>() { "Ability-/GUID2" };
            TestMT.addAffectedResources("Resource-/GUID1", 4);
            CurrentConfig.RacList.Add(TestMT);


            MainWindow mainwindow = new MainWindow(CurrentConfig);
            Application.Current.MainWindow.Content = mainwindow;
        }
    }
}
