<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "unity backend tutorial";

//variables submitted
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];
// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
$sql = "SELECT username from users where username ='" .$loginUser ."'";
$result = $conn->query($sql);
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
//Show users
if ($result->num_rows > 0) {
    //tell user that name is already taken
    echo "user already taken";
    
  } else {
    echo "creating user";
    $sql2 = "INSERT INTO users (username, password, level, coins) VALUES ('" .$loginUser . "', '" . $loginPass ."', 1, 0)";
    if ($conn->query($sql2) === TRUE) {
        echo "New record created successfully";
      } else {
        echo "Error: " . $sql2 . "<br>" . $conn->error;
      }
  }
 
  $conn->close();
?>