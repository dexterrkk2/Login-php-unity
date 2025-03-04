<?php
require 'ConnectionSettings.php';


//variables submitted
//$loginUser = $_POST["loginUser"];
//$loginPass = $_POST["loginPass"];
// Create connection
$itemID = $_POST["itemID"];
// Check connection
$sql = "SELECT name,description,price from items where id ='" .$itemID ."'";
$result = $conn->query($sql);
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
//Show users
if ($result->num_rows > 0) {
    // output data of each row
    $rows = array();
    while($row = $result->fetch_assoc()) {
      $rows[] = $row;
    }
  } else {
    echo "0 items";
  }
  echo json_encode($rows);
  $conn->close();
?>