using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleships;
using BattleshipBot;
using Battleships.Player.Interface;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //findnewShipDistribution.Run();
            //findnewShipDistribution.Run2();
            // hmmm.WeightedRandomShipPositonTest();
            deleteMe.Run();
        }

    }

    class deleteMe
    {
        public static void Run()
        {
            /*
            var map = new Map();
            var r = new Random();
            var eSR = new EnemyShipRecord();
            var tl = new TargeterLearn(map,r,eSR);
            var oldMap = new Map();
            for (int i = 0; i < 100; i++)
            {
                oldMap.shotFired(true, 3, 4);
                oldMap.shotFired(false, 5, 5);
                eSR.addMap(oldMap);
            }
            
            var test = tl.GetNextTarget(0, 0);
            */
            /*
            METest test = new METest();
            test.GetShipPositions();
            test.GetShipPositions();
            test.GetShipPositions();
            test.GetShipPositions();
            test.GetShipPositions();
            test.GetShipPositions();
            test.GetShipPositions();

            var ts = new TargeterSnipe(new Map(), new Random(), new EnemyShipRecord());
            ts.findNewShipM(5);
            */
            /*
            var map = new Map();
            EnemyShipRecord ESR = new EnemyShipRecord();
            var clusterBomb = new TargeterClusterBomb(map, new Random(), ESR);
            var x = clusterBomb.GetNextTarget(0,0);
            map.shotFired(true, x[0], x[1]);
            x = clusterBomb.GetNextTarget(x[0], x[1]);
            map.shotFired(true, x[0], x[1]);
            x = clusterBomb.GetNextTarget(x[0], x[1]);
            map.shotFired(false, x[0], x[1]);
            x = clusterBomb.GetNextTarget(x[0], x[1]);
            map.shotFired(false, x[0], x[1]);
            x = clusterBomb.GetNextTarget(x[0], x[1]);
            x = clusterBomb.GetNextTarget(x[0], x[1]);
            x = clusterBomb.GetNextTarget(x[0], x[1]);
            x = clusterBomb.GetNextTarget(x[0], x[1]);
            x = clusterBomb.GetNextTarget(x[0], x[1]);
            x = clusterBomb.GetNextTarget(x[0], x[1]);
            x = clusterBomb.GetNextTarget(x[0], x[1]);
            x = clusterBomb.GetNextTarget(x[0], x[1]);
            x = clusterBomb.GetNextTarget(x[0], x[1]);
            x = clusterBomb.GetNextTarget(x[0], x[1]);

            x = clusterBomb.GetNextTarget(x[0], x[1]);
            x = clusterBomb.GetNextTarget(x[0], x[1]);
            */
            /*
            MoreUniformConfigs asdf = new MoreUniformConfigs();
            var map = new Map();
            var lSP = new LegalShipPositioner(map, 5);
            map.shotFired(true,2,2);
            
            var x = asdf.GetSpaceValue(5, lSP.getLegalPositions());
            */

            METest test = new METest();
            
            var x = test.GetShipPositions();
            x= test.GetShipPositions();
            x= test.GetShipPositions();
            x = test.GetShipPositions();
            x = test.GetShipPositions();
            x = test.GetShipPositions();
            x = test.GetShipPositions();
        }
    }
    class hmmm
    {
        public static void Run()
        {
            METest bot = new METest();
            bot.GetShipPositions();
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    bot.HandleOpponentsShot(new GridSquare('E', j));
                    bot.HandleOpponentsShot(new GridSquare('B', j));
                    bot.HandleOpponentsShot(new GridSquare('G', j));
                }
            }
            foreach(ShipPosition sP in bot.GetShipPositions())
            {
                Console.Write(sP.StartingSquare.Row);
                Console.Write(",");
                Console.WriteLine(sP.StartingSquare.Column);
                Console.Write(sP.EndingSquare.Row);
                Console.Write(",");
                Console.WriteLine(sP.EndingSquare.Column);
                Console.WriteLine("");

            }

            Console.WriteLine("");
            Console.WriteLine("");

            for (int i = 0; i < 10000; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    bot.HandleOpponentsShot(new GridSquare('A', j));
                    bot.HandleOpponentsShot(new GridSquare('C', j));
                    bot.HandleOpponentsShot(new GridSquare('D', j));
                    bot.HandleOpponentsShot(new GridSquare('H', j));
                    bot.HandleOpponentsShot(new GridSquare('I', j));
                }
            }
            foreach (ShipPosition sP in bot.GetShipPositions())
            {
                Console.Write(sP.StartingSquare.Row);
                Console.Write(",");
                Console.WriteLine(sP.StartingSquare.Column);
                Console.Write(sP.EndingSquare.Row);
                Console.Write(",");
                Console.WriteLine(sP.EndingSquare.Column);
                Console.WriteLine("");

            }

        }
        public static void Run2()
        {
            Coordinate c = new Coordinate(0, 0, 0);
            Ship ship1 = new Ship(c, 2);
            Ship ship2 = new Ship(c, 3);
            Console.WriteLine(ship1.CompareTo(ship2));
            
        }

        public static void Run3()
        {
            Map map = new Map();
            LegalShipPositioner LSP = new LegalShipPositioner(map,5);
            ConsolePrinter.printIntMatrix(LSP.GetNumberOfConfigurationWithAShipOnSpaces());
            map.addShip(new Coordinate(3, 3, 1), 3);
            LSP = new LegalShipPositioner(map, 4);
            ConsolePrinter.printIntMatrix(LSP.GetNumberOfConfigurationWithAShipOnSpaces());
        }

        public static void Run4()
        {
            Map map = new Map();
            Targeter T = new Targeter(map, new Random());
            Console.Write(T.findNewShipM(5)[0]);
            Console.Write(",");
            Console.WriteLine(T.findNewShipM(5)[1]);
            map.shotFired(false, 4, 4);
            map.shotFired(false, 5, 5);
            LegalShipPositioner LSP = new LegalShipPositioner(map, 5);
            ConsolePrinter.printIntMatrix(LSP.GetNumberOfConfigurationWithAShipOnSpaces());
            map.addShip(new Coordinate(3, 3, 1), 3);
            LSP = new LegalShipPositioner(map, 4);
            ConsolePrinter.printIntMatrix(LSP.GetNumberOfConfigurationWithAShipOnSpaces());
            Console.Write(T.findNewShipM(5)[0]);
            Console.Write(",");
            Console.WriteLine(T.findNewShipM(5)[1]);
        }

        public static void WeightedRandomShipPositonTest()
        {
            
            ShipPositioner SP = new ShipPositioner();
            Random r = new Random();
            EnemyMap map = new EnemyMap();
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    map.enemyShot(false,new Vector2(4,j));
                    map.enemyShot(false, new Vector2(j, 4));
                }
            }
            
            SP.placeShipRandomlyToAvoidShots(5, r, map);
            
        }
        public static void UtilityTest()
        {
            int[,,] ints = new int[3, 3, 3];
            ints[0, 1, 2] = 1;
            Random r = new Random();
            Coordinate c = UtilityFunctions.getWeightedRandomTrueCoordinate(ints, r);
            Console.WriteLine(c.GetRow() == 0 && c.GetColumn() == 1 && c.GetOrientation() == 2);
            ints[0, 0, 0] = 5000;
            c = UtilityFunctions.getWeightedRandomTrueCoordinate(ints, r);
            Console.WriteLine(c.GetRow() == 2 && c.GetColumn() == 2 && c.GetOrientation() == 2);
        }

        public static void anotherUselessTest()
        {
            double[,,] doubles = new double[3, 3, 3];
            doubles[0, 1, 2] = 500;
            Random r = new Random();
            Coordinate c = UtilityFunctions.GetInverseWeightedRandomCoordinate(doubles, r);
            Console.WriteLine(c.GetRow() == 0 && c.GetColumn() == 1 && c.GetOrientation() == 2);
            doubles[2, 2, 2] = 1;
            c = UtilityFunctions.GetInverseWeightedRandomCoordinate(doubles, r);
            Console.WriteLine(c.GetRow() == 2 && c.GetColumn() == 2 && c.GetOrientation() == 2);
        }

    }
    class findnewShipDistribution
    {
        public static void Run2()
        {
            Map map = new Map();
            map.addShip(new Coordinate(3, 3, 0), 3);

            Random r = new Random();
            Targeter T = new Targeter(map, r);
            int[,,] distribution = new int[10, 10,2];
            for (int k = 0; k < 10000; k++)
            {
                
                        Coordinate c = T.getRandomTargetCoordinate(2);
                        distribution[c.GetRow(), c.GetColumn(), c.GetOrientation()]++;
                   
            }
            for (int k = 0; k < 2; k++)
            {


                for (int i = 0; i < 10; i++)
                {

                    for (int j = 0; j < 10; j++)
                    {
                        Console.Write(distribution[i, j, k]);
                        Console.Write("\t");
                    }
                    Console.WriteLine();
                    
                }
                Console.WriteLine();
                Console.WriteLine();

            }

        }


        public static void Run()
        {
            Map map = new Map();
            map.addShip(new Coordinate(3,3,0),3);
            
            Random r = new Random();
            Targeter T = new Targeter(map, r);
            int[,] distribution = new int[10, 10];
            for (int i = 0; i < 10000; i++)
            {
                int[] theInt = T.findNewShip(4);
                int row = theInt[0];
                int column = theInt[1];
                distribution[row, column]++;
            }

            for (int i = 0; i < 10; i++)
            {
                
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(distribution[i,j]);
                    Console.Write("\t");
                }
                Console.WriteLine();
            }
            
        }

    }
    
    class ShipTargetTest
    {
        public static void Run()
        {
            Map map = new Map();

            map.shotFired(true, 9, 4);
            map.shotFired(true, 8, 4);
            map.shotFired(true, 7, 4);
            map.shotFired(false, 6, 4);
            map.shotFired(false, 9, 5);
            map.shotFired(false, 9, 3);

            ShipTarget shipTarget = new ShipTarget(map, 9, 4);
            ShipTargeter ST = new ShipTargeter(map,shipTarget);

            for (int i = 0; i < 5; i++)
            {
                int[] T = ST.GetNextShot().ToArray();
                Console.Write(T[0]);
                Console.Write(",");
                Console.WriteLine(T[1]);
            }
            ST.GetNextShot();
            System.Diagnostics.Trace.WriteLine(map.GetOccupiedSpaces()[7, 4]);

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(map.GetOccupiedSpaces()[5 + i, 4]);

            }

            Console.WriteLine(map.GetOccupiedSpaces()[6, 4]);

        }
    }


    class TestingStuff
    {
        public static void Run()
        {
            Map map = new Map();
            Random r = new Random();
            Targeter Targeter = new Targeter(map,r);
            int[] target = new int[] { 0, 0 };
            Console.WriteLine(target[1]);


            while (map.GetShips().Count() < 2)
            {
                target = Targeter.GetNextTarget(target[0], target[1]);
                bool hit;
                if (HitShip(target))
                {
                    hit = true;
                }
                else { hit = false; }
                Console.Write(target[0]);
                Console.Write(",");
                Console.WriteLine(target[1]);
                Console.WriteLine(map.GetShips().Count());
                map.shotFired(hit, target[0], target[1]);

            }

            bool[,] b = map.GetOccupiedSpaces();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (b[i, j])
                    { Console.Write("X"); }
                    else { Console.Write("O"); }
                }
                Console.WriteLine("");
            }

            Console.WriteLine("");
            int[,] hitSpaces = map.GetHitSpaces();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (hitSpaces[i, j] == 2)
                    { Console.Write("X"); }
                    else if (hitSpaces[i, j] == 1)
                    { Console.Write("O"); }
                    else
                    { Console.Write("."); }

                }
                Console.WriteLine("");
            }


        }
        private static bool HitShip(int[] fired)
        {
            for (int i = 0; i < 5; i++)
            {
                if (fired[0] == i && fired[1] == 6)
                {
                    return true;
                }
            }
            for (int i = 8; i < 10; i++)
            {
                if (fired[0] == i && fired[1] == 6)
                {
                    return true;
                }
            }
            return false;
        }

        

    }
    class LegalPositionTesting
    {
        public static void Run()
        {
            int shipLength;
            Map map = new Map();
            map.addShip(new Coordinate(4, 4, 1), 4);
            
            LegalShipPositioner LSP = new LegalShipPositioner(map,2);
            bool[,,] legalPos = LSP.getLegalPositions();
            for (int k = 0; k < 2; k++)
            {


                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (legalPos[i,j,k])
                        {
                            Console.Write(1);
                        }
                        else { Console.Write(0); }
                        
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();

            }
            
        }
    }

    public class ConsolePrinter
    {
        public static void printBoolCoordinates(bool[,,] bools)
        {
            for (int k = 0; k < 2; k++)
            {


                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (bools[i, j, k])
                        {
                            Console.Write(1);
                        }
                        else { Console.Write(0); }

                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        public static void printIntMatrix(int[,] ints)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(ints[i,j]);
                    Console.Write("\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

        }

    }
}

