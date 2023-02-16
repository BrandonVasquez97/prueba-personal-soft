using System.Threading.Tasks;
using SoftApi.Controllers;
using SoftApi.Models;
using SoftApi.Contexts;
using SoftApiTest.MockData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions;
using Moq;
using Xunit;

namespace SoftApiTest.System.Controllers;

public class TestPolicyController{
    // 3 tests que prueban 1. listar todas las polizas, 2. Listar poliza por numero, 3. Login
    [Fact]
    public void TestSoftApi()
    {
        // Given
        List<Policy> value = PolicyMockData.GetAllPolicies();
        
        // When
        var options = new DbContextOptionsBuilder<ConexionSqlServer>()
            .UseInMemoryDatabase(databaseName: "PersonalBD")
            .Options;
        using (var context = new ConexionSqlServer(options)){
            context.Policies.Add(new Policy{
                id= 1,
                name_client= "Arturo Velez",
                dni_client= "43872121",
                date_of_birth= DateTime.Parse("1980-03-05T00:00:00"),
                date_of_start_policy= DateTime.Parse("2020-12-05T15:15:00"),
                coberture_policy= "Daños materiales",
                max_value= 5000000,
                name_of_policy= "Todo Riesgo",
                city_of_client= "Bogota",
                address_of_client= "Cra 22 #132b-60",
                plate= "AXH550",
                model_of_car= "Mazda 3",
                inspection_vehicle= "No",
                date_end_of_policy= DateTime.Parse("2021-12-05T15:15:00")
            });
            context.Policies.Add(new Policy{
                id= 2,
                name_client= "Diana Perez",
                dni_client= "35135135",
                date_of_birth= DateTime.Parse("1985-08-12T00:00:00"),
                date_of_start_policy= DateTime.Parse("2021-11-05T17:15:00"),
                coberture_policy= "Daños materiales",
                max_value= 5000000,
                name_of_policy= "Todo Riesgo",
                city_of_client= "Medellin",
                address_of_client= "Cra 35 #50-60",
                plate= "KFP412",
                model_of_car= "Renault clio",
                inspection_vehicle= "Si",
                date_end_of_policy= DateTime.Parse("2022-11-05T17:15:00")
            });
            context.SaveChanges();
        }
        using (var context = new ConexionSqlServer(options))
        {
            PolicyController policy_controller = new PolicyController(context);
            int id;
            List<Policy> policies = policy_controller.GetAllForTest();
            Assert.Equal(value.Count(), policies.Count());
        }
    }

    [Fact]
    public void TestSoftApi2()
    {
        // Given
        List<Policy> value = PolicyMockData.GetPolicyById();
        
        // When
        var options = new DbContextOptionsBuilder<ConexionSqlServer>()
            .UseInMemoryDatabase(databaseName: "PersonalBD")
            .Options;
        
        using (var context = new ConexionSqlServer(options))
        {
            PolicyController policy_controller = new PolicyController(context);
            int id;
            Policy policies = policy_controller.GetbyIdForTest(id=1);
            var firstPolicyMock = value.First();
            string name_client_mock = firstPolicyMock.name_client;
            string name_client = policies.name_client;
            Assert.Equal(name_client_mock, name_client);
        }
    }

    [Fact]
    public void TestSoftApi3()
    {
        var options = new DbContextOptionsBuilder<ConexionSqlServer>()
            .UseInMemoryDatabase(databaseName: "PersonalBD")
            .Options;
        using (var context = new ConexionSqlServer(options)){
            context.Users.Add(new User{
                id = 1,
                user_name = "bvasquez",
                user_password = "1234",
                user_type = "ADMIN"

            });
            context.SaveChanges();
        }
        using (var context = new ConexionSqlServer(options))
        {
            string user_name = "bvasquez";
            string user_password = "1234";
            UserController2 user_controller = new UserController2(context);
            string result = user_controller.LoginTest(user_name, user_password);
            Assert.Equal("login succeeded", result);
        }
    }
}