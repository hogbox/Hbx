//----------------------------------------------
//            Hbx: Ext
// Copyright © 2017-2018 Hogbox Studios
// GameObjec.cs
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hbx.Ext
{

    public static class GameObjectExt 
    {
        static Vector3 tempv3 = Vector3.one;

        //
        // Creation
        
        public static GameObject CreateWithMesh(Mesh aMesh)
        {
            GameObject go = new GameObject(!string.IsNullOrEmpty(aMesh.name) ? aMesh.name : "MeshGO");
            /*MeshRenderer mr =*/ go.AddComponent<MeshRenderer>();
            MeshFilter mf = go.AddComponent<MeshFilter>();
            mf.sharedMesh = aMesh;
            return go;
        }

        public static GameObject GetOrCreateGameObject(string aPath)
        {
            string[] els = aPath.Split('/');
            GameObject go = GameObject.Find(els[0]);
            if(go == null)
            {
                go = new GameObject(els[0]);
            }
            if(els.Length > 1)
            {
                string subpath = aPath.Remove(0, els[0].Length + 1);
                Transform result = go.transform.GetOrCreateChild(subpath);
                go = result.gameObject;
            }

            return go;
        }


        //
        // Transform

        public static void ZeroLocalTransform(this GameObject aGameObject)
        {
            aGameObject.transform.localPosition = Vector3.zero;
            aGameObject.transform.localRotation = Quaternion.identity;
            aGameObject.transform.localScale = Vector3.one;
        }
        public static void SetLocalPositionX(this GameObject aGameObject, float aX)
        {
            tempv3 = aGameObject.transform.localPosition;
            tempv3.x = aX;
            aGameObject.transform.localPosition = tempv3;
        }
        public static void SetLocalPositionY(this GameObject aGameObject, float aY)
        {
            tempv3 = aGameObject.transform.localPosition;
            tempv3.y = aY;
            aGameObject.transform.localPosition = tempv3;
        }
        public static void SetLocalPositionZ(this GameObject aGameObject, float aZ)
        {
            tempv3 = aGameObject.transform.localPosition;
            tempv3.z = aZ;
            aGameObject.transform.localPosition = tempv3;
        }
    
        public static void SetPositionX(this GameObject aGameObject, float aX)
        {
            tempv3 = aGameObject.transform.position;
            tempv3.x = aX;
            aGameObject.transform.position = tempv3;
        }
        public static void SetPositionY(this GameObject aGameObject, float aY)
        {
            tempv3 = aGameObject.transform.position;
            tempv3.y = aY;
            aGameObject.transform.position = tempv3;
        }
        public static void SetPositionZ(this GameObject aGameObject, float aZ)
        {
            tempv3 = aGameObject.transform.position;
            tempv3.z = aZ;
            aGameObject.transform.position = tempv3;
        }
    
        public static void SetLocalEulerX(this GameObject aGameObject, float aX)
        {
            tempv3 = aGameObject.transform.localEulerAngles;
            tempv3.x = aX;
            aGameObject.transform.localEulerAngles = tempv3;
        }
        public static void SetLocalEulerY(this GameObject aGameObject, float aY)
        {
            tempv3 = aGameObject.transform.localEulerAngles;
            tempv3.y = aY;
            aGameObject.transform.localEulerAngles = tempv3;
        }
        public static void SetLocalEulerZ(this GameObject aGameObject, float aZ)
        {
            tempv3 = aGameObject.transform.localEulerAngles;
            tempv3.z = aZ;
            aGameObject.transform.localEulerAngles = tempv3;
        }
    
        public static GameObject[] GetChildGameObjects(this GameObject aGameObject)
        {
            GameObject[] children = new GameObject[aGameObject.transform.childCount];
            for(int i=0;i<children.Length; i++)
                children[i] = aGameObject.transform.GetChild(i).gameObject;
            return children;
        }
      
        public static void DestroyAllChildren(this GameObject aGameObject)
        {
            foreach (GameObject aChildGameObject in aGameObject.GetChildGameObjects())
                GameObject.Destroy(aChildGameObject);
        }
        
        public static void DestroyAllChildrenImmediate(this GameObject aGameObject)
        {
            foreach (GameObject aChildGameObject in aGameObject.GetChildGameObjects())
                GameObject.DestroyImmediate(aChildGameObject);
        }
            
        public static GameObject[] GetGameObjectsWithComponent<T>(this GameObject aGameObject) where T : Component
        {
            T[] components = aGameObject.GetComponentsInChildren<T>(true);
            GameObject[] gameObjects = new GameObject[components.Length];
            for(int i=0; i<components.Length; i++)
                gameObjects[i] = components[i].gameObject;
            return gameObjects;
        }

        /// <summary>
        /// Convienience function. Set all passed gameobjects to specified layer
        /// </summary>
        /// <param name="aGameObjectArray">A game object array.</param>
        /// <param name="aLayer">A layer.</param>

        public static void SetToLayer(this GameObject[] aGameObjectArray, int aLayer)
        {
            foreach(GameObject go in aGameObjectArray)
                go.layer = aLayer;
        }

        /// <summary>
        /// Convienience function. Sets the gamobects in dictionary to use int value as their layer.
        /// The dictionary could come from a call to GetLayersForComponentsInChildren
        /// </summary>
        /// <param name="aGameObject">A game object.</param>
        /// <param name="layers">Layers.</param>

        public static void SetToLayers(this GameObject aGameObject, Dictionary<GameObject, int> layers)
        {
            foreach(KeyValuePair<GameObject, int> g in layers)
            {
                g.Key.layer = g.Value;
            }
        }


        /// <summary>
        /// Sets all the gameobjects with a T type component to use a specific layer.
        /// </summary>
        /// <param name="aGameObject">A game object.</param>
        /// <param name="aLayer">A layer.</param>

        public static void SetComponentsInChildrenToLayer<T>(this GameObject aGameObject, int aLayer) where T : Component
        {
            T[] components = aGameObject.GetComponentsInChildren<T>(true);
            foreach(T c in components)
                c.gameObject.layer = aLayer;
        }

        /// <summary>
        /// Convienience function. Sets the list of components to a specific layer.
        /// </summary>
        /// <param name="aGameObject">A game object.</param>
        /// <param name="layers">Layers.</param>

        public static void SetComponentsToLayer(this GameObject aGameObject, Component[] aComponentArray, int aLayer)
        {
            foreach(Component c in aComponentArray)
            {
                c.gameObject.layer = aLayer;
            }
        }

        /// <summary>
        /// Convienience function. Sets the components in dictionary to use int value as their layer.
        /// The dictionary could come from a call to GetComponentsInChildrenLayers
        /// </summary>
        /// <param name="aGameObject">A game object.</param>
        /// <param name="layers">Layers.</param>

        public static void SetComponentsToLayers<T>(this GameObject aGameObject, Dictionary<T, int> layers) where T : Component
        {
            foreach(KeyValuePair<T, int> c in layers)
            {
                c.Key.gameObject.layer = c.Value;
            }
        }

        public static T GetOrCreateComponent<T>(this GameObject aGameObject) where T : Component
        {
            T comp = aGameObject.GetComponent<T>();
            if(comp == null) comp = aGameObject.AddComponent<T>();
            return comp;
        }

        public static T[] GetComponentsInChildren<T>(this GameObject aGameObject, bool includeInactive, int includeLayer) where T : Component
        {
            List<T> filtered = new List<T>(); // TODO, avoid new here
            T[] components = aGameObject.GetComponentsInChildren<T>(includeInactive);
            if(components != null && components.Length > 0)
            {
                foreach(T c in components)
                {
                    if(c.gameObject.layer == includeLayer) filtered.Add(c);
                }

            }
            return filtered.ToArray();
        }

        /// <summary>
        /// Return a dictionary of all components mapped to the layer their gameobject uses
        /// </summary>
        /// <returns>The renderer layers.</returns>
        /// <param name="aGameObject">A game object.</param>

        public static Dictionary<T, int> GetLayersForComponentsInChildren<T>(this GameObject aGameObject) where T : Component
        {
            T[] components = aGameObject.GetComponentsInChildren<T>(true);
            Dictionary<T, int> layers = new Dictionary<T, int>(components.Length);
            foreach(T c in components)
                layers.Add(c, c.gameObject.layer);
            return layers;
        }

    
        public static void FlipFaces(this GameObject aGameObject)
        {
            MeshFilter[] filters = aGameObject.GetComponentsInChildren<MeshFilter>(true);
            foreach(MeshFilter filter in filters)
            {
                Mesh mesh = filter.mesh;
                int[] flipedTriangles = mesh.triangles;
                System.Array.Reverse(flipedTriangles);
                mesh.triangles = flipedTriangles;
                Vector3[] normals = mesh.normals;
                for(int i=0; i<normals.Length; i++)
                    normals[i] = -normals[i];
                mesh.normals = normals;
            }
        }

        public static void SetMaterial(this GameObject aGameObject, Material aMaterial, bool applyToChildren=true) 
        {
            Renderer[] renderers = applyToChildren ? aGameObject.GetComponentsInChildren<Renderer>(true) : new Renderer[] {aGameObject.GetComponent<Renderer>()}; //TODO, avoid calling new
            foreach(Renderer renderer in renderers) {
                renderer.material = aMaterial;
            }
        }
    
        public static void SetMaterialFloat(this GameObject aGameObject, string aName, float aValue, bool applyToChildren=true) 
        {
            Renderer[] renderers = applyToChildren ? aGameObject.GetComponentsInChildren<Renderer>(true) : new Renderer[] {aGameObject.GetComponent<Renderer>()};
            foreach(Renderer renderer in renderers) {
                foreach(Material mat in renderer.materials) {
                    mat.SetFloat(aName, aValue);
                }
            }
        }
    
        public static void SetMaterialVector2(this GameObject aGameObject, string aName, Vector2 aValue, bool applyToChildren=true) 
        {
            Renderer[] renderers = applyToChildren ? aGameObject.GetComponentsInChildren<Renderer>(true) : new Renderer[] {aGameObject.GetComponent<Renderer>()};
            foreach(Renderer renderer in renderers) {
                foreach(Material mat in renderer.materials) {
                    mat.SetVector(aName, aValue);
                }
            }
        }
    
        public static void SetMaterialVector3(this GameObject aGameObject, string aName, Vector3 aValue, bool applyToChildren=true) 
        {
            Renderer[] renderers = applyToChildren ? aGameObject.GetComponentsInChildren<Renderer>(true) : new Renderer[] {aGameObject.GetComponent<Renderer>()};
            foreach(Renderer renderer in renderers) {
                foreach(Material mat in renderer.materials) {
                    mat.SetVector(aName, aValue);
                }
            }
        }
                
        public static void SetMaterialVector4(this GameObject aGameObject, string aName, Vector4 aValue, bool applyToChildren=true) 
        {
            Renderer[] renderers = applyToChildren ? aGameObject.GetComponentsInChildren<Renderer>(true) : new Renderer[] {aGameObject.GetComponent<Renderer>()};
            foreach(Renderer renderer in renderers) {
                foreach(Material mat in renderer.materials) {
                    mat.SetVector(aName, aValue);
                }
            }
        }
    
        public static void SetMaterialMatrix(this GameObject aGameObject, string aName, Matrix4x4 aValue, bool applyToChildren=true) 
        {
            Renderer[] renderers = applyToChildren ? aGameObject.GetComponentsInChildren<Renderer>(true) : new Renderer[] {aGameObject.GetComponent<Renderer>()};
            foreach(Renderer renderer in renderers) {
                foreach(Material mat in renderer.materials) {
                    mat.SetMatrix(aName, aValue);
                }
            }
        }
    
        //
        //Bounds
    
        public static Bounds ComputeMeshBounds(this GameObject aGameObject)
        {
            Bounds totalBounds = new Bounds();
            MeshFilter[] filters = aGameObject.GetComponentsInChildren<MeshFilter>();
            foreach(MeshFilter filter in filters)
                totalBounds.Encapsulate(filter.sharedMesh.bounds);
            return totalBounds;
        }
    
        public static Bounds ComputeWorldSpaceMeshBounds(this GameObject aGameObject)
        {
            Transform t = aGameObject.transform;
    
            Bounds meshbounds = aGameObject.ComputeMeshBounds();
            BoundsExt.GetCorners(ref meshbounds, ref BoundsExt.tempCorners);
    
            Bounds bounds = new Bounds(t.TransformPoint(BoundsExt.tempCorners[0]), Vector3.zero);
            for(int i=1; i<BoundsExt.tempCorners.Length; i++)
                bounds.Encapsulate(t.TransformPoint(BoundsExt.tempCorners[i]));
    
            return bounds;
        }
        public static Bounds ComputeRendererBounds(this GameObject aGameObject)
        {
            Renderer[] renderers = aGameObject.GetComponentsInChildren<Renderer>();
            renderers.SumBounds(ref BoundsExt.tempBounds);
            return BoundsExt.tempBounds;
        }
        public static void SumBounds(this Renderer[] aRendererArray, ref Bounds refBounds)
        {
            refBounds.center = aRendererArray[0].bounds.center;
            refBounds.size = aRendererArray[0].bounds.size;
            for(int i=1; i<aRendererArray.Length; i++)
                refBounds.Encapsulate(aRendererArray[i].bounds);
        }
    
        public static void ComputeScreenSpaceRect(this GameObject aGameObject, Camera camera, ref Rect refRect)
        {
            Bounds bounds = aGameObject.ComputeRendererBounds();
            BoundsExt.ComputeScreenSpaceRect(ref bounds, camera, ref refRect);
        }
        public static void ComputeScreenSpaceRectCorners(this GameObject aGameObject, Camera aCamera, ref Vector3[] refCorners)
        {
            aGameObject.ComputeScreenSpaceRectCorners (aCamera, ref refCorners, 0);
        }
        public static void ComputeScreenSpaceRectCorners(this GameObject aGameObject, Camera aCamera, ref Vector3[] refCorners, int aStart)
        {
            Bounds bounds = aGameObject.ComputeRendererBounds();
            BoundsExt.ComputeScreenSpaceRectCorners(ref bounds, aCamera, ref refCorners, aStart);
        }
        public static void ComputeWorldSpaceScreenRectCorners(this GameObject aGameObject, Camera aCamera, ref Vector3[] refCorners) {
            aGameObject.ComputeWorldSpaceScreenRectCorners (aCamera, ref refCorners, 0);
        }
        public static void ComputeWorldSpaceScreenRectCorners(this GameObject aGameObject, Camera aCamera, ref Vector3[] refCorners, int aStart)
        {
            Bounds bounds = aGameObject.ComputeRendererBounds();
            BoundsExt.ComputeWorldSpaceScreenRectCorners(ref bounds, aCamera, ref refCorners, aStart);
        }
        public static Mesh CreateWorldSpaceQuadMeshForScreenRect(this GameObject aGameObject, Camera aCamera)
        {
            Mesh mesh = new Mesh();
            aGameObject.ComputeWorldSpaceScreenRectCorners(aCamera, ref RectExt.tempCorners);
           // mesh.BuildQuad(RectExt.tempCorners, new Rect(0f,1f,1f,1f));
            return mesh;
        }
    
    }

} // end Hbx Ext namespace
