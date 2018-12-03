using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

    [SerializeField]
    private RectTransform tutorialPanel;
    [SerializeField]
    private Vector2 tutorialPanelSize;
    [SerializeField]
    private Slider musicVolume;
    [SerializeField]
    private Slider sfxVolume;

    [SerializeField]
    private Button[] levelStartButtons = new Button[0];

    private bool hideTutorialPanel;

    private void Start()
    { 
        this.musicVolume.value = Config.MusicVolume;
        this.sfxVolume.value = Config.SfxVolume;

        for (int i = 0; i < this.levelStartButtons.Length; i++)
        {
            this.levelStartButtons[i].interactable = Config.GetLevelCompleted(i + 1);
        }
    }

    private void OnDestroy()
    {
        Config.Save();
    }

    public void ChangeSFXVolume(float volume)
    {
        Config.SfxVolume = volume;
    }

    public void ChangeMusicVolume(float volume)
    {
        Config.MusicVolume = volume;
    }

    public void StartLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ToggleTutorialPanel()
    {
        if(this.hideTutorialPanel)
        {
            this.StopCoroutine("ShowTutorialPanel");
            this.StartCoroutine("HideTutorialPanel");
        }
        else
        {
            this.StopCoroutine("HideTutorialPanel");
            this.StartCoroutine("ShowTutorialPanel");
        }
    }

    private IEnumerator HideTutorialPanel()
    {
        while(this.tutorialPanel.sizeDelta.magnitude > 0)
        {
            this.tutorialPanel.sizeDelta = Vector2.Lerp(this.tutorialPanel.sizeDelta, Vector2.zero, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator ShowTutorialPanel()
    {
        while (   this.tutorialPanel.sizeDelta.x < this.tutorialPanelSize.x
               && this.tutorialPanel.sizeDelta.y < this.tutorialPanelSize.y)
        {
            this.tutorialPanel.sizeDelta = Vector2.Lerp(this.tutorialPanel.sizeDelta, this.tutorialPanelSize, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
