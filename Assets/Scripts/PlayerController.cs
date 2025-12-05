using UnityEditor.Search;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 10f;

    [SerializeField] private float m_jumpVelocity = 5f;

    [SerializeField] private float m_distanceToGround = 0.1f;

    [SerializeField] private LayerMask m_groundLayer;

    [SerializeField] [Min(0)] private float m_maxVert = 45.0f;
    [SerializeField] [Min(0)] private float m_minVert = -45.0f;
    [SerializeField] private float m_rotationSpeedHor = 10f;
    [SerializeField] private float m_rotationSpeedVer = 10f;

    private float m_rotationX = 0;
    private Rigidbody m_rigidbody;
    private CapsuleCollider m_collider;
    private Transform m_playerTransform;
   
    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();

        if (m_rigidbody != null)
            m_rigidbody.freezeRotation = true;

        m_collider = GetComponent<CapsuleCollider>();
       
        m_playerTransform = this.transform;
    }

    private void Update()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            m_rigidbody.AddForce(Vector3.up * m_jumpVelocity, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            Run();
        }

        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            Sit();
        }
        
        float m_deltaX = Input.GetAxis("Horizontal") * m_moveSpeed * Time.deltaTime;
        float m_deltaZ = Input.GetAxis("Vertical") * m_moveSpeed * Time.deltaTime;
        transform.Translate(m_deltaX, 0, m_deltaZ);

        m_rotationX -= Input.GetAxis("Mouse Y") * m_rotationSpeedVer;
        m_rotationX = Mathf.Clamp(m_rotationX, m_minVert, m_maxVert);

        float m_delta = Input.GetAxis("Mouse X") * m_rotationSpeedHor;
        float m_rotationY = transform.localEulerAngles.y + m_delta;
        
        transform.localEulerAngles = new Vector3(m_rotationX, m_rotationY, 0);
    }

    private void Sit()
    {
        
    }

    private void Run()
    {
        float m_deltaX = Input.GetAxis("Horizontal") * m_moveSpeed * 2 * Time.deltaTime;
        float m_deltaZ = Input.GetAxis("Vertical") * m_moveSpeed * 2 * Time.deltaTime;
        transform.Translate(m_deltaX, 0, m_deltaZ);

        m_rotationX -= Input.GetAxis("Mouse Y") * m_rotationSpeedVer;
        m_rotationX = Mathf.Clamp(m_rotationX, m_minVert, m_maxVert);

        float m_delta = Input.GetAxis("Mouse X") * m_rotationSpeedHor;
        float m_rotationY = transform.localEulerAngles.y + m_delta;
        
        transform.localEulerAngles = new Vector3(m_rotationX, m_rotationY, 0);
    }

    private bool IsGrounded()
    {
        Vector3 m_capsuleBottom = new Vector3(m_collider.bounds.center.x, m_collider.bounds.min.y, m_collider.bounds.center.z);

        bool m_grounded = Physics.CheckCapsule(m_collider.bounds.center, m_capsuleBottom, m_distanceToGround, m_groundLayer, QueryTriggerInteraction.Ignore);

        return m_grounded;
    }
}
