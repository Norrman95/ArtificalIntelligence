using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uppgift_3._2
{
    class Astar
    {
        private SearchNode[,] searchNodes;
        private int levelWidth;
        private int levelHeight;
        private List<SearchNode> openList = new List<SearchNode>();
        private List<SearchNode> closedList = new List<SearchNode>();

        public Astar(Level level)
        {
            level = new Level();
            levelWidth = level.Width;
            levelHeight = level.Height;
            InitializeSearchNodes(level);
        }

        private void InitializeSearchNodes(Level level)
        {
            searchNodes = new SearchNode[levelWidth, levelHeight];

            for (int x = 0; x < levelWidth; x++)
            {

                for (int y = 0; y < levelHeight; y++)
                {

                    SearchNode node = new SearchNode();
                    node.Position = new Point(x, y);
                    node.Walkable = level.GetIndex(y, x) == 0;

                    if (node.Walkable == true)
                    {
                        node.Neighbors = new SearchNode[4];

                        searchNodes[x, y] = node;
                    }
                }
            }

            for (int x = 0; x < levelWidth; x++)
            {

                for (int y = 0; y < levelHeight; y++)
                {

                    SearchNode node = searchNodes[x, y];

                    if (node == null || node.Walkable == false)
                    {
                        continue;
                    }

                    Point[] neighbors = new Point[]
                    {
                        new Point (x, y - 1),
                        new Point (x, y + 1),
                        new Point (x - 1, y),
                        new Point (x + 1, y),
                    };

                    for (int i = 0; i < neighbors.Length; i++)
                    {
                        Point position = neighbors[i];

                        if (position.X < 0 || position.X > levelWidth - 1 || position.Y < 0 || position.Y > levelHeight - 1)
                        {
                            continue;
                        }
                        SearchNode neighbor = searchNodes[position.X, position.Y];
                        if (neighbor == null || neighbor.Walkable == false)
                        {

                            continue;

                        }

                        node.Neighbors[i] = neighbor;

                    }
                }
            }
        }

        private float Heuristic(Point point1, Point point2)
        {
            return Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
        }

        private SearchNode FindBestNode(ref List<Vector2> astarNodes)
        {
            SearchNode currentTile = openList[0];

            float smallestDistanceToGoal = float.MaxValue;

            // Find the closest node to the goal.
            for (int i = 0; i < openList.Count; i++)
            {
                astarNodes.Add(new Vector2(openList[i].Position.X * 50, openList[i].Position.Y * 50));
                if (openList[i].DistanceToGoal < smallestDistanceToGoal)
                {
                    currentTile = openList[i];
                    smallestDistanceToGoal = currentTile.DistanceToGoal;
                }
            }
            return currentTile;
        }

        private Queue<Vector2> FindFinalPath(ref List<Vector2> astarNodes, SearchNode startNode, SearchNode endNode)
        {

            closedList.Add(endNode);

            SearchNode parentTile = endNode.Parent;

            while (parentTile != startNode)
            {
                closedList.Add(parentTile);
                parentTile = parentTile.Parent;
            }

            Queue<Vector2> finalPath = new Queue<Vector2>();

            for (int i = closedList.Count - 1; i >= 0; i--)
            {
                finalPath.Enqueue(new Vector2(closedList[i].Position.X * 50, closedList[i].Position.Y * 50));
            }

            return finalPath;
        }

        public Queue<Vector2> FindPath(ref List<Vector2> astarNodes, Point startPoint, Point endPoint)
        {

            if (startPoint == endPoint)
            {
                return new Queue<Vector2>();
            }

            SearchNode startNode = searchNodes[startPoint.X, startPoint.Y];
            SearchNode endNode = searchNodes[endPoint.X, endPoint.Y];


            startNode.InOpenList = true;

            startNode.DistanceToGoal = Heuristic(startPoint, endPoint);
            startNode.DistanceTraveled = 0;

            openList.Add(startNode);


            while (openList.Count > 0)
            {

                SearchNode currentNode = FindBestNode(ref astarNodes);

                if (currentNode == endNode)
                {

                    return FindFinalPath(ref astarNodes, startNode, endNode);
                }


                for (int i = 0; i < currentNode.Neighbors.Length; i++)
                {
                    SearchNode neighbor = currentNode.Neighbors[i];

                    

                    if (neighbor == null || neighbor.Walkable == false)
                    {
                        continue;
                    }

                    
                    float distanceTraveled = currentNode.DistanceTraveled + 1;


                    float heuristic = Heuristic(neighbor.Position, endPoint);


                    if (neighbor.InOpenList == false && neighbor.InClosedList == false)
                    {

                        neighbor.DistanceTraveled = distanceTraveled;

                        neighbor.DistanceToGoal = distanceTraveled + heuristic;

                        neighbor.Parent = currentNode;

                        neighbor.InOpenList = true;
                        openList.Add(neighbor);
                    }

                    else if (neighbor.InOpenList || neighbor.InClosedList)
                    {

                        if (neighbor.DistanceTraveled > distanceTraveled)
                        {
                            neighbor.DistanceTraveled = distanceTraveled;
                            neighbor.DistanceToGoal = distanceTraveled + heuristic;

                            neighbor.Parent = currentNode;
                        }
                    }
                }


                openList.Remove(currentNode);
                currentNode.InClosedList = true;
            }


            return new Queue<Vector2>();
        }
    }
}
