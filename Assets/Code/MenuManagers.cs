using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManagers : MonoBehaviour
{

    public GameObject load, menu;
    public GameObject onboarding;

    public Image fill;

  public CanvasGroup[] panel;
    public Text record;

    public Image sound, music;


    public Sprite offMusic, offSound;
    public Sprite onMusic, onSound;

    public AudioSource soundSource, musicSource;
    public void CheckSound()
    {
        if(!PlayerPrefs.HasKey("music"))
        {
            music.sprite = onMusic;
            musicSource.volume = 1f;
        }
        else
        {
            music.sprite = offMusic;
            musicSource.volume = 0f;
        }

        if(!PlayerPrefs.HasKey("sound"))
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
    private void Start()
    {
        if(PlayerPrefs.HasKey("load"))
        {
            load.SetActive(false);
            if (PlayerPrefs.HasKey("help"))
                menu.SetActive(true);
            else
                onboarding.SetActive(true);

        }
        else
        {
            load.SetActive(true);
            fill.fillAmount = 0;
            fill.DOFillAmount(1f, 4f).OnComplete(() =>
            {
                PlayerPrefs.SetString("load", "load");
                load.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
                load.transform.DOScale(Vector3.one * 3f, 0.5f).OnComplete(() =>
                {
                    load.SetActive(false);
                });
                if (PlayerPrefs.HasKey("help"))
                {
                    menu.SetActive(true);
                    menu.GetComponent<CanvasGroup>().alpha = 0f;
                    menu.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
                }
                else
                {
                    onboarding.SetActive(true);
                    onboarding.GetComponent<CanvasGroup>().alpha = 0f;
                    onboarding.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
                }
            });
        }

        if(PlayerPrefs.HasKey("best"))
        {
            record.text = PlayerPrefs.GetInt("best").ToString();
        }
        else
        {
            record.text = "0";
        }
        CheckSound();
    }
    public void LoadGame()
    {
        menu.GetComponent<CanvasGroup>().alpha = 1f;
        menu.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).OnComplete(() => SceneManager.LoadScene("Game"));
       
    }
    public void Toggel(string name)
    {
        if(PlayerPrefs.HasKey(name))
        {
            PlayerPrefs.DeleteKey(name);
        }
        else
        {
            PlayerPrefs.SetString(name, name);
        }
        CheckSound();
    }

    public void OpenPanel(int id)
    {
        panel[id].alpha = 0f;
        panel[id].transform.localScale = Vector3.one * 3f;

        panel[id].gameObject.SetActive(true);

        panel[id].DOFade(1f, 0.5f);
        panel[id].transform.DOScale(Vector3.one, 0.5f);
    }

    public void ClosePanel(int id)
    {
      

        panel[id].DOFade(0f, 0.5f);
        panel[id].transform.DOScale(Vector3.one*3f, 0.5f).OnComplete(()=>
        {
            panel[id].gameObject.SetActive(false);
        });
    }
    public void PlayOnBoarding()
    {
        PlayerPrefs.SetString("help", "help");
        onboarding.GetComponent<CanvasGroup>().alpha = 1f;
        onboarding.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).OnComplete(()=>
        {
            onboarding.SetActive(false);
        });

        menu.SetActive(true);
        menu.GetComponent<CanvasGroup>().alpha = 0f;
        menu.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
    }


    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("load");
    }
}
