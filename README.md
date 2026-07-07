# ADS Assessed Exercise 2 — Trees (BST & AVL Video Game Manager)

**Module:** Algorithms and Data Structures (ADS)
**Author:** [Panashe Sanyanga](https://github.com/code-by-panashe-sanyanga)
**Language:** C# (.NET Framework)

> **Note:** This is a **re-upload** of my university assessed coursework so it lives on my personal GitHub profile alongside my other work.

---

## Data structures covered

This exercise is about **tree data structures**, implemented generically (`<T> where T : IComparable`):

1. **Binary Tree (`BinTree`)** — the abstract base defining the tree interface and traversals.
2. **Binary Search Tree (`BSTree`)** — ordered insert/search/remove where left < node < right.
3. **AVL Tree (`AVLTree`)** — a self-balancing BST that rotates on insert to keep the height balanced, guaranteeing O(log n) operations.

The AVL tree inherits from the BST and adds height tracking, balance-factor checks, and the four rotations (LL, RR, LR, RL).

## What the program does

It manages a catalogue of **video games** (title, developer, release year) stored in a tree keyed by title. The user can switch between a plain BST and a self-balancing AVL tree at runtime and compare their behaviour (e.g. tree height).

## Core operations (from the console menu)

- Add a game to the tree
- Display all games via **In-Order**, **Pre-Order**, and **Post-Order** traversals
- Find the earliest (alphabetically first) game
- Get the tree height
- Count the games in the tree
- Update a game by title
- List games by release year
- Check if a game exists / remove a game
- Switch between **BST** and **AVL** modes

## Why BST vs AVL matters

| | BST (unbalanced) | AVL (balanced) |
|---|---|---|
| Insert/Search/Remove (average) | O(log n) | O(log n) |
| Insert/Search/Remove (worst) | O(n) — degenerates to a list | O(log n) — rotations keep it balanced |

Inserting already-sorted data into a plain BST makes it behave like a linked list; the AVL tree avoids this by rebalancing.

## Project structure

```
ADSPortEx2/
├── Node.cs          # Generic tree node (data + left/right + height)
├── BinTree.cs       # Abstract base tree + traversals
├── BSTree.cs        # Binary Search Tree (insert/search/remove)
├── AVLTree.cs       # Self-balancing tree (rotations) — extends BSTree
├── VideoGame.cs     # Data model: title, developer, year (IComparable)
├── Program.cs       # Console menu / test harness
├── App.config
└── Properties/AssemblyInfo.cs
ADSPortEx2.sln
```

## File breakdown

- **`Node.cs`** — a single node holding the value, left/right child references, and (for AVL) a height field.
- **`BinTree.cs`** — the abstract binary tree defining the shared interface and the three depth-first traversals.
- **`BSTree.cs`** — ordered insertion, search, removal, height, and traversal output.
- **`AVLTree.cs`** — overrides insertion to rebalance using rotations after each insert.
- **`VideoGame.cs`** — the item stored in the tree; compares by title so the tree stays ordered alphabetically.
- **`Program.cs`** — menu-driven testing of every operation and the BST/AVL switch.

## How to run it

**Visual Studio**

1. Open `ADSPortEx2.sln`.
2. Press **F5** or **Ctrl+F5**.

**Command line**

```bash
msbuild ADSPortEx2.sln
ADSPortEx2\bin\Debug\ADSPortEx2.exe
```

## Key takeaways

- Recursion is the natural way to implement tree insert, search, and traversal.
- The difference between the three traversal orders and when each is useful.
- How AVL rotations keep a search tree balanced and why that protects worst-case performance.
