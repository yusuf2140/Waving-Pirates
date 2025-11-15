using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public string Tag = "Projectile";

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag)
        {
            Destroy(gameObject);
        }
    }
}
