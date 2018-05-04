using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolBlockLibrary
{
    class ModelPopularApp
    {
        public Image Logo { get; set; }
        public string Name { get; set; }
        public ModelPopularApp(Image Logo,string Name)
        {
            this.Logo = Logo;
            this.Name = Name;
        }
    }
    class PopularApp
    {
        List<ModelPopularApp> listPopular = new List<ModelPopularApp>();

    }
}
