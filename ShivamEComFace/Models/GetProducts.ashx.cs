using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace ShivamEComFace.Models
{
    /// <summary>
    /// Summary description for GetProducts
    /// </summary>
    public class GetProducts : IHttpHandler
    {

        public List<object> newcolumns = null;
        private int filteredRows = 0;
        private int SupplierId = Convert.ToInt32(ConfigurationManager.AppSettings["SupplierID"]);
        public void ProcessRequest(HttpContext context)
        {
            int displayLength = int.Parse(context.Request["displayLength"]);
            int displayStart = int.Parse(context.Request["displayStart"]);
            //int sortCol = int.Parse(context.Request["iSortCol_0"]);
            //string sortDir = context.Request["sSortDir_0"];
            string search = context.Request["searchText"];


            string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var allProducts = new List<object[]>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SP_GetAllSortedProductsFrontFace", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramDisplayLength = new SqlParameter()
                {
                    ParameterName = "@DisplayLength",
                    Value = displayLength
                };
                cmd.Parameters.Add(paramDisplayLength);

                SqlParameter paramDisplayStart = new SqlParameter()
                {
                    ParameterName = "@DisplayStart",
                    Value = displayStart
                };
                cmd.Parameters.Add(paramDisplayStart);

                SqlParameter paramSortCol = new SqlParameter()
                {
                    ParameterName = "@SortCol",
                    Value = "productName"
                };
                cmd.Parameters.Add(paramSortCol);

                SqlParameter paramSortDir = new SqlParameter()
                {
                    ParameterName = "@SortDir",
                    Value = "asc"
                };
                cmd.Parameters.Add(paramSortDir);

                SqlParameter paramSearchString = new SqlParameter()
                {
                    ParameterName = "@SearchText",
                    Value = string.IsNullOrEmpty(search) ? null : search
                };
                cmd.Parameters.Add(paramSearchString);
               

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                using (rdr)
                {


                    allProducts = Read(rdr).ToList();

                    //TempData["cols"] = newcolumns;

                }
                //while (rdr.Read())
                //{
                //    filteredRows = Convert.ToInt32(rdr["TotalCount"]);
                //    Employee employee = new Employee();
                //    employee.Id = Convert.ToInt32(rdr["Id"]);
                //    employee.FirstName = rdr["FirstName"].ToString();
                //    employee.LastName = rdr["LastName"].ToString();
                //    employee.Gender = rdr["Gender"].ToString();
                //    employee.JobTitle = rdr["JobTitle"].ToString();
                //    employees.Add(employee);
                //}
            }

            var result = new
            {
                iTotalRecords = GetProductsTotalCount(),
                iTotalDisplayRecords = filteredRows,
                aaData = allProducts,
                aoColumns = newcolumns
            };

            JavaScriptSerializer js = new JavaScriptSerializer();
            context.Response.Write(js.Serialize(result));
        }
        private IEnumerable<object[]> Read(DbDataReader reader)
        {
            newcolumns = new List<object>();
            var k = 0;
            while (reader.Read())
            {

                var values = new List<object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (k == 0)
                    {
                        newcolumns.Add(reader.GetName(i));
                    }
                    filteredRows = Convert.ToInt32(reader["TotalCount"]);

                    values.Add(reader.GetValue(i));
                }
                k++;
                yield return values.ToArray();
            }
        }
        private int GetProductsTotalCount()
        {
            int totalEmployees = 0;
            string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("[GetProductsCount] 1", con);
                con.Open();
                totalEmployees = (int)cmd.ExecuteScalar();
            }
            return totalEmployees;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}