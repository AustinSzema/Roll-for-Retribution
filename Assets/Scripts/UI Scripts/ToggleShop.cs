using UnityEngine;

    public class ToggleShop : MonoBehaviour
    {
        [SerializeField] private GameObject _shopCanvas;

        private bool _toggleCanvas = false;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                _toggleCanvas = !_toggleCanvas;
                if (_toggleCanvas)
                {
                    _shopCanvas.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Time.timeScale = 0;
                }
                else
                { 
                    _shopCanvas.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Time.timeScale = 1;

                }
            }
        }
    }