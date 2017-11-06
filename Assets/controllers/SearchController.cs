using UnityEngine;
using UnityEngine.UI;

public class SearchController : MonoBehaviour {

    private SearchService service;
    private InputField inputField;

    // Reference to where the results will be displayed.
    public GameObject resultsPanel;

    void Start () {
        inputField = GetComponent<InputField>();
        service = new SearchService();
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return)) {
            StartCoroutine(service.searchForSongs(inputField.text, resultsPanel));
        }
	}

}
