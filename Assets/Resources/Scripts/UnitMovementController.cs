using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovementController : MonoBehaviour
{

    [Header("Scene Bounds")]
    public GameObject battleScene;
    private PolygonCollider2D playArea;

    // Teams
    private GameObject[] teamLeft;
    private GameObject[] teamRight;
    
    void Start()
    {
        teamLeft = GameObject.FindGameObjectsWithTag("Hero");
        teamRight = GameObject.FindGameObjectsWithTag("Monster");
        playArea = battleScene.GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        foreach(GameObject obj in teamLeft)
        {
            Unit unit = obj.GetComponent<Unit>();
            if (unit.CanAct())
            {
                unit.Move(teamLeft, teamRight);
                unit.Act(teamLeft, teamRight);
            }
        }
        foreach (GameObject obj in teamRight)
        {
            Unit unit = obj.GetComponent<Unit>();
            if (unit.CanAct())
            {
                unit.Move(teamRight, teamLeft);
                unit.Act(teamRight, teamLeft);
            }
        }
    }

    public bool MoveTo(GameObject unit, Vector3 position)
    {
        print(unit + " " + position);
        Vector3 intendedPos = new Vector3(position.x, position.y, 0);
        if (playArea.bounds.Contains(intendedPos))
        {
            unit.transform.position = position;
            return true;
        }
        return false;
    }
}
