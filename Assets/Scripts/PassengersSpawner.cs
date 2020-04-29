using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengersSpawner : MonoBehaviour
{
    [Header("List's / Level Settings")]

    public GameObject passenger;
    public Transform startPosition;
    public int totalPassengers;
    public List<GameObject> passengersList;

    public float spawnDelayStart = 7f;
    private float spawnDelay;

    private GameObject currentPassenger;

    public bool hasStarted = false; // set when first passenger is instanciated
    private bool hasFinished = false;

    public IEnumerator LaunchPassengers(GridHelper dataGridHelper)
    {
        yield return new WaitForSeconds(2);

        spawnDelay = spawnDelayStart;

        //makes sure they match length
        for (int i = 0; i < totalPassengers; i++)
        {
            spawnDelay = spawnDelayStart;

            GameObject passengerInstance = Instantiate(passenger, new Vector3(startPosition.position.x, startPosition.position.y, startPosition.position.z), Quaternion.identity) as GameObject;

            // pass the DataGridHelper to the passenger MoveController
            MoveController moveController = passengerInstance.GetComponent<MoveController>();
            moveController.gridHelper = dataGridHelper;

            // save the new instance in the list
            passengersList.Add(passengerInstance);

            hasStarted = true;
            yield return new WaitForSeconds(spawnDelay);
        }

        hasFinished = true;
    }


    void Update()
    {
        if (hasStarted && passengersList.Count > 0)
        {
            ChangeControl();
        }

        if (hasFinished)
        {
            currentPassenger.GetComponent<MoveController>().lastOne = true;
        }
    }

    void ChangeControl()
    {
        currentPassenger = passengersList[0];
        currentPassenger.GetComponent<MoveController>().isControlled = true;

        if (currentPassenger.GetComponent<MoveController>().isSeated == true)
        {
            // remove sitted passenger from list
            passengersList.RemoveAt(0);
            
            if (passengersList.Count > 0)
            {
                currentPassenger = passengersList[0];
            }
            
            // spawn immediatelly
            spawnDelay = 0;
        }
    }
}
