using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Health;
using HealthTests.ApiRequests;
using HealthTests.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HealthTests;

public class Tests
{
    private Requests requests;

    [SetUp]
    public void Setup()
    {
        requests = new Requests();
    }

    [Test]
    public async Task PostHealthRecordWithValidValues()
    {
        var testHealthRecord = new TestHealthRecord
        {
            RecordName = "Main Test",
            IsComplete = true
        };

        var response = await requests.PostRecord("/healthrecords", testHealthRecord);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created), $"Status code was not {HttpStatusCode.Created}");

        var json = response.Content.ReadAsStringAsync().Result;
        var actualHealthRecord = JsonConvert.DeserializeObject<TestHealthRecord>(json);
        testHealthRecord.Id = actualHealthRecord.Id;

        JsonCompare.AreEqualByJson(testHealthRecord, actualHealthRecord);
    }

    [Test]
    public async Task GetHealthRecordWithValidValues()
    {
        var testHealthRecord = new TestHealthRecord
        {
            RecordName = "Get Health Record Test",
            IsComplete = true
        };

        // Create Record
        var postResponse = await requests.PostRecord("/healthrecords", testHealthRecord);

        Assert.That(postResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created), $"Status code was not {HttpStatusCode.Created}");
        var postJson = postResponse.Content.ReadAsStringAsync().Result;
        var postHealthRecord = JsonConvert.DeserializeObject<TestHealthRecord>(postJson);
        testHealthRecord.Id = postHealthRecord.Id;

        // Get Record
        var getResponse = await requests.GetRecord("/healthrecords");
        Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), $"Status code was not {HttpStatusCode.OK}");


        var getJson = getResponse.Content.ReadAsStringAsync().Result;
        var myobjList = JsonConvert.DeserializeObject<List<TestHealthRecord>>(getJson);
        var actualHealthRecord = myobjList.Where(x => x.Id!.Equals(testHealthRecord.Id)).FirstOrDefault();


        JsonCompare.AreEqualByJson(testHealthRecord, actualHealthRecord);
    }
}
