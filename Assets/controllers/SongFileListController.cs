using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongFileListController : MonoBehaviour {

    // Reference to the layout prefab for a SongFile.
    public GameObject songFilePrefab;

    // Reference to the panel where song files are displayed.
    public GameObject songFilesPanelContent;

    /**
     * Sets Song Files as children of the GameObject to which this script is attached.
     */
    public void setResults(List<SongFile> files) {
        foreach(Transform child in transform) {
            Destroy(child.gameObject);
        }

        foreach (SongFile file in files) {
            GameObject filePrefab = createSongFilePrefab(file);
            filePrefab.transform.SetParent(transform);
        }
    }

    private GameObject createSongFilePrefab(SongFile file) {
        GameObject filePrefab = Instantiate(songFilePrefab);

        SongFileController controller = filePrefab.GetComponent<SongFileController>();
        controller.filename.text = file.filename;
        controller.playLength.text = generatePlayLengthString(file.playLength);

        return filePrefab;
    }

    private string generatePlayLengthString(float playLength) {
        int integerLength = Mathf.FloorToInt(playLength);

        return (integerLength / 60).ToString() + ":" + (integerLength % 60).ToString();
    }
}
