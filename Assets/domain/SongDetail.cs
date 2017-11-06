
using System.Xml;

public class SongDetail {
    public string title;
    public string description;
    public string date;

    public void parseSongDetailXml(XmlDocument metadata) {
        SongDetail songDetail = new SongDetail();
        title = metadata.SelectSingleNode("/metadata/title").InnerText;
        description = getNodeText(metadata.SelectSingleNode("/metadata/description"));
        date = getNodeText(metadata.SelectSingleNode("/metadata/date"));
    }

    private string getNodeText(XmlNode node) {
        if (node != null) {
            return node.InnerText;
        }
        return "";
    }
}