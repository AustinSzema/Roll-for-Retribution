using UnityEngine;

    public class ToggleShop : MonoBehaviour
    {
        [SerializeField] private GameObject _shopCanvas;

        [SerializeField] private boolVariable _gameIsPaused;
        
        private bool _toggleCanvas = false;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _toggleCanvas = !_toggleCanvas;
                if (_toggleCanvas)
                {
                    _shopCanvas.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Time.timeScale = 0;
                    _gameIsPaused.Value = true;
                }
                else
                { 
                    _shopCanvas.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    Time.timeScale = 1;
                    _gameIsPaused.Value = false;
                }
            }
        }
    }