using Artsofte_SQL_API.Models.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Artsofte_SQL_API.Repository
{
    public class PLanguageCore
    {
        private readonly IConfiguration _config;
        public PLanguageCore(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public async Task<IEnumerable<ProgrammingLanguage>> GetLanguage()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT l.Id, l.LanguageName FROM ProgrammingLanguage l";

                    SqlDataReader reader = cmd.ExecuteReader();
                    List<ProgrammingLanguage> languages = new List<ProgrammingLanguage>();

                    ProgrammingLanguage language = new();

                    while (reader.Read())
                    {
                        language = new ProgrammingLanguage
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            LanguageName = reader.GetString(reader.GetOrdinal("LanguageName"))

                        };
                        languages.Add(language);
                    }


                    reader.Close();
                    return languages;
                }
            }
        }

        public async Task PostLanguage(ProgrammingLanguage language)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO ProgrammingLanguage (LanguageName)
                                        OUTPUT INSERTED.Id
                                        VALUES (@LanguageName)";
                    cmd.Parameters.Add(new SqlParameter("@LanguageName", language.LanguageName));


                    int newId = (int)cmd.ExecuteScalar();
                    language.Id = newId;
                }
            }
        }
        public async Task PutLanguage(ProgrammingLanguage language)
        {
            string query = @"
                   update Department set 
                    LanguageName = '" + language.LanguageName + @"',     
                    where Id = " + language.Id + @"
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _config.GetConnectionString("DefaultConnection");
            SqlDataReader MyReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myCon.Close();
                }
            }
        }
        public async Task DeleteLanguage(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM ProgrammingLanguage WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    if (rowsAffected > 0)
                    {
                        new StatusCodeResult(StatusCodes.Status204NoContent);
                    }
                    throw new Exception("No rows affected");
                }
            }
        }
    }
}
