using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float delay = 10f;

    private float destroyTime;

    void Start()
    {
        destroyTime = Time.time + delay;
    }

    void Update()
    {
        // İleri doğru hareket et
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Süre dolunca yok et
        if (Time.time >= destroyTime)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
