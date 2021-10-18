using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;

namespace RestAssured_Testes
{
    public class Tests
    {
        private const string BASE_URL = "https://616c320737f997001745d589.mockapi.io/";

        [Test]
        public void testGetNameSuccess()
        {
            // arrange
            RestClient client = new RestClient(BASE_URL);
            RestRequest request =
                new RestRequest("/student/1", Method.GET);

            // act
            IRestResponse response = client.Execute(request);
            StudentResponse studentResponse = new JsonDeserializer().Deserialize<StudentResponse>(response);

            // assert
            Assert.That(
                studentResponse.name,
                Is.EqualTo("name 1")
            );
        }

        [Test]
        public void testFailure()
        {
            // arrange
            RestClient client = new RestClient(BASE_URL);
            RestRequest request =
                new RestRequest("/student/-1", Method.GET);

            // act
            IRestResponse response = client.Execute(request);

            // assert
            Assert.That(
                  response.StatusCode.ToString(),
                  Is.EqualTo("NotFound")
              );
            // Failure test
        }

        [Test]
        public void TestFailureBreaking()
        {
            // arrange
            RestClient client = new RestClient(BASE_URL);
            RestRequest request =
                new RestRequest("/student/-1", Method.GET);

            // act
            IRestResponse response = client.Execute(request);

            // assert
            Assert.That(
                  response.StatusCode.ToString(),
                  Is.EqualTo("BadRequest")
              );
            // This test is purposely breaking!!
            // Failure test failing
        }
    }
}