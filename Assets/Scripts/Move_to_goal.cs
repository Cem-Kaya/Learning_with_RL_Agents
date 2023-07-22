using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Move_to_goal : Agent
{

    [SerializeField] private Transform goal;
    [SerializeField] private MeshRenderer Underground;


    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-9, 9), 1.5f, Random.Range(-9, 9));
        goal.localPosition = new Vector3(Random.Range(-9, 9), 1.5f, Random.Range(-9, 9));

    }


    public override void CollectObservations(VectorSensor sensor)
    {
        // sensor.AddObservation(transform.localPosition.x );
        // sensor.AddObservation(transform.localPosition.z );
        // sensor.AddObservation(goal.localPosition.x);
        // sensor.AddObservation(goal.localPosition.z);



        //Debug.Log("got observations ");
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> my_action = actionsOut.ContinuousActions;
        my_action[0] = Input.GetAxis("Horizontal");
        my_action[1] = Input.GetAxis("Vertical");


    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float x_mov = actions.ContinuousActions[0];
        float z_mov = actions.ContinuousActions[1];

        float mov_speed = 5.0f;

        transform.localPosition += mov_speed * new Vector3(x_mov,0, z_mov) * Time.deltaTime ;
        //Debug.Log(actions.ContinuousActions[0]);
        //Debug.Log("act 1 : " + actions.DiscreteActions[0] + "act 2 :" + actions.DiscreteActions[1]);
    }

    private void FixedUpdate()
    {
        AddReward(-0.02f);
    }
    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.name == "Goal")
        {
            Underground.material.color = Color.blue;
            AddReward(50);
            EndEpisode();
        }
        else if (collision.gameObject.name == "Underground")
        {
            Underground.material.color = Color.red;
            AddReward(-2);
            EndEpisode();
        }

    }


}
