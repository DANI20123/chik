using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int index = 2;
    public Transform[] point;

    public GameObject player;

    public Tweener pl;
    public void Start()
    {
        player.transform.position = new Vector2(point[index].position.x, player.transform.position.y);
    }


    public void Move(int id)
    {
        index += id;
        if (index < 0) index = 0;
        if (index > point.Length - 1)
            index = point.Length - 1;

        pl.Kill();
        pl = player.transform.DOMoveX(point[index].position.x, .2f).SetEase(Ease.Linear);
    }
}
