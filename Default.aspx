<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>The Weak End</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">

    <!--Le Forum-->
    <div>

        <asp:Table runat="server" ID="menuSujet" Width="1024">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Width="20%">Auteur</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="60%">Sujet</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="20%">Date de creation</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>

        <%if (Session["id"] != null)
          { %>
        <label id="lblTitre">Titre</label>

        <asp:TextBox
            runat="server"
            ID="txbTitre">
        </asp:TextBox>
        <asp:RequiredFieldValidator
            ID="requiredFieldVal1"
            runat="server"
            ControlToValidate="txbTitre"
            Display="Static"
            ErrorMessage="*Vous devez entrer un titre">
        </asp:RequiredFieldValidator><br />

        <label id="lblMessage">Message</label>
        <asp:TextBox runat="Server" TextMode="MultiLine" Width="500" Height="100" ID="txtMessage"></asp:TextBox>
        <asp:RequiredFieldValidator
            ID="RequiredFieldValidator2"
            runat="server"
            ControlToValidate="txtMessage"
            Display="Static"
            ErrorMessage="Vous devez entrer un message">
        </asp:RequiredFieldValidator>
        <br />
        <asp:Button
            Text="Envoyer"
            runat="server"
            ID="btnSujet"
            OnClick="btnSujet_Click" />

        <%}
          else
          { %>
        <asp:Label Text="Veuillez vous connecter pour créer un sujet" runat="server" />
        <%} %>
    </div>
</asp:Content>