<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PageAdmin.aspx.cs" Inherits="Default2" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <%if (Session["id"] != null && IsAdministrator())
      {%>

    <asp:GridView ID="gridUsers" runat="server" AutoGenerateColumns="False" DataKeyNames="nom_utilisateur" DataSourceID="SqlMainDataSource" AutoGenerateEditButton="true" AllowSorting="True">
        <Columns>
            <asp:BoundField DataField="nom_utilisateur" HeaderText="nom_utilisateur" ReadOnly="True" SortExpression="nom_utilisateur"></asp:BoundField>
            <asp:BoundField DataField="nom_fichier_avatar" HeaderText="nom_fichier_avatar" SortExpression="nom_fichier_avatar"></asp:BoundField>
            <asp:CheckBoxField DataField="moderateur" HeaderText="moderateur" SortExpression="moderateur"></asp:CheckBoxField>
            <asp:CheckBoxField DataField="administrateur" HeaderText="administrateur" SortExpression="administrateur"></asp:CheckBoxField>
            <asp:CheckBoxField DataField="banni" HeaderText="banni" SortExpression="banni"></asp:CheckBoxField>
        </Columns>
    </asp:GridView>

    <asp:SqlDataSource runat="server" ID="SqlMainDataSource" ConnectionString='<%$ ConnectionStrings:ConnectionString %>' ProviderName='<%$ ConnectionStrings:ConnectionString.ProviderName %>' SelectCommand="SELECT * FROM [Utilisateurs]" DeleteCommand="DELETE FROM [Utilisateurs] WHERE [nom_utilisateur] = ?" InsertCommand="INSERT INTO [Utilisateurs] ([nom_utilisateur], [mot_de_passe], [nom_fichier_avatar], [sel], [moderateur], [administrateur]) VALUES (?, ?, ?, ?, ?, ?)" UpdateCommand="UPDATE [Utilisateurs] SET [mot_de_passe] = ?, [nom_fichier_avatar] = ?, [sel] = ?, [moderateur] = ?, [administrateur] = ? WHERE [nom_utilisateur] = ?">
        <DeleteParameters>
            <asp:Parameter Name="nom_utilisateur" Type="String"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="nom_utilisateur" Type="String"></asp:Parameter>
            <asp:Parameter Name="mot_de_passe" Type="Object"></asp:Parameter>
            <asp:Parameter Name="nom_fichier_avatar" Type="String"></asp:Parameter>
            <asp:Parameter Name="sel" Type="Object"></asp:Parameter>
            <asp:Parameter Name="moderateur" Type="Boolean"></asp:Parameter>
            <asp:Parameter Name="administrateur" Type="Boolean"></asp:Parameter>
            <asp:Parameter Name="banni" Type="Boolean"></asp:Parameter>
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="mot_de_passe" Type="Object"></asp:Parameter>
            <asp:Parameter Name="nom_fichier_avatar" Type="String"></asp:Parameter>
            <asp:Parameter Name="sel" Type="Object"></asp:Parameter>
            <asp:Parameter Name="moderateur" Type="Boolean"></asp:Parameter>
            <asp:Parameter Name="administrateur" Type="Boolean"></asp:Parameter>
            <asp:Parameter Name="nom_utilisateur" Type="String"></asp:Parameter>
            <asp:Parameter Name="banni" Type="Boolean"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>

    <%}
      else
      {%>
    <%}%>
</asp:Content>