using System;
using NLog.Web;
using System.IO;

namespace movie_library
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Movie Library Application\n");
            
            
            string path = Directory.GetCurrentDirectory() + "\\nlog.config"; // Windows 10
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();
            logger.Info("Begin program.");

            
            string file = "movies.csv";
            
        
            if(!File.Exists(file)) {
                logger.Info("The file " + file + " does not exist.");
            }
            else {
                
                Console.WriteLine("Enter 1 to read movies from data file.");
                Console.WriteLine("Enter 2 to add a movie to the file.");
                Console.WriteLine("Enter anything else to quit.");
                string choice = Console.ReadLine();

                
                if (choice == "1") {
                    try {
                        StreamReader sr = new StreamReader(file);
                        
                        sr.ReadLine();

                        while (!sr.EndOfStream) {
                            string line = sr.ReadLine();
                            string[] infoArray = line.Split(',');

                    
                            string movieID = infoArray[0]; 

                            
                            string title = ""; 
                            for (int i = 1; i < (infoArray.Length - 1); i++) {
                                
                                if (i != (infoArray.Length - 2)) 
                                    title += infoArray[i] + ",";
                                else
                                    title += infoArray[i];
                            }
                            
                            
                            string genreList = infoArray[(infoArray.Length - 1)]; 
                            string[] genreArray = genreList.Split('|');
                            string seperator = ", ";
                            string genres = "";
                            genres += String.Join(seperator, genreArray);
                            
                            
                            Console.WriteLine($"Movie ID: {movieID}");
                            Console.WriteLine($"Title: {title}");
                            Console.WriteLine($"Genres: {genres}");
                            Console.WriteLine("");
                        }
                        sr.Close();
                    }
                    catch (Exception e) {
                        logger.Error(e.Message);
                    }
                }

                
                else if (choice == "2") {
                    try {
                        Console.WriteLine("Enter the Movie ID: ");
                        string movieID = Console.ReadLine();

                        
                        Console.WriteLine("Enter the title of the movie: ");
                        string title = Console.ReadLine();

                        
                        Console.WriteLine("Enter the genre of the movie: ");
                        string genres = Console.ReadLine();

                        
                        string addGenre;
                        do {
                            Console.WriteLine("\nWould you like to add another movie genre?");
                            Console.WriteLine("1) yes");
                            Console.WriteLine("2) no");
                            addGenre = Console.ReadLine();

                            if (addGenre == "1") 
                                Console.WriteLine("Enter the next genre of the movie: ");
                                string tempGenre = Console.ReadLine();
                                genres += "|" + tempGenre;
                            }

                        } while (addGenre == "1");

                        
                        Boolean movieExists = true;
                        StreamReader sr = new StreamReader(file);
                        sr.ReadLine();

                        while (!sr.EndOfStream) {
                            string line = sr.ReadLine();
                            string[] infoArray = line.Split(',');

                            string titleInLibrary = ""; 
                            for (int i = 1; i < (infoArray.Length - 1); i++) {
                            
                                if (i != (infoArray.Length - 2)) 
                                    titleInLibrary += infoArray[i] + ",";
                                else
                                    titleInLibrary += infoArray[i]; 
                            }

                           
                            if(title.Equals(titleInLibrary)) {
                                Console.WriteLine($"\n{title} is already in the library and cannot be added.\n");
                                movieExists = true;
                                break;
                            }
                            else {movieExists = false;}
                        }
                        sr.Close();

                        
                        if (movieExists == false) {
                            StreamWriter sw = new StreamWriter(file, append: true);
                            sw.WriteLine(movieID + "," + title + "," + genres);
                            sw.Close();
                        }
                        Console.WriteLine("");
                    }
                     catch (Exception e) {
                        logger.Error(e.Message);
                    }
                }
                
                else {
                    Console.WriteLine("\nFarewell.\n");
                }
                
            }
            logger.Info("End program.");
        }
    }
}
