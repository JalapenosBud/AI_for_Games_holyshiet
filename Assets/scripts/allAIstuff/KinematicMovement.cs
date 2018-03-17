using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicMovement : MonoBehaviour {
    //apply force and the force causes change in kinetic enery of an object
    //this causes acceleration, tho acceleration depends on inertia of an object
    //inertia resists acceleration; higher intertia: less acceleration for same force

    //ACTUATION:
    //this takes input as a change in velocity, the kind that is applied directly in a kinematic system
    //the actuator calculates the combination of forces that it can apply, to get near as possible to desired velocity change

        //multiply acceleration by inertia to give a force.

    public struct Kinematic
    {
        //position in world space
        Vector3 position;

        //need only an angle to represent orientation
        //a scalar representation: 
        //angle is measured from positive z-axis, in a right handed direction, about the pos y axis
        //-- can also use vector representation of orientation
        //here the vector is a unit vector (length of one), in the direction the char is facing
        float orientation;

        //linear velocity
        Vector3 velocity;

        //angular velocity is how fast char's orientation is changing
        //single value, number of radians per second the orientation is changin
        //angular velocity is called rotation, since rotation suggests motion
        float rotation;

        public void UpdateCharVelAndOrientation(SteeringOutput steering)
        {
            //update pos and orientation
            position += velocity * Time.deltaTime;
            orientation += rotation * Time.deltaTime;

            //velocity and rotation
            velocity += steering.linear * Time.deltaTime;
            rotation += steering.angular * Time.deltaTime;
        }

        public float GetNewOrientation(float orientation, Vector3 vel)
        {
            //to be sure theres a velocity
            if (vel.magnitude > 0)
            {
                //calculate the orientation by using an arc tangent of
                //the velocity components
                return Mathf.Atan2(-position.x,position.z);

            }
            //else us current orientation
            else return orientation;
        }
        

    }

    //steering operate with the kinematic data
    //return accelerations that changes velocity of a character

    //move facing of character a proportion of the way toward desired direction, to smooth the motion over many frames
    //change orientation to be halfway toward its current direction of motion each frame
    public struct SteeringOutput
    {
        //calling this velocity
        public Vector3 linear;
        //rotation
        public float angular;
    }


    
}
