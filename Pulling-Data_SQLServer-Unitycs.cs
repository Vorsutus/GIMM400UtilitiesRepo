﻿C# Code (messageData is just a global string variable that holds the url of the PHP file):

 //Import data from SQL server through PHP script
        UnityWebRequest messUpdate = UnityWebRequest.Get(messageData);
        yield return messUpdate.SendWebRequest();

        if (messUpdate.error != null)
        {
            Debug.Log(messUpdate.error);
        }
        else
        {
            //Split incoming string into message categories
            incomingText.text = messUpdate.downloadHandler.text;
            string allData = incomingText.text;
            string[] splitStr = allData.Split(" "[0]);
            int[] value = new int[splitStr.Length - 1];
            string[] splitArr = allData.Split(char.Parse(";"));
            a.text = splitArr[0];
            b.text = splitArr[1];
        }

PHP script:

php
    // Configuration
    $hostname = 'localhost';
    $username = '******'; //username of SQL server
    $password = '******'; //password of SQL server
    $database = 'database1'; //name of the database containing the table
    try {
        $dbh = new PDO('mysql:host='. $hostname.';dbname='. $database, $username, $password);
    } catch(PDOException $e) {
        echo 'an error has occured';

        <pre>', $e->getMessage() ,' pre>';
    }
    $sth = $dbh->query('SELECT * FROM table1'); //select which table in the database you want to pull from, this is a SQL script
    $sth->setFetchMode(PDO::FETCH_ASSOC);
    $result = $sth->fetchAll();
    if(count($result) > 0) {
        foreach($result as $r) {
            echo $r['a'], ";";
            echo $r['b'], ";";

     }

   }

?>

//The PHP script connects to the SQL server and acts as the middleman between the server and Unity.Unity can't connect to the SQL server directly due to security concerns. It takes the data on the table and echos it (prints it). The C# script takes that echoed data and assigns it into an array which each array key can then be used for something. This isn't the most secure way of sending data since you can see the data on the PHP page but it works for things like scores and data that doesn't necessarily need to be secret.