using System;
using System.Collections.Generic;
using System.Linq;

public class cinemaRoom{
    public int rows;
    public int seats;
    public int totalseats;
    public string[] arrayOfSeats;
    private int currentEarnings = 0;
    private int potentialEarnings;
    private int expensive = 12;
    private int cheap = 10;
    private bool highCapacity;

    public cinemaRoom(int rows, int seats)
    {
        this.rows = rows;
        this.seats = seats;
        totalseats = rows*seats;
        highCapacity = (totalseats > 50) ? true : false;
        arrayOfSeats = new string[rows*seats];
        potentialEarnings = highCapacity ? 
            (rows/2) * seats * cheap + (rows - (rows/2)) * seats * expensive : totalseats*cheap;
        
        for (int i = 0; i < arrayOfSeats.Length; i++ ) 
        {
            arrayOfSeats[i] = "A";
        }
    }

    public void printCinema()
    {
        bool expensiveRow;

        string output = "Seats   ";
        for (int i = 0; i < seats; i++)
            output += i + " ";
        Console.WriteLine(output + "  Cost\n");

        for(int i = 0; i < rows; i++)
        {
            expensiveRow = (highCapacity && i > (rows/2 - 1)) ? true : false;

            output = "Row " + i + ".  ";
            for(int j = 0; j < seats; j++)
            {
                output += arrayOfSeats[i*seats+j] + " "; 
            }

            if (expensiveRow) Console.WriteLine(output + "  " + expensive + "$");
            else Console.WriteLine(output + "  " + cheap + "$");
        }

        Console.WriteLine("\n        Screen Placement\n");
    }

    public void purchaseTicket(int row, int seat)
    {
        bool expensiveRow;

        if (arrayOfSeats[row*seats + seat] == "A")
        {
            expensiveRow = (highCapacity && row > (rows/2 -1)) ? true : false;
            Console.WriteLine("Are you sure you want to purchase this ticket?");
            if (expensiveRow) Console.WriteLine("Row: " + row + ". Seat: " + seat + ". Price: " + expensive + "$");
            else Console.WriteLine("Row: " + row + ". Seat: " + seat + ". Price: " + cheap + "$");
            Console.WriteLine("Type: 'yes' to confirm, 'no' to cancel\n");

            while (true)
            {
                switch (Console.ReadLine().ToLower())
                {
                    case "yes":
                        arrayOfSeats.SetValue("R", row*seats + seat);

                        currentEarnings = (totalseats > 50 && row > (rows/2 -1)) ? 
                            currentEarnings + expensive : currentEarnings + cheap;
                        Console.Clear();
                        printCinema();
                        return;
                    case "no":
                        Console.Clear();
                        printCinema();
                        return;
                    default:
                        Console.WriteLine("Unknown option. Type: 'yes' to confirm, 'no' to cancel?\n");
                        break;

                }
            }

        }
        else
        {
            Console.WriteLine("Seat has already been reserved\n");
        }
       
    }

    public void showMetrics()
    {
        var numberOfPurchases = arrayOfSeats.Where(seat => seat == "R");
        float percentageOccupied = (float)numberOfPurchases.Count()/(float)totalseats;
        Console.WriteLine("Number of purchased tickets:" + numberOfPurchases.Count());
        Console.WriteLine("Percentage of occupied seats: " + percentageOccupied*100 + "%");
        Console.WriteLine("Current income: " + currentEarnings + "$");
        Console.WriteLine("Potential income: " + potentialEarnings + "$");
    }
}

public class userInterface
{
    cinemaRoom room;
    private string password = "123";
    public void setupCinema()
    {
        Console.WriteLine("Enter the amount of rows the cinema room has."); 
        int rows;
        while(true)
        {
            try
            {
                rows = int.Parse(Console.ReadLine());
                if (rows > 0) break;
                else
                {
                    Console.WriteLine("The amount of rows must be a positive integer.");
                    continue;
                }
            }
            catch
            {
                Console.WriteLine("The amount of rows must be a positive integer.");
                continue;
            }
        }

        Console.WriteLine("Enter the amount of seats each of the rows has."); 
        int seats;
        while(true)
        {
            try
            {
                seats = int.Parse(Console.ReadLine());
                if (seats > 0) break;
                else
                {
                    Console.WriteLine("The amount of seats per row must be a positive integer.");
                    continue;
                }
            }
            catch
            {
                Console.WriteLine("The amount of seats per row must be a positive integer.");
                continue;
            }
        }

        Console.Clear();

        room = new cinemaRoom(rows, seats);
        room.printCinema();
    }

    public void purchaseTicket()
    {
        Console.WriteLine("Enter the desired row number."); 
        int row;
        while(true)
        {
            try
            {
                row = int.Parse(Console.ReadLine());
                if (row > -1 && row < room.rows) break;
                else
                {
                    Console.WriteLine("The row number must be an existing row.");
                    continue;
                }
            }
            catch
            {
                Console.WriteLine("The row number must be an existing row.");
                continue;
            }
        }

        Console.WriteLine("Enter the desired seat number."); 
        int seat;
        while(true)
        {
            try
            {
                seat = int.Parse(Console.ReadLine());
                if (seat > -1 && seat < room.seats) break;
                else
                {
                    Console.WriteLine("The seat number must be an existing seat number.");
                    continue;
                }
            }
            catch
            {
                Console.WriteLine("The seat number must be an existing seat number.");
                continue;
            }
        }

        room.purchaseTicket(row, seat);
    }

    public void showHelp()
    {
        Console.WriteLine("Write one of the following to commands to perform it:");
        Console.WriteLine("'purchase' : will guide you through purchasing a ticket for the cinema");
        Console.WriteLine("'showcinema' : will show you the cinema including seat reservations and prices");
        Console.WriteLine("'metrics' : will show you metrics associated with the cinema such as earnings");
        Console.WriteLine("'end' : this will end the program\n");
    }

    public void showMetrics()
    {
        Console.WriteLine("Please enter the password");
        if (Console.ReadLine() == password) room.showMetrics();
        else Console.WriteLine("Incorret password");

    }
    public void nextCommand(string command)
    {
        switch (command)
        {
            case "purchase":
                Console.Clear();
                room.printCinema();
                purchaseTicket();
                break;
            case "metrics":
                showMetrics();
                break;
            case "help":
                showHelp();
                break;
            case "showcinema":
                Console.Clear();
                room.printCinema();
                break;
            default:
                Console.WriteLine("Unknown option. Have you checked if your spelling is correct?\n");
                break;
        }
    }
}

class mainSetup {         
    static void Main(string[] args)
    {
        var UI = new userInterface();
        UI.setupCinema();
        string command;
        while (true)
        {
            Console.WriteLine("Type the option you wish to perform: 'purchase', 'showcinema', 'metrics', 'end', 'help'");
            command = Console.ReadLine().ToLower();
            if (command == "end") break;
            else UI.nextCommand(command);
        }
    }
}