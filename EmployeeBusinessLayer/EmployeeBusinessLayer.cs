using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class EmployeeBusinessLayer
    {
        public void AddEmploee(Employee employee)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ToString());
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "insert into Employee(Name, Gender, City, DateOfBirth) values ('" + employee.Name + "','" + employee.Gender + "','" + employee.City + "','" + employee.DateOfBirth + "')";
            conn.Open();
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public IEnumerable<Employee> GetEmployees()
        {
            Employee emp;
            IList<Employee> employees = new List<Employee>();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ToString());
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "select * from Employee";
            cmd.Connection = conn;

            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            da.SelectCommand = cmd;
            da.Fill(ds);

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                emp = new Employee();
                emp.Id = Convert.ToInt32(r["EmployeeId"]);
                emp.Name = r["Name"].ToString();
                emp.Gender = r["Gender"].ToString();
                emp.City = r["City"].ToString();
                if (!(r["DateOfBirth"] is DBNull))
                    emp.DateOfBirth = Convert.ToDateTime(r["DateOfBirth"].ToString());

                employees.Add(emp);
            }

            return employees;
        }

        public void UpdateEmployeeDetails(Employee emp)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            
            SqlParameter p1 = new SqlParameter("@name", emp.Name);
            SqlParameter p2 = new SqlParameter("@Id", emp.Id);
            SqlParameter p3 = new SqlParameter("@gender", emp.Gender);
            SqlParameter p4 = new SqlParameter("@city", emp.City);
            SqlParameter p5 = new SqlParameter("@dateofbirth", emp.DateOfBirth);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);

            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "spUpdateEmployeeDetails";
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void DeleteEmployee(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ToString());
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "delete from Employee where employeeid = " + id;
            conn.Open();
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}