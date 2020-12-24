using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LoadBackgroundImage : MonoBehaviour
{
    [SerializeField] private GameObject backgroundScreenObject;

    [SerializeField] private TMP_Dropdown backgroundSampleImageDropdown;

    [SerializeField] private List<Sprite> backgroundSampleImageList;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangedBackgroundSampleImageDropdownValue()
    {
        SpriteRenderer sr = backgroundScreenObject.GetComponent<SpriteRenderer>();
        sr.sprite = backgroundSampleImageList[backgroundSampleImageDropdown.value];
        ChangeBackgroundImageScale();
    }

    public void ChangeBackgroundImageScale()
    {
        SpriteRenderer sr = backgroundScreenObject.GetComponent<SpriteRenderer>();

        // カメラの外枠のスケールをワールド座標系で取得
        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        // スプライトのスケールもワールド座標系で取得
        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        //  両者の比率を出してスプライトのローカル座標系に反映
        //背景の縦横比を固定する
        //横長の背景の場合
        if (worldScreenWidth / worldScreenHeight > width / height)
        {
            backgroundScreenObject.transform.localScale =
                new Vector3(worldScreenWidth / width, worldScreenWidth / width);
        }
        //縦長の背景の場合
        else
        {
            backgroundScreenObject.transform.localScale =
                new Vector3(worldScreenHeight / height, worldScreenHeight / height);
        }

        //背景の縦横比を固定しない
        // {
        //     backgroundScreenObject.transform.localScale =
        //         new Vector3(worldScreenWidth / width, worldScreenHeight / height);
        // }
    }
}