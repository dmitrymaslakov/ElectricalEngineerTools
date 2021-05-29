using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            TestClass sample = new TestClass();
            sample.PropertyChanged += new PropertyChangedEventHandler(sample_PropertyChanged);
            while (true)
            {
                string str = Console.ReadLine();
                int val;
                if (int.TryParse(str, out val))
                    sample.TestValue = val;
            }
        }

        static void sample_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            TestClass sample = (TestClass)sender;
            /*
             * Use expression behind if you have more the one property instead sample.TestValue
             * typeof(TestClass).GetProperty(e.PropertyName).GetValue(sample, null)*/
            Console.WriteLine("Value of property {0} was changed! New value is {1}", e.PropertyName, sample.TestValue);
        }
    }

    public class TestClass : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        int testValue = 0;
        public int TestValue
        {
            get { return testValue; }
            set
            {
                testValue = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("TestValue"));
            }
        }
    }
}
