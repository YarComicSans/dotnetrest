using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit.Sdk;

namespace Rest.UnitTests
{
    public class ParserTestsDataAttribute : DataAttribute
    {
        private readonly string[] _fileNames;

        public ParserTestsDataAttribute(string filename)
            : this(new string[] { filename }) { }

        public ParserTestsDataAttribute(string[] fileNames)
        {
            _fileNames = fileNames;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            for(var i = 0; i < _fileNames.Length; i++)
            {
                var xml = ReadXmlData(_fileNames[i]);
                yield return new object[] { xml };
            }
        }

        public static string ReadXmlData(string resourceName)
        {
            var resourcePath = string.Format($"Rest.UnitTests.TestData.Xmls.{resourceName}");
            using(var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath))
            {
                if(stream == null)
                {
                    throw new InvalidOperationException($"Could not load manifest resource {resourceName} stream.");
                }
                using(var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
