using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace SKT.Component
{
    public class Class1
    {
        public Class1() {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
        }
    }
}
