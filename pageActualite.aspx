<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pageActualite.aspx.cs" Inherits="pageActualite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <h1>NEWS!</h1>
    <asp:Table runat="server" ID="tableActualite" CssClass="tableMessage maxWidth">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell Width="15%">Auteur</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="70%" CssClass="centerText">Message</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="15%">Date de creation</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
    <br/>

    <%if (Session["id"] != null && IsModerator())
      {%>
    <div style="text-align: center" Width="100%">
        <asp:TextBox TextMode="MultiLine" runat="server" Width="70%" Height="100" ID="txtActualite" CssClass="center"></asp:TextBox><br />
        <asp:Button runat="server" ID="btnEnvoyerMessage" Text="Envoyer" Width="70%" OnClick="btnEnvoyerMessage_Click" />
    </div>
    <%}%>
</asp:Content>

