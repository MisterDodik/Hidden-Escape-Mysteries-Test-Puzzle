using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    float duration = 1.5f;

    public static PuzzleManager instance;

    [SerializeField] private Transform circleParent;
    [SerializeField] private Transform buttonParent;

    int circleCount;

    [SerializeField] private GameObject pressedTriangle;

    [SerializeField] private Sprite disableSprite;
    [SerializeField] private Sprite enableSprite;
    CircleCollider2D[] buttons;
    bool toggleButton = true;

    GameObject currentButton;

    CheckWin[] circles;
    public Sprite glowSprite;
    public Sprite beamSprite;
    RayDetection[] rays;

    int winCondition = 8;
    int winProgress = 0;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (instance == null)
            instance = this;

    }
    private void Start()
    {
        circleCount = circleParent.childCount;
        pressedTriangle.SetActive(false);

        buttons = FindObjectsByType<CircleCollider2D>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        circles = FindObjectsByType<CheckWin>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        rays = FindObjectsByType<RayDetection>(FindObjectsInactive.Include, FindObjectsSortMode.None);

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
        toggleButtonActivity();

        Circles circles = GetCircles(buttonIndex);
        StartCoroutine(RotateOverTime(circles.circle1, circles.circle2, circles.centerCircle, buttonIndex));
    }

    Circles GetCircles(int index)
    {
        return new Circles {
            circle1 = circleParent.GetChild(index % (circleCount - 1)),
            circle2 = circleParent.GetChild((index + 1) % (circleCount - 1)),
            centerCircle = circleParent.GetChild(circleCount - 1)
        };
    }

    IEnumerator RotateOverTime(Transform circle1, Transform circle2, Transform centerCircle, int index)
    {
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

            currentButton.transform.localRotation = Quaternion.Lerp(currentButtonRotation, endRotation, elapsedTime/duration);

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
        toggleButtonActivity();

        checksRays();
        Win();
    }


    // Once a button is pressed, all the other get disabled for a brief time, and the pressed button changes its sprite
    void toggleButtonActivity()
    {
        toggleButton = !toggleButton;
        if (toggleButton)
            currentButton.GetComponent<SpriteRenderer>().sprite = enableSprite;
        else
            currentButton.GetComponent<SpriteRenderer>().sprite = disableSprite;
        foreach (CircleCollider2D button in buttons)
        {
            button.enabled = toggleButton;
        }

    }

    // Changes from beam to glow and vice versa
    void checksRays()
    {
        foreach(RayDetection ray in rays)
        {
            ray.testRay();
        }
    }

    // Called every time the button is pressed to check if the win condition is met
    void Win()
    {
        winProgress = 0;

        foreach(CheckWin circle in circles)
        {
            winProgress += circle.GetPosition();
        }
        if(winProgress>=winCondition)
        {
            toggleButtonActivity();
            animator.SetTrigger("isOver");
        }
    }
}

