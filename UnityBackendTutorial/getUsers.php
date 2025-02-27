<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "unity backend tutorial";
// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
$sql = "SELECT username, level FROM users";
$result = $conn->query($sql);
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
echo "Connected successfully<br>";
//Show users
if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
      echo "username: " . $row["username"]. " - level: " . $row["level"].  "<br>";
    }
  } else {
    echo "0 results";
  }
  $conn->close();
?>