////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///
//	Solution/Project:  Lab 2 
//	File Name:         VideoGame.cs
//	Description:       class to create a vidoegame object for use in driver.  
//	Course:            CSCI-2910
//	Author:            Christian Rock, rockcm@etsu.edu, East Tennessee State University
//	Created:           09/12/23
//	Copyright:         Christian Rock, 2023/
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




using Lab1;
using System.Xml.Linq;
///variables 
List<VideoGame> videoGames = new List<VideoGame>();
StreamReader streamReader = null;
Dictionary<string, List<VideoGame>> vgDictionary = new Dictionary<string, List<VideoGame>>();
string key = null;





// try catch for streamreader 
try
{


    // creating a stream reader to read in the file given. 
    streamReader = new StreamReader(@"../../../gameList/videogames.csv");

    streamReader.ReadLine(); // reading first line of document
                             // while stream reader is not at the end of doc, continue loop. 
    while (streamReader.Peek() != -1)
    {
        // splitting each line read in to fiels that can be accessed and used to create videogame item. 
        string textLine = streamReader.ReadLine();
        string[] fields = textLine.Split(",");
        VideoGame Videogame = new VideoGame(fields[0], fields[1], fields[2], fields[3], fields[4], Convert.ToDecimal(fields[5]), Convert.ToDecimal(fields[6]), Convert.ToDecimal(fields[7]), Convert.ToDecimal(fields[8]), Convert.ToDecimal(fields[9]));
        videoGames.Add(Videogame);
        

        

        

    }



}
catch (FormatException e)
{
    Console.WriteLine($"{e.Message}");
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
finally // makes sure reader closes regardless
{
    if (streamReader != null)
        streamReader.Close();
}

//creating a dictionary from list.. grouped by platform 
Dictionary<string, List<VideoGame>> myDictionary = videoGames
.GroupBy(o => o.platform).ToDictionary(g => g.Key, g => g.ToList());

//going through each key and taking the top 5 of each platforms global sales 
var top5ItemsByKey = myDictionary.ToDictionary(
           kvp => kvp.Key,
           kvp => kvp.Value.OrderByDescending(item => item.global_Sales).Take(5).ToList()
       );


//displaying each platforms top 5 selling games 
foreach (var kvp in top5ItemsByKey)
{
    Console.WriteLine($"Key: {kvp.Key}");
    foreach (var item in kvp.Value)
    {
        Console.WriteLine($"Name: {item.name}, Sales: {item.global_Sales}");
    }
    Console.WriteLine();
}



/*
// prints all global sales for all platforms 
foreach (var v in myDictionary)
{
    Console.WriteLine($"{v.Key}"); // list attatched to each key. 
    for (int i =0; i < myDictionary[v.Key].Count; i++)
    {
       Console.WriteLine(myDictionary[v.Key][i].global_Sales);
    }

}
*/









