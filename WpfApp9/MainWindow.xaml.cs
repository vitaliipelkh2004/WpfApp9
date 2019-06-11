﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace WpfApp9
{
    public partial class MainWindow : Window
    {
        List<Storage> items = new List<Storage>();
        DateTime now = new DateTime(2019, 6, 11);
        Storage item;
        ObservableCollection<Storage> itemsnew = new ObservableCollection<Storage>();
        private int current = -1;
        public MainWindow()
        {
            InitializeComponent();
            datepicker1.IsEnabled = false;          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            item = new Storage();
            item.Owner = ownertext.Text;
            item.Name = nametext.Text;
            item.Height = Int32.Parse(heighttext.Text);
            item.Width = Int32.Parse(widhttext.Text);
            item.Days = Int32.Parse(daystext.Text);
            item.Money = item.Days * 5;
            item.Arrears = 0;
            datepicker1.IsEnabled = true;
            items.Add(item);
            var itemss = main.Children;
            foreach (var item in itemss)
            {
                if (item is TextBox textbox1)
                {
                    textbox1.Clear();
                }
            }
            XmlSerializer xml = new XmlSerializer(typeof(List<Storage>));
            using (FileStream t = new FileStream("GAME1.xml", FileMode.OpenOrCreate, FileAccess.Write))
            {
                xml.Serialize(t,items);

            }
        }
        private void Datepicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datepicker1.SelectedDate.Value >= now.AddDays(item.Days))
            {
                item.Arrears = item.Money * 5 / 100;
                MessageBox.Show($"Arrears  {item.Arrears.ToString()}");
                datepicker1.IsEnabled = false;
            }
        }

       

        private void Itembox_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{itembox.Width}:{itembox.Height}");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<Storage>));
            FileStream t = new FileStream("GAME1.xml", FileMode.Open, FileAccess.Read);
            itemsnew = (ObservableCollection<Storage>)xml.Deserialize(t);
            t.Close();
            this.DataContext = itemsnew[1];
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (current == -1)
            {
                this.DataContext = itemsnew.FirstOrDefault();
                current = 0;

            }
            else if (current == 0)
            {
                this.DataContext = itemsnew.LastOrDefault();
                current = itemsnew.Count - 1;

            }
            else
            {
                this.DataContext = itemsnew[current-- - 1];
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (current == -1 || current == itemsnew.Count - 1)
            {
                this.DataContext = itemsnew.FirstOrDefault();
                current = 0;
            }
            else
            {
                this.DataContext = itemsnew[++current];
            }
        }
    }  
}
