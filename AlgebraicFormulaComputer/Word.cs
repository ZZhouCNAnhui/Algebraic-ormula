using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraicFormulaComputer
{
    [Serializable]
    class Word
    {

        public char N { get; set; }
        public float P { get; set; }

        public Word(char w='x',float p=1)
        {
            P = p;
            N = w;
        }
        
        public static bool operator==(Word l, Word r)
        {
            if (l.N == r.N && l.P == r.P)
                return true;
            else
                return false;
        }
        public static bool operator !=(Word l, Word r)
        {
            return !(l == r);
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
