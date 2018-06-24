using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using System;
using System.Linq;

namespace Uppgift_3._2
{
    public enum Mode
    {
        Astar, BFS, DFS, Dijkstras
    }

    public class Game1 : Game
    {
        int[] xDir = new int[] { 1, -1, 0, 0 };
        int[] yDir = new int[] { 0, 0, 1, -1 };
        bool finished;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Level level = new Level();
        Astar pathFinder;

        Mode current = Mode.DFS;

        Queue<Vector2> wayPoints;
        Vector2 source, end, destination;
        List<Vector2> nodes, prev, astarNodes;
        int[,] dist;

        bool first = true;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.LoadContent(Content);

            Init();
        }
        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.F1))
            {
                current = Mode.Astar;
                Init();
            }
            if (state.IsKeyDown(Keys.F2))
            {
                current = Mode.BFS;
                Init();
            }
            if (state.IsKeyDown(Keys.F3))
            {
                current = Mode.DFS;
                Init();
            }
            if (state.IsKeyDown(Keys.F4))
            {
                current = Mode.Dijkstras;
                Init();
            }
            base.Update(gameTime);
        }
        void Init()
        {
            first = true;
            finished = false;
            pathFinder = new Astar(level);
            wayPoints = new Queue<Vector2>();
            astarNodes = new List<Vector2>();
            nodes = new List<Vector2>();
            prev = new List<Vector2>();
            source = new Vector2(1, 1);
            destination = new Vector2(1, 1);
            end = new Vector2(8, 8);
            wayPoints.Enqueue(source);
            dist = level.dist;

            prev.Clear();

            for (int i = 1; i < level.Height; i++)
            {
                for (int j = 1; j < level.Width; j++)
                {
                    level.vis[i, j] = false;
                    level.dist[i, j] = 100;
                    nodes.Add(new Vector2(i, j));
                    prev.Add(Vector2.Zero);
                }
            }
            dist[(int)source.X, (int)source.Y] = 0;
        }
        Vector2 NewDestination(Vector2 source, int i)
        {
            if (source.X == 11 || source.Y == 11)
            {
                if (source.X == 11 && source.Y == 11)
                {
                    return new Vector2(source.X, (int)source.Y);
                }
                else if (source.Y == 11)
                {
                    return new Vector2(source.X + xDir[i], (int)source.Y);
                }
                else
                {
                    return new Vector2(source.X, (int)source.Y + yDir[i]);
                }
            }
            else
            {
                return new Vector2(source.X + xDir[i], (int)source.Y + yDir[i]);
            }
        }
        void Dijkstra()
        {
            //nodes = new List<Vector2>();
            //prev = new List<Vector2>();
            //dist = level.dist;

            //for (int i = 1; i < level.Height; i++)
            //{
            //    for (int j = 1; j < level.Width; j++)
            //    {
            //        nodes.Add(new Vector2(i, j));
            //        prev.Add(Vector2.Zero);
            //    }
            //}
            //dist[(int)source.X, (int)source.Y] = 0;

            //while(nodes.Count > 0)
            if (nodes.Count > 0)
            {
                nodes = Enumerable.ToList(nodes.OrderBy(Nodes => level.dist[(int)Nodes.X, (int)Nodes.Y]));
                source = nodes.First();

                nodes.RemoveAt(0);

                for (int i = 0; i < 4; i++)
                {
                    destination = NewDestination(source, i);
                    if (level.layout[(int)destination.X, (int)destination.Y] != 1)
                    {
                        int alt = level.layout[(int)source.X, (int)source.Y] + dist[(int)source.X, (int)source.Y];

                        if (alt < dist[(int)destination.X, (int)destination.Y])
                        {
                            spriteBatch.Draw(Globals.tex, new Vector2(destination.X * 50, destination.Y * 50), Color.Red);
                            dist[(int)destination.X, (int)destination.Y] = alt;
                            prev.Add(source);

                            if (destination == end)
                            {
                                finished = true;
                            }
                        }
                    }
                }
            }
        }
        void DFS(Vector2 source)
        {
            level.vis[(int)source.X, (int)source.Y] = true;
            if (finished)
            {
                return;
            }

            for (int i = 0; i < 4; i++)
            {
                destination = NewDestination(source, i);
                if (level.layout[(int)destination.X, (int)destination.Y] != 1 && level.vis[(int)destination.X, (int)destination.Y] == false)
                {
                    spriteBatch.Draw(Globals.tex, new Vector2(source.X * 50, source.Y * 50), Color.Red);
                    if (destination == end)
                    {
                        finished = true;
                    }
                    else
                    {
                        DFS(destination);
                    }

                    if (finished)
                    {
                        return;
                    }
                }
            }
            spriteBatch.Draw(Globals.tex, new Vector2(source.X * 50, source.Y * 50), Color.Blue);
            return;
        }
        void BFS()
        {
            //wayPoints = new Queue<Vector2>();
            //wayPoints.Enqueue(source);

            //for (int i = 1; i < level.Height; i++)
            //{
            //    for (int j = 1; j < level.Width; j++)
            //    {
            //        level.dist[i, j] = 100;
            //    }
            //}

            //while (wayPoints.Count > 0)
            if (wayPoints.Count > 0)
            {
                source = wayPoints.Dequeue();

                for (int i = 0; i < 4; i++)
                {
                    destination = NewDestination(source, i);
                    if (level.layout[(int)destination.X, (int)destination.Y] != 1 && level.dist[(int)destination.X, (int)destination.Y] == 100)
                    {
                        level.dist[(int)destination.X, (int)destination.Y] = level.dist[(int)source.X, (int)source.Y] + 1;
                        spriteBatch.Draw(Globals.tex, new Vector2(destination.X * 50, destination.Y * 50), Color.Red);

                        if (destination == end)
                        {
                            finished = true;
                            return;
                        }
                        else
                        {
                            wayPoints.Enqueue(destination);
                        }
                    }
                }
            }
        }
        void Astar()
        {
            wayPoints = pathFinder.FindPath(ref astarNodes, new Point((int)source.X, (int)source.Y), new Point((int)end.X, (int)end.Y));
            foreach (Vector2 v in astarNodes)
            {
                spriteBatch.Draw(Globals.tex, new Vector2(v.X, v.Y), Color.Blue);
            }
            foreach (Vector2 v in wayPoints)
            {
                spriteBatch.Draw(Globals.tex, new Vector2(v.X, v.Y), Color.Red);
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (first)
            {
                level.Draw(spriteBatch);
                first = false;
            }

            if (!finished)
            {
                switch (current)
                {
                    case Mode.Astar:
                        Astar();
                        break;
                    case Mode.BFS:
                        BFS();
                        break;
                    case Mode.DFS:
                        DFS(source);
                        break;
                    case Mode.Dijkstras:
                        Dijkstra();
                        break;
                    default:
                        break;
                }
                spriteBatch.Draw(Globals.tex2, new Vector2(end.X * 50, end.Y * 50), Color.Red);
            }

            //foreach (Vector2 v in prev)
            //{
            //    spriteBatch.Draw(Globals.tex, new Vector2(v.X * 50, v.Y * 50), Color.Red);
            //}
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
