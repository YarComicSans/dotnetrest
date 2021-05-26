using API.Services;
using System;
using Xunit;

namespace Rest.UnitTests
{
    public class ParserTests
    {
        private readonly IDeadlockParser _parser;

        public ParserTests(IDeadlockParser parser) => _parser = parser;

        [Theory]
        [ParserTestsData("CorrectDeadlock.xml")]
        [Trait("Category", "CorrectXmlParsing")]
        public void XmlToDeadlock_ProcessIdIsCorrectlyParsed(string xml)
        {
            var expectedId = "process24cab331848";

            var deadlock = _parser.FromXml(xml);

            Assert.Equal(expectedId, deadlock.ProcessId);
        }

        [Theory]
        [ParserTestsData("CorrectDeadlock.xml")]
        [Trait("Category", "CorrectXmlParsing")]
        public void XmlToDeadlock_CreatedDateIsCorrectlyParsed(string xml)
        {
            var expectedCreatedDate = Convert.ToDateTime("2020-12-07T10:38:22.640");

            var deadlock = _parser.FromXml(xml);

            Assert.Equal(expectedCreatedDate, deadlock.CreatedDate);
        }

        [Theory]
        [ParserTestsData("CorrectDeadlock.xml")]
        [Trait("Category", "CorrectXmlParsing")]
        public void XmlToDeadlock_ReturnedXmlData_IsEqualToProvidedXml(string xml)
        {
            var deadlock = _parser.FromXml(xml);

            Assert.Equal(xml, deadlock.XmlData);
        }

        [Theory]
        [ParserTestsData(new string[] { "DeadlockWithoutIdProperty.xml", "DeadlockIdPropertyWithEmptyValue.xml" })]
        [Trait("Category", "IncorrectXmlParsing")]
        public void XmlToDeadlock_ParsingDeadlockXmlWithoutIdProperty_ReturnsNull(string xml)
        {
            var deadlock = _parser.FromXml(xml);

            Assert.Null(deadlock);
        }

        [Theory]
        [ParserTestsData(new string[] { "DeadlockWithoutLasttranstartedProperty.xml", "DeadlockLasttranstartedPropertyWithEmptyValue.xml" })]
        [Trait("Category", "IncorrectXmlParsing")]
        public void XmlToDeadlock_ParsingDeadlockXmlWithoutLasttranstartedProperty_ReturnsDeadlockWithNullCreatedDate(string xml)
        {
            var deadlock = _parser.FromXml(xml);

            Assert.Null(deadlock.CreatedDate);
        }

        [Theory]
        [ParserTestsData(new string[] { "DeadlockWithNoClosingTag.xml", "DeadlockWithNoOpeningTag.xml", "DeadlockIdPropertyWithNoValue.xml", "DeadlocktLasttranstartedPropertyWithNoValue.xml" })]
        [Trait("Category", "InvalidXmlParsing")]
        public void XmlToDeadlock_ParsingDeadlockFromInvalidXml_ReturnsNull(string xml)
        {
            var deadlock = _parser.FromXml(xml);

            Assert.Null(deadlock);
        }
    }
}