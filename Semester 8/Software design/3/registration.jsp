<%@page import="java.sql.Statement"%>
<%@page import="java.sql.Connection"%>
<%@page import="com.example.util.DBUtil"%>
<%
	String userName = request.getParameter("username");
	String password = request.getParameter("password");
	String firstName = request.getParameter("firstname");
	String lastName = request.getParameter("lastname");
	String email = request.getParameter("email");
	Connection con = DBUtil.getMySqlConnection();
	Statement st = con.createStatement();
	//ResultSet rs;
	int i = st
			.executeUpdate("insert into EMPLOYEE(FIRST_NAME, LAST_NAME, EMAIL, USER_NAME, PASSWORD) values ('"
					+ firstName
					+ "','"
					+ lastName
					+ "','"
					+ email
					+ "','" 
					+ userName
					+ "','" 
					+ password 
					+ "')");
	if (i > 0) {
		response.sendRedirect("welcome.jsp");
	} else {
		response.sendRedirect("index.jsp");
	}
%>