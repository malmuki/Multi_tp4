using System;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;
using System.Web.UI.HtmlControls;

public partial class pageActualite : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ID = Request["id"];
        string nomSujet = Request["titre"];

        OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["GeneralDatabase"].ConnectionString);
        connection.Open();

        OleDbCommand command = new OleDbCommand("SELECT messages.message, messages.date_ecriture, messages.auteur, Utilisateurs.nom_fichier_avatar FROM Utilisateurs INNER JOIN messages ON Utilisateurs.[nom_utilisateur] = messages.[auteur] WHERE sujet = 1 ORDER BY messages.date_ecriture;", connection);
        command.Parameters.Add(new OleDbParameter("id", ID) { OleDbType = OleDbType.VarChar, Size = 255 });
        OleDbDataReader datareader = command.ExecuteReader();

        while (datareader.Read())
        {
            TableRow tableRow = new TableRow();
            TableCell userCell = new TableCell();
            HtmlGenericControl imgDiv = new HtmlGenericControl("div");
            HtmlGenericControl pseudoDiv = new HtmlGenericControl("div");

            Image avatar = new Image();
            avatar.ImageUrl = "~/App_Data/image/" + (string)datareader[3];
            imgDiv.Controls.Add(avatar);
            pseudoDiv.InnerText = (string)datareader[2];
            userCell.Controls.Add(imgDiv);
            userCell.Controls.Add(pseudoDiv);

            tableRow.Cells.Add(userCell);
            tableRow.Cells.Add(new TableCell() { Text = datareader.IsDBNull(0) ? "" : ((string)datareader[0]) });
            tableRow.Cells.Add(new TableCell() { Text = datareader.IsDBNull(1) ? "" : ((DateTime)datareader[1]).ToLongTimeString() });
            tableActualite.Rows.Add(tableRow);
        }
        connection.Close();
    }

   public bool IsModerator()
    {
         OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["GeneralDatabase"].ConnectionString);
        connection.Open();

        OleDbCommand command = new OleDbCommand("SELECT moderateur FROM Utilisateurs WHERE nom_utilisateur='"+(string)Session["id"]+"';", connection);
       OleDbDataReader datareader = command.ExecuteReader();

       if (datareader.Read())
       {
           return (bool) datareader[0];
       }
       else
       {
           return false;
       }
    }
   protected void btnEnvoyerMessage_Click(object sender, EventArgs e)
   {
       OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["GeneralDatabase"].ConnectionString);
        connection.Open();

        OleDbCommand command = new OleDbCommand("INSERT INTO messages (sujet, auteur, date_ecriture, message) VALUES (@sujet, @auteur, @date, @message)", connection);

        command.Parameters.Add(new OleDbParameter("sujet", 1)
        {
            OleDbType = OleDbType.Integer
        });


        command.Parameters.Add(new OleDbParameter("auteur", Session["id"])
        {
            OleDbType = OleDbType.VarChar, Size = 255
        });

        command.Parameters.Add(new OleDbParameter("date", DateTime.Now)
        {
            OleDbType = OleDbType.Date
        });


        command.Parameters.Add(new OleDbParameter("message", txtActualite.Text)
        {
            OleDbType = OleDbType.VarChar, Size = 65536 //Changer le nombre de caracteres pour un text long.
        });

        command.ExecuteNonQuery();
        connection.Close();
   }
}