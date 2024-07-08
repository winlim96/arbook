using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;


public class Track : MonoBehaviour
{
    public ARTrackedImageManager manager;

    // 배열과 동일
    public List<GameObject> list1 = new List<GameObject>();
    public List<AudioClip> list2 = new List<AudioClip>();


    //여러개의 정보를 저장가능 키값 / 찾는데 쉽고 빠르게 하기위한 열쇠
    private Dictionary<string, GameObject> dict1 = new Dictionary<string, GameObject>();
    //타입 오브젝트    이름

    private Dictionary<string, AudioClip> dict2 = new Dictionary<string, AudioClip>();


    // Start is called before the first frame update
    void Start()
    {   
        //      list1에서 하나 꺼내서 o에다 넣는다 // for문이 각각돈다
        foreach(GameObject o in list1)
        {
            dict1.Add(o.name, o);
        }

        foreach (AudioClip o in list2)
        {
            dict2.Add(o.name, o);
        }
    }

    
    // 시작할때 쓰는 함수
    private void OnEnable()
    {
        //이미지가 바뀔때, onchanged함수를 더해준다// 이미지가 바뀌면 곧바로 자동실행 // +=이용은 여러가지 이벤트를 실행할수있기때문 (= 이 들어가면 한번만 실행됨)
        manager.trackedImagesChanged += OnChanged;
    }

    void OnDisable()
    {
        manager.trackedImagesChanged -= OnChanged;
    }
    void UpdateImage(ARTrackedImage t)
    {
        string name = t.referenceImage.name;

        if (dict1.TryGetValue(name, out GameObject o))
        {
            if (t.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                //GameObject o = dict1[name];
                o.transform.position = t.transform.position;
                //o.transform.rotation = t.transform.rotation;

                o.SetActive(true);
            }
            else
            {
                o.SetActive(false);
            }
        }
       
    }

    void UpdateSound(ARTrackedImage t)
    {
        string name = t.referenceImage.name;

        AudioClip sound = dict2[name];
        GetComponent<AudioSource>().PlayOneShot(sound);
    }
    private void OnChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage t in args.added)
        {
            UpdateImage(t);
            UpdateSound(t);
        }

        foreach (ARTrackedImage t in args.updated)
        {
            UpdateImage(t);
        }
    }
    // Update is called once per frame
    void Update()                                                                                                                                                
    {
        
    }
}
