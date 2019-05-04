using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    SeekingObject,
    HeadingToObject,
    HeadingToRoom,
}

public class AIPlayer : MonoBehaviour
{
    AIState state = AIState.SeekingObject;
    Transform targetObj = null;
    Vector3 target;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] PlayerMovement movement;
    [SerializeField] PlayerInteract interact;

    public void searchForObject()
    {
        ObjectData[] objects = FindObjectsOfType<ObjectData>();
        if (objects.Length > 0)
        {
            int loopBreak = 0;
            while(targetObj == null && loopBreak++ < 10)
            {
                ObjectData obj = objects[Random.Range(0, objects.Length)];
                if (obj.getOwner() == movement.playerID)
                {
                    targetObj = obj.transform;
                    Vector3 pos = new Vector3(
                        targetObj.position.x,
                        transform.position.y,
                        targetObj.position.z);
                    target = pos;
                    agent.SetDestination(pos);
                    state = AIState.HeadingToObject;
                }
            }
        }
    }

    void findRoom()
    {
        target = FindObjectOfType<RoomIdentifier>().getSpotInRoom(transform.position.y);
        agent.SetDestination(target);
    }

    private void Update()
    {
        switch (state)
        {
            case AIState.SeekingObject:
                if (targetObj == null)
                    searchForObject();
                break;
            case AIState.HeadingToObject:
                if (Vector3.Distance(transform.position, target) < 0.3f)
                {
                    interact.GrabObject(targetObj.gameObject);
                    findRoom();
                    state = AIState.HeadingToRoom;
                }
                break;
            case AIState.HeadingToRoom:
                if (Vector3.Distance(transform.position, target) < 0.3f)
                {
                    interact.DropObject(this);
                    targetObj = null;
                }
                break;
        }
    }
    public void setState(AIState _state)
    {
        state = _state;
    }
}
