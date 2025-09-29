using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class hover_script : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject hoverObject;
    public GameObject selectedObject;
    public Vector3 WorldPosition;

    public static hover_script HoverSciptObj { get; private set; }
    void Awake()
    {
        if (HoverSciptObj != null && HoverSciptObj != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            HoverSciptObj = this;
        }
    }

    void Start()
    {
        if (tilemap == null)
        {
            tilemap = GetComponent<Tilemap>();
        }
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        // Convert to world space
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z - tilemap.transform.position.z);
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);

        // Snap to the tile cell
        Vector3Int cellPosition = tilemap.WorldToCell(worldPoint);

        Vector3 cellWorldPos = tilemap.GetCellCenterWorld(cellPosition);


        // Match hover object Z with its current Z
        hoverObject.transform.position = new Vector3(cellWorldPos.x, cellWorldPos.y, hoverObject.transform.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            selectedObject.transform.position = new Vector3(cellWorldPos.x, cellWorldPos.y, selectedObject.transform.position.z);
            if (tilemap.HasTile(cellPosition))
            {
                WorldPosition = cellWorldPos;
                Debug.Log($"Clicked Cell Position: {cellPosition}");
                Debug.Log($"Tile at {cellPosition} is {tilemap.GetTile(cellPosition).name}");
            }
            else
            {
                Debug.Log("Clicked on empty space.");
            }
        }
    }

}


/*
//Vector3 mousePos = Input.mousePosition;
Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);

// undo the squashing before sending to WorldToCell
Vector3 adjustedPoint = new Vector3(
    worldPoint.x / tilemap.cellSize.x,
    worldPoint.y / tilemap.cellSize.y,
    worldPoint.z
);

Vector3Int cellPosition = tilemap.WorldToCell(adjustedPoint);

// then rescale back when positioning hoverObject
Vector3 cellWorldPos = tilemap.GetCellCenterWorld(cellPosition);
cellWorldPos = new Vector3(
    cellWorldPos.x * tilemap.cellSize.x,
    cellWorldPos.y * tilemap.cellSize.y,
    hoverObject.transform.position.z
);

hoverObject.transform.position = cellWorldPos;
*/