using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainCharacter : MonoBehaviour
{
    [SerializeField]
    private bool m_isLeft;
    // Use this for initialization

    [SerializeField]
    public float m_shot = .5f;

    [SerializeField]
    private Transform m_parentCharacterTransform;

    [SerializeField]
    private GameObject m_gameobject;

    [SerializeField]
    private float m_projectileForce;

    [SerializeField]
    private Text m_text;

    private int m_score = 0;

    public static MainCharacter ms_instance;

    public void AddPoint()
    {
        m_score++;
        m_text.text = "Score:" + m_score + "\nHighScore:" + PlayerPrefs.GetInt("HighScore", 0);

        if(m_score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", m_score);
        }
    }

    void Awake()
    {
        ms_instance = this;

    }

    void Start()
    {
        ChangeDirection();

        StartCoroutine(ShootAtInerval());
        AddPoint();
    }

    private IEnumerator ShootAtInerval()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_shot);
            m_shot *= .99f;

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
        Time.fixedDeltaTime = .02f * Time.timeScale;

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
