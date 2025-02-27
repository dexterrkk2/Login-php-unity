<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "unity backend tutorial";

$userID = $_POST["userID"]; 

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
$sql = "SELECT itemID FROM usersitems where userID = '" .$userID ."'";
$result = $conn->query($sql);
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
echo "Connected successfully<br>";
//Show users
if ($result->num_rows > 0) {
    $rows = array();
    // output data of each row
    while($row = $result->fetch_assoc()) {
      $rows[] = $row;
    }
    echo json_encode($rows);
  } else {
    echo "0 results";
  }
  $conn->close();
?>