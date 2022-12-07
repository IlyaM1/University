using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
using System;
using System.Windows.Forms;

namespace Digger
{
    static class CreaturesNames
    {
        public static Type Player = new Player().GetType();
        public static Type Terrain = new Terrain().GetType();
        public static Type Sack = new Sack().GetType();
        public static Type Gold = new Gold().GetType();
        public static Type Monster = new Monster().GetType();
    }

    class Player : ICreature
    {
        public static int X;
        public static int Y;

        public Player()
        {
            if (!(Game.Map is null))
            {
                var coordinates = FindPlayerCoordinates();
                X = coordinates[0];
                Y = coordinates[1];
            }
        }

        CreatureCommand ICreature.Act(int x, int y)
        {
            var command = new CreatureCommand();

            var pressedKey = Game.KeyPressed;
            if (pressedKey == Keys.Up && y - 1 >= 0)
            {
                if (!GameCheckers.IsCreatureInCell(x, y - 1, CreaturesNames.Sack))
                {
                    command.DeltaY -= 1;
                    Y -= 1;
                }
            }
            
            if (pressedKey == Keys.Down && y + 1 < Game.MapHeight)
            {
                if (!GameCheckers.IsCreatureInCell(x, y + 1, CreaturesNames.Sack))
                {
                    command.DeltaY += 1;
                    Y += 1;
                }
            }  

            if (pressedKey == Keys.Right && x + 1 < Game.MapWidth)
            {
                if (!GameCheckers.IsCreatureInCell(x + 1, y, CreaturesNames.Sack))
                {
                    command.DeltaX += 1;
                    X += 1;
                }
            }

            if (pressedKey == Keys.Left && x - 1 >= 0)
            {
                if (!GameCheckers.IsCreatureInCell(x - 1, y, CreaturesNames.Sack))
                {
                    command.DeltaX -= 1;
                    X -= 1;
                }
            }
                
            return command;
        }

        bool ICreature.DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Sack || conflictedObject is Monster)
            {
                Game.IsOver = true;
                return true;
            }

            return false;
        }

        int ICreature.GetDrawingPriority()
        {
            return 1;
        }

        string ICreature.GetImageFileName()
        {
            return "Digger.png";
        }

        private static int[] FindPlayerCoordinates()
        {
            /// <summary>
            /// Метод для поиска координаты игрока по всему полю
            /// </summary>
            /// <returns>Возвращает координаты игрока в формате массива интов [x, y], иначе возвращает {- 1}</returns>
            for (var i = 0; i < Game.MapWidth; i++)
                for (var j = 0; j < Game.MapHeight; j++)
                    if (Game.Map[i, j] is Player)
                        return new[] { i, j };

            return new[] { -1 };
        }
    }

    class Terrain : ICreature
    {
        CreatureCommand ICreature.Act(int x, int y)
        {
            return new CreatureCommand();
        }

        bool ICreature.DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Player ? true : false;
        }

        int ICreature.GetDrawingPriority()
        {
            return 0;
        }

        string ICreature.GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    class Sack : ICreature
    {
        public int FallDistance = 0;

        CreatureCommand ICreature.Act(int x, int y)
        {
            var command = new CreatureCommand();
            
            if (y < Game.MapHeight - 1)
            {
                if (Game.Map[x, y + 1] is null 
                    || (GameCheckers.IsCreatureFromListInCell(x, y + 1, new Type[] {CreaturesNames.Player, CreaturesNames.Monster}) 
                                && FallDistance >= 1))
                {
                    command.DeltaY += 1;
                    FallDistance += 1;
                }
                else
                {
                    if (FallDistance >= 2)
                        command.TransformTo = new Gold();

                    FallDistance = 0;
                }
            }
            else
            {
                if (FallDistance >= 2)
                    command.TransformTo = new Gold();
                
                FallDistance = 0;
            }

            return command;
        }

        bool ICreature.DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        int ICreature.GetDrawingPriority()
        {
            return 1;
        }

        string ICreature.GetImageFileName()
        {
            return "Sack.png";
        }
    }

    class Gold : ICreature
    {
        CreatureCommand ICreature.Act(int x, int y)
        {
            return new CreatureCommand();
        }

        bool ICreature.DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Player)
            {
                Game.Scores += 10;
                return true;
            }
            else if (conflictedObject is Monster)
                return true;

            return false;
        }

        int ICreature.GetDrawingPriority()
        {
            return 0;
        }

        string ICreature.GetImageFileName()
        {
            return "Gold.png";
        }
    }

    class Monster : ICreature
    {
        CreatureCommand ICreature.Act(int x, int y)
        {
            var command = new CreatureCommand();

            var playerX = Player.X;
            var playerY = Player.Y;

            if (playerX < x)
            {
                if (!GameCheckers.IsCreatureFromListInCell(x - 1, y, new Type[] {CreaturesNames.Terrain, CreaturesNames.Sack, CreaturesNames.Monster}))
                    command.DeltaX -= 1;
            }
            else if (playerX > x)
            {
                if (!GameCheckers.IsCreatureFromListInCell(x + 1, y, new Type[] { CreaturesNames.Terrain, CreaturesNames.Sack, CreaturesNames.Monster }))
                    command.DeltaX += 1;
            }  
            else if (playerY < y)
            {
                if (!GameCheckers.IsCreatureFromListInCell(x, y - 1, new Type[] { CreaturesNames.Terrain, CreaturesNames.Sack, CreaturesNames.Monster }))
                    command.DeltaY -= 1;
            }
            else if (playerY > y)
            {
                if (!GameCheckers.IsCreatureFromListInCell(x, y + 1, new Type[] { CreaturesNames.Terrain, CreaturesNames.Sack, CreaturesNames.Monster }))
                    command.DeltaY += 1;
            }

            return command;
        }

        bool ICreature.DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Monster)
                return true;

            if (conflictedObject is Sack)
                if (((Sack)conflictedObject).FallDistance >= 2)
                    return true;

            return false;
        }

        int ICreature.GetDrawingPriority()
        {
            return 1;
        }

        string ICreature.GetImageFileName()
        {
            return "Monster.png";
        }
    }

    static class GameCheckers
    {
        public static bool IsCreatureInCell(int x, int y, Type creatureType)
        {
            if (Game.Map[x, y] is null)
                return false;

            return Game.Map[x, y].GetType() == creatureType;
        }

        public static bool IsCreatureFromListInCell(int x, int y, Type[] typesList)
        {
            foreach (var type in typesList)
            {
                if (GameCheckers.IsCreatureInCell(x, y, type))
                    return true;
            }
            return false;
        }
    }
}