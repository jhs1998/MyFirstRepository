using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace consoleproject1
{
    internal class Program
    {

        public struct GameData
        {
            public bool running;
            public bool surrender;
            public bool keyhave;
            public bool[,] map;
            public ConsoleKey inputKey;
            public Point playerPos;
            public Point goalPos;
            public Point enemyPos1;
            public Point enemyPos2;
            public Point key;
            public Point wall;
            public Point wallkey;
        }

        public struct Point
        {
            public int x;
            public int y;
        }

        static GameData data;

        static void Main(string[] args)
        {
            Start();

            while (data.running)
            {
                Render();
                Input();
                Update();             
            }
            if (data.surrender == false)
            {
                Endless();
            }
            else if (data.surrender == true && data.running == false)
            {
                End();
            }
        }

        static void Start()
        {
            Console.CursorVisible = false;

            data = new GameData();

            data.running = true;
            data.keyhave = false;

            data.map = new bool[,]
            {
                { false, false, false, false, false, false, false, false, false },
                { false,  true, false,  true,  true,  true, false,  true, false },
                { false,  true, false, false, false, false, false,  true, false },
                { false,  true,  true,  true,  true,  true,  true,  true, false },
                { false,  true, false,  true, false, false, false, false, false },
                { false,  true, false,  true,  true,  true,  true,  true, false },
                { false, false, false, false, false, false, false, false, false },
            };
            data.playerPos = new Point() { x = 1, y = 1 };
            data.goalPos = new Point() { x = 3, y = 1 };
            data.enemyPos1 = new Point() { x = 7, y = 1 };
            data.enemyPos2 = new Point() { x = 7, y = 5 };
            data.key = new Point() { x = 1, y = 5 };
            data.wall = new Point() { x = 5, y = 2 };
            data.wallkey = new Point() { x = 3, y = 5 };

            Console.Clear();
            Console.WriteLine("====================================");
            Console.WriteLine("=                                  =");
            Console.WriteLine("=           미로 찾기!             =");
            Console.WriteLine("=                                  =");
            Console.WriteLine("====================================");
            Console.WriteLine();
            Console.WriteLine("    계속하려면 아무키나 누르세요    ");
            Console.ReadKey();
        }


        static void Endless()
        {
            Console.Clear();
            Console.WriteLine("====================================");
            Console.WriteLine("=                                  =");
            Console.WriteLine("=             게임 오버             =");
            Console.WriteLine("=                                  =");
            Console.WriteLine("====================================");
            Console.WriteLine();
        }

        static void End()
        {
            Console.Clear();
            Console.WriteLine("====================================");
            Console.WriteLine("=                                  =");
            Console.WriteLine("=           게임 클리어!           =");
            Console.WriteLine("=                                  =");
            Console.WriteLine("====================================");
            Console.WriteLine();
        }

        static void Render()
        {
            Console.Clear();

            PrintMap();
            PrintPlayer();
            PrintGoal();
            PrintEnemy();
            PrintKey();
            PrintWallKey();
        }

        static void Input()
        {
            data.inputKey = Console.ReadKey(true).Key;
        }

        static void Update()
        {
            Move();
            KeyGet();
            WallKeyGet();
            CheckGameClear();
            GameOver();
        }

        static void PrintMap()
        {
            for (int y = 0; y < data.map.GetLength(0); y++)
            {
                for (int x = 0; x < data.map.GetLength(1); x++)
                {
                    if (data.map[y, x])
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("#");
                    }
                }
                Console.WriteLine();
            }
        }
        //유닛 생성
        static void PrintPlayer()
        {
            Console.SetCursorPosition(data.playerPos.x, data.playerPos.y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("O");
            Console.ResetColor();
        }
        //벽키 생성
        static void PrintWallKey()
        {
            Console.SetCursorPosition(data.wallkey.x, data.wallkey.y);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("W");
            Console.ResetColor();
        }
        //골 생성 키를 먹기 전에는 상호작용 불가
        static void PrintGoal()
        {
            Console.SetCursorPosition(data.goalPos.x, data.goalPos.y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("G");
            Console.ResetColor();
        }
        //적 생성
        static void PrintEnemy()
        {
            Console.SetCursorPosition(data.enemyPos1.x, data.enemyPos1.y);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("E");
            Console.ResetColor();
            Console.SetCursorPosition(data.enemyPos2.x, data.enemyPos2.y);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("E");
            Console.ResetColor();
        }
        // 키 생성 키를 먹을경우 키가 화면에서 사라짐
        static void PrintKey()
        {
            Console.SetCursorPosition(data.key.x, data.key.y);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("K");
            Console.ResetColor();
            if (data.keyhave == true)
            {
                Console.SetCursorPosition(data.key.x, data.key.y);
                Console.Write(" ");
            }          
        }

        static void Move()
        {
            switch (data.inputKey)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    MoveUp();
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    MoveDown();
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    MoveLeft();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    MoveRight();
                    break;
            }
        }
        //키를 먹으면 키값 트루
        static void KeyGet()
        {
            if (data.playerPos.x == data.key.x &&
                data.playerPos.y == data.key.y)
            {
                data.keyhave = true;
            }
        }

        //벽키를 누를시 닫혀있던 벽이 열림
        static void WallKeyGet()
        {
            if (data.playerPos.x == data.wallkey.x &&
                data.playerPos.y == data.wallkey.y)
            {                
                data.map[2, 5] = true;
            }
        }
        // 키를 먹을경우에만 골을 들어갈수 있음
        static void CheckGameClear()
        {
            if (data.playerPos.x == data.goalPos.x &&
                data.playerPos.y == data.goalPos.y &&
                data.keyhave == true)
            {
                data.running = false;
                data.surrender = true;
            }
        }
        // 적과 조우할경우 게임오버 화면이 뜸
        static void GameOver()
        {
            if (data.playerPos.x == data.enemyPos1.x &&
                data.playerPos.y == data.enemyPos1.y)
            {
                data.surrender = false;
                data.running = false;
            }
            else if (data.playerPos.x == data.enemyPos2.x &&
                data.playerPos.y == data.enemyPos2.y)
            {
                data.surrender = false;
                data.running = false;
            }
        }

        static void MoveUp()
        {
            Point next = new Point() { x = data.playerPos.x, y = data.playerPos.y - 1 };
            if (data.map[next.y, next.x])
            {
                data.playerPos = next;
            }
        }

        static void MoveDown()
        {
            Point next = new Point() { x = data.playerPos.x, y = data.playerPos.y + 1 };
            if (data.map[next.y, next.x])
            {
                data.playerPos = next;
            }
        }

        static void MoveLeft()
        {
            Point next = new Point() { x = data.playerPos.x - 1, y = data.playerPos.y };
            if (data.map[next.y, next.x])
            {
                data.playerPos = next;
            }
        }

        static void MoveRight()
        {
            Point next = new Point() { x = data.playerPos.x + 1, y = data.playerPos.y };
            if (data.map[next.y, next.x])
            {
                data.playerPos = next;
            }
        }
    }
}
