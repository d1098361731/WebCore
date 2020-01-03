using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace Common
{
    public static class AppSettingsHelpper
    {
        public static IConfiguration Configuration  {   get;set;  }
        public static string ConnectionString { get; set; }
        
        public static string  DbType { get; set; }

        public static string SecurityName
        {
            get
            {
                return @"Bearer ";
            }
        }
    }
}
