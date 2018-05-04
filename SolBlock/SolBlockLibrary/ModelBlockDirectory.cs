using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolBlockLibrary
{
    public class ModelBlockDirectory
    {
        public string IdBlock { get; set; }
        public string PathBlock { get; set; }
        public ModelBlockDirectory(string IdBlock,string PathBlock)
        {
            this.IdBlock = IdBlock;
            this.PathBlock = PathBlock;
        }
    }
}
