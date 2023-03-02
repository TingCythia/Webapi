using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Data.SqlClient;

namespace Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SiteController : ControllerBase
    {
        private readonly string _connectionString;

        public SiteController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Site>>> GetSite()
        {
            List<Site> sites = new List<Site>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Site";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Site site = new Site();

                            site.ID = (int)reader["ID"];
                            site.Name = reader["Name"] == DBNull.Value ? null : (string)reader["Name"];
                            site.LastCheckForLicens = (DateTime)reader["LastCheckForLicens"];
                            site.CreateDate = (DateTime)reader["CreateDate"];
                            site.ServerPath = reader["ServerPath"] == DBNull.Value ? null : (string)reader["ServerPath"];
                            site.LastCheckForLicens = (DateTime)reader["LastCheckForLicens"];

                            sites.Add(site);
                        }
                    }
                }
            }

            return sites;
        }

        [HttpGet("Site/{id}")]

        public async Task<ActionResult<Site>> GetSiteByID(int id)
        {
            Site site = new Site();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Site WHERE ID = @ID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            site.ID = (int)reader["ID"];
                            site.Name = reader["Name"] == DBNull.Value ? null : (string)reader["Name"];
                            site.LastCheckForLicens = (DateTime)reader["LastCheckForLicens"];
                            site.CreateDate = (DateTime)reader["CreateDate"];
                            site.ServerPath = reader["ServerPath"] == DBNull.Value ? null : (string)reader["ServerPath"];
                            site.LastCheckForLicens = (DateTime)reader["LastCheckForLicens"];
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }

            return site;
        }
    }
}