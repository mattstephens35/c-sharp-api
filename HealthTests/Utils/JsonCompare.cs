using System;
namespace HealthTests.Utils
{
	public class JsonCompare
	{
        public static void AreEqualByJson(object expected, object? actual)
        {
            var expectedJson = Newtonsoft.Json.JsonConvert.SerializeObject(expected);

            var actualJson = Newtonsoft.Json.JsonConvert.SerializeObject(actual);

            Assert.That(actualJson, Is.EqualTo(expectedJson));
        }
    }
}

