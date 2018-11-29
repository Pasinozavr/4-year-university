<%@page import="java.sql.ResultSet"%>
<%@page import="java.sql.Statement"%>
<%@page import="java.sql.Connection"%>
<%@page import="com.example.util.DBUtil"%>

<%
	String userName = request.getParameter("username");
	String password = request.getParameter("password");
	Connection con = DBUtil.getMySqlConnection();
	Statement st = con.createStatement();
	ResultSet rs;
	rs = st.executeQuery("select * from EMPLOYEE where USER_NAME='"
			+ userName + "' and PASSWORD='" + password + "'");
	if (rs.next()) {
		session.setAttribute("username", userName);
		response.sendRedirect("success.jsp");
	} else {
		out.println("Invalid password <a href='index.jsp'>try again</a>");
	}
%>