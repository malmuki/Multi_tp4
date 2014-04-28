<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pageActualite.aspx.cs" Inherits="pageActualite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <h1>NEWS!</h1>
    <asp:Table runat="server" ID="tableActualite">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell>Auteur</asp:TableHeaderCell>
            <asp:TableHeaderCell>Message</asp:TableHeaderCell>
            <asp:TableHeaderCell>Date de creation</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>

    <%if(Session["id"] != null && IsModerator()){%>
    <asp:TextBox TextMode="MultiLine" runat="server" ID="txtActualite"></asp:TextBox>
    <asp:Button runat="server" ID="btnEnvoyerMessage" Text="Envoyer" OnClick="btnEnvoyerMessage_Click"/>
    <%}else{%>
    <%}%>
</asp:Content>

