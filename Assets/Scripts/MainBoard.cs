using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBoard : MonoBehaviour
{
    List<Device> devices = new List<Device>();

    [SerializeField] int GRID_SIZE = 10;
    [SerializeField] float UNIT_SIZE = 1f;

    Grid<Cell> m_CellGrid;
    Grid<Interface> m_XInterfaceGrid;
    Grid<Interface> m_YInterfaceGrid;

    private void Start()
    {
        m_CellGrid = new Grid<Cell>(GRID_SIZE, GRID_SIZE, UNIT_SIZE,
                                    new Vector3(0, 0, 0));
        m_XInterfaceGrid = new Grid<Interface>(GRID_SIZE, GRID_SIZE+1, UNIT_SIZE,
                                                new Vector3(-UNIT_SIZE / 2f, 0, 0));
        m_YInterfaceGrid = new Grid<Interface>(GRID_SIZE+1, GRID_SIZE, UNIT_SIZE,
                                                new Vector3(0, UNIT_SIZE / 2f, 0));
    }
}
