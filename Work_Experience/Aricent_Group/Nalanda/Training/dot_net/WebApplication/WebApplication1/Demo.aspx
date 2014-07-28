<%@ Page Title="Demo of Controls" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Demo.aspx.cs" Inherits="WebApplication1.Demo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
       Demo of Controls
    </h2>
    <center><p>
        Display of Image.
    </p>
            <asp:Image ID="Image1" runat="server" AlternateText="Display of Picture" 
            BorderColor="#CC0000" BorderStyle="Double" DescriptionUrl="~/About.aspx" 
            ImageUrl ="~/Itachi Uchiha Naruto Shippuden-38.jpg"
            Width="288px" Height="224px" />


       
      
      <br /><br />
        A multiline TextBox:
        <asp:TextBox id="TextBox3" TextMode="multiline" runat="server" />
        <br />
        <br />
  
  
    </center>
</asp:Content>
