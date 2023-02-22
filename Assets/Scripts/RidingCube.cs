using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidingCube : MonoBehaviour
{
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void IncerementCubeVolume(float value)
    {

        if (value < 0)
        {
            _playerController.DropBoats(this);

        }
        else 
        {
            int cubesCount = _playerController.cubes.Count;
            
            transform.localPosition = new Vector3(transform.localPosition.x, -1f * (cubesCount - 1) + -0.5f * value, transform.localPosition.z);
            transform.localScale = new Vector3(value, value, value);
            
            //transform.localPosition = new Vector3(transform.localPosition.x, -0.5f * (cubesCount - 1) + -0.25f * value, transform.localPosition.z);
            //transform.localScale = new Vector3(0.5f * value, 0.5f * value * transform.localScale.y, 0.5f * value);
        }
    }
}
