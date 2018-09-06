using UnityEngine;

public class Rock : MonoBehaviour {
    [SerializeField] private float startForce;
    Rigidbody mRigidbody;

    private void Start()
    {
        mRigidbody = GetComponent<Rigidbody>();
        Invoke("Launch", 1f);
    }

    private void Launch() {
        mRigidbody.AddForce(-Vector3.forward * startForce, ForceMode.Impulse);
        Invoke("AutoDestroy", 20f);
    }

    private void AutoDestroy() {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Actor>() != null) {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
