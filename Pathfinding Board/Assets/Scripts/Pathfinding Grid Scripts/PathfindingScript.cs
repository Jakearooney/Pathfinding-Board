using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingScript
{
    public PathfindingGridScript<PathnodeScript> grid;

    private const int moveStriaghtCost = 10;
    private const int moveDiagonalCost = 14;

    private List<PathnodeScript> openList;
    private List<PathnodeScript> closedList;

    public PathfindingScript(int width, int height)
    {
        grid = new PathfindingGridScript<PathnodeScript>(width, height, 10f, Vector3.zero, (PathfindingGridScript<PathnodeScript> g, int x, int y) => new PathnodeScript(g, x, y));
    }

    public PathfindingGridScript<PathnodeScript> GetGrid()
    {
        return grid;
    }

    public List<PathnodeScript> FindPath(int startX, int startY, int endX, int endY)
    {
        PathnodeScript startNode = grid.GetGridObject(startX, startY);
        PathnodeScript endNode = grid.GetGridObject(endX, endY);

        openList = new List<PathnodeScript> { startNode };
        closedList = new List<PathnodeScript>();

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PathnodeScript pathNode = grid.GetGridObject(x, y);

                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathnodeScript currentNode = GetLowerFCostNode(openList);
            if (currentNode == endNode)
            {
                //Reached Final Node
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathnodeScript neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        //Out of nodes on the openList
        return null;
    }

    private List<PathnodeScript> GetNeighbourList(PathnodeScript currentNode)
    {
        List<PathnodeScript> neighbourList = new List<PathnodeScript>();

        if (currentNode.x - 1 >= 0)
        {
            // Left
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
            // Left Down
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            // Left Up
            if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }
        if (currentNode.x + 1 < grid.GetWidth())
        {
            // Right
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
            // Right Down
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            // Right Up
            if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }
        // Down
        if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        // Up
        if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));

        return neighbourList;
    }

    private PathnodeScript GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

    private List<PathnodeScript> CalculatePath(PathnodeScript endNode)
    {
        List<PathnodeScript> path = new List<PathnodeScript>();
        path.Add(endNode);
        PathnodeScript currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(PathnodeScript a, PathnodeScript b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);

        return moveDiagonalCost * Mathf.Min(xDistance, yDistance) + moveStriaghtCost * remaining;
    }

    private PathnodeScript GetLowerFCostNode(List<PathnodeScript> pathNodeList)
    {
        PathnodeScript lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }
}
