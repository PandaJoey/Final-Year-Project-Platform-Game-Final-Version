using System;
using UnityEngine;

/*
 * Class used by the main camera to allow following
 */
namespace UnityStandardAssets._2D {
    public class CameraFollow : MonoBehaviour {

        // used to find the target of the object you wish to follow
        public Transform target;                   
        // determines how fast the camera reacts 
        public float damping = 0.1f;
        // determints how far ahead in the x direction the camera looks
        public float lookAheadFactor = 2f;          
        // determines how fast the camaera swaps direction
        public float lookAheadReturnSpeed = 2f;     
        // determines how far the camaera moves based on character position
        public float lookAheadMoveThreshold = 0.3f; 

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;



        // Use this for initialization
        private void Start() {
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
        }

        // Update is called once per frame
        private void Update() {
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget) {
                m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
            }
            else {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            transform.position = newPos;

            m_LastTargetPosition = target.position;
        }

    }
}
