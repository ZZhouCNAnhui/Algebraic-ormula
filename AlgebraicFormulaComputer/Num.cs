using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraicFormulaComputer
{
    [Serializable]
    class Num
    {
        public Num(float v)
        {
            V = v;
        }

        public float V { get; set; }
        

        public static Num operator +(Num l,Num r)
        {
            var rel = F.DeepCopyByBin(l);
            rel.V += r.V;
            return rel;

        }
        public static Num operator -(Num l)
        {
            var rel = F.DeepCopyByBin(l);
            rel.V = -rel.V;
            return rel;

        }
    }
}
