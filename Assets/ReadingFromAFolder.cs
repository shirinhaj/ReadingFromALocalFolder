using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Vuforia;

public class ReadingFromAFolder : MonoBehaviour
{
    string FileName;

    void Start()
    {
        StartCoroutine(instantite("15MinutesItalian.jpg"));
        StartCoroutine(instantite("Kids.jpg"));
    }
    IEnumerator instantite(string fName)
    {      
        FileName = fName;
        Debug.Log(fName);
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(CreateImageTargetFromSideloadedTexture);
        yield return null;
    }

    void CreateImageTargetFromSideloadedTexture()
    {
        var objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

        // get the runtime image source and set the texture to load
        var runtimeImageSource = objectTracker.RuntimeImageSource;
        Debug.Log("Vuforia/" + FileName);
        runtimeImageSource.SetFile(VuforiaUnity.StorageType.STORAGE_APPRESOURCE, "Vuforia/" + FileName, 0.15f, "Target");

        // create a new dataset and use the source to create a new trackable
        var dataset = objectTracker.CreateDataSet();
        var trackableBehaviour = dataset.CreateTrackable(runtimeImageSource, "Target");

        // add the DefaultTrackableEventHandler to the newly created game object
        trackableBehaviour.gameObject.AddComponent<DefaultTrackableEventHandler>();

        // activate the dataset
        objectTracker.ActivateDataSet(dataset);

        // TODO: add virtual content as child object(s)
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.SetParent(trackableBehaviour.gameObject.transform);
        cube.transform.localScale = cube.transform.localScale / 10;
    }
}