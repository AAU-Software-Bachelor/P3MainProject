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


            majorTrait smite = new majorTrait(CurrentConfig.newUID("Ability"));
            majorTrait EldrichBLAST = new majorTrait(CurrentConfig.newUID("Ability"));
            majorTrait Warrior = new majorTrait(CurrentConfig.newUID("Career"));
            majorTrait Warlock = new majorTrait(CurrentConfig.newUID("Career"));
            majorTrait Dwarf = new majorTrait(CurrentConfig.newUID("Race"));
            majorTrait Elf = new majorTrait(CurrentConfig.newUID("Race"));
            resourceTrait health = new resourceTrait(CurrentConfig.newUID("Resource"));
            resourceTrait mana = new resourceTrait(CurrentConfig.newUID("Resource"));
            resourceTrait xp = new resourceTrait(CurrentConfig.newUID("Resource"));

            smite.Name = "SMITE";
            smite.Description = "slaa gud modstander med hellig styrke";
            smite.Type = "Ability";
            smite.cost = 4;
            smite.CostTypes = new List<string>() { xp.UID };
            smite.dependency = new List<List<string>>()
            {
                new List<string>(){ Warrior.UID}
            };
            smite.addDiscount(Warrior.UID);
            smite.discounts[0].Amount = 2;
            smite.addDiscountType(Warrior.UID, new List<string>() { xp.UID});
            CurrentConfig.AbilList.Add(smite);
            
            EldrichBLAST.Name = "Eldrich BLAST";
            EldrichBLAST.Description = "it's a BLAST to use";
            EldrichBLAST.Type = "Ability";
            EldrichBLAST.cost = 4;
            EldrichBLAST.CostTypes = new List<string>() { xp.UID };
            EldrichBLAST.dependency = new List<List<string>>()
            {
                new List<string>(){ Warrior.UID}
            };
            EldrichBLAST.addDiscount(Warlock.UID);
            EldrichBLAST.discounts[0].Amount = 2;
            EldrichBLAST.addDiscountType(Warlock.UID, new List<string>() { xp.UID});
            EldrichBLAST.addAffectedResources(mana.UID, 2);
            CurrentConfig.AbilList.Add(EldrichBLAST);
            
            Warrior.Name = "Warrior";
            Warrior.Description = "am gonna swing a sword";
            Warrior.Type = "Career";
            Warrior.addAffectedResources(health.UID, 2);
            CurrentConfig.CarList.Add(Warrior);

            Warlock.Name = "Warlock";
            Warlock.Description = "-_-";
            Warlock.Type = "Career";
            Warlock.dependency = new List<List<string>>()
            {
                new List<string>(){ Warrior.UID}
            };
            Warlock.addAffectedResources(mana.UID, 2);
            CurrentConfig.CarList.Add(Warlock);

            Elf.Name = "Elf";
            Elf.Description = "the most pompius pricks of all races";
            Elf.Type = "Race";
            Elf.freeAbilities = new List<string>() {EldrichBLAST.UID};
            Elf.addAffectedResources(health.UID, 2);
            Elf.addAffectedResources(mana.UID, 3);
            CurrentConfig.RacList.Add(Elf);

            Dwarf.Name = "Dwarf";
            Dwarf.Description = "this be armor";
            Dwarf.Type = "Race";
            Dwarf.freeAbilities = new List<string>() {smite.UID};
            Dwarf.addAffectedResources(health.UID, 4);
            CurrentConfig.RacList.Add(Dwarf);

            health.Name = "health";
            health.Description = "ye dead if this be zero";
            health.type = 1;
            CurrentConfig.ResList.Add(health);

            mana.Name = "Mana";
            mana.Description = "le-Magic juice";
            mana.type = 1;
            CurrentConfig.ResList.Add(mana);

            xp.Name = "xp";
            xp.Description = "GAINS!!";
            xp.type = 2;
            CurrentConfig.ResList.Add(xp);

            MainWindow mainwindow = new MainWindow(CurrentConfig);
            Application.Current.MainWindow.Content = mainwindow;
        }
    }
}
