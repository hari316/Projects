<%@ Page Title="Multi-Select List " Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="false"
    CodeBehind="list.aspx.cs" Inherits="WebApplication1.list" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript" language="javascript">
    function fun1() {
        alert("test message");
    }

</script>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <center>
    <h2>
        MULTI SELECTION OPTIONS
    </h2>
        <h5>Display of Multi select options with some control operations</h5>
    <br />
     
    <asp:Table ID="Table1"  BorderWidth = "2"  runat="server">

    <asp:TableHeaderRow> <asp:TableHeaderCell Font-Underline="true" >Mobile List 1 Items</asp:TableHeaderCell>
                            <asp:TableCell></asp:TableCell>
                         <asp:TableHeaderCell Font-Underline="true">Controls</asp:TableHeaderCell>
                            <asp:TableCell></asp:TableCell>
                         <asp:TableHeaderCell Font-Underline="true">Mobile List 2 Items</asp:TableHeaderCell>
    </asp:TableHeaderRow>

    <asp:TableRow><asp:TableCell>&nbsp;</asp:TableCell></asp:TableRow>
    <asp:TableRow>
         <asp:TableCell RowSpan = "7" >
            <asp:ListBox ID="ListBox1" SelectionMode="Multiple" runat="server" Height="72px" Width="110px">
                <asp:ListItem Text="Apple" value ="1"/>
                <asp:ListItem Text="Nokia" value ="2"/>
                <asp:ListItem Text="Samsung" value ="3"/>
                <asp:ListItem Text="Micromax" value ="4"/>
            </asp:ListBox>
        </asp:TableCell>
        
        <asp:TableCell>
        </asp:TableCell>

        <asp:TableCell BorderWidth ="0" >
            <asp:Button ID="Button1"  runat="server" Text="--->" Height="25px" Width="35px" 
          OnClick ="Button1_Click" />
        </asp:TableCell>

        <asp:TableCell>
        </asp:TableCell>
        
        <asp:TableCell RowSpan = "7"> 
            <asp:ListBox ID="ListBox2" SelectionMode="Multiple" runat="server" Height="72px" Width="110px">
            </asp:ListBox>
        </asp:TableCell>
         
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell> </asp:TableCell>
        <asp:TableCell BorderWidth ="0" >
            <asp:Button ID="Button2" runat="server" Text="<---" Height="25px" Width="35px" 
            onclick="Button2_Click" />
        </asp:TableCell>
        <asp:TableCell> </asp:TableCell>
    </asp:TableRow>
    
    <asp:TableRow>
        <asp:TableCell> </asp:TableCell>
        <asp:TableCell BorderWidth ="0" >
            <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Delete" />
        </asp:TableCell>
        <asp:TableCell> </asp:TableCell>
    </asp:TableRow>
    
    <asp:TableRow>
        <asp:TableCell> </asp:TableCell>
        <asp:TableCell BorderWidth ="0" >
            <asp:Button ID="Button4" runat="server" onclick="Button4_Click" Text="Delete_All" />
     </asp:TableCell>
        <asp:TableCell> </asp:TableCell>
    </asp:TableRow>
    
    </asp:Table>
     
</center>
</asp:Content>
