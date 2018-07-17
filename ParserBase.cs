using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterNew
{
    abstract class ParserBase
    {
        protected readonly float[,] currencyTable = new float[2, 4];

        public float [,] CurrencyTable
        {
            get { return currencyTable; }
        }
       
        public abstract string ParserName { get; }
    }
 } 

