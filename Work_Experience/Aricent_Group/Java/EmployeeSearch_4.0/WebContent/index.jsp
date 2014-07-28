<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
<title>Employee Search Engine</title>
<script src="js/jquery-1.9.0.min.js" type="text/javascript" ></script>
<script language="javascript" type="text/javascript">  

	$(document).ready(function(){
	  $("button").click(function(){
	    alert("Reseting the results .. Try Again  :)");
		$("#divResult").hide();
	  });
	});
	
	function validityCheck(object,event)
	{ 
		 reg = /^\d+$/;
		
         if(!reg.test(object.value)) 
         {
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

	function nullCheck()
	{
		var numObject = document.forms["empForm"]["keySearch"];
		if(numObject.value == null || numObject.value == '')
		{
			numObject.value = 0;		
			alert("Search for details of All employees");
			//numObject.focus();
			//return false;
		}
		clearTable();
		show();
		return true;
	}

	function show(){	

		$.ajax({
			type: 'Get',
			url: 'Result',
			data: {keySearch: $("input#kSearch").val()},
			dataType: 'json',
			success: function(result) {
				if(result.Success.Flag == 0) {
					alert("No records Found !!!. Please enter a valid Employee ID");
					document.getElementById("divResult").style.display="none";	
				 }else if(result.Success.Flag == 1){ 
					$("#divResult").show();
			     	createTableContent(result.Success,1);
				 }else {
					 var empList = result.All;				 
					 var rowCount = result.Success.Count;
					 $("#divResult").show();
					 for(var i=0;i<rowCount;i++) {
						 createTableContent(empList[i],1);
					 }
				}
			}
		});
		
	}	
	
	function createTableContent(obj,rIndex) {
		
		
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
	
	function clearTable() {
		var tableRows = document.getElementById("resTable").rows;
		while( tableRows[1] )
			tableRows[1].parentNode.removeChild(tableRows[1]);
	}
</script>
</head>

<body>
<center> 
	<h2> Employee Search Engine</h2>
	<form name="empForm"> 
	<table id ="myTable" width ="250px" border ="2">
		<tr>
			<td><b>Employee ID:</b></td> 
			<td><input type="text" id ="kSearch" name="keySearch" onkeyup="validityCheck(this,event)"></td>   
	    </tr>
	    <tr>
	    	<td colspan = 2><br></td>
	    </tr>
	    <tr>
	    	<th colspan = 2><input type="button" id="btnSearch" name="search" value = "Search" onclick = "return nullCheck()"></th>
	    </tr>
	</table>
	</form>
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
</center>	 
</body>
</html>