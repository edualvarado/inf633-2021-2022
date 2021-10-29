using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerControllerIKFeetPlacementTask : MonoBehaviour
{
    public Animator anim;
    public CharacterController controller;

    private float gravity;
    private Vector3 characterVelocity; // Measures the velocity of the character at each time.

    // To check if it is grounded.
    [Header("External Method to check isGrounded")]
    [Range(0, 2f)] public float groundDistance = 0.2f;
    public LayerMask ground; // In Inspector, Unity capitalizes the label to "Ground".
    [SerializeField] private bool isGrounded;
    private Transform groundChecker;

    [Header("Feet Positions")]
    public Vector3 rightFootPosition;
    public Vector3 leftFootPosition;
    public Vector3 rightFootIKPosition;
    public Vector3 leftFootIKPosition;

    private Quaternion leftFootIKRotation, rightFootIKRotation;
    private float lastPelvisPositionY, lastRightFootPositionY, lastLeftFootPositionY;

    [Header("Feet Grounder")]
    public bool enableFeetIK = true;
    [Range(0, 20f)] [SerializeField] private float heightFromGroundRaycast = 1.14f;
    [Range(0, 20f)] [SerializeField] private float raycastDownDistance = 1.5f;
    [SerializeField] private LayerMask environmentLayer;
    [SerializeField] private float pelvisOffset = 0f;
    [Range(0, 1f)] [SerializeField] private float pelvisUpAndDownSpeed = 0.28f;
    [Range(0, 1f)] [SerializeField] private float feetToIKPositionSpeed = 0.5f;

    public string leftFootAnimVariableName = "LeftFootCurve";
    public string rightFootAnimVariableName = "RightFootCurve";

    public bool useProIKFeature = false;
    public bool showSolverDebug = true;

    // Start is called before the first frame update
    void Start()
    {
        // For groundCheck.
        groundChecker = this.transform.GetChild(transform.childCount - 1); // Sphere must be the last child of the parent.
        gravity = Physics.gravity.y; // Uses gravity from physics system.

        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Ground Check: isGrounded is true if the "imaginary" sphere on the empty gameObject hits some collider. If so, gravity stops increasing.
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, ground, QueryTriggerInteraction.Ignore);
        if (isGrounded && characterVelocity.y < 0)
            characterVelocity.y = 0f;
        //Debug.Log("Is it grounded? " + isGrounded);
        //Debug.Log("Velocity (Y) inc. Gravity: " + characterVelocity.y);

        // If it not grounded , apply gravity and move in Y.
        characterVelocity.y += gravity * Time.deltaTime;
        controller.Move(characterVelocity * Time.deltaTime);
    }

    #region FeetGrounding

    /// <summary>
    /// Update the AdjustFeetTarget method and also find the position of each foot inside our Solver Position.
    /// </summary>
    private void FixedUpdate()
    {
        if(enableFeetIK == false) { return; }
        if(anim == null) { return; }

        AdjustFeetTarget(ref rightFootPosition, HumanBodyBones.RightFoot);
        AdjustFeetTarget(ref leftFootPosition, HumanBodyBones.LeftFoot);

        // Find a raycast to the ground to find positions.
        FeetPositionSolver(rightFootPosition, ref rightFootIKPosition, ref rightFootIKRotation); // Handle the solver for right foot
        FeetPositionSolver(leftFootPosition, ref leftFootIKPosition, ref leftFootIKRotation); // Handle the solver for left foot
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (anim == null) { return; }

        MovePelvisHeight();

        /* 
         * You need to set to 1 the IK weight in the animator controller for the position of each foot, right and left one. 
         * Remember, weight = 1 means in this case that such foot will be fully placed in terms of position (not rotation) to match the ground, 
         * and the walking animation will not have any effect in his movement.
         * Hint: Search in the API for a pre-built method for the Animator class to assign IK weights to different body parts of an humanoid rig (anim.Set...).
         */

        // START TODO ###################


        // END TODO ###################


        /* 
         * In this part, we have a boolean variable (useProIKFeature) that can be checked in the inspector during run-time.
         * This boolean should perform a similar operation compared to the previous task. However, there are two differences:
         * 
         * 1. Now, we want to change the IK weight that will vary the rotation of the feet, not its position (different built-in function).
         * 2. Instead of assigning a weight of 1, you will set a variable parameter. This parameter is defined in the Animator Controller that the character has assigned to it (Parameters tab in Animator window).
         *    You will need to retrieve by name such variable (float) from the Animator. 
         *    Hint: Check the Animator Controller or the beginning of the script to know the variables.
         */

        if (useProIKFeature)
        {
            // START TODO ###################


            // END TODO ###################
        }


        /* 
         * Finally, we need to move both feet to the desired IK position and rotation to match the ground.
         * To do so, you will just need to call the function "MoveFeetToIKPoint" once for each foot with the correct parameters:
         * - AvatarIKGoal foot: Each foot, which are already built in the enum variable.
         * - Vector3 positionIKHolder: Desired IK position for each foot.
         * - Quaternion rotationIKHolder: Desired IK rotation for each foot.
         * - ref float lastFootPositionY: Previous foot position in Y passed by reference.
         */

        // START TODO ###################


        // END TODO ###################

    }

    #endregion

    #region FeetGroundingMethods

    void MoveFeetToIKPoint(AvatarIKGoal foot, Vector3 positionIKHolder, Quaternion rotationIKHolder, ref float lastFootPositionY)
    {
        /* 
         * Before moving the feet to the new IK goal position (positionIKHolder), you need to retrieve the current IK position of each foot.
         * Create a Vector3 called "targetIKPosition" and save the current IK position for the foot you are providing to the function.
         * Hint: Search in the API for a pre-built method for the Animator class to get IK positions for different body parts (anim.Get...).
         */

        // START TODO ###################

        Vector3 targetIKPosition = Vector3.zero;

        // END TODO ###################


        // If there is a IK target in a different position (not 0 locally) than the position where we have our foot currently.
        if (positionIKHolder != Vector3.zero)
        {
            // Convert the world coordinates for current/target foot positions to local coordinates with respect to the character.
            targetIKPosition = transform.InverseTransformPoint(targetIKPosition);
            positionIKHolder = transform.InverseTransformPoint(positionIKHolder);


            /* 
            * Here, you will have to use the Mathf.Lerp function to calculate the necessary translation in Y to move the last foot Y position to
            * the target IK position, by a particular speed (which is "feetToIKPositionSpeed"). 
            * Save the necessary Y translation in a float called "yVariable".
            * Hint: The target IK position comes in a Vector3. Remember to access only the Y coordinate.
            */

            // START TODO ###################

            float yVariable = 0;

            // END TODO ###################

            // Add this desired translation in Y to our current feet position.
            targetIKPosition.y += yVariable;

            // We update the last foot position in Y.
            lastFootPositionY = yVariable;

            // Convert the current foot position to world coordinates.
            targetIKPosition = transform.TransformPoint(targetIKPosition);

            // Set the new goal rotation (world coordinates) for the foot.
            anim.SetIKRotation(foot, rotationIKHolder);
        }

        // Set the new goal position (world coordinates) for the foot.
        anim.SetIKPosition(foot, targetIKPosition);
    }

    /// <summary>
    /// Adapt height of pelvis
    /// </summary>
    private void MovePelvisHeight()
    {
        if(rightFootIKPosition == Vector3.zero || leftFootIKPosition == Vector3.zero || lastPelvisPositionY == 0)
        {
            lastPelvisPositionY = anim.bodyPosition.y;
            return;
        }

        float leftOffsetPosition = leftFootIKPosition.y - transform.position.y;
        float rightOffsetPosition = rightFootIKPosition.y - transform.position.y;

        float totalOffset = (leftOffsetPosition < rightOffsetPosition) ? leftOffsetPosition: rightOffsetPosition;

        // Hold new pelvis position where we want to move to.
        // Move from last to new position with certain speed.
        Vector3 newPelvisPosition = anim.bodyPosition + Vector3.up * totalOffset;
        newPelvisPosition.y = Mathf.Lerp(lastPelvisPositionY, newPelvisPosition.y, pelvisUpAndDownSpeed);

        // Update current body position.
        anim.bodyPosition = newPelvisPosition;

        // Now the last known pelvis position in Y is the current body position in Y.
        lastPelvisPositionY = anim.bodyPosition.y;
    }

    /// <summary>
    /// Locate the feet position via a raycast and then solving.
    /// </summary>
    /// <param name="fromSkyPosition"></param>
    /// <param name="feetIKPositions"></param>
    /// <param name="feetIKRotations"></param>
    private void FeetPositionSolver(Vector3 fromSkyPosition, ref Vector3 feetIKPositions, ref Quaternion feetIKRotations)
    {
        // To store all the info regarding the hit of the ray
        RaycastHit feetoutHit;

        // To visualize the ray
        if (showSolverDebug)
        {
            Debug.DrawLine(fromSkyPosition, fromSkyPosition + Vector3.down * (raycastDownDistance + heightFromGroundRaycast), Color.yellow);
        }

        // If the ray, starting at the sky position, goes down certain distance and hits an environment layer.
        if (Physics.Raycast(fromSkyPosition, Vector3.down, out feetoutHit, raycastDownDistance + heightFromGroundRaycast, environmentLayer))
        {
            // Position the new IK feet positions parallel to the sky position, and put them where the ray intersects with the environment layer.
            feetIKPositions = fromSkyPosition;
            feetIKPositions.y = feetoutHit.point.y + pelvisOffset;
            // Creates a rotation from the (0,1,0) to the normal of where the feet is placed it in the terrain.
            feetIKRotations = Quaternion.FromToRotation(Vector3.up, feetoutHit.normal) * transform.rotation;

            return;
        }

        feetIKPositions = Vector3.zero; // If we reach this, it didn't work.
    }

    private void AdjustFeetTarget(ref Vector3 feetPositions, HumanBodyBones foot)
    {
        // Takes the Vector3 transform of that human bone id.
        feetPositions = anim.GetBoneTransform(foot).position;
        feetPositions.y = transform.position.y + heightFromGroundRaycast;
    }

    #endregion


}
