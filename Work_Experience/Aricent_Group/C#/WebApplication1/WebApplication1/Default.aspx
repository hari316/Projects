<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
    .left_align { text-align:left; }
</style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to ASP.NET!
    </h2>
    <p>
        To learn more about ASP.NET visit <a href="http://www.asp.net" title="ASP.NET Website">www.asp.net</a>.
    </p>
    <p>
        You can also find <a href="http://go.microsoft.com/fwlink/?LinkID=152368&amp;clcid=0x409"
            title="MSDN ASP.NET Docs">documentation on ASP.NET at MSDN</a>.
    </p>

    
    <br/>
    <center>
    <h2> .NET DEMO </h2><br />
        <asp:Table ID="Table1" runat="server" BorderWidth ="2" Height="81px" 
            Width="217px">

        <asp:TableRow> 
            <asp:TableHeaderCell>1</asp:TableHeaderCell><asp:TableCell></asp:TableCell>
            <asp:TableCell CssClass="left_align">
                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl ="~/list.aspx" >  Multi select control
                </asp:HyperLink>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow> 
            <asp:TableHeaderCell>2</asp:TableHeaderCell><asp:TableCell></asp:TableCell>
            <asp:TableCell CssClass="left_align">
                <asp:HyperLink  ID="HyperLink1" runat="server" NavigateUrl ="~/Calculator.aspx">  Calculator
                </asp:HyperLink>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow> 
            <asp:TableHeaderCell>3</asp:TableHeaderCell><asp:TableCell></asp:TableCell>
            <asp:TableCell CssClass="left_align">
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl ="~/grocery.aspx" >  Online Shopping
                </asp:HyperLink>
            </asp:TableCell>
        </asp:TableRow>
    
    </asp:Table>
    </center>
</asp:Content>
