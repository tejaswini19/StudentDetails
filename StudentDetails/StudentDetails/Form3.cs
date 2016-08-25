using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StudentDetails
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public static SqlConnection con;
        public static SqlCommand com;
        public static SqlDataAdapter da;
        //public DataTable dt;
        DataSet ds = new DataSet();
        public static SqlCommandBuilder builder;
       

        private void Form3_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source = TEJASWINI\\SQLEXPRESS;Initial Catalog = Students;Integrated Security = true");
            com = new SqlCommand("select * from Books_Details", con);
            con.Open();
            da = new SqlDataAdapter();

            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            da.SelectCommand = com;
            builder = new SqlCommandBuilder(da);
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            DataRow dr = ds.Tables[0].NewRow();
            dr["book_id"] = id_TextBox.Text;
            dr["book_name"] = name_TextBox.Text;
            dr["book_author_name"] = authorName_TextBox.Text;
            dr["book_publication"] = pub_Textbox.Text;
            dr["book_price"] = price_Textbox.Text;

            ds.Tables[0].Rows.Add(dr);
            da.Update(ds);
            MessageBox.Show("Record added");
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Refresh();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DataRow dr = ds.Tables[0].Rows.Find(id_TextBox.Text);

            dr["book_name"] = name_TextBox.Text;
            dr["book_author_name"] = authorName_TextBox.Text;
            dr["book_publication"] = pub_Textbox.Text;
            dr["book_price"] = price_Textbox.Text;

            da.Update(ds);
            MessageBox.Show("Record changed");
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Refresh();

        }

       
    }
}
