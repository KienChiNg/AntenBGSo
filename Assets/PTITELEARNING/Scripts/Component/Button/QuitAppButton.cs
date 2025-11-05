using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class QuitAppButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate {
            DisplayYesNoMessagePopup.instance.ShowMessagePopup("Bạn có chắc chắn muốn thoát phần mềm?", () => { Application.Quit(); }, null);
        });
    }
}
