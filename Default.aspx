<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>The Weak End</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">

    <!--Le Forum-->
    <div>

        <asp:Table runat="server" ID="menuSujet" CssClass="maxWidth">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Width="15%">Auteur</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="70%" CssClass="centerText">Sujet</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="15%">Date de creation</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
        <br/>

        <%if (Session["id"] != null)
          { %>
        <asp:Table runat="server" CssClass="maxWidth">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" Width="15%" HorizontalAlign="Right" VerticalAlign="Middle">
                <label id="lblTitre">Titre</label>
                </asp:TableCell>
                <asp:TableCell ID="TableCell1" runat="server" Width="70%">
                    <asp:TextBox
                        runat="server"
                        ID="txbTitre"
                        Width="100%">
                    </asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ID="TableCell2" runat="server" Width="15%">
                    <asp:RequiredFieldValidator
                        ID="requiredFieldVal1"
                        runat="server"
                        ControlToValidate="txbTitre"
                        Display="Static"
                        ErrorMessage="*Vous devez entrer un titre">
                    </asp:RequiredFieldValidator>
                </asp:TableCell>

            </asp:TableRow>
            <asp:TableRow ID="TableRow1" runat="server">
                <asp:TableCell ID="TableCell3" runat="server" HorizontalAlign="Right" VerticalAlign="Middle">
                <label id="lblMessage">Message</label>
                </asp:TableCell>
                <asp:TableCell ID="TableCell4" runat="server">
                    <asp:TextBox runat="Server" TextMode="MultiLine" Height="100" ID="txtMessage" Width="100%"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ID="TableCell5" runat="server">
                    <asp:RequiredFieldValidator
                        ID="RequiredFieldValidator2"
                        runat="server"
                        ControlToValidate="txtMessage"
                        Display="Static"
                        ErrorMessage="*Vous devez entrer un message">
                    </asp:RequiredFieldValidator>
                </asp:TableCell>

            </asp:TableRow>

            <asp:TableRow runat="server">
                <asp:TableCell ID="TableCell6" runat="server">
                </asp:TableCell>
                <asp:TableCell ID="TableCell8" runat="server">
                    <asp:Button
                        Text="Envoyer"
                        runat="server"
                        ID="btnSujet"
                        Width="100%"
                        OnClick="btnSujet_Click" />
                </asp:TableCell>
                <asp:TableCell ID="TableCell7" runat="server">
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

        <%}
          else
          { %>
        <asp:Label Text="Veuillez vous connecter pour créer un sujet" runat="server" />
        <%} %>
    </div>
</asp:Content>
