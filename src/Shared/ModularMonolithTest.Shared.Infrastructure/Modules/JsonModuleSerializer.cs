﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ModularMonolithTest.Shared.Infrastructure.Modules
{
    internal class JsonModuleSerializer : IModuleSerializer
    {
        private static readonly JsonSerializerOptions SerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public byte[] Serialize<T>(T value)
            => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, SerializerOptions));

        public T Deserialize<T>(byte[] value)
            => JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(value), SerializerOptions)!;

        public object Deserialize(byte[] value, Type type)
            => JsonSerializer.Deserialize(Encoding.UTF8.GetString(value), type, SerializerOptions)!;
    }
}
