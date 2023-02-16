using System.Collections.Generic;
using SoftApi.Models;
namespace SoftApiTest.MockData;

public class PolicyMockData{
    
    public static List<Policy> GetAllPolicies(){
        return new List<Policy>{
            new Policy{
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
            },
            new Policy{
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
            }
        };
    }

    public static List<Policy> GetPolicyById(){
        return new List<Policy>{
            new Policy{
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
            }
        };
    }

}