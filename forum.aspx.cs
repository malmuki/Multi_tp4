﻿using System;
using System.Configuration;
using System.Data.OleDb;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class forum : System.Web.UI.Page
{
    private string titreSujet;

    protected void Page_Load(object sender, EventArgs e)
    {
        string ID = Request["id"];

        OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["GeneralDatabase"].ConnectionString);
        connection.Open();

        OleDbCommand command;// initialisation
        OleDbDataReader datareader;// initialisation

        if (Session["ID"] != null)
        {
            command = new OleDbCommand("SELECT moderateur FROM Utilisateurs WHERE nom_utilisateur=@id ", connection);
            command.Parameters.Add(new OleDbParameter("id", (string)Session["ID"]) { OleDbType = OleDbType.VarChar, Size = 255 });
            datareader = command.ExecuteReader();
            if (datareader.Read())
            {
                if (!(bool)datareader[0])
                {
                    buttonLock.Visible = false;
                }
            }

            command = new OleDbCommand("SELECT lock, titre FROM sujet WHERE ID=@id ", connection);
            command.Parameters.Add(new OleDbParameter("id", ID) { OleDbType = OleDbType.VarChar, Size = 255 });
            datareader = command.ExecuteReader();

            if (datareader.Read())
            {
                titreSujet = (string)datareader[1];
                if ((bool)datareader[0])
                {
                    btnEnvoyerMessage.Enabled = false;
                    btnEnvoyerMessage.Text = "sujet fermé";
                    txtReponseMessage.Visible = false;
                    instruction.Visible = false;
                    buttonLock.Visible = false;
                }
            }
        }
        else
        {
            buttonLock.Visible = false;
        }

        command = new OleDbCommand("SELECT messages.auteur, messages.message, messages.date_ecriture, Utilisateurs.nom_fichier_avatar, sujet.titre, messages.ID FROM Utilisateurs INNER JOIN (sujet INNER JOIN messages ON sujet.[ID] = messages.[sujet]) ON Utilisateurs.[nom_utilisateur] = messages.[auteur] WHERE sujet=@id;", connection);
        command.Parameters.Add(new OleDbParameter("id", ID) { OleDbType = OleDbType.VarChar, Size = 255 });
        datareader = command.ExecuteReader();

        while (datareader.Read())
        {
            TableRow tableRow = new TableRow();

            TableCell avatarCell = new TableCell();
            TableCell button = new TableCell();

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
            if ((string)datareader[0] == (string)Session["ID"])
            {
                Button modifier = new Button();
                modifier.Text = "Modifier";
                modifier.ID = datareader[5].ToString();
                modifier.Click += new EventHandler(this.Modifier_Click);
                button.Controls.Add(modifier);
            }
            imgDiv.Controls.Add(avatar);
            pseudoDiv.InnerText = datareader.IsDBNull(0) ? "" : (string)datareader[0];
            userCell.Controls.Add(imgDiv);
            userCell.Controls.Add(pseudoDiv);

            tableRow.Cells.Add(userCell);
            Label Text = new Label();

            tableRow.Cells.Add(new TableCell() { Text = datareader.IsDBNull(1) ? "" : ((string)datareader[1]) });
            tableRow.Cells.Add(new TableCell() { Text = datareader.IsDBNull(2) ? "" : ((DateTime.Now - (DateTime)datareader[2]).ToString("''%d' jours,'%h' heures,'%m' minutes et '%s' secondes'")) });
            tableRow.Cells.Add(button);
            menuMessage.Rows.Add(tableRow);
        }
        connection.Close();
    }

    private void Modifier_Click(object sender, EventArgs e)
    {
        Button objet = (Button)sender;
        string id = objet.ID;
        string nouveauMessage = txtReponseMessage.Text;
        OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["GeneralDatabase"].ConnectionString);
        connection.Open();

        OleDbCommand command = new OleDbCommand("UPDATE messages SET message=@message WHERE ID=@id", connection);
        command.Parameters.Add(new OleDbParameter("message", nouveauMessage));
        command.Parameters.Add(new OleDbParameter("id", id));

        command.ExecuteNonQuery();

        Session["message"] = id;

        connection.Close();

        Response.Redirect("~/forum.aspx?ID=" + Request["id"]);
    }

    protected void btnEnvoyerMessage_Click(object sender, EventArgs e)
    {
        if (this.IsValid)
        {
            OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["GeneralDatabase"].ConnectionString);
            connection.Open();

            OleDbCommand command = new OleDbCommand("INSERT INTO messages (sujet, auteur, date_ecriture, message) VALUES (@sujet, @auteur, @date, @message)", connection);

            command.Parameters.Add(new OleDbParameter("sujet", int.Parse(Request["ID"])) { OleDbType = OleDbType.Integer });

            command.Parameters.Add(new OleDbParameter("auteur", Session["id"]) { OleDbType = OleDbType.VarChar, Size = 255 });

            command.Parameters.Add(new OleDbParameter("date", DateTime.Now) { OleDbType = OleDbType.Date });

            command.Parameters.Add(new OleDbParameter("message", txtReponseMessage.Text) { OleDbType = OleDbType.VarChar, Size = 255 });

            command.ExecuteNonQuery();
            connection.Close();
            lblSuccess.Text = "Le message a bien été enregistré";
            Response.Redirect("~/forum.aspx?ID=" + Request["id"]);
        }
    }

    protected void buttonLock_Click(object sender, EventArgs e)
    {
        titreSujet = titreSujet + " (sujet fermé) ";

        OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["GeneralDatabase"].ConnectionString);
        connection.Open();

        OleDbCommand command = new OleDbCommand("UPDATE sujet SET lock=true, titre=@tire WHERE ID=@id", connection);
        command.Parameters.Add(new OleDbParameter("titre", titreSujet) { OleDbType = OleDbType.VarChar, Size = 255 });
        command.Parameters.Add(new OleDbParameter("id", Request["id"]) { OleDbType = OleDbType.Integer });

        command.ExecuteNonQuery();

        connection.Close();
        Response.Redirect("Default.aspx");
    }
}