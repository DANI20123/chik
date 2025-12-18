using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Sprite offMusic, offSound;
    public Sprite onMusic, onSound;
    public Image sound, music;
    public AudioSource soundSource, musicSource;

    public int valueScore;
    public Text record, score;
    public CanvasGroup[] panel;




    public GameObject lose;
    public GameObject loseNew;

    public Text ls;
    public Text newls;

    public bool isBest = false;
    public void Lose()
    {
        if(isBest)
        {
            loseNew.SetActive(true);
            newls.text = PlayerPrefs.GetInt("best").ToString();
        }
        else
        {
            lose.SetActive(true);
            ls.text = valueScore.ToString();
        }
    }
    private void OpenPanel(int id)
    {
        panel[id].alpha = 0f;
        panel[id].transform.localScale = Vector3.one * 3f;

        panel[id].gameObject.SetActive(true);

        panel[id].DOFade(1f, 0.5f).SetUpdate(true);
        panel[id].transform.DOScale(Vector3.one, 0.5f).SetUpdate(true);
    }

    private void ClosePanel(int id)
    {


        panel[id].DOFade(0f, 0.5f).SetUpdate(true);
        panel[id].transform.DOScale(Vector3.one * 3f, 0.5f).OnComplete(() =>
        {
            panel[id].gameObject.SetActive(false);
        }).SetUpdate(true);
    }


    private void Start()
    {
        Time.timeScale = 1f;
        CheckSound();
        ShowScore();
    }
    public void Pause(int id)
    {
        Time.timeScale = id;

        if(id==0)//Off
        {
            OpenPanel(0);
        }if(id==1)
        {
            ClosePanel(0);
        }
    }
    public void ShowScore()
    {

        if (valueScore > PlayerPrefs.GetInt("best"))
        {
            isBest = true;
            PlayerPrefs.SetInt("best", valueScore);
        }
            

        if (PlayerPrefs.HasKey("best"))
            record.text = PlayerPrefs.GetInt("best").ToString();
        else
            record.text = "0";

        score.text = valueScore.ToString();

    }
    public void Toggel(string name)
    {
        if (PlayerPrefs.HasKey(name))
        {
            PlayerPrefs.DeleteKey(name);
        }
        else
        {
            PlayerPrefs.SetString(name, name);
        }
        CheckSound();
    }
    public void CheckSound()
    {
        if (!PlayerPrefs.HasKey("music"))
        {
            music.sprite = onMusic;
            musicSource.volume = 1f;
        }
        else
        {
            music.sprite = offMusic;
            musicSource.volume = 0f;
        }

        if (!PlayerPrefs.HasKey("sound"))
        {
            sound.sprite = onSound;
            soundSource.volume = 1f;

        }
        else
        {
            sound.sprite = offSound;
            soundSource.volume = 0f;
        }
    }




    public void LoadScene(string name)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(name);
    }
    public void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("load");
    }
}
