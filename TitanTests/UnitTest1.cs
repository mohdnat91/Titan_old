using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Titan;
using Titan.Attributes;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace TitanTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestBasicTypeDeserialization()
        {
            string xml = @"<int>10.5</int>";
            XmlDeserializer d = new XmlDeserializer(xml);
            double output = d.Deserialize<double>();
            Assert.IsNotNull(output);
            Assert.AreEqual(output, 10.5);
        }

        [TestMethod]
        public void TestComplexTypeDeserialization()
        {
            string xml = @"<person><name>Mohammad</name><old>25</old><date>2-5-1999</date></person>";
            XmlDeserializer d = new XmlDeserializer(xml);
            aPerson person = d.Deserialize<aPerson>();
            Assert.IsNotNull(person);
            Assert.AreEqual("Mohammad", person.Name);
            Assert.AreEqual(25, person.Age);
        }

        [TestMethod]
        public void TestTagNameConvention()
        {
            string xml = @"<p><name>sss</name><age>11</age><person><name>Mohammad</name><old>25</old><date>2-5-1999</date></person></p>";
            XmlDeserializer d = new XmlDeserializer(xml);
            ConventionPerson person = d.Deserialize<ConventionPerson>();
            int s = person.Age;
        }

        [TestMethod]
        public void TestCollectionDeserializtion()
        {
            string xml = @"<list><int>1</int><int>2</int></list>";
            XmlDeserializer d = new XmlDeserializer(xml);
            List<int> list = d.Deserialize<List<int>>();
            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void TestGenericDeserialization()
        {
            string xml = @"<generic><prop>5/30/2013</prop></generic>";
            XmlDeserializer d = new XmlDeserializer(xml);
            Generic<DateTime> inst = d.Deserialize<Generic<DateTime>>();
            Assert.IsNotNull(inst);
            Assert.AreEqual(DateTime.Parse("5/30/2013"), inst.Prop);
        }

        [TestMethod]
        public void TestEnumDeserialization()
        {
            string xml = @"<enum><val>B</val></enum>";
            XmlDeserializer d = new XmlDeserializer(xml);
            TestEnum val = d.Deserialize<TestEnum>();
            Assert.AreEqual(TestEnum.B, val);
        }

        [TestMethod]
        public void TestInterfaceDeserialization()
        {
            string xml = @"<list><int>1</int><int>2</int></list>";
            XmlDeserializer d = new XmlDeserializer(xml);
            IEnumerable<int> list = d.Deserialize<IEnumerable<int>>();
            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.Count());
        }

        [TestMethod]
        public void TestGenericParent()
        {
            string xml = @"<list><int>1</int><int>2</int></list>";
            XmlDeserializer d = new XmlDeserializer(xml);
            IntCollection list = d.Deserialize<IntCollection>();
            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.Count());
        }

        [TestMethod]
        public void TestAttributeDeserialization()
        {
            string xml = "<int value=\"10\" />";
            XmlDeserializer d = new XmlDeserializer(xml);
            AttTest output = d.Deserialize<AttTest>();
            Assert.IsNotNull(output);
            Assert.AreEqual(output.AttProp, 10);
        }

        [TestMethod]
        public void TestNestedCollectionDeserializtion()
        {
            string xml = @"<lists><list><int>1</int><int>2</int></list><list><int>1</int><int>4</int></list></lists>";
            XmlDeserializer d = new XmlDeserializer(xml);
            List<int[]> list = d.Deserialize<List<int[]>>();
            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void TestArrayDeserializtion()
        {
            string xml = @"<list><int>1</int><int>20</int></list>";
            XmlDeserializer d = new XmlDeserializer(xml);
            int[] list = d.Deserialize<int[]>();
            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.Length);
        }

        [TestMethod]
        public void TestDictionaryDeserializtion()
        {
            string xml = "<list><int key=\"a\">100</int><int key=\"b\">20</int></list>";
            XmlDeserializer d = new XmlDeserializer(xml);
            Dictionary<string, int> list = d.Deserialize<Dictionary<string, int>>();
            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void TestAttributedDictionaryDeserializtion()
        {
            string xml = "<root><dict><int><key>a</key><value>100</value></int><int><key>b</key><value>20</value></int></dict></root>";
            XmlDeserializer d = new XmlDeserializer(xml);
            TTT list = d.Deserialize<TTT>();
            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.dict.Count);
        }

        [TestMethod]
        public void TestNullable()
        {
            string xml = "<ints><prop1>1</prop1><prop3>43</prop3></ints>";
            XmlDeserializer d = new XmlDeserializer(xml);
            NUL list = d.Deserialize<NUL>();
            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Prop1);
            Assert.AreEqual(null, list.Prop2);
            Assert.AreEqual(43, list.Prop3);
        }

        [TestMethod]
        public void TestComplex()
        {
            string xml = (new StreamReader("XMLFile1.xml")).ReadToEnd();
            XmlDeserializer d = new XmlDeserializer(xml);
            List<Person> people = d.Deserialize<List<Person>>();
            Assert.AreEqual(2, people.Count);
        }

    }

    internal class NUL
    {
        public int Prop1 { get; set; }
        public int? Prop2 { get; set; }
        public int Prop3 { get; set; }
    }

    internal class TTT
    {
        [XmlDictionaryKey(Name = "key", NodeType = XmlNodeType.Element)]
        [XmlDictionaryValue(Name = "value", NodeType = XmlNodeType.Element)]
        public Dictionary<string,int> dict { get; set; }
    }

    internal class IntCollection : List<int>
    {

    }

    internal enum TestEnum
    {
        A,
        B
    }

    internal class AttTest
    {
        [XmlAttribute("value")]
        public int AttProp { get; set; }
    }

    internal class Generic<T>
    {
        public T Prop { get; set; }
    }

    internal class aPerson {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("old")]
        public int Age { get; set; }

        [XmlElement("date")]
        public DateTime DoB { get; set; }
    }

    internal class ConventionPerson
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public aPerson person { get; set; }
    }

    public class Person
    {
        [XmlAttribute("guid")]
        public Guid guid { get; set; }
        public string Name { get; set; }
        public DateTime DoB { get; set; }
        public long Height { get; set; }    
        public int Weight { get; set; }
        public bool IsMarried { get; set; }
        [XmlElement("ETA")]
        public TimeSpan ETA { get; set; }
        [XmlElement("address")]
        public Address AddressInfo { get; set; }

        [XmlCollectionItem(Name="inst", NodeType=XmlNodeType.Element)]
        public IEnumerable<string> Education { get; set; }

        [XmlDictionaryKey(Name = "name", NodeType = XmlNodeType.Attribute)]
        [XmlDictionaryValue(Name = "value", NodeType = XmlNodeType.Attribute)]
        public Dictionary<string,decimal> Possitions { get; set; }

    }

    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
    }
}
