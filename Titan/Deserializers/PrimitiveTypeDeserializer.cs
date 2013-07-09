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
        private Dictionary<Type, Func<XObject, object>> primitives;

        public PrimitiveTypeDeserializer()
        {
            primitives = new Dictionary<Type, Func<XObject, object>>();
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

        public bool CanHandle(Type type, XObject xobject)
        {
            return primitives.ContainsKey(NormalizeType(type));
        }

        public object Handle(Type type, XObject xobject, Metadata metadata)
        {
            Type normalized = NormalizeType(type);
            return primitives[normalized](xobject);
        }

        private Type NormalizeType(Type type)
        {
            if (type.IsNullable())
            {
                return type.GetGenericArguments()[0];
            }
            return type;
        }

        private object Boolean(XObject xobject)
        {
            bool value;
            if (bool.TryParse(xobject.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object Double(XObject xobject)
        {
            double value;
            if (double.TryParse(xobject.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object Single(XObject xobject)
        {
            float value;
            if (float.TryParse(xobject.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object Int32(XObject xobject)
        {
            int value;
            if (int.TryParse(xobject.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object Int64(XObject xobject)
        {
            long value;
            if (long.TryParse(xobject.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object UInt32(XObject xobject)
        {
            uint value;
            if (uint.TryParse(xobject.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object UInt64(XObject xobject)
        {
            ulong value;
            if (ulong.TryParse(xobject.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object Byte(XObject xobject)
        {
            ulong value;
            if (ulong.TryParse(xobject.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object SByte(XObject xobject)
        {
            ulong value;
            if (ulong.TryParse(xobject.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object Int16(XObject xobject)
        {
            ulong value;
            if (ulong.TryParse(xobject.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object UInt16(XObject xobject)
        {
            ulong value;
            if (ulong.TryParse(xobject.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object Char(XObject xobject)
        {
            ulong value;
            if (ulong.TryParse(xobject.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object TimeSpan(XObject xobject)
        {
            TimeSpan value;
            if (System.TimeSpan.TryParse(xobject.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object String(XObject xobject)
        {
            return xobject.GetValue();
        }

        private object Guid(XObject xobject)
        {
            Guid value;
            if (System.Guid.TryParse(xobject.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object Decimal(XObject xobject)
        {
            decimal value;
            if (decimal.TryParse(xobject.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object DateTimeOffset(XObject xobject)
        {
            DateTimeOffset value;
            if (System.DateTimeOffset.TryParse(xobject.GetValue(), out value)) return value;
            throw new FormatException();
        }

        private object DateTime(XObject xobject)
        {
            DateTime value;
            if (System.DateTime.TryParse(xobject.GetValue(), out value)) return value;
            throw new FormatException();
        }

    }
}
