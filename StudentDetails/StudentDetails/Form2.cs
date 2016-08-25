using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentDetails
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public static SqlConnection con;
        public static SqlCommand com;
        public static SqlDataAdapter da;
        public DataTable dt;
        DataSet ds = new DataSet();
        private void Form2_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source = TEJASWINI\\SQLEXPRESS;Initial Catalog = Students;Integrated Security = true");
            com = new SqlCommand("select * from Books_Details", con);


            com.Connection = con;
            da = new SqlDataAdapter();
            da.SelectCommand = com;
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            id_TextBox.DataBindings.Add("Text", ds.Tables[0], "Book_Id");
            name_TextBox.DataBindings.Add("Text", ds.Tables[0], "Book_name");
            authorName_TextBox.DataBindings.Add("Text", ds.Tables[0], "Book_author_name");
            pub_Textbox.DataBindings.Add("Text", ds.Tables[0], "Book_publication");
            price_Textbox.DataBindings.Add("Text", ds.Tables[0], "Book_price");


        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            

            com = new SqlCommand("insert into Books_Details(Book_Id,Book_Name,Book_Author_Name,Book_Publication,Book_Price) values(@id,@bname,@bauthorname,@bpub,@bprice)", con);
            SqlParameter p1 = new SqlParameter("@id", SqlDbType.Int);
            SqlParameter p2 = new SqlParameter("@bname", SqlDbType.VarChar);
            SqlParameter p3 = new SqlParameter("@bauthorname", SqlDbType.VarChar);
            SqlParameter p4 = new SqlParameter("@bpub", SqlDbType.VarChar);
            SqlParameter p5 = new SqlParameter("@bprice", SqlDbType.Decimal);
            p1.Value = id_TextBox.Text;
            p2.Value = name_TextBox.Text;
            p3.Value = authorName_TextBox.Text;
            p4.Value = pub_Textbox.Text;
            p5.Value = price_Textbox.Text;

            com.Parameters.Add(p1);
            com.Parameters.Add(p2);
            com.Parameters.Add(p3);
            com.Parameters.Add(p4);
            com.Parameters.Add(p5);
            con.Open();
            com.ExecuteNonQuery();
            MessageBox.Show("Added Successfully");
            con.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            com = new SqlCommand("update Books_Details set Book_price=@bprice where Book_Id=@id", con);

            SqlParameter p3 = new SqlParameter("@id", SqlDbType.Int);
            SqlParameter p4 = new SqlParameter("@bprice", SqlDbType.Decimal);

            p3.Value = Convert.ToInt32(id_TextBox.Text);
            p4.Value = price_Textbox.Text;

            com.Parameters.Add(p3);
            com.Parameters.Add(p4);
            con.Open();
            com.ExecuteNonQuery();
            MessageBox.Show("Price Changed Successfully");
            con.Close();
        }

        private void btnTotalPrice_Click(object sender, EventArgs e)
        {
            
            com = new SqlCommand("select sum(Book_price) from Books_Details", con);
            con.Open();
            decimal sum = (decimal)com.ExecuteScalar();
            MessageBox.Show("Total Price is : " + sum);
            con.Close();
            

        }
    }
}
