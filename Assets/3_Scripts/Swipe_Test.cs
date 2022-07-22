using TMPro;
using JMRSDK;
using UnityEngine;
using JMRSDK.InputModule;

public class Swipe_Test : MonoBehaviour, ISwipeHandler
{
    [SerializeField] TMP_Text swipeIndicater;
    [SerializeField] bool isSwipeRight;

    public void OnSwipeCanceled(SwipeEventData eventData)
    {
        swipeIndicater.text = ("OnSwipeCanceled");
    }

    public void OnSwipeCompleted(SwipeEventData eventData)
    {
        swipeIndicater.text = ("OnSwipeCompleted");
    }

    public void OnSwipeDown(SwipeEventData eventData, float delta)
    {
        swipeIndicater.text = ("OnSwipeDown");
    }
    public void OnSwipeLeft(SwipeEventData eventData, float delta)
    {
        swipeIndicater.text = ("OnSwipeLeft");
    }
    public void OnSwipeRight(SwipeEventData eventData, float delta)
    {
        swipeIndicater.text = ("OnSwipeRight");
    }
    public void OnSwipeStarted(SwipeEventData eventData)
    {
        swipeIndicater.text = ("OnSwipeStarted");
    }
    public void OnSwipeUp(SwipeEventData eventData, float delta)
    {
        swipeIndicater.text = ("OnSwipeUp");
    }
    public void OnSwipeUpdated(SwipeEventData eventData, Vector2 delta)
    {
        swipeIndicater.text = ("OnSwipeUpdated");
    }

    private void Update()
    {
        float swipeValue = 0;
        isSwipeRight = JMRInteraction.GetSwipeRight(out swipeValue);

        Debug.Log($"{isSwipeRight}, value: {swipeValue}");
        if (isSwipeRight)
            swipeIndicater.text = ($"Update swipe right: {swipeValue}");
    }
}
