<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeBehind="grocery.aspx.cs" Inherits="WebApplication1.lisy" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
  
  <center>
  <label  style="font-weight:bolder; font-style:italic; text-decoration:'blink'; color:Purple; font-size:xx-large" >Online Shopping</label>
   <br /><br />
  <asp:Table runat="server" ID="Table1"  Height="49px"  >
  
  <asp:TableRow>
    <asp:TableCell RowSpan="6"> 
      <asp:ListBox ID="ListBox1" runat="server" SelectionMode="Single" BackColor="Orange" Font-Bold="true" ForeColor="ControlDarkDark" >
      <asp:ListItem Text="Electronic Items" Value="1"></asp:ListItem>
      <asp:ListItem Text="Dairy" Value="2"></asp:ListItem>
      <asp:ListItem Text="Clothing" Value="3"></asp:ListItem>
      </asp:ListBox> 
    </asp:TableCell>
        
    <asp:TableCell RowSpan="6">
    <asp:Button ID="Button1" runat="server" Text="Select" OnClick="next_box"/></asp:TableCell>
    <asp:TableCell></asp:TableCell>
    <asp:TableCell RowSpan="6">
              <asp:ListBox ID="ListBox2" runat="server" SelectionMode="Single" Font-Bold="true" BackColor="Azure" ForeColor="DarkOliveGreen" Width="100" ></asp:ListBox>
    </asp:TableCell>
    
   </asp:TableRow>

   <asp:TableRow>
    <asp:TableCell></asp:TableCell>
    
    
     <asp:TableCell>
              
    </asp:TableCell><asp:TableCell>
     </asp:TableCell>
     
   </asp:TableRow>

   <asp:TableRow>
      <asp:TableCell></asp:TableCell><asp:TableCell></asp:TableCell>
      <asp:TableCell ColumnSpan="2">
      Quantity: <asp:DropDownList ID="DropDownList1" runat="server">
              <asp:ListItem Value="1"></asp:ListItem>
              <asp:ListItem Value="2"></asp:ListItem>
              <asp:ListItem Value="3"></asp:ListItem>
              <asp:ListItem Value="4"></asp:ListItem>
              <asp:ListItem Value="5"></asp:ListItem>
              <asp:ListItem Value="6"></asp:ListItem>
              <asp:ListItem Value="7"></asp:ListItem>
              <asp:ListItem Value="8"></asp:ListItem>
              <asp:ListItem Value="9"></asp:ListItem>
              <asp:ListItem Value="10"></asp:ListItem>
              </asp:DropDownList>
              
     </asp:TableCell>
   </asp:TableRow>

   <asp:TableRow>
    <asp:TableCell></asp:TableCell><asp:TableCell></asp:TableCell><asp:TableCell></asp:TableCell>
    <asp:TableCell><asp:Button ID="Button3" runat="server" Text="Purchase" OnClick="purchase_item" /></asp:TableCell> 
   </asp:TableRow><asp:TableRow></asp:TableRow><asp:TableRow></asp:TableRow>
  
  
  </asp:Table>
  <br />

  <asp:Table ID="Table2" runat="server" BorderWidth ="2" Height="16px">
  <asp:TableHeaderRow><asp:TableCell>Bill Information</asp:TableCell></asp:TableHeaderRow>
  <asp:TableRow></asp:TableRow>
  <asp:TableRow>
    <asp:TableCell ColumnSpan="5" RowSpan="5" > 
        <asp:ListBox ID="ListBox3" runat="server" Width="180px" Height="200px">   
        <asp:ListItem Text= "Product           Price           Quantity            Total"></asp:ListItem>  
        </asp:ListBox>
    </asp:TableCell>
     
  </asp:TableRow>

  </asp:Table>
  </center>
</asp:Content>
