using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["GeneralDatabase"].ConnectionString);
        connection.Open();

        OleDbCommand command = new OleDbCommand("SELECT nom_utilisateur FROM Utilisateurs WHERE moderateur=0", connection);

        OleDbDataReader datareader = command.ExecuteReader();

        while (datareader.Read())
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.Text = datareader[0].ToString();
            row.Cells.Add(cell);
            tblMods.Rows.Add(row);
        }
    }
}