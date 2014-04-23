using System;
using System.Configuration;
using System.Data.OleDb;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["id"] != null)
        {
            lblUsername.Text = (string)Session["id"];

            OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["GeneralDatabase"].ConnectionString);
            connection.Open();

            OleDbCommand command = new OleDbCommand("SELECT nom_fichier_avatar FROM Utilisateurs WHERE nom_utilisateur =@id", connection);
            command.Parameters.Add(new OleDbParameter("id", Session["id"].ToString()) { OleDbType = OleDbType.VarChar, Size = 255 });
            OleDbDataReader datareader = command.ExecuteReader();
            if (datareader.Read())
            {
                if (datareader.IsDBNull(0) || string.IsNullOrEmpty((string)datareader[0]))
                {
                    imgAvatar.ImageUrl = "~/assets/image/avatar.jpg";
                }
                else
                {
                    imgAvatar.ImageUrl = "~/assets/image/" + (string)datareader[0];
                }
            }
        }
    }

    protected void btnDeconnexion_Click(object sender, EventArgs e)
    {
        Session["id"] = null;
    }

    protected void BtnConnection_Click(object sender, EventArgs e)
    {
        OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["GeneralDatabase"].ConnectionString);
        connection.Open();

        OleDbCommand command = new OleDbCommand("SELECT mot_de_passe FROM Utilisateurs WHERE nom_utilisateur = @txbLogin", connection);
        command.Parameters.Add(new OleDbParameter("txbLogin", txbLogin.Text) { OleDbType = OleDbType.VarChar, Size = 255 });
        OleDbDataReader datareader = command.ExecuteReader();

        if (datareader.Read())
        {
            if ((string)datareader[0] == txbMPasse.Text)
            {
                Session["id"] = txbLogin.Text;
            }
            else
            {
                lblErrorLogin.Text = "Cet identifiant/mot de passe est incorrect";
            }
        }
        else
        {
            lblErrorLogin.Text = "Cet identifiant/mot de passe est incorrect";
        }
        if (Session["id"] != null)
        {
            lblUsername.Text = (string)Session["id"];
        }
    }
}