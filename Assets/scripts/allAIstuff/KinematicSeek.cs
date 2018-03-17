using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicSeek : MonoBehaviour {

    //holds static data for character and target
    StaticMovement character;
    StaticMovement target;

    //max speed chracter can travel
    float maxSpeed;

    public KinematicMovement.SteeringOutput GetSteering()
    {
        //output
        var steering = new KinematicMovement.SteeringOutput();
        var kineMov = new KinematicMovement.Kinematic();

        //TODO: for a FLEE METHOD
        //reverse to: character.position - target.position
        //direction to the target
        steering.linear = target.position - character.position;

        //velocity along this direction, at full speed
        steering.linear.Normalize();
        steering.linear *= maxSpeed;

        //face in dir we want to move
        character.orientation = kineMov.GetNewOrientation(character.orientation, steering.linear);

        steering.angular = 0;
        return steering;
    }
}
