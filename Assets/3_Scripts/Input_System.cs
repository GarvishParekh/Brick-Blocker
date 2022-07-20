using UnityEngine;

public class Input_System : MonoBehaviour
{
    [SerializeField] Transform player_Racket;

    private void Update()
    {
        Ray ray = JMRSDK.InputModule.JMRPointerManager.Instance.GetCurrentRay();
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log(hit.transform.name);

            Vector3 pos = player_Racket.position;

            pos = ray.GetPoint(5);
            pos.x = Mathf.Clamp(pos.x, -2.3f, 2.3f);
            pos.y = Mathf.Clamp(pos.y, -1.15f, 1.3f);

            player_Racket.position = pos;
        }


    }
}