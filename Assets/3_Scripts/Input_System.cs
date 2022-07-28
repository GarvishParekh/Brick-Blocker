
using UnityEngine;

public class Input_System : MonoBehaviour
{
    [SerializeField] Transform player_Racket;

    [Header ("Min max values")]
    [SerializeField] float xMin;
    [SerializeField] float xMax;
    [Space]
    [SerializeField] float ymin;
    [SerializeField] float ymax;

    [Space]
    [Header ("Swipe inputs")]
    [SerializeField] bool isRight;
    [SerializeField] float swipteValue;

    // make the racket move in the box accoring to the controller's ray
    private void Update()
    {
        Ray ray = JMRSDK.InputModule.JMRPointerManager.Instance.GetCurrentRay();
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log(hit.transform.name);

            Vector3 pos = player_Racket.position;

            pos = ray.GetPoint(5);
            pos.x = Mathf.Clamp(pos.x, xMin, xMax);
            pos.y = Mathf.Clamp(pos.y, ymin, ymax);

            player_Racket.position = pos;
        }
    }

}