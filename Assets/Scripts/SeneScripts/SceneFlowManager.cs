using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneFlowManager : MonoBehaviour
{
     

    public void LoadGardenMenu()
    {
        SceneManager.LoadScene("GardenMenuScene");
    }

    public void LoadTemplateSelect()
    {
        SceneManager.LoadScene("TemplateSelectScene");
    }

    public void LoadTemplate1()
    {
        SceneManager.LoadScene("SampleScene");
    }
 
}

 