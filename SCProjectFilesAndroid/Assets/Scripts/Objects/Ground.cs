using UnityEngine;

public class Ground : MonoBehaviour 
{
	public static BoxCollider2D m_groundCollider2D;
	public static SpriteRenderer m_groundRenderer;

	void Start() 
	{
        m_groundCollider2D = GetComponent<BoxCollider2D>();	
		m_groundRenderer = GetComponent<SpriteRenderer>();
	}
}
