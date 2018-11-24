using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraicFormulaComputer
{
    [Serializable]
    class Terms
    {
        public Terms() { terms = new List<Term>(); }
        public Terms(Term[] t) { terms = t.ToList(); }
        public Terms(Term t) { terms = new List<Term>() { t }; }
        public List<Term> terms { get; set; }

        public Term this[int i]
        {
            get
            {
                return terms[i];
            }
            set
            {
                terms[i] = value;
            }
        }
        public Terms this[char c]
        {
            get
            {
                var Terms = F.DeepCopyByBin(this);
                for (int i = 0; i < Terms.terms.Count; i++)
                {
                    for (int j = i; j < Terms.terms.Count; j++)
                    {
                        var pj = 0f;
                        var pi = 0f;
                        if (Terms.terms[j].Words.ContainsKey(c))
                            pj = Terms.terms[j].Words[c].P;
                        if (Terms.terms[i].Words.ContainsKey(c))
                            pi = Terms.terms[i].Words[c].P;
                        if (pj > pi)
                        {
                            var h = Terms.terms[i];
                            Terms.terms[i] = Terms.terms[j];
                            Terms.terms[j] = h;
                        }
                    }
                }
                return Terms;

            }
        }

        public void Merge()
        {
            List<List<Term>> tes = new List<List<Term>>();

            foreach (var t in terms)
            {
                bool ishas = false;
                foreach (var lis in tes)
                {
                    if (lis[0].CheakWE(t))
                    {
                        ishas = true;
                        lis.Add(t);
                    }
                }
                if(ishas==false)
                {
                    tes.Add(new List<Term>() { t });
                }
            }

            List<Term> newtes = new List<Term>();
            foreach (var item in tes)
                newtes.Add(Term.addall(item.ToArray()));
            terms = newtes;

        }

        public static Terms operator*(Terms r,Term l)
        {
            var rel = F.DeepCopyByBin(r);
            for (int i = 0; i < rel.terms.Count; i++)
                rel.terms[i] *= l;
            return rel;
        }
        public static Terms operator *(Terms r, Terms l)
        {
            Terms rel = new Terms();

            foreach (var tr in r.terms)
            {
                foreach (var tl in l.terms)
                {
                    rel.terms.Add(tl * tr);
                }
            }
            return rel;
        }
        public static Terms operator +(Terms r, Term l)
        {
            var rel = F.DeepCopyByBin(r);

            rel.terms.Add(l);
            return rel;
        }
        public static Terms operator +(Terms r, Terms l)
        {
            var rel = F.DeepCopyByBin(r);
            foreach (var item in l.terms)
                rel.terms.Add(item);
            return rel;
        }
        public static Terms operator -(Terms r, Terms l)
        {
            var rel = F.DeepCopyByBin(r);
            foreach (var item in l.terms)
                rel.terms.Add(-item);
            return rel;
        }
        public static Terms operator -(Terms r, Term l)
        {
            var rel = F.DeepCopyByBin(r);
            rel.terms.Add(-l);
            return rel;
        }
        public static Terms operator -(Terms r)
        {
            var rel = F.DeepCopyByBin(r);
            for (int i = 0; i < rel.terms.Count; i++)
            {
                rel.terms[i] = -rel.terms[i];
            }
            return rel;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < terms.Count; i++)
            {
                if(i == 0)
                    s += terms[i].ToString();
                else
                    s+= terms[i].ToString_s();
            }
            return s;
        }

    }
}
