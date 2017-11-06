using System;

[Serializable]
public class SearchResponse {
    public ResponseBody response;

    public SearchResult[] getResults() {
        return response.docs;
    }
}