using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public float limitX;
    public float xSpeed;
    public float runningSpeed;

    public GameObject ridingBoatPrefab;

    public List<RidingBoat> boats;    // list of Boat

    public Rigidbody m_Rigidbody;  // reference to the rigidbody

    private bool _canMoved;

    private PlayerAnimation _animPlayer;

    [SerializeField] private UnityEvent _dead;

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
            PlayerControl();
        }
    }

    public void AddBoatStart()   // add boat method
    {
        IncrementBoatVolume(1f);
        //anim.SetTrigger("idle");
    }

    private void PlayerControl()        // control the player
    {
        float newX;
        float touchXDelta = 0;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {

            touchXDelta = Input.GetTouch(0).deltaPosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            touchXDelta = Input.GetAxis("Mouse X");
        }

        newX = transform.position.x + xSpeed * touchXDelta * Time.deltaTime;
        newX = Mathf.Clamp(newX, -limitX, limitX);

        Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z + runningSpeed * Time.deltaTime);
        transform.position = newPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickCube")      // when the player colided with tho boat
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.2f, transform.localPosition.z);
            IncrementBoatVolume(1f);   // increase Boat Volume By 1
            Destroy(other.gameObject);    // destroy the boat

        }
        if (other.tag == "Wall")   // when the player colided with the Obstacles
        {
            IncrementBoatVolume(-1f);   // decrease boat volume by 1
            if (boats.Count == 0)
            {
                _canMoved = false;
                _dead?.Invoke();
                //GameManager.instance.LoseGame();   // if the boat count equal O lose the game

            }
        }
    }

    public void IncrementBoatVolume(float value)       // increment boat volume method
    {
        _animPlayer.Jump();
        if (value > 0)
        {
            CreateBoat(value);
        }

        else if (value < 0 && boats.Count != 0)
        {
            boats[boats.Count - 1].IncerementCubeVolume(value);
        }

    }

    public void CreateBoat(float value)      // creat boat method
    {
        RidingBoat createdBoat = Instantiate(ridingBoatPrefab, transform).GetComponent<RidingBoat>();    // instantiat the boat for player

        boats.Add(createdBoat);
        createdBoat.IncerementCubeVolume(value);

    }

    public void DropBoats(RidingBoat boat) // when remove tho boat
    {
        boats.Remove(boat);
        boat.gameObject.transform.parent = null;

    }
}
