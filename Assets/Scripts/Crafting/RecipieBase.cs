using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crafting
{
    public class RecipieBase<I,O>
    {
        public I input;
        public O ouput;

        public bool canMake(I inputs)
        {
            return input.Equals(inputs);
        }
    }
}
