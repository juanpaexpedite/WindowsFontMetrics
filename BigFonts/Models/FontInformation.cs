using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFontsMetrics.Models
{
    public class FontInformation : BindableBase
    {
        public FontInformation()
        {

        }

        public FontInformation(string name, string description, int size)
        {
            Name = name;
            Description = description;
            Size = size;
        }

        public string Name { get; set; }
        public string Description { get; set; }

        private int size = 1;
        public int Size
        {
            get { return size; }
            set
            {
                SetProperty(ref size, value);
            }
        }


    }
}
