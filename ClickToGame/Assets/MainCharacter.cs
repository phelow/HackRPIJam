using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainCharacter : MonoBehaviour
{
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

    [SerializeField]
    private Rigidbody2D m_rigidbody;

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
    void AddTimeDilation(float amount)
    {
        Time.timeScale = Mathf.Clamp(Time.timeScale + amount, 0.3f, 4.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = Mathf.Lerp(Time.timeScale, .3f, Time.deltaTime);
        Time.fixedDeltaTime = .02f * Time.timeScale;

        Cursor.visible = false;
        

        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            AddTimeDilation(.3f);
            m_rigidbody.gravityScale *= -1;
        }
    }
}
