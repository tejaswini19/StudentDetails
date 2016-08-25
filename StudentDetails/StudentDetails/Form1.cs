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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static SqlConnection con;
        public static SqlCommand com,com1,com2;
        public static SqlDataReader dr,dr1,dr2;
        public DataTable dt;

        int _id;
        string _name;

       

        string _address;

       

        int _phoneNo;

        //string _id;
        //string _name;
        //string _address;
        //string _phoneNo;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection("Data Source = TEJASWINI\\SQLEXPRESS;Initial Catalog = Students;Integrated Security = true");
                com = new SqlCommand("Select * from Student_Details",con);
                con.Open();
                //dr = new SqlDataReader(com);
                dr = com.ExecuteReader();
                dt = new DataTable();
                dt.Load(dr);

                dataGridView1.DataSource = dt;

                _id = Convert.ToInt32(id);
                _name = name.ToString();
                _address = address.ToString();
                _phoneNo = Convert.ToInt32(phoneNo);

            } 
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                dr.Close();
            }

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            
                      
            try
            {
                com1 = new SqlCommand("insert into Student_Details(Student_ID, Student_Name, Address, Phone_Number) values('" + this.id.Text + "', '" + this.name.Text + "', '" + this.address.Text + "', '" + this.phoneNo.Text + "')", con);
                //con.Open();
               
                dr1 = com1.ExecuteReader();
                MessageBox.Show("saved");

                //dr1.Close();

                com2 = new SqlCommand("select * from Student_Details", con);
                dr2 = com2.ExecuteReader();
                dt.Load(dr2);

                dataGridView1.DataSource = dt;
                //dr2.Close();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
            finally
            {
                con.Close();
                dr1.Close();
                dr2.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            com = new SqlCommand("update Student_Details set Address=@adrs where Student_ID=@id", con);
           
            SqlParameter p3 = new SqlParameter("@id", SqlDbType.Int);
            SqlParameter p4 = new SqlParameter("@adrs", SqlDbType.NVarChar);
           
            p3.Value = Convert.ToInt32(id.Text);
            p4.Value = address.Text;
           
            com.Parameters.Add(p3);
            com.Parameters.Add(p4);
            con.Open();
            com.ExecuteNonQuery();
            MessageBox.Show("Address changed Successfully");
            con.Close();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {

            com = new SqlCommand("delete Student_Details where Student_ID=@id", con);
            SqlParameter p3 = new SqlParameter("@id", SqlDbType.Int);
            //SqlParameter p4 = new SqlParameter("@adrs", SqlDbType.NVarChar);

            p3.Value = Convert.ToInt32(id.Text);
            //p4.Value = address.Text;

            com.Parameters.Add(p3);
            //com.Parameters.Add(p4);
            con.Open();
            com.ExecuteNonQuery();
            MessageBox.Show("Record Deleted Successfully");
            con.Close();

            //id.Clear();
            //name.Clear();
            //address.Clear();
            //phoneNo.Clear();
        }
    }
}

    

