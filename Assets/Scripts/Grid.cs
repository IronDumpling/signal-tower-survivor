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

    private int m_width;
    private int m_height;
    

    private float m_cellSizeX;
    private float m_cellSizeY;
    private float m_cellGapX;
    private float m_cellGapY;

    private Vector3 m_originPosition;
    private TGridObject[,] m_gridArray;

    //#######################################################################################################################################
    //#############################################################Constructors##############################################################
    //#######################################################################################################################################
    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.m_width = width;
        this.m_height = height;
        this.m_cellSizeX = cellSize;
        this.m_cellSizeY = cellSize;
        this.m_cellGapX = 0;
        this.m_cellGapY = 0;
        this.m_originPosition = originPosition;

        m_gridArray = new TGridObject[width, height];

        // Initialize the grid with default values.
        for (int x = 0; x < m_gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < m_gridArray.GetLength(1); y++)
            {
                m_gridArray[x, y] = default(TGridObject);
            }
        }
    }

    public Grid(int width, int height, float cellSize, Vector3 originPosition,
        Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.m_width = width;
        this.m_height = height;
        this.m_cellSizeX = cellSize;
        this.m_cellSizeY = cellSize;
        this.m_cellGapX = 0;
        this.m_cellGapY = 0;
        this.m_originPosition = originPosition;

        m_gridArray = new TGridObject[width, height];

        for (int x = 0; x < m_gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < m_gridArray.GetLength(1); y++)
            {
                m_gridArray[x, y] = createGridObject(this, x, y);
            }
        }
    }

    public Grid(int width, int height, float cellSizeX, float cellSizeY,
        float cellGapX, float cellGapY, Vector3 originPosition)
    {
        this.m_width = width;
        this.m_height = height;
        this.m_cellSizeX = cellSizeX;
        this.m_cellSizeY = cellSizeY;
        this.m_cellGapX = cellGapX;
        this.m_cellGapY = cellGapY;
        this.m_originPosition = originPosition;

        m_gridArray = new TGridObject[width, height];

        // Initialize the grid with default values.
        for (int x = 0; x < m_gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < m_gridArray.GetLength(1); y++)
            {
                m_gridArray[x, y] = default(TGridObject);
            }
        }
    }

    public Grid(int width, int height, float cellSizeX, float cellSizeY,
        float cellGapX, float cellGapY, Vector3 originPosition,
        Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.m_width = width;
        this.m_height = height;
        this.m_cellSizeX = cellSizeX;
        this.m_cellSizeY = cellSizeY;
        this.m_cellGapX = cellGapX;
        this.m_cellGapY = cellGapY;
        this.m_originPosition = originPosition;

        m_gridArray = new TGridObject[width, height];

        // Initialize the grid with default values.
        for (int x = 0; x < m_gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < m_gridArray.GetLength(1); y++)
            {
                m_gridArray[x, y] = createGridObject(this, x, y);
            }
        }
    }

    //#######################################################################################################################################
    //#############################################################Get Methods##############################################################
    //#######################################################################################################################################
    public int GetWidth()
    {
        return m_width;
    }

    public int GetHeight()
    {
        return m_height;
    }

    public float GetCellSizeX()
    {
        return m_cellSizeX;
    }

    public float GetCellSizeY()
    {
        return m_cellSizeY;
    }

    public float GetCellSize()
    {
        if (m_cellSizeX != m_cellSizeY)
        {
            Debug.LogWarning("The cell size is not square.");
        }

        return m_cellSizeX;
    }

    /// <summary>
    /// If we consider the cell size and the cell gap as a whole, this function will return the ratio of the cell size to the whole.
    /// This is for the x axis.
    /// </summary>
    /// <returns></returns>
    public float GetSizeToTotalRatioX()
    {
        return m_cellSizeX / (m_cellSizeX + m_cellGapX);
    }

    /// <summary>
    /// If we consider the cell size and the cell gap as a whole, this function will return the ratio of the cell size to the whole.
    /// This is for the y axis.
    /// </summary>
    /// <returns></returns>
    public float GetSizeToTotalRatioY()
    {
        return m_cellSizeY / (m_cellSizeY + m_cellGapY);
    }

    /// <summary>
    /// Get the world position of the grid cell given the position in the grid space.
    /// The position is always at the bottom left corner of the cell.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x * (m_cellSizeX + m_cellGapX), y * (m_cellSizeY + m_cellGapY)) + m_originPosition;
    }

    /// <summary>
    /// Get the x and y coordinate of the grid cell that the world position is in.
    /// It will gives -1 for x and y if the world position is not in the cell of the grid.
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        float x_temp = (worldPosition - m_originPosition).x / (m_cellSizeX + m_cellGapX);
        float y_temp = (worldPosition - m_originPosition).y / (m_cellSizeY + m_cellGapY);

        //check for out of boundary
        if (x_temp < 0 || y_temp < 0 || x_temp >= m_width || y_temp >= m_height)
        {
            //Write the warning message here and print out the world position and it grid width and height.
            Debug.LogWarning($"The world position is not in the grid");
            x = -1;
            y = -1;
            return;
        }

        x = Mathf.FloorToInt(x_temp);
        y = Mathf.FloorToInt(y_temp);

        
        //check whether the position is in the gap
        if (x_temp - x > GetSizeToTotalRatioX() || y_temp - y > GetSizeToTotalRatioY())
        {
            Debug.LogWarning($"The world position is in the gap");
            x = -1;
            y = -1;
            return;
        }
    }


    /// <summary>
    /// Get the grid object given the grid coordinate.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public TGridObject GetGridObj(int x, int y)
    {
        if (x < 0 || y < 0 || x >= m_width || y >= m_height) return default(TGridObject);
        return m_gridArray[x, y];
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
        if (x < 0 || y < 0 || x >= m_width || y >= m_height) return;
        m_gridArray[x, y] = value;
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
