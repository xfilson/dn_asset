﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace XEditor
{
    public class MakeEquip : EditorWindow
    {
        public class ExtraSkinMeshTex
        {
            public Mesh mesh;
            public string name = "";
            public Texture2D tex;
        }
        public class ExtraMeshTex
        {
            public bool isSkin = false;
            public Mesh mesh;
            public string name;
            public Texture2D tex;
            public Material srcMat;
        }
        public Object model = null;
        protected Object currentModel = null;
        protected Vector2 scrollPos = Vector2.zero;
        private string srcString = "01";
        private string replaceString = "02";
        private string modelPath = "";
        protected List<ExtraSkinMeshTex> mtdList = new List<ExtraSkinMeshTex>();
        protected List<ExtraMeshTex> meshList = new List<ExtraMeshTex>();
        protected string equipPath = "Assets/Resources/Equipments/";
        protected string weaponPath = "Assets/Resources/Equipments/weapon/";
        protected virtual void OnGUI()
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(model != null ? model.name : "Empty");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            equipPath = EditorGUILayout.TextField("equip path", equipPath);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            weaponPath = EditorGUILayout.TextField("weapon path", weaponPath);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            model = EditorGUILayout.ObjectField("", model, typeof(UnityEngine.Object), true, GUILayout.MaxWidth(250));
            if (currentModel != model)
            {
                mtdList.Clear();
                meshList.Clear();
                modelPath = "";
                if (model != null)
                {
                    modelPath = AssetDatabase.GetAssetPath(model);
                    modelPath = modelPath.Replace("\\", "/");
                    int index = modelPath.LastIndexOf("/");
                    if (index >= 0)
                    {
                        modelPath = modelPath.Substring(0, index) + "/";
                    }
                    currentModel = model;
                    string rootPath = "Equipments/";
                    GameObject go = GameObject.Instantiate(model) as GameObject;
                    SkinnedMeshRenderer[] smrs = go.GetComponentsInChildren<SkinnedMeshRenderer>();
                    foreach (SkinnedMeshRenderer smr in smrs)
                    {
                        string path = rootPath + smr.sharedMesh.name;
                        if (smr.sharedMesh.name.ToLower().Contains("weapon"))
                        {
                            string meshPath = weaponPath + smr.sharedMesh.name + ".asset";
                            Mesh loadMesh = AssetDatabase.LoadAssetAtPath(meshPath, typeof(Mesh)) as Mesh;

                            ExtraMeshTex emt = new ExtraMeshTex();
                            emt.isSkin = true;
                            emt.mesh = loadMesh;
                            emt.name = "";
                            emt.tex = null;
                            emt.srcMat = smr.sharedMaterial;
                            meshList.Add(emt);
                        }
                        else
                        {
                            Mesh mesh = XResources.Load<Mesh>(path,AssetType.Asset);
                            Texture2D tex = XResources.Load<Texture2D>(path, AssetType.TGA);
                            if (mesh != null)
                            {
                                ExtraSkinMeshTex emt = new ExtraSkinMeshTex();
                                emt.mesh = mesh;
                                emt.tex = tex;
                                emt.name = "";
                                mtdList.Add(emt);
                            }
                        }
                    }

                    MeshFilter[] meshs = go.GetComponentsInChildren<MeshFilter>();
                    foreach (MeshFilter mesh in meshs)
                    {
                        MeshRenderer mr = mesh.transform.GetComponent<MeshRenderer>();
                        if (mesh.sharedMesh != null)
                        {
                            string meshPath = weaponPath + mesh.name + ".asset";
                            Mesh loadMesh = AssetDatabase.LoadAssetAtPath(meshPath, typeof(Mesh)) as Mesh;

                            ExtraMeshTex emt = new ExtraMeshTex();
                            emt.mesh = loadMesh;
                            emt.name = "";
                            emt.tex = null;
                            emt.srcMat = mr != null ? mr.sharedMaterial : null;
                            meshList.Add(emt);
                        }
                    }
                    GameObject.DestroyImmediate(go);
                }
            }
            GUILayout.EndHorizontal();

            scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.ExpandWidth(true));
            for (int i = 0, imax = mtdList.Count; i < imax; ++i)
            {
                ExtraSkinMeshTex emt = mtdList[i];
                if (emt.mesh != null)
                {
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(emt.mesh.name);
                    GUILayout.EndHorizontal();
                }

                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical();
                emt.name = EditorGUILayout.TextField("", emt.name, GUILayout.MaxWidth(300));
                GUILayout.EndVertical();

                GUILayout.BeginVertical();
                Texture2D tex = emt.tex;
                emt.tex = EditorGUILayout.ObjectField(emt.tex, typeof(Texture2D), true) as Texture2D;
                if (tex != emt.tex && emt.tex != null)
                {
                    emt.name = emt.tex.name;
                }
                GUILayout.EndVertical();
                GUILayout.BeginVertical();
                if (GUILayout.Button("X", GUILayout.MaxWidth(20)))
                {
                    emt.tex = null;
                    emt.name = "";
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            }
            for (int i = 0, imax = meshList.Count; i < imax; ++i)
            {
                ExtraMeshTex emt = meshList[i];
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(emt.mesh != null ? emt.mesh.name : "");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical();
                emt.name = EditorGUILayout.TextField("", emt.name, GUILayout.MaxWidth(300));
                GUILayout.EndVertical();

                GUILayout.BeginVertical();
                Texture2D tex = emt.tex;
                emt.tex = EditorGUILayout.ObjectField(emt.tex, typeof(Texture2D), true) as Texture2D;
                if (tex != emt.tex && emt.tex != null)
                {
                    emt.name = emt.tex.name;
                }
                GUILayout.EndVertical();
                GUILayout.BeginVertical();

                if (GUILayout.Button("X", GUILayout.MaxWidth(20)))
                {
                    emt.tex = null;
                    emt.name = "";
                }
                GUILayout.EndVertical();

                GUILayout.EndHorizontal();
            }


            GUILayout.EndScrollView();
            srcString = EditorGUILayout.TextField("Src String", srcString);
            replaceString = EditorGUILayout.TextField("Replace String", replaceString);
            if (GUILayout.Button("Auto Gen", GUILayout.ExpandWidth(false)))
            {
                for (int i = 0, imax = mtdList.Count; i < imax; ++i)
                {
                    ExtraSkinMeshTex emt = mtdList[i];
                    if (emt.mesh != null)
                    {
                        if (emt.mesh != null)
                        {
                            emt.name = emt.mesh.name.Replace(srcString, replaceString);
                        }
                        if (emt.tex != null)
                        {
                            string texName = emt.tex.name.Replace(srcString, replaceString);
                            emt.tex = AssetDatabase.LoadAssetAtPath(modelPath + texName + ".tga", typeof(Texture2D)) as Texture2D;
                        }
                    }
                }
                for (int i = 0, imax = meshList.Count; i < imax; ++i)
                {
                    ExtraMeshTex emt = meshList[i];
                    if (emt.mesh != null)
                    {
                        emt.name = emt.mesh.name.Replace(srcString, replaceString);
                    }
                    if (emt.srcMat != null)
                    {
                        Texture2D tex = emt.srcMat.mainTexture as Texture2D;
                        if (tex != null)
                        {
                            string texName = tex.name.Replace(srcString, replaceString);
                            emt.tex = AssetDatabase.LoadAssetAtPath(modelPath + texName + ".tga", typeof(Texture2D)) as Texture2D;
                        }
                    }
                }
            }
            if (GUILayout.Button("Export", GUILayout.ExpandWidth(false)))
            {
                if (model != null)
                {
                    for (int i = 0, imax = mtdList.Count; i < imax; ++i)
                    {
                        ExtraSkinMeshTex emt = mtdList[i];
                        if (emt.tex != null)
                        {
                            GameObject go = new GameObject(emt.name);
                            if (emt.mesh != null)
                            {
                                FbxEditor.CleanMesh(emt.mesh);
                                AssetDatabase.CreateAsset(emt.mesh, equipPath + emt.name + ".asset");
                                AssetDatabase.SaveAssets();
                            }
                            GameObject.DestroyImmediate(go);
                        }
                    }
                    for (int i = 0, imax = meshList.Count; i < imax; ++i)
                    {
                        ExtraMeshTex emt = meshList[i];
                        if (emt.tex != null)
                        {
                            GameObject go = new GameObject(emt.name);
                            Material mat = new Material(emt.srcMat);
                            mat.mainTexture = emt.tex;
                            mat.name = emt.name;
                            string matPath = modelPath + "Materials/" + emt.name + ".mat";
                            AssetDatabase.CreateAsset(mat, matPath);
                            AssetDatabase.SaveAssets();
                            Material newMat = AssetDatabase.LoadAssetAtPath(matPath, typeof(Material)) as Material;
                            if (emt.srcMat != null)
                            {
                                newMat.shader = emt.srcMat.shader;
                            }
                            else
                            {
                                newMat.shader = Shader.Find("Custom/Common/MobileDiffuse");
                            }

                            if (emt.isSkin)
                            {
                                SkinnedMeshRenderer smr = go.AddComponent<SkinnedMeshRenderer>();
                                smr.sharedMesh = emt.mesh;
                                smr.sharedMaterial = newMat;
                                smr.receiveShadows = false;
                                smr.lightProbeUsage = LightProbeUsage.BlendProbes;
                                smr.shadowCastingMode = ShadowCastingMode.Off;
                            }
                            else
                            {
                                MeshFilter mf = go.AddComponent<MeshFilter>();
                                mf.sharedMesh = emt.mesh;
                                MeshRenderer mr = go.AddComponent<MeshRenderer>();
                                mr.sharedMaterial = newMat;
                                mr.receiveShadows = false;
                                mr.lightProbeUsage = LightProbeUsage.BlendProbes;
                                mr.shadowCastingMode = ShadowCastingMode.Off;
                            }
                            go.layer = LayerMask.NameToLayer("Role");
                            PrefabUtility.CreatePrefab(weaponPath + emt.name + ".prefab", go, ReplacePrefabOptions.ReplaceNameBased);
                            GameObject.DestroyImmediate(go);
                        }
                    }
                }
            }
            if (GUILayout.Button("Cancel", GUILayout.ExpandWidth(false)))
            {
                Close();
            }
        }
    }

}