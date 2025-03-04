<?php

require 'ConnectionSettings.php';
//variables submitted by user
$itemID = $_POST["itemID"];
$ID = $_POST["ID"];
$userID = $_POST["userID"];
//check connection
if ($conn->connect_error) 
{
    die("Connection failed: " . $conn->connect_error);
}

//first sql
$sql = "SELECT price from items where ID = '" . $itemID . "'";

$result = $conn-> query($sql);

if($result ->num_rows >0)
{

    //Store item price
    $itemPrice = $result->fetch_assoc()["price"];

    $sql2 = "DELETE FROM useritems where ID = '" .$ID ."'"; 

    $result2 = $conn->query($sql2);
    if($result2)
    {
        $sql3 = "UPDATE `users` SET `coins` = coins + '" . $itemPrice . "' WHERE `id` = '". $userID. "'";
        $conn->query($sql3);
    }
    else
    {
        echo "Error could not delete item";
    }
}
else
{
    echo "0";
}
$conn->close();
?>