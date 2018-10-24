using System;
using System.Collections.Generic;
using System.Threading;
using Thinktecture.IdentityModel.Client;
using ThreeShape.Identity.Data.Client;
using ThreeShape.Identity.Data.Client.Users;
using ThreeShape.Identity.Data.Dto;

namespace ThreeShape.Identity.Samples.CreateFull
{
    class CreateCompanyAndUser
    {
        static void Main(string[] args)
        {

            //Get the token
            var client = new OAuth2Client(new Uri("https://test-identity.3shape.com/connect/token"), "3Shape.eCommerce.Creator", "secret");
            var tokenResponse = client.RequestClientCredentialsAsync("data.users.full_access data.companies.full_access").Result;
            var token = tokenResponse.AccessToken;


            // Get the data api client and getting a users
            var discoveryClient = new DiscoveryClient("https://test-data.identity.3shape.com");
            var dataClient = new IdentityDataClient(discoveryClient.GetAsync(CancellationToken.None).Result);

            var id = Guid.NewGuid().ToString();
            //Create User
            CreateUser user = new CreateUser
            {
                Email = $"test_ecom{id}@test.com",
                FirstName = "FirstName",
                LastName = "LastName",
                Password = "password#123",
                Username = $"test_ecom{id}@test.com",
                Roles = new List<string>()
                
            };
            var userId = dataClient.Users.CreateAsync(token, user).Result;

            var usertokenResponse = client.RequestResourceOwnerPasswordAsync(user.Email, user.Password, "data.users.full_access data.companies.full_access").Result;

            //Set code for user, lives for 48hours usable only once
            // usable in: https://test-identity.3shape.com/account/activate?code=whatevercode
            dataClient.Users.SetCodeAsync(token, userId, "whatevercode");

            //Create Company
            Company company = new Company
            {
                Address = new Address
                {
                    AddressLine = "Street without name",
                    City = "City",
                    Country = "DK",
                    PostalCode = "1602",
                    State = ""
                },
                Name="Company without name",
                PhoneNumber = "054522354",
                Website = "",
                Roles = new List<Role> { new Role { Name = "3Shape.Reseller" } } 
            };

            var companyId = dataClient.Companies.CreateAsync(usertokenResponse.AccessToken, company,userId).Result;
            
            Console.ReadLine();
        }
    }
}
