using System;
using System.Text.RegularExpressions;

namespace battleships
{
  class Program
  {
    static Random rnd = new Random();

    static void Main(string[] args)
    {
      //constants
      const int width = 10;
      const int height = width;
      const int ships_count = 10;

      //variables
      bool debug_on = false;
      bool game_over = false;
      bool turn_over = false;
     //create the game grid
      int[,] grid = new int[width, height];
      int input_x = -1;
      int input_y = -1;
      int turn = 0;
      int game_over_count = 0;
      Ship[] ships = new Ship[ships_count];

      //regular expressions
      Regex not_a_number = new Regex("[^0-9]");

      //structs
      //create the ships
      //battleship * 1
      Ship battleship = new Ship();
      battleship.Name = "battleship";
      battleship.Length = 5;
      battleship.Destroyed = false;
      //cruiser * 2
      Ship cruiser_1 = new Ship();
      cruiser_1.Name = "cruiser_1";
      cruiser_1.Length = 4;
      cruiser_1.Destroyed = false;
      Ship cruiser_2 = new Ship();
      cruiser_2.Name = "cruiser_2";
      cruiser_2.Length = 4;
      cruiser_2.Destroyed = false;
      //destroyer * 3
      Ship destroyer_1 = new Ship();
      destroyer_1.Name = "destroyer_1";
      destroyer_1.Length = 3;
      destroyer_1.Destroyed = false;
      Ship destroyer_2 = new Ship();
      destroyer_2.Name = "destroyer_2";
      destroyer_2.Length = 3;
      destroyer_2.Destroyed = false;
      Ship destroyer_3 = new Ship();
      destroyer_3.Name = "destroyer_3";
      destroyer_3.Length = 3;
      destroyer_3.Destroyed = false;
      //submarine * 4
      Ship submarine_1 = new Ship();
      submarine_1.Name = "submarine_1";
      submarine_1.Length = 2;
      submarine_1.Destroyed = false;
      Ship submarine_2 = new Ship();
      submarine_2.Name = "submarine_2";
      submarine_2.Length = 2;
      submarine_2.Destroyed = false;
      Ship submarine_3 = new Ship();
      submarine_3.Name = "submarine_3";
      submarine_3.Length = 2;
      submarine_3.Destroyed = false;
      Ship submarine_4 = new Ship();
      submarine_4.Name = "submarine_4";
      submarine_4.Length = 2;
      submarine_4.Destroyed = false;
      
      //fill grid with the ships and add them to the ships array
      battleship = CreateShip(grid, battleship, debug_on);
      ships[0] = battleship;
      cruiser_1 = CreateShip(grid, cruiser_1, debug_on);
      ships[1] = cruiser_1;
      cruiser_2 = CreateShip(grid, cruiser_2, debug_on);
      ships[2] = cruiser_2;
      destroyer_1 = CreateShip(grid, destroyer_1, debug_on);
      ships[3] = destroyer_1;
      destroyer_2 = CreateShip(grid, destroyer_2, debug_on);
      ships[4] = destroyer_2;
      destroyer_3 = CreateShip(grid, destroyer_3, debug_on);
      ships[5] = destroyer_3;
      submarine_1 = CreateShip(grid, submarine_1, debug_on);
      ships[6] = submarine_1;
      submarine_2 = CreateShip(grid, submarine_2, debug_on);
      ships[7] = submarine_2;
      submarine_3 = CreateShip(grid, submarine_3, debug_on);
      ships[8] = submarine_3;
      submarine_4 = CreateShip(grid, submarine_4, debug_on);
      ships[9] = submarine_4;

      //game loop
      while (game_over == false)
      {
        //debug output
        if (debug_on == true)
        {
          Console.WriteLine();
          Console.WriteLine("ships[0] batttleship (Lenth, Horizontal, X/Y, Destroyed): " + ships[0].Length + ", " + ships[0].Horizontal + ", " + ships[0].PositionX + ", " + ships[0].PositionY + ", " + ships[0].Destroyed);
          Console.WriteLine("ships[1]   cruiser_1 (Lenth, Horizontal, X/Y, Destroyed): " + ships[1].Length + ", " + ships[1].Horizontal + ", " + ships[1].PositionX + ", " + ships[1].PositionY + ", " + ships[1].Destroyed);
          Console.WriteLine("ships[2]   cruiser_2 (Lenth, Horizontal, X/Y, Destroyed): " + ships[2].Length + ", " + ships[2].Horizontal + ", " + ships[2].PositionX + ", " + ships[2].PositionY + ", " + ships[2].Destroyed);
          Console.WriteLine("ships[3] destroyer_1 (Lenth, Horizontal, X/Y, Destroyed): " + ships[3].Length + ", " + ships[3].Horizontal + ", " + ships[3].PositionX + ", " + ships[3].PositionY + ", " + ships[3].Destroyed);
          Console.WriteLine("ships[4] destroyer_2 (Lenth, Horizontal, X/Y, Destroyed): " + ships[4].Length + ", " + ships[4].Horizontal + ", " + ships[4].PositionX + ", " + ships[4].PositionY + ", " + ships[4].Destroyed);
          Console.WriteLine("ships[5] destroyer_3 (Lenth, Horizontal, X/Y, Destroyed): " + ships[5].Length + ", " + ships[5].Horizontal + ", " + ships[5].PositionX + ", " + ships[5].PositionY + ", " + ships[5].Destroyed);
          Console.WriteLine("ships[6] submarine_1 (Lenth, Horizontal, X/Y, Destroyed): " + ships[6].Length + ", " + ships[6].Horizontal + ", " + ships[6].PositionX + ", " + ships[6].PositionY + ", " + ships[6].Destroyed);
          Console.WriteLine("ships[7] submarine_2 (Lenth, Horizontal, X/Y, Destroyed): " + ships[7].Length + ", " + ships[7].Horizontal + ", " + ships[7].PositionX + ", " + ships[7].PositionY + ", " + ships[7].Destroyed);
          Console.WriteLine("ships[8] submarine_3 (Lenth, Horizontal, X/Y, Destroyed): " + ships[8].Length + ", " + ships[8].Horizontal + ", " + ships[8].PositionX + ", " + ships[8].PositionY + ", " + ships[8].Destroyed);
          Console.WriteLine("ships[9] submarine_4 (Lenth, Horizontal, X/Y, Destroyed): " + ships[9].Length + ", " + ships[9].Horizontal + ", " + ships[9].PositionX + ", " + ships[9].PositionY + ", " + ships[9].Destroyed);
          Console.WriteLine();
        }

        //clear screen from the previous turn
        if (debug_on == false)
        {
          Console.Clear();
        }

        //display the game screen
        ShowTitle();
        ShowGrid(grid, width, height, debug_on);

        //display the user input part of the game screen
        //get input_x for the x coordinate
        if (input_x < 0)
        {
          turn_over = false;
          ShowHelp();
          input_x = UserInput(not_a_number, ConsoleColor.Cyan, "X");
        }
        //get input_y for the y coordinate
        else if ((input_x >= 0) && (input_y < 0))
        {
          ShowHelp(input_x);
          input_y = UserInput(not_a_number, ConsoleColor.Magenta, "Y");
        }
        //confirm target, shoot at the x/y coordinates and then end the turn
        else if (((input_x >= 0) && (input_y >= 0)) && (turn_over == false))
        {
          ShowHelp(input_x, input_y);
          Console.Write("Press A Key To Shoot");
          Console.ReadKey();
          ShootAt(grid, input_y, input_x);
          input_x = -1;
          input_y = -1;
          //increase turn count for the game over screen
          turn++;
          turn_over = true;
        }
        //check for destroyed ships and update the ships array accordingly
        ships = ShipsDestroyed(grid, ships, debug_on);
        //check if all ships are destroyed ( = game over ) or if there is a next turn
        game_over_count = 0;
        for (int i = 0; i < ships.Length; i++)
        {
          if (debug_on == true)
          {
            Console.WriteLine(ships[i].Name + " destroyed: " + ships[i].Destroyed); 
          }
          if (ships[i].Destroyed == true)
          {
            game_over_count++;
          }
          if (game_over_count == ships_count)
          {
            game_over = true;
          }
        }
      }
      //display game over screen and the number of turns it took to win the game
      if (debug_on == false)
      {
        Console.Clear();
      }
      ShowTitle();
      Console.WriteLine();
      GameOver(turn);
    }

    //structs
    struct Ship
    {
      public string Name;
      public int Length;
      public bool Horizontal;
      public int PositionX;
      public int PositionY;
      public bool Destroyed;
    };

    //methods
    static void ClearLastLine()
    {
      Console.SetCursorPosition(0, Console.CursorTop - 1);
      Console.Write(new string(' ', Console.BufferWidth));
      Console.SetCursorPosition(0, Console.CursorTop - 1);
    }

    static Ship CreateShip(int[,] grid, Ship ship, bool debug_on)
    {
      bool ship_set = false;

      while (ship_set == false)
      {
        // random horizontal (if true) or vertical placement (if false)
        ship.Horizontal = rnd.NextDouble() >= 0.5;
        bool valid_position = false;

        //debug output
        if (debug_on == true)
	      {
		      Console.WriteLine();
          Console.WriteLine("ship.Name: " + ship.Name);
          Console.WriteLine("ship.Length: " + ship.Length);
          Console.WriteLine("ship.Horizontal: " + ship.Horizontal); 
        }

        //position ships horizontally
        if (ship.Horizontal)
        {
          //get random x and y position long enough for the ship
          if (debug_on == true)
          {
            Console.WriteLine("x max: " + (grid.GetUpperBound(0) - ship.Length + 1) + ", y max: " + grid.GetUpperBound(1));
          }
          int x_position = rnd.Next(0, (grid.GetUpperBound(0) - ship.Length + 2));
          ship.PositionX = x_position;
          int y_position = rnd.Next(0, grid.GetUpperBound(1));
          ship.PositionY = y_position;
          if (debug_on == true)
          {
            Console.WriteLine("ship.X: " + x_position + ", ship.Y: " + y_position);
          }
          //check for valid position for the ship
          int trigger = 0;
          //check at the ship
          for (int i = 0; i < ship.Length; i++)
          {
            if (grid[y_position, x_position + i] == 1)
            {
              trigger++;
              if (debug_on == true)
              {
                Console.WriteLine(ship.Name + " horizontal at ship position [" + (x_position + i) + "," + y_position + "] not valid!"); 
              }
            }
          }
          //check left of the ship
          if (x_position > 0)
          {
            if (grid[y_position, x_position - 1] == 1)
            {
              trigger++;
              if (debug_on == true)
              {
                Console.WriteLine(ship.Name + " horizontal left ship position [" + (x_position - 1) + "," + y_position + "] not valid!"); 
              }
            }
          }
          //check right of the ship
          if ((x_position + ship.Length - 1) < 9)
          {
            if (grid[y_position, x_position + ship.Length] == 1)
            {
              trigger++;
              if (debug_on == true)
              {
                Console.WriteLine(ship.Name + " horizontal right ship position [" + (x_position + ship.Length - 1) + "," + y_position + "] not valid!"); 
              }
            }
          }
          //check above the ship
          if (y_position > 0)
          {
            for (int i = 0; i < ship.Length; i++)
            {
              if (grid[y_position - 1, x_position + i] == 1)
              {
                trigger++;
                if (debug_on == true)
                {
                  Console.WriteLine(ship.Name + " horizontal above ship position [" + (x_position + i) + "," + (y_position - 1) + "] not valid!"); 
                }
              }
            }
          }
          //check below the ship
          if (y_position < 9)
          {
            for (int i = 0; i < ship.Length; i++)
            {
              if (grid[y_position + 1, x_position + i] == 1)
              {
                trigger++;
                if (debug_on == true)
                {
                  Console.WriteLine(ship.Name + " horizontal above ship position [" + (x_position + i) + "," + (y_position + 1) + "] not valid!"); 
                }
              }
            }
          }
          //decide if position is valid or not
          if (trigger > 0)
          {
            if (debug_on == true)
            {
              Console.WriteLine(ship.Name + " trigger = " + trigger + ", position unacceptable!"); 
            }
            valid_position = false;
          }
          else
          {
            if (debug_on == true)
            {
              Console.WriteLine(ship.Name + " position is valid!"); 
            }
            valid_position = true;
          }
          //set ship if position is valid
          if (valid_position == true)
          {
            for (int i = 0; i < ship.Length; i++)
            {
              grid[y_position, x_position + i] = 1;
            }
            if (debug_on == true)
            {
              Console.WriteLine(ship.Name + " set!"); 
            }
            ship_set = true;
          }
        }
        //position ships vertically
        else
        {
          //get random x and y position long enough for the ship
          if (debug_on == true)
          {
            Console.WriteLine("x max: " + grid.GetUpperBound(0) + ", y max: " + (grid.GetUpperBound(1) - ship.Length + 1)); 
          }
          int x_position = rnd.Next(0, grid.GetUpperBound(0) + 1);
          ship.PositionX = x_position;
          int y_position = rnd.Next(0, (grid.GetUpperBound(1) - ship.Length + 2));
          ship.PositionY = y_position;
          if (debug_on == true)
          {
            Console.WriteLine("ship.X: " + x_position + ", ship.Y: " + y_position); 
          }
          //check if position is valid for the ship
          int trigger = 0;
          //check at the ship
          for (int i = 0; i < ship.Length; i++)
          {
            if (grid[y_position + i, x_position] == 1)
            {
              trigger++;
              if (debug_on == true)
              {
                Console.WriteLine(ship.Name + " vertical position [" + x_position + "," + (y_position + i) + "] not valid!"); 
              }
            }
          }
          //check left of the ship
          if (x_position > 0)
          {
            for (int i = 0; i < ship.Length; i++)
            {
              if (grid[y_position + i, x_position - 1] == 1)
              {
                trigger++;
                if (debug_on == true)
                {
                  Console.WriteLine(ship.Name + " vertical left ship position [" + (x_position - 1) + "," + (y_position + i) + "] not valid!"); 
                }
              }
            }
          }
          //check right of the ship
          if (x_position < 9)
          {
            for (int i = 0; i < ship.Length; i++)
            {
              if (grid[y_position + i, x_position + 1] == 1)
              {
                trigger++;
                if (debug_on == true)
                {
                  Console.WriteLine(ship.Name + " vertical right ship position [" + (x_position + 1) + "," + (y_position + i) + "] not valid!"); 
                }
              }
            }
          }
          //check above the ship
          if (y_position > 0)
          {
            if (grid[y_position - 1, x_position] == 1)
            {
              trigger++;
              if (debug_on == true)
              {
                Console.WriteLine(ship.Name + " vertical above ship position [" + x_position + "," + (y_position - 1) + "] not valid!"); 
              }
            }
          }
          //check below the ship
          if ((y_position + ship.Length - 1) < 9)
          {
            if (grid[y_position + ship.Length, x_position] == 1)
            {
              trigger++;
              if (debug_on == true)
              {
                Console.WriteLine(ship.Name + " horizontal right ship position [" + x_position + "," + (y_position + ship.Length) + "] not valid!"); 
              }
            }
          }
          //decide if position is valid or not
          if (trigger > 0)
          {
            if (debug_on == true)
            {
              Console.WriteLine(ship.Name + " trigger = " + trigger + ", position unacceptable!"); 
            }
            valid_position = false;
          }
          else
          {
            if (debug_on == true)
            {
              Console.WriteLine(ship.Name + " position is valid!"); 
            }
            valid_position = true;
          }
          //set ship if position is valid
          if (valid_position == true)
          {
            for (int i = 0; i < ship.Length; i++)
            {
              grid[y_position + i, x_position] = 1;
            }
            if (debug_on == true)
            {
              Console.WriteLine(ship.Name + " set!"); 
            }
            ship_set = true;
          }
        }
      }
      return ship;
    }

    static void GameOver(int turns)
    {
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.WriteLine(">><<<<<<<<<<<<<<<<<<<");
      Console.WriteLine(">>                 <<");
      Console.WriteLine(">> !!!!!!!!!!!!!!! <<");
      Console.WriteLine(">> !!           !! <<");
      Console.WriteLine(">> !! Game Over !! <<");
      Console.WriteLine(">> !!           !! <<");
      Console.WriteLine(">> !!!!!!!!!!!!!!! <<");
      Console.WriteLine(">>                 <<");
      Console.WriteLine(">>>>>>>>>>>>>>>>>>><<");
      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine("Turns taken: " + turns);
      Console.ReadLine();
    }

    static void ShowTitle()
    {
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine(">>>> Battleships <<<<");
    }

    static void ShowHelp()
    {
      Console.ForegroundColor = ConsoleColor.White;
      Console.Write("< Enter Target: ");
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.Write("X");
      Console.ForegroundColor = ConsoleColor.White;
      Console.Write("/");
      Console.ForegroundColor = ConsoleColor.Magenta;
      Console.Write("Y");
      Console.ForegroundColor = ConsoleColor.White;
      Console.Write(" >");
      Console.WriteLine();
    }

    static void ShowHelp(int x)
    {
      Console.ForegroundColor = ConsoleColor.White;
      Console.Write("< Enter Target: ");
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.Write(x);
      Console.ForegroundColor = ConsoleColor.White;
      Console.Write("/");
      Console.ForegroundColor = ConsoleColor.Magenta;
      Console.Write("Y");
      Console.ForegroundColor = ConsoleColor.White;
      Console.Write(" >");
      Console.WriteLine();
    }

    static void ShowHelp(int x, int y)
    {
      Console.ForegroundColor = ConsoleColor.White;
      Console.Write("< Enter Target: ");
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.Write(x);
      Console.ForegroundColor = ConsoleColor.White;
      Console.Write("/");
      Console.ForegroundColor = ConsoleColor.Magenta;
      Console.Write(y);
      Console.ForegroundColor = ConsoleColor.White;
      Console.Write(" >");
      Console.WriteLine();
    }

    static int UserInput(Regex expression, ConsoleColor axiscolor, string axis)
    {
      string eingabe = null;
      Console.ForegroundColor = axiscolor;
      Console.Write(axis);
      Console.ForegroundColor = ConsoleColor.White;
      Console.Write(":");
      Console.ForegroundColor = axiscolor;
      eingabe = Console.ReadLine();
      //continue input while it's not a number, nothing (just enter) or more than one digit
      while ((expression.IsMatch(eingabe) == true) || eingabe == "" || eingabe.Length > 1)
      {
        ClearLastLine();
        Console.ForegroundColor = axiscolor;
        Console.Write(axis);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(":");
        Console.ForegroundColor = axiscolor;
        eingabe = Console.ReadLine();
      }
      //if its a single digit number cast it to int and use it
      int user_input = int.Parse(eingabe);
      return user_input;
    }

    static void ShowGrid(int[,] grid, int width, int height, bool debug_on)
    {
      //width headers
      Console.Write("  ");
      for (int i = 0; i < width; i++)
      {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(i + " ");
      }
      Console.WriteLine();

      //grid and height headers
      for (int x = 0; x < height; x++)
      {
        //heigth headers
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write(x + " ");

        //grid
        for (int y = 0; y < width; y++)
        {
          //display water
          if (grid[x, y] == 0)
          {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(grid[x, y]);
          }
          //display sunk ships
          else if (grid[x, y] == -2)
          {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(2);
          }
          //display hit ships
          else if (grid[x, y] == -1)
          {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(grid[x, y] * -1);
          }
          //display ships
          else if (grid[x, y] == 1)
          {
            if (debug_on == true)
            {
              //show ships for debugging
              Console.ForegroundColor = ConsoleColor.Yellow;
              Console.Write(grid[x, y]); 
            }
            else
            {
              //hide ships
              Console.ForegroundColor = ConsoleColor.Blue;
              Console.Write(0);
            }
          }
          //display missed shots
          else if (grid[x, y] == 2)
          {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(grid[x, y]);
          }
          //display horizontal spaces
          Console.ForegroundColor = ConsoleColor.White;
          Console.Write(" ");
        }
        Console.WriteLine();
      }
    }

    static Ship[] ShipsDestroyed(int[,] grid, Ship[] ships, bool debug_on)
    {
      for (int i = 0; i < ships.Length; i++)
      {
        bool destroyed = ShipDestroyed(grid, ships[i].PositionX, ships[i].PositionY, ships[i].Horizontal, ships[i].Length, debug_on);
        if (destroyed == true)
        {
          if (debug_on == true)
          {
            Console.WriteLine("name: " + ships[i].Name + ", position: " + ships[i].PositionX + "/" + ships[i].PositionY + ", length: " + ships[i].Length + ", horizontal: " + ships[i].Horizontal); 
          }
          DestroyShip(grid, ships[i].PositionX, ships[i].PositionY, ships[i].Horizontal, ships[i].Length, debug_on);
          ships[i].Destroyed = true;
        }
        else
        {
          //do nothing...
        }
      }
      return ships;
    }

    static void DestroyShip(int[,] grid, int positionX, int positionY, bool horizontal, int length, bool debug_on)
    {
      for (int i = 0; i < length; i++)
      {
        //sink if horizontal
        if (horizontal == true)
        {
          grid[positionY, positionX + i] = -2;
          Console.WriteLine(grid[positionY, positionX + i] + " ");
        }
        //sink if vertical
        else
        {
          grid[positionY + i, positionX] = -2;
          Console.WriteLine(grid[positionY + i, positionX] + " ");
        }
      }
      if (debug_on == true)
      {
        Console.WriteLine("glugg, glugg, glugg"); 
      }
    }

    static bool ShipDestroyed(int[,] grid, int positionX, int positionY, bool horizontal, int length, bool debug_on)
    {
      bool destroyed = false;
      int ship_destroyed_at = length * -1;
      int ship_hitpoints = 0;
      if (debug_on == true)
      {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("destroyed if " + ship_destroyed_at); 
      }
      for (int i = 0; i < length; i++)
      {
        //check horizontal ships if sunk
        if (horizontal == true)
        {
          if (debug_on == true)
          {
            Console.WriteLine(grid[positionY, positionX + i] + " "); 
          }
          ship_hitpoints += grid[positionY, positionX + i];
          if (debug_on == true)
          {
            Console.WriteLine("ship_hitpoints: " + ship_hitpoints); 
          }
          if (ship_hitpoints == ship_destroyed_at)
          {
            destroyed = true;
          }
        }
        //check vertical ships if sunk
        else
        {
          if (debug_on == true)
          {
            Console.WriteLine(grid[positionY + i, positionX] + " "); 
          }
          ship_hitpoints += grid[positionY + i, positionX];
          if (debug_on == true)
          {
            Console.WriteLine("ship_hitpoints: " + ship_hitpoints); 
          }
          if (ship_hitpoints == ship_destroyed_at)
          {
            destroyed = true;
          }
        }
      }
      return destroyed;
    }

    static void ShootAt(int[,] grid, int position_x, int position_y)
    {
      switch (grid[position_x, position_y])
      {
        //there is a ship that has been hit before and sank
        case -2:
          //do nothing
          break;
        //there is a ship that has been hit before
        case -1:
          //do nothing
          break;
        //there is water
        case 0:
          grid[position_x, position_y] = 2;
          break;
        //there is a ship
        case 1:
          grid[position_x, position_y] = -1;
          break;
        //there is water that has been hit before
        case 2:
          //do nothing
          break;
        default:
          //do nothing
          Console.WriteLine("An Error Occured! Contact your Programmer!");
          break;
      }
    }
  }
}
