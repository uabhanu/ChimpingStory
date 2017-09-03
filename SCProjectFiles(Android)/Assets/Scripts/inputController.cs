using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
	bool isMobile = true;
	ChimpController m_chimpController;

	void Start()
    {
		if(Application.isEditor)
        {
            isMobile = false;
        }

		m_chimpController = GameObject.FindGameObjectWithTag("Player").GetComponent<ChimpController>();
	}

	void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

		if(isMobile)
        {
			int tmpC = Input.touchCount;
			tmpC--;

			if(Input.GetTouch(tmpC).phase == TouchPhase.Began)
            {
				HandleInteraction(true);
			}

			if(Input.GetTouch(tmpC).phase == TouchPhase.Ended)
            {
				HandleInteraction(false);
			}

		}
        else
        {
			if(Input.GetMouseButtonDown(0))
            {
				HandleInteraction(true);
			}

			if(Input.GetMouseButtonUp(0))
            {
				HandleInteraction(false);
			}
		}
	}

	void HandleInteraction(bool starting)
    {
		if(starting)
        {
			m_chimpController.Jump();
		}
        else
        {
		    m_chimpController.jumpPress=false;
		}
	}
}
