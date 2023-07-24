using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Configuration;
using learning.Models;

namespace learning.Models
{
    public class Repos
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());

        public List<SelectListItem> BindState()
        {
           List<SelectListItem> ss = new List<SelectListItem>();
            SqlCommand cmd = new SqlCommand("BindDropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                SelectListItem sa = new SelectListItem();
                //ss.Add(new SelectListItem
                //{
                //    Text = ds.Tables[0].Rows[i]["Text"].ToString(),
                //    Value = ds.Tables[0].Rows[i]["Value"].ToString()
                //});
                sa.Text = ds.Tables[0].Rows[i]["Text"].ToString();
                sa.Value = ds.Tables[0].Rows[i]["Value"].ToString();

                ss.Add(sa);



            }
            return ss;
        }

        public List<SelectListItem> BindCity(int id)
        {
            SqlCommand cmd = new SqlCommand("BindDropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<SelectListItem> ss = new List<SelectListItem>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ss.Add(new SelectListItem()
                {
                    Text = ds.Tables[0].Rows[i]["Text"].ToString(),
                    Value = ds.Tables[0].Rows[i]["Value"].ToString()
                });

            }
            return ss;
        }

        public Student InsertStudent(Student model)
        {
            Student obj = new Student();
            SqlCommand cmd = new SqlCommand("Insert_Update_Student", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SId", model.SId);
            cmd.Parameters.AddWithValue("@Name", model.Name);
            cmd.Parameters.AddWithValue("@FName", model.FName);
            cmd.Parameters.AddWithValue("@MName", model.MName);
            cmd.Parameters.AddWithValue("@State", model.State);
            cmd.Parameters.AddWithValue("@City", model.City);
            cmd.Parameters.AddWithValue("@Address", model.Addr);
            cmd.Parameters.AddWithValue("@Mob", model.Mob);
            cmd.Parameters.AddWithValue("@Photo", model.PhotoPath);
            cmd.Parameters.AddWithValue("@Sig", model.SignaturePath);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                obj.flag = ds.Tables[0].Rows[0]["flag"].ToString();
                obj.msg = ds.Tables[0].Rows[0]["msg"].ToString();

            }

            con.Close();
            return obj;

        }

        public List<Student> Studentlst()
        {
            List<Student> list = new List<Student>();
          
            SqlCommand cmd = new SqlCommand("Fetch_students", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Student stu = new Student();
                stu.SId = Convert.ToInt32(dr["Sid"]);
                stu.Name = dr["Name"].ToString();
                stu.FName = dr["FName"].ToString();
                stu.MName = dr["MName"].ToString();
                stu.Addr = dr["Addr"].ToString();
                stu.Photo = dr["Photo"].ToString();
                stu.SignaturePath = dr["SignaturePath"].ToString();
                stu.Mob = dr["Mob"].ToString();
                stu.State = dr["State"].ToString();
                stu.City = dr["City"].ToString();
            
                list.Add(stu);
            }
        
            return list;

        }



    }



}

