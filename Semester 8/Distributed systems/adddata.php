<html>
<body>
<?php
if(isset($_POST['owner']) && isset($_POST['quality']) && isset($_POST['size']) && isset($_POST['cost']) && isset($_POST['formofown']) && isset($_POST['country']) && isset($_POST['water']) && isset($_POST['part'])){
		$db=mysqli_connect('localhost','root','','labs') or die("Ошибка " . mysqli_error($db));
		$dbnew=mysqli_connect('localhost','root','','labs2') or die("Ошибка " . mysqli_error($dbnew));

	//$owner = htmlentities(mysqli_real_escape_string($db, $_POST['owner']));
    //$quality = htmlentities(mysqli_real_escape_string($db, $_POST['quality']));
    //$size = htmlentities(mysqli_real_escape_string($db, $_POST['size']));
    //$cost = htmlentities(mysqli_real_escape_string($db, $_POST['cost']));
    //$formofown = htmlentities(mysqli_real_escape_string($db, $_POST['form of own']));
    //$country = htmlentities(mysqli_real_escape_string($db, $_POST['country']));
    //$water = htmlentities(mysqli_real_escape_string($db, $_POST['water']));
    //$part = htmlentities(mysqli_real_escape_string($dbnew, $_POST['part']));

$owner = $_POST['owner'];
$quality = $_POST['quality'];
$size = $_POST['size'];
$cost = $_POST['cost'];
$formofown = $_POST['formofown'];
$country= $_POST['country'];
$water = $_POST['water'];
$part = $_POST['part'];
$id=rand(10000,100000);
	 $query1 ="INSERT INTO saler VALUES ('".$id."','".$part."')";
	 $query2 ="INSERT INTO tablet VALUES ('".$id."','".$owner."','".$quality."','".$size."','".$cost."','".$formofown."')";
	 $query3 ="INSERT INTO tablet VALUES ('".$id."','".$country."','".$water."')";

	 $result1 = $db->query($query1) or die("Ошибка " . mysqli_error($db)); 


    $result2 = mysqli_query($db, $query2) or die("Ошибка " . mysqli_error($db)); 

    $result3 = mysqli_query($dbnew, $query3) or die("Ошибка " . mysqli_error($dbnew)); 
if ($result3==true)
{
echo "<br>Информация в базу добавлена успешно.";
}
else echo "<br>Информация в базу не добавлена.";

    mysqli_close($db);
    mysqli_close($dbnew);
}
echo "<p style=font-style:italic align=center>Main page-><a href=http://localhost/start.php>Click here</a></p><br>";
	?>
			<table>
<form action="" method="post">
	<tr>
        <td>ID:</td>
        <td><?php


        ?></td>
    </tr>
    <tr>
        <td>Имя владельца:</td>
        <td><input type="text" name="owner"></td>
    </tr>
    <tr>
        <td>Качество:</td>
        <td><input type="text" name="quality" size="3"> /8</td>
    </tr>
    <tr>
        <td>Размер:</td>
        <td><input type="text" name="size" size="3"> м.кв.</td>
    </tr>
    <tr>
        <td>Цена:</td>
        <td><input type="text" name="cost" size="3"> грн.</td>
    </tr>
    <tr>
        <td>Форма владения:</td>
        <td><input type="text" name="formofown" size="3"></td>
    </tr>
    <tr>
        <td>Страна:</td>
        <td><input type="text" name="country" size="3"></td>
    </tr>
    <tr>
        <td>Вода (да/нет):</td>
        <td><input type="text" name="water" size="3"> (1/0)</td>
    </tr>
    <tr>
        <td>Размер, идущий на продажу:</td>
        <td><input type="text" name="part" size="3"> %</td>
    </tr>
    <tr>
        <td colspan="2"><input type="submit" value="OK"></td>
    </tr>
</form>
</table>
</body>
</html>