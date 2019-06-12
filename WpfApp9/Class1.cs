using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp9
{
    [Serializable]
    public class Storage
    {
        public string Name {set;get;}
        public string Owner { set; get; }
        public double Days { set; get; }
        public int Width { set; get; }
        public int Height { set; get; }
        public double Arrears { set; get; }
        public double Money { set; get; }
        public Storage()
        {

        }
        public Storage(string n,string o,double d,double a,double m,int w,int h)
        {
            Name = n;
            Owner = o;
            Arrears = a;
            Money = m;
            Days = d;
            Width = w;
            Height = h; 
        }
        public override string ToString()
        {
            return $"\n______________________________\nOwner: {Owner}\nName: {Name}\nMoney: {Money}\nArrears: {Arrears}\n______________________________";
        }

    }
}
