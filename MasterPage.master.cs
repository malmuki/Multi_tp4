using System;
using System.Configuration;
using System.Data.OleDb;
using System.Security.Cryptography;
using System.Linq;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_prerender(object sender, EventArgs e)
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
                imgAvatar.Width = 50;
                imgAvatar.Height = 50;
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


        OleDbCommand command = new OleDbCommand("SELECT mot_de_passe, sel FROM Utilisateurs WHERE nom_utilisateur = @txbLogin", connection);
        command.Parameters.Add(new OleDbParameter("txbLogin", txbLogin.Text) { OleDbType = OleDbType.VarChar, Size = 255 });
        OleDbDataReader datareader = command.ExecuteReader();

        if (datareader.Read())
        {
            string mdp = txbMPasse.Text;  //TextBox du mot de passe
            byte[] tabSels = (byte[])datareader[1]; //Tableau pour le sel
            byte[] motDePasseBaseDonnees = (byte[])datareader[0];

            Rfc2898DeriveBytes hash = new Rfc2898DeriveBytes(mdp, tabSels, 1000);

            byte[] password = hash.GetBytes(24);

            if (motDePasseBaseDonnees.SequenceEqual(password))
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
    }
}