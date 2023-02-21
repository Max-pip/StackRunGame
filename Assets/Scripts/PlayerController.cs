using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("Player`s settings")]
    [SerializeField] private float limitX;
    [SerializeField] private float xSpeed;
    [SerializeField] private float runningSpeed;

    [SerializeField] private GameObject ridingBoatPrefab;

    [SerializeField] private GameObject _cubePickupEffect;

    [SerializeField] private UnityEvent _dead;

    public List<RidingCube> cubes;

    private bool _canMoved;

    private PlayerAnimation _animPlayer;

    //For Input
    private float newX;
    private float touchXDelta = 0;

    private void Awake()
    {
        _animPlayer = GetComponentInChildren<PlayerAnimation>();
        AddBoatStart();
    }

    private void Start()
    {
        _canMoved = true;
    }

    void Update()
    {
        if (_canMoved)
        {
#if UNITY_EDITOR
            PlayerControl();
#endif

#if UNITY_ANDROID
            PlayerControlMobile();
#endif
        }
    }

    public void AddBoatStart() 
    {
        IncrementBoatVolume(1f);
    }

    private void PlayerControl()
    {
        if (Input.GetMouseButton(0))
        {
            touchXDelta = Input.GetAxis("Mouse X");
        }

        newX = transform.position.x + xSpeed * touchXDelta * Time.deltaTime;
        newX = Mathf.Clamp(newX, -limitX, limitX);

        Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z + runningSpeed * Time.deltaTime);
        transform.position = newPosition;
    }

    private void PlayerControlMobile()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {

            touchXDelta = Input.GetTouch(0).deltaPosition.x;
        }

        newX = transform.position.x + xSpeed * touchXDelta * Time.deltaTime;
        newX = Mathf.Clamp(newX, -limitX, limitX);

        Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z + runningSpeed * Time.deltaTime);
        transform.position = newPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickCube") 
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.3f, transform.localPosition.z);
            GameObject pickupEffect = (GameObject)Instantiate(_cubePickupEffect, transform.position, transform.rotation);
            Destroy(pickupEffect, 1f);
            IncrementBoatVolume(1f);  
            Destroy(other.gameObject);    

        }
        if (other.tag == "Wall")   
        {
            IncrementBoatVolume(-1f);  
            if (cubes.Count == 0)
            {
                _canMoved = false;
                _dead?.Invoke();
            }
        }
    }

    public void IncrementBoatVolume(float value)
    {
        _animPlayer.Jump();
        if (value > 0)
        {
            CreateBoat(value);
        }

        else if (value < 0 && cubes.Count != 0)
        {
            cubes[cubes.Count - 1].IncerementCubeVolume(value);
        }

    }

    public void CreateBoat(float value)
    {
        RidingCube createdBoat = Instantiate(ridingBoatPrefab, transform).GetComponent<RidingCube>(); 

        cubes.Add(createdBoat);
        createdBoat.IncerementCubeVolume(value);

    }

    public void DropBoats(RidingCube boat)
    {
        cubes.Remove(boat);
        boat.gameObject.transform.parent = null;

    }
}
