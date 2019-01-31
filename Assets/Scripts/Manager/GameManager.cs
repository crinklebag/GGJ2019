using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{


    public IEnumerator Start()
    {
        yield return StartCoroutine(Initialize());

        yield return MenuManager.Instance.StartCoroutine(MenuManager.OpenMenu());

        yield return null;
    }

    /// <summary>
    /// Initialize stuff...
    /// </summary>
    /// <returns></returns>
    public IEnumerator Initialize()
    {

        yield return null;
    }

    public IEnumerator StartGame()
    {
        yield return null;
    }

}
