using Artsofte_SQL_API.Models.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Artsofte_SQL_API.Repository
{
    public class EmployeeCore
    {
        private readonly IConfiguration _config;
        public EmployeeCore(IConfiguration config)
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

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT e.Id, e.Name, e.SurName, e.Age, e.Gender, e.DepartmentId, e.LanguageId, d.DepartmentName, d.Floor, pl.LanguageName FROM Employee e LEFT JOIN Department d on e.DepartmentId = d.Id LEFT JOIN ProgrammingLanguage pl on e.LanguageId = pl.Id";

                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Employee> employees = new List<Employee>();

                    Employee employee = new();

                    while (reader.Read())
                    {
                        employee = new Employee
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            SurName = reader.GetString(reader.GetOrdinal("SurName")),
                            Age = reader.GetInt32(reader.GetOrdinal("Age")),
                            Gender = reader.GetString(reader.GetOrdinal("Gender")),
                            DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                            Departments = new Department
                            {
                                DepartmentName = reader.GetString(reader.GetOrdinal("DepartmentName")),
                                Floor = reader.GetInt32(reader.GetOrdinal("Floor")),
                                Id = reader.GetInt32(reader.GetOrdinal("Id"))
                            },
                            LanguageId = reader.GetInt32(reader.GetOrdinal("Id")),
                            ProgrammingLanguages = new ProgrammingLanguage
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                LanguageName = reader.GetString(reader.GetOrdinal("LanguageName"))
                            }

                        };
                        employees.Add(employee);
                    }


                    reader.Close();
                    return employees;
                }
            }
        }

        public async Task PostEmployee(Employee employee)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Employee (Name, SurName, Age, Gender, DepartmentId, LanguageId)
                                        OUTPUT INSERTED.Id
                                        VALUES (@Name, @SurName, @Age, @Gender, @DepartmentId, @LanguageId)";
                    cmd.Parameters.Add(new SqlParameter("@Name", employee.Name));
                    cmd.Parameters.Add(new SqlParameter("@SurName", employee.SurName));
                    cmd.Parameters.Add(new SqlParameter("@Age", employee.Age));
                    cmd.Parameters.Add(new SqlParameter("@Gender", employee.Gender));
                    cmd.Parameters.Add(new SqlParameter("@DepartmentId", employee.DepartmentId));
                    cmd.Parameters.Add(new SqlParameter("@LanguageId", employee.LanguageId));


                    int newId = (int)cmd.ExecuteScalar();
                    employee.Id = newId;
                }
            }
        }
        public async Task PutEmployee(Employee employee)
        {
            string query = @"
                   UPDATE Employee SET 
                    Name = '" + employee.Name + @"',
                    SurName = '" + employee.SurName + @"',
                    Age = '" + employee.Age + @"',
                    Gender = '" + employee.Gender + @"',
                    DepartmentId = '" + employee.DepartmentId + @"',
                    LanguageId = '" + employee.LanguageId + @"'        
                    WHERE Id = " + employee.Id + @"
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
        public async Task DeleteEmployee(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Employee WHERE Id = @id";
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
