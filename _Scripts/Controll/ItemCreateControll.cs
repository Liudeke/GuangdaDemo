using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using kernal;
using UnityEngine;

namespace Assets._Scripts.Controll
{
    public enum ItemCreateControllEvent
    {
        ClearYnYingItems,
        ClearJianSheItems,
        PlayYunYingItemAni,
        PlayJianSheItemAni
    }


    public class ItemCreateControll : MonoBehaviour
    {

        public static ItemCreateControll Instance;
        #region 在运营变量

        public const string shengHuiFileName = "shenghuiData";
        
        Dictionary<string, List<string>> shengDictionary = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> shengJianSheDictionary = new Dictionary<string, List<string>>();

        List<string> shengList = new List<string>();
        public List<GameObject> listObjForYunYing = new List<GameObject>();
        public List<GameObject> listObjForJianShe = new List<GameObject>();
        private int index = -1;
        private int indexJianShe = -1;

        #endregion
        void Awake()
        {
           // print(Application.persistentDataPath);
            Instance = this;
            GlobalEvent.AddEvent(YunYingItemEvent.AddObj, AddYunYingItem);
            GlobalEvent.AddEvent(YunYingItemEvent.AddObjJianShe, AddJianSheItem);
            GlobalEvent.AddEvent(ItemCreateControllEvent.ClearYnYingItems, ClearYunYinItem);
            GlobalEvent.AddEvent(ItemCreateControllEvent.ClearJianSheItems, ClearJianSheItem);
            GlobalEvent.AddEvent(ItemCreateControllEvent.PlayYunYingItemAni, PlayYunYingani_Event);
            GlobalEvent.AddEvent(ItemCreateControllEvent.PlayJianSheItemAni, PlayJianSheani_Event);
        }

        void Start()
        {
            
        }
        public void TestMothed()
        {
            GlobalEvent.DispatchEvent(ZaiYunYingWindowEvent.ShoWYunYingItem, GetShilistFromSheng("山东"));
        }
        public void TestMothed1()
        {
            GlobalEvent.DispatchEvent(ZaiYunYingWindowEvent.ShoWJianSheItem, GetShilistFromShengJianShe("山东"));
        }
        public void TestMothed2()
        {
            GlobalEvent.DispatchEvent(ZaiYunYingWindowEvent.ShoWDiQuItem, GetShengList());
            //GlobalEvent.DispatchEvent("123", GetShengList());
        }
        public void ClearObj()
        {
            GlobalEvent.DispatchEvent(ItemCreateControllEvent.ClearYnYingItems);
        }
        #region 在运营Item方法

        public List<string> GetShengList()
        {
            if (shengList.Count == 0)
            {
                GetDataFromText();
                return shengList;
            }
            return shengList;
        }

        public Dictionary<string, List<string>> GetShiList()
        {
            if (shengDictionary.Count == 0)
            {
                GetDataFromText();
                return shengDictionary;
            }
            return shengDictionary;

        }
        public Dictionary<string, List<string>> GetShiListJianShe()
        {
            if (shengJianSheDictionary.Count == 0)
            {
                GetDataFromText();
                return shengJianSheDictionary;
            }
            return shengJianSheDictionary;

        }

        public List<string> GetShilistFromSheng(string shengName)
        {
            List<string> temp = null;
            if (shengDictionary.Count == 0)
            {
                GetDataFromText();
            }
            if (shengDictionary.TryGetValue(shengName, out temp))
            {
                return temp;
            }
            else
            {
                Debug.LogWarning("集合为空，请检查！！！");
                return null;
            }

        }
        public List<string> GetShilistFromShengJianShe(string shengName)
        {
            List<string> temp = null;
            if (shengJianSheDictionary.Count == 0)
            {
                GetDataFromText();
            }
            if (shengJianSheDictionary.TryGetValue(shengName, out temp))
            {
                return temp;
            }
            else
            {
                Debug.LogWarning("集合为空，请检查！！！");
                return null;
            }

        }
        //从文本里读取数据
        void GetDataFromText()
        {
            #region MyRegion
            //if (DataManager.GetIsExistData(shengHuiFileName))
            //{

            //}
            //else
            //{
            //    Debug.LogError("找不到-->" + shengHuiFileName + "<---文件，请检查");
            //}
            #endregion

            DataTable shenghuiTable = DataManager.GetData(shengHuiFileName);
            for (int i = 0; i < shenghuiTable.TableIDs.Count; i++)
            {
                string strShenghuiName = shenghuiTable.TableIDs[i];
                string strShiName = shenghuiTable.GetLineFromKey(shenghuiTable.TableIDs[i])
                    .GetString(shenghuiTable.TableKeys[2]);
                string strShiJianSheName = shenghuiTable.GetLineFromKey(shenghuiTable.TableIDs[i])
                    .GetString(shenghuiTable.TableKeys[3]);
                //Debug.Log(strShiJianSheName+ "---strShiJianSheName");
                if (!shengList.Contains(strShenghuiName))
                {
                    shengList.Add(strShenghuiName);
                }
                // shengDictionary.Add(strShenghuiName, GetShiList(strShiName));
                List<string> templist;
                if (!shengDictionary.TryGetValue(strShenghuiName, out templist))
                {
                    shengDictionary.Add(strShenghuiName, ParseTool.String2StringArrayBaseSplitChar(strShiName, '、').ToList());
                }
                List<string> templistJianshe;
                if (!shengJianSheDictionary.TryGetValue(strShenghuiName, out templistJianshe))
                {
                    shengJianSheDictionary.Add(strShenghuiName, ParseTool.String2StringArrayBaseSplitChar(strShiJianSheName, '、').ToList());
                }
                //foreach (var VARIABLE in shengJianSheDictionary.Values)
                //{
                //    foreach (var item in VARIABLE)
                //    {
                //        Debug.Log(item + "--999999");
                //    }
                //}
            }
        }

        private void AddYunYingItem(object[] args)
        {
            GameObject go = (GameObject)args[0];
            go.SetActive(false);
            listObjForYunYing.Add(go);
        }
        private void PlayYunYingani_Event(object[] args)
        {
            index++;
            if (index >= listObjForYunYing.Count)
            {
                GlobalEvent.DispatchEvent(ZaiYunYingWindowEvent.EnableMask );
                return;
            }
            GameObject go = listObjForYunYing[index];
            go.SetActive(true);
            go.GetComponent<test>().MoveToObj();
        }
        private void ClearYunYinItem(object[] args)
        {
            if (listObjForYunYing.Count != 0)
            {
                for (int i = 0; i < listObjForYunYing.Count; i++)
                {
                    GameObjectManager.DestroyGameObjectByPool(listObjForYunYing[i]);
                }
            }
            listObjForYunYing.Clear();
            index = -1;
        }
        private void AddJianSheItem(object[] args)
        {
            GameObject go = (GameObject)args[0];
            go.SetActive(false);
            listObjForJianShe.Add(go);
        }
        private void PlayJianSheani_Event(object[] args)
        {
            indexJianShe++;
            if (indexJianShe >= listObjForJianShe.Count)
            {
                GlobalEvent.DispatchEvent(ZaiYunYingWindowEvent.EnableMask);
                return;
            }
            GameObject go = listObjForJianShe[indexJianShe];
            go.SetActive(true);
            go.GetComponent<test>().MoveToObj();
        }
        private void ClearJianSheItem(object[] args)
        {
            if (listObjForJianShe.Count != 0)
            {
                for (int i = 0; i < listObjForJianShe.Count; i++)
                {
                    GameObjectManager.DestroyGameObjectByPool(listObjForJianShe[i]);
                }
            }
            listObjForJianShe.Clear();
            indexJianShe = -1;
        }
        #endregion
    }
}