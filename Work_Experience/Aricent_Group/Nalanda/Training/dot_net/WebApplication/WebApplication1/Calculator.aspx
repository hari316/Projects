<%@ Page Title="Calculator" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Calculator.aspx.cs"  Inherits="WebApplication1.Calculator" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<style type="text/css">
    .right_align { text-align:right; }
    .highlight {background-color :Red; }
    .nohighlight {background-color :White; }
</style>

<script type="text/javascript" >
    function Button_Click(text_box,button)
    {
        var te
</script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<center>
    <h2>
        Calculator
    </h2>
    
    <h4> Displaying a simple calculator functions </h4> &nbsp;
    <asp:Table ID="Table1"  BorderWidth = "2" runat="server" >
        <asp:TableHeaderRow>
            <asp:TableHeaderCell Font-Underline ="true">Number 1</asp:TableHeaderCell>
            <asp:TableCell ColumnSpan = "3" Font-Underline ="true">Operatons</asp:TableCell>
            <asp:TableHeaderCell Font-Underline ="true">Number 2</asp:TableHeaderCell>    
        </asp:TableHeaderRow>

        <asp:TableRow>
            <asp:TableCell>
                <asp:TextBox ID="num1"  CssClass="right_align" ReadOnly ="false"  runat="server" ></asp:TextBox>
                
            </asp:TableCell>
            <asp:TableCell ColumnSpan ="3">
                <asp:DropDownList ID="op" runat="server">
                    <asp:ListItem Value ="+"></asp:ListItem>
                    <asp:ListItem Value ="-"></asp:ListItem>
                    <asp:ListItem Value ="*"></asp:ListItem>
                    <asp:ListItem Value ="/"></asp:ListItem>
                    </asp:DropDownList>   
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="num2"  CssClass="right_align" ReadOnly ="false"  runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell RowSpan ="1">
                <asp:Button ID="Button1" runat="server"  Text="1" onclick="Button1_Click"/>
                <asp:Button ID="Button2" runat="server"  Text="2" onclick="Button1_Click"/>
                <asp:Button ID="Button3" runat="server"  Text="3" onclick="Button1_Click"/>
            </asp:TableCell>
            <asp:TableCell ColumnSpan ="3"></asp:TableCell>
            <asp:TableCell RowSpan ="1">
                <asp:Button ID="Button10" runat="server" Text="1" onclick="Button2_Click"/>
                <asp:Button ID="Button11" runat="server" Text="2" onclick="Button2_Click"/>
                <asp:Button ID="Button12" runat="server" Text="3" onclick="Button2_Click"/>
            </asp:TableCell>
         </asp:TableRow>

         <asp:TableRow>
            <asp:TableCell RowSpan ="1">
                <asp:Button ID="Button4" runat="server" Text="4" onclick="Button1_Click"/>
                <asp:Button ID="Button5" runat="server" Text="5" onclick="Button1_Click"/>
                <asp:Button ID="Button6" runat="server" Text="6" onclick="Button1_Click"/>
            </asp:TableCell>
            <asp:TableCell ColumnSpan ="3"></asp:TableCell>
            <asp:TableCell RowSpan ="1">
                <asp:Button ID="Button16" runat="server" Text="4" onclick="Button2_Click"/>
                <asp:Button ID="Button17" runat="server" Text="5" onclick="Button2_Click"/>
                <asp:Button ID="Button18" runat="server" Text="6" onclick="Button2_Click"/>
            </asp:TableCell>
            
         </asp:TableRow>

         <asp:TableRow>
            <asp:TableCell RowSpan ="1">
                <asp:Button ID="Button7" runat="server" Text="7" onclick="Button1_Click"/>
                <asp:Button ID="Button8" runat="server" Text="8" onclick="Button1_Click"/>
                <asp:Button ID="Button9" runat="server" Text="9" onclick="Button1_Click"/>
            </asp:TableCell>
            <asp:TableCell ColumnSpan ="3"></asp:TableCell>
            <asp:TableCell RowSpan ="1">
                <asp:Button ID="Button13" runat="server" Text="7" onclick="Button2_Click"/>
                <asp:Button ID="Button14" runat="server" Text="8" onclick="Button2_Click"/>
                <asp:Button ID="Button15" runat="server" Text="9" onclick="Button2_Click"/>
            </asp:TableCell>   
         </asp:TableRow>

         <asp:TableRow>
            <asp:TableCell>
                <asp:Button ID="Button19" runat="server" Text="0" onclick="Button1_Click"/>
                <asp:Button ID="Button22" runat="server" Text="." onclick="Button1_Click"/>
                <asp:Button ID="Buttonc1" runat="server" CommandName = "c1" Text="c" onclick="Button3_Click"/>
            </asp:TableCell>

            <asp:TableCell ColumnSpan ="3"></asp:TableCell>
            <asp:TableCell RowSpan ="1">
                <asp:Button ID="Button20" runat="server" Text="0" onclick="Button2_Click"/>
                <asp:Button ID="Button21" runat="server" Text="." onclick="Button2_Click"/>
                <asp:Button ID="Buttonc2" runat="server" CommandName = "c2" Text="c" onclick="Button3_Click"/>
            </asp:TableCell> 
          </asp:TableRow>

    </asp:Table>
    <br />
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ControlToValidate="num1"  ErrorMessage="Please enter a valid value in Number1 text box" 
                ValidationExpression = "^\d+\.{0,1}\d*$" ValidationGroup="valChkSummay" Display="None" ></asp:RegularExpressionValidator>
    &nbsp;&nbsp;
    <asp:Button ID="calc" runat="server" Text="Calculate" onclick="calc_Click" ValidationGroup="valChkSummay" />
    &nbsp;&nbsp;
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                ControlToValidate="num2" ErrorMessage="Please enter a valid value in Number2 text box" 
                ValidationExpression = "^\d+\.{0,1}\d*$" ValidationGroup="valChkSummay" Display="None" ></asp:RegularExpressionValidator>
    <br />
    &nbsp;<asp:ValidationSummary ID="ValidationSummary1" HeaderText = "ERROR MESSAGE"
        ValidationGroup="valChkSummay" runat="server" BorderColor="#FF3300" 
        BorderWidth="2px" Height="81px" Width="362px" />
    &nbsp;<asp:Table ID="Table2" runat="server">
    
    <asp:TableHeaderRow>
        <asp:TableHeaderCell ColumnSpan ="4">RESULT :</asp:TableHeaderCell>
        <asp:TableHeaderCell>
            <asp:TextBox ID="res" runat="server" Text ="0" CssClass ="right_align" ></asp:TextBox></asp:TableHeaderCell>
    </asp:TableHeaderRow>
    </asp:Table>
</center>     
</asp:Content>
