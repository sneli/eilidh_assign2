using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using sodv2202_assign2;
using System.Runtime.Serialization;
using System.Runtime;

namespace eilidh_assign2
{
    internal class Program
    {
        static List<PlayerStats> Stats = new List<PlayerStats>();

        static void BuildDBFromFile()
        {
            using (var reader = File.OpenText("NHL Player Stats 2017-18.csv"))
            {
                string input = reader.ReadLine(); // Skip header line
                while ((input = reader.ReadLine()) != null)
                {
                    PlayerStats playerStats = new PlayerStats(input);
                    Stats.Add(playerStats);
                }
            }
        }

        static void Main(string[] args)
        {
            BuildDBFromFile();

            while (true)
            {
                Console.WriteLine("NHL 2017-18 Player Stats Browser");
                Console.WriteLine("1. Display All Players");
                Console.WriteLine("2. Filter Data");
                Console.WriteLine("3. Sort Data");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayPlayers(Stats);
                        break;
                    case "2":
                        Console.Write("Enter filter expression: ");
                        var filter = Console.ReadLine();
                        var filteredPlayers = FilterPlayers(Stats, filter);
                        DisplayPlayers(filteredPlayers);
                        break;
                    case "3":
                        Console.Write("Enter sort expression (a property and an order (asc or desc): ");
                        var sort = Console.ReadLine();
                        Stats = SortPlayers(Stats, sort); // Update the Stats list
                        DisplayPlayers(Stats); // Display sorted list
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
                Console.WriteLine("Press enter to restart!");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public static void DisplayPlayers(List<PlayerStats> stats)
        {
            Console.WriteLine($"{"Name",-25} {"Team",3} {"Pos",5} {"GP",3} {"G",2} {"A",3} {"P",3} {"+/-",5} {"PIM",4} {"PGB",5} {"PPG",4} {"PPP",3} {"SHG",3} {"SHP",3} {"GWG",3} {"OTG",3} {"S",3} {"S%",5} {"TOI/GP",9} {"Shifts/GP",12} {"FOW%",6}");
            foreach (var player in stats) // Display the list passed as argument
            {
                Console.WriteLine($"{player.Name, -25} {player.Team, 3} {player.Pos, 5} {player.GP, 4} {player.G, 3} {player.A, 3} {player.P, 3} {player.PlusMinus,4} {player.PIM,4} {player.PGP,5} {player.PPG,3} {player.PPP,3} {player.SHG,3} {player.SHP,3} {player.GWG,3} {player.OTG,3} {player.S,5} {player.Spercent, 5} {player.TOIperGP,8} {player.ShiftsPerGP,10} {player.FOWpercent,8}");
            }
        }

        public static List<PlayerStats> FilterPlayers(List<PlayerStats> stats, string filter)
        {
            var filters = filter.Split(','); // Support multiple filters separated by commas
            foreach (var f in filters)
            {
                string property = "", op = "", value = "";
                string trimmedFilter = f.Trim();

                // Detect the operator and split accordingly
                if (trimmedFilter.Contains(">="))
                {
                    op = ">=";
                }
                else if (trimmedFilter.Contains("<="))
                {
                    op = "<=";
                }
                else if (trimmedFilter.Contains("=="))
                {
                    op = "==";
                }
                else if (trimmedFilter.Contains(">"))
                {
                    op = ">";
                }
                else if (trimmedFilter.Contains("<"))
                {
                    op = "<";
                }
                else
                {
                    Console.WriteLine($"Invalid filter format: {trimmedFilter}");
                    continue;
                }

                // Split into property and value using the detected operator
                var parts = trimmedFilter.Split(new[] { op }, StringSplitOptions.None);
                if (parts.Length != 2)
                {
                    Console.WriteLine($"Invalid filter format: {trimmedFilter}");
                    continue;
                }

                property = parts[0].Trim();
                value = parts[1].Trim();

                // Apply the filter based on the property
                switch (property.ToLower())
                {
                    case "name":
                        stats = stats.Where(s => Compare(s.Name, op, value)).ToList();
                        break;
                    case "team":
                        stats = stats.Where(s => Compare(s.Team, op, value)).ToList();
                        break;
                    case "pos":
                        stats = stats.Where(s => Compare(s.Pos, op, value)).ToList();
                        break;
                    case "gp":
                        stats = stats.Where(s => Compare(s.GP, op, int.Parse(value))).ToList();
                        break;
                    case "g":
                        stats = stats.Where(s => Compare(s.G, op, int.Parse(value))).ToList();
                        break;
                    case "a":
                        stats = stats.Where(s => Compare(s.A, op, int.Parse(value))).ToList();
                        break;
                    case "p":
                        stats = stats.Where(s => Compare(s.P, op, int.Parse(value))).ToList();
                        break;
                    case "plusminus":
                    case "+/-":
                        stats = stats.Where(s => Compare(s.PlusMinus, op, int.Parse(value))).ToList();
                        break;
                    case "pim":
                        stats = stats.Where(s => Compare(s.PIM, op, int.Parse(value))).ToList();
                        break;
                    case "pgp":
                        stats = stats.Where(s => Compare(s.PGP, op, double.Parse(value))).ToList();
                        break;
                    case "ppg":
                        stats = stats.Where(s => Compare(s.PPG, op, int.Parse(value))).ToList();
                        break;
                    case "ppp":
                        stats = stats.Where(s => Compare(s.PPP, op, int.Parse(value))).ToList();
                        break;
                    case "shg":
                        stats = stats.Where(s => Compare(s.SHG, op, int.Parse(value))).ToList();
                        break;
                    case "shp":
                        stats = stats.Where(s => Compare(s.SHP, op, int.Parse(value))).ToList();
                        break;
                    case "gwg":
                        stats = stats.Where(s => Compare(s.GWG, op, int.Parse(value))).ToList();
                        break;
                    case "otg":
                        stats = stats.Where(s => Compare(s.OTG, op, int.Parse(value))).ToList();
                        break;
                    case "s":
                        stats = stats.Where(s => Compare(s.S, op, int.Parse(value))).ToList();
                        break;
                    case "spercent":
                    case "s%":
                        stats = stats.Where(s => Compare(s.Spercent, op, double.Parse(value))).ToList();
                        break;
                    case "toipergp":
                    case "toi/gp":
                        stats = stats.Where(s => Compare(s.TOIperGP, op, value)).ToList();
                        break;
                    case "shiftspergp":
                    case "shifts/gp":
                        stats = stats.Where(s => Compare(s.ShiftsPerGP, op, double.Parse(value))).ToList();
                        break;
                    case "fowpercent":
                    case "fow%":
                        stats = stats.Where(s => Compare(s.FOWpercent, op, double.Parse(value))).ToList();
                        break;
                    default:
                        Console.WriteLine($"Unsupported property: {property}");
                        break;
                }
            }

            if (!stats.Any())
            {
                Console.WriteLine("No players match the given filter.");
            }

            return stats;
        }


        public static bool Compare<T>(T a, string op, T b) where T : IComparable<T>
        {
            switch (op)
            {
                case "<": return a.CompareTo(b) < 0;
                case "<=": return a.CompareTo(b) <= 0;
                case "==": return a.CompareTo(b) == 0;
                case ">=": return a.CompareTo(b) >= 0;
                case ">": return a.CompareTo(b) > 0;
                default: throw new InvalidOperationException("Invalid comparison operator");
            }
        }

        public static List<PlayerStats> SortPlayers(List<PlayerStats> stats, string sort)
        {
            var parts = sort.Split(' ');
            if (parts.Length != 2)
            {
                Console.WriteLine("Please enter a valid sort string in the format 'property direction'.");
                return stats;
            }

            var property = parts[0];
            var direction = parts[1].ToLower();

            switch (property.ToLower())
            {
                case "name":
                    stats = direction == "desc" ? stats.OrderByDescending(s => s.Name).ToList() : stats.OrderBy(s => s.Name).ToList();
                    break;
                case "team":
                    stats = direction == "desc" ? stats.OrderByDescending(s => s.Team).ToList() : stats.OrderBy(s => s.Team).ToList();
                    break;
                case "pos":
                    stats = direction == "desc" ? stats.OrderByDescending(s => s.Pos).ToList() : stats.OrderBy(s => s.Pos).ToList();
                    break;
                case "gp":
                    stats = direction == "desc" ? stats.OrderByDescending(s => s.GP).ToList() : stats.OrderBy(s => s.GP).ToList();
                    break;
                case "g":
                    stats = direction == "desc" ? stats.OrderByDescending(s => s.G).ToList() : stats.OrderBy(s => s.G).ToList();
                    break;
                case "a":
                    stats = direction == "desc" ? stats.OrderByDescending(s => s.A).ToList() : stats.OrderBy(s => s.A).ToList();
                    break;
                case "p":
                    stats = direction == "desc" ? stats.OrderByDescending(s => s.P).ToList() : stats.OrderBy(s => s.P).ToList();
                    break;
                case "plusminus":
                case "+/-":
                    stats = direction == "desc" ? stats.OrderByDescending(s => s.PlusMinus).ToList() : stats.OrderBy(s => s.PlusMinus).ToList();
                    break;
                case "pim":
                    stats = direction == "desc" ? stats.OrderByDescending(s => s.PIM).ToList() : stats.OrderBy(s => s.PIM).ToList();
                    break;
                case "pgp":
                    stats = direction == "desc" ? stats.OrderByDescending(s => s.PGP).ToList() : stats.OrderBy(s => s.PGP).ToList();
                    break;
                case "ppg":
                    stats = direction == "desc" ? stats.OrderByDescending(s => s.PPG).ToList() : stats.OrderBy(s => s.PPG).ToList();
                    break;
                case "ppp":
                    stats = direction == "desc" ? stats.OrderByDescending(s => s.PPP).ToList() : stats.OrderBy(s => s.PPP).ToList();
                    break;
                case "shg":
                    stats = direction == "desc" ? stats.OrderByDescending(s => s.SHG).ToList() : stats.OrderBy(s => s.SHG).ToList();
                    break;
                case "shp":
                    stats = direction == "desc" ? stats.OrderByDescending(s => s.SHP).ToList() : stats.OrderBy(s => s.SHP).ToList();
                    break;
                case "gwg":
                    stats = direction == "desc" ? stats.OrderByDescending(s => s.GWG).ToList() : stats.OrderBy(s => s.GWG).ToList();
                    break;
                case "otg":
                    stats = direction == "desc" ? stats.OrderByDescending(s => s.OTG).ToList() : stats.OrderBy(s => s.OTG).ToList();
                    break;
                case "s":
                    stats = direction == "desc" ? stats.OrderByDescending(s => s.S).ToList() : stats.OrderBy(s => s.S).ToList();
                    break;
                case "spercent":
                case "s%":
                    stats = direction == "desc" ? stats.OrderByDescending(s => s.Spercent).ToList() : stats.OrderBy(s => s.Spercent).ToList();
                    break;
                case "toiperpg":
                case "toi/gp":
                    stats = direction == "desc" ? stats.OrderByDescending(s => s.TOIperGP).ToList() : stats.OrderBy(s => s.TOIperGP).ToList();
                    break;
                case "shiftspergp":
                case "shifts/gp":
                    stats = direction == "desc" ? stats.OrderByDescending(s => s.ShiftsPerGP).ToList() : stats.OrderBy(s => s.ShiftsPerGP).ToList();
                    break;
                case "fowpercent":
                case "fow%":
                    stats = direction == "desc" ? stats.OrderByDescending(s => s.FOWpercent).ToList() : stats.OrderBy(s => s.FOWpercent).ToList();
                    break;
                default:
                    Console.WriteLine("Please enter a valid column name.");
                    break;
            }
            return stats;
        }
    }
}

