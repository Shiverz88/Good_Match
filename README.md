# Good_Match
Good Match program for assessment

The Assessment was completed using Visual Studio 2022 as a C# Console App

The requirements:
Write a program that accepts two strings and calculates the match percentage as above.
The program should only accept alphabetic characters and should gracefully handle invalid input
Casing should not matter
If the result is greater than or equal to 80, 'good match' should be appended to the output string.

Modify the program to accept a CSV file as input. 
Eg 
The results should be listed from the highest percentage to the lowest. Ie. order by percentage descending
If multiple results are the same, order them alphabetically.
The CSV file will contain a string followed by a character indicating gender. The gender character will be either m or f 
Kimberly, f
Jason, m
Billy, m
Trini, f
Tommy, m
Zack, m
Billy, f
Jason, m
Read the data, group them by the gender indicator, you will now have two sets of data, one with males, one with females. 
A single set should not contain duplicates, From the example data above, Jason should only be in the male set once, however Billy will be in the male set as well as the female set.
Run the good match program for every entry in the first set against every entry in the second set. Store the results
Print the results in a textfile called output.txt
