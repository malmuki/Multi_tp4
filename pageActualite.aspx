<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pageActualite.aspx.cs" Inherits="pageActualite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <h1>NEWS!</h1>
    <asp:Table runat="server" ID="tableActualite" Width="1024" class="tableMessage maxWight">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell Width="20%">Auteur</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="60%">Message</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="20%">Date de creation</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>

    <%if(Session["id"] != null && IsModerator()){%>
    <asp:TextBox TextMode="MultiLine" runat="server"  Width="500" Height="100" ID="txtActualite"></asp:TextBox><br/>
    <asp:Button runat="server" ID="btnEnvoyerMessage" Text="Envoyer" OnClick="btnEnvoyerMessage_Click"/>
    <%}else{%>
    <%}%>
</asp:Content>

