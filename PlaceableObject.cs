
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PlaceableObject : UdonSharpBehaviour
{

    public GameObject debugSphere;
    private BoxCollider objCollider;

    void Start()
    {
        objCollider = gameObject.GetComponent<BoxCollider>();
        displayCorners(objCollider);
    }

    public Vector3[] getObjectVectors() 
    {
        float posX = (objCollider.size.x) / 2;
        float posY = (objCollider.size.y) / 2;
        float posZ = (objCollider.size.z) / 2;
        Vector3[] res = new Vector3[4];
        res[0] = new Vector3(posX, -posY, posZ);
        res[1] = new Vector3(-posX, -posY, posZ);
        res[2] = new Vector3(posX, -posY, -posZ);
        res[3] = new Vector3(-posX, -posY, -posZ);
        return res;
    }

    public Vector3 fixRotation(Vector3 current) 
    {
        return new Vector3(0, (Mathf.Round(current.y / 90) * 90), 0);
    }

    bool isCoordOutOfBounds(float num, char coord) 
    {
        bool result = false;
        switch (coord) 
        {
            case 'x':
                // X cannot be negative or greater than 10
                result = (num < 0.0f || num > 10.0f);
                break;
            case 'z':
                // Z is always negative and greater than -10
                result = (num > 0.0f || num < -10.0f);
                break;
            default:
                break;
        }
        return result;
    }

    // Keeping this just in case.
    bool isVectorOutOfBounds(Vector3 pos) 
    {
        bool res = isCoordOutOfBounds(pos.x, 'x');
        if (!res) 
        {
            res = isCoordOutOfBounds(pos.z, 'z');
        }
        return res;
    }


    // DEBUG ONLY!
    void displayCenter(Vector3 center) 
    {
        GameObject centerSphere = Instantiate(debugSphere, gameObject.transform.TransformPoint(center), Quaternion.identity, gameObject.transform);
    }

    void displayCorners(BoxCollider objCollider) 
    {
        float posX = (objCollider.size.x) / 2;
        float posY = (objCollider.size.y) / 2;
        float posZ = (objCollider.size.z) / 2;

        Instantiate(debugSphere, gameObject.transform.TransformPoint(new Vector3(posX, posY, posZ)), Quaternion.identity, gameObject.transform);
        Instantiate(debugSphere, gameObject.transform.TransformPoint(new Vector3(-posX, -posY, -posZ)), Quaternion.identity, gameObject.transform);
        Instantiate(debugSphere, gameObject.transform.TransformPoint(new Vector3(-posX, posY, posZ)), Quaternion.identity, gameObject.transform);
        Instantiate(debugSphere, gameObject.transform.TransformPoint(new Vector3(posX, -posY, posZ)), Quaternion.identity, gameObject.transform);
        Instantiate(debugSphere, gameObject.transform.TransformPoint(new Vector3(posX, posY, -posZ)), Quaternion.identity, gameObject.transform);
        Instantiate(debugSphere, gameObject.transform.TransformPoint(new Vector3(-posX, -posY, posZ)), Quaternion.identity, gameObject.transform);
        Instantiate(debugSphere, gameObject.transform.TransformPoint(new Vector3(posX, -posY, -posZ)), Quaternion.identity, gameObject.transform);
        Instantiate(debugSphere, gameObject.transform.TransformPoint(new Vector3(-posX, posY, -posZ)), Quaternion.identity, gameObject.transform);
    }
}
