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

    public void setIdentifier(string identifier) {
        this.identifier = identifier;
    }

    public void setSongDetailPanelReference(GameObject songDetailPanelContent) {
        this.songDetailPanelContent = songDetailPanelContent;
    }

    public void setSongDetail(SongDetail songDetail) {
        this.songDetail = songDetail;
    }

    public void OnPointerClick(PointerEventData eventData) {
        SongDetailController controller = songDetailPanelContent.GetComponent<SongDetailController>();
        controller.title.text = this.songDetail.title;
        controller.date.text = this.songDetail.date;
        controller.description.text = this.songDetail.description;
    }
}
