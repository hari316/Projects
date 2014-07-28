<%@ Page Title="Demo of Controls" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Demo.aspx.cs" Inherits="WebApplication1.Demo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
     Demo of Controls in Ado.net
    </h2>
    <center>
   

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:companyConnectionString %>" 
            SelectCommand="SELECT * FROM [EMPLOYEE] ORDER BY [Ssn]"           
            InsertCommand="INSERT INTO EMPLOYEE(Fname, Lname, Ssn, Designation, Dno, Super_ssn) VALUES (@Fname, @Lname, @Ssn, @Designation, @Dno, @Super_ssn)"           
            UpdateCommand="UPDATE EMPLOYEE SET Fname = @Fname, Lname = @Lname, Designation = @Designation, Dno = @Dno, Super_ssn = @Super_ssn where Ssn = @Ssn" 
            DeleteCommand="DELETE FROM EMPLOYEE where Ssn = @Ssn" >
            
            
            
            <InsertParameters>
                <asp:Parameter Name = "Fname" Type = "String" />
                <asp:Parameter Name = "Lname" Type = "String" />
                <asp:Parameter Name = "Ssn" Type = "Int32" />
                <asp:Parameter Name = "Designation" Type = "String" />
                <asp:Parameter Name = "Dno" Type = "Int32" />
                <asp:Parameter Name = "Super_ssn" Type = "Int32" />
            </InsertParameters>
                
            <UpdateParameters>
                <asp:Parameter Name = "Fname" Type = "String" />
                <asp:Parameter Name = "Lname" Type = "String" />
                <asp:Parameter Name = "Designation" Type = "String" />
                <asp:Parameter Name = "Dno" Type = "Int32" />
                <asp:Parameter Name = "Super_ssn" Type = "Int32" />
            </UpdateParameters>

            <DeleteParameters>
                 <asp:Parameter Name = "Ssn" Type = "Int32" />
            </DeleteParameters>
            </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:companyConnectionString %>" 
            SelectCommand="SELECT * FROM [DEPARTMENT] ORDER BY [Dnumber], [Dname]"></asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
            ConnectionString="<%$ ConnectionStrings:companyConnectionString %>" 
            SelectCommand="SELECT * FROM [DEPT_LOCATIONS] ORDER BY [Dnumber]"></asp:SqlDataSource>

      <h2> Company Database Contents : </h2><br />

        <asp:Table ID="Table1"  runat="server" Height="117px">
        
        <asp:TableRow>
        <asp:TableHeaderCell ColumnSpan = "1">EMPLOYEE TABLE</asp:TableHeaderCell>
        <asp:TableHeaderCell ColumnSpan = "1">DEPARTMENT TABLE</asp:TableHeaderCell>
        <asp:TableHeaderCell ColumnSpan = "1">DEPT_LOCATIONS</asp:TableHeaderCell>
        </asp:TableRow>
        <asp:TableRow><asp:TableCell>&nbsp </asp:TableCell></asp:TableRow>
        <asp:TableRow>
        
            <asp:TableCell>
            
            </asp:TableCell>
      <%--       <asp:TableCell VerticalAlign="Top"><asp:GridView ID="GridView2" runat="server" DataSourceID="SqlDataSource2">
        </asp:GridView></asp:TableCell>

            <asp:TableCell VerticalAlign="Top"><asp:GridView ID="GridView3" runat="server" DataSourceID="SqlDataSource3">
        </asp:GridView>
        </asp:TableCell>

     --%>
        </asp:TableRow>
        </asp:Table>

        <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" 
            AllowPaging="True" 
            AutoGenerateEditButton="True" EnableSortingAndPagingCallbacks="True" 
            CellPadding="4" ForeColor="#333333" GridLines="None" 
            AutoGenerateColumns="False" DataKeyNames="Ssn">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowDeleteButton="True" />
                <asp:BoundField DataField="Fname" HeaderText="Fname" SortExpression="Fname" />
                <asp:BoundField DataField="Lname" HeaderText="Lname" SortExpression="Lname" />
                <asp:BoundField DataField="Ssn" HeaderText="Ssn" ReadOnly="True" 
                    SortExpression="Ssn" />
                <asp:BoundField DataField="Designation" HeaderText="Designation" 
                    SortExpression="Designation" />
                <asp:BoundField DataField="Dno" HeaderText="Dno" SortExpression="Dno" />
                <asp:BoundField DataField="Super_ssn" HeaderText="Super_ssn" 
                    SortExpression="Super_ssn" />
            </Columns>
            <EditRowStyle BorderStyle="Solid" />
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
            
        </asp:GridView>
        <asp:TextBox ID="FnameTb" runat="server"></asp:TextBox>
        <asp:TextBox ID="LnameTb" runat="server"></asp:TextBox>
        <asp:TextBox ID="SsnTb" runat="server"></asp:TextBox>
        <asp:TextBox ID="DesignationTb" runat="server"></asp:TextBox>
        <asp:TextBox ID="DnoTb" runat="server"></asp:TextBox>
        <asp:TextBox ID="Super_ssnTb" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Insertion" />

        <asp:DetailsView ID="DetailsView1" runat="server" AllowPaging="True" 
            AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" 
            AutoGenerateInsertButton="True" DataSourceID="SqlDataSource1" Height="50px" 
            Width="125px" AutoGenerateRows="False" DataKeyNames="Ssn">
            <Fields>
                <asp:BoundField DataField="Fname" HeaderText="Fname" SortExpression="Fname" />
                <asp:BoundField DataField="Lname" HeaderText="Lname" SortExpression="Lname" />
                <asp:BoundField DataField="Ssn" HeaderText="Ssn" ReadOnly="True" 
                    SortExpression="Ssn" />
                <asp:BoundField DataField="Designation" HeaderText="Designation" 
                    SortExpression="Designation" />
                <asp:BoundField DataField="Dno" HeaderText="Dno" SortExpression="Dno" />
                <asp:BoundField DataField="Super_ssn" HeaderText="Super_ssn" 
                    SortExpression="Super_ssn" />
            </Fields>
        </asp:DetailsView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>

    </center>
</asp:Content>
