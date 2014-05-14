<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InfoPerso.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Information Personnelle</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <%if (Session["id"] == null)
      { %>
    <label>veullier vous connecter pour contiuer</label>
    <asp:LinkButton ID="LinkButton1"
        runat="server"
        Text="Redirection"
        PostBackUrl="~/Default.aspx"
        CausesValidation="false"></asp:LinkButton>

    <%}
      else
      { %>
    <div>

        <asp:Label Text="" runat="server" ID="lblUsername"/>

        <br />

        <asp:Label Text="" runat="server" id="lblEmail"/>

        <asp:Label Text="" runat="server" ID="lblModoOuAdmin"/>

        <br />

        <asp:Label Text="" runat="server" ID="lblsucces" CssClass="label-success" />

        <asp:Image ImageUrl="#" runat="server" ID="imgAvatar" class="pull-right"/>

        <asp:FileUpload
            runat="server"
            ID="fileUpload1" />
        <asp:RequiredFieldValidator 
            ID="RequiredFieldValidator1"
            runat="Server"
            ControlToValidate="fileUpload1"
            ErrorMessage="l'image est requise pour faire un changement"
            class="alert-danger"> </asp:RequiredFieldValidator>

        <asp:CustomValidator
            runat="server"
            ID="imageValidator"
            ErrorMessage="l'image est trop grande ou invalide(max de 180px par 180px)"
            ControlToValidate="fileUpload1"
            OnServerValidate="imageValidator_ServerValidate"
            class="alert-danger"> </asp:CustomValidator>
        <br />
        <asp:Button ID="Button1"
            runat="server"
            OnClick="btnImage_Click"
            Text="Modifier l'avatar"
            class="btn" />
    </div>

    <%} %>

</asp:Content>

