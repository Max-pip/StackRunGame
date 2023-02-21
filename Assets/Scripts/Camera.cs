using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private GameObject _object; //An object camera will follow
    [SerializeField] private Vector3 _distanceFromObject; // Camera's distance from the object

    private void LateUpdate() //Works after all update functions called
    {
        Vector3 positionToGo = new Vector3(0,0, _object.transform.position.z) + _distanceFromObject; //Target position of the camera
        Vector3 smoothPosition = Vector3.Lerp(a: transform.position, b: positionToGo, t: 0.125F); 
        transform.position = smoothPosition;
        //transform.LookAt(_object.transform.position); //Camera will look(returns) to the object
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
