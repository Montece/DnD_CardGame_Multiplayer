using System;
using UnityEngine;
using UnityEngine.UI;

public class GridCorrection : MonoBehaviour
{
    private GridLayoutGroup grid;
    private RectTransform rect;
    private Vector2 vector = new Vector2();

	void Start()
    {
        rect = GetComponent<RectTransform>();
        grid = GetComponent<GridLayoutGroup>();
    }
	
	void Update()
    {
        vector.y = rect.sizeDelta.y;
        vector.x = grid.padding.left + (grid.cellSize.x + grid.spacing.x) * grid.transform.childCount + grid.padding.right;
        rect.sizeDelta = vector;
    }
}
