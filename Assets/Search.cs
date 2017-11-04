using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class Search : MonoBehaviour {

    private InputField inputField;

	void Start () {
        inputField = GetComponent<InputField>();

	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return)) {
            StartCoroutine(searchForSongs(inputField.text));
        }
	}

    /**
     * Searches for songs at archive.org.
     * 
     * <param name="query">The user's search input.</param>
     */
    public IEnumerator searchForSongs(string query) {
        UnityWebRequest request  = UnityWebRequest.Get("https://archive.org/advancedsearch.php" + generateQueryString(query));
        yield return request.Send();

        if (request.downloadHandler.isDone) {
            SearchResponse searchResponse = JsonUtility.FromJson<SearchResponse>(request.downloadHandler.text);
            foreach (SearchResult result in searchResponse.getResults()) {
                Debug.Log(result.title);
            }
        }

    }

    /**
     * Generates the search string.
     * 
     * <param name="query">The user's search input.</param>
     */
    private string generateQueryString(string query) {
        StringBuilder builder = new StringBuilder();

        /*
         * We're looking for .ogg audio files published before 2000!
         */
        builder.Append("?q=(\"" + query + "\")");
        builder.Append(" AND mediaType:(audio) AND format:(\"OGG VORBIS\")");
        builder.Append(" AND date:[null to \"2000\"]"); // Might need to adjust this depending on how interesting the results are.
        builder.Append("&fl[]=downloads&fl[]=identifier&fl[]=subject&fl[]=title");
        builder.Append("&sort[]=downloads desc");
        builder.Append("&rows=20&output=json&save=yes");

        return builder.ToString();
    }
}
