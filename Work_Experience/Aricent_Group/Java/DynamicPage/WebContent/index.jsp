<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
<title>Employee Details</title>
<script src="js/jquery-1.9.0.min.js" type="text/javascript" ></script>
<script language="javascript" type="text/javascript">  

	var counter = 0;
	
	$(document).ready(function() {
	  $("button").click(function(){
	    alert("Reseting the results .. Try Again  :)");
		$("#divResult").hide();
		$("#divEmpDetails").hide();
		$("#divTableFunc").hide();
	  });
	});
	
	function validityCheck(object,event) { 
		 reg = /^\d+$/;
	     if(!reg.test(object.value)) {
	         alert("Please enter a valid Numeric value only"); 
			 object.value = "";
			 object.focus();
	         return false;
	     }
	     if(event.keyCode == 13) {
				nullCheck();
			 }
	     return true;
	}
	
	function nullCheck() {
		var numObject = document.forms["empForm"]["keySearch"];
		if(numObject.value == null || numObject.value == '') {
			numObject.value = 0;		
			alert("Search for details of All employees");
			//numObject.focus();
			//return false;
		}
		searchList();
		return true;
	}

	function searchList() {
		clearTable(1);
		$.ajax({
			type: 'Get',
			url: 'Result',
			data: {keySearch: $("input#kSearch").val()},
			dataType: 'json',
			success: function(result) {
				//alert(result.Success.Flag);
				if(result.Success.Flag == 0) {
					alert("No records Found !!!. Please enter a valid Employee ID");
					document.getElementById("divResult").style.display="none";	
				 }else if(result.Success.Flag == 1){ 
					$("#divResult").show();
					createResTableContent(result.Success,1);
				 }else {
					 var empList = result.All;				 
					 var rowCount = result.Success.Count;
					 $("#divResult").show();
					 for(var i=0;i<rowCount;i++) {
						 createResTableContent(empList[i],1);
					 }
				}
			}
		});
	}
	
	function showList() {	
		clearTable(0);		
		$.ajax({
			type: 'Get',
			url: 'LoadEmpData',
			dataType: 'json',
			success: function(result) {
				if(result.Success.Flag == 0) {
					alert("ERROR: No records Found !!! ...");
					document.getElementById("divEmpDetails").style.display="none";	
				 }else {
					 var empList = result.All;				 
					 var rowCount = result.Success.Count;
					 $("#divEmpDetails").show();
					 $("#divTableList").hide();
					 $("#divTableFunc").show();
					 counter = rowCount;
					 for(var i=0;i<rowCount;i++) {
						 createEmpTableContent(empList[i]);
					 }
				}
			}
		});		
	}	
	
function createResTableContent(obj,rIndex) {		
		
		//document.getElementById("divResult").style.display="block";	
        var table = document.getElementById("resTable");        
        var row = table.insertRow(rIndex);
        
        var cell1 = row.insertCell(0);
        var element1 = document.createElement("td");
        var ele1Txt = document.createTextNode(obj.Id);
        element1.appendChild(ele1Txt);
        cell1.appendChild(element1);
        
        var cell2 = row.insertCell(1);
        var element2 = document.createElement("td");
        var ele2Txt = document.createTextNode(obj.Name);
        element2.appendChild(ele2Txt);
        cell2.appendChild(element2);
        
        var cell3 = row.insertCell(2);
        var element3 = document.createElement("td");
        var ele3Txt = document.createTextNode(obj.Age);
        element3.appendChild(ele3Txt);
        cell3.appendChild(element3);
      
        var cell4 = row.insertCell(3);
        var element4 = document.createElement("td");
        var ele4Txt = document.createTextNode(obj.Designation);
        element4.appendChild(ele4Txt);
        cell4.appendChild(element4);
        
    }
	
	function createEmpTableContent(obj) {
		
        
		$('#empTable').append('<tr><td><input type="text" name="EmpID" style="width:95%" value ='+obj.Id+' /></td>'+
        					  '<td><input type="text" name="EmpName" style="width:95%" value ='+obj.Name+' /></td>'+
        					  '<td><input type="text" name="EmpAge" style="width:95%" value ='+obj.Age+' /></td>'+
        					  '<td><input type="text" name="EmpDesg" style="width:95%" value ='+obj.Designation+' /></td>'+
        					  '<td style="text-align:center;"><input type="checkbox" name ="delChkBox" /></td></tr>');
		
    }
	
	function clearTable(flag) {
		var tableRows;
		if(flag == 1) {
			tableRows = document.getElementById("resTable").rows;
		}
		else {
			tableRows = document.getElementById("empTable").rows;
		}
		while( tableRows[1] )
			tableRows[1].parentNode.removeChild(tableRows[1]);
	}
	
	
	function addMoreRows() {
	     
        $('#empTable').append('<tr><td ><input type="text" name="EmpID" style="width:95%"/></td>'+
        					  '<td><input type="text" name="EmpName" style="width:95%"/></td>'+
        					  '<td><input type="text" name="EmpAge" style="width:95%"/></td>'+
        					  '<td><input type="text" name="EmpDesg" style="width:95%"/></td>'+
        					  '<td style="text-align:center;"><input type="checkbox" name ="delChkBox"/></td></tr>');
	}

	function deleteRow(tableID) {
        try {
	        var table = document.getElementById(tableID);
	        var rowCount = table.rows.length;
	        
	        for(var i=1; i<rowCount; i++) {
	            var row = table.rows[i];
	            var chkbox = row.cells[4].childNodes[0];
	            if(null != chkbox && true == chkbox.checked) {
	                table.deleteRow(i);
	                rowCount--;
	                i--;
	            }
	        }
        }catch(e) {
            alert(e);
        }
    }
	
	
	function firstView() {
		$("#divTableList").show();
		$("#divEmpDetails").hide();
		$("#divTableFunc").hide();
		$("#divResult").hide();
	}

</script>
<style>
	table.myTableClass {table-layout:fixed}
</style>
</head>

<body>
<center>
<h2> Employee Search Engine</h2>
	<form name="empForm"> 
	<table id ="myTable" width="20%" border ="2">
		<tr>
			<td width ="100%"><b>Employee ID:</b></td> 
			<td width ="100%"><input type="text" id ="kSearch" name="keySearch" onkeyup="validityCheck(this,event)"></td>   
	    </tr>
	</table>
	</form>
	<br>
	<div id = "divTableList">
		<input type="button" id="btnSearch" name="search" value = "Search" onclick = "return nullCheck()">
		<input type="button" id="viewDetails" value = "Employee List" onclick = "showList()">
	</div>
	<div id = "divResult" style="display: none" >
	<h3> Employee Search Details </h3>
		<form name="resForm">  
			<table id ="resTable" border ="2">
				<tr>
					<th>Emp ID</th>
					<th>Name</th>
					<th>Age</th>
					<th>Designation</th>
				</tr>
			</table>
		</form>
		<br>
		<button>RESET</button>
	</div>
<div id = "divEmpDetails" style="display: none" >
<h3>EMPLOYEE DETAILS</h3>
	<form id ="myForm" action ="StoreEmpData" method ="POST">
		<table id ="empTable" border = 1 class="myTableClass" width="38%">
			<tr>
				<th width="15%">EmpID</th>
				<th width="35%">Name</th>
				<th width="8%">Age</th>
				<th width="35%">Designation</th>
				<th width="15%">Controls</th>
			</tr>
		</table>
		<br>
		<div id = "divTableFunc" style="display: none">
			<input type="button" value = "Back" onclick = "firstView()">
			<input type="button" id="AddDetails" value = "Add Employee" onclick = "addMoreRows()">	
			<input type="button" id="DeleteDetails" value = "Delete" onclick = "deleteRow('empTable')">
			<input type="submit" id="SubmitDetails" value = "Save" >	
		</div>	
	</form>
</div>
<br>
</center>

</body>

</html>