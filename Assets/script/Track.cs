using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;


public class Track : MonoBehaviour
{
    public ARTrackedImageManager manager;

    // �迭�� ����
    public List<GameObject> list1 = new List<GameObject>();
    public List<AudioClip> list2 = new List<AudioClip>();


    //�������� ������ ���尡�� Ű�� / ã�µ� ���� ������ �ϱ����� ����
    private Dictionary<string, GameObject> dict1 = new Dictionary<string, GameObject>();
    //Ÿ�� ������Ʈ    �̸�

    private Dictionary<string, AudioClip> dict2 = new Dictionary<string, AudioClip>();


    // Start is called before the first frame update
    void Start()
    {   
        //      list1���� �ϳ� ������ o���� �ִ´� // for���� ��������
        foreach(GameObject o in list1)
        {
            dict1.Add(o.name, o);
        }

        foreach (AudioClip o in list2)
        {
            dict2.Add(o.name, o);
        }
    }

    
    // �����Ҷ� ���� �Լ�
    private void OnEnable()
    {
        //�̹����� �ٲ�, onchanged�Լ��� �����ش�// �̹����� �ٲ�� ��ٷ� �ڵ����� // +=�̿��� �������� �̺�Ʈ�� �����Ҽ��ֱ⶧�� (= �� ���� �ѹ��� �����)
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
