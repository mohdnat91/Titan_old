using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Utilities;

namespace Titan.Deserializers
{
    internal class DirectoryDeserializer : ITypeDeserializer
    {
        public bool CanHandle(DeserializationRequest requset)
        {
            return requset.TargetType == typeof(DirectoryInfo);
        }

        public object Handle(DeserializationRequest requset)
        {
            string path = requset.Root.GetValue();
            return new DirectoryInfo(path);
        }
    }
}
