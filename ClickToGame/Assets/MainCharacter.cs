using UnityEngine;
using System.Collections;

public class MainCharacter : MonoBehaviour
{
    [SerializeField]
    private bool m_isLeft;
    // Use this for initialization

    [SerializeField]
    private float m_shot = .5f;

    [SerializeField]
    private Transform m_parentCharacterTransform;

    [SerializeField]
    private GameObject m_gameobject;

    [SerializeField]
    private float m_projectileForce;

    void Start()
    {
        ChangeDirection();

        StartCoroutine(ShootAtInerval(m_shot));
    }

    private IEnumerator ShootAtInerval(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            Projectile projectile = (GameObject.Instantiate(m_gameobject, transform.position, new Quaternion(0,0,0,0), null) as GameObject).GetComponent<Projectile>();

            float f = m_isLeft ? m_projectileForce : - m_projectileForce;
            Debug.Log(f);
            projectile.Shoot(f);


        }
    }

    private void ChangeDirection()
    {
        m_isLeft = !m_isLeft;
        m_parentCharacterTransform.Rotate(new Vector3(0, 0, 180));
    }

    void AddTimeDilation(float amount)
    {
        Time.timeScale = Mathf.Clamp(Time.timeScale + amount, 0.1f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = Mathf.Lerp(Time.timeScale, .1f, Time.deltaTime);

        Cursor.visible = false;

        float oldY = transform.position.y;

        float newY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        AddTimeDilation(Mathf.Abs(oldY - newY));


        transform.position = new Vector3(0, Mathf.Clamp(newY,- 4.0f,6.0f), 0);

        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            ChangeDirection();
            Debug.Log("Changing Directions");
        }
    }
}
