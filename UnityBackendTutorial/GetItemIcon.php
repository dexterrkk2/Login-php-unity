<?php
require 'ConnectionSettings.php';

if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
  }
//variables submitted
//$loginUser = $_POST["loginUser"];
//$loginPass = $_POST["loginPass"];
$itemID = $_POST["itemID"];
$path = "http://localhost/UnityBackendTutorial/itemsicons/" . $itemID. ".png";

$image = file_get_contents($path);
echo $image;

$conn->close();
?>