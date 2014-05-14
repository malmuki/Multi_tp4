<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Inscription.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Inscription</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">

    <div>
        <label class="alert-success" style="display: none">L'incription c'est bien effectué</label>
    </div>
    <div>
        <asp:Label Text="* Ces champs sont obligatoires" runat="server" class="alert-warning" />
    </div>

    <asp:Table runat="server" Width="600">
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" Width="20%" HorizontalAlign="right">

                <asp:Label runat="server" ID="lblPseudo">*Pseudonyme</asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell2" runat="server" Width="40%">

                <asp:TextBox
                    runat="server"
                    ID="txbPseudo">
                </asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell1" runat="server" Width="40%">

                <asp:RequiredFieldValidator
                    runat="server"
                    ID="valdateurReqNom"
                    ErrorMessage="Le pseudonyme est obligatoire"
                    ControlToValidate="txbPseudo"
                    class="alert-danger">
                </asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow1" runat="server">

            <asp:TableCell ID="TableCell3" runat="server" HorizontalAlign="right">

                <asp:Label runat="server" ID="lblEmail">*Email</asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell4" runat="server">

                <asp:TextBox
                    runat="server"
                    ID="txbEmail"
                    TextMode="Email">
                </asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell5" runat="server">

                <asp:RequiredFieldValidator
                    runat="server"
                    ID="valdateurReqEmail"
                    ErrorMessage="Le Email est obligatoire"
                    ControlToValidate="txbEmail"
                    class="alert-danger">
                </asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator
                    ID="regular1"
                    runat="server"
                    ErrorMessage="Mail erroné"
                    ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
                    ControlToValidate="txbEmail"
                    class="alert-danger">
                </asp:RegularExpressionValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow2" runat="server">
            <asp:TableCell ID="TableCell6" runat="server" HorizontalAlign="right">

                <asp:Label runat="server" ID="lblMPasse">*Mot de passe</asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell7" runat="server">

                <asp:TextBox
                    TextMode="Password"
                    runat="server"
                    ID="txbMPasse">
                </asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell8" runat="server">

                <asp:RequiredFieldValidator
                    runat="server"
                    ID="valdateurReqPass"
                    ErrorMessage="Le mot de passe est obligatoire"
                    ControlToValidate="txbMPasse"
                    class="alert-danger">
                </asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow3" runat="server">
            <asp:TableCell ID="TableCell9" runat="server" HorizontalAlign="right">
                <asp:Label runat="server" ID="Label1">Avatar</asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell10" runat="server">
                <asp:FileUpload
                    runat="server"
                    ID="fileUpload" />
            </asp:TableCell>
            <asp:TableCell ID="TableCell11" runat="server">
                <asp:CustomValidator
                    runat="server"
                    ID="imageValidator"
                    ErrorMessage="l'image n'est pas valide"
                    ControlToValidate="fileUpload"
                    OnServerValidate="imageValidator_ServerValidate"
                    class="alert-danger">
                </asp:CustomValidator>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <div>
        <asp:Button
            ID="BtnInscription"
            runat="server"
            Text="S'Inscrire"
            OnClick="BtnInscription_Click" />
    </div>
</asp:Content>