using ApiCruDap.Model;
using System.Data.SqlClient;
using Dapper;

namespace ApiCruDap.Service
{
    public class ProductService
    {
        public static async Task<IEnumerable<Product>> GetAllProducts(SqlConnection conn)
        {
            try
            {
                var products = await conn.QueryAsync<Product>("SELECT * from Product");
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar todos os produtos: " + ex.Message);
                throw;
            }
        }

        public static async Task<IEnumerable<Product>> GetAProduct(SqlConnection conn, int id)
        {
            try
            {
                return await conn.QueryAsync<Product>("SELECT * from Product WHERE id = @Id", new { Id = id });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar um produto: " + ex.Message);
                throw;
            }
        }

        public static void CreateAProduct(SqlConnection conn, Product product)
        {
            try
            {
                conn.ExecuteAsync("INSERT into Product (name, code, description, unit) " +
                    "values (@Name, @Code, @Desciption, @Unit)", product);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao criar um produto: " + ex.Message);
                throw;
            }
        }

        public static void UpdateAProduct(SqlConnection conn, Product product)
        {
            try
            {
                conn.ExecuteAsync("UPDATE Product set name = @Name, code = @Code, description = @Descriptions, unit = @Unit WHERE id=@Id", product);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar um produto: " + ex.Message);
                throw;
            }
        }

        public static void DeleteAProduct(SqlConnection conn, int Id)
        {
            try
            {
                conn.ExecuteAsync("DELETE Product WHERE id=@Id", new { Id });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao excluir um produto: " + ex.Message);
                throw;
            }
        }
    }
}
