using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public List<GameObject> cubes;
    public float timer = 90f;
    [SerializeField] private GameObject tryAgainPopup;

    public UnityAction OpenTryAgainPopup;

    public bool isGamePaused = false;

    private void Awake()
    {
        OpenTryAgainPopup += TryAgainPopupOpen;
    }
    public void PlayerCollectCubes()
    {
        cubes.RemoveAt(cubes.Count - 1);

        if(cubes.Count == 0 )
        {
            Debug.Log("P{layer winms");
        }
    }

    public void TryAgainPopupOpen()
    {
        tryAgainPopup.SetActive(true);
        OpenTryAgainPopup -= TryAgainPopupOpen;
        isGamePaused = true;
    }
}
