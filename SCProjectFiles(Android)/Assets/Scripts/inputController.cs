using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
	bool m_isMobile = true;
	ChimpController m_chimpController;

	void Start()
    {
		if(Application.isEditor)
        {
            m_isMobile = false;
        }

		m_chimpController = GameObject.FindGameObjectWithTag("Player").GetComponent<ChimpController>();
	}

	void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

		if(m_isMobile)
        {
			int tmpC = Input.touchCount;
			tmpC--;

			if(Input.GetTouch(tmpC).phase == TouchPhase.Began)
            {
				HandleMouseLeftInteraction(true);
                HandleMouseRightInteraction(true);
			}

			if(Input.GetTouch(tmpC).phase == TouchPhase.Ended)
            {
				HandleMouseLeftInteraction(false);
                HandleMouseRightInteraction(false);
			}
		}
        else
        {
			if(Input.GetMouseButtonDown(0))
            {
				HandleMouseLeftInteraction(true);
			}

            if(Input.GetMouseButtonDown(1))
            {
				HandleMouseRightInteraction(true);
			}

			if(Input.GetMouseButtonUp(0))
            {
				HandleMouseLeftInteraction(false);
			}

            if(Input.GetMouseButtonUp(1))
            {
				HandleMouseRightInteraction(false);
			}
		}
	}

	void HandleMouseLeftInteraction(bool starting)
    {
		if(starting)
        {
			m_chimpController.Jump();
		}
        else
        {
		    m_chimpController.m_jumpPress = false;
		}
	}

    void HandleMouseRightInteraction(bool starting)
    {
		if(starting)
        {
			m_chimpController.Slide();
		}
	}
}
