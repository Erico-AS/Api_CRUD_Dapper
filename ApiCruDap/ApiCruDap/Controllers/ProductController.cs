using ApiCruDap.Model;
using ApiCruDap.Service;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ApiCruDap.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IConfiguration _config;
        public ProductController(IConfiguration config) 
        {
            _config = config;
        }

        [HttpGet("/Ler")]
        public async Task<ActionResult<List<Product>>> GetAll() 
        {
            using SqlConnection conn = SqlConn();
            IEnumerable<Product> products = await ProductService.GetAllProducts(conn);
            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Product>>> GetProduct(int id)
        {
            using SqlConnection conn = SqlConn();
            var product = await ProductService.GetAProduct(conn, id);
            return Ok(product);
        }

        [HttpPost("/Adicionar")]
        public async Task<ActionResult<List<Product>>>  CreateProduct(Product product)
        {
            using SqlConnection conn = SqlConn();
            ProductService.CreateAProduct(conn, product);
            return Ok(await ProductService.GetAllProducts(conn));
        }

        [HttpPut("/Atualizar")]
        public async Task<ActionResult<List<Product>>> UpdateProduct(Product product)
        {
            using SqlConnection conn = SqlConn();
            ProductService.UpdateAProduct(conn, product);
            return Ok(await ProductService.GetAllProducts(conn));
        }

        [HttpDelete("{Id}", Name = "Deletar")]
        public async Task<ActionResult<List<Product>>> DeleteProduct(int Id)
        {
            using SqlConnection conn = SqlConn();
            ProductService.DeleteAProduct(conn, Id);
            return Ok(await ProductService.GetAllProducts(conn));
        }

        private SqlConnection SqlConn()
        {
            try
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Erro ao criar a conexão SQL: " + sqlEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro genérico ao criar a conexão: " + ex.Message);
                throw;
            }

        }
    }
}
