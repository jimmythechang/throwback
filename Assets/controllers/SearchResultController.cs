using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SearchResultController : MonoBehaviour, IPointerClickHandler {

    public Text title;
    public Text subjects;

    private string identifier;
    private SongDetail songDetail;

    // Reference to the panel in which song details are populated.
    private GameObject songDetailPanelContent;

    // Reference to the panel in which song files are populated.
    private GameObject songFileListContent;

    private SearchService searchService;

    void Start() {
        searchService = new SearchService();
    }

    public void setIdentifier(string identifier) {
        this.identifier = identifier;
    }

    public void setSongDetailPanelReference(GameObject songDetailPanelContent) {
        this.songDetailPanelContent = songDetailPanelContent;
    }

    public void setSongFileListContentReference(GameObject songFileListContent) {
        this.songFileListContent = songFileListContent;
    }

    public void setSongDetail(SongDetail songDetail) {
        this.songDetail = songDetail;
    }

    public void OnPointerClick(PointerEventData eventData) {
        /*
         * Set the details for the song, and retrieve the files associated with it.
         */
        SongDetailController controller = songDetailPanelContent.GetComponent<SongDetailController>();
        controller.title.text = this.songDetail.title;
        controller.date.text = this.songDetail.date;
        controller.description.text = this.songDetail.description;

        StartCoroutine(searchService.getSongFiles(identifier, songFileListContent));
    }
}
