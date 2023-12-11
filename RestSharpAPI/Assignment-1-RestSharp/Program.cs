using Newtonsoft.Json.Linq;
using RestSharp;

string baseUrl = "https://jsonplaceholder.typicode.com/";
var client = new RestClient(baseUrl);

//Modularized code

GetAllUsers(client);
CreateUser(client);
UpdateUser(client);

DeleteUser(client);
GetSingleUser(client);

//GET All Users
static void GetAllUsers(RestClient client)
{
    var getUserRequest = new RestRequest("posts", Method.Get);
    getUserRequest.AddQueryParameter("page", "1"); //Adding Query Parameter

    var getUserResponse = client.Execute(getUserRequest);
    Console.WriteLine("GET Response: \n" + getUserResponse.Content);
}

//POST
static void CreateUser(RestClient client)
{
    var createUserRequest = new RestRequest("posts", Method.Post);
    createUserRequest.AddHeader("Content-Type", "application/json");
    createUserRequest.AddJsonBody(new { userId = "2", title = "Wings Of Fire",body="Autobiography of A P J Abdulkalam" });

    var createUserResponse = client.Execute(createUserRequest);
    Console.WriteLine("POST Response: \n" + createUserResponse.Content);
}

//PUT

static void UpdateUser(RestClient client)
{
    var updateUserRequest = new RestRequest("posts/2", Method.Put);
    updateUserRequest.AddHeader("Content-Type", "application/json");
    updateUserRequest.AddJsonBody(new { userId = "3", title = "Fire",body="BTS" });

    var updateUserResponse = client.Execute(updateUserRequest);
    Console.WriteLine("PUT Response: \n" + updateUserResponse.Content);
}


//DELETE
static void DeleteUser(RestClient client)
{
    var deleteUserRequest = new RestRequest("posts/2", Method.Delete);
    var deleteUserResponse = client.Execute(deleteUserRequest);
    Console.WriteLine("DELETE Response: \n" + deleteUserResponse.Content);
}

//GET Single User
static void GetSingleUser(RestClient client)
{
    var getUserRequest = new RestRequest("posts/5", Method.Get);

    var getUserResponse = client.Execute(getUserRequest);
    if (getUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        //Parse Json response content
        JObject? userJson = JObject.Parse(getUserResponse.Content);

        string? userId = userJson["userId"].ToString();
        string? title = userJson["title"].ToString();
        string? body = userJson["body"].ToString();

        Console.WriteLine($"title and body: {userId} {title}  {body}");
    }
    else
    {
        Console.WriteLine($"Error: {getUserResponse.ErrorMessage}");
    }
}
