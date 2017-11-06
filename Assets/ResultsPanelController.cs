using System.Text;
using UnityEngine;

public class ResultsPanelController : MonoBehaviour {

    public GameObject resultsList;
    public GameObject searchResultPrefab;

    /**
     * Sets Search Results in the specified resultsList GameObject.
     * 
     * <param name="results">An array of SearchResults.</param>
     */
    public void setResults(SearchResult[] results) {
        foreach (SearchResult result in results) {
            GameObject resultPrefab = createSearchResultPrefab(result);
            resultPrefab.transform.SetParent(this.transform);
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
