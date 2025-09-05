using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : Panel{

    private void OnEnable(){
        RefreshUI();
    }

    public override void Close(){
        PanelManager.Instance.ClosePause();
    }

    public override void ResetUI(){
        RefreshUI();
    }

    public override void RefreshUI()
    {
        
    }
    public void Restart(){
        Close();
        SceneManager.LoadScene(0);
    }
}
