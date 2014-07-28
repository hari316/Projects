import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import oracle.jdbc.driver.OracleSQLException;
import oracle.spatial.geometry.JGeometry;
import oracle.sql.STRUCT;


public class hw2 {

	public static void main(String[] args) {

		int index = 0;
		
		String queryType = args[index].toString();
		String objectType = null;
		
		if(queryType.contentEquals("window")) {
			
			objectType = args[++index].toString();
			int lowerLeftX = Integer.parseInt(args[++index].toString());
			int lowerLeftY = Integer.parseInt(args[++index].toString());
			int upperRightX = Integer.parseInt(args[++index].toString());
			int upperRightY = Integer.parseInt(args[++index].toString());
			
			getIdInsideWindow(objectType,lowerLeftX,lowerLeftY,upperRightX,upperRightY);
			
		}else if(queryType.contentEquals("within")) {
			
			objectType = args[++index].toString();
			String location = args[++index].toString();
			int dist = Integer.parseInt(args[++index].toString());
			getIdWithinDistance(objectType, location,dist);
		
		}else if(queryType.contentEquals("nn")) {
			
			objectType = args[++index].toString();
			String location = args[++index].toString();
			int dist = Integer.parseInt(args[++index].toString());
			getNearestNeighbor(objectType, location, dist);
			
		}else if(queryType.contentEquals("demo")) {
			demo(Integer.parseInt(args[++index].toString()));
		}else {
			
			System.out.println("ERROR: Invalid Arguments passed.");
			System.exit(0);
		
		}
	
		
				
	}
	
	public static void getIdInsideWindow(String objectType, int lX, int lY, int uX, int uY) {
		
		/*SELECT t.BUILDING_ID
		FROM USC_BUILDING b ,USC_BUILDING_ON_FIRE t
		WHERE SDO_INSIDE(b.coordinates, SDO_GEOMETRY(2003, NULL, NULL,
		SDO_ELEM_INFO_ARRAY(1,1003,3) ,SDO_ORDINATE_ARRAY(10,20,300,500)) ) = 'TRUE'
		and b.building_id = t.building_id
		*/
		boolean flag = false;
		
		Connection myConn;
		try {
			myConn = ConnectOracleDB.getConnection();
			Statement stmt = myConn.createStatement();
			
			String columnName = "BUILDING_ID";
			String query = null;
			
			if(objectType.toLowerCase().equals("firebuilding")) {
				//tableName = "USC_BUILDING_ON_FIRE";
				query = "SELECT t."+columnName+
						  " FROM USC_BUILDING b ,USC_BUILDING_ON_FIRE t "+
						  " WHERE SDO_INSIDE(b.coordinates, " +
						            "SDO_GEOMETRY(2003, NULL, NULL, "+
						             "SDO_ELEM_INFO_ARRAY(1,1003,3) ,"+
						             "SDO_ORDINATE_ARRAY("+lX+","+lY+","+uX+","+uY+")) "+
						             ") = 'TRUE' and b.building_id = t.building_id";
			
			}else if(objectType.toLowerCase().equals("firehydrant")) {
				//tableName = "USC_FIRE_HYDRANT";
				query = "SELECT b."+columnName+
						  " FROM USC_FIRE_HYDRANT b" +
						  " WHERE SDO_INSIDE(b.coordinates, " +
						            "SDO_GEOMETRY(2003, NULL, NULL, "+
						             "SDO_ELEM_INFO_ARRAY(1,1003,3) ,"+
						             "SDO_ORDINATE_ARRAY("+lX+","+lY+","+uX+","+uY+")) "+
						             ") = 'TRUE' ";
				
			}else {
				//tableName = "USC_BUILDING";
				query = "SELECT b."+columnName+
						  " FROM USC_BUILDING b" +
						  " WHERE SDO_INSIDE(b.coordinates, " +
						            "SDO_GEOMETRY(2003, NULL, NULL, "+
						             "SDO_ELEM_INFO_ARRAY(1,1003,3) ,"+
						             "SDO_ORDINATE_ARRAY("+lX+","+lY+","+uX+","+uY+")) "+
						             ") = 'TRUE' and b.building_id not in(select building_id from usc_building_on_fire)";
			
			}
			
			
			ResultSet rs= stmt.executeQuery(query);
			System.out.println("------------------------------------------------------------------");
			System.out.println("QUERY 1: Type - Window");
			System.out.println("------------------------------------------------------------------");
			System.out.println();
			System.out.println(columnName);
			System.out.println("------------------------------------------------------------------");
			
			while (rs.next()) {
				flag = true;
				String buildingId = rs.getString(columnName);
				System.out.println(buildingId+ " ");
			}
			if(!flag) {
				System.out.println("No results found !!");
			}

		} catch (OracleSQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		
	}

	public static void getIdWithinDistance(String objectType, String loc, int dist) {
		
		/*
		 select f.building_id from usc_fire_hydrant f, usc_building b  where b.building_name = 'OHE'
		 and sdo_within_distance (
				        f.coordinates, b.coordinates,
				        'distance=50') = 'TRUE';        
		*/
		
		boolean flag = false;
		
		Connection myConn;
		try {
			myConn = ConnectOracleDB.getConnection();
			Statement stmt = myConn.createStatement();
			
			String columnName = "BUILDING_ID";
			String query = null;
			
			if(objectType.toLowerCase().equals("firebuilding")) {
				//tableName = "USC_BUILDING_ON_FIRE";
				query = "select t.building_id"+ 
						" from USC_BUILDING_ON_FIRE t, usc_building b1, usc_building b2"+  
						" where b1.building_name = '"+loc+"' and t.building_id = b2.building_id"+
						" and t.building_name != '"+loc+"' and sdo_within_distance ( b2.coordinates, b1.coordinates,'distance="+dist+"') = 'TRUE'";
				
			}else if(objectType.toLowerCase().equals("firehydrant")) {
				//tableName = "USC_FIRE_HYDRANT";
				query = "select t.building_id from USC_FIRE_HYDRANT t, usc_building b  where b.building_name = '"+loc+"'"+
						" and sdo_within_distance ("+
				        " t.coordinates, b.coordinates,"+
				        "'distance="+dist+"') = 'TRUE'";        
				
			}else {
				//tableName = "USC_BUILDING";
				query = "select t.building_id from USC_BUILDING t, usc_building b  where b.building_name = '"+loc+"'"+
						" and t.building_name != '"+loc+"' and sdo_within_distance ("+
				        "t.coordinates, b.coordinates,"+
				        "'distance="+dist+"') = 'TRUE' and t.building_id not in(select building_id from usc_building_on_fire)";        
				
			}
			
				
			ResultSet rs= stmt.executeQuery(query);
			System.out.println("------------------------------------------------------------------");
			System.out.println("QUERY 2: Type - Within");
			System.out.println("------------------------------------------------------------------");
			System.out.println();
			System.out.println(columnName);
			System.out.println("------------------------------------------------------------------");
			
			
			while (rs.next()) {
				flag = true;
				String buildingId = rs.getString(columnName);
				System.out.println(buildingId+ " ");
			}
			if(!flag) {
				System.out.println("No results found !!");
			}

		} catch (OracleSQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}	

	}
	
	public static void getNearestNeighbor(String objectType, String loc, int dist) {
	
		/*		select t.building_id from usc_building t, usc_building b 
			where b.building_id = 'b3' and t.building_id != 'b3' and
			SDO_NN(t.coordinates, b.coordinates, 'sdo_num_res=5') = 'TRUE'; 
		 */
	
		boolean flag = false;
		
		Connection myConn;
		try {
			myConn = ConnectOracleDB.getConnection();
			Statement stmt = myConn.createStatement();
			
			String tableName = null;
			String columnName = "BUILDING_ID";
			String query = null;
			
			if(objectType.toLowerCase().equals("firebuilding")) {
				tableName = "USC_BUILDING_ON_FIRE";
				query = "SELECT t.BUILDING_ID"+
						" from USC_BUILDING_ON_FIRE t, usc_building b1, usc_building b2"+ 
						" where b1.building_id = '"+loc+"' and  t.building_id = b2.building_id"+ 
						" and sdo_NN ( b2.coordinates, b1.coordinates,'sdo_num_res="+(dist+1)+"') = 'TRUE'";        
			}else if(objectType.toLowerCase().equals("firehydrant")) {
				tableName = "USC_FIRE_HYDRANT";
				query = "SELECT t."+columnName+
						" from "+ tableName+" t, usc_building b"+
						" where b.building_id = '"+loc+"'"+
						" and t.building_id != '"+loc+"' and sdo_NN ("+
				        " t.coordinates, b.coordinates, 'sdo_num_res="+dist+"') = 'TRUE'";
			}else {
				tableName = "USC_BUILDING";
				query = "SELECT t."+columnName+
						" from "+ tableName+" t, usc_building b"+
						" where b.building_id = '"+loc+"'"+
						" and t.building_id != '"+loc+"' and sdo_NN ("+
				        " t.coordinates, b.coordinates, 'sdo_num_res="+(dist+1)+"') = 'TRUE'"+
						" and t.building_id not in(select building_id from usc_building_on_fire)";        
				
			}
			        
				
			ResultSet rs= stmt.executeQuery(query);
			
			System.out.println("------------------------------------------------------------------");
			System.out.println("QUERY 3: Type - Nearest Neighbor");
			System.out.println("------------------------------------------------------------------");
			System.out.println();
			System.out.println(columnName);
			System.out.println("------------------------------------------------------------------");
			
			while (rs.next()) {
				flag = true;
				String buildingId = rs.getString(columnName);
				System.out.println(buildingId+ " ");
			}
			
			if(!flag) {
				System.out.println("No results found !!");
			}


		} catch (OracleSQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		
	}
	
	public static void demo(int mode) {
		
		boolean flag = false;
		Connection myConn;
		try {
			myConn = ConnectOracleDB.getConnection();
			Statement stmt = myConn.createStatement();
			
			String query = null;
			ResultSet rs = null;
			
			switch(mode) {
				
				case 1:
					query = "SELECT b.building_name"+
							" FROM usc_building b"+
							" WHERE b.building_name LIKE 'S%'"+
							" AND NOT EXISTS(SELECT f.building_id FROM usc_building_on_fire f WHERE b.building_id = f.building_id)";        
				
					rs= stmt.executeQuery(query);
					System.out.println("------------------------------------------------------------------");
					System.out.println("Demo 1:");
					System.out.println("------------------------------------------------------------------");
					System.out.println();
					System.out.println("BUILDING_NAME");
					System.out.println("------------------------------------------------------------------");
					
					while (rs.next()) {
						flag = true;
						String buildingId = rs.getString("building_name");
						System.out.println(buildingId+ " ");
					}
					
					if(!flag) {
						System.out.println("No results found !!");
					}

					break;
					
				case 2:
					query = "SELECT f.building_name, h.building_id"+
							" FROM usc_building_on_fire f, usc_fire_hydrant h, usc_building b"+
							" WHERE b.building_id = f.building_id and h.building_id IN (SELECT uh.building_id"+
							      " FROM usc_fire_hydrant uh, usc_building ub"+
							      " WHERE sdo_NN (uh.coordinates, b.coordinates, 'sdo_num_res=5',1) = 'TRUE')"; 	
					rs= stmt.executeQuery(query);
					System.out.println("------------------------------------------------------------------");
					System.out.println("Demo 2:");
					System.out.println("------------------------------------------------------------------");
					System.out.println();
					System.out.println("BUILDING_NAME  BUILDING_ID");
					System.out.println("------------------------------------------------------------------");
					
					
					while (rs.next()) {
						flag = true;
						String buildingName = rs.getString("building_name");
						String buildingId = rs.getString("building_id");
						System.out.println(buildingName+ "  "+buildingId );
					}
					
					if(!flag) {
						System.out.println("No results found !!");
					}

					break;
					
				case 3:
					query = "SELECT h.building_id as bid,count(b.building_id) As MaxCount"+
							" FROM usc_fire_hydrant h, usc_building b"+
							" WHERE sdo_within_distance(b.coordinates,h.coordinates,'distance=120') = 'TRUE'"+
							" GROUP BY h.building_id"+
							"  HAVING count(b.building_id) = (SELECT MAX(MaxCount)"+
									" FROM (SELECT h.building_id as bid,count(b.building_id) As MaxCount"+
									" FROM usc_fire_hydrant h, usc_building b"+
									" WHERE sdo_within_distance(b.coordinates,h.coordinates,'distance=120') = 'TRUE'"+
									" GROUP BY h.building_id"+
									" ORDER BY MaxCount DESC ))"
							; 	
					rs= stmt.executeQuery(query);
					
					System.out.println("------------------------------------------------------------------");
					System.out.println("Demo 3:");
					System.out.println("------------------------------------------------------------------");
					System.out.println();
					System.out.println("BUILDING_ID");
					System.out.println("------------------------------------------------------------------");
					
					while (rs.next()) {
						flag = true;
						String buildingId = rs.getString("bid");
						System.out.println(buildingId+ "  ");
					}
					
					if(!flag) {
						System.out.println("No results found !!");
					}

					break;	
					
				case 4:
					query = "SELECT * FROM"+
							" (SELECT t.building_id, count(b.building_id) as MaxCount"+
								    " FROM usc_fire_hydrant t, usc_building b"+ 
								    " WHERE SDO_NN(t.coordinates, b.coordinates, 'sdo_num_res=1') = 'TRUE'"+ 
								    " GROUP BY t.building_id"+
								    " ORDER BY MaxCount DESC) WHERE ROWNUM <= 5";
					
					rs= stmt.executeQuery(query);
					
					System.out.println("------------------------------------------------------------------");
					System.out.println("Demo 4:");
					System.out.println("------------------------------------------------------------------");
					System.out.println();
					System.out.println("BUILDING_ID");
					System.out.println("------------------------------------------------------------------");
					
					while (rs.next()) {
						flag = true;
						String buildingId = rs.getString("building_id");
						System.out.println(buildingId+ "  ");
					}
					
					if(!flag) {
						System.out.println("No results found !!");
					}

					break;
					
				case 5:
					query = "SELECT SDO_AGGR_MBR(b.coordinates)"+
							" FROM USC_BUILDING b"+
							" WHERE b.building_name like '%HE'"
							; 	
					rs= stmt.executeQuery(query);
					
					System.out.println("------------------------------------------------------------------");
					System.out.println("Demo 5:");
					System.out.println("------------------------------------------------------------------");
					System.out.println();
					
					while(rs.next()){

						flag = true;
						STRUCT st = (STRUCT) rs.getObject(1);
						    //convert STRUCT into geometry
						JGeometry j_geom = JGeometry.load(st);
						System.out.println("Lower Left X = "+j_geom.getMBR()[0]);
						System.out.println("Lower Left Y = "+j_geom.getMBR()[1]);
						System.out.println("Upper Right X = "+j_geom.getMBR()[2]);
						System.out.println("Upper Right Y = "+j_geom.getMBR()[3]);

					}
					
					if(!flag) {
						System.out.println("No results found !!");
					}

					break;
					
				default: System.out.println("Error: Invalid Demo Number !! please enter within range (1-5)");
						 System.exit(0);
				
				
			}
			

		} catch (OracleSQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		
		
	}

}
