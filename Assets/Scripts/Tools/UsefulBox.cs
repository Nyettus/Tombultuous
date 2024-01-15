using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UsefulBox
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public static class MurderBag
    {
        /// <summary>
        /// Calculate a random point in a torus in normalised cartesian with magnitude (formatted x,y,magnitude)
        /// </summary>
        /// <param name="lowerBound">The first integer.</param>
        /// <param name="upperBound">The second integer.</param>
        /// <returns>The Vector3 formatted (direction x, direction z, magnitude.</returns>
        public static Vector3 RandomTorusCoords(float lowerBound, float upperBound)
        {
            var randomDir = Random.insideUnitCircle.normalized;
            var randomDist = Random.Range(lowerBound, upperBound);
            return new Vector3(randomDir.x, randomDir.y, randomDist);
        }

        /// <summary>
        /// Calculate the navmesh position of point in a torus around the starting position
        /// </summary>
        /// <param name="startLocation"></param>
        /// <param name="torusCoords"></param>
        /// <returns>The navmesh point or the starting location if no navmesh point is found </returns>
        public static Vector3 PositionInTorus(Vector3 startLocation, Vector3 torusCoords)
        {
            var flatStart = new Vector2(startLocation.x, startLocation.z);
            var randomDir = new Vector2(torusCoords.x, torusCoords.y);
            var randomDist = torusCoords.z;
            var v2position = flatStart + randomDir * randomDist;
            var worldspacev3 = new Vector3(v2position.x, startLocation.y, v2position.y);
            return worldspacev3;
        }

        /// <summary>
        /// Predict the location of a target perfectly not performant maybe crashes?
        /// </summary>
        /// <param name="targetPos"></param>
        /// <param name="targetVel"></param>
        /// <param name="hostPos"></param>
        /// <param name="hostVel"></param>
        /// <returns>Vector 3 Predicted position of the target</returns>
        public static Vector3 PerfectPredictLocation(Vector3 targetPos, Vector3 targetVel, Vector3 hostPos, float hostVel)
        {
            Vector3 displacement = targetPos - hostPos;
            float targetMoveAngle = Vector3.Angle(-displacement, targetVel) * Mathf.Deg2Rad;
            //if the target is stopping or if it is impossible for the projectile to catch up with the target (Sine Formula)
            if (targetVel.magnitude == 0 || targetVel.magnitude > hostVel && Mathf.Sin(targetMoveAngle) / hostVel > Mathf.Cos(targetMoveAngle) / targetVel.magnitude)
            {
                Debug.Log("Position prediction is not feasible.");
                return targetPos;
            }
            //also Sine Formula
            float shootAngle = Mathf.Asin(Mathf.Sin(targetMoveAngle) * targetVel.magnitude / hostVel);
            var returnValue = targetPos + targetVel * displacement.magnitude / Mathf.Sin(Mathf.PI - targetMoveAngle - shootAngle) * Mathf.Sin(shootAngle) / targetVel.magnitude;
            return returnValue;
        }


        /// <summary>
        /// Quick and dirty position prediction
        /// </summary>
        /// <param name="targetPos"></param>
        /// <param name="targetVel"></param>
        /// <param name="hostPos"></param>
        /// <param name="hostVel"></param>
        /// <returns></returns>
        public static Vector3 RoughPredictLocation(Vector3 targetPos, Vector3 targetVel, Vector3 hostPos, float hostVel, float lerpPercent = 1)
        {
            float distance = Vector3.Distance(targetPos, hostPos);
            float targetSpeed = targetVel.magnitude;
            Vector3 moveDir = targetVel.normalized;

            float directionMultiplier = (targetSpeed / hostVel)*distance*lerpPercent;


            Vector3 predPos = targetPos + (moveDir * directionMultiplier);
            return predPos;

        }

        /// <summary>
        /// Rotate a torus coordinate by angle per seconds
        /// </summary>
        /// <param name="TorusCoords"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector3 OrbitCoords(Vector3 TorusCoords, float angle)
        {
            float R = TorusCoords.z;
            float theta = Mathf.Atan2(TorusCoords.y, TorusCoords.x);
            theta += (angle * Mathf.Deg2Rad * Time.deltaTime);
            Vector3 returnVec = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), R);

            return returnVec;

        }

        /// <summary>
        /// The optimal degrees per second orbit for an object
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static float OrbitOptimalDeg(float radius, float speed)
        {
            float circ = radius * 2 * Mathf.PI;
            float degPerSec = 360 / (circ / speed);
            return degPerSec;
        }


    }

    public static class PsychoticBox
    {
        public static string ConvertToPercent(float input, string format = "00.00")
        {
            string asPercent = (input * 100).ToString(format) + "%";
            return asPercent;
        }
    }


}

