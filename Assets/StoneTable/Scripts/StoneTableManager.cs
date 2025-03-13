using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTableManager : MonoBehaviour
{
    [SerializeField] GameObject centerCirclePrefab;
    [SerializeField] GameObject circlePrefab;
    [SerializeField] GameObject buttonPrefab;

    [SerializeField] Transform circleParent;
    [SerializeField] Transform buttonParent;

    public DiscData data;

    CollisionChecks[] circleCollision;
    Collider2D[] colliders;
    bool toggleCollision = true;

    bool toggleButton = true;

    GameObject currentButton;
    [SerializeField]GameObject pressedTriangle;
    float duration = 1.5f;
    int circleCount;

    public static StoneTableManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        InitPuzzle();

        circleCount = data.discCount + 1; //plus 1 central

        circleCollision = FindObjectsByType<CollisionChecks>(FindObjectsSortMode.None);
        colliders = FindObjectsByType<Collider2D>(FindObjectsSortMode.None);
    }
    void InitPuzzle()
    {
        float angleStep = 360 / data.discCount;

        // Spawning Buttons
        for (int i=0; i<data.discCount; i++)
        {
            GameObject spawned = Instantiate(buttonPrefab, buttonParent);

            float angle = -135 + i * angleStep;  
            float radians = angle * Mathf.Deg2Rad;
            Vector2 spawnPosition = new Vector2(data.buttonDist * Mathf.Cos(radians), data.buttonDist * Mathf.Sin(radians));

            spawned.transform.localPosition = spawnPosition;
            spawned.transform.localEulerAngles = new Vector3(0, 0, angle);
        }


        // Spawning Circles
        for (int i = 0; i < data.discCount; i++)
        {
            GameObject spawned = Instantiate(circlePrefab, circleParent);

            float angle = i * angleStep;
            float radians = angle * Mathf.Deg2Rad;
            print(angle);
            Vector2 spawnPosition = new Vector2(data.circleDist * Mathf.Cos(radians + data.circleAngularOffset), data.circleDist * Mathf.Sin(radians + data.circleAngularOffset));

            spawned.transform.localPosition = spawnPosition;

            spawned.transform.GetChild(0).localEulerAngles = new Vector3(0, 0, data.circleAngles[i]);
        }
        Instantiate(centerCirclePrefab, new Vector3(0,0,0), Quaternion.identity, circleParent);
    }
















    struct Circles
    {
        public Transform circle1;
        public Transform circle2;
        public Transform centerCircle;
    }

    public void Rotate(int buttonIndex)
    {
        currentButton = buttonParent.GetChild(buttonIndex).gameObject;
        //toggleButtonActivity();

        Circles circles = GetCircles(buttonIndex);
        StartCoroutine(RotateOverTime(circles.circle1, circles.circle2, circles.centerCircle, buttonIndex));
    }

    Circles GetCircles(int index)
    {
        return new Circles
        {
            circle1 = circleParent.GetChild(index % (circleCount - 1)),
            circle2 = circleParent.GetChild((index + 1) % (circleCount - 1)),
            centerCircle = circleParent.GetChild(circleCount - 1)
        };
    }

    IEnumerator RotateOverTime(Transform circle1, Transform circle2, Transform centerCircle, int index)
    {
        toggleCollisionChecks();

        pressedTriangle.SetActive(true);
        pressedTriangle.transform.localRotation = Quaternion.Euler(0, 0, (index - 3) * 45);

        float elapsedTime = 0f;
        Vector3 circle1Pos = circle1.localPosition;
        Vector3 circle2Pos = circle2.localPosition;
        Vector3 centerPos = centerCircle.localPosition;

        Quaternion currentButtonRotation = currentButton.transform.localRotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, currentButtonRotation.eulerAngles.z + 123);

        //1 -> 2, 2->center, center -> 1
        while (elapsedTime < duration)
        {
            circle1.transform.localPosition = Vector3.Lerp(circle1Pos, circle2Pos, elapsedTime / duration);
            circle2.transform.localPosition = Vector3.Lerp(circle2Pos, centerPos, elapsedTime / duration);
            centerCircle.transform.localPosition = Vector3.Lerp(centerPos, circle1Pos, elapsedTime / duration);

            currentButton.transform.localRotation = Quaternion.Lerp(currentButtonRotation, endRotation, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        circle1.localPosition = circle2Pos;
        circle2.localPosition = centerPos;
        centerCircle.localPosition = circle1Pos;

        circle1.SetSiblingIndex((index + 1) % (circleCount - 1));
        circle2.SetSiblingIndex(circleCount - 1);
        centerCircle.SetSiblingIndex(index % (circleCount - 1));

        pressedTriangle.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        toggleCollisionChecks();
        //toggleButtonActivity();
        //checksRays();
        //Win();
    }

    void toggleCollisionChecks()
    {
        toggleCollision = !toggleCollision;
        //foreach (CollisionChecks circle in circleCollision)
        //{
        //    circle.enabled = toggleCollision;
        //}

        foreach (Collider2D circle in colliders)
        {
            circle.enabled = toggleCollision;
        }
    }
    // Once a button is pressed, all the other get disabled for a brief time, and the pressed button changes its sprite
    //void toggleButtonActivity()
    //{
    //    toggleButton = !toggleButton;
    //    if (toggleButton)
    //        currentButton.GetComponent<SpriteRenderer>().sprite = enableSprite;
    //    else
    //        currentButton.GetComponent<SpriteRenderer>().sprite = disableSprite;
    //    foreach (CircleCollider2D button in buttons)
    //    {
    //        button.enabled = toggleButton;
    //    }

    //}

    //// Changes from beam to glow and vice versa
    //void checksRays()
    //{
    //    foreach (RayDetection ray in rays)
    //    {
    //        ray.testRay();
    //    }
    //}

    //// Called every time the button is pressed to check if the win condition is met
    //void Win()
    //{
    //    winProgress = 0;

    //    foreach (CheckWin circle in circles)
    //    {
    //        winProgress += circle.GetPosition();
    //    }
    //    if (winProgress >= winCondition)
    //    {
    //        toggleButtonActivity();
    //        animator.SetTrigger("isOver");
    //    }
    //}
}
