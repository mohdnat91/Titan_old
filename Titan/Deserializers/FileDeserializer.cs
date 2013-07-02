using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titan.Utilities;

namespace Titan.Deserializers
{
    internal class FileDeserializer : ITypeDeserializer
    {
        public bool CanHandle(DeserializationRequest requset)
        {
            return requset.TargetType == typeof(FileInfo);
        }

        public object Handle(DeserializationRequest requset)
        {
            string path = requset.Root.GetValue();     
            return new FileInfo(path);
        }
    }
}
