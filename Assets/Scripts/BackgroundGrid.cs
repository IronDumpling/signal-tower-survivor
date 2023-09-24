using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using UnityEngine;

public class BackgroundGrid: MonoBehaviour
{
    private Grid<BackgroundGridObject> m_avalibilityGrid;

    [SerializeField] 
    private GameObject m_avalibleCellPrefab, m_nonAvalibleCellPrefab;

    private void Start()
    {

        m_avalibilityGrid = new Grid<BackgroundGridObject>(10, 10, 1, new Vector3(0, 0, 0), 
            (Grid<BackgroundGridObject> grid, int x, int y ) =>
            {
                return new BackgroundGridObject(grid, x, y);
            });

        
        // Initialize the back ground, only the center 4*4 cells are avalible
        for (int i = 0; i < m_avalibilityGrid.GetWidth(); i++)
        {
            for (int j = 0; j < m_avalibilityGrid.GetHeight(); j++)
            {
                BackgroundGridObject gridObj = m_avalibilityGrid.GetGridObj(i, j);
                Transform cellTransform = null;
                bool avalible;

                if (i >= 3 && i <= 6 && j >= 3 && j <= 6)
                {
                    cellTransform = Instantiate(m_avalibleCellPrefab, m_avalibilityGrid.GetWorldPosition(i, j), 
                        Quaternion.identity, transform).transform;
                    avalible = true;
                }
                else
                {
                    cellTransform = Instantiate(m_nonAvalibleCellPrefab, m_avalibilityGrid.GetWorldPosition(i, j),
                        Quaternion.identity, transform).transform;
                    avalible = false;
                }
                gridObj.SetValue(avalible, cellTransform);
            }
        }

        //for (int i = 0; i< m_avalibilityGrid.GetWidth(); i++)
        //{
        //    for (int j = 0; j < m_avalibilityGrid.GetHeight(); j++)
        //    {
        //        Debug.Log(m_avalibilityGrid.GetGridObj(i, j));
        //    }
        //}
    }

    //private void SetCellBackground(object sender, Grid<bool>.OnGridObjectChangedEventArgs args)
    //{

    //    Vector3 cellWorldPosition = m_avalibilityGrid.GetWorldPosition(args.x, args.y);

    //    if (m_avalibilityGrid.GetGridObj(args.x, args.y) == true)
    //    {
    //        Instantiate(m_avalibleCellPrefab, cellWorldPosition, Quaternion.identity, transform);
    //    }
    //    else
    //    {
    //        Instantiate(m_nonAvalibleCellPrefab, cellWorldPosition, Quaternion.identity, transform);
    //    }
    //}
}
