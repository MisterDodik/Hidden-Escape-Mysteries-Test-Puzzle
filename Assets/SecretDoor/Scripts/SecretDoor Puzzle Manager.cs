using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretDoorPuzzleManager : MonoBehaviour
{
    [SerializeField] List<LevelData> levels;

    int currentLevel = 0;
    int currentProgress = 0;
    int winCon;

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

    [SerializeField] Animator animator;

    public static SecretDoorPuzzleManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Init(levels[currentLevel]);
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

        currentProgress = 0;
        winCon = levels[currentLevel].winCondition;
    }
    void Init(LevelData data)
    {
        clearScene();

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


            //spawned.GetComponent<ButtonScript>().buttonData = data.buttonData[i];
            spawned.GetComponent<ButtonScript>().GetInitValues(i, levels[currentLevel]);
        }
    }
    public void resetLevel()
    {
        Init(levels[currentLevel]);
    }
    public void updateProgress()
    {
        currentProgress++;

        if (currentProgress < winCon)
            return;

        currentLevel++;
        if (currentLevel < levels.Count)
            Init(levels[currentLevel]);
        else
            EndGame();
    }
    void EndGame()
    {
        animator.SetTrigger("isOver");
    }
}

public enum SpawnDirection
{
    up, 
    right,
    down,
    left
}