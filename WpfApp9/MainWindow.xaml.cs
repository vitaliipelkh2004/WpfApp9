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
        int Area=10000;
        ObservableCollection<Storage> itemsnew = new ObservableCollection<Storage>();
        private int current = -1;
        public MainWindow()
        {
            InitializeComponent();
            datepicker1.IsEnabled = false;          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var itemss = main.Children;
            try
            {
                if (Area -  Int32.Parse(widhttext.Text) *Int32.Parse(heighttext.Text) <= 0)
                {
                    MessageBox.Show($"{item.Name} very big");
                    foreach (var item in itemss)
                    {
                        if (item is TextBox textbox1)
                        {
                            textbox1.Clear();
                        }
                    }
                }        
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
                Area = Area - item.Width * item.Height;
                foreach (var item in itemss)
                {
                    if (item is TextBox textbox1)
                    {
                        textbox1.Clear();
                    }
                }
            XmlSerializer xml = new XmlSerializer(typeof(List<Storage>));
            using (FileStream t = new FileStream("1.xml", FileMode.OpenOrCreate, FileAccess.Write))
            {
                xml.Serialize(t,items);

            }
            }
            catch
            {

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
            try
            {
             //itembox.Visibility = Visibility;
                XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<Storage>));
                FileStream t = new FileStream("1.xml", FileMode.Open, FileAccess.Read);
                itemsnew = (ObservableCollection<Storage>)xml.Deserialize(t);
                t.Close();
                this.DataContext = itemsnew[1];
                deletebutton.IsEnabled = true;
                leftbutton.IsEnabled = true;
                rightbutton.IsEnabled = true;
            }
        catch
            {

            }
       }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
                if (current == -1 || current == itemsnew.Count - 1)
                {
                    this.DataContext = itemsnew[1];
                    current = 0;
                }
                else
                {
                    this.DataContext = itemsnew[++current];
                }
                        
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
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

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"To pay {itemsnew[current].Arrears+itemsnew[current].Money} $");
            Area = Area + itemsnew[current].Width * itemsnew[current].Height;
            itemsnew.RemoveAt(current);
            try
            {
                this.DataContext = itemsnew.LastOrDefault();

            }
            catch
            { }

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Label label = new Label();
            
            for (int i = 1; i < itemsnew.Count; i++)
                label.Content += itemsnew[i].ToString();
          
            Window window = new Window();
            window.Height = 400;
            window.Width = 400;
            window.Show();
            window.ToolTip = "Some Window With Money";
            window.Title = "Some Window With Money";            
            window.Content = label;
           


        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
        }
    }  
}
