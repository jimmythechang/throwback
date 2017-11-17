using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;

/**
 * Service for search functionality.
 */
public class SearchService {
    /**
     * Searches for songs at archive.org.
     * 
     * <param name="query">The user's search input.</param>
     * <param name="resultsPanel">The GameObject in which we populate the results.</param>
     */
    public IEnumerator searchForSongs(string query, GameObject resultsPanel) {
        using (UnityWebRequest request = UnityWebRequest.Get("https://archive.org/advancedsearch.php" + generateQueryString(query))) {
            yield return request.Send();

            /*
             * Pass off the results to the supplied Results Panel.
             */
            if (request.downloadHandler.isDone) {
                SearchResponse searchResponse = JsonUtility.FromJson<SearchResponse>(request.downloadHandler.text);
                resultsPanel.GetComponent<ResultsPanelController>().setResults(searchResponse.getResults());
            }
        }
    }

    /**
     * Retrieves links to files for the supplied song.
     * 
     * <param name="identifier">An archive.org identifier.</param>
     * <param name="fileList">The GameObject in which we populate the results.</param>
     */
    public IEnumerator getSongFiles(string identifier, GameObject fileList) {
        string filesUrl = identifier + "/" + identifier + "_files.xml";
        Debug.Log(filesUrl);
        using (UnityWebRequest request = UnityWebRequest.Get("https://archive.org/download/" + filesUrl)) {
            yield return request.Send();
            if (request.downloadHandler.isDone) {
                XmlDocument fileData = new XmlDocument();
                fileData.LoadXml(request.downloadHandler.text);
                XmlNodeList fileNodes = fileData.SelectNodes("/files/file");

                List<SongFile> songFiles = new List<SongFile>();
                foreach (XmlNode node in fileNodes) {
                    if (node.SelectSingleNode("format").InnerText.Equals("Ogg Vorbis")) {
                        songFiles.Add(new SongFile(node.Attributes["name"].Value, float.Parse(node.SelectSingleNode("length").InnerText), identifier));
                    }
                }

                fileList.GetComponent<SongFileListController>().setResults(songFiles);
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
        //builder.Append(" AND date:[null to \"2000\"]"); // Might need to adjust this depending on how interesting the results are.
        builder.Append("&fl[]=downloads&fl[]=identifier&fl[]=subject&fl[]=title&fl[]=description&fl[]=date");
        builder.Append("&sort[]=downloads desc");
        builder.Append("&rows=20&output=json&save=yes");

        return builder.ToString();
    }

}