using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform lookAt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = new Vector3(lookAt.transform.position.x, transform.position.y, transform.position.z);
        int lockRot = 0;
        transform.LookAt(position); //= Quaternion.LookRotation(position, new Vector3(0, 0, 1));
        //https://api.unity.com/v1/oauth2/authorize?client_id=unity_learn&locale=de_DE&redirect_uri=https%3A%2F%2Flearn.unity.com%2Fauth%2Fcallback%3Fredirect_to%3D%252Ftutorial%252Fquaternions%253Futm_source%253Dyoutube%2526utm_medium%253Dsocial%2526utm_campaign%253Deducation_global_generalpromo_2020-08-20_youtube-video-quat&response_type=code&scope=identity+offline&state=8e416d0a-d25f-4e35-90cb-3f8c3853f1b6
    }
}
