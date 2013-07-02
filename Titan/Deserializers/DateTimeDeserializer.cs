﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Utilities;

namespace Titan.Deserializers
{
    internal class DateTimeDeserializer : ITypeDeserializer
    {
        public bool CanHandle(DeserializationRequest requset)
        {
            return requset.TargetType == typeof(DateTime) || requset.TargetType == typeof(DateTime?);
        }

        public object Handle(DeserializationRequest requset)
        {
            DateTime value;
            if (DateTime.TryParse(requset.Root.GetValue(), out value)) return value;
            if (requset.TargetType.IsNullable())
            {
                return null;
            }
            else
            {
                throw new InvalidCastException();
            }
        }
    }
}