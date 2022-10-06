using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEnums;

///<summary>
/// Create soil based on the input it recives
///</summary>
public class OutputMachine : Machine
{
    [Header("Output Resource")]
    public ResourceType outputType;
    protected GameObject go;
    [SerializeField] protected float resourceAmount;
    public float timeToExtract;
    protected float refTimer;

    protected Dictionary<string, int> resourcesInTheMachine = new Dictionary<string, int>();

    public override void OnResourceEnter(ResourceType type, GameObject obj)
    {
        string key = type.ToString();

        //Add object to the list of its type
        if(resourcesInTheMachine.ContainsKey(key))
            resourcesInTheMachine[key] += 1;
        //Add object to the list of its type by creating a new list if it is a new resource type
        else
        {
            int q = 1;
            resourcesInTheMachine.Add(key, q);
        }

        ObjectPool.Instance.AddToPool(key, obj.gameObject);
        obj.GetComponent<ConveyorItem>().RemoveLink();
        obj.SetActive(false);
        //resourceAmount++;
    }

    private void Update() 
    {
        refTimer -= Time.deltaTime;
        
        if(refTimer <= 0) { 
            if(!isConnected) CheckOutput();
            OutputResource(); refTimer = timeToExtract; 
        }
    }

    public virtual void OutputResource()
    {
        if(resourceAmount <= 0) return;

        if(!isConnected) CheckOutput();
        if(!isConnected) return;

        if(outputType == ResourceType.none)
        {
            Debug.Log("NO RESOURCE SELECTED");
            return;
        }
        
        switch (outputType)
        {
            case ResourceType.soil:
                go = ObjectPool.Instance.GrabFromPool(outputType.ToString(), Library.Instance.soilPrefab);
            break;
            case ResourceType.ore:
                go = ObjectPool.Instance.GrabFromPool(outputType.ToString(), Library.Instance.soilPrefab);
            break;
            case ResourceType.stone:
                go = ObjectPool.Instance.GrabFromPool(outputType.ToString(), Library.Instance.soilPrefab);
            break;
            case ResourceType.corn:
                go = ObjectPool.Instance.GrabFromPool(outputType.ToString(), Library.Instance.rawCorn);
            break;
        }
        
        go.transform.position = outputPoint.transform.position;
        go.transform.rotation = Quaternion.identity;
        go.SetActive(true);

        resourceAmount--;
    }
}