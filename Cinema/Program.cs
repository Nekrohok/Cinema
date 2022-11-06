using System.Collections;
using System.IO;

public enum Genre {Horror, Comedy, Fantasy}
class Director : ICloneable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Director(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
    public object Clone() => new Director(FirstName, LastName);
    public override string ToString() => $"{FirstName}. {LastName}";
}
class Movie : IComparable<Movie>, ICloneable
{
    public string Name { get; set; }
    public Director Director { get; set; }
    public string Country { get; set; }
    public Genre Genre { get; set; }
    public int Year { get; set; }
    public double Rating { get; set; }

    public Movie(string name, Director director, string country, Genre genre, int year, double rating)
    {
        this.Name = name;
        this.Director = director;
        this.Country = country;
        this.Genre = genre;
        this.Year = year;
        this.Rating = rating;
    }
    public object Clone() => new Movie(Name, new Director(Director.FirstName, Director.LastName), Country, Genre, Year, Rating);
    public int CompareTo(Movie? movie)
    {
        if (movie is Movie) return Year.CompareTo(movie.Year);
        throw new ArgumentException();
    }
    public override string ToString() => $"Film: {Name}\nDirector: {Director.ToString()}\nCountry: {Country} \nGenre: {Genre}\nYear: {Year}\n Rating: {Rating}";
}

    class Cinema : IEnumerable
    {
    public Movie[] Movies { get; set; } 
    public string Address { get; set; }

    public void Sort()
    {
         Array.Sort(Movies);
    }
    public void Sort(IComparer<Movie> comparer)
    {
         Array.Sort(Movies, comparer);
    }

    public IEnumerator GetEnumerator()
        {
            return Movies.GetEnumerator();
        }
    }
    class RatingComparer : IComparer<Movie>
    {
        public int Compare(Movie x, Movie y)
        {
            if (x is Movie && y is Movie)
                return x.Rating.CompareTo(y.Rating);
            throw new ArgumentException();
        }
    }
class YearComparer : IComparer<Movie>
{
    public int Compare(Movie x, Movie y)
    {
        if (x is Movie && y is Movie)
            return x.Year.CompareTo(y.Year);
        throw new ArgumentException();
    }
}
internal class Program
{
    static void Main()
    {
        Movie PHENOMENA = new(
            "PHENOMENA",
            new Director("Dario", "Argento"),
            "USA",
            Genre.Horror,
            2008,
            7.5
        );
        Movie AirPlane = new(
            "AirPlane!",
            new Director("Jim", "Abrahams"),
            "USA",
            Genre.Comedy,
            2005,
            6
        );
        Movie HIGHLANDER = new(
            "HIGHLANDER",
            new Director("Russel", "Mulcahy"),
            "USA",
            Genre.Fantasy,
            2009,
            6.5
        );
        Movie[] movies = { PHENOMENA, AirPlane, HIGHLANDER };
        Cinema cinema = new Cinema()
        {
            Movies = movies,
            Address = "California"
        };
        cinema.Sort();
        foreach (Movie movie in cinema.Movies)
        {
            Console.WriteLine(movie.ToString());
            Console.WriteLine("==============================");
        }
        Console.WriteLine("==============RatingComparer================");
        cinema.Sort(new RatingComparer());
        foreach (Movie movie in cinema.Movies)
        {
            Console.WriteLine(movie.ToString());
            Console.WriteLine("==============================");
        }
        Console.WriteLine("==============YearComparer================");
        cinema.Sort(new YearComparer());
        foreach (Movie movie in cinema.Movies)
        {
            Console.WriteLine(movie.ToString());
            Console.WriteLine("==============================");
        }


    }
}