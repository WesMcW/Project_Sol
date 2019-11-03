using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathFinding : MonoBehaviour
{
    public enum theHeuristics { EuclideanDistance = 0, ManhattanDistance = 1, TileCost = 2 } // three states to choose from where mouse controls one or the other, or both axes
    public theHeuristics heuristics = theHeuristics.EuclideanDistance;
    bool EuclideanHeuristic;
    bool manhattanHeuristic;
    bool differedManhattan = true;

    [Header("This optimizes the Euclidean Heuristic for relatively smaller maps(i.e. 500 X 500 meters max)")]
    [SerializeField]
    bool isSmall = true;


    Grid grid;

    void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Update()
    {
        if (heuristics == theHeuristics.EuclideanDistance)
        {
            EuclideanHeuristic = true;
            manhattanHeuristic = false;
            differedManhattan = false;
        }
        else if (heuristics == theHeuristics.ManhattanDistance)
        {
            EuclideanHeuristic = false;
            manhattanHeuristic = true;
            differedManhattan = false;
        }
        if (heuristics == theHeuristics.TileCost)
        {
            EuclideanHeuristic = false;
            manhattanHeuristic = false;
            differedManhattan = true;
        }
    }

    //finds the path
    public void FindPath(PathRequest request, Action<PathResult> callback)
    {

        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = grid.NodeFromWorldPoint(request.pathStart);
        Node targetNode = grid.NodeFromWorldPoint(request.pathEnd);

        //if the creature starts on a walkable area and the target is walkable continue;
        if (startNode.walkable && targetNode.walkable)
        {
            //creates the heap of nodes with a maximum of the world size
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            //creates a HashSet of Nodes
            HashSet<Node> closedSet = new HashSet<Node>();
            //adds the start to the heap as the initial node
            openSet.Add(startNode);
            //while path not found
            while (openSet.Count > 0)
            {
                //looks at the next most efficient possible move
                Node currentNode = openSet.RemoveFirst();
                //adds that to the closed set
                closedSet.Add(currentNode);
                //if we reached our destination stop looking for the shortest path
                if (currentNode == targetNode)
                {
                    pathSuccess = true;
                    break;
                }
                //Gets the neigbors and compares each neighbor to find the least costing square to move to
                foreach (Node neighbour in grid.GetNeighbors(currentNode))
                {
                    //if the node isn't wlakable or if its alread in the closedSet continue
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }
                    //finds the cost to go to that node
                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    //if the new movement is less than the gcost of the neighbor or if it isn't already in the open set continue
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        //neighbour gCost is equal to the new movement
                        neighbour.gCost = newMovementCostToNeighbour;
                        //Gets the distance
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        //neigbours parent is now the current node
                        neighbour.parent = currentNode;
                        //if its not in the openSet, add it to the open set
                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                        else
                            openSet.UpdateItem(neighbour);//This updates everything accordingly
                    }
                }
            }
        }
        //on a success full find get the path in the correct order
        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
            pathSuccess = waypoints.Length > 0;
        }
        callback(new PathResult(waypoints, pathSuccess, request.callback));

    }
    //gets all the positions to go
    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        //retraces path and adds it to the path list via the nodes parentage. stop when the currentNode is the start node
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        //adds the start to the path list
        path.Add(startNode);
        //makes paths more streamlined
        Vector3[] waypoints = SimplifyPath(path);
        //reverses the points
        Array.Reverse(waypoints);
        //returns a array of vector3s
        return waypoints;

    }

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;
        //only records specific points where the creature changes direction and heads towards that direction
        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i - 1].worldPosition);
            }
            directionOld = directionNew;
        }
        //returns waypoints as an array of vector3s
        return waypoints.ToArray();
    }
    //distance from A to B
    int GetDistance(Node nodeA, Node nodeB)
    {

        if (EuclideanHeuristic)
        {
            int dstX = (int)Mathf.Pow(nodeA.gridX - nodeB.gridX, 2);
            int dstY = (int)Mathf.Pow(nodeA.gridY - nodeB.gridY, 2);
            //This is a little more optimized since we aren't going to be dealing as large of values since the map is relatively small
            if (isSmall)
            {

                return dstX + dstY;
            }
            else
            {
                float heurstic = (float)Math.Sqrt(dstX + dstY);
                heurstic *= (1 + 0.001f);

                return (int)heurstic;
            }
        }
        //
        else if (manhattanHeuristic)
        {
            int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
            return dstX + dstY;
        }
        //A form of the manhattan heuristic where diagonal moves cost 14 and vertical moves cost 10
        else if (differedManhattan)
        {
            int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

            if (dstX > dstY)
                return 7 * dstY + 5 * (dstX - dstY);//Divded the values above by 2
            return 7 * dstX + 5 * (dstY - dstX);
        }
        return 0;


    }

}
/*
int GetDistance(Node nodeA, Node nodeB)
{

	if (EuclideanHeuristic)
	{
		int dstX = (int)Mathf.Pow(nodeA.gridX - nodeB.gridX, 2);
		int dstY = (int)Mathf.Pow(nodeA.gridY - nodeB.gridY, 2);
		//return dstX + dstY;
		return (int)Math.Sqrt(dstX + dstY);
	}
	//
	else if (manhattanHeuristic)
	{
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
		return dstX + dstY;
	}
	//A form of the manhattan heuristic where diagonal moves cost 14 and vertical moves cost 10
	else if (differedManhattan)
	{
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (dstX > dstY)
			return 14 * dstY + 10 * (dstX - dstY);
		return 14 * dstX + 10 * (dstY - dstX);
	}
	return 0;


}
*/
/*
//distance from A to B
	int GetDistance(Node nodeA, Node nodeB)
	{
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (dstX > dstY)
			return 14 * dstY + 10 * (dstX - dstY);
		return 14 * dstX + 10 * (dstY - dstX);
	}
	*/
