using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkRadiusStop = .5f;

    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;

    bool indirectInputMode = false;
    bool m_Jump;



    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            //reset currentclick target to prevent going back to location last time the G key was hit
            currentClickTarget = transform.position;
            //swop between input styles (WASD VS controller)
            indirectInputMode = !indirectInputMode;
        }
        if (indirectInputMode)
        {
            ProcessIndirectInput();
        }
        else
        {
            ProcessMouseClickMovement();
        }
        

    }

    private void ProcessIndirectInput()
    {
        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool crouch = Input.GetKey(KeyCode.C);

        if (!m_Jump)
        {
            m_Jump = Input.GetButtonDown("Jump");
        }

        Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 m_Move = v * m_CamForward + h * Camera.main.transform.right;

        m_Character.Move(m_Move, false, m_Jump);
        m_Jump = false;

    }

    private void ProcessMouseClickMovement()
    {
        if (Input.GetMouseButton(0))
        {
            //print("Cursor raycast hit - " + cameraRaycaster.layerHit); // can use for quick debug

            switch (cameraRaycaster.layerHit)
            {

                case Layer.Walkable:
                    currentClickTarget = cameraRaycaster.hit.point;
                    break;

                case Layer.Enemy:
                    print("not going to the Enemy for now");
                    break;

                default:
                    print("layer not set in PlayerMovement.cs switch in FixedUpdate!");
                    break;


            }
        }

        if (!m_Jump)
        {
            m_Jump = Input.GetButtonDown("Jump");
        }

        var playerToClickPoint = currentClickTarget - transform.position;
        
        if (playerToClickPoint.magnitude >= walkRadiusStop)
        {
            m_Character.Move(playerToClickPoint, false, m_Jump);
            m_Jump = false;
        }
        else
        {
            m_Character.Move(Vector3.zero, false, m_Jump);
            currentClickTarget = transform.position;
            m_Jump = false;
        }
    }
}

