using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Core.Runtime
{
    public class XmlLoader
    {
        public static Dictionary<string, string> LoadDictionary(string filepath)
        {
            XDocument xmlDoc = XDocument.Load(filepath);
            return xmlDoc.Root
                .Elements()
                .ToDictionary(
                    e => e.Name.LocalName,
                    e => e.Value
                    );
        }
    }
}

