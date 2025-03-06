using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public float ContainerMoveSpeed = 150f;
    public float DelayBetweenButtons = .5f;

    [SerializeField] private RectTransform PlayButton;
    [SerializeField] private RectTransform AboutButton;
    [SerializeField] private RectTransform QuitButton;

    private Vector3 playButtonPosition = new Vector3(0, -400);
    private Vector3 aboutButtonPosition = new Vector3(0, -450);
    private Vector3 quitButtonPosition = new Vector3(0, -500);

    private Vector3 playButtonVelocity = Vector3.zero;
    private Vector3 aboutButtonVelocity = Vector3.zero;
    private Vector3 quitButtonVelocity = Vector3.zero;



    private bool canPlayButtonMove = false;
    private bool canAboutButtonMove = false;
    private bool canQuitButtonMove = false;

    private void Start()
    {
        StartCoroutine(EnableButtonMovement(1f));
    }

    private void Update()
    {
        if (canPlayButtonMove)
        {
            MoveButton(PlayButton, playButtonPosition, ref playButtonVelocity);
        }
        if (canAboutButtonMove)
        {
            MoveButton(AboutButton, aboutButtonPosition, ref aboutButtonVelocity);
        }
        if (canQuitButtonMove)
        {
            MoveButton(QuitButton, quitButtonPosition, ref quitButtonVelocity);
        }

    }


    private void MoveButton(RectTransform button, Vector3 endPos, ref Vector3 buttonVelocity)
    {
        float deltaYPosition = 75f;
        if (button.anchoredPosition3D.y > endPos.y)
        {
            button.anchoredPosition3D = Vector3.SmoothDamp(button.anchoredPosition3D, endPos - new Vector3(0, deltaYPosition, 0),
                ref buttonVelocity, ContainerMoveSpeed * Time.deltaTime);
        }
        else
        {
            button.anchoredPosition3D = Vector3.SmoothDamp(button.anchoredPosition3D, endPos,
                ref buttonVelocity, ContainerMoveSpeed * Time.deltaTime);
        }
    }
    private IEnumerator EnableButtonMovement(float startDelay)
    {
        yield return new WaitForSeconds(startDelay);
        canQuitButtonMove = true;
        yield return new WaitForSeconds(DelayBetweenButtons);
        canAboutButtonMove = true;
        yield return new WaitForSeconds(DelayBetweenButtons);
        canPlayButtonMove = true;
    }


    // Button Unity Events
    public void OnPlayButtonPresssed()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void OnAboutButtonPressed()
    {
        Debug.Log("About button pressed");
    }
    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
