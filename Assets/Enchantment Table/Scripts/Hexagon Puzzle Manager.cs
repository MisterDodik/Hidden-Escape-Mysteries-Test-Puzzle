using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonPuzzleManager : MonoBehaviour
{
    [SerializeField] GameObject hexagonPrefab;
    [SerializeField] Transform hexagonParent;       // parent of all hexagons
    [SerializeField] GameObject trianglePrefab;

    [SerializeField] GameObject pathPrefab;
    [SerializeField] Transform pathParent;

    public static HexagonPuzzleManager instance;

    public Sprite hexagonShineSprite;
    public Sprite defaultHexagonSprite;

    // end screen
    [SerializeField] Animator animator;
    int winCondition = 18;
    [HideInInspector] int gameProgress = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        GameInit();
    }

    public void checkWin(int score)
    {
        gameProgress += score;
        if (gameProgress >= winCondition)
        {
            animator.SetTrigger("isOver");
            gameObject.SetActive(false);
        }
    }
    
    void GameInit()
    {
        float[] yPositionsHor = { 3.324f, 1.624f, -0.07f, -1.775f, -3.47f}; // corresponding Y values
        // x-coordinates for each hexagon
        float[][] hexagonPositions = new float[][]
        {
            new float[] { -2, 0, 2 },       // Y = 3.32
            new float[] { -3, -1, 1, 3 },   // Y = 1.624
            new float[] { -4, -2, 2, 4 },   // Y = -0.07
            new float[] { -3, -1, 1, 3 },   // Y = -1.775
            new float[] { -2, 0, 2 }        // Y = -3.47
        };

        // from 0 to 5, representing angles of triangles (0 is right, 3 is left etc)
        float[][] trianglePositions = new float[][]
        {
            new float[] { 0, 5 },              
            new float[] { 0, 1, 4 },
            new float[] { 0, 4 },

            new float[] { 0, 1 },
            new float[] { 0, 4, 5 },
            new float[] { 0, 3 },
            new float[] { 0, 4 },

            new float[] { 0, 4, 5 },
            new float[] { 0, 1, 4 },
            new float[] { 0, 4, 5 },
            new float[] { 0, 5 },

            new float[] { 0, 4, 5 },
            new float[] { 0, 4, 5 },
            new float[] { 0, 1, 4 },
            new float[] { 0 },

            new float[] { 0, 4 },
            new float[] { 0, 1, 4 },
            new float[] { 0 },
        };

        // spawning hexagons and triangles
        for (int i = 0, j=0; i < hexagonPositions.Length; i++)
        {
            float y = yPositionsHor[i];

            foreach (float x in hexagonPositions[i])
            {
                Vector3 spawnPosition = new Vector3(x, y, 0);
                GameObject hexagon = (GameObject)Instantiate(hexagonPrefab, spawnPosition, Quaternion.identity, hexagonParent);

                foreach(int k in trianglePositions[j])
                {
                    GameObject triangle = Instantiate(trianglePrefab, hexagon.transform);
                    triangle.transform.localPosition = Vector3.zero;  
                    triangle.transform.localRotation = Quaternion.Euler(0, 0, -k * 60);
                }
                j++;

            }       
        }

        
        // Spawning paths
        float[][] pathPositionsHor = new float[][]            //rot.z = 0 ie horizontal paths
        {
            new float[] { -1.07f, 0.93f },       // Y = 3.32
            new float[] { -2.07f, -0.07f, 1.93f },   // Y = 1.624
            new float[] { -3.07f, -1.07f, 0.93f, 2.93f },   // Y = -0.07
            new float[] { -2.07f, -0.07f, 1.93f },   // Y = -1.775
            new float[] { -1.07f, 0.93f },        // Y = -3.47

        };
        for (int i = 0; i < pathPositionsHor.Length; i++)
        {
            float y = yPositionsHor[i];

            foreach (float x in pathPositionsHor[i])
            {
                Vector3 spawnPosition = new Vector3(x, y, 0);
                Instantiate(pathPrefab, spawnPosition, Quaternion.identity, pathParent);
            }
        }


        float[] yPositionsAng = { 2.4f, 0.77f, -0.95f, -2.6f }; // corresponding Y values
        float[][] pathPositionsAng = new float[][]
        {
            //rot.z = 60
            new float[] { -2.6f, -0.58f, 1.43f},
            new float[] { -3.53f, -1.53f, 0.5f, 2.5f},
            new float[] { -2.53f, -0.52f, 1.48f, 3.45f},
            new float[] { -1.48f, 0.5f,2.5f},

            //rot.z = -60
            new float[] { -1.48f, 0.5f, 2.5f },
            new float[] { -2.5f, -0.54f, 1.48f, 3.5f},
            new float[] { -3.5f, -1.5f, 0.5f, 2.5f},
            new float[] { -2.5f, -0.5f, 1.45f, },

        };
        for (int i = 0; i < pathPositionsAng.Length; i++)
        {
            float y = yPositionsAng[i % yPositionsAng.Length];

            foreach (float x in pathPositionsAng[i])
            {
                Vector3 spawnPosition = new Vector3(x, y, 0);
                Instantiate(pathPrefab, spawnPosition, i < pathPositionsAng.Length/2 ? Quaternion.Euler(0, 0, 60) : Quaternion.Euler(0, 0, -60), pathParent);
            }
        }
    }
}
