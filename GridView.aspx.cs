using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRIDVIEWAPPWITHSP
{
    public partial class GridView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindGridView();
            }
        }
        protected void BindGridView()
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection
                ("DataSource=VENKATASUPRAJA/SSMS; username=sa; password=123; Initial Catalog=MyDB; Integrated Security=true;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GV_CRUDOPERATIONS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@STATUS", "SELECT");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                if(ds.Tables[0].Rows.Count>0)
                {
                    gvDetails.DataSource = ds;
                    gvDetails.DataBind();
                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    gvDetails.DataSource = ds;
                    gvDetails.DataBind();
                    int columncount = gvDetails.Rows[0].Cells.Count;
                    gvDetails.Rows[0].Cells.Clear();
                    gvDetails.Rows[0].Cells.Add(new TableCell());
                    gvDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                    gvDetails.Rows[0].Cells[0].Text = "No Records Found";
                }
            }
        }

        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            BindGridView();
        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int EMPNO = Convert.ToInt32(gvDetails.DataKeys[e.RowIndex].Values["EMPNO"].ToString());
            TextBox txtEname = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtEname");
            TextBox txtjob = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtjob");
            TextBox txtmgr = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtmgr");
            TextBox txthiredate = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txthiredate");
            TextBox txtsal = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtsal");
            TextBox txtcomm = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtcomm");
            TextBox txtdeptno = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtdeptno");
            GV_CRUDOPERATIONS("UPDATE", txtEname.Text, txtjob.Text,txtmgr.Text, txthiredate.Text, txtsal.Text, txtcomm.Text,txtdeptno.Text ,EMPNO);

        }

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int EMPNO = Convert.ToInt32(gvDetails.DataKeys[e.RowIndex].Values["EMPNO"].ToString());
            String Ename = gvDetails.Rows[e.RowIndex].Values["Ename"];
            GV_CRUDOPERATIONS("Delete",Ename,"",EMPNO);

        }

        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}