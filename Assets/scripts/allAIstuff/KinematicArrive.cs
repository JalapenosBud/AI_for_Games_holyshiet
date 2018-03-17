using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicArrive  {

    StaticMovement character;
    StaticMovement target;

    //max speed
    float maxSpeed;

    float radius;

    float timeToTarget = 0.25f;

    public void GetSteering()
    {
        var steering = new KinematicMovement.SteeringOutput();
        var kine = new KinematicMovement.Kinematic();
        //dir to target
        steering.linear = target.position - character.position;

        if(steering.linear.magnitude < radius)
        {
            return;
        }

        steering.linear /= timeToTarget;

        if(steering.linear.magnitude > maxSpeed)
        {
            steering.linear.Normalize();
            steering.linear *= maxSpeed;
        }

        character.orientation = kine.GetNewOrientation(character.orientation, steering.linear);

        steering.angular = 0;

    }
}
