using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SoftApi.Contexts;
using SoftApi.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace SoftApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]

    public class PolicyController : ControllerBase
    {
        private readonly ConexionSqlServer context;
        public PolicyController(ConexionSqlServer context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try{
                List<Policy> policies = new List<Policy>();
                policies = context.Policies.ToList();
                return Ok(policies);
                
            }catch{
                return BadRequest("Error");
            }
        }

        public List<Policy> GetAllForTest()
        {
            List<Policy> policies = new List<Policy>();
            policies = context.Policies.ToList();
            return policies;
        }

        public Policy GetbyIdForTest(int id)
        {
            Policy policy = new Policy();
            policy = context.Policies.FirstOrDefault(x => x.id == id);
            //List<Policy> policies = new List<Policy>();
            //policies.Add(policy);
            return policy;
        }

        [HttpGet("GetPolicy/{plate?}")]
        public IActionResult GetByIdOrPlate(string plate = ""){
            if(plate == ""){
                return BadRequest("Error in plate");
            }else{
                List<Policy> policies = new List<Policy>();
                //Usar un SP para filtrar en la BD
                //Se llama de nuevo a la conexion a la BD
                SqlConnection conexion = (SqlConnection) context.Database.GetDbConnection();
                //Se instancia un comando
                SqlCommand comando = conexion.CreateCommand();
                //Se abre la connexion
                conexion.Open();
                //Se coloca que el tipo de comando sera un SP
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                //Se coloca el nombre del SP
                comando.CommandText = "sp_list_policies_plate";
                //Se añade el parametro del SP
                comando.Parameters.Add("@plate", System.Data.SqlDbType.VarChar, 9).Value = plate;
                //Se ejecuta el SP y se encapsula
                SqlDataReader reader = comando.ExecuteReader();
                //Se recorre los resultados
                while(reader.Read()){
                    //Se almacena cada resultado en un nuevo objeto de clase
                    Policy policy = new Policy();
                    policy.id = (int) reader["id"];
                    policy.name_client = (string) reader["name_client"];
                    policy.dni_client = (string) reader["dni_client"];
                    policy.date_of_birth = (DateTime) reader["date_of_birth"];
                    policy.date_of_start_policy = (DateTime) reader["date_of_start_policy"];
                    policy.coberture_policy = (string) reader["coberture_policy"];
                    policy.max_value = (int) reader["max_value"];
                    policy.name_of_policy = (string) reader["name_of_policy"];
                    policy.city_of_client = (string) reader["city_of_client"];
                    policy.address_of_client = (string) reader["address_of_client"];
                    policy.plate = (string) reader["plate"];
                    policy.model_of_car = (string) reader["model_of_car"];
                    policy.inspection_vehicle = (string) reader["inspection_vehicle"];
                    policy.date_end_of_policy = (DateTime) reader["date_end_of_policy"];
                    //Se añade al array
                    policies.Add(policy);
                }
                conexion.Close();
                return Ok(policies);
            }
        }

        public List<Policy> GetByIdOrPlateForTest(string plate = ""){
            if(plate == ""){
                List<Policy> policies = new List<Policy>();
                return policies;
            }else{
                List<Policy> policies = new List<Policy>();
                //Usar un SP para filtrar en la BD
                //Se llama de nuevo a la conexion a la BD
                SqlConnection conexion = (SqlConnection) context.Database.GetDbConnection();
                //Se instancia un comando
                SqlCommand comando = conexion.CreateCommand();
                //Se abre la connexion
                conexion.Open();
                //Se coloca que el tipo de comando sera un SP
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                //Se coloca el nombre del SP
                comando.CommandText = "sp_list_policies_plate";
                //Se añade el parametro del SP
                comando.Parameters.Add("@plate", System.Data.SqlDbType.VarChar, 9).Value = plate;
                //Se ejecuta el SP y se encapsula
                SqlDataReader reader = comando.ExecuteReader();
                //Se recorre los resultados
                while(reader.Read()){
                    //Se almacena cada resultado en un nuevo objeto de clase
                    Policy policy = new Policy();
                    policy.id = (int) reader["id"];
                    policy.name_client = (string) reader["name_client"];
                    policy.dni_client = (string) reader["dni_client"];
                    policy.date_of_birth = (DateTime) reader["date_of_birth"];
                    policy.date_of_start_policy = (DateTime) reader["date_of_start_policy"];
                    policy.coberture_policy = (string) reader["coberture_policy"];
                    policy.max_value = (int) reader["max_value"];
                    policy.name_of_policy = (string) reader["name_of_policy"];
                    policy.city_of_client = (string) reader["city_of_client"];
                    policy.address_of_client = (string) reader["address_of_client"];
                    policy.plate = (string) reader["plate"];
                    policy.model_of_car = (string) reader["model_of_car"];
                    policy.inspection_vehicle = (string) reader["inspection_vehicle"];
                    policy.date_end_of_policy = (DateTime) reader["date_end_of_policy"];
                    //Se añade al array
                    policies.Add(policy);
                }
                conexion.Close();
                return policies;
            }
        }


        [HttpGet("GetPolicy/{id:int?}")]
        public IActionResult GetByIdOrPlate(int id = 0){
            
                if(id == 0){
                    return BadRequest("Error in id");
                }else{
                    List<Policy> policies = new List<Policy>();
                    SqlConnection conexion = (SqlConnection) context.Database.GetDbConnection();
                    SqlCommand comando = conexion.CreateCommand();
                    conexion.Open();
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.CommandText = "sp_list_policies_id";
                    comando.Parameters.Add("@id_policy", System.Data.SqlDbType.Int).Value = id;
                    SqlDataReader reader = comando.ExecuteReader();
                    while(reader.Read()){
                        Policy policy = new Policy();
                        policy.id = (int) reader["id"];
                        policy.name_client = (string) reader["name_client"];
                        policy.dni_client = (string) reader["dni_client"];
                        policy.date_of_birth = (DateTime) reader["date_of_birth"];
                        policy.date_of_start_policy = (DateTime) reader["date_of_start_policy"];
                        policy.coberture_policy = (string) reader["coberture_policy"];
                        policy.max_value = (int) reader["max_value"];
                        policy.name_of_policy = (string) reader["name_of_policy"];
                        policy.city_of_client = (string) reader["city_of_client"];
                        policy.address_of_client = (string) reader["address_of_client"];
                        policy.plate = (string) reader["plate"];
                        policy.model_of_car = (string) reader["model_of_car"];
                        policy.inspection_vehicle = (string) reader["inspection_vehicle"];
                        policy.date_end_of_policy = (DateTime) reader["date_end_of_policy"];
                        policies.Add(policy);
                    }
                    conexion.Close();
                    return Ok(policies);
                }
            

        }

        public List<Policy> GetByIdOrPlateForTest(int id = 0){
            
                if(id == 0){
                    List<Policy> policies = new List<Policy>();
                    return policies;
                }else{
                    List<Policy> policies = new List<Policy>();
                    SqlConnection conexion = (SqlConnection) context.Database.GetDbConnection();
                    SqlCommand comando = conexion.CreateCommand();
                    conexion.Open();
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.CommandText = "sp_list_policies_id";
                    comando.Parameters.Add("@id_policy", System.Data.SqlDbType.Int).Value = id;
                    SqlDataReader reader = comando.ExecuteReader();
                    while(reader.Read()){
                        Policy policy = new Policy();
                        policy.id = (int) reader["id"];
                        policy.name_client = (string) reader["name_client"];
                        policy.dni_client = (string) reader["dni_client"];
                        policy.date_of_birth = (DateTime) reader["date_of_birth"];
                        policy.date_of_start_policy = (DateTime) reader["date_of_start_policy"];
                        policy.coberture_policy = (string) reader["coberture_policy"];
                        policy.max_value = (int) reader["max_value"];
                        policy.name_of_policy = (string) reader["name_of_policy"];
                        policy.city_of_client = (string) reader["city_of_client"];
                        policy.address_of_client = (string) reader["address_of_client"];
                        policy.plate = (string) reader["plate"];
                        policy.model_of_car = (string) reader["model_of_car"];
                        policy.inspection_vehicle = (string) reader["inspection_vehicle"];
                        policy.date_end_of_policy = (DateTime) reader["date_end_of_policy"];
                        policies.Add(policy);
                    }
                    conexion.Close();
                    return policies;
                }
            

        }

        [HttpPost("createPolicy")]
        public async Task<IActionResult> PostRawBufferManual()
        {
            string requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
            dynamic stuff1 = JsonConvert.DeserializeObject(requestBody);
            string name_client = stuff1["nombre_cliente"];
            string dni_client = stuff1["cedula_cliente"];
            string date_of_birth = stuff1["fecha_nacimiento"];
            string name_of_policy = stuff1["tipo_de_poliza"];
            string city_of_client = stuff1["ciudad_de_cliente"];
            string address_of_client = stuff1["direccion_de_cliente"];
            string plate = stuff1["placa"];
            string model_of_car = stuff1["modelo_de_carro"];
            string inspection_vehicle = stuff1["tiene_inspeccion"];
            string coberture_policy;
            int max_value;
            List<Policy> policy_val = GetByIdOrPlateForTest(plate);
            Int32 lenght = policy_val.Count();
            if(lenght > 0){
                Policy element = policy_val.First();
                DateTime date_of_today = DateTime.UtcNow;
                int result = DateTime.Compare(element.date_end_of_policy, date_of_today);
                if(result < 0){
                    return StatusCode(StatusCodes.Status400BadRequest, new { response = "Poliza vencida, se debe proceder a renovar"});
                }else {
                    return StatusCode(StatusCodes.Status400BadRequest, new { response = "Poliza vigente"});
                }
            }
            if(name_of_policy == "Todo Riesgo"){
                coberture_policy = "Daños materiales";
                max_value = 5000000;
            }else if(name_of_policy == "Parcial"){
                coberture_policy = "50% del costo total";
                max_value = 2500000;
            }else{
                return StatusCode(StatusCodes.Status400BadRequest, new { response = "Tipo de poliza no encontrada"});
            }
            List<Policy> policyCreated = CreatePolicy(name_client, dni_client, date_of_birth, name_of_policy, city_of_client, address_of_client, plate, model_of_car, inspection_vehicle, coberture_policy, max_value);
            Policy firstElement = policyCreated.First();

            string date_start = firstElement.date_of_start_policy.ToString("yyyy-MM-dd");
            string date_end = firstElement.date_end_of_policy.ToString("yyyy-MM-dd");
            return StatusCode(StatusCodes.Status200OK, new { response = "Poliza creada", numero_poliza = firstElement.id, fecha_inicio_poliza = date_start, fecha_final_poliza = date_end});
        }

        public List<Policy> CreatePolicy(string name_client, string dni_client, string date_of_birth, string name_of_policy, string city_of_client, string address_of_client, string plate, string model_of_car, string inspection_vehicle, string coberture_policy, int max_value){
            DateTime date_of_birth_date = DateTime.Parse(date_of_birth);
            DateTime date_of_start_policy = DateTime.UtcNow;
            DateTime date_end_of_policy = DateTime.UtcNow.AddYears(1);
            SqlConnection conexion = (SqlConnection) context.Database.GetDbConnection();
            SqlCommand comando = conexion.CreateCommand();
            conexion.Open();
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = "sp_create_policy";
            comando.Parameters.Add("@name_client", System.Data.SqlDbType.VarChar, 100).Value = name_client;
            comando.Parameters.Add("@dni_client", System.Data.SqlDbType.VarChar, 20).Value = dni_client;
            comando.Parameters.Add("@date_of_birth", System.Data.SqlDbType.DateTime).Value = date_of_birth_date;
            comando.Parameters.Add("@date_of_start_policy", System.Data.SqlDbType.DateTime).Value = date_of_start_policy;
            comando.Parameters.Add("@coberture_policy", System.Data.SqlDbType.VarChar, 100).Value = coberture_policy;
            comando.Parameters.Add("@max_value", System.Data.SqlDbType.Int).Value = max_value;
            comando.Parameters.Add("@name_of_policy", System.Data.SqlDbType.VarChar, 100).Value = name_of_policy;
            comando.Parameters.Add("@city_of_client", System.Data.SqlDbType.VarChar, 50).Value = city_of_client;
            comando.Parameters.Add("@address_of_client", System.Data.SqlDbType.VarChar, 50).Value = address_of_client;
            comando.Parameters.Add("@plate", System.Data.SqlDbType.VarChar, 9).Value = plate;
            comando.Parameters.Add("@model_of_car", System.Data.SqlDbType.VarChar, 50).Value = model_of_car;
            comando.Parameters.Add("@inspection_vehicle", System.Data.SqlDbType.VarChar, 10).Value = inspection_vehicle;
            comando.Parameters.Add("@date_end_of_policy", System.Data.SqlDbType.DateTime).Value = date_end_of_policy;
            SqlDataReader reader = comando.ExecuteReader();
            conexion.Close();

            List<Policy> policies = new List<Policy>();
            SqlConnection conexion2 = (SqlConnection) context.Database.GetDbConnection();
            SqlCommand comando2 = conexion2.CreateCommand();
            conexion2.Open();
            comando2.CommandType = System.Data.CommandType.StoredProcedure;
            comando2.CommandText = "sp_list_policies_plate";
            comando2.Parameters.Add("@plate", System.Data.SqlDbType.VarChar, 9).Value = plate;
            SqlDataReader reader2 = comando2.ExecuteReader();
            while(reader2.Read()){
                Policy policy = new Policy();
                policy.id = (int) reader2["id"];
                policy.name_client = (string) reader2["name_client"];
                policy.dni_client = (string) reader2["dni_client"];
                policy.date_of_birth = (DateTime) reader2["date_of_birth"];
                policy.date_of_start_policy = (DateTime) reader2["date_of_start_policy"];
                policy.coberture_policy = (string) reader2["coberture_policy"];
                policy.max_value = (int) reader2["max_value"];
                policy.name_of_policy = (string) reader2["name_of_policy"];
                policy.city_of_client = (string) reader2["city_of_client"];
                policy.address_of_client = (string) reader2["address_of_client"];
                policy.plate = (string) reader2["plate"];
                policy.model_of_car = (string) reader2["model_of_car"];
                policy.inspection_vehicle = (string) reader2["inspection_vehicle"];
                policy.date_end_of_policy = (DateTime) reader2["date_end_of_policy"];
                policies.Add(policy);
            }
            conexion2.Close();
            return policies;
        }


        [HttpPost("renewPolicy")]
        public async Task<IActionResult> PostRawBufferManualRenew()
        {
            string requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
            dynamic stuff1 = JsonConvert.DeserializeObject(requestBody);
            string name_of_policy = stuff1["tipo_de_poliza"];
            string city_of_client = stuff1["ciudad_de_cliente"];
            string address_of_client = stuff1["direccion_de_cliente"];
            string plate = stuff1["placa"];
            string coberture_policy;
            int max_value;
            List<Policy> policy_val = GetByIdOrPlateForTest(plate);
            Int32 lenght = policy_val.Count();
            if(name_of_policy == "Todo Riesgo"){
                coberture_policy = "Daños materiales";
                max_value = 5000000;
            }else if(name_of_policy == "Parcial"){
                coberture_policy = "50% del costo total";
                max_value = 2500000;
            }else{
                return StatusCode(StatusCodes.Status400BadRequest, new { response = "Tipo de poliza no encontrada"});
            }
            if(lenght > 0){
                Policy element = policy_val.First();
                DateTime date_of_today = DateTime.UtcNow;
                int result = DateTime.Compare(element.date_end_of_policy, date_of_today);
                if(result < 0){
                    List<Policy> policyCreated = RenewPolicy(name_of_policy, city_of_client, address_of_client, plate, coberture_policy, max_value);
                    Policy firstElement = policyCreated.First();
                    string date_start = firstElement.date_of_start_policy.ToString("yyyy-MM-dd");
                    string date_end = firstElement.date_end_of_policy.ToString("yyyy-MM-dd");
                    return StatusCode(StatusCodes.Status200OK, new { response = "Poliza renovada", numero_poliza = firstElement.id, fecha_inicio_poliza = date_start, fecha_final_poliza = date_end});
                    
                }else {
                    return StatusCode(StatusCodes.Status400BadRequest, new { response = "Poliza vigente"});
                }
            }else{
                return StatusCode(StatusCodes.Status400BadRequest, new { response = "Poliza no encontrada"});
            }
            
        }

        public List<Policy> RenewPolicy(string name_of_policy, string city_of_client, string address_of_client, string plate,  string coberture_policy, int max_value){
            DateTime date_of_start_policy = DateTime.UtcNow;
            DateTime date_end_of_policy = DateTime.UtcNow.AddYears(1);
            SqlConnection conexion = (SqlConnection) context.Database.GetDbConnection();
            SqlCommand comando = conexion.CreateCommand();
            conexion.Open();
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = "sp_update_policy";
            comando.Parameters.Add("@date_of_start_policy", System.Data.SqlDbType.DateTime).Value = date_of_start_policy;
            comando.Parameters.Add("@coberture_policy", System.Data.SqlDbType.VarChar, 100).Value = coberture_policy;
            comando.Parameters.Add("@max_value", System.Data.SqlDbType.Int).Value = max_value;
            comando.Parameters.Add("@name_of_policy", System.Data.SqlDbType.VarChar, 100).Value = name_of_policy;
            comando.Parameters.Add("@city_of_client", System.Data.SqlDbType.VarChar, 50).Value = city_of_client;
            comando.Parameters.Add("@address_of_client", System.Data.SqlDbType.VarChar, 50).Value = address_of_client;
            comando.Parameters.Add("@plate", System.Data.SqlDbType.VarChar, 9).Value = plate;
            comando.Parameters.Add("@date_end_of_policy", System.Data.SqlDbType.DateTime).Value = date_end_of_policy;
            SqlDataReader reader = comando.ExecuteReader();
            conexion.Close();

            List<Policy> policies = new List<Policy>();
            SqlConnection conexion2 = (SqlConnection) context.Database.GetDbConnection();
            SqlCommand comando2 = conexion2.CreateCommand();
            conexion2.Open();
            comando2.CommandType = System.Data.CommandType.StoredProcedure;
            comando2.CommandText = "sp_list_policies_plate";
            comando2.Parameters.Add("@plate", System.Data.SqlDbType.VarChar, 9).Value = plate;
            SqlDataReader reader2 = comando2.ExecuteReader();
            while(reader2.Read()){
                Policy policy = new Policy();
                policy.id = (int) reader2["id"];
                policy.name_client = (string) reader2["name_client"];
                policy.dni_client = (string) reader2["dni_client"];
                policy.date_of_birth = (DateTime) reader2["date_of_birth"];
                policy.date_of_start_policy = (DateTime) reader2["date_of_start_policy"];
                policy.coberture_policy = (string) reader2["coberture_policy"];
                policy.max_value = (int) reader2["max_value"];
                policy.name_of_policy = (string) reader2["name_of_policy"];
                policy.city_of_client = (string) reader2["city_of_client"];
                policy.address_of_client = (string) reader2["address_of_client"];
                policy.plate = (string) reader2["plate"];
                policy.model_of_car = (string) reader2["model_of_car"];
                policy.inspection_vehicle = (string) reader2["inspection_vehicle"];
                policy.date_end_of_policy = (DateTime) reader2["date_end_of_policy"];
                policies.Add(policy);
            }
            conexion2.Close();
            return policies;
        }
        
    }
}