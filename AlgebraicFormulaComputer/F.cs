using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Collections;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace AlgebraicFormulaComputer
{
    static class F
    {
        public static T DeepCopyByBin<T>(T obj)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            stream.Position = 0;
            return (T)formatter.Deserialize(stream);
        }



    }
}
