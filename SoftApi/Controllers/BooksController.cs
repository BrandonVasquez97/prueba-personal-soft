using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SoftApi.Contexts;
using SoftApi.Models;

namespace PersonalSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BooksController : ControllerBase
    {
        private readonly ConexionSqlServer context;
        public BooksController(ConexionSqlServer context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Get(string nombre)
        {
            try{
                List<Books> books = new List<Books>();
                if (nombre == null){
                    books = context.Books.ToList();
                }
                else{
                    //usar el parametro nombre para filtrar en la BD
                    //books = context.Books.Where(x => x.title.ToLower().IndexOf(nombre) > -1).ToList();

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
                    comando.CommandText = "sp_list_books";
                    //Se añade el parametro del SP
                    comando.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar, 50).Value = nombre;
                    //Se ejecuta el SP y se encapsula
                    SqlDataReader reader = comando.ExecuteReader();
                    //Se recorre los resultados
                    while(reader.Read()){
                        //Se almacena cada resultado en un nuevo objeto de clase
                        Books book = new Books();
                        book.id = (int) reader["id"];
                        book.title = (string) reader["title"];
                        book.author = (string) reader["author"];
                        book.pages = (int) reader["pages"];
                        //Se añade al array
                        books.Add(book);
                    }
                    conexion.Close();
                }
                return Ok(books);
            }catch{
                return BadRequest("Error");
            }
        }
    }
}