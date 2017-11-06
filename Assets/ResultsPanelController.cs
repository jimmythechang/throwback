using System.Text;
using UnityEngine;

public class ResultsPanelController : MonoBehaviour {

    public GameObject resultsList;
    public GameObject searchResultPrefab;

    public void setResults(SearchResult[] results) {
        foreach (SearchResult result in results) {
            GameObject resultPrefab = Instantiate<GameObject>(searchResultPrefab);
            SearchResultController controller = resultPrefab.GetComponent<SearchResultController>();

            controller.title.text = result.title;
           
            if (result.subject != null && result.subject.Length > 0) {
                int min = Mathf.Min(result.subject.Length, 3);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < min; i++) {
                    builder.Append(result.subject[i]);
                    if (i < min - 1) {
                        builder.Append(", ");
                    }
                }
                controller.subjects.text = builder.ToString();
            }

            resultPrefab.transform.SetParent(this.transform);
        }
    }
}
