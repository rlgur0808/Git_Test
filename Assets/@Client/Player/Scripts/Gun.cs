using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //총을 발사하는 로직을 가지고 있어야 함
    //shootPosition 고거를 찾아서
    //bullet을 shootPosition에 소환

    private ShootPosition shootPosition = null;

    [SerializeField]
    private GameObject bullet = null;

    private AudioSource source = null;

    private void Awake()
    {
        shootPosition
            = GetComponentInChildren<ShootPosition>();
        source = GetComponent<AudioSource>();
    }

    public void Attack()
    {
        //bullet을 shootPosition에 소환하는 기능
        Instantiate(bullet,
            shootPosition.transform.position,
            shootPosition.transform.rotation);

        source.Play();
    }
}
