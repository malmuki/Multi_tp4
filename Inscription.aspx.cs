﻿using System;
using System.Configuration;
using System.Data.OleDb;
using System.Security.Cryptography;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void BtnInscription_Click(object sender, EventArgs e)
    {
        if (this.IsValid)
        {
            string userName = txbPseudo.Text;
            string courriel = txbEmail.Text;
            string mdp = txbMPasse.Text;  //TextBox du mot de passe
            byte[] tabSels = new byte[24];  //Tableau pour le sel

            RNGCryptoServiceProvider rdmSel = new RNGCryptoServiceProvider();

            rdmSel.GetBytes(tabSels);  //Creation du sel aléatoire

            Rfc2898DeriveBytes hash = new Rfc2898DeriveBytes(mdp, tabSels, 1000);

            byte[] password = hash.GetBytes(24);  //Obtention du mot de passe crypté

            OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["GeneralDatabase"].ConnectionString);
            connection.Open();

            OleDbCommand command = new OleDbCommand(" SELECT Count(nom_utilisateur) FROM Utilisateurs WHERE nom_utilisateur = @userName;", connection);
            command.Parameters.Add(new OleDbParameter("userName", userName) { OleDbType = OleDbType.VarChar, Size = 255 });
            command.Prepare();

            if ((int)command.ExecuteScalar() < 1)
            {
                string image = "";
                if (fileUpload.HasFile)
                {
                    image = userName + System.IO.Path.GetExtension(fileUpload.FileName);
                    fileUpload.SaveAs(Server.MapPath("assets/image/" + image));
                }

                command = new OleDbCommand("INSERT INTO Utilisateurs (nom_utilisateur, mot_de_passe, nom_fichier_avatar, sel, courriel) VALUES (@userName,@password, @image, @sel, @courriel)", connection);
                command.Parameters.Add(new OleDbParameter("userName", userName) { OleDbType = OleDbType.VarChar, Size = 255 });
                command.Parameters.Add(new OleDbParameter("password", password) { OleDbType = OleDbType.VarBinary, Size = 255 });
                command.Parameters.Add(new OleDbParameter("image", image) { OleDbType = OleDbType.VarChar, Size = 255 });
                command.Parameters.Add(new OleDbParameter("sel", tabSels) { OleDbType = OleDbType.VarBinary, Size = 255 });
                command.Parameters.Add(new OleDbParameter("courriel", courriel) { OleDbType = OleDbType.VarChar, Size = 255 });
                command.ExecuteNonQuery();
            }

            connection.Close();
            Server.Transfer("Default.aspx");
        }
    }

    protected void imageValidator_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
    {
        if (fileUpload.HasFile)
        {
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(fileUpload.FileContent);
                if (img.Height > 180 || img.Width > 180)
                {
                    args.IsValid = false;
                }
            }
            catch
            {
                args.IsValid = false;
            }
        }
    }
}