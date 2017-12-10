using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WpfApplicationDemo
{
    public class LotsOfItems : ObservableCollection<String>
    {
        public LotsOfItems()
        {
            for (int i = 0; i < 10000000; ++i)
            {
                Add("item " + i.ToString());
            }
        }
    }
}
