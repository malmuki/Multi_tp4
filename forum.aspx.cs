﻿using System;
using System.Configuration;
using System.Data.OleDb;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class forum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ID = Request["id"];

        OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["GeneralDatabase"].ConnectionString);
        connection.Open();

        OleDbCommand command = new OleDbCommand("SELECT messages.auteur, messages.message, messages.date_ecriture, Utilisateurs.nom_fichier_avatar, sujet.titre FROM Utilisateurs INNER JOIN (sujet INNER JOIN messages ON sujet.[ID] = messages.[sujet]) ON Utilisateurs.[nom_utilisateur] = messages.[auteur] WHERE sujet=@id;", connection);
        command.Parameters.Add(new OleDbParameter("id", ID) { OleDbType = OleDbType.VarChar, Size = 255 });
        OleDbDataReader datareader = command.ExecuteReader();

        while (datareader.Read())
        {
            TableRow tableRow = new TableRow();

            TableCell avatarCell = new TableCell();

           
            HtmlGenericControl imgDiv = new HtmlGenericControl("span");
            HtmlGenericControl pseudoDiv = new HtmlGenericControl("h3");

            TableCell userCell = new TableCell();
            Image avatar = new Image();
             if (datareader.IsDBNull(3) || string.IsNullOrEmpty((string)datareader[3]))
            {
                avatar.ImageUrl = "~/assets/image/avatar.jpg";
            }
            else
            {
                avatar.ImageUrl = "~/assets/image/" + (string)datareader[3];
            }
            if (lblSujet.Text == "")
            {
                lblSujet.Text = string.Format("<h1>{0}</h1>", (string)datareader[4]);
            }

            imgDiv.Controls.Add(avatar);
            pseudoDiv.InnerText = datareader.IsDBNull(0) ? "" : (string)datareader[0];
            userCell.Controls.Add(imgDiv);
            userCell.Controls.Add(pseudoDiv);

            tableRow.Cells.Add(userCell);
            Label Text = new Label();

            tableRow.Cells.Add(new TableCell() { Text = datareader.IsDBNull(1) ? "" : ((string)datareader[1]) });
            tableRow.Cells.Add(new TableCell() { Text = datareader.IsDBNull(2) ? "" : ((DateTime.Now - (DateTime)datareader[2]).ToString("''%d' jours,'%h' heures,'%m' minutes et '%s' secondes'")) });

            menuMessage.Rows.Add(tableRow);
        }
        connection.Close();
    }

    protected void btnEnvoyerMessage_Click(object sender, EventArgs e)
    {
        if (this.IsValid)
        {
            OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["GeneralDatabase"].ConnectionString);
            connection.Open();

            OleDbCommand command = new OleDbCommand("INSERT INTO messages (sujet, auteur, date_ecriture, message) VALUES (@sujet, @auteur, @date, @message)", connection);

            command.Parameters.Add(new OleDbParameter("sujet", int.Parse(Request["ID"]))
            {
                OleDbType = OleDbType.Integer
            });

            command.Parameters.Add(new OleDbParameter("auteur", Session["id"])
            {
                OleDbType = OleDbType.VarChar,
                Size = 255
            });

            command.Parameters.Add(new OleDbParameter("date", DateTime.Now)
            {
                OleDbType = OleDbType.Date
            });

            command.Parameters.Add(new OleDbParameter("message", txtReponseMessage.Text)
            {
                OleDbType = OleDbType.VarChar,
                Size = 255
            });

            command.ExecuteNonQuery();
            connection.Close();
            lblSuccess.Text = "Le message a bien été enregistré";
        }
    }
}