using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantmentTableManagerRefactor : MonoBehaviour
{
    public EnchantmentTableNodePositions data;

    [SerializeField] GameObject hexagonPrefab;
    [SerializeField] Transform hexagonParent;

    [SerializeField] GameObject pathPrefab;
    [SerializeField] Transform pathParent;

    public static EnchantmentTableManagerRefactor instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        Init();
    }

    void Init()
    {
        // Spawning hexagons
        for (int i = 0, j = 0; i < data.hexagonPositions.Count; i++)
        {
            float y = data.hexagonPositions[i].yPos;

            foreach (float x in data.hexagonPositions[i].xPos)
            {
                Vector3 spawnPosition = new Vector3(x, y, 0);
                GameObject hexagon = (GameObject)Instantiate(hexagonPrefab, spawnPosition, Quaternion.identity, hexagonParent);

              //  hexagon.GetComponent<HexagonScriptRefactor>().GetTriangleData();

                j++;

            }
        }

        // Spawning paths (angle 0)
        for (int i = 0; i < data.pathPositions1.Count; i++)
        {
            float y = data.pathPositions1[i].yPos;

            foreach (float x in data.pathPositions1[i].xPos)
            {
                Vector3 spawnPosition = new Vector3(x, y, 0);
                Instantiate(pathPrefab, spawnPosition, Quaternion.identity, pathParent);
            }
        }

        // Spawning paths (angle 60)
        for (int i = 0; i < data.pathPositions2.Count; i++)
        {
            float y = data.pathPositions2[i].yPos;

            foreach (float x in data.pathPositions2[i].xPos)
            {
                Vector3 spawnPosition = new Vector3(x, y, 0);
                Instantiate(pathPrefab, spawnPosition, Quaternion.Euler(0, 0, 60), pathParent);
            }
        }

        // Spawning paths (angle -60)
        for (int i = 0; i < data.pathPositions3.Count; i++)
        {
            float y = data.pathPositions3[i].yPos;

            foreach (float x in data.pathPositions3[i].xPos)
            {
                Vector3 spawnPosition = new Vector3(x, y, 0);
                Instantiate(pathPrefab, spawnPosition, Quaternion.Euler(0, 0, -60), pathParent);
            }
        }
    }
}
