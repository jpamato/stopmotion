using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class WebCamPhotoCamera : MonoBehaviour
{
    WebCamTexture webCamTexture;
    public RawImage rawImage;
    private bool photoTaken;
    private bool ready;

    void Start()
    {
       
		Events.OnKeyYellow += TakePhoto;
		//webCamTexture = new WebCamTexture(WebCamTexture.devices[0].name, (int)Data.Instance.defaultCamSize.x, (int)Data.Instance.defaultCamSize.y, 30);
		webCamTexture = new WebCamTexture();			

        if (webCamTexture.isPlaying)
        {
            webCamTexture.Stop();
        } else
        webCamTexture.Play();

        Vector3 scale = rawImage.transform.localScale;
        
#if UNITY_IOS
        scale.x *= -1;
       rawImage.transform.localEulerAngles = new Vector3(0, 0, 180);
#endif
        rawImage.transform.localScale = scale;
    }

    void Update()
    {
		if(Data.Instance.state == Data.States.live)
        rawImage.texture = webCamTexture;        
    }
    
    void OnDestroy()
    {
        webCamTexture.Stop();
		Events.OnKeyYellow -= TakePhoto;
    }
    public void TakePhoto()
    {
		if(Data.Instance.state!=Data.States.playing)
       	Data.Instance.timelineManager.SaveFrame (webCamTexture);
    }

}