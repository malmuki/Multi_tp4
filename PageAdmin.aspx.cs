﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;
using System.Web.UI.HtmlControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Update()
    {
        OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["GeneralDatabase"].ConnectionString);
        connection.Open();

        OleDbCommand command = new OleDbCommand("UPDATE Utilisateurs", connection);

        command.Prepare();
        command.ExecuteNonQuery();
    }

    public bool IsAdministrator()
    {
        OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["GeneralDatabase"].ConnectionString);
        connection.Open();

        OleDbCommand command = new OleDbCommand("SELECT administrateur FROM Utilisateurs WHERE nom_utilisateur = @sessionID", connection);
        command.Parameters.Add(new OleDbParameter("sessionID", (string)Session["id"]) { OleDbType = OleDbType.VarChar, Size = 255 });
        OleDbDataReader datareader = command.ExecuteReader();

        if (datareader.Read())
        {
            return (bool)datareader[0];
        }
        else
        {
            return false;
        }

    }
}