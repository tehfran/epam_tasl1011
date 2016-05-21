using System;
using System.Linq;
using UserBase.BLContracts;

namespace UserBase.ConsoleUI
{
    internal class Program
    {
        private static IUserLogic userLogic = new UserLogic.UserLogic();
        private static IAwardLogic awardLogic = new UserLogic.AwardLogic();

        private static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1 - View user list");
                Console.WriteLine("2 - Add user");
                Console.WriteLine("3 - Delete user");
                Console.WriteLine("4 - View award list");
                Console.WriteLine("5 - Add award");
                Console.WriteLine("6 - Delete award");
                Console.WriteLine("7 - Grant award to user");
                Console.WriteLine("8 - Rescind award from user");
                Console.WriteLine("9 - View user's awards");
                Console.WriteLine("0 - View users awarded");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        ShowList();
                        break;

                    case "2":
                        AddUser();
                        break;

                    case "3":
                        DeleteUser();
                        break;

                    case "4":
                        AwardList();
                        break;

                    case "5":
                        AddAward();
                        break;

                    case "6":
                        DeleteAward();
                        break;

                    case "7":
                        GrantAward();
                        break;

                    case "8":
                        RescindAward();
                        break;

                    case "9":
                        ShowUserAwards();
                        break;

                    case "0":
                        ShowAwardUsers();
                        break;

                    default:
                        Console.WriteLine("Incorrect input");
                        break;
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        private static void ShowAwardUsers()
        {
            Console.WriteLine("Enter award id");
            string awardstring = Console.ReadLine();
            int awardid;
            while (!int.TryParse(awardstring, out awardid))
            {
                Console.WriteLine("Enter a valid integer");
                awardstring = Console.ReadLine();
            }
            try
            {
                var users = awardLogic.Users(awardid);

                var award = awardLogic.Get(awardid);

                if (users.Count > 0)
                {
                    Console.WriteLine($"Award {award.Name} has been granted to these users:");
                    foreach (var user in users)
                    {
                        Console.WriteLine($"{user.Username}");
                    }
                }
                else
                {
                    Console.WriteLine($"Award {award.Name} has not been granted to any user");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void ShowUserAwards()
        {
            Console.WriteLine("Enter user id");
            string userstring = Console.ReadLine();
            int userid;
            while (!int.TryParse(userstring, out userid))
            {
                Console.WriteLine("Enter a valid integer");
                userstring = Console.ReadLine();
            }
            try
            {
                var user = userLogic.Get(userid);
                if (user.Awards.Count > 0)
                {
                    Console.WriteLine($"User {user.Username} has the following awards:");
                    foreach (var id in user.Awards)
                    {
                        Console.WriteLine($"{awardLogic.Get(id).Name}");
                    }
                }
                else
                {
                    Console.WriteLine($"User {user.Username} has no awards");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void RescindAward()
        {
            Console.WriteLine("Enter user id");
            string user = Console.ReadLine();
            int userid;
            while (!int.TryParse(user, out userid))
            {
                Console.WriteLine("Enter a valid integer");
                user = Console.ReadLine();
            }

            Console.WriteLine("Enter award id");
            string award = Console.ReadLine();
            int awardid;
            while (!int.TryParse(award, out awardid))
            {
                Console.WriteLine("Enter a valid integer");
                award = Console.ReadLine();
            }

            try
            {
                awardLogic.Rescind(userid, awardid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine($"Successfully rescinded {awardLogic.Get(awardid).Name} from {userLogic.Get(userid).Username}");
            }
        }

        private static void DeleteAward()
        {
            Console.WriteLine("Enter award id");
            string awardstr = Console.ReadLine();
            int awardid;
            while (!int.TryParse(awardstr, out awardid))
            {
                Console.WriteLine("Enter a valid integer");
                awardstr = Console.ReadLine();
            }

            try
            {
                var awardname = awardLogic.Get(awardid).Name;
                awardLogic.Delete(awardid);
                Console.WriteLine($"Award {awardname} was deleted successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void GrantAward()
        {
            Console.WriteLine("Enter user id");
            string user = Console.ReadLine();
            int userid;
            while (!int.TryParse(user, out userid))
            {
                Console.WriteLine("Enter a valid integer");
                user = Console.ReadLine();
            }

            Console.WriteLine("Enter award id");
            string award = Console.ReadLine();
            int awardid;
            while (!int.TryParse(award, out awardid))
            {
                Console.WriteLine("Enter a valid integer");
                award = Console.ReadLine();
            }

            try
            {
                awardLogic.Grant(userid, awardid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine($"Successfully granted {awardLogic.Get(awardid).Name} to {userLogic.Get(userid).Username}");
            }
        }

        private static void AddAward()
        {
            Console.WriteLine("Enter award name");
            string award = Console.ReadLine();
            int id = awardLogic.Add(award);
            Console.WriteLine($"Award with id {id} added successfully");
        }

        private static void AwardList()
        {
            var awards = awardLogic.GetAll()
                .OrderByDescending(x => x.Id);
            if (awards.Count() > 0)
            {
                Console.WriteLine("Awards:");
                foreach (var award in awards)
                {
                    Console.WriteLine($"{award.Id}. {award.Name}");
                }
            }
            else
            {
                Console.WriteLine("There are no awards");
            }
        }

        private static void ShowList()
        {
            var users = userLogic.GetAll()
                .OrderByDescending(x => x.Id);
            if (users.Count() > 0)
            {
                Console.WriteLine("Users:");
                foreach (var user in users)
                {
                    Console.WriteLine($"{user.Id}. {user.Username}, {user.BirthDate.ToShortDateString()} ({user.Age} years)");
                }
            }
            else
            {
                Console.WriteLine("There are no users");
            }
        }

        private static void AddUser()
        {
            Console.WriteLine("Enter username");
            string name = Console.ReadLine();
            Console.WriteLine("Enter birth date");
            string date = Console.ReadLine();
            DateTime stamp = new DateTime();
            while (!DateTime.TryParse(date, out stamp))
            {
                Console.WriteLine("Birth date is not valid");
                date = Console.ReadLine();
            }
            int id = userLogic.Add(name, stamp.Date);
            Console.WriteLine($"User with id {id} was added successfully");
        }

        private static void DeleteUser()
        {
            Console.WriteLine("Enter ID");
            string textid = Console.ReadLine();
            int id;
            while (!int.TryParse(textid, out id))
            {
                Console.WriteLine("ID must be a valid integer");
                textid = Console.ReadLine();
            }
            try
            {
                userLogic.Delete(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine($"User with id {id} was deleted successfully");
            }
        }
    }
}