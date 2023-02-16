using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SoftApi.Contexts;
using SoftApi.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Newtonsoft.Json;

namespace SoftApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase{
        private readonly ConexionSqlServer context;
        private readonly string secretKey;
        public UserController(ConexionSqlServer context, IConfiguration config)
        {
            this.context = context;
            secretKey = config.GetSection("settings").GetSection("secret_key").ToString();

        }


        //[HttpPost("ex")]
        /**public IActionResult Validate([FromBody] User request){
            List<User> users = new List<User>();
            SqlConnection conexion = (SqlConnection) context.Database.GetDbConnection();
            SqlCommand comando = conexion.CreateCommand();
            conexion.Open();
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = "sp_list_user";
            comando.Parameters.Add("@user", System.Data.SqlDbType.VarChar, 30).Value = request.user_name;
            SqlDataReader reader = comando.ExecuteReader();
            while(reader.Read()){
                User user = new User();
                user.id = (int) reader["id"];
                user.user_name = (string) reader["user_name"];
                user.user_password = (string) reader["user_password"];
                user.user_type = (string) reader["user_type"];
                users.Add(user);
            }
            conexion.Close();
            Int32 lenght = users.Count();
            Console.WriteLine(lenght);

            if(request.user_name == "bvasquez" && request.user_password == "1234"){
                var keyInBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.user_name));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyInBytes), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokenCreated = tokenHandler.WriteToken(tokenConfig);
                return StatusCode(StatusCodes.Status200OK, new { token = tokenCreated});
            }else {
                return BadRequest("Usuario o contraseña no encontrados");
            }
        }**/

        [HttpPost("login")]
        public async Task<IActionResult> PostRawBufferManual()
        {
            string requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
            dynamic stuff1 = JsonConvert.DeserializeObject(requestBody);
            string user = stuff1["user_name"];
            string pass = stuff1["user_password"];
            string tokenCreated = CreateTokenJWT(user, pass);
            return StatusCode(StatusCodes.Status200OK, new { response = tokenCreated});
        }

        public string CreateTokenJWT(string user_name, string user_password){
            List<User> users = new List<User>();
            SqlConnection conexion = (SqlConnection) context.Database.GetDbConnection();
            SqlCommand comando = conexion.CreateCommand();
            conexion.Open();
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = "sp_list_user";
            comando.Parameters.Add("@user", System.Data.SqlDbType.VarChar, 30).Value = user_name;
            SqlDataReader reader = comando.ExecuteReader();
            while(reader.Read()){
                User user = new User();
                user.id = (int) reader["id"];
                user.user_name = (string) reader["user_name"];
                user.user_password = (string) reader["user_password"];
                user.user_type = (string) reader["user_type"];
                users.Add(user);
            }
            conexion.Close();
            Int32 lenght = users.Count();
            if(lenght > 0){
                var firstUser = users.First();
                string pass = firstUser.user_password;
                if(user_password == pass){
                    var keyInBytes = Encoding.ASCII.GetBytes(secretKey);
                    var claims = new ClaimsIdentity();

                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user_name));
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyInBytes), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                    string tokenCreated = tokenHandler.WriteToken(tokenConfig);
                    return tokenCreated;
                }else {
                    return "Contraseña incorrecta";
                }
            }else{
                return "Usuario no encontrado";
            }

            
        }

    }

    public class UserController2 : ControllerBase{
        private readonly ConexionSqlServer context;
        private readonly string secretKey;
        public UserController2(ConexionSqlServer context)
        {
            this.context = context;

        }

        public string LoginTest(string user_name, string user_password){
            User user = new User();
            try{
                user = context.Users.First(x => x.user_name == user_name);
            }catch{
                user.user_name = "Not found";
                user.user_password = "Not found";
                user.user_type = "Not found";
            }
            if(user.user_name != "Not found"){
                if(user.user_password == user_password){
                    return "login succeeded";
                }else{
                    return "Wrong password";
                }
            }else{
                return "User not found";
            }
        }
    }
}