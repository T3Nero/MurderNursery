using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuttroCutscene : MonoBehaviour
{
    public GameObject gameManager;

    // stores the location for the camera to transition too
    public GameObject drewPos, scarletPos, chasePos, JBPos, eddiePos, badEndingPos;

    // List of all posible positions camera can transition to
    private List<GameObject> positions = new();




    // Start is called before the first frame update
    void Start()
    {
        positions.Add(drewPos);
    }

    // Update is called once per frame
    void Update()
    {
        MoveCameraPosition();
    }

    public void ChangePosition(int index)
    {
        if(positions.Count > 0)
        {
            positions.RemoveAt(0);
        }

        switch(index)
        {
            case 0:
                positions.Add(drewPos);
                break;
            case 1:
                positions.Add(eddiePos);
                break;
            case 2:
                positions.Add(chasePos);
                break;
            case 3:
                positions.Add(scarletPos);
                break;
            case 4:
                positions.Add(JBPos);
                break;
            case 5:
                positions.Add(badEndingPos);
                break;
        }
    }

    void MoveCameraPosition()
    {
        if(positions.Count > 0 && gameManager.GetComponent<Conclusion>().inEnding)
        {
                transform.SetPositionAndRotation(Vector3.Lerp(transform.position,
                    positions[0].transform.position, 2.0f * Time.deltaTime), Quaternion.Lerp(transform.rotation,
                    positions[0].transform.rotation, 2.0f * Time.deltaTime));
        }
    }
}
