using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data")]
public class LevelData : ScriptableObject
{
    public int winCondition;

    public GameObject background;
    public Vector2 gridSize;
    public Vector2 incrementAmount;
    public Vector2 gridOrigin;

    public List<ButtonData> buttonData;

    public List<Vector2> barricadeCoordinates;

    [System.Serializable]
    public class ButtonData
    {
        public Vector2 buttonCoordinates;
        public int amount;
        public SpawnDirection spawnDirection;
        public Sprite sprite;
    }
}
