<html>
<body>
	<?php
	$db=mysqli_connect('localhost','root','','labs');
	$sql="select * from tablet";

	$result=$db->query($sql);

	echo "<table border=1>\n";
	echo "<tr><td>id</td><td>owner</td><td>quality</td><td>size</td><td>cost</td><td>form of own</td></tr>";
	while($actor=$result->fetch_assoc())
	{
		echo "<tr><td>" . $actor['id'] . "</td><td>" . $actor['owner'] . "</td><td>" . $actor['quality'] . "</td><td>" . $actor['size'] . "</td><td>" . $actor['cost'] . "</td><td>" . $actor['form of own'] . "</td></tr>";
	}
	echo "</table>\n";
echo "<p style=font-style:italic align=center>Main page-><a href=http://localhost/start.php>Click here</a></p><br>";
	?>
</body>
</html>