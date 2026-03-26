using System;
using ADOMovieDatabaseConsoleAppV3.Models;
using Microsoft.EntityFrameworkCore;

namespace ADOMovieDatabaseConsoleAppV3
{
    internal class ADOMovieDatabaseProgramV3
    {
        static void Main(string[] args)
        {
            int? menuChoice;
            do
            {
                DisplayMenu();
                menuChoice = PromptForMenuChoice();

                switch (menuChoice)
                {
                    case 1:
                        AddMovie();
                        break;
                    case 2:
                        ListAllMovies();
                        break;
                    case 3:
                        AddGenre();
                        break;
                    case 4:
                        ListAllGenres();
                        break;
                    default:
                        break;
                }

            } while (menuChoice != 0);
        }

        public static void DisplayMenu()
        {
            Console.WriteLine("Movie Database Main Menu");
            Console.WriteLine("========================" + Environment.NewLine);
            Console.WriteLine("1. Add Movie");
            Console.WriteLine("2. List All Movies");
            Console.WriteLine("3. Add Genre");
            Console.WriteLine("4. List All Genres");
            Console.WriteLine($"0. Exit{Environment.NewLine}");
        }

        public static int? PromptForMenuChoice()
        {
            Console.Write("Input menu choice number: ");
            var inputOk = int.TryParse(Console.ReadLine(), out int choice);
            Console.WriteLine();
            return inputOk ? choice : null;
        }

        private static void ListAllMovies()
        {
            using (var context = new ApplicationDbContext())
            {
                foreach (var movie in context.Movies.Include(m => m.Genre))
                {
                    Console.WriteLine($"Id: {movie.Id}");
                    Console.WriteLine($"Movie Name: {movie.Title}");
                    Console.WriteLine($"Release Year: {movie.ReleaseYear}");
                    Console.WriteLine($"Genre: {movie.Genre?.Name}{Environment.NewLine}");
                }
                Console.WriteLine("Press a key!");
                Console.ReadKey();
            }
        }

        private static void AddMovie()
        {
            var movie = new Movie();

            Console.WriteLine("Title? ");
            movie.Title = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Year Released? ");
            movie.ReleaseYear = int.Parse(Console.ReadLine() ?? "0");

            ListAllGenres(false);

            Console.WriteLine("Genre Id? ");
            movie.GenreId = int.Parse(Console.ReadLine() ?? "0");

            using (var context = new ApplicationDbContext())
            {
                context.Movies.Add(movie);
                context.SaveChanges();
            }

            Console.WriteLine();
        }

        private static void ListAllGenres(bool waitForKeyPressAtEnd = true)
        {
            using (var context = new ApplicationDbContext())
            {
                foreach (var genre in context.Genres)
                {
                    Console.WriteLine($"{genre.Id}: {genre.Name}");
                }

                if (waitForKeyPressAtEnd)
                {
                    Console.WriteLine($"{Environment.NewLine}Press a key!");
                    Console.ReadKey();
                }
            }
        }

        private static void AddGenre()
        {
            var genre = new Genre();

            Console.Write("Name? ");
            genre.Name = Console.ReadLine() ?? string.Empty;

            using (var context = new ApplicationDbContext())
            {
                context.Genres.Add(genre);
                context.SaveChanges();
            }

            Console.WriteLine();
        }
    }
}