# Assignment 2 - NHL Players

## **Overview**

 

Create a GUI to browse and filter player stats from the NHL 2017-18 regular season.





## **Directions**

 

The code includes a CSV (comma-separated value) file containing the player stats for the NHL 2017-18 regular season. Write code to parse this file and store the results into a standard collection that can be queried using LINQ.

 

Build a GUI (can be console) that displays the player stats in a table format and includes controls for sorting and filtering the data.

 

You shoul be able to filter data. Users should be able to filter the data picking columns on the table (you can display options via console). A filter expression would look like “G >= 50”, which would only display players that scored 50 or more goals. The filter should work with any of the fields (Player, Team, Pos, GP, G, A, P, +/-, PIM, P/GP, PPG, PPP, SHG, SHP, GWG, OTG, S, S%, TOI/GP, Shifts/GP, FOW%). The filter should also work with any standard comparison operator (<, <=, ==, =>, >). The filter expression should also be able to include multiple filters separated by commas. For example, “GP < 10, P > 10” would display all players that played less than 10 games and scored more than 10 points.

 

The GUI should also include a a way to enter sorting information, looking all fields for information. 
Users should be able to sort by any column in ascending or descending order. 
