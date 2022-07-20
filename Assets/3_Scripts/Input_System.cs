using UnityEngine;
using JMRSDK.InputModule;

public class Input_System : MonoBehaviour
{
    [SerializeField] Transform racket;

    private void Update()
    {
        Ray ray = JMRSDK.InputModule.JMRPointerManager.Instance.GetCurrentRay();
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log(hit.transform.name);
            var pos = racket.position;
            pos.z = Mathf.Clamp(racket.position.z, 0, 0);
            racket.position = pos;

            racket.position = hit.point;
        }


    }
}