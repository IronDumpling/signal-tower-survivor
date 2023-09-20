using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<TGridObject>
{
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private TGridObject[,] gridArray;

    //#######################################################################################################################################
    //#############################################################Constructors##############################################################
    //#######################################################################################################################################
    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];

        // Initialize the grid with default values.
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = default(TGridObject);
            }
        }
    }

    public Grid(int width, int height, float cellSize, Vector3 originPosition,
        Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }
    }

    //#######################################################################################################################################
    //#############################################################Get Methods##############################################################
    //#######################################################################################################################################
    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public float GetCellSize()
    {
        return cellSize;
    }
    
    /// <summary>
    /// Get the world position of the grid cell given the position in the grid space.
    /// The position is always at the bottom left corner of the cell.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    /// <summary>
    /// Get the x and y coordinate of the grid cell that the world position is in.
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }


    /// <summary>
    /// Get the grid object given the grid coordinate.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public TGridObject GetGridObj(int x, int y)
    {
        if (x < 0 || y < 0 || x >= width || y >= height) return default(TGridObject);
        return gridArray[x, y];
    }

    /// <summary>
    /// Get the grid object given the world position.
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public TGridObject GetGridObj(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObj(x, y);
    }

    //#######################################################################################################################################
    //#############################################################Set Methods##############################################################
    //#######################################################################################################################################
    /// <summary>
    /// Set the grid object at the given position in grid space.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="value"></param>
    public void SetGridObj(int x, int y, TGridObject value)
    {
        if (x < 0 || y < 0 || x >= width || y >= height) return;
        gridArray[x, y] = value;
        TriggerGridObjectChanged(x, y);
    }

    /// <summary>
    /// Set the grid object at the given position in world coordinate.
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="value"></param>
    public void SetGridObj(Vector3 worldPosition, TGridObject value)
    {
        GetXY(worldPosition, out int x, out int y);
        SetGridObj(x, y, value);
    }


    //#######################################################################################################################################
    //#############################################################Others##############################################################
    //#######################################################################################################################################
    
    /// <summary>
    /// Trigger the event when the grid object at the given position is changed.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void TriggerGridObjectChanged(int x, int y)
    {
        OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEventArgs { x = x, y = y });
    }
}

public struct Point
{
    public int x;
    public int y;
}
