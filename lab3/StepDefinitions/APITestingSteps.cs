using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace lab3
{
    [Binding]
    public class Create
    {
        private RestClient client;
        private RestRequest request;
        private RestResponse response;
        private string createdUserId;
        private dynamic responseobj;

        [Given(@"the base API URL is ""(.*)""")]
        public void GivenTheBaseApiUrlIs(string baseUrl)
        {
            client = new RestClient(baseUrl);
        }

        [When(@"a POST request is sent to ""(.*)""")]
        public void WhenAPostRequestIsSentTo(string endpoint)
        {
            request = new RestRequest(endpoint, Method.Post);
            var requestData = new
            {
                name = "Mark Mark",
                job = "Developer",
            };

            request.AddJsonBody(requestData);
            response = client.Execute(request);

            responseobj = JsonConvert.DeserializeObject(response.Content);
        }

        [Then(@"the server returns a status code (.*)")]
        public void ThenTheServerReturnsAStatusCode(int expectedStatusCode)
        {
            Assert.AreEqual(expectedStatusCode, (int)response.StatusCode);
        }

        [Then(@"the response body contains information about the new user")]
        public void ThenTheResponseBodyContainsInformationAboutTheNewUser()
        {
            Assert.IsNotNull(response.Content);

            Assert.IsNotNull(responseobj["name"]);
            Assert.IsNotNull(responseobj["job"]);
        }

        [Then(@"the user identifier is not empty")]
        public void ThenTheUserIdentifierIsNotEmpty()
        {
            Assert.IsFalse(string.IsNullOrEmpty((string)responseobj["id"]));
        }
    }

    [Binding]
    public class Read
    {
        private RestClient client;
        private RestRequest request;
        private RestResponse response;

        [Given(@"the base API URL GET is ""(.*)""")]
        public void GivenTheBaseApiUrlGETIs(string baseUrl)
        {
            client = new RestClient(baseUrl);
        }

        [When(@"a GET request is sent to ""(.*)""")]
        public void WhenAGetRequestIsSentTo(string endpoint)
        {
            request = new RestRequest($"{endpoint}", Method.Get);
            response = client.Execute(request);
        }

        [Then(@"the server returns a status code for get (.*)")]
        public void ThenTheServerReturnsAStatusCodeForGet(int expectedStatusCode)
        {
            Assert.AreEqual(expectedStatusCode, (int)response.StatusCode);
        }
    }
    [Binding]
    public class update
    {
        private RestClient client;
        private RestRequest request;
        private RestResponse response;
        private string endpoint;

        [Given(@"the base API URL PUT is ""(.*)""")]
        public void GivenTheBaseApiUrlPUTIs(string baseUrl)
        {
            client = new RestClient(baseUrl);
        }

        [When(@"a PUT request is sent to ""(.*)""")]
        public void WhenAGetRequestIsSentTo(string endpoint)
        {
            endpoint = endpoint;
            request = new RestRequest(endpoint, Method.Put);
            var requestData = new
            {
                name = "Luca Willson",
                job = "Senior developer",
            };

            request.AddJsonBody(requestData);
            response = client.Execute(request);
        }
        [Then(@"the server returns a status code for put (.*)")]
        public void ThenTheServerReturnsAStatusCodeForPut(int expectedStatusCode)
        {
            Assert.AreEqual(expectedStatusCode, (int)response.StatusCode);
        }
    }
    [Binding]
    public class delete
    {
        private RestClient client;
        private RestRequest request;
        private RestResponse response;
        private string endpoint;

        [Given(@"the base API URL DEL is ""(.*)""")]
        public void GivenTheBaseApiUrlDELIs(string baseUrl)
        {
            client = new RestClient(baseUrl);
        }

        [When(@"a DELETE request is sent to ""(.*)""")]
        public void WhenADELETERequestIsSentTo(string endpoint)
        {
            RestRequest requestPOST = new RestRequest("", Method.Post);
            var requestDataPOST = new
            {
                name = "Mark Mark",
                job = "Developer",
                id = "666",
            };

            requestPOST.AddJsonBody(requestDataPOST);
            client.Execute(requestPOST);

            endpoint = endpoint;
            request = new RestRequest(endpoint, Method.Delete);
            response = client.Execute(request);
        }
        [Then(@"the server returns a status code for del (.*)")]
        public void ThenTheServerReturnsAStatusCodeForDel(int expectedStatusCode)
        {
            Assert.AreEqual(expectedStatusCode, (int)response.StatusCode);


            RestRequest requestcheck = new RestRequest($"{endpoint}", Method.Get);
            RestResponse responsecheck = client.Execute(requestcheck);

            Assert.AreEqual(404, (int)responsecheck.StatusCode);
        }
    }
}