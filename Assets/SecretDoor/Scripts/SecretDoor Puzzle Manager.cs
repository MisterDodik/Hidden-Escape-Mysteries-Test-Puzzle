using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretDoorPuzzleManager : MonoBehaviour
{
    [SerializeField] LevelData level1Data;
    [SerializeField] LevelData level2Data;
    [SerializeField] LevelData level3Data;
    LevelData currentLevel;

    [HideInInspector] public Vector2 gridOrigin;
    [HideInInspector] public Vector2 gridSize;
    [HideInInspector] public Vector2 incrementAmount;

    [SerializeField] GameObject barricadePrefab;
    [SerializeField] Transform barricadeParent;

    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform buttonParent;
    
    public Transform skullParent;

    [SerializeField] Sprite buttonOne;
    [SerializeField] Sprite buttonTwo;
    [SerializeField] Sprite buttonThree;

    GameObject currentBackground;

    public static SecretDoorPuzzleManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Init(level3Data);
    }
    void clearScene()
    {
        if (currentBackground)
            Destroy(currentBackground);
        foreach (Transform child in barricadeParent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in skullParent)
        {
            Destroy(child.gameObject);
        }
    }
    void Init(LevelData data)
    {
        clearScene();
        
        currentLevel = data;

        incrementAmount = data.incrementAmount;
        gridOrigin = data.gridOrigin;

        currentBackground = Instantiate(data.background);

        for (int i = 0; i < data.barricadeCoordinates.Count; i++)
        {
            GameObject spawned = Instantiate(barricadePrefab, barricadeParent);
            spawned.transform.localPosition = gridOrigin + incrementAmount * data.barricadeCoordinates[i];
        }
        for (int i = 0; i < data.buttonData.Count; i++)
        {
            GameObject spawned = Instantiate(buttonPrefab, buttonParent);


            if (data.buttonData[i].amount == 1)
                data.buttonData[i].sprite = buttonOne;
            else if (data.buttonData[i].amount == 2)
                data.buttonData[i].sprite = buttonTwo;
            else if (data.buttonData[i].amount == 3)
                data.buttonData[i].sprite = buttonThree;


            spawned.GetComponent<ButtonScript>().buttonData = data.buttonData[i];
        }
    }
    public void resetLevel()
    {
        Init(currentLevel);
    }

    //public void CheckGridOccupancy()
    //{
    //    for (int row = 0; row < gridSize.x; row++)
    //    {
    //        for (int col = 0; col < gridSize.y; col++)
    //        {
    //            Vector2 cellCenter = gridOrigin + incrementAmount * new Vector2(row, col);

    //            Collider2D hit = Physics2D.OverlapBox(cellCenter, new Vector2(0.1f, 0.1f), 0);

    //            if (hit == null)
    //            {
    //                return;
    //            }
    //        }
    //    }
    //}
}

public enum SpawnDirection
{
    up, 
    right,
    down,
    left
}