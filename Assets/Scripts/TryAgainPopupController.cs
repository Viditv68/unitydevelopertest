using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainPopupController : MonoBehaviour
{
    public void TryAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
