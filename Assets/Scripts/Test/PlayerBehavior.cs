using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 50f;
    

    public float jumpVelocity = 5f;

    public float distanceToGround = 0.1f;

    public LayerMask groundLayer;

    public float maxVert = 45.0f;
    public float minVert = -45.0f;

    private float _rotationX = 0;

    private Rigidbody _rb;

    private CapsuleCollider _col;

    public float _rotationSpeedHor = 10f;
    public float _rotationSpeedVer = 10f;

    public GameObject weaponPrefab;

    public GameObject machinGun;

    private GameObject weaponPrefab2;
    public Vector3 wTransform = new Vector3(0.7f, 0f, 1f);

    private Transform _playerTransform;

    private bool haveGun = false;






    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        if (_rb != null)
            _rb.freezeRotation = true;


        _col = GetComponent<CapsuleCollider>();
       
        //weaponPrefab.SetActive(false);

        _playerTransform = this.transform;

    }

    private void Update()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);

            
        }

        float deltaX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float deltaZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(deltaX, 0, deltaZ);

        _rotationX -= Input.GetAxis("Mouse Y") * _rotationSpeedVer;
        _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);

        float delta = Input.GetAxis("Mouse X") * _rotationSpeedHor;
        float _rotationY = transform.localEulerAngles.y + delta;

        transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);

        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);

        return grounded;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Gun")
        {
            
            if (haveGun)
            {
                Destroy(weaponPrefab2);
            }

            GameObject newWeapon = Instantiate(weaponPrefab, transform.position, transform.rotation);
            newWeapon.transform.parent = transform;
            weaponPrefab2 = newWeapon;
            Debug.LogFormat("Ooo yee!!!");
        }


        if (collision.gameObject.name == "MachineGun")
        {
            
            if (haveGun)
            {
                Destroy(weaponPrefab2);
            }

            GameObject newWeapon = Instantiate(machinGun, transform.position, transform.rotation);
            newWeapon.transform.parent = transform;
            weaponPrefab2 = newWeapon;
            Debug.LogFormat("Ooo yee!!! I have a MACHIN GUN!!!");
        }
    }

    void LateUpdate()
    {

        if (weaponPrefab2 != null)
        {
            
                haveGun = true;
            
            weaponPrefab2.transform.position = _playerTransform.TransformPoint(wTransform);
        }

    }
}
