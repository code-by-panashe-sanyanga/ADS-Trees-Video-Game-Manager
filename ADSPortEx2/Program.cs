using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADSPortEx2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create tree structure, user can choose BSTree or AVLTree
            BinTree<VideoGame> gameTree = new BSTree<VideoGame>();
            bool usingAVL = false;

            // Display welcome message
            Console.WriteLine("Video Game Management System");
            Console.WriteLine("Welcome to the video game tree system!");
            Console.WriteLine();

            // Main menu loop that keeps running until user chooses to exit
            while (true)
            {
                // Display menu options
                Console.WriteLine("Please select an option:");
                Console.WriteLine("1. Add a new video game to the tree");
                Console.WriteLine("2. Display all games (In-Order traversal)");
                Console.WriteLine("3. Display all games (Pre-Order traversal)");
                Console.WriteLine("4. Display all games (Post-Order traversal)");
                Console.WriteLine("5. Find the earliest game (alphabetically first title)");
                Console.WriteLine("6. Get tree height");
                Console.WriteLine("7. Get number of games in tree");
                Console.WriteLine("8. Update a video game (by title)");
                Console.WriteLine("9. List all games by release year");
                Console.WriteLine("10. Check if game exists (by title)");
                Console.WriteLine("11. Remove a video game (by title)");
                Console.WriteLine("12. Switch to AVL Tree");
                Console.WriteLine("13. Switch to BST Tree");
                Console.WriteLine("14. Exit the program");
                Console.WriteLine();
                Console.WriteLine($"Current tree type: {(usingAVL ? "AVL Tree" : "BST Tree")}");
                Console.WriteLine();

                // Get user input for menu selection
                Console.Write("Enter your choice (1-14): ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    // Process user choice and call appropriate method
                    switch (choice)
                    {
                        case "1":
                            // Add new video game to the tree
                            AddNewVideoGame(gameTree);
                            break;

                        case "2":
                            // Display all games using in-order traversal
                            DisplayInOrder(gameTree);
                            break;

                        case "3":
                            // Display all games using pre-order traversal
                            DisplayPreOrder(gameTree);
                            break;

                        case "4":
                            // Display all games using post-order traversal
                            DisplayPostOrder(gameTree);
                            break;

                        case "5":
                            // Find and display the game with earliest release year
                            FindEarliestGame(gameTree);
                            break;

                        case "6":
                            // Get and display the current height of the tree
                            GetTreeHeight(gameTree);
                            break;

                        case "7":
                            // Get and display the total number of games in the tree
                            GetGameCount(gameTree);
                            break;

                        case "8":
                            // Update an existing video game's details by searching for its title
                            UpdateVideoGame(gameTree);
                            break;

                        case "9":
                            // List all games that match a given release year
                            ListGamesByYear(gameTree);
                            break;

                        case "10":
                            // Check if a game with given title exists in the tree
                            CheckGameExists(gameTree);
                            break;

                        case "11":
                            // Remove a game from the tree by title
                            RemoveVideoGame(gameTree);
                            break;

                        case "12":
                            // Switch from BST to AVL tree and migrate all data
                            gameTree = SwitchToAVLTree(gameTree);
                            usingAVL = true;
                            Console.WriteLine("Switched to AVL Tree!");
                            break;

                        case "13":
                            // Switch from AVL to BST tree and migrate all data
                            gameTree = SwitchToBSTree(gameTree);
                            usingAVL = false;
                            Console.WriteLine("Switched to BST Tree!");
                            break;

                        case "14":
                            // Exit the program and display goodbye message
                            Console.WriteLine("Thank you for using the Video Game Management System!");
                            Console.WriteLine("Goodbye!");
                            return;

                        default:
                            // Invalid choice entered by user
                            Console.WriteLine("Invalid choice! Please enter a number between 1 and 14.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        // This method adds a new video game to the tree.
        // It reads input from the user and creates a new game object.
        static void AddNewVideoGame(BinTree<VideoGame> tree)
        {
            Console.WriteLine("Add New Video Game");

            // Get game title from user input.
            // Keep asking until a valid title that is not empty is provided.
            string title = string.Empty;
            while (true)
            {
                Console.Write("Enter game title: ");
                title = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(title))
                    break;
                Console.WriteLine("Title cannot be empty.");
            }

            // Get developer name from user input.
            // Keep asking until a valid developer name that is not empty is provided.
            string developer = string.Empty;
            while (true)
            {
                Console.Write("Enter developer name: ");
                developer = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(developer))
                    break;
                Console.WriteLine("Developer name cannot be empty.");
            }

            // Get release year from user input.
            // Validate that it is a number and within a reasonable range.
            string yearInput = string.Empty;
            int releaseYear = 0;
            while (true)
            {
                Console.Write("Enter release year: ");
                yearInput = Console.ReadLine();
                if (!int.TryParse(yearInput, out releaseYear))
                {
                    Console.WriteLine("Invalid year! Please enter a number.");
                    continue;
                }
                if (releaseYear < 1900 || releaseYear > DateTime.Now.Year + 10)
                {
                    Console.WriteLine($"Release year must be between 1900 and {DateTime.Now.Year + 10}!");
                    continue;
                }
                break;
            }

            // Create new video game object with user input and add it to the tree.
            VideoGame newGame = new VideoGame(title, developer, releaseYear);

            if (tree is BSTree<VideoGame> bst)
            {
                bst.InsertItem(newGame);
            }
            else if (tree is AVLTree<VideoGame> avl)
            {
                avl.InsertItem(newGame);
            }

            Console.WriteLine($"Video game added successfully!");
            Console.WriteLine($"Game details: {newGame}");
        }

        // This method displays all games using in order traversal.
        // In order traversal visits nodes in sorted order.
        static void DisplayInOrder(BinTree<VideoGame> tree)
        {
            Console.WriteLine("Video Games (In-Order Traversal)");
            bool isEmpty = false;
            if (tree is BSTree<VideoGame> bst)
                isEmpty = bst.Count() == 0;
            else if (tree is AVLTree<VideoGame> avl)
                isEmpty = avl.Count() == 0;

            if (isEmpty)
            {
                Console.WriteLine("Tree is empty! No games to display.");
            }
            else
            {
                string buffer = "";
                tree.InOrder(ref buffer);
                if (string.IsNullOrWhiteSpace(buffer))
                    Console.WriteLine("Tree is empty! No games to display.");
                else
                    Console.WriteLine(buffer);
            }
        }

        // This method displays all games using pre order traversal.
        // Pre order traversal visits the root before subtrees.
        static void DisplayPreOrder(BinTree<VideoGame> tree)
        {
            Console.WriteLine("Video Games (Pre-Order Traversal)");
            bool isEmpty = false;
            if (tree is BSTree<VideoGame> bst)
                isEmpty = bst.Count() == 0;
            else if (tree is AVLTree<VideoGame> avl)
                isEmpty = avl.Count() == 0;

            if (isEmpty)
            {
                Console.WriteLine("Tree is empty! No games to display.");
            }
            else
            {
                string buffer = "";
                tree.PreOrder(ref buffer);
                if (string.IsNullOrWhiteSpace(buffer))
                    Console.WriteLine("Tree is empty! No games to display.");
                else
                    Console.WriteLine(buffer);
            }
        }

        // This method displays all games using post order traversal.
        // Post order traversal visits subtrees before the root.
        static void DisplayPostOrder(BinTree<VideoGame> tree)
        {
            Console.WriteLine("Video Games (Post-Order Traversal)");
            bool isEmpty = false;
            if (tree is BSTree<VideoGame> bst)
                isEmpty = bst.Count() == 0;
            else if (tree is AVLTree<VideoGame> avl)
                isEmpty = avl.Count() == 0;

            if (isEmpty)
            {
                Console.WriteLine("Tree is empty! No games to display.");
            }
            else
            {
                string buffer = "";
                tree.PostOrder(ref buffer);
                if (string.IsNullOrWhiteSpace(buffer))
                    Console.WriteLine("Tree is empty! No games to display.");
                else
                    Console.WriteLine(buffer);
            }
        }

        // This method finds and displays the game with the earliest release year.
        // It searches through the entire tree to find the minimum release year.
        static void FindEarliestGame(BinTree<VideoGame> tree)
        {
            Console.WriteLine("Find Earliest Game (Earliest Release Year)");
            if (tree is BSTree<VideoGame> bst)
            {
                if (bst.Count() == 0)
                {
                    Console.WriteLine("Tree is empty! No games to find.");
                }
                else
                {
                    VideoGame earliest = bst.EarlieseGame();
                    Console.WriteLine($"Earliest game: {earliest}");
                }
            }
            else if (tree is AVLTree<VideoGame> avl)
            {
                if (avl.Count() == 0)
                {
                    Console.WriteLine("Tree is empty! No games to find.");
                }
                else
                {
                    VideoGame earliest = avl.EarlieseGame();
                    Console.WriteLine($"Earliest game: {earliest}");
                }
            }
        }

        // This method gets and displays the current height of the tree.
        // Height is the number of edges from root to the deepest leaf node.
        static void GetTreeHeight(BinTree<VideoGame> tree)
        {
            Console.WriteLine("Tree Height");
            if (tree is BSTree<VideoGame> bst)
            {
                int height = bst.Height();
                Console.WriteLine($"Tree height: {height}");
                if (bst.Count() == 0)
                {
                    Console.WriteLine("(Tree is empty)");
                }
            }
            else if (tree is AVLTree<VideoGame> avl)
            {
                int height = avl.Height();
                Console.WriteLine($"Tree height: {height}");
                if (avl.Count() == 0)
                {
                    Console.WriteLine("(Tree is empty)");
                }
            }
        }

        // Method to get and display the total number of games in the tree
        // Counts all nodes recursively to get the total number of entries
        static void GetGameCount(BinTree<VideoGame> tree)
        {
            Console.WriteLine("Game Count");
            if (tree is BSTree<VideoGame> bst)
            {
                int count = bst.Count();
                Console.WriteLine($"Number of games in tree: {count}");
            }
            else if (tree is AVLTree<VideoGame> avl)
            {
                int count = avl.Count();
                Console.WriteLine($"Number of games in tree: {count}");
            }
        }

        // This method updates an existing video game by searching for its title.
        // It finds the game and replaces its data with new information provided by the user.
        static void UpdateVideoGame(BinTree<VideoGame> tree)
        {
            Console.WriteLine("Update Video Game (by Title)");
            if (tree is BSTree<VideoGame> bst && !(tree is AVLTree<VideoGame>))
            {
                if (bst.Count() == 0)
                {
                    Console.WriteLine("Tree is empty! No games to update.");
                    return;
                }

                // Get the title of the game to update from user
                Console.Write("Enter the title of the game to update: ");
                string oldTitle = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(oldTitle))
                {
                    Console.WriteLine("Title cannot be empty!");
                    return;
                }

                // Create a temporary game object to search for in the tree
                // We use title for comparison since that is the unique identifier
                VideoGame tempGame = new VideoGame(oldTitle, "", 0);

                // Check if the game exists in the tree before attempting update
                if (!bst.Contains(tempGame))
                {
                    Console.WriteLine($"Game with title '{oldTitle}' not found in the tree.");
                    return;
                }

                // Get new game details from user to replace existing data
                Console.Write("Enter new title (or press Enter to keep current): ");
                string newTitle = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newTitle))
                {
                    newTitle = oldTitle; // Keep current title
                }

                Console.Write("Enter new developer: ");
                string newDeveloper = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newDeveloper))
                {
                    Console.WriteLine("Developer cannot be empty!");
                    return;
                }

                Console.Write("Enter new release year: ");
                if (!int.TryParse(Console.ReadLine(), out int newYear))
                {
                    Console.WriteLine("Invalid year!");
                    return;
                }

                // Create updated game object with new details and update it in the tree
                VideoGame updatedGame = new VideoGame(newTitle, newDeveloper, newYear);
                bst.Update(updatedGame);
                Console.WriteLine("Game updated successfully!");
                Console.WriteLine($"Updated game: {updatedGame}");
            }
            else if (tree is AVLTree<VideoGame> avl)
            {
                if (avl.Count() == 0)
                {
                    Console.WriteLine("Tree is empty! No games to update.");
                    return;
                }

                // Get the title of the game to update from user
                Console.Write("Enter the title of the game to update: ");
                string oldTitle = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(oldTitle))
                {
                    Console.WriteLine("Title cannot be empty!");
                    return;
                }

                // Create a temporary game object to search for in the tree
                VideoGame tempGame = new VideoGame(oldTitle, "", 0);

                // Check if the game exists in the tree before attempting update
                if (!avl.Contains(tempGame))
                {
                    Console.WriteLine($"Game with title '{oldTitle}' not found in the tree.");
                    return;
                }

                // Get new game details from user to replace existing data
                Console.Write("Enter new title (or press Enter to keep current): ");
                string newTitle = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newTitle))
                {
                    newTitle = oldTitle; // Keep current title
                }

                Console.Write("Enter new developer: ");
                string newDeveloper = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newDeveloper))
                {
                    Console.WriteLine("Developer cannot be empty!");
                    return;
                }

                Console.Write("Enter new release year: ");
                if (!int.TryParse(Console.ReadLine(), out int newYear))
                {
                    Console.WriteLine("Invalid year!");
                    return;
                }

                // Create updated game object with new details and update it in the tree
                VideoGame updatedGame = new VideoGame(newTitle, newDeveloper, newYear);
                avl.Update(updatedGame);
                Console.WriteLine("Game updated successfully!");
                Console.WriteLine($"Updated game: {updatedGame}");
            }
        }

        // Method to list all games that match a given release year
        // Searches through the entire tree and displays all games with matching year
        static void ListGamesByYear(BinTree<VideoGame> tree)
        {
            Console.WriteLine("List Games by Release Year");
            if (tree is BSTree<VideoGame> bst && !(tree is AVLTree<VideoGame>))
            {
                if (bst.Count() == 0)
                {
                    Console.WriteLine("Tree is empty! No games to list.");
                    return;
                }

                Console.Write("Enter release year to search for: ");
                if (!int.TryParse(Console.ReadLine(), out int year))
                {
                    Console.WriteLine("Invalid year!");
                    return;
                }

                string buffer = "";
                bst.ListGamesByYear(year, ref buffer);
                if (string.IsNullOrWhiteSpace(buffer))
                {
                    Console.WriteLine($"No games found with release year {year}.");
                }
                else
                {
                    Console.WriteLine($"Games released in {year}:");
                    Console.WriteLine(buffer);
                }
            }
            else if (tree is AVLTree<VideoGame> avl)
            {
                if (avl.Count() == 0)
                {
                    Console.WriteLine("Tree is empty! No games to list.");
                    return;
                }

                Console.Write("Enter release year to search for: ");
                if (!int.TryParse(Console.ReadLine(), out int year))
                {
                    Console.WriteLine("Invalid year!");
                    return;
                }

                string buffer = "";
                avl.ListGamesByYear(year, ref buffer);
                if (string.IsNullOrWhiteSpace(buffer))
                {
                    Console.WriteLine($"No games found with release year {year}.");
                }
                else
                {
                    Console.WriteLine($"Games released in {year}:");
                    Console.WriteLine(buffer);
                }
            }
        }

        // This method checks if a game with the given title exists in the tree.
        // It uses the tree search functionality to find the game by title.
        static void CheckGameExists(BinTree<VideoGame> tree)
        {
            Console.WriteLine("Check if Game Exists (by Title)");
            if (tree is BSTree<VideoGame> bst && !(tree is AVLTree<VideoGame>))
            {
                if (bst.Count() == 0)
                {
                    Console.WriteLine("Tree is empty! No games to check.");
                    return;
                }

                Console.Write("Enter game title to search for: ");
                string title = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Title cannot be empty!");
                    return;
                }

                VideoGame searchGame = new VideoGame(title, "", 0);
                bool exists = bst.Contains(searchGame);
                if (exists)
                {
                    Console.WriteLine($"Game with title '{title}' exists in the tree.");
                }
                else
                {
                    Console.WriteLine($"Game with title '{title}' does not exist in the tree.");
                }
            }
            else if (tree is AVLTree<VideoGame> avl)
            {
                if (avl.Count() == 0)
                {
                    Console.WriteLine("Tree is empty! No games to check.");
                    return;
                }

                Console.Write("Enter game title to search for: ");
                string title = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Title cannot be empty!");
                    return;
                }

                VideoGame searchGame = new VideoGame(title, "", 0);
                bool exists = avl.Contains(searchGame);
                if (exists)
                {
                    Console.WriteLine($"Game with title '{title}' exists in the tree.");
                }
                else
                {
                    Console.WriteLine($"Game with title '{title}' does not exist in the tree.");
                }
            }
        }

        // This method removes a video game from the tree by searching for its title.
        // It finds the game and removes it while maintaining the tree structure.
        static void RemoveVideoGame(BinTree<VideoGame> tree)
        {
            Console.WriteLine("Remove Video Game (by Title)");
            if (tree is BSTree<VideoGame> bst)
            {
                if (bst.Count() == 0)
                {
                    Console.WriteLine("Tree is empty! No games to remove.");
                    return;
                }

                Console.Write("Enter title of the game to remove: ");
                string title = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Title cannot be empty!");
                    return;
                }

                VideoGame gameToRemove = new VideoGame(title, "", 0);
                if (bst.Contains(gameToRemove))
                {
                    bst.RemoveItem(gameToRemove);
                    Console.WriteLine($"Game with title '{title}' removed successfully!");
                    Console.WriteLine($"Remaining games: {bst.Count()}");
                }
                else
                {
                    Console.WriteLine($"Game with title '{title}' not found in the tree.");
                }
            }
            else if (tree is AVLTree<VideoGame> avl)
            {
                if (avl.Count() == 0)
                {
                    Console.WriteLine("Tree is empty! No games to remove.");
                    return;
                }

                Console.Write("Enter title of the game to remove: ");
                string title = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Title cannot be empty!");
                    return;
                }

                VideoGame gameToRemove = new VideoGame(title, "", 0);
                if (avl.Contains(gameToRemove))
                {
                    avl.RemoveItem(gameToRemove);
                    Console.WriteLine($"Game with title '{title}' removed successfully!");
                    Console.WriteLine($"Remaining games: {avl.Count()}");
                }
                else
                {
                    Console.WriteLine($"Game with title '{title}' not found in the tree.");
                }
            }
        }

        // Method to switch to AVL Tree (migrates data)
        static BinTree<VideoGame> SwitchToAVLTree(BinTree<VideoGame> currentTree)
        {
            AVLTree<VideoGame> avlTree = new AVLTree<VideoGame>();

            if (currentTree is BSTree<VideoGame> bst)
            {
                // Migrate all data from BST to AVL tree to preserve user's data
                if (bst.Count() > 0)
                {
                    // Get all items from current BST tree using in-order traversal
                    List<VideoGame> items = bst.GetAllItems();

                    // Insert all items into the new AVL tree
                    // AVL tree will automatically balance itself during insertion
                    foreach (VideoGame game in items)
                    {
                        avlTree.InsertItem(game);
                    }

                    Console.WriteLine($"Switched to AVL Tree! Migrated {items.Count} games.");
                }
                else
                {
                    Console.WriteLine("Switched to AVL Tree. Tree was empty, so starting fresh.");
                }
            }
            else if (currentTree is AVLTree<VideoGame> avl)
            {
                // Already using AVL tree, so no migration needed
                Console.WriteLine("Already using AVL Tree!");
                return currentTree;
            }

            return avlTree;
        }

        // Method to switch to BST Tree (migrates data)
        static BinTree<VideoGame> SwitchToBSTree(BinTree<VideoGame> currentTree)
        {
            BSTree<VideoGame> bstTree = new BSTree<VideoGame>();

            if (currentTree is BSTree<VideoGame> bst && !(currentTree is AVLTree<VideoGame>))
            {
                // Already using BST tree, so no migration needed
                Console.WriteLine("Already using BST Tree!");
                return currentTree;
            }
            else if (currentTree is AVLTree<VideoGame> avl)
            {
                // Migrate all data from AVL to BST tree to preserve user's data
                if (avl.Count() > 0)
                {
                    // Get all items from current AVL tree using in-order traversal
                    // GetAllItems is inherited from BSTree base class
                    List<VideoGame> items = avl.GetAllItems();

                    // Insert all items into the new BST tree
                    foreach (VideoGame game in items)
                    {
                        bstTree.InsertItem(game);
                    }

                    Console.WriteLine($"Switched to BST Tree! Migrated {items.Count} games.");
                }
                else
                {
                    Console.WriteLine("Switched to BST Tree. Tree was empty, so starting fresh.");
                }
            }

            return bstTree;
        }
    }
}
