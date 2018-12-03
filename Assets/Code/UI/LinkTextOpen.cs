using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Text))]
public class LinkTextOpen : MonoBehaviour
{
    private string link;

    private void Start()
    {
        this.link = this.GetComponent<Text>().text;
    }

    public void OpenLink()
    {
        Application.OpenURL(this.link);
    }
}
