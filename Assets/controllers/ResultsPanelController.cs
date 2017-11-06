using System.Text;
using UnityEngine;

public class ResultsPanelController : MonoBehaviour {

    // Reference to the layout prefab for a SearchResult.
    public GameObject searchResultPrefab;

    // Reference to the panel where song details are displayed.
    public GameObject songDetailPanelContent;

    /**
     * Sets Search Results as children of the GameObject to which this script is attached.
     * 
     * <param name="results">An array of SearchResults.</param>
     */
    public void setResults(SearchResult[] results) {
        foreach(Transform child in transform) {
            Destroy(child.gameObject);
        }

        foreach (SearchResult result in results) {
            GameObject resultPrefab = createSearchResultPrefab(result);
            resultPrefab.transform.SetParent(transform);
        }
    }

    /**
     * Creates an instance of the specified searchResultPrefab GameObject, and sets
     * desired properties on it.
     * 
     * <param name="result">SearchResult.</param>
     */
    private GameObject createSearchResultPrefab(SearchResult result) {
        GameObject resultPrefab = Instantiate(searchResultPrefab);
        SearchResultController controller = resultPrefab.GetComponent<SearchResultController>();

        controller.title.text = result.title;
        controller.setIdentifier(result.identifier);
        controller.setSongDetailPanelReference(songDetailPanelContent);
        controller.setSongDetail(new SongDetail(result.title, result.description, result.date));

        if (result.subject != null && result.subject.Length > 0) {
            controller.subjects.text = generateSubjectString(result);
        }

        return resultPrefab;
    }

    /**
     * Returns up to the first three subjects of a SearchResult
     * in a comma-delimited string.
     * 
     * <param name="result">SearchResult.</param>
     */
    private string generateSubjectString(SearchResult result) {
        int min = Mathf.Min(result.subject.Length, 3);
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < min; i++) {
            builder.Append(result.subject[i]);
            if (i < min - 1) {
                builder.Append(", ");
            }
        }
        return builder.ToString();
        
    }
}
