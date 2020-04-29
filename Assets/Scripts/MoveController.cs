using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{

    [Header("Passenger's Attributes")]

    public float speed = 5f;
    public float sprint = 15f;
    public Transform MovePoint;

    [Header("Passenger's State")]
    // These should stay public -> being used from another script (PassArray)
    public bool isControlled = false;
    public GameObject Hora;
    public bool isSeated = false;

    //definition of state machine
    private enum State { moving, takeAseat, seated };
    private State state;

    //[Header("Seats Attributes")]
    //private List<Transform> Rows = new List<Transform>(5);
    //private int _selectedRow = 2;
    //private int _selectedColumn = 0;
    //private float xToSnap = 0;

    public bool lastOne = false;

    //private bool _blockMoveUp = false;
    //private bool _blockMoveDown = false;
    //public GameObject m_ObjectCollider;

   public GridHelper gridHelper;  // set by BoardViewManager

    // Start is called before the first frame update
    void Start()
    {
        state = State.moving;
    }

    void Update()
    {
        switch (state)
        {
            // Moving down the aisle
            case State.moving:

                // move player to the right constantly
                transform.position = Vector3.MoveTowards(transform.position, MovePoint.position, speed * Time.deltaTime);

                // turn "hora" for control indicator on \ off
                Hora.gameObject.SetActive(isControlled);

                // make the controlled player sprint
                if (Input.GetKeyDown(KeyCode.D) && isControlled)
                {
                    transform.position = Vector3.MoveTowards(transform.position, MovePoint.position, speed * sprint * Time.deltaTime);
                }
            
                if (transform.position.x > 7.4f)
                {
                    // the player reached the end of the aisle, so stop movement
                    isSeated = true;
                    state = State.seated;
                }

                if (isControlled)
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        state = State.takeAseat;
                        TakeASeat("w");
                    }
                    else if (Input.GetKeyDown(KeyCode.S))
                    {
                        state = State.takeAseat;
                        TakeASeat("s");

                    }
                }

                break;

            // this is the state in which we control the player.
            case State.takeAseat:

                if (Input.GetKeyDown(KeyCode.W))
                {
                    TakeASeat("w");
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    TakeASeat("s");
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    if (gridHelper.InAisle())
                    {
                        transform.Translate(Vector2.right * speed * sprint * Time.deltaTime);
                    }
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (!gridHelper.InAisle())
                    {
                        Hora.gameObject.SetActive(false);
                        //_seatTaken.gameObject.GetComponent<SeatLogic>().seated = true;
                        isControlled = false;
                        isSeated = true;
                        gridHelper.PlayerHasTakenASeat();
                        //m_ObjectCollider.GetComponent<BoxCollider2D>().isTrigger = true;
                        //this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                        state = State.seated;
                    }
                }

                break;

            //this is the final state.
            case State.seated:

                if (lastOne)
                {
                    GameObject controller = GameObject.Find("Controller");
                    controller.GetComponent<GameTimer>().hasFinished = true;
                }

                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // when colliding with a cellView, change passenger position
        // since we controll the movement, collision will occur only when expected
        CellView cellView = collision.gameObject.GetComponent<CellView>();
        if (cellView != null)
        {
            gridHelper.playerPosition = cellView.cellData.position;
        }
    }

    private void TakeASeat(string keyDown)
    {
        if (keyDown == "w")
        {
            if (gridHelper.CanMoveUp())
            { 
                transform.position = gridHelper.GetMoveUpPosition(); 
            }
        }
        else if (keyDown == "s")
        {
            if (gridHelper.CanMoveDown())
            {
                transform.position = gridHelper.GetMoveDownPosition();
            }
        }

        // after moving there will be a trigger with the cellview that the player moved to
        // and then the current player position will be updated
    }
}
