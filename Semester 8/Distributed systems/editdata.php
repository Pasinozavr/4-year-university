<html>
<body>
	<?php

	$db=mysqli_connect('localhost','root','','labs');
	$sql="select * from tablet";

	$result=$db->query($sql);	

if (isset($_GET['del_id'])) {
 $sqldel = "delete from tablet WHERE id = ".$_GET['del_id']. " ";
 $resultdel=$db->query($sqldel);
 $sqldelnew="delete from saler Where id = ".$_GET['del_id']. " ";
 $resultdelnew=$db->query($sqldelnew);

 $dbnew=mysqli_connect('localhost','root','','labs2');
 $resultnewnew=$dbnew->query($sqldel);
 Header("Location: editdata.php"); 
exit(); 
}
if (isset($_GET['red_id'])) {
	if (isset($_POST['owner']) || isset($_POST['quality']) || isset($_POST['size']) || isset($_POST['cost']) || isset($_POST['formofown']))
	{
$sqlup="update tablet set `owner`='".$_POST['owner']."', `quality`='".$_POST['quality']."', `size`='".$_POST['size']."', `cost`='".$_POST['cost']."', `form of own`='".$_POST['formofown']."' where id=".$_GET['red_id']."";
$resultup=$db->query($sqlup);
Header("Location: editdata.php"); 
exit(); 

	}

	}
	echo "<table bgcolor=#91AC20 border=1 width=500 style='display:block; float:left'>\n";
	echo "<tr><td>id</td><td>owner</td><td>quality</td><td>size</td><td>cost</td><td>form of own</td></tr>";
	while($actor=$result->fetch_assoc())
	{
		echo "<tr><td>" . $actor['id'] . "</td><td>" . $actor['owner'] . "</td><td>" . $actor['quality'] . "</td><td>" . $actor['size'] . "</td><td>" . $actor['cost'] . "</td><td>" . $actor['form of own'] . "</td>";
		echo '<td><a href="?del_id=' . $actor['id'] . '">Удалить</a></td>	';
		echo '<td><a href="?red_id=' . $actor['id'] . 	'">Редактировать</a></td></tr>';
		 
	}
	echo "</table>\n";

	if (isset($_GET['red_id'])) { //Если передана переменная на редактирование

        $sqlselect = "SELECT `id`, `owner`, `quality`, `size`, `cost`, `form of own` FROM tablet WHERE `id`=".$_GET['red_id']." "; //запрос к БД
        $resultselect=$db->query($sqlselect);
        $actorselect=$resultselect->fetch_assoc();

       ?>
<table>
<form action="" method="post">
    <tr>
        <td>Имя владельца:</td>
        <td><input type="text" name="owner" value="<?php echo ($actorselect['owner']); ?>"></td>
    </tr>
    <tr>
        <td>Качество:</td>
        <td><input type="text" name="quality" value="<?php echo ($actorselect['quality']); ?>">/8</td>
    </tr>
    <tr>
        <td>Размер:</td>
        <td><input type="text" name="size" value="<?php echo ($actorselect['size']); ?>">м.кв.</td>
    </tr>
    <tr>
        <td>Цена:</td>
        <td><input type="text" name="cost" value="<?php echo ($actorselect['cost']); ?>">грн.</td>
    </tr>
    <tr>
        <td>Форма владения:</td>
        <td><input type="text" name="formofown" value="<?php echo ($actorselect['form of own']); ?>"></td>
    </tr>
        <td colspan="2"><input type="submit" value="Обновить"></td>
    </tr>
</form>
</table>
<?php
}
echo "<p style=font-style:italic align=center>Main page-><a href=http://localhost/start.php>Click here</a></p><br>";
?>
</body>
</html>