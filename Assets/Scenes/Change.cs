using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change : MonoBehaviour
{
    private async void Start()
    {
        await Task.Delay(50000);

        SceneManager.LoadScene("ArenaStory");
    }
}
