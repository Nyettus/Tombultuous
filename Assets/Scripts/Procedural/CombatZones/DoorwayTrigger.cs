using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulBox;

public class DoorwayTrigger : MonoBehaviour
{
    [SerializeField]
    private RoomGrid[] neighbours = new RoomGrid[2];
    public GameObject UICanvas;
    [SerializeField] private Animator anim;
    [SerializeField] private ParticleSystem[] openParticles;
    public ParticleSystem slamParticles;

    private void Start()
    {
        FindNeighbours();
        UICanvas.SetActive(false);
        CombatZone.OnRoomEnter += EnableMiniMap;
        CombatZone.OnCombatStarted += CloseDoors;
        CombatZone.OnCombatEnded += OpenDoors;


    }
    private void OnDestroy()
    {
        CombatZone.OnRoomEnter -= EnableMiniMap;
        CombatZone.OnCombatStarted -= CloseDoors;
        CombatZone.OnCombatEnded -= OpenDoors;
    }

    private void FindNeighbours()
    {
        //Find above
        Vector3 locationA = transform.position + (transform.forward * (RoomManager._.roomSize / 2));
        neighbours[0] = ReturnRoom(locationA);
        //Find Below
        Vector3 locationB = transform.position - (transform.forward * (RoomManager._.roomSize / 2));
        neighbours[1] = ReturnRoom(locationB);
    }

    private RoomGrid ReturnRoom(Vector3 worldPos)
    {
        Vector2Int gridpos = PsychoticBox.ConvertWorldPosToGrid(worldPos);
        var initial = RoomManager._.RG.activeGrid.Find(room => room.position == gridpos);
        if (initial.state == RoomGrid.State.Occupied || (initial.state == RoomGrid.State.MultiGrid && initial.shape == RoomGrid.Shape._1x1 && initial.position != new Vector2Int(0, 0)))
        {
            //Debug.Log("Returned occupied: " + initial.position);
            return initial;
        }
        else if (initial.state == RoomGrid.State.MultiGrid && (initial.position != new Vector2Int(0, 0)))
        {
            RoomGrid multiHost = RoomManager._.RG.activeGrid.Find(room => (room.state == RoomGrid.State.Occupied && room.roomID == initial.roomID));
            //Debug.Log("Returned Multigrid: " + multiHost.position);
            return multiHost;
        }
        else
        {
            RoomGrid origin = RoomManager._.RG.activeGrid.Find(room => room.position == new Vector2Int(0, 0));
            return origin;
        }

    }
    //Allow null for testing purposes
    private bool RelevantRoom(RoomGrid Grid)
    {
        if (Grid == null) return true;
        return (Grid.position == neighbours[0].position || Grid.position == neighbours[1].position);
    }
    private void EnableMiniMap(RoomGrid Grid)
    {
        if (!RelevantRoom(Grid)) return;

        UICanvas.SetActive(true);

    }

    private void CloseDoors(RoomGrid Grid)
    {
        if (!RelevantRoom(Grid)) return;
        anim.Play("Close");


    }


    private void OpenDoors(RoomGrid Grid)
    {
        if (!RelevantRoom(Grid)) return;
        anim.Play("Open");
        foreach (ParticleSystem par in openParticles)
        {
            par.Play();
        }

    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKey(KeyCode.L))
            OpenDoors(null);
        if (Input.GetKey(KeyCode.K))
            CloseDoors(null);
    }
#endif
}
