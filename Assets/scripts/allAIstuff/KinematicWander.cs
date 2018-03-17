using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicWander {

    StaticMovement character;

    float maxSpeed;

    float maxRotation;

    void GetSteering()
    {
        var steering = new KinematicMovement.SteeringOutput();

        steering.linear = new Vector3(character.orientation, 0, character.orientation) * maxSpeed;

        steering.angular = RandomBinomial() * maxRotation;
    }

    int RandomBinomial()
    {
        return Random.Range(-1, 1);
    }
}
