using UnityEngine;
using System.Collections;

public class MainCharacter : MonoBehaviour
{
    [SerializeField]
    private bool m_isLeft;
    // Use this for initialization

    [SerializeField]
    private Transform m_parentCharacterTransform;

    void Start()
    {
        ChangeDirection();
    }

    private void ChangeDirection()
    {
        m_isLeft = !m_isLeft;

        m_parentCharacterTransform.Rotate(new Vector3(0, 0, 180));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            ChangeDirection();
            Debug.Log("Changing Directions");
        }
    }
}
