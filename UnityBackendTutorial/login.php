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
$sql = "SELECT password, id from users where username ='" .$loginUser ."'";
$result = $conn->query($sql);
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
//Show users
if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
      if($row["password"] == $loginPass)
      {
        echo $row["id"];
        //get user data

        //get player info

        //get inventory

        //modify player data

        //update inventory
      }
      else{
        echo "wrong creditinals";
      }
    }
  } else {
    echo "user does not exit";
  }
  $conn->close();
?>