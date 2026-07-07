using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADSPortEx2
{
    //VideoGame class implementation for Assessed Exercise 2

    class VideoGame : IComparable
    {
        // Private fields to store the video game data
        private string title;
        private string developer;
        private int releaseyear;

        // Default constructor that creates a video game with empty/default values
        // Used when no initial data is provided


        public VideoGame()
        {
            title = string.Empty;
            developer = string.Empty;
            releaseyear = 0;
        }
        // Constructor with parameters to create a video game with provided information
        // Takes title, developer name, and release year as arguments
        public VideoGame(string title, string developer, int releaseyear)
        {
            this.title = title;
            this.developer = developer;
            this.releaseyear = releaseyear;
        }
        // Game title (must be unique)

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        // Developer name
        public string Developer
        {
            get { return developer; }
            set { developer = value; }
        }
        // Release year
        public int Releaseyear
        {
            get { return releaseyear; }
            set { releaseyear = value; }
        }
        // Compare two video games by title (title is unique identifier)

        public int CompareTo(object obj)
        {
            // Check if the object is a VideoGame using pattern matching
            if (obj is VideoGame otherGame)
            {
                // Compare by title using case-insensitive string comparison
                return string.Compare(this.title, otherGame.title, StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                // If it's not a VideoGame, throw an exception
                throw new ArgumentException("Object is not a VideoGame");
            }
        }
        // Return a string with all the video game information
        public override string ToString()
        {
            return $"Title: {title}, Developer: {developer}, Release Year: {releaseyear}";
        }

    }

}// End of class
}
