using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
    }
    public class CategoryDA
    {
        public List<Category> GetAll()
        {

            SqlConnection sqlConn = new SqlConnection(Ultilities.ConnectionString);
            sqlConn.Open();

            SqlCommand command = sqlConn.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Ultilities.Category_GetAll;

            SqlDataReader reader = command.ExecuteReader();
            List<Category> list = new List<Category>();
            while (reader.Read())
            {
                Category category = new Category();
                category.ID = Convert.ToInt32(reader["ID"]);
                category.Name = reader["Name"].ToString();
                category.Type = Convert.ToInt32(reader["Type"]);
                list.Add(category);
            }

            sqlConn.Close();
            return list;
        }

        public int Insert_Update_Delete(Category category, int action)
        {

            SqlConnection sqlConn = new SqlConnection(Ultilities.ConnectionString);
            sqlConn.Open();
            //Khai báo đối tượng SqlCommand có kiểu xử lý là StoredProcedure
            SqlCommand command = sqlConn.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Ultilities.Category_InsertUpdateDelete;

            SqlParameter IDPara = new SqlParameter("@ID", SqlDbType.Int);
            IDPara.Direction = ParameterDirection.InputOutput;
            command.Parameters.Add(IDPara).Value = category.ID;
            command.Parameters.Add("@Name", SqlDbType.NVarChar, 200).Value = category.Name;
            command.Parameters.Add("@Type", SqlDbType.Int).Value = category.Type;
            command.Parameters.Add("@Action", SqlDbType.Int).Value = action;

            int result = command.ExecuteNonQuery();
            if (result > 0) // Nếu thành công thì trả về ID đã thêm
                return (int)command.Parameters["@ID"].Value;
            return 0;
        }
    }
    
}


