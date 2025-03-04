<?php

require 'ConnectionSettings.php';

$userID = $_POST["userID"];
// Check connection
$sql = "SELECT ID, itemID FROM useritems where userID = '" .$userID ."'";
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