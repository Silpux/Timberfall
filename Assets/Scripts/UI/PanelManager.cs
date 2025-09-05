using UnityEngine;

public class PanelManager : Singleton<PanelManager>{

    [SerializeField] private CameraController cameraController;

    [SerializeField] private AddBuildingPanel addBuildingPanel;
    [SerializeField] private LumbermillPanel lumbermillPanel;
    [SerializeField] private MinePanel minePanel;

    [SerializeField] private MarketPanel marketPanel;
    [SerializeField] private ForgePanel forgePanel;
    [SerializeField] private InventoryPanel inventoryPanel;

    [SerializeField] private PausePanel pausePanel;

    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject inventoryButton;
    [SerializeField] private GameObject addBuildingsButton;

    [SerializeField] private BackgroundMusic backgroundMusic;

    private AudioSource audioSource;

    [SerializeField] private AudioClip clickSound;

    protected override void Awake(){
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable(){
        addBuildingPanel.OnClick += PlayClickSound;
    }

    private void OnDisable(){
        addBuildingPanel.OnClick -= PlayClickSound;
    }

    private void PlayClickSound(){
        audioSource.PlayOneShot(clickSound);
    }

    public void Pause(){
        pausePanel.gameObject.SetActive(true);
        backgroundMusic.SetPausedMode();
        Time.timeScale = 0f;
        PlayClickSound();
    }

    public void ClosePause(){
        pausePanel.gameObject.SetActive(false);
        backgroundMusic.SetNormalMode();
        Time.timeScale = 1f;
        PlayClickSound();
    }

    public void DisableButtons(){
        pauseButton.SetActive(false);
        inventoryButton.SetActive(false);
        addBuildingsButton.SetActive(false);
    }

    public void EnableButtons(){
        pauseButton.SetActive(true);
        inventoryButton.SetActive(true);
        addBuildingsButton.SetActive(true);
    }

    public void OpenAddBuildingPanel(){
        DisableButtons();
        addBuildingPanel.gameObject.SetActive(true);
        cameraController.EnableBuildingMode();
        PlayClickSound();
    }

    public void CloseAddBuildingPanel(){
        EnableButtons();
        addBuildingPanel.gameObject.SetActive(false);
        cameraController.EnableMovementMode();
        PlayClickSound();
    }

    public void OpenInventoryPanel(){
        DisableButtons();
        cameraController.DisableAllModes();
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshUI();
        PlayClickSound();
    }

    public void CloseInventoryPanel(){
        EnableButtons();
        inventoryPanel.gameObject.SetActive(false);
        cameraController.EnableMovementMode();
        PlayClickSound();
    }

    public void OpenMarketPanel(MarketBuilding sender){
        DisableButtons();
        cameraController.DisableAllModes();
        marketPanel.gameObject.SetActive(true);
        marketPanel.SetBuilding(sender);
        PlayClickSound();
    }

    public void CloseMarketPanel(){
        EnableButtons();
        marketPanel.gameObject.SetActive(false);
        cameraController.EnableMovementMode();
        PlayClickSound();
    }

    public void OpenForgePanel(ForgeBuilding sender){
        DisableButtons();
        cameraController.DisableAllModes();
        forgePanel.gameObject.SetActive(true);
        forgePanel.SetBuilding(sender);
        PlayClickSound();
    }

    public void CloseForgePanel(){
        EnableButtons();
        forgePanel.gameObject.SetActive(false);
        cameraController.EnableMovementMode();
        PlayClickSound();
    }


    public void OpenLumbermillPanel(LumbermillBuilding sender){
        DisableButtons();
        cameraController.DisableAllModes();
        lumbermillPanel.gameObject.SetActive(true);
        lumbermillPanel.SetBuilding(sender);
        PlayClickSound();
    }

    public void CloseLumbermillPanel(){
        EnableButtons();
        lumbermillPanel.gameObject.SetActive(false);
        cameraController.EnableMovementMode();
        PlayClickSound();
    }

    public void OpenMinePanel(MineBuilding sender){
        DisableButtons();
        cameraController.DisableAllModes();
        minePanel.gameObject.SetActive(true);
        minePanel.SetBuilding(sender);
        PlayClickSound();
    }

    public void CloseMinePanel(){
        EnableButtons();
        minePanel.gameObject.SetActive(false);
        cameraController.EnableMovementMode();
        PlayClickSound();
    }

}
