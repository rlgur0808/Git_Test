using UnityEngine;

public class Bullet : MonoBehaviour
{
    //소환됐을때, 앞으로 날라가기만 하면 됨
    private Rigidbody rb = null;
    [SerializeField] private GameObject effectPrefab = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.AddForce(transform.forward * 20f,
            ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageableObject =
            other.gameObject.GetComponent<IDamageable>();

        if (damageableObject != null)
        {
            damageableObject.TakeDamage(20f);
            // 이펙트 플레이
            Vector3 detecedPosition = other.ClosestPoint(transform.position);
            Instantiate(effectPrefab, detecedPosition, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
