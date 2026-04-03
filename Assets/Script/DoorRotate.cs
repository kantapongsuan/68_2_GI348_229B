using UnityEngine;

public class DoorRotate : MonoBehaviour
{
    public Transform doorPivot;
    public float openAngle = 90f;
    public float speed = 3f;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen = false;

    void Start()
    {
        closedRotation = doorPivot.rotation;
        openRotation = Quaternion.Euler(0, openAngle, 0) * closedRotation;
    }

    void Update()
    {
        if (isOpen)
        {
            doorPivot.rotation = Quaternion.Lerp(doorPivot.rotation, openRotation, Time.deltaTime * speed);
        }
        else
        {
            doorPivot.rotation = Quaternion.Lerp(doorPivot.rotation, closedRotation, Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpen = false;
        }
    }
}