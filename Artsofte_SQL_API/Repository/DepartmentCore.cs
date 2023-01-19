using Artsofte_SQL_API.Models.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Artsofte_SQL_API.Repository
{
    public class DepartmentCore
    {
        private readonly IConfiguration _config;
        public DepartmentCore(IConfiguration config)
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

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT d.Id, d.DepartmentName, d.Floor FROM Department d";

                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Department> departments = new List<Department>();

                    Department department = new();

                    while (reader.Read())
                    {
                        department = new Department
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            DepartmentName = reader.GetString(reader.GetOrdinal("DepartmentName")),
                            Floor = reader.GetInt32(reader.GetOrdinal("Floor"))

                        };
                        departments.Add(department);
                    }


                    reader.Close();
                    return departments;
                }
            }
        }

        public async Task PostDepartment(Department department)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Department (DepartmentName, Floor)
                                        OUTPUT INSERTED.Id
                                        VALUES (@DepartmentName, @Floor)";
                    cmd.Parameters.Add(new SqlParameter("@DepartmentName", department.DepartmentName));
                    cmd.Parameters.Add(new SqlParameter("@Floor", department.Floor));


                    int newId = (int)cmd.ExecuteScalar();
                    department.Id = newId;
                }
            }
        }
        public async Task PutDepartment(Department department)
        {
            string query = @"
                   update Department set 
                    DepartmentName = '" + department.DepartmentName + @"',
                    Floor = '" + department.Floor + @"',     
                    where Id = " + department.Id + @"
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
        public async Task DeleteDepartment(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Department WHERE Id = @id";
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
