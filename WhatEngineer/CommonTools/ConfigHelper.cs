using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatEngineer.CommonTools
{
    public class ConfigHelper
    {
        private static IConfiguration Config = null; //ConfigurationRoot
        public static void Init(IConfiguration Configuration)
        {
            Config = Configuration;
        }

        public static string Get(string Key)
        {
            IConfigurationSection ISection = Config.GetSection(Key);
            return ISection.Value;
        }

        public static void Set(string Key, string Value)
        {
            IConfigurationSection ISection = Config.GetSection(Key);
            ISection.Value = Value;
        }
    }
}
