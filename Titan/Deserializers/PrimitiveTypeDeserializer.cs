using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titan.Utilities;

namespace Titan.Deserializers
{
    internal class PrimitiveTypeDeserializer : ITypeDeserializer
    {
        private Dictionary<Type, Func<DeserializationRequest, object>> primitives;

        public PrimitiveTypeDeserializer()
        {
            primitives = new Dictionary<Type, Func<DeserializationRequest, object>>();
            primitives.Add(typeof(bool), Boolean);
            primitives.Add(typeof(byte), Byte);
            primitives.Add(typeof(sbyte), SByte);
            primitives.Add(typeof(short), Int16);
            primitives.Add(typeof(ushort), UInt16);
            primitives.Add(typeof(int), Int32);
            primitives.Add(typeof(uint), UInt32);
            primitives.Add(typeof(long), Int64);
            primitives.Add(typeof(ulong), UInt64);
            primitives.Add(typeof(float), Single);
            primitives.Add(typeof(double), Double);
            primitives.Add(typeof(string), String);
            primitives.Add(typeof(decimal), Decimal);
            primitives.Add(typeof(DateTime), DateTime);
            primitives.Add(typeof(DateTimeOffset), DateTimeOffset);
            primitives.Add(typeof(TimeSpan), TimeSpan);
            primitives.Add(typeof(Guid), Guid);
        }

        public bool CanHandle(DeserializationRequest request)
        {
            return primitives.ContainsKey(NormalizeType(request.TargetType));
        }

        public object Handle(DeserializationRequest request)
        {
            Type type = NormalizeType(request.TargetType);
            return primitives[type](request);
        }

        private Type NormalizeType(Type type)
        {
            if (type.IsNullable())
            {
                return type.GetGenericArguments()[0];
            }
            return type;
        }

        private object Boolean(DeserializationRequest requset)
        {
            bool value;
            if (bool.TryParse(requset.Root.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object Double(DeserializationRequest requset)
        {
            double value;
            if (double.TryParse(requset.Root.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object Single(DeserializationRequest requset)
        {
            float value;
            if (float.TryParse(requset.Root.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object Int32(DeserializationRequest requset)
        {
            int value;
            if (int.TryParse(requset.Root.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object Int64(DeserializationRequest requset)
        {
            long value;
            if (long.TryParse(requset.Root.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object UInt32(DeserializationRequest requset)
        {
            uint value;
            if (uint.TryParse(requset.Root.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object UInt64(DeserializationRequest requset)
        {
            ulong value;
            if (ulong.TryParse(requset.Root.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object Byte(DeserializationRequest requset)
        {
            ulong value;
            if (ulong.TryParse(requset.Root.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object SByte(DeserializationRequest requset)
        {
            ulong value;
            if (ulong.TryParse(requset.Root.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object Int16(DeserializationRequest requset)
        {
            ulong value;
            if (ulong.TryParse(requset.Root.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object UInt16(DeserializationRequest requset)
        {
            ulong value;
            if (ulong.TryParse(requset.Root.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object Char(DeserializationRequest requset)
        {
            ulong value;
            if (ulong.TryParse(requset.Root.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object TimeSpan(DeserializationRequest requset)
        {
            TimeSpan value;
            if (System.TimeSpan.TryParse(requset.Root.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object String(DeserializationRequest request)
        {
            return request.Root.GetValue();
        }

        private object Guid(DeserializationRequest requset)
        {
            Guid value;
            if (System.Guid.TryParse(requset.Root.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object Decimal(DeserializationRequest requset)
        {
            decimal value;
            if (decimal.TryParse(requset.Root.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object DateTimeOffset(DeserializationRequest requset)
        {
            DateTimeOffset value;
            if (System.DateTimeOffset.TryParse(requset.Root.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object DateTime(DeserializationRequest requset)
        {
            DateTime value;
            if (System.DateTime.TryParse(requset.Root.GetValue(), out value)) return value;
            throw new FormatException();
        }

    }
}
