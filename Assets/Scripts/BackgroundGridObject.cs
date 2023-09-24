using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGridObject
{
    private Grid<BackgroundGridObject> m_grid;
    private int m_x, m_y;
    private bool m_isAvalible;
    private Transform m_backgroundTransform;


    public BackgroundGridObject(Grid<BackgroundGridObject> grid, int x, int y) { 
        m_grid = grid;
        m_x = x;
        m_y = y;
        m_isAvalible = false;
        m_backgroundTransform = null;
    
    }

    public void SetValue(bool isAvalible, Transform backgroundTransform)
    {
        m_isAvalible = isAvalible;
        m_backgroundTransform = backgroundTransform;
        m_grid.TriggerGridObjectChanged(m_x, m_y);
    }

    public bool IsAvalible()
    {
        return m_isAvalible;
    }

}
