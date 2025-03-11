using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    //Podaci iz skriptabl objekta
    SpawnDirection spawnDirection;  
    int skullNumber = 1;
    Vector2 gridCoordintates;

    Vector2 gridOrigin; //ovo neka izvadi iz skriptable objekta isto
    Vector2 incrementAmount;     //i ovo
    Vector2 gridSize;

    [SerializeField] private GameObject skullObjectPrefab;
    Transform skullParent;

    Vector2 buttonPosition;
    Vector2 orienation;
    Transform spawnPosition;

    //[HideInInspector]public LevelData.ButtonData buttonData;
    LevelData.ButtonData buttonData;

    [SerializeField] private Transform buttonDark;
    [SerializeField] private GameObject buttonEmpty;

    private void Start()
    {
        spawnDirection = buttonData.spawnDirection;
        skullNumber = buttonData.amount;
        gridCoordintates = buttonData.buttonCoordinates;

        GetComponent<SpriteRenderer>().sprite = buttonData.sprite;

        skullParent = SecretDoorPuzzleManager.instance.skullParent;
        //gridOrigin = SecretDoorPuzzleManager.instance.gridOrigin;
        //incrementAmount = SecretDoorPuzzleManager.instance.incrementAmount;

        buttonPosition = gridOrigin + incrementAmount * gridCoordintates;
        transform.localPosition = buttonPosition;


        spawnPosition = transform.GetChild(0);

        if (spawnDirection == SpawnDirection.up)
            orienation = new Vector2(0, 1f);
        else if (spawnDirection == SpawnDirection.down)
            orienation = new Vector2(0, -1f);
        else if (spawnDirection == SpawnDirection.right)
            orienation = new Vector2(1f, 0);
        else if (spawnDirection == SpawnDirection.left)
            orienation = new Vector2(-1f, 0);
        
        spawnPosition.localPosition = incrementAmount * orienation;
    }

    public void GetInitValues(int index, LevelData currentLevel)
    {
        buttonData = currentLevel.buttonData[index];

        incrementAmount = currentLevel.incrementAmount;
        gridOrigin = currentLevel.gridOrigin;
        gridSize = currentLevel.gridSize;
    }

    private void OnMouseDown()
    {
        if (skullNumber == 0)
        {
            return;
        }
        if (canBeSpawned())
        {        
            GameObject spawned = Instantiate(skullObjectPrefab, skullParent);
            spawned.transform.localPosition = transform.position; 

            StartCoroutine(MoveBlocks(spawned, spawnPosition.position));

            skullNumber--;
            if(skullNumber == 0)
            {
                Instantiate(buttonEmpty, transform);
            }
            changeSprite();

           

        }
    }
    void changeSprite()
    {
        Transform darkSpawned = Instantiate(buttonDark, transform).transform;

        
        if(buttonData.amount==2)
            darkSpawned.localPosition = new Vector3(-0.125f + 0.2f * skullNumber, 0.015f, 0);
        else if (buttonData.amount == 3)
            darkSpawned.localPosition = new Vector3(-0.19f + 0.175f * skullNumber, 0.015f, 0);
    }
    IEnumerator MoveBlocks(GameObject newBlock, Vector3 targetPosition)
    {
        List<GameObject> blocksToMove = new List<GameObject>();

        Vector2 checkPosition = targetPosition;

        int index = 1;
        while (IsBlockAtPosition(checkPosition)) // Check for existing blocks
        {
            blocksToMove.Add(GetBlockAtPosition(checkPosition));
            checkPosition += index * incrementAmount * orienation; // Move check to the next position
        }

        // Move all blocks in reverse order
        for (int i = blocksToMove.Count - 1; i >= 0; i--)
        {
            StartCoroutine(AnimateMove(blocksToMove[i], (i+1) * incrementAmount * orienation));
        }

        // Move new block into position
        yield return StartCoroutine(AnimateMove(newBlock, Vector2.zero));

        yield return new WaitForSeconds(0.1f);
        SecretDoorPuzzleManager.instance.updateProgress();
    }

    IEnumerator AnimateMove(GameObject block, Vector2 direction)
    {
        Vector2 startPos = block.transform.localPosition;
        Vector2 endPos = (Vector2)spawnPosition.position + direction;
        float elapsedTime = 0f;
        float moveDuration = 0.25f;

        while (elapsedTime < moveDuration)
        {
            block.transform.localPosition = Vector3.Lerp(startPos, endPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        block.transform.localPosition = endPos; 
    }

    bool IsBlockAtPosition(Vector3 position)
    {
        return Physics2D.OverlapCircle(position, 0.1f, 128) != null;
    }

    GameObject GetBlockAtPosition(Vector3 position)
    {
        Collider2D hit = Physics2D.OverlapCircle(position, 0.1f, 128);
        return hit ? hit.gameObject : null;
    }
    bool canBeSpawned()
    {  
        for (int i = 1; i < gridSize.x - 1; i++)
        {           
            Vector2 cellCenter = buttonPosition + incrementAmount * i * orienation;

            Collider2D hit = Physics2D.OverlapBox(cellCenter, new Vector2(0.1f, 0.1f), 0);

            //barricade block
            if (hit!=null && hit.gameObject.layer == 0)
            {
                return false;
            }

            if (hit == null)
            {
                return true;
            }
            
        }
        return false;
    }
}
