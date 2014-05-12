<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="forum.aspx.cs" Inherits="forum" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>The Weak End</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div>
        <asp:Label Text="" runat="server" ID="lblSuccess" />
        <asp:Label runat="server" Text="" ID="lblSujet"></asp:Label>
        <asp:Table runat="server" ID="menuMessage" CssClass="maxWidth">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Width="15%">Auteur</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="70%" CssClass="centerText">Message</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="15%">Créé il y a :</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
        <br/>

        <%if (Session["ID"] != null)
          {%>
        <div style="text-align: center" Width="70%">
            <asp:TextBox TextMode="MultiLine" runat="server" Height="100" ID="txtReponseMessage" CssClass="center"></asp:TextBox><br/>
            <asp:Button runat="server" ID="btnEnvoyerMessage" Text="Envoyer" OnClick="btnEnvoyerMessage_Click" CssClass="btn center"/>
        </div>
        <%}
          else
          {%>
        <asp:Label runat="server" ID="lblDevoirConnecter">Vous devez être connecté pour poster un message</asp:Label>
        <%} %>
    </div>
</asp:Content>