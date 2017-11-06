using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SearchResultController : MonoBehaviour, IPointerClickHandler {

    public Text title;
    public Text subjects;
    private string identifier;

    private SearchService service;

    public void Start() {
        service = new SearchService();
    }

    public void setIdentifier(string identifier) {
        this.identifier = identifier;
    }

    public void OnPointerClick(PointerEventData eventData) {
        StartCoroutine(service.getSongDetail(identifier));
    }
}
