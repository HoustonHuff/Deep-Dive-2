using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DeepDive;

namespace DeepDive
{

    public class ScannerHud : MonoBehaviour
    {

        public int snapshots = 0;
        public string pic;
        private GameObject scanningObj = null;
        private List<GameObject> fishList = new List<GameObject>();
        private bool canScan = true;
        public GameObject scan;
        public GameObject foundFish;
        public Text numberText;
        public GameObject num;




        // Start is called before the first frame update
        void Start()
        {

            // grab all the fish objects
            Transform[] allChildren = GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                if (child.tag == "Fish")
                {
                    fishList.Add(child.gameObject);
                }
                //Debug.Log("fish count: " + fishList.Count);
            }

            // grab scanHub Game Objects
            scan = GameObject.Find("Scan HUD");
            foundFish = GameObject.Find("Found Fish");
            numberText = this.gameObject.GetComponentInChildren<Text>();
            num = GameObject.Find("Scan Text");
        }

        // Update is called once per frame
        void Update()
        {
            if (canScan&&(scan.activeSelf)) 
            { 
                scanningProcess();
                pic = snapshots.ToString();
                Debug.Log("pic is " + pic);
                numberText.text = pic;
                num.GetComponent<Text>().text = pic;
            }
            if (Input.GetKeyUp(KeyCode.J))
            {
                //showScanHud();
                //highlightAll();
                //highlight(FindClosestFish());
                //scanningProcess();

            }

            
            if (Input.GetKeyDown(KeyCode.K))
            {
                removeHighlight();
                //hideScanHud();
            }

        }
        // highlights all fish
        public void highlightAll()
        {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Fish");
            foreach (GameObject go in gos)
            {
                highlight(go);
            }
        }

        //finds closest fish used to highlight currently not working ...
        /*
        public GameObject FindClosestFish()
        {
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in fishList)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
            scanningObj = closest;
            return closest;
            Debug.Log("closest fish is : " + closest);
        }
        */

        // random fish
        public GameObject randomFish()
        {
            if (fishList.Count > 0)
            {
                int ranNum = Random.Range(0, fishList.Count);
                //Debug.Log("list size is " + fishList.Count + " ranNum is: " + ranNum);
                return (fishList[ranNum]);
            }
            else return null;
        }


        // hightlights fish
        public void highlight(GameObject gameObject)
        {
            if (gameObject != null)
            {
                var outline = gameObject.AddComponent<Outline>();
                outline.OutlineMode = Outline.Mode.OutlineAll;
                outline.OutlineColor = Color.red;
                outline.OutlineWidth = 5f;
                //Debug.Log("highlighted " + gameObject);

                canScan = false;

            }

        }

        // removes the outline 
        public void removeHighlight()
        {
            
            foreach (GameObject go in fishList)
            {
                //Debug.Log("Trying to clear " + go + " In " + fishList);
                if (go.GetComponent<Outline>() != null)
                {
                    Destroy(go.GetComponent<Outline>());
                    canScan = true;
                    snapshots += 2;
                    Debug.Log(snapshots + " amount of fish");

                }
            }
        }

        //hide scanner HUD These two methods where used during debuging and creating the scan Hud
        /*
       public void hideScanHud()
       {
           GameObject[] gos;
           gos = GameObject.FindGameObjectsWithTag("ScanHUD");
           foreach (GameObject go in gos)
           {
               go.SetActive(false); //turn off hud components
           }
       }

       //show scanner HUD
       public void showScanHud()
           {
               GameObject[] gos;
               gos = GameObject.FindGameObjectsWithTag("ScanHUD");
               foreach (GameObject go in gos)
           {
               go.SetActive(true); //turn on hud components

           }
       }
       */
        public void scanningProcess()
        {
            //highlight(FindClosestFish());
            StartCoroutine(waitForScanning());
            
        }

        public IEnumerator waitForScanning()
        {
            //Debug.Log("highlighting a fish");

            highlight(randomFish());
            yield return new WaitForSecondsRealtime(5);
            //Debug.Log("finished highlighting a fish");
            removeHighlight();
            //Debug.Log("Scanned a fish : ");
        }

    }

}

