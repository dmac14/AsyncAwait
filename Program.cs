using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

//Dictionary Definition:
/* 
 * Of or requiring a form of computer control timing protocol 
 * in which a specific operation begins upon receipt of an 
 * indication (signal) that the preceding operation has been completed.
 */



namespace AsyncAwait
{
    internal class Program
    {
        private static string ClassLevelString { get; set; }

        private static void Main(string[] args)
        {
            #region blah blah blah
            var keepGoing = "y";

            while (keepGoing == "y")
            {
                Console.WriteLine("Which example would you like to do? (Type the number and press enter)");
                Console.WriteLine("1. Understanding Async Ideology");
                Console.WriteLine("2. Asnyc and Threads");
                Console.WriteLine("3. Understanding Async with a Delay");
                Console.WriteLine("4. Async Void: Functionality");
                Console.WriteLine("5. Async Void: Race Conditions");
                Console.WriteLine("6. Async Void: Exceptions");
                Console.WriteLine("7. Debugging Async Code");
                Console.WriteLine("8. More Debugging Async Code");
                
                var selection = Console.ReadLine();

                if (selection == "1")
                {
                    Example1();
                }
                else if (selection == "2")
                {
                    Example2();
                }
                else if (selection == "3")
                {
                    Example3();
                }
                else if (selection == "4")
                {
                    Example4();
                }
                else if (selection == "5")
                {
                    Example5();
                }
                else if (selection == "6")
                {
                    Example6();
                }
                else if (selection == "7")
                {
                    Example7();
                }
                else if (selection == "8")
                {
                    Example8();
                }

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Would you like to do another example? (Y/N)");
                var choice = Console.ReadLine();
                keepGoing = choice == null ? "n" : choice.ToLower();
                Console.Clear();

            }
            #endregion blah blah blah
        }

        #region Example 1
        // Understanding Async Ideology
        private static async void Example1()
        {
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Hey, diddle, diddle...");
            await CallWrite1();
            Console.WriteLine("The little dog laughted...");
        }

        private static async Task CallWrite1()
        {
            await Write1();
            Console.WriteLine("The cow jumped over the moon...");
        }

        private static async Task Write1()
        {
            // Synchronous
            Console.WriteLine("The cat and the fiddle...");
        }

        #endregion Example 1

        #region Example 2
        // Async and Threads
        /*
         * When an await is hit, the code attempts to capture the Synchronization Context or the TaskScheduler.
         * Console Apps have neither.
         * Thus we have no control over which thread the continuation is run in.
         * Could run "anywhere".... :D
         */


        private static async Task Example2()
        {
            // Credit to Stephen Toub for the code (blogs.msdn.com - Await, SynchronizationContext, and Console Apps)
            var dict = new Dictionary<int, int>();

            for (int i = 0; i < 10000; i++)
            {
                int id = Thread.CurrentThread.ManagedThreadId;
                int count;
                dict[id] = dict.TryGetValue(id, out count) ? count + 1 : 1;

                await Task.Yield();
            }

            foreach (var pair in dict)
            {
                Console.WriteLine(pair);
            }
        }

        #endregion Example 2

        // UI and Async Threads

        // Deadlocks        

        #region Example 3
        // Async with a delay
        private static async Task Example3()
        {
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Beginning Example 3!");
            await MakeCalls3();
            Console.WriteLine("Example 3 Complete!");
        }

        private static async Task MakeCalls3()
        {
            for (int j = 0; j < 1000; j++)
            {
                await WritePlusSign3();
            }
        }

        private static async Task WritePlusSign3()
        {
            // Asynchronous, return an uncompleted task
            await Task.Delay(1);
            Console.Write('+');
        }

        #endregion Example 3        

        #region Example 4
        // Async Void: Functionality (NO AWAIT)
        private static async Task Example4()
        {
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Beginning Example 4!");
            await MakeCalls4();
            Console.WriteLine("Example 4 Complete!");
        }

        private static async Task MakeCalls4()
        {for (int i = 0; i < 1000; i++)
            {
                WritePlusSign4();
            }
        }

        private static async void WritePlusSign4()
        {
            // Asynchronous
            await Task.Delay(1);
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Console.Write('+');
        }

        #endregion Example 4

        #region Example 5
        // Async Void: Race Conditions
        private static async Task Example5()
        {
            Console.WriteLine("Beginning Example 5!");
            try
            {
                await AsyncMethod5();
                await Task.Delay(2000);
                Console.WriteLine("The result is: " + ClassLevelString);
                await Task.Delay(2000);
                Console.WriteLine("The result2 is: " + ClassLevelString);
            }
            catch (Exception ex)
            {
                var x = ex;
            }
            Console.WriteLine("Example 5 Finished!");
        }

        private static async Task AsyncMethod5()
        {
            await Task.Delay(3000);
            ClassLevelString = "WOWZERS";
        }

        #endregion Example 5

        #region Example 6
        // Async Void: Exceptions
        private static async Task Example6()
        {
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Beginning Example 6!");
            await MakeCalls6();
            Console.WriteLine("Example 6 Complete!");
        }

        private static async Task MakeCalls6()
        {

            for (int i = 0; i < 100; i++)
            {
                try
                {
                    await WritePlusSign6();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception Caught!");
                }
                
            }
        }

        private static async Task WritePlusSign6()
        {
            // Asynchronous
            await Task.Delay(1);
            Console.Write('+');
            throw new Exception();
        }

        #endregion Example 6

        #region Example 7
        // Debugging async: Threads and Call Stack
        private static async Task Example7()
        {
            Console.WriteLine("Beginning Example 7!");
            await DoWork(10);
            Console.WriteLine("Example 7 Finished!");
        }

        private static async Task DoWork(int x)
        {
            await Task.Yield();
            int i = x*x;
            await GetFile(i);
        }

        private static async Task GetFile(int i)
        {
            await Task.Yield();
            for (int k = 0; k < 20; k++)
            {
                var file = PretendToReadText();
            }
        }

        private static async Task<string> PretendToReadText()
        {
            await Task.Delay(200);
            var id = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("(1 : "+id+")");
            Console.WriteLine("(2 : "+id+")");
            Console.WriteLine("(3 : "+id+")");
            Console.WriteLine("(4 : "+id+")");
            Console.WriteLine("(5 : "+id+")");
            Console.WriteLine("(6 : "+id+")");
            Console.WriteLine("(7 : "+id+")");
            Console.WriteLine("(8 : "+id+")");
            Console.WriteLine("(9 : "+id+")");
            return "COMPLETE!";
        }

        #endregion Example 7

        #region Example 8
        // More async debugging: Task menu?
        private static async Task Example8()
        {
            Console.WriteLine("Starting Example 8!");

            for (int i = 0; i < 22000; i++)
            {
                StartDelay8();
            }

            Console.WriteLine("Example 8 Complete!");
        }

        private static async Task StartDelay8()
        {
            await Task.Run(async () =>
            {
                await Task.Delay(5000);
            });
        }

        #endregion

        #region Example 9
        // Redo example 3 with a butt ton of break points, F5 and F10/F11 diffs
        #endregion Example 9


    }
}