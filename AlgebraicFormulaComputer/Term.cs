using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraicFormulaComputer
{
    [Serializable]
    class Term
    {
        public Term(float num,char[] cs)
        {
            Num = new Num(num);
            Words = new Dictionary<char, Word>();
            if (cs == null || cs.Length == 0) return;
            foreach (var item in cs)
                Words.Add(item, new Word(item));
        }
        public Term(Word[] words, float num=1)
        {
            Num = new Num(num);
            Words = new Dictionary<char, Word>();
            if (words == null || words.Length == 0) return;
            foreach (var item in words)
                Words.Add(item.N, item);
        }

        public Num Num { get; set; }
        public Dictionary<char, Word> Words { get; set; }

        public bool isNumTerm { get { return (Words == null || Words.Count == 0); } }
        public float T
        {
            get
            {
                float t = 0;
                foreach (var item in Words)
                    t += item.Value.P;
                return t;
            }
        }
        
        public bool CheakWE(Term t)
        {
            foreach (var item in t.Words)
            {
                bool b = false;
                foreach (var s in Words)
                {
                    if (item.Value == s.Value)
                        b = true;
                }
                if (b == false)
                    return false;
            }
            return true;
        }



        public static Terms Adds(params Term[] t)
        {
            return new Terms(t);
        }

        public static Term operator *(Term l, Num r)
        {
            var rel = F.DeepCopyByBin(l);
            rel.Num.V *= r.V;
            return rel;
        }
        public static Term operator*(Term l, Word r)
        {
            var rel = F.DeepCopyByBin(l);
            foreach (var item in rel.Words)
                if (item.Key == r.N)
                {
                    item.Value.P += r.P;
                    return rel;
                }
            rel.Words.Add(r.N, r);
            return rel;
        }
        public static Term operator *(Term l, Term r)
        {
            var rel = l * r.Num;
            foreach (var item in r.Words)
            {
                rel *= item.Value;
            }
            return rel;
        }
        
        public static Term addall(params Term[] t)
        {
            var rel = F.DeepCopyByBin(t[0]);
            for (int i = 1; i < t.Length; i++)
                rel.Num += t[i].Num;
            return rel;
        }

        public static Terms operator +(Term l,Term r)
        {
            return new Terms(new Term[] { l, r });
        }

        public static Term operator -(Term l)
        {
            var rel = F.DeepCopyByBin(l);
            rel.Num = -rel.Num;
            return rel;
        }

        public override string ToString()
        {
            string s = "";
            if (Num.V == 0)
                return s;
            //Console.WriteLine(Num.V);
            if (Num.V == -1)
                s += "-";
            if ((Num.V != 1&&Num.V!=-1) || isNumTerm)
                s += Num.V.ToString();

            foreach (var item in Words)
            {
                s += item.Value.N;
                if(item.Value.P != 1)
                {
                    s += "^";
                    s += item.Value.P.ToString();
                }
            }       
            return s;
        }
        public string ToString_s()
        {
            string s = ToString();
            if (Num.V > 0)
                s = "+" + s;
            return s;
        }


    }
}
